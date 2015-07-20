using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.View
{
    public interface IMenuPageView 
    {
        void BindNavigationMenu(List<SAMenuItemDTO> menuItems);
    }
}
