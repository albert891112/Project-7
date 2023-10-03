namespace _7_Team_WebApi.Models.EFModels
{
    using _7_Team_WebApi.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            Roles_Permissions = new HashSet<Roles_Permissions>();
            Users_Roles = new HashSet<Users_Roles>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Roles_Permissions> Roles_Permissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users_Roles> Users_Roles { get; set; }
    }


    public static class RoleExtension
    {
        public static Role ToModel(this RoleEntity role)
        {
            return new Role()
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }
    }
}
