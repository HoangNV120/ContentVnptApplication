using System;
using System.Web;
using System.Web.Mvc;

public class AdminAuthorizeAttribute : AuthorizeAttribute
{
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            filterContext.Result = new RedirectResult("/Account/Login");
        }
        else
        {
            filterContext.Result = new RedirectResult("/Account/Login");
        }
    }

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        return httpContext.User.Identity.IsAuthenticated
               && httpContext.User.IsInRole("Admin");
    }
}