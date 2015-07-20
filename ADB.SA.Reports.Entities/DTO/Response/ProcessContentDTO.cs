using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class ProcessContentDTO : IResizeDiagram
    {
        public DiagramContent Diagram { get; set; }
        public ProcessDescription Description { get; set; }
        public List<ProcessRelationItem> ProcessRelations { get; set; }
        public List<SubProcessRelationItem> SubProcessRelations { get; set; }
        public List<BusinessRuleMappingItem> BusinessRuleMappings { get; set; }
        public List<AcronymItem> Acronyms { get; set; }
        public List<ApplicationRelationshipItem> Applications { get; set; }
        public List<FrameworkReferenceItem> Frameworks { get; set; }
        public List<InternalReferenceItem> InternalReferences { get; set; }
        public int CurrentID { get; set; }
        public bool ShowResize { get;set; }
    }

    public class InternalReferenceItem
    {
        public string DocumentType { get; set; }
        public string Title { get; set; }
        public string DocumentReferenceNumber { get; set; }
    }

    public class FrameworkReferenceItem
    {
        public string Framework { get; set; }
        public string FrameworkID { get; set; }
        public string Description { get; set; }
    }
    
    public class ProcessRelationItem
    {
        public string ReferenceNumber { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
    }

    public class SubProcessRelationItem
    {
        public string SubProcessDiagram { get; set; }
        public string SubProcessOverview { get; set; }
        public string SubProcessOwner { get; set; }
        public string Authors { get; set; }
    }

    public class ProcessDescription 
    {
        public string ProcessName { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string Objective { get; set; }
        public string Strategy { get; set; }
        public string DocumentOwners { get; set; }
    }

    public class DiagramContent 
    {
        public string DiagramPath { get; set; }
        public List<EntityDTO> RelatedProcess { get; set; }
        public List<EntityDTO> RelatedSubProcess { get; set; }
    }

    public class BusinessRuleMappingItem
    {
        public string SectionName { get; set; }
        public string SectionDescription { get; set; }
    }

    public class AcronymItem
    {
        public string Acronym { get; set; }
        public string Description { get; set; }
        //public int ID { get; set; }
    }

    public class ApplicationRelationshipItem
    {
        public string Application { get; set; }
        public string Description { get; set; }
    }
}
