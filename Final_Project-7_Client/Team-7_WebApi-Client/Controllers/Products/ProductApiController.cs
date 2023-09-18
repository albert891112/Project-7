using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Views;
using Team_7_WebApi_Client.Services;

namespace Team_7_WebApi_Client.Controllers.Products
{
    public class ProductApiController : ApiController
    {
        ProductService service = new ProductService();


        /// <summary>
        /// Get Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            ProductDTO dto = this.service.Get(id);

            return Ok(dto);
        }


        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<ProductDTO> dtos = this.service.GetAll();

            return Ok(dtos);
        }

        [HttpPost]
        public IHttpActionResult Search(ProductSearchVM vm)
        {
            ProductSearchDTO dto = vm.ToDTO();

            List<ProductDTO> dtos = this.service.Search(dto);

            return Ok(dtos);
        }   
   
    }
}
