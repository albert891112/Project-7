using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _7_Team_WebApi.Controllers
{
    public class UserApiController : ApiController
    {

        UserServie serv = new UserServie();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserRoles()
        {
            var account = User.Identity.Name;

            var user = serv.GetUserRoles(account);

            var vm = user.ToVM();

            return Ok(vm);
        }


        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<UserDTO> dtos = this.serv.GetAll();

            List<UserVM> vms = dtos.Select(x => x.ToVM()).ToList();

            return Ok(vms);
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int UserId)
        {
            UserDTO dto = this.serv.Get(UserId);

            UserVM vm = dto.ToVM();

            return Ok(vm);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create(UserVM user)
        {

            UserDTO dto = user.ToDTO();

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
        /// update a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update(UserVM user)
        {
            UserDTO dto = user.ToDTO();

            this.serv.Update(dto);

            return Ok();
        }


    }
}
