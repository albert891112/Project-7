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

		public OrderEntity GetOrderById(int orderId)
		{
			using (var connection = new SqlConnection("default")) 
			{
				connection.Open();

				string sql = @"SELECT O.*, OI.*, M.*, P.*, OS.*
                           FROM Orders AS O
                           INNER JOIN OrderItems AS OI ON O.Id = OI.OrderId
                           INNER JOIN Members AS M ON M.Id = O.MemberId
                           INNER JOIN Products AS P ON P.Id = OI.ProductId
                           INNER JOIN OrderStatus AS OS ON OS.Id = O.OrderStatusId
                           WHERE O.Id = @OrderId";

				var result = connection.Query<OrderEntity, OrderItemEntity, MemberEntity, ProductEntity, OrderStatusEntity, OrderEntity>(
					sql,
					(order, orderItem, member, product, orderStatus) =>
					{
						order.OrderItemList = new List<OrderItemEntity> { orderItem };
						order.Member = member;
						orderItem.Product = product;
						order.OrderStatus = orderStatus;
						return order;
					},
					new { OrderId = orderId },
					splitOn: "Id,OrderId,Id,Id,Id"
				).FirstOrDefault();

				return result;
			}
		}

	}
}