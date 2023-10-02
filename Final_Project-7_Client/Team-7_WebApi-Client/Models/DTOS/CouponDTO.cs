using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class CouponDTO
	{
		public int Id { get; set; }
		public string CouponCode { get; set; }
		public string CouponName { get; set; }
		public int DiscountTypeId { get; set; }
		public decimal DiscountValue { get; set; }
		public string CouponDescription { get; set; }
		public DateTime ExpiratinDate { get; set; }
		public int UsageCount { get; set; }
		public bool Enable { get; set; }
		public string Image { get; set; }
	}

	public class GetCouponDTO
	{
		public int Id { get; set; }

		public string CouponName { get; set; }

		public decimal DiscountValue { get; set; }

		public string CouponDescription { get; set; }

		public DateTime EndDate { get; set; }

		public bool Enabled { get; set; }
	}

	public class DiscountTypeDTO
	{
		public int Id { get; set; }
		public string Type { get; set; }
	}


	public static class CouponDTOExtenssion
	{

		public static GetCouponDTO ToDTO(this GetCouponVM vm)
		{
			return new GetCouponDTO
			{
				Id = vm.Id,
				CouponName = vm.CouponName,
				DiscountValue = vm.DiscountValue,
				CouponDescription = vm.CouponDescription,
				EndDate = vm.EndDate,
				Enabled = vm.Enabled,
			};
		}

		public static GetCouponDTO ToDTO(this GetCouponEntity entity)
		{
			return new GetCouponDTO
			{
				Id = entity.Id,
				CouponName = entity.CouponName,
				DiscountValue = entity.DiscountValue,
				CouponDescription = entity.CouponDescription,
				EndDate = entity.EndDate,
				Enabled = entity.Enabled,
			};
		}
		public static CouponDTO ToDTO(this CouponEntity entity)
		{
			return new CouponDTO
			{
				Id = entity.Id,
				CouponCode = entity.CouponCode,
				CouponName = entity.CouponName,
				DiscountTypeId = entity.DiscountTypeId,
				DiscountValue =entity.DiscountValue,
				CouponDescription = entity.CouponDescription,
				ExpiratinDate =entity.ExpiratinDate,
				UsageCount = entity.UsageCount,
				Enable =entity.Enable,
				Image = entity.Image,			
			};
		}

		public static CouponDTO ToDTO(this CouponVM vm)
		{
			return new CouponDTO
			{
				Id = vm.Id,
				CouponCode = vm.CouponCode,
				CouponName = vm.CouponName,
				DiscountTypeId = vm.DiscountTypeId,
				DiscountValue = vm.DiscountValue,
				CouponDescription = vm.CouponDescription,
				ExpiratinDate = vm.ExpiratinDate,
				UsageCount = vm.UsageCount,
				Enable = vm.Enable,
				Image = vm.Image,
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

		public static DiscountTypeDTO ToDTO(this DiscountTypeVM vm)
		{
			return new DiscountTypeDTO
			{
				Id = vm.Id,
				Type = vm.Type,
			};
		}
	}
}