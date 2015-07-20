using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Presenter.Utils;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Utilities.HtmlHelper;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Entities.Utils;
using ADB.SA.Reports.Utilities.WMF;

namespace ADB.SA.Reports.Presenter.Content
{
    public class ProcessStrategy : ContentStrategyBase
    {
        private EntityData entityData;

        public ProcessStrategy()
        {
            entityData = new EntityData();
        }

        public override string BuildContent(EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();

            //html.Append(base.BuildTitle(dto.Name));
            //html.Append(Resources.Split);
            //html.Append(base.BuildRefresh(dto.ID));
            //html.Append(base.BuildDiagramImage(dto));

            List<KeyValuePair<string, string>> contents = new List<KeyValuePair<string, string>>();

            contents.Add(new KeyValuePair<string, string>("Diagram", BuildDiagramImage(dto)));
            contents.Add(new KeyValuePair<string, string>("Process Desc.", BuildDiagramDescription(dto)));
            contents.Add(new KeyValuePair<string, string>(GlobalStringResource.ProcessRelation, BuildProcessRelation(dto.ID)));
            contents.Add(new KeyValuePair<string, string>(GlobalStringResource.SubProcessRelation, BuildSubProcessRelation(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Business Rule Mapping", BuildBusinessMapping(dto.ID)));
            contents.Add(new KeyValuePair<string, string>(GlobalStringResource.Acronyms, BuildAcronyms(dto.ID)));
            contents.Add(new KeyValuePair<string, string>(GlobalStringResource.ReviewersAndApprovers, BuildReviewersAndApprovers(dto.ID)));
            contents.Add(new KeyValuePair<string, string>(GlobalStringResource.ChangeHistory, base.BuildChangeHistory(dto.ID)));
            contents.Add(new KeyValuePair<string, string>(GlobalStringResource.ApplicationRelationship, BuildApplicationRelationship(dto.ID)));
            contents.Add(new KeyValuePair<string, string>(GlobalStringResource.FrameworkRef, BuildFrameworkReference(dto.ID)));
            contents.Add(new KeyValuePair<string, string>(GlobalStringResource.InternalRef, BuildInternalReference(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Comments", BuildCommentsSection(dto)));

            html.Append("<div id=\"tabs\">");
            html.Append("<ul>");
            for (int i = 0; i < contents.Count; i++)
            {
                if (!string.IsNullOrEmpty(contents[i].Value))
                {
                    html.AppendFormat("<li><a href=\"#tabs-{0}\">{1}</a></li>", i + 1, contents[i].Key);
                }
            }
            html.Append("</ul>");

            for (int i = 0; i < contents.Count; i++)
            {
                if (!string.IsNullOrEmpty(contents[i].Value))
                {
                    html.AppendFormat("<div class=\"content-holder\" id=\"tabs-{0}\">{1}</div>", i + 1, contents[i].Value);
                }
            }

            html.Append("</div>");


            //html.Append(base.BuildRolesAndResponsibilities(dto.ID));

            html.Append(base.BuildReportHiddenField(dto.ID));

            html.Append("<div id=\"dialog-modal\"></div>");
            
            return html.ToString();
        }

        private string BuildCommentsSection(EntityDTO dto)
        {
            return string.Format(GlobalStringResource.CommentFormHtml, dto.Name, dto.ID);
        }

        private string BuildQuickLinks(EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();
            QuickLinksData quicklinksData = new QuickLinksData();
            List<EntityDTO> relatedProcess = quicklinksData.GetAllRelatedProcess(dto.ID);
            List<EntityDTO> relatedSubProcess = quicklinksData.GetAllRelatedSubProcess(dto.ID);

            relatedSubProcess.Sort((entity1, entity2) => string.Compare(entity1.Name, entity2.Name, true));
            relatedProcess.Sort((entity1, entity2) => string.Compare(entity1.Name, entity2.Name, true));

            Dictionary<string, List<EntityDTO>> mainList = new Dictionary<string, List<EntityDTO>>();
            mainList.Add(GlobalStringResource.Process, relatedProcess);
            mainList.Add(GlobalStringResource.SubProcess, relatedSubProcess);
            

            html.Append("<div id=\"diagram-line-right\">");
            html.Append("<table border=\"0\" cellspacing=\"0\"><tr>");
            int ctr = 0;
            foreach (var pair in mainList)
            {
                html.Append("<td>");
                ctr++;
                html.Append(string.Format("<ul id=\"menu{0}\">", ctr));

                html.AppendFormat("<li><a>{0}</a>", pair.Key);
                html.Append("<ul>");
                foreach (EntityDTO innerList in pair.Value)
                {
                    html.AppendFormat("<li><a href=\"Default.aspx?id={0}\">{1}</a></li>", innerList.ID, innerList.Name);
                }
                html.Append("</ul>");
                html.Append("</li>");

                html.Append("</ul>");
                html.Append("</td>");
            }

            html.Append("</tr></table>");
            html.Append("</div>");

            return html.ToString(); 
        }

        protected override string BuildDiagramDescription(EntityDTO dto)
        {
            HtmlTable t = new HtmlTable(2, 0, "grid", new int[] { 15, 85 });

            t.AddHeader("&emsp;", 2);

            t.AddCell(GlobalStringResource.ProcessName);
            t.AddCell(dto.Name);

            t.AddCell(GlobalStringResource.Description);
            t.AddCell(dto.RenderHTML(GlobalStringResource.ProcessDescription, RenderOption.Paragraph));

            t.AddCell(GlobalStringResource.Purpose);
            t.AddCell(dto.RenderHTML(GlobalStringResource.Description, RenderOption.Paragraph));

            t.AddCell(GlobalStringResource.Objective);
            t.AddCell(dto.RenderHTML(GlobalStringResource.ProcessObjective, RenderOption.Paragraph));

            t.AddCell(GlobalStringResource.Strategy);
            t.AddCell(dto.RenderHTML(GlobalStringResource.Strategy, RenderOption.Paragraph));

            t.AddCell(GlobalStringResource.DocumentOwners);
            t.AddCell(dto.RenderHTML(GlobalStringResource.DocumentOwners, RenderOption.Break));

            return t.EndHtmlTable();
        }

        protected override string BuildDiagramImage(EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();
            EntityData data = new EntityData();
            FileData files = new FileData();


            FileDTO file = files.GetFile(dto.DGXFileName);
            byte[] imageBytes = file.Data;

            string path = string.Format("{0}_{1}", file.Date.ToFileTime().ToString(), dto.DGXFileName);

            int poolCount = data.GetPoolCount(dto.ID);
            WmfImageManager imageManager = new WmfImageManager(dto, imageBytes,
                path, dto.Type, poolCount, false);
            path = imageManager.ProcessImage();

            html.Append("<div id=\"diagram-line\" class=\"clearfix\">");
            html.Append("<div id=\"diagram-line-left\" class=\"infoBox\">To navigate to the related process and sub-process of this diagram, use the menu on the right.<br/>To navigate related informations, select the tabs above.</div>");
            if (dto.Type == 111)
            {
                html.Append(BuildQuickLinks(dto));
            }
            html.Append("<div style=\"clear: both;\"></div>");
            html.Append("</div>");
            html.AppendFormat(GlobalStringResource.Presenter_BuildDiagramImage_Tag, path.Replace(@"\", @"/"));
            html.Append(GlobalStringResource.BreakTag);
            return html.ToString();
        }

        protected override string BuildProcessRelation(int id)
        {
            string result = string.Empty;
            List<EntityDTO> relatedProcess = entityData.GetRelatedProcess(id);
            if (relatedProcess.Count > 0)
            {
                HtmlTable t = new HtmlTable(3, 0, "grid", new int[] { 10, 45, 45 });
                //t.AddHeader(GlobalStringResource.ProcessRelation, 3);
                t.AddHeader(GlobalStringResource.ReferenceNumber);
                t.AddHeader(GlobalStringResource.Name);
                t.AddHeader(GlobalStringResource.Relationship);
                foreach (EntityDTO related in relatedProcess)
                {
                    related.ExtractProperties();

                    EntityDTO actual = entityData.GetActualRelatedProcess(related.ID);
                    if (actual != null)
                    {
                        actual.ExtractProperties();

                        /*
                         * gets the actual diagram
                         * displays plain text if the diagram does not exist
                         */
                        EntityDTO diag = entityData.GetOneEntityByReferenceNumberAndClass(related.RenderHTML(
                            GlobalStringResource.ReferenceNumber, RenderOption.None), 1);
                        if (diag != null)
                        {
                            t.AddCell(diag.RenderAsLink(related.RenderHTML(GlobalStringResource.ReferenceNumber, RenderOption.None), diag.ID, RenderOption.None));
                        }
                        else
                        {
                            t.AddCell(related.RenderHTML(GlobalStringResource.ReferenceNumber, RenderOption.None));
                        }
                    }
                    else
                    {
                        //Display plain text
                        t.AddCell(related.RenderHTML(GlobalStringResource.ReferenceNumber, RenderOption.None));
                    }
                    t.AddCell(related.RenderHTML(GlobalStringResource.Process, RenderOption.None));
                    t.AddCell(related.RenderHTML(GlobalStringResource.Relationship, RenderOption.None));
                }
                result = t.EndHtmlTable();
            }
            return result;
        }

        protected override string BuildSubProcessRelation(int id)
        {
            string result = string.Empty;
            List<EntityDTO> relatedSubProcess = entityData.GetRelatedSubProcess(id);
            if (relatedSubProcess.Count > 0)
            {
                HtmlTable t = new HtmlTable(4, 0, "grid", new int[] { 20,55,15,10 } );
                //t.AddHeader(GlobalStringResource.SubProcessRelation, 4);
                t.AddHeader(GlobalStringResource.SubProcessDiagram);
                t.AddHeader(GlobalStringResource.SubProcessOverview);
                t.AddHeader(GlobalStringResource.SubProcessOwner);
                t.AddHeader(GlobalStringResource.Authors);

                foreach (EntityDTO related in relatedSubProcess)
                {
                    related.ExtractProperties();
                    t.AddCell(related.RenderAsLink());
                    t.AddCell(related.RenderHTML(GlobalStringResource.Description, RenderOption.Paragraph));
                    t.AddCell(related.RenderHTML(GlobalStringResource.DocumentOwners, RenderOption.Break));
                    t.AddCell(related.RenderHTML(GlobalStringResource.Authors,
                        RenderOption.Break));
                }
                result = t.EndHtmlTable();
            }
            return result;
        }

        protected override string BuildBusinessMapping(int id)
        {
            string result = string.Empty;
            List<EntityDTO> sections = entityData.GetSections(id);
            if (sections.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 0, "grid", new int[] { 10, 90 } );
                //t.AddHeader(GlobalStringResource.BusinessProcessandBusinessRuleMapping, 2);

                t.AddHeader(GlobalStringResource.SectionName);
                t.AddHeader(GlobalStringResource.SectionDescription);

                foreach (EntityDTO related in sections)
                {
                    related.ExtractProperties();

                    t.AddCell(MappingToolUrlHelper.GenerateValidSectionLinkMarkup(related.Name));
                    t.AddCell(related.RenderHTML(GlobalStringResource.Description, RenderOption.None).ToUpper());
                }
                result = t.EndHtmlTable();
            }
            return result;
        }

        protected override string BuildAcronyms(int id)
        {
            string result = string.Empty;
            List<EntityDTO> acronyms = entityData.GetAcronyms(id);
            if (acronyms.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 0, "grid", new int[] { 5, 95 } );
                //t.AddHeader(GlobalStringResource.Acronyms, 2);

                t.AddHeader(GlobalStringResource.Acronyms);
                t.AddHeader(GlobalStringResource.Description);

                foreach (EntityDTO related in acronyms)
                {
                    related.ExtractProperties();

                    t.AddCell(related.RenderAsPopupLink());
                    t.AddCell(
                        related.RenderHTML(
                        GlobalStringResource.AbbreviationDescription,
                        RenderOption.Break));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        protected override string BuildReviewersAndApprovers(int id)
        {
            return string.Empty;
        }

        protected override string BuildApplicationRelationship(int id)
        {
            string result = string.Empty;
            List<EntityDTO> applications = entityData.GetApplicationRelationship(id);
            if (applications.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 0, "grid");
                //t.AddHeader(GlobalStringResource.ApplicationRelationship);

                t.AddHeader(GlobalStringResource.Application);
                t.AddHeader(GlobalStringResource.Description);

                foreach (EntityDTO related in applications)
                {
                    related.ExtractProperties();
                    t.AddCell(related.Name);
                    t.AddCell(related.RenderHTML
                        (GlobalStringResource.Description, RenderOption.Paragraph));
                }
                result = t.EndHtmlTable();
            }
            return result;
        }

        protected override string BuildFrameworkReference(int id)
        {
            string result = string.Empty;
            List<EntityDTO> frameworks = entityData.GetFrameworkReference(id);
            if (frameworks.Count > 0)
            {
                HtmlTable t = new HtmlTable(3, 0, "grid");
                //t.AddHeader(GlobalStringResource.FrameworkReference, 3);

                t.AddHeader(GlobalStringResource.Framework);
                t.AddHeader(GlobalStringResource.FrameworkIndexID);
                t.AddHeader(GlobalStringResource.Description);

                foreach (EntityDTO related in frameworks)
                {
                    related.ExtractProperties();

                    t.AddCell(related.Name);
                    t.AddCell(related.RenderHTML(GlobalStringResource.FrameworkIndexID, RenderOption.Paragraph));
                    t.AddCell(related.RenderHTML(GlobalStringResource.FrameworkDescription, RenderOption.Paragraph));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        protected override string BuildInternalReference(int id)
        {
            string result = string.Empty;

            List<EntityDTO> references = entityData.GetInternalReference(id);

            if (references.Count > 0)
            {
                HtmlTable t = new HtmlTable(3, 0, "grid", new int[] { 20,40,40 });
                
                //t.AddHeader(GlobalStringResource.InternalReference, 3);

                t.AddHeader(GlobalStringResource.DocumentType);
                t.AddHeader(GlobalStringResource.Title);
                t.AddHeader(GlobalStringResource.DocumentReferenceNumber);

                foreach (EntityDTO related in references)
                {
                    related.ExtractProperties();

                    t.AddCell(related.RenderHTML(GlobalStringResource.ReferenceType, RenderOption.Paragraph));
                    t.AddCell(related.RenderHTML(GlobalStringResource.Description, RenderOption.Paragraph));
                    t.AddCell(related.Name);
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        protected override string BuildModuleRelationship(int id)
        {
            return string.Empty;
        }
    }
}
