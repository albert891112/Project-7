using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.ViewModels;
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
        OrderService order = new OrderService();
        // GET: Order
        public ActionResult Index()
        {
            var orderVM = order.GetAll().Select(x => x.ToVM());
            return View(orderVM);
        }
    }
}