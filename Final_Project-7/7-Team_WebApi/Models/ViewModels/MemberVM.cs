using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class MemberVM
    {
        public int Id { get; set; }

        [Display(Name="姓名")]
        public string FirstName { get; set; }

        [Display(Name ="姓氏")]
        public string LastName { get; set; }

        [Display(Name ="電子郵件")]
        public string Email { get; set; }

        [Display(Name ="帳號")]
        public string Account { get; set; }

        [Display(Name = "帳號可用狀態")]
        public bool Enable { get; set; }

        [Display(Name = "帳號可用狀態")]
        public string StatusText
        {
            get
            {
                return Enable ? "啟用" : "停用";
            }
        }

    }

    public static class MemberVMExtension
    {
        public static MemberVM ToVM(this MemberDTO dto)
        {
            return new MemberVM()
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Account = dto.Account,
                Email = dto.Email,
                Enable = dto.Enable
            };
        }
    }
}