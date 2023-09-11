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
    public class CategoryRepository : IRepository<CategoryEntity>
    {
        SqlDb connection = new SqlDb();

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


            string sql = "SELECT * FROM Categories Order By Id;";


            //正式連線
            //List<CategoryEntity> result = this.connection.GetAll<CategoryEntity>(sql, "default");

            //測試連線
            List<CategoryEntity> result = this.TestEntities;


            return result;

        }


        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public CategoryEntity Get(int Id)
        {
            string sql = "SELECT * FROM Categories WHERE Id = @Id";

            object obj = new { Id = Id };

            CategoryEntity result = this.connection.Get<CategoryEntity>(sql, "default", obj);

            return result;
        }



        /// <summary>
        /// Create Category 
        /// </summary>
        /// <param name="dto"></param>
        public void Create(CategoryEntity entity)
        {
            

            string sql = "INSERT INTO Categories(Name) VALUES (@Name)";

            object obj = new
            {
                Name = entity.Name,
            };

            this.connection.Create(sql, "default", obj);

        }


        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="dto"></param>
        public void Update(CategoryEntity entity)
        {
            

            string sql = "UPDATE　Categories SET Name = @Name WHERE Id = @Id";

            object obj = new
            {
                Id = entity.Id,
                Name = entity.Name,
            };

            this.connection.Update(sql, "default", obj);
        }


        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
          

            string sql = "DELETE Categories WHERE Id = @Id";

            object obj = new { Id = id };

            this.connection.Delete(sql, "default", obj);
        }
    }

}
