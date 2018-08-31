<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/MenuIDP.master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="SIGE.WebApp.IDP.Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .divControlIzquierdaIntegracion {
            width: 550px;
            float: left;
        }
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
            var idPrueba = "";
            var vclTokenExterno = "";
            var vFlBateria = "";

            function obtenerIdFila() {
                vFlBateria = "";
                var grid = $find("<%=dgvBateria.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    vFlBateria = row.getDataKeyValue("ID_BATERIA");
                    vclTokenExterno = row.getDataKeyValue("CL_TOKEN");
                }
            }

            function onCloseWindow(oWnd, args) {
                var idPrueba = "";
                var vclTokenExterno = "";
                $find("<%=dgvBateria.ClientID%>").get_masterTableView().rebind();
            }

            function sustitucionBaremos() {
                obtenerIdFila();
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 300,
                    height: browserWnd.innerHeight - 20
                };
                if ((vFlBateria != "")) {
                    var win = openChildDialog("VentanaSustitucionBaremos.aspx?pIdBateria=" + vFlBateria, 'rwBaremos', "Sustitución de Baremos", windowProperties);
                }
                else { radalert("No has seleccionado una bateria.", 400, 150, ""); }
            }

            function VentanaResultados() {
                var vFlBateria = "";
                var grid = $find("<%= rgBaterias.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 300,
                    height: browserWnd.innerHeight - 20
                };
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    vFlBateria = row.getDataKeyValue("ID_BATERIA");
                    if (vFlBateria != "") {
                        var win = openChildDialog("VentanaReporteResultados.aspx?pIdBateria=" + vFlBateria, 'rwResultados', "Resultados de pruebas por bateria", windowProperties);
                    }
                }
                else {
                    radalert("No has seleccionado una bateria.", 400, 150, "");
                }
            }

            function ShowInsertForm() {
                OpenWindow(null);
                return false;
            }
            function enableCtrlCorreo(sender, args) {
                //$get("<%=ctrlCorreos.ClientID %>").style.display = sender.get_checked() ? "block" : "none";
            }

            function OpenSeleccionarEmpleadoAutoriza() {
                openChildDialog("../Comunes/SeleccionEmpleado.aspx?mulSel=0", "winSeleccionEmpleados", "Selección de empleados")
            }

            function useDataFromChild(pDato) {
                var list;
                if (pDato != null) {
                    switch (pDato[0].clTipoCatalogo) {
                        case "EMPLEADO":
                            var vEmpleadoSeleccionado = pDato[0];
                            var nbCorreo = vEmpleadoSeleccionado.nbCorreoElectronico.replace("&nbsp;", "");

                            if (nbCorreo == "") {
                                radalert("El empleado que has seleccionado no tiene correo eletrónico.", 450, 150, "");
                                return;
                            }
                            //var txtNombre = $find("<=rtbNombreRrhh.ClientID %>");
                            //var txtCorreo = $find('<= rtbCorreoRrhh.ClientID %>');

                            //SetListBoxItem(vLstEmpleados, vEmpleadoSeleccionado.nbEmpleado, vEmpleadoSeleccionado.idEmpleado);
                            //hfIdEmpleado.value = vEmpleadoSeleccionado.idEmpleado;
                            //document.getElementById("<= hfIdAutorizaPuestoRequisicion.ClientID %>").value = vEmpleadoSeleccionado.idEmpleado;
                            txtNombre.set_value(vEmpleadoSeleccionado.nbEmpleado);
                            txtCorreo.set_value(vEmpleadoSeleccionado.nbCorreoElectronico);
                            break;
                    }
                }
            }

            //function CleanSelectionAutoriza(sender, args) {

            //    var txtNombre = $find("<=rtbNombreRrhh.ClientID %>");
            //    var txtCorreo = $find('<= rtbCorreoRrhh.ClientID %>');

            //    document.getElementById("<= hfIdAutorizaPuestoRequisicion.ClientID %>").value = "";
            //    txtNombre.set_value("");
            //    txtCorreo.set_value("");                                    
            //  }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbPrueba">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbPrueba" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="grdAjusteTiempo" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdAjusteTiempo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAjusteTiempo" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdAjusteTiempo" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarCorreo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCorreos" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txbCorreo" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txbNombre" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarCorreo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCorreos" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbAgregarRrhh">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgNotificacionRrhh" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rtbCorreoRrhh" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rtbNombreRrhh" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbEliminarRrhh">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgNotificacionRrhh" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvBateria">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvBateria" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnReinicializar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalmental" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txnMinutosLaboral1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosLaboral2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>



        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosPensamiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosIntereses">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosMental1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalmental" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosMental2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalmental" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosMental3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalmental" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosMental4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalmental" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosMental5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalmental" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosMental6">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalmental" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosMental7">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalmental" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosMental8">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalmental" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosMental9">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalmental" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMinutosMental10">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtTotalmental" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtnMentalDos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtOrt1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtOrt2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtOrt3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtRedaccion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtAdapatacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtTiva">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtTecnica">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtIngles1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtIngles2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtIngles3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtIngles4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAjuste" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" BorderColor="Red"></telerik:RadAjaxLoadingPanel>
    <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion">
        <Tabs>
            <telerik:RadTab Text="Ajuste de tiempo a pruebas"></telerik:RadTab>
            <telerik:RadTab Text="Mensajes"></telerik:RadTab>
            <telerik:RadTab Text="Depuración de cartera"></telerik:RadTab>
            <%-- <telerik:RadTab Text="Autorización de Requisiciones"></telerik:RadTab>--%>
            <telerik:RadTab Text="Sustitución de baremos"></telerik:RadTab>
            <telerik:RadTab Text="Resultados prueba"></telerik:RadTab>
            <telerik:RadTab Text="Consultas de integración"></telerik:RadTab>

        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 50px);">
        <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvTiempo" runat="server">
                <label class="labelTitulo">Ajuste de tiempo de las pruebas</label>
                <%-- <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lbPrueba" name="Prueba" width="200px" runat="server">Prueba:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbPrueba" Width="240" MarkFirstMatch="true" AutoPostBack="true" EmptyMessage="Seleccione..."
                            DropDownWidth="240" ValidationGroup="Seccion" OnSelectedIndexChanged="cmbPrueba_SelectedIndexChanged">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCL_FORMULARIO" Text="Prueba" runat="server" Width="240px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCL_FORMULARIO" Text='<%# DataBinder.Eval(Container.DataItem, "NB_PRUEBA") %>' runat="server" Width="200px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                    </div>
                </div>--%>
                <div style="height: calc(100% - 60px); padding-top: 0px;">
                    <table border="1" style="height: 300px;">
                        <tr>
                            <td>
                                <table cellspacing="0" style="border-radius: 3px;" width="580px">
                                    <tr style="color: black; background-color: #C6DB95; height: 40px">
                                        <th width="30px" style="text-align: center;">No.</th>
                                        <th width="250px" style="text-align: center;">Prueba</th>
                                        <th width="120px" style="text-align: center;">Tiempo minutos</th>
                                        <th width="160px" style="text-align: center;">Ajuste minutos</th>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 600px; height: 355px; overflow: auto;">
                                    <table cellspacing="0" cellpadding="1" border="1" width="580px">
                                        <tr>
                                            <td colspan="4" style="text-align: center; background: #E0E0E0;">Personalidad Laboral 1</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">1</td>
                                            <td width="300px">Empuje</td>
                                            <td width="120px" rowspan="4" style="text-align: center;">15</td>
                                            <td rowspan="4" width="160px" style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txnMinutosLaboral1" Name="txnMinutos" Width="80px" MinValue="15" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">2</td>
                                            <td>Influencia</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">3</td>
                                            <td>Constancia</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">4</td>
                                            <td>Cumplimiento</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center; background: #E0E0E0;">Personalidad Laboral 2</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">5</td>
                                            <td>Da y apoya</td>
                                            <td rowspan="4" style="text-align: center;">25</td>
                                            <td rowspan="4" style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosLaboral2" Name="txnMinutos" Width="80px" MinValue="25" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">6</td>
                                            <td>Toma y controla</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">7</td>
                                            <td>Mantiene y conserva</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">8</td>
                                            <td>Adopta y negocia</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center; background: #E0E0E0;">Estilo de Pensamiento</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">9</td>
                                            <td>Análisis</td>
                                            <td rowspan="4" style="text-align: center;">15</td>
                                            <td rowspan="4" style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosPensamiento" Name="txnMinutos" Width="80px" MinValue="15" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">10</td>
                                            <td>Visión</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">11</td>
                                            <td>Intuición</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">12</td>
                                            <td>Lógica</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="background: #E0E0E0; text-align: center;">Intereses Personales</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">13</td>
                                            <td>Teórica</td>
                                            <td rowspan="6" style="text-align: center;">15</td>
                                            <td rowspan="6" style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosIntereses" Name="txnMinutos" Width="80px" MinValue="15" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">14</td>
                                            <td>Económico</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">15</td>
                                            <td>Artístico estético</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">16</td>
                                            <td>Social</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">17</td>
                                            <td>Político</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">18</td>
                                            <td>Regulatorio</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center; background: #E0E0E0;">Aptitud Mental 1</td>
                                        </tr>
                                        <tr style="height: 40px;">
                                            <td width="30px" style="text-align: center">19</td>
                                            <td>Cultura general</td>
                                            <td style="text-align: center;">1</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosMental1" Name="txnMinutos" Width="80px" MinValue="1" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txtnMinutosMental1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr style="height: 40px;">
                                            <td width="30px" style="text-align: center">20</td>
                                            <td>Capacidad de juicio</td>
                                            <td style="text-align: center;">2</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosMental2" Name="txnMinutos" Width="80px" MinValue="2" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txtnMinutosMental1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr style="height: 40px;">
                                            <td width="30px" style="text-align: center">21</td>
                                            <td>Capacidad de análisis y síntesis</td>
                                            <td style="text-align: center;">2</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosMental3" Name="txnMinutos" Width="80px" MinValue="2" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txtnMinutosMental1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr style="height: 40px;">
                                            <td width="30px" style="text-align: center">22</td>
                                            <td>Capacidad de abstracción</td>
                                            <td style="text-align: center;">3</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosMental4" Name="txnMinutos" Width="80px" MinValue="3" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txtnMinutosMental1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr style="height: 40px;">
                                            <td width="30px" style="text-align: center">23</td>
                                            <td>Capacidad de razonamiento</td>
                                            <td style="text-align: center;">5</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosMental5" Name="txnMinutos" Width="80px" MinValue="5" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txtnMinutosMental1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr style="height: 40px;">
                                            <td width="30px" style="text-align: center">24</td>
                                            <td>Sentido común</td>
                                            <td style="text-align: center;">2</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosMental6" Name="txnMinutos" Width="80px" MinValue="2" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txtnMinutosMental1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr style="height: 40px;">
                                            <td width="30px" style="text-align: center">25</td>
                                            <td>Pensamiento organizado</td>
                                            <td style="text-align: center;">2</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosMental7" Name="txnMinutos" Width="80px" MinValue="2" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txtnMinutosMental1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr style="height: 40px;">
                                            <td width="30px" style="text-align: center">26</td>

                                            <td>Capacidad de planeación</td>
                                            <td style="text-align: center;">3</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosMental8" Name="txnMinutos" Width="80px" MinValue="3" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txtnMinutosMental1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr style="height: 40px;">
                                            <td width="30px" style="text-align: center">27</td>
                                            <td>Capacidad de discriminación</td>
                                            <td style="text-align: center;">2</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosMental9" Name="txnMinutos" Width="80px" MinValue="2" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txtnMinutosMental1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr style="height: 40px;">
                                            <td width="30px" style="text-align: center">28</td>
                                            <td>Capacidad de deducción</td>
                                            <td style="text-align: center;">4</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMinutosMental10" Name="txnMinutos" Width="80px" MinValue="4" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txtnMinutosMental1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">29</td>
                                            <td>Inteligencia</td>
                                            <td style="text-align: center;">NA</td>
                                            <td style="text-align: center;">NA</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">30</td>
                                            <td>Aprendizaje</td>
                                            <td style="text-align: center;">NA</td>
                                            <td style="text-align: center;">NA</td>
                                        </tr>
                                        <tr style="height: 40px;">

                                            <td style="text-align: right;" colspan="2">Total:</td>
                                            <td style="text-align: center;">26</td>
                                            <td style="text-align: center;">
                                                <input disabled style="width: 95px; height: 15px; text-align: center;" id="txtTotalmental" runat="server" type="text" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center; background: #E0E0E0;">Aptitud Mental 2</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">19</td>
                                            <td>Cultura general</td>
                                            <td rowspan="12" style="text-align: center;">30</td>
                                            <td rowspan="12" style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtnMentalDos" Name="txnMinutos" Width="80px" MinValue="30" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">20</td>
                                            <td>Capacidad de juicio</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">21</td>
                                            <td>Capacidad de análisis y síntesis</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">22</td>
                                            <td>Capacidad de abstracción</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">23</td>
                                            <td>Capacidad de razonamiento</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">24</td>
                                            <td>Sentido común</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">25</td>
                                            <td>Pensamiento organizado</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">26</td>
                                            <td>Capacidad de planeación</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">27</td>
                                            <td>Capacidad de discriminación</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">28</td>
                                            <td>Capacidad de deducción</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">29</td>
                                            <td>Inteligencia</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">30</td>
                                            <td>Aprendizaje</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center; background: #E0E0E0;">Comunicación</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" rowspan="3" style="text-align: center">31</td>
                                            <td style="height: 40px;">Ortografía 1</td>
                                            <td style="text-align: center;">5</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtOrt1" Name="txnMinutos" Width="80px" MinValue="5" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 40px;">Ortografía 2</td>
                                            <td style="text-align: center;">5</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtOrt2" Name="txnMinutos" Width="80px" MinValue="5" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>

                                        </tr>
                                        <tr>
                                            <td style="height: 40px;">Ortografía 3</td>
                                            <td style="text-align: center;">5</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtOrt3" Name="txnMinutos" Width="80px" MinValue="5" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>

                                        </tr>
                                        <tr style="height: 40px;">
                                            <td width="30px" style="text-align: center">32</td>
                                            <td>Redacción</td>
                                            <td style="text-align: center;">20</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtRedaccion" Name="txnMinutos" Width="80px" MinValue="20" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>

                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">33</td>
                                            <td>Comunicación verbal</td>
                                            <td style="text-align: center;">NA</td>
                                            <td style="text-align: center;">NA</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">34</td>
                                            <td>Comunicación no verbal</td>
                                            <td style="text-align: center;">NA</td>
                                            <td style="text-align: center;">NA</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center; background: #E0E0E0;">Adaptación al medio</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">35</td>
                                            <td>Productibidad</td>
                                            <td rowspan="8" style="text-align: center;">3</td>
                                            <td rowspan="8" style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtAdapatacion" Name="txnMinutos" Width="80px" MinValue="3" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">36</td>
                                            <td>Empuje</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">37</td>
                                            <td>Sociabilidad</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">38</td>
                                            <td>Creatividad</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">39</td>
                                            <td>Paciencia</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">40</td>
                                            <td>Energia</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">41</td>
                                            <td>Participación</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">42</td>
                                            <td>Autoestima y seguridad</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center; background: #E0E0E0;">Técnica PC</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">43</td>
                                            <td>Software</td>
                                            <td rowspan="4" style="text-align: center;">8</td>
                                            <td rowspan="4" style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtTecnica" Name="txnMinutos" Width="80px" MinValue="8" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">44</td>
                                            <td>Internet</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">45</td>
                                            <td>Hardware</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">46</td>
                                            <td>Comunicaciones</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center; background: #E0E0E0;">TIVA</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">47</td>
                                            <td>Personal</td>
                                            <td rowspan="4" style="text-align: center;">10</td>
                                            <td rowspan="4" style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtTiva" Name="txnMinutos" Width="80px" MinValue="10" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">48</td>
                                            <td>Leyes y reglamento</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">49</td>
                                            <td>Integridad y ética laboral</td>
                                        </tr>
                                        <tr>
                                            <td width="30px" style="text-align: center">50</td>
                                            <td>Cívica</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center; background: #E0E0E0;">Inglés</td>
                                        </tr>
                                        <tr>
                                            <td width="30px"></td>
                                            <td style="height: 40px;">Sección 1</td>
                                            <td style="text-align: center;">20</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtIngles1" Name="txnMinutos" Width="80px" MinValue="20" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                        <tr>
                                            <td width="30px"></td>
                                            <td style="height: 40px;">Sección 2</td>
                                            <td style="text-align: center;">20</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtIngles2" Name="txnMinutos" Width="80px" MinValue="20" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>

                                        </tr>
                                        <tr>
                                            <td width="30px"></td>
                                            <td style="height: 40px;">Sección 3</td>
                                            <td style="text-align: center;">20</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtIngles3" Name="txnMinutos" Width="80px" MinValue="20" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>

                                        </tr>
                                        <tr>
                                            <td width="30px"></td>
                                            <td style="height: 40px;">Sección 4</td>
                                            <td style="text-align: center;">20</td>
                                            <td style="text-align: center;">
                                                <telerik:RadNumericTextBox runat="server" ID="txtIngles4" Name="txnMinutos" Width="80px" MinValue="20" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned" AutoPostBack="true" OnTextChanged="txnMinutosLaboral1_TextChanged"></telerik:RadNumericTextBox></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" style="border-radius: 3px;" width="580px">
                                    <tr style="color: black; background-color: #C6DB95; height: 40px">
                                        <td width="30px"></td>
                                        <th width="300px" style="text-align: center;">Total</th>
                                        <td width="120px" style="text-align: center;">
                                            <input disabled style="width: 90px; height: 15px; text-align: center;" id="txtTotalOriginal" runat="server" type="text" value="04:22 Horas" /></td>
                                        <td width="160px" style="text-align: center;">
                                            <input disabled style="width: 90px; height: 15px; text-align: center;" id="txtAjuste" type="text" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <%-- <telerik:RadGrid ID="grdAjusteTiempo" runat="server" HeaderStyle-Font-Bold="true"
                        OnNeedDataSource="grdAjusteTiempo_NeedDataSource" Width="580" Height="100%" AutoGenerateColumns="False">
                        <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                        </ClientSettings>
                        <MasterTableView ClientDataKeyNames="ID_PRUEBA_SECCION" DataKeyNames="ID_PRUEBA_SECCION,NO_TIEMPO_MINIMO_ESTANDAR,NB_PRUEBA_SECCION" AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                            <DetailTables>
                            </DetailTables>
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="false" HeaderStyle-Width="80" HeaderText="Sección examen" DataField="NB_PRUEBA_SECCION" UniqueName="NB_PRUEBA_SECCION"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="false" HeaderStyle-Width="80" HeaderText="Tiempo mínimo" DataField="NO_TIEMPO_MINIMO_ESTANDAR" UniqueName="NO_TIEMPO_MINIMO_ESTANDAR"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="NO_TIEMPO" HeaderText="Minutos" UniqueName="NO_TIEMPO" HeaderStyle-Width="70" AutoPostBackOnFilter="false">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox runat="server" ID="txnMinutos" Text='<%#Eval("NO_TIEMPO") %>' Name="txnMinutos" Width="80px" MinValue="0" MaxValue="120" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" CssClass="LeftAligned"></telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>--%>
                    <%--  <telerik:RadGrid
                        ID="grdAjusteTiempo"
                        runat="server"
                        HeaderStyle-Font-Bold="true"
                        Width="580" 
                        Height="100%"
                        AutoGenerateColumns="false"
                        EnableHeaderContextMenu="false"
                        AllowSorting="true"
                        AllowMultiRowSelection="false"
                        OnNeedDataSource="grdAjusteTiempo_NeedDataSource"
                        ShowFooter="true">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        </ClientSettings>
                        <MasterTableView ClientDataKeyNames="ID_PRUEBA_SECCION" DataKeyNames="ID_PRUEBA_SECCION,NO_TIEMPO_MINIMO_ESTANDAR,NB_PRUEBA_SECCION" AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                            <Columns>

                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>--%>
                    <div style="height: 5px;"></div>
                    <div class="ctrlBasico" style="padding-left: 160px;">
                        <div class="ctrlBasico">
                            <div class="ctrlBasico">
                                <label id="Label17" name="Prueba" width="200px" runat="server">Mostrar cronometro:</label>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="chkMostrarConometro" runat="server" ToggleType="CheckBox" name="btVerAviso" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnReinicializar" runat="server" name="btnReinicializar" AutoPostBack="true" Text="Reinicializar" OnClick="btnReinicializar_Click" Width="100"></telerik:RadButton>
                        </div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnGuardar" runat="server" name="btnGuardar" AutoPostBack="true" Text="Guardar" OnClick="btnGuardarTiempo_Click" Width="100"></telerik:RadButton>
                        </div>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvMensajes" runat="server" Height="100%">
                <label class="labelTitulo">Mensajes</label>
                <telerik:RadTabStrip ID="rtsMensajes" runat="server" SelectedIndex="0" MultiPageID="rmpMensajes">
                    <Tabs>
                        <telerik:RadTab Text="Generales"></telerik:RadTab>
                        <telerik:RadTab Text="Solicitudes de empleo"></telerik:RadTab>
                        <telerik:RadTab Text="Correo electrónico"></telerik:RadTab>
                        <telerik:RadTab Text="Pruebas"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: calc(100% -90px);">
                    <telerik:RadMultiPage ID="rmpMensajes" runat="server" SelectedIndex="0" Height="100%">
                        <telerik:RadPageView ID="rpvGenerales" runat="server">
                            <div style="clear: both; height: 5px"></div>
                            <div class="ctrlBasico">
                                <div class="ctrlIzquierda">
                                    <label id="Label2" name="lblAvisoPrivacidad" width="220px" runat="server">Aviso de privacidad:</label>
                                </div>
                                <div class="ctrlDerecha">
                                    <label id="lblPrivacidad" name="lblPrivacidad" width="200px" runat="server">¿Mostrar aviso de privacidad?</label>
                                </div>
                            </div>
                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btVerAviso" runat="server" ToggleType="CheckBox" name="btVerAviso" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                             <%--   <telerik:RadTextBox ID="txbPrivacidad" runat="server" Width="350px" Height="120PX" TextMode="MultiLine">
                                </telerik:RadTextBox>--%>
                                 <telerik:RadEditor 
                                            Width="700px" 
                                            Height="250PX" 
                                            EditModes="Design"
                                            ID="txbPrivacidad" 
                                            runat="server" 
                                            ToolbarMode="Default" 
                                            ToolsFile="~/Assets/AdvancedTools.xml" 
                                             >
                                        </telerik:RadEditor>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvSolicitudes" runat="server">
                          <%--  <div style="clear: both; height: 5px"></div>--%>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label3" name="lblSolicitudBienvenida" width="260px" runat="server">Bienvenida e intrucciones de solicitudes de empleo:</label>
                                </div>
                                <div class="divControlDerecha">
                                   <%-- <telerik:RadTextBox ID="txbSolicitudBienvenida" runat="server" Width="350px" Height="120PX" TextMode="MultiLine">
                                    </telerik:RadTextBox>--%>
                                    <telerik:RadEditor 
                                            Width="700px" 
                                            Height="150PX" 
                                            EditModes="Design"
                                            ID="txbSolicitudBienvenida" 
                                            runat="server" 
                                            ToolbarMode="Default" 
                                            ToolsFile="~/Assets/AdvancedTools.xml" 
                                             >
                                        </telerik:RadEditor>
                                </div>
                            </div>
                            <%--<div style="clear: both"></div>--%>
                            <%--<div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label5" name="lblNotificaciones" width="260px" runat="server">Mensaje de notificaciones de requisiones de empleo:  </label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txbNotificaciones" runat="server" Width="350px" Height="120PX" TextMode="MultiLine">
                                    </telerik:RadTextBox>
                                </div>
                            </div>--%>
                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                                <label id="Label10" name="lblPiePagina" width="260px" runat="server">¿Mostrar mensaje de pie de página en solicitudes de empleo? </label>
                                <telerik:RadButton ID="btVerPiePagina" runat="server" ToggleType="CheckBox" name="btVerPiePagina" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label4" name="lblPiePagina" width="260px" runat="server">Mensaje de pie de página en solicitudes de empleo: </label>
                                </div>
                                <div class="divControlDerecha">
                                 <%--   <telerik:RadTextBox ID="txbPiePagina" runat="server" Width="350px" Height="120PX" TextMode="MultiLine">
                                    </telerik:RadTextBox>--%>
                                    <telerik:RadEditor 
                                            Width="700px" 
                                            Height="150PX" 
                                            EditModes="Design"
                                            ID="txbPiePagina" 
                                            runat="server" 
                                            ToolbarMode="Default" 
                                            ToolsFile="~/Assets/AdvancedTools.xml" 
                                             >
                                        </telerik:RadEditor>
                                </div>
                            </div>

                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lblMensajeCorreo"  width="260px" runat="server">Mensaje de envio de solicitud por correo:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadEditor 
                                            Width="700px" 
                                            Height="150PX" 
                                            EditModes="Design"
                                            ID="reMensajeCorreo" 
                                            runat="server" 
                                            ToolbarMode="Default" 
                                            ToolsFile="~/Assets/AdvancedTools.xml" 
                                             >
                                        </telerik:RadEditor>
                                </div>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvCorreo" runat="server">
                            <div style="clear: both; height: 5px"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label6" name="lblAsunto" width="260px" runat="server">Asunto de correo electrónico para envío de avisos de actualización de cartera y/o eliminación de cartera manual:</label>
                                </div>
                                <div class="divControlDerecha">
                                  <%--  <telerik:RadTextBox ID="txbAsunto" runat="server" Width="350px" Height="120PX" TextMode="MultiLine">
                                    </telerik:RadTextBox>--%>
                                     <telerik:RadEditor 
                                            Width="700px" 
                                            Height="150PX" 
                                            EditModes="Design"
                                            ID="txbAsunto" 
                                            runat="server" 
                                            ToolbarMode="Default" 
                                            ToolsFile="~/Assets/AdvancedTools.xml" 
                                             >
                                        </telerik:RadEditor>
                                </div>
                            </div>
                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label7" name="lblCuerpo" width="560px" runat="server">Cuerpo de correo electrónico para envío de avisos de actualización y/o eliminación de cartera manual:</label>
                                </div>
                                <div class="divControlDerecha">
                                   <%-- <telerik:RadTextBox ID="txbCuerpo" runat="server" Width="350px" Height="120PX" TextMode="MultiLine">
                                    </telerik:RadTextBox>--%>
                                    <telerik:RadEditor 
                                            Width="700px" 
                                            Height="150PX" 
                                            EditModes="Design"
                                            ID="txbCuerpo" 
                                            runat="server" 
                                            ToolbarMode="Default" 
                                            ToolsFile="~/Assets/AdvancedTools.xml" 
                                             >
                                        </telerik:RadEditor>
                                </div>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvPrueba" runat="server">
                            <div style="clear: both; height: 5px"></div>
                            <div class="ctrlBasico">
                                <label id="Label8" name="lblPruebaBienvenida" width="360px" runat="server">Mensaje de bienvenida e instrucciones en evaluación por competencias psicométricas:</label>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="ctrlBasico">
                                <%--<telerik:RadTextBox ID="txbPruebaBienvenida" runat="server" Width="500px" Height="200px" TextMode="MultiLine"></telerik:RadTextBox>--%>

                               <%-- <telerik:RadTextBox Height="200px" Width="500px" ID="txbPruebaBienvenida" runat="server" TextMode="MultiLine">
                                </telerik:RadTextBox>--%>
                                 <telerik:RadEditor 
                                            Width="700px" 
                                            Height="150PX" 
                                            EditModes="Design"
                                            ID="txbPruebaBienvenida" 
                                            runat="server" 
                                            ToolbarMode="Default" 
                                            ToolsFile="~/Assets/AdvancedTools.xml" 
                                             >
                                        </telerik:RadEditor>
                            </div>
                            <div style="clear: both; height: 5px;"></div>
                            <div class="ctrlBasico">
                                <label id="Label9" name="lblPruebaAgradecimiento" width="360px" runat="server">Mensaje de despedida y agradecimiento al evaluado:</label>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="ctrlBasico">
                               <%-- <telerik:RadTextBox ID="txbPruebaAgradecimiento" runat="server" Width="500px" Height="200px" TextMode="MultiLine">
                                </telerik:RadTextBox>--%>

                                 <telerik:RadEditor 
                                            Width="700px" 
                                            Height="150PX" 
                                            EditModes="Design"
                                            ID="txbPruebaAgradecimiento" 
                                            runat="server" 
                                            ToolbarMode="Default" 
                                            ToolsFile="~/Assets/AdvancedTools.xml" 
                                             >
                                        </telerik:RadEditor>
                            </div>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <div style="clear: both"></div>
                    <div style="padding-top: 10px;">
                        <telerik:RadButton ID="btnGuardarMensajes" runat="server" name="btnGuardarMensajes" AutoPostBack="true" Text="Guardar" OnClick="btnGuardarMensajes_Click" Width="100"></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvDepuracionCartera" runat="server">
                <telerik:RadSplitter ID="rsDepuracion" Width="100%" Height="100%" BorderSize="0" runat="server">
                    <telerik:RadPane ID="rpDepuracion" runat="server">
                        <label class="labelTitulo">Depuración de cartera</label>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label1" name="lblActualizacion" width="260px" runat="server">Actualización periódica de solicitudes de empleo:</label>
                            </div>
                            <div class="divControlDerecha">
                                <%--<telerik:RadTextBox ID="txbNotificacion" runat="server" Width="460px" MaxLength="1000" Height="80" TextMode="MultiLine">
                                </telerik:RadTextBox>--%>
                                 <telerik:RadEditor 
                                            Width="700px" 
                                            Height="150PX" 
                                            EditModes="Design"
                                            ID="txbNotificacion" 
                                            runat="server" 
                                            ToolbarMode="Default" 
                                            ToolsFile="~/Assets/AdvancedTools.xml" 
                                             >
                                        </telerik:RadEditor>
                            </div>
                        </div>
                        <div style="clear: both"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label11" name="lblPeriodicidad" runat="server">Periodicidad:</label>
                            </div>
                            <div class="divControlDerecha">
                                <div class="ctrlBasico">
                                    <telerik:RadNumericTextBox ID="txPeriodicidad" runat="server" Width="80px" NumberFormat-DecimalDigits="0" MinValue="0" CssClass="RightAligned"></telerik:RadNumericTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbTipoPeriodicidad" Width="215" MarkFirstMatch="true" AutoPostBack="false" EmptyMessage="Seleccione..."
                                        DropDownWidth="215">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Días" Value="" />
                                            <telerik:RadComboBoxItem Text="Semanas" Value="" />
                                            <telerik:RadComboBoxItem Text="Meses" Value="" />
                                            <telerik:RadComboBoxItem Text="Años" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                        </div>
                        <div style="clear: both"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label16" name="lblNotificarCandidatosEstatus" width="260px" runat="server">Proceso activo:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadButton ID="btnProcesoActivo" runat="server" ToggleType="CheckBox" name="btProceso" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                        <div style="clear: both"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label15" name="lblNotificarCandidatos" width="260px" runat="server">Notificar a candidatos:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadButton ID="btNotificarCandidatos" runat="server" ToggleType="CheckBox" name="btVerPiePagina" AutoPostBack="false">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                        <div style="clear: both"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label12" name="lblNotificar" width="260px" runat="server">Notificar RR HH:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadButton ID="btNotificar" runat="server" ToggleType="CheckBox" name="btVerPiePagina" AutoPostBack="false" OnClientCheckedChanged="enableCtrlCorreo">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </div>
                        </div>
                        <div id="ctrlCorreos" runat="server" style="display: block;">
                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label13" name="lblNombre" runat="server">Nombre:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txbNombre" runat="server" Width="220px" MaxLength="1000">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="Label14" name="lblCorreo" runat="server">Correo:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txbCorreo" runat="server" Width="220px" MaxLength="1000">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnAgregarCorreo" runat="server" name="btnAgregar" AutoPostBack="true" Text="Agregar" Width="100" OnClick="btnAgregarCorreo_Click"></telerik:RadButton>
                            </div>
                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                                <div style="margin-left: 120px">
                                    <telerik:RadGrid ID="grdCorreos" runat="server" Width="380px"
                                        AutoGenerateColumns="true" AllowSorting="true" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdCorreos_NeedDataSource">
                                        <ClientSettings AllowKeyboardNavigation="false">
                                            <Selecting AllowRowSelect="true" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                                        </ClientSettings>
                                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="NB_MAIL">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Nombre" DataField="NB_DISPLAY" UniqueName="NB_DISPLAY"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Correo" DataField="NB_MAIL" UniqueName="NB_MAIL"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnEliminarCorreo" runat="server" name="btnEliminarCorreo" AutoPostBack="true" Text="Eliminar" Width="100" OnClick="btnEliminarCorreo_Click"></telerik:RadButton>
                            </div>
                        </div>
                        <div style="clear: both"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnGuardarCartera" runat="server" GroupName="btnGuardarCartera" AutoPostBack="true" Text="Guardar" Width="100" OnClick="btnGuardarCartera_Click"></telerik:RadButton>
                            <telerik:RadButton ID="btnEjecutarLimpiaCartera" runat="server" AutoPostBack="true" Text="Ejecutar Depuración" Width="200" OnClick="btnEjecutarLimpiaCartera_Click"></telerik:RadButton>
                        </div>
                    </telerik:RadPane>
                    <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px">
                        <telerik:RadSlidingZone ID="rszAvisoDePrivacidad" runat="server" SlideDirection="Left" ExpandedPaneId="rsDepuracion" Width="22px">
                            <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Instrucciones" Width="240px" RenderMode="Mobile" Height="200">
                                <div style="padding: 10px; text-align: justify;">
                                    <p>
                                        Este proceso te permite eliminar de manera automática aquellas solicitudes que han alcanzado una antigüedad determinada. Deberás especificar cuál es la antigüedad que deseas que el sistema considere, puedes definirla por días, semanas, meses o años, dependiendo de lo que te convenga.
                                        También deberás indicar si quieres que al solicitante se le envíe un correo para que capture nuevamente sus datos en la cartera de personal.
                                    </p>
                                    <p style="font-weight: bold;">Campo Periodicidad</p>
                                    <p>Elige el mínimo de antigüedad que deberán tener las solicitudes de empleo a partir de la fecha actual.</p>
                                    <p style="font-weight: bold;">Notificar RR HH</p>
                                    <p>Permite indicar si serán enviadas notificaciones a una lista de correos acerca de que la depuración se realizó con éxito.</p>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>
            <%--   <telerik:RadPageView ID="rpvNotificacionRrhh" runat="server">
                <telerik:RadSplitter ID="rsNotificacionRrhh" Width="100%" Height="100%" BorderSize="0" runat="server">
                    <telerik:RadPane ID="RadPane1" runat="server">
                        <label class="labelTitulo">Notificación de requisiciones</label>

                        <div id="divNotificacionRrhh" runat="server" style="display: block;">

                            <asp:HiddenField runat="server" ID="hfIdAutorizaPuestoRequisicion" />

                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lbNombreNotificacionRrhh" name="lblNombre" runat="server">Nombre:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="rtbNombreRrhh" ReadOnly="true" runat="server" Width="220px" MaxLength="1000"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div style="clear: both; height: 10px;"></div>

                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    <label id="lbCorreoNotificaciónRrhh" name="lblCorreo" runat="server">Correo:</label>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="rtbCorreoRrhh" ReadOnly="true" runat="server" Width="220px" MaxLength="1000">
                                    </telerik:RadTextBox>
                                </div>
                            </div>

                            <div style="clear: both; height: 10px;"></div>


                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnBuscarEmpleadoAR" runat="server" Text="Agregar" ToolTip="Seleccionar una persona para autorizar la requisición" OnClientClicked="OpenSeleccionarEmpleadoAutoriza" AutoPostBack="false"></telerik:RadButton>
                            </div>

                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnBorrarEmpleadoAR" runat="server" Text="Eliminar" ToolTip="Quitar persona" AutoPostBack="false" OnClientClicked="CleanSelectionAutoriza"></telerik:RadButton>
                            </div>

 <%--                           <div class="ctrlBasico">
                                <telerik:RadButton ID="rbAgregarRrhh" runat="server" name="btnAgregar" AutoPostBack="true" Text="Agregar" Width="100" OnClick="rbAgregarRrhh_Click"></telerik:RadButton>
                            </div>


                            <div style="clear: both"></div>
                            <div class="ctrlBasico">
                                <div style="margin-left: 120px">
                                    <telerik:RadGrid ID="rgNotificacionRrhh" runat="server" Width="380px"
                                        AutoGenerateColumns="true" AllowSorting="true" OnNeedDataSource="rgNotificacionRrhh_NeedDataSource">
                                        <ClientSettings AllowKeyboardNavigation="false">
                                            <Selecting AllowRowSelect="true" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                                        </ClientSettings>
                                        <MasterTableView AutoGenerateColumns="false" DataKeyNames="NB_MAIL">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Notificar" DataField="NB_DISPLAY" UniqueName="NB_DISPLAY"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Correo" DataField="NB_MAIL" UniqueName="NB_MAIL"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="rbEliminarRrhh" runat="server" name="btnEliminarCorreo" AutoPostBack="true" Text="Eliminar" Width="100" OnClick="rbEliminarRrhh_Click"></telerik:RadButton>
                            </div>
                        </div>
                        <div style="clear: both"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="rbGuardarNotificacionRrhh" runat="server" GroupName="rbGuardarNotificacionRrhh" AutoPostBack="true" Text="Guardar" Width="100" OnClick="rbGuardarNotificacionRrhh_Click"></telerik:RadButton>
                        </div>
                    </telerik:RadPane>
                    <telerik:RadPane ID="rpAyudaNotificacion" runat="server" Scrolling="None" Width="22px">
                        <telerik:RadSlidingZone ID="rszNofificacionRrhh" runat="server" SlideDirection="Left" ExpandedPaneId="rszNotificacionRrhh" Width="22px">
                            <telerik:RadSlidingPane ID="rspNotificacionRrhh" runat="server" Title="Instrucciones" Width="240px" RenderMode="Mobile" Height="200">
                                <div style="padding: 10px; text-align: justify;">
                                    <p>Ingresar correos de RRHH que recibirán la notificación.</p>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>--%>
            <telerik:RadPageView ID="rpvSustitucionBaremos" runat="server">
                <label class="labelTitulo">Sustitución de baremos</label>
                <div style="height: calc(100% - 100px);">
                    <telerik:RadSplitter ID="splSolicitudes" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Vertical">
                        <telerik:RadPane ID="rpnGridPruebas" runat="server">
                            <telerik:RadGrid
                                ID="dgvBateria"
                                runat="server"
                                AutoGenerateColumns="false"
                                EnableHeaderContextMenu="true"
                                Height="100%"
                                ShowGroupPanel="true"
                                AllowSorting="true"
                                HeaderStyle-Font-Bold="true"
                                OnNeedDataSource="dgvBateria_NeedDataSource"
                                OnItemCommand="dgvBateria_ItemCommand"
                                AllowMultiRowSelection="false">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView ClientDataKeyNames="ID_BATERIA,CL_TOKEN" Name="ID_BATERIA,CL_TOKEN" EnableColumnsViewState="false" DataKeyNames="ID_BATERIA,CL_TOKEN" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" PageSize="20">
                                    <Columns>
                                        <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Folio batería" DataField="FL_BATERIA" UniqueName="FL_BATERIA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Folio de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" Display="false" DataField="CL_TOKEN" UniqueName="CL_TOKEN"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Nombre Candidato" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Correo" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Estatus" DataField="ESTATUS" UniqueName="ESTATUS"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Fecha de aplic." DataField="FE_TERMINO" UniqueName="FE_TERMINO" DataFormatString="{0:dd/MM/yyyy HH:mm}"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="Enviado" DataField="FG_ENVIO_CORREO" UniqueName="FG_ENVIO_CORREO"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton runat="server" Text="Ver baremos" ID="btnBaremos" OnClientClicked="sustitucionBaremos" AutoPostBack="false"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvResultadosPrueba" runat="server">
                <label class="labelTitulo">Consulta de resultados de prueba</label>
                <div style="height: calc(100% - 100px);">
                    <telerik:RadGrid
                        ID="rgBaterias"
                        runat="server"
                        AutoGenerateColumns="false"
                        EnableHeaderContextMenu="true"
                        Height="100%"
                        ShowGroupPanel="true"
                        AllowSorting="true"
                        HeaderStyle-Font-Bold="true"
                        OnNeedDataSource="rgBaterias_NeedDataSource"
                        AllowMultiRowSelection="false">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="ID_BATERIA,CL_TOKEN" Name="ID_BATERIA,CL_TOKEN" EnableColumnsViewState="false" DataKeyNames="ID_BATERIA,CL_TOKEN" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" PageSize="20">

                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Folio batería" DataField="FL_BATERIA" UniqueName="FL_BATERIA"></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Folio de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" Display="false" DataField="CL_TOKEN" UniqueName="CL_TOKEN"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Nombre Candidato" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Correo" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Estatus" DataField="ESTATUS" UniqueName="ESTATUS"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Fecha de aplic." DataField="FE_TERMINO" UniqueName="FE_TERMINO" DataFormatString="{0:dd/MM/yyyy HH:mm}"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="Enviado" DataField="FG_ENVIO_CORREO" UniqueName="FG_ENVIO_CORREO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton runat="server" Text="Ver resultados de pruebas" ID="btnResultados" OnClientClicked="VentanaResultados" AutoPostBack="false"></telerik:RadButton>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvIntegracion" runat="server">
                <label class="labelTitulo">Consultas de integración</label>
                <div style="height: calc(100% - 100px);">
                    <div style="clear: both"></div>
                    <div class="ctrlBasico">

                        <div style="padding-top: 8px;" class="divControlIzquierdaIntegracion">
                            <label id="Label5" name="lblConsultasParciales" width="260px" runat="server">Consultas parciales(no incluir competencias del puesto con requerimiento 0).</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadButton ID="chkConsultasParciales" runat="server" ToggleType="CheckBox" name="btConsultasParciales" AutoPostBack="false">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                    </div>
                    <div style="clear: both"></div>
                    <div class="ctrlBasico">
                        <div style="padding-top: 8px;" class="divControlIzquierdaIntegracion">
                            <label id="Label18" name="lblIgnorarCompetencias" width="260px" runat="server">Ignorar competencias con calificación cero (Entrevistador).</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadButton ID="chkIgnorarCompetencias" runat="server" ToggleType="CheckBox" name="btConsultasParciales" AutoPostBack="false">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>


                    </div>
                    <div style="clear: both; height: 50px;"></div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardarIntegracion" runat="server" AutoPostBack="true" Text="Guardar" Width="100" OnClick="btnGuardarIntegracion_Click"></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPageView>

        </telerik:RadMultiPage>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server">
        <Windows>
            <telerik:RadWindow ID="rwBaremos" runat="server"  ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Modal="true" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="rwResultados" runat="server" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Modal="true" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winPlantillas" runat="server" Title="Solicitud" Behaviors="Close" Modal="true" VisibleStatusbar="false"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionEmpleados" runat="server" Title="Seleccionar empleado" Height="600px" Width="1100px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <%--<telerik:RadWindow ID="winPlantillas" runat="server" Title="Agregar/Editar Plantilla" Height="500px" Width="900px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>--%>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Height="100%"></telerik:RadWindowManager>
</asp:Content>
