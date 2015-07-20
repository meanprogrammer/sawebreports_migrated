using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Presenter.Utils;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Entities.Utils;

namespace ADB.SA.Reports.Presenter.Content
{
    public class BpmnDetailCTLStrategy : DetailStrategyBase
    {
        EntityData entityData = null;

        public BpmnDetailCTLStrategy()
        {
            entityData = new EntityData();
        }

        public override object BuildDetail(ADB.SA.Reports.Entities.DTO.EntityDTO dto)
        {
            BpmnDetailCTLDTO detail = new BpmnDetailCTLDTO();
            List<EntityDTO> relatedCtlSubProcess = entityData.GetCtlSubProcess(dto.ID);

            if (relatedCtlSubProcess.Count > 0)
            {
                foreach (var ctl in relatedCtlSubProcess)
                {
                    detail.RelatedSubProcess.Add(ctl.Name);
                }
            }

            detail.User = dto.RenderHTML(GlobalStringResource.User, RenderOption.Break);
            detail.ActivityNature = dto.RenderHTML(GlobalStringResource.ActivityNature, RenderOption.Break);
            detail.TriggerInput = dto.RenderHTML(GlobalStringResource.TriggerInput, RenderOption.Break);
            detail.Output = dto.RenderHTML(GlobalStringResource.Output, RenderOption.Break);
            detail.ActivityStepDescription = dto.RenderHTML(GlobalStringResource.ActivityStepDescription, RenderOption.Break);
            detail.ActivityNarrative = dto.RenderHTML(GlobalStringResource.ActivityNarrative,
                RenderOption.Paragraph);

            List<EntityDTO> controTypes = entityData.GetControlTypesCtl(dto.ID);
            detail.ControlDetails = new List<ControlDetailItem>();
            if (controTypes.Count > 0)
            {
                foreach (EntityDTO control in controTypes)
                {
                    ControlDetailItem item = new ControlDetailItem();
                    control.ExtractProperties();
                    item.ControlName = control.Name;
                    item.ControlDescription = control.RenderHTML("Description", RenderOption.None);
                    item.Completeness =  GetImageTag(control.RenderHTML("Completeness", RenderOption.None));
                    item.Accuracy = GetImageTag(control.RenderHTML("Accuracy", RenderOption.None));
                    item.Validity = GetImageTag(control.RenderHTML("Validity", RenderOption.None));
                    item.RestrictedAccess = GetImageTag(control.RenderHTML("Restricted Access", RenderOption.None));
                    item.ControlType = control.RenderHTML("Control Type", RenderOption.None);
                    item.ControlKind = control.RenderHTML("Control Kind", RenderOption.None);
                    item.ControlOwner =control.RenderHTML("WHO",RenderOption.None);


                    
                    item.ControlDetail = CreateControlDetailsHtml(control);
                    item.RiskDetail = CreateRiskDetails(control);
                    detail.ControlDetails.Add(item);
                }
            }
            detail.Title = dto.Name;
            return detail;
        }

        private RiskDetailDTO CreateRiskDetails(EntityDTO control)
        {
            RiskDetailDTO detail = new RiskDetailDTO();

            EntityDTO relatedRisk = entityData.GetControlRelatedRisk(control.ID);
            if (relatedRisk != null)
            {
                relatedRisk.ExtractProperties();

                //Consequence
                detail.Consequence = relatedRisk.RenderHTML("Consequence", RenderOption.Break);

                //Completeness
                detail.Completeness = relatedRisk.RenderHTML("Completeness", RenderOption.Break);

                //Existence/Occurrence
                detail.ExistenceOccurrence = relatedRisk.RenderHTML("Existence/Occurrence", RenderOption.Break);

                //Valuation or Allocation
                detail.ValuationOrAllocation = relatedRisk.RenderHTML("Valuation or Allocation", RenderOption.Break);

                //Rights and Obligations
                detail.RightsAndObligations = relatedRisk.RenderHTML("Rights and Obligations", RenderOption.Break);

                //Presentation and Disclosure
                detail.PresentationAndDisclosure = relatedRisk.RenderHTML("Presentation and Disclosure", RenderOption.Break);

                //Description
                detail.Description = relatedRisk.RenderHTML("Description", RenderOption.Break);

                //Reference Documents
                detail.ReferencedDocuments = relatedRisk.RenderHTML("Reference Documents", RenderOption.Break);
            }
            return detail;
        }

