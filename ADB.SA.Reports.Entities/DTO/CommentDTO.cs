using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class CommentDTO
    {
        public int CommentID { get; set; }
        public int DiagramID { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        //public string Department { get; set; }
        public int CommentOrder { get; set; }
        public DateTime CommentDate { get; set; }
        public string FilePath { get; set; }
        public string CommentDateFormatted 
        {
            get {
                return this.CommentDate.ToString();
            }
        }
        public List<CommentAttachment> Attachments { get; set; }
    }
}
