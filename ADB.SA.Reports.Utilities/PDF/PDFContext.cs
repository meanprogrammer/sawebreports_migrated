using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;

namespace ADB.SA.Reports.Utilities
{
    public class PDFContext
    {
        PDFBuilderStrategyBase strategy;

        public PDFContext(PDFBuilderStrategyBase strategy)
        {
            this.strategy = strategy;
        }

        public byte[] BuildPDF(List<PdfContentParameter> contents, string[] images, int type)
        {
            return this.strategy.CreatePdf(contents, images, type);
        }
    }
}
