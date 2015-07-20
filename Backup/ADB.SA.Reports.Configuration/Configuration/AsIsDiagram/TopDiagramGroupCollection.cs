using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ADB.SA.Reports.Configuration
{
    public class TopDiagramGroupCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TopGroupElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return string.Concat(((TopGroupElement)element).Name, ((TopGroupElement)element).Color);
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
        }

        [ConfigurationProperty("cssClass", IsRequired = true)]
        public string CssClass
        {
            get
            {
                return this["cssClass"] as string;
            }
        }

        public void AddNew(TopGroupElement element)
        {
            base.BaseAdd(element);
        }

        public void Remove(string key)
        {
            base.BaseRemove(key);
        }

        public void EditElement(TopGroupElement element, string key)
        {
            var index = base.BaseIndexOf(base.BaseGet(key));
            base.BaseRemove(key);
            base.BaseAdd(index, element);
        }
    }
}
