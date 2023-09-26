using _7_Team_WebApi.Models.DTOs;
using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using _7_Team_WebApi.Models.Entities.PermissionControll;
using _7_Team_WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Dapper.SqlMapper;

namespace _7_Team_WebApi.Services
{
    public class RoleService
    {

        RoleRepository repo = new RoleRepository();


        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        public List<RoleDTO> GetAll()
        {
            List<RoleEntity> entities = this.repo.GetAll();

            List<RoleDTO> dtos = entities.Select(x => x.ToDTO()).ToList();

            return dtos;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleDTO Get(int id)
        {
            RoleEntity entity = this.repo.Get(id);

            RoleDTO dto = entity.ToDTO();

            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public void Update(RoleDTO dto)
        {
            RoleEntity entity = dto.ToEntity();

            this.repo.Update(entity);
        }

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="dto"></param>
        public void Create(RoleDTO dto)
        {
            Role existRole = this.repo.Get(dto.Name);

            if (existRole != null)
            {
                throw new Exception("Role name is exist");
            }

            RoleEntity entity = dto.ToEntity();

            this.repo.Create(entity);
        }



        /// <summary>
        /// Get all roles permission by role id
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


        /// <summary>
        /// Get all roles user by role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleUserDTO GetRolesUser(int id)
        {
            List<Roles_UsersEntity> roles_Users = this.repo.GetUserByRoleId(id);

            RoleUserDTO roleUserDTO = new RoleUserDTO();

            roleUserDTO.Id = id;

            roleUserDTO.InGroup = new List<UserDTO>();
            roleUserDTO.OutOfGroup = new List<UserDTO>();

            foreach (var item in roles_Users)
            {
                if (item.Id == 0)
                {
                    //if role id is null, it means this user is not in this role
                    roleUserDTO.OutOfGroup.Add(item.User.ToDTO());
                }
                else
                {
                    //if role id is not null, it means this user is in this role
                    roleUserDTO.InGroup.Add(item.User.ToDTO());
                }
            }

            return roleUserDTO;

        }
        
        /// <summary>
        /// Add permission to role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionId"></param>
        public void AddPermissionToRole(RoleUpdateDTO dto)
        {
            var entity = dto.ToEntity();

            this.repo.AddPermissionToRole(entity);
        }

        /// <summary>
        /// Delete permission from role
        /// </summary>
        public void DeletePermissionFromRole(RoleUpdateDTO dto)
        {

            var entity = dto.ToEntity();

            this.repo.DeletePermissionFromRole(entity);
        }

        /// <summary>
        /// Add user to role
        /// </summary>
        public void AddUserToRole(RoleUpdateDTO dto)
        {
            var entity = dto.ToEntity();

            this.repo.AddUserToRole(entity);
        }

        /// <summary>
        /// Delete user from role
        /// </summary>
        public void DeleteUserFromRole(RoleUpdateDTO dto)
        {
            var entity = dto.ToEntity();

            this.repo.DeleteUserFromRole(entity);
        }
    }
}