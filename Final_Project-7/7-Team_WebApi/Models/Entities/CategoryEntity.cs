using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace _7_Team_WebApi.Models.Entities
{

    /// <summary>
    /// Category Entity model
    /// </summary>
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GenderCategoryEntity> GenderCategories { get; set; }
    }

    

    /// <summary>
    /// Extension methods for CategoryEntity , DTOs convert to Entity
    /// </summary>
    public static class CategoryEntityExtensions
    {

        /// <summary>
        /// DTO to Entity
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static CategoryEntity ToEntity(this CategoryDTO category)
        {
            return new CategoryEntity
            {
                Id = category.Id,
                Name = category.Name
            };
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static CategoryEntity ToEntity(this CategoryCreateDTO category , List<GenderCategoryEntity> gender)
        {
            return new CategoryEntity
            {
                Id = category.Id,
                Name = category.Name,
                GenderCategories = gender
            };
        }

      
        
    }
}