using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Views;
using Team_7_WebApi_Client.Services;

namespace Team_7_WebApi_Client.Controllers.Category
{
    public class CategoryApiController : ApiController
    {

        CategoryService serv = new CategoryService();

        /// <summary>
        /// Get category by Gender
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int Gender)
        {
            List<CategoryDTO> dtos = this.serv.Get(Gender);

            List<CategoryVM> vms = dtos.Select(x => x.ToVM()).ToList();

            return Ok(vms);
        }
    }
}
