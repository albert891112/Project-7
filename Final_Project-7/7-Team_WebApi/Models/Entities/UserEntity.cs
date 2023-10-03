using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }

    public class UserPermissionsEntity
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }

        public List<PermissionEntity> Permission { get; set; }
    }

    public class UserRoleEntity 
    {
        public int Id { get; set; }
        public string Account { get; set; }

        public List<RoleEntity> Role { get; set; }

    }




    public static class UserRegisterEntityExtension
    {
        public static UserEntity ToEntity(this UserDTO dto)
        {

            return new UserEntity()
            {
                Id = dto.Id,
                Account = dto.Account,
                Password = dto.Password,
                Name = dto.Name
            };
        }

        public static UserEntity ToEntity(this User model)
        {
            string salt = Hashing.GetSalt();

      

            return new UserEntity()
            {
                Id = model.Id,
                Account = model.Account,
                Name = model.Name
            };
        }
    }
}