using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ADB.SA.Reports.Entities.DTO;
using System.Data.Common;
using System.Data;

namespace ADB.SA.Reports.Data
{
    public class CommentsData
    {
        Database db;
        public CommentsData()
        {
            db = DatabaseFactory.CreateDatabase("Comments");
        }

        public CommentDTO GetCommentByCommentID(int commentId)
        {
            DbCommand cmd = db.GetStoredProcCommand("GetByCommentId");
            db.AddInParameter(cmd, "@CommentID", DbType.Int32, commentId);

            CommentDTO dto = null;
            IDataReader reader = db.ExecuteReader(cmd);
            using (reader)
            {
                while (reader.Read())
                {
                    dto = new CommentDTO();
                    dto.CommentID = reader.GetInt32(0);
                    dto.DiagramID = reader.GetInt32(1);
                    dto.Username = reader.GetString(2);
                    dto.Comment = reader.GetString(3);
                    dto.CommentOrder = reader.GetInt32(4);
                    dto.CommentDate = reader.GetDateTime(5);
                    dto.Attachments = GetAllAttachmentByCommentIDAndDiagramID(dto.CommentID, dto.DiagramID);
                }
            }
            cmd.Dispose();
            return dto;
        }

        public bool SaveComment(CommentDTO comment)
        {
            DbCommand cmd = db.GetStoredProcCommand("SaveComment");
            db.AddInParameter(cmd, "@DiagramID", System.Data.DbType.Int32, comment.DiagramID);
            db.AddInParameter(cmd, "@Username", System.Data.DbType.String, comment.Username);
            db.AddInParameter(cmd, "@Comment", System.Data.DbType.String, comment.Comment);
            db.AddInParameter(cmd, "@CommentOrder", System.Data.DbType.Int32, comment.CommentOrder);
            db.AddInParameter(cmd, "@CommentDate", System.Data.DbType.DateTime, comment.CommentDate);
            //db.AddInParameter(cmd, "@Department", System.Data.DbType.String, comment.Department);

            int result = db.ExecuteNonQuery(cmd);

            cmd.Dispose();

            return result > 0;
        }

        public bool SaveComment(CommentDTO comment, out int commentId)
        {
            DbCommand cmd = db.GetStoredProcCommand("SaveCommentWithCommentID");
            db.AddInParameter(cmd, "@DiagramID", System.Data.DbType.Int32, comment.DiagramID);
            db.AddInParameter(cmd, "@Username", System.Data.DbType.String, comment.Username);
            db.AddInParameter(cmd, "@Comment", System.Data.DbType.String, comment.Comment);
            db.AddInParameter(cmd, "@CommentOrder", System.Data.DbType.Int32, comment.CommentOrder);
            db.AddInParameter(cmd, "@CommentDate", System.Data.DbType.DateTime, comment.CommentDate);
            db.AddOutParameter(cmd, "@CommentID", DbType.Int32, int.MaxValue);
            //db.AddInParameter(cmd, "@Department", System.Data.DbType.String, comment.Department);

            int result = db.ExecuteNonQuery(cmd);

            int id = Convert.ToInt32(db.GetParameterValue(cmd, "@CommentID"));
            commentId = id;

            cmd.Dispose();

            return result > 0;
        }

        public int GetCommentLastCountForDiagram(int diagramId)
        {
            DbCommand cmd = db.GetSqlStringCommand(
                string.Format("select max(CommentOrder) as LastOrder from Comment where DiagramID = {0}",
                diagramId));

            object result = db.ExecuteScalar(cmd);
            int order = 0;
            int.TryParse(result.ToString(), out order);

            cmd.Dispose();
            return order;
        }

        public List<CommentDTO> GetAllCommentsByDiagramID(int diagramId)
        {
            DbCommand cmd = db.GetStoredProcCommand("GetCommentsByDiagramID");
            db.AddInParameter(cmd, "@DiagramID", System.Data.DbType.Int32,
                diagramId);
            IDataReader reader = db.ExecuteReader(cmd);

            List<CommentDTO> comments = new List<CommentDTO>();
            while (reader.Read())
            {
                CommentDTO comment = new CommentDTO();
                comment.CommentID = reader.GetInt32(0);
                comment.DiagramID = reader.GetInt32(1);
                comment.Username = reader.GetString(2);
                comment.Comment = reader.GetString(3);
                comment.CommentOrder = reader.GetInt32(4);
                comment.CommentDate = reader.GetDateTime(5);

                comment.Attachments = GetAllAttachmentByCommentIDAndDiagramID(
                                            comment.CommentID,
                                            comment.DiagramID);

                comments.Add(comment);
            }

            reader.Close();
            reader.Dispose();
            cmd.Dispose();

            return comments;
        }

