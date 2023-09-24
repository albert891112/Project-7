using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using Albert.Lib;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Repositories
{
    public class RoleRepository
    {
        SqlDb connection = new SqlDb();

        AppDbContext db = new AppDbContext();

        /// <summary>
        /// Get all users by role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Roles_PermissionsEntity> GetPermissionByRoleId(int id)
        {
            string sql = @"SELECT R.* , P.* FROM  Premission as p 
                        LEFT OUTER JOIN 
                        (SELECT * FROM Roles_Permissions  where Roles_Permissions.RoleId = @RoleId) as RP 
                        on RP.PermissionId = P.Id
                        LEFT OUTER JOIN Roles as R ON RP.RoleId = R.Id";

            object obj = new { RoleId = id };


            Func<SqlConnection, string, object, List<Roles_PermissionsEntity>> func = (conn, s, o) =>
            {
                return conn.Query<Roles_PermissionsEntity, PermissionEntity, Roles_PermissionsEntity>(s, (rp, p) =>
                {
                    rp.Permissions = p;
                   return rp;

                }, o).ToList();
            };


            List<Roles_PermissionsEntity> roles_Permissions = this.connection.Get<List<Roles_PermissionsEntity>>(sql, "default" , obj, func);


            return roles_Permissions;
        }


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