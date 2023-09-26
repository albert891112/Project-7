using _7_Team_WebApi.Models.EFModels;
using _7_Team_WebApi.Models.Entities;
using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Dapper.SqlMapper;

namespace _7_Team_WebApi.Repositories
{
    public class PermissionRepository
    {
        SqlDb connection = new SqlDb();

        AppDbContext db = new AppDbContext();

        /// <summary>
        /// Create a new permission
        /// </summary>
        /// <param name="permission"></param>
        public void Create(PermissionEntity permission)
        {
            Premission newPermission = permission.ToModel();
            
            db.Premissions.Add(newPermission);

            db.SaveChanges();
        }


        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <returns></returns>
        public List<PermissionEntity> GetAll()
        {
            List<Premission> permissions = db.Premissions.ToList();
            
            List<PermissionEntity> entities = permissions.Select(x => x.ToEntity()).ToList();   

            return entities;
        }


        /// <summary>
        /// Get permission by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PermissionEntity Get(int id)
        {
            PermissionEntity permission = db.Premissions.FirstOrDefault(p => p.Id == id).ToEntity();

            return permission;
        }

        /// <summary>
        /// Get permission by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Premission Get(string name)
        {
            Premission permission = db.Premissions.FirstOrDefault(p => p.PermissionName == name);

            return permission;
        }


        /// <summary>
        /// Update permission
        /// </summary>
        /// <param name="permission"></param>
        public void Update(PermissionEntity permission)
        {
            Premission exist = db.Premissions.FirstOrDefault(p => p.Id == permission.Id);

            Premission  newPermission = permission.ToModel();

            newPermission.Id = exist.Id;
         
            db.Entry(exist).CurrentValues.SetValues(newPermission);

            db.SaveChanges();
        }

        /// <summary>
        /// Delete permission
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            Premission permissionToDelete = db.Premissions.FirstOrDefault(p => p.Id == id);

            db.Premissions.Remove(permissionToDelete);

            db.SaveChanges();
        }
    }
}