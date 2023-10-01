using Albert.Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Team_7_WebApi_Client.Repositories
{
    public class MemberRepository
    {
        SqlDb connection = new SqlDb();

        public int GetIdByAccount(string account)
        {
            string sql = "select Id from Members where Account = @account";

            object obj = new
            {
                account = account
            };

            int id = this.connection.Get<int>(sql , "default" , obj);   

            return id;  
        }

		public string GetEmailByAccount(string account)
        {
			string sql = "select Email from Members where Account = @Account";

			object obj = new
			{
				account = account 

			};

			string email = this.connection.Get<string>(sql, "default", obj);

			return email;
		}
	}
}