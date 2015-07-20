using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Presenter.Converter;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.View;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Presenter.Content;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.UI;

namespace ADB.SA.Reports.Presenter
{
    public class Strategy2020Presenter
    {
        IStrategy2020ReportView view;
        Strategy2020FilterData filterData;
        private Dictionary<FilterType, string> filterNames;
        ICacheManager cache = null;
        public Strategy2020Presenter(IStrategy2020ReportView view)
        {
            this.view = view;
            this.filterData = new Strategy2020FilterData();
            filterNames = InitializeFilterNames();
            this.cache = CacheFactory.GetCacheManager();
        }

        private Dictionary<FilterType,string> InitializeFilterNames()
        {
 	        Dictionary<FilterType,string> names = 
                new Dictionary<FilterType,string>();
            
            names.Add(FilterType.Type, "typefilter");
            names.Add(FilterType.Agenda, "agendafilter");
            names.Add(FilterType.Policy, "policyfilter");
            names.Add(FilterType.Rule, "rulefilter");
            names.Add(FilterType.Process, "processfilter");
            names.Add(FilterType.SubProcess, "subprocessfilter");
            names.Add(FilterType.Application, "applicationfilter");
            names.Add(FilterType.Module, "modulefilter");
            return names;
        }

        public Dictionary<string, List<DropdownItem>> GetAllFilters()
        {
            Dictionary<string, List<DropdownItem>> filters = new Dictionary<string, List<DropdownItem>>();
            filters.Add("type", CreateFilterObject(FilterType.Type));
            filters.Add("agenda", CreateFilterObject(FilterType.Agenda));
            filters.Add("policy", CreateFilterObject(FilterType.Policy));
            filters.Add("rule", CreateFilterObject(FilterType.Rule));
            filters.Add("process", CreateFilterObject(FilterType.Process));
            filters.Add("subprocess", CreateFilterObject(FilterType.SubProcess));
            filters.Add("application", CreateFilterObject(FilterType.Application));
            filters.Add("module", CreateFilterObject(FilterType.Module));

            return filters;
        }

        public void GetFiltersForType()
        {
            CreateFilter(FilterType.Type);
        }

        public void GetFiltersForAgenda()
        {
            CreateFilter(FilterType.Agenda);
        }

        public void GetFiltersForPolicy()
        {
            CreateFilter(FilterType.Policy);
        }

        public void GetFiltersForRule()
        {
            CreateFilter(FilterType.Rule);
        }

        public void GetFiltersForProcess()
        {
            CreateFilter(FilterType.Process);
        }

        public void GetFiltersForSubProcess()
        {
            CreateFilter(FilterType.SubProcess);
        }

        public void GetFiltersForApplication()
        {
            CreateFilter(FilterType.Application);
        }

        public void GetFiltersForModule()
        {
            CreateFilter(FilterType.Module);
        }

        private string CreateFilter(FilterType type)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();
            List<DropdownItem> dds = new List<DropdownItem>();
            switch (type)
            {
                case FilterType.Type:
                    dds = filterData.GetValidTypeFilter(list);
                    break;
                case FilterType.Agenda:
                    dds = filterData.GetValidAgendaFilter(list);
                    break;
                case FilterType.Policy:
                    dds = filterData.GetValidPolicyFilter(list);
                    break;
                case FilterType.Rule:
                    dds = filterData.GetValidRuleFilter(list);
                    break;
                case FilterType.Process:
                    dds = filterData.GetValidProcessFilter(list);
                    break;
                case FilterType.SubProcess:
                    dds = filterData.GetValidSubProcessFilter(list);
                    break;
                case FilterType.Application:
                    dds = filterData.GetValidApplicationFilter(list);
                    break;
                case FilterType.Module:
                    dds = filterData.GetValidModuleFilter(list);
                    break;
                default:
                    break;
            }

            return CreateFilterDropdownMarkup(dds, filterNames[type]);
            //this.view.ShowContent(html);
        }

