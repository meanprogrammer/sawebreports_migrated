using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using ADB.SA.Reports.Entities.DTO;
using iTextSharp.text;
using ADB.SA.Reports.Presenter.Content;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.Utils;
using ADB.SA.Reports.Entities.Enums;
using iTextSharp.text.html.simpleparser;
using System.IO;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Report
{
    public class SubProcessReportStrategy : ReportStrategyBase
    {
        EntityDTO dto;
        EntityData entityData;

        public SubProcessReportStrategy()
        {
            entityData = new EntityData();
        }

        public override List<PdfContentParameter> BuildContent(EntityDTO dto)
        {
            this.dto = dto;
            List<PdfContentParameter> contents = new List<PdfContentParameter>();
            contents.Add(base.CreateTitlePage(this.dto));
            contents.Add(base.CreateChangeHistory(this.dto));
            contents.Add(base.CreateReviewers(this.dto));
            contents.Add(base.CreateApprovers(this.dto));
            contents.Add(CreateSubProcessObjective());
            contents.Add(CreateRolesAndResponsibility());
            contents.Add(CreateSubProcessDependency());
            contents.Add(CreateSubProcessDiagramTitle());
            contents.Add(CreateActivityOverviewHeader());
            contents.AddRange(CreateActivityOverview());
            return contents;
        }

        private PdfContentParameter CreateSubProcessDiagramTitle()
        {
            PdfPTable t = CreateTable(1, true);
            t.AddCell(
                    CreateBigHeader(GlobalStringResource.Presenter_ReportSubProcessDiagram, 1, 10)
                );
            return new PdfContentParameter() { Table = t, 
                Header = GlobalStringResource.Presenter_ReportSubProcessDiagram, };
        }

        private PdfPTable CreateActivityOverviewTitle()
        {
            PdfPTable t = CreateTable(1, true);
            t.AddCell(
                    CreateBigHeader(GlobalStringResource.Presenter_ReportActivityOverview, 1, 10)
                );
            return t;
        }

        private PdfContentParameter CreateActivityOverviewHeader()
        {
            PdfPTable t = CreateTable(1, true);
            t.AddCell(CreateBigHeader(GlobalStringResource.Presenter_ReportActivityOverview, 1, 20));

            return new PdfContentParameter() { 
                Header = GlobalStringResource.Presenter_ReportActivityOverview, Table = t, };
        }

        private List<PdfContentParameter> CreateActivityOverview()
        {
            List<EntityDTO> activities = entityData.GetActivityOverview(this.dto.ID);
            List<PdfContentParameter> contents = new List<PdfContentParameter>();

            if (activities.Count > 0)
            {
                foreach (EntityDTO related in activities)
                {
                    PdfPTable t = CreateTable(5, true);
                    t.SplitLate = false;
                    t.SplitRows = true;
                    related.ExtractProperties();

                    t.AddCell(
                            CreateHeader3(related.Name, 5)
                        );

                    t.AddCell(CreateHeaderCell(GlobalStringResource.ActivityNumber));
                    t.AddCell(CreateHeaderCell(GlobalStringResource.User));
                    t.AddCell(CreateHeaderCell(GlobalStringResource.ActivityNature));
                    t.AddCell(CreateHeaderCell(GlobalStringResource.TriggerInput));
                    t.AddCell(CreateHeaderCell(GlobalStringResource.Output));

                    t.AddCell(CreatePlainContentCell(
                        related.RenderHTML(
                            GlobalStringResource.ActivityNumber, RenderOption.NewLine)));
                    t.AddCell(CreatePlainContentCell(
                        related.RenderHTML(
                            GlobalStringResource.User, RenderOption.NewLine)));
                    t.AddCell(CreatePlainContentCell(
                        related.RenderHTML(
                            GlobalStringResource.ActivityNature, RenderOption.NewLine)));
                    t.AddCell(CreatePlainContentCell(
                        related.RenderHTML(
                            GlobalStringResource.TriggerInput, RenderOption.NewLine)));
                    t.AddCell(CreatePlainContentCell(
                        related.RenderHTML(
                            GlobalStringResource.Output, RenderOption.NewLine)));

                    t.AddCell(CreateHeaderCell(GlobalStringResource.ActivityStepDescription, 2));
                    t.AddCell(CreateHeaderCell(GlobalStringResource.ActivityVariation, 2));
                    t.AddCell(CreateHeaderCell(GlobalStringResource.SampleReference, 1));

                    t.AddCell(CreatePlainContentCell(
                        related.RenderHTML(
                            GlobalStringResource.ActivityStepDescription,
                            RenderOption.NewLine), 2));

                    //t.AddCell(CreatePlainContentCell(related.RenderHTML("Activity Variation", RenderOption.NewLine), 2));

                    string avKeys = related.RenderHTML(GlobalStringResource.ActivityVariation, 
                        RenderOption.NewLine);
                    t.AddCell(
                            new PdfPCell(
                                CreateBusinessRulesRequiredData(avKeys)
                            ) { Colspan = 2, }
                        );

                    //t.AddCell(CreatePlainContentCell(related.RenderHTML("Sample References", RenderOption.NewLine), 1));
                    string srKeys = related.RenderHTML(GlobalStringResource.SampleReferences, 
                        RenderOption.NewLine);
                    t.AddCell(
                            new PdfPCell(
                                CreateBusinessRulesRequiredData(srKeys)
                            ) { Colspan = 1, }
                        );

                    t.AddCell(CreateHeaderCell(GlobalStringResource.OperationalRules, 1));
                    string brKeys = related.RenderHTML(GlobalStringResource.BussinessRule, 
                        RenderOption.NewLine);
                    t.AddCell(
                            new PdfPCell(
                                CreateBusinessRulesRequiredData(brKeys)
                            ) { Colspan = 4, }
                        );

                    t.AddCell(CreateHeaderCell(GlobalStringResource.RequiredData, 1));
                    string rdKeys = related.RenderHTML(GlobalStringResource.RequiredData, 
                        RenderOption.NewLine);
                    t.AddCell(
                            new PdfPCell(
                                CreateBusinessRulesRequiredData(rdKeys)
                                ) { Colspan = 4 }
                        );
                    contents.Add(new PdfContentParameter() { Table = t, Header = related.Name, });
                }

            }

            return contents;
        }

        private Phrase CreateBusinessRulesRequiredData(string businessRuleKey)
        {
            if (string.IsNullOrEmpty(businessRuleKey))
            {
                return new Phrase(new Chunk(string.Empty));
            }

            string[] keys = businessRuleKey.Split( new string[] { Environment.NewLine }, 
                StringSplitOptions.RemoveEmptyEntries );

            EntityData data = new EntityData();
            StringBuilder html = new StringBuilder();
            foreach (string key in keys)
            {
                EntityDTO dto = entityData.GetOneEntity(key.Trim());
                if (dto != null)
                {
                    dto.ExtractProperties();
                    html.AppendFormat("{0}{1}{2}", key,Environment.NewLine, 
                        dto.RenderHTML(GlobalStringResource.Description, 
                        RenderOption.NewLine));
                }
            }

            return new Phrase(new Chunk(html.ToString(),
                FontFactory.GetFont(GlobalStringResource.Font_Arial, 10)));
        }

        private PdfContentParameter CreateSubProcessObjective()
        {
            PdfPTable objective = CreateTable(1, true);
            objective.AddCell(
                    CreateBigHeader(
                        GlobalStringResource.Presenter_ReportSubProcessObjective, 
                        1, 0)
                );
            objective.AddCell(
                    new PdfPCell()
                    {
                        Phrase = new Phrase(
                            this.dto.RenderHTML(
                                GlobalStringResource.Description, 
                                RenderOption.NewLine), 
                                    FontFactory.GetFont(
                                        GlobalStringResource.Font_Arial, 10)
                                ),
                        Border = 0,
                        PaddingBottom = 15,
                    }
                );
            return new PdfContentParameter() { Table = objective,
                Header = GlobalStringResource.Presenter_ReportSubProcessObjective, };
        }

        private PdfContentParameter CreateRolesAndResponsibility()
        {
            List<EntityDTO> rolesAndResp = entityData.GetRolesAndResponsibilities(this.dto.ID);
            PdfPTable t = null;

            t = CreateTable(2, true);
            t.AddCell(
                    new PdfPCell()
                    {
                        Phrase = new Phrase(
                            GlobalStringResource.Presenter_ReportRolesAndResponsibilities,
                            FontFactory.GetFont(GlobalStringResource.Font_Arial, 18, 1))
                        {
                        },
                        Border = 0,
                        Colspan = 2,
                        PaddingBottom = 5,
                    }
                );

            t.AddCell(
                    CreateHeaderCell(GlobalStringResource.Role)
                );
            t.AddCell(
                    CreateHeaderCell(GlobalStringResource.Responsibilities)
                );

            if (rolesAndResp.Count > 0)
            {
                foreach (EntityDTO related in rolesAndResp)
                {
                    related.ExtractProperties();
                    EntityDTO descriptionDto = entityData.GetRolesDescription(related.ID);
                    string description = string.Empty;
                    if (descriptionDto != null)
                    {
                        descriptionDto.ExtractProperties();
                        description = descriptionDto.RenderHTML(
                            GlobalStringResource.Description,
                            RenderOption.NewLine);
                    }

                    t.AddCell(
                        CreatePlainContentCell(related.RenderHTML(
                        GlobalStringResource.Role, RenderOption.NewLine))
                        );
                    t.AddCell(
                            CreatePlainContentCell(description)
                        );

                }
            }
            t.AddCell(CreatePaddingCell(2, 15));
            return new PdfContentParameter() { Table = t,
                Header = GlobalStringResource.Presenter_ReportRolesAndResponsibilities, };
        }

        private PdfContentParameter CreateSubProcessDependency()
        {
            List<EntityDTO> relatedSubProcess = entityData.GetSubProcessDependencies(this.dto.ID);

            PdfPTable t = CreateTable(3, true);
            float[] widths = new float[] { 2f, 2f, 5f };
            t.SetWidths(widths);
            t.AddCell(CreateBigHeader(GlobalStringResource.Presenter_ReportSubProcessDependency, 4, 5));
            //t.AddCell(CreateHeaderCell(GlobalStringResource.SubProcessNumber));
            t.AddCell(CreateHeaderCell(GlobalStringResource.SubProcess));
            t.AddCell(CreateHeaderCell(GlobalStringResource.PrecedingSucceeding));
            t.AddCell(CreateHeaderCell(GlobalStringResource.Objective));

            if (relatedSubProcess.Count > 0)
            {
                foreach (EntityDTO related in relatedSubProcess)
                {
                    related.ExtractProperties();
                    //t.AddCell(CreatePlainContentCell(
                    //    related.RenderHTML(GlobalStringResource.SubProcessNumber, 
                    //    RenderOption.None)));

                    string eventType = related.RenderHTML(GlobalStringResource.EventType,
                        RenderOption.None);
                    string description = string.Empty;
                    if (eventType == "Preceding")
                    {
                        description = related.RenderHTML(GlobalStringResource.FromSubProcess,
                            RenderOption.None);
                    }
                    else
                    {
                        description = related.RenderHTML(GlobalStringResource.ToSubProcess,
                            RenderOption.None);
                    }

                    t.AddCell(CreatePlainContentCell(
                        description
                        ));
                    t.AddCell(CreatePlainContentCell(
                        eventType));
                    t.AddCell(CreatePlainContentCell(
                        related.RenderHTML(GlobalStringResource.SubProcessObjective, 
                        RenderOption.None)));
                }

            }

            return new PdfContentParameter() { Table = t,
                Header = GlobalStringResource.Presenter_ReportSubProcessDependency, };
        }

    }
}
