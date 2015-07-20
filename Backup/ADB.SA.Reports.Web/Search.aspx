<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Master.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ADB.SA.Reports.Web.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="SearchResultGridView" runat="server" 
    AutoGenerateColumns="False" BorderStyle="None" BorderWidth="1px" Width="90%" 
        CssClass="grid" Font-Size="13px">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="ID" 
                DataNavigateUrlFormatString="Default.aspx?id={0}" DataTextField="Name" 
                HeaderText="Diagram" ItemStyle-Width="35%" 
                NavigateUrl="Default.aspx?id={0}" >
<ItemStyle Width="35%"></ItemStyle>
            </asp:HyperLinkField>
            <asp:BoundField DataField="TypeName" ItemStyle-Width="15%" HeaderText="Type" >
<ItemStyle Width="15%"></ItemStyle>
            </asp:BoundField>
        </Columns>
    </asp:GridView>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(document).ready(function() {
        $(".grid *").highlight(GetQueryStringParams('Key'), "highlight");
    });

    function GetQueryStringParams(sParam) {
        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] == sParam) {
                return sParameterName[1];
            }
        }
    }
</script>
</asp:Content>