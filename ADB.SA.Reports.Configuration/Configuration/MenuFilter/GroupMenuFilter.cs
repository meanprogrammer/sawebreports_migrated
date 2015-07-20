using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ADB.SA.Reports.Configuration
{
    public class GroupMenuFilter : ConfigurationElement
    {
        [ConfigurationProperty("targetType", IsRequired = true)]
        public int TargetType
        {
            get
            {
                return Convert.ToInt32(this["targetType"]);
            }
            set
            {
                this["targetType"] = value;
            }
        }


        [ConfigurationProperty("id", IsRequired = false)]
        public int Id
        {
            get
            {
                return Convert.ToInt32(this["id"]);
            }
        }

        [ConfigurationProperty("propertyName", IsRequired = true)]
        public string PropertyName
        {
            get
            {
                return this["propertyName"] as string;
            }
            set
            {
                this["propertyName"] = value;
            }
        }

        [ConfigurationProperty("propertyValue", IsRequired = true)]
        public string PropertyValue
        {
            get
            {
                return this["propertyValue"] as string;
            }
            set
            {
                this["propertyValue"] = value;
            }
        }
    }
}
