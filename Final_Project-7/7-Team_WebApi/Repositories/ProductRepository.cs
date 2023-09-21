using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _7_Team_WebApi.Models.Entities;
using System.Data.SqlClient;
using Antlr.Runtime.Misc;
using Dapper;
using System.Data;

namespace _7_Team_WebApi.Repositories
{
    public class ProductRepository : IRepository<ProductEntity>
    {

        SqlDb connection = new SqlDb();

        /// <summary>
        /// Get Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductEntity Get(int id)
        {
            string sql = "SELECT P.* , GC.* , C.* , S.* FROM Products as P " +
                "INNER JOIN GenderCategories as GC ON P.GenderId = GC.Id " +
                "INNER JOIN Categories as C ON P.CategoryId = C.Id " +
                "INNER JOIN Stocks as S ON P.StockId = S.Id " +
                "WHERE P.Id = @Id";

            object obj = new
            {
                Id = id
            };

            Func<SqlConnection, string, object, ProductEntity> func = (conn, s, o) =>
            {
                return conn.Query<ProductEntity, GenderCategoryEntity, CategoryEntity, StockEntity, ProductEntity>(s, (p, gc, c, st) =>
                {
                    p.Gender = gc;
                    p.Category = c;
                    p.Stock = st;
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



            Func<SqlConnection, string, List<ProductEntity>> func = (conn, s) =>
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
            string sql = "SELECT P.* , GC.* , C.* , S.* FROM Products as P " +
                "INNER JOIN GenderCategories as GC ON P.GenderId = GC.Id " +
                "INNER JOIN Categories as C ON P.CategoryId = C.Id " +
                "INNER JOIN Stocks as S ON P.StockId = S.Id " +
                "WHERE (@CategoryId IS NULL OR C.Id = @CategoryId) " +
                "AND (@Name IS NULL OR P.Name LIKE '%' + @Name +'%') " +
                "AND (@LowPrice IS NULL OR P.Price >= @LowPrice) " +
                "AND (@HightPrice IS NULL OR P.Price <= @HightPrice) " +
                "AND (@Gender IS NULL OR GC.Gender = @Gender)" +
                "Order By p.Id";

            object obj = new
            {

                Name = entity.Name,
                LowPrice = entity.LowPrice,
                HightPrice = entity.HightPrice,
                Gender = entity.Gender,
                CategoryId = entity.CategoryId

            };

            Func<SqlConnection, string, object, List<ProductEntity>> func = (conn, s, o) =>
            {
                return conn.Query<ProductEntity, GenderCategoryEntity, CategoryEntity, StockEntity, ProductEntity>(s, (p, gc, c, st) =>
                {
                    p.Gender = gc;
                    p.Category = c;
                    p.Stock = st;
                    return p;
                }, o).ToList();

            };


            List<ProductEntity> products = this.connection.Search<List<ProductEntity>>(sql, "default", obj, func);



            return products;
        }




        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="entity"></param>
        public void Create(ProductEntity entity)
    {
        string sql = "INSERT INTO Products " +
            "(Name, Price, Image, Description, StockId, Enable) " +
            "VALUES (@Name, @Price, @Image, @Description, @StockId, @Enable)";

        object obj = new
        {
            Name = entity.Name,
            Price = entity.Price,
            Image = entity.Image,
            Description = entity.Description,
            StockId = entity.Stock.Id,
            Enable = entity.Enable
        };

        this.connection.Create(sql, "defualt" , obj);
    }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        
        public void Update(ProductEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}