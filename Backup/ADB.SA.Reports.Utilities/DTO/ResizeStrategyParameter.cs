using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Utilities
{
    public class ResizeStrategyParameter
    {
        public int Type { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool IsReport { get; set; }
        public int PoolCount { get; set; }
    }
}
