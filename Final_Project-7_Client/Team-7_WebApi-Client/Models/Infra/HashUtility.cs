using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Team_7_WebApi_Client.Models.Infra
{
    public static class HashUtility
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