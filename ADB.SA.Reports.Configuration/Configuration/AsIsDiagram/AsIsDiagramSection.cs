using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ADB.SA.Reports.Configuration
{
    public class AsIsDiagramSection : ConfigurationSection
    {
        [ConfigurationProperty("leftGroup", IsDefaultCollection=true)]
        public TopDiagramGroupCollection LeftGroup
        {
            get
            {
                return this["leftGroup"] as TopDiagramGroupCollection;
            }
        }

        [ConfigurationProperty("rightGroup", IsDefaultCollection = true)]
        public TopDiagramGroupCollection RightGroup
        {
            get
            {
                return this["rightGroup"] as TopDiagramGroupCollection;
            }
        }

        public static AsIsDiagramSection GetConfig()
        {
            return ConfigurationManager.GetSection("initDiagrams") as AsIsDiagramSection;
        }

        //[ConfigurationProperty("name", IsRequired = false)]
        //public string Name
        //{
        //    get
        //    {
        //        return this["name"] as string;
        //    }
        //}
    }
}
