using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class BreadCrumbItemDTO
    {
        public int Order { get; set; }
        public string Link { get; set; }
        public string Label { get; set; }
        public string CssClass { get; set; }
    }
}
