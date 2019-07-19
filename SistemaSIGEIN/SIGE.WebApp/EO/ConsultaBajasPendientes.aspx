<%@ Page Title="" Language="C#" MasterPageFile="~/EO/MenuEO.master" AutoEventWireup="true" CodeBehind="ConsultaBajasPendientes.aspx.cs" Inherits="SIGE.WebApp.EO.ConsultaBajasPendientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var idEmpleadoBaja = "";
        var idEmpleado = "";

        function OpenWindowCapturarBaja() {
            obtenerIdFila();
            if (idEmpleadoBaja != "")
                openChildDialog("CapturarBajaPendiente.aspx?pIdEmpleadoBaja=" + idEmpleadoBaja + "&pIdEmpleado=" + idEmpleado, "winBajaPendiente", "Capturar baja pendiente");
            else
                radalert("Selecciona un empleado.", 400, 150, "Aviso");
        }

        function ConfirmarCancelar(sender, args) {
            var MasterTable = $find("<%=rgBajasPendientes.ClientID %>").get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            var row = selectedRows[0];
            if (row != null) {
                CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_EMPLEADO");
                if (selectedRows != "") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            this.click();
                        }
                    });
                    radconfirm('¿Deseas cancelar la baja de "' + CELL_NOMBRE.innerHTML + '"?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Aviso");
                    args.set_cancel(true);
                }
            } else {
                radalert("Selecciona un empleado", 400, 150, "Aviso");
                args.set_cancel(true);
            }
        }

        function obtenerIdFila() {
            idEmpleadoBaja = "";
            idEmpleado = "";
            var grid = $find("<%=rgBajasPendientes.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                idEmpleadoBaja = SelectDataItem.getDataKeyValue("ID_BAJA_EMPLEADO");
                idEmpleado = SelectDataItem.getDataKeyValue("ID_EMPLEADO");
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <label class="labelTitulo">Bajas Pendientes</label>
        <div style="height: calc(100% - 100px);">
            <telerik:RadMultiPage ID="rmpBajasPendientes" runat="server" SelectedIndex="0" Height="100%">
                <telerik:RadPageView ID="rpvBajaPendientes" runat="server">
                    <div style="clear: both;"></div>
                    <telerik:RadGrid
                        ID="rgBajasPendientes" HeaderStyle-Font-Bold="true"
                        runat="server"
                        OnNeedDataSource="rgBajasPendientes_NeedDataSource"
                        AutoGenerateColumns="false"
                        Height="100%"
                        OnItemDataBound="rgBajasPendientes_ItemDataBound">
                        <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView DataKeyNames="ID_BAJA_EMPLEADO, ID_EMPLEADO, NB_EMPLEADO" ClientDataKeyNames="ID_BAJA_EMPLEADO, ID_EMPLEADO,NB_EMPLEADO" AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="true" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="70" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="160" HeaderText="Nombre completo" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="160" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
        <div style="height: 10px; clear: both"></div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCapturarBaja" runat="server" name="btnCapturarBaja" AutoPostBack="false" Text="Capturar baja" OnClientClicked="OpenWindowCapturarBaja"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelarBaja" runat="server" Text="Cancelar baja" AutoPostBack="true" OnClientClicking="ConfirmarCancelar" OnClick="btnCancelarBaja_Click"></telerik:RadButton>
        </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="WinConsultaPersonal"
                runat="server"
                Width="1050px"
                Height="580px"
                VisibleStatusbar="false"
                ShowContentDuringLoad="false"
                Behaviors="Close"
                Modal="true"
                Animation="Fade">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winBajaPendiente" runat="server" Animation="Fade" Width="600" Height="560" VisibleStatusbar="false" ShowContentDuringLoad="true" Behaviors="Close" Modal="true"></telerik:RadWindow>
            <telerik:RadWindow ID="WinSeleccionCausa" runat="server" Width="1050px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
