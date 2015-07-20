using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADB.SA.Reports.Presenter;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Utilities;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Web
{
    public partial class ContentGateway : System.Web.UI.Page, IDefaultView
    {
        DefaultPagePresenter presenter;

        public ContentGateway()
        {
            presenter = new DefaultPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Request[Resources.IdKey] != null)
            {
                int id = Convert.ToInt32(Request[Resources.IdKey]);
                if (Request["isDetail"] != null)
                {
                    presenter.RenderDetail(id);
                    return;
                }
                presenter.RenderEntity(id);
            }
            else
            {
                int defaultHomeId = Convert.ToInt32(AppSettingsReader.GetValue("HOMEPAGE_ID"));
                if (defaultHomeId > 0)
                {
                    int type = presenter.GetHomePageType(defaultHomeId);
                    switch (type)
                    {
                        case 105:
                            presenter.RenderEntity(defaultHomeId);
                            break;
                        default:
                            var asIsPresenter = new HomePagePresenter(this);
                            asIsPresenter.RenderDetail(defaultHomeId);
                            break;
                    }

                }
            }
        }

        #region IDefaultView Members

        public void RenderContent(string html)
        {
            Response.Write(html);
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
            Response.Write(JsonConvert.SerializeObject(dto));
            Response.End();
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
