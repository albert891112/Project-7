using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team_7_WebApi_Client.Models.Views
{
	public class OrderVM
	{
		public int OrderId { get; set; }
		public int MemberId { get; set; }
		public string MemberFirstName { get; set; }
		public string MemberLastName { get; set; }
		public string Address { get; set; }

		public int Total { get; set; }
		public string Status { get; set; }
		public DateTime OrderTime { get; set; }
	}

	public class OrderItemVM
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public string Size { get; set; }
		public int Qty { get; set; }
		public int Price { get; set; }
		public int Subtotal { get; set; }
	}
}