using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Utilities.HtmlHelper;

namespace ADB.SA.Reports.Presenter.Content
{
    public class ReviewerApproverPositionDetailStrategy : DetailStrategyBase
    {
        public override object BuildDetail(EntityDTO dto)
        {
            ReviewerApproverPositionDetailDTO detail = new ReviewerApproverPositionDetailDTO();
            detail.AssignedTo = dto.RenderHTML(GlobalStringResource.Assignedto, RenderOption.Break);
            detail.Description = BuildDescription(dto);
            detail.ReferencedDocuments = BuildReferencedDocuments(dto);
            return detail;
        }
    }
}
