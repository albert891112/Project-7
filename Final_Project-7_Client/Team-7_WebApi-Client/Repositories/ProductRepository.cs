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

            //Delegate for get all products
            Func<SqlConnection, string, object, ProductEntity> func = (conn, s, o) =>
            {
                ProductEntity p = null;

                conn.Query<ProductEntity, CategoryEntity, StockEntity, ProductEntity>(s, (product, category, stock) =>
                {
                    if (p ==  null)
                    {
                        product.Stock = stock;
                        product.Categories = new List<CategoryEntity>
                        {
                            category
                        };
                        p = product;
                    }
                    else
                    {
                        p.Categories.Add(category);
                    }
                    return p;

                }, o);

                return p;
            };


            ProductEntity entity = this.connection.Get<ProductEntity>(sql, "defualt", obj, func);

            return entity;
        }


        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public List<ProductEntity> GetAll()
        {
            string sql = "SELECT p.* , C.* , S.* " +
                "FROM Products as P " +
                "INNER JOIN " +
                "(Categroysies_Products as CP INNER JOIN Categories as C ON CP.CategoryId = C.Id)  " +
                "ON P.id = CP.ProductId " +
                "INNER JOIN Stock as S ON S.Id = P.StockId " +
                "ORDER BY P.id";

            //Delegate for get all products
            Func<SqlConnection, string, List<ProductEntity>> func = (conn, s) =>
            {
                Dictionary<int, ProductEntity> products = new Dictionary<int, ProductEntity>();

                conn.Query<ProductEntity, CategoryEntity, StockEntity, ProductEntity>(s, (product, category, stock) =>
                {
                    if (!products.TryGetValue(product.Id, out ProductEntity p))
                    {
                        product.Stock = stock;
                        product.Categories = new List<CategoryEntity>
                        {
                            category
                        };
                        products.Add(product.Id, product);
                    }
                    else
                    {
                        p.Categories.Add(category);
                    }
                    return p;

                });

                return products.Values.ToList();
            };

            //Get all products
            List<ProductEntity> entities = this.connection.GetAll<ProductEntity>(sql, "defualt", func);

            return entities;
        }


        /// <summary>
        /// Search product 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public List<ProductEntity> Search(ProductSearchEntity entity)
        //{
        //    string sql = "SELECT p.* , C.* , S.* " +
        //         "FROM Products as P " +
        //         "INNER JOIN " +
        //         "(Categroysies_Products as CP INNER JOIN Categories as C ON CP.CategoryId = C.Id)  " +
        //         "ON P.id = CP.ProductId " +
        //         "INNER JOIN Stock as S ON S.Id = P.StockId " +
        //         "WHERE (@CategoryId IS NUll OR C.Id = @CategoryId) " +
        //         "AND (@Name IS NULL OR P.Name LIKE '%' + @Name +'%') " +
        //         "AND (@Pricelow IS NULL OR P.Price >= @Pricelow) " +
        //         "AND (@Pricehight IS NULL OR P.Price <= @PriceHight) " +
        //         "AND (P.Enable = true)" +
        //         "Order By p.Id";

        //    object obj = new
        //    {
        //        categoryId = entity.Categories.Id,
        //        name = entity.Name,
        //        pricelow = entity.LowPrice,
        //        pricehight = entity.HeightPrice,

        //    };

        //    //delegate for Query
        //    Func<SqlConnection, string, object, List<ProductEntity>> func = (conn, pro, o) =>
        //    {
        //        Dictionary<int, ProductEntity> products = new Dictionary<int, ProductEntity>();
        //        conn.Query<ProductEntity, CategoryEntity, StockEntity, ProductEntity>(pro, (product, category, stock) =>
        //        {
        //            if (!products.TryGetValue(product.Id, out ProductEntity p))
        //            {
        //                product.Stock = stock;
        //                product.Categories = new List<CategoryEntity>();
        //                product.Categories.Add(category);
        //                products.Add(product.Id, product);
        //            }
        //            else
        //            {
        //                p.Categories.Add(category);
        //            }
        //            return p;

        //        }, o, commandType: CommandType.StoredProcedure);

        //        return products.Values.ToList();
        //    };

        //    List<ProductEntity> result = connection.Search<List<ProductEntity>>(procedure, "default", obj, func);

        //    return result;
        //}

    }
}