﻿using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Dapper;

namespace Team_7_WebApi_Client.Repositories
{
    public class ProductRepository : IRepository<ProductEntity>
    {
        SqlDb connection = new SqlDb();

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

        public void Create(ProductEntity entity)
        {
            
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}