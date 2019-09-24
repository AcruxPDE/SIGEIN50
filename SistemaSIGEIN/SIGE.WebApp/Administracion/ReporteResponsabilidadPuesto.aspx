<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="ReporteResponsabilidadPuesto.aspx.cs" Inherits="SIGE.WebApp.Administracion.ReporteResponsabilidadPuesto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="labelTitulo">Responsabilidad del puesto por persona</label>
    <div style="height: calc(100% - 60px);">
        <telerik:RadGrid ID="grdEmpleados" runat="server" Height="100%" Width="100%" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" ShowGroupPanel="True" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
            OnNeedDataSource="grdEmpleados_NeedDataSource" OnItemCommand="grdEmpleados_ItemCommand" OnItemDataBound="grdEmpleados_ItemDataBound" >
                  <ClientSettings AllowDragToGroup="True" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="false" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
              <ExportSettings ExportOnlyData="true" FileName="DatosEmpleados"  Excel-Format="Xlsx" IgnorePaging="true">
               </ExportSettings>
                 <GroupingSettings ShowUnGroupButton="true" CaseSensitive="false"></GroupingSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView   EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true"
                        ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" CommandItemDisplay="Top" 
                 DataKeyNames="ID_EMPLEADO" ClientDataKeyNames="ID_EMPLEADO" >
                <GroupByExpressions>
                        </GroupByExpressions>
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />                                           
                <Columns>
                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="#" HeaderStyle-HorizontalAlign="Center" DataField="RENGLON" UniqueName="RENGLON" HeaderStyle-Width="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave del empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Width="100" FilterControlWidth="70"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre del empleado" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO" HeaderStyle-Width="200" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true"  HeaderText="Fecha de ingreso " DataField="FE_INGRESO" UniqueName="FE_INGRESO" HeaderStyle-Width="110" FilterControlWidth="100" DataType="System.DateTime"  ></telerik:GridDateTimeColumn>                
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Antigüedad" DataField="FE_ANTIGUEDAD" UniqueName="FE_ANTIGUEDAD" HeaderStyle-Width="80" FilterControlWidth="60"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave del puesto" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="90" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Objetivo del puesto" DataField="NB_OBJETIVO_PUESTO" UniqueName="NB_OBJETIVO_PUESTO" HeaderStyle-Width="350" FilterControlWidth="130"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
