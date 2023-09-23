using _7_Team_WebApi.Models.EFModels;
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

        /// <summary>
        /// Get all role 
        /// </summary>
        /// <returns></returns>
        public List<RoleEntity> GetAll()
        {
            List<RoleEntity> roles = db.Roles.Select(r => r.ToEntity()).ToList();

            return roles;
        }

        //Get role by id
        public RoleEntity GetById(int id)
        {
            RoleEntity role = db.Roles.FirstOrDefault(r => r.Id == id).ToEntity();

            return role;
        }


        /// <summary>
        /// update role
        /// </summary>
        /// <param name="role"></param>
        public void Update(RoleEntity role)
        {
            Role newRole = role.ToModel();

            Role existRole = db.Roles.FirstOrDefault(r => r.Id == role.Id);

            newRole.Id = existRole.Id;

            db.Entry(existRole).CurrentValues.SetValues(newRole);

            db.SaveChanges();

        }
         

        /// <summary>
        /// delete role
        /// </summary>
        /// <param name="role"></param>
        public void Delete(RoleEntity role)
        {
            Role newRole = role.ToModel();

            db.Roles.Remove(newRole);

            db.SaveChanges();
        }

         

    }
}