<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentFullDetail.aspx.cs" Inherits="ADB.SA.Reports.Web.CommentFullDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SA Web Reports - Comment Detail</title>
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
    <script src="javascript/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(
            function() {
                $('.comment-item').css('width', '50%');
            }
        );
        
        function downloadattachment(path) {
            window.open("AttachmentDownload.aspx?path=" + path, "_blank", "width=50px,height=50px", false);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="ContentLiteral" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
