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
        
        public ActionResult Index()
        {
            List<OrderVM> vms = serv.GetAll().Select(x => x.ToVM()).ToList();

            return View(vms);
        }
        public ActionResult GetOrderItems(int orderId)
        {

            List<OrderItemEntity> orderItems = serv.Get(orderId);

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

    }
}