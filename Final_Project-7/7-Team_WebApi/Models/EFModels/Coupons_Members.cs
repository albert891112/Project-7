namespace _7_Team_WebApi.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Coupons_Members
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int CouponId { get; set; }

        public bool Enabled { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Member Member { get; set; }
    }
}
