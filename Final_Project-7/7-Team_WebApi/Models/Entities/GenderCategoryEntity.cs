using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.Entities
{
    public class GenderCategoryEntity
    {
        public int Id { get; set; }
        public int Gender { get; set; }
        public List<CategoryEntity> Category { get; set; }
    }

    public static class GenderCategoryEntityExtenssion
    {
        public static GenderCategoryEntity ToEntity(this GenderCategoryDTO dto)
        {
            return new GenderCategoryEntity
            {
                Id = dto.Id,
                Gender = dto.Gender
            };
        }

        
    }
}