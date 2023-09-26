using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
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
            Premission permission = this.repo.Get(dto.Name);

            if(permission != null)
            {
                throw new Exception("Permission already exists");
            }

            PermissionEntity entity = dto.ToEntity();

            this.repo.Create(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PermissionDTO> GetAll()
        {
            List<PermissionEntity> entities = this.repo.GetAll();

            List<PermissionDTO> dtos = entities.Select(x => x.ToDTO()).ToList();

            return dtos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PermissionDTO Get(int id)
        {
            PermissionEntity entity = this.repo.Get(id);

            PermissionDTO dto = entity.ToDTO();

            return dto;
        }

        /// <summary>
        /// update permission
        /// </summary>
        /// <param name="dto"></param>
        public void Update(PermissionDTO dto)
        {
            PermissionEntity entity = dto.ToEntity();

            this.repo.Update(entity);
        }
    }
}