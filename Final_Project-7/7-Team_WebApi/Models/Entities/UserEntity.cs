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


    public static class UserRegisterEntityExtension
    {
        public static UserEntity ToEntity(this UserDTO dto)
        {

            string salt = Hashing.GetSalt();

            string HashedPassword = Hashing.ToSHA256(dto.Password, salt);

            return new UserEntity()
            {
                Account = dto.Account,
                Password = HashedPassword,
                Name = dto.Name
            };
        }

        public static UserEntity ToEntity(this User model)
        {
            string salt = Hashing.GetSalt();

            string HashedPassword = Hashing.ToSHA256(model.Password, salt);

            return new UserEntity()
            {
                Account = model.Account,
                Password = HashedPassword,
                Name = model.Name
            };
        }
    }
}