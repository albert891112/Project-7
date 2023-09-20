using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team_7_WebApi_Client.Models.EFModels;

namespace Team_7_WebApi_Client.Models.Views.Members
{
    public static class MemberExts
    {
        public static EditProfileVm ToEditProfileVm(this Member member)
        {
            return new EditProfileVm
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
            };
        }
    }
}