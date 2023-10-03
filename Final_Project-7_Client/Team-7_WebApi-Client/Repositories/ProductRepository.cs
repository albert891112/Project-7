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
                return conn.Query<ProductEntity, GenderCategoryEntity, CategoryEntity, StockEntity ,ProductEntity>(s, (p, gc, c , st) =>
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
            string sql = @"SELECT P.* , GC.* , C.* FROM Products as P 
                        INNER JOIN GenderCategories as GC ON P.GenderId = GC.Id
                        INNER JOIN Categories as C ON P.CategoryId = C.Id 
                        WHERE P.Enable = 1";



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
            string sql = "SELECT P.* , GC.* , C.* , S.* FROM Products as P " +
                "INNER JOIN GenderCategories as GC ON P.GenderId = GC.Id " +
                "INNER JOIN Categories as C ON P.CategoryId = C.Id " +
                "INNER JOIN Stocks as S ON P.StockId = S.Id " +
                "WHERE (@CategoryId IS NULL OR C.Id = @CategoryId) " +
                "AND (@Name IS NULL OR P.Name LIKE '%' + @Name +'%') " +
                "AND (@LowPrice IS NULL OR P.Price >= @LowPrice) " +
                "AND (@HightPrice IS NULL OR P.Price <= @HightPrice) " +
                "AND (@Gender IS NULL OR GC.Gender = @Gender) " +
                "AND P.Enable = 1" +
                "Order By p.Id";

            object obj = new
            {
             
                Name = entity.Name,
                LowPrice= entity.LowPrice,
                HightPrice = entity.HightPrice,
                Gender = entity.Gender,
                CategoryId = entity.CategoryId

            };

            Func<SqlConnection, string, object, List<ProductEntity>> func = (conn, s, o) =>
            {
                return conn.Query<ProductEntity, GenderCategoryEntity, CategoryEntity, StockEntity ,  ProductEntity>(s, (p, gc, c , st) =>
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
        /// GEt Product Sale Ranking
        /// </summary>
        /// <returns></returns>
        public List<ProductRankingEntity> GetProductRanking()
        {
            SqlDb connection = new SqlDb();

            string procedure = "GetSalesRank";

            object obj = new { Id = 0 };

            Func<SqlConnection, string, List<ProductRankingEntity>> func = (conn, pro) =>
            {
                return conn.Query<CategoryEntity, GenderCategoryEntity, ProductRankingEntity, ProductRankingEntity>(pro, (c, g, p) =>
                {
                    p.Category = c;
                    p.Category.GenderCategories = new List<GenderCategoryEntity>();
                    p.Category.GenderCategories.Add(g);
                    p.Gender = g;
                    return p;

                }, commandType: CommandType.StoredProcedure).ToList();
            };

            List<ProductRankingEntity> result = connection.GetAll<ProductRankingEntity>(procedure, "default", func);

            return result;
        }


    }
}