using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Data
{
    /// <summary>
    /// Data class for the Home page of the Website.
    /// </summary>
    public class AsIsData
    {
        /// <summary>
        /// Internal Database instance.
        /// </summary>
        Database db;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AsIsData()
        {
            db = DatabaseFactory.CreateDatabase(GlobalStringResource.Data_DefaultConnection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<AsIsItemEntity>> GetSections()
        {
            IDataReader reader = db.ExecuteReader(
                                                CommandType.Text,
                                                GlobalStringResource.AsisData_GetSections);
            Dictionary<string, List<AsIsItemEntity>> sections = 
                            new Dictionary<string, List<AsIsItemEntity>>();

            while (reader.Read())
            {
                sections.Add(reader.GetString(0), new List<AsIsItemEntity>());
            }

            reader.Close();
            reader.Dispose();

            return sections;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<EntityDTO> GetAllAsIsSymbols(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                                GlobalStringResource.Data_GetAllAsIsSymbolsQuery);
        }


    }
}
