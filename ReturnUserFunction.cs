using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using User_API.Models;

namespace User_API;

// Function 2: ReturnUser
// This function accepts a user id and returns that user's info as JSON, if they exist

// <!-- Microsoft Learn, 2024 [4] -->
// <!-- The tableClient.GetEntityIfExists was learned, gathered and taken from Microsoft Learn -->

// <!-- Microsoft Learn, 2024 [5] -->
// <!-- The NotFoundObjectResult class was learned, gathered and taken from Microsoft Learn -->

public class ReturnUserFunction
{
    private readonly ILogger<ReturnUserFunction> _logger;
    private readonly TableServiceClient _tableServiceClient;

    public ReturnUserFunction(ILogger<ReturnUserFunction> logger, TableServiceClient tableServiceClient)
    {
        _logger = logger;
        _tableServiceClient = tableServiceClient;
    }

    [Function("ReturnUser")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "users/id")] HttpRequest req, string id )
    {
        try
        {
            _logger.LogInformation("C# HTTP trigger ReturnUser function processed a request for id {id}.", id);

            var tableClient = _tableServiceClient.GetTableClient("User");
            var response = tableClient.GetEntityIfExists<User>("User", id);

            if (!response.HasValue)
            {
                return new NotFoundObjectResult(new { message = $"No user found with id '{id}'." });
            }

            return new OkObjectResult("response.Value");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user {id}.", id);
            return new ObjectResult(new {message = "Application Error" }) { StatusCode  = 500 } ;
        }
    }
}

// <!-- REFERENCE LIST -->
// -----------------------------
// <!-- Microsoft Learn. 2024[1]. Azure Functions HTTP trigger, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook-trigger?tabs=python-v2%2Cisolated-process%2Cnodejs-v4%2Cfunctionsv2&pivots=programming-language-csharp> [Accessed 8 August 2026]. -->
// <!-- Microsoft Learn. 2024[4]. TableClient.GetEntityIfExist<T> Method, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/azure.data.tables.tableclient.getentityifexists?view=azure-dotnet> [Accessed 9 August 2026]. -->
// <!-- Microsoft Learn. 2024[5]. NotFoundObjectResult Class, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.notfoundobjectresult?view=aspnetcore-10.0> [Accessed 9 August 2026]. -->
// <!-- Microsoft Learn. 2024[6]. ObjectResult Class, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.objectresult?view=aspnetcore-10.0> [Accessed 9 August 2026]. -->
