using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADB.SA.Reports.Entities.DTO;
using System.Web.UI.WebControls;
using System.Text;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Web.Helpers
{
    public class MenuPageHelper
    {
        public static string BuildNavigationMenu(List<SAMenuItemDTO> menuItems)
        {
            string cacheValue = CacheHelper.GetFromCacheWithCheck<string>("tabs_menu");
            if (!string.IsNullOrEmpty(cacheValue))
            {
                return cacheValue;
            }


            StringBuilder builder = new StringBuilder();
            builder.Append("<nav role=\"navigation\" class=\"navbar navbar-default\" style=\"margin-bottom:0px !important;border-width:0 !important;\">");
            builder.Append("<div class=\"container-fluid\">");
            //builder.Append("<div class=\"collapse navbar-collapse remove-left-padding\">");
            //foreach (var item in menuItems)
            //{
                //string additionalStyles = string.Empty;
                
                //if (item.ChildItems.Count > 10)
                //{
                //    additionalStyles = "overflow-x:hidden;overflow-y:scroll;";
                //}

                //int divHeight = 699 - (menuItems.Count * 25);

                //builder.AppendFormat("<h3 title=\"Click to expand diagrams of {0}\">{1}</h3>", item.Text, item.Text);
                ////builder.Append("<dd>");
                //builder.AppendFormat("<div style=\"{0};{1}\">", additionalStyles, string.Format("max-height:{0}px;", divHeight));
                //builder.Append("<ul>");
                //foreach (var sub in item.ChildItems)
                //{
                //    //builder.AppendFormat("<li><a id=\"childmenu_{0}\" onclick=\"RenderEntity('Default.aspx?id={1}',{2});\" title=\"{3}\">{4}</a></li>", sub.Key, sub.Key, sub.Key, sub.Value, sub.Value);
                //    //Uncomment if delayed call is enabled
                //    builder.AppendFormat("<li><a id=\"childmenu_{0}\" href=\"Default.aspx?id={1}\" title=\"{2}\">{3}</a></li>", sub.Key, sub.Key, sub.Value, sub.Value);
                //}
                //builder.Append("</ul>");
                //builder.Append("</div>");
                ////builder.Append("</dd>");
                


            //}
            //builder.Append("</dl>");
            builder.Append("<ul class=\"nav navbar-nav\">");

            for (int i = 1; i <= menuItems.Count; i++)
            {
                builder.Append("<li class=\"dropdown\">");
                builder.AppendFormat("<a id=\"drop{0}\" href=\"#\" role=\"button\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">{1} &nbsp;<b class=\"caret\"></b></a>",
                    i, menuItems[i -1].Text);
                builder.AppendFormat("<ul class=\"dropdown-menu menu-suppress\" role=\"menu\" aria-labelledby=\"drop{0}\">", i);
                foreach (var child in menuItems[i - 1].ChildItems)
                {
                    builder.AppendFormat("<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"-1\" href=\"Default.aspx?id={0}\" title=\"{1}\">{2}</a></li>",
                        child.Key, child.Value, child.Value);
                }
                builder.Append("</ul>");
            }

            //for (int i = 1; i <= menuItems.Count; i++)
            //{
            //    builder.AppendFormat("<li class=\"menu_list\"><a href=\"#tabs-{0}\">{1}</a></li>", i, menuItems[i - 1].Text);
            //}
            //builder.Append("</ul>");

            //for (int j = 1; j <= menuItems.Count; j++)
            //{
            //    builder.AppendFormat("<div id=\"tabs-{0}\" class=\"list-holder\">", j);
            //    builder.Append("<ul>");
            //    foreach (var child in menuItems[j - 1].ChildItems)
            //    {
            //        builder.AppendFormat("<li><a id=\"childmenu_{0}\" href=\"Default.aspx?id={1}\" title=\"{2}\">{3}</a></li>", child.Key, child.Key, child.Value, child.Value);
            //    }
            //    builder.Append("</ul>");
            //    builder.Append("</div>");
            //}

            builder.Append("</ul>");
            builder.Append("</div></nav>");

            CacheHelper.AddToCacheWithCheck("tabs_menu", builder.ToString());

            return builder.ToString();
        }
    }
}
