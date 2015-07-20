using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Compression;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Web.Helpers
{
    public class GzipHelper
    {
        public static void CompressPage()
        {
            SAWebContext.Response.Filter = new GZipStream(SAWebContext.Response.Filter,
                            CompressionMode.Compress);
            SAWebContext.Response.AddHeader("Content-Encoding", "gzip");
        }
    }
}
