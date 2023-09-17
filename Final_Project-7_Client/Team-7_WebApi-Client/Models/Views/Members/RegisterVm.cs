using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Team_7_WebApi_Client.Models.Views
{
    public class RegisterVm
    {
        public int Id { get; set; }

        [Display(Name = "帳號")]
        [Required]
        [StringLength(50)]
        public string Account { get; set; }

        [Display(Name = "密碼")]
        [Required]
        [StringLength(70)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "確認密碼")]
        [Required]
        [StringLength(70)]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "電子郵件")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "名")]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "姓")]
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
    }
}