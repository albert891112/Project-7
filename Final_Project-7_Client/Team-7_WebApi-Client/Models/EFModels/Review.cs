namespace Team_7_WebApi_Client.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(10)]
        public string ProductSize { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public DateTime ReviewTime { get; set; }

        public DateTime OrderTime { get; set; }

        public virtual Member Member { get; set; }

        public virtual Product Product { get; set; }
    }
}
