using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Presenter.Content;
using iTextSharp.text.pdf;
using ADB.SA.Reports.Utilities;

namespace ADB.SA.Reports.Presenter.Report
{
    public class ReportContext
    {
        ReportStrategyBase strategy;

        public ReportContext(ReportStrategyBase strategy)
        {
            this.strategy = strategy;
        }

        public List<PdfContentParameter> BuildContent(EntityDTO dto)
        {
            return this.strategy.BuildContent(dto);
        }
    }
}
