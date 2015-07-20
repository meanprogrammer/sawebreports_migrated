using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Presenter;

namespace ADB.SA.Reports.Web
{
    public partial class Search : System.Web.UI.Page, ISearchView
    {
        SearchPresenter presenter;

        public Search()
        {
            presenter = new SearchPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string filter = Request["Key"];
            presenter.DoSearch(filter);
        }

        #region ISearchView Members

        public void RenderSearchResult(List<SearchResultDTO> results)
        {
            if (results.Count <= 0)
            {
                this.SearchResultGridView.EmptyDataText = "No search Results.";
            }
            this.SearchResultGridView.DataSource = results;
            this.SearchResultGridView.DataBind();

        }

        #endregion
    }
}
