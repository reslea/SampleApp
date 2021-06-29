using System.Collections.Generic;
using AuthSample.Core;

namespace AuthSample.AuthDb.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public RoleType RoleType { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}