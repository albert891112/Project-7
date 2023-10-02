using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using Albert.Lib;

namespace _7_Team_WebApi.Services
{
    public class ProductService
    {
        ProductRepository repo = new ProductRepository();

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductDTO Get(int id)
        {
            ProductEntity product = this.repo.Get(id);

            var dto = product.ToDTO();

            return dto;
        }

        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns></returns>
        public List<ProductDTO> GetAll()
        {
            List<ProductEntity> products = this.repo.GetAll();

            var dtos = products.Select(p => p.ToDTO()).ToList();

            return dtos;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="dto"></param>
        public void Create(ProductUploadDTO dto)
        {
            HttpPostedFile Image = dto.Image;

            string fileName = String.Empty;
            string path = HttpContext.Current.Server.MapPath("~/File/");
            string destRelativePath = @"..\..\..\Final_Project-7_Client\Team-7_WebApi-Client\Files";

            IFileValidator[] validator = new IFileValidator[]
            {
                //檔案是必須的
                new FileRequired(),
                //照片必須是圖片
                new ImageValidator(),
            };

            try
            {

                fileName = UploadFileHelper.Save(Image, path, validator);

                UploadFileHelper.Copy(path, destRelativePath, fileName);

            }
            catch (Exception ex)
            {
                throw ex;
            }



            ProductUploadEntity entity = dto.ToEntity(fileName);

            this.repo.Create(entity);
        }   

        /// <summary>
        /// Update Stock
        /// </summary>
        /// <param name="stock"></param>
        public void UpdateStock(StockUploadEntity stock)
        {
            StockRepository stockRepository = new StockRepository();

            stockRepository.Update(stock);
        }
    
    
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="dto"></param>
        public void Update(ProductUploadDTO dto)
        {
            HttpPostedFile Image = dto.Image;

            string fileName = String.Empty;
            string path = HttpContext.Current.Server.MapPath("~/File/");
            string destRelativePath = @"..\..\..\Final_Project-7_Client\Team-7_WebApi-Client\Files";

            IFileValidator[] validator = new IFileValidator[]
            {
                //照片必須是圖片
                new ImageValidator(),
            };
            
            try
            {
                fileName = UploadFileHelper.Save(Image, path, validator);

                UploadFileHelper.Copy(path, destRelativePath, fileName);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            ProductUploadEntity entity = dto.ToEntity(fileName);

            this.repo.Update(entity);

        }
    
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ProductDTO> Search(ProductSearchDTO dto)
        {
            List<ProductEntity> entities = this.repo.Search(dto.ToEntity());

            var result = entities.Select(x => x.ToDTO()).ToList();

            return result;
        }   
    }
}