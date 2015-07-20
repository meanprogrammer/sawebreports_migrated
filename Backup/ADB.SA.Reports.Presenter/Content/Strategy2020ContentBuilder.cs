using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Presenter.Converter;
using System.IO;

namespace ADB.SA.Reports.Presenter.Content
{
    public class Strategy2020ContentBuilder
    {
        ICacheManager cache;
        Strategy2020Data data;
        Strategy2020Content content;

        public Strategy2020ContentBuilder()
        {
            cache = CacheFactory.GetCacheManager();
            data  = new Strategy2020Data();
            InitializeTypes();
            content = new Strategy2020Content();
        }

        public List<KeyValuePair<int, TypeInfo>> TypeNames { get; private set; }

        public List<Strategy2020DTO> BuildStrategy2020List()
        {
            content.StrategyList = TransformRawStrategyList(
                            content.RawList,
                            content.ProcessToSubProcess,
                            content.ProcessToApplication,
                            content.SubProcessToModule,
                            content.ApplicationToModule);

            return content.StrategyList;
        }

        private List<Strategy2020ListItemDTO> FillUpType(List<Strategy2020ListItemDTO> initialList)
        {
            foreach (Strategy2020ListItemDTO item in initialList)
            {
                if (item.AgendaTypeID != 0)
                {
                    item.TypeName = TypeNames.Where(c => c.Key == item.AgendaTypeID).FirstOrDefault().Value.Name;
                }
                else
                {
                    item.TypeName = string.Empty;
                }
            }
            return initialList;
        }

        public List<Strategy2020DTO> TransformRawStrategyList
            (List<Strategy2020ListItemDTO> initialList,
            List<ProcessSubProcessRelation> processSubProcessRelation,
            List<ProcessApplicationRelation> processApplicationRelation,
            List<SubProcessModuleRelation> subProcModuleRelation,
            List<ApplicationModuleRelation> appModulesRelation)
        {
            FillUpType(initialList);

            List<Strategy2020DTO> newList = new List<Strategy2020DTO>();
            Strategy2020Data data = new Strategy2020Data();
            Strategy2020Content lookup = new Strategy2020Content();

            //NOTE: The entry point is still the initiative topic,
            //but they are not shown in the result data
            List<EntityDTO> entryPoint = data.GetAllInitiativeTopic();
            //DumpInitialList(initialList.OrderBy(c=>c.ProcessID).ToList());
            //DumpProcessToSubprocess(processSubProcessRelation.OrderBy(c=>c.ProcessID).ToList());
            //return null;
            entryPoint.Add(new EntityDTO());
            foreach (EntityDTO e in entryPoint)
            {
                var tempTable = (from il in initialList
                                 where il.InitiativeTopicID == e.ID
                                 select il).Distinct(new AgendaComparer()).ToList();
                foreach (Strategy2020ListItemDTO sub in tempTable)
                {
                    Strategy2020DTO s = new Strategy2020DTO();
                    s.InitiativeTopic = sub.InitiativeTopicName;
                    s.InitiativeTopicID = sub.InitiativeTopicID;
                    s.AgendaTypeID = sub.AgendaTypeID;
                    if (sub.AgendaTypeID != 0)
                    {
                        s.Type = TypeNames.Where(c => c.Key == sub.AgendaTypeID).FirstOrDefault().Value.Name;
                        s.Order = TypeNames.Where(c => c.Key == sub.AgendaTypeID).FirstOrDefault().Value.Order;
                    }
                    else
                    {
                        s.Order = 9999;
                    }
                    
                    s.Agenda = sub.AgendaName;
                    s.AgendaID = sub.AgendaID;


                    var policies = (from pp in initialList
                                    where pp.AgendaID == s.AgendaID
                                    select pp).Distinct(new PolicyComparer()).ToList();

                    List<BusinessPolicy> b_policies =
                        Strategy2020DTOConverter.ConvertToListOfBusinessPolicy(policies);

                    foreach (BusinessPolicy b_policy in b_policies)
                    {
                        var rules = (from rr in initialList
                                     where rr.PolicyID == b_policy.BusinessPolicyID
                                     select rr).Distinct(new RuleComparer()).ToList();

                        List<BusinessRule> b_rules =
                            Strategy2020DTOConverter.ConvertToListOfBusinessRule(rules);



                        foreach (BusinessRule rule in b_rules)
                        {
                            var processes = (from proc in initialList
                                             where proc.RuleID == rule.BusinessRuleID
                                             select proc).Distinct(new ProcessComparer()).ToList();

                            List<ProcessRelation> p_list =
                                Strategy2020DTOConverter.ConvertToListOfProcessRelation(processes, lookup);

                            foreach (ProcessRelation p_rel in p_list)
                            {
                                var subprocesses = (from subproc in processSubProcessRelation
                                                    where subproc.ProcessID == p_rel.ProcessID
                                                    orderby subproc.SubProcessName 
                                                    select subproc).Distinct(new SubProcessComparer()).ToList();

                                List<SubProcessRelation> sp_list =
                                    Strategy2020DTOConverter.ConvertToListOfSubProcessRelation(subprocesses, lookup);

                                foreach (SubProcessRelation sp_rel in sp_list)
                                {
                                    var modules = (from mods in subProcModuleRelation
                                                   where mods.SubProcessID == sp_rel.SubProcessID
                                                   select mods).ToList(); //new ModulesComparer()).ToList();

                                    List<ModuleRelation> mod_list =
                                        Strategy2020DTOConverter.ConvertToListOfModuleRelation(modules);
                                    sp_rel.Modules = mod_list;
                                }

                                p_rel.SubProcesses = sp_list;

                                //here goes the application relationship

                                var apps = (from app in processApplicationRelation
                                            where app.ProcessID == p_rel.ProcessID
                                            select app).ToList();

                                List<ApplicationRelation> apprelation =
                                    Strategy2020DTOConverter.ConvertToListOfApplicationRelation(apps);

                                foreach (ApplicationRelation app_rel in apprelation)
                                {
                                    var app_mods = (from mods in appModulesRelation
                                                          where mods.ApplicationID == app_rel.ApplicationID
                                                          select mods).ToList();


                                    List<ModuleRelation> app_mod_list =
                                        Strategy2020DTOConverter.ConvertToListOfApplicationModuleRelation(app_mods);

                                    app_rel.Modules = app_mod_list;
                                }

                                p_rel.Application = apprelation;
                            }

                            rule.Processes = p_list;
                        }

                        b_policy.Rules = b_rules;
                    }

                    s.Policies = b_policies;

                    newList.Add(s);
                }
            }

            return newList.OrderBy(c => c.Order).ToList();
        }

