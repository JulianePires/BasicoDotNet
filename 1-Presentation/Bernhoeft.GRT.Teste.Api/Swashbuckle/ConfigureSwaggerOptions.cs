using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bernhoeft.GRT.Teste.Api.Swashbuckle
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider) => _apiVersionDescriptionProvider = apiVersionDescriptionProvider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                {
                    Title = description.GroupName.Substring(0, description.GroupName.IndexOf(" API")),
                    Version = description.GroupName,
                    Description = description.IsDeprecated ? "This version of the API has been deprecated." : ""
                });
            }

            // Adiciona suporte para [FromHeader], [FromQuery], [FromRoute], [FromBody], etc.
            options.DescribeAllParametersInCamelCase();
            options.CustomOperationIds(apiDesc =>
                $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}_{apiDesc.ActionDescriptor.RouteValues["action"]}");
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Bernhoeft.GRT.Teste.Api.xml"), true);
        }
    }
}