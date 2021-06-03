using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SampleAPI.Data.Repositories;
using SampleApi.Web.Models;
using SampleApi.Web.Utilities;

namespace SampleApi.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repository;

        public AuthController(IConfiguration configuration, IUnitOfWork unitOfWork, IUserRepository repository)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = _repository.Get(model.Email, model.Password);

            var securityKey = _configuration["SecurityKey"];
            var keyBytes = Encoding.UTF8.GetBytes(securityKey);

            var now = DateTime.Now;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, existingUser.Email),
            };

            const string issuer = "OurAsp.Net API";
            var jwt = new JwtSecurityToken(issuer, null, claims, now, now.AddMinutes(5), new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(token);
        }
    }
}
