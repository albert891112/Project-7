using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Repositories;

namespace Team_7_WebApi_Client.Services
{
    public class CategoryService
    {

        CategoryRepository repo = new CategoryRepository();

        /// <summary>
        /// get category by Gender
        /// </summary>
        /// <param name="Gender"></param>
        /// <returns></returns>
        public List<CategoryDTO> Get(int Gender)
        {
            List<CategoryEntity> entities = this.repo.Get(Gender);

            List<CategoryDTO> dtos = entities.Select(x => x.ToDTO()).ToList();

            return dtos;
        }
    }
}