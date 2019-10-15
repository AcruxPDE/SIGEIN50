<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaAgregarCompromiso.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaAgregarCompromiso" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .reTool.reInsertSpecialLink {
            background-image: url(Custom.gif) !important;
            background-repeat: no-repeat;
        }

        .reTool.rePrintPreview {
            background-image: url(CustomDialog.gif) !important;
            background-repeat: no-repeat;
        }

        .reTool.reInsertEmoticon {
            background-image: url(Emoticons/1.gif) !important;
            background-repeat: no-repeat;
        }

            .reTool.reInsertSpecialLink:before,
            .reTool.rePrintPreview:before,
            .reTool.reInsertEmoticon:before {
                display: none;
            }
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function closeWindow() {
                GetRadWindow().close();
            }

            function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 20,
                    height: browserWnd.innerHeight - 20
                };
                openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
            }

            function onCloseWindow(oWnd, args) {
                $find("<%=grdEmpleadosSeleccionados.ClientID%>").get_masterTableView().rebind();
            }

            (function (global, undefined) {
                var EditorCommandList;

                var registerCustomCommands = function () {
                    EditorCommandList["InsertSpecialLink"] = function (commandName, editor, args) {
                        var elem = editor.getSelectedElement(); //returns the selected element.

                        if (elem && elem.tagName == "A") {
                            editor.selectElement(elem);
                            argument = elem.cloneNode(true);
                        }
                        else {
                            var content = editor.getSelectionHtml();
                            var link = editor.get_document().createElement("A");
                            link.innerHTML = content;
                            argument = link;
                        }

                        var myCallbackFunction = function (sender, args) {
                            editor.pasteHyperLink(args, "Insert Link");
                        };

                        editor.showExternalDialog(
                        "InsertLink.aspx",
                        argument,
                        270,
                        200,
                        myCallbackFunction,
                        null,
                        "Insert Link",
                        true,
                        Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
                        false,
                        true);
                    };

                    EditorCommandList["PrintPreview"] = function (commandName, editor, args) {
                        var args = editor.get_html(true); //returns the HTML of the selection.  

                        editor.showExternalDialog(
                        "PrintPreview.aspx",
                        args,
                        570,
                        420,
                        null,
                        null,
                        "Print Preview",
                        true,
                        Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
                        true,
                        true);
                    };

                    EditorCommandList["InsertEmoticon"] = function (commandName, editor, args) {
                        var myCallbackFunction = function (sender, args) {
                            editor.pasteHtml(String.format("<img src='{0}' border='0' align='middle' alt='emoticon' /> ", args.image));
                        }

                        editor.showExternalDialog(
                        "InsertEmoticon.aspx",
                        {},
                        400,
                        310,
                        myCallbackFunction,
                        null,
                        "Insert Emoticon",
                        true,
                        Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move,
                        false,
                        true);
                    };
                };

                var TelerikDemo = global.TelerikDemo = {
                    editor_onClientLoad: function (editor, args) {
                        EditorCommandList = Telerik.Web.UI.Editor.CommandList;
                        registerCustomCommands();
                    }
                };
            })(window);

            function AbrirVentanaSeleccionPuesto() {
                openChildDialog("../Comunes/SeleccionPuesto.aspx?mulSel=1", "winSeleccion", "Selección por puestos")
            }

            function AbrirVentanaSeleccionDepartamento() {
                openChildDialog("../Comunes/SeleccionArea.aspx", "winSeleccion", "Selección por departamentos")
            }

            function AbrirVentanaSeleccionEmpleado() {

                openChildDialog("../Comunes/SeleccionEmpleadoPde.aspx", "winSeleccion", "Selección de empleados")
            }

            function AbrirVentanaSeleccionAdscripcion() {

                openChildDialog("../Comunes/SeleccionAdscripcion.aspx", "winSeleccion", "Selección por adscripción")
            }
            function AbrirVentanaSeleccionUsuarios() {

                openChildDialog("../Comunes/SeleccionUsuario.aspx?vClTipoUsuario=TODOS", "winSeleccion", "Selección de usuarios")
            }


            

            function AbrirVentanaArchivo() {
                var idArchivo = ('<%= vIdArchivo %>');
                var Title = "Archivo Adjunto";
                var vURL = "VisorDeArchivos.aspx?IdArchivo=" + idArchivo;
                var oWin = window.radopen(vURL, "rwVerArchivos", document.documentElement.clientWidth - 20, document.documentElement.clientHeight - 20);
                oWin.set_title(Title);
            }

            function OpenEmployeeSelectionWindow() {
                var VarIdPlaza = ('<%= vIdPlaza %>');
                var VarIdEmpleado = ('<%= vIdEmpleado %>');
                if (VarIdPlaza == "") {
                    OpenSelectionWindow("../Comunes/SeleccionUsuario.aspx?mulSel=0", "winSeleccion", "Selección de usuarios")
                } else {
                    if (VarIdEmpleado != 0) {
                        var currentWnd = GetRadWindow();
                        var browserWnd = window;
                        if (currentWnd)
                            browserWnd = currentWnd.BrowserWindow;
                        browserWnd.radalert("Para asignar esta plaza a otra persona, es importante que se libere desde el inventario de personal.", 400, 180, "Aviso");
                    } else { OpenSelectionWindow("../Comunes/SeleccionUsuario.aspx?mulSel=0", "winSeleccion", "Selección de empleado") }
                }
            }

            function CleanEmployeeSelection(sender, args) {
                var VarIdPlaza = ('<%= vIdPlaza %>');
                var VarIdEmpleado = ('<%= vIdEmpleado %>');
                if (VarIdPlaza == "") {
                    ChangeListItem("", "No Seleccionado", $find("<%=lstEmpleado.ClientID %>"));
                }
                else {

                    if (VarIdEmpleado != 0) {

                        var currentWnd = GetRadWindow();
                        var browserWnd = window;
                        if (currentWnd)
                            browserWnd = currentWnd.BrowserWindow;
                        browserWnd.radalert("Para asignar esta plaza a otra persona, es importante que se libere desde el inventario de personal.", 400, 180, "Aviso");
                    } else { ChangeListItem("", "No Seleccionado", $find("<%=lstEmpleado.ClientID %>")); }
                }
            }


            function useDataFromChild(pDato) {
               
                if (pDato != null) {
                    console.info(pDato);
                    var vDatosSeleccionados = pDato[0];
                    var vLstDato = {
                        idItem: "",
                        nbItem: ""
                    };

                    var list;

                    switch (vDatosSeleccionados.clTipoCatalogo) {

                        case "USUARIO":
                            list = $find("<%=lstEmpleado.ClientID %>");
                            vLstDato.idItem = vDatosSeleccionados.idUsuario;
                            vLstDato.nbItem = vDatosSeleccionados.nbUsuario;
                            break; 
   
                       
                    }

                    if (list)
                        ChangeListItem(vLstDato.idItem, vLstDato.nbItem, list);
                }
            }

      
         

            //FUNCTION INSERTAR DATO
            function InsertarDato(pDato) {
                var ajaxManager = $find('<%= RadAjaxManager1.ClientID %>');
                ajaxManager.ajaxRequest(pDato);
            }

            //FUNCION ENCAPSULAR DATO
         
            function EncapsularDatos(pClTipoDato, pLstDatos) {
                return JSON.stringify({ clTipo: pClTipoDato, oSeleccion: pLstDatos });
            }

            function ChangeListItem(pIdItem, pNbItem, pList) {
                var vListBox = pList;
                vListBox.trackChanges();

                var items = vListBox.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(pNbItem);
                item.set_value(pIdItem);
                items.add(item);
                item.set_selected(true);
                vListBox.commitChanges();
            }



        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdEmpleadosSeleccionados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosSeleccionados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridSeleccion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridSeleccion" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="Uploadfiles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosSeleccionados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rnMensaje">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridSeleccion" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rbAgregar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbAuto" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridSeleccion" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rbAgregar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtContenido" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rcbAuto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridSeleccion" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleadosSeleccionados" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridSeleccion" UpdatePanelHeight="100%" />

                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rbRango">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rbPermanente" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rdtFinfecha" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rbPermanente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rbRango" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rdtFinfecha" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="txtContenido">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Tipo1" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="Tipo2" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="Tipo3" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="Tipo1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Tipo2" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="Tipo3" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridSeleccion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnPuestos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rcbAuto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbHabilitar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbLectura" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbEditar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbAgregar2" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbAgregar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarEmpleados" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarPuestos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarAreas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlMensajeInformación" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarAdscripcion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtContenido" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionUsuario" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />

                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="Tipo2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Tipo1" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="Tipo3" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridSeleccion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnPuestos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rcbAuto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbHabilitar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbLectura" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbEditar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbAgregar2" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbAgregar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarEmpleados" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarPuestos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarAreas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlMensajeInformación" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarAdscripcion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtContenido" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionUsuario" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />



                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="Tipo3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Tipo1" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="Tipo2" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridSeleccion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnPuestos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rcbAuto" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbHabilitar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbLectura" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbEditar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbAgregar2" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rbAgregar" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarEmpleados" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarPuestos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarAreas" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlMensajeInformación" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionarAdscripcion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="txtContenido" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnSeleccionUsuario" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />

                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="height: calc(100% - 5px);">
        <telerik:RadSplitter ID="rsSolicitud" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="rpSolicitud" runat="server">

                <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="1" MultiPageID="rmpConfiguracion">
                    <Tabs>
                        <telerik:RadTab Text="Selección de empleados" Visible="false"></telerik:RadTab>
                        <telerik:RadTab Text="Compromiso" Visible="false"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: calc(100% - 45px);">
                    <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="1" Height="100%">
                        <telerik:RadPageView ID="rpvSeleccionEmpleados" runat="server" Visible="false">
                            <div style="clear: both; height: 10px;"></div>
                            <div style="height: calc(100% - 70px);">
                                <telerik:RadGrid ID="grdEmpleadosSeleccionados"
                                    runat="server"
                                    Height="100%"
                                    Width="950"
                                    AllowSorting="true"
                                    ShowHeader="true"
                                    HeaderStyle-Font-Bold="true"
                                    AllowMultiRowSelection="true"
                                    OnNeedDataSource="grdEmpleadosSeleccionados_NeedDataSource"
                                    OnItemCommand="grdEmpleadosSeleccionados_ItemCommand">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView DataKeyNames="M_CL_USUARIO,ID_EMPLEADO" ClientDataKeyNames="M_CL_USUARIO,ID_EMPLEADO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="350" HeaderText="NOMBRE" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="280" HeaderText="PUESTO" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn UniqueName="BTNELIMINAR" Text="Eliminar" CommandName="Delete" HeaderStyle-Width="100">
                                                <ItemStyle Width="10%" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div style="clear: both; height: 20px;"></div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnSeleccionarEmpleados" runat="server" name="btnSeleccionarEmpleados" AutoPostBack="false" Text="Seleccionar Empleados" Width="200" OnClientClicked="AbrirVentanaSeleccionEmpleado"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnSeleccionarPuestos" runat="server" name="btnSeleccionarPuestos" AutoPostBack="false" Text="Seleccionar por puesto" Width="200" OnClientClicked="AbrirVentanaSeleccionPuesto"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnSeleccionarAreas" runat="server" name="btnSeleccionarAreas" AutoPostBack="false" Text="Seleccionar por departamento" Width="220" OnClientClicked="AbrirVentanaSeleccionDepartamento"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton Visible="false" ID="btnSeleccionarAdscripcion" runat="server" name="btnSeleccionarAdscripcion" AutoPostBack="false" Text="Seleccionar por adscripción" Width="220" OnClientClicked="AbrirVentanaSeleccionAdscripcion"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnSeleccionUsuario" runat="server" name="btnSeleccionUsuario" AutoPostBack="false" Text="Seleccionar por usuario" Width="220" OnClientClicked="AbrirVentanaSeleccionUsuarios"></telerik:RadButton>
                            </div>
                            <telerik:RadLabel runat="server" ID="rlMensajeInformación" Text="Para volver a agregar a un empleado, seleccione el tipo: 'Comunicado' en la otra pestaña." Visible="false"></telerik:RadLabel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvCompromiso" runat="server">
                            <div style="height: calc(100% - 60px);">
                                <div style="clear: both; height: 20px;"></div>
                                <label>Título de compromiso:</label>
                                <div style="clear: both; height: 10px;"></div>
                                <telerik:RadTextBox runat="server" ID="txtTituloCompromiso" TextMode="SingleLine" Resize="None" Width="80%" Height="40px"></telerik:RadTextBox>
                                <div style="clear: both; height: 10px;"></div>
                                <label>Descripción del compromiso:</label>
                                <div style="clear: both; height: 10px;"></div>
                                <telerik:RadEditor Height="250px" OnClientLoad="TelerikDemo.editor_onClientLoad" Width="80%" ToolbarMode="Default" ToolsWidth="310px" EditModes="Design" ID="txtContenido" runat="server" ToolsFile="~/Assets/AdvancedTools.xml">
                                </telerik:RadEditor>
                                <div style="clear: both; height: 20px;"></div>
                                <div style="float: left;">
                                </div>
                                <div style="padding-left: 20px; float: left;">
                                    <telerik:RadButton RenderMode="Lightweight" OnClick="rbPermanente_Click" ID="rbPermanente" runat="server" ButtonType="ToggleButton"
                                        ToggleType="Radio" AutoPostBack="true" GroupName="Radios"
                                        ToolTip="Permanente" Visible="false">
                                    </telerik:RadButton>

                                    <div style="padding-bottom: 5px; padding-left: 20px;">
                                    </div>
                                    <telerik:RadButton RenderMode="Lightweight" ID="rbRango" OnClick="rbRango_Click" runat="server" ButtonType="ToggleButton"
                                        ToggleType="Radio" AutoPostBack="true" GroupName="Radios" Checked="true"
                                        ToolTip="Rango" Visible="false">
                                    </telerik:RadButton>
                                    <label>Fecha de Creación: </label>
                                    <telerik:RadDatePicker ID="rdtiIniciofecha" Width="150px" runat="server" ></telerik:RadDatePicker>
                                    <label>Fecha de Entrega: </label>
                                    <telerik:RadDatePicker ID="rdtFinfecha" Width="150px" runat="server" Enabled="true"></telerik:RadDatePicker>
                                    <label>Fecha de Negociable: </label>
                                    <telerik:RadDatePicker ID="rdtNegociableFecha" Width="150px" runat="server" Enabled="true"></telerik:RadDatePicker>
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                <telerik:RadCheckBox runat="server" ID="rcbprivado" Text="marcar como privado" AutoPostBack="false" Checked="false" Visible="false"></telerik:RadCheckBox>
                                </div>
                                <div style="clear: both; height: 10px;"></div>
                                <div class="divControlIzquierda">
                                    <div class="divControlIzquierda">
                                        <asp:Label ID="lbCalificación" runat="server" Text="Calificación" HeaderStyle-Width="100"></asp:Label>
                                        <telerik:RadComboBox ID="cmbCalificacion" runat="server" EmptyMessage="Seleccione..." AutoPostBack="false">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div style="clear: both; height: 5px;"></div>
                                    <div class="divControlIzquierda">
                                        <asp:Label ID="lbAsignado" runat="server" Text="Asiganado a:" HeaderStyle-Width="100"></asp:Label>
                                        <telerik:RadListBox ID="lstEmpleado" Style="text-align:center" Width="160"  runat="server" OnClientItemDoubleClicking="OpenEmployeeSelectionWindow" Enabled="true">
                                            <Items>
                                                <telerik:RadListBoxItem Text="No Seleccionado"  Value="" />

                                            </Items>
                                        </telerik:RadListBox>
                                        <telerik:RadButton ID="btnBuscarEmpleado"  runat="server" Text="B" OnClientClicked="OpenEmployeeSelectionWindow" AutoPostBack="false" Enabled="true"></telerik:RadButton>
                                        <telerik:RadButton ID="btnLimpiarEmpleado" runat="server" Text="X" OnClientClicked="CleanEmployeeSelection" AutoPostBack="false" Enabled="true"></telerik:RadButton>
                                    </div>
                                    <div style="clear: both; height: 5px;"></div>
                                    <div class="divControlIzquierda">
                                        <asp:Label ID="lbTipoCompromiso" runat="server" Text="Tipo de Compromiso" HeaderStyle-Width="100"></asp:Label>
                                        <telerik:RadComboBox ID="cmbTipoCompromiso" runat="server" EmptyMessage="Seleccione..." AutoPostBack="false">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div style="clear: both; height: 5px;"></div>
                                    <div class="divControlIzquierda">
                                        <asp:Label ID="lbPrioridad" runat="server" Text="Prioridad" HeaderStyle-Width="100"></asp:Label>
                                        <telerik:RadComboBox ID="cmbPrioridad" runat="server" EmptyMessage="Seleccione..." AutoPostBack="false">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div style="clear: both; height: 5px;"></div>
                                </div>
                                <div class="divControlIzquierda">
                                    &nbsp;&nbsp;<telerik:RadButton RenderMode="Lightweight" ID="Tipo1" runat="server" ButtonType="ToggleButton"
                                        ToggleType="Radio" GroupName="Tipos" Text="Comunicado" Checked="true" OnClick="Tipo1_Click" AutoPostBack="true" Visible="false">
                                    </telerik:RadButton>
                                </div>
                                <div class="divControlIzquierda">
                                    <telerik:RadButton RenderMode="Lightweight" ID="Tipo2" runat="server" ButtonType="ToggleButton"
                                        ToggleType="Radio" GroupName="Tipos" Text="Descriptivo" OnClick="Tipo2_Click" AutoPostBack="true" Visible="false">
                                    </telerik:RadButton>
                                </div>
                                <div class="divControlIzquierda">
                                    <telerik:RadButton RenderMode="Lightweight" ID="Tipo3" runat="server" ButtonType="ToggleButton"
                                        ToggleType="Radio" GroupName="Tipos" Text="Inventario" OnClick="Tipo3_Click" AutoPostBack="true" Visible="false">
                                    </telerik:RadButton>
                                </div>
                                <div class="divControlDerecha" style="margin-right: 10%;">
                                    <telerik:RadAsyncUpload CssClass="Uploadfiles" ID="rauArchivo" runat="server" Width="250px" Height="150px" MaxFileInputsCount="1" Localization-Cancel="Cancelar"
                                        Localization-Remove="Quitar" Localization-Select="Adjuntar" Font-Size="12px">
                                    </telerik:RadAsyncUpload>
                                </div>
                                <div style="clear: both; height: 10px;"></div>
                                <div class="divControlDerecha" style="margin-right: 20%;">
                                    <telerik:RadCheckBox runat="server" ID="rcbEliminarAdjunto" Text="Eliminar Archivo Adjunto" AutoPostBack="false" Visible="false"></telerik:RadCheckBox>
                                </div>
                                <div class="divControlDerecha" style="cursor: pointer;">
                                    <img id="ArchivosAdjuntos" visible="false" runat="server" src="../Assets/images/PDE/Adjunto.png" class="img-responsive" width="17" height="17" onclick="AbrirVentanaArchivo()" />
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadButton UseSubmitBehavior="false" runat="server" AutoPostBack="true" Text="Cancelar" Width="100"></telerik:RadButton>
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadButton ID="rbEnviar" UseSubmitBehavior="false" runat="server" AutoPostBack="true" Text="Enviar" Width="100" OnClick="rbEnviar_Click"></telerik:RadButton>
                                </div>
                                <div style="clear: both; height: 10px;"></div>
                                <telerik:RadGrid ID="RadGridSeleccion"
                                    Visible="false"
                                    runat="server"
                                    Height="100%"
                                    HeaderStyle-Font-Bold="true"
                                    Width="950"
                                    AllowSorting="true"
                                    ShowHeader="true"
                                    AllowMultiRowSelection="true"
                                    OnNeedDataSource="RadGridSeleccion_NeedDataSource"
                                    OnItemCommand="RadGridSeleccion_ItemCommand">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView DataKeyNames="ID_EMPLEADO" ClientDataKeyNames="ID_EMPLEADO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="350" HeaderText="NOMBRE" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="280" HeaderText="PUESTO" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn UniqueName="BTNELIMINAR" Text="Eliminar" CommandName="Delete" HeaderStyle-Width="100">
                                                <ItemStyle Width="10%" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <div style="clear: both; height: 20px;"></div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnSeleccion" Visible="false" runat="server" name="btnSeleccion" AutoPostBack="true" Text="Seleccionar Empleados" Width="200" OnClientClicked="AbrirVentanaSeleccionEmpleado"></telerik:RadButton>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnPuestos" runat="server" name="btnSeleccionarPuestos" AutoPostBack="false" Text="Seleccionar por puesto" Width="200" OnClientClicked="AbrirVentanaSeleccionPuesto" Visible="false"></telerik:RadButton>
                                </div>
                                <div>
                                    <div class="divControlIzquierda">
                                        <telerik:RadCheckBox runat="server" ID="rcbAuto" Text="Personal" AutoPostBack="true" Visible="false" OnClick="rcbAuto_Click"></telerik:RadCheckBox>
                                    </div>
                                    <div class="divControlIzquierda">
                                        <telerik:RadLabel runat="server" ID="rbHabilitar" Text="Permisos:" Visible="false" Font-Bold="true"></telerik:RadLabel>
                                    </div>
                                    <div class="divControlIzquierda">
                                        <telerik:RadButton RenderMode="Lightweight" ID="rbLectura" AccessKey="2" runat="server" ButtonType="ToggleButton"
                                            ToggleType="Radio" GroupName="tareas" Text="Lectura" Visible="false">
                                        </telerik:RadButton>
                                    </div>
                                    <div class="divControlIzquierda">
                                        <telerik:RadButton RenderMode="Lightweight" ID="rbEditar" AccessKey="2" runat="server" ButtonType="ToggleButton"
                                            ToggleType="Radio" GroupName="tareas" Text="Edición" Visible="false">
                                        </telerik:RadButton>
                                    </div>
                                </div>
                                <div style="clear: both; height: 20px;"></div>
                                <div class="divControlIzquierda">
                                    <telerik:RadButton ID="rbAgregar2" Visible="false" runat="server" AutoPostBack="true" Text="Agregar" Width="100" OnClick="rbAgregar2_Click"></telerik:RadButton>
                                </div>
                            </div>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px" Visible="false">
                <telerik:RadSlidingZone ID="RadSlidingPane2" runat="server" SlideDirection="Left" ExpandedPaneId="rspEdicionDeTramites" Width="20px">
                    <telerik:RadSlidingPane DockOnOpen="true" ID="rspEdicionDeTramites" runat="server" Title="Ayuda" Width="200px" RenderMode="Mobile" Height="80%">
                        <div style="text-align: left;">
                            <telerik:RadLabel ID="pIns" runat="server"></telerik:RadLabel>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <telerik:RadWindowManager ID="rwmEdicionFormatos" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="rwComunicado"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>
            <telerik:RadWindow
                ID="rwVerArchivos"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnExito" runat="server"></telerik:RadWindowManager>

    

</asp:Content>
