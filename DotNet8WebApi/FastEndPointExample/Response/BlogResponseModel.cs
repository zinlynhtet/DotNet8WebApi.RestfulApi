namespace DotNet8WebApi.FastEndpointExample.RequestAndResponse.Response
{
    public class BlogResponseModel
    {
        public string BlogData { get; set; }
        public string BlogAuthor { get; set; }
    }
    public class MyResponse
    {
        public string? Blog_Title { get; set; }
        public string? Blog_Author { get; set; }
        public string? Blog_Content { get; set; }
    }
}
