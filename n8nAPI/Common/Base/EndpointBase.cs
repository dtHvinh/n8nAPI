namespace n8nAPI.Common.Base;

public abstract class EndpointBase
{
    protected RouteGroupBuilder Group { get; set; } = default!;

    public abstract IEndpointRouteBuilder RegisterEndpoint(IEndpointRouteBuilder endpointRouteBuilder);

    public EndpointBase WithGroup(RouteGroupBuilder group)
    {
        Group = group;
        return this;
    }

    /// <summary>
    /// Allow anonymous access to this endpoint
    /// </summary>
    /// <returns>Self</returns>
    public EndpointBase AllowAnonymous()
    {
        Group.AllowAnonymous();
        return this;
    }

    public EndpointBase MapGet(string pattern, Delegate handler, Action<EndpointMetadata> metadataConfig)
    {
        EndpointMetadata metadata = new();
        metadataConfig(metadata);

        metadata.WithMetadata(Group.MapGet(pattern, handler));

        return this;
    }

    public class EndpointMetadata
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Type ProducesType { get; set; } = typeof(void);

        public RouteHandlerBuilder WithMetadata(RouteHandlerBuilder builder)
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
                    genericMethod.Invoke(null, new object[] { builder });
                }
            }

            return builder;
        }
    }
}
