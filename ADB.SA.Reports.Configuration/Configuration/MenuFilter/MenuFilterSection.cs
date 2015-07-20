using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ADB.SA.Reports.Configuration
{
    public class MenuFilterSection : ConfigurationSection
    {
        [ConfigurationProperty("parentMenuFilters")]
        public ParentMenuFilterCollection ParentMenuFilters
        {
            get
            {
                return this["parentMenuFilters"] as ParentMenuFilterCollection;
            }
        }

        [ConfigurationProperty("groupMenuFilters")]
        public GroupMenuFilterCollection GroupMenuFilters
        {
            get
            {
                return this["groupMenuFilters"] as GroupMenuFilterCollection;
            }
        }

        public static MenuFilterSection GetConfig()
        {
            return ConfigurationManager.GetSection("menufilters") as MenuFilterSection;
        }

        public List<string> GetItemsToBeRemove()
        {
            ParentMenuFilterCollection filters = this["parentMenuFilters"] as ParentMenuFilterCollection;
            List<string> ids = new List<string>();
            if (filters != null)
            {
                foreach (ParentMenuFilter filter in filters)
                {
                    ids.Add(filter.Id.ToString());
                }
            }
            return ids;
        }

        public List<GroupMenuFilter> GetGroupMenuFilters()
        {
            GroupMenuFilterCollection groupFilters = this["groupMenuFilters"] as GroupMenuFilterCollection;
            List<GroupMenuFilter> filters = new List<GroupMenuFilter>();
            if (groupFilters != null)
            {
                foreach (GroupMenuFilter item in groupFilters)
                {
                    filters.Add(item);
                }
            }
            return filters;
        }
    }
}
