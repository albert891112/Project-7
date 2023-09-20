using _7_Team_WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Models.ViewModels
{
    public class GenderCategoryVM
    {
        public int Id { get; set; }
        public bool Gender { get; set; }
    }

    public static class GenderCategoryExtenssion
    {
        public static GenderCategoryVM ToVM(this GenderCategoryDTO dto)
        {
            return new GenderCategoryVM
            {
                Id = dto.Id,
                Gender = dto.Gender,

            };
        }
    }
}