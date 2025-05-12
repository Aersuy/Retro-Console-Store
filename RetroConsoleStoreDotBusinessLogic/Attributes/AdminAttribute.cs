using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web.Mvc;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStoreDotDomain.User;
using RetroConsoleStoreDotDomain.Model.User;


namespace RetroConsoleStoreDotBusinessLogic.Attributes
{
    public class AdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var authCookie = filterContext.HttpContext.Request.Cookies["X-KEY"];
            var businessLogic = new BusinessLogic();
            if (authCookie != null)
            {
                UserSmall currentUser = businessLogic.GetLoginBL().GetUserByCookie(authCookie.Value);
                if (currentUser == null || currentUser.Role != RetroConsoleStoreDotDomain.Enums.URole.Administrator)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"controller","Auth" },
                            {"action","Login" },
                            {"errorMessage","Log in as Admin" }
                        });
                    return;
                   
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                     new RouteValueDictionary
                {
                    {"controller","Auth"},
                    {"action","Login" },
                    {"errorMessa","Log on" }
                } );
                return;


            }
           
           
                base.OnActionExecuting(filterContext);
        }
    }
}
