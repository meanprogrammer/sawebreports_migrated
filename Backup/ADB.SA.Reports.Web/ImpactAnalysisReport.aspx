<%@ Page Title="Impact Analysis Report" Language="C#" MasterPageFile="~/Shared/Strategy2020.Master"
    AutoEventWireup="true" CodeBehind="ImpactAnalysisReport.aspx.cs" Inherits="ADB.SA.Reports.Web.ImpactAnalysisReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="javascript/constants.js" type="text/javascript"></script>
    <link href="styles/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="javascript/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="javascript/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="javascript/bootstrap.min.js" type="text/javascript"></script>
    <script src="javascript/json2.js" type="text/javascript"></script>
    <script src="javascript/main.js" type="text/javascript"></script>
    <script src="javascript/iafilters.js" type="text/javascript"></script>
    <script type="text/javascript">
        var direction = '';

        function disabledropdown(ddid) {
            var id = arrangement[ddid];
            $('#' + id).attr('disabled', true);
        }

        function enabledropdown(ddid) {
            var id = arrangement[ddid];
            $('#' + id).removeAttr('disabled');
        }

        function toggledropdowsafterfiltering(index) {
            var dds = (direction == 'ltr' ? toggleListLTR[index] : toggleListRTL[index]);
            $.each(dds, function(key, value) {
                disabledropdown(value);
                $('#' + arrangement[value]).val('0');

            });
            $('#direction').html(direction);
        }

        function toggledropdowsafterfilteringempty(index) {
            var dds = (direction == 'ltr' ? toggleListLTR[index] : toggleListRTL[index]);
            $.each(dds, function(key, value) {
                alert('toggledropdowsafterfilteringempty ' + value);
                disabledropdown(value);
            });
            resetselected(index);
            $('#direction').html(direction);
        }

        function resetselected(index) {
            var dds = (direction == 'ltr' ? resetListLTR[index] : resetListRTL[index]);
            $.each(dds, function(key, value) {
                alert('resetselected ' + value);
                var id = arrangement[value];
                $('#' + id).val('0');
            });

            var nextBasis = (direction == 'ltr' ? nextListLTR[index] : nextListRTL[index]);
            $.each(nextBasis, function(key, value) {
                alert('nextbasis ' + value);
                $('#' + value).attr('disabled', true);
            });
        }

        function conditionalReset(id) {
            if ($('#' + id).val() != '0') {
                resetfilter(id);
            }
        }

        function agendafilterchange() {
            var agenda = $('#agendafilter').val();

            toggleenabledropdowns(true);
            determinedirection('agendafilter');
            if (direction == 'ltr') {

                if (agenda == '0' || agenda == '') {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfilteringempty('agendafilter');
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                    data: { "column": "agendatopolicy", "filter": agenda },
                    success: function(result) {
                        var obj = JSON.parse(result);
                        $('#policyfilter').html(createfilterhtml(obj));
                        toggleenabledropdowns(false);
                        toggledropdowsafterfiltering('agendafilter');
                    }
                });
            } else {
                toggleenabledropdowns(false);
            }
        }

        function policyfilterchange() {
            var policy = $('#policyfilter').val();
            toggleenabledropdowns(true);
            determinedirection('policyfilter');
            if (direction == 'ltr') {

                if (policy == '0' || policy == '') {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfilteringempty('policyfilter');
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                    data: { "column": "policytorule", "filter": policy },
                    success: function(result) {
                        var obj = JSON.parse(result);
                        $('#rulefilter').html(createfilterhtml(obj));
                        toggleenabledropdowns(false);
                        toggledropdowsafterfiltering('policyfilter');
                    }
                });
            } else {

                if (policy == '0' || policy == '') {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfilteringempty('policyfilter');
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                    data: { "column": "policytoagenda", "filter": policy },
                    success: function(result) {
                        var obj = JSON.parse(result);
                        $('#agendafilter').html(createfilterhtml(obj));
                        toggleenabledropdowns(false);
                        toggledropdowsafterfiltering('policyfilter');
                    }
                });
            }
        }

        function rulefilterchange() {
            var rule = $('#rulefilter').val();
            toggleenabledropdowns(true);
            determinedirection('rulefilter');
            if (direction == 'ltr') {

                if (rule == '0' || rule == '') {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfilteringempty('rulefilter');
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                    data: { "column": "ruletoprocess", "filter": rule },
                    success: function(result) {
                        var obj = JSON.parse(result);
                        $('#processfilter').html(createfilterhtml(obj));
                        toggleenabledropdowns(false);
                        toggledropdowsafterfiltering('rulefilter');
                    }
                });
            } else {

                if (rule == '0' || rule == '') {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfilteringempty('rulefilter');
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                    data: { "column": "ruletopolicy", "filter": rule },
                    success: function(result) {
                        var obj = JSON.parse(result);
                        $('#policyfilter').html(createfilterhtml(obj));
                        toggleenabledropdowns(false);
                        toggledropdowsafterfiltering('rulefilter');
                    }
                });
            }
        }

        function processfilterchange() {
            var process = $('#processfilter').val();
            toggleenabledropdowns(true);
            determinedirection('applicationfilter');
            if (direction == 'ltr') {

                if (process == '0' || process == '') {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfilteringempty('processfilter');
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                    data: { "column": "processtosubprocessandapplication", "filter": process },
                    success: function(result) {
                        var obj = JSON.parse(result);
                        $('#applicationfilter').html(createfilterhtml(obj['apps']));
                        $('#subprocessfilter').html('');
                        $('#subprocessfilter').html(createfilterhtml(obj['sps']));
                        toggleenabledropdowns(false);
                        toggledropdowsafterfiltering('processfilter');
                    }
                });
            } else {

                if (process == '0' || process == '') {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfilteringempty('processfilter');
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                    data: { "column": "processtorule", "filter": process },
                    success: function(result) {
                        0
                        var obj = JSON.parse(result);
                        $('#rulefilter').html(createfilterhtml(obj));
                        toggleenabledropdowns(false);
                        toggledropdowsafterfiltering('processfilter');
                    }
                });
            }
        }

        function subprocessfilterchange() {
            var subprocess = $('#subprocessfilter').val();
            toggleenabledropdowns(true);
            determinedirection('subprocessfilter');

            if (direction == 'ltr') {

                var app = $('#applicationfilter').val();

                if (parseInt(app) > 0) {
                    if (subprocess == '0' || subprocess == '') {
                        //Reset the module filter
                        resetfilter('modulefilter');
                        //enable dropdowns
                        toggleenabledropdowns(false);
                        //select which should be disabled and enabled
                        toggledropdowsafterfilteringempty('subprocessfilter');
                        //if application has selected then dont disable the 
                        //module dropdown
                        if (parseInt($('#applicationfilter').val()) > 0) {
                            //put requery here
                            $.ajax({
                                type: "POST",
                                url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                                data: { "column": "applicationtomodule", "filter": app, "secondfilter": subprocess },
                                success: function(result) {
                                    var obj = JSON.parse(result);
                                    $('#modulefilter').html(createfilterhtml(obj));
                                }
                            });


                            toggleenabledropdowns(false);
                            toggledropdowsafterfiltering('subprocessfilter');
                            $('#modulefilter').attr('disabled', false);
                        } else {

                        }
                        return;
                    }

                    $.ajax({
                        type: "POST",
                        url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                        data: { "column": "applicationtomodule", "filter": app, "secondfilter": subprocess },
                        success: function(result) {
                            var obj = JSON.parse(result);
                            $('#modulefilter').html(createfilterhtml(obj));
                            toggleenabledropdowns(false);
                            toggledropdowsafterfiltering('subprocessfilter');
                            $('#modulefilter').attr('disabled', false);
                        }
                    });
                } else {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfiltering('subprocessfilter');
                }

            } else {

                if (subprocess == '0' || subprocess == '') {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfilteringempty('subprocessfilter');
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                    data: { "column": "subprocesstoprocess", "filter": subprocess },
                    success: function(result) {
                        var obj = JSON.parse(result);
                        $('#processfilter').html(createfilterhtml(obj));
                        toggleenabledropdowns(false);
                        toggledropdowsafterfiltering('subprocessfilter');
                        if ($('#modulefilter').val() != null || $('#modulefilter').val() > 0) {
                            $('#applicationfilter').attr('disabled', true);
                        }
                    }
                });
            }
        }

        function applicationfilterchange() {
            var app = $('#applicationfilter').val();
            var secondfilter = $('#subprocessfilter').val();
            var module = $('#modulefilter').val();
            toggleenabledropdowns(true);
            determinedirection('applicationfilter');
            if (direction == 'ltr') {

                if (app == '0' || app == '') {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfilteringempty('applicationfilter');
                    $('#modulefilter').attr('disabled', true);
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                    data: { "column": "applicationtomodule", "filter": app, "secondfilter": secondfilter },
                    success: function(result) {
                        var obj = JSON.parse(result);
                        $('#modulefilter').html(createfilterhtml(obj));
                        toggleenabledropdowns(false);
                        toggledropdowsafterfiltering('applicationfilter');
                    }
                });
            } else {
                if (app == '0' || app == '') {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfilteringempty('applicationfilter');
                    $('#processfilter').attr('disabled', true);
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                    data: { "column": "applicationtoprocess", "filter": app, "module": module },
                    success: function(result) {
                        var obj = JSON.parse(result);
                        $('#processfilter').html(createfilterhtml(obj['process']));
                        $('#subprocessfilter').html(createfilterhtml(obj['sps']));
                        toggleenabledropdowns(false);
                        toggledropdowsafterfiltering('applicationfilter');
                    }
                });
            }
        }

        function modulefilterchange() {
            determinedirection('modulefilter');
            if (direction == 'rtl') {
                var module = $('#modulefilter').val();
                toggleenabledropdowns(true);

                if (module == '0' || module == '') {
                    toggleenabledropdowns(false);
                    toggledropdowsafterfilteringempty('modulefilter');
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: 'Ajax/Strategy2020ContentGateway.aspx?action=filterthefilter',
                    data: { "column": "moduletosubprocessapplication", "filter": module },
                    success: function(result) {
                        var obj = JSON.parse(result);
                        $('#applicationfilter').html(createfilterhtml(obj['apps']));
                        //Filter the sub process by the selected application
                        //then filter the subprocess based on the current 
                        //values of the process
                        //$('#subprocessfilter').html(createfilterhtml(obj['sps']));
                        toggleenabledropdowns(false);
                        toggledropdowsafterfiltering('modulefilter');
                        if (obj['apps'].length > 0) {
                            $('#applicationfilter').val(obj['apps'][0].Value);
                            applicationfilterchange();
                        }

                        //alert($('#applicationfilter').val());
                    }
                });
            }
        }

        function getDropdownSelected(name) {
            return $("select[id=" + name + "] option:selected").text();
        }

        function determinedirection(id) {
            var dir = directionlist[id];

            if (direction == '') {
                direction = dir;
            }
        }

        function toggleenabledropdowns(enable) {
            $('select').attr('disabled', enable);
        }

        function binddirection() {
            $('#direction').html(direction);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="filters">
        <table border="0" width="100%" class="table table-override" cellpadding="4">
            <tr>
                <td style="width: 100px;">
                    Agenda:
                </td>
                <td id="td_agendafilter">
                    <select id="agendafilter" class="form-control input-sm" disabled="disabled">
                    </select>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <span id="direction" style="display:none;"></span>
                </td>
                <td align="center">
                    <a id="homebutton" class="btn btn-success btn-sm" ><span class="glyphicon glyphicon-home"></span> Return Home</a>
                </td>
            </tr>
            <tr>
                <td>
                    Policy:
                </td>
                <td id="td_policyfilter">
                    <select id="policyfilter" class="form-control input-sm" disabled="disabled">
                    </select>
                </td>
                <td>
                    Business Rules:
                </td>
                <td id="td_rulefilter">
                    <select id="rulefilter" class="form-control input-sm" disabled="disabled">
                    </select>
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Process:
                </td>
                <td id="td_processfilter">
                    <select id="processfilter" class="form-control input-sm" disabled="disabled">
                    </select>
                </td>
                <td>
                    Sub-Process:
                </td>
                <td id="td_subprocessfilter" colspan="3">
                    <select id="subprocessfilter" class="form-control input-sm" disabled="disabled">
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Application:
                </td>
                <td id="td_applicationfilter">
                    <select id="applicationfilter" class="form-control input-sm" disabled="disabled">
                    </select>
                </td>
                <td>
                    Module:
                </td>
                <td id="td_modulefilter">
                    <select id="modulefilter" class="form-control input-sm" disabled="disabled">
                    </select>
                </td>
                <td>
                    <a id="Search" class="btn btn-primary btn-sm" ><span class="glyphicon glyphicon-search"></span> Search</a>
                    <a id="Reset" class="btn btn-default btn-sm" ><span class="glyphicon glyphicon-refresh"></span> Reset</a>
                    <a id="Report" class="btn btn-default btn-sm" ><span class="glyphicon glyphicon-file"></span> Report</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="ia-content" class="clearfix">
    </div>
</asp:Content>
