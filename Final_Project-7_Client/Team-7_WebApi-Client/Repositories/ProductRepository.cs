using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Dapper;
using Team_7_WebApi_Client.Models.Views;
using System.Data;
using System.Reflection;

namespace Team_7_WebApi_Client.Repositories
{
    public class ProductRepository 
    {
        SqlDb connection = new SqlDb();


        /// <summary>
        /// Get Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductEntity Get(int id)
        {
            string sql = "SELECT p.* , C.* , S.* " +
                "FROM Products as P " +
                "INNER JOIN " +
                "(Categroysies_Products as CP INNER JOIN Categories as C ON CP.CategoryId = C.Id)  " +
                "ON P.id = CP.ProductId " +
                "INNER JOIN Stock as S ON S.Id = P.StockId " +
                "WHERE P.Id = @Id " +
                "ORDER BY P.id";

            object obj = new
            {
                Id = id
            };

      

            return new ProductEntity();
        }


        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public List<ProductEntity> GetAll()
        {
            string sql = "SELECT P.* , GC.* , C.* FROM Products as P " +
                "INNER JOIN GenderCategories as GC ON P.GenderId = GC.Id " +
                "INNER JOIN Categories as C ON P.CategoryId = C.Id;";

           

                return new List<ProductEntity>();
 
         
        }


        /// <summary>
        /// Search product 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<ProductEntity> Search(ProductSearchEntity entity)
        {
            string sql = "SELECT P.* , GC.* , C.* FROM Products as P " +
                "INNER JOIN GenderCategories as GC ON P.GenderId = GC.Id " +
                "INNER JOIN Categories as C ON P.CategoryId = C.Id;";

            object obj = new
            {
             
                name = entity.Name,
                pricelow = entity.LowPrice,
                pricehight = entity.HeightPrice,

            };

            return new List<ProductEntity>();
        }

    }
}