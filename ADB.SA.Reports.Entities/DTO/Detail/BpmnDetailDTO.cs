using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class BpmnDetailDTO
    {
        public string TemplateID { get { return "#bpmndetail-content"; } }
        public BpmnDetailDTO() {
            this.ActivityVariations = new List<ActivityVariationItem>();
            this.Paragraphs = new List<ParagraphItem>();
            this.UseCases = new List<UseCaseItem>();
            this.RequiredData = new List<RequiredDataItem>();
            this.SampleReferences = new List<SampleReferenceItem>();
        }
        public string Title { get; set; }
        public string RelatedSubprocess { get; set; }
        public string User { get; set; }
        public string ActivityNature { get; set; }
        public string TriggerInput { get; set; }
        public string Output { get; set; }
        public string ActivityStepDescription { get; set; }
        public string ActivityNarrative { get; set; }
        public List<ActivityVariationItem> ActivityVariations { get; set; }
        public List<ParagraphItem> Paragraphs { get; set; }
        public List<UseCaseItem> UseCases { get; set; }
        public List<RequiredDataItem> RequiredData { get; set; }
        public List<SampleReferenceItem> SampleReferences { get; set; }
    }

    public class ActivityVariationItem
    {
        public string ActivityVariation { get; set; }
        public string Description { get; set; }
    }

    public class ParagraphItem
    {
        public string ParagraphName { get; set; }
        public string ParagraphRef { get; set; }
    }

    public class UseCaseItem
    {
        public string UseCaseID { get; set; }
        public string Description { get; set; }
    }

    public class RequiredDataItem
    {
        public string RequiredData { get; set; }
        public string Description { get; set; }
    }

    public class SampleReferenceItem
    {
        public string SampleReference { get; set; }
        public string Description { get; set; }
        public string ReferenceLink { get; set; }
    }
}
