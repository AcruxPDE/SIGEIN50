<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaCalendarioCurso.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaCalendarioCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    
    
    <telerik:RadScheduler ID="rsCusro" runat="server" Height="100%" Width="100%" DataKeyField="ID_EVENTO" DataStartField="FE_INICIO" DataSubjectField="NB_EVENTO" DataEndField="FE_TERMINO_COMPLETO" AllowDelete="false" AllowEdit="false" AllowInsert="false" DataDescriptionField="DS_EVENTO" SelectedView="MonthView" >
    <%--<telerik:RadScheduler ID="rsCusro" runat="server" Height="100%" Width="100%" DataKeyField="ID_EVENTO" DataStartField="FE_INICIAL" DataSubjectField="NO_HORAS" DataEndField="FE_FINAL" AllowDelete="false" AllowEdit="false" AllowInsert="false" SelectedView="MonthView" >--%>
        <DayView UserSelectable="false" />
        <TimelineView UserSelectable="false" />
        <WeekView UserSelectable="false" />
        <MonthView UserSelectable="false" />
        <Localization HeaderToday="Hoy" HeaderWeek="Semana" HeaderMonth="Mes" HeaderYear="Año" HeaderDay="Día" />
    </telerik:RadScheduler>
</asp:Content>
