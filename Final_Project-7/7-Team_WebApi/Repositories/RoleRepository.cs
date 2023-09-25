using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.Entities.PermissionControll;
using Albert.Lib;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

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
        ///  Add permission to role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionId"></param>
        public void AddPermissionToRole(RoleUpdateEntity entity)
        {
            string sql = @"INSERT INTO Roles_Permissions (RoleId, PermissionId) VALUES (@RoleId, @PermissionId)";

            object obj = new { RoleId = entity.RoleId, PermissionId = entity.UpdateId };

            this.connection.Update(sql, "default", obj);
        }

        /// <summary>
        /// Delete permission from role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionId"></param>
        public void DeletePermissionFromRole(RoleUpdateEntity entity)
        {
            string sql = @"DELETE FROM Roles_Permissions WHERE RoleId = @RoleId AND PermissionId = @PermissionId";

            object obj = new { RoleId = entity.RoleId, PermissionId = entity.UpdateId };

            this.connection.Update(sql, "default", obj);
        }

        /// <summary>
        /// Add user to role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        public void AddUserToRole(RoleUpdateEntity entity)
        {
            string sql = @"INSERT INTO Users_Roles (RoleId, UserId) VALUES (@RoleId, @UserId)";

            object obj = new { RoleId = entity.RoleId, UserId = entity.UpdateId };

            this.connection.Update(sql, "default", obj);
        }

        /// <summary>
        /// Delete user from role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        public void DeleteUserFromRole(RoleUpdateEntity entity)
        {
            string sql = @"DELETE FROM Users_Roles WHERE RoleId = @RoleId AND UserId = @UserId";

            object obj = new { RoleId = entity.RoleId, UserId = entity.UpdateId };

            this.connection.Update(sql, "default", obj);
        }

        /// <summary>
        /// Get User by role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Roles_UsersEntity> GetUserByRoleId(int id)
        {
            string sql = @"SELECT R.* , U.* FROM Users as U 
                        LEFT OUTER JOIN 
                        (SELECT * FROM Users_Roles WHERE Users_Roles.RoleId = @RoleId) as UR 
                        ON UR.UserId = U.Id
                        LEFT OUTER JOIN Roles as R ON R.Id = UR.RoleId";

            object obj = new { RoleId = id };

            Func<SqlConnection , string , object , List<Roles_UsersEntity>> func = (conn , s , o) =>
            {
                return conn.Query<Roles_UsersEntity , UserEntity , Roles_UsersEntity>(s , (ru , u) =>
                {
                    ru.User = u;
                    return ru;

                } , o).ToList();
            };


            List<Roles_UsersEntity> roles_Users = this.connection.Get<List<Roles_UsersEntity>>(sql , "default" , obj , func);

            return roles_Users;
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

            List<Role> rolesModel = db.Roles.ToList();

            List<RoleEntity> roles = rolesModel.Select(r => r.ToEntity()).ToList();

            return roles;
        }

        /// <summary>
        ///  Get role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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