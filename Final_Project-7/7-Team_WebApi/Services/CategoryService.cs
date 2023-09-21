using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Repositories;
using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Services
{
    public class CategoryService 
    {
        /// <summary>
        /// category repository
        /// </summary>
        CategoryRepository repo = new CategoryRepository();


        /// <summary>
        /// Category get all data 
        /// </summary>
        /// <returns></returns>
		public List<CategoryDTO> GetAll()
        {

            List<CategoryEntity> entities = this.repo.GetAll();

            List<CategoryDTO> dtos = entities.Select(x => x.ToDTO()).ToList();

            return dtos;

        }


        /// <summary>
        /// Get Categories by Gender
        /// </summary>
        /// <param name="Gender"></param>
        /// <returns></returns>
        public List<CategoryDTO> GetByGender(int Gender)
        {
            List<CategoryEntity> entities = this.repo.GetAll();

            //get category that GenderCategory contain Gender
            entities = entities.Select(x => x).Where(x => 
            {
                bool result = false;
                //if GenderCategory contain gender , set result to true
                foreach(var item in x.GenderCategories)
                {
                    if(item.Gender == Gender)
                    {
                        result = true;
                    }
                }
                return result;

            }).ToList();

            List<CategoryDTO> dtos = entities.Select(x => x.ToDTO()).ToList();

            return dtos;
        }


        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryDTO Get(int id)
        {
            CategoryEntity entity = this.repo.Get(id);

            CategoryDTO dto = entity.ToDTO();

            return dto;
        }


        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="dto"></param>
        public void Create(CategoryCreateDTO dto)
        {
            List<GenderCategoryEntity> genderCategories = dto.Gender.Select(x => new GenderCategoryEntity
            {
                Id = x

            }).ToList();
            
            CategoryEntity entities = dto.ToEntity(genderCategories);

            this.repo.Create(entities);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public void Update(CategoryCreateDTO dto)
        {
            List<GenderCategoryEntity> genderCategories = dto.Gender.Select(x => new GenderCategoryEntity
            {
                Id = x

            }).ToList();

            CategoryEntity entities = dto.ToEntity(genderCategories);

            this.repo.Update(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var product = new ProductRepository().Search(new ProductSearchEntity { CategoryId = id });
            
            if(product.Count != 0)
            {
                throw new Exception("Category is using");
            }
            else
            {
                this.repo.Delete(id);
            }
        }

    }
}