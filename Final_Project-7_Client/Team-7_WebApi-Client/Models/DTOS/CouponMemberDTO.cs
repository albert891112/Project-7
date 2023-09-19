using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class CouponMemberDTO
	{
		public int Id { get; set; }
		public MemberDTO Member { get; set; }
		public CouponDTO Coupon { get; set; }
	}


	public static class CouponMemberDTOExtenssion
	{
		public static CouponMemberDTO ToDTO(this CouponMemberEntity entity)
		{
			return new CouponMemberDTO
			{
				Id = entity.Id,
				Member = entity.Member.ToDTO(),
				Coupon = entity.Coupon.ToDTO(),
			};
		}

		public static CouponMemberDTO ToDTO(this CouponMemberVM vm)
		{
			return new CouponMemberDTO
			{
				Id = vm.Id,
				Member = vm.Member.ToDTO(),
				Coupon = vm.Coupon.ToDTO(),
			};
		}
	}
}