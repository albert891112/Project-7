namespace Team_7_WebApi_Client.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Premission")]
    public partial class Premission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Premission()
        {
            Roles_Permissions = new HashSet<Roles_Permissions>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PermissionName { get; set; }

        [Required]
        [StringLength(200)]
        public string PermissionDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Roles_Permissions> Roles_Permissions { get; set; }
    }
}
