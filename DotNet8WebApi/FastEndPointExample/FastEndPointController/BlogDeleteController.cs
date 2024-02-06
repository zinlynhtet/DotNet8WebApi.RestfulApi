using DotNet8WebApi.EFDbContext;
using DotNet8WebApi.FastEndpointExample.RequestAndResponse.Request;
using DotNet8WebApi.FastEndpointExample.RequestAndResponse.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.FastEndpointExample.BlogDeleteController
{
    public class MyEndpoint : EndpointWithoutRequest<BlogResponseModel>
    {
        private readonly AppDbContext _context;

        public MyEndpoint(AppDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Delete("/api/blog/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            string id = Route<string>("id")!;
            //string id = HttpContext.Request.RouteValues.GetValueOrDefault("id")!.ToString()!;
            var item = await _context.Data.FirstOrDefaultAsync(x => x.Blog_Id == id);
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