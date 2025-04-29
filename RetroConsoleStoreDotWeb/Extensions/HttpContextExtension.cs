using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using RetroConsoleStoreDotDomain.Model.User;

namespace RetroConsoleStoreDotWeb.Extensions
{
    public static class HttpContextExtension
    {
        public static UserSmall GetMySessionObject(this HttpContext context)
        {
            return (UserSmall)context?.Session["__SessionObject"];
        }
        public static void SetMySessiobObject(this HttpContext context, UserSmall user)
        {
            context.Session.Add("__SessionObject", user);
        }

    }
}