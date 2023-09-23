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

        public IHttpActionResult Create(UserVM user)
        {

            UserDTO dto = user.ToDTO();

            this.serv.Create(dto);

            return Ok();
        }


    }
}
