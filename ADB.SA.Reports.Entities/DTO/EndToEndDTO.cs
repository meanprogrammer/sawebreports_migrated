using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class EndToEndDTO
    {
        public EntityDTO AssociatedEventDTO { get; set; }
        public EntityDTO FromDiagramDTO { get; set; }
        public EntityDTO ToDiagramDTO { get; set; }
        //public EntityDTO AssociatedDiagramDTO { get; set; }
        public List<EndToEndDTO> SubItems { get; set; }
    }

    //public class EndToEndSubItemDTO
    //{
    //    public EntityDTO AssociatedDTO { get; set; }
    //    public List<EndToEndSubItemDTO> SubItems { get; set; }
    //}
}
