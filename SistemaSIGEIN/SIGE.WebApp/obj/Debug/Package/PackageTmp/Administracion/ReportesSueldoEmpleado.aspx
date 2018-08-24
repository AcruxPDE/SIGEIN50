<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="ReportesSueldoEmpleado.aspx.cs" Inherits="SIGE.WebApp.Administracion.ReportesSueldoEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: calc(100% - 60px);">
        <div style="clear: both; height: 10px;"></div>
        <label class="labelTitulo">Sueldo de empleados</label>
        <telerik:RadGrid
            ID="grdSueldoEmpleados"
            runat="server"
            AutoGenerateColumns="false"
            ShowGroupPanel="True"
            AllowPaging="true"
            Height="99%"
            AllowSorting="true"
            AllowFilteringByColumn="true"
            HeaderStyle-Font-Bold="true"
            OnNeedDataSource="grdSueldoEmpleados_NeedDataSource"
            OnItemCommand="grdSueldoEmpleados_ItemCommand"
            OnItemDataBound="grdSueldoEmpleados_ItemDataBound">
            <ClientSettings AllowDragToGroup="True" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="false" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <ExportSettings ExportOnlyData="true" FileName="SueldoEmpleados" Excel-Format="Xlsx" IgnorePaging="true">
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
                DataKeyNames="CL_EMPLEADO"
                ClientDataKeyNames="CL_EMPLEADO">
                <GroupByExpressions>
                </GroupByExpressions>
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                <Columns>
                    <telerik:GridBoundColumn AllowFiltering="false" CurrentFilterFunction="Contains" HeaderText="#" HeaderStyle-HorizontalAlign="Center" DataField="RENGLON" UniqueName="RENGLON" HeaderStyle-Width="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre completo" DataField="NOMBRE_COMPLETO" UniqueName="NOMBRE_COMPLETO" HeaderStyle-Width="280" FilterControlWidth="200"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="ESTATUS" UniqueName="ESTATUS" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="250" FilterControlWidth="170"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="250" FilterControlWidth="170"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataType="System.Decimal" HeaderText="Sueldo" DataField="MN_SUELDO" UniqueName="MN_SUELDO" HeaderStyle-Width="120" FilterControlWidth="40" DataFormatString="{0:C2}"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
