using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddSingleton(x =>
            new QueueClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "demo-message-queue",
            new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            }));


    })
    .Build();

host.Run();