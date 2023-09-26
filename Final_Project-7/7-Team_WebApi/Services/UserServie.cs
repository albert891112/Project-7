using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace _7_Team_WebApi.Services
{
    
    public class UserServie
    {
        UserRepository repo = new UserRepository();

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public List<UserDTO> GetAll()
        {
            List<UserEntity> entities = this.repo.GetAll();

            List<UserDTO> dtos = entities.Select(x => x.ToDTO()).ToList();

            return dtos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDTO Get(int id)
        {
            UserEntity entity = this.repo.Get(id);

            UserDTO dto = entity.ToDTO();

            return dto;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="dto"></param>
        public void Create(UserDTO dto)
        {
            User user = this.repo.Get(dto.Account);

            if(user == null)
            {
                string salt = Hashing.GetSalt();

                string HashPassword = Hashing.ToSHA256(dto.Password, salt);

                dto.Password = HashPassword;

                UserEntity entity = dto.ToEntity();

                this.repo.Create(entity);
            }
            else
            {
                throw new Exception("Account already exists");
            }
            
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public void Update(UserDTO dto)
        {
            string Password = dto.Password;

            UserEntity entity = new UserEntity();

            //if password is null or empty, don't update password
            if (String.IsNullOrEmpty(Password))
            {
                dto.Password = null;
                entity = dto.ToEntity();
            }
            else
            {
                string salt = Hashing.GetSalt();

                string HashedPassword = Hashing.ToSHA256(Password, salt);

                dto.Password = HashedPassword;

                entity = dto.ToEntity();
            }

            this.repo.Update(entity);
        }   

    }

    public static class Hashing
    {
        //hashpassword
        public static string ToSHA256(string plainText, string salt)
        {
            using (var mySHA256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(plainText + salt);
                var hash = mySHA256.ComputeHash(passwordBytes);
                var sb = new StringBuilder();
                foreach (var key in hash) { sb.Append(key.ToString("X2")); }

                return sb.ToString();
            }
        }

        //get salt

        public static string GetSalt()
        {
            return System.Configuration.ConfigurationManager.AppSettings["salt"];
        }
    }
}