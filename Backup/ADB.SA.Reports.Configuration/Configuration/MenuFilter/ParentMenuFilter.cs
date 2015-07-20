using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ADB.SA.Reports.Configuration
{
    public class ParentMenuFilter : ConfigurationElement
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


        [ConfigurationProperty("id", IsRequired = true)]
        public int Id
        {
            get
            {
                return Convert.ToInt32(this["id"]);
            }
            set
            {
                this["id"] = value;
            }
        }
    }
}
