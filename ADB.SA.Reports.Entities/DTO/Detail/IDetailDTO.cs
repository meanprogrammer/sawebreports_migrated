using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public interface IDetailDTO
    {
        string Description { get; set; }
        string ReferencedDocuments { get; set; }
        string TemplateID { get; }
        string Title { get; set; }
    }
}
