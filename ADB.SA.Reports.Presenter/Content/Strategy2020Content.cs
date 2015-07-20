using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Content
{
    public class Strategy2020Content
    {
        ICacheManager cache;
        Strategy2020Data data;
        public Strategy2020Content()
        {
            cache = CacheFactory.GetCacheManager();
            data = new Strategy2020Data();
        }

        public List<Strategy2020ListItemDTO> RawList
        {
            get
            {
                if (cache.Contains("rawlist"))
                {
                    return cache.GetData("rawlist") as List<Strategy2020ListItemDTO>;
                }
                this.RawList = data.GetStrategy2020();
                return this.RawList;
            }
            private set
            {
                CacheHelper.AddToCachePermanently(cache, "rawlist", value);
            }
        }

        public Dictionary<int, int> ProcessIdLookup
        {
            get
            {
                if (cache.Contains("ProcessIdLookup"))
                {
                    return cache.GetData("ProcessIdLookup") as Dictionary<int, int>;
                }
                this.ProcessIdLookup = data.GetProcessIDLookup();
                return this.ProcessIdLookup;
            }
            private set
            {
                CacheHelper.AddToCachePermanently(cache, "ProcessIdLookup", value);
            }
        }

        public Dictionary<int, int> SubProcessIdLookup
        {
            get
            {
                if (cache.Contains("SubProcessIdLookup"))
                {
                    return cache.GetData("SubProcessIdLookup") as Dictionary<int, int>;
                }
                this.SubProcessIdLookup = data.GetSubProcessIDLookup();
                return this.SubProcessIdLookup;
            }
            private set
            {
                CacheHelper.AddToCachePermanently(cache, "SubProcessIdLookup", value);
            }
        }

        public List<ProcessSubProcessRelation> ProcessToSubProcess
        {
            get
            {
                if (cache.Contains("processtosubprocess"))
                {
                    return cache.GetData("processtosubprocess") as List<ProcessSubProcessRelation>;
                }
                this.ProcessToSubProcess = data.GetStrategy2020ProcessSubProcessRelation();
                return this.ProcessToSubProcess;
            }

            private set
            {
                CacheHelper.AddToCachePermanently(cache, "processtosubprocess", value);
            }
        }
        public List<ProcessApplicationRelation> ProcessToApplication
        {
            get
            {
                if (cache.Contains("processtoapplication"))
                {
                    return cache.GetData("processtoapplication") as List<ProcessApplicationRelation>;
                }
                this.ProcessToApplication = data.GetStrategy2020ProcessApplicationRelation();
                return this.ProcessToApplication;
            }

            private set
            {
                CacheHelper.AddToCachePermanently(cache, "processtoapplication", value);
            }
        }
        public List<SubProcessModuleRelation> SubProcessToModule
        {
            get
            {
                if (cache.Contains("subprocesstomodule"))
                {
                    return cache.GetData("subprocesstomodule") as List<SubProcessModuleRelation>;
                }
                this.SubProcessToModule = data.GetStrategy2020SubProcessModuleRelation();
                return this.SubProcessToModule;
            }

            private set
            {
                CacheHelper.AddToCachePermanently(cache, "subprocesstomodule", value);
            }
        }

        public List<ApplicationModuleRelation> ApplicationToModule
        {
            get
            {
                if (cache.Contains("applicationtomodule"))
                {
                    return cache.GetData("applicationtomodule") as List<ApplicationModuleRelation>;
                }
                this.ApplicationToModule = data.GetStrategy2020ApplicationModuleRelation();
                return this.ApplicationToModule;
            }

            private set
            {
                CacheHelper.AddToCachePermanently(cache, "applicationtomodule", value);
            }
        }


        private List<Strategy2020DTO> strategyList;
        public List<Strategy2020DTO> StrategyList
        {
            get
            {
                if (cache.Contains("strategylist"))
                {
                    return cache.GetData("strategylist") as List<Strategy2020DTO>;
                }
                return this.strategyList;
            }

            set
            {
                CacheHelper.AddToCachePermanently(cache, "strategylist", value);
                this.strategyList = value;
            }
        }
    }
}
