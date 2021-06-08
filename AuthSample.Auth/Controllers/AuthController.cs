using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthSample.Auth.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthSample.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity;

            var user = model;
            var secret = Encoding.UTF8.GetBytes("SuperDuperSecretKey");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                notBefore: now,
                expires: now.AddMinutes(30),
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256));

            var stringToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            return Ok(stringToken);
        }
    }
}
