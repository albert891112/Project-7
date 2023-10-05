using Albert.Lib;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Repositories
{
    public class ReviewRepository
    {

        SqlDb connection = new SqlDb();
        AppDbContext db = new AppDbContext();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public List<ReviewEntity> Get(int ProductId)
        {
            string sql = @"SELECT r.*  , m.*  FROM Reviews as r 
                        INNER JOIN Members as m ON m.Id = r.MemberId
                        WHERE r.ProductId = @Id";
            
            object obj  = new {Id = ProductId };

            Func<SqlConnection, string, object, List<ReviewEntity>> func = (conn, s, o) =>
            {
                return conn.Query<ReviewEntity,  MemberEntity,  ReviewEntity>(s, (r,  m) =>
                {
                    r.Member = m;
                    return r;

                }, o).ToList();
            };

            List<ReviewEntity> result = connection.Get<List<ReviewEntity>>(sql, "default", obj, func);

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="review"></param>
        public void Create(ReviewEntity review)
        {
            string sql = @"INSERT INTO Reviews (MemberId, ProductId, Rating, Description, ReviewTime, OrderTime)
                        VALUES (@MemberId, @ProductId, @Rating, @Description, @ReviewTime, @OrderTime)";

            object obj = new
            {
                MemberId = review.Member.Id,
                ProductId = review.ProductId,
                Rating = review.Rating,
                Description = review.Description,
                OrderTime = review.OrderTime,
                ReviewTime = review.ReviewTime
            };

           this.connection.Create(sql, "default", obj);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public OrderEntity GetOrderItem(int ProductId, int MemberId)
        {
            string sql = @"SELECT O.* , OI.* FROM Orders as O 
                            INNER JOIN OrderItems as OI ON O.Id = OI.OrderId
                            WHERE O.MemberId = @MemberId
                            AND Oi.ProductId = @ProductId
                            Order By O.OrderTime DESC";

            object obj = new { MemberId = MemberId, ProductId = ProductId };


            Func<SqlConnection, string, object, OrderEntity> func = (conn, s, o) =>
            {

                return conn.Query<OrderEntity, OrderItemEntity, OrderEntity>(s, (or, oi) =>
                {
                    or.OrderItemList = new List<OrderItemEntity>();
                    or.OrderItemList.Add(oi);
                    return or;

                }, o).FirstOrDefault();
            };


            OrderEntity result = connection.Get<OrderEntity>(sql, "default", obj, func);

            return result;
                
        }
           
    }
}