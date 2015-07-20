using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Presenter;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Web
{
    public partial class CommentFullThread : System.Web.UI.Page, ICommentsView
    {
        CommentsPresenter presenter;
        public CommentFullThread()
        {
            presenter = new CommentsPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request[GlobalStringResource.ID.ToLower()];
            presenter.GetAllComments(id);
        }

        #region ICommentsView Members

        public void RenderComments(string comments)
        {
            this.ContentLiteral.Text = comments;
        }

        #endregion

        #region ICommentsView Members


        public void RenderComments(ADB.SA.Reports.Entities.DTO.CommentResponseDTO response)
        {
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(response));
        }

        #endregion
    }
}
