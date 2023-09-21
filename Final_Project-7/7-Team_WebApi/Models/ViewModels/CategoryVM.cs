using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GenderCategoryVM> GenderCategories { get; set; }

    }

    public class CategoryCreateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Gender { get; set; }
    }

    public static class CategoryVMExtensions
    {
        public static CategoryVM ToVM(this CategoryDTO dto)
        {
            return new CategoryVM
            {
                Id = dto.Id,
                Name = dto.Name,
                GenderCategories = dto.GenderCategories.Select(x => x.ToVM()).ToList()
            };
        }
    }
}