using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _7_Team_WebApi.Services
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public string Functions { get; set; }
        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                UserPrincipal currentUser = filterContext.HttpContext.User as UserPrincipal;

                if (string.IsNullOrEmpty(Functions)) return;

     
                var allowFunctions = Functions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);



                if (allowFunctions.Any(x => currentUser.IsInRole(x)))
                {
                    return;
                }
                else
                {
         
                    filterContext.Result = new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary(
                            new
                            {
                                controller = "PermissionControll",
                                action = "InsufficientPermissions"
                            })
                    );
                }
            }

            //不動為甚麼要加入這個
            base.OnAuthorization(filterContext);
        }
    }
}