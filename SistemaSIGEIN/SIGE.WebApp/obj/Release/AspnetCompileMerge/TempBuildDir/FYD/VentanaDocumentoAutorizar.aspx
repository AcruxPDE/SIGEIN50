<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaDocumentoAutorizar.aspx.cs" Inherits="SIGE.WebApp.FYD.DocumetoAutorizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
        }

        function onCloseWindow(oWnd, args) {
            $find("<%=grdDocumentosAutorizar.ClientID%>").get_masterTableView().rebind();
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 30,
                height: browserWnd.innerHeight - 30
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenEmployeeSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx", "winSeleccion", "Selección de empleado")
        }


        function useDataFromChild(pDato) {
            if (pDato != null) {
                console.info(pDato);
                var vDatosSeleccionados = pDato[0];
                var arr = [];
                var datos = new Array();

                switch (vDatosSeleccionados.clTipoCatalogo) {
                    case "EMPLEADO":
                        for (var i = 0; i < pDato.length; ++i) {
                            arr.push(pDato[i].idEmpleado);
                        }
                        datos = arr;
                        break;
                }
                var ajaxManager = $find("<%= ramDocumentosAutorizar.ClientID %>");
                ajaxManager.ajaxRequest(JSON.stringify(datos) + "_" + vDatosSeleccionados.clTipoCatalogo); //Making ajax request with the argument 
            }
        }


        function ConfirmarEliminarEmpleados(sender, args) {
            var MasterTable = $find("<%=grdDocumentosAutorizar.ClientID %>").get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            var row = selectedRows[0];

            if (row != null) {
                CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_EMPLEADO");
                if (selectedRows != "") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas eliminar el empleado ' + CELL_NOMBRE.innerHTML + ' ?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                    args.set_cancel(true);
                }
            } else {
                radalert("Seleccione un empleado", 400, 150, "");
                args.set_cancel(true);
            }
        }


        function ConfirmarEnviarCorreos(sender, args) {
            var MasterTable = $find("<%=grdDocumentosAutorizar.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row != null) {
                    //
                } else {
                    radalert("Seleccione un empleado", 400, 150, "");
                    args.set_cancel(true);
                }
            }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramDocumentosAutorizar" runat="server" OnAjaxRequest="ramProgramasCapacitacion_AjaxRequest">

        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSeleccionar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocumentosAutorizar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="btnAceptar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocumentosAutorizar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>


            <telerik:AjaxSetting AjaxControlID="ramDocumentosAutorizar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocumentosAutorizar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocumentosAutorizar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grdDocumentosAutorizar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocumentosAutorizar" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="clear: both; height: 5px;"></div>

    <div style="height: calc(100% - 50px);">

        <div class="ctrlBasico">
            <div style="width: 150px; margin-right: 15px; float: left;">
                <label id="lblClaveDocumento" name="lblClaveDocumento" runat="server">Clave del documento:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtClDocumento" runat="server" Width="200px" MaxLength="1000"></telerik:RadTextBox>
                <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator1" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtClDocumento" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>

            </div>
        </div>

        <div class="ctrlBasico">
            <div style="width: 150px; margin-right: 15px; float: left;">
                <label id="Label4" name="lblFechaElaboracion" runat="server">Fecha de elaboración:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadDatePicker ID="rdpFeElaboracion" Width="200" runat="server"></telerik:RadDatePicker>
                <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator3" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="rdpFeElaboracion" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div style="clear: both;"></div>

        <div class="ctrlBasico">

            <div style="width: 150px; margin-right: 15px; float: left;">
                <label id="lblVersionDocumento" name="lblVersionDocumento" runat="server">Versión:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtVersionDocumento" runat="server" Width="200px" MaxLength="1000"></telerik:RadTextBox>
                <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator2" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtVersionDocumento" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="ctrlBasico">
            <div style="width: 150px; margin-right: 15px; float: left;">
                <label id="Label1" name="lblFechaRevision" runat="server">Fecha de revisión:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadDatePicker ID="rdpFechaRevision" Width="200" runat="server"></telerik:RadDatePicker>
                <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator4" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="rdpFechaRevision" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div style="clear: both;"></div>

        <div class="ctrlBasico">
            <div style="width: 150px; margin-right: 15px; float: left;">
                <label id="lblElaboro" name="lblElaboro" runat="server">Elaboró:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtElaboro" runat="server" Width="400px" MaxLength="1000" Enabled="true"></telerik:RadTextBox>
            </div>
        </div>

        <div style="clear: both;"></div>

        <div class="ctrlBasico" style="width: 100%; height: calc(100% - 150px);">

            <telerik:RadGrid ID="grdDocumentosAutorizar" ShowHeader="true" HeaderStyle-Font-Bold="true" runat="server" GroupPanelPosition="Top" AllowMultiRowSelection="true">
                <ClientSettings>
                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                    <Selecting AllowRowSelect="true" />
                </ClientSettings>
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView ClientDataKeyNames="ID_AUTORIZACION" DataKeyNames="ID_AUTORIZACION" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10">
                    <Columns>
                        <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre de quién autoriza" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto de quién autoriza" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Email al que se enviará solicitud de Autorización " HeaderStyle-Width="150" FilterControlWidth="80" AutoPostBackOnFilter="false" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO">
                            <ItemTemplate>
                                <telerik:RadTextBox ID="txtClCorreoElectronico" runat="server" Width="210" Text='<%#Eval("CL_CORREO_ELECTRONICO") %>'></telerik:RadTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus Autorización" DataField="CL_ESTADO" UniqueName="CL_ESTADO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha Autorización" DataField="FE_AUTORIZACION" UniqueName="FE_AUTORIZACION" HeaderStyle-Width="100" FilterControlWidth="30" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Observaciones" DataField="DS_OBSERVACIONES" UniqueName="DS_OBSERVACIONES" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

        </div>

        <div class="ctrlBasico">
            <telerik:RadButton ID="btnSeleccionar" AutoPostBack="false" runat="server" Text="Seleccionar" CssClass="ctrlBasico" OnClientClicked="OpenEmployeeSelectionWindow"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" CssClass="ctrlBasico" OnClick="btnEliminar_Click" OnClientClicking="ConfirmarEliminarEmpleados"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEnviar" runat="server" Text="Enviar" CssClass="ctrlBasico" OnClick="btnEnviar_Click" OnClientClicking="ConfirmarEnviarCorreos"></telerik:RadButton>
        </div>

    </div>

    <div class="divControlDerecha" style="margin-right: 10px;">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnAceptar" runat="server" Text="Aceptar" CssClass="ctrlBasico" OnClick="btnAceptar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" CssClass="ctrlBasico" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>

    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
</asp:Content>
