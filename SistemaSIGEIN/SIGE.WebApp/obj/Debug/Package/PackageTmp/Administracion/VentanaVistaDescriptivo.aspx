<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaVistaDescriptivo.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaVistaDescriptivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .lblNivel span {
            border: 1px solid gray;
            padding: 2px 7px !important;
            background-color: white;
            border-radius: 14px;
        }

        .rslItemsWrapper {
            z-index: 10 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadCodeBlock runat="server" ID="radCodeblock">
        <script type="text/javascript">
            function closeWindow() {
                GetRadWindow().close();
            }

            //EVENTO CANCELAR EN LA LISTA DE EXPERIENCIAS  EN EL DESCRIPTIVO DE PUESTO
            function CancelarAccionExperiencia(sender, args) {
                CerrarFormularioExperiencia();
            }

            //ABRIR VENTANA MODAL
            function OpenPuestosSelectionWindow(sender, args) {
                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };
                openChildDialog("../Comunes/SeleccionPuesto.aspx", "winSeleccionPuestos", "Selección de Jefe inmediato", windowProperties);
            }

            var ChangeDisplayStyle = function (element, display) {
                $get(element).style.display = display ? "block" : "none";
            }

            var HideForms = function () {
                GetAllForms().forEach(HideForm);
            }

            var HideForm = function (form) {
                ChangeDisplayStyle(form.idControl, false);
            }

            var ShowForm = function (form) {
                ChangeDisplayStyle(form.idControl, true);
            }

            var filterByTriggerId = function (e) {
                return e.idTrigger == this;
            }

            function ShowFormFromButton(sender, args) {
                HideForms();
                GetAllForms().filter(filterByTriggerId, sender.get_id()).forEach(ShowForm);
            }


            function ConfirmFuncionGenericaDelete(sender, args) {
                var masterTable = $find("<%= grdFuncionesGenericas.ClientID %>").get_masterTableView();
                confirmarEliminar(sender, args, masterTable, false);
            }

            function confirmarEliminar(sender, args, masterTable, isMultiSelection) {
                var selectedItems = masterTable.get_selectedItems();
                if (selectedItems != undefined) {
                    var length = 1;

                    if (isMultiSelection)
                        selectedItems.length;

                    var confirmMessage = "¿Deseas eliminar este elemento?";

                    for (var i = 0; i < length; i++) {
                        var vNombre = masterTable.getCellByColumnUniqueName(selectedItems[i], "NB_FUNCION_GENERICA").innerHTML;
                        confirmMessage = String.format("¿Deseas eliminar la función genérica {0}?", vNombre);
                    }

                    confirmar(sender, args, confirmMessage);
                }
                else {
                    radalert("Selecciona una función genérica.", 400, 150);
                    args.set_cancel(true);
                }
            }

            function confirmar(sender, args, text, winProperties) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });

                var wnd = radconfirm(text, callBackFunction, 400, 200, null, "Confirmar");
                //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                args.set_cancel(true);
            }

           
        </script>
    </telerik:RadCodeBlock>

    <style>
        .rbToggleButton {
            line-height: 15px;
        }

        .RadSlider_Default .rslHorizontal .rslSelectedregion {
            background: transparent;
            border-color: transparent;
        }

        .RadSlider_Default .rslHorizontal .rslTrack {
            background: transparent;
            border-color: transparent;
        }

        .RadSlider_Default .rslHorizontal .rslItem {
            background: transparent;
        }

        .RadSlider_Default .rslHorizontal a.rslHandle {
            background-image: none;
        }
    </style>

    <div style="clear: both;"></div>

    <div>
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Clave:</label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtNombreCorto" runat="server" style="width: 300px;"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Nombre: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtDescripcionPuesto" runat="server" style="width: 300px;"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>No. de plazas: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtNoPlazas" runat="server" style="width: 300px;"></span>
                </td>
            </tr>
        </table>
    </div>

    <div style="clear: both;"></div>

    <%-- Perfil del puesto --%>

    <div>
        <div class="ctrlBasico">
            <div>
                <table class="ctrlTableForm">
                    <tr>
                        <td class="ctrlTableDataContext">

                            <label id="lblRangoedad" name="lblRangoedad">
                                <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                                <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;
                                Rango de edad:
                            </label>
                        </td>
                        <td class="ctrlTableDataBorderContext">
                            <span id="txtRangoEdadMin" runat="server"></span>
                        </td>
                        <td class="ctrlTableDataContext">
                            <label id="lblRangoI" name="lblRangoI">a</label>
                        </td>
                        <td class="ctrlTableDataBorderContext">
                            <span id="txtRangoEdadMax" runat="server"></span>
                        </td>
                        <td class="ctrlTableDataContext">
                            <label id="lblRangoF" name="lblRangoF">años de edad</label>
                        </td>
                        <td class="ctrlTableDataContext">
                            <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                            <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;
                                        <label id="lblGenero" name="lblGenero">Género: </label>
                        </td>
                        <td class="ctrlTableDataBorderContext">
                            <span id="cmbGenero" runat="server"></span>
                        </td>
                        <td class="ctrlTableDataContext">
                            <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                            <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;
                                        <label id="lblEstadoCivil">Estado civil: </label>
                        </td>
                        <td class="ctrlTableDataBorderContext">
                            <span id="cmbEdoCivil" runat="server"></span>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>

    <div>
        <label class="labelTitulo" id="lblescolaridad" name="lblescolaridad">
            <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
            <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;Escolaridad</label>
    </div>
    <div style="clear: both"></div>
    <div class="ctrlBasico">
        <div class="BorderRadioComponenteHTML" style="width: 500px; float: left;">
            <div class="divBarraTitulo">
                <label style="float: left">Postgrado</label>
            </div>
            <div style="padding: 5px">
                <div class="ctrlBasico" style="text-align: right; width: 100%;">
                    <telerik:RadListBox runat="server" ID="lstPostgrados" Width="100%" AllowDelete="false" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                </div>
            </div>
        </div>
    </div>

    <div class="ctrlBasico">
        <div class="BorderRadioComponenteHTML" style="width: 500px; float: left;">
            <div class="divBarraTitulo">
                <label style="float: left">Carrera profesional</label>
            </div>
            <div style="padding: 5px">
                <div class="ctrlBasico" style="text-align: right; width: 100%;">
                    <telerik:RadListBox runat="server" ID="lstCarreraprof" Width="100%" AllowDelete="false" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                </div>
            </div>
        </div>
    </div>

    <div class="ctrlBasico">
        <div class="BorderRadioComponenteHTML" style="width: 500px; float: left;">
            <div class="divBarraTitulo">
                <label style="float: left">Carrera técnica</label>
            </div>
            <div style="padding: 5px">
                <div class="ctrlBasico" style="text-align: right; width: 100%;">
                    <telerik:RadListBox runat="server" ID="lstCarreraTec" Width="100%" AllowDelete="false" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                </div>
            </div>
        </div>
    </div>

    <div class="ctrlBasico">
        <div class="divBarraTitulo">
            <telerik:RadTextBox ID="txtOtroNivelEst" runat="server" Width="500" Label="Otro" LabelWidth="100" Enabled="false"></telerik:RadTextBox>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div>
        <label class="labelTitulo" id="lblcompetencias" name="lblcompetencias">
            <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
            <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
            <span style="border: 1px solid gray; background: #0087CF; border-radius: 5px;" title="Metodología para la compensación ">&nbsp;&nbsp;</span>&nbsp;Competencias</label>
    </div>

    <div class="ctrlBasico">
        <div class="BorderRadioComponenteHTML" style="width: 520px; float: left;">
            <div class="divBarraTitulo">
                <label style="float: left">Competencias específicas necesarias</label>
            </div>
            <div style="padding: 5px">
                <div class="ctrlBasico" style="text-align: right; width: 100%;">
                    <telerik:RadListBox runat="server" ID="lstCompetenciasEspecificas" Width="100%" AllowDelete="false" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                </div>
            </div>
        </div>
    </div>

