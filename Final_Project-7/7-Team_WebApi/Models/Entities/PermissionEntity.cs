using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public static class PermissionExtension
    {
        public static PermissionEntity ToEntity(this PermissionDTO dto)
        {
            return new PermissionEntity()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description
            };
        }


        public static PermissionEntity ToEntity(this Premission model)
        {
            return new PermissionEntity()
            {
                Id = model.Id,
                Name = model.PermissionName,
                Description = model.PermissionDescription
            };
        }
    }
}