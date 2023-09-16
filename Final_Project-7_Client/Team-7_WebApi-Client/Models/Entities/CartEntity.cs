using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class CartEntity
	{
		public int Id { get; set; }
		public MemberEntity Member { get; set; }
	}

	public class CartItemEntity
	{
		public int Id { get; set; }
		public OrderEntity Order { get; set; }
		public ProductEntity Product { get; set; }
		public string ProductName { get; set; }
		public int Qty { get; set; }
		public int Price { get; set; }
		public string Size { get; set; }
		public int SubTotal { get; set; }
	}
}