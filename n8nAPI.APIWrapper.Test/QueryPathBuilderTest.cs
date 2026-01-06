using n8nAPI.APIWrapper.Core.Builder;

namespace n8nAPI.APIWrapper.Test;

public class QueryPathBuilderTest
{
    [Fact]
    public void QueryTest()
    {
        QueryPathBuilder builder = new();

        string path = builder.Start()
            .WithCategory(Core.Enums.QueryCategory.Workflow)
            .AddSegment("activityId")
            .AddQueryParam("limit", "10")
            .AddQueryParam("offset", "20")
            .BuildPath();

        Assert.Equal("workflows/activityId?limit=10&offset=20", path);
    }
}
