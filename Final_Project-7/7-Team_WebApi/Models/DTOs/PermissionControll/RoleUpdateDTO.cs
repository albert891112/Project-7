using _7_Team_WebApi.Models.ViewModels.PermissionControll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{
    public class RoleUpdateDTO
    {
        public int RoleId { get; set; }
        public int UpdateId { get; set; }
    }

    public static class RoleUpdateDTOExtenssion
    {
        public static RoleUpdateDTO ToDTO(this RoleUpdateVM vm)
        {
            return new RoleUpdateDTO()
            {
                RoleId = vm.RoleId,
                UpdateId = vm.UpdateId
            };
        }
    }
}