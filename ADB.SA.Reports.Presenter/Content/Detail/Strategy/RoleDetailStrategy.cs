using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Utilities.HtmlHelper;

namespace ADB.SA.Reports.Presenter.Content
{
    public class RoleDetailStrategy : DetailStrategyBase
    {
        public override object BuildDetail(EntityDTO dto)
        {
            RoleDetailDTO detail = new RoleDetailDTO();
            EntityData data = new EntityData();
            EntityDTO description = data.GetRoleDetail(dto.ID);
            description.ExtractProperties();

            detail.Responsibilities = description.RenderHTML(GlobalStringResource.Responsibilities, RenderOption.Break);
            detail.Description = BuildDescription(dto);
            detail.ReferencedDocuments = BuildReferencedDocuments(dto);
            return detail;
        }
    }
}
