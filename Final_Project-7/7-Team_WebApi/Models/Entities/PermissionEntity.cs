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
        public string PermissionName { get; set; }

        public string PermissionDescription { get; set; }
    }

    public static class PermissionExtension
    {
        public static PermissionEntity ToEntity(this PermissionDTO dto)
        {
            return new PermissionEntity()
            {
                Id = dto.Id,
                PermissionName = dto.Name,
                PermissionDescription = dto.Description
            };
        }


        public static PermissionEntity ToEntity(this Premission model)
        {
            return new PermissionEntity()
            {
                Id = model.Id,
                PermissionName = model.PermissionName,
                PermissionDescription = model.PermissionDescription
            };
        }
    }
}