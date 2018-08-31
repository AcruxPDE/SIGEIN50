<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaComunicadosLeidos.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaComunicadosLeidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
        <div style="height: calc(100% - 20px);">
        <div style="margin-right: 30px; margin-left: 30px;">
            <div style="clear: both; height: 10px;"></div>
            <telerik:RadGrid ID="grdAdmcomunicados" ShowHeader="true" HeaderStyle-Font-Bold="true" runat="server" AllowPaging="true"
                AllowSorting="true" Width="100%"
                OnNeedDataSource="grdAdmcomunicados_NeedDataSource">
                <ClientSettings EnablePostBackOnRowClick="false">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                </ClientSettings>
                <PagerStyle AlwaysVisible="true" />
                <MasterTableView  ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20"
                    HorizontalAlign="NotSet" PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="true">
                    <Columns>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_NOMBRE" UniqueName="NB_NOMBRE" HeaderStyle-Width="220" FilterControlWidth="150"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Status" DataField="FG_LEIDO" UniqueName="FG_LEIDO" HeaderStyle-Width="150" FilterControlWidth="150"></telerik:GridBoundColumn>
                                        </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div style="clear: both; height: 20px;"></div>
      
        </div>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>

</asp:Content>
