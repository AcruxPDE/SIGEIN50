<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="ReporteEmpleadoCamposExtra.aspx.cs" Inherits="SIGE.WebApp.Administracion.ReporteEmpleadoCamposExtra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="labelTitulo">Campos adicionales</label>
    <div style="height: calc(100% - 60px);">
        <telerik:RadGrid ID="grdCamposExtra" runat="server" Height="100%" AutoGenerateColumns="false" ShowGroupPanel="True" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
            HeaderStyle-Font-Bold="true" OnItemCommand="grdCamposExtra_ItemCommand" OnItemDataBound="grdCamposExtra_ItemDataBound">
            <ClientSettings AllowDragToGroup="True" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="false" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <ExportSettings ExportOnlyData="true" FileName="CandidatosCamposAdicionles" Excel-Format="Xlsx" IgnorePaging="true">
            </ExportSettings>
            <GroupingSettings ShowUnGroupButton="true" CaseSensitive="false"></GroupingSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true"
                ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" CommandItemDisplay="Top"
                DataKeyNames="M_EMPLEADO_ID_EMPLEADO" ClientDataKeyNames="M_EMPLEADO_ID_EMPLEADO">
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                <Columns>
                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="#" HeaderStyle-HorizontalAlign="Center" DataField="RENGLON" UniqueName="RENGLON" HeaderStyle-Width="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="No. de empleado" DataField="M_EMPLEADO_CL_EMPLEADO" UniqueName="M_EMPLEADO_CL_EMPLEADO" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre completo" DataField="M_EMPLEADO_NB_EMPLEADO_COMPLETO" UniqueName="M_EMPLEADO_NB_EMPLEADO_COMPLETO" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="M_PUESTO_NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área/Departamento" DataField="M_DEPARTAMENTO_NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
