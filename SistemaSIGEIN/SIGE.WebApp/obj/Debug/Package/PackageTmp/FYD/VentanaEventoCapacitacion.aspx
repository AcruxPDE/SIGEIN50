<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaEventoCapacitacion.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaEventoCapacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style>
        .Etiqueta {
            width: 200px;
            text-align: right;
        }
    </style>

    <script type="text/javascript">
        var programa = "";
        var curso = "";
        var vinculo_check = "";
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

        function OpenSelectionCursoWindow(sender, args) {
            var vinculo = $find("<%= rbVinculado.ClientID %>");
            if (vinculo.get_checked() == true) {

                vinculo_check = "Si";
            }

            var novinculo = $find("<%= rbNoVinculado.ClientID %>");
            if (novinculo.get_checked() == true) {
                vinculo_check = "No";
            }
            if ('<%#vIdPrograma%>' != "0") {
                programa = '<%#vIdPrograma%>';
            }
            if (vinculo_check == "No") {
                programa = "";
            }
            var list = $find("<%= rlbPrograma.ClientID %>");
            var item = list.getItem(0);
            var valorPrograma = item.get_text();
            if (valorPrograma == "No Seleccionado" && vinculo_check == "Si") {
                radalert("Selecciona un programa.", 400, 150);
                return;
            } else {
                OpenSelectionWindow("../Comunes/SeleccionCurso.aspx?Idprograma=" + programa + "&pVinculado=" + vinculo_check + "&mulSel=0", "winSeleccion", "Selección de curso");

            }
        }


        //Eliminar el tooltip del control
        function pageLoad() {
            var datePicker = $find("<%= dtpEvaluacion.ClientID %>");
            datePicker.get_popupButton().title = "";
            var datePicker2 = $find("<%=dtpInicial.ClientID %>");
            datePicker2.get_popupButton().title = "";
            var datePicker3 = $find("<%=dtpFinal.ClientID %>");
            datePicker3.get_popupButton().title = "";
            var datePicker4 = $find("<%=dpFecha.ClientID %>");
            datePicker4.get_popupButton().title = "";
            //var datePicker5 = $find("<=tpHoraInicial.ClientID %>");
            //datePicker5.get_popupButton().title = "";
            //var datePicker6 = $find("<=tpHorafinal.ClientID %>");
            //datePicker6.get_popupButton().title = "";
            
        }


        function OpenSelectionProgramaWindow(sender, args) {

            OpenSelectionWindow("../Comunes/SeleccionProgramaCapacitacion.aspx?vClTipoSeleccion=TERMINADO&mulSel=0", "winSeleccion", "Selección de programa de capacitación");
        }

        function OpenSelectionInstructorWindow(sender, args) {
            if ('<%#vIdCurso%>' != "0") {
                curso = '<%#vIdCurso%>';
            } 
            if (curso == "") {
                curso = ""
            }
           
            var list = $find("<%= rlbCurso.ClientID %>");
            var item = list.getItem(0);
            var valorCurso = item.get_text();
            if (valorCurso != "No Seleccionado") {
                OpenSelectionWindow("../Comunes/SeleccionInstructor.aspx?IdCursoInstructor=" + curso + "&mulSel=0", "winSeleccion", "Selección de instructor");
            } else {
                radalert("Selecciona un curso.", 400, 150);
                return;
            }
        }

        function OpenSelectionParticipantesWindow(sender, args) {

            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=PARTICIPANTE", "winSeleccion", "Selección de participantes");
        }

        function OpenSelectionEvaluadorWindow(sender, args) {

            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx?CatalogoCl=EVALUADOR&CLFILTRO=NINGUNO&mulSel=0", "winSeleccion", "Selección de evaluador");
        }

        function useDataFromChild(pData) {
            if (pData != null) {

                var tipo = pData[0].clTipoCatalogo;
                var texto = "";

                switch (tipo) {

                    case "CURSO":
                        var listaCurso = $find("<%# rlbCurso.ClientID %>");
                            var txtHorasCurso = $find("<%# txtHorasCurso.ClientID%>");

                            txtHorasCurso.set_value(pData[0].noDuracion);
                            texto = pData[0].clCurso + " - " + pData[0].nbCurso;
                            SetListBoxItem(listaCurso, texto, pData[0].idCurso);
                            curso = pData[0].idCurso;
                            enviarDatosCurso(EncapsularSeleccion("CURSO", pData));
                            var vListBox = $find("<%=rlbInstructor.ClientID %>");
                            if (vListBox != undefined) {
                                vListBox.trackChanges();
                                var items = vListBox.get_items();
                                items.clear();

                                var item = new Telerik.Web.UI.RadListBoxItem();
                                item.set_value(0);
                                item.set_text("No Seleccionado");
                                vListBox.get_items().add(item)
                                item.select();

                                vListBox.commitChanges();
                            }

                            break;

                        case "INSTRUCTOR":
                            var listaInstructor = $find("<%# rlbInstructor.ClientID %>");
                        texto = pData[0].clInstructor + " - " + pData[0].nbInstructor;

                        SetListBoxItem(listaInstructor, texto, pData[0].idInstructor);
                        break;

                    case "EVALUADOR":
                        var listaEvaluador = $find("<%# rlbEvaluador.ClientID %>");

                        texto = pData[0].clEmpleado + " - " + pData[0].nbEmpleado;
                        SetListBoxItem(listaEvaluador, texto, pData[0].idEmpleado);
                        break;

                    case "PARTICIPANTE":
                        InsertParticipantes(EncapsularSeleccion("PARTICIPANTE", pData));
                        break;

                    case "PROGRAMA":
                        var listaPrograma = $find("<%# rlbPrograma.ClientID %>");

                            texto = pData[0].clPrograma + ' - ' + pData[0].nbPrograma;
                            SetListBoxItem(listaPrograma, texto, pData[0].idPrograma);
                            enviarDatosCurso(EncapsularSeleccion("PROGRAMA", pData));
                            programa = pData[0].idPrograma;
                            var vListBox = $find("<%=rlbCurso.ClientID %>");
                            if (vListBox != undefined) {
                                vListBox.trackChanges();
                                var items = vListBox.get_items();
                                items.clear();

                                var item = new Telerik.Web.UI.RadListBoxItem();
                                item.set_value(0);
                                item.set_text("No Seleccionado");
                                vListBox.get_items().add(item)
                                item.select();

                                vListBox.commitChanges();
                            }
                            var vListBox = $find("<%=rlbInstructor.ClientID %>");
                        if (vListBox != undefined) {
                            vListBox.trackChanges();
                            var items = vListBox.get_items();
                            items.clear();

                            var item = new Telerik.Web.UI.RadListBoxItem();
                            item.set_value(0);
                            item.set_text("No Seleccionado");
                            vListBox.get_items().add(item)
                            item.select();

                            vListBox.commitChanges();
                        }

                        break;
                }
            }
        }

        function EncapsularSeleccion(pClTipoSeleccion, pLstSeleccion) {
            return JSON.stringify({ cltipo: pClTipoSeleccion, oSeleccion: pLstSeleccion });
        }

        function enviarDatosCurso(pDato) {
            var ajaxManager = $find('<%= ramEventos.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function InsertParticipantes(pDato) {
            var ajaxManager = $find('<%= ramEventos.ClientID%>');
            ajaxManager.ajaxRequest(pDato);
        }

        function ProgramaVinculado(sender, args) {
            if (args.get_checked()) {
                var button = $find("<%= btnBuscarPrograma.ClientID %>");
                button.set_enabled(true);
            }
        }

        function ProgramaNoVinculado(sender, args) {
            if (args.get_checked()) {
                var button = $find("<%= btnBuscarPrograma.ClientID %>");
                button.set_enabled(false);
                var vListBox = $find("<%=rlbPrograma.ClientID %>");
                if (vListBox != undefined) {
                    vListBox.trackChanges();
                    var items = vListBox.get_items();
                    items.clear();

                    var item = new Telerik.Web.UI.RadListBoxItem();
                    item.set_value(0);
                    item.set_text("No Seleccionado");
                    vListBox.get_items().add(item)
                    item.select();

                    vListBox.commitChanges();
                }
            }
        }

        function SetListBoxItem(list, text, value) {
            if (list != undefined) {
                list.trackChanges();

                var items = list.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(text);
                item.set_value(value);
                item.set_selected(true);
                items.add(item);

                list.commitChanges();
            }
        }

        function closeWindow() {
            GetRadWindow().close();
        }

        function DeletePrograma() {
            var vListBox = $find("<%=rlbPrograma.ClientID %>");
            var vSelectedItems = vListBox.get_selectedItems();
            vListBox.trackChanges();
            if (vSelectedItems)
                vSelectedItems.forEach(function (item) {
                    vListBox.get_items().remove(item);
                });

            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text("No Seleccionado");
            item.set_value(0);
            vListBox.get_items().add(item)
            item.select();
            vListBox.commitChanges();
        }

        function ReturnDataToParentEdit() {
            var vAcciones = [];
            var vAccion = { clTipoCatalogo: "ACTUALIZAR" };
            vAcciones.push(vAccion);
            sendDataToParent(vAcciones);
        }

        function ReturnDataToParent() {
            var vAcciones = [];
            var vAccion = { clTipoCatalogo: "ACTUALIZARLISTA" };
            vAcciones.push(vAccion);
            sendDataToParent(vAcciones);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpEventos" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="ramEventos" runat="server" OnAjaxRequest="ramEventos_AjaxRequest" DefaultLoadingPanelID="ralpEventos">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramEventos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgParticipantes" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarFecha">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCalendario" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="dpFecha" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="tpHorafinal" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="tpHoraInicial" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="txtHoras" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbVinculado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnBuscarPrograma" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnBuscarCurso2" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnBuscarInstructor" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlbPrograma" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlbCurso" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlbInstructor" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                     <telerik:AjaxUpdatedControl ControlID="btnAgregarParticipantes" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rbNoVinculado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnBuscarPrograma" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnBuscarCurso2" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnBuscarInstructor" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlbPrograma" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlbCurso" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rlbInstructor" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="btnAgregarParticipantes" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnAgregarParticipantes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgParticipantes" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadTabStrip ID="rtsEventos" runat="server" MultiPageID="rmpEventos" SelectedIndex="0" Width="100%">
        <Tabs>
            <telerik:RadTab Text="Evento"></telerik:RadTab>
            <telerik:RadTab Text="Calendario"></telerik:RadTab>
            <telerik:RadTab Text="Participantes"></telerik:RadTab>
            <telerik:RadTab Text="Campos extra"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>

    <div style="height: calc(100% - 100px); overflow: auto;">

        <telerik:RadMultiPage ID="rmpEventos" runat="server" Width="100%" Height="100%" SelectedIndex="0">

            <telerik:RadPageView ID="rpvEvento" runat="server">

                <div style="clear: both; height: 10px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Clave:</label>
                    <telerik:RadTextBox runat="server" ID="txtClave" Width="100px" MaxLength="50"></telerik:RadTextBox>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Descripción:</label>
                    <telerik:RadTextBox runat="server" ID="txtNombre" Width="400px"></telerik:RadTextBox>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico" style="padding-left: 200px;">
                    <telerik:RadButton runat="server" RenderMode="Lightweight" OnClick="rbVinculado_Click" AutoPostBack="true" ID="rbVinculado" Text="Vinculado a un programa de capacitación" ToggleType="Radio" ButtonType="ToggleButton" GroupName="programa" OnClientCheckedChanged="ProgramaVinculado" Checked="true" ToolTip="En este apartado deberás especificar si deseas que este evento de capacitación esté vinculado con algún programa de capacitación que hayas creado, esta opción te permitirá que automáticamente se registre el estatus de los cursos en el programa. Una vez que hayas vinculado un evento a uno o más programas únicamente podrás seleccionar cursos y participantes que hayas aprovado en el programa(s) de capacitación que has seleccionado.">
                        <ToggleStates>
                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked" />
                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio" />
                        </ToggleStates>
                    </telerik:RadButton>

                    <telerik:RadButton runat="server" RenderMode="Lightweight" AutoPostBack="true" OnClick="rbNoVinculado_Click" ID="rbNoVinculado" Text="No vinculado a un programa de capacitación" ToggleType="Radio" ButtonType="ToggleButton" GroupName="programa" OnClientCheckedChanged="ProgramaNoVinculado">
                        <ToggleStates>
                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked" />
                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio" />
                        </ToggleStates>
                    </telerik:RadButton>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Programa de capacitación:</label>
                    <telerik:RadListBox ID="rlbPrograma" ReadOnly="true" runat="server" Width="230px" MaxLength="100">
                        <Items>
                            <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                        </Items>
                    </telerik:RadListBox>
                    <telerik:RadButton runat="server" ID="btnBuscarPrograma" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionProgramaWindow"></telerik:RadButton>
                    <telerik:RadButton runat="server" ID="btnEliminaPrograma" Text="X" AutoPostBack="false" OnClientClicked="DeletePrograma"></telerik:RadButton>

                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Curso:</label>
                    <telerik:RadListBox ID="rlbCurso" ReadOnly="true" runat="server" Width="400px" MaxLength="100">
                        <Items>
                            <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                        </Items>
                    </telerik:RadListBox>
                    <telerik:RadButton runat="server" ID="btnBuscarCurso2" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionCursoWindow"></telerik:RadButton>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Instructor:</label>
                    <telerik:RadListBox ID="rlbInstructor" ReadOnly="true" runat="server" Width="400px" MaxLength="100">
                        <Items>
                            <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                        </Items>
                    </telerik:RadListBox>
                    <telerik:RadButton runat="server" ID="btnBuscarInstructor" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionInstructorWindow"></telerik:RadButton>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Tipo:</label>
                    <telerik:RadComboBox runat="server" ID="cmbTipo">
                        <Items>
                            <telerik:RadComboBoxItem Text="Grupal" Value="GRUPAL" />
                            <telerik:RadComboBoxItem Text="Individual" Value="INDIVIDUAL" />
                        </Items>
                    </telerik:RadComboBox>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Estado:</label>
                    <telerik:RadComboBox runat="server" ID="cmbEstado">
                        <Items>
                  <%--          <telerik:RadComboBoxItem Text="Creado" Value="CREADO" />
                            <telerik:RadComboBoxItem Text="Programado" Value="PROGRAMADO" />
                            <telerik:RadComboBoxItem Text="En curso" Value="ENCURSO" />
                            <telerik:RadComboBoxItem Text="Concluido" Value="CONCLUIDO" />--%>
                            <telerik:RadComboBoxItem Text="Calendarizado" Value="CALENDARIZADO" />
                            <telerik:RadComboBoxItem Text="Reprogramar" Value="REPROGRAMAR" />
                            <telerik:RadComboBoxItem Text="Cancelado" Value="CANCELADO" />
                        </Items>
                    </telerik:RadComboBox>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Evaluador:</label>
                    <telerik:RadListBox ID="rlbEvaluador" ReadOnly="true" runat="server" Width="400px" MaxLength="100">
                        <Items>
                            <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                        </Items>
                    </telerik:RadListBox>
                    <telerik:RadButton runat="server" ID="btnBuscarEvaluador" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionEvaluadorWindow"></telerik:RadButton>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Fecha de evaluación:</label>
                    <telerik:RadDatePicker runat="server" ID="dtpEvaluacion"></telerik:RadDatePicker>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Lugar del evento:</label>
                    <telerik:RadTextBox runat="server" ID="txtLugarEvento" Width="300px"></telerik:RadTextBox>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Refrigerio:</label>
                    <telerik:RadTextBox runat="server" ID="txtRefrigerio" Width="400px" TextMode="MultiLine" Rows="4"></telerik:RadTextBox>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Costo directo:</label>
                    <telerik:RadNumericTextBox runat="server" ID="txtCostoDirecto"></telerik:RadNumericTextBox>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <label class="Etiqueta">Costo indirecto:</label>
                    <telerik:RadNumericTextBox runat="server" ID="txtCostoIndirecto"></telerik:RadNumericTextBox>
                </div>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico" style="padding-left: 200px;">
                    <telerik:RadCheckBox runat="server" ID="chkIncluirResultados" AutoPostBack="false" Text="Incluir los resultados de la evaluación en el reporte de plantillas de reemplazo"></telerik:RadCheckBox>
                </div>
            </telerik:RadPageView>

            <telerik:RadPageView runat="server" ID="rpvCalendario">
                <div style="clear: both; height: 10px;"></div>

                <div class="ctrlBasico">

                    <label>Fecha de inicio:</label>
                    <telerik:RadDatePicker runat="server" ID="dtpInicial"></telerik:RadDatePicker>
                </div>

                <div class="ctrlBasico">
                    <label>Fecha de término:</label>
                    <telerik:RadDatePicker runat="server" ID="dtpFinal"></telerik:RadDatePicker>
                </div>

                <div class="ctrlBasico">
                    <label>Horas totales del curso:</label>
                    <telerik:RadTextBox runat="server" ID="txtHorasCurso" Width="50px" Enabled="false">
                        <DisabledStyle HorizontalAlign="Right" />
                    </telerik:RadTextBox>
                </div>
                <div style="clear: both; height: 2px;"></div>

                <label class="labelTitulo"></label>

                <div style="clear: both; height: 2px;"></div>

                <div class="ctrlBasico">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Fecha:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadDatePicker runat="server" ID="dpFecha"></telerik:RadDatePicker>
                        </div>
                    </div>

                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Hora inicial:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTimePicker runat="server" ID="tpHoraInicial"></telerik:RadTimePicker>
                        </div>
                    </div>

                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Hora final:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTimePicker runat="server" ID="tpHorafinal"></telerik:RadTimePicker>
                        </div>
                    </div>

                    <div class="ctrlBasico">
                        <div class="divControlIzquierda">
                            <label>Horas efectivas:</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadNumericTextBox runat="server" ID="txtHoras" NumberFormat-DecimalDigits="0">
                                <IncrementSettings InterceptMouseWheel="false" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="ctrlBasico">
                        <telerik:RadButton runat="server" ID="btnAgregarFecha" Text="Agregar" OnClick="btnAgregarFecha_Click"></telerik:RadButton>
                    </div>
                </div>

                <div style="height: calc(100% - 150px); overflow: auto; width: 100%;">
                    <telerik:RadGrid runat="server" ID="rgCalendario" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" OnNeedDataSource="rgCalendario_NeedDataSource" Height="100%" OnItemCommand="rgCalendario_ItemCommand" ShowFooter="true">

                        <ClientSettings AllowKeyboardNavigation="true">
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <MasterTableView DataKeyNames="ID_EVENTO_CALENDARIO, ID_EVENTO">

                            <Columns>
                                <telerik:GridBoundColumn UniqueName="FE_INICIAL" DataField="FE_INICIAL" HeaderText="Inicio">
                                    <HeaderStyle Width="200" />

                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FE_FINAL" DataField="FE_FINAL" HeaderText="Término">
                                    <%--<ItemStyle Width="200px" />--%>
                                    <HeaderStyle Width="200" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NO_HORAS" DataField="NO_HORAS" HeaderText="Horas" Aggregate="Sum" DataType="System.Int32" FooterText="Total de horas:">
                                    <HeaderStyle Width="100" />
                                    <%--<ItemStyle Width="100px" />--%>
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn CommandName="Delete" ButtonType="ImageButton" Text="Eliminar" UniqueName="DeleteColumn" ConfirmTextFormatString="¿Desea eliminar el horario?" ConfirmDialogWidth="400" ConfirmDialogHeight="150" ConfirmDialogType="RadWindow">
                                    <HeaderStyle Width="30" />
                                </telerik:GridButtonColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

            </telerik:RadPageView>

            <telerik:RadPageView runat="server" ID="rpvParticipantes">
                <div style="clear: both; height: 10px;"></div>
                <div style="height: calc(100% - 80px); overflow: auto;">
                    <telerik:RadGrid runat="server" Height="100%" Width="100%" ID="rgParticipantes" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" OnNeedDataSource="rgParticipantes_NeedDataSource" OnItemCommand="rgParticipantes_ItemCommand">
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <MasterTableView DataKeyNames="ID_EMPLEADO,ID_EVENTO_PARTICIPANTE" ShowHeadersWhenNoRecords="true" AllowFilteringByColumn="true">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="CL_PARTICIPANTE" DataField="CL_PARTICIPANTE" HeaderText="No. de Empleado"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NB_PARTICIPANTE" DataField="NB_PARTICIPANTE" HeaderText="Nombre"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NB_DEPARTAMENTO" DataField="NB_DEPARTAMENTO" HeaderText="Área/Departamento"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto"></telerik:GridBoundColumn>
                                <telerik:GridButtonColumn CommandName="Delete" ButtonType="ImageButton" Text="Eliminar" UniqueName="DeleteColumn" ConfirmTextFields="NB_PARTICIPANTE" ConfirmTextFormatString="¿Desea eliminar a {0} de la lista de participantes?" ConfirmDialogWidth="400" ConfirmDialogHeight="150" ConfirmDialogType="RadWindow">
                                    <HeaderStyle Width="30" />
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

                <div style="clear: both; height: 20px;"></div>

                <telerik:RadButton runat="server" ID="btnAgregarParticipantes" Text="Agregar" Enabled="false" AutoPostBack="false" OnClientClicked="OpenSelectionParticipantesWindow"></telerik:RadButton>
            </telerik:RadPageView>

            <telerik:RadPageView ID="pvwCamposExtras" runat="server">
            </telerik:RadPageView>
        </telerik:RadMultiPage>

    </div>

    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnGuardarEvento" Text="Guardar" OnClick="btnGuardarEvento_Click" />
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnCancelarEvento" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>

    <telerik:RadWindowManager ID="rwmEvento" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
