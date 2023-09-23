using Albert.Lib;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;
using Team_7_WebApi_Client.Models.Views.Members;
using Team_7_WebApi_Client.Repositories;

namespace Team_7_WebApi_Client.Controllers.Orders
{
    public class OrdersController : Controller
    {
        // GET: Orders      

		public ActionResult Index()
		{
			return Index();
		}
	       
        private OrderRepository _orderRepository = new OrderRepository();
        public ActionResult GetOrdersForCurrentUser()
        {
            var buyer = User.Identity.Name;

            int memberId = GetMemberIdByAccount(buyer);

            List<OrderEntity> orderItems = _orderRepository.GetOrderItemsbyMember(memberId);

            List<MemberOrderVm> vmList = orderItems.Select(order =>
                new MemberOrderVm
                {
                    memberId = order.Member, 
                    Id = order.Id,
                    OrderDate = order.OrderTime,
                    OrderItems = order.OrderItemList, 
                    Status = order.OrderStatus.Status
                }
            ).ToList();

            return View(vmList);
        }

        public ActionResult GetOrderItems()
        {
            //todo
            var buyer = User.Identity.Name;

            int memberId = GetMemberIdByAccount(buyer);

            //List<OrderItemEntity> orderItems = _orderRepository.GetOrderItemsbyMember(memberId);

            return View();
        }

        private int GetMemberIdByAccount(string buyer)
        {
            var db = new AppDbContext();

            var member=db.Members.Where(x => x.Account == buyer).FirstOrDefault();
            
            if(member == null)
            {
                throw new Exception("查無此會員");
            }

            return member.Id;
        }
    }
}