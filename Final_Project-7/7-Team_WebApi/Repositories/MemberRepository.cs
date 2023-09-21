
using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Dapper;
using _7_Team_WebApi.Models.Entities;
using System.Diagnostics.Contracts;
using static Dapper.SqlMapper;
using System.Security.Principal;

namespace _7_Team_WebApi.Repositories
{
    public class MemberRepository : IRepository<MemberEntity>
    {
        SqlDb connection = new SqlDb();


        /// <summary>
        /// Get all members data
        /// </summary>
        /// <returns></returns>
        public  List<MemberEntity> GetAll()
        {
            string sql = "SELECT * FROM Members Order By Id";

            List<MemberEntity> entities = connection.GetAll<MemberEntity>(sql ,"default");

            return entities;
        }


        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MemberEntity Get(int id)
        {
            string sql = "SELECT * FROM Members WHERE Id = @Id";

            object obj = new { Id = id };   

            MemberEntity entity = connection.Get<MemberEntity>(sql, "default", obj);

            return entity;
        }

        /// <summary>
        /// Create new member
        /// </summary>
        /// <param name="entity"></param>
        public void Create(MemberEntity entity)
        {
            string sql = "INSERT INTO Members (Account, FristName, LastName, Email, Password, Enable) VALUES (@Account, @FristName, @LastName, @Email, @Password, @Enable)";

            object obj = new 
            { 
                Account= entity.Account,
                FristName = entity.FirstName, 
                LastName = entity.LastName, 
                Email = entity.Email, 
                Password = entity.Password,
                Enable = entity.Enable
            };

            connection.Create(sql, "default", obj);
        }

        /// <summary>
        /// Update member
        /// </summary>
        /// <param name="entity"></param>
        public void Update(MemberEntity entity)
        {
            string sql = "UPDATE Members SET Enable = @Enable WHERE Id = @Id";

            object obj = new
            {
                Id= entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = entity.Password,
                Enable = entity.Enable
            };

            connection.Update(sql, "default", obj);
        }

        /// <summary>
        /// Delete member
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            string sql = "DELETE FROM Members WHERE Id = @Id";

            object obj = new { Id = id };

            connection.Delete(sql, "default", obj);
        }
    }
}