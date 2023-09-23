using _7_Team_WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _7_Team_WebApi.Services;

namespace _7_Team_WebApi.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }        
        public string Name { get; set; }
    }

    public static class UserRegisterDTOExtension
    {
        public static UserDTO ToDTO(this UserVM vm)
        {


            return new UserDTO()
            {
                Account = vm.Account,
                Password = vm.Password,
                Name = vm.Name
            };
        }
    }
}