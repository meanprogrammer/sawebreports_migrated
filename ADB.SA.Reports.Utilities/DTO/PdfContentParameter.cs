using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;

namespace ADB.SA.Reports.Utilities
{
    public class PdfContentParameter
    {
        public PdfPTable Table { get; set; }
        public int PageNumber { get; set; }
        public string Header { get; set; }
        public PdfTemplate Template { get; set; }
    }
}
