using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Utilities
{
    public abstract class PDFBuilderStrategyBase
    {
        public const int heightOffset = 130;
        public abstract byte[] CreatePdf(List<PdfContentParameter> tables, string[] images, int type);
        public abstract void CleanUp(string[] images);

        protected virtual void InitializeTemplatesForTOC(List<PdfContentParameter> contents, PdfWriter writer)
        {
            foreach (PdfContentParameter item in contents)
            {
                item.Template = writer.DirectContent.CreateTemplate(30, 16);
            }
        }

        protected virtual void WriteTOC(List<PdfContentParameter> contents, PdfWriter writer, Document document)
        {
            document.NewPage();
            PdfPTable t = new PdfPTable(2);
            t.WidthPercentage = 100;
            t.SetWidths(new float[] { 98f, 2f });
            t.TotalWidth = document.PageSize.Width - (document.LeftMargin + document.RightMargin);
            t.AddCell(new PdfPCell(
                new Phrase(GlobalStringResource.TableOfContents,
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16))
                ) { Colspan = 2, Border = Rectangle.NO_BORDER, PaddingBottom = 25 });

            foreach (PdfContentParameter item in contents)
            {
                if (!string.IsNullOrEmpty(item.Header))
                {
                    t.AddCell(
                        new PdfPCell(
                                new Phrase(item.Header,
                                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8)
                                    )
                            ) { Border = Rectangle.NO_BORDER, NoWrap = false, FixedHeight = 15, }
                        );

                    PdfPCell templateCell = new PdfPCell(Image.GetInstance(item.Template));
                    templateCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    templateCell.Border = Rectangle.NO_BORDER;
                    t.AddCell(templateCell);
                }
            }
            float docHeight = document.PageSize.Height - heightOffset;
            document.Add(t);

        }
    }

    
}
