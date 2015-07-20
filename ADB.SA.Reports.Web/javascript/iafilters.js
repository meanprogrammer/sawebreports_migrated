$('#Search').click(
    function() {
        queryreport();
    }
);

$('#homebutton').click(
    function() {
        document.location.href = 'Default.aspx';
    }
);

$('#Reset').click(
    function() {
        initializefilters();

        //$('#typefilter').val('0');
        $('#agendafilter').val('0');
        $('#policyfilter').val('0');
        $('#rulefilter').val('0');
        $('#processfilter').val('0');
        $('#subprocessfilter').val('0');
        $('#applicationfilter').val('0');
        $('#modulefilter').val('0');

        $('#ia-content').html('');

        direction = '';
    }
);

$('#Report').click(
    function() {

        //var type = $('#typefilter').val();
        var agenda = $('#agendafilter').val();
        var policy = $('#policyfilter').val();
        var rule = $('#rulefilter').val();
        var process = $('#processfilter').val();
        var subprocess = $('#subprocessfilter').val();
        var application = $('#applicationfilter').val();
        var module = $('#modulefilter').val();

        //------

        //var typetext = getDropdownSelected('typefilter');
        var agendatext = getDropdownSelected('agendafilter');
        var policytext = getDropdownSelected('policyfilter');
        var ruletext = getDropdownSelected('rulefilter');
        var processtext = getDropdownSelected('processfilter');
        var subprocesstext = getDropdownSelected('subprocessfilter');
        var applicationtext = getDropdownSelected('applicationfilter');
        var moduletext = getDropdownSelected('modulefilter');

        if (agenda == null || policy == null || rule == null || process == null || subprocess == null || application == null || module == null) {
            alert('wait for the filters to load first.');
            return;
        }

        var url = 'ImpactAnalysisPdf.aspx?';
        url += 'agenda=' + agenda;
        url += '&policy=' + policy;
        url += '&rule=' + rule;
        url += '&process=' + process;
        url += '&subprocess=' + subprocess;
        url += '&application=' + application;
        url += '&module=' + module;
        url += '&ruletext=' + escape(ruletext);
        url += '&agendatext=' + escape(agendatext);
        url += '&policytext=' + escape(policytext);
        url += '&processtext=' + escape(processtext);
        url += '&subprocesstext=' + escape(subprocesstext);
        url += '&applicationtext=' + escape(applicationtext);
        url += '&moduletext=' + escape(moduletext);

        window.open(url);
        return false;
    }
);

function queryreport() {
    var agenda = $('#agendafilter').val();
    var policy = $('#policyfilter').val();
    var rule = $('#rulefilter').val();
    var process = $('#processfilter').val();
    var subprocess = $('#subprocessfilter').val();
    var application = $('#applicationfilter').val();
    var module = $('#modulefilter').val();

    if (agenda == null || policy == null || rule == null || process == null || subprocess == null || application == null || module == null) {
        alert('wait for the filters to load first.');
        return;
    }

    $('#ia-content').html('<h3>Loading content ...</h3>');

    $.ajax({
        type: "POST",
        url: 'Ajax/Strategy2020ContentGateway.aspx?action=query',
        data: { "agenda": agenda, "policy": policy, "rule": rule, "process": process, "subprocess": subprocess, "application": application, "module": module },
        beforeSend: function(xhr) {
            showloader();
        },
        complete: function(xhr, status) {
            hideloader();
        },
        success: function(result) {
            $('#ia-content').html(result);

            var topheight = $('#ia-header').height() + $('#orange-line').height() + $('#filters').height() + $('#float-header').height();
            var remaining = $(document).height() - topheight;
            $('#ia-holder').height(remaining - 20);

            toggleenabledropdowns(true);
        }
    });
}


$(document).ready(
    function() {
        //$('input[type=button]').button();
        $('html').css('overflow', 'hidden');
        initializefilters();
        $('#agendafilter').change(agendafilterchange);
        $('#policyfilter').change(policyfilterchange);
        $('#rulefilter').change(rulefilterchange);
        $('#processfilter').change(processfilterchange);
        $('#subprocessfilter').change(subprocessfilterchange);
        $('#applicationfilter').change(applicationfilterchange);
        $('#modulefilter').change(modulefilterchange);
    }
);


function initializefilters() {
    //$('#ia-content').html('<center><strong>Loading Filters ...</strong></center>');
    $.ajax({
        type: "POST",
        cache: true,
        url: 'Ajax/Strategy2020ContentGateway.aspx?action=filter',
        beforeSend: function(xhr) {
            showloader();
        },
        complete: function(xhr, status) {
            hideloader();
        },
        success: function(result) {
            var filters = JSON.parse(result);

            $('#agendafilter').html(createfilterhtml(filters.agenda));
            $('#agendafilter').removeAttr('disabled');

            $('#policyfilter').html(createfilterhtml(filters.policy));
            $('#policyfilter').removeAttr('disabled');

            $('#rulefilter').html(createfilterhtml(filters.rule));
            $('#rulefilter').removeAttr('disabled');

            $('#processfilter').html(createfilterhtml(filters.process));
            $('#processfilter').removeAttr('disabled');

            $('#subprocessfilter').html(createfilterhtml(filters.subprocess));
            $('#subprocessfilter').removeAttr('disabled');

            $('#applicationfilter').html(createfilterhtml(filters.application));
            $('#applicationfilter').removeAttr('disabled');

            $('#modulefilter').html(createfilterhtml(filters.module));
            $('#modulefilter').removeAttr('disabled');

            //$('#ia-content').html('');
        }
    });

}


function resetfilter(filter) {
    $.ajax({
        type: "POST",
        cache: true,
        url: 'Ajax/Strategy2020ContentGateway.aspx?action=filter',
        success: function(result) {
            var filters = JSON.parse(result);
            var data;
            switch (filter) {
                case 'agendafilter':
                    data = filters.agenda;
                    break;
                case 'policyfilter':
                    data = filters.policy;
                    break;
                case 'rulefilter':
                    data = filters.rule;
                    break;
                case 'processfilter':
                    data = filters.process;
                    break;
                case 'subprocessfilter':
                    data = filters.subprocess;
                    break;
                case 'applicationfilter':
                    data = filters.application;
                    break;
                case 'modulefilter':
                    data = filters.module;
                    break;
                default:
                    break;
            }

            $('#' + filter).html(createfilterhtml(data));
            //$('#' + filter).removeAttr('disabled');
        }
    });
}

function createfilterhtml(array) {
    var dropdown = '';
    dropdown += "<option value='0' title=''>-- ALL --</option>";

    $.each(array, function(key, value) {
        dropdown += "<option value='" + value.Value + "' title='" + value.Text + "'>" + value.Text + "</option>";
    });
    return dropdown;
}