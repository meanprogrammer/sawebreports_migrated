using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ADB.SA.Reports.Presenter;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Web.Helpers;

namespace ADB.SA.Reports.Web
{
    public partial class Master : System.Web.UI.MasterPage, IMenuPageView
    {
        MenuPagePresenter presenter;
        public Master()
        {
            presenter = new MenuPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region IMenuPageView Members

        public void BindNavigationMenu(List<SAMenuItemDTO> menuItems)
        {
            //this.MenuLiteral.Text = MenuPageHelper.BuildNavigationMenu(menuItems);
        }

        #endregion

    }
}
