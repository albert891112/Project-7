using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{

    /// <summary>
    /// Category Entity model
    /// </summary>
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
    }
}