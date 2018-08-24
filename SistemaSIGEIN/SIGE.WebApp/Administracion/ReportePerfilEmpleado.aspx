<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="ReportePerfilEmpleado.aspx.cs" Inherits="SIGE.WebApp.Administracion.ReportePerfilEmpleado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <label class="labelTitulo">Perfil de empleados</label>
    <div style="height: calc(100% - 60px);">
        <telerik:RadGrid
            ID="grdPerfilEmpleados"
            runat="server"
            AutoGenerateColumns="false"
            ShowGroupPanel="True"
            AllowPaging="true"
            Height="100%"
            Width="100%"
            AllowSorting="true"
            HeaderStyle-Font-Bold="true"
            AllowFilteringByColumn="true"
            OnNeedDataSource="grdPerfilEmpleados_NeedDataSource"
            OnItemCommand="grdPerfilEmpleados_ItemCommand"
            OnItemDataBound="grdPerfilEmpleados_ItemDataBound">
            <ClientSettings AllowDragToGroup="True" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="false" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <ExportSettings ExportOnlyData="true" FileName="PerfilEmpleados" Excel-Format="Xlsx" IgnorePaging="true">
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
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Dirección" DataField="DIRECCION" UniqueName="DIRECCION" HeaderStyle-Width="320" FilterControlWidth="240"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Edad" DataField="EDAD" DataType="System.Int64" UniqueName="EDAD" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estado civil" DataField="CL_ESTADO_CIVIL" UniqueName="CL_ESTADO_CIVIL" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nivel academico" DataField="CL_NIVEL_ESCOLARIDAD" UniqueName="CL_NIVEL_ESCOLARIDAD" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Escolaridad" DataField="NB_NIVEL_ESCOLARIDAD" UniqueName="NB_NIVEL_ESCOLARIDAD" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="250" FilterControlWidth="170"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="250" FilterControlWidth="170"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataType="System.Decimal" HeaderText="Sueldo" DataField="MN_SUELDO" UniqueName="MN_SUELDO" HeaderStyle-Width="120" FilterControlWidth="40"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" DataType="System.DateTime" DataFormatString="{0:d}" HeaderText="Fecha de alta" DataField="FE_ALTA" UniqueName="FE_ALTA" HeaderStyle-Width="160" FilterControlWidth="80"></telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" DataType="System.DateTime" DataFormatString="{0:d}" HeaderText="Fecha de baja" DataField="FE_BAJA" UniqueName="FE_BAJA" HeaderStyle-Width="160" FilterControlWidth="80"></telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataType="System.Int64" CurrentFilterFunction="Contains" HeaderText="Tiempo en años" DataField="ANTIGUEDAD" UniqueName="ANTIGUEDAD" HeaderStyle-Width="120" FilterControlWidth="40"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Causa de baja" DataField="NB_CATALOGO_VALOR" UniqueName="NB_CATALOGO_VALOR" HeaderStyle-Width="320" FilterControlWidth="240"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Notas" DataField="DS_COMENTARIO" UniqueName="DS_COMENTARIO" HeaderStyle-Width="320" FilterControlWidth="240"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="CL_ESTADO_EMPLEADO" UniqueName="CL_ESTADO_EMPLEADO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
