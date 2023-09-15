using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.Entities
{
    public class GenderCategoryDTO
    {
        public int Id { get; set; }
        public bool Gender { get; set; }
    }

    public static class GenderCategoryDTOEntenssion
    {
        public static GenderCategoryDTO ToDTO(this GenderCategoryEntity entity)
        {
            return new GenderCategoryDTO
            {
                Id =  entity.Id,
                Gender = entity.Gender,
            };
        }

        public static GenderCategoryDTO ToDTO(this GenderCategoryVM vm)
        {
            return new GenderCategoryDTO
            {
                Id = vm.Id,
                Gender = vm.Gender,
            };
        }
    }
}