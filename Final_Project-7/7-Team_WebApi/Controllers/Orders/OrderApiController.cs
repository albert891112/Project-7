
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _7_Team_WebApi.Controllers.Orders
{
    public class OrderApiController : ApiController
    {
        OrderService serv = new OrderService();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StatusId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetByStatus(int StatusId)
        {
            var result = this.serv.GetByStatusId(StatusId);

            return Ok(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetOrderItem(int OrderId)
        {
            var result = this.serv.GetOrderItem(OrderId);

            return Ok(result);
        }
    }
}
