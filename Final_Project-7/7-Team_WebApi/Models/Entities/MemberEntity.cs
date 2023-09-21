using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class MemberEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string  Account { get; set; }
        public string Password { get; set; }
        
        public bool Enable { get; set; }
    }

    public static class MemberEntityExtension
    {
        public static MemberEntity ToEntity(this MemberDTO dto)
        {
            return new MemberEntity()
            {
                Id = dto.Id,
                Account = dto.Account,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                Enable = dto.Enable
            };
        }
    }
}