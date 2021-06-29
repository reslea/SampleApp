using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthSample.Core;
using Microsoft.AspNetCore.Mvc;

namespace AuthSample.Web.Utilities
{
    public class RequirePermissionAttribute : TypeFilterAttribute
    {
        public RequirePermissionAttribute(PermissionType permissionType) : base(typeof(RequireClaimFilter))
        {
            Arguments = new object[] { new Claim("permission", permissionType.ToString()), };
        }
    }
}
