using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ADB.SA.Reports.Utilities
{
    public class ProcessPDFBuilderStrategy : PDFBuilderStrategyBase
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

            //Initialize templates from contents
            InitializeTemplatesForTOC(contents, writer);

            //add the cover page
            document.Add(contents[0].Table);

            //write the change history,reviewers and approvers
            document.NewPage();
            for (int i = 1; i <= 3; i++)
            {
                document.Add(contents[i].Table);
            }

            //write the table of contents
            WriteTOC(contents, writer, document);

            //objectives, roles and resp and sub process dependency
            document.NewPage();
            for (int i = 4; i < 10; i++)
            {
                contents[i].PageNumber = writer.PageNumber;
                document.Add(contents[i].Table);
            }

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

            document.NewPage();
            for (int i = 10; i < 19; i++)
            {
                contents[i].PageNumber = writer.PageNumber;
                document.Add(contents[i].Table);
            }

            //write the values for the templates
            WritePageNumbers(contents);

            document.Close();
            return output.ToArray();
        }

        private void WritePageNumbers(List<PdfContentParameter> contents)
        {
            foreach (PdfContentParameter item in contents)
            {
                if (item.PageNumber > 0)
                {
                    ColumnText.ShowTextAligned(item.Template, Element.ALIGN_RIGHT,
                        new Phrase(item.PageNumber.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8)),
                        10, 7, 0);
                }
            }
        }

        public override void CleanUp(string[] images)
        {
            
        }
    }
}
