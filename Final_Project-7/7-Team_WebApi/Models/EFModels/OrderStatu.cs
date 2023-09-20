namespace _7_Team_WebApi.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderStatu
    {
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string Status { get; set; }

        public int DisplayOrder { get; set; }
    }
}
