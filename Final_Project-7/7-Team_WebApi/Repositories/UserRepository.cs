using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Repositories
{
    public class UserRepository
    {
        SqlDb connection = new SqlDb();

        AppDbContext db = new AppDbContext();


        public void Create(UserEntity user)
        {
            User newUser = user.ToModel();

            db.Users.Add(newUser);

            db.SaveChanges();
        }
    
    }
}