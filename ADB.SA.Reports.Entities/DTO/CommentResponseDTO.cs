using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class CommentResponseDTO
    {
        public List<CommentDTO> Comments { get; set; }
        public int PageCount { get; set; }
        public List<int> PageCountList 
        {
            get {
                List<int> list = new List<int>();
                for (int i = 0; i < PageCount; i++)
                {
                    list.Add(i);
                }
                return list;
            }
        }
        public int CurrentPage { get; set; }
    }
}
