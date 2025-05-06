using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ContactApi.Functions.Services;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;   


var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureOpenApi() // 👈 This is what makes Swagger work!
    .ConfigureServices(s =>
    {
        s.AddSingleton<ContactRepository>();
    })
    .Build();

