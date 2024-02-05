using DotNet8WebApi.Features.Blog;

namespace DotNet8WebApi.FastEndpointExample.RequestAndResponse.Response
{
    public class BlogResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<BlogDataModel> Data { get; set; }
    }

    public class BlogViewResponseModel
    {
        public string? Blog_Title { get; set; }
        public string? Blog_Author { get; set; }
        public string? Blog_Content { get; set; }
    }
}