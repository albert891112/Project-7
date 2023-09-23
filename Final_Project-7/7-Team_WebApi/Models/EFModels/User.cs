namespace _7_Team_WebApi.Models.EFModels
{
    using _7_Team_WebApi.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Users_Roles = new HashSet<Users_Roles>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Account { get; set; }

        [Required]
        [StringLength(70)]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users_Roles> Users_Roles { get; set; }
    }

    public static class UserExtension
    {
        public static User ToModel(this UserEntity dto)
        {
            return new User()
            {
                Account = dto.Account,
                Password = dto.Password,
                Name = dto.Name
            };
        }
    }
}
