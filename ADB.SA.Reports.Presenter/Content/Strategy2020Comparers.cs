using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Presenter.Content
{

    public class AgendaComparer : IEqualityComparer<Strategy2020ListItemDTO>
    {

        #region IEqualityComparer<Strategy2020ListItemDTO> Members

        public bool Equals(Strategy2020ListItemDTO x, Strategy2020ListItemDTO y)
        {
            return x.AgendaID == y.AgendaID;
        }

        public int GetHashCode(Strategy2020ListItemDTO obj)
        {
            return 0;
        }

        #endregion
    }

    public class PolicyComparer : IEqualityComparer<Strategy2020ListItemDTO>
    {

        #region IEqualityComparer<Strategy2020ListItemDTO> Members

        public bool Equals(Strategy2020ListItemDTO x, Strategy2020ListItemDTO y)
        {
            return x.PolicyID == y.PolicyID;
        }

        public int GetHashCode(Strategy2020ListItemDTO obj)
        {
            return 0;
        }

        #endregion
    }

    public class RuleComparer : IEqualityComparer<Strategy2020ListItemDTO>
    {

        #region IEqualityComparer<Strategy2020ListItemDTO> Members

        public bool Equals(Strategy2020ListItemDTO x, Strategy2020ListItemDTO y)
        {
            return x.RuleID == y.RuleID;
        }

        public int GetHashCode(Strategy2020ListItemDTO obj)
        {
            return 0;
        }

        #endregion
    }

    public class ProcessComparer : IEqualityComparer<Strategy2020ListItemDTO>
    {

        #region IEqualityComparer<Strategy2020ListItemDTO> Members

        public bool Equals(Strategy2020ListItemDTO x, Strategy2020ListItemDTO y)
        {
            return x.ProcessID == y.ProcessID;
        }

        public int GetHashCode(Strategy2020ListItemDTO obj)
        {
            return 0;
        }

        #endregion
    }

    public class SubProcessComparer : IEqualityComparer<ProcessSubProcessRelation>
    {
        public bool Equals(ProcessSubProcessRelation x, ProcessSubProcessRelation y)
        {
            return x.SubProcessID == y.SubProcessID;
        }

        public int GetHashCode(ProcessSubProcessRelation obj)
        {
            return 0;
        }
    }

    public class ModulesComparer : IEqualityComparer<SubProcessModuleRelation>
    {
        public bool Equals(SubProcessModuleRelation x, SubProcessModuleRelation y)
        {
            return x.ModuleID == y.ModuleID;
        }

        public int GetHashCode(SubProcessModuleRelation obj)
        {
            return 0;
        }
    }

    public class DropdownItemComparer : IEqualityComparer<DropdownItem>
    {

        #region IEqualityComparer<DropdownItem> Members

        public bool Equals(DropdownItem x, DropdownItem y)
        {
            return x.Value.ToLower() == y.Value.ToLower();
        }

        public int GetHashCode(DropdownItem obj)
        {
            return 0;
        }

        #endregion
    }
}
