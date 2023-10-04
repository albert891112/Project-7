using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Repositories
{
    public class ReviewRepository
    {

        SqlDb connection = new SqlDb();
        AppDbContext db = new AppDbContext();

        public ReviewEntity Get(int ProductId)
        {
            string sql = @"SELECT * FROM Reviews as r 
                        INNER JOIN Members as m ON m.Id = r.MemberId
                        INNER JOIN Products as p ON p.Id = r.ProductId
                        INNER JOIN Categories as c ON c.Id = p.CategoryId
                        INNER JOIN Stocks as s ON s.Id = p.StockId
                        INNER JOIN GenderCategories as g ON g.Id = p.GenderId
                        WHERE r.ProductId = @Id";

            return new ReviewEntity();
        }

    }
}