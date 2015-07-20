using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class Strategy2020DTO
    {
        public int Order { get; set; }
        public int InitiativeTopicID { get; set; }
        public string InitiativeTopic { get; set; }

        //to be filled from the list
        public string Type { get; set; }

        public int AgendaID { get; set; }
        public string Agenda { get; set; }

        public int AgendaTypeID { get; set; }

        public List<BusinessPolicy> Policies { get; set; }

        ////public List<EntityDTO> Section { get; set; }
        //public List<EntityDTO> Processes { get; set; }
        //public KeyValuePair<EntityDTO, List<EntityDTO>> SectionProcess { get; set; }
        //public Section Section { get; set; }
    }

    public class Strategy2020DiagramRelation
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }

        public int SubProcessID { get; set; }
        public string SubProcessName { get; set; }

        public int ModuleID { get; set; }
        public string ModuleName { get; set; }

    }

    public class ProcessApplicationRelation
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }

        public int ApplicationID { get; set; }
        public string ApplicationName { get; set; }
    }

    public class ProcessSubProcessRelation
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }

        public int SubProcessID { get; set; }
        public string SubProcessName { get; set; }
    }

    public interface IModuleRelation
    {
        int ModuleID { get; set; }
        string ModuleName { get; set; }
    }

    public class SubProcessModuleRelation
    {
        public int SubProcessID { get; set; }
        public string SubProcessName { get; set; }

        public int ModuleID { get; set; }
        public string ModuleName { get; set; }

        
    }

    public class ApplicationModuleRelation
    {
        public int ApplicationID { get; set; }
        public string ApplicationName { get; set; }

        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
    }

    public class Strategy2020ListItemDTO
    {
        public int InitiativeTopicID { get; set; }
        public string InitiativeTopicName { get; set; }

        public int TypeID { get; set; }
        public string TypeName { get; set; }

        public int AgendaID { get; set; }
        public string AgendaName { get; set; }
        public int AgendaTypeID { get; set; }

        public int PolicyID { get; set; }
        public string PolicyName { get; set; }

        public int RuleID { get; set; }
        public string RuleName { get; set; }

        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
    }

    public class BusinessPolicy
    {
        public int BusinessPolicyID { get; set; }
        public string BusinessPolicyName { get; set; }
        public List<BusinessRule> Rules { get; set; }
    }

    public class BusinessRule
    {
        public int BusinessRuleID { get; set; }
        public string BusinessRuleName { get; set; }
        public string ShortBusinessRuleName { get; set; }
        public List<ProcessRelation> Processes { get; set; }
    }

    public class ProcessRelation
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public int ProcessDiagramID { get; set; }
        public List<SubProcessRelation> SubProcesses { get; set; }
        public List<ApplicationRelation> Application { get; set; }
    }

    public class SubProcessRelation
    {
        public int SubProcessID { get; set; }
        public string SubProcessName { get; set; }
        public int SubProcessDiagramID { get; set; }
        public List<ModuleRelation> Modules { get; set; }
    }

    public class ApplicationRelation
    {
        public int ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public List<ModuleRelation> Modules { get; set; }
    }

    public class ModuleRelation
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
    }

    public class ProcessApplicationSubProcessModule
    {
        public List<EntityDTO> Processes { get; set; }
        public List<EntityDTO> Applications { get; set; }
        public List<EntityDTO> SubProcesses { get; set; }
        public List<EntityDTO> Modules { get; set; }
    }

    public class Section
    {
        public Section()
        {
            SectionWithProcess = new Dictionary<EntityDTO, List<EntityDTO>>();
            ProcessWithSubprocess = new Dictionary<EntityDTO, List<EntityDTO>>();
            ProcessWithApplication = new Dictionary<EntityDTO, List<EntityDTO>>();
            SubProcessWithModule = new Dictionary<EntityDTO, List<EntityDTO>>();
        }

        public Dictionary<EntityDTO, List<EntityDTO>> SectionWithProcess { get; set; }
        public Dictionary<EntityDTO, List<EntityDTO>> ProcessWithSubprocess { get; set; }
        public Dictionary<EntityDTO, List<EntityDTO>> ProcessWithApplication { get; set; }
        public Dictionary<EntityDTO, List<EntityDTO>> SubProcessWithModule { get; set; }

        public KeyValuePair<EntityDTO, List<EntityDTO>> GetSubProcess(int id)
        {
            return this.ProcessWithSubprocess.Where(x => x.Key.ID == id).FirstOrDefault();
        }

        public KeyValuePair<EntityDTO, List<EntityDTO>> GetApplication(int id)
        {
            return this.ProcessWithApplication.Where(x => x.Key.ID == id).FirstOrDefault();
        }

    }
}
