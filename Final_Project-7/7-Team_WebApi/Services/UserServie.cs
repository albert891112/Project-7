using _7_Team_WebApi.Models.DTOs;
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
        /// Create a new user
        /// </summary>
        /// <param name="dto"></param>
        public void Create(UserDTO dto)
        {
            UserEntity entity = dto.ToEntity();

            this.repo.Create(entity);
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