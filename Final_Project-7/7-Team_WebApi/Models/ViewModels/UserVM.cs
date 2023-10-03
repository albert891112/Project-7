using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class UserVM
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

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50)]
        public string Name { get; set; }
    }

    public class UserRoleVM
    {
        public int Id { get; set; }
        public string Account { get; set; }

        public string[] Role { get; set; }

        public string LoginTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

    }



    public static class UserVMExtenssion
    {
        public static UserVM ToVM(this UserDTO dto)
        {
            return new UserVM()
            {
                Id = dto.Id,
                Account = dto.Account,
                Name = dto.Name
            };
        }


        public static UserRoleVM ToVM(this UserRoleDTO dto)
        {
            return new UserRoleVM
            {
                Id = dto.Id,
                Account = dto.Account,
                Role = dto.Role.Select(x => x.Name).ToArray()    
            };
        }
    }   
}