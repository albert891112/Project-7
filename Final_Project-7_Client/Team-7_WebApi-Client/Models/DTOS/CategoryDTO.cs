using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
    public class CategoryDTO
    {
        public int  Id { get; set; }
        public string Name { get; set; }

    }

    public static class CategoryDTOExtensions
    {
        public static CategoryDTO ToDTO(this CategoryEntity entity)
        {
            return new CategoryDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

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