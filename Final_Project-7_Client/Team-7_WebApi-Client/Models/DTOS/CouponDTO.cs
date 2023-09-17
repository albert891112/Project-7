using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class CouponDTO
	{
		public int Id { get; set; }
		public string CouponCode { get; set; }
		public string CouponName { get; set; }
		public DiscountTypeDTO Discount { get; set; }
		public decimal DiscountValue { get; set; }
		public string CouponDescription { get; set; }
		public DateTime ExpiratinDate { get; set; }
		public int UsageCount { get; set; }
		public bool Enable { get; set; }
		public string Image { get; set; }
	}

	public class DiscountTypeDTO
	{
		public int Id { get; set; }
		public string Type { get; set; }
	}


	public static class CouponDTOExtenssion
	{
		public static CouponDTO ToDTO(this CouponEntity entity)
		{
			return new CouponDTO
			{
				Id = entity.Id,
				CouponCode = entity.CouponCode,
				CouponName = entity.CouponName,
				Discount = entity.Discount.ToDTO(),
				DiscountValue =entity.DiscountValue,
				CouponDescription = entity.CouponDescription,
				ExpiratinDate =entity.ExpiratinDate,
				UsageCount = entity.UsageCount,
				Enable =entity.Enable,
				Image = entity.Image,			
			};
		}
	}


	public static class DiscountTypeDTOExtenssion
	{
		public static DiscountTypeDTO ToDTO(this DiscountTypeEntity entity)
		{
			return new DiscountTypeDTO
			{
				Id = entity.Id,				
				Type = entity.Type,				
			};
		}
	}
}