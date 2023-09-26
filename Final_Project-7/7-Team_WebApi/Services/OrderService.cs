using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Services
{
    public class OrderService
    {
        OrderRepository repo = new OrderRepository();
        public List<OrderEntity> GetAll()
        {
            List<OrderEntity> entities = this.repo.GetAll();

            //List<OrderDTO> dtos = entities.Select(x => x.ToDTO()).ToList();

            return entities;
        }

    }
}