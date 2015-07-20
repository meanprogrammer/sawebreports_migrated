<%@ Page Title="" Language="C#" EnableViewState="false" MasterPageFile="~/Shared/Master.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ADB.SA.Reports.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="javascript/main.js" type="text/javascript"></script>
    <script type="text/javascript">
        var filearray = [];
        function ajaxcallmaincontent(qstring) {
            $('#content').html('Loading Content ... ');
            var complete_url = "Ajax/ContentGateway.aspx?" + qstring;
            $.ajax({
                type: "POST",
                url: complete_url,
                beforeSend: function(xhr) {
                    showloader();
                },
                complete: function(xhr, status) {
                    hideloader();
                },
                success: function(result) {
                    var content = '';
                    var obj = JSON.parse(result);

                    if (obj.HomeInformation != null && obj.HomeInformation.length > 0) {
                        var source = $('#homeinfo-content').html();
                        var template = Handlebars.compile(source);
                        content = template(obj.HomeInformation);
                        $(content).insertAfter('#breadcrumb-placeholder');
                    }

                    if (obj.Page == 'page') {
                        var source = $('#breadcrumb-content').html();
                        var template = Handlebars.compile(source);
                        content = template(obj.BreadCrumbContent);
                        $('#breadcrumb-placeholder').html(content);

                        if (obj.BreadCrumbContent == null || obj.BreadCrumbContent == undefined) {
                            $('#breadcrumb-placeholder').css('display', 'none');
                        }

                        $('#caption-left').html('<h4>' + obj.Header + '</h4>');

                        if (obj.RenderType == "process") {
                            var source = $('#process-content').html();
                            var template = Handlebars.compile(source);
                            content = template(obj.Content);
                        } else if (obj.RenderType == "subprocess") {
                            var source = $('#subprocess-content').html();
                            var template = Handlebars.compile(source);
                            content = template(obj.Content);
                        } else if (obj.RenderType == "st2020") {
                            var source = $('#st2020-content').html();
                            var template = Handlebars.compile(source);
                            content = template(obj.Content);
                        } else {
                            var source = $('#generic-content').html();
                            var template = Handlebars.compile(source);
                            content = template(obj.Content);
                        }
                    } else if (obj.Page == 'home') {
                        var source = $('#home-page-content').html();
                        var template = Handlebars.compile(source);
                        content = template(obj);
                        $('#caption-left').html('<h4>' + obj.Title + '</h4>');
                    }

                    $('#content').html(content);

                    if ($("#tabs")) {
                        $("#tabs").tabs();
                    }


                    if (location.search.length > 0) {
                        if (location.search.toLowerCase().indexOf('popupid') >= 0) {
                            var popid = getQuerystring('popupid');
                            $('#activityoverview').click();
                            $('#' + popid).click();
                        }
                    }

                    $('#information-container').bind('closed.bs.alert', function() {
                        SetCookie('hide_home_box', '1', '30');
                    })

                    $('#content-tab a:first').tab('show');
                    $('[data-toggle="tooltip"]').tooltip()
                }

            });


        }

        function downloadattachment(path) {
            window.open("AttachmentDownload.aspx?path=" + path, "_blank", "width=50px,height=50px", false);
        }

        function dochangepercent() {
            $.ajax({
                type: "POST",
                url: "Ajax/resize.ashx?action=resize&percentage=" + $('#newpercentage').val() + "&id=" + $('#reportId').val(),
                complete: function(xhr, status) {
                    console.log(xhr);
                },
                success: function(result) {
                    console.log(result);
                    var randomNum = Math.ceil(Math.random() * 999999);
                    $(".diagram-containter .center-block").attr("src", result + "?v=" + randomNum);
                }

            });
        }

        function dochangepercent2() {
            $.ajax({
                type: "POST",
                url: "Ajax/resize.ashx?action=resize&percentage=" + $('#newpercentage2').val() + "&id=" + $('#reportId').val(),
                complete: function(xhr, status) {
                    console.log(xhr);
                },
                success: function(result) {
                    console.log(result);
                    var randomNum = Math.ceil(Math.random() * 999999);
                    $(".diagram-containter .center-block").attr("src", result + "?v=" + randomNum);
                }

            });
        }

        function dochangepercent3() {
            $.ajax({
                type: "POST",
                url: "Ajax/resize.ashx?action=resize&percentage=" + $('#newpercentage3').val() + "&id=" + $('#reportId').val(),
                complete: function(xhr, status) {
                    console.log(xhr);
                },
                success: function(result) {
                    console.log(result);
                    var randomNum = Math.ceil(Math.random() * 999999);
                    $(".diagram-containter .center-block").attr("src", result + "?v=" + randomNum);
                }

            });
        }

        function dochangepercent4() {
            $.ajax({
                type: "POST",
                url: "Ajax/resize.ashx?action=resize&percentage=" + $('#newpercentage4').val() + "&id=" + $('#reportId').val(),
                complete: function(xhr, status) {
                    console.log(xhr);
                },
                success: function(result) {
                    console.log(result);
                    var randomNum = Math.ceil(Math.random() * 999999);
                    $(".diagram-containter .center-block").attr("src", result + "?v=" + randomNum);
                }

            });
        }

    </script>
    <script id="generic-content" type="text/x-handlebars-template">
        <ul class="nav nav-tabs content-tab" id="content-tab">
            <li><a href="#diagram" data-toggle="tab">Diagram</a></li>
            <li><a href="#ddesc" data-toggle="tab">Diagram Desc.</a></li>
            <li><a href="#comments" data-toggle="tab">Comments</a></li>
        </ul>
        <div class="tab-content top-margin-10">
            <!-- Diagram -->
            {{#if ShowResize}}
            <div class="row">
                <div class="col-md-3">
                    <input id="newpercentage4" type="text" class="form-control input-sm show-inline" style="width:100px;" />
                    <input id="gopercentage4" name="gopercentage4" class="btn btn-primary btn-sm" onclick="dochangepercent4();" type="button" value="Proceed" />
                </div> 
                <div class="col-md-9">
                </div>
            </div>
            {{/if}}
            <div class="tab-pane" id="diagram">
                <div class="diagram-containter">
                    <img src="{{Diagram.DiagramPath}}" class="center-block" />
                </div>
            </div>
            <!-- Diagram -->
            <!-- Diagram Description -->
            <div class="tab-pane" id="ddesc">
              <table class="table table-bordered table-striped">
                  <tr>
                      <td>{{{DiagramDescription}}}</td>
                  </tr>
              </table>
            </div>
            <!-- Diagram Description -->
          <!-- Comments -->
          <div class="tab-pane" id="comments" style="margin:0px 15px 0px 15px;">
              <iframe src="CommentPage.aspx?id={{CurrentID}}" seamless="seamless"frameBorder="0" id="myif" style="width:100%;height:340px;">
              </iframe>
              <div id="comments-container">
             </div>
          </div>
          <!-- Comments -->
         </div>
         <input id="reportId" type="hidden" value="{{CurrentID}}">
    </script>
    <script id="st2020-content" type="text/x-handlebars-template">
         <ul class="nav nav-tabs content-tab" id="content-tab">
            <li><a href="#diagram" data-toggle="tab">Diagram</a></li>
            <li><a href="#ddesc" data-toggle="tab">Diagram Desc.</a></li>
            <li><a href="#chall" data-toggle="tab">Challenges</a></li>
            <li><a href="#sta" data-toggle="tab">Strategic Agenda</a></li>
            <li><a href="#doc" data-toggle="tab">Drivers of Change</a></li>
            <li><a href="#dpc" data-toggle="tab">Developing Partner Countries</a></li>
            <li><a href="#coa" data-toggle="tab">Core Operation Area</a></li>
            <li><a href="#oao" data-toggle="tab">Other Operation Area</a></li>
            <li><a href="#cv" data-toggle="tab">Corporate Values</a></li>
            <li><a href="#ogs" data-toggle="tab">Operational Goals</a></li>
            <li><a href="#igs" data-toggle="tab">Institutional Goals</a></li>
            <li><a href="#rfl" data-toggle="tab">Result Framework Level</a></li>
            <li><a href="#comments" data-toggle="tab">Comments</a></li>
         </ul>
         <div class="tab-content top-margin-10">
             <!-- Diagram -->
             {{#if ShowResize}}
                        <div class="row">
             <div class="col-md-3">             
                        <input id="newpercentage3" type="text" class="form-control input-sm show-inline" style="width:100px;" />
                        <input id="gopercentage3" name="gopercentage3" class="btn btn-primary btn-sm" onclick="dochangepercent3();" type="button" value="Proceed" />
                    </div> 
                    <div class="col-md-9">
                    </div>
               </div>
               {{/if}}
             <div class="tab-pane" id="diagram">
             
                <div class="diagram-containter">
                  <img src="{{Diagram.DiagramPath}}" class="center-block" />
                </div>
             </div>
             <!-- Diagram -->
             <!-- Diagram Description -->
             <div class="tab-pane" id="ddesc">
               <table class="table table-bordered table-striped">
                   <tr>
                       <td>{{{DiagramDescription}}}</td>
                   </tr>
               </table>
             </div>
             <!-- Diagram Description -->
             <!-- Challenges -->
             <div class="tab-pane" id="chall">
                 <table class="table table-bordered table-striped">
                    <tr>
                        <th width="50%">Challenge</th>
                        <th width="50%">Description</th>
                    </tr>
                    {{#each Challenges}}
                    <tr>
                        <td>{{{Challenge}}}</td>
                        <td>{{{Description}}}</td>
                    </tr>
                    {{/each}}
                </table>
             </div>
             <!-- Challenges -->
             <!-- Strategic Agenda -->
             <div class="tab-pane" id="sta">
                 <table class="table table-bordered table-striped">
                    <tr>
                        <th width="50%">Strategic Agenda</th>
                        <th width="50%">Description</th>
                    </tr>
                    {{#each StrategicAgendas}}
                    <tr>
                        <td>{{{StrategicAgenda}}}</td>
                        <td>{{{Description}}}</td>
                    </tr>
                    {{/each}}
                </table>
             </div>
             <!-- Strategic Agenda -->
             <!-- Drivers Of Change -->
             <div class="tab-pane" id="doc">
                 <table class="table table-bordered table-striped">
                    <tr>
                        <th width="50%">Strategic Agenda</th>
                        <th width="50%">Description</th>
                    </tr>
                    {{#each DriversOfChange}}
                    <tr>
                        <td>{{{DriversOfChange}}}</td>
                        <td>{{{Description}}}</td>
                    </tr>
                    {{/each}}
                </table>
             </div>
             <!-- Drivers Of Change -->
             <!-- Developing Partner Countries -->
             <div class="tab-pane" id="dpc">
                 <table class="table table-bordered table-striped">
                    <tr>
                        <th width="50%">Developing Partner Countries</th>
                        <th width="50%">Description</th>
                    </tr>
                    {{#each DevelopingPartners}}
                    <tr>
                        <td>{{{DevelopingPartnerCountries}}}</td>
                        <td>{{{Description}}}</td>
                    </tr>
                    {{/each}}
                </table>
             </div>
             <!-- Developing Partner Countries -->
             <!-- Core Operation Area -->
             <div class="tab-pane" id="coa">
                 <table class="table table-bordered table-striped">
                    <tr>
                        <th width="50%">Core Areas of Operation</th>
                        <th width="50%">Description</th>
                    </tr>
                    {{#each CoreAreas}}
                    <tr>
                        <td>{{{CoreAreasOfOperation}}}</td>
                        <td>{{{Description}}}</td>
                    </tr>
                    {{/each}}
                </table>
             </div>
             <!-- Core Operation Area -->
             <!-- Other Operation Area -->
             <div class="tab-pane" id="oao">
                 <table class="table table-bordered table-striped">
                    <tr>
                        <th width="50%">Other Areas of Operation</th>
                        <th width="50%">Description</th>
                    </tr>
                    {{#each OtherAreas}}
                    <tr>
                        <td>{{{OtherAreasOfOperation}}}</td>
                        <td>{{{Description}}}</td>
                    </tr>
                    {{/each}}
                </table>
             </div>
             <!-- Other Operation Area -->
             <!-- Corporate Values -->
             <div class="tab-pane" id="cv">
                 <table class="table table-bordered table-striped">
                    <tr>
                        <th width="50%">Corporate Values</th>
                        <th width="50%">Description</th>
                    </tr>
                    {{#each CorporateValues}}
                    <tr>
                        <td>{{{CorporateValues}}}</td>
                        <td>{{{Description}}}</td>
                    </tr>
                    {{/each}}
                </table>
             </div>
             <!-- Corporate Values -->
             <!-- Operational Goals -->
             <div class="tab-pane" id="ogs">
                 <table class="table table-bordered table-striped">
                    <tr>
                        <th width="50%">Operational Goals</th>
                        <th width="50%">Description</th>
                    </tr>
                    {{#each OperationalGoals}}
                    <tr>
                        <td>{{{OperationalGoals}}}</td>
                        <td>{{{Description}}}</td>
                    </tr>
                    {{/each}}
                </table>
             </div>
             <!-- Operational Goals -->
             <!-- Institutional Goals -->
             <div class="tab-pane" id="igs">
                 <table class="table table-bordered table-striped">
                    <tr>
                        <th width="50%">Institutional Goals</th>
                        <th width="50%">Description</th>
                    </tr>
                    {{#each InstitutionalGoals}}
                    <tr>
                        <td>{{{InstitutionalGoals}}}</td>
                        <td>{{{Description}}}</td>
                    </tr>
                    {{/each}}
                </table>
             </div>
             <!-- Institutional Goals -->
             <!-- Result Framework Level -->
             <div class="tab-pane" id="rfl">
                 <table class="table table-bordered table-striped">
                    <tr>
                        <th width="50%">Result Framework Level</th>
                        <th width="50%">Description</th>
                    </tr>
                    {{#each ResultFrameworks}}
                    <tr>
                        <td>{{{ResultFrameworkLevel}}}</td>
                        <td>{{{Description}}}</td>
                    </tr>
                    {{/each}}
                </table>
             </div>
             <!-- Result Framework Level -->
          <!-- Comments -->
          <div class="tab-pane" id="comments" style="margin:0px 15px 0px 15px;">
              <iframe src="CommentPage.aspx?id={{CurrentID}}" seamless="seamless"frameBorder="0" id="myif" style="width:100%;height:340px;">
              </iframe>
              <div id="comments-container">
             </div>
          </div>
          <!-- Comments -->
         </div>
         <input id="reportId" type="hidden" value="{{CurrentID}}">
    </script>
    <script id="subprocess-content" type="text/x-handlebars-template">
        <ul class="nav nav-tabs" id="content-tab">
          <li><a href="#diagram" data-toggle="tab">Diagram</a></li>
          <li><a href="#processdesc" data-toggle="tab">Process</a></li>
          <li><a href="#subprocdesc" data-toggle="tab">Sub-Process Desc.</a></li>
          {{#greaterthanzero RolesAndResponsibilities.length}}
          <li><a href="#rolesresp" data-toggle="tab">Roles and Resp.</a></li>
          {{/greaterthanzero}}
          {{#greaterthanzero Dependencies.length}}
          <li><a href="#dependencies" data-toggle="tab">Sub-Process Dependencies</a></li>
          {{/greaterthanzero}}
          {{#greaterthanzero ActivityOverviews.length}}
          <li><a href="#actoverview" data-toggle="tab">Activity Overview</a></li>
          {{/greaterthanzero}}
          {{#greaterthanzero ModuleRelationships.length}}
          <li><a href="#modrep" data-toggle="tab">Module Relationship</a></li>
          {{/greaterthanzero}}
          {{#greaterthanzero BusinessRuleMappings.length}}
          <li><a href="#busrulemap" data-toggle="tab">Business Rule Mapping</a></li>
          {{/greaterthanzero}}
          {{#greaterthanzero ChangeHistories.length}}
          <li><a href="#changehis" data-toggle="tab">Change History</a></li>
          {{/greaterthanzero}}
          <li><a href="#comments" data-toggle="tab">Comments</a></li>
        </ul>
        <div class="tab-content top-margin-10">
        {{#if ShowResize}}
        <div class="row">
             <div class="col-md-3">             
                        <input id="newpercentage2" type="text" class="form-control input-sm show-inline" style="width:100px;" />
                        <input id="gopercentage2" name="gopercentage2" class="btn btn-primary btn-sm" onclick="dochangepercent2();" type="button" value="Proceed" />
                    </div> 
                    <div class="col-md-9">
                    </div>
               </div>
          {{/if}}
          <!-- Diagram -->
          <div class="tab-pane" id="diagram">
            <div class="diagram-containter">
              <img src="{{Diagram.DiagramPath}}" class="center-block" />
            </div>
          </div>
          <!-- Diagram -->
          <!-- Process Description -->
          <div class="tab-pane" id="processdesc">
            
            <table class="table table-bordered table-striped">
                <tr>
                    <td width="15%">Process Name</td>
                    <td width="85%">Description</td>
                </tr>
                <tr>
                    <td>{{{ProcessDescription.ProcessName}}}</td>
                    <td>{{{ProcessDescription.Description}}}</td>
                </tr>
            </table>
          </div>
          <!-- Process Description -->
          <!-- Sub Process Description -->
          <div class="tab-pane" id="subprocdesc">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="100%" colspan="2">&nbsp;</th>
                </tr>
                <tr>
                    <td width="20%">Objective</td>
                    <td width="80%">{{{SubProcessDescription.Objective}}}</td>
                </tr>
                <tr>
                    <td>Document Owner(s)</td>
                    <td>{{{SubProcessDescription.DocumentOwners}}}</td>
                </tr>
                <tr>
                    <td>Framework Reference</td>
                    <td>{{{SubProcessDescription.FrameworkReference}}}</td>
                </tr>
                <tr>
                    <td>Internal Reference</td>
                    <td>{{{SubProcessDescription.InternalReference}}}</td>
                </tr>
            </table>
          </div>
          <!-- Sub Process Description -->
          <!-- Roles and Responsibilities -->
          {{#greaterthanzero RolesAndResponsibilities.length}}
          <div class="tab-pane" id="rolesresp">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="20%">Role</th>
                    <th width="80%">Responsibilities</th>
                </tr>
                {{#each RolesAndResponsibilities}}
                <tr>
                    <td>{{{Role}}}</td>
                    <td>{{{Responsibilities}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Roles and Responsibilities -->
          <!-- Sub Process Dependencies -->
          {{#greaterthanzero Dependencies.length}}
          <div class="tab-pane" id="dependencies">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="15%">Preceding / Succeeding</th>
                    <th width="30%">From Sub-Process</th>
                    <th width="30%">To Sub-Process</th>
                    <th width="25%">	Integration Objectives</th>
                </tr>
                {{#each Dependencies}}
                <tr>
                    <td>{{{Order}}}</td>
                    <td>{{{From}}}</td>
                    <td>{{{To}}}</td>
                    <td>{{{Objective}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Sub Process Dependencies -->
          <!-- Activity Overview -->
          {{#greaterthanzero ActivityOverviews.length}}
          <div class="tab-pane" id="actoverview">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="20%">Activity</th>
                    <th width="10%">User</th>
                    <th width="30%">Trigger/Input</th>
                    <th width="10%">Output</th>
                    <th width="10%">Key Documents</th>
                    <th width="10%">Systems Information</th>
                    <th width="10%">Memo</th>
                </tr>
                {{#each ActivityOverviews}}
                <tr>
                    <td>{{{Activity}}}</td>
                    <td>{{{User}}}</td>
                    <td>{{{Trigger}}}</td>
                    <td>{{{Output}}}</td>
                    <td>{{{keyDocs}}}</td>
                    <td>{{{SystemsInfo}}}</td>
                    <td>{{{Memo}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Activity Overview -->
          <!-- Module Relationship -->
          {{#greaterthanzero ModuleRelationships.length}}
          <div class="tab-pane" id="modrep">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="20%">Module</th>
                    <th width="80%">Description</th>
                </tr>
                {{#each ModuleRelationships}}
                <tr>
                    <td>{{{Module}}}</td>
                    <td>{{{Description}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Module Relationship -->
          <!-- Business Rule Mappings -->
          {{#greaterthanzero BusinessRuleMappings.length}}
          <div class="tab-pane" id="busrulemap">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="40%">Activity</th>
                    <th width="10%">Paragraph Name</th>
                    <th width="50%">Paragraph Reference</th>
                </tr>
                {{#each BusinessRuleMappings}}
                    {{#isonecount Activity.Paragraphs}}
                    <tr>
                        <td rowspan="{{Activity.RowSpan}}">{{{Activity.Activity}}}</td>
                        {{#each Activity.Paragraphs}}
                            <td>{{{ParagraphName}}}</td>
                            <td>{{{ParagraphReference}}}</td>
                        {{/each}}
                    </tr>
                    {{/isonecount}}
                    {{#greaterthanonecount Activity.Paragraphs}}
                        <tr>
                            {{#each Activity.Paragraphs}}
                                {{#indexiszero @index}}
                                    <td rowspan="{{../../Activity.RowSpan}}">{{{../../Activity.Activity}}}</td>
                                {{/indexiszero}}
                                <td>{{{ParagraphName}}}</td>
                                <td>{{{ParagraphReference}}}</td>
                                </tr>
                                <tr>
                            {{/each}}
                        </tr>
                    {{/greaterthanonecount}}
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Business Rule Mappings -->
          <!-- Change History -->
          {{#greaterthanzero ChangeHistories.length}}
          <div class="tab-pane" id="changehis">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="5%">Version</th>
                    <th width="10%">Date</th>
                    <th width="70%">Reason for Change</th>
                    <th width="15%">Author</th>
                </tr>
                {{#each ChangeHistories}}
                <tr>
                    <td>{{{Version}}}</td>
                    <td>{{{Date}}}</td>
                    <td>{{{Reason}}}</td>
                    <td>{{{Author}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Change History -->
          <!-- Comments -->
          <div class="tab-pane" id="comments" style="margin:0px 15px 0px 15px;">
              <iframe src="CommentPage.aspx?id={{CurrentID}}" seamless="seamless"frameBorder="0" id="myif" style="width:100%;height:340px;">
              </iframe>
              <div id="comments-container">
             </div>
          </div>
          <!-- Comments -->
        </div>
        <input id="reportId" type="hidden" value="{{CurrentID}}">
    </script>
    <script id="process-content" type="text/x-handlebars-template">
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" id="content-tab">
          <li><a href="#diagram" data-toggle="tab">Diagram</a></li>
          <li><a href="#processdesc" data-toggle="tab">Process Desc.</a></li>
          {{#greaterthanzero ProcessRelations.length}}
          <li><a href="#processrel" data-toggle="tab">Process Relation</a></li>
          {{/greaterthanzero}}
          {{#greaterthanzero SubProcessRelations.length}}
          <li><a href="#subprocrel" data-toggle="tab">Sub-Process Relation</a></li>
          {{/greaterthanzero}}
          {{#greaterthanzero BusinessRuleMappings.length}}
          <li><a href="#businessrule" data-toggle="tab">Business Rule Mapping</a></li>
          {{/greaterthanzero}}
          {{#greaterthanzero Acronyms.length}}
          <li><a href="#acronyms" data-toggle="tab">Acronyms</a></li>
          {{/greaterthanzero}}
          {{#greaterthanzero Applications.length}}
          <li><a href="#apprel" data-toggle="tab">Application Relationship</a></li>
          {{/greaterthanzero}}
          {{#greaterthanzero Frameworks.length}}
          <li><a href="#frameworkref" data-toggle="tab">Framework Ref.</a></li>
          {{/greaterthanzero}}
          {{#greaterthanzero InternalReferences.length}}
          <li><a href="#internalref" data-toggle="tab">Internal Ref.</a></li>
          {{/greaterthanzero}}
          <li><a href="#comments" data-toggle="tab">Comments</a></li>
        </ul>
        <!-- Tab panes -->
        <div class="tab-content top-margin-10">
          <!-- Diagram -->
          <div class="tab-pane" id="diagram">
              <div class="row" style="margin-right:10px;">
{{#if ShowResize}}
             <div class="col-md-3">             
                        <input id="newpercentage" type="text" class="form-control input-sm show-inline" style="width:100px;" />
                        <input id="gopercentage" name="gopercentage" class="btn btn-primary btn-sm" onclick="dochangepercent();" type="button" value="Proceed" />
                    </div> 
                    <div class="col-md-5">
                    </div>
 {{else}}
    <div class="col-md-8">&nbsp;</div>
 {{/if}}
                    <div class="col-md-4" style="text-align:right;">       
                    <button title="" data-placement="left" data-toggle="tooltip" class="btn btn-sm btn-default" type="button" data-original-title="To navigate to the related process and sub-process of this diagram, use the menu on the right. To navigate related informations, select the tabs above."><span class="glyphicon glyphicon-info-sign" style="font-size:18px;"></span></button>
                                            <div class="btn-group">
                                            
                          <button type="button" style="width:120px;" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">
                           <span class="glyphicon glyphicon-tasks"></span>&nbsp; Process&nbsp; <span class="caret"></span>
                          </button>
                          <ul class="dropdown-menu quicklink-override navbar-right" role="menu">
                            {{#each Diagram.RelatedProcess}}
                                <li><a href="Default.aspx?id={{ID}}">{{Name}}</a></li>
                            {{/each}}
                          </ul>
                        </div>
                        <div class="btn-group">
                          <button type="button" style="width:140px;" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">
                           <span class="glyphicon glyphicon-indent-left"></span>&nbsp; Sub-Process&nbsp; <span class="caret"></span>
                          </button>
                          <ul class="dropdown-menu quicklink-override" role="menu">
                            {{#each Diagram.RelatedSubProcess}}
                                <li><a href="Default.aspx?id={{ID}}">{{Name}}</a></li>
                            {{/each}}
                          </ul>
                        </div>
                    </div>
              </div>  
              <div class="diagram-containter">
                <img src="{{Diagram.DiagramPath}}" class="center-block" />
              </div>
          </div>
          <!-- Diagram -->
          <!-- Process Description -->
          <div class="tab-pane" id="processdesc">
            
            <table class="table table-bordered table-striped">
                <tr>
                    <td width="15%">Process Name</td>
                    <td width="85%">{{{Description.ProcessName}}}</td>
                </tr>
                <tr>
                    <td>Description</td>
                    <td>{{{Description.Description}}}</td>
                </tr>
                <tr>
                    <td>Purpose</td>
                    <td>{{{Description.Purpose}}}</td>
                </tr>
                <tr>
                    <td>Objective</td>
                    <td>{{{Description.Objective}}}</td>
                </tr>
                <tr>
                    <td>Strategy</td>
                    <td>{{{Description.Strategy}}}</td>
                </tr>
                <tr>
                    <td>Document Owner(s)</td>
                    <td>{{{Description.DocumentOwners}}}</td>
                </tr>
            </table>
          </div>
          <!-- Process Description -->
          <!-- Process Relations -->
          {{#greaterthanzero ProcessRelations.length}}
          <div class="tab-pane" id="processrel">
            
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="15%">Reference Number</th>
                    <th width="20%">Name</th>
                    <th width="65%">Relationship</th>
                </tr>
                {{#each ProcessRelations}}
                <tr>
                    <td>{{{ReferenceNumber}}}</td>
                    <td>{{{Name}}}</td>
                    <td>{{{Relationship}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Process Relations -->
          <!-- Sub Process Relations -->
          {{#greaterthanzero SubProcessRelations.length}}
          <div class="tab-pane" id="subprocrel">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="15%">Sub Process Diagram</th>
                    <th width="55%">Sub Process Overview</th>
                    <th width="15%">Sub Process Owner</th>
                    <th width="15%">Author(s)</th>
                </tr>
                {{#each SubProcessRelations}}
                <tr>
                    <td>{{{SubProcessDiagram}}}</td>
                    <td>{{{SubProcessOverview}}}</td>
                    <td>{{{SubProcessOwner}}}</td>
                    <td>{{{Authors}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Sub Process Relations -->
          <!-- Business Rule Mapping -->
          {{#greaterthanzero BusinessRuleMappings.length}}
          <div class="tab-pane" id="businessrule">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="20%">Section Name</th>
                    <th width="80%">Section Description</th>
                </tr>
                {{#each BusinessRuleMappings}}
                <tr>
                    <td>{{{SectionName}}}</td>
                    <td>{{{SectionDescription}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Business Rule Mapping -->
          
          <!-- Acronyms -->
          {{#greaterthanzero Acronyms.length}}
          <div class="tab-pane" id="acronyms">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="20%">Acronym</th>
                    <th width="80%">Description</th>
                </tr>
                {{#each Acronyms}}
                <tr>
                    <td>{{{Acronym}}}</td>
                    <td>{{{Description}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Acronyms -->
          <!-- Application Relationship -->
          {{#greaterthanzero Applications.length}}
          <div class="tab-pane" id="apprel">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="40%">Application</th>
                    <th width="60%">Description</th>
                </tr>
                {{#each Applications}}
                <tr>
                    <td>{{{Application}}}</td>
                    <td>{{{Description}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Application Relationship -->
          <!-- Framework Reference -->
          {{#greaterthanzero Frameworks.length}}
          <div class="tab-pane" id="frameworkref">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="20%">Framework</th>
                    <th width="40%">Framework Index/ID</th>
                    <th width="40%">Description</th>
                </tr>
                {{#each Frameworks}}
                <tr>
                    <td>{{{Framework}}}</td>
                    <td>{{{FrameworkID}}}</td>
                    <td>{{{Description}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Framework Reference -->
          <!-- Internal Reference -->
          {{#greaterthanzero InternalReferences.length}}
          <div class="tab-pane" id="internalref">
             
             <table class="table table-bordered table-striped">
                <tr>
                    <th width="20%">Document Type</th>
                    <th width="40%">Title</th>
                    <th width="40%">Document Reference Number</th>
                </tr>
                {{#each InternalReferences}}
                <tr>
                    <td>{{{DocumentType}}}</td>
                    <td>{{{Title}}}</td>
                    <td>{{{DocumentReferenceNumber}}}</td>
                </tr>
                {{/each}}
            </table>
          </div>
          {{/greaterthanzero}}
          <!-- Internal Reference -->
          <!-- Comments -->
          <div class="tab-pane" id="comments" style="margin:0px 15px 0px 15px;">
              <iframe src="CommentPage.aspx?id={{CurrentID}}" seamless="seamless"frameBorder="0" id="myif" style="width:100%;height:340px;">
              </iframe>
              <div id="comments-container">
             </div>
          </div>
          <!-- Comments -->
      </div>
      <input id="reportId" type="hidden" value="{{CurrentID}}">
    </script>
    <script type="text/javascript">
        Handlebars.registerHelper('is_name_equal', function(left, right, options) {
            if (left == right) {
                return options.fn(this);
            }
        });
        Handlebars.registerHelper('indexiszero', function(index, options) {
            if (index == 0) {
                return options.fn(this);
            }
        });
        Handlebars.registerHelper('greaterthanzero', function(index, options) {
            if (index > 0) {
                return options.fn(this);
            }
        });
        Handlebars.registerHelper('iszerocount', function(diagrams, options) {
            if (diagrams.length == 0) {
                return options.fn(this);
            }
        });
        Handlebars.registerHelper('isonecount', function(diagrams, options) {
            if (diagrams.length == 1) {
                return options.fn(this);
            }
        });
        Handlebars.registerHelper('greaterthanonecount', function(diagrams, options) {
            if (diagrams.length > 1) {
                return options.fn(this);
            }
        });
        Handlebars.registerHelper('getcursor', function(diagrams, options) {
            var cursor = '';
            if (diagrams.length == 0) {
                cursor = 'default !important';
            } else {
                cursor = 'pointer !important';
            }
            return cursor;
        });
        Handlebars.registerHelper('get_correct_color', function(boxcolor, bgcolor, options) {
            var realcolor = '';
            if (boxcolor != '') {
                realcolor = boxcolor;
            } else {
                realcolor = bgcolor;
            }
            return realcolor;
        });
        Handlebars.registerHelper('get_correct_fontcolor', function(fontcolor, options) {
            var realcolor = '';
            if (fontcolor != '') {
                realcolor = fontcolor;
            } else {
                realcolor = '#333333';
            }
            return realcolor;
        });
        Handlebars.registerHelper('hasattachments', function(attachments, options) {
            if (attachments.length > 0) {
                return options.fn(this);
            }
        });
        Handlebars.registerHelper('addone', function(number, options) {
            return number + 1;
        });
        Handlebars.registerHelper('getselectedclass', function(number, currentpage, options) {
            if (number == currentpage) {
                return 'active';
            }
            return '';
        });
        
    </script>
    <script id="homeinfo-content" type="text/x-handlebars-template">
<div id="information-container" class="alert alert-info alert-dismissable alert-override">
  <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
  {{this}}
</div>
    </script>
    <script id="breadcrumb-content" type="text/x-handlebars-template">
<ol class="breadcrumb">
  {{#each this}}
        <li class="{{CssClass}}">
            {{#if Link}}
                <a href="{{Link}}" class="{{CssClass}}">{{Label}}</a>
            {{else}}
                {{Label}}
            {{/if}}
            
        </li>
  {{/each}}
</ol>
    </script>
    <script id="home-page-content" type="text/x-handlebars-template">
<div class="row">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading panel-heading-override">
                    {{this.LeftGroupName}}</div>
                <div class="panel-body">
                {{#each DiagramSection.LeftGroup}}
                      <div class="panel panel-default">
                        <div class="panel-heading panel-heading-override">
                            {{Name}}</div>
                        <div class="panel-body">
                            {{#each ../SectionList}}
                                {{#is_name_equal @key ../Name}}
                                    {{#each this}}
                                        <div class="square-well" style="background-color:{{get_correct_color BoxColor ../../../Color}}; cursor: {{getcursor DiagramDTO}};">
                                            {{#iszerocount DiagramDTO}}
                                                <p style="color:{{get_correct_fontcolor FontColor}};">{{DefinitionDTO.Name}}</p>
                                            {{/iszerocount}}
                                            {{#isonecount DiagramDTO}}
                                                <a href="Default.aspx?id={{DiagramDTO.[0].ID}}" style="color:{{get_correct_fontcolor FontColor}};">{{DiagramDTO.[0].Name}}</a>
                                            {{/isonecount}}
                                            {{#greaterthanonecount DiagramDTO}}
                                                <div class="btn-group show-inline">
                                                  <a class="dropdown-toggle" data-toggle="dropdown" style="color:{{get_correct_fontcolor FontColor}};">
                                                  {{DefinitionDTO.Name}}
                                                  </a>
                                                  <ul class="dropdown-menu dropdown-menu-override" role="menu">
                                                    {{#each DiagramDTO}}
                                                        <li><a href="Default.aspx?id={{ID}}">{{Name}}</a></li>
                                                     {{/each}}
                                                  </ul>
                                                </div>
                                            {{/greaterthanonecount}}
                                        </div>
                                    {{/each}}
                                {{/is_name_equal}}
                            {{/each}}    
                        </div>
                    </div>  
                {{/each}}
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary" style="margin-left:-10px;margin-right:10px;">
                <div class="panel-heading panel-heading-override">
                    {{this.RightGroupName}}</div>
                <div class="panel-body">
                {{#each DiagramSection.RightGroup}}
                      <div class="panel panel-default">
                        <div class="panel-heading panel-heading-override">
                            {{Name}}</div>
                        <div class="panel-body">
                            {{#each ../SectionList}}
                                {{#is_name_equal @key ../Name}}
                                    {{#each this}}
                                        <div class="square-well" style="background-color:{{get_correct_color BoxColor ../../../Color}}; cursor: {{getcursor DiagramDTO}};">
                                            {{#iszerocount DiagramDTO}}
                                                <p style="color:{{get_correct_fontcolor FontColor}};">{{DefinitionDTO.Name}}</p>
                                            {{/iszerocount}}
                                            {{#isonecount DiagramDTO}}
                                                <a href="Default.aspx?id={{DiagramDTO.[0].ID}}" style="color:{{get_correct_fontcolor FontColor}};">{{DiagramDTO.[0].Name}}</a>
                                            {{/isonecount}}
                                            {{#greaterthanonecount DiagramDTO}}
                                                <div class="btn-group show-inline">
                                                  <a class="dropdown-toggle" data-toggle="dropdown" style="color:{{get_correct_fontcolor FontColor}};">
                                                  {{DefinitionDTO.Name}}
                                                  </a>
                                                  <ul class="dropdown-menu dropdown-menu-override" role="menu">
                                                    {{#each DiagramDTO}}
                                                        <li><a href="Default.aspx?id={{ID}}">{{Name}}</a></li>
                                                     {{/each}}
                                                  </ul>
                                                </div>
                                            {{/greaterthanonecount}}
                                        </div>
                                    {{/each}}
                                {{/is_name_equal}}
                            {{/each}}    
                        </div>
                    </div>  
                {{/each}}
                </div>
            </div>
        </div>
        
</div>
<input id="reportId" type="hidden" value="{{CurrentID}}">
    </script>
</asp:Content>
<asp:Content ID="Content2" EnableViewState="false" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
</asp:Content>


