using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Presenter.Content
{

    public class Strategy2020Filter
    {
        public int TypeFilter { get; set; }
        public int AgendaFilter { get; set; }
        public int PolicyFilter { get; set; }
        public int RuleFilter { get; set; }
        public int ProcessFilter { get; set; }
        public int ApplicationFilter { get; set; }
        public int SubProcessFilter { get; set; }
        public int ModuleFilter { get; set; }

        public string TypeFilterText { get; set; }
        public string AgendaFilterText { get; set; }
        public string PolicyFilterText { get; set; }
        public string RuleFilterText { get; set; }
        public string ProcessFilterText { get; set; }
        public string ApplicationFilterText { get; set; }
        public string SubProcessFilterText { get; set; }
        public string ModuleFilterText { get; set; }

        Strategy2020Content content;
        Strategy2020ContentBuilder builder;
        public Strategy2020Filter()
        {
            InitializeRelatedClasses();
        }

        private void InitializeRelatedClasses()
        {
            content = new Strategy2020Content();
            builder = new Strategy2020ContentBuilder();
        }

        public Strategy2020Filter(string agenda, 
            string policy, string rule, string process,
            string subprocess, string application, string module)
        {
            InitializeRelatedClasses();
            //this.TypeFilter = int.Parse(type);
            this.AgendaFilter = int.Parse(agenda);
            this.PolicyFilter = int.Parse(policy);
            this.RuleFilter = int.Parse(rule);
            this.ProcessFilter = int.Parse(process);
            this.SubProcessFilter = int.Parse(subprocess);
            this.ApplicationFilter = int.Parse(application);
            this.ModuleFilter = int.Parse(module);
        }

        public Strategy2020Filter(
            string agenda, string agendatext,
            string policy, string policytext,
            string rule, string ruletext,
            string process, string processtext,
            string subprocess, string subprocesstext,
            string application, string applicationtext,
            string module, string moduletext)
        {
            InitializeRelatedClasses();
            //this.TypeFilter = int.Parse(type);
            this.AgendaFilter = int.Parse(agenda);
            this.PolicyFilter = int.Parse(policy);
            this.RuleFilter = int.Parse(rule);
            this.ProcessFilter = int.Parse(process);
            this.SubProcessFilter = int.Parse(subprocess);
            this.ApplicationFilter = int.Parse(application);
            this.ModuleFilter = int.Parse(module);


            //this.TypeFilterText = typetext;
            this.AgendaFilterText = agendatext;
            this.PolicyFilterText = policytext;
            this.RuleFilterText = ruletext;
            this.ProcessFilterText = processtext;
            this.SubProcessFilterText = subprocesstext;
            this.ApplicationFilterText = applicationtext;
            this.ModuleFilterText = moduletext;
        }

        public List<Strategy2020DTO> ApplyDefaultFilter(List<Strategy2020DTO> list)
        {
            return list.Where(c => c.AgendaTypeID == 460).ToList();
        }

        public List<Strategy2020DTO> ApplyTypeFilter()
        {
            List<Strategy2020ListItemDTO> rawList =
                content.RawList.Where(c => c.AgendaTypeID == this.TypeFilter).ToList();

            return builder.TransformRawStrategyList(rawList,
                content.ProcessToSubProcess,
                content.ProcessToApplication,
                content.SubProcessToModule,
                content.ApplicationToModule);
        }

        public List<Strategy2020DTO> ApplyPolicyFilter()
        {
            List<Strategy2020ListItemDTO> rawList =
                content.RawList.Where(c => c.AgendaTypeID == this.TypeFilter).ToList();

            return builder.TransformRawStrategyList(rawList,
                content.ProcessToSubProcess,
                content.ProcessToApplication,
                content.SubProcessToModule,
                content.ApplicationToModule);
        }

        public List<Strategy2020DTO> ApplyFilters()
        {
            List<Strategy2020DTO> filtered = new List<Strategy2020DTO>(); // = content.StrategyList;
            List<Strategy2020ListItemDTO> rawList = content.RawList;
            List<ProcessSubProcessRelation> procTosub = content.ProcessToSubProcess;
            List<ProcessApplicationRelation> procToApp = content.ProcessToApplication;
            List<SubProcessModuleRelation> spToMods = content.SubProcessToModule;
            List<ApplicationModuleRelation> appToMods = content.ApplicationToModule;

            if (this.ModuleFilter != 0)
            { 
                spToMods = (from mod in spToMods
                           where mod.ModuleID == this.ModuleFilter
                                select mod).Distinct().ToList();

                procTosub = (from mod in spToMods 
                             join proc in procTosub on
                            mod.SubProcessID equals proc.SubProcessID 
                                 select proc).ToList();

                rawList = (from left in procTosub
                           join right in rawList
                               on left.ProcessID equals right.ProcessID
                           select right).ToList();
            }

            if (this.ApplicationFilter != 0)
            {
                procToApp = (from p in procToApp
                             where p.ApplicationID == this.ApplicationFilter
                             select p).Distinct().ToList();
                rawList = (from left in rawList
                           join right in procToApp
                               on left.ProcessID equals right.ProcessID
                           where right.ApplicationID == this.ApplicationFilter
                           select left).ToList();
            }

            if (this.SubProcessFilter != 0)
            {
                procTosub = (from p in procTosub
                            where p.SubProcessID == this.SubProcessFilter
                                 select p).Distinct().ToList();
                rawList = (from left in rawList 
                          join right in procTosub
                              on left.ProcessID equals right.ProcessID
                              where right.SubProcessID == this.SubProcessFilter
                               select left).ToList();
            }

            if (this.ProcessFilter != 0)
            {
                rawList =
                    rawList.Where(c => c.ProcessID == this.ProcessFilter).ToList();
            }

            if (this.RuleFilter != 0)
            {
                rawList =
                    rawList.Where(c => c.RuleID == this.RuleFilter).ToList();
            }

            if (this.PolicyFilter != 0)
            {
                rawList =
                    rawList.Where(c => c.PolicyID == this.PolicyFilter).ToList();
            }

            if (this.TypeFilter != 0)
            {
                rawList =
                    rawList.Where(c => c.AgendaTypeID == this.TypeFilter).ToList();
            }

            if (this.AgendaFilter != 0)
            {
                rawList =
                    rawList.Where(c => c.AgendaID == this.AgendaFilter).ToList();
            }

                filtered = builder.TransformRawStrategyList(rawList,
                    procTosub,
                    procToApp,
                    spToMods, appToMods);
            return filtered;
        }
    }

    public class Strategy2020FilterData
    {
        public List<DropdownItem> GetValidTypeFilter(List<Strategy2020DTO> list)
        {
            var filters = (from f in list
                           select new DropdownItem()
                           {
                               Value = f.AgendaTypeID.ToString(),
                               Text = f.Type
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c=>c.Value != "0").ToList();
        }

        public List<DropdownItem> GetValidAgendaFilter(List<Strategy2020DTO> list)
        {
            var filters = (from f in list
                           select new DropdownItem()
                           {
                               Value = f.AgendaID.ToString(),
                               Text = f.Agenda
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0").OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidPolicyFilterByAgenda(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           where f.AgendaID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = a.BusinessPolicyID.ToString(),
                               Text = a.BusinessPolicyName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                //.Where(x => x.Value == filterValue)
                .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidPolicyFilter(List<Strategy2020DTO> list)
        {
            var filters = (from f in list
                           from a in f.Policies
                           select new DropdownItem()
                           {
                               Value = a.BusinessPolicyID.ToString(),
                               Text = a.BusinessPolicyName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c=>c.Value != "0").OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidRuleFilter(List<Strategy2020DTO> list)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           select new DropdownItem()
                           {
                               Value = b.BusinessRuleID.ToString(),
                               Text = b.BusinessRuleName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidProcessFilter(List<Strategy2020DTO> list)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           select new DropdownItem()
                           {
                               Value = p.ProcessID.ToString(),
                               Text = p.ProcessName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidSubProcessFilter(List<Strategy2020DTO> list)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from sp in p.SubProcesses
                           select new DropdownItem()
                           {
                               Value = sp.SubProcessID.ToString(),
                               Text = sp.SubProcessName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidApplicationFilter(List<Strategy2020DTO> list)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from app in p.Application
                           select new DropdownItem()
                           {
                               Value = app.ApplicationID.ToString(),
                               Text = app.ApplicationName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidModuleFilter(List<Strategy2020DTO> list)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from sp in p.SubProcesses
                           from mod in sp.Modules
                           select new DropdownItem()
                           {
                               Value = mod.ModuleID.ToString(),
                               Text = mod.ModuleName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidBusinessRuleFilterByPolicy(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           where a.BusinessPolicyID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = b.BusinessRuleID.ToString(),
                               Text = b.BusinessRuleName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidProcessFilterByBusinessRule(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           where b.BusinessRuleID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = p.ProcessID.ToString(),
                               Text = p.ProcessName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidSubProcessFilterByProcess(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from sp in p.SubProcesses
                           where p.ProcessID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = sp.SubProcessID.ToString(),
                               Text = sp.SubProcessName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                        .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidModuleFilterBySubProcess(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from sp in p.SubProcesses
                           from mod in sp.Modules
                           where sp.SubProcessID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = mod.ModuleID.ToString(),
                               Text = mod.ModuleName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
            .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidApplicationFilterByProcess(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from app in p.Application
                           where p.ProcessID == int.Parse(filterValue) 
                           select new DropdownItem()
                           {
                               Value = app.ApplicationID.ToString(),
                               Text = app.ApplicationName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                    .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidModuleFilterByApplication(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from app in p.Application
                           from mod in app.Modules
                           where app.ApplicationID == int.Parse(filterValue) 
                           select new DropdownItem()
                           {
                               Value = mod.ModuleID.ToString(),
                               Text = mod.ModuleName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                    .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidModuleFilterByApplication(
            List<Strategy2020DTO> list, string filterValue,
            string secondFilter)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from app in p.Application
                           from mod in app.Modules
                           where app.ApplicationID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = mod.ModuleID.ToString(),
                               Text = mod.ModuleName
                           }).Distinct(new DropdownItemComparer()).ToList();

            var filter2 = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from sp in p.SubProcesses
                           from m in sp.Modules
                           where sp.SubProcessID == int.Parse(secondFilter)
                           select new DropdownItem()
                          {
                              Value = m.ModuleID.ToString(),
                              Text = m.ModuleName
                          }).Distinct(new DropdownItemComparer()).ToList();

            List<DropdownItem> finalList = new List<DropdownItem>();

            foreach (DropdownItem item in filter2)
            {
                if (filters.Contains(item, new DropdownItemComparer()))
                {
                    finalList.Add(item);
                }
            }

            return finalList.Where(c => c.Value != "0")
                    .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidSubProcessFilterByModule(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from sp in p.SubProcesses
                           from app in p.Application
                           from mod in app.Modules
                           where mod.ModuleID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = sp.SubProcessID.ToString(),
                               Text = sp.SubProcessName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                    .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidApplicationFilterByModule(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from sp in p.SubProcesses
                           from app in p.Application
                           from mod in app.Modules
                           where mod.ModuleID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = app.ApplicationID.ToString(),
                               Text = app.ApplicationName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                    .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidProcessFilterBySubProcess(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from sp in p.SubProcesses
                           where sp.SubProcessID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = p.ProcessID.ToString(),
                               Text = p.ProcessName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                        .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidProcessFilterByApplication(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from app in p.Application
                           where app.ApplicationID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = p.ProcessID.ToString(),
                               Text = p.ProcessName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                    .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidRuleFilterByProcess(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           where p.ProcessID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = b.BusinessRuleID.ToString(),
                               Text = b.BusinessRuleName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                    .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidPolicyFilterByRule(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           from r in a.Rules
                           where r.BusinessRuleID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = a.BusinessPolicyID.ToString(),
                               Text = a.BusinessPolicyName
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidSubprocessByProcesses(List<Strategy2020DTO> list,
            List<int> processesID, int moduleId)
        {
            //TODO: Revisit this for a much more elegant solution
            var filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from sp in p.SubProcesses
                           from mod in sp.Modules
                           where processesID.Contains(p.ProcessID)
                           select new DropdownItem()
                           {
                               Value = sp.SubProcessID.ToString(),
                               Text = sp.SubProcessName
                           }).Distinct(new DropdownItemComparer()).ToList();



            if (moduleId > 0)
            {
                filters = (from f in list
                           from a in f.Policies
                           from b in a.Rules
                           from p in b.Processes
                           from sp in p.SubProcesses
                           from mod in sp.Modules
                           where processesID.Contains(p.ProcessID)
                           && mod.ModuleID == moduleId
                           select new DropdownItem()
                           {
                               Value = sp.SubProcessID.ToString(),
                               Text = sp.SubProcessName
                           }).Distinct(new DropdownItemComparer()).ToList();
            }
       
            return filters.Where(c => c.Value != "0")
                    .OrderBy(c => c.Text).ToList();
        }

        public List<DropdownItem> GetValidAgendaFilterByPolicy(List<Strategy2020DTO> list, string filterValue)
        {
            var filters = (from f in list
                           from a in f.Policies
                           where a.BusinessPolicyID == int.Parse(filterValue)
                           select new DropdownItem()
                           {
                               Value = f.AgendaID.ToString(),
                               Text = f.Agenda
                           }).Distinct(new DropdownItemComparer()).ToList();

            return filters.Where(c => c.Value != "0")
                .OrderBy(c => c.Text).ToList();
        }
    }

    public class Strategy2020ColumnFilter
    {
        public bool ShowSubProcess { get; set; }
        public bool ShowModule { get; set; }
    }
}
