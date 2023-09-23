using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Views;
using System.Web.Security;
using Team_7_WebApi_Client.Services;

namespace Team_7_WebApi_Client.Controllers.Cart
{
    public class CartApiController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public IHttpActionResult AddCartItem(CartItemCreateVM vm)
        {
            var service = new CartService();

            var dto = vm.ToDTO();

            service.AddCartItem(dto);

            return Ok();
        }

    }
}
