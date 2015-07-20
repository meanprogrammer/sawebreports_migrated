using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Utilities.HtmlHelper;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Entities.Utils;

namespace ADB.SA.Reports.Presenter.Content
{
    public abstract class DetailStrategyBase
    {
        public abstract object BuildDetail(EntityDTO dto);

        protected virtual string BuildType(int type, int colspan)
        {
            StringBuilder html = new StringBuilder();
            html.Append(Resources.TableRowStartTag);
            html.AppendFormat(Resources.TableCellLabel, string.Empty, GlobalStringResource.Type);
            html.AppendFormat(Resources.TableCellWithColSpan, string.Empty,colspan, TypesReader.GetDefinitonName(type));
            html.Append(Resources.TableRowEndTag);
            return html.ToString();
        }


        protected virtual string BuildDescription(EntityDTO dto)
        {
            return dto.RenderHTML(GlobalStringResource.Description, RenderOption.Break);
        }

        protected virtual string BuildReferencedDocuments(EntityDTO dto)
        {
            string link = dto.RenderHTML(GlobalStringResource.ReferenceDocuments, RenderOption.NewLine);
            return dto.RenderMultipleLinks(link, true);
        }

        protected virtual string BuildReturnLink(int id)
        {
            StringBuilder html = new StringBuilder();
            html.Append(Resources.TableRowStartTag);
            html.AppendFormat("<td colspan=\"2\"><a onclick=\"GoToParentUrl({0});\">Go to parent</a></td>", id);
            html.Append(Resources.TableRowEndTag);
            return html.ToString();
        }

        protected virtual string BuildCloseLink(int colspan)
        {
            StringBuilder html = new StringBuilder();
            html.Append(Resources.TableRowStartTag);
            html.AppendFormat("<td colspan=\"{0}\" style=\"text-align:right;\"><a onclick=\"javascript:window.close();\">[ Close ]</a></td>", colspan);
            html.Append(Resources.TableRowEndTag);
            return html.ToString();
        }

            
    

    }
}
