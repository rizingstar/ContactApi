using Azure.Identity;
using Microsoft.Extensions.Configuration;
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
    .Build();

host.Run();
