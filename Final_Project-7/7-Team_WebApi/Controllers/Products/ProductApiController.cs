using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Albert.Lib;


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
        /// Create Product
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Create()
        {
            HttpPostedFile file = HttpContext.Current.Request.Files["file"];
            var Form = HttpContext.Current.Request.Form;

            ProductUploadDTO dto = Form.ToDTO(file);

            try
            {
                this.serv.Create(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

            return Ok();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update()
        {
            HttpPostedFile file = HttpContext.Current.Request.Files["file"];
            var Form = HttpContext.Current.Request.Form;

            ProductUploadDTO dto = Form.ToDTO(file);
           
            this.serv.Update(dto);

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Search(ProductSearchVM vm)
        {

            var dto  = vm.ToDTO();
            List<ProductDTO> dtos = this.serv.Search(dto);

          

            return Ok(dtos);
        }



                                                      
    }
}
