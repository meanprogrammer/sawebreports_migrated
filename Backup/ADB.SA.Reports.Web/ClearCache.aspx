<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClearCache.aspx.cs" Inherits="ADB.SA.Reports.Web.ClearCache" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SA Web Reports - Clear Cache</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div style="border-style: solid; border-width: 1px; position:fixed;top:20%; padding: 15px; width:400px; left:50%; margin-left:-200px;">
        <div>
            <h3 style="border-style: solid; border-width: 1px; padding: 10px; background-color: #6699FF;">
                Information</h3>
        </div>
        <div>
            <asp:Literal ID="Message" runat="server"></asp:Literal>
        </div>
        <br />
        Return <asp:LinkButton ID="Return" runat="server" PostBackUrl="~/Default.aspx">home</asp:LinkButton>
    </div>
    </div>
    </form>
</body>
</html>
