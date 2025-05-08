using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ContactApi.Functions.Models;

namespace ContactApi.Functions.Services;

public class ContactRepository
{
    private readonly IConfiguration _config;
    public ContactRepository(IConfiguration config) => _config = config;

    private string ConnStr => _config["SqlConnection"];

    public async Task<int> CreateAsync(Contact contact)
    {
        using var conn = new SqlConnection(ConnStr);
        await conn.OpenAsync();
        var cmd = new SqlCommand(@"
            INSERT INTO Contacts (Name, Email)
            OUTPUT INSERTED.Id
            VALUES (@Name, @Email)", conn);
        cmd.Parameters.AddWithValue("@Name", contact.Name);
        cmd.Parameters.AddWithValue("@Email", contact.Email);
        return (int)await cmd.ExecuteScalarAsync();
    }

    public async Task<List<Contact>> GetAllAsync()
    {
        var list = new List<Contact>();
        using var conn = new SqlConnection(ConnStr);
        await conn.OpenAsync();
        var cmd = new SqlCommand("SELECT Id, Name, Email FROM Contacts", conn);
        using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
            list.Add(new Contact(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2)));
        return list;
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(ConnStr);
        await conn.OpenAsync();
        var cmd = new SqlCommand("SELECT Id, Name, Email FROM Contacts WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        using var rdr = await cmd.ExecuteReaderAsync();
        return await rdr.ReadAsync()
            ? new Contact(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2))
            : null;
    }

    public async Task<bool> UpdateAsync(Contact contact)
    {
        using var conn = new SqlConnection(ConnStr);
        await conn.OpenAsync();
        var cmd = new SqlCommand(@"
            UPDATE Contacts SET Name = @Name, Email = @Email WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", contact.Id);
        cmd.Parameters.AddWithValue("@Name", contact.Name);
        cmd.Parameters.AddWithValue("@Email", contact.Email);
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var conn = new SqlConnection(ConnStr);
        await conn.OpenAsync();
        var cmd = new SqlCommand("DELETE FROM Contacts WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        return await cmd.ExecuteNonQueryAsync() > 0;
    }
}
