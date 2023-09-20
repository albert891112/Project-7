using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using Team_7_WebApi_Client.Models.Entities;

namespace Team_7_WebApi_Client.Models.Views
{
	public class CouponMemberVM
	{
		public int Id { get; set; }
		public MemberVM Member { get; set; }
		public CouponVM Coupon { get; set; }
	}

	public static class CouponMemberVMExtenssion
	{
		public static CouponMemberVM ToVM(this CouponMemberDTO dto)
		{
			return new CouponMemberVM
			{
				Id = dto.Id,
				Member = dto.Member.ToVM(),
				Coupon = dto.Coupon.ToVM(),
			};
		}
	}
}