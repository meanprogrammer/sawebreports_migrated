using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Utils
{
    public class MappingToolUrlHelper
    {
        public static string GenerateValidParagraphLinkMarkup(string name)
        {
            string baseName = name;
            StringBuilder baseUrl = new StringBuilder(AppSettingsReader.GetValue(GlobalStringResource.Presenter_MAPPINGTOOL_URL));

            if (name.Contains(GlobalStringResource.Presenter_OM_TYPE))
            {
                string[] removalList = new string[] { GlobalStringResource.Presenter_OP_TYPE, 
                    GlobalStringResource.Presenter_BP_TYPE, GlobalStringResource.DOT };
                for (int i = 0; i < removalList.Length; i++)
                {
                    name = name.Contains(removalList[i]) ? name.Replace(removalList[i], string.Empty) : name;
                }
            }

            string[] splitedName = name.Split(new char[] { char.Parse(GlobalStringResource.DASH) });
            string type = splitedName[0];
            baseUrl.AppendFormat(GlobalStringResource.Presenter_MappingToolUrlPartFormat, type);
            baseUrl.AppendFormat("{0}-{1}/", splitedName[0], splitedName[1]);
            baseUrl.Append(GlobalStringResource.Presenter_MAPPING_CURRENT_DIR);

            baseUrl.AppendFormat(GlobalStringResource.Presenter_MappingToolUrlPartFormat, baseName
                .Replace(GlobalStringResource.DASH, string.Empty)
                .Replace(GlobalStringResource.DOT, string.Empty));

            baseUrl.AppendFormat(GlobalStringResource.Presenter_MappingToolUrlEndPartFormat, baseName);
            return string.Format(GlobalStringResource.Presenter_ParagraphReferenceLinkMarkupFormat,
                                        baseUrl.ToString(),
                                        baseName);
        }

        public static string GenerateValidSectionLinkMarkup(string name)
        {
            string baseName = name;
            StringBuilder baseUrl = new StringBuilder(AppSettingsReader.GetValue(GlobalStringResource.Presenter_MAPPINGTOOL_URL));

            string[] splitedName = name.Split(new char[] { char.Parse(GlobalStringResource.DASH) });
            string type = splitedName[0];
            baseUrl.AppendFormat(GlobalStringResource.Presenter_MappingToolUrlPartFormat, type);
            baseUrl.AppendFormat("{0}/", name);
            baseUrl.Append(GlobalStringResource.Presenter_MAPPING_CURRENT_DIR);

            baseUrl.AppendFormat(GlobalStringResource.Presenter_MappingToolUrlPartFormat, baseName
                .Replace(GlobalStringResource.DASH, string.Empty)
                .Replace(GlobalStringResource.DOT, string.Empty));

            baseUrl.AppendFormat("{0}.html", name);
            return string.Format(GlobalStringResource.Presenter_ParagraphReferenceLinkMarkupFormat,
                                        baseUrl.ToString(),
                                        baseName);
        }

        public static string GenerateValidSectionLinkOnly(string name)
        {
            string baseName = name;
            StringBuilder baseUrl = new StringBuilder(AppSettingsReader.GetValue(GlobalStringResource.Presenter_MAPPINGTOOL_URL));

            string[] splitedName = name.Split(new char[] { char.Parse(GlobalStringResource.DASH) });
            string type = splitedName[0];
            baseUrl.AppendFormat(GlobalStringResource.Presenter_MappingToolUrlPartFormat, type);
            baseUrl.AppendFormat("{0}/", name);
            baseUrl.Append(GlobalStringResource.Presenter_MAPPING_CURRENT_DIR);

            baseUrl.AppendFormat(GlobalStringResource.Presenter_MappingToolUrlPartFormat, baseName
                .Replace(GlobalStringResource.DASH, string.Empty)
                .Replace(GlobalStringResource.DOT, string.Empty));

            baseUrl.AppendFormat("{0}.html", name);
            return baseUrl.ToString();
        }
    }
}
