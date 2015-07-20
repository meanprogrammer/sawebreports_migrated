<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FullSPCycle.aspx.cs" Inherits="ADB.SA.Reports.Web.FullSPCycle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        html {
            overflow-x: scroll !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:DropDownList ID="SubProcessDropDownList" runat="server" Width="350px">
        <asp:ListItem Value="106571">BC-040-000 Approve Document</asp:ListItem>
        <asp:ListItem Value="170160">CE-010-000 Define Consulting Need</asp:ListItem>
        <asp:ListItem Value="160843">IS-121-000 Develop / Revise / Cancel Mission Plan</asp:ListItem>
        <asp:ListItem Value="194205">SM-010-000 Raise/Change/Cancel Service Request</asp:ListItem>
        <asp:ListItem Value="188719">RC-010-000 Initiate/Raise Change Request</asp:ListItem>
    
    </asp:DropDownList>&nbsp;
    <asp:CheckBox ID="WithImageCheckBox" runat="server" Text=" With Image" />&nbsp;
    <asp:Button ID="GoButton" runat="server" Text="Go" onclick="GoButton_Click" 
        Width="50px" />
    <div>
        <asp:Literal ID="FullLiteral" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
