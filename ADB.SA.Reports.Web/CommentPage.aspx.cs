using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Presenter;
using Typps;
using ADB.SA.Reports.Utilities.HtmlHelper;

namespace ADB.SA.Reports.Web
{
    public partial class CommentPage : System.Web.UI.Page, ICommentsView
    {
        CommentsPresenter presenter;

        public CommentPage()
        {
            presenter = new CommentsPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DiagramIDHiddenField.Value = Request["id"];
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (!this.AttachmentFileUpload.HasFile)
            {
                return;
            }

            Dictionary<string, byte[]> currentFiles = GetFileTemporaryStorage();

            if (this.AttachmentFileUpload.HasFile && currentFiles != null)
            {
                byte[] binary = this.AttachmentFileUpload.FileBytes;
                string name = this.AttachmentFileUpload.FileName;

                if (currentFiles.ContainsKey(name))
                {
                    currentFiles.Remove(name);
                }

                currentFiles.Add(name, binary);

                //if (this.FileListBox.Items.FindByValue(name) != null)
                //{
                //    this.FileListBox.Items.Remove(name);
                //}

                //this.FileListBox.Items.Add(name);
                ViewState["fileuploads"] = currentFiles;
            }
        }

        private Dictionary<string, byte[]> GetFileTemporaryStorage()
        {
            Dictionary<string, byte[]> currentFiles = null;
            if (ViewState["fileuploads"] == null)
            {
                currentFiles = new Dictionary<string, byte[]>();
            }
            else
            {
                currentFiles = ViewState["fileuploads"] as Dictionary<string, byte[]>;
            }
            return currentFiles;
        }

        protected void PostCommentButton_Click(object sender, EventArgs e)
        {
            //var editorvalue = this.EditorValueHiddenField.Value;
            string validationMsg = string.Empty;
            if (!IsFormValid(out validationMsg))
            {
                this.ValidationLiteral.Text = string.Format(
                    "<div class='validation-summary'><ul>{0}</ul></div>",validationMsg);
                return;
            }

            string diagramId = Request["id"];
            //var html = this.Editor.Text;

            presenter.SaveComment(diagramId,
                this.EmailTextBox.Text.Trim(),
                CleanComment(this.editor_id.Value).Trim(),
                GetPostedFiles());
            this.EmailTextBox.Text = string.Empty;
            this.editor_id.Value = string.Empty;
            //this.FileListBox.Items.Clear();
            this.ValidationLiteral.Text = string.Empty;
            ClearButton_Click(sender, e);
        }

        private Dictionary<string, byte[]> GetPostedFiles()
        {
            Dictionary<string, byte[]> list = new Dictionary<string, byte[]>();

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile file = Request.Files.Get(i);
                if (file != null && file.ContentLength > 0)
                {
                    list.Add(Path.GetFileName(file.FileName), file.ToByteArray());
                }
            }
            
            return list;
        }


        private string CleanComment(string source)
        {
            source = HtmlRemoval.StripCarriageReturn(source);
            source = HtmlRemoval.RemoveParagraphTag(source);
            return source;
        }

        private bool IsFormValid(out string messages) 
        {
            messages = string.Empty;
            bool result = true;

            string html = StripHtmlComment(this.editor_id.Value);

            if (string.IsNullOrEmpty(html)) 
            {
                result = false;
                messages += "<li>The comment cannot be empty.</li>";
            }

            if (string.IsNullOrEmpty(this.EmailTextBox.Text.Trim()))
            {
                result = false;
                messages += "<li>The email cannot be empty.</li>";
            }
            else 
            {
                if (!RegexUtilities.EmailIsValid(this.EmailTextBox.Text.Trim()))
                {
                    result = false;
                    messages += "<li>The email is invalid format.</li>";
                }
            }
            

            return result;
        }

        private string StripHtmlComment(string source)
        {
            string result = HtmlRemoval.StripTagsCharArray(source);
            result = HtmlRemoval.StripCarriageReturn(result);
            return result.Trim();
        }

        #region ICommentsView Members

        public void RenderComments(string comments)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ViewState["fileuploads"] = null;
            //this.FileListBox.Items.Clear();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ClearButton_Click(sender, e);
            this.EmailTextBox.Text = string.Empty;
            this.editor_id.Value = string.Empty;
        }


        #region ICommentsView Members


        public void RenderComments(ADB.SA.Reports.Entities.DTO.CommentResponseDTO response)
        {
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(response));
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
