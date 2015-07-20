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
    public class ProcessStrategy2 : MainContentStrategyBase
    {
        private EntityData entityData;

        public ProcessStrategy2()
        {
            entityData = new EntityData();
        }

        ////public override object BuildContent(EntityDTO entity) 
        ////{
        //    int id = entity.ID;
        //    ProcessContentDTO process = new ProcessContentDTO();
        //    process.Diagram = new DiagramContent() { 
        //        DiagramPath = BuildDiagramContent(entity),
        //        RelatedProcess = GetRelatedProcess(entity),
        //        RelatedSubProcess = GetRelatedSubProcess(entity)
        //    };
        //    process.Description = ProcessDescription(entity);
        //    process.ProcessRelations = ProcessRelation(id);
        //    process.SubProcessRelations = SubProcessRelation(id);
        //    process.BusinessRuleMappings = BusinessMapping(id);
        //    process.Acronyms = Acronyms(id);
        //    process.Applications = ApplicationRelationship(id);
        //    process.Frameworks = FrameworkReference(id);
        //    process.InternalReferences = InternalReference(id);
        //    return process;
        ////}

        public override object BuildContent(EntityDTO entity)
        {
            int id = entity.ID;
            ProcessContentDTO process = new ProcessContentDTO();
            process.Diagram = new DiagramContent() { 
                DiagramPath = BuildDiagramContent(entity),
                RelatedProcess = GetRelatedProcess(entity),
                RelatedSubProcess = GetRelatedSubProcess(entity)
            };
            process.Description = ProcessDescription(entity);
            process.ProcessRelations = ProcessRelation(id);
            process.SubProcessRelations = SubProcessRelation(id);
            process.BusinessRuleMappings = BusinessMapping(id);
            process.Acronyms = Acronyms(id);
            process.Applications = ApplicationRelationship(id);
            process.Frameworks = FrameworkReference(id);
            process.InternalReferences = InternalReference(id);
            process.CurrentID = id;
            process.ShowResize = ShowResize();
            return process;
        }

        private string BuildCommentsSection(EntityDTO dto)
        {
            return string.Format(GlobalStringResource.CommentFormHtml, dto.Name, dto.ID);
        }

        private List<EntityDTO> GetRelatedProcess(EntityDTO dto)
        {
            QuickLinksData quicklinksData = new QuickLinksData();
            List<EntityDTO> relatedProcess = quicklinksData.GetAllRelatedProcess(dto.ID);
            List<EntityDTO> relatedSubProcess = quicklinksData.GetAllRelatedSubProcess(dto.ID);

            relatedSubProcess.Sort((entity1, entity2) => string.Compare(entity1.Name, entity2.Name, true));
            relatedProcess.Sort((entity1, entity2) => string.Compare(entity1.Name, entity2.Name, true));

            return relatedProcess;
        }

        private List<EntityDTO> GetRelatedSubProcess(EntityDTO dto)
        {
            QuickLinksData quicklinksData = new QuickLinksData();
            List<EntityDTO> relatedSubProcess = quicklinksData.GetAllRelatedSubProcess(dto.ID);

            relatedSubProcess.Sort((entity1, entity2) => string.Compare(entity1.Name, entity2.Name, true));

            return relatedSubProcess;
        }

        private ProcessDescription ProcessDescription(EntityDTO dto)
        {
            ProcessDescription pd = new ProcessDescription() 
            {
                ProcessName = dto.Name,
                Description = dto.RenderHTML(GlobalStringResource.ProcessDescription, RenderOption.Paragraph),
                Purpose = dto.RenderHTML(GlobalStringResource.Description, RenderOption.Paragraph),
                Objective = dto.RenderHTML(GlobalStringResource.ProcessObjective, RenderOption.Paragraph),
                Strategy = dto.RenderHTML(GlobalStringResource.Strategy, RenderOption.Paragraph),
                DocumentOwners = dto.RenderHTML(GlobalStringResource.DocumentOwners, RenderOption.Break)
            };
            return pd;
        }

        private List<ProcessRelationItem> ProcessRelation(int id)
        {
            List<ProcessRelationItem> items = new List<ProcessRelationItem>();
            List<EntityDTO> relatedProcess = entityData.GetRelatedProcess(id);
            if (relatedProcess.Count > 0)
            {
                foreach (EntityDTO related in relatedProcess)
                {
                    ProcessRelationItem p = new ProcessRelationItem();
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
                            p.ReferenceNumber = diag.RenderAsLink(related.RenderHTML(GlobalStringResource.ReferenceNumber, RenderOption.None), diag.ID, RenderOption.None);
                        }
                        else
                        {
                            p.ReferenceNumber = related.RenderHTML(GlobalStringResource.ReferenceNumber, RenderOption.None);
                        }
                    }
                    else
                    {
                        //Display plain text
                        p.ReferenceNumber = related.RenderHTML(GlobalStringResource.ReferenceNumber, RenderOption.None);
                    }
                    p.Name = related.RenderHTML(GlobalStringResource.Process, RenderOption.None);
                    p.Relationship = related.RenderHTML(GlobalStringResource.Relationship, RenderOption.None);
                    items.Add(p);
                }
            }
            return items;
        }

        private List<SubProcessRelationItem> SubProcessRelation(int id)
        {
            List<SubProcessRelationItem> items = new List<SubProcessRelationItem>();
            List<EntityDTO> relatedSubProcess = entityData.GetRelatedSubProcess(id);
            if (relatedSubProcess.Count > 0)
            {
                foreach (EntityDTO related in relatedSubProcess)
                {
                    SubProcessRelationItem spr = new SubProcessRelationItem();
                    related.ExtractProperties();
                    spr.SubProcessDiagram = related.RenderAsLink();
                    spr.SubProcessOverview = related.RenderHTML(GlobalStringResource.Description, RenderOption.Paragraph);
                    spr.SubProcessOwner = related.RenderHTML(GlobalStringResource.DocumentOwners, RenderOption.Break);
                    spr.Authors = related.RenderHTML(GlobalStringResource.Authors, RenderOption.Break);

                    items.Add(spr);
                }
            }
            return items;
        }

        private List<BusinessRuleMappingItem> BusinessMapping(int id)
        {
            List<BusinessRuleMappingItem> items = new List<BusinessRuleMappingItem>();
            List<EntityDTO> sections = entityData.GetSections(id);
            if (sections.Count > 0)
            {

                foreach (EntityDTO related in sections)
                {
                    BusinessRuleMappingItem br = new BusinessRuleMappingItem();
                    related.ExtractProperties();

                    br.SectionName = MappingToolUrlHelper.GenerateValidSectionLinkMarkup(related.Name);
                    br.SectionDescription = related.RenderHTML(GlobalStringResource.Description, RenderOption.None).ToUpper();
                    items.Add(br);
                }
            }
            return items;
        }

        private List<AcronymItem> Acronyms(int id)
        {
            List<AcronymItem> items = new List<AcronymItem>();
            List<EntityDTO> acronyms = entityData.GetAcronyms(id);
            if (acronyms.Count > 0)
            {
                foreach (EntityDTO related in acronyms)
                {
                    AcronymItem ac = new AcronymItem();
                    related.ExtractProperties();

                    ac.Acronym = related.RenderAsPopupLink();
                    ac.Description =
                        related.RenderHTML(
                        GlobalStringResource.AbbreviationDescription,
                        RenderOption.Break);
                    items.Add(ac);
                }
            }
            return items;
        }

        protected List<ApplicationRelationshipItem> ApplicationRelationship(int id)
        {
            List<ApplicationRelationshipItem> items = new List<ApplicationRelationshipItem>();
            List<EntityDTO> applications = entityData.GetApplicationRelationship(id);
            if (applications.Count > 0)
            {
                foreach (EntityDTO related in applications)
                {
                    ApplicationRelationshipItem app = new ApplicationRelationshipItem();
                    related.ExtractProperties();
                    app.Application = related.Name;
                    app.Description = related.RenderHTML
                        (GlobalStringResource.Description, RenderOption.Paragraph);
                    items.Add(app);
                }
            }
            return items;
        }

        private List<FrameworkReferenceItem> FrameworkReference(int id)
        {
            List<FrameworkReferenceItem> items = new List<FrameworkReferenceItem>();
            List<EntityDTO> frameworks = entityData.GetFrameworkReference(id);
            if (frameworks.Count > 0)
            {
                foreach (EntityDTO related in frameworks)
                {
                    FrameworkReferenceItem f = new FrameworkReferenceItem();
                    related.ExtractProperties();

                    f.Framework = related.Name;
                    f.FrameworkID = related.RenderHTML(GlobalStringResource.FrameworkIndexID, RenderOption.Paragraph);
                    f.Description = related.RenderHTML(GlobalStringResource.FrameworkDescription, RenderOption.Paragraph);
                    items.Add(f);
                }
            }
            return items;
        }

        private List<InternalReferenceItem> InternalReference(int id)
        {
            List<InternalReferenceItem> items = new List<InternalReferenceItem>();
            List<EntityDTO> references = entityData.GetInternalReference(id);
            if (references.Count > 0)
            {
                foreach (EntityDTO related in references)
                {
                    InternalReferenceItem ii = new InternalReferenceItem();
                    related.ExtractProperties();

                    ii.DocumentType = related.RenderHTML(GlobalStringResource.ReferenceType, RenderOption.Paragraph);
                    ii.Title =  related.RenderHTML(GlobalStringResource.Description, RenderOption.Paragraph);
                    ii.DocumentReferenceNumber = related.Name;
                    items.Add(ii);
                }
            }
            return items;
        }
    }
}
