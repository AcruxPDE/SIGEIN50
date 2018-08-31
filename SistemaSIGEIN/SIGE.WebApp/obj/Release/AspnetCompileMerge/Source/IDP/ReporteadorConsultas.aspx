<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="ReporteadorConsultas.aspx.cs" Inherits="SIGE.WebApp.IDP.ReporteadorConsultas" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2016.1.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
      <div style="width: 100%; height: 100%;">
        <cc1:StiWebViewer ID="swvReporte" runat="server" Height="99%" Width="99%" ScrollBarsMode="true" />
    </div>
</asp:Content>
