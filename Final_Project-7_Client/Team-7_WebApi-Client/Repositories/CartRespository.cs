using Albert.Lib;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
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
		public CartEntity Search(int MemberId)
		{
			var Cart = db.Carts.Include("CartItem").Where(x => x.MemberId == MemberId).FirstOrDefault();

			return Cart.ToEnity();
		}


		public void Update(CartEntity entity)
		{
            var cart = entity.ToEnity();

            db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

		    
		
}