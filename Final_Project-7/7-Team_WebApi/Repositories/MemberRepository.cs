
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
            string sql = "SELECT * FROM Member Order By Id";


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
            string sql = "SELECT * FROM Member WHERE Id = @Id";

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
            string sql = "INSERT INTO Member (FristName, LastName, Email, Password, AccountStatus) VALUES (@FristName, @LastName, @Email, @Password, @AccountStatus)";

            object obj = new 
            { 
                FristName = entity.FristName, 
                LastName = entity.LastName, 
                Email = entity.Email, 
                Password = entity.Password,
                AccountStatus = entity.AccountStatus
            };

            connection.Create(sql, "default", obj);
        }

        /// <summary>
        /// Update member
        /// </summary>
        /// <param name="entity"></param>
        public void Update(MemberEntity entity)
        {
            string sql = "UPDATE Member SET FristName = @FristName, LastName = @LastName, Email = @Email, Password = @Password, AccountStatus = @AccountStatus WHERE Id = @Id";

            object obj = new
            {
                FristName = entity.FristName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = entity.Password,
                AccountStatus = entity.AccountStatus
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
            string sql = "DELETE FROM Member WHERE Id = @Id";

            object obj = new { Id = id };

            connection.Delete(sql, "default", obj);
        }
    }
}