using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Entities.Enums;

namespace ADB.SA.Reports.Entities.Utils
{
    public static class EntityRenderer
    {
        public static string RenderAsLink(this EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();
            html.Append(string.Format("<a class=\"obvious-link\" href=\"Default.aspx?id={0}\">{1}</a>", dto.ID, dto.Name));
            return html.ToString();
        }

        public static string RenderAsLink(this EntityDTO dto, int id, string name, RenderOption option)
        {
            StringBuilder html = new StringBuilder();
            html.Append(string.Format("<a class=\"obvious-link\" href=\"Default.aspx?id={0}\">{1}</a>", id, dto.RenderHTML(name, option)));
            return html.ToString();
        }

        public static string RenderMultipleLinks(this EntityDTO dto, string longLink, bool isPopup)
        {
            StringBuilder html = new StringBuilder();

            string popupMarkup = string.Empty;
            if (isPopup)
            {
                popupMarkup = "target=\"_blank\"";
            }

            string[] links = longLink.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string link in links)
            {
                html.AppendFormat("<a class=\"obvious-link\" href=\"{0}\" {1}>{2}</a>{3}", link, popupMarkup, link, "<br/>");
            }
            return html.ToString();
        }

        //TODO: Revisit
        public static string RenderAsLink(this EntityDTO dto, string label, int id, RenderOption option)
        {
            StringBuilder html = new StringBuilder();
            html.Append(string.Format("<a class=\"obvious-link\" href='Default.aspx?id={0}'>{1}</a>", id, label));
            return html.ToString();
        }

        public static string RenderDetailAsLink(this EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();
            html.Append(string.Format("<a class=\"obvious-link\" onclick=\"RenderEntity('Default.aspx?id={0}&isDetail=1');\">{1}</a>", dto.ID, dto.Name));
            return html.ToString();
        }

        //TODO: Revisit
        public static string RenderAsPopupLink(this EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();
            html.Append(string.Format("<a class=\"obvious-link\" id=\"{3}\" onclick=\"getDetailAjax({0},\'{1}\');\">{2}</a>", dto.ID, PrepareStringForJavascript( dto.Name ), dto.Name, dto.ID));
            return html.ToString();
        }

        public static string RenderAsPopupLink(this EntityDTO dto, bool isCTL)
        {
            StringBuilder html = new StringBuilder();
            if (isCTL)
            {
                html.Append(string.Format("<a class=\"obvious-link\" id=\"{3}\"  onclick=\"getDetailAjax({0},\'{1}\','ctl');\">{2}</a>", dto.ID, PrepareStringForJavascript( dto.Name ) , PrepareStringForJavascript(  dto.Name ) , dto.ID));
            }
            else
            {
                html.Append(string.Format("<a class=\"obvious-link\" id=\"{3}\"  onclick=\"getDetailAjax({0},\'{1}\');\">{2}</a>", dto.ID, PrepareStringForJavascript(dto.Name), PrepareStringForJavascript( dto.Name ) , dto.ID));
            }
            return html.ToString();
        }

        public static string RenderDetailAsLink(this EntityDTO dto, string name, RenderOption option)
        {
            StringBuilder html = new StringBuilder();
            html.Append(string.Format("<a class=\"obvious-link\" onclick=\"getDetailAjax({0},'{1}');\">{2}</a>", dto.ID, dto.RenderHTML(name, option), dto.RenderHTML(name, option)));
            return html.ToString();
        }


        /// <summary>
        /// Renders into a href.
        /// </summary>
        /// <param name="id">Property name.</param>
        /// <param name="option">Render option.</param>
        /// <returns>The html markup for the link.</returns>
        public static string RenderHTMLAsAnchor(this EntityDTO dto, string id, RenderOption option)
        {
            string html = dto.RenderHTML(id, RenderOption.None);
            return string.Format("<a  class=\"obvious-link\"  href=\"{0}\" >{1}</a>", html, html);
        }

        /// <summary>
        /// Renders into a href.
        /// </summary>
        /// <param name="id">Property name.</param>
        /// <param name="option">Render option.</param>
        /// <param name="isPopup"></param>
        /// <returns>The html markup for the link.</returns>
        public static string RenderHTMLAsAnchor(this EntityDTO dto, string id, RenderOption option, bool isPopup)
        {
            string html = dto.RenderHTML(id, RenderOption.None);
            string additionalProperty = string.Empty;
            if (isPopup)
            {
                additionalProperty = "target=\"_blank\"";
            }
            return string.Format("<a class=\"obvious-link\"  href=\"{0}\" {1}>{2}</a>", html, additionalProperty, html);
        }

        public static string RenderHTMLAsAnchor(this EntityDTO dto, string label, string id, RenderOption option, bool isPopup)
        {
            string html = dto.RenderHTML(id, RenderOption.None);
            string additionalProperty = string.Empty;
            if (isPopup)
            {
                additionalProperty = "target=\"_blank\"";
            }
            return string.Format("<a class=\"obvious-link\"  href=\"{0}\" {1}>{2}</a>", html, additionalProperty, label);
        }

        private static string PrepareStringForJavascript(string content)
        {
            return content.Replace("\'", "&rsquo;");
        }
    }
}
