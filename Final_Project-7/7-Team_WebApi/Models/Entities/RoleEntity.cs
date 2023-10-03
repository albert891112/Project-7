﻿
using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }

    public static class RoleEntityExtension
    {
        public static RoleEntity ToEntity(this RoleDTO dto)
        {
            return new RoleEntity()
            {
                Id = dto.Id,
                RoleName = dto.Name
            };
        }


        public static RoleEntity ToEntity(this Role model)
        {
            return new RoleEntity()
            {
                Id = model.Id,
                RoleName = model.RoleName
            };
        }
    }
}