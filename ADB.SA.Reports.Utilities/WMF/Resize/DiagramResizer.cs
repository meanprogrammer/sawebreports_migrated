using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Utilities.WMF
{
    public class DiagramResizer
    {
        public static float GetPercentage(ResizeStrategyParameter parameter)
        {
            ResizeContext context;
            switch (parameter.Type)
            {
                case 111:
                    context = new ResizeContext(new ProcessResizeStrategy());
                    break;
                case 142:
                    context = new ResizeContext(new SubProcessResizeStrategy());
                    break;
                case 79:
                //system context
                case 81:
                    context = new ResizeContext(new SystemArchitectureResizeStrategy());
                    break;
                //case 81:
                //    context = new ResizeContext(new SystemContextResizeStrategy());
                //    break;
                default:
                    context = new ResizeContext(new GenericResizeStrategy());
                    break;
            }

            return context.GetPercentage(parameter);
        }
    }
}
