using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace _7_Team_WebApi.Models.Entities
{
    public class Roles_PermissionsEntity
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public PermissionEntity Permissions { get; set; }

    }
}