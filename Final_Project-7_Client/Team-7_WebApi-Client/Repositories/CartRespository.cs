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
		/// Get Cart by MemberId
		/// </summary>
		/// <param name="Account"></param>
		/// <returns></returns>
		public CartEntity GetCartByMember(string Account)
		{
			string sql = @"SELECT C.* , M.* FROM Carts as C 
                        INNER JOIN Members as M ON C.MemberId = M.Id 
                        WHERE M.Account = @Account";

			object obj = new { Account = Account };

			Func<SqlConnection, string, object, CartEntity> func = (conn, s, o) =>
			{
                CartEntity cart = null;

                return conn.Query<CartEntity, MemberEntity, CartEntity>(s, (c, m) =>
				{
                    if (cart != null)
					{
                        cart.MemberId = m.Id;
                    }
                    else
					{
                        c.MemberId = m.Id;
                        cart = c;
                    }
                    return cart;

                }, o).FirstOrDefault();

            };

			CartEntity Cart = this.connection.Get(sql, "default", obj, func);	

			return Cart;
		}

		/// <summary>
		/// Get CartItem by CartId
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public List<CartItemEntity> GetCartItem(CartEntity entity)
		{
			string sql = @"SELECT CI.* , P.* , C.* , GC.* , S.*  FROM CartItems as CI 
						INNER JOIN Products as P ON CI.ProductId = P.Id
						INNER JOIN Categories as C ON P.CategoryId = C.Id
						INNER JOIN GenderCategories as GC ON P.GenderId = GC.Id
                        INNER JOIN Stocks as S ON P.StockId = S.Id
						WHERE CartId = @CartId";

			object obj = new { CartId = entity.Id };

			Func<SqlConnection, string, object, List<CartItemEntity>> func = (conn, s, o) =>
			{
				return conn.Query<CartItemEntity, ProductEntity, CategoryEntity, GenderCategoryEntity, StockEntity ,CartItemEntity>(s, (ci, p, c ,g ,st) =>
				{
                    p.Category = c;
					p.Gender = g;
					p.Stock = st;
                    ci.Product = p;
					return ci;

                }, o).ToList();

            };
			
			List<CartItemEntity> CartItems = this.connection.Get<List<CartItemEntity>>(sql, "default", obj, func);

			return CartItems;


		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<ShippingEntity> GetShippings()
		{
			SqlDb connection = new SqlDb();

			string sql = @"SELECT * FROM Shippings  ORDER BY Id";


			List<ShippingEntity> entities = connection.GetAll<ShippingEntity>(sql, "default");

			return entities;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<PaymentEntity> GetPayments()
		{
			SqlDb connection = new SqlDb();

			string sql = @"SELECT * FROM Payments  ORDER BY Id";
			

			List<PaymentEntity> entities = connection.GetAll<PaymentEntity>(sql, "default");

			return entities;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<GetCouponEntity> GetCoupons()
		{
			SqlDb connection = new SqlDb();

			string sql = @"SELECT * FROM Coupons AS C ORDER BY C.Id";

			List<GetCouponEntity> entities = connection.GetAll<GetCouponEntity>(sql, "default");

			return entities;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="CartItemId"></param>
		public void DeleteCartItem(int CartItemId)
		{
            var CartItem = db.CartItems.FirstOrDefault(c => c.Id == CartItemId);

            db.CartItems.Remove(CartItem);
            db.SaveChanges();
        }
	
	
	}		
}