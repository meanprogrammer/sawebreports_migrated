using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class BpmnDetailCTLDTO : IDetailDTO
    {
        public BpmnDetailCTLDTO() {
            this.RelatedSubProcess = new List<string>();
        }

        public List<string> RelatedSubProcess { get; set; }
        public string User { get; set; }
        public string ActivityNature { get; set; }
        public string TriggerInput { get; set; }
        public string Output { get; set; }
        public string ActivityStepDescription { get; set; }
        public string ActivityNarrative { get; set; }
        public List<ControlDetailItem> ControlDetails { get; set; }
        public string Description { get; set; }
        public string ReferencedDocuments { get; set; }
        public string TemplateID { get { return "#ctlbpmndetail-content"; } }
        public string Title { get; set; }
    }

    public class ControlDetailItem
    {
        public string ControlName { get; set; }
        public string ControlDescription { get; set; }
        public string Completeness { get; set; }
        public string Accuracy { get; set; }
        public string Validity { get; set; }
        public string RestrictedAccess { get; set; }
        public string ControlType { get; set; }
        public string ControlKind { get; set; }
        public string ControlOwner { get; set; }

        public ControlDetailDTO ControlDetail { get; set; }
        public RiskDetailDTO RiskDetail { get; set; }
    }

    public class ControlDetailDTO
    {
        public string MitigatesRisk { get; set; }
        public string Evidence { get; set; }
        public List<BpmnDetailHoverDTO> BusinessUnit { get; set; }
        public List<BpmnDetailHoverDTO> ControlOwner { get; set; }
        public List<BpmnDetailHoverDTO> ControlObjectives { get; set; }
        public List<BpmnDetailHoverDTO> Frequency { get; set; }
        public List<BpmnDetailHoverDTO> ApplicationName { get; set; }
        public string ControlCategory { get; set; }
        public string Corrective { get; set; }
        public string ReferenceDocuments { get; set; }
    }

    public class RiskDetailDTO : IDetailDTO
    {
        public string Consequence { get; set; }
        public string Completeness { get; set; }
        public string ExistenceOccurrence { get; set; }
        public string ValuationOrAllocation { get; set; }
        public string RightsAndObligations { get; set; }
        public string PresentationAndDisclosure { get; set; }
        public string Description { get;set;}
        public string ReferencedDocuments { get;set;}
        public string TemplateID
        {
            get { return string.Empty; }
        }
        public string Title { get; set; }
    }
}
