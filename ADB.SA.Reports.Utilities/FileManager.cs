using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Utilities
{
    public class FileManager
    {
        public static void WriteFile(byte[] data, string fileName)
        {
            File.WriteAllBytes(fileName, data);
        }

        public static string GetFileName(string path)
        {
            string fileName = path.Split(
                new string[] { 
                    GlobalStringResource.PathSpliterDash 
                },
                StringSplitOptions.None).Last();
            return fileName;
        }
    }
}
