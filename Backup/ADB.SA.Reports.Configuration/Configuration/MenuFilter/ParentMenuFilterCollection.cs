using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ADB.SA.Reports.Configuration
{
    public class ParentMenuFilterCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ParentMenuFilter();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ParentMenuFilter)element).Name;
        }

        public ParentMenuFilter this[int index]
        {
            get
            {
                return base.BaseGet(index) as ParentMenuFilter;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public ParentMenuFilter this[string key]
        {
            get
            {
                return base.BaseGet(key) as ParentMenuFilter;
            }
        }

        public void Remove(string name)
        {
            base.BaseRemove(name);
        }

        public void AddNew(ParentMenuFilter item)
        {
            base.BaseAdd(item);
        }
    }
}
