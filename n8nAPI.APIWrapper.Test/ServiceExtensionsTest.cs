using Microsoft.Extensions.DependencyInjection;
using n8nAPI.APIWrapper.Common.Constants;
using n8nAPI.APIWrapper.Common.Extensions;

namespace n8nAPI.APIWrapper.Test;

public class ServiceExtensionsTest
{
    [Theory]
    [InlineData("http://localhost:1234/", "abc")]
    [InlineData("http://localhost:5678/", "cdf")]
    [InlineData("http://localhost:3210/", "ghi")]
    public void AddN8nClient_ShouldSuccess(string baseUrl, string apiKey)
    {
        ServiceCollection services = new();

        services.AddN8nClient(cf =>
        {
            cf.BaseUrl = baseUrl;
            cf.ApiKey = apiKey;
        });

        Assert.NotNull(services.FirstOrDefault(e => e.ServiceType == typeof(IHttpClientFactory)));

        IHttpClientFactory factory = services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();
        HttpClient client = factory.CreateClient(Name.N8nHttpClientName);

        Assert.Equal(baseUrl, client.BaseAddress?.ToString());
        Assert.True(client.DefaultRequestHeaders.Contains(Headers.XN8nApiKey));
        Assert.Equal(client.DefaultRequestHeaders.GetValues(Headers.XN8nApiKey).First(), apiKey);
    }
}
