using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using n8nAPI.MinimalAPI.Interfaces;

namespace n8nAPI.MinimalAPI.Base;

public class EndpointMetadataEnricher : IEndpointEnricher
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Type ProducesType { get; set; } = typeof(void);
    public string[] Tags { get; set; } = default!;

    public IEndpointConventionBuilder Enrich(IEndpointConventionBuilder builder)
    {
        if (!string.IsNullOrEmpty(Name))
        {
            builder.WithName(Name);
        }
        if (!string.IsNullOrEmpty(Description))
        {
            builder.WithDescription(Description);
        }
        if (ProducesType != typeof(void))
        {
            var method = typeof(OpenApiRouteHandlerBuilderExtensions).GetMethods()
                .FirstOrDefault(m => m.Name == "Produces" && m.IsGenericMethodDefinition && m.GetParameters().Length == 1);
            if (method != null)
            {
                var genericMethod = method.MakeGenericMethod(ProducesType);
                genericMethod.Invoke(null, [builder]);
            }
        }

        if (Tags != null && Tags.Length > 0)
        {
            builder.WithTags(Tags);
        }

        return builder;
    }
}
