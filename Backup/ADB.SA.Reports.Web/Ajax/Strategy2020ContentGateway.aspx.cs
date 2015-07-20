using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADB.SA.Reports.Presenter;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Presenter.Content;
using System.Web.Script.Serialization;
using ADB.SA.Reports.Entities.DTO;
using System.IO;
using Newtonsoft.Json;

namespace ADB.SA.Reports.Web.Ajax
{
    public partial class Strategy2020ContentGateway : System.Web.UI.Page, IStrategy2020ReportView
    {
        Strategy2020Presenter presenter;

        public Strategy2020ContentGateway()
        {
            presenter = new Strategy2020Presenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string agenda = Request.Form["agenda"];
            string policy = Request.Form["policy"];
            string rule = Request.Form["rule"];
            string process = Request.Form["process"];
            string subprocess = Request.Form["subprocess"];
            string application = Request.Form["application"];
            string module = Request.Form["module"];

            var action = Request["action"];

            
            switch (action)
            {
                case "query":
                    Strategy2020Filter filter =
                        new Strategy2020Filter(agenda, policy, rule, process, subprocess, application, module);
                    presenter.CreateContent(filter);
                    //List<Strategy2020DTO> list = presenter.GetList(filter);
                    //JavaScriptSerializer s = new JavaScriptSerializer();
                    //string js = s.Serialize(list);
                    //Response.Write(js);
                    //Response.End();
                    break;
                case "filter":
                    GetAllFilters();
                    break;
                //case "agendafilter":
                //    presenter.GetFiltersForAgenda();
                //    break;
                //case "policyfilter":
                //    presenter.GetFiltersForPolicy();
                //    break;
                //case "rulefilter":
                //    presenter.GetFiltersForRule();
                //    break;
                //case "processfilter":
                //    presenter.GetFiltersForProcess();
                //    break;
                //case "subprocessfilter":
                //    presenter.GetFiltersForSubProcess();
                //    break;
                //case "modulefilter":
                //    presenter.GetFiltersForModule();
                //    break;
                //case "applicationfilter":
                //    presenter.GetFiltersForApplication();
                //    break;
                //case "report":
                //    Strategy2020Filter filter2 =
                //        new Strategy2020Filter(type, agenda, policy, rule, process, subprocess, application, module);
                //    presenter.CreateReport(filter2);
                //    break;
                case "filterthefilter":
                    FilterTheFilter();
                    break;
                default:
                    break;
            }
        }

        private void FilterTheFilter()
        {
            string column = Request.Form["column"];
            string filter = Request.Form["filter"];
            switch (column)
            {
                case "agendatopolicy":
                    HandleAgendaToPolicy(filter);
                    break;
                case "policytorule":
                    HandlePolicyToBusinessRule(filter);
                    break;
                case "ruletoprocess":
                    HandleBusinessRuleToProcess(filter);
                    break;
                case "processtosubprocessandapplication":
                    HandleProcessToSubProcess(filter);
                    break;
                //case "application":
                //    HandleProcessToApplication(filter);
                //    break;
                case "subprocesstomodule":
                    HandleSubProcessToModule(filter);
                    break;
                case "applicationtomodule":
                    HandleApplicationToModule(filter, Request.Form["secondfilter"]);
                    break;
                case "moduletosubprocessapplication":
                    HandleModuleToSubProcessAndApplication(filter);
                    break;
                case "subprocesstoprocess":
                    HandleSubProcessToProcess(filter);
                    break;
                case "applicationtoprocess":
                    HandleApplicationToProcess(filter, Request.Form["module"]);
                    break;
                case "processtorule":
                    HandleProcessToRule(filter);
                    break;
                case "ruletopolicy":
                    HandleRuleToPolicy(filter);
                    break;
                case "policytoagenda":
                    HandlePolicyToAgenda(filter);
                    break;
                default:
                    break;
            }

        }

        //RTL
        private void HandleModuleToSubProcessAndApplication(string filter)
        {
            presenter.FilterSubProcessAndApplicationByModule(filter);
        }

        private void HandleSubProcessToProcess(string filter)
        {
            presenter.FilterProcessBySubProcess(filter);
        }

        private void HandleApplicationToProcess(string filter, string extraFilter)
        {
            presenter.FilterProcessByApplication(filter, extraFilter);
        }

        private void HandleProcessToRule(string filter)
        {
            presenter.FilterRuleByProcess(filter);
        }

        private void HandleRuleToPolicy(string filter)
        {
            presenter.FilterPolicyByRule(filter);
        }

        private void HandlePolicyToAgenda(string filter)
        {
            presenter.FilterAgendaByPolicy(filter);
        }

        //LTR
        private void HandleApplicationToModule(string filter)
        {
            presenter.FilterModuleByApplication(filter);
        }

        private void HandleApplicationToModule(string filter, string secondFilter)
        {
            if (!string.IsNullOrEmpty(secondFilter) && secondFilter != "0")
            {
                presenter.FilterModuleByApplication(filter, secondFilter);
            }
            else
            {
                presenter.FilterModuleByApplication(filter);
            }
        }

        private void HandleAgendaToPolicy(string filter)
        {
             presenter.FilterPolicyByAgenda(filter);
        }

        private void HandlePolicyToBusinessRule(string filter)
        {
            presenter.FilterBusinessRuleByPolicy(filter);
        }

        private void HandleBusinessRuleToProcess(string filter)
        {
            presenter.FilterProcessByBusinessRule(filter);
        }

        private void HandleProcessToSubProcess(string filter)
        {
            presenter.FilterSubProcessByProcess(filter);
        }

        private void HandleProcessToApplication(string filter)
        {
            presenter.FilterApplicationsByProcess(filter);
        }

        private void HandleSubProcessToModule(string filter)
        {
            presenter.FilterModuleBySubProcess(filter);
        }

        private void GetAllFilters()
        {
            //Dictionary<string, List<DropdownItem>> filters = presenter.GetAllFilters();
            //JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            //string js = jsSerializer.Serialize(filters);
            //ShowContent(js);
            Dictionary<string, List<DropdownItem>> filters = presenter.GetAllFilters();
            using (StringWriter writer = new StringWriter())
            {
                //JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(writer, filters);
                ShowContent(writer.ToString());
            }
        }
        
        #region IStrategy2020ReportView Members

        public void ShowContent(string content)
        {
            Response.Write(content);
            Response.Flush();
            Response.End();
        }

        #endregion

        #region IStrategy2020ReportView Members


        public void ShowFilteredFilter(List<DropdownItem> items)
        {
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //Response.Write(serializer.Serialize(items));
            //Response.Flush();
            //Response.End();
            using (StringWriter writer = new StringWriter()) 
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, items);
                Response.Write(writer.ToString());
                Response.Flush();
                Response.End();
            }
        }

        #endregion

        #region IStrategy2020ReportView Members


        public void ShowFilteredFilter(Dictionary<string, List<DropdownItem>> items)
        {
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //Response.Write(serializer.Serialize(items));
            //Response.Flush();
            //Response.End();
            using (StringWriter writer = new StringWriter())
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, items);
                Response.Write(writer.ToString());
                Response.Flush();
                Response.End();
            }
        }

        #endregion
    }
}
