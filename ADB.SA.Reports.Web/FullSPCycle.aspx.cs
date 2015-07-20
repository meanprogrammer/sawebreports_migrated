using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADB.SA.Reports.Presenter;
using ADB.SA.Reports.View;

namespace ADB.SA.Reports.Web
{
    public partial class FullSPCycle : System.Web.UI.Page, IFullSPCycle
    {
        FullSPCyclePresenter presenter;
        public FullSPCycle() {
            presenter = new FullSPCyclePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #region IFullSPCycle Members

        public void RenderFullCycle(string html)
        {
            this.FullLiteral.Text = html;
        }

        #endregion

        protected void GoButton_Click(object sender, EventArgs e)
        {
            int id = int.Parse(this.SubProcessDropDownList.SelectedValue);
            bool withImage = this.WithImageCheckBox.Checked;
            presenter.LoadSampleData(id, withImage);
        }
    }
}
