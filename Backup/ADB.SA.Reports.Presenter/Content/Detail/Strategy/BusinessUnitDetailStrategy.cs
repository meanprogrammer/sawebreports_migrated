using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Utilities.HtmlHelper;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Presenter.Content
{
    public class BusinessUnitDetailStrategy : DetailStrategyBase
    {
        public override object BuildDetail(ADB.SA.Reports.Entities.DTO.EntityDTO dto)
        {
            GenericDetailDTO detail = new GenericDetailDTO();
            detail.Description = BuildDescription(dto);
            detail.ReferencedDocuments = BuildReferencedDocuments(dto);
            return detail;
        }
    }
}
