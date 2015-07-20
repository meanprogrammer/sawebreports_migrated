<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentFullThread.aspx.cs" Inherits="ADB.SA.Reports.Web.CommentFullThread" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SA Web Reports - Comments Thread</title>
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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
