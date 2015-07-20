using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Presenter.Content
{
    public class MainContentBuilder
    {
        public static object BuildContent(EntityDTO dto)
        {
            MainContentContext context = null;
            switch (dto.Type)
            {
                case 111:
                    context = new MainContentContext(new ProcessStrategy2());
                    break;
                case 142:
                    context = new MainContentContext(new SubProcessContentStrategy2());
                    break;
                ////case 79:
                ////    context = new ContentContext(new SystemArchitectureContentStrategy());
                ////    break;
                case 145:
                    context = new MainContentContext(new Strategy2020ContentStrategy2());
                    break;
                default:
                    context = new MainContentContext(new GenericContentStrategy2());
                    break;
            }
            
            return context.BuildContent(dto);
        }
    }
}
