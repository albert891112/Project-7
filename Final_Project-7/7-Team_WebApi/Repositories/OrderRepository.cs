using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _7_Team_WebApi.Models.Entities;
using Dapper;

namespace _7_Team_WebApi.Repositories
{
    public class OrderRepository : IRepository<OrderEntity>
    {

        SqlDb connection = new SqlDb();

        /// <summary>
        /// Get Order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OrderEntity Get(int id)
        {
            string sql = $"SELECT * FROM Orders WHERE Id = @Id";

            object obj = new { Id = id };

            OrderEntity entity = this.connection.Get<OrderEntity>(sql, "default", obj);

            return entity;
        }


        /// <summary>
        /// Get All Orders Data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<OrderEntity> GetAll()
        {
            string sql = $"SELECT * FROM Orders";

            List<OrderEntity> entity = this.connection.GetAll<OrderEntity>(sql, "default");

            return entity;
        }


        /// <summary>
        /// Create new Order
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Create(OrderEntity entity)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Delete Order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(OrderEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}