using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Presenter;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Web.Helpers;

namespace ADB.SA.Reports.Web
{
    public partial class GenerateReport : System.Web.UI.Page, IGenerateReportView
    {
        GenerateReportPresenter _presenter; 
        public GenerateReport()
        {
            _presenter = new GenerateReportPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GzipHelper.CompressPage();
            if (Request["reportid"] != null && Request["reportid"] != "undefined")
            {
                int reportId = int.Parse(Request["reportid"]);
                _presenter.GenerateReport(reportId);
            }
        }

        #region IGenerateReportView Members

        public void RenderReport(byte[] pdfBytes)
        {
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(pdfBytes);
        }

        #endregion
    }
}
