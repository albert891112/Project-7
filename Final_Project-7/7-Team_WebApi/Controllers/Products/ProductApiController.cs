using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _7_Team_WebApi.Controllers.Products
{
    public class ProductApiController : ApiController
    {
        ProductService serv = new ProductService();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var dto = this.serv.Get(id);

            return Ok(dto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var dtos = this.serv.GetAll();

            return Ok(dtos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create(ProductCreateVM vm)
        {
            ProductDTO dto = vm.ToDTO();

            this.serv.Create(dto);

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update(ProductVM vm)
        {
            ProductDTO dto = vm.ToDTO();

            this.serv.Update(dto);

            return Ok();
        }



                                                      
    }
}
