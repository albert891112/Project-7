﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class CouponEntity
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

	public class GetCouponEntity
	{
		public int Id { get; set; }
		
		public string CouponName { get; set; }

        public int DiscountTypeId { get; set; }


        public decimal DiscountValue { get; set; }

		public string CouponDescription { get; set; }

		public DateTime EndDate { get; set; }

		public bool Enabled { get; set; }
	}



	public class DiscountTypeEntity
	{
		public int Id { get; set; }
		public string Type { get; set; }
	}

	public static class CouponEntityExtenssion
	{
		public static CouponEntity ToEntity(this CouponDTO dto)
		{
			return new CouponEntity
			{
				Id = dto.Id,
				CouponCode = dto.CouponCode,
				CouponName = dto.CouponName,
				DiscountTypeId = dto.DiscountTypeId,
				DiscountValue = dto.DiscountValue,
				CouponDescription = dto.CouponDescription,
				ExpiratinDate = dto.ExpiratinDate,
				UsageCount = dto.UsageCount,
				Enable = dto.Enable,
				Image = dto.Image,
			};
		}

		public static GetCouponEntity ToEntity(this GetCouponDTO dto)
		{
			return new GetCouponEntity
			{
				Id = dto.Id,
				CouponName = dto.CouponName,
				DiscountTypeId = dto.DiscountTypeId,
				DiscountValue = dto.DiscountValue,
				CouponDescription = dto.CouponDescription,
				EndDate = dto.EndDate,
				Enabled = dto.Enabled,
			};
		}
	}

	public static class DiscountTypeEntityExtenssion
	{
		public static DiscountTypeEntity ToEntity(this DiscountTypeDTO dto)
		{
			return new DiscountTypeEntity
			{
				Id = dto.Id,
				Type = dto.Type,
			};
		}
	}
}