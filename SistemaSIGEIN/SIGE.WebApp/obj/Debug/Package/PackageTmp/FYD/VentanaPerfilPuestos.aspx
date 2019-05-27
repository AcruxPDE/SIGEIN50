<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaPerfilPuestos.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaPerfilPuestos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="clear:both; height:10px;"></div>
    <telerik:RadGrid runat="server" ID="grdpuestos" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdpuestos_NeedDataSource" OnColumnCreated="grdpuestos_ColumnCreated" OnItemDataBound="grdpuestos_ItemDataBound"></telerik:RadGrid>
</asp:Content>
