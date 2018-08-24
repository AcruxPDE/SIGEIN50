<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VisorDeFormatosTramites.aspx.cs" Inherits="SIGE.WebApp.PDE.VisorDeFormatosTramites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 60px);">
        <telerik:RadEditor  Height="300" Width="100%" ToolsWidth="600" EditModes="Design" ID="reTramite" runat="server" ToolbarMode="Default" OnClientLoad="OnClientLoad" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
    </div>
    </asp:Content>
