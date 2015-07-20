using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace ADB.SA.Reports.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //if (Request.QueryString["ClearCache"] != null)
            //{
            //    var value = Request.QueryString["ClearCache"];
            //    if (value == "1")
            //    {
            //        Response.Write("Cache Cleared!");
            //        Response.End();
            //    }
            //}
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            if (ex != null && ex.InnerException != null)
            {
                ex.InnerException.Data.Add("Request Url", Request.Url.AbsoluteUri);
                ExceptionPolicy.HandleException(ex.InnerException, "General");
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}