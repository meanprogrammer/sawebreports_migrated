using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ADB.SA.Reports.Configuration;

namespace ADB.SA.Reports.Entities.DTO
{
    public class HomePageContentDTO
    {
        public Dictionary<string, List<AsIsItemEntity>> SectionList { get; set; }
        public AsIsDiagramSection DiagramSection { get; set; }
        public string LeftGroupName { get; set; }
        public string LeftGroupCssClass { get; set; }
        public string RightGroupName { get; set; }
        public string RightGroupCssClass { get; set; }
        public string HomeInformation { get; set; }
        public int CurrentID { get; set; }
        public string Title { get; set; }
        public string Page
        {
            get
            {
                return "home";
            }
        }
    }
}
