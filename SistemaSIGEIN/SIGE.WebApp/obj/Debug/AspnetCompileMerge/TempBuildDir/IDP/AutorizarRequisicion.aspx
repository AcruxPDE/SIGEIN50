<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="AutorizarRequisicion.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaAutorizarRequisicion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    
     <telerik:RadEditor Height="80%" Width="98%" ToolsWidth="400px" EditModes="Design"
        ID="radEditorAutorizar" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/BasicTools.xml"
        >
    </telerik:RadEditor>

</asp:Content>
