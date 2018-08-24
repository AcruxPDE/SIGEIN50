<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlAvance.aspx.cs" Inherits="SIGE.WebApp.EO.ControlAvance" MasterPageFile="~/EO/ContextEO.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: 100%; padding: 10px;">
        <telerik:RadGrid ID="grdResultados" runat="server" HeaderStyle-Font-Bold="true" Height="100%" Width="100%" AutoGenerateColumns="false" AllowSorting="true" OnNeedDataSource="grdResultados_NeedDataSource" OnItemDataBound="grdResultados_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <ExportSettings ExportOnlyData="true" FileName="ResultadoCuestionarios" Excel-Format="Xlsx" IgnorePaging="true">
            </ExportSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="NB_EVALUADO" ClientDataKeyNames="NB_EVALUADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true"
                EnableHeaderContextFilterMenu="true" CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Evaluador" DataField="NB_EVALUADOR" UniqueName="NB_EVALUADOR" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="250" FilterControlWidth="170" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Evaluado" DataField="NB_EVALUADO" UniqueName="NB_EVALUADO" FilterControlWidth="170" CurrentFilterFunction="Contains" HeaderStyle-Width="250" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Área del evaluador" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" AllowFiltering="true" ItemStyle-HorizontalAlign="Left" FilterControlWidth="200"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" HeaderText="Fecha" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" AllowFiltering="true" HeaderStyle-Width="130" ItemStyle-HorizontalAlign="Right" FilterControlWidth="90"></telerik:GridDateTimeColumn>
                    <telerik:GridNumericColumn HeaderText="Puntaje" DataFormatString="{0:#0.00}" DataField="NO_PUNTAJE" UniqueName="NO_PUNTAJE" AllowFiltering="true" HeaderStyle-Width="90" ItemStyle-HorizontalAlign="Right"  NumericType="Number" FilterControlWidth="50" AutoPostBackOnFilter="true"></telerik:GridNumericColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <div style="clear: both; height: 2px;"></div>
    </div>
</asp:Content>