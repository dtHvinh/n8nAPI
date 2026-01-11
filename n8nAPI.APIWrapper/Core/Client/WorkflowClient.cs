using n8nAPI.APIWrapper.Core.Pagniation;
using n8nAPI.APIWrapper.Models;
using System.Net.Http.Json;

namespace n8nAPI.APIWrapper.Core.Client;

public class WorkflowClient(IHttpClientFactory hca) : N8NClient(hca)
{
    public async Task<PageResponse<Workflow>> GetWorkflowsAsync(CancellationToken cancellationToken = default)
    {
        var path = new Builder.QueryPathBuilder()
            .Start()
            .WithCategory(Enums.QueryCategory.Workflow)
            .BuildPath();

        HttpRequestMessage request = new(HttpMethod.Get, path);
        HttpResponseMessage response = await Client.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        PageResponse<Workflow> workflows = await response.Content.ReadFromJsonAsync<PageResponse<Workflow>>(cancellationToken);

        return workflows;
    }
}
