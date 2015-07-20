using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Global
{
    public class SAWebContext
    {
        public static System.Web.HttpServerUtility Server
        {
            get
            {
                return System.Web.HttpContext.Current.Server;
            }
        }

        public static System.Web.HttpRequest Request
        {
            get
            {
                return System.Web.HttpContext.Current.Request;
            }
        }

        public static System.Web.HttpResponse Response
        {
            get
            {
                return System.Web.HttpContext.Current.Response;
            }
        }
    }
}
