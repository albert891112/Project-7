using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _7_Team_WebApi.Models.Entities;
using Dapper;
using System.Data.SqlClient;

namespace _7_Team_WebApi.Repositories
{
    public class OrderRepository : IRepository<OrderEntity>
    {

        SqlDb connection = new SqlDb();

        /// <summary>
        /// Get Order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OrderEntity Get(int id)
        {
            string sql = $"SELECT * FROM Orders WHERE Id = @Id";

            object obj = new { Id = id };

            OrderEntity entity = this.connection.Get<OrderEntity>(sql, "default", obj);

            return entity;
        }


        /// <summary>
        /// Get All Orders Data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<OrderEntity> GetAll()
        {
            //string sql = $"SELECT * FROM Orders";
            string sql = $"SELECT O.*, OS.*, M.*, C.*, P.*, S.*, OI.* FROM Orders as O " +
                "INNER JOIN OrderStatus as OS ON OS.Id = O.StatusId " +
                "INNER JOIN Members as M ON M.Id = O.MemberId " +
                "INNER JOIN Coupons as C ON C.Id = O.CouponId " +
                "INNER JOIN Payments as P ON P.Id = O.PaymentId " +
                "INNER JOIN Shippings as S ON S.Id = O.ShippingId " +
                "INNER JOIN OrderItems as OI ON OI.OrderId = O.Id ";

            Func<SqlConnection, string, List<OrderEntity>> func = (conn, s) =>
            {
                Dictionary<int, OrderItemEntity> OrderItemList = new Dictionary<int, OrderItemEntity>();

                return conn.Query<OrderEntity, OrderStatusEntity, MemberEntity, CouponEntity, PaymentEntity, ShippingEntity, OrderItemEntity, OrderEntity>(sql, (o, os, m, c, p, sh, oi) =>
                {   o.OrderStatus = os;
                    o.Member = m;
                    o.Coupon=c;
                    o.Payment = p;
                    o.Shipping = sh;
                    OrderItemList.Add(oi.Id, oi);
                    return o;
                
                }).ToList();
                    

            };
            List< OrderEntity > orders = this.connection.GetAll<OrderEntity>(sql, "default", func);
            return orders;
            //List<OrderEntity> entity = this.connection.GetAll<OrderEntity>(sql, "default");
            //return entity;
        }


        /// <summary>
        /// Create new Order
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Create(OrderEntity entity)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Delete Order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(OrderEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}