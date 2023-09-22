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
            string sql = "SELECT P.* , GC.* , C.* , S.* FROM Products as P " +
                "INNER JOIN GenderCategories as GC ON P.GenderId = GC.Id " +
                "INNER JOIN Categories as C ON P.CategoryId = C.Id " +
                "INNER JOIN Stocks as S ON P.StockId = S.Id " +
                "ORDER BY P.Id ";



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
        /// Create new product and get its id
        /// </summary>
        /// <param name="entity"></param>
        public void Create(ProductEntity entity)
        {
            //Create new Stock and get its id
            StockRepository stockRepository = new StockRepository();

            int stockId = stockRepository.Create(entity.Stock);


            //Create new Product and get its id
            string sql = "INSERT INTO Products " +
                "(Name, Price, Image, Description, StockId, Enable) " +
                "VALUES (@Name, @Price, @Image, @Description, @StockId, @Enable) " +
                "SELECT CAST(SCOPE_IDENTITY() as int);";

            object obj = new
            {
                Name = entity.Name,
                Price = entity.Price,
                Image = entity.Image,
                Description = entity.Description,
                StockId = stockId,
                Enable = entity.Enable
            };

            int productId = this.connection.CreateAndGetId(sql, "defualt" , obj);


            //Update Stock with ProductId
            var stock = new StockEntity
            {
                Id = stockId,
                ProductId = productId,
            };  

            stockRepository.setProductId(stock);
  
        }

        
        /// <summary>
        /// Update products
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(ProductEntity entity)
        {
            string sql = "UPDATE Products SET " +
                "Name = CASE WHEN @Name IS NULL THEN Name ELSE @Name END, " +
                "Price = CASE WHEN @Price IS NULL THEN Price ELSE @Price END, " +
                "Image = CASE WHEN @Image IS NULL THEN Image ELSE @Image END, " +
                "Description = CASE WHEN @Description IS NULL THEN Description ELSE @Description END, " +
                "StockId = CASE WHEN @StockId IS NULL THEN StockId ELSE @StockId END, " +
                "Enable = CASE WHEN @Enable IS NULL THEN Enable ELSE @Enable END" +
                "WHERE Id = @Id";

            object obj = new
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Image = entity.Image,
                Description = entity.Description,
                StockId = entity.Stock.Id,
                Enable = entity.Enable
            };

            this.connection.Update(sql, "default", obj);
        }
    }
}