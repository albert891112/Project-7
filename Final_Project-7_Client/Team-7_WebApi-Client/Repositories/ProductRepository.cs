using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Dapper;
using Team_7_WebApi_Client.Models.Views;
using System.Data;
using System.Reflection;

namespace Team_7_WebApi_Client.Repositories
{
    public class ProductRepository 
    {
        SqlDb connection = new SqlDb();


        /// <summary>
        /// Get Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductEntity Get(int id)
        {
            string sql = "SELECT P.* , GC.* , C.* FROM Products as P " +
                "INNER JOIN GenderCategories as GC ON P.GenderId = GC.Id " +
                "INNER JOIN Categories as C ON P.CategoryId = C.Id;" +
                "WHERE P.Id = @Id";

            object obj = new
            {
                Id = id
            };

            Func<SqlConnection, string, object, ProductEntity> func = (conn, s, o) =>
            {
                return conn.Query<ProductEntity, GenderCategoryEntity, CategoryEntity, ProductEntity>(s, (p, gc, c) =>
                {
                    p.Gender = gc;
                    p.Category = c;
                    return p;
                }, o).FirstOrDefault();

            };


            ProductEntity product = this.connection.Get<ProductEntity>(sql, "default", obj, func);


            return product;

        }


        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public List<ProductEntity> GetAll()
        {
            string sql = "SELECT P.* , GC.* , C.* FROM Products as P " +
                "INNER JOIN GenderCategories as GC ON P.GenderId = GC.Id " +
                "INNER JOIN Categories as C ON P.CategoryId = C.Id;";



            Func<SqlConnection, string, List<ProductEntity>> func = (conn, s ) =>
            {
                
                return conn.Query<ProductEntity, GenderCategoryEntity, CategoryEntity, ProductEntity>(s, (p, gc, c) =>
                {
                 
                    p.Gender = gc;
                    p.Category = c;
                    return p;

                }).ToList();

            };

            List<ProductEntity> products = this.connection.GetAll<ProductEntity>(sql, "default", func);

            return products;

        }


        /// <summary>
        /// Search product 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<ProductEntity> Search(ProductSearchEntity entity)
        {
            string sql = "SELECT P.* , GC.* , C.* FROM Products as P " +
                "INNER JOIN GenderCategories as GC ON P.GenderId = GC.Id " +
                "INNER JOIN Categories as C ON P.CategoryId = C.Id " +
                "WHERE (@CategoryId IS NULL OR C.Id = @CategoryId) " +
                "AND (@Name IS NULL OR P.Name LIKE '%' + @Name +'%') " +
                "AND (@LowPrice IS NULL OR P.Price >= @LowPrice) " +
                "AND (@HeightPrice IS NULL OR P.Price <= @HightPrice) " +
                "AND (@GenderId IS NULL OR GC.Id = @GenderId)" +
                "Order By p.Id";

            object obj = new
            {
             
                Name = entity.Name,
                LowPrice= entity.LowPrice,
                HeightPrice = entity.HeightPrice,
                Genderid = entity.Gender.Id,
                CategoryId = entity.Category.Id

            };

            Func<SqlConnection, string, object, List<ProductEntity>> func = (conn, s, o) =>
            {
                return conn.Query<ProductEntity, GenderCategoryEntity, CategoryEntity, ProductEntity>(s, (p, gc, c) =>
                {
                    p.Gender = gc;
                    p.Category = c;
                    return p;
                }, o).ToList();

            };


            List<ProductEntity> products = this.connection.Search<List<ProductEntity>>(sql, "default", obj, func);



            return products;
        }

    }
}