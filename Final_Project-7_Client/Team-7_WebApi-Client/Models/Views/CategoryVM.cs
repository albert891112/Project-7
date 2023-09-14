using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Views
{
    public class CategoryVM
    {
        public int  Id { get; set; }
        public string Name { get; set; }

    }

    public static class CategoryVMExtensions
    {
        public static CategoryVM ToVM(this CategoryDTO dto)
        {
            return new CategoryVM
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}