using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Repositories;

namespace Team_7_WebApi_Client.Services
{
	public class OrderService
	{
		OrderRepository repo = new OrderRepository();		

		public void Create(OrderPostDTO order)
		{
			OrderPostEntity orders = order.ToEntity();			

			this.repo.CreateOrder(orders);
		}
	}
}