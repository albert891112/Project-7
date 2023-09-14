using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class MemberEntity
	{
		public int Id { get; set; }

		[Display(Name = "帳號")]
		[Required]
		public string Account { get; set; }

		[Display(Name = "密碼")]
		[Required]
		public string Password { get; set; }

		[Display(Name = "電子郵件")]
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Display(Name = "名")]
		[Required]
		public string FirstName { get; set; }

		[Display(Name = "姓")]
		[Required]
		public string LastName { get; set; }

		public bool Enable { get; set; }
	}
}