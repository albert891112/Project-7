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

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        public void Create(UserEntity user)
        {
            User newUser = user.ToModel();

            db.Users.Add(newUser);

            db.SaveChanges();
        }


        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public List<UserEntity> GetAll()
        {
            List<User> users = db.Users.ToList();

            List<UserEntity> userEntities = users.Select(x => x.ToEntity()).ToList();

            return userEntities;
        }   
    
    }
}