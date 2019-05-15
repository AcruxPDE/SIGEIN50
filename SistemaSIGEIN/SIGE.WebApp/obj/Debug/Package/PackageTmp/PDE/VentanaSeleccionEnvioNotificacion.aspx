<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaSeleccionEnvioNotificacion.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaSeleccionEnvioNotificacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>
        function onCloseWindow(oWnd, args) {
            GetRadWindow().close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
     <div style="height: calc(100% - 20px);">
                        <telerik:RadGrid ID="grdNotificacionEmpleado" runat="server" Height="90%" AllowMultiRowSelection="true" OnNeedDataSource="grdNotificacionEmpleado_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true">
                            <ClientSettings>
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView ClientDataKeyNames="ID_PUESTO" DataKeyNames="ID_PUESTO" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true"  PageSize="50">
                                <Columns>
                                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="90" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="170" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="MB_PUESTO">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    <div class="divControlDerecha" style="padding-top: 10px;">
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnSeleccion" runat="server" name="btnSeleccion" Text="Seleccionar"  OnClick="btnSeleccion_Click"></telerik:RadButton>
                        </div>
                    </div>
         </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
