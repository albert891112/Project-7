using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _7_Team_WebApi.Services
{
    public class PermissionService
    {
        PermissionRepository repo= new PermissionRepository();


        /// <summary>
        /// Create a new permission
        /// </summary>
        /// <param name="dto"></param>
        public void Create(PermissionDTO dto)
        {
            PermissionEntity entity = dto.ToEntity();

            this.repo.Create(entity);
        }
    }
}