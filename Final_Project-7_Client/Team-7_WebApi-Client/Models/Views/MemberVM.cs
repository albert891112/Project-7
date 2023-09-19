using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.DTOS;
using static Dapper.SqlMapper;

namespace Team_7_WebApi_Client.Models.Views
{
	public class MemberVM
	{
		public int Id { get; set; }

		public string Account { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public bool Enable { get; set; }
	}

	public static class MemberVMExtenssion
	{
		public static MemberVM ToVM(this MemberDTO dto)
		{
			return new MemberVM
			{
				Id = dto.Id,
				Account = dto.Account,
				Password = dto.Password,
				Email = dto.Email,
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Enable = dto.Enable,
			};
		}
	}
}