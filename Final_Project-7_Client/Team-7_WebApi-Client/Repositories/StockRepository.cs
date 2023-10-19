using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Repositories
{
    public class StockRepository
    {
        SqlDb connection = new SqlDb();

        AppDbContext db = new AppDbContext();

        public Stock GetByStockId(int ProductId)
        {
            var stock = db.Stocks.FirstOrDefault(s => s.ProductId == ProductId);

            return stock;
        }
    }
}