using DotNet8WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNet8WebApi.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost, Route("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO _auth)
        {
            try
            {
                if (string.IsNullOrEmpty(_auth.UserName) || string.IsNullOrEmpty(_auth.Password))
                    return BadRequest("Username and/or Password not specified");

                if (_auth.UserName.Equals("mack") && _auth.Password.Equals("mack1234"))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SU57Ie4vseXyJeUUSL6y8Z1QMFRMb2ZN"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: "https://localhost:7091",
                        audience: "https://localhost:7091",
                        claims: new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, _auth.UserName),
                            GetRoleClaim(_auth.UserName),
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
        private Claim GetRoleClaim(string username)
        {
            var roles = DetermineRoles(username);
            var identity = new ClaimsIdentity();
            identity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return identity.Claims.First();
        }
        private IEnumerable<string> DetermineRoles(string username)
        {
            if (username == "mack")
            {
                return new List<string> { "Admin" };
            }
            else
            {
                return new List<string> { "User" };
            }
        }
    }
}
