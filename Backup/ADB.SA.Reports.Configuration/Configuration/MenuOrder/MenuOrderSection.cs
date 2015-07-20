using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ADB.SA.Reports.Configuration
{
    public class MenuOrderSection : ConfigurationSection
    {
        [ConfigurationProperty("menuOrders", IsDefaultCollection=true)]
        public MenuOrdersCollection MenuOrders
        {
            get
            {
                return this["menuOrders"] as MenuOrdersCollection;
            }
        }

        public static MenuOrderSection GetConfig()
        {
            return ConfigurationManager.GetSection("menuOrders") as MenuOrderSection;
        }
    }
}
