using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Team_7_WebApi_Client.Models.Views.Members
{
    public class EditProfileVm
    {
        public int Id { get; set; }

        [Display(Name = "姓氏")]
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "名字")]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "電子郵件")]
        [Required]
        [EmailAddress(ErrorMessage = "{0}格式錯誤")]
        public string Email { get; set; }
    }
}