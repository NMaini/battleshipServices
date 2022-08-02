using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace battleshipServices.Filters;

/// <summary>
/// Operations filter used to add Custom Header to http request.
/// </summary>
public class OperationFilter : IOperationFilter
{
    /// <summary>
    /// Adds Custom Header to http request in SwaggerUI
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "gameId",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema() { Type = "string" },
            Required = false
        });
    }
}



