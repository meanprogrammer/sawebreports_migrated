using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ADB.SA.Reports.Configuration
{
    public class MenuOrder : ConfigurationElement
    {
        [ConfigurationProperty("text", IsRequired = true)]
        public string Text
        {
            get
            {
                return this["text"].ToString();
            }
            set
            {
                this["text"] = value;
            }
        }


        [ConfigurationProperty("id", IsRequired = false)]
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

        [ConfigurationProperty("order", IsRequired = true)]
        public int Order
        {
            get
            {
                return Convert.ToInt32(this["order"]);
            }
            set
            {
                this["order"] = value;
            }
        }
    }
}
