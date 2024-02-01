using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DotNet8WebApi.Middlewares
{
    public class JwtAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    Console.WriteLine("Decrypted Token Claims:");
                    foreach (var claim in jsonToken.Claims)
                    {
                        Console.WriteLine($"{claim.Type}: {claim.Value} : {claim.Issuer}");
                    }
                }
                else
                {
                    Console.WriteLine("Unable to decode the JWT token.");
                }
            }

            await _next(context);
        }
    }

    public static class JwtDecryptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtDecryptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtAuthorizationMiddleware>();
        }
    }
}
