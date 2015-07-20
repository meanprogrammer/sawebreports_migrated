using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Utilities.HtmlHelper;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Content
{
    public class AcronymDetailStrategy : DetailStrategyBase
    {
        public override object BuildDetail(EntityDTO dto)
        {
            AcronymDetailDTO detail = new AcronymDetailDTO();
            detail.Title = dto.Name;
            detail.AbbreviationDescription = dto.RenderHTML(GlobalStringResource.AbbreviationDescription, RenderOption.Break);
            detail.Description = BuildDescription(dto);
            detail.ReferencedDocuments = BuildReferencedDocuments(dto);
            return detail;
        }
    }
}
