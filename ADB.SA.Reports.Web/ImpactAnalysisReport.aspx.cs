using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Presenter;
using ADB.SA.Reports.Web.Helpers;

namespace ADB.SA.Reports.Web
{
    public partial class ImpactAnalysisReport : System.Web.UI.Page, IStrategy2020ReportView
    {
        Strategy2020Presenter presenter;

        public ImpactAnalysisReport()
        {
//            presenter = new Strategy2020Presenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GzipHelper.CompressPage();
        }

        #region IStrategy2020ReportView Members

        public void ShowContent(string content)
        {
            
        }

        #endregion

        #region IStrategy2020ReportView Members


        public void ShowFilteredFilter(List<ADB.SA.Reports.Entities.DTO.DropdownItem> items)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IStrategy2020ReportView Members


        public void ShowFilteredFilter(Dictionary<string, List<ADB.SA.Reports.Entities.DTO.DropdownItem>> items)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
