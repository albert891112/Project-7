namespace Team_7_WebApi_Client.Models.EFModels
{
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Remoting.Messaging;
    using Team_7_WebApi_Client.Models.Entities;

    public partial class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        [Required]
        [StringLength(50)]
        public string Size { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual Product Product { get; set; }
    }

    public static class CartItemExtenssion
    {
        public static CartItem ToModel(this CartItemCreateEntity entity)
        {
            return new CartItem
            {
                Id = entity.Id,
                CartId = entity.CartId,
                ProductId = entity.ProductId,
                Qty = entity.Qty,
                Size = entity.Size
            };
        }
    }
}
