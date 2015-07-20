using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Global
{
    public class PathResolver
    {
        public static string MapPath(string folderName)
        { 
            if(!string.IsNullOrEmpty(folderName))
            {
                return string.Concat(AppDomain.CurrentDomain.BaseDirectory, folderName);
            }
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
