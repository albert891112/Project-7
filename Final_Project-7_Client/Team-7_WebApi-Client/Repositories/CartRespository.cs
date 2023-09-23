using Albert.Lib;
using System.Collections.Generic;
using System.Linq;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;



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

			//Set new cartitem
			var newCartItem = entity.CartItem.ToModel();
			newCartItem.CartId = newCartId;

			//Create new cartitem
			db.CartItems.Add(newCartItem);
			db.SaveChanges();

            
        }	

	}
		    
		
}