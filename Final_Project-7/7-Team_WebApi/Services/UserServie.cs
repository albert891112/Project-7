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
using System.Web.Security;

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




        /// <summary>
        /// 找出使用者是否存在。若是沒有會回傳false，有的話會比對密碼是否正確? 回傳true : 回傳false
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsValid(string account, string password)
        {
            var user = this.repo.GetByAccount(account);

            var salt = Hashing.GetSalt();
            var HashedPassword = Hashing.ToSHA256(password, salt);

            if (user == null) return false;
            else
                return String.Compare(user.Password.Trim(), HashedPassword, StringComparison.OrdinalIgnoreCase) == 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="rememberMe"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public string ProcessLogin(string account, bool rememberMe, out HttpCookie cookie)
        {

            UserPermissionDTO dto = this.repo.GetByAccount(account).ToDTO();

            string functions = dto.Permissions;

            //建立一張認證票
            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(
                    1,          //版本別, 沒特別用處
                    account,
                    DateTime.Now,   //發行日
                    DateTime.Now.AddDays(2), //到期日
                    rememberMe,     //是否續存
                    functions,          //userdata
                    "/"             //cookie位置
                );
            //將它加密
            string value = FormsAuthentication.Encrypt(ticket);
            //存入cookie
            cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

            //取得return url
            string url = FormsAuthentication.GetRedirectUrl(account, true); //第二個引數沒有用處

            return url;

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