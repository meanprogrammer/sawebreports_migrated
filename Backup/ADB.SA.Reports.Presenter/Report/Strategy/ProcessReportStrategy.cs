using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Presenter.Content;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.Enums;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Report
{
    public class ProcessReportStrategy : ReportStrategyBase
    {
        EntityDTO dto;
        EntityData data = null;

        public ProcessReportStrategy()
        {
            data = new EntityData();
        }

        public override List<PdfContentParameter> BuildContent(EntityDTO dto)
        {
            dto.ExtractProperties();
            this.dto = dto;
            List<PdfContentParameter> contents = new List<PdfContentParameter>();
            contents.Add(base.CreateTitlePage(this.dto));
            contents.Add(base.CreateChangeHistory(this.dto));
            contents.Add(base.CreateReviewers(this.dto));
            contents.Add(base.CreateApprovers(this.dto));
            contents.Add(CreateTitle(GlobalStringResource.Report_Introduction));
            contents.Add(CreatePurpose());
            contents.Add(CreateProcessObjective());
            contents.Add(CreateStrategy());
            contents.Add(CreateStakeHolders());
            contents.Add(CreateProcessDescription());
            contents.Add(CreateProcessRelation());
            contents.Add(CreateSubProcessRelation());
            contents.Add(CreateTitle(GlobalStringResource.Report_References));
            contents.Add(CreateFrameworkReference());
            contents.Add(CreateInternalReference());
            contents.Add(CreateTitle(GlobalStringResource.Report_Appendices));
            contents.Add(CreateAcronyms());
            contents.Add(CreateDefintionOfTerms());
            contents.Add(CreateBookmarks());
            return contents;
        }

        private PdfContentParameter CreateBookmarks()
        {
            List<EntityDTO> bookmarks = data.GetBookmarks(this.dto.ID);
            PdfPTable t = CreateTable(2, true);
            t.SetWidths(new float[] { 1f,4f });
            t.AddCell(
                    CreateHeader3(GlobalStringResource.Report_Appendix_3, 2)
                );
            t.AddCell(
                    CreateHeaderCell(GlobalStringResource.Name)
                );
            t.AddCell(
                    CreateHeaderCell(GlobalStringResource.ReferenceLink)
                );

            if (bookmarks.Count > 0)
            {
                foreach (EntityDTO related in bookmarks)
                {
                    related.ExtractProperties();
                    t.AddCell(
                            CreatePlainContentCell(related.Name)
                        );
                    t.AddCell(
                            CreatePlainContentCell(related.RenderHTML(" Reference Link", RenderOption.NewLine))
                        );
                }
            }
            t.AddCell(CreatePaddingCell(2, 15));
            return new PdfContentParameter() { Table = t };
        }

        private PdfContentParameter CreateDefintionOfTerms()
        {
            List<EntityDTO> dots = data.GetDefinitionOfTerms(this.dto.ID);
            PdfPTable t = CreateTable(2, true);
            t.SetWidths(new float[] { 1f, 4f });
            t.AddCell(
                    CreateHeader3(GlobalStringResource.Report_Appendix_2, 2)
                );
            t.AddCell(
                    CreateHeaderCell(GlobalStringResource.Name)
                );
            t.AddCell(
                    CreateHeaderCell(GlobalStringResource.Explanation)
                );

            if (dots.Count > 0)
            {
                foreach (EntityDTO related in dots)
                {
                    related.ExtractProperties();
                    t.AddCell(
                            CreatePlainContentCell(related.Name)
                        );
                    t.AddCell(
                            CreatePlainContentCell(
                                related.RenderHTML(
                                    GlobalStringResource.Explanation, 
                                    RenderOption.NewLine))
                        );
                }
            }
            t.AddCell(CreatePaddingCell(2, 15));
            return new PdfContentParameter() { Table = t };
        }

        private PdfContentParameter CreateAcronyms()
        {
            List<EntityDTO> acronyms = data.GetAcronyms(this.dto.ID);

            PdfPTable t = CreateTable(2, true);
            t.SetWidths(new float[] { 1f,4f });
            t.AddCell(
                    CreateHeader3(GlobalStringResource.Report_Appendix_1, 2)
                );
            t.AddCell(
                    CreateHeaderCell(GlobalStringResource.Abbreviation)
                );
            t.AddCell(
                    CreateHeaderCell(GlobalStringResource.AbbreviationDescription)
                );

            if (acronyms.Count > 0)
            {
                foreach (EntityDTO related in acronyms)
                {
                    related.ExtractProperties();

                    t.AddCell(
                            CreatePlainContentCell(related.Name)
                        );
                    t.AddCell(
                            CreatePlainContentCell(
                                related.RenderHTML(
                                    GlobalStringResource.AbbreviationDescription, 
                                    RenderOption.None))
                        );
                }
            }
            t.AddCell(CreatePaddingCell(2, 15));
            return new PdfContentParameter() { Table = t };
        }

        private PdfContentParameter CreateFrameworkReference()
        {
            List<EntityDTO> frameworks = data.GetFrameworkReference(this.dto.ID);
            PdfPTable t = CreateTable(3, true);
            t.SetWidths(new float[] { 2f,4f,4f });

            t.AddCell(CreateHeader3(GlobalStringResource.Report_Framework_Ref, 3));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Framework));
            t.AddCell(CreateHeaderCell(GlobalStringResource.FrameworkIndexID));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Description));

            if (frameworks.Count > 0)
            {
                foreach (EntityDTO related in frameworks)
                {
                    related.ExtractProperties();
                    t.AddCell(
                            CreatePlainContentCell(related.Name)
                        );
                    t.AddCell(
                            CreatePlainContentCell(
                                related.RenderHTML(GlobalStringResource.FrameworkIndexID, 
                                RenderOption.NewLine))
                        );
                    t.AddCell(
                            CreatePlainContentCell(
                                related.RenderHTML(GlobalStringResource.FrameworkDescription, 
                                RenderOption.NewLine))
                        );
                }
            }
            t.AddCell(CreatePaddingCell(3, 15));
            return new PdfContentParameter() { Table = t };
        }

        private PdfContentParameter CreateInternalReference()
        {
            List<EntityDTO> references = data.GetInternalReference(this.dto.ID);

            PdfPTable t = CreateTable(3, true);
            t.SetWidths(new float[] { 2f, 4f, 4f });

            t.AddCell(CreateHeader3(GlobalStringResource.Report_Internal_Ref, 3));
            t.AddCell(CreateHeaderCell(GlobalStringResource.DocumentType));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Title));
            t.AddCell(CreateHeaderCell(GlobalStringResource.DocumentReferenceNumber));

            if (references.Count > 0)
            {
                foreach (EntityDTO related in references)
                {
                    related.ExtractProperties();
                    t.AddCell(
                            CreatePlainContentCell(related.RenderHTML(GlobalStringResource.ReferenceType, RenderOption.NewLine))
                        );
                    t.AddCell(
                            CreatePlainContentCell(related.RenderHTML(GlobalStringResource.Description, RenderOption.NewLine))
                    );
                    t.AddCell(
                            CreatePlainContentCell(related.Name)
                    );
                }
            }

            t.AddCell(CreatePaddingCell(3, 15));
            return new PdfContentParameter() { Table = t };
        }

        private PdfContentParameter CreateSubProcessRelation()
        {
            List<EntityDTO> relatedSubProcess = data.GetRelatedSubProcess(this.dto.ID);

            PdfPTable t = CreateTable(4, true);
            t.SetWidths(new float[] { 2f,3f,10f,3f });
            t.AddCell(
                    CreateBigHeader(GlobalStringResource.Report_SubProcesses_5, 4, 15)
                );
            t.AddCell(CreateHeaderCell(GlobalStringResource.SubProcessNumber));
            t.AddCell(CreateHeaderCell(GlobalStringResource.SubProcess));
            t.AddCell(CreateHeaderCell(GlobalStringResource.SubProcessOverview));
            t.AddCell(CreateHeaderCell(GlobalStringResource.SubProcessOwner));

            if (relatedSubProcess.Count > 0)
            {

                foreach (EntityDTO related in relatedSubProcess)
                {
                    related.ExtractProperties();

                    t.AddCell(
                            CreatePlainContentCell(
                                related.RenderHTML(GlobalStringResource.SubProcessNumber, 
                                RenderOption.NewLine))
                        );
                    t.AddCell(
                            CreatePlainContentCell(related.Name)
                        );
                    t.AddCell(
                            CreatePlainContentCell(
                                related.RenderHTML(GlobalStringResource.Description, 
                                RenderOption.NewLine))
                        );
                    t.AddCell(
                            CreatePlainContentCell(
                                related.RenderHTML(GlobalStringResource.DocumentOwners, 
                                RenderOption.NewLine))
                        );
                }
            }
            t.AddCell(CreatePaddingCell(4, 15));
            return new PdfContentParameter() { Table = t, 
                Header = GlobalStringResource.Report_SubProcesses_5 };
        }

        private PdfContentParameter CreateProcessRelation()
        {
            List<EntityDTO> relatedProcess = data.GetRelatedProcess(this.dto.ID);

            PdfPTable t = CreateTable(3, true);
            t.SetWidths(new float[] { 2f,3f,5f });

            t.AddCell(
                CreateBigHeader(GlobalStringResource.Report_RelationshipwithotherProcesses_4, 3, 15)
            );

            t.AddCell(CreateHeaderCell(GlobalStringResource.ReferenceNumber));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Process));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Relationship));

            if (relatedProcess.Count > 0)
            {
                foreach (EntityDTO related in relatedProcess)
                {
                    related.ExtractProperties();
                    t.AddCell(
                        CreatePlainContentCell(
                            related.RenderHTML(GlobalStringResource.ReferenceNumber, 
                            RenderOption.NewLine))
                        );

                    t.AddCell(
                        CreatePlainContentCell(
                            related.RenderHTML(GlobalStringResource.Process, 
                            RenderOption.NewLine))
                        );

                    t.AddCell(
                        CreatePlainContentCell(
                            related.RenderHTML(GlobalStringResource.Relationship, 
                            RenderOption.NewLine))
                        );
                }
            }

            t.AddCell(CreatePaddingCell(3, 15));

            return new PdfContentParameter() { Table = t, 
                Header = GlobalStringResource.Report_RelationshipwithotherProcesses_4 };
        }

        private PdfContentParameter CreateProcessDescription()
        {
            PdfPTable t = CreateTable(1, true);
            t.AddCell(
                    CreateHeaderCell(GlobalStringResource.Report_ProcessDescription_3)
                );
            PdfPCell cell = CreatePlainContentCell(
                dto.RenderHTML(GlobalStringResource.ProcessDescription, 
                RenderOption.NewLine));
            cell.Border = 0;
            t.AddCell(
                    cell
                );

            t.AddCell(CreatePaddingCell(1, 15));
            return new PdfContentParameter() { Table = t, 
                Header = GlobalStringResource.Report_ProcessDescription_3 };
        }

        private PdfContentParameter CreateStakeHolders()
        {
            List<EntityDTO> rolesAndResp = data.GetRolesAndResponsibilities(this.dto.ID);
            PdfPTable t = CreateTable(2, true);
            t.AddCell(
                    CreateHeader3(GlobalStringResource.Report_Stakeholders_2, 2)
                );
            t.AddCell(
                    CreateHeaderCell(GlobalStringResource.Role)
                );
            t.AddCell(
                    CreateHeaderCell(GlobalStringResource.Responsibility)
                );

            if (rolesAndResp.Count > 0)
            {
                foreach (EntityDTO related in rolesAndResp)
                {
                    related.ExtractProperties();

                    t.AddCell(
                        CreatePlainContentCell(
                            related.RenderHTML(GlobalStringResource.Role, RenderOption.NewLine))
                        );
                    t.AddCell(
                        CreatePlainContentCell(
                            related.RenderHTML(GlobalStringResource.Responsibilities, 
                            RenderOption.NewLine))
                        );

                }
            }
            t.AddCell(CreatePaddingCell(2, 15));

            return new PdfContentParameter() { Table = t, 
                Header = GlobalStringResource.Report_Stakeholders_2 };
        }

        private PdfContentParameter CreateStrategy()
        {
            PdfPTable t = CreateTable(1, true);
            t.AddCell(
                    CreateHeader3(GlobalStringResource.Report_Strategy_C, 1)
                );
            PdfPCell cell = CreatePlainContentCell(
                    dto.RenderHTML(GlobalStringResource.Strategy, RenderOption.NewLine)
                );
            cell.Border = 0;
            t.AddCell(
                    cell
                );

            t.AddCell(CreatePaddingCell(1, 15));
            return new PdfContentParameter() { Table = t, 
                Header = GlobalStringResource.Report_Strategy_C };
        }

        private PdfContentParameter CreateProcessObjective()
        {
            PdfPTable t = CreateTable(1, true);
            t.AddCell(
                    CreateHeader3(GlobalStringResource.Report_ProcessObjective_B, 1)
                );
            PdfPCell cell = CreatePlainContentCell(
                    dto.RenderHTML(GlobalStringResource.ProcessObjective, RenderOption.NewLine)
                );
            cell.Border = 0;
            t.AddCell(
                    cell
                );

            t.AddCell(CreatePaddingCell(1, 15));
            return new PdfContentParameter() { Table = t, 
                Header = GlobalStringResource.Report_ProcessObjective_B };
        }

        private PdfContentParameter CreatePurpose()
        {
            PdfPTable t = CreateTable(1, true);
            t.AddCell(
                    CreateHeader3(GlobalStringResource.Report_Purpose_A, 1)
                );
            PdfPCell cell = CreatePlainContentCell(
                        dto.RenderHTML(GlobalStringResource.Description, RenderOption.NewLine), 1
                    );
            cell.Border = 0;
            t.AddCell(
                     cell
                );

            t.AddCell(CreatePaddingCell(1, 15));

            return new PdfContentParameter() { Table = t, 
                Header = GlobalStringResource.Report_Purpose_A };

        }

        private PdfContentParameter CreateTitle(string title)
        {
            PdfPTable t = CreateTable(1, true);
            t.AddCell(
                CreateBigHeader(title, 1, 15)
                );
            return new PdfContentParameter() { Header = title, Table = t };
        }
    }
}
