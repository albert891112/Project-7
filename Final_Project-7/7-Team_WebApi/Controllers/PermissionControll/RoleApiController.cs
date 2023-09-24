using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Repositories;
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _7_Team_WebApi.Controllers.PermissionControll
{
    public class RoleApiController : ApiController
    {

        RoleService serv = new RoleService();

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create(RoleVM vm)
        {
            
            RoleDTO roleDTO = vm.ToDTO();

            this.serv.Create(roleDTO);

            return Ok();
        }



        [HttpGet]
        public IHttpActionResult GetRolesPermission(int id)
        {
           RolePermissionDTO rolePermissionDTO = this.serv.GetRolesPermission(id);

            return Ok(rolePermissionDTO);
        }
    }
}
