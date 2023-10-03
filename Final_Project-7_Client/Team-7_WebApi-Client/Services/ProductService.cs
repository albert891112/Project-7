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


        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductDTO Get(int id)
        {
            ProductEntity product = this.repo.Get(id);

            ProductDTO productDTO = product.ToDTO();

            return productDTO;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public List<ProductDTO> GetAll()
        {
            List<ProductEntity> products = repo.GetAll();

            List<ProductDTO> productDTOs = products.Select(p => p.ToDTO()).ToList();

            return productDTOs;
        }


        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ProductDTO> Search(ProductSearchDTO dto)
        {
            ProductSearchEntity entity = dto.ToEntity();

            List<ProductEntity> products = this.repo.Search(entity);

            return products.Select(p => p.ToDTO()).ToList();
        }


        /// <summary>
        /// Get Product sales Ranking
        /// </summary>
        /// <returns></returns>
        public List<ProductDTO> GetSalesRank(int bestNum)
        {
            List<ProductRankingEntity> rankProduct = this.repo.GetProductRanking().ToList();

            //if bestNum is bigger than the number of products, set bestNum to the number of products
            if(bestNum > rankProduct.Count)
            {
                bestNum = rankProduct.Count;
            }

            var dtos = rankProduct.Take(bestNum).Select(e => e).ToList();

            List<ProductEntity>  entities = new List<ProductEntity>();

            //convert ProductRankingEntity to ProductEntity
            foreach (var dto in dtos)
            {
                var product = this.repo.Get(dto.Id);
                entities.Add(product);
            }

            return entities.Select(e => e.ToDTO()).ToList();

            
        }

    }
}