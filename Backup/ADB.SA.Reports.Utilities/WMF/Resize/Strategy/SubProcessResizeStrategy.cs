using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Utilities.WMF
{
    public class SubProcessResizeStrategy : ResizeStrategyBase
    {
        public override float GetPercentage(ResizeStrategyParameter parameter)
        {

            float percent = 0;
            switch (parameter.PoolCount)
            {
                case 1:
                    percent = (float)4;
                    break;
                case 2:
                    percent = (float)4.1;
                    break;
                case 3:
                    percent = 5 * (float)1.2;
                    break;
                case 4:
                    percent = 7 * (float)1.2;
                    break;
                case 5:
                    percent = 5 * 2;
                    break;
                case 6:
                    percent = 6 * (float)1.3;
                    break;
                case 7:
                    percent = 7 * 2;
                    break;
                case 8:
                    percent = 8 * 2;
                    break;
                default:
                    if (parameter.PoolCount > 8)
                    {
                        percent = 20;
                    }
                    else
                    {
                        percent = 0;
                    }
                    break;


            }

            if (parameter.IsReport)
            {
                percent += (float)7;
            }

            return percent;
            //float ratioHeight = (float)height / (float)width;
            //float ratioWidth = (float)width / (float)height;
            ////// use whichever multiplier is smaller
            ////float ratio = ratioX < ratioY ? ratioX : ratioY; 



            //if (ratioHeight > ratioWidth)
            //{
            //    float multiplicand = 4;
            //    if (poolCount >= 4)
            //    {
            //        multiplicand = poolCount;
            //        if (multiplicand > 5)
            //        {
            //            multiplicand = multiplicand * (float).75;
            //        }
            //    }

            //    percent = (float)multiplicand * ratioHeight;
            //    return percent;
            //}

            //if (poolCount == 1)
            //{
            //    return (float)4.5;
            //}

            //if (width > 25000)
            //{
            //    percent = 4;// *ratio;
            //}
            //else if (width > 20000 && width < 25000)
            //{
            //    percent = 5;// *ratio;
            //}
            //else if (width > 15000 && width < 20000)
            //{
            //    percent = 5;
            //}
            //else if (width > 10000 && width < 15000)
            //{
            //    percent = 5;
            //}
            //else if (width > 5000 && width < 10000)
            //{
            //    percent = 55;
            //}
            //else if (width > 3000 && width < 5000)
            //{
            //    percent = 95;
            //}
            //else
            //{
            //    percent = 100;
            //}
            //return percent;
        }

    }
}
