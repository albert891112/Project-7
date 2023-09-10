using _7_Team_WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool AccountStatus { get; set; }
    }


    public static class MemberDTOExtension
    {
        public static MemberDTO ToDTO(this MemberEntity entity)
        {
            return new MemberDTO()
            {
                Id = entity.Id,
                FristName = entity.FristName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = entity.Password,
                AccountStatus = entity.AccountStatus
            };
        }
    }s
}