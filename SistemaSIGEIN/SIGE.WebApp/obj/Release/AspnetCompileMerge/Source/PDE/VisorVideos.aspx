<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VisorVideos.aspx.cs" Inherits="SIGE.WebApp.PDE.VisorVideos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
<telerik:RadMediaPlayer ID="rmpVideo" runat="server" AutoPlay="true" Height="300px" Width="500px" Visible="false"> </telerik:RadMediaPlayer>
</asp:Content>
