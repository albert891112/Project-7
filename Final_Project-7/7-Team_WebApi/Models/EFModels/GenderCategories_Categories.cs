namespace _7_Team_WebApi.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GenderCategories_Categories
    {
        public int Id { get; set; }

        public int GenderCategoryId { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual GenderCategory GenderCategory { get; set; }
    }
}
