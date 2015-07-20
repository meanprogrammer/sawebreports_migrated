using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Presenter.Content;
using iTextSharp.text.pdf;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Global;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Drawing;

namespace ADB.SA.Reports.Presenter.Report
{
    public class Strategy2020ReportBuilder
    {
        Strategy2020Content content;
        Strategy2020Filter filter;

        public Strategy2020ReportBuilder(Strategy2020Filter filter)
        {
            this.content = new Strategy2020Content();
            this.filter = filter;
        }

        public byte[] BuildReport()
        {

            this.content.StrategyList = this.filter.ApplyFilters();

            if (this.content.StrategyList.Count <= 0)
            {
                return null;
            }

            //StringBuilder headerHtml = new StringBuilder();
            StringBuilder bodyHtml = new StringBuilder();

            //headerHtml.Append("<table border=1>");
            //headerHtml.Append("<tr bgcolor='#6495ED'>");
            //headerHtml.Append("<td><h5><b>Type</b></h5></td>");
            //headerHtml.Append("<td><h5><b>Agenda</b></h5></td>");
            //headerHtml.Append("<td>");
            //headerHtml.Append("<table><tr>" +
            //    "<td width='13.1%'><h5><b>Business Policy</b></h5></td>" +
            //    "<td width='10%'><h5><b>Business Rule</b></h5></td>" +
            //    "<td width='14.8%'><h5><b>Process</b></h5></td>" +
            //    "<td width='12.45%'><h5><b>Application</b></h5></td>" +
            //    "<td><h5><b>Sub-Process</b></h5></td>" +
            //    "<td><h5><b>Module</b></h5></td></tr></table></td>");
            //headerHtml.Append("</tr>");
            //headerHtml.Append("</table>");


            bodyHtml.Append("<table border=0>");

            foreach (var item in content.StrategyList)
            {
                #region policies
                StringBuilder policies = new StringBuilder();

                policies.Append("<table border=0 cellspacing='0' cellpadding='0' width='100%'>");
                int ctr1 = 0;
                foreach (var ip in item.Policies)
                {
                    policies.Append("<tr>");
                    policies.AppendFormat("<td width='13.2%' valign='top' bgcolor='{0}'><font size='1'>{1}</font></td>", DetermineBgColor(ctr1), ip.BusinessPolicyName);

                    ctr1++;
                    policies.Append("<td>");
                    policies.Append("<table border=1 width='100%'>");

                    int ctr2 = 0;
                    foreach (var r in ip.Rules)
                    {
                        policies.Append("<tr>");
                        policies.AppendFormat("<td width='29%'  valign='top' bgcolor='{0}'><font size='1'>{1}</font></td>", DetermineBgColor(ctr2), r.BusinessRuleName);

                        ctr2++;
                        policies.Append("<td>");

                        policies.Append("<table border=0>");
                        int ctr3 = 0;
                        foreach (var p in r.Processes)
                        {
                            policies.Append("<tr>");
                            policies.AppendFormat("<td width='29.8%' valign='top' bgcolor='{0}'><font size='1'>{1}</font></td>", DetermineBgColor(ctr3), p.ProcessName);
                            ctr3++;

                            if (p.Application.Count == 1)
                            {
                                policies.AppendFormat(
                                    "<td width='24.7%' valign='top'><font size='1'>{0}</font></td>",
                                    p.Application.FirstOrDefault().ApplicationName
                                    );
                            }
                            else
                            {
                                policies.Append("<td width='24.7%' valign='top'>");
                                int ctr4 = 0;
                                foreach (var app in p.Application)
                                {
                                    policies.AppendFormat("<span><font size='1'>{0}</font></span><br />", app.ApplicationName);
                                    ctr4++;
                                }
                                policies.Append("</td>");
                            }

                            policies.Append("<td valign='top'>");
                            policies.Append("<table border=0 width='100%'>");
                            int ctr5 = 0;
                            foreach (var sp in p.SubProcesses)
                            {
                                policies.Append("<tr>");
                                policies.AppendFormat("<td valign='top' width='99.4%' class='pStyle' bgcolor='{0}'><font size='1'>{1}</font></td>", DetermineBgColor(ctr5), sp.SubProcessName);
                                ctr5++;

                                policies.Append("<td>");

                                policies.Append("<table>");
                                int ctr6 = 0;
                                foreach (var mod in sp.Modules)
                                {
                                    policies.Append("<tr>");
                                    policies.AppendFormat("<td valign='top' bgcolor='{0}'><font size='1'>{1}</font></td>", DetermineBgColor(ctr6), mod.ModuleName);
                                    policies.Append("</tr>");
                                    ctr6++;
                                }
                                policies.Append("</table>");
                                policies.Append("</td>");


                                policies.Append("</tr>");
                            }
                            policies.Append("</table>");

                            policies.Append("</td>");



                            policies.Append("</tr>");
                        }
                        policies.Append("</table>");
                        policies.Append("</td>");
                        policies.Append("</tr>");
                    }
                    policies.Append("</table>");
                    policies.Append("</td>");
                    policies.Append("</tr>");
                }
                policies.Append("</table>");
                #endregion
                //<td width='10%' valign='top'><font size='1'>{0}</font></td>
                bodyHtml.AppendFormat(
                    "<tr><td width='10%' valign='top'><font size='1'>{0}</font></td><td valign='top'>{1}</td></tr>",

                    item.Agenda,
                    policies.ToString()
                    );

            }
            bodyHtml.Append("</table>");


            byte[] output = CreatePDF(bodyHtml.ToString());
            return output;
        }

