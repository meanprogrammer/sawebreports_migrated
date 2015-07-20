using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Data
{
    public class EndToEndSubProcessData
    {
        Database db;
        public EndToEndSubProcessData() {
            db = DatabaseFactory.CreateDatabase(GlobalStringResource.Data_DefaultConnection);
        }

        public List<EntityDTO> GetAllSucceedingEvent(int baseDiagramId)
        {
            var list = DataHelper.ExecuteReaderReturnDTO(
                this.db,
                baseDiagramId,
                GlobalStringResource.Data_GetAllSucceedingEvents
                    );

            return list.Where(c=> (IsSucceeding(c) == true)).ToList();
        }

        private bool IsSucceeding(EntityDTO dto)
        {
            dto.ExtractProperties();
            string e_type = dto.RenderHTML(GlobalStringResource.EventType, ADB.SA.Reports.Entities.Enums.RenderOption.None);
            if (e_type.ToLower().Trim() == "succeeding")
            {
                return true;
            }
            return false;
        }

        public List<EntityDTO> GetRelatedDiagram(int eventId)
        {
            return DataHelper.ExecuteReaderReturnDTO(
                this.db,
                eventId,
                GlobalStringResource.Data_EndToEndGetRelatedDiagram
                );

        }
    }
}
