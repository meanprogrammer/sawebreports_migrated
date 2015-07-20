using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.View
{
    public interface IStrategy2020ReportView
    {
        void ShowContent(string content);
        void ShowFilteredFilter(List<DropdownItem> items);
        void ShowFilteredFilter(Dictionary<string, List<DropdownItem>> items);
    }
}
