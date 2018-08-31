<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="ReportesCompetenciasLaborales.aspx.cs" Inherits="SIGE.WebApp.Administracion.ReportesCompetenciasLaborales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="labelTitulo">Competencias Laborales</label>
    <div style="height: calc(100% - 60px);">
        <telerik:RadGrid 
            ID="grdCompLaborales" 
            runat="server" 
            AutoGenerateColumns="false" 
            ShowGroupPanel="True" 
            AllowPaging="true"
            Height="100%" 
            AllowSorting="true" 
            HeaderStyle-Font-Bold="true"
            AllowFilteringByColumn="true"
            OnNeedDataSource="grdRepCompLaborales_NeedDataSource" 
            OnItemCommand="grdCompLaborales_ItemCommand"  
            OnItemDataBound="grdCompLaborales_ItemDataBound">
            <ClientSettings AllowDragToGroup="True" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="false" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <ExportSettings ExportOnlyData="true" FileName="CompetenciasLaborales" Excel-Format="Xlsx" IgnorePaging="true">
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
                DataKeyNames="CLAVE" 
                ClientDataKeyNames="CLAVE">
                <GroupByExpressions>
                </GroupByExpressions>
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                <Columns>
                    <telerik:GridBoundColumn AllowFiltering="false" CurrentFilterFunction="Contains" HeaderText="#" HeaderStyle-HorizontalAlign="Center" DataField="RENGLON" UniqueName="RENGLON" HeaderStyle-Width="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CLAVE" UniqueName="CLAVE" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Competencia" DataField="COMPETENCIA" UniqueName="COMPETENCIA" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Descripción" DataField="DESCRIPCION" UniqueName="DESCRIPCION" HeaderStyle-Width="350" FilterControlWidth="270"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clasificación" DataField="CLASIFICACION" UniqueName="CLASIFICACION" HeaderStyle-Width="160" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Categoría" DataField="CATEGORIA" UniqueName="CATEGORIA" HeaderStyle-Width="160" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true"  HeaderText="Última Actualización" DataField="ACTUALIZACION" UniqueName="ACTUALIZACION" HeaderStyle-Width="100" FilterControlWidth="70" DataType="System.DateTime"  ></telerik:GridDateTimeColumn>                
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Actualizado Por" DataField="ACTUALIZO" UniqueName="ACTUALIZO" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
