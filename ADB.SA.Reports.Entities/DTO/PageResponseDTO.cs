using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class PageResponseDTO
    {
        public string Page {
            get {
                return "page";
            }
        }

        public virtual string RenderType { get; set; }
        public string Header { get; set; }
        public List<BreadCrumbItemDTO> BreadCrumbContent { get; set; }
        public object Content { get; set; }

    }
}
