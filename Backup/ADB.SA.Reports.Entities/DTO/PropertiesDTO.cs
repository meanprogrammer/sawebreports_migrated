using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    public class PropertiesDTO
    {
        public string Key { get; set; }
        public List<string> Value { get; set; }

        public PropertiesDTO()
        {
            Value = new List<string>();
        }

        public void AddValue(string value)
        {
            this.Value.Add(value);
        }
    }
}
