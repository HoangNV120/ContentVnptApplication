using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContentVnptApplication.Filters
{
    public class CheckUserStatusAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (user.Identity.IsAuthenticated)
            {
                var userId = user.Identity.GetUserId();

                using (var db = new AppDbContext())
                {
                    var currentUser = db.Users
                        .FirstOrDefault(x => x.Id == userId);

                    if (currentUser == null ||
                        currentUser.IsDeleted ||
                        currentUser.IsBanned)
                    {
                        filterContext.HttpContext
                            .GetOwinContext()
                            .Authentication
                            .SignOut();

                        filterContext.Result =
                            new RedirectResult("/Account/Login");
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}