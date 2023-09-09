using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using Albert.Lib;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace _7_Team_WebApi.Repositories
{
    public class CategoryRepository
    {
        

        #region 測試連線
        /// <summary>
        /// 讀取測試json資料
        /// </summary>
        List<CategoryEntity> TestEntities = new List<CategoryEntity>();

        public CategoryRepository()
        {
            string filename =Path.Combine( AppDomain.CurrentDomain.BaseDirectory , @".\TestData\Categories.json");
           

            string jsonString = File.ReadAllText(filename);
            List<CategoryEntity> result = JsonSerializer.Deserialize<List<CategoryEntity>>(jsonString);
            this.TestEntities = result;
        }
        #endregion 測試連線



        /// <summary>
		/// get all Categories table data
		/// </summary>
		/// <returns></returns>
		public List<CategoryEntity> GetAll()
        {
            SqlDb connection = new SqlDb();

            string sql = "SELECT * FROM Categories Order By Id;";

            Func<SqlConnection, string, List<CategoryEntity>> func = (conn, s) =>
            {

                return conn.Query<CategoryEntity>(sql).ToList();

            };


            //正式連線
            //List<CategoryEntity> result = this.connection.GetAll<List<CategoryEntity>>(sql, "default", func);

            //測試連線
            List<CategoryEntity> result = this.TestEntities;


            return result;

        }



        /// <summary>
        /// Create Category 
        /// </summary>
        /// <param name="dto"></param>
        public void Create(CategoryEntity entity)
        {
            SqlDb connection = new SqlDb();

            string sql = "INSERT INTO Categories(Name) VALUES (@Name)";

            object obj = new
            {
                Name = entity.Name,
            };

            connection.Create(sql, "default", obj);

        }


        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="dto"></param>
        public void Update(CategoryEntity entity)
        {
            SqlDb connection = new SqlDb();

            string sql = "UPDATE　Categories SET Name = @Name WHERE Id = @Id";

            object obj = new
            {
                Id = entity.Id,
                Name = entity.Name,
            };

            connection.Update(sql, "default", obj);
        }


        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            SqlDb connection = new SqlDb();

            string sql = "DELETE Categories WHERE Id = @Id";

            object obj = new { Id = id };

            connection.Delete(sql, "default", obj);
        }
    }

}
