using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.Views
{
    public class GenderCategoryVM
    {
        public int Id   { get; set; }
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