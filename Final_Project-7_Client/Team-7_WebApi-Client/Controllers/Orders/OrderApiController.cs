using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Views;
using Team_7_WebApi_Client.Services;

namespace Team_7_WebApi_Client.Controllers.Orders
{

    public class OrderApiController : ApiController
    {		

		OrderService serv = new OrderService();

		[HttpPost]
		public IHttpActionResult CreateOrder(OrderPostVM order)
		{
			OrderPostDTO orders = order.ToDTO();			

			this.serv.Create(orders);

			return Ok();
		}
	}
}
