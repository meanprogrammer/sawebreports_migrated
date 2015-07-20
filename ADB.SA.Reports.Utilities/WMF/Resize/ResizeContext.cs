using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Utilities.WMF
{
    public class ResizeContext
    {
        ResizeStrategyBase strategy;

        public ResizeContext(ResizeStrategyBase strategy)
        {
            this.strategy = strategy;
        }

        public float GetPercentage(ResizeStrategyParameter parameter)
        {
            return this.strategy.GetPercentage(parameter);
        }
    }
}
