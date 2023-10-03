using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;
using System.Data.SqlClient;
using Dapper;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Repositories
{
	public class CartRepository
	{
		SqlDb connection = new SqlDb();
		AppDbContext db = new AppDbContext();


		/// <summary>
		/// Search for a cart by MemberId
		/// </summary>
		/// <param name="MemberId"></param>
		/// <returns></returns>
		public Cart Search(int MemberId)
		{
			var Cart = db.Carts.FirstOrDefault(c => c.MemberId == MemberId);

			return Cart;
		}


		/// <summary>
		/// Update Cartitem if exist or add new cartitem if not exist
		/// </summary>
		/// <param name="entity"></param>
		public void Upsert(CartItemCreateEntity entity)
		{
            var exist = db.CartItems.FirstOrDefault(o => o.CartId == entity.CartId && o.ProductId == entity.ProductId && o.Size == entity.Size);


			if(exist == null)
			{
				db.CartItems.Add(entity.ToModel());
			}
			else
			{	
				entity.Id = exist.Id;
				entity.Qty += exist.Qty;
				db.Entry(exist).CurrentValues.SetValues(entity.ToModel());
			}

			db.SaveChanges();
           
        }

		
		/// <summary>
		/// Create a new cart and set new cartitem
		/// </summary>
		/// <par1qwdd111am name="entity"></param>
		public void Create(CartCreateEntity entity)
		{
			// Create a new cart
			var newCart = entity.ToModel();

            db.Carts.Add(newCart);
            db.SaveChanges();

			//Get new cart id
            int newCartId = newCart.Id;	

			if(entity.CartItem == null)
			{
                return;
            }
			//Set new cartitem
			var newCartItem = entity.CartItem.ToModel();
			newCartItem.CartId = newCartId;

			//Create new cartitem
			db.CartItems.Add(newCartItem);
			db.SaveChanges();
        }	

	
		/// <summary>
		/// Get cart by member account
		/// </summary>
		/// <param name="Account"></param>
		/// <returns></returns>
		public CartEntity GetCartByMember(string Account)
		{
			string sql = @"SELECT C.* , M.Id , Ci.* , P.* , Ca.* , G.* , S.* FROM Carts as C 
						INNER JOIN Members as M ON C.MemberId = M.Id 
						INNER JOIN CartItems as CI ON Ci.CartId = c.Id
						INNER JOIN Products as P ON CI.ProductId = P.Id
						INNER JOIN Categories as Ca ON P.CategoryId = Ca.Id
						INNER JOIN GenderCategories as G ON P.GenderId = G.Id
						INNER JOIN Stocks as S ON P.Id = S.ProductId		 
						WHERE M.Account = @Account";

			object obj = new { Account = Account };

			Func<SqlConnection, string, object, CartEntity> func = (conn, s, o) =>
			{
				CartEntity cart = null;

				return conn.Query<CartEntity, MemberEntity, CartItemEntity, ProductEntity, CategoryEntity , GenderCategoryEntity ,StockEntity, CartEntity>(s, (c, m, ci, p , ca , g , st) =>
				{
                    if (cart != null)
					{
						p.Stock = st;
						p.Gender = g;
						p.Category = ca;
						ci.Product = p;
						cart.CartItems.Add(ci);
                    }
					else
					{
						c.MemberId = m.Id;
						p.Stock = st;
						p.Category = ca;
						p.Gender = g;
						ci.Product = p;
						c.CartItems = new List<CartItemEntity> ();
						c.CartItems.Add(ci);
						cart = c;
                        
					}
                    return cart;

                }, o).FirstOrDefault();

			};
			

			CartEntity entity = this.connection.Get(sql, "default" , obj , func );

			return entity;
		
		}

		public List<ShippingEntity> GetShippings()
		{
			SqlDb connection = new SqlDb();

			string sql = @"SELECT * FROM Shippings  ORDER BY Id";


			List<ShippingEntity> entities = connection.GetAll<ShippingEntity>(sql, "default");

			return entities;
		}


		public List<PaymentEntity> GetPayments()
		{
			SqlDb connection = new SqlDb();

			string sql = @"SELECT * FROM Payments  ORDER BY Id";
			

			List<PaymentEntity> entities = connection.GetAll<PaymentEntity>(sql, "default");

			return entities;
		}

		public List<GetCouponEntity> GetCoupons()
		{
			SqlDb connection = new SqlDb();

			string sql = @"SELECT * FROM Coupons AS C ORDER BY C.Id";

			List<GetCouponEntity> entities = connection.GetAll<GetCouponEntity>(sql, "default");

			return entities;
		}

		

	
	
	}		
}