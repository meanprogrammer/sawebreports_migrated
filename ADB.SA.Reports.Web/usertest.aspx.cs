using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;

namespace ADB.SA.Reports.Web
{
    public partial class usertest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string user = HttpContext.Current.Session["CurrentUser"].ToString();
            this.Label1.Text = user;

                //WindowsIdentity.GetCurrent().Name;
        }
    }
}
