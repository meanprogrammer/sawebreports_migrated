using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class StrategicAgendaReport
    {
        public string Agenda { get; set; }
        public string Description { get; set; }

        public string RelatedBusinessPolicy { get; set; }
        public string Year { get; set; }
        public string ReferenceDocuments { get; set; }

        public string BusinessRules { get; set; }
        public List<RelatedPSA> RightSide { get; set; }

    }

    public class RelatedPSA
    {
        public string ProcessName { get; set; }
        public string SubProcessName { get; set; }
        public List<string> Applications { get; set; }
    }
}
