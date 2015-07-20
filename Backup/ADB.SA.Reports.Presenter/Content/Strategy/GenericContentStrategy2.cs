using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Content
{
    public class GenericContentStrategy2 : MainContentStrategyBase
    {

        public override object BuildContent(EntityDTO dto)
        {
            GenericContentDTO content = new GenericContentDTO();
            content.Diagram = new DiagramContent(){
                DiagramPath = BuildDiagramContent(dto)
            };
            content.DiagramDescription = DiagramDescription(dto);
            content.CurrentID = dto.ID;
            content.ShowResize = ShowResize();
            return content;
        }


        private string BuildCommentsSection(EntityDTO dto)
        {
            return string.Format(GlobalStringResource.CommentFormHtml, dto.Name, dto.ID);
        }

        protected string DiagramDescription(EntityDTO dto)
        {
            dto.ExtractProperties();
            string description = dto.RenderHTML("Description", ADB.SA.Reports.Entities.Enums.RenderOption.Break);
            if (string.IsNullOrEmpty(description))
            {
                description = "There is no description for this diagram.";
            }

            return description;
        }
    }
}
