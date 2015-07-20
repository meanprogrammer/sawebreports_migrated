using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Entities.Enums;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using ADB.SA.Reports.Data.Helper;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Data
{
    /// <summary>
    /// Class that manages data for the navigation pane.
    /// </summary>
    public class NavigationData
    {
        /// <summary>
        /// Internal Database instance.
        /// </summary>
        Database db;

        /// <summary>
        /// Internal instance of cachemanager.
        /// </summary>
        ICacheManager cache;

        /// <summary>
        /// Default constructor.
        /// Initialize database and cachemanager.
        /// </summary>
        public NavigationData()
        {
            db = DatabaseFactory.CreateDatabase(GlobalStringResource.Data_DefaultConnection);
            cache = CacheFactory.GetCacheManager();
        }

        /// <summary>
        /// Gets all the the used diagrams.
        /// </summary>
        /// <param name="filter">SQL filter (where ....)</param>
        /// <returns>List of menus with child items. Like in System Architect.</returns>
        public List<SAMenuItemDTO> GetAllUsedDiagrams(string filter)
        {
            List<SAMenuItemDTO> cachedMenu = CacheHelper.GetFromCacheWithCheck<List<SAMenuItemDTO>>(cache, "sa_menu_key");
            if (cachedMenu != null)
            {
                return cachedMenu;
            }

            IDataReader reader = db.ExecuteReader(CommandType.Text,
                string.Concat(GlobalStringResource.Data_GetAllUsedDiagramsQuery,
                    !string.IsNullOrEmpty(filter) ? 
                    string.Format(GlobalStringResource.NavData_TypeNotIn,filter) : string.Empty)
                                                    );
            List<SAMenuItemDTO> treeViewItems = new List<SAMenuItemDTO>();

            int typeIndex = reader.GetOrdinal(GlobalStringResource.Type);

            while (reader.Read())
            {
                Int16 value = reader.GetInt16(typeIndex);
                if (value > 0)
                {
                    SAMenuItemDTO item = new SAMenuItemDTO();
                    string name = TypesReader.DiagramTypes[value];

                    item.ID = value;
                    item.Text = name;
                    item.ChildItems = GetSubDiagrams((int)value);

                    if (item.ChildItems.Count > 0)
                    {
                        treeViewItems.Add(item);
                    }
                }
            }
            reader.Close();
            reader.Dispose();

            CacheHelper.AddToCacheWithCheck(cache, GlobalStringResource.Data_sa_menu_key,
                treeViewItems);
            return treeViewItems;
        }

        /// <summary>
        /// Gets the child items of the parent.
        /// </summary>
        /// <param name="type">type number of the diagram.</param>
        /// <returns>Key value pair of ID and text of child item.</returns>
        private Dictionary<int, string> GetSubDiagrams(int type)
        {
            EntityData data = new EntityData();
            IDataReader reader = db.ExecuteReader(CommandType.Text,
                                                    string.Format(GlobalStringResource.Data_GetSubDiagramsQuery, type));
            Dictionary<int, string> list = new Dictionary<int, string>();
            int idIndex = reader.GetOrdinal(GlobalStringResource.ID);
            int nameIndex = reader.GetOrdinal(GlobalStringResource.Name);
            while (reader.Read())
            {
                int id = reader.GetInt32(idIndex);
                string name = reader.GetString(nameIndex);

                EntityDTO dto = data.GetOneEntity(id);


                if (SAModeHelper.IsValidForCurrentMode(id)) // && (dto != null && dto.Publish == true))
                {
                    list.Add(id, name);
                }
                //if(GroupFilterHelper.IsValidForShow(type, id))
                //{
                //    list.Add(id, name);
                //}
            }
            reader.Close();
            reader.Dispose();
            return list;
        }
    }
}
