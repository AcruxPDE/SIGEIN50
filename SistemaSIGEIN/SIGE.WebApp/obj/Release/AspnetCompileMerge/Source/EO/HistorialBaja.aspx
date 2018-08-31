<%@ Page Title="" Language="C#" MasterPageFile="~/EO/MenuEO.master" AutoEventWireup="true" CodeBehind="HistorialBaja.aspx.cs" Inherits="SIGE.WebApp.EO.HistorialBaja" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: calc(100% - 80px);">
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="grdHistorialBaja">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdHistorialBaja" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
         <label class="labelTitulo">Historial de baja</label>
        <div style="height: 10px;"></div>
        <div style="height: calc(100% - 20px);">
            <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
                <telerik:RadPageView ID="rpvCapturaResultados" runat="server">
                   <div style="clear: both;"></div>
                        <telerik:RadGrid ID="grdHistorialBaja" HeaderStyle-Font-Bold="true" runat="server"  OnNeedDataSource="grdHistorialBaja_NeedDataSource" AutoGenerateColumns="false" Height="100%" OnItemDataBound="grdHistorialBaja_ItemDataBound">
                            <ClientSettings>
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                <Selecting AllowRowSelect="false" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                             <MasterTableView DataKeyNames="ID_EMPLEADO" AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="true">
                                <Columns>
                                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true"  Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de la baja" DataField="FECHA_BAJA" UniqueName="FECHA_BAJA"  DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="80" FilterControlWidth="80" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150"  FilterControlWidth="120"  HeaderText="Nombre del empleado" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="80" FilterControlWidth="80" HeaderText="Causa de baja" DataField="NB_CAUSA_BAJA" UniqueName="NB_CAUSA_BAJA"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="120" HeaderText="Cometarios" DataField="DS_COMENTARIO" UniqueName="DS_COMENTARIO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="80" FilterControlWidth="80" HeaderText="Estado" DataField="CL_ESTADO" UniqueName="CL_ESTADO"></telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn AutoPostBackOnFilter="true"  Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de reingreso" DataField="FECHA_REINGRESO" UniqueName="FECHA_REINGRESO"  DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    </div>
</asp:Content>
