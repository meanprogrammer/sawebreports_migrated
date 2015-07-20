using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADB.SA.Reports.Presenter;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Web.Helpers;
using System.IO.Compression;

namespace ADB.SA.Reports.Web
{
    public partial class Menu : System.Web.UI.Page, IMenuPageView
    {
        MenuPagePresenter presenter;

        public Menu()
        {
            presenter = new MenuPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GzipHelper.CompressPage();
            presenter.BuildNavigation();
        }

        #region IMenuPageView Members

        public void BindNavigationMenu(List<SAMenuItemDTO> menuItems)
        {
            string content = MenuPageHelper.BuildNavigationMenu(menuItems);
            Response.Write(content);
            Response.End();
        }

        #endregion
    }
}
