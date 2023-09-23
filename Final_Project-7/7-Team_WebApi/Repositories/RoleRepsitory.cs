﻿using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Repositories
{
    public class RoleRepsitory
    {
        SqlDb connection = new SqlDb();

        AppDbContext db = new AppDbContext();


        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="role"></param>
        public void Create(RoleEntity role)
        {
            Role newRole = role.ToModel();

            db.Roles.Add(newRole);

            db.SaveChanges();
        }
    }
}