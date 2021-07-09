using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthSample.AuthDb.Entities;
using AuthSample.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthSample.Web.Tests
{
    public class ApiTestBase
    {
        protected readonly HttpClient _client;
        private const string SecretKey = "testSecretTestSecretTestSecretTestSecretTestSecret";

        public ApiTestBase()
        {
            var hostBuilder = new WebHostBuilder()
                .UseEnvironment("Test")
                .ConfigureAppConfiguration(_ => _.AddJsonFile("appsettings.json"))
                .UseSetting("jwtSecret", SecretKey)
                .UseStartup<Startup>();

            var server = new TestServer(hostBuilder);

            _client = server.CreateClient();
        }

        protected string GetUserToken(User user)
        {
            var secret = Encoding.UTF8.GetBytes(SecretKey);

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
                expires: now.AddMinutes(10),
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256));

            var stringToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            return stringToken;
        }

        protected User GenerateUserWithPermissions(IEnumerable<PermissionType> permissions)
        {
            return new User
            {
                Id = 1,
                Email = "testemail@gmail.com",
                Name = "testname",
                Password = "test",
                Role = new Role
                {
                    RolePermissions = permissions.Select(p => new RolePermission
                    {
                        PermissionType = p
                    }).ToList(),
                }
            };
        }

        protected void AddTokenWithPermissions(IEnumerable<PermissionType> permissions)
        {
            var user = GenerateUserWithPermissions(permissions);
            var token = GetUserToken(user);
            
            _client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
