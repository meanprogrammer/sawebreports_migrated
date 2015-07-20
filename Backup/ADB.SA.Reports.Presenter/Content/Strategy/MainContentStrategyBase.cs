using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Utilities.WMF;
using ADB.SA.Reports.Utilities;

namespace ADB.SA.Reports.Presenter.Content
{
    public abstract class MainContentStrategyBase
    {
        public abstract object BuildContent(EntityDTO dto);
        public string BuildDiagramContent(EntityDTO dto)
        {
            EntityData data = new EntityData();
            FileData files = new FileData();
            FileDTO file = files.GetFile(dto.DGXFileName);
            byte[] imageBytes = file.Data;
            string path = string.Format("{0}_{1}", file.Date.ToFileTime().ToString(), dto.DGXFileName);
            int poolCount = data.GetPoolCount(dto.ID);
            WmfImageManager imageManager = new WmfImageManager(dto, imageBytes,
                path, dto.Type, poolCount, false);
            path = imageManager.ProcessImage();
            return path.Replace(@"\", @"/");
        }

        public bool ShowResize()
        {
            return bool.Parse(AppSettingsReader.GetValue("SHOWRESIZE"));
        }
    }
}
