using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;

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

            if (string.IsNullOrEmpty(token))
            {
                goto result;
            }

            token = token.Substring("Bearer ".Length);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken is null)
            {
                Console.WriteLine("Unable to decode the JWT token.");
                goto result;
            }

            Console.WriteLine("Decrypted Token Claims:");
            var lst = jsonToken.Claims.Select(claim => new
            {
                Type = claim.Type,
                Value = claim.Value,
                Issuer = claim.Issuer
            }).ToList();
            Console.WriteLine(JsonConvert.SerializeObject(lst, Formatting.Indented));

            result:
            await _next(context);
        }
    }

    public static class JwtAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtDecryptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtAuthorizationMiddleware>();
        }
    }
}
