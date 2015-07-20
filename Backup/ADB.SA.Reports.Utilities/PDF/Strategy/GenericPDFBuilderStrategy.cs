using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;

namespace ADB.SA.Reports.Utilities
{
    public class GenericPDFBuilderStrategy : PDFBuilderStrategyBase
    {

        public override byte[] CreatePdf(List<PdfContentParameter> contents, string[] images, int type)
        {
            var document = new Document();
            float docHeight = document.PageSize.Height - heightOffset;
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetMargins(50, 50, 10, 40);

            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            writer.PageEvent = new HeaderFooterHandler(type);

            document.Open();

            document.Add(contents[0].Table);

            for (int i = 0; i < images.Length; i++)
            {
                document.NewPage();
                float subtrahend = document.PageSize.Height - heightOffset;
                iTextSharp.text.Image pool = iTextSharp.text.Image.GetInstance(images[i]);
                pool.Alignment = 3;
                pool.ScaleToFit(document.PageSize.Width - (document.RightMargin * 2), subtrahend);
                //pool.ScaleAbsolute(document.PageSize.Width - (document.RightMargin * 2), subtrahend);
                document.Add(pool);
            }

            document.Close();
            return output.ToArray();
        }

        public override void CleanUp(string[] images)
        {
            
        }
    }
}
