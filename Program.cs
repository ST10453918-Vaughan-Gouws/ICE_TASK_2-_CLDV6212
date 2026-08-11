using Azure.Data.Tables;
using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// <-- Microsoft Learn, 2024 --> 
// <-- Local settings file for Azure Functions is used to code and test the Azure Functions Locally via the connection string -->
var connectionString = builder.Configuration.GetValue<string>("AzureWebJobsStorage");
builder.Services.AddSingleton(new TableServiceClient(connectionString));

if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")))
{
    builder.Services.AddOpenTelemetry()
        .UseFunctionsWorkerDefaults()
        .UseAzureMonitorExporter();
}

builder.Build().Run();



// <!-- REFERENCE LIST -->
// -------------------------
// <!-- Microsoft Learn. 2024[a]. Code and Test Azure Functions Locally, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-local?pivots=programming-language-csharp#local-settings-file> [Accessed 8 August 2026]. -->
// <!-- Microsoft Learn. 2024[b]. OpenTelemetry, Azure Functions [Online]. Available at:  [Accessed 8 August 2026]. -->

