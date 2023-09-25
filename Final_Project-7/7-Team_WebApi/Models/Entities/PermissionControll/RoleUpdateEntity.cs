using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities.PermissionControll
{
    public class RoleUpdateEntity
    {
        public int RoleId { get; set; }
        public int UpdateId { get; set; }
    }

    public static class RoleUpdateEntityExtenssion
    {
        public static RoleUpdateEntity ToEntity(this RoleUpdateDTO dto)
        {
            return new RoleUpdateEntity()
            {
                RoleId = dto.RoleId,
                UpdateId = dto.UpdateId
            };
        }
    }
}