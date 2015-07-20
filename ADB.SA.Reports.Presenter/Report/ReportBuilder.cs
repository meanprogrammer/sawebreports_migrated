using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using iTextSharp.text.pdf;
using ADB.SA.Reports.Utilities;

namespace ADB.SA.Reports.Presenter.Report
{
    public class ReportBuilder
    {
        public static List<PdfContentParameter> BuildReport(EntityDTO dto)
        {
            ReportContext context = null;
            switch (dto.Type)
            {
                case 111:
                    context = new ReportContext(new ProcessReportStrategy());
                    break;
                case 142:
                    context = new ReportContext(new SubProcessReportStrategy());
                    break;
                default:
                    context = new ReportContext(new GenericReportStrategy());
                    break;
            }

            return context.BuildContent(dto);
        }
    }
}