        private void DumpProcessToSubprocess(List<ProcessSubProcessRelation> processSubProcessRelation)
        {
            StringBuilder b = new StringBuilder();
            b.Append("<table border=1 cellspacing=2 cellpadding=4>");
            foreach (ProcessSubProcessRelation item in processSubProcessRelation)
            {
                b.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
                        item.ProcessID,
                        item.ProcessName,
                        item.SubProcessID,
                        item.SubProcessName);
            }
            b.Append("</table>");
            File.WriteAllText(@"C:\proctosub.htm", b.ToString());
        }

        private void DumpInitialList(List<Strategy2020ListItemDTO> initialList)
        {
            StringBuilder b = new StringBuilder();
            b.Append("<table border=1 cellspacing=2 cellpadding=4>");
            foreach (Strategy2020ListItemDTO item in initialList)
            {
                b.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>" +
                        "<td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td>" +
                        "<td>{9}</td><td>{10}</td><td>{11}</td><td>{12}</td></tr>",
                        item.TypeID,
                        item.InitiativeTopicID,
                        item.InitiativeTopicName,
                        item.AgendaTypeID,
                        item.TypeName,
                        item.AgendaID,
                        item.AgendaName,
                        item.PolicyID,
                        item.PolicyName,
                        item.RuleID,
                        item.RuleName,
                        item.ProcessID,
                        item.ProcessName);
            }
            b.Append("</table>");
            File.WriteAllText(@"C:\dump.htm", b.ToString());

        }

        private void InitializeTypes()
        {
            TypeNames = new List<KeyValuePair<int, TypeInfo>>();
            TypeNames.Add(new KeyValuePair<int, TypeInfo>(462, new TypeInfo(0, "Challenges Ahead")));
            TypeNames.Add(new KeyValuePair<int, TypeInfo>(460, new TypeInfo(1, "Strategic Agenda")));
            TypeNames.Add(new KeyValuePair<int, TypeInfo>(451, new TypeInfo(2, "Drivers of Change")));
            TypeNames.Add(new KeyValuePair<int, TypeInfo>(464, new TypeInfo(3, "Developing Partner Countries")));
            TypeNames.Add(new KeyValuePair<int, TypeInfo>(452, new TypeInfo(4, "Core Areas of Operation")));
            TypeNames.Add(new KeyValuePair<int, TypeInfo>(457, new TypeInfo(5, "Other Areas of Operation")));
            TypeNames.Add(new KeyValuePair<int, TypeInfo>(461, new TypeInfo(6, "Corporate Values")));
            TypeNames.Add(new KeyValuePair<int, TypeInfo>(458, new TypeInfo(7, "Operational Goals")));
            TypeNames.Add(new KeyValuePair<int, TypeInfo>(459, new TypeInfo(8, "Institutional Goals")));
            TypeNames.Add(new KeyValuePair<int, TypeInfo>(455, new TypeInfo(9, "Results Framework")));
        }
    }

    public class TypeInfo
    {
        public TypeInfo(int order, string name)
        {
            this.Order = order;
            this.Name = name;
        }

        public int Order { get; set; }
        public string Name { get; set; }
    }

}
