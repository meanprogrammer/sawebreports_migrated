using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class Strategy2020ContentDTO : IResizeDiagram
    {
        public DiagramContent Diagram { get; set; }
        public string DiagramDescription { get; set; }
        public List<ChallengeItem> Challenges { get; set; }
        public List<StrategicAngendaItem> StrategicAgendas { get; set; }
        public List<DriversOfChangeItem> DriversOfChange { get; set; }
        public List<DevelopingPartnerCountriesItem> DevelopingPartners { get; set; }
        public List<CoreAreasOfOperationItem> CoreAreas { get; set; }
        public List<OtherAreasOfOperationItem> OtherAreas { get; set; }
        public List<CorporateValuesItem> CorporateValues { get; set; }
        public List<OperationalGoalsItem> OperationalGoals { get; set; }
        public List<InstitutionalGoalsItem> InstitutionalGoals { get; set; }
        public List<ResultFrameworkLevelItem> ResultFrameworks { get; set; }
        public int CurrentID { get; set; }
        public bool ShowResize
        {
            get;
            set;
        }
    }

    public class ChallengeItem
    {
        public string Challenge { get; set; }
        public string Description { get; set; }
    }

    public class StrategicAngendaItem
    {
        public string StrategicAgenda { get; set; }
        public string Description { get; set; }
    }

    public class DriversOfChangeItem
    {
        public string DriversOfChange { get; set; }
        public string Description { get; set; }
    }

    public class DevelopingPartnerCountriesItem
    {
        public string DevelopingPartnerCountries { get; set; }
        public string Description { get; set; }
    }

    public class CoreAreasOfOperationItem
    {
        public string CoreAreasOfOperation { get; set; }
        public string Description { get; set; }
    }

    public class OtherAreasOfOperationItem
    {
        public string OtherAreasOfOperation { get; set; }
        public string Description { get; set; }
    }

    public class CorporateValuesItem
    {
        public string CorporateValues { get; set; }
        public string Description { get; set; }
    }

    public class OperationalGoalsItem
    {
        public string OperationalGoals { get; set; }
        public string Description { get; set; }
    }

    public class InstitutionalGoalsItem
    {
        public string InstitutionalGoals { get; set; }
        public string Description { get; set; }
    }

    public class ResultFrameworkLevelItem
    {
        public string ResultFrameworkLevel { get; set; }
        public string Description { get; set; }
    }
}
