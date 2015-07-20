
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class ProcessPageResponseDTO : PageResponseDTO
    {
        public ProcessContentDTO Content { get; set; }
        public override string RenderType
        {
            get
            {
                return "process";
            }
            set
            {
                base.RenderType = value;
            }
        }
    }
}
