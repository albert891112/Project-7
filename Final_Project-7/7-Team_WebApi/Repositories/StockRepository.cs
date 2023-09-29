using _7_Team_WebApi.Models.Entities;
using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Repositories
{
    public class StockRepository
    {
        SqlDb connection = new SqlDb();


        /// <summary>
        /// Create new stock and return new Stock id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Create(StockUploadEntity entity)
        {
            string sql = "INSERT INTO Stocks (S , M , L  , XL) VALUES (@S , @M , @L  , @XL); SELECT CAST(SCOPE_IDENTITY() as int)";

            object obj = new
            {
                S = entity.S,
                M = entity.M,
                L = entity.L,
                XL = entity.XL
            };

            int newId = this.connection.CreateAndGetId(sql, "default", obj);

            return newId;
        }



        /// <summary>
        /// Update stock data
        /// </summary>
        /// <param name="entity"></param>
        public void Update(StockUploadEntity entity)
        {
            string sql = @"UPDATE Stocks SET 
                        S = CASE WHEN @S IS NULL THEN S ELSE @S END , 
                        M = CASE WHEN @M IS NULL THEN M ELSE @M END , 
                        L = CASE WHEN @L IS NULL THEN L ELSE @L END , 
                        XL = CASE WHEN @XL IS NULL THEN XL ELSE @XL END 
                        WHERE ProductId = @ProductId";

            object obj = new
            {
                S = entity.S,
                M = entity.M,
                L = entity.L,
                XL = entity.XL,
                ProductId = entity.ProductId
            };

            this.connection.Update(sql, "default", obj);
        }

        /// <summary>
        /// set ProductId to stock
        /// </summary>
        /// <param name="entity"></param>
        public void setProductId(StockEntity entity) 
        {
            string sql = "UPDATE Stocks SET ProductId = @ProductId WHERE Id = @Id";

            object obj = new
            {
                ProductId = entity.ProductId,
                Id = entity.Id
            };
            
            this.connection.Update(sql, "default", obj);
        }
    }
}