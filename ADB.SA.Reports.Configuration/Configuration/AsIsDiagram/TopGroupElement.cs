using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ADB.SA.Reports.Configuration
{
    public class TopGroupElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("color", IsRequired = true)]
        public string Color
        {
            get
            {
                return this["color"] as string;
            }
            set
            {
                this["color"] = value;
            }
        }
    }
}
