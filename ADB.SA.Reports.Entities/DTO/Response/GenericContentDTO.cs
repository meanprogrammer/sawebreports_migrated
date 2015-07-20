using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class GenericContentDTO : IResizeDiagram
    {
        public DiagramContent Diagram { get; set; }
        public string DiagramDescription { get; set; }
        public int CurrentID { get; set; }
        public bool ShowResize { get;set; }
    }
}
