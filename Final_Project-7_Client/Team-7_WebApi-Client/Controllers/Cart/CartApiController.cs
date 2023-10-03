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
using System.Web;

namespace Team_7_WebApi_Client.Controllers.Cart
{
    public class CartApiController : ApiController
    {

        /// <summary>
        /// Add CartItem to Cart
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


        /// <summary>
        /// Show Cart
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IHttpActionResult ShowCart()
        {
            var service = new CartService();

            string account = HttpContext.Current.User.Identity.Name;

            var cart = service.ShowCart(account);

            var cartVM = cart.ToVM();   

            return Ok(cartVM);
        }

		CartService serv = new CartService();

		[HttpGet]
		public IHttpActionResult GetShippingMethod()
		{			

			List<ShippingDTO> dtos = this.serv.GetShipping();

			List<ShippingVM> vms = dtos.Select(s => s.ToVM()).ToList();

			return Ok(vms);
		}

		[HttpGet]
		public IHttpActionResult GetPaymentMethod()
		{

			List<PaymentDTO> dtos = this.serv.GetPayment();

			List<PaymentVM> vms = dtos.Select(p => p.ToVM()).ToList();

			return Ok(vms);
		}


		[HttpGet]
		public IHttpActionResult GetCouponMethod()
		{

			List<GetCouponDTO> dtos = this.serv.GetCoupon();

			List<GetCouponVM> vms = dtos.Select(c => c.ToVM()).ToList();

			return Ok(vms);
		}

        /// <summary>
        /// Delete CartItem from Cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteCartItem(int Id)
        {
            var service = new CartService();

            service.DeleteCartItem(Id);

            return Ok();
        }
	}
}
