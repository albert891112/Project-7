using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Team_7_WebApi_Client.Models.DTOS;

namespace Team_7_WebApi_Client.Models.Entities
{
	public class MemberEntity
	{
		public int Id { get; set; }

		public string Account { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public bool Enable { get; set; }
	}

	public static class MemberEntityExtenssion
	{
		public static MemberEntity ToEntity(this MemberDTO dto)
		{
			return new MemberEntity
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