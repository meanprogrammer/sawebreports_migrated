using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;

namespace ADB.SA.Reports.Presenter.Utils
{
    public class BreadcrumbHelper
    {
        static EntityData data;
        public static List<BreadCrumbItemDTO> BuildBreadcrumbContent(EntityDTO currentDTO)
        {
            data = new EntityData();
            BreadCrumbItemDTO home = new BreadCrumbItemDTO() { 
                Order = 0,
                Label = "Home",
                Link = "Default.aspx",
            };
            List<BreadCrumbItemDTO> breadcrumbs = new List<BreadCrumbItemDTO>();
            breadcrumbs.Add(home);
            if (currentDTO != null)
            {
                switch (currentDTO.Type)
                {
                    //Don't show any Breadcrumb on the home page
                    //because it doesn't make any sense
                    case 104:
                        return breadcrumbs;
                    case 142:
                        //if the entity has parent, find it then put it before the selected entity
                        breadcrumbs = CreateBreadcrumbWithSecondLevel(currentDTO, breadcrumbs);
                        break;
                    default:
                        //Home > current selected
                        BreadCrumbItemDTO defaultItem = new BreadCrumbItemDTO()
                        {
                            Order = 1,
                            Label = currentDTO.Name,
                            CssClass = "breadcrumb-active",
                        };
                        breadcrumbs.Add(defaultItem);
                        break;
                }
            }
            return breadcrumbs.OrderBy(c => c.Order).ToList();
        }

        private static List<BreadCrumbItemDTO> CreateBreadcrumbWithSecondLevel(EntityDTO currentDTO, List<BreadCrumbItemDTO> currentList)
        {

            EntityDTO parent = data.GetParentDiagram(currentDTO.ID);
            if (parent != null)
            {
                //builder.AppendFormat("<a href=\"Default.aspx?id={0}\">{1}</a>", parent.ID, parent.Name);
                //builder.Append(" > ");
                BreadCrumbItemDTO p = new BreadCrumbItemDTO() {
                    Order = 1,
                    Link = string.Format("Default.aspx?id={0}", parent.ID),
                    Label = parent.Name,
                };
                currentList.Add(p);
            }

            BreadCrumbItemDTO final = new BreadCrumbItemDTO()
            {
                Order = 2,
                Label = currentDTO.Name,
                CssClass = "breadcrumb-active",
            };
            currentList.Add(final);
            return currentList;
        }
    }
}
