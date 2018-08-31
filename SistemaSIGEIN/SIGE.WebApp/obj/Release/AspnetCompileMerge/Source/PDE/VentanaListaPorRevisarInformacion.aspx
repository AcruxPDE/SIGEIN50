<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaListaPorRevisarInformacion.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaListaPorRevisarInformacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>


        function onCloseWindow(oWnd, args) {
            GetRadWindow().close();

        }

        function OpenModificacionWindowAdmin(pIdEmpleado, pIdPuesto, pTipoComunicado, pTipoAcccion, pEstatus, pIdComunicado) {

            var win = window.open("VentanaMenuInformacion.aspx?&IdEmpleadoMenuInformacion=" + pIdEmpleado + "&IdPuestoMenuInformacion=" + pIdPuesto + "&TipoComunicadoMenuInformacion=" + pTipoComunicado + "&TipoAccionMenuInformacion=" + pTipoAcccion + "&Estatus=" + pEstatus + "&IdComunicado=" + pIdComunicado, true);
            win.focus();
            }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
     <div style="height: calc(100% - 74px);">
                        <telerik:RadGrid ID="grdSeleccionEmpleado" runat="server" AllowMultiRowSelection="true" OnNeedDataSource="grdSeleccionEmpleado_NeedDataSource" AutoGenerateColumns="false" EnableHeaderContextMenu="true" ShowGroupPanel="true" AllowSorting="true">
                            <ClientSettings>
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView ClientDataKeyNames="ID_PUESTO, ID_EMPLEADO,ID_COMUNICADO" DataKeyNames="ID_COMUNICADO,ID_PUESTO, ID_EMPLEADO, TIPO_COMUNICADO, TIPO_ACCION" EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true"  PageSize="50">
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="90" HeaderText="Clave" DataField="ID_EMPLEADO" UniqueName="ID_EMPLEADO">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="90" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="90" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="90" HeaderText="Estatus" DataField="ESTATUS" UniqueName="ESTATUS"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="30" AllowFiltering="false" >
                                     
                                        <ItemTemplate>
                                                 <div style="float: left; cursor: pointer;">

                                                <img src="../Assets/images/StatusPDE.png" onclick="OpenModificacionWindowAdmin('<%# Eval("ID_EMPLEADO") %>', '<%# Eval("ID_PUESTO") %>', '<%# Eval("TIPO_COMUNICADO") %>','<%# Eval("TIPO_ACCION") %>','<%# Eval("ESTATUS") %>', <%# Eval("ID_COMUNICADO") %> );" />

                                            </div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>

</asp:Content>
