using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Utilities.HtmlHelper;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Entities.Utils;

namespace ADB.SA.Reports.Presenter.Content
{
    public class SectionNameDetailStrategy : DetailStrategyBase
    {
        public override object BuildDetail(EntityDTO dto)
        {
            SectionNameDetailDTO detail = new SectionNameDetailDTO();
            detail.SectionReference = dto.RenderHTMLAsAnchor(GlobalStringResource.SectionReference, RenderOption.Break);
            detail.Description = BuildDescription(dto);
            detail.ReferencedDocuments = BuildReferencedDocuments(dto);
            return detail;
        }
    }
}
