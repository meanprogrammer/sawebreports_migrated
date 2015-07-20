using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Presenter.Content;

namespace ADB.SA.Reports.Presenter.Converter
{
    public class Strategy2020DTOConverter
    {
        public static List<SubProcessRelation> ConvertToListOfSubProcessRelation(List<ProcessSubProcessRelation> subprocesses, Strategy2020Content content )
        {
            List<SubProcessRelation> relation = new List<SubProcessRelation>();
            foreach (ProcessSubProcessRelation subprocess in subprocesses)
            {
                SubProcessRelation relationItem = new SubProcessRelation()
                {
                    SubProcessID = subprocess.SubProcessID,
                    SubProcessName = subprocess.SubProcessName
                };


                int realId;

                if (content.SubProcessIdLookup.TryGetValue(subprocess.SubProcessID, out realId))
                {
                    relationItem.SubProcessDiagramID = realId;
                }

                relation.Add(relationItem);
            }
            return relation;
        }

        public static List<ProcessRelation> ConvertToListOfProcessRelation(
            List<Strategy2020ListItemDTO> processes, Strategy2020Content content)
        {
            List<ProcessRelation> relation = new List<ProcessRelation>();
            foreach (Strategy2020ListItemDTO process in processes)
            {
                ProcessRelation relationItem = new ProcessRelation();
                relationItem.ProcessID = process.ProcessID;
                relationItem.ProcessName = process.ProcessName;
              
                int realId;

                if (content.ProcessIdLookup.TryGetValue(process.ProcessID, out realId) && realId > 0)
                {
                    relationItem.ProcessDiagramID = realId;
                }
                
                relation.Add(relationItem);
            }
            return relation;
        }

        public static List<BusinessPolicy> ConvertToListOfBusinessPolicy(List<Strategy2020ListItemDTO> list)
        {
            List<BusinessPolicy> relation = new List<BusinessPolicy>();
            foreach (Strategy2020ListItemDTO policy in list)
            {
                BusinessPolicy relationItem = new BusinessPolicy()
                {
                    BusinessPolicyID = policy.PolicyID,
                    BusinessPolicyName = policy.PolicyName
                };
                relation.Add(relationItem);
            }
            return relation;
        }

        public static List<BusinessRule> ConvertToListOfBusinessRule(List<Strategy2020ListItemDTO> list)
        {
            EntityData eData = new EntityData();
            List<BusinessRule> rules = new List<BusinessRule>();
            foreach (var item in list)
            {
                EntityDTO dto = eData.GetOneEntity(item.RuleID);
                dto.ExtractProperties();

                BusinessRule p = new BusinessRule()
                {
                    BusinessRuleID = item.RuleID,
                    BusinessRuleName = (dto == null) ? item.RuleName : string.Format("{0} ({1})", item.RuleName, dto.RenderHTML(GlobalStringResource.Description, ADB.SA.Reports.Entities.Enums.RenderOption.None)),
                    ShortBusinessRuleName = item.RuleName
                };
                rules.Add(p);
            }
            return rules;
        }

        public static List<ModuleRelation> ConvertToListOfModuleRelation(List<SubProcessModuleRelation> modules)
        {
            List<ModuleRelation> relation = new List<ModuleRelation>();
            foreach (SubProcessModuleRelation module in modules)
            {
                ModuleRelation relationItem = new ModuleRelation()
                {
                    ModuleID = module.ModuleID,
                    ModuleName = module.ModuleName
                };
                relation.Add(relationItem);
            }
            return relation;
        }

        public static List<ApplicationRelation> ConvertToListOfApplicationRelation(List<ProcessApplicationRelation> apps)
        {
            List<ApplicationRelation> relation = new List<ApplicationRelation>();
            foreach (ProcessApplicationRelation app in apps)
            {
                ApplicationRelation relationItem = new ApplicationRelation()
                {
                    ApplicationID = app.ApplicationID,
                    ApplicationName = app.ApplicationName
                };
                relation.Add(relationItem);
            }
            return relation;
        }


        public static List<ModuleRelation> ConvertToListOfApplicationModuleRelation(List<ApplicationModuleRelation> app_mods)
        {
            List<ModuleRelation> relation = new List<ModuleRelation>();
            foreach (ApplicationModuleRelation module in app_mods)
            {
                ModuleRelation relationItem = new ModuleRelation()
                {
                    ModuleID = module.ModuleID,
                    ModuleName = module.ModuleName
                };
                relation.Add(relationItem);
            }
            return relation;
        }
    }
}
