using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class GenericDetailDTO : IDetailDTO
    {
        public string TemplateID { get { return "#g-content"; } }
        public string Title { get; set; }
        public string Description { get;set; }
        public string ReferencedDocuments { get; set; }
    }
}
