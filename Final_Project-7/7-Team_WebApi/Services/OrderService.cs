﻿using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
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

            List<OrderDTO> dtos = entities.Select(x => x.ToDTO()).ToList();

            return entities;
        }

        public List<OrderEntity> GetByMemberId(int MemberId)
        {
            List<OrderEntity> entities = this.repo.GetAll();

            entities = entities.Select(x => x).Where(x => x.Member.Id == MemberId).ToList();

            return entities;
        }

        public List<OrderItemEntity> Get(int orderid)
        {
            List<OrderItemEntity> entity = this.repo.GetOrderById(orderid);

            return entity;
        }

        public void Update(OrderVM orderVM)
        {
            OrderEntity entity = orderVM.ToEntity();
            this.repo.Update(entity);
        }

    }
}