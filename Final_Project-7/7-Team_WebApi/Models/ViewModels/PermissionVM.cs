using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class PermissionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }


    public static class PermissionVMExtenssion
    {
        public static PermissionVM ToVM(this PermissionDTO dto)
        {
            return new PermissionVM()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description
            };
        }
        
    }   

}