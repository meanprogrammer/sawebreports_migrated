
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Utilities;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Data
{
    /// <summary>
    /// Manages data from the files table.
    /// </summary>
    public class FileData
    {
        /// <summary>
        /// Internal Database instance.
        /// </summary>
        Database db;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public FileData()
        {
            db = DatabaseFactory.CreateDatabase(GlobalStringResource.Data_DefaultConnection);    
        }

        /// <summary>
        /// Gets a single row of file.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>A instance of the FileDTO.</returns>
        public FileDTO GetFile(string fileName)
        {
            FileDTO result = null;
            fileName = fileName.Replace(GlobalStringResource.Data_DGXExtension,
                            GlobalStringResource.WMFExtension);
            IDataReader reader = db.ExecuteReader(CommandType.Text,
                string.Format(GlobalStringResource.Data_GetFileQuery, fileName));

            while (reader.Read())
            {
                result = new FileDTO();
                result.Data = (byte[])reader.GetValue(0);
                result.Date = reader.GetDateTime(1);
                result.Name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                result.Type = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
            }
            reader.Close();
            reader.Dispose();
            return result;
        }
    }
}
