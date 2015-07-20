using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Utilities.WMF
{
    public abstract class ResizeStrategyBase
    {
        public abstract float GetPercentage(ResizeStrategyParameter parameter);
    }
}
