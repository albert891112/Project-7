using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
    public class CategoryEntity
    {
        public int  Id { get; set; }

        public string  Name { get; set; }
        public List<GenderCategoryEntity> GenderCategories { get; set; }
    }

    public static class CategoryEntityExtensions
    {
        public static CategoryEntity ToEntity(this CategoryDTO dto)
        {
            return new CategoryEntity
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}