        private List<CommentAttachment>
            GetAllAttachmentByCommentIDAndDiagramID(int commentId,
            int diagramId)
        {
            DbCommand cmd = db.GetStoredProcCommand("GetAttachmentsByCommentIDandDiagramID");
            db.AddInParameter(cmd, "@CommentID", DbType.Int32, commentId);
            db.AddInParameter(cmd, "@DiagramID", DbType.Int32, diagramId);

            IDataReader reader = db.ExecuteReader(cmd);

                List<CommentAttachment> attachments = new List<CommentAttachment>();
            using (reader)
            {
                while (reader.Read())
                {
                    CommentAttachment att = new CommentAttachment();
                    att.RecordID = reader.GetInt32(0);
                    att.CommentID = reader.GetInt32(1);
                    att.PhysicalPath = reader.GetString(2);
                    att.VirtualPath = reader.GetString(3).Replace(@"\", @"\\");
                    att.DiagramID = reader.GetInt32(4);
                    attachments.Add(att);
                }
            }
            cmd.Dispose();
            return attachments;
        }

        public bool ExecuteStatement(string insert)
        {
            DbCommand cmd = db.GetSqlStringCommand(insert);
            int result = db.ExecuteNonQuery(cmd);
            cmd.Dispose();
            return result > 0;
        }

        private string GetDistinctCommentDiagramIDs()
        {
            StringBuilder ids = new StringBuilder();
            DbCommand cmd = db.GetSqlStringCommand("select distinct(DiagramID) as DiagramID from Comment");
            IDataReader reader = db.ExecuteReader(cmd);
            using (reader)
            {
                while (reader.Read())
                {
                    ids.AppendFormat("{0},", reader.GetInt32(0));
                }
            }
            if (ids.Length == 0)
                return string.Empty;

            return ids.Remove(ids.Length - 1, 1).ToString();
        }

        public List<CommentEntityDTO> GetEntitiesWithComments() 
        {
            Database encyDb = DatabaseFactory.CreateDatabase("Publisher");
            
            string ids = GetDistinctCommentDiagramIDs();
            if(string.IsNullOrEmpty(ids))
            {
                return new List<CommentEntityDTO>();
            }

            string sql =  string.Format(
                "SELECT * FROM [Entity] WHERE ID IN ({0}) AND Class = 1",
                ids); //);

            return ConvertToCE(DataHelper.ExecuteReaderReturnDTO(encyDb, sql));
        }

        private List<CommentEntityDTO> ConvertToCE(List<EntityDTO> list)
        {
            List<CommentEntityDTO> result = new List<CommentEntityDTO>();
            foreach (EntityDTO entity in list)
            {
                CommentEntityDTO c = new CommentEntityDTO();
                c.Name = entity.Name;
                c.ID = entity.ID;
                result.Add(c);
            }
            return result;
        }

        public List<CommentExcelDTO> GetCommentForExcel(string ids)
        {
            EntityData eData = new EntityData();
            string[] idList = ids.Split(new string[]{ "," }, StringSplitOptions.RemoveEmptyEntries);
            List<CommentExcelDTO> excelComments = new List<CommentExcelDTO>();
            foreach (string id in idList)
            {
                CommentExcelDTO commentExcelItem = new CommentExcelDTO();
                EntityDTO dto = eData.GetOneEntity(int.Parse(id));
                commentExcelItem.ID = dto.ID;
                commentExcelItem.Name = dto.Name;
                //TODO: Handle attachments someday
                commentExcelItem.Comments = GetAllCommentsByDiagramID(dto.ID);
                excelComments.Add(commentExcelItem);
            }
            return excelComments;
        }

        public bool LoginUser(string username, string password)
        {
            DbCommand cmd = db.GetStoredProcCommand("DoLogin");
            db.AddInParameter(cmd, "@Username", DbType.String, username);
            db.AddInParameter(cmd, "@Password", DbType.String, password);

            int result = Convert.ToInt32(db.ExecuteScalar(cmd));
            if (result > 0)
            {
                cmd.Dispose();
                return true;
            }
            cmd.Dispose();
            return false;
        }
    }
}
