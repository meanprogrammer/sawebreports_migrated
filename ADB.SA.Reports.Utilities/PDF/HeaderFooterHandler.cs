using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Web;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Utilities
{
    public class HeaderFooterHandler : PdfPageEventHelper
    {
        PdfTemplate total;
        private int type;
        public HeaderFooterHandler(int type)
        {
            this.type = type;
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            total = writer.DirectContent.CreateTemplate(30, 16);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable footer = new PdfPTable(2);
            footer.SetWidths(new float[] { 98f, 2f });
            footer.WidthPercentage = 100;
            footer.TotalWidth = document.PageSize.Width - (document.LeftMargin + document.RightMargin);

            Chunk text = new Chunk(string.Format(GlobalStringResource.PageOfFooter, 
                document.PageNumber), FontFactory.GetFont(FontFactory.HELVETICA, 8));

            PdfPCell footerCell = new PdfPCell(new Phrase(text));
            footerCell.Border = 1;
            footerCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            footer.AddCell(footerCell);

            PdfPCell cell = new PdfPCell(Image.GetInstance(total));
            cell.Border = 1;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            footer.AddCell(cell);
            footer.WriteSelectedRows(0, -1, 50, (document.BottomMargin - 10), writer.DirectContent);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            ColumnText.ShowTextAligned(total, Element.ALIGN_RIGHT,
                    new Phrase((writer.PageNumber - 1).ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8)),
                    10, 6, 0);
        }

        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            iTextSharp.text.Image header = iTextSharp.text.Image.GetInstance(PathResolver.MapPath("images/ADB_Logo.gif"));

            PdfPTable tableHeader = new PdfPTable(2);
            tableHeader.WidthPercentage = 100;

            PdfPCell imageHeaderCell = new PdfPCell(header);
            imageHeaderCell.Rowspan = 2;
            imageHeaderCell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            imageHeaderCell.Border = 0;
            imageHeaderCell.PaddingBottom = 3;
            tableHeader.AddCell(imageHeaderCell);

            Font helvetica20 = FontFactory.GetFont(FontFactory.HELVETICA, 16);
            PdfPCell typeNameCell = new PdfPCell(new Phrase(TypesReader.GetDiagramName(this.type), helvetica20));
            typeNameCell.HorizontalAlignment = 2;
            typeNameCell.Border = 0;
            tableHeader.AddCell(typeNameCell);

            PdfPCell adbCell = new PdfPCell(new Phrase(GlobalStringResource.ADB, helvetica20));
            adbCell.HorizontalAlignment = 2;
            adbCell.Border = 0;
            tableHeader.AddCell(adbCell);

            PdfPCell lineCell = new PdfPCell();
            lineCell.Colspan = 2;
            lineCell.Border = 1;
            lineCell.PaddingBottom = 5;
            tableHeader.AddCell(lineCell);

            document.Add(tableHeader);
        }
    }
}
