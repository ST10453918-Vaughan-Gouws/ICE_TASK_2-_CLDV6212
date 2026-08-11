using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace User_API;

public class GetAllUsersFunction
{
    private readonly ILogger<GetAllUsersFunction> _logger;

    public GetAllUsersFunction(ILogger<GetAllUsersFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetAllUsers")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}