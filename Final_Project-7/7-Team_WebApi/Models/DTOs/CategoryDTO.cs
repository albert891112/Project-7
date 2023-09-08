using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.DTOs
{

    /// <summary>
    /// Category DTO model
    /// </summary>
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }



    /// <summary>
    /// Extension methods for CategoryDTO , Models convert to DTO
    /// </summary>
    public static class CategoryDTOExtensions
    {

        /// <summary>
        /// Entity to DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static CategoryDTO ToDTO(this CategoryEntity entity)
        {
            return new CategoryDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        /// <summary>
        /// ViewModel to DTO
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static CategoryDTO ToDTO(this CategoryVM vm)
        {
            return new CategoryDTO
            {
                Id = vm.Id,
                Name = vm.Name
            };
        }
    }
    
}