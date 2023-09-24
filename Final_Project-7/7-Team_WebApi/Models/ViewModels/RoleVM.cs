using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.SessionState;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class RoleVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }


    public class RolePermissionVM
    {
        public int Id { get; set; }
        public List<PermissionVM> InGroup { get; set; }
        public List<PermissionVM> OutOfGroup { get; set; }

    }

    public class RoleUserVM
    {
        public int Id { get; set; }
        public List<UserVM> InGroup { get; set; }
        public List<UserVM> OutOfGroup { get; set; }

    }


    public static class RoleVMExtenssion
    {
        public static RoleVM ToVM(this RoleDTO dto)
        {
            return new RoleVM()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static RolePermissionVM ToVM(this RolePermissionDTO dto)
        {
            return new RolePermissionVM()
            {
                Id = dto.Id,
                InGroup = dto.InGroup.Select(x => x.ToVM()).ToList(),
                OutOfGroup = dto.OutOfGroup.Select(x => x.ToVM()).ToList()
            };
        }

        public static RoleUserVM ToVM(this RoleUserDTO dto)
        {
            return new RoleUserVM()
            {
                Id = dto.Id,
                InGroup = dto.InGroup.Select(x => x.ToVM()).ToList(),
                OutOfGroup = dto.OutOfGroup.Select(x => x.ToVM()).ToList()
            };
        }
    }
}