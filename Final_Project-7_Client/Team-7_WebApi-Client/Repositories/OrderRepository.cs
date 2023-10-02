﻿using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;
using Dapper;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Repositories
{
	public class OrderRepository
	{

		SqlDb connection = new SqlDb();

		AppDbContext db = new AppDbContext();
		public List<OrderEntity> GetOrdersbyMember(int memberId)
		{
			SqlDb connection = new SqlDb();

			string sql = @"SELECT O.*, OS.*, M.*, P.*, Sh.*, OI.* FROM Orders as O 
INNER JOIN OrderStatus as OS ON OS.Id = O.StatusId 
INNER JOIN Members as M ON M.Id = O.MemberId 
INNER JOIN Payments as P ON P.Id = O.PaymentId 
INNER JOIN Shippings as Sh ON Sh.Id = O.ShippingId 
INNER JOIN OrderItems as OI ON OI.OrderId = O.Id
WHERE O.MemberId = @MemberId
ORDER BY O.ID DESC";

			object obj = new { MemberId = memberId };

			Func<SqlConnection, string, object, List<OrderEntity>> func = (conn, s, ob) =>
			{
				Dictionary<int, OrderEntity> OrderList = new Dictionary<int, OrderEntity>();
				conn.Query<OrderEntity, OrderStatusEntity, MemberEntity, PaymentEntity, ShippingEntity, OrderItemEntity, OrderEntity>(sql, (O, OS, M, P, SH, OI) =>
				{

					if (OrderList.TryGetValue(O.Id, out OrderEntity Order) == false)
					{
						O.Member = M;
						O.Payment = P;
						O.Shipping = SH;
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

				}, ob);

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


		public  int CreateOrder( OrderPostEntity order)
		{
			string sql = @"INSERT INTO Orders(MemberId,PhoneNumber,Address,ShippingId,CouponId,PaymentId,Total,StatusId,OrderTime)
VALUES(@MemberId,@PhoneNumber,@Address,@ShippingId,@CouponId,@PaymentId,@Total,@StatusId,@OrderTime)
SELECT * FROM  ORDERS  WHERE Id = SCOPE_IDENTITY()";		

			object orders = new 
			{
				MemberId = order.MemberId,
				PhoneNumber = order.PhoneNumber,
				Address = order.Address,
				ShippingId = order.ShippingId,
				CouponId = order.CouponId,
				PaymentId = order.PaymentId,
				Total = order.Total,
				StatusId = "2",
				OrderTime = DateTime.Now
			};			

			var orderId = this.connection.CreateAndGetId(sql, "default", orders);

			var memberId = int.TryParse(order.MemberId, out int id) ? id : 0;

			var cartItems = GetCartItem(memberId);

			var orderItems =  cartItems.Select(x => new OrderItem
			{
				OrderId = orderId,
				ProductId = x.ProductId,
				ProductName = x.Product.Name,
				Price = x.Product.Price,
				Size = x.Size,
				Qty = x.Qty,
				SubTotal = x.Product.Price * x.Qty

			}).ToList();

			db.OrderItems.AddRange(orderItems);
			db.SaveChanges();

			EmptyCart(memberId);

			return orderId;
		}


	

		public List<CartItem>GetCartItem (int memberId)
		{

			var cart = db.Carts.Where(x => x.MemberId == memberId).FirstOrDefault();

			var cartItems = db.CartItems.Where(x => x.CartId == cart.Id).ToList();

			cartItems.ForEach(x => x.Product = db.Products.Where(p => p.Id == x.ProductId).FirstOrDefault());


			return cartItems;
		}

		private void EmptyCart(int memberId)
		{
			
			var cart = db.Carts.Where(x => x.MemberId == memberId).FirstOrDefault();

			if (cart == null) return;

			db.Carts.Remove(cart);
			db.SaveChanges();
		}


	}
}



