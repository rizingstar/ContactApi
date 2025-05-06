using System.Net;
using ContactApi.Functions.Models;
using ContactApi.Functions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ContactApi.Functions;

public class ContactsApi
{
    private readonly ContactRepository _repo;
    private readonly ILogger<ContactsApi> _log;

    public ContactsApi(ContactRepository repo, ILogger<ContactsApi> log)
    {
        _repo = repo;
        _log = log;
    }

    // GET /api/contacts
    [Function("GetContacts")]
    public async Task<HttpResponseData> GetContacts(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contacts")]
        HttpRequestData req)
    {
        var contacts = await _repo.ListAsync();

        var res = req.CreateResponse(HttpStatusCode.OK);
        await res.WriteAsJsonAsync(contacts);
        return res;
    }

    // POST /api/contacts  { "name": "...", "email": "..." }
    [Function("AddContact")]
    public async Task<HttpResponseData> AddContact(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "contacts")]
        HttpRequestData req)
    {
        var dto = await req.ReadFromJsonAsync<Contact>();

        if (dto is null || string.IsNullOrWhiteSpace(dto.Name) ||
            string.IsNullOrWhiteSpace(dto.Email))
        {
            var bad = req.CreateResponse(HttpStatusCode.BadRequest);
            await bad.WriteStringAsync("Must supply name and email.");
            return bad;
        }

        var id = await _repo.CreateAsync(dto);

        var res = req.CreateResponse(HttpStatusCode.Created);
        await res.WriteAsJsonAsync(new { id });
        return res;
    }
}
