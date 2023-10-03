﻿using _7_Team_WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _7_Team_WebApi.Services;
using _7_Team_WebApi.Models.Entities;

namespace _7_Team_WebApi.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }        
        public string Name { get; set; }
    }

    public class UserPermissionDTO
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }

        public string Permissions { get; set; }
    }

    public class UserRoleDTO
    {
        public int Id { get; set; }
        public string Account { get; set; }

        public List<RoleDTO> Role { get; set; }

    }

    public static class UserRegisterDTOExtension
    {
        public static UserDTO ToDTO(this UserVM vm)
        {


            return new UserDTO()
            {
                Id = vm.Id,
                Account = vm.Account,
                Password = vm.Password,
                Name = vm.Name
            };
        }

        public static UserRoleDTO ToDTO(this UserRoleEntity entity)
        {
            return new UserRoleDTO()
            {
                Id = entity.Id,
                Account = entity.Account,
                Role = entity.Role.Select(x => x.ToDTO()).ToList()
            };
        }


        public static UserDTO ToDTO(this UserEntity entity)
        {
            return new UserDTO()
            {
                Id = entity.Id,
                Account = entity.Account,
                Name = entity.Name
            };
        }



        public static UserPermissionDTO ToDTO(this UserPermissionsEntity entity)
        {
            string permissions = entity.Permission.Select(x => x.Id.ToString()).Aggregate(string.Empty, (current, next) => current + "," + next);

            return new UserPermissionDTO
            {
                Id = entity.Id,
                Account = entity.Account,
                Password = entity.Password,
                Permissions = permissions
            };
        }
    }
}