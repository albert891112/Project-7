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
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create(UserVM user)
        {

            UserDTO dto = user.ToDTO();

            this.serv.Create(dto);

            return Ok();
        }


    }
}
