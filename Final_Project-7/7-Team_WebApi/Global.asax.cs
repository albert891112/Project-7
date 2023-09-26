using _7_Team_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace _7_Team_WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest()
        {
            if (!Request.IsAuthenticated)
            {
                return;
            }

      
            var id = (FormsIdentity)User.Identity;

       
            FormsAuthenticationTicket ticket = id.Ticket;

         
            string functions = ticket.UserData; 
            string[] arrFunctions =
                functions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            IPrincipal currentUser = new UserPrincipal(User.Identity, arrFunctions);

            Context.User = currentUser;
        }
    }
}
