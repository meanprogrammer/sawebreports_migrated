using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.View
{
    public interface IDefaultView 
    {
        void RenderContent(string html);
        void RenderDetail(object content);
        void RenderContent(PageResponseDTO response);
        void RenderBreadcrumb(string html);
        void RenderHomePageData(HomePageContentDTO dto);
    }
}
