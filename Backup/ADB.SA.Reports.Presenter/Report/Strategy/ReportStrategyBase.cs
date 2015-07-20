using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Presenter.Content;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.Utils;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Report
{
    public abstract class ReportStrategyBase
    {
        public abstract List<PdfContentParameter> BuildContent(EntityDTO dto);

        protected virtual PdfContentParameter CreateChangeHistory(EntityDTO dto)
        {
            EntityData data = new EntityData();

            PdfPTable t = CreateTable(4, true);
            float[] widths = new float[] { 1f, 2f, 6f, 3f };
            t.SetWidths(widths);
            List<EntityDTO> changehistory = data.GetChangeHistory(dto.ID);
            ChangeHistoryComparer comparer = new ChangeHistoryComparer();
            changehistory.Sort(comparer);

            t.AddCell(CreateHeader3(GlobalStringResource.DocumentHistoryAndApproval, 4));
            t.AddCell(CreatePaddingCell(4, 10));
            t.AddCell(CreateHeader3(GlobalStringResource.ChangeHistory, 4));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Version));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Date));
            t.AddCell(CreateHeaderCell(GlobalStringResource.ReasonforChange));
            t.AddCell(CreateHeaderCell(GlobalStringResource.AuthorOnly));

            if (changehistory.Count > 0)
            {
                foreach (EntityDTO related in changehistory)
                {
                    related.ExtractProperties();

                    t.AddCell(CreatePlainContentCell(related.RenderHTML(GlobalStringResource.Version, RenderOption.None)));
                    t.AddCell(CreatePlainContentCell(related.RenderHTML(GlobalStringResource.Date, RenderOption.None)));
                    t.AddCell(CreatePlainContentCell(related.RenderHTML(GlobalStringResource.ReasonforChange, RenderOption.NewLine)));
                    t.AddCell(CreatePlainContentCell(related.RenderHTML(GlobalStringResource.AuthorOnly, RenderOption.NewLine)));
                }
            }
            t.AddCell(CreatePaddingCell(4, 15));
            return new PdfContentParameter() { Table = t };
        }

        public virtual PdfContentParameter CreateTitlePage(EntityDTO dto)
        {
            PdfPTable table = CreateTable(2, true);
            Font helvetica14Bold = FontFactory.GetFont(FontFactory.HELVETICA, 14);


            PdfPCell officialUseCell = new PdfPCell();
            officialUseCell.Colspan = 2;
            officialUseCell.Phrase = new Phrase(GlobalStringResource.ForOfficialUseOnly, helvetica14Bold);
            officialUseCell.Border = 0;
            officialUseCell.PaddingTop = 20;
            officialUseCell.PaddingBottom = 10;

            table.AddCell(officialUseCell);

            Font h1 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 28);

            PdfPCell titleCell = new PdfPCell();
            titleCell.Border = 0;
            titleCell.Phrase = new Phrase(dto.Name, h1);
            titleCell.Colspan = 2;
            table.AddCell(titleCell);

            PdfPCell spaceCell = new PdfPCell();
            spaceCell.Colspan = 2;
            spaceCell.Border = 0;
            spaceCell.PaddingBottom = 200;

            table.AddCell(spaceCell);

            table.AddCell(CreateHeaderCell(GlobalStringResource.DocumentOwners));
            table.AddCell(CreateHeaderCell(GlobalStringResource.Authors));
            table.AddCell(CreatePlainContentCell(dto.RenderHTML(GlobalStringResource.DocumentOwners, ADB.SA.Reports.Entities.Enums.RenderOption.NewLine)));
            table.AddCell(CreatePlainContentCell(dto.RenderHTML(GlobalStringResource.Authors, ADB.SA.Reports.Entities.Enums.RenderOption.NewLine)));

            return new PdfContentParameter() { Table = table, Header = string.Empty, };
        }

        protected virtual PdfContentParameter CreateReviewers(EntityDTO dto)
        {
            PdfPTable t = CreateTable(4, true);
            float[] widths = new float[] { 2f, 5f, 1f, 1f };
            t.SetWidths(widths);
            t.AddCell(CreateHeader3(GlobalStringResource.Reviewers, 4));

            t.AddCell(CreateHeaderCell(GlobalStringResource.Name));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Position));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Date));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Signature));

            EntityData entityData = new EntityData();
            //TODO:
            //What if both have reviewers 
            List<string> reviewers = RemoveBelongsToOrg(dto.GetPropertyList(GlobalStringResource.Reviewers));
            List<EntityDTO> reviewerList = entityData.GetReviewersAndApprovers(dto.ID);

            if (reviewers != null && reviewers.Count > 0)
            {
                foreach (string reviewer in reviewers)
                {
                    EntityDTO rev = reviewerList.Find(x => x.Name == reviewer.Trim());
                    if (rev == null)
                    {
                        t.AddCell(string.Empty);
                    }
                    else
                    {
                        rev.ExtractProperties();
                        t.AddCell(new Phrase(rev.RenderHTML("Assigned to", RenderOption.None), FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                    }
                    t.AddCell(new Phrase(reviewer, FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                    t.AddCell(string.Empty);
                    t.AddCell(string.Empty);

                }
            }

            t.AddCell(CreatePaddingCell(4, 15));
            return new PdfContentParameter() { Table = t };
        }

        protected virtual PdfContentParameter CreateApprovers(EntityDTO dto)
        {
            //TODO: Must be checked against the related entities in the database
            PdfPTable t = CreateTable(4, true);
            float[] widths = new float[] { 2f, 5f, 1f, 1f };
            t.SetWidths(widths);
            t.AddCell(CreateHeader3(GlobalStringResource.Approvers, 4));

            t.AddCell(CreateHeaderCell(GlobalStringResource.Name));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Position));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Date));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Signature));

            EntityData entityData = new EntityData();

            List<string> approvers = RemoveBelongsToOrg(dto.GetPropertyList(GlobalStringResource.Approvers));
            List<EntityDTO> approverList = entityData.GetReviewersAndApprovers(dto.ID);

            if (approvers != null && approvers.Count > 0)
            {
                foreach (string approver in approvers)
                {
                    EntityDTO app = approverList.Find(x => x.Name == approver.Trim());
                    if (app == null)
                    {
                        t.AddCell(string.Empty);
                    }
                    else
                    {
                        app.ExtractProperties();
                        t.AddCell(new Phrase(app.RenderHTML("Assigned to", RenderOption.None), FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                    }
                    t.AddCell(new Phrase(approver, FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                    t.AddCell(string.Empty);
                    t.AddCell(string.Empty);
                }
            }

            t.AddCell(CreatePaddingCell(4, 15));
            return new PdfContentParameter() { Table = t };
        }

        public PdfPTable CreateTable(int columns, bool isFullWidth)
        {
            PdfPTable table = new PdfPTable(columns);
            if (isFullWidth)
            {
                table.WidthPercentage = 100;
            }
            return table;
        }

        public PdfPCell CreateHeaderCell(string title)
        {
            return CreateHeaderCell(title, 0);
        }

        public PdfPCell CreateHeaderCell(string title, int span)
        {
            return new PdfPCell()
            {
                Phrase = new Phrase(title, FontFactory.GetFont(GlobalStringResource.Font_Arial, 10, 1)),
                BackgroundColor = iTextSharp.text.pdf.CMYKColor.LIGHT_GRAY,
                Colspan = span
            };
        }

        public PdfPCell CreatePaddingCell(int colspan, float padding)
        {
            return new PdfPCell()
            {
                Colspan = colspan,
                PaddingBottom = padding,
                Border = 0,
            };
        }

        public PdfPCell CreatePlainContentCell(string content)
        {
            return CreatePlainContentCell(content, 0);
        }

        public PdfPCell CreatePlainContentCell(string content, int span)
        {
            return new PdfPCell()
            {
                Phrase = new Phrase(content, FontFactory.GetFont(GlobalStringResource.Font_Arial, 10)),
                Colspan = span,
            };
        }

        public static PdfPCell CreateHeader3(string title, int colspan)
        {
            return new PdfPCell()
            {
                Colspan = colspan,
                Phrase = new Phrase(title, FontFactory.GetFont(GlobalStringResource.Font_Arial, 14, 1)),
                PaddingBottom = 5,
                Border = 0,
            };
        }

        public static PdfPCell CreateBigHeader(string title, int colspan, float paddingBottom)
        {
            return new PdfPCell()
            {
                Phrase = new Phrase(title, FontFactory.GetFont(GlobalStringResource.Font_Arial, 18, 1)),
                Border = 0,
                PaddingBottom = paddingBottom,
                Colspan = colspan,
            };
        }

        private List<string> RemoveBelongsToOrg(List<string> baseList)
        {
            if (baseList == null || baseList.Count == 0)
            {
                return new List<string>();
            }

            List<string> newList = new List<string>();
            foreach (string item in baseList)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    int dotIndex = item.IndexOf('.');
                    if (dotIndex >= 0)
                    {
                        string[] splitted = item.Split('.');
                        string result = item.Replace(splitted.Last(), "");

                        if(result.EndsWith("."))
                        {
                            result = result.TrimEnd(new char[]{ '.' });
                        }
                        newList.Add(result);
                    }
                }
            }
            return newList;
        }
    }
}
