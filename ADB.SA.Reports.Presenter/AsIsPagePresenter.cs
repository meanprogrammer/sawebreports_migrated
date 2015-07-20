using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Entities.Enums;
using ADB.SA.Reports.Data.Helper;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Configuration;
using ADB.SA.Reports.Presenter.Utils;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace ADB.SA.Reports.Presenter
{
    public class AsIsPagePresenter
    {
        IDefaultView view;
        ICacheManager cache;
        public AsIsPagePresenter(IDefaultView view)
        {
            this.view = view;
            this.cache = CacheFactory.GetCacheManager();
        }

        public void RenderDetail(int id)
        {
            if (this.cache.GetData("asiscontent") != null)
            {
                string cachedContent = CacheHelper.GetFromCacheWithCheck<string>("asiscontent");
                this.view.RenderContent(cachedContent);
                return;
            }

            AsIsData asIsData = new AsIsData();
            EntityData entityData = new EntityData();
            Dictionary<string, List<AsIsItemEntity>> sectionList = asIsData.GetSections();

            StringBuilder html = new StringBuilder();

            EntityDTO mainDto = entityData.GetOneEntity(id);
            List<EntityDTO> symbols = asIsData.GetAllAsIsSymbols(id);

            foreach (EntityDTO symbol in symbols)
            {
                EntityDTO defDto = entityData.GetRelatedDefinition(symbol.ID);

                //04-07-2013
                //Added this check in order to ignore
                //symbols which doesnt have definition
                if (defDto == null)
                {
                    continue;
                }

                defDto.ExtractProperties();

                string group = defDto.RenderHTML(GlobalStringResource.ProcessModelGroup,
                    RenderOption.None);
                int itemorder = 0;
                string order = defDto.RenderHTML("Item Order",
                    RenderOption.None);

                if (!string.IsNullOrEmpty(order))
                {
                    int.TryParse(order, out itemorder);
                }

                List<EntityDTO> relatedDiagramDto = entityData.GetChildDiagrams(symbol.ID);

                if (sectionList.ContainsKey(group))
                {
                    sectionList[group].Add(
                        new AsIsItemEntity()
                    {
                        DefinitionDTO = defDto,
                        DiagramDTO = relatedDiagramDto,
                        ItemOrder = itemorder
                    });
                }
            }


            /*
             * Arrange the items per group
             * if the item order is 0 then group the zeroes the arrange them alphabetically
             * then group the ones with orders then arrange them
             * clear the original list
             * append the zeroes first then the ordered
             */
            foreach (KeyValuePair<string, List<AsIsItemEntity>> section in sectionList)
            {
                var list = section.Value;
                var zeroes = list.Where(c => c.ItemOrder == 0)
                                 .OrderBy(d=>d.DefinitionDTO.Name).ToList();
                var ordered = list.Where(x => x.ItemOrder > 0)
                                  .OrderBy(y => y.ItemOrder).ToList();
                section.Value.Clear();
                section.Value.AddRange(zeroes);
                section.Value.AddRange(ordered);
                
            }

            html.Append(string.Empty);
            html.Append(Resources.Split);
            html.Append(mainDto.Name);
            html.Append(Resources.Split);

            if (ShowInformationBox())
            {
                //adds the info box on the homepage
                html.AppendFormat("<div id=\"home-info-box\" class=\"infoBox asis-info\"><a onclick=\"SetCookie('hide_home_box','1','30');remove_element('home-info-box');\" class=\"home-close ui-icon ui-icon-closethick\">Close</a>{0}</div>", AppSettingsReader.GetValue("HOME_DESCRIPTION"));
            }

            AsIsDiagramSection diagrams = AsIsDiagramSection.GetConfig();

            RenderOuterDivs(html, diagrams.LeftGroup, sectionList);
            RenderOuterDivs(html, diagrams.RightGroup, sectionList);
            html.AppendFormat(GlobalStringResource.Presenter_ReportId_HiddenField, id);
            CacheHelper.AddToCacheWithCheck("asiscontent", html.ToString());
            view.RenderContent(html.ToString());
            
        }

        private bool ShowInformationBox()
        {
            bool result = true;
            if (SAWebContext.Request.Cookies.AllKeys.Contains("hide_home_box"))
            {
                result = false;
            }
            return result;
        }

        private static void RenderOuterDivs(StringBuilder html, TopDiagramGroupCollection collection,
            Dictionary<string, List<AsIsItemEntity>> sectionList)
        {
            html.AppendFormat(Resources.DivWithClassPropertyFormat, collection.CssClass);
            html.AppendFormat(GlobalStringResource.Presenter_AsIsHeaderFormat, GlobalStringResource.Presenter_GroupingHeaderCss, collection.Name);

            foreach (TopGroupElement element in collection)
            {
                RenderIndividualDivs(html, element.Name, sectionList[element.Name], element.Color);
            }

            html.Append(Resources.DivEndTag);
        }

        private static void RenderIndividualDivs(StringBuilder outerHtml, string key, List<AsIsItemEntity> list, string color)
        {
            outerHtml.AppendFormat(Resources.DivWithClassPropertyFormat,
                GlobalStringResource.Presenter_GroupingCss);
            outerHtml.AppendFormat(GlobalStringResource.Presenter_AsIsHeaderFormat,
                GlobalStringResource.Presenter_GroupingHeaderCss, key);

            foreach (AsIsItemEntity dto in list)
            {
                string divId = Guid.NewGuid().ToString();

                string boxcolor = dto.DefinitionDTO.RenderHTML("Symbol Box Color", RenderOption.None);
                string extendedStyle = string.Empty;
                string fontColor = dto.DefinitionDTO.RenderHTML("Symbol Font Color", RenderOption.None);
                if (!string.IsNullOrEmpty(fontColor))
                {
                    extendedStyle = string.Format(" style=\"color:{0} !important;\" ", fontColor);
                }

                if (dto.DiagramDTO.Count == 1)
                {
                    outerHtml.AppendFormat(GlobalStringResource.Presenter_AsIsDiagramItemsFormat, GlobalStringResource.Presenter_SquareCss, divId, divId, divId, (!string.IsNullOrEmpty(boxcolor)? boxcolor : color), string.Empty);
                    outerHtml.Append(string.Format("<a href=\"Default.aspx?id={0}\" {1}>{2}</a>", dto.DiagramDTO.FirstOrDefault().ID, extendedStyle,dto.DefinitionDTO.Name));
                    outerHtml.Append(Resources.DivEndTag);
                    continue;
                }

                //RoundedCornersImageManager.CreateImage(color);
                //outerHtml.AppendFormat(GlobalStringResource.Presenter_AsIsDiagramItemsFormat2, GlobalStringResource.Presenter_SquareCss, divId, divId, divId, color.Remove(0, 1));

                string additionalProperty = string.Empty;
                if (dto.DiagramDTO.Count == 0)
                {
                    additionalProperty = "cursor:default !important";
                }

                outerHtml.AppendFormat(GlobalStringResource.Presenter_AsIsDiagramItemsFormat, GlobalStringResource.Presenter_SquareCss, divId, divId, divId, (!string.IsNullOrEmpty(boxcolor) ? boxcolor : color), additionalProperty);
                outerHtml.AppendFormat("<p {0}>{1}</p>", extendedStyle,dto.DefinitionDTO.Name);
                outerHtml.Append(Resources.DivEndTag);
                StringBuilder innerHtml = new StringBuilder();
                if (dto.DiagramDTO != null && dto.DiagramDTO.Count > 0)
                {
                    innerHtml.Append(Resources.UlStartTag);
                    foreach (EntityDTO innerDto in dto.DiagramDTO.OrderBy(d=>d.Name).ToList())
                    {
                        if (SAModeHelper.IsValidForCurrentMode(innerDto.ID))
                        {
                            innerHtml.AppendFormat(
                                GlobalStringResource.Presenter_AsIsDiagramDetailItemsFormat, 
                                innerDto.ID, innerDto.ID, innerDto.Name, innerDto.Name);
                        }
                        //if (GroupFilterHelper.IsValidForShowOnHomepage(innerDto.Type, 
                        //    innerDto.ID))
                        //{
                        //    innerHtml.AppendFormat(GlobalStringResource.Presenter_AsIsDiagramDetailItemsFormat, innerDto.ID, innerDto.ID, innerDto.Name, innerDto.Name);
                        //}
                    }
                    innerHtml.Append(Resources.UlEndTag);
                    outerHtml.AppendFormat(GlobalStringResource.Presenter_DivFloatFormat, divId, GlobalStringResource.Presenter_SquareFloatCss, divId, divId, innerHtml.ToString());
                }
            }
            outerHtml.Append(Resources.DivEndTag);
        }
    }
}