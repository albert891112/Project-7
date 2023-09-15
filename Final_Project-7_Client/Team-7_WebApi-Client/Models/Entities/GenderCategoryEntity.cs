using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Web;

namespace Team_7_WebApi_Client.Models.Entities
{
    public class GenderCategoryEntity
    {
        public int Id { get; set; }
        public bool Gender { get; set; }
    }

    public static class GenderCategoryEntityExtenssion
    {
        public static GenderCategoryEntity ToEntit(this GenderCategoryDTO dto)
        {
            return new GenderCategoryEntity
            {
                Id = dto.Id,
                Gender = dto.Gender,
            };
        }
    }
}