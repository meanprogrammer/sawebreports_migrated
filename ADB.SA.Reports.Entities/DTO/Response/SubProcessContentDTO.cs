using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class SubProcessContentDTO : IResizeDiagram
    {
        public DiagramContent Diagram { get; set; }
        public ProcessDescription ProcessDescription { get; set; }
        public SubProcessDescription SubProcessDescription { get; set; }
        public List<RolesAndResponsibilityItem> RolesAndResponsibilities { get; set; }
        public List<SubProcessDependency> Dependencies { get; set; }
        public List<ActivityOverviewItem> ActivityOverviews { get; set; }
        public List<ModuleRelationshipItem> ModuleRelationships { get; set; }
        public List<BusinessRuleMappingSPItem> BusinessRuleMappings { get; set; }
        public List<ChangeHistoryItem> ChangeHistories { get; set; }
        public int CurrentID { get; set; }
        public bool ShowResize { get; set; }
    }

    public class ChangeHistoryItem
    {
        public string Version { get; set; }
        public string Date { get; set; }
        public string Reason { get; set; }
        public string Author { get; set; }
    }

    public class ModuleRelationshipItem
    {
        public string Module { get; set; }
        public string Description { get; set; }
    }

    public class BusinessRuleMappingSPItem
    {
        public BusinessRuleMappingSPItem() { Activity = new BusinessRuleMappingActivity(); }
        public BusinessRuleMappingActivity Activity { get; set; }
    }

    public class BusinessRuleMappingActivity
    {
        public BusinessRuleMappingActivity() { Paragraphs = new List<BusinessRuleMappingParagraph>(); }
        public string Activity { get; set; }
        public int RowSpan { get; set; }
        public List<BusinessRuleMappingParagraph> Paragraphs { get; set; }
    }

    public class BusinessRuleMappingParagraph
    {
        public string ParagraphName { get; set; }
        public string ParagraphReference { get; set; }
    }

    public class SubProcessDescription
    {
        public string Objective { get; set; }
        public string DocumentOwners { get; set; }
        public string FrameworkReference { get; set; }
        public string InternalReference { get; set; }
    }

    public class RolesAndResponsibilityItem
    {
        public string Role { get; set; }
        public string Responsibilities { get; set; }
    }

    public class SubProcessDependency
    {
        public string Order { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Objective { get; set; }
    }

    public class ActivityOverviewItem
    {
        public string Activity { get; set; }
        public string User { get; set; }
        public string Trigger { get; set; }
        public string Output { get; set; }
        public string keyDocs { get; set; }
        public string SystemsInfo { get; set; }
        public string Memo { get; set; }
    }
}
