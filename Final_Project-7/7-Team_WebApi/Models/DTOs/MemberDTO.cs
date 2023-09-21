using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string  Account { get; set; }
        public string Password { get; set; }

        public bool Enable { get; set; }
    }


    public static class MemberDTOExtension
    {
        public static MemberDTO ToDTO(this MemberEntity entity)
        {
            return new MemberDTO()
            {
                Id = entity.Id,
                Account = entity.Account,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = entity.Password,
                Enable = entity.Enable
            };
        }

        public static MemberDTO ToDTO(this MemberVM vm)
        {
            return new MemberDTO()
            {
                Id = vm.Id,
                Account= vm.Account,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                Enable = vm.Enable
            };
        }
    }


}