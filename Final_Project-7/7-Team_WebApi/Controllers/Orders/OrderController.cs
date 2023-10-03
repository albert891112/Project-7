using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Repositories;
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _7_Team_WebApi.Controllers.Orders
{
    public class OrderController : Controller
    {
        // GET: Order
        private OrderService serv = new OrderService();

        [UserAuthorize(Functions = "7")]
        public ActionResult Index()
        {
            List<OrderVM> vms = serv.GetAll().Select(x => x.ToVM()).ToList();

            return View(vms);
        }

        [UserAuthorize(Functions = "7")]
        [HttpGet]
        public ActionResult GetOrderItems(int orderId)
        {

            List<OrderItemEntity> orderItems = serv.GetOrderItem(orderId);

            List<OrderItemVM> vmList = new List<OrderItemVM>();

            foreach (var orderItem in orderItems)
            {
                OrderItemVM vm = new OrderItemVM
                {
                    Id = orderItem.Id,
                    ProductName = orderItem.Product.Name,
                    ProductPrice = orderItem.Product.Price,
                    Size = orderItem.Size,
                    ProductQuantity = orderItem.Qty,
                    Total = orderItem.Product.Price * orderItem.Qty
                };

                vmList.Add(vm);
            }

            return View(vmList);
        }

        [UserAuthorize(Functions = "8")]
        [HttpGet]
        public ActionResult Edit(int Id)
        {
			var vm = serv.Get(Id).ToVM();

            vm.OrderStatusList=serv.GetStatus();

			return View(vm);

		}

        [UserAuthorize(Functions = "8")]
        [HttpPost]
        public ActionResult Edit(OrderVM orderVM)
        {
            
            if (ModelState.IsValid)
            {
                serv.Update(orderVM);
                return RedirectToAction("Index");
            }

            return View(orderVM);
        }


    }
}