<%--    <div class="ctrlBasico">
        <div class="BorderRadioComponenteHTML" style="width: 520px; float: left;">
            <div class="divBarraTitulo">
                <label style="float: left">Competencias requeridas</label>
            </div>
            <div style="padding: 5px">
                <div class="ctrlBasico" style="text-align: right; width: 100%;">
                    <span id="txtCompetenciasRequeridas" runat="server"></span>
                </div>
            </div>
        </div>
    </div>--%>
    <div style="clear: both;"></div>

    <div>
        <label class="labelTitulo" id="lblExperiencia" name="lblExperiencia">
            <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
            <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;Experiencia</label>
    </div>

    <div class="ctrlBasico">
        <div class="BorderRadioComponenteHTML" style="width: 700px; float: left;">
            <div class="divBarraTitulo">
                <label style="float: left">Experiencia necesaria</label>
            </div>
            <div style="padding: 5px">
                <div class="ctrlBasico" style="text-align: right; width: 100%;">
                    <telerik:RadListBox runat="server" ID="lstExperiencia" Width="100%" AllowDelete="false" ButtonSettings-AreaWidth="35px">
                        <HeaderTemplate>
                            <table style="width: 100%">
                                <colgroup>
                                    <col style="width: 50%">
                                    <col style="width: 20%">
                                    <col style="width: 30%">
                                </colgroup>
                                <tr style="text-align: center">
                                    <td><b>Experiencia</b></td>
                                    <td><b>Tiempo/años</b></td>
                                    <td><b>Tipo</b></td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table class="ctrlTableForm" style='width: 100%; table-layout: fixed;'>
                                <colgroup>
                                    <col style='width: 50%'>
                                    <col style='width: 20%'>
                                    <col style='width: 30%'>
                                </colgroup>
                                <tr style='text-align: center'>
                                    <td><%# DataBinder.Eval(Container, "Text") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "NO_TIEMPO") %></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "CL_TIPO_EXPERIENCIA") %></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:RadListBox>
                </div>
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>

    <div>
        <table class="ctrlTableForm" style="width: 100%;">
            <tr>
                <td class="ctrlTableDataContext">
                    <label class="labelTitulo" id="lblRequerimientos" name="lblRequerimientos">
                        <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                        <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;Requerimientos / aportaciones adicionales del puesto (equipo, materiales, etc.)</label>
                </td>
            </tr>
            <tr>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtRequerimientos" runat="server"></div>
                </td>
            </tr>
            <tr>
                <td class="ctrlTableDataContext">
                    <label class="labelTitulo" id="lblobservaciones" name="lblobservaciones"><span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>&nbsp;Observaciones</label>
                </td>
            </tr>
            <tr>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtObservaciones" runat="server"></span>
                </td>
            </tr>
        </table>
    </div>

    <div style="clear: both; height: 25px"></div>

    <%-- Organigrama --%>
    <label class="labelTitulo" id="lblOrganigrama" name="lblOrganigrama">Organigrama</label>
    <div>
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label id="lbltipoPuesto" name="lbltipoPuesto">
                        <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
                        <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                        <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>
                        <span style="border: 1px solid gray; background: #0087CF; border-radius: 5px;" title="Metodología para la compensación ">&nbsp;&nbsp;</span>&nbsp;Tipo de puesto:</label>
                </td>

                <td class="ctrlTableDataBorderContext">
                    <span id="txtTipoPuesto" runat="server"></span>
                </td>

               <%-- <td class="ctrlTableDataContext">
                    <label id="lblarea" name="lblarea">
                        <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                        <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>
                        <span style="border: 1px solid gray; background: #0087CF; border-radius: 5px;" title="Metodología para la compensación ">&nbsp;&nbsp;</span>&nbsp;Área:
                    </label>
                </td>--%>
              <%--  <td class="ctrlTableDataBorderContext">
                    <span id="txtArea" runat="server"></span>
                </td>--%>
               <%-- <td class="ctrlTableDataContext">
                    <label id="lblcentroadministrativo" name="lblcentroadministrativo">Centro administrativo: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCentroAdmin" runat="server"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label id="lblcentrooperativo" name="lblcentrooperativo">Centro operativo: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCentroOptvo" runat="server"></span>
                </td>--%>
              <%--  <td class="ctrlTableDataContext">
                    <label id="lblpuestoJefeInmediato" name="lblpuestoJefeInmediato">
                        <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                        <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>&nbsp;Puesto del jefe inmediato:
                    </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtPuestoJefe" runat="server"></span>
                </td>--%>
            </tr>
        </table>
    </div>
    <div style="clear: both;"></div>

        <div class="ctrlBasico">
        <div class="BorderRadioComponenteHTML" style="width: 550px; float: left;">
            <div class="divBarraTitulo">
                <label style="float: left">Jefe(s) inmediato(s)</label>
            </div>
            <div style="padding: 5px">
                <div class="ctrlBasico" style="text-align: right; width: 100%;">
                    <telerik:RadListBox runat="server" ID="lstJefesInmediatos" Width="100%" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                </div>
            </div>
        </div>
    </div>


     <div style="clear: both;"></div>
    <div>
        <label class="labelTitulo" id="lblTituloPuestos" name="lblTituloPuestos">
            <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
            <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>&nbsp;Puestos</label>
    </div>

    <div class="ctrlBasico">
        <div class="BorderRadioComponenteHTML" style="width: 550px; float: left;">
            <div class="divBarraTitulo">
                <label style="float: left">Puestos que supervisa en forma inmediata</label>
            </div>
            <div style="padding: 5px">
                <div class="ctrlBasico" style="text-align: right; width: 100%;">
                    <telerik:RadListBox runat="server" ID="lstPuestosSubordinado" Width="100%" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                </div>
            </div>
        </div>
    </div>

    <div class="ctrlBasico">
        <div class="BorderRadioComponenteHTML" style="width: 450px; float: left;">
            <div class="divBarraTitulo">
                <label style="float: left">Puestos interrelacionados</label>
            </div>
            <div style="padding: 5px">
                <div class="ctrlBasico" style="text-align: right; width: 100%;">
                    <telerik:RadListBox runat="server" ID="lstPuestosInterrelacionados" Width="100%" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                </div>
            </div>
        </div>
    </div>

    <div style="clear: both;"></div>
    <label class="labelTitulo" id="lblRutas" name="lblRutas"><span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;Rutas</label>

    <div>
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label id="lblPosicionOrganigrama" name="lblPosicionOrganigrama">Posición en el organigrama:</label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtPosicionOrganigrama" runat="server"></span>
                </td>
            </tr>
        </table>
    </div>

    <div style="clear: both"></div>

    <div class="ctrlBasico">
        <div class="BorderRadioComponenteHTML" style="width: 500px; float: left;">
            <div class="divBarraTitulo">
                <label style="float: left">Ruta de crecimiento alternativa</label>
            </div>
            <div style="padding: 5px">
                <div class="ctrlBasico" style="text-align: right; width: 100%;">
                    <telerik:RadListBox runat="server" ID="lstAlternativa" Width="100%" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                </div>
            </div>
        </div>
    </div>

    <div class="ctrlBasico">
        <div class="BorderRadioComponenteHTML" style="width: 500px; float: left;">
            <div class="divBarraTitulo">
                <label style="float: left">Ruta de crecimiento lateral</label>
            </div>
            <div style="padding: 5px">
                <div class="ctrlBasico" style="text-align: right; width: 100%;">
                    <telerik:RadListBox runat="server" ID="lstLateral" Width="100%" ButtonSettings-AreaWidth="35px"></telerik:RadListBox>
                </div>
            </div>
        </div>
    </div>

    <!-- Inicio de responsabilidades y funciones genericas -->
    <div style="clear: both"></div>
    <div>
        <table class="ctrlTableForm" style="width: 100%;">
            <tr>
                <td class="ctrlTableDataContext">
                    <label class="labelTitulo" id="lblResponsable" name="lblResponsable">
                        <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                        <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>&nbsp;Es responsable de:</label>
                </td>
            </tr>
            <tr>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtResponsable" runat="server"></span>
                </td>
            </tr>
            <tr>
                <td class="ctrlTableDataContext">
                    <label class="labelTitulo" id="lblAutoridad" name="lblAutoridad">
                        <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>&nbsp;Autoridad
                    </label>
                </td>
            </tr>
            <tr>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtAutoridad" runat="server"></span>
                </td>
            </tr>
        </table>

    </div>

    <div style="clear: both; height:5px;"></div>
    <label class="labelTitulo" id="lblPoliticaIntegral" name="lblAutoridad" runat="server">
        &nbsp;Autoridad / Política Integral
    </label>
    <div id="MnsAutoridad" runat="server" class="ctrlBasico" style="padding:10px; border:1px solid gray; border-radius:5px;">
        <telerik:RadLabel ID="MnsAutoridadPoliticaIntegral" runat="server"></telerik:RadLabel>
    </div>
    <div style="clear: both; height:5px;"></div>

    <!-- Funciones genericas -->
    <label id="Label1" class="labelTitulo" name="lblFuncionesGenericas">
        <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
        <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>
        <span style="border: 1px solid gray; background: #0087CF; border-radius: 5px;" title="Metodología para la compensación ">&nbsp;&nbsp;</span>&nbsp; 
        Funciones genericas</label>
    <div style="width: 100%">

        <telerik:RadGrid ID="grdFuncionesGenericas" runat="server" AutoGenerateColumns="false"
            OnNeedDataSource="grdFuncionesGenericas_NeedDataSource"
            OnItemDataBound="grdFuncionesGenericas_ItemDataBound" HeaderStyle-Font-Bold="true">
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="false" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <MasterTableView DataKeyNames="ID_ITEM" ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Función Genérica" DataField="NB_FUNCION_GENERICA" HeaderStyle-Width="300" UniqueName="NB_FUNCION_GENERICA"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="Detalle" UniqueName="Funciones">
                        <ItemTemplate>
                            <div><%# Eval("DS_DETALLE") %></div>
                            <telerik:RadGrid ID="grdCompetencias" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Competencias específicas" DataField="NB_COMPETENCIA" HeaderStyle-Width="300" UniqueName="NB_COMPETENCIA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Nivel" DataField="NB_NIVEL" UniqueName="NB_NIVEL"></telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Indicadores desempeño - Evidencias" DataField="DS_INDICADORES" UniqueName="DS_INDICADORES">
                                            <ItemTemplate>
                                                <%# Eval("DS_INDICADORES") %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>

    <div style="clear: both; height: 10px;"></div>

    <!-- competencias genericas -->

    <label id="Label2" class="labelTitulo" name="lblFuncionesGenericas">
        <span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span>
        <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
        <span style="border: 1px solid gray; background: #0087CF; border-radius: 5px;" title="Metodología para la compensación ">&nbsp;&nbsp;</span>&nbsp;
        Competencias genéricas</label>

    <div style="width: 100%;">
        <telerik:RadGrid ID="dgvCompetencias" runat="server" ShowStatusBar="True" AutoGenerateColumns="False" AllowPaging="false" AllowSorting="false" Width="100%"
            OnNeedDataSource="dgvCompetencias_NeedDataSource" OnDataBound="dgvCompetencias_DataBound" HeaderStyle-Font-Bold="true">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="false" />
            </ClientSettings>
            <MasterTableView AllowMultiColumnSorting="true" ShowHeadersWhenNoRecords="true" Name="Competencias Genéricas"
                Width="100%" DataKeyNames="ID_COMPETENCIA, ID_NIVEL0, ID_NIVEL1, ID_NIVEL2, ID_NIVEL3, ID_NIVEL4, ID_NIVEL5">
                <Columns>
                    <telerik:GridTemplateColumn DataField="NB_CLASIFICACION" HeaderText="Clasificacion" UniqueName="NB_CLASIFICACION" ReadOnly="true">
                        <ItemTemplate>
                            <div style="border: 2px solid <%# Eval("CL_CLASIFICACION_COLOR") %>; height: 100%; width: calc(100% + 15px); margin: -8px; padding: 8px;" title="<%# Eval("DS_CLASIFICACION") %>"><%# Eval("NB_CLASIFICACION") %></div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="NB_COMPETENCIA" HeaderText="Competencia" UniqueName="NB_COMPETENCIA" ReadOnly="true">
                        <ItemTemplate>
                            <div style="border: 2px solid <%# Eval("CL_CLASIFICACION_COLOR") %>; height: 100%; width: calc(100% + 15px); margin: -8px; padding: 8px;" title="<%# Eval("DS_COMPETENCIA") %>"><%# Eval("NB_COMPETENCIA") %></div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="ID_NIVEL0" HeaderText="ID_NIVEL0" UniqueName="ID_NIVEL0" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ID_NIVEL1" HeaderText="ID_NIVEL1" UniqueName="ID_NIVEL1" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ID_NIVEL2" HeaderText="ID_NIVEL2" UniqueName="ID_NIVEL2" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ID_NIVEL3" HeaderText="ID_NIVEL3" UniqueName="ID_NIVEL3" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ID_NIVEL4" HeaderText="ID_NIVEL4" UniqueName="ID_NIVEL4" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ID_NIVEL5" HeaderText="ID_NIVEL5" UniqueName="ID_NIVEL5" ReadOnly="true" Visible="false"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn DataField="NO_VALOR_NIVEL" HeaderText="Nivel" UniqueName="NO_NIVEL_DESEADO">
                        <HeaderTemplate>
                            <div style="padding-left: 40px;">
                                <telerik:RadSlider ID="rsEncabezado" AutoPostBack="false" runat="server" AnimationDuration="400" Skin="Default" ShowDecreaseHandle="false"
                                    ShowIncreaseHandle="false" Value="5" ShowDragHandle="false" Height="60px" ItemType="item" ThumbsInteractionMode="Free" Width="720">
                                    <Items>
                                        <telerik:RadSliderItem Text="Nivel 0<br />0%<br />No lo necesita" Value="0" />
                                        <telerik:RadSliderItem Text="Nivel 1<br />20%<br />Lo necesita poco" Value="1" />
                                        <telerik:RadSliderItem Text="Nivel 2<br />40%<br />Lo necesita medio bajo" Value="2" />
                                        <telerik:RadSliderItem Text="Nivel 3<br />60%<br />Lo necesita medio alto" Value="3" />
                                        <telerik:RadSliderItem Text="Nivel 4<br />80%<br />Lo necesita de manera importante" Value="4" />
                                        <telerik:RadSliderItem Text="Nivel 5<br />100%<br />Lo necesita de manera imprescindible" Value="5" />
                                    </Items>
                                </telerik:RadSlider>
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div style="border: 2px solid <%# Eval("CL_CLASIFICACION_COLOR") %>; height: 100%; width: calc(100% + 15px); margin: -8px; padding: 8px;">
                                <telerik:RadSlider ID="rsNivel1" AutoPostBack="false" runat="server" AnimationDuration="400" TrackMouseWheel="false"
                                    Value='<%# decimal.Parse(Eval("NO_VALOR_NIVEL").ToString()) %>' CssClass="ItemsSlider"  Enabled="false" 
                                    Height="22" ItemType="item" ThumbsInteractionMode="Free" Width="800px">
                                    <Items>
                                        <telerik:RadSliderItem Text="0" Value="0" Height="22" CssClass="lblNivel" />
                                        <telerik:RadSliderItem Text="1" Value="1" CssClass="lblNivel" />
                                        <telerik:RadSliderItem Text="2" Value="2" CssClass="lblNivel" />
                                        <telerik:RadSliderItem Text="3" Value="3" CssClass="lblNivel" />
                                        <telerik:RadSliderItem Text="4" Value="4" CssClass="lblNivel" />
                                        <telerik:RadSliderItem Text="5" Value="5" CssClass="lblNivel" />
                                    </Items>
                                </telerik:RadSlider>
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>

    <!-- Campos Extras -->
    <div style="clear: both; height: 10px;"></div>

    <div>
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label id="lblTipoPrestacion" name="lblTipoPrestacion"><span style="border: 1px solid gray; background: #C6DB95; border-radius: 5px;" title="Intregación de personal">&nbsp;&nbsp;</span> Tipo de prestación: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtTipoPrestaciones" runat="server"></span>
                </td>
            </tr>
            <tr>
                <td class="ctrlTableDataContext">
                    <label id="lblPrerstaciones" name="lblPrerstaciones">Prestaciones:</label>
                </td>
            </tr>
            <tr>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtPrestaciones" runat="server"></span>
                </td>
            </tr>
        </table>
    </div>
    <div style="clear: both; height: 10px;"></div>

    <!-- ocupaciones -->

    <label id="lblOcupacionPuesto" class="labelTitulo" name="lblOcupacionPuesto">Ocupación</label>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierdaAT">
            <label id="lblCveOcupacion" name="lblCveOcupacion" runat="server">Clave de la ocupación:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadLabel runat="server" ID="lblClOcupación"></telerik:RadLabel>
        </div>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierdaAT">
            <label id="lblOcupacionS" name="lblOcupacionS" runat="server">Descripción:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadLabel runat="server" ID="lblOcupacionSeleccionada"></telerik:RadLabel>
        </div>
    </div>
    <div style="clear: both; height: 10px;"></div>

    <!-- Control de documentos -->
    <label id="Label3" class="labelTitulo" name="lblOcupacionPuesto">Control de documentos</label>
    <div style="clear: both; height: 10px;"></div>

    <div>
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label id="Label4" name="lblTipoPrestacion">Clave del documento: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCDClaveDocumento" runat="server"></span>
                </td>

                <td class="ctrlTableDataContext">
                    <label id="Label6" name="lblTipoPrestacion">Versión: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCDVersion" runat="server"></span>
                </td>

                <td class="ctrlTableDataContext">
                    <label id="Label7" name="lblTipoPrestacion">Fecha Elaboración: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCDFechaElaboracion" runat="server"></span>
                </td>
            </tr>

            <tr>
                <td class="ctrlTableDataContext">
                    <label id="Label5" name="lblTipoPrestacion">Elaboró: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCDElaboro" runat="server"></span>
                </td>

                <td class="ctrlTableDataContext">
                    <label id="Label8" name="lblTipoPrestacion">Fecha de revisón: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCDFechaRevision" runat="server"></span>
                </td>

                <td class="ctrlTableDataContext">
                    <label id="Label9" name="lblTipoPrestacion">Revisó: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCDRevision" runat="server"></span>
                </td>
            </tr>

            <tr>
                <td class="ctrlTableDataContext">
                    <label id="Label10" name="lblTipoPrestacion">Fecha de autorización: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCDFechaAutorizacion" runat="server"></span>
                </td>

                <td class="ctrlTableDataContext">
                    <label id="Label11" name="lblTipoPrestacion">Autorizó: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCDAutorizo" runat="server"></span>
                </td>

                <td class="ctrlTableDataContext">
                    <label id="Label12" name="lblTipoPrestacion">Control de cambios: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCDControlCambios" runat="server"></span>
                </td>
            </tr>

        </table>
    </div>

    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
