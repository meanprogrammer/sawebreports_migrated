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
    public class Strategy2020ContentStrategy : ContentStrategyBase
    {
        Strategy2020Data data;

        public Strategy2020ContentStrategy()
        {
            data = new Strategy2020Data();
        }

        public override string BuildContent(EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();
            //html.Append(base.BuildTitle(dto.Name));
            //html.Append(Resources.Split);

            List<KeyValuePair<string, string>> contents = new List<KeyValuePair<string, string>>();

            contents.Add(new KeyValuePair<string, string>("Diagram", BuildDiagramImage(dto)));
            contents.Add(new KeyValuePair<string, string>("Diagram Desc.", BuildDiagramDescription(dto)));
            contents.Add(new KeyValuePair<string, string>("Challenges", BuildChallenges(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Strategic Agenda", BuildStrategicAgenda(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Drivers of Change", BuilddriversOfChange(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Developing Partner Countries", BuildDevelopingPartnerCountries(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Core Operation Area", BuildCoreAreasOfOperations(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Other Operation Area", BuildOtherAreasOfOperations(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Corporate Values", BuildCorporateValues(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Operational Goals", BuildOperationalGoals(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Institutional Goals", BuildInstitutionalGoals(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Result Framework Level", BuildResultFrameworkLevel(dto.ID)));
            contents.Add(new KeyValuePair<string, string>("Comments", BuildCommentsSection(dto)));


            html.Append("<div id=\"tabs\">");
            html.Append("<ul>");
            for (int i = 0; i < contents.Count; i++)
            {
                if (!string.IsNullOrEmpty(contents[i].Value))
                {
                    html.AppendFormat("<li><a href=\"#tabs-{0}\">{1}</a></li>", i + 1, contents[i].Key);
                }
            }
            html.Append("</ul>");

            for (int i = 0; i < contents.Count; i++)
            {
                if (!string.IsNullOrEmpty(contents[i].Value))
                {
                    html.AppendFormat("<div class=\"content-holder\" id=\"tabs-{0}\">{1}</div>", i + 1, contents[i].Value);
                }
            }

            html.Append("</div>");

            html.Append(base.BuildReportHiddenField(dto.ID));

            return html.ToString();
        }

        private string BuildCommentsSection(EntityDTO dto)
        {
            return string.Format(GlobalStringResource.CommentFormHtml, dto.Name, dto.ID);
        }

        private string BuildChallenges(int id)
        {
            string result = string.Empty;
            List<EntityDTO> challenges = data.GetAllChallenges(id);

            if (challenges.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 1, "grid", new int[] { 50,50 });
                t.AddHeader("Challenge");
                t.AddHeader("Description");

                foreach (EntityDTO challenge in challenges)
                {
                    challenge.ExtractProperties();
                    t.AddCell(challenge.Name);
                    t.AddCell(challenge.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        private string BuildStrategicAgenda(int id)
        {
            string result = string.Empty;
            List<EntityDTO> agendas = data.GetAllStrategicAgenda(id);

            if (agendas.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 1, "grid", new int[] { 50, 50 });
                t.AddHeader("Strategic Agenda");
                t.AddHeader("Description");

                foreach (EntityDTO agenda in agendas)
                {
                    agenda.ExtractProperties();
                    t.AddCell(agenda.Name);
                    t.AddCell(agenda.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        private string BuildDevelopingPartnerCountries(int id)
        {
            string result = string.Empty;
            List<EntityDTO> dpcs = data.GetAllDevelopingPartnerCountries(id);

            if (dpcs.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 1, "grid", new int[] { 50, 50 });
                t.AddHeader("Developing Partner Countries");
                t.AddHeader("Description");

                foreach (EntityDTO dpc in dpcs)
                {
                    dpc.ExtractProperties();
                    t.AddCell(dpc.Name);
                    t.AddCell(dpc.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        private string BuildCoreAreasOfOperations(int id)
        {
            string result = string.Empty;
            List<EntityDTO> areas = data.GetAllCoreAreasOfOperations(id);

            if (areas.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 1, "grid", new int[] { 50, 50 });
                t.AddHeader("Core Areas of Operation");
                t.AddHeader("Description");

                foreach (EntityDTO area in areas)
                {
                    area.ExtractProperties();
                    t.AddCell(area.Name);
                    t.AddCell(area.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        private string BuildOtherAreasOfOperations(int id)
        {
            string result = string.Empty;
            List<EntityDTO> otherareas = data.GetAllOtherAreasOfOperations(id);

            if (otherareas.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 1, "grid", new int[] { 50, 50 });
                t.AddHeader("Other Areas of Operation");
                t.AddHeader("Description");

                foreach (EntityDTO area in otherareas)
                {
                    area.ExtractProperties();
                    t.AddCell(area.Name);
                    t.AddCell(area.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        private string BuildCorporateValues(int id)
        {
            string result = string.Empty;
            List<EntityDTO> values = data.GetAllCorporateValues(id);

            if (values.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 1, "grid", new int[] { 50, 50 });
                t.AddHeader("Corporate Values");
                t.AddHeader("Description");

                foreach (EntityDTO v in values)
                {
                    v.ExtractProperties();
                    t.AddCell(v.Name);
                    t.AddCell(v.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        private string BuildOperationalGoals(int id)
        {
            string result = string.Empty;
            List<EntityDTO> goals = data.GetAllOperationalGoals(id);

            if (goals.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 1, "grid", new int[] { 50, 50 });
                t.AddHeader("Operational Goals");
                t.AddHeader("Description");

                foreach (EntityDTO g in goals)
                {
                    g.ExtractProperties();
                    t.AddCell(g.Name);
                    t.AddCell(g.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        private string BuildInstitutionalGoals(int id)
        {
            string result = string.Empty;
            List<EntityDTO> goals = data.GetAllInstitutionalGoals(id);

            if (goals.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 1, "grid", new int[] { 50, 50 });
                t.AddHeader("Institutional Goals");
                t.AddHeader("Description");

                foreach (EntityDTO g in goals)
                {
                    g.ExtractProperties();
                    t.AddCell(g.Name);
                    t.AddCell(g.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        private string BuildResultFrameworkLevel(int id)
        {
            string result = string.Empty;
            List<EntityDTO> results = data.GetAllResultFrameworkLevel(id);

            if (results.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 1, "grid", new int[] { 50, 50 });
                t.AddHeader("Result Framework Level");
                t.AddHeader("Description");

                foreach (EntityDTO r in results)
                {
                    r.ExtractProperties();
                    t.AddCell(r.Name);
                    t.AddCell(r.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        private string BuilddriversOfChange(int id)
        {
            string result = string.Empty;
            List<EntityDTO> drivers = data.GetAllDriversOfChange(id);

            if (drivers.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 1, "grid", new int[] { 50, 50 });
                t.AddHeader("Drivers of Change");
                t.AddHeader("Description");

                foreach (EntityDTO d in drivers)
                {
                    d.ExtractProperties();
                    t.AddCell(d.Name);
                    t.AddCell(d.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None));
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        protected override string BuildDiagramDescription(EntityDTO dto)
        {
            dto.ExtractProperties();
            string description = dto.RenderHTML("Description", ADB.SA.Reports.Entities.Enums.RenderOption.Break);
            if (string.IsNullOrEmpty(description))
            {
                description = "There is no description for this diagram.";
            }

            return description;
        }

        protected override string BuildProcessRelation(int id)
        {
            return string.Empty;
        }

        protected override string BuildSubProcessRelation(int id)
        {
            return string.Empty;
        }

        protected override string BuildBusinessMapping(int id)
        {
            return string.Empty;
        }

        protected override string BuildAcronyms(int id)
        {
            return string.Empty;
        }

        protected override string BuildReviewersAndApprovers(int id)
        {
            return string.Empty;
        }

        protected override string BuildApplicationRelationship(int id)
        {
            return string.Empty;
        }

        protected override string BuildFrameworkReference(int id)
        {
            return string.Empty;
        }

        protected override string BuildInternalReference(int id)
        {
            return string.Empty;
        }

        protected override string BuildModuleRelationship(int id)
        {
            return string.Empty;
        }
    }
}
