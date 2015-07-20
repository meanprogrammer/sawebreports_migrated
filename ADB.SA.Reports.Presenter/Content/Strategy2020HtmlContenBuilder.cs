using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Presenter.Utils;

namespace ADB.SA.Reports.Presenter.Content
{
    public class Strategy2020HtmlContenBuilder
    {
        const string TABLETAG = "<table border=0 class='strategy-table'>";
        const string ENDTABLETAG = "</table>";

        public string BuildHtml(List<Strategy2020DTO> strategyList)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<div id='float-header'>");
            html.Append("<table border='0'>");
            html.Append("<tr><td colspan='100'><h5><strong>Strategy 2020 and its elements</strong></h5></td></tr>");
            html.Append("<tr>");
            html.Append("<td class='strategy-table-header width-100'>Agenda</td>");
            html.Append("<td><table border='0' width='100%'><tr><td class='strategy-table-header width-150'>Business Policy</td><td class='strategy-table-header width-200'>Business Rule</td><td class='strategy-table-header width-200'>Process</td><td class='strategy-table-header width-150' style='text-indent:5px;'>Application</td><td class='strategy-table-header width-200' style='text-indent:5px;'>Sub-Process</td><td class='strategy-table-header' style='text-indent:5px;'>Module</td></tr></table></td>");
            html.Append("</tr>");
            html.Append("</table>");
            html.Append("</div>");

            html.Append("<div id='ia-holder'>");
            html.Append("<table class='table table-striped main-table' width='100%' height='100%'>");

