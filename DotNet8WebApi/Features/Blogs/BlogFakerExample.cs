using Bogus;
using DotNet8WebApi.Models;

namespace DotNet8WebApi.Features.Blogs
{
    public class BlogFakerExample  : Faker<BlogFakerModel>
    {
        public BlogFakerExample()
        {
            RuleFor(o => o.Blog_Title, f => f.Lorem.Sentence(10));
            RuleFor(o => o.Blog_Author, f => f.Lorem.Word());
            RuleFor(o => o.Blog_Content, f => f.Lorem.Word());
        }
    }
}
