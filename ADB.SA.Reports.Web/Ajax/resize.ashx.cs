using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ADB.SA.Reports.Presenter;

namespace ADB.SA.Reports.Web.Ajax
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class resize : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["action"] != null)
            {
                var percentage = context.Request["percentage"];
                ResizeWmfPresenter wmfP = new ResizeWmfPresenter();
                int id = Convert.ToInt32(context.Request[Resources.IdKey]);
                string path = wmfP.BuildDiagramContent(id, float.Parse(percentage));
                context.Response.Write(path);
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
