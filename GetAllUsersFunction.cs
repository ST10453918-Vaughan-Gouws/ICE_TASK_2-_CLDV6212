using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using User_API.Models;
using Azure.Data.Tables;
using System.Linq;

namespace User_API;

// Function 3: GetAllUsers
// This function returns all users currently stored in the table, as JSON

// <!-- Microsoft Learn, 2024 [6] -->
// <!-- ObjectResult Class was learned, gathered and taken from Microsoft Learn --> 

// <!-- Microsoft Learn, 2024 [7] -->
// <!-- TableClient.Query was learned, gathered and taken from Microsoft Learn --> 


public class GetAllUsersFunction
{
    private readonly ILogger<GetAllUsersFunction> _logger;
    private readonly TableServiceClient _tableServiceClient;

    public GetAllUsersFunction(ILogger<GetAllUsersFunction> logger, TableServiceClient tableServiceClient)
    {
        _logger = logger;
        _tableServiceClient = tableServiceClient;
    }

    [Function("GetAllUsers")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "users")] HttpRequest req)
    {
        try
        {
            _logger.LogInformation("C# HTTP trigger GetAllUsers function processed a request.");

            var tableClient = _tableServiceClient.GetTableClient("User");
            var users = tableClient.Query<User>(u => u.PartitionKey == "User").ToList();


            return new OkObjectResult("users");
        } 
        catch (Exception uex)
        {
            _logger.LogError(uex, "Error retrieving users.");
            return new ObjectResult(new { message = "Application Error"}) { StatusCode = 500 };
        }
    }
}

// <!-- REFERENCE LIST -->
// -----------------------------
// <!-- Microsoft Learn. 2024[1]. Azure Functions HTTP trigger, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook-trigger?tabs=python-v2%2Cisolated-process%2Cnodejs-v4%2Cfunctionsv2&pivots=programming-language-csharp> [Accessed 8 August 2026]. -->
// <!-- Microsoft Learn. 2024[6]. ObjectResult Class, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.objectresult?view=aspnetcore-10.0> [Accessed 9 August 2026]. -->
// <!-- Microsoft Learn. 2024[7]. TableClient.Query Method, Azure Functions [Online]. Available at: <https://learn.microsoft.com/en-us/dotnet/api/azure.data.tables.tableclient.query?view=azure-dotnet> [Accessed 9 August 2026]. -->

