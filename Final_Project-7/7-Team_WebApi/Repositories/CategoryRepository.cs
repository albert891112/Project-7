using _7_Team_WebApi.Models.DTOs;
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
    public class CategoryRepository
    {
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

            List<CategoryEntity> result = connection.GetAll<List<CategoryEntity>>(sql, "default", func);

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
        public void Update(CategoryDTO dto)
        {
            SqlDb connection = new SqlDb();

            string sql = "UPDATE　Categories SET Name = @Name WHERE Id = @Id";

            object obj = new
            {
                Id = dto.Id,
                Name = dto.Name,
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
