using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;

namespace ADB.SA.Reports.Utilities
{
    public class PDFBuilder
    {
        public static byte[] CreatePDF(List<PdfContentParameter> contents, string[] images, int type)
        {
            PDFContext context = null;
            switch (type)
            {
                case 111:
                    context = new PDFContext(new ProcessPDFBuilderStrategy());
                    break;
                case 142:
                    context = new PDFContext(new SubProcessPDFBuilderStrategy());
                    break;
                default:
                    context = new PDFContext(new GenericPDFBuilderStrategy());
                    break;
            }

            return context.BuildPDF(contents, images, type);
        }
    }
}
