using NUlid;

namespace DotNet8WebApi.Features.Blogs
{
    public class BlogFakerModel
    {
        public string? Blog_Id { get; set; } = Ulid.NewUlid().ToString();
        public string? Blog_Title { get; set; }
        public string? Blog_Author { get; set; }
        public string? Blog_Content { get; set; }
    }

    public class BlogFakerListModel
    {
        public string? Blog_Id { get; set; } = Ulid.NewUlid().ToString();
        public string? Blog_Title { get; set; }
        public string? Blog_Author { get; set; }
        public string? Blog_Content { get; set; }
    }
}
