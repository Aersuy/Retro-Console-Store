using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using RetroConsoleStore.BusinessLogic;
using RetroConsoleStoreDotDomain.Model.User;
using RetroConsoleStoreDotDomain.Enums;
using RetroConsoleStoreDotBusinessLogic.Interfaces;
namespace RetroConsoleStoreDotBusinessLogic.Attributes
{
    public class UserAttribute : ActionFilterAttribute
    {   private readonly IAdmin _admin;
        private readonly ILogin _login;
        public UserAttribute()
        { var businessLogic = new BusinessLogic();
            _admin = businessLogic.GetAdminBl();
            _login = businessLogic.GetLoginBL();
            
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var authCookie = filterContext.HttpContext.Request.Cookies["X-KEY"];
            var businessLogic = new BusinessLogic();

            if (authCookie != null)
                {
                  UserSmall user = _login.GetUserByCookie(authCookie.Value);
                if (user == null)
                    {
                    filterContext.Result = new RedirectToRouteResult(
                     new RouteValueDictionary
                     {
                            {"controller","Auth" },
                            {"action","Login" },
                            {"errorMessage","Log in" }
                     });
                    return;
                }
                if (user.Role == URole.Banned)
                {
                    _admin.AutoUnbanBL(user);
                    user = _login.GetUserByCookie(authCookie.Value);
                    if (user.Role == URole.Banned)
                    {
                        filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary {
                        {"controller","Error"},
                        {"action", "Banned" },
                        {"errorMessage", "Banned" }
                    });
                    }
                    
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
                });
                return;
            }


            base.OnActionExecuting(filterContext);
            return;
        }
    };
}