        private string DetermineBgColor(int counter)
        {
            string css = string.Empty;

            if (counter % 2 == 0)
                css = "#f9f9f9";
            else
                css = "";

            return css;
        }

        private byte[] CreatePDF(string html)
        {
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetMargins(50, 50, 10, 40);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);
            writer.PageEvent = new ImpactAnalysisHeaderHandler();
            document.Open();

            PdfPTable frontPage = CreateFrontPage();
            document.Add(frontPage);

            iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
            var elem = HTMLWorker.ParseToList(new StringReader(html), styles);

            foreach (IElement item in elem)
            {
                if (item is PdfPTable)
                {
                    PdfPTable t = item as PdfPTable;
                    if (t != null)
                    {
                        t.SplitLate = false;
                        t.KeepTogether = true;
                        PdfPHelper.FixTables(t);
                        PdfPHelper.FixMainRowsPadding(t);
                        document.Add(t);
                    }
                }
            }
            document.Close();

            return output.ToArray();
        }

        private PdfPTable CreateFrontPage()
        {
            PdfPTable table = new PdfPTable(2);
            table.SetWidths(new float[] { 25,75 });
            table.WidthPercentage = 100;

            table.AddCell(new PdfPCell() {
                Phrase = new Phrase(GlobalStringResource.ForOfficialUseOnly, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20)),
                Colspan = 2,
                Border = 0,
                PaddingTop = 70
            });

