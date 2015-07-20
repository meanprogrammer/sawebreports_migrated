using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ADB.SA.Reports.Data
{
    public class DataHelper
    {
        /// <summary>
        /// Executes the readers and converts it to list of EntityDTO.
        /// </summary>
        /// <param name="db">Instance of Enterprise Library database.</param>
        /// <param name="id">The ID for the filter.</param>
        /// <param name="sql">The sql command.</param>
        /// <returns>List of populated EntityDTO.</returns>
        public static List<EntityDTO> ExecuteReaderReturnDTO(Database db, object id, string sql)
        {
            return ExecuteReaderReturnDTO(db, new object[] { id }, sql);
        }

        public static List<EntityDTO> ExecuteReaderReturnDTO(Database db, string sql)
        {
            IDataReader reader = db.ExecuteReader(CommandType.Text, sql);
            List<EntityDTO> relatedProcess = new List<EntityDTO>();
            while (reader.Read())
            {
                EntityDTO dto = ReaderToEntityDTO(reader);
                relatedProcess.Add(dto);
            }

            reader.Close();
            reader.Dispose();

            return relatedProcess;
        }

        /// <summary>
        /// Executes the readers and converts it to list of EntityDTO.
        /// </summary>
        /// <param name="db">Instance of Enterprise Library database.</param>
        /// <param name="id">The ID for the filter.</param>
        /// <param name="sql">The sql command.</param>
        /// <returns>List of populated EntityDTO.</returns>
        public static List<EntityDTO> ExecuteReaderReturnDTO(Database db, object[] id, string sql)
        {
            IDataReader reader = db.ExecuteReader(
                                            CommandType.Text,
                                            string.Format(sql, id));
            List<EntityDTO> relatedProcess = new List<EntityDTO>();
            while (reader.Read())
            {
                EntityDTO dto = ReaderToEntityDTO(reader);
                relatedProcess.Add(dto);
            }

            reader.Close();
            reader.Dispose();

            return relatedProcess;
        }

        /// <summary>
        /// Converts a reader to EntityDTO instance.
        /// </summary>
        /// Column indexes:
        /// 
        /// Audit = 0
        /// Class = 1
        /// FromArrow = 2
        /// FromAssc = 3
        /// ID = 4
        /// Name = 5
        /// SeqNum = 6
        /// Properties = 7
        /// ToArrow = 8
        /// ToAssc = 9
        /// Type = 10
        /// UpdateDate = 11
        /// SAGuid = 12
        /// PropType = 13
        /// ShortProps = 14
        /// <param name="reader">The data reader instance.</param>
        /// <returns>Instance of EntityDTO populated from the reader.</returns>
        private static EntityDTO ReaderToEntityDTO(IDataReader reader)
        {
            
            EntityDTO dto = new EntityDTO();
            dto.Audit = reader.GetString(0);
            dto.Class = reader.GetInt16(1);
            dto.FromArrow = reader.GetBoolean(2);
            dto.FromAssc = reader.GetInt32(3);
            dto.ID = reader.GetInt32(4);

            dto.Name = reader.IsDBNull(5) ?
                string.Empty : reader.GetString(5);

            dto.SeqNum = reader.GetInt16(6);

            dto.Properties = reader.IsDBNull(7) ?
                string.Empty : reader.GetString(7);

            dto.ToArrow = reader.GetBoolean(8);
            dto.ToAssc = reader.GetInt32(9);

            dto.Type = reader.GetInt16(10);
            dto.UpdateDate = reader.GetDateTime(11);


            dto.SAGuid = reader.IsDBNull(12) ?
                string.Empty : reader.GetString(12);

            dto.PropType = reader.GetInt16(13);

            dto.ShortProps = reader.IsDBNull(14) ?
                string.Empty : reader.GetString(14);

            return dto;
        }

        /// <summary>
        /// Fix the escape character for sql.
        /// </summary>
        /// <param name="text">The input text.</param>
        /// <returns>The fixed text.</returns>
        public static string FixString(string text)
        {
            return text.Replace("'", "''");
        }
    }
}
