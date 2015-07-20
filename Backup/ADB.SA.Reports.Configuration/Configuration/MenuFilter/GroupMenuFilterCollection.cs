using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ADB.SA.Reports.Configuration
{
    public class GroupMenuFilterCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupMenuFilter();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return string.Concat(((GroupMenuFilter)element).PropertyName, ((GroupMenuFilter)element).TargetType);
        }

        public void Remove(string key)
        {
            base.BaseRemove(key);
        }

        public void AddNew(GroupMenuFilter item)
        {
            base.BaseAdd(item);
        }

        public void EditElement(GroupMenuFilter element, string key)
        {
            base.BaseRemove(key);
            base.BaseAdd(element);
        }

        public GroupMenuFilter this[string key]
        {
            get {
                return base.BaseGet(key) as GroupMenuFilter;
            }
        }
    }
}
