using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class CouponMemberEntity
	{
		public int Id { get; set; }
		public MemberEntity Member { get; set; }
		public CouponEntity Coupon { get; set; }

	}

	public static class CouponMemberEntityExtenssion
	{
		public static CouponMemberEntity ToEntity(this CouponMemberDTO dto)
		{
			return new CouponMemberEntity
			{
				Id = dto.Id,
				Member = dto.Member.ToEntity(),
				Coupon = dto.Coupon.ToEntity(),
			};
		}
	}
}