            table.AddCell(
                new PdfPCell()
                {
                    Phrase = new Phrase("Impact Analysis Report", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 36)),
                    Colspan = 2,
                    Border = 0,
                    PaddingLeft = 0,
                    PaddingBottom = 150
                });

            table.AddCell(
                PdfPHelper.CreatePaddingCell()
                );
            table.AddCell(
                PdfPHelper.CreatePaddingCell()
            );
             table.AddCell(
                PdfPHelper.CreatePlainCell("Agenda", BaseColor.LIGHT_GRAY)
                );
            table.AddCell(
                PdfPHelper.CreatePlainCell(this.filter.AgendaFilterText)
                );

            table.AddCell(
                PdfPHelper.CreatePlainCell("Business Policy", BaseColor.LIGHT_GRAY)
                );
            table.AddCell(
                PdfPHelper.CreatePlainCell(this.filter.PolicyFilterText)
                );

            table.AddCell(
                PdfPHelper.CreatePlainCell("Business Rule", BaseColor.LIGHT_GRAY)
            );
            table.AddCell(
                PdfPHelper.CreatePlainCell(this.filter.RuleFilterText)
                );

            table.AddCell(
                PdfPHelper.CreatePlainCell("Process", BaseColor.LIGHT_GRAY)
            );
            table.AddCell(
                PdfPHelper.CreatePlainCell(this.filter.ProcessFilterText)
            );

            table.AddCell(
                PdfPHelper.CreatePlainCell("Application", BaseColor.LIGHT_GRAY)
            );
            table.AddCell(
                PdfPHelper.CreatePlainCell(this.filter.ApplicationFilterText)
            );

            table.AddCell(
                PdfPHelper.CreatePlainCell("Sub-Process", BaseColor.LIGHT_GRAY)
            );
            table.AddCell(
                PdfPHelper.CreatePlainCell(this.filter.SubProcessFilterText)
            );

            table.AddCell(
                PdfPHelper.CreatePlainCell("Module", BaseColor.LIGHT_GRAY)
            );
            table.AddCell(
                PdfPHelper.CreatePlainCell(this.filter.ModuleFilterText)
            );

            table.AddCell(
                new PdfPCell() { 
                    Border = 0,
                    PaddingBottom = 100,
                    Colspan = 2
                }
                );

            return table;
        }

    }

    public static class PdfPHelper
    {
        public static void FixMainRowsPadding(PdfPTable mainTable)
        {
            for (int i = 0; i < mainTable.Rows.Count; i++)
            {
                PdfPRow contentRow = mainTable.Rows[i];
                foreach (PdfPCell cell in contentRow.GetCells())
                {
                    cell.Padding = 0;
                    cell.Top = 100;

                    if (cell.CompositeElements == null) continue;

                    IElement elem = cell.CompositeElements.Where(c => c.Type == 23).FirstOrDefault();
                    if (elem != null && elem is PdfPTable)
                    {
                        PdfPTable t = elem as PdfPTable;
                        if (t != null)
                        {
                            foreach (PdfPRow tr in t.Rows)
                            {
                                foreach (PdfPCell trc in tr.GetCells())
                                {
                                    trc.Padding = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void FixTables(PdfPTable t)
        {
            foreach (PdfPRow row in t.Rows)
            {
                PdfPCell[] rCell = row.GetCells();
                foreach (PdfPCell c in rCell)
                {
                    c.BorderWidth = .5f;
                    c.UseBorderPadding = false;
                    List<IElement> compositeElements = c.CompositeElements;
                    if (compositeElements != null && compositeElements.Count > 0)
                    {
                        foreach (IElement e in compositeElements)
                        {

                            if (e is PdfPTable)
                            {
                                PdfPTable lastTable = e as PdfPTable;
                                if (lastTable != null)
                                {
                                    lastTable.SplitLate = false;
                                    lastTable.KeepTogether = true;
                                    FixTables(lastTable);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static PdfPCell CreatePlainCell(string text)
        {
            return new PdfPCell(
                new Phrase(text)
                );
        }

        public static PdfPCell CreatePlainCell(string text, BaseColor color)
        {
            PdfPCell cell = new PdfPCell();
            cell.BackgroundColor = color;
            cell.Phrase = new Phrase(text);
            return cell;
        }

        public static PdfPCell CreatePaddingCell()
        {
            return new PdfPCell(
                new Phrase(string.Empty)
            ) { Padding = 10, Border = 0 };
        }
    }

    public class ImpactAnalysisHeaderHandler : PdfPageEventHelper
    {
        int ctr = 0;
        PdfTemplate total;
        public ImpactAnalysisHeaderHandler()
        {

        }

        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            if (ctr == 0)
            {
                ctr++;
                return;
            }

            iTextSharp.text.Image header = iTextSharp.text.Image.GetInstance(PathResolver.MapPath("images/ADB_Logo.gif"));

            PdfPTable tableHeader = new PdfPTable(2);
            tableHeader.WidthPercentage = 100;

            PdfPCell imageHeaderCell = new PdfPCell(header);
            imageHeaderCell.Rowspan = 2;
            imageHeaderCell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            imageHeaderCell.Border = 0;
            imageHeaderCell.PaddingBottom = 3;
            tableHeader.AddCell(imageHeaderCell);

            iTextSharp.text.Font helvetica20 = FontFactory.GetFont(FontFactory.HELVETICA, 16);
            PdfPCell typeNameCell = new PdfPCell(new Phrase(""));
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

            PdfPTable table = new PdfPTable(7);
            table.SplitLate = false;
            table.KeepTogether = false;
            table.SkipFirstHeader = true;

            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 
                80,
                84,
                143,
                95,
                79,
                159,
                160
            });

            System.Drawing.Color c = ColorTranslator.FromHtml("#6495ED");
            BaseColor realColor = new BaseColor(c);
            iTextSharp.text.Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

            //table.AddCell(new PdfPCell(
            //    new Phrase("Type")) { BackgroundColor = realColor, Padding = 5 });
            table.AddCell(new PdfPCell(
                new Phrase("Agenda", font)) { BackgroundColor = realColor, Padding = 5 });
            table.AddCell(new PdfPCell(
                new Phrase("Business Policy", font)) { BackgroundColor = realColor, Padding = 5 });
            table.AddCell(new PdfPCell(
                new Phrase("Business Rule", font)) { BackgroundColor = realColor, Padding = 5 });
            table.AddCell(new PdfPCell(
                new Phrase("Process", font)) { BackgroundColor = realColor, Padding = 5 });
            table.AddCell(new PdfPCell(
                new Phrase("Application", font)) { BackgroundColor = realColor, Padding = 5 });
            table.AddCell(new PdfPCell(
                new Phrase("Sub-Process", font)) { BackgroundColor = realColor, Padding = 5 });
            table.AddCell(new PdfPCell(
                new Phrase("Module", font)) { BackgroundColor = realColor, Padding = 5 });

            document.Add(table);
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            total = writer.DirectContent.CreateTemplate(30, 16);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable footer = new PdfPTable(3);
            footer.SetWidths(new float[] { 88f, 7f, 5f });
            footer.WidthPercentage = 100;
            footer.TotalWidth = document.PageSize.Width - (document.LeftMargin + document.RightMargin);

            PdfPCell emptycell = new PdfPCell();
            emptycell.Border = 0;
            footer.AddCell(emptycell);


            Chunk text = new Chunk(string.Format(GlobalStringResource.PageOfFooter,
                document.PageNumber), FontFactory.GetFont(FontFactory.HELVETICA, 8));

            PdfPCell footerCell = new PdfPCell(new Phrase(text));
            footerCell.Border = 0;
            footerCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            footer.AddCell(footerCell);

            PdfPCell cell = new PdfPCell(iTextSharp.text.Image.GetInstance(total));
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            footer.AddCell(cell);
            footer.WriteSelectedRows(0, -1, 50, (document.BottomMargin - 10), writer.DirectContent);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            ColumnText.ShowTextAligned(total, Element.ALIGN_LEFT,
                    new Phrase((writer.PageNumber - 1).ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8)),
                    2, 6, 0);
        }

    }
}
