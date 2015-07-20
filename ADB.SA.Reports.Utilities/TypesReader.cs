using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Utilities
{
    public class TypesReader
    {
        static string path = AppSettingsReader.GetValue(GlobalStringResource.AccessControlMap_Path);
        static Dictionary<int, string> _diagramTypes;
        static Dictionary<int, string> _definitionTypes;
        public static Dictionary<int, string> DiagramTypes
        {
            get
            {
                if (_diagramTypes == null || _diagramTypes.Count <= 0)
                {
                    _diagramTypes = GetEntityTypes(EntityType.Diagram);
                }
                return _diagramTypes;
            }
        }

        public static Dictionary<int, string> DefinitionTypes
        {
            get
            {
                if (_definitionTypes == null || _definitionTypes.Count <= 0)
                {
                    _definitionTypes = GetEntityTypes(EntityType.Definition);
                }
                return _definitionTypes;
            }
        }

        public static string GetDefinitonName(int type)
        {
            string result = GlobalStringResource.Unknown;
            if (_definitionTypes == null || _definitionTypes.Count <= 0)
            {
                _definitionTypes = GetEntityTypes(EntityType.Definition);
            }

            if (_definitionTypes.ContainsKey(type))
            {
                result = _definitionTypes[type];
            }

            return result;
        }

        public static string GetDiagramName(int type)
        {
            string result = GlobalStringResource.Unknown;
            if (_definitionTypes == null || _definitionTypes.Count <= 0)
            {
                _definitionTypes = GetEntityTypes(EntityType.Diagram);
            }

            if (_definitionTypes.ContainsKey(type))
            {
                result = _definitionTypes[type];
            }

            return result;
        }

        private static Dictionary<int, string> GetEntityTypes(EntityType type)
        {
            XDocument doc = XDocument.Load(path);

            var x = from c in doc.Descendants(type.ToString())
                    select c.DescendantNodes().ToList(); 

            Dictionary<int, string> list = new Dictionary<int, string>();

            foreach (XElement item in x.FirstOrDefault())
            {
                int id = int.Parse(item.Attribute(GlobalStringResource.Identifier).Value);
                string name = item.Attribute(GlobalStringResource.Name).Value;

                list.Add(id, name);
            }

            return list;
        }
    }
}
