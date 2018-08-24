<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="ReporteCapacitacionEmpleado.aspx.cs" Inherits="SIGE.WebApp.Administracion.ReporteCapacitacionEmpleado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="height: calc(100% - 60px);">
        <div style="clear: both; height: 10px;"></div>
        <label class="labelTitulo">Capacitación</label>
        <telerik:RadGrid
            ID="grdCapacitacion"
            runat="server"
            AutoGenerateColumns="false"
            ShowGroupPanel="True"
            AllowPaging="true"
            Height="99%"
            HeaderStyle-Font-Bold="true"
            AllowSorting="true"
            OnItemDataBound="grdCapacitacion_ItemDataBound"
            AllowFilteringByColumn="true"
            OnNeedDataSource="grdCapacitacion_NeedDataSource"
            OnItemCommand="grdCapacitacion_ItemCommand">
            <ClientSettings AllowDragToGroup="True" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="false" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <ExportSettings ExportOnlyData="true" FileName="CapacitacionEmpleados" Excel-Format="Xlsx" IgnorePaging="true">
            </ExportSettings>
            <GroupingSettings ShowUnGroupButton="true" CaseSensitive="false"></GroupingSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView
                EnableColumnsViewState="false"
                AllowPaging="true"
                AllowFilteringByColumn="true"
                ShowHeadersWhenNoRecords="true"
                EnableHeaderContextFilterMenu="true"
                CommandItemDisplay="Top"
                DataKeyNames="ID_EMPLEADO"
                ClientDataKeyNames="ID_EMPLEADO">
                <GroupByExpressions>
                </GroupByExpressions>
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                <Columns>
                    <telerik:GridBoundColumn AllowFiltering="false" CurrentFilterFunction="Contains" HeaderText="Renglón" DataField="RENGLON" UniqueName="RENGLON" HeaderStyle-Width="90"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave evento" DataField="CL_EVENTO" UniqueName="CL_EVENTO" HeaderStyle-Width="180" FilterControlWidth="100"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Evento" DataField="NB_EVENTO" UniqueName="NB_EVENTO" HeaderStyle-Width="320" FilterControlWidth="240"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" DataType="System.DateTime" DataFormatString="{0:d}" HeaderText="Fecha inicio" DataField="FE_INICIO" UniqueName="FE_INICIO" HeaderStyle-Width="160" FilterControlWidth="80"></telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" DataType="System.DateTime" DataFormatString="{0:d}" HeaderText="Fecha fin" DataField="FE_TERMINO" UniqueName="FE_TERMINO" HeaderStyle-Width="160" FilterControlWidth="80"></telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="CL_ESTADO" UniqueName="CL_ESTADO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Curso" DataField="NB_CURSO" UniqueName="NB_CURSO" HeaderStyle-Width="320" FilterControlWidth="240"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Instructor" DataField="NB_INSTRUCTOR" UniqueName="NB_INSTRUCTOR" HeaderStyle-Width="280" FilterControlWidth="200"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="No. de Empleado" DataField="CL_PARTICIPANTE" UniqueName="CL_PARTICIPANTE" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre completo" DataField="NB_PARTICIPANTE" UniqueName="NB_PARTICIPANTE" HeaderStyle-Width="280" FilterControlWidth="200"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataType="System.Decimal" HeaderText="Calificación %" DataField="CALIFICACION" UniqueName="CALIFICACION" HeaderStyle-Width="120" FilterControlWidth="40"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="250" FilterControlWidth="170"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="250" FilterControlWidth="170"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>

</asp:Content>
