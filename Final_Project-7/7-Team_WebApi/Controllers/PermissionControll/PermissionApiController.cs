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
        /// Get all permissions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<PermissionDTO> dtos = this.serv.GetAll();

            List<PermissionVM> vms = dtos.Select(x => x.ToVM()).ToList();

            return Ok(vms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PermissionId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int PermissionId)
        {
            PermissionDTO dto = this.serv.Get(PermissionId);

            PermissionVM vm = dto.ToVM();

            return Ok(vm);
        }

        /// <summary>
        /// Update permission
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update(PermissionVM vm)
        {
            PermissionDTO dto = vm.ToDTO();

            this.serv.Update(dto);

            return Ok();
        }
    }
}
