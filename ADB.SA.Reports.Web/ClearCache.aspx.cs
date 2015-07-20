using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Presenter;

namespace ADB.SA.Reports.Web
{
    public partial class ClearCache : System.Web.UI.Page, IClearCacheView
    {
        ClearCachePresenter presenter;

        public ClearCache()
        {
            presenter = new ClearCachePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            presenter.ClearCache();
        }

        #region IClearCacheView Members

        public void NotifyClear(string message)
        {
            this.Message.Text = message;
        }

        #endregion
    }
}
