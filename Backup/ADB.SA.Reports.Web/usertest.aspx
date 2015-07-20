<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usertest.aspx.cs" Inherits="ADB.SA.Reports.Web.usertest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function getusername() {
            var wshshell = new ActiveXObject("wscript.shell");
            var username = wshshell.ExpandEnvironmentStrings("%username%");
            alert('hello, ' + username);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Test" />
&nbsp;<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <input id="Button2" type="button" onclick="getusername();" value="button" />
    </div>
    </form>
</body>
</html>
