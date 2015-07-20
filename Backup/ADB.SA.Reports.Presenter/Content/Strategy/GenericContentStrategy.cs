using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Content
{
    public class GenericContentStrategy : ContentStrategyBase
    {

        public override string BuildContent(EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();
            //html.Append(base.BuildTitle(dto.Name));
            //html.Append(Resources.Split);

            List<KeyValuePair<string, string>> contents = new List<KeyValuePair<string, string>>();

            contents.Add(new KeyValuePair<string, string>("Diagram", BuildDiagramImage(dto)));
            contents.Add(new KeyValuePair<string, string>("Diagram Desc.", BuildDiagramDescription(dto)));
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
