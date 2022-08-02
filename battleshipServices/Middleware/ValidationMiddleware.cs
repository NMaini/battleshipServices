using System.Globalization;
using battleshipServices.Services;
using Microsoft.Extensions.Primitives;

namespace battleshipServices.Middleware;

/// <summary>
/// Middleware for verifying the Header "gameId" is available. 
/// </summary>
public class ValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly BattleshipSingleton _battleship;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next">Next middleware delegate</param>
    /// <param name="battleship">singleton service used to manage state.</param>
    public ValidationMiddleware(RequestDelegate next, BattleshipSingleton battleship)
    {
        _next = next;
        _battleship = battleship;
    }

    /// <summary>
    /// Called by framework. when the request reaches this middleware in pipeline.
    /// </summary>
    /// <param name="context">httpContext</param>
    /// <returns>a Task</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.HasValue == true && context.Request.Path.Value.Contains("board", StringComparison.InvariantCultureIgnoreCase))
        {
            await _next(context);
        }
        else
        {
            StringValues stringValues;
            if (context.Request.Headers.TryGetValue("gameId", out stringValues))
            {
                var gameIdString = stringValues.FirstOrDefault() ?? string.Empty;
                int gameId = int.MinValue;
                if (int.TryParse(gameIdString, out gameId))
                {
                    if (_battleship.Boards.ContainsKey(gameId))
                    {
                        await _next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}

/// <summary>
/// Extention method. helps in registering middleware 
/// </summary>
public static class ValidationMiddlewareExtensions
{
    /// <summary>
    /// Extension method.
    /// </summary>
    /// <param name="builder">Application Builder</param>
    /// <returns></returns>
    public static IApplicationBuilder UseValidationMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ValidationMiddleware>();
    }
}
