using n8nAPI.APIWrapper.Core.Enums;

namespace n8nAPI.APIWrapper.Common.Extensions;

public static class QueryCategoryExtensions
{
    public static string ToQueryPath(this QueryCategory category)
    {
        return category switch
        {
            QueryCategory.Workflow => "workflows",
            QueryCategory.Execution => "executions",
            QueryCategory.User => "users",
            QueryCategory.Tags => "tags",
            QueryCategory.Credential => "credentials",
            QueryCategory.SourceControl => "source-control",
            QueryCategory.Variables => "variables",
            QueryCategory.Projects => "projects",
            QueryCategory.Audit => "audit",
            _ => throw new InvalidOperationException($"The query category '{category}' is not implemented."),
        };
    }
}