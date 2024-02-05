using DotNet8WebApi.FastEndpointExample.RequestAndResponse.Request;
using DotNet8WebApi.FastEndpointExample.RequestAndResponse.Response;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.FastEndpointExample.BlogPostControllers
{
    public class MyEndpoint : Endpoint<BlogRequestModel, BlogResponseModel>
    {
        public override void Configure()
        {
            Post("/api/user/create");
            AllowAnonymous();
        }
        public override async Task HandleAsync(BlogRequestModel reqModel, CancellationToken ct)
        {
            await SendAsync(new()
            {
                
            });
        }
    }

}
