using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADB.SA.Reports.Presenter;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Web.Ajax
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class commentajax : IHttpHandler,  ICommentsView
    {
        CommentsPresenter presenter;
        HttpContext context;
        public commentajax() {
            presenter = new CommentsPresenter(this);
        }
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            var file = context.Request.Params["files"];
            var att = context.Request.Params["attachments"];
            var action = context.Request.Form["action"];
            switch (action)
            {
                case "save":
                    HandleSave(context);
                    break;
                case "getall":
                    HandleGetAll(context);
                    break;
                default:
                    break;
            }

        }

        private void HandleGetAll(HttpContext context)
        {
            var diagramId = context.Request.Form["id"];
            var page = context.Request.Form["page"];
            presenter.GetPagedComments(diagramId, int.Parse(page));
        }

        private void HandleSave(HttpContext context)
        {
            HttpFileCollection fs = context.Request.Files;
            Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
            var id = context.Request.Form["id"];
            var email = context.Request.Form["email"];
            var comment = context.Request.Form["comment"];
            
            //foreach (string file in fs)
            //{
            //    HttpPostedFile mainf = fs.Get(file);
            //    files.Add(file, mainf.ToByteArray());
            //}

            for (int i = 0; i < fs.Count; i++)
            {
                HttpPostedFile mainf = fs.Get(i);
                files.Add(mainf.FileName, mainf.ToByteArray());
            }


            bool result = presenter.SaveComment(id, email, comment, files);
            if (result)
            {
                context.Response.Write("success");
            }
            else
            {
                context.Response.Write("failed");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region ICommentsView Members

        public void RenderComments(string comments)
        {
            SAWebContext.Response.Write(comments);
        }

        #endregion


        #region ICommentsView Members


        public void RenderComments(ADB.SA.Reports.Entities.DTO.CommentResponseDTO response)
        {
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(response));
        }

        #endregion
    }

    public static class HttpPostedFileBaseExtensions
    {
        public static Byte[] ToByteArray(this HttpPostedFile value)
        {
            if (value == null)
                return null;

            var array = new Byte[value.ContentLength];
            value.InputStream.Position = 0;
            value.InputStream.Read(array, 0, value.ContentLength);
            return array;

        }
    }
}
