using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Presenter;
using Newtonsoft.Json;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Web
{
    public partial class Detail : System.Web.UI.Page, IDefaultView
    {
        DefaultPagePresenter presenter;
        public Detail()
        {
            presenter = new DefaultPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["detailId"] != null)
            {
                int id = int.Parse(Request["detailId"]);
                presenter.RenderDetail(id);
            }
        }

        #region IDefaultView Members

        public void RenderContent(string html)
        {
            Response.Write(html);
            Response.End();
        }

        #endregion

        #region IDefaultView Members


        public void RenderQuickLinks(string arrays)
        {
            
        }

        #endregion

        #region IDefaultView Members


        public void RenderBreadcrumb(string html)
        {
            
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

        void IDefaultView.RenderContent(string html)
        {
            Response.Write(html);
            Response.End();
        }

        void IDefaultView.RenderBreadcrumb(string html)
        {
            throw new NotImplementedException();
        }

        void IDefaultView.RenderHomePageData(ADB.SA.Reports.Entities.DTO.HomePageContentDTO dto)
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
