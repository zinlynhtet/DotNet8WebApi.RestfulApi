using DotNet8WebApi.EFDbContext;
using DotNet8WebApi.FastEndpointExample.RequestAndResponse.Request;
using DotNet8WebApi.FastEndpointExample.RequestAndResponse.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.FastEndpointExample.BlogDeleteController
{
    public class MyEndpoint : Endpoint<BlogDeleteRequestModel, BlogResponseModel>
    {
        private readonly AppDbContext _context;

        public MyEndpoint(AppDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Post("/api/blog/");
            AllowAnonymous();
        }

        public override async Task HandleAsync(BlogDeleteRequestModel reqModel, CancellationToken ct)
        {
            var item = await _context.Data.FirstOrDefaultAsync(x => x.Blog_Id == reqModel.Id);
            _context.Data.Remove(item);
            var result = await _context.SaveChangesAsync();
            await SendAsync(new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Success." : "Failed."
            });
        }
    }
}