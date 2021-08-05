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
using AuthSample.AuthDb;
using AuthSample.AuthDb.Entities;
using AuthSample.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthSample.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public AuthController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users
                .Include(u => u.RefreshToken)
                .Include(u => u.Role)
                .ThenInclude(r => r.RolePermissions)
                .FirstOrDefaultAsync(u => 
                    u.Email == model.Email && u.Password == model.Password);

            if (user is null)
            {
                return BadRequest();
            }

            var stringToken = GetUserToken(user);
            
            return Ok(stringToken);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Login(RefreshTokenModel model)
        {
            var user = (await _context.RefreshTokens
                .Include(_ => _.User)
                .ThenInclude(_ => _.Role)
                .ThenInclude(_ => _.RolePermissions)
                .FirstOrDefaultAsync(_ => _.Value == model.RefreshToken)
                )?.User;

            if (user is null)
            {
                return BadRequest();
            }

            var tokenModel = GetUserToken(user);

            return Ok(tokenModel);
        }

        private TokenModel GetUserToken(User user)
        {
            var secret = Encoding.UTF8.GetBytes("SuperDuperSecretKey");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var permissions = user.Role.RolePermissions
                .Select(rp => 
                    new Claim("permission", rp.PermissionType.ToString()));

            claims.AddRange(permissions);

            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                notBefore: now,
                expires: now.AddHours(10),
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256));

            var stringToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            var refreshToken = Guid.NewGuid();
            if (user.RefreshToken is null)
            {
                user.RefreshToken = new RefreshToken { Value = refreshToken };
            }
            else
            {
                user.RefreshToken.Value = refreshToken;
            }

            _context.SaveChanges();

            return new TokenModel {JwtToken = stringToken, RefreshToken = refreshToken };
        }
    }
}
