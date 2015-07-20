using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using System.IO;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Utilities.WMF
{
    public class SvgImageManager : ImageManagerBase
    {
        private EntityDTO dto;
        private byte[] data;
        private string fileName;

        public SvgImageManager(EntityDTO dto, byte[] data, string fileName)
        {
            this.dto = dto;
            this.data = data;
            this.fileName = fileName;
        }

        public override string ProcessImage()
        {
            if (data != null)
            {
                SaveRawFile();
                return string.Format(@"Diagrams\{0}", this.fileName);
            }
            else
            {
                return string.Empty;
            }
        }

        private void SaveRawFile()
        {
            File.WriteAllBytes(
                PathResolver.MapPath(
                    string.Format(@"Diagrams\{0}", this.fileName)),
                    this.data
                );
        }
    }
}
