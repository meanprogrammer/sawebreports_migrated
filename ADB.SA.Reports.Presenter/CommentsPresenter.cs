using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;
using System.Security.Principal;
using System.Web.UI.WebControls;
using System.Data;
using Office.Excel.Helper;
using ADB.SA.Reports.Global;
using System.IO;

namespace ADB.SA.Reports.Presenter
{
    public class CommentsPresenter
    {
        ICommentsView view;
        CommentsData cData;
        EntityData eData;
        public CommentsPresenter(ICommentsView view)
        {
            this.view = view;
            this.cData = new CommentsData();
            this.eData = new EntityData();
        }

        public void SaveComment(string diagramId, string username, 
            string comment)
        {
            CommentDTO dto = new CommentDTO();
            dto.DiagramID = int.Parse(diagramId);
            dto.Username = username;
            dto.Comment = comment;
            dto.CommentDate = DateTime.Now;
            dto.CommentOrder = this.cData.GetCommentLastCountForDiagram(dto.DiagramID) + 1;

            this.cData.SaveComment(dto);
        }

        public bool SaveComment(string diagramId, string username,  string comment, Dictionary<string, byte[]> attachments)
        {
            int id = 0;
            bool savecommentresult = false;
            bool savefilesresult = true;
            CommentDTO dto = new CommentDTO();
            dto.DiagramID = int.Parse(diagramId);
            dto.Username = username;
            dto.Comment = comment;
            dto.CommentDate = DateTime.Now;
            dto.CommentOrder = this.cData.GetCommentLastCountForDiagram(dto.DiagramID) + 1;

            try
            {
                savecommentresult = this.cData.SaveComment(dto, out id);
                if (attachments != null && attachments.Count > 0)
                {
                    string insert = this.WriteAllAttachments(attachments,
                        diagramId,
                        id);
                    savefilesresult = cData.ExecuteStatement(insert);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return savecommentresult && savefilesresult;
        }

        private string WriteAllAttachments(Dictionary<string, byte[]> attachments, string parentFolder, int id)
        {
            StringBuilder insertStatement = new StringBuilder();

            string commentFilesFolderPath = SAWebContext.Server.MapPath("CommentFiles");
            string targetDir = string.Format(@"{0}\{1}\{2}",
                                commentFilesFolderPath,
                                parentFolder, id);
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            foreach (var item in attachments)
            {
                string fullPath = string.Format(@"{0}\{1}",
                    targetDir, Path.GetFileName(item.Key));
                File.WriteAllBytes(fullPath, item.Value);

                insertStatement.AppendFormat(
                    "INSERT INTO [Attachments](DiagramID,PhysicalPath,VirtualPath,CommentID) " +
                    "VALUES({0},'{1}','{2}',{3});",
                    parentFolder,
                    fullPath,
                    string.Format(@"{0}\{1}\{2}", parentFolder, id, item.Key),
                    id);
            }
            return insertStatement.ToString();
        }

        public void GetAllComments(string diagramId)
        {
            List<CommentDTO> comments = this.cData.GetAllCommentsByDiagramID(int.Parse(diagramId));
            StringBuilder html = new StringBuilder();
            EntityDTO dto = this.eData.GetOneEntity(int.Parse(diagramId));
            if (dto != null)
            {
                html.AppendFormat("<h3 style='color: #517704;'>{0}</h3>", dto.Name);
            }

            foreach (CommentDTO c in comments)
            {
                StringBuilder attachmentHtml = new StringBuilder();
                if (c.Attachments.Count > 0)
                {
                    foreach (CommentAttachment ca in c.Attachments)
                    {
                        attachmentHtml.AppendFormat(
                            "<a onclick=\"downloadattachment('{0}');\">{1}</a><b>|</b>",
                            ca.VirtualPath.Replace(@"\", @"\\"),
                            Path.GetFileName(ca.VirtualPath));
                    }
                    attachmentHtml.Remove(attachmentHtml.Length - 8, 8);
                }



                html.AppendFormat("<div class='comment-item'>{0}<div id='attachments'><b>Attachments:</b>&nbsp;{1}</div><br/>by: <b>{2}</b> on <i>{3}</i></div>",
                    c.Comment, attachmentHtml.ToString(), c.Username, c.CommentDate);
            }
            this.view.RenderComments(html.ToString());
        }

        private void RenderCommentsHtml(List<CommentDTO> comments, int pageCount, int currentPage)
        {
            //StringBuilder html = new StringBuilder();
            //foreach (CommentDTO c in comments)
            //{
            //    StringBuilder attachmentHtml = new StringBuilder();
            //    if (c.Attachments.Count > 0)
            //    {
            //        foreach (CommentAttachment ca in c.Attachments)
            //        {
            //            attachmentHtml.AppendFormat(
            //                "<a class=\"btn btn-info btn-xs\" onclick=\"downloadattachment('{0}');\">{1}</a><b>|</b>",
            //                ca.VirtualPath.Replace(@"\",@"\\"),
            //                ca.FileName);
            //        }
            //        attachmentHtml.Remove(attachmentHtml.Length - 8, 8);
            //    }

            //    string commentText = c.Comment;
                //commentText = string.Format(@"{0}&nbsp;|&nbsp;", commentText, c.CommentID);

//                <div class="panel panel-default">
//  <div class="panel-body">
//    Panel content
//  </div>
//  <div class="panel-footer">Panel footer</div>
//</div>

            //    html.AppendFormat(
            //        "<div class=\"panel panel-default\">" +
            //        "<div class=\"panel-body\">" +
            //            "<div class='truncate clearfix'>{0}</div>" +
            //                "<div class='viewfullcomment'>"+
            //                        "<input id='viewfullbutton' type='button' onclick='viewfullcomment({1});' value='View Full' />" +
            //                "</div>" +
            //                "<div id='attachments'><b>Attachments:</b>&nbsp;{2}</div>" + 
            //                "<br/>by: <b>{3}</b> on <i>{4}</i>" +
            //                 "</div>" +
            //        "</div>",
            //        commentText, c.CommentID, attachmentHtml.ToString(), c.Username, c.CommentDate);
            //}

            //html.Append("<div id='comment-pager'>");

            //for (int i = 0; i < pageCount; i++)
            //{
            //    string selectedPageCss = string.Empty;
            //    if (currentPage == i)
            //    {
            //        selectedPageCss = "pager-selected";
            //    }
            //    html.AppendFormat(
            //        "<a class='page-item {0}' onclick='getallcomments({1});'>{2}</a>",
            //        selectedPageCss, i, i + 1);
            //}

            //html.Append("</div>");

            CommentResponseDTO response = new CommentResponseDTO();
            response.Comments = comments;
            response.CurrentPage = currentPage;
            response.PageCount = pageCount;
            this.view.RenderComments(response);
        }

        private void RenderOneCommentsHtml(CommentDTO comment)
        {
            StringBuilder html = new StringBuilder();

                StringBuilder attachmentHtml = new StringBuilder();
                if (comment.Attachments.Count > 0)
                {
                    foreach (CommentAttachment ca in comment.Attachments)
                    {
                        attachmentHtml.AppendFormat(
                            "<a class=\"btn btn-info btn-xs\" onclick=\"downloadattachment('{0}');\">{1}</a><b>|</b>",
                            ca.VirtualPath.Replace(@"\", @"\\"),
                            Path.GetFileName(ca.VirtualPath));
                    }
                    attachmentHtml.Remove(attachmentHtml.Length - 8, 8);
                }

                html.AppendFormat("<div class='comment-item'>{0}<div id='attachments'><b>Attachments:</b>&nbsp;{1}</div><br/>by: <b>{2}</b> on <i>{3}</i></div>",
                    comment.Comment, attachmentHtml.ToString(), comment.Username, comment.CommentDate);


            this.view.RenderComments(html.ToString());
        }

        public void GetOneComment(int commentId)
        {
            CommentDTO dto = this.cData.GetCommentByCommentID(commentId);
            if (dto != null)
            {
                RenderOneCommentsHtml(dto);
            }
        }

        public void GetPagedComments(string diagramId, int page)
        {
            List<CommentDTO> comments = this.cData.GetAllCommentsByDiagramID(int.Parse(diagramId));
            if (comments.Count <= 5)
            {
                //default
                RenderCommentsHtml(comments, 0, page);
            }
            else
            {
                List<List<CommentDTO>> pagedList = SplitList<CommentDTO>(
                    comments, 5, null);
                RenderCommentsHtml(pagedList[page], pagedList.Count, page);
            }
        }

        public List<List<T>> SplitList<T>(IEnumerable<T> values, int groupSize, int? maxCount)
        {
            List<List<T>> result = new List<List<T>>();
            // Quick and special scenario
            if (values.Count() <= groupSize)
            {
                result.Add(values.ToList());
            }
            else
            {
                List<T> valueList = values.ToList();
                int startIndex = 0;
                int count = valueList.Count;
                int elementCount = 0;

                while (startIndex < count && (!maxCount.HasValue || (maxCount.HasValue && startIndex < maxCount)))
                {
                    elementCount = (startIndex + groupSize > count) ? count - startIndex : groupSize;
                    result.Add(valueList.GetRange(startIndex, elementCount));
                    startIndex += elementCount;
                }
            }


            return result;
        }

        public static string Truncate(string source, int length)
        {
            if (source.Length > length)
            {
                source = source.Substring(0, length);
            }
            return source;
        }
    }
}