        private List<DropdownItem> CreateFilterObject(FilterType type)
        {
            if (CacheHelper.GetFromCacheWithCheck<List<DropdownItem>>(type.ToString()) != null)
            {
                return CacheHelper.GetFromCacheWithCheck<List<DropdownItem>>(type.ToString());
            }

            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();

            List<Strategy2020DTO> list = new List<Strategy2020DTO>();
            if (CacheHelper.GetFromCacheWithCheck<List<Strategy2020DTO>>("filterbaselist") != null 
                && CacheHelper.GetFromCacheWithCheck<List<Strategy2020DTO>>("filterbaselist").Count > 0) 
            {
                list = CacheHelper.GetFromCacheWithCheck<List<Strategy2020DTO>>("filterbaselist");
            }
            else
            {
                list = contentBuilder.BuildStrategy2020List();
                CacheHelper.AddToCacheForOneMinute(this.cache, "filterbaselist", list);
            }

            List<DropdownItem> dds = new List<DropdownItem>();
            switch (type)
            {
                case FilterType.Type:
                    dds = filterData.GetValidTypeFilter(list);
                    break;
                case FilterType.Agenda:
                    dds = filterData.GetValidAgendaFilter(list);
                    break;
                case FilterType.Policy:
                    dds = filterData.GetValidPolicyFilter(list);
                    break;
                case FilterType.Rule:
                    dds = filterData.GetValidRuleFilter(list);
                    break;
                case FilterType.Process:
                    dds = filterData.GetValidProcessFilter(list);
                    break;
                case FilterType.SubProcess:
                    dds = filterData.GetValidSubProcessFilter(list);
                    break;
                case FilterType.Application:
                    dds = filterData.GetValidApplicationFilter(list);
                    break;
                case FilterType.Module:
                    dds = filterData.GetValidModuleFilter(list);
                    break;
                default:
                    break;
            }

            CacheHelper.AddToCacheWithCheck(type.ToString(), dds);

            return dds;
            //this.view.ShowContent(html);
        }

