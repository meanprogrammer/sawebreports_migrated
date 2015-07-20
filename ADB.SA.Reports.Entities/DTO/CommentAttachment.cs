using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ADB.SA.Reports.Entities.DTO
{
    public class CommentAttachment
    {
        public int RecordID { get; set; }
        public int CommentID { get; set; }
        public int DiagramID { get; set; }
        public string PhysicalPath { get; set; }
        public string VirtualPath { get; set; }
        public string FileName 
        { 
            get {
                return Path.GetFileName(this.VirtualPath);
            } 
        }
    }
}
