using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Utilities;

namespace ADB.SA.Reports.Presenter.Report
{
    public class GenericReportStrategy : ReportStrategyBase
    {
        EntityDTO dto;
        public override List<PdfContentParameter> BuildContent(ADB.SA.Reports.Entities.DTO.EntityDTO dto)
        {
            this.dto = dto;
            List<PdfContentParameter> contents = new List<PdfContentParameter>();
            contents.Add(CreateTitlePage(this.dto));
            return contents;
        }
    }
}
