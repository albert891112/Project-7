using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Dapper;

namespace Team_7_WebApi_Client.Repositories
{
    public class CategoryRepository
    {
        SqlDb connection = new SqlDb();

        //Get Category by Gender 
        public List<CategoryEntity> Get(int Gender)
        {
            string sql = "SELECT C.* FROM GenderCategories as G " +
                "INNER JOIN GenderCategory_Category as GC ON G.Id = GC.GenderCategoryId " +
                "INNER JOIN Categories as C ON C.Id = CategoryId " +
                "WHERE G.Gender = @Gender;";

            object obj = new
            {
                Gender = Gender,
            };


            Func<SqlConnection, string, object, List<CategoryEntity>> func = (conn, s, o) =>
            {
                return conn.Query<CategoryEntity>(s, o).ToList();
            };


            List<CategoryEntity> entity = this.connection.Get<List<CategoryEntity>>(sql, "default", obj, func);

            return entity;
        }
    }
}