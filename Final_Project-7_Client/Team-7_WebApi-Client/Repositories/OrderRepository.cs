using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;
using Dapper;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Repositories
{
    public class OrderRepository
    {
        public List<OrderEntity> GetOrdersbyMember(int memberId)
        {
            SqlDb connection = new SqlDb();

            string sql = @"SELECT O.*, OI.*, M.*, P.*, OS.*
                   FROM OrderItems as OI
                   INNER JOIN Orders as O ON O.Id = OI.OrderId
                   INNER JOIN OrderStatus as OS ON OS.Id = O.StatusId
                   INNER JOIN Members as M ON M.Id = O.MemberId
                   INNER JOIN Products as P ON P.Id = OI.ProductId
                   WHERE O.MemberId = @MemberId
                   ORDER BY O.ID";

            object obj = new { MemberId = memberId };

            Func<SqlConnection, string, object, List<OrderEntity>> func = (conn, s, o) =>
            {
                Dictionary<int, OrderEntity> OrderList = new Dictionary<int, OrderEntity>();
                conn.Query<OrderEntity, OrderItemEntity, MemberEntity, ProductEntity, OrderStatusEntity, OrderEntity >(s, (O, OI, M, P, OS) =>
                {

                    if (OrderList.TryGetValue(O.Id, out OrderEntity Order) == false)
                    {
                        O.Member = M;
                        OI.Product = P;
                        O.OrderStatus = OS;
                        O.OrderItemList = new List<OrderItemEntity>();
                        O.OrderItemList.Add(OI);
                        OrderList.Add(O.Id, O);
                    }
                    else
                    {
                        Order.OrderItemList.Add(OI);
                    }
                    return O;
                    
                }, o);

                return OrderList.Values.ToList();
            };

            List<OrderEntity> result = connection.Search<List<OrderEntity>>(sql, "default", obj, func);
            return result;
        }

		public List<OrderItemEntity> GetOrderById(int orderId)
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

			object obj = new { OrderId = orderId };

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

	}
}