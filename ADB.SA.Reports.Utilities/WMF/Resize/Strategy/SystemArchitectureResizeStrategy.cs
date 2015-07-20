using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Utilities.WMF
{
    public class SystemArchitectureResizeStrategy : ResizeStrategyBase
    {

        public override float GetPercentage(ResizeStrategyParameter parameter)
        {
            float percent = 0;
            if (parameter.Width > 30000)
            {
                percent = 9;
            }
            else if(parameter.Width > 25000 && parameter.Width < 30000)
            {
                percent = 7;
            }
            else if (parameter.Width > 20000 && parameter.Width < 25000)
            {
                percent = 5;
            }
            else if (parameter.Width > 15000 && parameter.Width < 20000)
            {
                percent = 5;
            }
            else if (parameter.Width > 10000 && parameter.Width < 15000)
            {
                percent = 5;
            }
            else if (parameter.Width > 5000 && parameter.Width < 10000)
            {
                percent = 15;
            }
            else if (parameter.Width > 3000 && parameter.Width < 5000)
            {
                percent = 95;
            }
            else
            {
                percent = 100;
            }

            if (parameter.IsReport)
            {
                percent += 7;
            }

            return percent;
        }
    }
}
