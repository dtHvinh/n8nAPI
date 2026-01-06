using Microsoft.Extensions.DependencyInjection;
using n8nAPI.APIWrapper.Common.Constants;
using n8nAPI.APIWrapper.Core.Configuration;

namespace n8nAPI.APIWrapper.Common.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddN8nClient(this IServiceCollection services, Action<ConnectionConfiguration> configuration)
    {
        ConnectionConfiguration config = new();
        configuration.Invoke(config);

        services.AddHttpClient(Name.N8nHttpClientName, cf =>
        {
            cf.BaseAddress = new Uri(config.BaseUrl);
            cf.DefaultRequestHeaders.Add(Headers.XN8nApiKey, config.ApiKey);
        });

        return services;
    }
}
