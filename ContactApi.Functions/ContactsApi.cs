using ContactApi.Functions.Models;
using ContactApi.Functions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace ContactApi.Functions;

public class ContactsApi
{
    private readonly ILogger<ContactsApi> _logger;
    private readonly ContactRepository _repo;

    public ContactsApi(ILogger<ContactsApi> logger, ContactRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [Function("GetAllContacts")]
    [OpenApiOperation("GetAllContacts", "contacts")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(List<Contact>))]
    public async Task<HttpResponseData> GetAll(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contacts")] HttpRequestData req)
    {
        var contacts = await _repo.GetAllAsync();
        var res = req.CreateResponse(HttpStatusCode.OK);
        await res.WriteAsJsonAsync(contacts);
        return res;
    }

    [Function("GetContactById")]
    [OpenApiOperation("GetContactById", "contacts", Summary = "Get contact by ID")]
    [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Contact))]
    [OpenApiResponseWithBody(HttpStatusCode.NotFound, "application/json", typeof(object))]
    public async Task<HttpResponseData> GetById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contacts/{id:int}")] HttpRequestData req,
        [FromRoute] int id)
    {
        var contact = await _repo.GetByIdAsync(id);
        var res = req.CreateResponse(contact is null ? HttpStatusCode.NotFound : HttpStatusCode.OK);
        if (contact is null)
        {
            await res.WriteAsJsonAsync(new { message = "Contact not found" });
        }
        else
        {
            await res.WriteAsJsonAsync(contact);
        }
        return res;
    }


    [Function("AddContact")]
    [OpenApiOperation("AddContact", "contacts")]
    [OpenApiRequestBody("application/json", typeof(CreateContactDto))]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(object))]
    public async Task<HttpResponseData> Add(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "contacts")] HttpRequestData req)
    {
        var dto = await req.ReadFromJsonAsync<CreateContactDto>();
        var id = await _repo.CreateAsync(new Contact(0, dto!.Name, dto.Email));
        var res = req.CreateResponse(HttpStatusCode.Created);
        await res.WriteAsJsonAsync(new { id });
        return res;
    }

    [Function("UpdateContact")]
    [OpenApiOperation("UpdateContact", "contacts")]
    [OpenApiParameter("id", In = ParameterLocation.Path, Required = true)]
    [OpenApiRequestBody("application/json", typeof(CreateContactDto))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(object))]
    public async Task<HttpResponseData> Update(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "contacts/{id:int}")] HttpRequestData req, int id)
    {
        var dto = await req.ReadFromJsonAsync<CreateContactDto>();
        var updated = await _repo.UpdateAsync(new Contact(id, dto!.Name, dto.Email));
        var res = req.CreateResponse(updated ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        await res.WriteAsJsonAsync(new { updated });
        return res;
    }

    [Function("DeleteContact")]
    [OpenApiOperation("DeleteContact", "contacts")]
    [OpenApiParameter("id", In = ParameterLocation.Path, Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(object))]
    public async Task<HttpResponseData> Delete(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "contacts/{id:int}")] HttpRequestData req, int id)
    {
        var deleted = await _repo.DeleteAsync(id);
        var res = req.CreateResponse(deleted ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        await res.WriteAsJsonAsync(new { deleted });
        return res;
    }
}