            if (strategyList.Count > 0)
            {
                int ctr0 = 0;
                foreach (var item in strategyList)
                {
                    StringBuilder policies = new StringBuilder();

                    policies.Append("<table  class='table table-striped ia-table-override bp-table' width='100%' height='100%'>");
                    int ctr1 = 0;
                    foreach (var ip in item.Policies)
                    {
                        policies.Append("<tr>");
                        policies.AppendFormat("<td class='{0} width-150'>{1}</td>", DetermineCSS(ctr1), ip.BusinessPolicyName);

                        ctr1++;
                        policies.Append("<td class='td-nested'>");
                        policies.Append("<table class='table table-striped ia-table-override br-table'  width='100%'  height='100%'>");

                        int ctr2 = 0;
                        foreach (var r in ip.Rules)
                        {
                            policies.Append("<tr>");
                            policies.AppendFormat("<td class='{0} width-200'>{1}</td>", 
                                DetermineCSS(ctr2), 
                                string.Format(GlobalStringResource.Presenter_ParagraphReferenceLinkMarkupFormat, 
                                MappingToolUrlHelper.GenerateValidSectionLinkOnly(r.ShortBusinessRuleName), 
                                r.BusinessRuleName));

                            ctr2++;
                            policies.Append("<td class='td-nested'>");

                            policies.Append("<table class='table table-striped ia-table-override proc-table' width='100%' height='100%'>");
                            int ctr3 = 0;
                            foreach (var p in r.Processes)
                            {
                                policies.Append("<tr>");
                                if (p.ProcessDiagramID > 0)
                                {
                                    policies.AppendFormat("<td class='{0} width-200'><a href=\"Default.aspx?id={1}\" target=\"_blank\">{2}</a></td>", DetermineCSS(ctr3), p.ProcessDiagramID, p.ProcessName);
                                }
                                else
                                {
                                    policies.AppendFormat("<td class='{0} width-200'>{1}</td>", DetermineCSS(ctr3), p.ProcessName);
                                }

                                ctr3++;

                                if (p.Application.Count == 1)
                                {
                                    policies.Append(
                                        "<td class='width-150'>");
                                    policies.AppendFormat("<ul class='mod-list'><li>{0}</li></ul>",
                                        p.Application.FirstOrDefault().ApplicationName);
                                    policies.Append("</td>");
                                        
                                }
                                else
                                {
                                    policies.Append("<td class='width-150'>");
                                    //policies.Append("<ul class='table-ul'>");
                                    int ctr4 = 0;
                                    policies.Append("<ul class='mod-list'>");
                                    foreach (var app in p.Application)
                                    {
                                        //policies.Append("<tr>");
                                        //policies.AppendFormat("<li class='{0} width-150 table-li'>{1}</li>", DetermineCSS(ctr4), app.ApplicationName);
                                        //policies.Append("</tr>");
                                        //policies.AppendFormat("<span class='width-150'>{0}</span><br />", app.ApplicationName);
                                        policies.AppendFormat("<li>{0}</li>", app.ApplicationName);
                                        ctr4++;
                                    }
                                    policies.Append("</ul>");
                                    policies.Append("</td>");
                                }

                                    policies.Append("<td class='td-nested'>");
                                    policies.Append("<table class='table table-striped ia-table-override sproc-table' width='100%' height='100%'>");
                                    int ctr5 = 0;
                                    foreach (var sp in p.SubProcesses)
                                    {
                                        string lastRowCss = string.Empty;
                                        if (ctr5 + 1 == (p.SubProcesses.Count)) {
                                            lastRowCss = " sproc-last-row ";
                                        }
                                        policies.Append("<tr>");
                                        if (sp.SubProcessDiagramID > 0)
                                        {
                                            policies.AppendFormat("<td class='{0} width-200 {1}'><a href=\"Default.aspx?id={2}\" target=\"_blank\">{3}</a></td>", DetermineCSS(ctr5), lastRowCss, sp.SubProcessDiagramID, sp.SubProcessName);
                                        }
                                        else
                                        {
                                            policies.AppendFormat("<td class='{0} width-200 {1}'>{2}</td>", DetermineCSS(ctr5), lastRowCss, sp.SubProcessName);
                                        }
                                        ctr5++;

                                            policies.Append("<td class='td-nested'>");
                                        /*
                                            policies.Append("<table class='table table-striped  ia-table-override mod-table' width='100%' height='100%'>");
                                            int ctr6 = 0;
                                            string modLastRowCss = string.Empty;
                                            
                                            foreach (var mod in sp.Modules)
                                            {

                                                if (ctr6 + 1 == (sp.Modules.Count))
                                                {
                                                    modLastRowCss = " mod-last-row ";
                                                }

                                                policies.Append("<tr>");
                                                policies.AppendFormat("<td class='{0} {1}'>{2}</td>", DetermineCSS(ctr6), modLastRowCss, mod.ModuleName);
                                                policies.Append("</tr>");
                                                ctr6++;
                                            }

                                            if (sp.Modules.Count == 0)
                                            {
                                                policies.Append("<tr><td>&nbsp;</td></tr>");
                                            }

                                            policies.Append(ENDTABLETAG);*/
                                            policies.Append("<ul class='mod-list'>");
                                            foreach (var mod in sp.Modules)
                                            {
                                                
                                                policies.AppendFormat("<li>{0}</li>", mod.ModuleName);
                                                
                                            }
                                            policies.Append("</ul>");
                                            policies.Append("</td>");
                                        
                                        policies.Append("</tr>");
                                    }
                                    policies.Append(ENDTABLETAG);

                                    policies.Append("</td>");


                                policies.Append("</tr>");
                            }
                            policies.Append(ENDTABLETAG);
                            policies.Append("</td>");
                            policies.Append("</tr>");
                        }
                        policies.Append(ENDTABLETAG);
                        policies.Append("</td>");
                        policies.Append("</tr>");
                    }
                    policies.Append(ENDTABLETAG);
                    string css0 = DetermineCSS(ctr0);
                    html.AppendFormat(
                        "<tr><td class='{0} width-100 agenda-column'>{1}</td><td class='td-nested'>{2}</td></tr>",
                        css0,
                        item.Agenda,
                        policies.ToString()
                        );

                }
            }
            else
            {
                html.Append("<tr><td colspan='4'>No Data Found!<td></tr>");
            }
            html.Append(ENDTABLETAG);
            html.Append("</div>");

            return html.ToString();
        }

        private string DetermineCSS(int counter)
        {
            string css = string.Empty;

            if (counter % 2 == 0)
                css = "";
            else
                css = "";

            return css;
        }
    }
}
