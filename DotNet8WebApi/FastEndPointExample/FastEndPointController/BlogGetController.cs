using DotNet8WebApi.EFDbContext;
using DotNet8WebApi.FastEndpointExample.RequestAndResponse.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.FastEndpointExample.BlogGetController
{
    public class MyEndpoint : EndpointWithoutRequest<MyResponse>
    {
        private readonly AppDbContext _context;

        public MyEndpoint(AppDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Get("/api/blog");
            AllowAnonymous();
        }

        public override Task HandleAsync(CancellationToken ct)
        {
            //var blog = await _context.Data.FirstOrDefaultAsync();

            //Response.Blog_Title = blog!.Blog_Title;
            //Response.Blog_Author = blog.Blog_Author;
            //Response.Blog_Content = blog.Blog_Content;

            Response = new()
            {
                Blog_Title = "john doe",
                Blog_Author = "Author",
                Blog_Content = "Content"
            };
            return Task.CompletedTask;
        }
       
    }
}
