using DotNet8WebApi.EFDbContext;
using DotNet8WebApi.FastEndpointExample.RequestAndResponse.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.FastEndpointExample.BlogGetController
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
            Get("/api/blogList");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var lst = _context.Data.ToList();
            await SendAsync(new()
            {
                IsSuccess = lst!= null,
                Message = "Success",
                Data = lst
            });
        }
    }
}