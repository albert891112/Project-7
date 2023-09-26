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
        // GET: Order
        private OrderService serv = new OrderService();
        public ActionResult Index()
        {
            List<OrderVM> vms = serv.GetAll().Select(x => x.ToVM()).ToList();

            return View(vms);
        }

    }
}