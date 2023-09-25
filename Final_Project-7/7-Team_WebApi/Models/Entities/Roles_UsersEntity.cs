using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class Roles_UsersEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public UserEntity  User { get; set; }
    }
}