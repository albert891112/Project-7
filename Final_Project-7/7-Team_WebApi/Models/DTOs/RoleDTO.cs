using _7_Team_WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string  Name { get; set; }
    }


    public static class RoleRegisterDTOExtension
    {
        public static RoleDTO ToDTO(this RoleVM vm)
        {
            return new RoleDTO()
            {
                Name = vm.Name
            };
        }
    }
}