using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;
using System.Data.Common;
using ADB.SA.Reports.Configuration;

namespace ADB.SA.Reports.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityData
    {
        /// <summary>
        /// 
        /// </summary>
        Database db;

        /// <summary>
        /// 
        /// </summary>
        public EntityData()
        {
            db = DatabaseFactory.CreateDatabase(GlobalStringResource.Data_DefaultConnection);
        }

        public EntityDTO GetOneEntity(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                GlobalStringResource.Data_GetOneEntityQuery).FirstOrDefault();
        }

        public EntityDTO GetOneEntity(string name)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, name,
                 GlobalStringResource.Data_GetOneEntityByNameQuery).FirstOrDefault();
        }

        public EntityDTO GetOneEntityByNameAndClass(string name, int cls)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, new object[]{ DataHelper.FixString(name), cls },
                 GlobalStringResource.Data_GetOneEntityByNameAndClassQuery).FirstOrDefault();
        }

        public EntityDTO GetOneEntityByReferenceNumberAndClass(string name, int cls)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, new object[] { DataHelper.FixString(name), cls },
                 GlobalStringResource.Data_GetOneEntityByReferenceNumberAndClass).FirstOrDefault();
        }

        public EntityDTO GetOneEntityByNameAndType(string name, int type)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, new object[] { DataHelper.FixString(name), type },
                 GlobalStringResource.Data_GetOneEntityByNameAndType).FirstOrDefault();
        }

        public virtual int GetPoolCount(int id)
        {
            int count = 0;
            count = (int)db.ExecuteScalar(CommandType.Text,
                string.Format(GlobalStringResource.Data_GetPoolCountQuery, id));
            return count;
        }

        public virtual List<EntityDTO> GetBookmarks(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetBookmarksQuery);
        }

        public virtual List<EntityDTO> GetDefinitionOfTerms(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetDefinitionOfTermsQuery);
        }

        public virtual List<EntityDTO> GetRelatedProcess(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id, 
                GlobalStringResource.Data_GetRelatedProcess);
        }

        public virtual List<EntityDTO> GetRelatedSubProcess(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id, 
                GlobalStringResource.Data_GetRelatedSubProcess);
        }

        public virtual List<EntityDTO> GetRolesAndResponsibilities(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id, 
                GlobalStringResource.Data_GetRolesAndResponsibilities);
        }

        public virtual EntityDTO GetRolesDescription(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id, 
                GlobalStringResource.Data_GetRolesDescription).FirstOrDefault();
        }

        public virtual EntityDTO GetRoleDetail(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id, GlobalStringResource.Data_GetRoleDetail).FirstOrDefault();
        }

        public virtual List<EntityDTO> GetReviewersAndApprovers(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id, 
                GlobalStringResource.Data_GetReviewersAndApprovers);
        }

        public virtual List<EntityDTO> GetAcronyms(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id, 
                GlobalStringResource.Data_GetAcronyms);
        }

        public virtual List<EntityDTO> GetSections(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id, 
                GlobalStringResource.Data_GetSections);
        }

        public virtual List<EntityDTO> GetChangeHistory(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                                    GlobalStringResource.Data_GetChangeHistoryQuery);
        }

        public virtual List<EntityDTO> GetApplicationRelationship(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                                    GlobalStringResource.Data_GetApplicationRelationshipQuery);
        }

        public virtual List<EntityDTO> GetModuleRelationship(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                                    GlobalStringResource.Data_GetModuleRelationshipQuery);
        }

        public virtual List<EntityDTO> GetFrameworkReference(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                                    GlobalStringResource.Data_GetFrameworkReferenceQuery);
        }

        public virtual List<EntityDTO> GetInternalReference(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                                    GlobalStringResource.Data_GetInternalReferenceQuery);
        }

        //public virtual List<EntityDTO> GetRelatedEntities(int id)
        //{
        //    return DataHelper.ExecuteReaderReturnDTO(db, id,
        //                            GlobalStringResource.Data_GetRelatedEntitiesQuery);
        //}

        public List<EntityDTO> GetSubProcessDependencies(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetSubProcessDependenciesQuery);
        }

        public List<EntityDTO> GetActivityOverview(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetActivityOverviewQuery);
        }

        public List<EntityDTO> GetSubProcessBusinessMapping(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetSubProcessBusinessMappingQuery);
        }

        public List<EntityDTO> GetSubProcessParagraphs(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetSubProcessParagraphs);
        }

        public List<EntityDTO> GetUseCases(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetUseCasesQuery);
        }

        public List<EntityDTO> GetActivityVariations(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetActivityVariationsQuery);
        }

        public List<EntityDTO> GetKeyDocuments(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetKeyDocumentsQuery);
        }

        public List<EntityDTO> GetRelatedUsers(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id, GlobalStringResource.Data_GetRelatedUsersQuery);
        }

        public EntityDTO GetActivityOverviewParent(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                                GlobalStringResource.Data_GetActivityOverviewParentQuery)
                                .FirstOrDefault();
        }

        public List<EntityDTO> GetRelatedPersons(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                GlobalStringResource.Data_GetRelatedPersonsQuery);
        }

        public EntityDTO GetRelatedDefinition(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                                GlobalStringResource.Data_GetRelatedDefinitionQuery).FirstOrDefault();
        }

        public List<EntityDTO> GetChildDiagrams(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                GlobalStringResource.Data_GetRelatedDiagramQuery);
        }

        public EntityDTO GetParentDiagram(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetParentDiagramQuery).FirstOrDefault();
        }

        public List<EntityDTO> GetBusinessRules(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetBusinessRulesQuery);
        }

        public List<EntityDTO> GetRequiredData(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                            GlobalStringResource.Data_GetRequiredDataQuery);
        }

        public List<EntityDTO> GetSampleReference(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db, id,
                GlobalStringResource.Data_GetSampleReferenceQuery);
        }

        //TODO: Revisit
        public EntityDTO GetActualRelatedProcess(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                id, GlobalStringResource.Data_GetActualRelatedProcess).FirstOrDefault();
        }

        public List<EntityDTO> GetControlTypesCtl(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                id, GlobalStringResource.Data_GetControlTypesCtl);
        }

        public EntityDTO GetControlRelatedRisk(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                id, GlobalStringResource.Data_GetControlRelatedRisk).FirstOrDefault();
        }

        public List<EntityDTO> GetCtlSubProcess(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                id, GlobalStringResource.Data_GetCtlSubProcess);
        }

        public List<SearchResultDTO> SearchEntity(string filter)
        {
            List<SearchResultDTO> results = new List<SearchResultDTO>();

            MenuFilterSection menu = MenuFilterSection.GetConfig();
            List<string> ids = menu.GetItemsToBeRemove();

            string notin = string.Join(",", ids.ToArray());

            DbCommand cmd = db.GetSqlStringCommand(
                string.Format(GlobalStringResource.Data_SearchEntity, filter, notin)
                );

            IDataReader reader = db.ExecuteReader(cmd);
            while (reader.Read())
            {
                SearchResultDTO dto = new SearchResultDTO();
                dto.ID = reader.GetInt32(0);
                dto.Name = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                dto.Type = reader.GetInt16(2);
                dto.TypeName = TypesReader.DiagramTypes[dto.Type];

                string prop = string.Empty;

                if (!reader.IsDBNull(3) && !string.IsNullOrEmpty(reader.GetString(3)))
                {
                    prop = reader.GetString(3);
                }
                else
                {
                    prop = reader.GetString(4);
                }

                dto.Properties = prop; //string.Format("<div class=\"accordion\"><h3>Properties Match</h3><div>{0}</div><div>", prop);

                results.Add(dto);
            }

            reader.Close();
            reader.Dispose();

            return results;

        }

        public List<EntityDTO> GetRelatedBusinessUnit(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                id, GlobalStringResource.Data_GetRelatedBusinessUnit);
        }

        public List<EntityDTO> GetRelatedControlOwner(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                id, GlobalStringResource.Data_GetRelatedControlOwner); ;
        }

        public List<EntityDTO> GetRelatedControlObjectives(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                id, GlobalStringResource.Data_GetRelatedControlObjectives); ;
        }

        public List<EntityDTO> GetRelatedFrequency(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                id, GlobalStringResource.Data_GetRelatedFrequency); ;
        }

        public List<EntityDTO> GetRelatedControlApplications(int id)
        {
            return DataHelper.ExecuteReaderReturnDTO(db,
                id, GlobalStringResource.Data_GetRelatedControlApplications); ;
        }
    }
}