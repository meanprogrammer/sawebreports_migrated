using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Data.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class SAModeHelper
    {
        private static int[] types = new int[] { 142, 111 };

        public static bool IsValidForCurrentMode(int id)
        {
            #region patch_Code

            EntityData data = new EntityData();
            EntityDTO dto = data.GetOneEntity(id);

            if (dto == null)
            {
                return false;
            }

            return dto.Publish;

            #endregion

            #region Old_Code
            //if (AppSettingsReader.GetValue(GlobalStringResource.Config_WebMode) == GlobalStringResource.CTL)
            //{
            //    return true;
            //}
            //EntityData data = new EntityData();
            //EntityDTO dto = data.GetOneEntity(id);

            //if (dto != null)
            //{
            //    dto.ExtractProperties();
            //    string category = dto.RenderHTML(GlobalStringResource.GroupCategory, 
            //        RenderOption.None);
            //    if (category == GlobalStringResource.CTL)
            //    {
            //        return false;
            //    }
            //}

            //return true;
            #endregion
        }
    }
}
