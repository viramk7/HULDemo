using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVA.Web.Common
{
    public class SessionHelper
    {
        public static string UserId
        {
            get
            {
                return HttpContext.Current.Session["UserId"] == null ? string.Empty : (string)HttpContext.Current.Session["UserId"];
            }
            set { HttpContext.Current.Session["UserId"] = value; }
        }

        public static string WelcomeUser
        {
            get
            {
                return HttpContext.Current.Session["WelcomeUser"] == null
                    ? "Guest"
                    : (string)HttpContext.Current.Session["WelcomeUser"];
            }
            set { HttpContext.Current.Session["WelcomeUser"] = value; }
        }
    }
}