using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Repositories;
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _7_Team_WebApi.Controllers.Categories
{
    public class CategoryApiController : ApiController
    {
        CategoryService serv = new CategoryService(); 

        

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<CategoryVM> vms = serv.GetAll().Select(x => x.ToVM()).ToList();

            var vms2 = new CategoryRepository().GetAll();


            return Ok(vms2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int Id)
        {
            CategoryVM vm = serv.Get(Id).ToVM();

            return Ok(vm);
        }


        [HttpGet]
        public IHttpActionResult GetByGender(int GenderId)
        {
            List<CategoryVM> vms = this.serv.GetByGender(GenderId).Select(x => x.ToVM()).ToList();

            return Ok(vms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create(CategoryCreateVM vm)
        {
            try
            {
                serv.Create(vm.ToDTO());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            string ErrorMessage = String.Empty;
            try
            {
               serv.Delete(id);
            }
            catch(Exception ex)
            {
               return BadRequest(ex.Message);
            }
           

            return Ok(ErrorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update(CategoryCreateVM vm)
        {
            try
            {
                serv.Update(vm.ToDTO());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            return Ok();
        }

        
        
    }
}
