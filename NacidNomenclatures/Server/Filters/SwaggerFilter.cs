using Microsoft.OpenApi.Models;
using Models.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Server.Filters
{
    public class SwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var description in context.ApiDescriptions)
            {
                description.TryGetMethodInfo(out var info);
                var showSwaggerAttribute = info.GetCustomAttributes(typeof(ShowSwagger), false);

                if (!showSwaggerAttribute.Any())
                {
                    var searchPath = $"/{description.RelativePath.ToLower()}";
                    var removeRoutes = swaggerDoc.Paths.Where(e => e.Key.ToLower() == searchPath).ToList();
                    removeRoutes.ForEach(e => { swaggerDoc.Paths.Remove(e.Key); });
                }
            }
        }
    }
}
