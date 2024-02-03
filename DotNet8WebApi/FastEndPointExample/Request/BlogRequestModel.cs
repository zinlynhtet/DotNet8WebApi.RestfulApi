namespace DotNet8WebApi.FastEndpointExample.RequestAndResponse.Request
{
    public class BlogRequestModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
    }
}
