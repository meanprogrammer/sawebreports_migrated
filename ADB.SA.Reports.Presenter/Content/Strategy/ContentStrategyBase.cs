using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Data;
using System.IO;
using ADB.SA.Reports.Utilities.WMF;
using ADB.SA.Reports.Entities.Utils;
using ADB.SA.Reports.Utilities.HtmlHelper;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter.Content
{
    public abstract class ContentStrategyBase
    {
        public abstract string BuildContent(EntityDTO dto);
        protected abstract string BuildDiagramDescription(EntityDTO dto);
        protected abstract string BuildProcessRelation(int id);
        protected abstract string BuildSubProcessRelation(int id);
        protected abstract string BuildBusinessMapping(int id);
        protected abstract string BuildAcronyms(int id);
        protected abstract string BuildReviewersAndApprovers(int id);
        protected abstract string BuildApplicationRelationship(int id);
        protected abstract string BuildFrameworkReference(int id);
        protected abstract string BuildInternalReference(int id);
        protected abstract string BuildModuleRelationship(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual string BuildTitle(string name)
        {
            //StringBuilder html = new StringBuilder();
            //html.AppendFormat(GlobalStringResource.Presenter_HeaderFormat, name);
            return name; //html.ToString();
        }

        //protected virtual string BuildRefresh(int id)
        //{ 
        //    StringBuilder html = new StringBuilder();
        //    html.AppendFormat("<input id=\"refreshId\" type=\"hidden\" value=\"{0}\">", id);
        //    return html.ToString();
        //}

        protected virtual string BuildReportHiddenField(int id)
        {
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<input id=\"reportId\" type=\"hidden\" value=\"{0}\">", id);
            return html.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual string BuildDiagramImage(EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();
            EntityData data = new EntityData();
            FileData files = new FileData();
            

            FileDTO file = files.GetFile(dto.DGXFileName);
            byte[] imageBytes = file.Data;

            string path = string.Format("{0}_{1}", file.Date.ToFileTime().ToString(), dto.DGXFileName);

            int poolCount = data.GetPoolCount(dto.ID);
            WmfImageManager imageManager = new WmfImageManager(dto, imageBytes,
                path, dto.Type, poolCount, false);
            path = imageManager.ProcessImage();


            html.AppendFormat(GlobalStringResource.Presenter_BuildDiagramImage_Tag, path.Replace(@"\", @"/"));
            html.Append(GlobalStringResource.BreakTag);
            return html.ToString();
        }

        protected virtual string BuildRolesAndResponsibilities(int id)
        {
            string result = string.Empty;
            EntityData entityData = new EntityData();
            List<EntityDTO> rolesAndResponsibilities = entityData.GetRolesAndResponsibilities(id);

            if (rolesAndResponsibilities.Count > 0)
            {
                HtmlTable t = new HtmlTable(2, 0, "grid", new int[] { 20, 80 } );

                t.AddHeader(GlobalStringResource.Role);
                t.AddHeader(GlobalStringResource.Responsibilities);

                foreach (EntityDTO dto in rolesAndResponsibilities)
                {
                    dto.ExtractProperties();
                    EntityDTO descriptionDto = entityData.GetRolesDescription(dto.ID);
                    string description = string.Empty;
                    if (descriptionDto != null)
                    {
                        descriptionDto.ExtractProperties();
                        description = descriptionDto.RenderHTML(GlobalStringResource.Description,
                            RenderOption.Break);
                    }
                    t.AddCell(dto.RenderHTML(GlobalStringResource.Role, RenderOption.None));//(Resources.Role, RenderOption.Paragraph));
                    t.AddCell(description);
                }
                result = t.EndHtmlTable();
            }

            return result;
        }

        protected virtual string BuildChangeHistory(int id)
        {
            string result = string.Empty;
            EntityData entityData = new EntityData();

            List<EntityDTO> changeHistory = entityData.GetChangeHistory(id);
            ChangeHistoryComparer comparer = new ChangeHistoryComparer();
            changeHistory.Sort(comparer);
            if (changeHistory.Count > 0)
            {
                HtmlTable t = new HtmlTable(4, 0, "grid");
                //t.AddHeader(GlobalStringResource.ChangeHistory, 4);

                t.AddHeader(GlobalStringResource.Version);
                t.AddHeader(GlobalStringResource.Date);
                t.AddHeader(GlobalStringResource.ReasonforChange);
                t.AddHeader(GlobalStringResource.AuthorOnly);

                foreach (EntityDTO related in changeHistory)
                {
                    related.ExtractProperties();
                    t.AddCell(related.RenderHTML(GlobalStringResource.Version, RenderOption.Span));
                    t.AddCell(related.RenderHTML(GlobalStringResource.Date, RenderOption.Span));
                    t.AddCell(related.RenderHTML(GlobalStringResource.ReasonforChange, RenderOption.Span));

                    List<EntityDTO> users = entityData.GetRelatedPersons(related.ID);

                    StringBuilder userLinks = new StringBuilder();
                    if (users.Count > 0)
                    {
                        foreach (EntityDTO user in users)
                        {
                            userLinks.Append(user.RenderAsPopupLink());
                            userLinks.Append(GlobalStringResource.BreakTag);
                        }
                    }

                    t.AddCell(userLinks.ToString());
                }
                result = t.EndHtmlTable();
            }

            return result;
        }
    }
}
