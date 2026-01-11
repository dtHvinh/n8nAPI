using n8nAPI.APIWrapper.Common.Constants;
using n8nAPI.APIWrapper.Common.Interfaces;

namespace n8nAPI.APIWrapper.Core.Client;

public class N8NClient(IHttpClientFactory clientFactory) : IN8nClient
{
    public HttpClient Client { get; init; } = clientFactory.CreateClient(Names.N8nHttpClientName);
}
