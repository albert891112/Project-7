using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;
using Team_7_WebApi_Client.Repositories;

namespace Team_7_WebApi_Client.Services
{
    public class ProductService
    {
        ProductRepository repo = new ProductRepository();

        public List<ProductDTO> Search(ProductSearchVM vm)
        {

            List<ProductEntity> entities = repo.Search();

            List<ProductDTO> dtos = entities.Select(x => x.ToDTO()).ToList();

            return dtos;
        }

        /// <summary>
        /// Get Product by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ProductDTO Get(int Id)
        {
            ProductDTO dtos = repo.Get(Id).ToDTO();

            return dtos;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public List<ProductDTO> GetAll()
        {
            List<ProductEntity> entities = this.repo.GetAll();

            List<ProductDTO> dtos = entities.Select(x => x.ToDTO()).ToList();

            return dtos;
        }




        

    }
}