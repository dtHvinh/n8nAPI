using n8nAPI.APIWrapper.Common.Extensions;
using n8nAPI.APIWrapper.Core.Enums;
using System.Text;

namespace n8nAPI.APIWrapper.Core.Builder;

public sealed class QueryPathBuilder
{
    private readonly QueryPathBuilderStarter starter;
    private readonly QueryPathBuilderSegment segmentBuilder;
    private readonly StringBuilder sb;

    public QueryPathBuilder()
    {
        sb = new();
        segmentBuilder = new QueryPathBuilderSegment(this);
        starter = new QueryPathBuilderStarter(segmentBuilder);
    }

    public QueryPathBuilderStarter Start()
    {
        return starter;
    }

    public string BuildPath()
    {
        return sb.Append(starter.GetPath()).Append('/').Append(segmentBuilder.GetSegmentPath()).ToString();
    }

    public sealed class QueryPathBuilderStarter(QueryPathBuilderSegment segment)
    {
        private QueryCategory _category;
        private readonly QueryPathBuilderSegment _segment = segment;
        public int Version { get; set; } = 1;

        public QueryPathBuilderStarter WithApiVersion(int version)
        {
            Version = version;
            return this;
        }

        public QueryPathBuilderSegment WithCategory(QueryCategory category)
        {
            _category = category;
            return _segment;
        }

        public string GetPath()
        {
            return $"api/v{Version}/{_category.ToQueryPath()}";
        }
    }

    public sealed class QueryPathBuilderSegment(QueryPathBuilder parent)
    {
        private readonly StringBuilder _str = new();
        private readonly Dictionary<string, string> _queryParams = [];
        private readonly QueryPathBuilder _parent = parent;

        public QueryPathBuilderSegment AddSegment(string segment)
        {
            _str.Append($"{(_str.Length > 0 ? "/" : "")}{segment}");
            return this;
        }

        public QueryPathBuilderSegment AddQueryParam(string key, string value)
        {
            _queryParams[key] = value;
            return this;
        }

        public string GetSegmentPath()
        {
            if (_queryParams.Count == 0)
            {
                return _str.ToString();
            }
            StringBuilder queryStr = new(_str.ToString());
            queryStr.Append('?');
            foreach (var (key, value) in _queryParams)
            {
                queryStr.Append($"{key}={value}&");
            }
            queryStr.Length--;
            return queryStr.ToString();
        }

        public string BuildPath()
        {
            return _parent.BuildPath();
        }
    }
}
