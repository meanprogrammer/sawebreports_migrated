using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Entities.Utils;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Entities.DTO
{
    /// <summary>
    /// The class that represents a row in entity table.
    /// </summary>
    public class EntityDTO
    {
        /// <summary>
        /// Audit column.
        /// </summary>
        public string Audit { get; set; }

        /// <summary>
        /// Class column.
        /// </summary>
        public int Class { get; set; }

        /// <summary>
        /// FromArrow column.
        /// </summary>
        public bool FromArrow { get; set; }

        /// <summary>
        /// FromAssc column.
        /// </summary>
        public int FromAssc { get; set; }

        /// <summary>
        /// ID column.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name column.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// SeqNum column.
        /// </summary>
        public int SeqNum { get; set; }

        /// <summary>
        /// Properties column.
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// ToArrow column.
        /// </summary>
        public bool ToArrow { get; set; }

        /// <summary>
        /// ToAssc column.
        /// </summary>
        public int ToAssc { get; set; }

        /// <summary>
        /// Type column.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// UpdateDate column.
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// SAGuid column.
        /// </summary>
        public string SAGuid { get; set; }

        /// <summary>
        /// PropType column.
        /// </summary>
        public int PropType { get; set; }

        /// <summary>
        /// ShortProps column.
        /// </summary>
        public string ShortProps { get; set; }

        /// <summary>
        /// Contains the parsed ShortProperties.
        /// </summary>
        public List<PropertiesDTO> ShortProperties { get; private set; }

        /// <summary>
        /// Contains the parsed Properties.
        /// </summary>>
        public List<PropertiesDTO> MainProperties { get; private set; }

        /// <summary>
        /// The Parent Entity
        /// </summary>
        public EntityDTO ParentDTO { get; set; }

        /// <summary>
        /// Returns the Diagram percentage property from the properties.
        /// </summary>
        public float DiagramPercentage
        {
            get {
                float result = 0;
                float.TryParse(this.RenderHTML("Diagram Percentage", RenderOption.None), out result);
                return result;
            }
        }

        /// <summary>
        /// Return if the entity mush be shown (Publish == true [for show]).
        /// </summary>
        public bool Publish
        {
            get
            {
                this.ExtractProperties();
                var value = this.RenderHTML("Publish", RenderOption.None);
                if (value == "T")
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsCTL
        {
            get
            {
                this.ExtractProperties();
                string value = this.RenderHTML(GlobalStringResource.GroupCategory, RenderOption.None);
                if (value == GlobalStringResource.CTL)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsValidForCurrentModule { get; set; }

        /// <summary>
        /// Returns the WMF filename.
        /// </summary>
        public string DGXFileName
        {
            get
            {
                string key = "DGX File Name";
                string value = string.Empty;
                if (!string.IsNullOrEmpty(this.Properties))
                {
                    if (this.MainProperties.FirstOrDefault(x => x.Key == key) != null)
                    {
                        value = this.MainProperties.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
                    }
                }
                else
                {
                    if (this.ShortProperties.FirstOrDefault(x => x.Key == key) != null)
                    {
                        value = this.ShortProperties.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
                    }
                }

                return value.Replace(".DGX", ".wmf");
            }
        }

        /// <summary>
        /// Returns the Svg filename.
        /// </summary>
        public string SvgFileName
        {
            get
            {
                string key = "DGX File Name";
                string value = string.Empty;
                if (!string.IsNullOrEmpty(this.Properties))
                {
                    if (this.MainProperties.FirstOrDefault(x => x.Key == key) != null)
                    {
                        value = this.MainProperties.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
                    }
                }
                else
                {
                    if (this.ShortProperties.FirstOrDefault(x => x.Key == key) != null)
                    {
                        value = this.ShortProperties.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
                    }
                }

                return value.Replace(".DGX", ".svg");
            }
        }

        /// <summary>
        /// Extracts the string type property to list.
        /// </summary>
        public void ExtractProperties()
        {
            if (!string.IsNullOrEmpty(this.ShortProps))
            {
                this.ShortProperties = PropertiesReader.ReadShortProperties(this.ShortProps);
            }

            if (!string.IsNullOrEmpty(this.Properties))
            {
                this.MainProperties = PropertiesReader.ReadShortProperties(this.Properties);
            }
        }




        public string RenderHTML(string id, RenderOption option)
        {
            List<string> values = GetPropertyList(id);

            if (values != null && values.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                for (int i = 0; i < values.Count; i++)
                {
                    if (!string.IsNullOrEmpty(values[i]))
                    {
                        string template = string.Empty;

                        switch (option)
                        {
                            case RenderOption.List:
                                template = "<li>{0}</li>";
                                break;
                            case RenderOption.Paragraph:
                                template = "<p>{0}</p>";
                                break;
                            case RenderOption.Span:
                                template = "<span>{0}</span>";
                                break;
                            case RenderOption.Break:
                                template = "{0}<br>";
                                break;
                            case RenderOption.None:
                                template = "{0}";
                                break;
                            case RenderOption.NewLine:
                                template = string.Concat("{0}", Environment.NewLine);
                                break;
                            default:
                                break;
                        }

                        html.Append(string.Format(template, values[i]));
                    }
                }
                return html.ToString();
            }
            return string.Empty;
        }

        public List<string> GetPropertyList(string id)
        {
            List<string> values = null;
            if (!string.IsNullOrEmpty(this.Properties))
            {
                if (this.MainProperties != null && this.MainProperties.FirstOrDefault(x => x.Key == id) != null)
                {
                    values = this.MainProperties.FirstOrDefault(x => x.Key == id).Value;
                }
            }
            else
            {
                if (this.ShortProperties != null && this.ShortProperties.FirstOrDefault(x => x.Key == id) != null)
                {
                    values = this.ShortProperties.FirstOrDefault(x => x.Key == id).Value;
                }
            }
            return values;
        }


    }
}
