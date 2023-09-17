using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.Views
{
	public class CouponVM
	{
		public int Id { get; set; }
		public string CouponCode { get; set; }
		public string CouponName { get; set; }
		public DiscountTypeVM Discount { get; set; }
		public decimal DiscountValue { get; set; }
		public string CouponDescription { get; set; }
		public DateTime ExpiratinDate { get; set; }
		public int UsageCount { get; set; }
		public bool Enable { get; set; }
		public string Image { get; set; }
	}

	public class DiscountTypeVM
	{
		public int Id { get; set; }
		public string Type { get; set; }
	}


	public static class CouponDTOExtenssion
	{
		public static CouponVM ToVM(this CouponDTO dto)
		{
			return new CouponVM
			{
				Id = dto.Id,
				CouponCode = dto.CouponCode,
				CouponName = dto.CouponName,
				Discount = dto.Discount.ToVM(),
				DiscountValue = dto.DiscountValue,
				CouponDescription = dto.CouponDescription,
				ExpiratinDate = dto.ExpiratinDate,
				UsageCount = dto.UsageCount,
				Enable = dto.Enable,
				Image = dto.Image,
			};
		}
	}


	public static class DiscountTypeVMExtenssion
	{
		public static DiscountTypeVM ToVM(this DiscountTypeDTO dto)
		{
			return new DiscountTypeVM
			{
				Id = dto.Id,
				Type = dto.Type,
			};
		}
	}
}