        private ControlDetailDTO CreateControlDetailsHtml(EntityDTO control)
        {
            ControlDetailDTO detail = new ControlDetailDTO();

 
            //Mitigates Risk
            detail.MitigatesRisk = control.RenderHTML("Mitigates Risk", RenderOption.None);

            //Evidence
            detail.Evidence = control.RenderHTML("Evidence", RenderOption.Break);

            //Business Unit
            List<BpmnDetailHoverDTO> relatedBusinessUnit = ConvertToHoverDTO(entityData.GetRelatedBusinessUnit(control.ID));
            //string rbuhtml = RenderControlRelatedProperties(relatedBusinessUnit);
            detail.BusinessUnit = relatedBusinessUnit;

            //WHO - (control owner)
            List<BpmnDetailHoverDTO> relatedControlOwners = ConvertToHoverDTO(entityData.GetRelatedControlOwner(control.ID));
            //string rcoHtml = RenderControlRelatedProperties(relatedControlOwners);
            detail.ControlOwner = relatedControlOwners;

            //Control Objectives
            List<BpmnDetailHoverDTO> relatedControlObj = ConvertToHoverDTO(entityData.GetRelatedControlObjectives(control.ID));
            detail.ControlObjectives = relatedControlObj;

            //Frequency
            List<BpmnDetailHoverDTO> relatedFrequency = ConvertToHoverDTO(entityData.GetRelatedFrequency(control.ID));
            detail.Frequency = relatedFrequency;

            //Application Name
            List<BpmnDetailHoverDTO> relatedApplications = ConvertToHoverDTO(entityData.GetRelatedControlApplications(control.ID));
            detail.ApplicationName = relatedApplications;

            //Control Category
            detail.ControlCategory = control.RenderHTML("Control Category", RenderOption.Break);

            //Corrective
            detail.Corrective = control.RenderHTML("Corrective", RenderOption.Break);

            //Reference Documents
            detail.ReferenceDocuments = control.RenderHTML("Reference Documents", RenderOption.Break);

            return detail;
        }

        private List<BpmnDetailHoverDTO> ConvertToHoverDTO(List<EntityDTO> rawData) 
        {
            List<BpmnDetailHoverDTO> list = new List<BpmnDetailHoverDTO>();
            foreach (EntityDTO d in rawData)
            {
                d.ExtractProperties();
                BpmnDetailHoverDTO dto = new BpmnDetailHoverDTO();
                dto.Title = d.Name;
                dto.Description = BuildDescription(d);
                dto.ReferencedDocuments = BuildReferencedDocuments(d);
                list.Add(dto);
            }
            return list;
        }

        //private string BuildDescription(HtmlTable table, EntityDTO dto)
        //{
        //     return dto.RenderHTML(GlobalStringResource.Description, RenderOption.Break);
        //}

        //private string BuildReferencedDocuments(HtmlTable table, EntityDTO dto)
        //{
        //    string link = dto.RenderHTML(GlobalStringResource.ReferenceDocuments, RenderOption.NewLine);
        //    return dto.RenderMultipleLinks(link, true);
        //}


        private string RenderControlRelatedProperties(List<EntityDTO> controlRelatedEntity)
        {
            StringBuilder html = new StringBuilder();
            if (controlRelatedEntity.Count > 0)
            {
                html.Append("<ul style=\"list-style:none;margin:0;padding:0;\">");
                foreach (EntityDTO entity in controlRelatedEntity)
                {
                    entity.ExtractProperties();
                    html.Append("<li>");
                    html.AppendFormat("<a tag=\"{0}\">{1}</a>", entity.ID, entity.Name);
                    html.AppendFormat("<div id=\"div_{0}\" class=\"tooltip-holder\">", entity.ID);
                    html.Append(DetailBuilder2.BuildDetail(entity));
                    html.Append("</div>");
                    html.Append("</li>");
                }
                html.Append("</ul>");
            }
            return html.ToString();
        }

        private static string GetImageTag(string value)
        {
            if (value.ToUpper() == "T")
            {
                return "<img src=\"images/tick-icon.png\" alt=\"\" />";
            }
            return "<img src=\"images/cross-icon.png\" alt=\"\" />";
        }
    }
}
