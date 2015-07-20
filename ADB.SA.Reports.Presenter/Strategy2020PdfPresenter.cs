using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Presenter.Report;
using ADB.SA.Reports.Utilities;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using ADB.SA.Reports.Presenter.Content;

namespace ADB.SA.Reports.Presenter
{
    public class Strategy2020PdfPresenter
    {
        IGenerateReportView view;
        public Strategy2020PdfPresenter(IGenerateReportView view)
        {
            this.view = view;
        }

        private byte[] CreateContent(PdfContentParameter content)
        {
            var document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetMargins(50, 50, 10, 40);

            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            document.Open();
            document.Add(content.Table);
            document.Close();
            return output.ToArray();
        }

        public void GenerateReport( 
            string agenda, string agendatext,
            string policy, string policytext, 
            string rule, string ruletext, 
            string process, string processtext,
            string subprocess, string subprocesstext,
            string application, string applicationtext,
            string module, string moduletext)
        {
            Strategy2020Filter filter = new Strategy2020Filter(
                agenda, agendatext,
                policy, policytext,
                rule, ruletext,
                process, processtext,
                subprocess, subprocesstext,
                application, applicationtext,
                module, moduletext);
            Strategy2020ReportBuilder reportBuilder = new Strategy2020ReportBuilder(filter);
            byte[] content = reportBuilder.BuildReport();
            this.view.RenderReport(content);
        }
    }
}
