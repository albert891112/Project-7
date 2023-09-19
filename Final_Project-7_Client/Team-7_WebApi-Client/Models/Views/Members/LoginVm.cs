using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team_7_WebApi_Client.Models.Views.Members
{
	public class LoginVm
	{
        [Display(Name="帳號")]
        [Required]
        public string Account { get; set; }

        [Display(Name="密碼")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="記住我")]
        public bool RememberMe { get; set; }
    }
}