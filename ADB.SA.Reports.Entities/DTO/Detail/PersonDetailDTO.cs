using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class PersonDetailDTO
    {
        public string Title { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string TemplateID { get { return "#person-content"; } }
    }
}
