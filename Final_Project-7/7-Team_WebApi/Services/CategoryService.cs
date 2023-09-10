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
    public class CategoryService : IService<CategoryDTO>
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
        public void Create(CategoryDTO dto)
        {
            CategoryEntity entities = dto.ToEntity();

            this.repo.Create(entities);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public void Update(CategoryDTO dto)
        {

            CategoryEntity entities = dto.ToEntity();

            this.repo.Update(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

    }
}