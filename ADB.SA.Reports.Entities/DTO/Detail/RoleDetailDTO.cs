using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class RoleDetailDTO : IDetailDTO
    {
        public string Responsibilities { get; set; }
        public string Description { get;set;}
        public string ReferencedDocuments { get; set; }
        public string TemplateID 
        {
            get { return "#role-content"; }
        }

        public string Title  { get; set; }
    }
}
