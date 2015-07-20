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
    public class SubProcessContentStrategy2 : MainContentStrategyBase
    {

        EntityData entityData = null;

        public SubProcessContentStrategy2()
        {
            entityData = new EntityData();
        }

        public override object BuildContent(EntityDTO dto)
        {
            int id = dto.ID;
            SubProcessContentDTO sp = new SubProcessContentDTO();
            sp.Diagram = new DiagramContent() {
                DiagramPath = BuildDiagramContent(dto)
            };
            sp.ProcessDescription = ParentProcessDescription(id);
            sp.SubProcessDescription = SubProcessDescription(dto);
            sp.RolesAndResponsibilities = RolesAndResponsibilities(id);
            sp.Dependencies = SubProcessDependencies(id);
            sp.ActivityOverviews = ActivityOverview(id);
            sp.ModuleRelationships = ModuleRelationship(id);
            sp.BusinessRuleMappings = BusinessMapping(id);
            sp.ChangeHistories = ChangeHistory(id);
            sp.CurrentID = id;
            sp.ShowResize = ShowResize();
            return sp;
        }

        private List<ChangeHistoryItem> ChangeHistory(int id)
        {
            List<ChangeHistoryItem> items = new List<ChangeHistoryItem>();
            EntityData entityData = new EntityData();

            List<EntityDTO> changeHistory = entityData.GetChangeHistory(id);
            ChangeHistoryComparer comparer = new ChangeHistoryComparer();
            changeHistory.Sort(comparer);
            if (changeHistory.Count > 0)
            {
                foreach (EntityDTO related in changeHistory)
                {
                    ChangeHistoryItem ch = new ChangeHistoryItem();
                    related.ExtractProperties();
                    ch.Version = related.RenderHTML(GlobalStringResource.Version, RenderOption.Span);
                    ch.Date = related.RenderHTML(GlobalStringResource.Date, RenderOption.Span);
                    ch.Reason = related.RenderHTML(GlobalStringResource.ReasonforChange, RenderOption.Span);

                    List<EntityDTO> users = entityData.GetRelatedPersons(related.ID);

                    StringBuilder userLinks = new StringBuilder();
                    if (users.Count > 0)
                    {
                        foreach (EntityDTO user in users)
                        {
                            userLinks.Append(user.RenderAsPopupLink());
                            userLinks.Append(GlobalStringResource.BreakTag);
                        }
                    }
                    ch.Author = userLinks.ToString();
                    items.Add(ch);
                }
            }
            return items;
        }

        private List<RolesAndResponsibilityItem> RolesAndResponsibilities(int id)
        {
            List<RolesAndResponsibilityItem> rrs = new List<RolesAndResponsibilityItem>();
            EntityData entityData = new EntityData();
            List<EntityDTO> rolesAndResponsibilities = entityData.GetRolesAndResponsibilities(id);

            if (rolesAndResponsibilities.Count > 0)
            {
                foreach (EntityDTO dto in rolesAndResponsibilities)
                {
                    RolesAndResponsibilityItem rr = new RolesAndResponsibilityItem();
                    dto.ExtractProperties();
                    EntityDTO descriptionDto = entityData.GetRolesDescription(dto.ID);
                    string description = string.Empty;
                    if (descriptionDto != null)
                    {
                        descriptionDto.ExtractProperties();
                        description = descriptionDto.RenderHTML(GlobalStringResource.Description,
                            RenderOption.Break);
                    }
                    rr.Role = dto.RenderHTML(GlobalStringResource.Role, RenderOption.None);
                    rr.Responsibilities = description;
                    rrs.Add(rr);
                }
            }
            return rrs;
        }

        private ProcessDescription ParentProcessDescription(int id)
        {
            EntityDTO parentDto = entityData.GetParentDiagram(id);
            ProcessDescription pd = new ProcessDescription();
            if(parentDto != null)
            {
                parentDto.ExtractProperties();

                pd.ProcessName = parentDto.RenderAsLink();
                pd.Description = parentDto.RenderHTML(GlobalStringResource.ProcessDescription, RenderOption.Paragraph);
            }

            return pd;
        }

        private string BuildCommentsSection(EntityDTO dto)
        {
            return string.Format(GlobalStringResource.CommentFormHtml, dto.Name, dto.ID);
        }


        private SubProcessDescription SubProcessDescription(EntityDTO dto)
        {
            SubProcessDescription spd = new SubProcessDescription();
            dto.ExtractProperties();

            spd.Objective = dto.RenderHTML(GlobalStringResource.Description, RenderOption.Paragraph);
            spd.DocumentOwners = dto.RenderHTML(GlobalStringResource.DocumentOwners, RenderOption.Break);
            spd.FrameworkReference = dto.RenderHTML(GlobalStringResource.FrameworkReference, RenderOption.Break);
            spd.InternalReference = dto.RenderHTML(GlobalStringResource.InternalReference, RenderOption.Break);

            return spd;
        }


        private List<SubProcessDependency> SubProcessDependencies(int id)
        {
            List<SubProcessDependency> items = new List<SubProcessDependency>();
            string result = string.Empty;
            //TODO: Add sorting to the items
            List<EntityDTO> relatedSubProcess = entityData.GetSubProcessDependencies(id);

            if (relatedSubProcess.Count > 0)
            {

                foreach (EntityDTO related in relatedSubProcess)
                {
                    SubProcessDependency dp = new SubProcessDependency();
                    related.ExtractProperties();
                    string linkFrom = RenderLink(related, GlobalStringResource.FromSubProcess);
                    string linkTo = RenderLink(related, GlobalStringResource.ToSubProcess);

                    dp.Order = related.RenderHTML(GlobalStringResource.EventType, RenderOption.Break);
                    dp.From = linkFrom;
                    dp.To = linkTo;
                    dp.Objective = related.RenderHTML(GlobalStringResource.SubProcessObjective, RenderOption.Break);
                    items.Add(dp);
                }
            }
            return items;
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

        

        private List<ActivityOverviewItem> ActivityOverview(int id)
        {
            List<ActivityOverviewItem> items = new List<ActivityOverviewItem>();
            EntityDTO parent = entityData.GetOneEntity(id);
            List<EntityDTO> activities = entityData.GetActivityOverview(id);
            if (activities.Count > 0)
            {
                foreach (EntityDTO activity in activities)
                {
                    ActivityOverviewItem ao = new ActivityOverviewItem();
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

                    ao.Activity = activity.RenderAsPopupLink(isCtl);
                    ao.User = userLinks.ToString();
                    ao.Trigger = activity.RenderHTML(GlobalStringResource.TriggerInput, RenderOption.Break);
                    ao.Output = activity.RenderHTML(GlobalStringResource.Output, RenderOption.Break);
                    ao.keyDocs = activity.RenderHTML(GlobalStringResource.KeyDocuments, RenderOption.Break);
                    ao.SystemsInfo = activity.RenderHTML(GlobalStringResource.SystemsInformation, RenderOption.Break);
                    ao.Memo = activity.RenderHTML(GlobalStringResource.Memo, RenderOption.Break);
                    items.Add(ao);
                }
            }

            return items;
        }

        protected List<BusinessRuleMappingSPItem> BusinessMapping(int id)
        {
            List<BusinessRuleMappingSPItem> items = new List<BusinessRuleMappingSPItem>();
            List<EntityDTO> sections = entityData.GetSubProcessBusinessMapping(id);
            if (sections.Count > 0)
            {
                List<string> renderedTitles = new List<string>();
                foreach (EntityDTO section in sections)
                {
                    List<EntityDTO> paragraphs = entityData.GetSubProcessParagraphs(section.ID);
                    if (paragraphs.Count > 0)
                    {
                        BusinessRuleMappingSPItem brm = new BusinessRuleMappingSPItem();
                        section.ExtractProperties();
                        foreach (EntityDTO paragraph in paragraphs)
                        {
                            paragraph.ExtractProperties();
                            
                            if (paragraphs.Count == 1)
                            {
                                brm.Activity.Activity = section.RenderAsPopupLink();
                                brm.Activity.RowSpan = paragraphs.Count;
                            }
                            else
                            {
                                if (!renderedTitles.Contains(section.Name))
                                {
                                    brm.Activity.Activity = section.RenderAsPopupLink();
                                    brm.Activity.RowSpan = paragraphs.Count;
                                    renderedTitles.Add(section.Name);
                                }
                            }
                            brm.Activity.Paragraphs.Add(
                                new BusinessRuleMappingParagraph() {
                                    ParagraphName = MappingToolUrlHelper.GenerateValidParagraphLinkMarkup(paragraph.Name),
                                    ParagraphReference = paragraph.RenderHTMLAsAnchor(GlobalStringResource.ParagraphReference, RenderOption.Span, false)
                                });
                        }
                        items.Add(brm);
                    }
                }
            }

            return items;
        }


        //protected string BuildBusinessMapping(int id)
        //{
        //    int validCount = 0;
        //    StringBuilder html = new StringBuilder();
        //    List<EntityDTO> sections = entityData.GetSubProcessBusinessMapping(id);

        //    if (sections.Count > 0)
        //    {
        //        html.AppendFormat(Resources.TableStartTag, "grid");
        //        //html.AppendFormat(Resources.TableHeaderTag, 3, GlobalStringResource.BusinessProcessandBusinessRuleMapping);

        //        html.Append(Resources.TableRowStartTag);
        //        html.AppendFormat(Resources.TableHeaderTag, 1, GlobalStringResource.Activity);
        //        html.AppendFormat(Resources.TableHeaderTag, 1, GlobalStringResource.ParagraphName);
        //        html.AppendFormat(Resources.TableHeaderTag, 1, GlobalStringResource.ParagraphReference);
        //        html.Append(Resources.TableRowEndTag);

        //        List<string> renderedTitles = new List<string>();
        //        foreach (EntityDTO section in sections)
        //        {
        //            List<EntityDTO> paragraphs = entityData.GetSubProcessParagraphs(section.ID);
        //            if (paragraphs.Count > 0)
        //            {
        //                section.ExtractProperties();
        //                html.Append(Resources.TableRowStartTag);

        //                foreach (EntityDTO paragraph in paragraphs)
        //                {
        //                    paragraph.ExtractProperties();


        //                    if (paragraphs.Count == 1)
        //                    {
        //                        html.AppendFormat(Resources.TableCellWithRowSpan, string.Empty, paragraphs.Count, section.RenderAsPopupLink());
        //                    }
        //                    else
        //                    {
        //                        if (!renderedTitles.Contains(section.Name))
        //                        {
        //                            html.AppendFormat(Resources.TableCellWithRowSpan, string.Empty, paragraphs.Count, section.RenderAsPopupLink());
        //                            renderedTitles.Add(section.Name);
        //                        }
        //                        else
        //                        {
        //                            html.Append(Resources.TableRowStartTag);
        //                        }
        //                    }

        //                    //html.AppendFormat(Resources.TableCell, string.Empty, paragraph.RenderHTMLAsAnchor(paragraph.Name, Resources.ParagraphReference, RenderOption.Span, true));

        //                    html.AppendFormat(Resources.TableCell, string.Empty, MappingToolUrlHelper.GenerateValidParagraphLinkMarkup(paragraph.Name));
        //                    html.AppendFormat(Resources.TableCell, string.Empty, paragraph.RenderHTMLAsAnchor(GlobalStringResource.ParagraphReference, RenderOption.Span, false));
        //                    html.Append(Resources.TableRowEndTag);
        //                }
        //                validCount++;
        //            }
        //        }

        //        html.Append(Resources.TableEndTag);
        //    }

        //    return validCount > 0 ? html.ToString() : string.Empty;
        //}

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

        protected string BuildReviewersAndApprovers(int id)
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

        protected List<ModuleRelationshipItem> ModuleRelationship(int id)
        {
            List<ModuleRelationshipItem> items = new List<ModuleRelationshipItem>();
            List<EntityDTO> applications = entityData.GetModuleRelationship(id);
            if (applications.Count > 0)
            {
                foreach (EntityDTO related in applications)
                {
                    ModuleRelationshipItem mr = new ModuleRelationshipItem();
                    related.ExtractProperties();
                    mr.Module = related.Name;
                    mr.Description = related.RenderHTML("Description", RenderOption.Paragraph);
                    items.Add(mr);
                }
            }
            return items;
        }

        protected string BuildApplicationRelationship(int id)
        {
            throw new NotImplementedException();
        }

        protected string BuildFrameworkReference(int id)
        {
            throw new NotImplementedException();
        }

        protected string BuildInternalReference(int id)
        {
            throw new NotImplementedException();
        }
    }
}
