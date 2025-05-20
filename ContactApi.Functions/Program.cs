using Azure.Identity;
using ContactApi.Functions.Services; // <-- add this using!
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; // <-- add this using!
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration((context, config) =>
    {
        var settings = config.Build();
        var endpoint = settings["AppConfigEndpoint"];
        if (!string.IsNullOrEmpty(endpoint))
        {
            config.AddAzureAppConfiguration(options =>
            {
                options.Connect(new Uri(endpoint), new DefaultAzureCredential())
                       .ConfigureKeyVault(kv => kv.SetCredential(new DefaultAzureCredential()));
            });
        }
    })
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddScoped<ContactRepository>(); // <-- this is required for DI!
    })
    .Build();

host.Run();
