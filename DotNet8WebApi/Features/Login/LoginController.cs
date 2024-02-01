using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNet8WebApi.Features.Login
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginDataModel loginRequestModel)
        {
            try
            {
                if (string.IsNullOrEmpty(loginRequestModel.UserName) || string.IsNullOrEmpty(loginRequestModel.Password))
                    return BadRequest("Username or Password not specified");

                if (loginRequestModel.UserName.Equals("mack") && loginRequestModel.Password.Equals("mack1234"))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SU57Ie4vseXyJeUUSL6y8Z1QMFRMb2ZN"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: "https://localhost:7091",
                        audience: "https://localhost:7091",
                        claims: new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, loginRequestModel.UserName),
                            GetRoleClaim(loginRequestModel.UserName),
                        },
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred in generating the token" + ex.ToString());
            }

            return Unauthorized();
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
