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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserEntity Get(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);

            UserEntity userEntity = user.ToEntity();

            return userEntity;
        }


        public User Get(string account)
        {
            User user = db.Users.FirstOrDefault(x => x.Account == account);

            return user;
        }



        /// <summary>
        /// update user
        /// </summary>
        /// <param name="user"></param>
        public void Update(UserEntity user)
        {
            string sql = @"UPDATE Users SET 
                    Name = CASE WHEN @Name IS NULL THEN Name ELSE @Name END,
                    Account = CASE WHEN @Account IS NULL THEN Account ELSE @Account END, 
                    Password = CASE WHEN @Password IS NULL THEN Password ELSE @Password END
                    WHERE Id = @Id";

            object obj = new
            {
                Id = user.Id,
                Name = user.Name,
                Account = user.Account,
                Password = user.Password
            };

            this.connection.Update(sql, "default" , obj);
        }
    
    }
}