using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using SampleMvc.Data.Entity;
using SampleMvc.Web.Controllers;

namespace SampleMvc.Web.Utilities
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EnsureAuthenticatedAttribute : ActionFilterAttribute
    {
        private bool _unauthorized;
        //private readonly IHttpContextAccessor _accessor;

        public EnsureAuthenticatedAttribute() {}

        //public EnsureAuthenticatedAttribute(IHttpContextAccessor accessor)
        //{
        //    _accessor = accessor;
        //}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            var isAuthenticated = user.Identity?.IsAuthenticated;

            var isAdmin = user.Claims.Any(c =>
                c.Type == ClaimTypes.Role &&
                c.Value == Role.Admin.ToString());

            if (isAuthenticated == true && isAdmin) return;

            _unauthorized = true;
            filterContext.Result = new UnauthorizedResult();
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (_unauthorized)
            {
                filterContext.Result = new RedirectToActionResult(nameof(AuthController.Login), "Auth", null);
            }
        }
    }
}
