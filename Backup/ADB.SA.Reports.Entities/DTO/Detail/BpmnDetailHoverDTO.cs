﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class BpmnDetailHoverDTO : IDetailDTO
    {
        public string Description { get;set; }
        public string ReferencedDocuments { get;set; }
        public string TemplateID 
        {
            get { return string.Empty; }
        }
        public string Title { get;set; }
    }
}