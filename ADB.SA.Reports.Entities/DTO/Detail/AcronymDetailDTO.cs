using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class AcronymDetailDTO :IDetailDTO
    {
        public string AbbreviationDescription { get; set; }
        public string TemplateID { get { return "#acronym-content"; } }
        #region IDetailDTO Members

        public string Description
        {
            get;

            set;

        }

        public string ReferencedDocuments
        {
            get;
            set;
        }

        #endregion

        #region IDetailDTO Members


        public string Title
        {
            get;
            set;
        }

        #endregion
    }
}
