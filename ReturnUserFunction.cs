using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace User_API;

public class ReturnUserFunction
{
    private readonly ILogger<ReturnUserFunction> _logger;

    public ReturnUserFunction(ILogger<ReturnUserFunction> logger)
    {
        _logger = logger;
    }

    [Function("ReturnUser")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}