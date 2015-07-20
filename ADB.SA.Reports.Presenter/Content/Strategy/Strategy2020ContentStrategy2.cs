using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Utilities.HtmlHelper;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Content
{
    public class Strategy2020ContentStrategy2 : MainContentStrategyBase
    {
        Strategy2020Data data;

        public Strategy2020ContentStrategy2()
        {
            data = new Strategy2020Data();
        }

        public override object BuildContent(EntityDTO dto)
        {
            Strategy2020ContentDTO st2020 = new Strategy2020ContentDTO();
            int id = dto.ID;
            st2020.Diagram = new DiagramContent() { 
                DiagramPath = base.BuildDiagramContent(dto)
            };
            st2020.DiagramDescription = DiagramDescription(dto);
            st2020.Challenges = Challenges(id);
            st2020.StrategicAgendas = StrategicAgenda(id);
            st2020.DriversOfChange = DriversOfChange(id);
            st2020.DevelopingPartners = DevelopingPartnerCountries(id);
            st2020.CoreAreas = CoreAreasOfOperations(id);
            st2020.OtherAreas = OtherAreasOfOperations(id);
            st2020.CorporateValues = CorporateValues(id);
            st2020.OperationalGoals = OperationalGoals(id);
            st2020.InstitutionalGoals = InstitutionalGoals(id);
            st2020.ResultFrameworks = ResultFrameworkLevel(id);
            st2020.CurrentID = id;
            st2020.ShowResize = ShowResize();
            return st2020;
        }

        private List<OperationalGoalsItem> OperationalGoals(int id)
        {
            List<OperationalGoalsItem> items = new List<OperationalGoalsItem>();
            List<EntityDTO> goals = data.GetAllOperationalGoals(id);
            if (goals.Count > 0)
            {
                foreach (EntityDTO g in goals)
                {
                    OperationalGoalsItem og = new OperationalGoalsItem();
                    g.ExtractProperties();
                    og.OperationalGoals = g.Name;
                    og.Description = g.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None);
                    items.Add(og);
                }
            }
            return items;
        }

        private string BuildCommentsSection(EntityDTO dto)
        {
            return string.Format(GlobalStringResource.CommentFormHtml, dto.Name, dto.ID);
        }

        private List<ChallengeItem> Challenges(int id)
        {
            List<ChallengeItem> items = new List<ChallengeItem>();
            List<EntityDTO> challenges = data.GetAllChallenges(id);

            if (challenges.Count > 0)
            {

                foreach (EntityDTO challenge in challenges)
                {
                    ChallengeItem c = new ChallengeItem();
                    challenge.ExtractProperties();
                    c.Challenge = challenge.Name;
                    c.Description = challenge.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None);
                    items.Add(c);
                }
            }

            return items;
        }

        private List<StrategicAngendaItem> StrategicAgenda(int id)
        {
            List<StrategicAngendaItem> items = new List<StrategicAngendaItem>();
            List<EntityDTO> agendas = data.GetAllStrategicAgenda(id);

            if (agendas.Count > 0)
            {
                foreach (EntityDTO agenda in agendas)
                {
                    StrategicAngendaItem st = new StrategicAngendaItem();
                    agenda.ExtractProperties();
                    st.StrategicAgenda = agenda.Name;
                    st.Description = agenda.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None);
                    items.Add(st);
                }
            }

            return items;
        }

        private List<DevelopingPartnerCountriesItem> DevelopingPartnerCountries(int id)
        {
            List<DevelopingPartnerCountriesItem> items = new List<DevelopingPartnerCountriesItem>();
            List<EntityDTO> dpcs = data.GetAllDevelopingPartnerCountries(id);

            if (dpcs.Count > 0)
            {
                foreach (EntityDTO dpc in dpcs)
                {
                    DevelopingPartnerCountriesItem dp = new DevelopingPartnerCountriesItem();
                    dpc.ExtractProperties();
                    dp.DevelopingPartnerCountries = dpc.Name;
                    dp.Description = dpc.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None);
                    items.Add(dp);
                }
            }
            return items;
        }

        private List<CoreAreasOfOperationItem> CoreAreasOfOperations(int id)
        {
            List<CoreAreasOfOperationItem> items = new List<CoreAreasOfOperationItem>();
            List<EntityDTO> areas = data.GetAllCoreAreasOfOperations(id);

            if (areas.Count > 0)
            {
                foreach (EntityDTO area in areas)
                {
                    CoreAreasOfOperationItem cao = new CoreAreasOfOperationItem();
                    area.ExtractProperties();
                    cao.CoreAreasOfOperation = area.Name;
                    cao.Description = area.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None);
                    items.Add(cao);
                }
            }
            return items;
        }

        private List<OtherAreasOfOperationItem> OtherAreasOfOperations(int id)
        {
            List<OtherAreasOfOperationItem> items = new List<OtherAreasOfOperationItem>();
            List<EntityDTO> otherareas = data.GetAllOtherAreasOfOperations(id);

            if (otherareas.Count > 0)
            {
                foreach (EntityDTO area in otherareas)
                {
                    OtherAreasOfOperationItem oao = new OtherAreasOfOperationItem();
                    area.ExtractProperties();
                    oao.OtherAreasOfOperation = area.Name;
                    oao.Description = area.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None);
                    items.Add(oao);
                }
            }
            return items;
        }

        private List<CorporateValuesItem> CorporateValues(int id)
        {
            List<CorporateValuesItem> items = new List<CorporateValuesItem>();
            List<EntityDTO> values = data.GetAllCorporateValues(id);

            if (values.Count > 0)
            {
                foreach (EntityDTO v in values)
                {
                    CorporateValuesItem cv = new CorporateValuesItem();
                    v.ExtractProperties();
                    cv.CorporateValues = v.Name;
                    cv.Description = v.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None);
                    items.Add(cv);
                }
            }

            return items;
        }

        private List<InstitutionalGoalsItem> InstitutionalGoals(int id)
        {
            List<InstitutionalGoalsItem> items = new List<InstitutionalGoalsItem>();
            List<EntityDTO> goals = data.GetAllInstitutionalGoals(id);

            if (goals.Count > 0)
            {
                foreach (EntityDTO g in goals)
                {
                    InstitutionalGoalsItem ig = new InstitutionalGoalsItem();
                    g.ExtractProperties();
                    ig.InstitutionalGoals = g.Name;
                    ig.Description = g.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None);
                    items.Add(ig);
                }
            }
            return items;
        }

        private List<ResultFrameworkLevelItem> ResultFrameworkLevel(int id)
        {
            List<ResultFrameworkLevelItem> items = new List<ResultFrameworkLevelItem>();
            List<EntityDTO> results = data.GetAllResultFrameworkLevel(id);

            if (results.Count > 0)
            {
                foreach (EntityDTO r in results)
                {
                    ResultFrameworkLevelItem rf = new ResultFrameworkLevelItem();
                    r.ExtractProperties();
                    rf.ResultFrameworkLevel = r.Name;
                    rf.Description = r.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None);
                    items.Add(rf);
                }
            }
            return items;
        }

        private List<DriversOfChangeItem> DriversOfChange(int id)
        {
            List<DriversOfChangeItem> items = new List<DriversOfChangeItem>();
            List<EntityDTO> drivers = data.GetAllDriversOfChange(id);

            if (drivers.Count > 0)
            {
                foreach (EntityDTO d in drivers)
                {
                    DriversOfChangeItem dc = new DriversOfChangeItem();
                    d.ExtractProperties();
                    dc.DriversOfChange = d.Name;
                    dc.Description = d.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None);
                    items.Add(dc);
                }
            }
            return items;
        }

        protected string DiagramDescription(EntityDTO dto)
        {
            dto.ExtractProperties();
            string description = dto.RenderHTML("Description", ADB.SA.Reports.Entities.Enums.RenderOption.Break);
            if (string.IsNullOrEmpty(description))
            {
                description = "There is no description for this diagram.";
            }

            return description;
        }

        //protected override string BuildProcessRelation(int id)
        //{
        //    return string.Empty;
        //}

        //protected override string BuildSubProcessRelation(int id)
        //{
        //    return string.Empty;
        //}

        //protected override string BuildBusinessMapping(int id)
        //{
        //    return string.Empty;
        //}

        //protected override string BuildAcronyms(int id)
        //{
        //    return string.Empty;
        //}

        //protected override string BuildReviewersAndApprovers(int id)
        //{
        //    return string.Empty;
        //}

        //protected override string BuildApplicationRelationship(int id)
        //{
        //    return string.Empty;
        //}

        //protected override string BuildFrameworkReference(int id)
        //{
        //    return string.Empty;
        //}

        //protected override string BuildInternalReference(int id)
        //{
        //    return string.Empty;
        //}

        //protected override string BuildModuleRelationship(int id)
        //{
        //    return string.Empty;
        //}
    }
}
