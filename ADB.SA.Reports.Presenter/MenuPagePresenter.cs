using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter
{
    public class MenuPagePresenter
    {
        IMenuPageView view;
        ICacheManager cache;
        public MenuPagePresenter(IMenuPageView view)
        {
            this.view = view;
            this.cache = CacheFactory.GetCacheManager();
        }

        public void BuildNavigation()
        {
            List<SAMenuItemDTO> arrangedMenuItems = new List<SAMenuItemDTO>();

            //Remove the ff lines to disable caching
            if (CacheHelper.GetFromCacheWithCheck<List<SAMenuItemDTO>>("arrangedMenuItems") != null
                && CacheHelper.GetFromCacheWithCheck<List<SAMenuItemDTO>>("arrangedMenuItems").Count > 0)
            {
                arrangedMenuItems = CacheHelper.GetFromCacheWithCheck<List<SAMenuItemDTO>>("arrangedMenuItems");
                this.view.BindNavigationMenu(arrangedMenuItems);
                return;
            }
            //Remove the ff lines to disable caching

            NavigationData data = new NavigationData();

            MenuFilterSection menu = MenuFilterSection.GetConfig();
            List<string> ids = menu.GetItemsToBeRemove();

            string filter = string.Join(",", ids.ToArray());
            MenuOrderSection menuOrder = MenuOrderSection.GetConfig();
            List<SAMenuItemDTO> menuItems = data.GetAllUsedDiagrams(filter);
            
            if (menuOrder.MenuOrders.Count > 0)
            {
                foreach (MenuOrder item in menuOrder.MenuOrders)
                {
                    SAMenuItemDTO order = menuItems.FirstOrDefault(c => c.ID == item.Id);
                    if (order != null)
                    {
                        arrangedMenuItems.Insert(item.Order, order);
                    }
                }

                if (menuItems.Count > menuOrder.MenuOrders.Count)
                {
                    foreach (MenuOrder item in menuOrder.MenuOrders)
                    {
                        var order = menuItems.FirstOrDefault(c => c.ID == item.Id);
                        if (order != null)
                        {
                            menuItems.Remove(order);
                        }
                    }
                    arrangedMenuItems.AddRange(menuItems);
                }
            }
            else
            {
               arrangedMenuItems = menuItems.OrderBy(diag => diag.Text).ToList();
            }

            //Remove the ff lines to disable caching
            CacheHelper.AddToCacheWithCheck("arrangedMenuItems", arrangedMenuItems);
            //Remove the ff lines to disable caching

            this.view.BindNavigationMenu(arrangedMenuItems);
        }
    }
}
