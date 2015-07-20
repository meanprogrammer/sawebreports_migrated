using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Web
{
    /// <summary>
    /// Page used to process downloading of attachment
    /// </summary>
    public partial class AttachmentDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = HttpUtility.HtmlDecode(Request[GlobalStringResource.Path]);
            string commentFilePath = Server.MapPath(GlobalStringResource.CommentFilesFolder);

            string fullPath = string.Format(
                GlobalStringResource.CommentFileFullPathFormat, 
                commentFilePath, path);

            if (File.Exists(fullPath))
            {
                string filename = Path.GetFileName(fullPath);
                byte[] fileContents = File.ReadAllBytes(fullPath);
                Response.AddHeader(GlobalStringResource.ContentDispositionHeader,
                    string.Format(GlobalStringResource.DownloadAttachmentFormat,
                    filename));
                Response.ContentType = GlobalStringResource.UnknownContentType;
                Response.BinaryWrite(fileContents);
                Response.End();
            }

        }
    }
}
