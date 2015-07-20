using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Utilities.HtmlHelper;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Content
{
    public class PersonDetailStrategy : DetailStrategyBase
    {
        public override object BuildDetail(EntityDTO dto)
        {
            PersonDetailDTO detail = new PersonDetailDTO();
            detail.Title = dto.Name;
            detail.Email = dto.RenderHTML(GlobalStringResource.Email, RenderOption.Break);
            detail.Contact = dto.RenderHTML(GlobalStringResource.Contact, RenderOption.Break);
            return detail;
        }
    }
}
