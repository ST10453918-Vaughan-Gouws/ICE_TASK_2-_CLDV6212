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

// <-- Microsoft Learn, 2024 [a] --> 
// <-- Local settings file for Azure Functions is used to code and test the Azure Functions Locally via the connection string -->
var connectionString = builder.Configuration.GetValue<string>("AzureWebJobsStorage");
builder.Services.AddSingleton(new TableServiceClient(connectionString));

if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")))
{
    // <!-- Microsoft Learn, 2024[b] -->
    // <!-- OpenTelemetry configuration was gathered, taken and used from Microsoft Learn --> 
    builder.Services.AddOpenTelemetry()
        .UseFunctionsWorkerDefaults()
        .UseAzureMonitorExporter();
}

builder.Build().Run();



// <!-- REFERENCE LIST -->
// -------------------------
// <!-- Microsoft Learn. 2024[a]. Code and Test Azure Functions Locally, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-local?pivots=programming-language-csharp#local-settings-file> [Accessed 8 August 2026]. -->
// <!-- Microsoft Learn. 2024[b]. Configure Azure OpenTelemetry, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/azure/azure-monitor/app/opentelemetry-configuration?tabs=aspnetcore> [Accessed 8 August 2026]. -->
// <!-- Microsoft Learn. 2024[c]. ITableEntity Interface, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/azure.data.tables.itableentity?view=azure-dotnet> [Accessed 8 August 2026]. -->
// <!-- Microsoft Learn. 2024[d]. .NET observability with OpenTelemetry, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/core/diagnostics/observability-with-otel> [Accessed 8 August 2026]. -->
// <!-- Microsoft Learn. 2024[1]. Azure Functions HTTP trigger, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook-trigger?tabs=python-v2%2Cisolated-process%2Cnodejs-v4%2Cfunctionsv2&pivots=programming-language-csharp> [Accessed 8 August 2026]. -->
// <!-- Microsoft Learn. 2024[2]. Azure Functions Overview, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/azure/azure-functions/functions-overview>  [Accessed 8 August 2026]. -->
// <!-- Microsoft Learn. 2024[3]. Guid.NewGuid Method, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/system.guid.newguid?view=net-10.0> [Accessed 8 August 2026]. -->
// <!-- Microsoft Learn. 2024[4]. TableClient.GetEntityIfExist<T> Method, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/azure.data.tables.tableclient.getentityifexists?view=azure-dotnet> [Accessed 9 August 2026]. -->
// <!-- Microsoft Learn. 2024[5]. NotFoundObjectResult Class, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.notfoundobjectresult?view=aspnetcore-10.0> [Accessed 9 August 2026]. -->
// <!-- Microsoft Learn. 2024[6]. ObjectResult Class, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.objectresult?view=aspnetcore-10.0> [Accessed 9 August 2026]. -->
// <!-- Microsoft Learn. 2024[7]. TableClient.Query Method, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/azure.data.tables.tableclient.query?view=azure-dotnet> [Accessed 9 August 2026]. -->



