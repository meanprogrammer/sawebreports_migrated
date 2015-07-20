using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.DTO;
using System.IO;
using ADB.SA.Reports.Utilities.WMF;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Presenter.Content;
using ADB.SA.Reports.Presenter.Utils;

namespace ADB.SA.Reports.Presenter
{
    public class DefaultPagePresenter
    {
        IDefaultView view;
        EntityData data;
        public DefaultPagePresenter(IDefaultView view) 
        {
            this.view = view;
            data = new EntityData();
        }

        public void RenderDetail(int id)
        {
            EntityDTO dto = data.GetOneEntity(id);
            dto.ExtractProperties();
            object detail = DetailBuilder2.BuildDetail(dto);
            this.view.RenderDetail(detail);
        }

        public void RenderEntity(int id)
        {
            EntityDTO dto = data.GetOneEntity(id);
            dto.ExtractProperties();
            ProcessStrategy2 st = new ProcessStrategy2();
            PageResponseDTO response = new PageResponseDTO();
            response.BreadCrumbContent = BreadcrumbHelper.BuildBreadcrumbContent(dto);
            response.Header = dto.Name;
            response.Content = MainContentBuilder.BuildContent(dto);
            switch (dto.Type)
            {
                case 111:
                    response.RenderType = "process";
                    break;
                case 142:
                    response.RenderType = "subprocess";
                    break;
                case 145:
                    response.RenderType = "st2020";
                    break;
                default:
                    break;
            }
            this.view.RenderContent(response);
            
        }

        public int GetHomePageType(int id)
        {
            EntityDTO dto = data.GetOneEntity(id);
            return dto.Type;
        }
    }
}
