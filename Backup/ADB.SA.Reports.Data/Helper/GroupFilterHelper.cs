using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Configuration;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Data.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupFilterHelper
    {
        public static bool IsValidForShow(int type, int id)
        {
            bool result = false;
            EntityData data = new EntityData();
            MenuFilterSection menuFilter = MenuFilterSection.GetConfig();
            List<GroupMenuFilter> groupFilters = menuFilter.GetGroupMenuFilters();
            if (groupFilters.FirstOrDefault(y => y.TargetType == type) != null)
            {
                GroupMenuFilter filter = groupFilters.FirstOrDefault(y => y.TargetType == type);

                EntityDTO dto = data.GetOneEntity(id);

                if (dto != null)
                {
                    dto.ExtractProperties();
                    string category = dto.RenderHTML(filter.PropertyName, RenderOption.None);
                    if (category == filter.PropertyValue)
                    {
                        result = true;
                    }
                }
            }
            else
            {
                result = true;
            }
            return result;
        }

        //public static bool IsValidForShowOnHomepage(int type, int id)
        //{
        //    bool result = false;

        //    if (AppSettingsReader.GetValue("HOMEPAGE") == GlobalStringResource.CTL)
        //    {
        //        return true;
        //    }

        //    EntityData data = new EntityData();
        //    MenuFilterSection menuFilter = MenuFilterSection.GetConfig();
        //    List<GroupMenuFilter> groupFilters = menuFilter.GetGroupMenuFilters();
        //    if (groupFilters.FirstOrDefault(y => y.TargetType == type) != null)
        //    {
        //        GroupMenuFilter filter = groupFilters.FirstOrDefault(y => y.TargetType == type);
        //        EntityDTO dto = data.GetOneEntity(id);
        //        if (dto != null)
        //        {
        //            dto.ExtractProperties();
        //            string category = dto.RenderHTML(filter.PropertyName, RenderOption.None);
        //            if (category == filter.PropertyValue)
        //            {
        //                result = true;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        result = true;
        //    }
        //    return result;
        //}
    }
}
