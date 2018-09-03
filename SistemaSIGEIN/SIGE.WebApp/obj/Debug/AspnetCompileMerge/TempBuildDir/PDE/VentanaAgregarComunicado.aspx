<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaAgregarComunicado.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaAgregarComunicado" %>

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

                openChildDialog("../Comunes/SeleccionUsuario.aspx", "winSeleccion", "Selección de usuarios")
            }
            function useDataFromChild(pDato) {
                if (pDato != null) {
                    switch (pDato[0].clTipoCatalogo) {
                        case "EMPLEADO":
                            InsertarEmpleado(EncapsularSeleccion("EMPLEADO", pDato));
                            break;
                        case "PUESTO":
                            InsertarEmpleado(EncapsularSeleccion("PUESTO", pDato));
                            break;
                        case "DEPARTAMENTO":
                            InsertarEmpleado(EncapsularSeleccion("DEPARTAMENTO", pDato));
                            break;
                        case "ADSCRIPCION":
                            InsertarEmpleado(EncapsularSeleccion("ADSCRIPCION", pDato));
                            break;
                        case "USUARIO":
                            InsertarEmpleado(EncapsularSeleccion("USUARIO", pDato));
                            break;
                    }
                }
            }

            function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
                return JSON.stringify({ clTipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
            }

            function InsertarEmpleado(pDato) {
                var ajaxManager = $find('<%= RadAjaxManager1.ClientID%>');
                ajaxManager.ajaxRequest(pDato);
            }

            function AbrirVentanaArchivo() {
                var idArchivo = ('<%= vIdArchivo %>');
                var Title = "Archivo Adjunto";
                var vURL = "VisorDeArchivos.aspx?IdArchivo=" + idArchivo;
                var oWin = window.radopen(vURL, "rwVerArchivos", document.documentElement.clientWidth - 20, document.documentElement.clientHeight - 20);
                oWin.set_title(Title);
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
                    <telerik:AjaxUpdatedControl ControlID="rcbAuto" UpdatePanelHeight="100%"  />
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

                <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion">
                    <Tabs>
                        <telerik:RadTab Text="Selección de empleados"></telerik:RadTab>
                        <telerik:RadTab Text="Comunicado"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: calc(100% - 45px);">
                    <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
                        <telerik:RadPageView ID="rpvSeleccionEmpleados" runat="server">
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
                                <telerik:RadButton  ID="btnSeleccionUsuario" runat="server" name="btnSeleccionUsuario" AutoPostBack="false" Text="Seleccionar por usuario" Width="220" OnClientClicked="AbrirVentanaSeleccionUsuarios"></telerik:RadButton>
                            </div>
                            <telerik:RadLabel runat="server" ID="rlMensajeInformación" Text="Para volver a agregar a un empleado, seleccione el tipo: 'Comunicado' en la otra pestaña." Visible="false"></telerik:RadLabel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvComunicado" runat="server">
                            <div style="height: calc(100% - 60px);">
                                <div style="clear: both; height: 20px;"></div>
                                <label>Título de comunicado:</label>
                                <div style="clear: both; height: 10px;"></div>
                                <telerik:RadTextBox runat="server" ID="txtTituloComunicado" TextMode="SingleLine" Resize="None" Width="80%" Height="40px"></telerik:RadTextBox>
                                <div style="clear: both; height: 10px;"></div>
                                <label>Contenido del comunicado:</label>
                                <div style="clear: both; height: 10px;"></div>
                                <telerik:RadEditor Height="250px" OnClientLoad="TelerikDemo.editor_onClientLoad" Width="80%" ToolbarMode="Default" ToolsWidth="310px" EditModes="Design" ID="txtContenido" runat="server" ToolsFile="~/Assets/AdvancedTools.xml">
                                </telerik:RadEditor>
                                <div style="clear: both; height: 20px;"></div>
                                <div style="float: left;">
                                    <label>Comunicado Visible:</label>
                                </div>
                                <div style="padding-left: 20px; float: left;">
                                    <telerik:RadButton RenderMode="Lightweight" OnClick="rbPermanente_Click" ID="rbPermanente" runat="server" ButtonType="ToggleButton"
                                        ToggleType="Radio" AutoPostBack="true" GroupName="Radios"
                                        ToolTip="Permanente">
                                    </telerik:RadButton>
                                    <label>Permanente</label>
                                    <div style="padding-bottom: 5px; padding-left: 20px;">
                                    </div>
                                    <telerik:RadButton RenderMode="Lightweight" ID="rbRango" OnClick="rbRango_Click" runat="server" ButtonType="ToggleButton"
                                        ToggleType="Radio" AutoPostBack="true" GroupName="Radios" Checked="true"
                                        ToolTip="Rango">
                                    </telerik:RadButton>
                                    <label>Del: </label>
                                    <telerik:RadDatePicker ID="rdtiIniciofecha" Width="150px" runat="server"></telerik:RadDatePicker>
                                    <label>Al: </label>
                                    <telerik:RadDatePicker ID="rdtFinfecha" Width="150px" runat="server" Enabled="true"></telerik:RadDatePicker>
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                <telerik:RadCheckBox runat="server" ID="rcbprivado" Text="marcar como privado" AutoPostBack="false" Checked="false"></telerik:RadCheckBox>
                                </div>
                                <div style="clear: both; height: 10px;"></div>
                                <div class="divControlIzquierda">
                                    <label>Tipo:</label>
                                </div>
                                <div class="divControlIzquierda">
                                    &nbsp;&nbsp;<telerik:RadButton RenderMode="Lightweight" ID="Tipo1" runat="server" ButtonType="ToggleButton"
                                        ToggleType="Radio" GroupName="Tipos" Text="Comunicado" Checked="true" OnClick="Tipo1_Click" AutoPostBack="true">
                                    </telerik:RadButton>
                                </div>
                                <div class="divControlIzquierda">
                                    <telerik:RadButton RenderMode="Lightweight" ID="Tipo2" runat="server" ButtonType="ToggleButton"
                                        ToggleType="Radio" GroupName="Tipos" Text="Descriptivo" OnClick="Tipo2_Click" AutoPostBack="true">
                                    </telerik:RadButton>
                                </div>
                                <div class="divControlIzquierda">
                                    <telerik:RadButton RenderMode="Lightweight" ID="Tipo3" runat="server" ButtonType="ToggleButton"
                                        ToggleType="Radio" GroupName="Tipos" Text="Inventario" OnClick="Tipo3_Click" AutoPostBack="true">
                                    </telerik:RadButton>
                                </div>
                                <div class="divControlDerecha" style="margin-right: 10%;">
                                    <telerik:RadAsyncUpload CssClass="Uploadfiles" ID="rauArchivo" runat="server" Width="250px" MaxFileInputsCount="1" Localization-Cancel="Cancelar"
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
                                <div class="divControlIzquierda">
                                    <telerik:RadButton ID="rbAgregar" UseSubmitBehavior="false" runat="server" AutoPostBack="true" Text="Agregar" Width="100" OnClick="rbAgregar_Click"></telerik:RadButton>
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
            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px">
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

</asp:Content>
