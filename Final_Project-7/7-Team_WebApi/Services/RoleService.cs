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

        RoleRepository repo = new RoleRepository();

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="dto"></param>
        public void Create(RoleDTO dto)
        {
            RoleEntity entity = dto.ToEntity();

            this.repo.Create(entity);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RolePermissionDTO GetRolesPermission(int id)
        {
            List<Roles_PermissionsEntity> roles_Permissions = this.repo.GetPermissionByRoleId(id);

            RolePermissionDTO rolePermissionDTO = new RolePermissionDTO();

            rolePermissionDTO.Id = id;

            rolePermissionDTO.InGroup = new List<PermissionDTO>();
            rolePermissionDTO.OutOfGroup = new List<PermissionDTO>();

            foreach (var item in roles_Permissions)
            {
               if(item.Id == 0)
               {
                    //if role id is null, it means this permission is not in this role
                    rolePermissionDTO.OutOfGroup.Add(item.Permissions.ToDTO());
               }
               else
               {
                   
                    //if role id is not null, it means this permission is in this role
                    rolePermissionDTO.InGroup.Add(item.Permissions.ToDTO());
               }
            }
            
            return rolePermissionDTO;
            
        }
    }
}