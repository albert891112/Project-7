using Albert.Lib;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Repositories
{
	public class CartRepository
	{
		SqlDb connection = new SqlDb();


		public List<CartEntity> GetAll()
		{
			string sql = @"SELECT C.* , CI.* , P.* FROM Carts as C 
INNER JOIN CartItems as CI ON CI.CartId = C.Id 
INNER JOIN Products as P ON CI.ProductId = P.Id
INNER JOIN Members as M ON C.MemberId = M.Id";

			Func<SqlConnection, string, List<CartEntity>> func = (conn, s) =>
			{
				var cartDictionary = new Dictionary<int, CartEntity>();

				return conn.Query<CartEntity, CartItemEntity, ProductEntity, MemberEntity, CartEntity>(s, (c, ci, p, m) =>
				{
					if (!cartDictionary.TryGetValue(c.Id, out var cart))
					{
						cart = c;						
						cart.CartItems = new List<CartItemEntity>();
						cart.Member = m;
						cartDictionary.Add(c.Id, cart);
					}

					ci.Product = p;
					cart.CartItems.Add(ci);

					return cart;
				}, splitOn: "Id").Distinct().ToList();
			};

			List<CartEntity> carts = this.connection.GetAll<CartEntity>(sql, "default", func);

			return carts;
		}
	}
}