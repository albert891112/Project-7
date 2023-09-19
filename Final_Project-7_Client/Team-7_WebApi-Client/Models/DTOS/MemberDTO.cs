using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.Entities;
using Team_7_WebApi_Client.Models.Views;

namespace Team_7_WebApi_Client.Models.DTOS
{
	public class MemberDTO
	{
		public int Id { get; set; }

		public string Account { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public bool Enable { get; set; }
	}

	public static class MemberDTOExtenssion
	{
		public static MemberDTO ToDTO(this MemberEntity entity)
		{
			return new MemberDTO
			{
				Id = entity.Id,
				Account = entity.Account,
				Password = entity.Password,
				Email = entity.Email,
				FirstName = entity.FirstName,
				LastName = entity.LastName,
				Enable = entity.Enable,			
			};
		}

		public static MemberDTO ToDTO(this MemberVM vm)
		{
			return new MemberDTO
			{
				Id = vm.Id,
				Account = vm.Account,
				Password = vm.Password,
				Email = vm.Email,
				FirstName = vm.FirstName,
				LastName = vm.LastName,
				Enable = vm.Enable,
			};
		}
	}
}