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
using _7_Team_WebApi.Models.EFModels;

namespace _7_Team_WebApi.Repositories
{
    public class ProductRepository 
    {

        SqlDb connection = new SqlDb();
      
        AppDbContext db  = new AppDbContext();

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
                    p.Category.GenderCategories = new List<GenderCategoryEntity>();
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

                return conn.Query<ProductEntity, GenderCategoryEntity, CategoryEntity, StockEntity , ProductEntity>(s, (p, gc, c , st) =>
                {

                    p.Gender = gc;
                    p.Category = c;
                    p.Category.GenderCategories = new List<GenderCategoryEntity>();
                    p.Stock = st;
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
                "AND (@GenderId IS NULL OR GC.Id = @GenderId) " +
                "AND(@Enable IS NULL OR P.Enable = @Enable)"+
                "Order By p.Id";

            object obj = new
            {

                Name = entity.Name,
                LowPrice = entity.LowPrice,
                HightPrice = entity.HightPrice,
                GenderId = entity.GenderId,
                CategoryId = entity.CategoryId,
                Enable = entity.Enable

            };

            Func<SqlConnection, string, object, List<ProductEntity>> func = (conn, s, o) =>
            {
                return conn.Query<ProductEntity, GenderCategoryEntity, CategoryEntity, StockEntity, ProductEntity>(s, (p, gc, c, st) =>
                {
                    p.Gender = gc;
                    p.Category = c;
                    p.Category.GenderCategories = new List<GenderCategoryEntity>();
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
        public void Create(ProductUploadEntity entity)
        {
            //Create new Stock and get its id
            StockRepository stockRepository = new StockRepository();

            int stockId = stockRepository.Create(entity.Stock);


            //Create new Product and get its id
            string sql = "INSERT INTO Products " +
                "(Name, Price, Image, Description, StockId, Enable ,CategoryId , GenderId) " +
                "VALUES (@Name, @Price, @Image, @Description, @StockId, @Enable , @CategoryId , @GenderId ) " +
                "SELECT CAST(SCOPE_IDENTITY() as int);";

            object obj = new
            {
                Name = entity.Name,
                Price = entity.Price,
                Image = entity.Image,
                CategoryId = entity.CategoryId,
                GenderId = entity.GenderId, 
                Description = entity.Description,
                StockId = stockId,
                Enable = entity.Enable
            };

            int productId = this.connection.CreateAndGetId(sql, "default" , obj);


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
        public void Update(ProductUploadEntity entity)
        {

            //Update Product data
            string sql = @"UPDATE Products SET 
                    Name = CASE WHEN @Name IS NULL THEN Name ELSE @Name END, 
                    Price = CASE WHEN @Price IS NULL THEN Price ELSE @Price END,  
                    Image = CASE WHEN @Image IS NULL THEN Image ELSE @Image END, 
                    Description = CASE WHEN @Description IS NULL THEN Description ELSE @Description END,
                    Enable = CASE WHEN @Enable IS NULL THEN Enable ELSE @Enable END ,
                    GenderId = CASE WHEN @GenderId IS NULL THEN GenderId ELSE @GenderId END ,
                    CategoryId = CASE WHEN @CategoryId IS NULL THEN CategoryId ELSE @CategoryId END
                    WHERE Id = @Id";

            object obj = new
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Image = entity.Image,
                Description = entity.Description,
                Enable = entity.Enable,
                GenderId = entity.GenderId,
                CategoryId = entity.CategoryId
            };

            this.connection.Update(sql, "default", obj);


            //Update Stock data
            StockRepository stockRepository = new StockRepository();

            stockRepository.Update(entity.Stock);
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
                return conn.Query<CategoryEntity , GenderCategoryEntity ,ProductRankingEntity , ProductRankingEntity>(pro , (c , g , p) =>
                {
                    p.Category = c;
                    p.Category.GenderCategories = new List<GenderCategoryEntity>();
                    p.Category.GenderCategories.Add(g);
                    p.Gender = g;
                    return p;

                },commandType: CommandType.StoredProcedure).ToList();
            };

            List<ProductRankingEntity> result = connection.GetAll<ProductRankingEntity>(procedure, "default", func);

            return result;
        }

    }
}