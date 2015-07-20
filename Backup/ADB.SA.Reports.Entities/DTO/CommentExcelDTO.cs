using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class CommentExcelDTO
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
