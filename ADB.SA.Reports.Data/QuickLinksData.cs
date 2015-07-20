using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Data
{
    /// <summary>
    /// Class that handles the data of the quicklinks.
    /// Quicklinks can be seen in the upper right side of the diagram page.
    /// </summary>
    public class QuickLinksData
    {
        /// <summary>
        /// Internal Database instance.
        /// </summary>
        Database db;

        /// <summary>
        /// Default constructor, initializes the database instance.
        /// </summary>
        public QuickLinksData()
        {
            db = DatabaseFactory.CreateDatabase(GlobalStringResource.Data_DefaultConnection);
        }

        /// <summary>
        /// Gets all the related sub-process by the diagram id.
        /// </summary>
        /// <param name="id">The diagram id.</param>
        /// <returns>List of EntityDTO which is the related sub-processes.</returns>
        public List<EntityDTO> GetAllRelatedSubProcess(int id)
        {
            List<EntityDTO> result = new List<EntityDTO>();
            List<string> relatedProcessSymbols = GetAllRelatedSymbols(id, GlobalStringResource.QuickLinksData_GetAllRelatedSubProcessQuery);
            string condition = string.Join(",", relatedProcessSymbols.ToArray());
            if (relatedProcessSymbols.Count > 0)
            {
                return DataHelper.ExecuteReaderReturnDTO(db, condition, GlobalStringResource.QuicklinksData_GetAllRelatedSubProcessQuery2);
            }
            return result;
        }

        /// <summary>
        /// Gets the related symbol by the diagram id.
        /// </summary>
        /// <param name="id">The diagram id.</param>
        /// <param name="query">The sql query.</param>
        /// <returns>List of Symbol names.</returns>
        private List<string> GetAllRelatedSymbols(int id, string query)
        {
            List<string> ids = new List<string>();
            IDataReader reader = db.ExecuteReader(CommandType.Text,
                                        string.Format(query,
                                        id));

            while (reader.Read())
            {
                ids.Add(DataHelper.FixString(reader[0].ToString()));
            }
            reader.Close();
            reader.Dispose();
            return ids;
        }

        /// <summary>
        /// Gets the related processes by diagram id.
        /// </summary>
        /// <param name="id">The diagram id.</param>
        /// <returns>List of related process to the diagram.</returns>
        public List<EntityDTO> GetAllRelatedProcess(int id)
        {
            List<EntityDTO> result = new List<EntityDTO>();
            List<string> relatedProcessSymbols = GetAllRelatedSymbols(id, GlobalStringResource.QuickLinksData_GetAllRelatedProcessQuery);
            string condition = string.Join("','", relatedProcessSymbols.ToArray());
            if (relatedProcessSymbols.Count > 0)
            {
                result = DataHelper.ExecuteReaderReturnDTO(db, condition, GlobalStringResource.QuicklinksData_GetAllRelatedProcessQuery2);
            }
            return result;
        }


    }
}