        private static string CreateFilterDropdownMarkup(List<DropdownItem> dds, string name)
        {
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<select id=\"{0}\">", name);
            html.Append("<option value='0' title=''>-- ALL --</option>");
            foreach (DropdownItem d in dds)
            {
                html.AppendFormat("<option value='{0}' title='{2}'>{1}</option>", d.Value, d.Text, d.Text);
            }
            html.Append("</select>");
            return html.ToString();
        }

        public void CreateContent(Strategy2020Filter filter)
        {
            string content = CreateFilteredContent(filter);
            view.ShowContent(content);
        }

        public List<Strategy2020DTO> GetList(Strategy2020Filter filter) 
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();

            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            list = filter.ApplyFilters();


            if (filter.TypeFilter == 0 &&
                filter.AgendaFilter == 0 &&
                filter.PolicyFilter == 0 &&
                filter.RuleFilter == 0 &&
                filter.ProcessFilter == 0 &&
                filter.SubProcessFilter == 0 &&
                filter.ApplicationFilter == 0 &&
                filter.ModuleFilter == 0
                )
            {
                list = filter.ApplyDefaultFilter(list);
            }

            return list;
        }

        private string CreateFilteredContent(Strategy2020Filter filter)
        {
            Strategy2020HtmlContenBuilder htmlBuilder = new Strategy2020HtmlContenBuilder();
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            list = filter.ApplyFilters();


            if (filter.TypeFilter == 0 &&
                filter.AgendaFilter == 0 &&
                filter.PolicyFilter == 0 &&
                filter.RuleFilter == 0 &&
                filter.ProcessFilter == 0 &&
                filter.SubProcessFilter == 0 &&
                filter.ApplicationFilter == 0 &&
                filter.ModuleFilter == 0
                )
            {
                list = filter.ApplyDefaultFilter(list);
            }

            string content = htmlBuilder.BuildHtml(list);

            return content;
        }

        //public void CreateReport(Strategy2020Filter filter)
        //{
        //    string content = CreateFilteredContent(filter);


        //    //Render PlaceHolder to temporary stream
        //    System.IO.StringWriter stringWrite = new StringWriter();
        //    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        //    StringReader reader = new StringReader(content);

        //    //Create PDF document
        //    Document doc = new Document(PageSize.A4_LANDSCAPE);
        //    doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
        //    HTMLWorker parser = new HTMLWorker(doc);


        //    //StyleSheet styles = new StyleSheet();
        //    //styles.LoadTagStyle("td", "width", "50px");
        //    ////styles.LoadTagStyle("th", "face", "helvetica");
        //    ////styles.LoadTagStyle("span", "size", "8px");
        //    ////styles.LoadTagStyle("span", "face", "helvetica");
        //    ////styles.LoadTagStyle("td", "size", "8px");
        //    ////styles.LoadTagStyle("td", "face", "helvetica");
        //    ////Dictionary<string, string> w100 = new Dictionary<string,string>();
        //    ////w100.Add("width", "100px");
        //    //styles.LoadStyle("width-100", "width", "100px");
        //    //styles.LoadStyle("strategy-table-header", "width", "100px");
        //    //parser.SetStyleSheet(styles);

        //    PdfWriter.GetInstance(doc, new FileStream(@"C:\test.pdf", FileMode.Create));
        //                doc.Open();

        //    try
        //    {
        //        //Parse Html and dump the result in PDF file
        //        parser.Parse(reader);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Display parser errors in PDF.
        //        Paragraph paragraph = new Paragraph("Error!" + ex.Message);
        //        Chunk text = paragraph.Chunks[0] as Chunk;
        //        if (text != null)
        //        {
        //            text.Font.Color = BaseColor.RED;
        //        }
        //        doc.Add(paragraph);
        //    }
        //    finally
        //    {
        //        doc.Close();
        //    }

        //    //this.view.ShowContent(
        //}

        #region ImpactAnalysis_FilterFilter

        public void FilterTheFilter(string column, string type)
        { 

        }

        public void FilterPolicyByAgenda(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            List<DropdownItem> dds = filterData.GetValidPolicyFilterByAgenda(list, filterValue);
            this.view.ShowFilteredFilter(dds);
        }

        public void FilterBusinessRuleByPolicy(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            List<DropdownItem> dds = filterData.GetValidBusinessRuleFilterByPolicy(list, filterValue);
            this.view.ShowFilteredFilter(dds);
        }

        #endregion


        public void FilterProcessByBusinessRule(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            List<DropdownItem> dds = filterData.GetValidProcessFilterByBusinessRule(list, filterValue);
            this.view.ShowFilteredFilter(dds);
        }

        public void FilterSubProcessByProcess(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();
            Dictionary<string, List<DropdownItem>> spAndApps = new Dictionary<string, List<DropdownItem>>();
            List<DropdownItem> dds = filterData.GetValidSubProcessFilterByProcess(list, filterValue);
            spAndApps.Add("sps", dds);
            List<DropdownItem> apps = filterData.GetValidApplicationFilterByProcess(list, filterValue);
            spAndApps.Add("apps", apps);
            this.view.ShowFilteredFilter(spAndApps);
        }

        public void FilterModuleBySubProcess(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            List<DropdownItem> dds = filterData.GetValidModuleFilterBySubProcess(list, filterValue);
            this.view.ShowFilteredFilter(dds);
        }

        public void FilterApplicationsByProcess(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            List<DropdownItem> dds = filterData.GetValidApplicationFilterByProcess(list, filterValue);
            this.view.ShowFilteredFilter(dds);
        }

        public void FilterModuleByApplication(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            List<DropdownItem> dds = filterData.GetValidModuleFilterByApplication(list, filterValue);
            this.view.ShowFilteredFilter(dds);
        }

        public void FilterModuleByApplication(string filterValue, string secondFilterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            List<DropdownItem> dds = filterData.GetValidModuleFilterByApplication(list, filterValue, secondFilterValue);
            this.view.ShowFilteredFilter(dds);
        }

        public void FilterSubProcessAndApplicationByModule(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();
            Dictionary<string, List<DropdownItem>> spAndApps = new Dictionary<string, List<DropdownItem>>();
            List<DropdownItem> dds = filterData.GetValidSubProcessFilterByModule(list, filterValue);
            spAndApps.Add("sps", dds);
            List<DropdownItem> apps = filterData.GetValidApplicationFilterByModule(list, filterValue);
            spAndApps.Add("apps", apps);
            this.view.ShowFilteredFilter(spAndApps);
        }

        public void FilterProcessBySubProcess(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            List<DropdownItem> dds = filterData.GetValidProcessFilterBySubProcess(list, filterValue);
            this.view.ShowFilteredFilter(dds);
        }

        public void FilterProcessByApplication(string filterValue, string extraFilter)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();
            //Dictionary instance
            Dictionary<string, List<DropdownItem>> response = new Dictionary<string, List<DropdownItem>>();
            //List of processes
            List<DropdownItem> dds = filterData.GetValidProcessFilterByApplication(list, filterValue);
            //add to the dictionary
            response.Add("process", dds);
            //ids of the processes
            List<int> idList = (from ids in dds
                                select Convert.ToInt32(ids.Value)).ToList();
            //List of sub-processes
            List<DropdownItem> sps = filterData.GetValidSubprocessByProcesses(list, idList, int.Parse(extraFilter));
            //add to the dictionary
            response.Add("sps", sps);
            this.view.ShowFilteredFilter(response);
        }

        public void FilterRuleByProcess(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            List<DropdownItem> dds = filterData.GetValidRuleFilterByProcess(list, filterValue);
            this.view.ShowFilteredFilter(dds);
        }

        public void FilterPolicyByRule(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            List<DropdownItem> dds = filterData.GetValidPolicyFilterByRule(list, filterValue);

            this.view.ShowFilteredFilter(dds);
        }

        public void FilterAgendaByPolicy(string filterValue)
        {
            Strategy2020ContentBuilder contentBuilder = new Strategy2020ContentBuilder();
            List<Strategy2020DTO> list = contentBuilder.BuildStrategy2020List();

            List<DropdownItem> dds = filterData.GetValidAgendaFilterByPolicy(list, filterValue);
            this.view.ShowFilteredFilter(dds);
        }
    }

    enum FilterType
    { 
        Type = 0,
        Agenda = 1,
        Policy = 2,
        Rule = 3,
        Process = 4,
        SubProcess = 5,
        Application = 6,
        Module = 7
    }

}

