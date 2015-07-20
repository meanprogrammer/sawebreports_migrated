using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Entities.Enums;
using System.Globalization;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Entities.Utils
{
    public class ChangeHistoryComparer : IComparer<EntityDTO>
    {

        #region IComparer<EntityDTO> Members

        public int Compare(EntityDTO x, EntityDTO y)
        {
            x.ExtractProperties();
            y.ExtractProperties();

            string xStringDate = x.RenderHTML(GlobalStringResource.Date, RenderOption.None);
            string yStringDate = y.RenderHTML(GlobalStringResource.Date, RenderOption.None);

            //TODO: Revisit this one.
            //hack for checking empty strings
            if (string.IsNullOrEmpty(xStringDate) && string.IsNullOrEmpty(yStringDate))
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(xStringDate) && !string.IsNullOrEmpty(yStringDate))
            {
                return -1;
            }
            else if(!string.IsNullOrEmpty(xStringDate) && string.IsNullOrEmpty(yStringDate))
            {
                return 1;
            }

            DateTime xDate = DateTime.Parse(DateFixer.FixMonth(xStringDate));
            DateTime yDate = DateTime.Parse(DateFixer.FixMonth(yStringDate));

            if (xDate == null)
            {
                if (yDate == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (yDate == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    int retval = xDate.CompareTo(yDate);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater.
                        //
                        return retval;
                    }
                    else
                    {
                        // If the strings are of equal length,
                        // sort them with ordinary string comparison.
                        //
                        return xDate.CompareTo(yDate);
                    }
                }
            }
        }

        #endregion
    }

    //HACK
    class DateFixer
    {
        public static string FixMonth(string month)
        {
            if (month.Contains("Sept"))
            {
                month = month.Replace("Sept", "September");
            }
            return month;
        }
    }
}
