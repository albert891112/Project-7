using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
using Albert.Lib;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;



namespace _7_Team_WebApi.Repositories
{
    public class CategoryRepository : IRepository<CategoryEntity>
    {
        SqlDb connection = new SqlDb();

    
        /// <summary>
		/// get all Categories table data
		/// </summary>
		/// <returns></returns>
		public List<CategoryEntity> GetAll()
        {
            string sql = @"SELECT  G.*, C.*
                    FROM[dbo].[GenderCategories_Categories] AS GC
                    INNER JOIN[dbo].[GenderCategories] AS G ON GC.[GenderCategoryId] = G.[Id]
                    INNER JOIN[dbo].[Categories] AS C ON GC.[CategoryId] = C.[Id]";


            Func<SqlConnection, string, List<CategoryEntity>> func = (conn, s) =>
            {
                Dictionary<int, CategoryEntity> dict = new Dictionary<int, CategoryEntity>();

                conn.Query<GenderCategoryEntity, CategoryEntity, CategoryEntity>(s, (G, C) =>
                {
                    if (dict.TryGetValue(C.Id, out CategoryEntity category))
                    {
                        GenderCategoryEntity entity = G;
                        category.GenderCategories.Add(entity);

                    }
                    else
                    {
                        C.GenderCategories = new List<GenderCategoryEntity>();
                        C.GenderCategories.Add(G);
                        dict.Add(C.Id, C);
                    }

                    return category;

                });

                return dict.Values.ToList();    
            };

            List<CategoryEntity> result = this.connection.GetAll<CategoryEntity>(sql, "default", func);

            return result;
        }


        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public CategoryEntity Get(int Id)
        {
            string sql = @"SELECT C.*  , G.* FROM Categories as C 
                            INNER JOIN GenderCategories_Categories as GC ON C.Id = GC.CategoryId
                            INNER JOIN GenderCategories as G ON GC.GenderCategoryId = G.Id
                            WHERE C.Id = @Id";

            object obj = new { Id = Id };

            Func<SqlConnection, string, object ,CategoryEntity> func = (conn, s , o) =>
            {
                CategoryEntity result = null;

                conn.Query<CategoryEntity, GenderCategoryEntity, CategoryEntity>(s, (C, G) =>
                {
                    if (result == null)
                    {
                        result = C;
                        result.GenderCategories = new List<GenderCategoryEntity>();
                        result.GenderCategories.Add(G);
                    }
                    else
                    {
                        result.GenderCategories.Add(G);
                    }
                    return C;

                }, o);

                return result;
            };

            CategoryEntity entity = this.connection.Get<CategoryEntity>(sql, "default", obj ,func);

            return entity;
        
        }


        /// <summary>
        /// Create Category 
        /// </summary>
        /// <param name="dto"></param>
        public void Create(CategoryEntity entity)
        {
            
            //Create new Category
            string sql = "INSERT INTO Categories(Name) VALUES (@Name) SELECT CAST(SCOPE_IDENTITY() as int);";

            object obj = new
            {
                Name = entity.Name,
            };

            Func<SqlConnection, string, object, int> func = (conn, s, o) =>
            {
                int id = conn.QuerySingle<int>(sql, o);
                return id;
            };

            int newId = this.connection.CreateAndGetId(sql, "default", obj , func);

            
            //Set Gender Reference
            string sql2 = @"INSERT INTO GenderCategories_Categories (GenderCategoryId , CategoryId)
                            VALUES(@GenderCategoryId , @CategoryId) ";

            foreach (var item in entity.GenderCategories)
            {
                object obj2 = new
                {
                    GenderCategoryId = item.Id,
                    CategoryId = newId
                };

                this.connection.Create(sql2, "default", obj2);
            }

        }


        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="dto"></param>
        public void Update(CategoryEntity entity)
        {
            //Update Category datas
            string createNewCategory = "UPDATE　Categories SET Name = @Name WHERE Id = @Id";

            object obj = new
            {
                Id = entity.Id,
                Name = entity.Name,
            };

            this.connection.Update(createNewCategory, "default", obj);


            //Delete all GenderCategory reference in GenderCategories_Categories
            string deleteAllReference = "DELETE FROM GenderCategories_Categories WHERE CategoryId = @CategoryId";

            object obj2 = new
            {
                CategoryId = entity.Id,
            };

            this.connection.Delete(deleteAllReference, "default", obj2);


            //Set new GenderCategory reference in GenderCategories_Categories
            string setNewReference = @"INSERT INTO GenderCategories_Categories (GenderCategoryId , CategoryId)
                            VALUES(@GenderCategoryId , @CategoryId)";

            foreach (var item in entity.GenderCategories)
            {
                object obj3 = new
                {
                    GenderCategoryId = item.Id,
                    CategoryId = entity.Id,
                };

                this.connection.Create(setNewReference, "default", obj3);
            }

            
        }


        /// <summary>
        /// Delele Category by Id
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(int Id)
        {
            
            //Delete all GEnderCategory reference in GenderCategories_Categories
            string deleteAllReferende = "DELETE FROM GenderCategories_Categories WHERE CategoryId = @CategoryId";

            object obj2 = new
            {
                CategoryId = Id,
            };

            this.connection.Delete(deleteAllReferende, "default", obj2);

            //Delete Category by id
            string deleteCategory = "DELETE FROM Categories WHERE Id = @Id";

            object obj = new
            {
                Id = Id,
            };

            this.connection.Delete(deleteCategory, "default", obj);
        }
    }

}
