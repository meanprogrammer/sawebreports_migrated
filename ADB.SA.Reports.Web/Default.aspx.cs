using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Presenter;
using System.Threading;
using System.IO.Compression;
using ADB.SA.Reports.Web.Helpers;
using ADB.SA.Reports.Utilities;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Web
{
    public partial class Default : System.Web.UI.Page, IDefaultView
    {
        DefaultPagePresenter presenter;

        public Default()
        {
            presenter = new DefaultPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GzipHelper.CompressPage();

            string qstring = ExpandQueryString(Request.QueryString);

            Page.ClientScript.RegisterStartupScript(GetType(), "hwa", string.Format("ajaxcallmaincontent('{0}');", qstring), true);

            //if (Request[Resources.IdKey] != null)
            //{
            //    int id = Convert.ToInt32(Request[Resources.IdKey]);
            //    if( Request["isDetail"] != null )
            //    {
            //        presenter.RenderDetail(id);
            //        return;
            //    }
            //    presenter.RenderEntity(id);
            //}
            //else
            //{
            //    int defaultHomeId = Convert.ToInt32(AppSettingsReader.GetValue("HOMEPAGE_ID"));
            //    if (defaultHomeId > 0)
            //    {
            //        int type =presenter.GetHomePageType(defaultHomeId);
            //        switch (type)
            //        {
            //            case 105:
            //                presenter.RenderEntity(defaultHomeId);
            //                break;
            //            default:
            //                var asIsPresenter = new AsIsPagePresenter(this);
            //                asIsPresenter.RenderDetail(defaultHomeId);
            //                break;
            //        }
                    
            //    }
            //}
        }

        private string ExpandQueryString(NameValueCollection value)
        {
            if (value == null || value.Count <= 0)
            {
                return string.Empty;
            }

            StringBuilder b = new StringBuilder();
            foreach (string item in value.AllKeys)
            {
                if (b.Length > 0)
                {
                    b.Append("&");
                }

                b.AppendFormat("{0}={1}", item, value[item]);
            }
            return b.ToString();
        }

        #region IDefaultView Members

        public void RenderContent(string html)
        {
            //string[] splitted;
            //if (html.Contains("{split}"))
            //{
            //    splitted = html.Split(new string[] { "{split}" }, StringSplitOptions.RemoveEmptyEntries);
            //    ((Literal)this.Master.FindControl("HeaderLiteral")).Text = string.Format("<h4>{0}</h4>", splitted[0]);
            //    this.ContentLiteral.Text = splitted[1];
            //}
            //else
            //{
            //    this.ContentLiteral.Text = html;
            //}
        }

        public void RenderBreadcrumb(string html)
        {
            //((Literal)this.Master.FindControl("BreadCrumbLiteral")).Text = html;
        }

        #endregion

        #region IDefaultView Members


        public void RenderHomePageData(ADB.SA.Reports.Entities.DTO.HomePageContentDTO dto)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDefaultView Members


        public void RenderContent(ADB.SA.Reports.Entities.DTO.PageResponseDTO response)
        {
            Response.Write(JsonConvert.SerializeObject(response));
            Response.End();
        }

        #endregion

        #region IDefaultView Members


        public void RenderDetail(object content)
        {
            Response.Write(JsonConvert.SerializeObject(content));
            Response.End();
        }

        #endregion
    }
}
