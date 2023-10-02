using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Team_7_WebApi_Client.Repositories;

namespace Team_7_WebApi_Client.Services
{
    
    public class MemberService
    {
        MemberRepository repo = new MemberRepository();

        public int GetUserId()
        {
            string account = HttpContext.Current.User.Identity.Name;

            int id = repo.GetIdByAccount(account);

            return id;
        } 

        public string GetEmail(string account)
        {				
				string email = repo.GetEmailByAccount(account);
				return email;	
			
		}

    }
}