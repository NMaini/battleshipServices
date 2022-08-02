using Newtonsoft.Json;
using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;


namespace battleshipServices.Tests;

public class BoardControllerTests
{
    [Fact]
    public async Task TestGoodFlow()
    {
        //Add a Board
        var lambdaFunction = new LambdaEntryPoint();

        var requestStr = File.ReadAllText("./SampleRequests/BoardController-Post.json");
        var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
        
        var context = new TestLambdaContext();
        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.Equal(201, response.StatusCode);
        Assert.Equal("1", response.Body);
        Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
        Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);

        
        //Add a Ship
        requestStr = File.ReadAllText("./SampleRequests/ShipController-Post.json");

        request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);

        response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.Equal(200, response.StatusCode);
        Assert.Equal("Ship Added", response.Body);
        Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
        Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);

        //Attack
        requestStr = File.ReadAllText("./SampleRequests/ShipController-Delete.json");
        request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(requestStr);
        response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.Equal(200, response.StatusCode);
        Assert.Equal("true", response.Body);
        Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
        Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
    }
}