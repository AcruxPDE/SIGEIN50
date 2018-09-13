<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="ReporteDescripcionesPuestos.aspx.cs" Inherits="SIGE.WebApp.Administracion.ReporteDescripcionesPuestos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <label class="labelTitulo">Descripciones de puesto</label>
        <div style="height: calc(100% - 60px);">
        <telerik:RadGrid ID="grdPuestos" runat="server" Height="100%" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" ShowGroupPanel="True" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
            OnNeedDataSource="grdPuestos_NeedDataSource" OnItemCommand="grdPuestos_ItemCommand"  OnItemDataBound="grdPuestos_ItemDataBound">
                  <ClientSettings AllowDragToGroup="True" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="false" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
              <ExportSettings ExportOnlyData="true" FileName="DescripcionesPuestos"  Excel-Format="Xlsx" IgnorePaging="true">
               </ExportSettings>
                 <GroupingSettings ShowUnGroupButton="true" CaseSensitive="false"></GroupingSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView   EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true"
                        ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" CommandItemDisplay="Top" 
                 DataKeyNames="CL_PUESTO" ClientDataKeyNames="CL_PUESTO" >
                <GroupByExpressions>
                        </GroupByExpressions>
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />                                           
                <Columns>
                    <telerik:GridBoundColumn  AllowFiltering="false"  HeaderText="#" HeaderStyle-HorizontalAlign="Center" DataField="RENGLON" UniqueName="RENGLON" HeaderStyle-Width="50" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="60" FilterControlWidth="40"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Descripción" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Reporta a" DataField="REPORTA_A" UniqueName="REPORTA_A" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
