using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Views;
using Team_7_WebApi_Client.Services;

namespace Team_7_WebApi_Client.Controllers.Members
{
    public class MemberApiController : ApiController
    {

		MemberService serv = new MemberService();

		[HttpGet]		
		public IHttpActionResult GetEmail(string account)
		{
			string email = this.serv.GetEmail(account);
			return Ok(new { Email = email });
		}
	}
}
