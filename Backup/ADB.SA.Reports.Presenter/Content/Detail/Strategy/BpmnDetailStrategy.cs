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
    public class BpmnDetailStrategy : DetailStrategyBase
    {
        public override object BuildDetail(EntityDTO dto)
        {
            BpmnDetailDTO detail = new BpmnDetailDTO();
            EntityData data = new EntityData();

            detail.Title = dto.Name;
            EntityDTO parent = data.GetActivityOverviewParent(dto.ID);
            //TODO:
            detail.RelatedSubprocess = parent.Name;
            detail.User = dto.RenderHTML(GlobalStringResource.User, RenderOption.Break);
            detail.ActivityNature = dto.RenderHTML(GlobalStringResource.ActivityNature, RenderOption.Break);
            detail.TriggerInput = dto.RenderHTML(GlobalStringResource.TriggerInput, RenderOption.Break);
            detail.Output = dto.RenderHTML(GlobalStringResource.Output, RenderOption.Break);
            detail.ActivityStepDescription = dto.RenderHTML(GlobalStringResource.ActivityStepDescription, RenderOption.Break);
            detail.ActivityNarrative = dto.RenderHTML(GlobalStringResource.ActivityNarrative,
                RenderOption.Break);

            List<EntityDTO> variations = data.GetActivityVariations(dto.ID);

            if (variations.Count > 0)
            {
                foreach (EntityDTO variation in variations)
                {
                    ActivityVariationItem av = new ActivityVariationItem();
                    variation.ExtractProperties();
                    av.ActivityVariation = variation.Name;
                    av.Description = variation.RenderHTML(GlobalStringResource.Description, RenderOption.Break);
                    detail.ActivityVariations.Add(av);
                }
            }

            List<EntityDTO> mappings = data.GetSubProcessParagraphs(dto.ID);

            if (mappings.Count > 0)
            {
                foreach (EntityDTO map in mappings)
                {
                    ParagraphItem p = new ParagraphItem();
                    map.ExtractProperties();
                    p.ParagraphName = MappingToolUrlHelper.GenerateValidParagraphLinkMarkup(map.Name);
                    p.ParagraphRef = map.RenderHTMLAsAnchor(GlobalStringResource.ParagraphReference, RenderOption.Span, true);
                    detail.Paragraphs.Add(p);
                }
            }

            List<EntityDTO> useCases = data.GetUseCases(dto.ID);

            if (useCases.Count > 0)
            {
                foreach (EntityDTO useCase in useCases)
                {
                    UseCaseItem uc = new UseCaseItem();
                    useCase.ExtractProperties();
                    uc.UseCaseID = useCase.Name;
                    uc.Description = useCase.RenderHTML(GlobalStringResource.Description, RenderOption.Break);
                    detail.UseCases.Add(uc);
                }
            }

            List<EntityDTO> requiredData = data.GetRequiredData(dto.ID);

            if (requiredData.Count > 0)
            {
                requiredData.Sort((b1, b2) => string.Compare(b1.Name, b2.Name, true));
                foreach (EntityDTO document in requiredData)
                {
                    RequiredDataItem rq = new RequiredDataItem();
                    document.ExtractProperties();
                    rq.RequiredData = document.Name;
                    rq.Description = document.RenderHTML(GlobalStringResource.Description, RenderOption.Break);
                    detail.RequiredData.Add(rq);
                }
            }

            List<EntityDTO> sampleReferences = data.GetSampleReference(dto.ID);

            if (sampleReferences.Count > 0)
            {
                foreach (EntityDTO reference in sampleReferences)
                {
                    SampleReferenceItem sr = new SampleReferenceItem();
                    reference.ExtractProperties();
                    sr.SampleReference = reference.Name;
                    sr.Description = reference.RenderHTML(GlobalStringResource.Description, RenderOption.Break);
                    sr.ReferenceLink = reference.RenderHTMLAsAnchor(GlobalStringResource.ReferenceLink, RenderOption.Span, true);
                    detail.SampleReferences.Add(sr);
                }
            }
            return detail;
        }
    }
}
