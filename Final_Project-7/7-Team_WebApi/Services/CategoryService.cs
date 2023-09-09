using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
using _7_Team_WebApi.Repositories;
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
        /// Create new category
        /// </summary>
        /// <param name="dto"></param>
        public void Create(CategoryDTO dto)
        {
            CategoryEntity entities = dto.ToEntity();

            this.repo.Create(entities);
        }


        public void Edit(CategoryDTO dto)
        {

            CategoryEntity entities = dto.ToEntity();

            this.repo.Update(entities);
        }

    }
}