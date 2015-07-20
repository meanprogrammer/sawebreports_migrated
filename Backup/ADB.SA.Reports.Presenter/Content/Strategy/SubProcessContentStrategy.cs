using System;
using System.Collections.Generic;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Presenter.Utils;
using ADB.SA.Reports.Utilities.HtmlHelper;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;
using System.Linq;
using ADB.SA.Reports.Entities.Utils;

namespace ADB.SA.Reports.Presenter.Content
{
    public class SubProcessContentStrategy : ContentStrategyBase
    {

        EntityData entityData = null;

        public SubProcessContentStrategy()
        {
            entityData = new EntityData();
        }

        public override string BuildContent(EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();

            //html.Append(Resources.BreakTag);
            //html.Append(BuildTitle(dto.Name));
            //html.Append(Resources.Split);
            //html.Append(base.BuildRefresh(dto.ID));
            //html.Append(base.BuildDiagramImage(dto));

            //

            List<KeyValuePair<string, string>> contents = new List<KeyValuePair<string, string>>();

            contents.Add(new KeyValuePair<string, string>("Diagram", BuildDiagramImage(dto)));
            contents.Add(new KeyValuePair<string, string>("Process", BuildParentProcessDescription(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Sub-Process Desc.", BuildDiagramDescription(dto)));
            contents.Add(new KeyValuePair<string, string>("Roles and Resp.", base.BuildRolesAndResponsibilities(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Sub-Process Dependencies", BuildSubProcessRelation(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Activity Overview", BuildActivityOverview(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Module Relationship", BuildModuleRelationship(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Business Rule Mapping", BuildBusinessMapping(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Reviewers and Approvers", BuildReviewersAndApprovers(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Change History", base.BuildChangeHistory(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Comments", BuildCommentsSection(dto)));

            html.Append("<div id=\"tabs\">");
            html.Append("<ul>");
            for (int i = 0; i < contents.Count; i++)
            {
                if (!string.IsNullOrEmpty(contents[i].Value))
                {
                    string linkId = contents[i].Key.ToLower().Replace(" ", string.Empty).Replace(".","").Replace("/","");
                    html.AppendFormat("<li><a href=\"#tabs-{0}\" id=\"{1}\">{2}</a></li>", i + 1, linkId,contents[i].Key);
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

            //
        }

        private string BuildParentProcessDescription(int id)
        {
            HtmlTable t = new HtmlTable(2, 0, "grid");
            EntityDTO parentDto = entityData.GetParentDiagram(id);

            if(parentDto != null)
            {
                parentDto.ExtractProperties();

                //t.AddHeader(GlobalStringResource.Process, 2);

                t.AddHeader(GlobalStringResource.ProcessName);
                t.AddHeader(GlobalStringResource.Description);

                t.AddCell(parentDto.RenderAsLink());
                t.AddCell(parentDto.RenderHTML(GlobalStringResource.ProcessDescription, RenderOption.Paragraph));
            }

            return t.EndHtmlTable();
        }

        private string BuildCommentsSection(EntityDTO dto)
        {
            return string.Format(GlobalStringResource.CommentFormHtml, dto.Name, dto.ID);
        }


        protected override string BuildDiagramDescription(EntityDTO dto)
        {
            HtmlTable t = new HtmlTable(2, 0, "grid", new int[]{ 15,85 });
            dto.ExtractProperties();

            //t.AddHeader(GlobalStringResource.SubProcessDescription, 2);

            t.AddHeader("&emsp;", 2);

            t.AddCell(GlobalStringResource.Objective);
            t.AddCell(dto.RenderHTML(GlobalStringResource.Description, RenderOption.Paragraph));

            t.AddCell(GlobalStringResource.DocumentOwners);
            t.AddCell(dto.RenderHTML(GlobalStringResource.DocumentOwners, RenderOption.Break));

            t.AddCell(GlobalStringResource.FrameworkReference);
            t.AddCell(dto.RenderHTML(GlobalStringResource.FrameworkReference, RenderOption.Break));

            t.AddCell(GlobalStringResource.InternalReference);
            t.AddCell(dto.RenderHTML(GlobalStringResource.InternalReference, RenderOption.Break));

            return t.EndHtmlTable();
        }

        protected override string BuildProcessRelation(int id)
        {
            throw new NotImplementedException();
        }

        protected override string BuildSubProcessRelation(int id)
        {
            string result = string.Empty;
            //TODO: Add sorting to the items
            List<EntityDTO> relatedSubProcess = entityData.GetSubProcessDependencies(id);

            if (relatedSubProcess.Count > 0)
            {
                HtmlTable t = new HtmlTable(4, 0, "grid");
                //t.AddHeader(GlobalStringResource.SubProcessDependencies, 4);

                t.AddHeader(GlobalStringResource.PrecedingSucceeding);
                t.AddHeader(GlobalStringResource.FromSubProcess);
                t.AddHeader(GlobalStringResource.ToSubProcess);
                t.AddHeader(GlobalStringResource.IntegrationObjectives);


                foreach (EntityDTO related in relatedSubProcess)
                {
                    related.ExtractProperties();
                    string linkFrom = RenderLink(related, GlobalStringResource.FromSubProcess);
                    string linkTo = RenderLink(related, GlobalStringResource.ToSubProcess);

                    t.AddCell(related.RenderHTML(GlobalStringResource.EventType, RenderOption.Break));
                    t.AddCell(linkFrom);
                    t.AddCell(linkTo);
                    t.AddCell(related.RenderHTML(GlobalStringResource.SubProcessObjective, RenderOption.Break));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        private string RenderLink(EntityDTO related, string key)
        {
            string link = string.Empty;
            EntityDTO dto = entityData.GetOneEntityByNameAndClass(related.RenderHTML(key, RenderOption.None), 1);
            if (dto != null)
            {
                dto.ExtractProperties();
                link = related.RenderAsLink(dto.ID, key, RenderOption.None);
            }
            else
            {
                link = related.RenderHTML(key, RenderOption.None);
            }
            return link;
        }

        

        private string BuildActivityOverview(int id)
        {
            EntityDTO parent = entityData.GetOneEntity(id);


            List<EntityDTO> activities = entityData.GetActivityOverview(id);
            string result = string.Empty;
            if (activities.Count > 0)
            {
                int[] widths = new int[] { 20,10,30,10,10,10,10 };
                HtmlTable t = new HtmlTable(7, 0, "grid", widths);
                //t.AddHeader(GlobalStringResource.ActivityOverview, 7);

                t.AddHeader(GlobalStringResource.Activity);
                t.AddHeader(GlobalStringResource.User);
                t.AddHeader(GlobalStringResource.TriggerInput);
                t.AddHeader(GlobalStringResource.Output);
                t.AddHeader(GlobalStringResource.KeyDocuments);
                t.AddHeader(GlobalStringResource.SystemsInformation);
                t.AddHeader(GlobalStringResource.Memo);

                foreach (EntityDTO activity in activities)
                {
                    activity.ExtractProperties();

                    List<EntityDTO> users = entityData.GetRelatedUsers(activity.ID);
                    StringBuilder userLinks = new StringBuilder();
                    if (users.Count > 0)
                    {
                        foreach (EntityDTO user in users)
                        {
                            userLinks.Append(user.RenderAsPopupLink());
                            userLinks.Append(GlobalStringResource.BreakTag);
                        }
                    }

                    bool isCtl = false;

                    if (parent != null)
                    {
                        isCtl = parent.IsCTL;
                    }

                    t.AddCell(activity.RenderAsPopupLink(isCtl));
                    t.AddCell(userLinks.ToString());
                    t.AddCell(activity.RenderHTML(GlobalStringResource.TriggerInput, RenderOption.Break));
                    t.AddCell(activity.RenderHTML(GlobalStringResource.Output, RenderOption.Break));
                    t.AddCell(activity.RenderHTML(GlobalStringResource.KeyDocuments, RenderOption.Break));
                    t.AddCell(activity.RenderHTML(GlobalStringResource.SystemsInformation, RenderOption.Break));
                    t.AddCell(activity.RenderHTML(GlobalStringResource.Memo, RenderOption.Break));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        protected override string BuildBusinessMapping(int id)
        {
            int validCount = 0;
            StringBuilder html = new StringBuilder();
            List<EntityDTO> sections = entityData.GetSubProcessBusinessMapping(id);

            if (sections.Count > 0)
            {
                html.AppendFormat(Resources.TableStartTag, "grid");
                //html.AppendFormat(Resources.TableHeaderTag, 3, GlobalStringResource.BusinessProcessandBusinessRuleMapping);

                html.Append(Resources.TableRowStartTag);
                html.AppendFormat(Resources.TableHeaderTag, 1, GlobalStringResource.Activity);
                html.AppendFormat(Resources.TableHeaderTag, 1, GlobalStringResource.ParagraphName);
                html.AppendFormat(Resources.TableHeaderTag, 1, GlobalStringResource.ParagraphReference);
                html.Append(Resources.TableRowEndTag);

                List<string> renderedTitles = new List<string>();
                foreach (EntityDTO section in sections)
                {
                    List<EntityDTO> paragraphs = entityData.GetSubProcessParagraphs(section.ID);
                    if (paragraphs.Count > 0)
                    {
                        section.ExtractProperties();
                        html.Append(Resources.TableRowStartTag);

                        foreach (EntityDTO paragraph in paragraphs)
                        {
                            paragraph.ExtractProperties();


                            if (paragraphs.Count == 1)
                            {
                                html.AppendFormat(Resources.TableCellWithRowSpan, string.Empty, paragraphs.Count, section.RenderAsPopupLink());
                            }
                            else
                            {
                                if (!renderedTitles.Contains(section.Name))
                                {
                                    html.AppendFormat(Resources.TableCellWithRowSpan, string.Empty, paragraphs.Count, section.RenderAsPopupLink());
                                    renderedTitles.Add(section.Name);
                                }
                                else
                                {
                                    html.Append(Resources.TableRowStartTag);
                                }
                            }

                            //html.AppendFormat(Resources.TableCell, string.Empty, paragraph.RenderHTMLAsAnchor(paragraph.Name, Resources.ParagraphReference, RenderOption.Span, true));

                            html.AppendFormat(Resources.TableCell, string.Empty, MappingToolUrlHelper.GenerateValidParagraphLinkMarkup(paragraph.Name));
                            html.AppendFormat(Resources.TableCell, string.Empty, paragraph.RenderHTMLAsAnchor(GlobalStringResource.ParagraphReference, RenderOption.Span, false));
                            html.Append(Resources.TableRowEndTag);
                        }
                        validCount++;
                    }
                }

                html.Append(Resources.TableEndTag);
            }

            return validCount > 0 ? html.ToString() : string.Empty;
        }




        protected override string BuildAcronyms(int id)
        {
            throw new NotImplementedException();
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

                        if (result.EndsWith("."))
                        {
                            result = result.TrimEnd(new char[] { '.' });
                        }
                        newList.Add(result);
                    }
                }
            }
            return newList;
        }

        protected override string BuildReviewersAndApprovers(int id)
        {
            StringBuilder result = new StringBuilder();
            
            EntityDTO dto = entityData.GetOneEntity(id);
            dto.ExtractProperties();

            List<string> reviewersBasis = RemoveBelongsToOrg(dto.GetPropertyList("Reviewers"));
            List<string> approversBasis = RemoveBelongsToOrg(dto.GetPropertyList("Approvers"));
            
            List<EntityDTO> reviewers = entityData.GetReviewersAndApprovers(id);

            if (reviewersBasis.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 0, "grid", new int[] { 30, 70 });
                t.AddHeader(GlobalStringResource.Reviewers, 2);

                t.AddHeader(GlobalStringResource.Name);
                t.AddHeader(GlobalStringResource.Position);
                foreach (string item in reviewersBasis)
                {
                    EntityDTO rev = reviewers.Find(x => x.Name == item.Trim());
                    CreateMainTable(t, rev);
                }
                result.Append(t.EndHtmlTable());
            }


            if (approversBasis.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 0, "grid", new int[] { 30, 70 });
                t.AddHeader(GlobalStringResource.Approvers, 2);

                t.AddHeader(GlobalStringResource.Name);
                t.AddHeader(GlobalStringResource.Position);
                foreach (string item in approversBasis)
                {
                    EntityDTO appr = reviewers.Find(x => x.Name == item.Trim());
                    CreateMainTable(t, appr);
                }
                result.Append(t.EndHtmlTable());
            }

            return result.ToString();
        }

        private void CreateMainTable(HtmlTable t, EntityDTO dto)
        {
            if (dto != null)
            {
                dto.ExtractProperties();

                string personName = dto.RenderHTML(GlobalStringResource.Assignedto, RenderOption.None);
                if (!string.IsNullOrEmpty(personName))
                {
                    EntityDTO relatedPerson = entityData.GetOneEntityByNameAndType(personName, 663);
                    if (relatedPerson != null)
                    {
                        relatedPerson.ExtractProperties();
                        personName = relatedPerson.RenderAsPopupLink();
                    }
                }
                t.AddCell(personName);
                t.AddCell(dto.RenderAsPopupLink());
            }
        }

        protected override string BuildModuleRelationship(int id)
        {
            string result = string.Empty;
            List<EntityDTO> applications = entityData.GetModuleRelationship(id);
            if (applications.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 0, "grid");
                //t.AddHeader(GlobalStringResource.ApplicationRelationship);

                t.AddHeader("Module");
                t.AddHeader("Description");

                foreach (EntityDTO related in applications)
                {
                    related.ExtractProperties();
                    t.AddCell(related.Name);
                    t.AddCell(related.RenderHTML("Description", RenderOption.Paragraph));
                }
                result = t.EndHtmlTable();
            }
            return result;
        }

        protected override string BuildApplicationRelationship(int id)
        {
            throw new NotImplementedException();
        }

        protected override string BuildFrameworkReference(int id)
        {
            throw new NotImplementedException();
        }

        protected override string BuildInternalReference(int id)
        {
            throw new NotImplementedException();
        }
    }
}
