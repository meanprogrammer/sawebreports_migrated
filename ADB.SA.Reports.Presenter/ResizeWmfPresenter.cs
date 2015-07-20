using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Utilities.WMF;

namespace ADB.SA.Reports.Presenter
{
    public class ResizeWmfPresenter
    {
        public string BuildDiagramContent(int id, float percentage)
        {
            
            EntityData data = new EntityData();

            EntityDTO dto = data.GetOneEntity(id);
            dto.ExtractProperties();
            FileData files = new FileData();
            FileDTO file = files.GetFile(dto.DGXFileName);
            byte[] imageBytes = file.Data;
            string path = string.Format("{0}_{1}", file.Date.ToFileTime().ToString(), dto.DGXFileName);
            int poolCount = data.GetPoolCount(dto.ID);
            WmfImageManager imageManager = new WmfImageManager(dto, imageBytes,
                path, dto.Type, poolCount, false);
            path = imageManager.ProcessImageWithPercentage(percentage);
            return path.Replace(@"\", @"/");
        }
    }
}
