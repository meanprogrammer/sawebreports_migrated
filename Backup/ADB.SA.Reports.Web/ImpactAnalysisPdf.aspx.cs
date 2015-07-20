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
    public partial class ImpactAnalysisPdf : System.Web.UI.Page, IGenerateReportView
    {
        Strategy2020PdfPresenter presenter;

        public ImpactAnalysisPdf()
        {
            presenter = new Strategy2020PdfPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GzipHelper.CompressPage();
            //string type = Request.QueryString["type"];
            string agenda = Request.QueryString["agenda"];
            string policy = Request.QueryString["policy"];
            string rule = Request.QueryString["rule"];
            string process = Request.QueryString["process"];
            string subprocess = Request.QueryString["subprocess"];
            string application = Request.QueryString["application"];
            string module = Request.QueryString["module"];


            //var typename = Request.QueryString["typetext"];
            var agendaname = Request.QueryString["agendatext"];
            var policyname = Request.QueryString["policytext"];
            var rulename = Request.QueryString["ruletext"];
            var processname = Request.QueryString["processtext"];
            var subprocessname = Request.QueryString["subprocesstext"];
            var applicationname = Request.QueryString["applicationtext"];
            var modulename = Request.QueryString["moduletext"];

            presenter.GenerateReport(agenda, agendaname, 
                policy, policyname, rule, rulename, process, processname,
                subprocess, subprocessname, application, applicationname,
                module, modulename);
        }

        #region IGenerateReportView Members

        public void RenderReport(byte[] pdfBytes)
        {
            if (pdfBytes == null)
            {
                Response.Write("No data to produce report.");
                Response.End();
            }
            else
            {
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(pdfBytes);
            }
        }

        #endregion
    }
}
