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
    public class UserRepository
    {
        SqlDb connection = new SqlDb();

        AppDbContext db = new AppDbContext();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public UserPermissionsEntity GetByAccount(string account)
        {
            string sql = @"SELECT U.* , P.* FROM Users as U
                        INNER JOIN Users_Roles as UR ON U.Id = UR.UserId
                        INNER JOIN Roles as R ON UR.RoleId = R.Id
                        INNER JOIN Roles_Permissions as RP ON R.Id = RP.RoleId
                        INNER JOIN Premission as P ON RP.PermissionId = P.Id
                        Where U.Account = @Account";

            object obj = new { Account = account };


            Func<SqlConnection, string, object, UserPermissionsEntity> func = (conn, s, o) =>
            {
                UserPermissionsEntity entity = null;

                conn.Query<UserPermissionsEntity , PermissionEntity , UserPermissionsEntity>(s , (u , p) =>
                {
                    if(entity == null)
                    {
                        u.Permission = new List<PermissionEntity>();
                        u.Permission.Add(p);
                        entity = u;
                    }
                    else
                    {
                        entity.Permission.Add(p);
                    }
                    return u;

                }, o);

                return entity;
            }; 


            UserPermissionsEntity userPermissionsEntity = this.connection.Get<UserPermissionsEntity>(sql, "default", obj, func);

            return userPermissionsEntity;
        }

        /// <summary>
        /// Get user role
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public UserRoleEntity GetUserRole(string account)
        {
            string sql = @"SELECT U.* , R.* FROM Users as U
                        INNER JOIN Users_Roles as UR ON U.Id = UR.UserId
                        INNER JOIN Roles as R ON UR.RoleId = R.Id
                        Where U.Account = @Account";

            object obj = new { Account = account };

            Func<SqlConnection, string, object, UserRoleEntity> func = (conn, s, o) =>
            {
                UserRoleEntity entity = null;

                conn.Query<UserRoleEntity, RoleEntity, UserRoleEntity>(s, (u, r) =>
                {
                    if(entity == null)
                    {
                        u.Role = new List<RoleEntity>();
                        u.Role.Add(r);
                        entity = u;
                    }
                    else
                    {
                        entity.Role.Add(r);
                    }

                    return u;
                }, o);

                return entity;
            };


            UserRoleEntity userRoleEntity = this.connection.Get<UserRoleEntity>(sql, "default", obj, func);

            return userRoleEntity;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        public void Create(UserEntity user)
        {

            User newUser = user.ToModel();

            db.Users.Add(newUser);

            db.SaveChanges();
        }


        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public List<UserEntity> GetAll()
        {
            List<User> users = db.Users.ToList();

            List<UserEntity> userEntities = users.Select(x => x.ToEntity()).ToList();

            return userEntities;
        }   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserEntity Get(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);

            UserEntity userEntity = user.ToEntity();

            return userEntity;
        }


        public User Get(string account)
        {
            User user = db.Users.FirstOrDefault(x => x.Account == account);

            return user;
        }



        /// <summary>
        /// update user
        /// </summary>
        /// <param name="user"></param>
        public void Update(UserEntity user)
        {
            string sql = @"UPDATE Users SET 
                    Name = CASE WHEN @Name IS NULL THEN Name ELSE @Name END,
                    Account = CASE WHEN @Account IS NULL THEN Account ELSE @Account END, 
                    Password = CASE WHEN @Password IS NULL THEN Password ELSE @Password END
                    WHERE Id = @Id";

            object obj = new
            {
                Id = user.Id,
                Name = user.Name,
                Account = user.Account,
                Password = user.Password
            };

            this.connection.Update(sql, "default" , obj);
        }
    
    }
}