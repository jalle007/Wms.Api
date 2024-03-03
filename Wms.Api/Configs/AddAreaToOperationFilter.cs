using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Wms.Api.Configs
{
    public class AddAreaToOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var area = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                .OfType<AreaAttribute>()
                .FirstOrDefault();

            if (area != null)
            {
                operation.Tags = new List<OpenApiTag> { new OpenApiTag { Name = area.RouteValue } };
            }
        }
    }


}
