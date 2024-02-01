using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNet8WebApi.Features.Login
{
    [Route("api/[controller]")]
    public class LoginController(IConfiguration configuration) : Controller
    {
        private readonly IConfiguration _configuration = configuration;

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginDataModel loginRequestModel)
        {
            try
            {
                if (string.IsNullOrEmpty(loginRequestModel.UserName) || string.IsNullOrEmpty(loginRequestModel.Password))
                    return BadRequest("Username or Password is wrong.");

                if (loginRequestModel.UserName.Equals("mack") && !loginRequestModel.Password.Equals("mack1234"))
                    return BadRequest("Username or Password is wrong.");

                return Ok(GenerateToken(loginRequestModel.UserName));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred in generating the token" + ex.ToString());
            }
        }

        [HttpGet("{userName}")]
        public string GenerateToken(string userName)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt:Issuer").Value!,
                audience: _configuration.GetSection("Jwt:Audience").Value!,
                claims: new List<Claim>
                {
                            new Claim(ClaimTypes.Name, userName),
                            GetRoleClaim(userName),
                },
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signInCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private Claim GetRoleClaim(string userName)
        {
            var roles = DetermineRoles(userName);
            var identity = new ClaimsIdentity();
            identity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return identity.Claims.First();
        }

        private IEnumerable<string> DetermineRoles(string username)
        {
            return new List<string> { username == "mack" ? "Admin" : "User" };
        }
    }
}
