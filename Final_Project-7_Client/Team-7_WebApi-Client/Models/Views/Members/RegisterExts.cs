using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;
using Team_7_WebApi_Client.Models.Infra;

namespace Team_7_WebApi_Client.Models.Views.Members
{
	public static class RegisterExts
	{
		public static Member ToEFModel(this RegisterVm vm)
		{
			var salt=HashUtility.GetSalt();
			var hashPassword=HashUtility.ToSHA256(salt,vm.Password);

			return new Member
			{
				Id = vm.Id,
				Account = vm.Account,
				Password = hashPassword,
				Email = vm.Email,
				FirstName = vm.FirstName,
				LastName = vm.LastName,
				Enable = true,
			};
		}
	}
}