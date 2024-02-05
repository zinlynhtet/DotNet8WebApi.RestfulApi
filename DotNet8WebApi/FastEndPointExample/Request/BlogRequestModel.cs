namespace DotNet8WebApi.FastEndpointExample.RequestAndResponse.Request
{
    public class BlogRequestModel
    {
        public string Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
    }

    public class BlogIdRequestModel
    {
        public string Id { get; set; }
    }
}
