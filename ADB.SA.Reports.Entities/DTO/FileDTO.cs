using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class FileDTO
    {
        public byte[] Data { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
