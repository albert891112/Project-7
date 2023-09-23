using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Repositories
{
    public class PermissionRepository
    {
        SqlDb connection = new SqlDb();

        AppDbContext db = new AppDbContext();

        /// <summary>
        /// Create a new permission
        /// </summary>
        /// <param name="permission"></param>
        public void Create(PermissionEntity permission)
        {
            Premission newPermission = permission.ToModel();
            
            db.Premissions.Add(newPermission);
        }
    }
}