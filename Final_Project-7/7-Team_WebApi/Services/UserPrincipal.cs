using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace _7_Team_WebApi.Services
{
    public class UserPrincipal : IPrincipal
    {
        private readonly string[] _functions;

        public UserPrincipal(IIdentity identity, string[] functions)
        {
            _functions = functions;
            this.Identity = identity;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string functionKey)
        {
            return _functions.Contains(functionKey);
        }
    }
}