using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Models.ViewModels.PermissionControll;
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
        /// get all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<RoleDTO> roleDTOs = this.serv.GetAll();

            List<RoleVM> roleVMs = roleDTOs.Select(x => x.ToVM()).ToList();

            return Ok(roleVMs);
        }

        /// <summary>
        /// get role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int RoleId)
        {
            RoleDTO roleDTO = this.serv.Get(RoleId);

            RoleVM roleVM = roleDTO.ToVM();

            return Ok(roleVM);
        }


        /// <summary>
        /// Update a role
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update(RoleVM vm)
        {
            RoleDTO dto = vm.ToDTO();

            this.serv.Update(dto);

            return Ok();
        }

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create(RoleVM vm)
        {
            
            RoleDTO roleDTO = vm.ToDTO();

            try
            {
                this.serv.Create(roleDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            return Ok();
        }


        /// <summary>
        /// Get all roles permission by role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRolesPermission(int RoleId)
        {
           RolePermissionDTO rolePermissionDTO = this.serv.GetRolesPermission(RoleId);

            RolePermissionVM rolePermissionVM = rolePermissionDTO.ToVM();

            return Ok(rolePermissionVM);
        }


        /// <summary>
        /// Get all roles user by role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRolesUser(int RoleId)
        {
            RoleUserDTO roleDTO = this.serv.GetRolesUser(RoleId);

            RoleUserVM roleVM = roleDTO.ToVM();

            return Ok(roleVM);
        }

        /// <summary>
        /// Add Permission to role
        /// </summary>
        /// <param name="VM"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddPermissionToRole(RoleUpdateVM VM)
        {
            var dto = VM.ToDTO();

            this.serv.AddPermissionToRole(dto);

            return Ok();
        }

        /// <summary>
        /// Remove Permission from role
        /// </summary>
        /// <param name="VM"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult RemovePermissionFromRole(RoleUpdateVM VM)
        {
            var dto = VM.ToDTO();

            this.serv.DeletePermissionFromRole(dto);

            return Ok();
        }


        /// <summary>
        /// Add User to role
        /// </summary>
        /// <param name="VM"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddUserToRole(RoleUpdateVM VM)
        {
            var dto = VM.ToDTO();

            this.serv.AddUserToRole(dto);

            return Ok();
        }

        /// <summary>
        /// Remove User from role
        /// </summary>
        /// <param name="VM"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult RemoveUserFromRole(RoleUpdateVM VM)
        {
            var dto = VM.ToDTO();

            this.serv.DeleteUserFromRole(dto);

            return Ok();
        }
    
    
    
    
    }
}
