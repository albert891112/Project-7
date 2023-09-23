using _7_Team_WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public static class PermissionExtension
    {
        public static PermissionDTO ToDTO(this PermissionVM vm)
        {
            return new PermissionDTO()
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description
            };
        }
    }
}