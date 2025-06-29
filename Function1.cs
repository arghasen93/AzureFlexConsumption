using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FlexConsumptionFunction;

public class Function1(ILogger<Function1> logger, HttpClient httpClient)
{
    private readonly ILogger<Function1> _logger = logger;

    [Function("Function1")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        var response = await httpClient.GetAsync("https://test-func-aqcdfab6beaecaej.centralindia-01.azurewebsites.net/api/Function1");
        if(response.IsSuccessStatusCode)
        {
            var resbonseBody = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(resbonseBody);
        }
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new ObjectResult("Failure")
        {
            StatusCode = (int?)response.StatusCode
        };
    }
}