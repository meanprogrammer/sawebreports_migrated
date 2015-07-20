<%@ Page Language="C#" Async="true" AutoEventWireup="true" ValidateRequest="false" CodeBehind="CommentPage.aspx.cs"
    Inherits="ADB.SA.Reports.Web.CommentPage" %>

<%@ Register Assembly="Typps" Namespace="Typps" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="styles/default/default.css" rel="stylesheet" type="text/css" />
    <script src="javascript/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="javascript/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="javascript/handlebars-v1.3.0.js" type="text/javascript"></script>
    <script src="javascript/kindeditor-min.js" type="text/javascript"></script>
    <script src="javascript/en.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager" ScriptMode="Release" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="DiagramIDHiddenField" runat="server" />
        <div class="row">
            <div class="col-md-1"><span>Email </span></div>
            <div class="col-md-5"><asp:TextBox ID="EmailTextBox" runat="server" Width="300px" TabIndex="1" 
                        CssClass="form-control input-sm" style="width:100% !important;"></asp:TextBox>&nbsp;</div>
            <div class="col-md-6"><asp:FileUpload ID="AttachmentFileUpload" runat="server" TabIndex="3" 
                        CssClass="form-control input-sm" Multiple="Multiple" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-1"><span>Comment </span></div>
            <div class="col-md-5">
                <textarea id="editor_id" runat="server" name="content" cols="100" rows="8"></textarea>
                <asp:HiddenField ID="EditorValueHiddenField" runat="server" />
            </div>
            <div class="col-md-6">
                <asp:Literal ID="ValidationLiteral" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1"></div>
            <div class="col-md-11" style="padding-top:10px">
                <asp:Button ID="PostCommentButton" TabIndex="5" runat="server" Text="Post" OnClick="PostCommentButton_Click" CssClass="btn btn-primary btn-md" />
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="CancelButton_Click" CssClass="btn btn-default btn-md" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    $(document).ready(
        function() {

            getallcomments(0);

            $('#' + '<%= EmailTextBox.ClientID %>').focus();

            //HACK: to hide the menu on iframe body click
            //After months, I don't know what this does really
            $(document).on("click", function(e) {
                $('.list-holder', window.parent.document).css('display', 'none');
            });
            
            KindEditor.ready(function(K) {
                window.editor = K.create('#editor_id', {
                    langType : 'en',
                    items : [ 'bold','italic', 'underline','|','justifyleft', 'justifycenter', 'justifyright','justifyfull' ],
                    resizeType : 0,
                    height:230,
                    width:600,
                    });
            });
        }
    );

    function viewfullcomment(id) {
        window.open("CommentFullDetail.aspx?id=" + id, "_blank", "width=550px,height=300px", false);
    }

    function viewallcomments() {
        var id = $('#' + '<%= DiagramIDHiddenField.ClientID %>').val();
        window.open("CommentFullThread.aspx?id=" + id, "_blank", "", false);
    }

    function downloadattachment(path) {
        window.open("AttachmentDownload.aspx?path=" + path, "_blank", "width=50px,height=50px", false);
    }

    function getallcomments(page) {
        var id = $('#' + '<%= DiagramIDHiddenField.ClientID %>').val();
        if (page == '' || page == undefined) {
            page = 0;
        }

        $.ajax({
            type: "POST",
            url: "Ajax/commentajax.ashx",
            data: { "id": id, "page": page, "action": "getall" },
            success: function(result) {
                var obj = JSON.parse(result);
                if (obj != null) {
                    var source = $('#comments-content').html();
                    var template = Handlebars.compile(source);
                    content = template(obj);
                    window.parent.$('#comments-container').html(content)
                }

            },
            error: function() {
                alert('error retrieving comments');
            }

        });
    }

    Handlebars.registerHelper('greaterthanzero', function(index, options) {
        if (index > 0) {
            return options.fn(this);
        }
    });
    Handlebars.registerHelper('getselectedclass', function(number, currentpage, options) {
        if (number == currentpage) {
            return 'active';
        }
        return '';
    });
    Handlebars.registerHelper('hasattachments', function(attachments, options) {
        if (attachments.length > 0) {
            return options.fn(this);
        }
    });
    Handlebars.registerHelper('addone', function(number, options) {
        return number + 1;
    });
</script>
<script id="comments-content" type="text/x-handlebars-template">
        {{#greaterthanzero PageCount}}
            <ul class="pagination">
            {{#each PageCountList}}
                <li class="{{#getselectedclass this ../CurrentPage}}{{/getselectedclass}}"><a onclick="getallcomments({{this}});">{{#addone this}}{{/addone}}</a></li>
            {{/each}}
            </ul>
        {{/greaterthanzero}}        
        {{#each Comments}}
            <div class="panel panel-default panel-bottom-override">
              <div class="panel-heading panel-heading-override">{{Username}} - {{CommentDateFormatted}}</div>
              <div class="panel-body">
                {{{Comment}}}
                {{#hasattachments Attachments}}
                    <strong>Attachments:</strong>
                    {{#each Attachments}}
                        <a class="btn btn-info btn-xs" onclick="downloadattachment('{{{VirtualPath}}}')">{{FileName}}</a>
                    {{/each}}
                {{/hasattachments}}
              </div>
            </div>
        {{/each}}
        {{#greaterthanzero PageCount}}
            <ul class="pagination">
            {{#each PageCountList}}
                <li class="{{#getselectedclass this ../CurrentPage}}{{/getselectedclass}}"><a onclick="getallcomments({{this}});">{{#addone this}}{{/addone}}</a></li>
            {{/each}}
            </ul>
        {{/greaterthanzero}}     
    </script>

