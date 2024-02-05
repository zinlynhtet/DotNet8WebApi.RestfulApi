using DotNet8WebApi.EFDbContext;
using DotNet8WebApi.FastEndpointExample.RequestAndResponse.Request;
using DotNet8WebApi.FastEndpointExample.RequestAndResponse.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DotNet8WebApi.FastEndpointExample.BlogDeleteController
{
    public class MyEndpoint : Endpoint<BlogIdRequestModel,BlogResponseModel>
    {
        private readonly AppDbContext _context;

        public MyEndpoint(AppDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Delete("/api/blog/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(BlogIdRequestModel reqId, CancellationToken ct)
        {
            var item = await _context.Data.FindAsync(reqId);
            // _context.Remove(item);
            await SendAsync(new BlogResponseModel()
            {
               IsSuccess = item != null,
            });
        }
    }
}