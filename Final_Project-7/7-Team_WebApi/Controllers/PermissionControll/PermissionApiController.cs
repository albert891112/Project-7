using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _7_Team_WebApi.Controllers.PermissionControll
{
    public class PermissionApiController : ApiController
    {
        PermissionService serv = new PermissionService();


        /// <summary>
        /// Create a new permission
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create(PermissionVM vm)
        {
            PermissionDTO  dto = vm.ToDTO();    

            this.serv.Create(dto);

            return Ok();
        }


    }
}
