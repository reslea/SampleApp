using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SampleMvc.Data;
using SampleMvc.Data.Entity;
using SampleMvc.Web.Models;

namespace SampleMvc.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly LibraryContext _context;

        public AuthController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }

            var toCreate = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Phone = model.Phone
            };

            await _context.Users.AddAsync(toCreate);
            await _context.SaveChangesAsync();

            var principal = GetClaimsPrincipal(toCreate);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction(nameof(BookController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var existingUser = _context.Users
                .FirstOrDefault(u => u.Email == model.Email && 
                                     u.Password == model.Password);

            if (existingUser is null)
            {
                return View();
            }

            var principal = GetClaimsPrincipal(existingUser);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction(nameof(BookController.Index), "Home");
        }

        public ClaimsPrincipal GetClaimsPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
