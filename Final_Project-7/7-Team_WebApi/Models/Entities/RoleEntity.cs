
using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class RoleEntityExtension
    {
        public static RoleEntity ToEntity(this RoleDTO dto)
        {
            return new RoleEntity()
            {
                Name = dto.Name
            };
        }
    }
}