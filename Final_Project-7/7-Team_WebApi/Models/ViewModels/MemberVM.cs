using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class MemberVM
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool AccountStatus { get; set; }
    }

    public static class MemberVMExtension
    {
        public static MemberVM ToVM(this MemberDTO dto)
        {
            return new MemberVM()
            {
                Id = dto.Id,
                FristName = dto.FristName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                AccountStatus = dto.AccountStatus
            };
        }
    }
}