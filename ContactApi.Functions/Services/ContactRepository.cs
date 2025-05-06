using Microsoft.Data.SqlClient;
using ContactApi.Functions.Models;
using Microsoft.Extensions.Configuration;

namespace ContactApi.Functions.Services;

public class ContactRepository
{
    private readonly string _conn;

    public ContactRepository(IConfiguration cfg)
    {
        _conn = cfg["SqlConnection"] ?? throw new InvalidOperationException(
            "SqlConnection setting is missing.");
    }

    public async Task<IEnumerable<Contact>> ListAsync()
    {
        var list = new List<Contact>();
        await using var conn = new SqlConnection(_conn);
        await conn.OpenAsync();
        var cmd = new SqlCommand("SELECT Id, Name, Email FROM Contacts;", conn);
        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
            list.Add(new Contact(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2)));

        return list;
    }

    public async Task<int> CreateAsync(Contact c)
    {
        const string sql = @"INSERT INTO Contacts(Name,Email)
                             OUTPUT INSERTED.Id VALUES(@n,@e)";
        await using var conn = new SqlConnection(_conn);
        await conn.OpenAsync();
        var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@n", c.Name);
        cmd.Parameters.AddWithValue("@e", c.Email);
        return (int)await cmd.ExecuteScalarAsync();
    }
}
