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
    public class OrderRepository 
    {

        SqlDb connection = new SqlDb();

        /// <summary>
        /// Get Order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
 
        
        public List<OrderItemEntity> GetOrderById(int Id)
        {
            SqlDb connection = new SqlDb();

            string sql = @"SELECT O.*, OI.*, M.*, P.*, OS.*
                   FROM OrderItems as OI
                   INNER JOIN Orders as O ON O.Id = OI.OrderId
                   INNER JOIN OrderStatus as OS ON OS.Id = O.StatusId
                   INNER JOIN Members as M ON M.Id = O.MemberId
                   INNER JOIN Products as P ON P.Id = OI.ProductId
                   WHERE OI.OrderId = @OrderId
                   ORDER BY O.ID";

            object obj = new { OrderId = Id };

            Func<SqlConnection, string, object, List<OrderItemEntity>> func = (conn, s, parameter) =>
            {
                return conn.Query<OrderEntity, OrderItemEntity, MemberEntity, ProductEntity, OrderStatusEntity, OrderItemEntity>(s, (O, OI, M, P, OS) =>
                {
                    OI.Product = P;
                    O.OrderItemList = new List<OrderItemEntity>();


                    return OI;

                }, parameter).ToList();

            };

            List<OrderItemEntity> result = connection.Search<List<OrderItemEntity>>(sql, "default", obj, func);

            return result;
        }


        /// <summary>
        /// Get All Orders Data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<OrderEntity> GetAll()
        {
            //string sql = $"SELECT * FROM Orders";
            string sql = @"SELECT O.*, OS.*, M.*, P.*, Sh.*, OI.* FROM Orders as O 
INNER JOIN OrderStatus as OS ON OS.Id = O.StatusId 
INNER JOIN Members as M ON M.Id = O.MemberId 
INNER JOIN Payments as P ON P.Id = O.PaymentId 
INNER JOIN Shippings as Sh ON Sh.Id = O.ShippingId 
INNER JOIN OrderItems as OI ON OI.OrderId = O.Id 
ORDER BY O.ID";

            Func<SqlConnection, string, List<OrderEntity>> func = (conn, s) =>
            {
                
                Dictionary<int, OrderEntity> OrderList = new Dictionary<int, OrderEntity>();

                conn.Query<OrderEntity, OrderStatusEntity, MemberEntity, PaymentEntity, ShippingEntity, OrderItemEntity,  OrderEntity>(sql, (o, os, m ,p ,sh ,oi) =>
                {
                    if (OrderList.TryGetValue(o.Id, out OrderEntity Order) == false)
                    {
                        o.OrderStatus = os;
                        o.Member = m;
                        o.Payment = p;
                        o.Shipping = sh;
                        o.OrderItemList = new List<OrderItemEntity>();
                        o.OrderItemList.Add(oi);
                        OrderList.Add(o.Id, o);
                        
                    }
                    else
                    {
                        Order.OrderItemList.Add(oi);
                    }
                    return o;
                });
                return OrderList.Values.ToList();

            };
            List<OrderEntity> orders = this.connection.GetAll<OrderEntity>(sql, "default", func);
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
            SqlDb connection = new SqlDb();

            string sql = "UPDATE Orders SET statusId = @StatusId , Address = @Address WHERE Id = @Id;";

            object obj = new
            {
                Id = entity.Id,
                Address = entity.Address,
                StatusId = entity.OrderStatus,
            };

            connection.Update(sql, "default", obj);
        }
    }
}