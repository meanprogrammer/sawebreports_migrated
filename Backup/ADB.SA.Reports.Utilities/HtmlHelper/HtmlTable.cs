using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Utilities.HtmlHelper
{
    public class HtmlTable
    {
        private StringBuilder builder;
        private int counter = 0;
        private int columns = 0;
        private int[] widths;
        public int Border { get; set; }

        public HtmlTable(int _cols, int border, string cssClass)
        {
            Initialize(_cols, border, cssClass, new int[] { 100 } );
        }

        private void Initialize(int _cols, int border, string cssClass, int[] widths)
        {
            this.Border = border;
            this.columns = _cols;
            this.widths = widths;
            builder = new StringBuilder();
            builder.AppendFormat("<table {0} {1} width=\"100%\">",
                Border > 0 ? string.Format("Border={0}", Border) : string.Empty,
                !string.IsNullOrEmpty(cssClass) ? string.Format("class={0}", cssClass) : string.Empty
                );
        }

        public HtmlTable(int _cols, int border, string cssClass, int[] widths)
        {
            Initialize(_cols, border, cssClass, widths);
        }

        public void AppendRowHtml(string html)
        {
            builder.Append(html);
        }

        public void AddHeader(string text)
        {
            AddHeader(text, 1);
        }

        public void AddHeader(string text, int colspan)
        {
            if (colspan == 0) colspan = 1;
            if (AddTableTowCheck())
            {
                builder.Append("<tr>");
            }

            builder.AppendFormat(
                "<th {0} {1}>{2}</th>", 
                colspan >= 1 ? string.Format("colspan={0}", colspan) 
                : string.Empty, 
                columns == widths.Count() ? string.Format("width={0}%", widths[counter]) : string.Empty
                ,text);

            if (colspan > 1)
            {
                counter += colspan;
            }
            else
            {
                counter++;
            }

            CloseRowIfNecessary();
        }

        public void AddCell(string text, int colspan)
        {
            if (colspan == 0) colspan = 1;
            if (AddTableTowCheck())
            {
                builder.Append("<tr>");
            }

            builder.AppendFormat(
                "<td {0} {1}>{2}</td>",
                colspan >= 1 ? string.Format("colspan={0}", colspan)
                : string.Empty,
                columns == widths.Count() ? string.Format("width={0}%", widths[counter]) : string.Empty
                , text);

            if (colspan > 1)
            {
                counter += colspan;
            }
            else
            {
                counter++;
            }

            CloseRowIfNecessary();
        }

        public void AddCell(string text)
        {
            if (AddTableTowCheck())
            {
                builder.Append("<tr>");
            }
            builder.AppendFormat("<td {0}>{1}</td>", 
                columns == widths.Count() ? string.Format("width={0}%", widths[counter]) : string.Empty,
                text);
            counter++;

            CloseRowIfNecessary();
        }

        private void CloseRowIfNecessary()
        {
            if (counter == columns)
            {
                builder.Append("</tr>");
                ResetCounter();
            }
        }

        private void ResetCounter()
        {
            this.counter = 0;
        }

        private bool AddTableTowCheck()
        {
            if (counter == 0)
            {
                return true;
            }

            return (counter == columns);
        }

        public string EndHtmlTable()
        {
            builder.Append("</table>");
            return builder.ToString();
        }
    }
}
