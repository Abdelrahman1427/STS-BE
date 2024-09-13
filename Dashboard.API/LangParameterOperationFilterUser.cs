using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace STS.API
{
    public class LangParameterOperationFilterUser : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Lang",
                In = ParameterLocation.Header,
                Description = "Language",
                Required = false,

            });

        }
    }
}
