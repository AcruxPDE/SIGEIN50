<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="PruebaReporte.aspx.cs" Inherits="SIGE.WebApp.IDP.PruebaReporte" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2016.1.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    
    <cc1:StiWebViewer ID="StiWebViewer1" runat="server" />

</asp:Content>
