using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Entities.DTO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace ADB.SA.Reports.Data
{
    public class Strategy2020Data
    {
        Database db;
        ICacheManager cache;
        public Strategy2020Data()
        {
            db = DatabaseFactory.CreateDatabase(GlobalStringResource.Data_DefaultConnection);
            cache = CacheFactory.GetCacheManager();
        }

        public Dictionary<int, int> GetSubProcessIDLookup()
        {
            IDataReader reader = db.ExecuteReader(CommandType.Text,
                GlobalStringResource.Data_SubProcess_ID_Lookup);

            Dictionary<int, int> kv = new Dictionary<int, int>();
            while (reader.Read())
            {
                kv.Add(
                    reader.GetInt32(0),
                    reader.GetInt32(1)
                    );
            }
            reader.Close();
            reader.Dispose();
            return kv;
        }

        public Dictionary<int, int> GetProcessIDLookup()
        {
            IDataReader reader = db.ExecuteReader(CommandType.Text,
                GlobalStringResource.Data_Process_ID_Lookup_NEW);

            Dictionary<int, int> kv = new Dictionary<int, int>();
            while (reader.Read())
            {
                kv.Add(
                    reader.GetInt32(0),
                    reader.GetInt32(1)
                    );
            }
            reader.Close();
            reader.Dispose();
            return kv;
        }

        public List<Strategy2020ListItemDTO> GetStrategy2020()
        {
            IDataReader reader = db.ExecuteReader(CommandType.Text,
                GlobalStringResource.Data_Strategy2020_Query);

            List<Strategy2020ListItemDTO> st2020 = new List<Strategy2020ListItemDTO>();

            while (reader.Read())
            {
                Strategy2020ListItemDTO st = new Strategy2020ListItemDTO();
                //st.InitiativeTopicID = reader.GetInt32(0);
                //st.InitiativeTopicName = reader.GetString(1);

                st.AgendaID = reader.GetInt32(0);
                st.AgendaName = reader.GetString(1);
                st.AgendaTypeID = reader.GetInt16(2);

                st.PolicyID = reader.GetInt32(3);
                st.PolicyName = reader.GetString(4);

                st.RuleID = reader.GetInt32(5);
                st.RuleName = reader.GetString(6);

                st.ProcessID = reader.GetInt32(7);
                st.ProcessName = reader.GetString(8);

                st2020.Add(st);

            }
            reader.Close();
            reader.Dispose();
            return st2020;

        }

        public List<ProcessApplicationRelation> GetStrategy2020ProcessApplicationRelation()
        {
            IDataReader reader = db.ExecuteReader(CommandType.Text,
                GlobalStringResource.Data_Strategy2020_ProcessAppRelation_Query);

            List<ProcessApplicationRelation> st2020 = new List<ProcessApplicationRelation>();

            while (reader.Read())
            {
                ProcessApplicationRelation st = new ProcessApplicationRelation();
                st.ProcessID = reader.GetInt32(0);
                st.ProcessName = reader.GetString(1);

                st.ApplicationID = reader.GetInt32(2);
                st.ApplicationName = reader.GetString(3);

                st2020.Add(st);
            }
            reader.Close();
            reader.Dispose();
            return st2020;
        }


        public List<ProcessSubProcessRelation> GetStrategy2020ProcessSubProcessRelation()
        {
            IDataReader reader = db.ExecuteReader(CommandType.Text,
                    GlobalStringResource.Data_Strategy2020ProcessToSubProcess_Query);

            List<ProcessSubProcessRelation> st2020 = new List<ProcessSubProcessRelation>();

            while (reader.Read())
            {
                ProcessSubProcessRelation st = new ProcessSubProcessRelation();
                st.ProcessID = reader.GetInt32(0);
                st.ProcessName = reader.GetString(1);

                st.SubProcessID = reader.GetInt32(2);
                st.SubProcessName = reader.GetString(3);

                st2020.Add(st);
            }
            reader.Close();
            reader.Dispose();
            return st2020;
        }

        public List<SubProcessModuleRelation> GetStrategy2020SubProcessModuleRelation()
        {
            IDataReader reader = db.ExecuteReader(CommandType.Text,
                    GlobalStringResource.Data_Strategy2020SubProcessToModule_Query);

            List<SubProcessModuleRelation> st2020 = new List<SubProcessModuleRelation>();

            while (reader.Read())
            {
                SubProcessModuleRelation st = new SubProcessModuleRelation();
                st.SubProcessID = reader.GetInt32(0);
                st.SubProcessName = reader.GetString(1);

                st.ModuleID = reader.GetInt32(2);
                st.ModuleName = reader.GetString(3);

                st2020.Add(st);
            }
            reader.Close();
            reader.Dispose();
            return st2020;
        }

        public List<ApplicationModuleRelation> GetStrategy2020ApplicationModuleRelation()
        {
            IDataReader reader = db.ExecuteReader(CommandType.Text,
                    GlobalStringResource.Data_Strategy2020_ApplicationModulesRelation_Query);

            List<ApplicationModuleRelation> st2020 = new List<ApplicationModuleRelation>();

            while (reader.Read())
            {
                ApplicationModuleRelation st = new ApplicationModuleRelation();
                st.ApplicationID = reader.GetInt32(0);
                st.ApplicationName = reader.GetString(1);

                st.ModuleID = reader.GetInt32(2);
                st.ModuleName = reader.GetString(3);

                st2020.Add(st);
            }
            reader.Close();
            reader.Dispose();
            return st2020;
        }

        public List<EntityDTO> GetAllInitiativeTopic()
        {
            return DataHelper.ExecuteReaderReturnDTO(
                db, "SELECT * FROM Entity WHERE [type] = 463 and [class] = 3 "); // and [ID] = 72393");
        }

        public List<EntityDTO> GetAllChallenges(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                string.Format(GlobalStringResource.Data_Strategy2020_GetChallenges, id));
        }

        public List<EntityDTO> GetAllStrategicAgenda(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                string.Format(GlobalStringResource.Data_Strategy2020_GetStrategicAgendas, id));
        }

        public List<EntityDTO> GetAllDevelopingPartnerCountries(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                string.Format(GlobalStringResource.Data_Strategy2020_GetDevelopingPartnerCountries, id));
        }

        public List<EntityDTO> GetAllCoreAreasOfOperations(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                string.Format(GlobalStringResource.Data_Strategy2020_GetCoreAreas_of_Operations, id));
        }

        public List<EntityDTO> GetAllOtherAreasOfOperations(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                string.Format(GlobalStringResource.Data_Strategy2020_GetOtherAreas_of_Operations, id));
        }

        public List<EntityDTO> GetAllCorporateValues(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                string.Format(GlobalStringResource.Data_Strategy2020_GetCorporateValues, id));
        }

        public List<EntityDTO> GetAllOperationalGoals(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                string.Format(GlobalStringResource.Data_Strategy2020_GetOperationalGoals, id));
        }

        public List<EntityDTO> GetAllInstitutionalGoals(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                string.Format(GlobalStringResource.Data_Strategy2020_GetInstitutional_Goals, id));
        }

        public List<EntityDTO> GetAllResultFrameworkLevel(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                string.Format(GlobalStringResource.Data_Strategy2020_GetResults_Framework_Level, id));
        }

        public List<EntityDTO> GetAllDriversOfChange(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                string.Format(GlobalStringResource.Data_Strategy2020_GetDrivers_of_Change, id));
        }

    }
}
