using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Services
{
    public class RoleService
    {

        RoleRepsitory repo = new RoleRepsitory();

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="dto"></param>
        public void Create(RoleDTO dto)
        {
            RoleEntity entity = dto.ToEntity();

            this.repo.Create(entity);
        }
    }
}