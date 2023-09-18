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
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        public string Account { get; set; }

        [Display(Name = "密碼")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(70)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "確認密碼")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(70)]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "電子郵件")]
        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage ="{0}格式錯誤")]
        public string Email { get; set; }

        [Display(Name = "名字")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "姓氏")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        public string LastName { get; set; }
    }
}