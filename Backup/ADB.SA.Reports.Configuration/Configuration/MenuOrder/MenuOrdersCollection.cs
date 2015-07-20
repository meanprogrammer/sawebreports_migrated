using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ADB.SA.Reports.Configuration
{
    public class MenuOrdersCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MenuOrder();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MenuOrder)element).Id;
        }

        public void Remove(int key)
        {
            base.BaseRemove(key);
        }

        public void AddNew(MenuOrder item)
        {
            base.BaseAdd(item);
        }

        public void EditElement(MenuOrder element, string key)
        {
            base.BaseRemove(key);
            base.BaseAdd(element);
            
        }

        public void Clear()
        {
            base.BaseClear();
        }

        public MenuOrder this[string key]
        {
            get
            {
                return base.BaseGet(key) as MenuOrder;
            }
        }
    }
}
