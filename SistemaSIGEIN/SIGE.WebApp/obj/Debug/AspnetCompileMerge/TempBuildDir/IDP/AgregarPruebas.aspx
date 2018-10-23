<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="AgregarPruebas.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAgregarPruebas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="radCmbNiveles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radCmbNiveles" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdPruebas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="radCmbPuesto" />
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radCmbPuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPruebas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGenerar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenidoPruebas" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="radCmbNiveles" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="radCmbPuesto" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
<%--            <telerik:AjaxSetting AjaxControlID="PersonalidadLab1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="InteresesPersonales">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="EstiloPensamiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="AptitupMental">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PersonalidadLaboralII">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Tiva">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="OrtografiaI">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="OrtografiaII">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="OrtografiaIII">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Redaccion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="TecnicaPC">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="AptitupMentalII">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="AdaptacionMiedo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PruebaIngles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="radSliderNivel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPruebas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAddCandidato">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCandidatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCandidatos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdPruebas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="radCmbNiveles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPuesto"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBuscarPuesto">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="contenidoPruebas" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            function NivelOnchange() {
                var vCmbNivel = $find("<%= radCmbNiveles.ClientID %>");
                OcultarContenedores(vCmbNivel.get_value());
                if (vCmbNivel.get_value() == "COP") {
                    document.getElementById("divNivelConformePuesto").style.display = "block";
                    document.getElementById("divPuesto").style.display = "block";
                    document.getElementById("divPer").style.display = "none";
                    document.getElementById("divEjecutivo").style.display = "none";
                    document.getElementById("divOperativo").style.display = "none";
                }
                else if (vCmbNivel.get_value() == "PER") {
                    document.getElementById("divPersonalizado").style.display = "block";
                    document.getElementById("divPer").style.display = "block";
                    document.getElementById("divPuesto").style.display = "none";
                    document.getElementById("divEjecutivo").style.display = "none";
                    document.getElementById("divOperativo").style.display = "none";
                }
                else if (vCmbNivel.get_value() == "EJE") {
                    document.getElementById("divEjecutivo").style.display = "block";
                    document.getElementById("divPuesto").style.display = "none";
                    document.getElementById("divPer").style.display = "none";
                    document.getElementById("divOperativo").style.display = "none";
                }
                else if (vCmbNivel.get_value() == "OPE") {
                    document.getElementById("divOperativo").style.display = "block";
                    document.getElementById("divEjecutivo").style.display = "none";
                    document.getElementById("divPuesto").style.display = "none";
                    document.getElementById("divPer").style.display = "none";

                }
            }

            function EditPruebas() {
                document.getElementById("divPersonalizado").style.display = "block";
                document.getElementById("divPer").style.display = "block";
                document.getElementById("divPuesto").style.display = "none";
                document.getElementById("divEjecutivo").style.display = "none";
                document.getElementById("divOperativo").style.display = "none";
            }

            function OcultarContenedores(nivel) {
                document.getElementById("divNivelConformePuesto").style.display = "none";
                document.getElementById("divPersonalizado").style.display = "none";

            }

            function OpenCandidatoSelectionWindow() {
                openChildDialog("../Comunes/SeleccionCandidato.aspx?mulSel=1", "winSeleccionCandidato", "Selección de candidatos")
            }

            function useDataFromChild(pEmpleados) {
                if (pEmpleados != null) {
                    //  var list = $find("<grdCandidatos.ClientID %>");
                    //  list.trackChanges();
                    //    var items = list.get_items();
                    //   for (i = 0; i < pEmpleados.length; i++) {
                    //        var vEmpleadoSeleccionado = pEmpleados[i];
                    //      //items.clear();
                    //     var item = new Telerik.Web.UI.RadListBoxItem();
                    //    item.set_text(vEmpleadoSeleccionado.nbCandidato);
                    //   item.set_value(vEmpleadoSeleccionado.idCandidato);
                    //    items.add(item);
                    //    }

                    // list.commitChanges();

                    var vPuestoSeleccionado = pEmpleados[0];
                    var vClTipoCatalogo = vPuestoSeleccionado.clTipoCatalogo;
                    if (vClTipoCatalogo == "PUESTO") {
                        
                        var list = $find("<%=lstPuesto.ClientID %>");
                        list.trackChanges();

                        var items = list.get_items();
                        items.clear();

                        var item = new Telerik.Web.UI.RadListBoxItem();
                        item.set_text(vPuestoSeleccionado.nbPuesto);
                        item.set_value(vPuestoSeleccionado.idPuesto);
                        items.add(item);
                        item.set_selected(true);
                        list.commitChanges();

                        var ajaxManager = $find('<%= RadAjaxManager1.ClientID%>');
                        ajaxManager.ajaxRequest("Puesto");
                    }
                    else {
                        InsertarCandidato(EncapsularSeleccion(pEmpleados))
                    }
                }
            }

            function EncapsularSeleccion(pLstSeleccion) {
                return JSON.stringify({ oSeleccion: pLstSeleccion });
            }

            function InsertarCandidato(pDato) {
                var ajaxManager = $find('<%= RadAjaxManager1.ClientID%>');
                ajaxManager.ajaxRequest(pDato);
            }

            function deleteItem() {
                //var list = $find("<listCandidatos.ClientID %>");

                //  var selectedItem = list.get_selectedItem();
                //  if (!selectedItem) {
                //     radalert("Selecciona un candidato.", 400, 150);
                //    return false;
                // }
                // list.deleteItem(selectedItem);
                // return false;
            }

            function changeColor(divClick) {
                var vCmbNivel = $find("<%= radCmbNiveles.ClientID %>");
                if (divClick.className == "divPrueba divPruebaColorDesactivado" && vCmbNivel.get_value() == "PER") {
                    divClick.className = "divPrueba divPruebaColorActivado";
                } else if (divClick.className == "divPrueba divPruebaColorActivado" && vCmbNivel.get_value() == "PER") {
                    divClick.className = "divPrueba divPruebaColorDesactivado";
                }

            }

            function confirmarEliminar2(sender, args) {
                var MasterTable = $find("<%=grdCandidatos.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (row == null) {
                    radalert("Selecciona a un candidato", 400, 150, "Warning");
                    args.set_cancel(true);
                }
            }

            function closeWindow() {
                GetRadWindow().close();
            }

            function OpenPuestosSelectionWindow(sender, args) {
                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };
                openChildDialog("../Comunes/SeleccionPuesto.aspx", "winSeleccionCandidato", "Selección de puesto", windowProperties);
            }

            //-------------------------------------------------------------------------------------------------------------------

            function OpenEnviarCorreos() {
                var windowProperties = {
                    width: document.documentElement.clientWidth - 300,
                    height: document.documentElement.clientHeight - 20
                };
                openChildDialog("EnvioCorreosPruebas.aspx", "winSeleccionCandidato", "Envío correos", windowProperties);
            }

            //function OpenAplicarPruebasInterna() {
            //    var pFlBateria = '<= vFlBateria %>';
            //    var pClToken = '<= vClToken %>';
            //    var pIdCandidato = '<= vIdCandidatoBateria %>';

            //    var vBaterias = [];

            //    var vBateria = {
            //        flBateria: pFlBateria,
            //        clToken: pClToken,
            //        idCandidato: pIdCandidato,
            //        clTipoCatalogo: "BATERIAINTERNA"
            //     };
            //       vBaterias.push(vBateria);
                
            //       sendDataToParent(vBaterias);
            //    //var win = window.open("Pruebas/PruebaBienvenida.aspx?ID=" + pFlBateria + "&T=" + pClToken + "&idCandidato=" + pIdCandidato, '_self', true);
            //    //win.focus();
            //}

        </script>
    </telerik:RadCodeBlock>
        <div style="clear: both; height: 10px;"></div>
        <!-- Inicio Secciones de niveles -->
                <%-- Etiqueta personalizado --%>
                <div class="ctrlBasico" style="width: 100%; height:80px;">
                    <div style="width: 25%; float: left;">
                        <div class="ctrlBasico">
                            <label>Niveles:</label>
                        </div>
                  <%--      <div style="clear: both;"></div>--%>

                        <div class="ctrlBasico">
                            <telerik:RadComboBox runat="server"
                                ID="radCmbNiveles"
                                MarkFirstMatch="true"
                                EmptyMessage="Selecciona"
                                Width="180"
                                Height="120"
                                AutoPostBack="true"
                                OnClientSelectedIndexChanged="NivelOnchange"
                                OnSelectedIndexChanged="radCmbNiveles_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="Ejecutivo" Value="EJE" />
                                    <telerik:RadComboBoxItem runat="server" Text="Operativo" Value="OPE" />
                                    <telerik:RadComboBoxItem runat="server" Text="Conforme al puesto" Value="COP" />
                                    <telerik:RadComboBoxItem runat="server" Text="Personalizado" Value="PER" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div style="width: 75%; float: left;">
                        <div>
                            <div style="display: none" id="divEjecutivo">
                                <div class="ctrlBasico" style="text-align: justify; font-size: 14px;">
                                    <telerik:RadLabel runat="server" ID="lblEjecutivo" Text="Este nivel es conveniente usarlo con personas con un nivel escolar superior a bachillerato."></telerik:RadLabel>
                                </div>
                            </div>
                            <div style="display: none" id="divOperativo">
                                <div class="ctrlBasico" style="text-align: justify; font-size: 14px;">
                                    <telerik:RadLabel runat="server" ID="lblOperativo" Text="Este nivel considera la aplicación de las pruebas para personas orientadas a puestos operativos. En estos puestos los requerimientos de competencias genéricas son menores por lo que la escolaridad puede ser a nivel secundaria. Se requiere que la persona sepa leer y escribir con fluidez. Es muy importante considerar que si la aplicación va a realizarse directamente en la computadora la persona deberá tener las capacidades necesarias en el uso de la misma; de no ser así recomendamos realizar una aplicación manual y posteriormente capturar las pruebas en el sistema."></telerik:RadLabel>
                                </div>
                            </div>
                            <div style="display: none" id="divPuesto">
                                <div class="ctrlBasico" style="text-align: justify; font-size: 14px;">
                                    <telerik:RadLabel runat="server" ID="lblConformePuesto" Text="Este nivel adecúa la aplicación de pruebas psicométricas a los requerimientos del puesto contra el que se va a comparar, de tal forma que aplicará únicamente las pruebas involucradas en la medición de las competencias que el puesto requiere."></telerik:RadLabel>
                                </div>
                            </div>
                            <div style="display: none" id="divPer">
                                <div class="ctrlBasico" style="text-align: justify; font-size: 14px;">
                                    <telerik:RadLabel runat="server" ID="lblPersonalizado" Text="Este nivel permite al ejecutivo de selección aplicar las pruebas que él mismo seleccione. En este caso la traduccion de la psicometría a competencias puede verse afectada por lo que recomendamos hacer uso prudente de este nivel."></telerik:RadLabel>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="0" MultiPageID="rmpCapturaResultados">
                    <Tabs>
                        <telerik:RadTab SelectedIndex="0" Text="Candidatos"></telerik:RadTab>
                        <telerik:RadTab SelectedIndex="1" Text="Pruebas"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="clear: both; height: 10px;"></div>
             <div style="height: calc(100% - 200px);">
                <telerik:RadMultiPage ID="rmpCapturaResultados" runat="server" Height="100%" SelectedIndex="0">
                    <telerik:RadPageView ID="rpvCandidatos" runat="server" Height="100%">
                                    <telerik:RadGrid ID="grdCandidatos" ShowHeader="true" runat="server" AllowPaging="false"
                                        Width="100%" GridLines="None" Height="100%" HeaderStyle-Font-Bold="true"
                                AllowMultiRowSelection="true"
                                        AllowFilteringByColumn="false" OnItemCommand="grdCandidatos_ItemCommand"
                                        ClientSettings-EnablePostBackOnRowClick="false" OnNeedDataSource="grdCandidatos_NeedDataSource" >
                                        <ClientSettings AllowKeyboardNavigation="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <MasterTableView ClientDataKeyNames="ID_CANDIDATO, CL_SOLICITUD,ID_BATERIA" DataKeyNames="ID_CANDIDATO, CL_SOLICITUD,ID_BATERIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20"
                                            HorizontalAlign="NotSet" EditMode="EditForms">

                                            <Columns>

                                         
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Folio" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="150"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Candidato" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO" HeaderStyle-Width="350"></telerik:GridBoundColumn>
                                       <%--         <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Última batería" DataField="FL_BATERIA" UniqueName="FL_BATERIA" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn UniqueName="BTNELIMINAR" Text="Eliminar respuestas" CommandName="Delete" HeaderStyle-Width="150" ConfirmText="Este proceso borrará las respuestas de todas las pruebas de la batería ¿Desea continuar?">
                                                    <ItemStyle Width="10%" />
                                                </telerik:GridButtonColumn>--%>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
            <%--                    <div class="ctrlBasico">
                                    <div class="divControlDerecha">
                                        <telerik:RadButton runat="server" Text="Agregar" OnClientClicked="OpenCandidatoSelectionWindow" ID="btnAddCandidato" AutoPostBack="false" />
                                    </div>
                                </div>

                                <div class="ctrlBasico">
                                    <div class="divControlDerecha">
                                        <telerik:RadButton ID="btnDelCandidato" runat="server" Text="Eliminar" OnClientClicking="confirmarEliminar2" OnClick="btnDelCandidato_Click"></telerik:RadButton>
                                    </div>
                                </div>--%>
                               
                              <%--  <div style="clear: both"></div>--%>

                      <%--      <div style="clear: both"></div>--%>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpPruebas" runat="server" Height="100%">
                        <div style="height:calc(100% - 300px);">
                            <div style="border: 1px solid #ddd; border-radius: 5px; display: none; padding:15px;" id="divNivelConformePuesto">
                                    <div class="ctrlBasico">
                                        <div class="ctrlBasico">
                                            <label>Puesto: </label>
                                        </div>
                                      <%--  <div style="clear: both;"></div>--%>


                                            <%--<telerik:RadComboBox runat="server"
                                                ID="radCmbPuesto"
                                                MarkFirstMatch="true"
                                                EmptyMessage="Selecciona"
                                                Width="320"
                                                EnableLoadOnDemand="false"
                                                AutoPostBack="true"
                                                OnSelectedIndexChanged="radCmbPuesto_SelectedIndexChanged"
                                                Height="300">
                                            </telerik:RadComboBox>--%>

                                            <div class="ctrlBasico" runat="server" id="dvPuesto">
                                                <%--<label id="lblpuesto" name="lblpuesto">
                                                    <span style="border: 1px solid gray; background: #FF7400; border-radius: 5px;" title="Formación y desarrollo ">&nbsp;&nbsp;</span>
                                                    <span style="border: 1px solid gray; background: #A20804; border-radius: 5px;" title="Evaluación de desempeño">&nbsp;&nbsp;</span>&nbsp;Puesto del jefe inmediato:
                                                </label>
                                                <br />--%>
                                                <telerik:RadListBox ID="lstPuesto" runat="server" Width="260" OnSelectedIndexChanged="lstPuesto_SelectedIndexChanged" OnTextChanged="lstPuesto_TextChanged">
                                                    <Items>
                                                        <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                                                    </Items>
                                                </telerik:RadListBox>
                                                <telerik:RadButton runat="server" ID="btnBuscarPuesto" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="OpenPuestosSelectionWindow" />
                                            </div>

                                    </div>
                                    <div class="ctrlBasico" style="padding-left: 100px;">
                                        <div class="ctrlBasico">
                                            <label>Nivel mínimo de competencia: </label>
                                        </div>

                        <%--                <div style="clear: both;"></div>--%>
                                        <div class="ctrlBasico">

                                            <telerik:RadSlider ID="radSliderNivel" runat="server" ItemType="item"
                                                Width="300px" Height="50px" Visible="true" DragText="Arrastrar" DecreaseText="Disminuir" IncreaseText="Aumentar" AnimationDuration="400" CssClass="ItemsSlider" AutoPostBack="true" ThumbsInteractionMode="Free" OnValueChanged="radSliderNivel_ValueChanged">
                                                <Items>
                                                    <telerik:RadSliderItem Text="1" Value="1"></telerik:RadSliderItem>
                                                    <telerik:RadSliderItem Text="2" Value="2"></telerik:RadSliderItem>
                                                    <telerik:RadSliderItem Text="3" Value="3"></telerik:RadSliderItem>
                                                    <telerik:RadSliderItem Text="4" Value="4"></telerik:RadSliderItem>
                                                    <telerik:RadSliderItem Text="5" Value="5"></telerik:RadSliderItem>
                                                </Items>
                                            </telerik:RadSlider>
                                        </div>
                                    </div>
                                    <div style="clear: both"></div>
                                </div>

                                    <div class="ctrlBasico"  style="border: 1px solid #ddd; border-radius: 5px; display: none" id="divPersonalizado">
                                        <div class="divControlDerecha">
                                            <label>Se aplicarán las pruebas seleccionadas en color verde, seleccionar las pruebas para activar o desactivar utilizando la tecla control del teclado (Ctrl) y seleccionado las pruebas.</label>
                                        </div>
                                    </div>


                        <%--Contenedor de pruebas--%>

           <%--             <div style="border: 1px solid #ddd; border-radius: 5px; display: block" id="divContenedorPruebas">

                            <div style="clear: both; height: 10px"></div>
                            <div id="contenidoPruebas" runat="server">

                                <telerik:RadButton ID="PersonalidadLab1" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Personalidad laboral I"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Personalidad laboral I" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="InteresesPersonales" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Intereses personales"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Intereses personales" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="EstiloPensamiento" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Estilo de pensamiento"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Estilo de pensamiento" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="AptitupMental" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Aptitud mental I"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Aptitud mental I" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="AptitupMentalII" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Aptitud mental II"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Aptitud mental II" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="PersonalidadLaboralII" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Personalidad laboral II"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Personalidad laboral II" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="AdaptacionMiedo" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Adaptación al medio"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Adaptación al medio" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="Tiva" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="TIVA"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="TIVA" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="OrtografiaI" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Ortografía I"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Ortografía I" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="OrtografiaII" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Ortografía II"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Ortografía II" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="OrtografiaIII" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Ortografía III"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Ortografía III" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="TecnicaPC" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Técnica (PC)"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Técnica (PC)" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="Redaccion" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Redacción"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Redacción" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="PruebaIngles" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Prueba de inglés"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Prueba de inglés" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                                <telerik:RadButton ID="PruebaEntrevista" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle" CssClass="divPrueba" Height="65">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaActivada.png" IsBackgroundImage="true" Text="Entrevista"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState ImageUrl="../Assets/images/pruebaDesactivada.png" IsBackgroundImage="true" Text="Entrevista" Selected="true"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>

                            </div>
                            <div style="clear: both"></div>
                        </div>--%>
                        <div style="clear:both; height:10px;"></div>
              <telerik:RadGrid
                ID="grdPruebas"
                ShowHeader="true"
                runat="server"
                AllowPaging="false"
                AllowSorting="true"
                GridLines="None"
                Height="230"
                Width="100%"
                AllowMultiRowSelection="true"
                AutoGenerateColumns="false"
                HeaderStyle-Font-Bold="true"
                OnNeedDataSource="grdPruebas_NeedDataSource">
                <ClientSettings EnableRowHoverStyle="true" >
                    <Selecting AllowRowSelect="true" EnableDragToSelectRows="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                </ClientSettings>
                <PagerStyle AlwaysVisible="true" />
               <MasterTableView ClientDataKeyNames="ID_PRUEBA" DataKeyNames="ID_PRUEBA" EnableColumnsViewState="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="false">
                        <Columns>                        
							<telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35" HeaderText="Sel."></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Prueba" DataField="NB_PRUEBA" UniqueName="NB_PRUEBA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Height="300" FilterControlWidth="200" HeaderText="Factores que se evaluan" DataField="DS_PRUEBA_FACTOR" UniqueName="DS_PRUEBA_FACTOR"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
            </telerik:RadGrid>
                            </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
        </div>
<%--        <div class="ctrlBasico">
            <telerik:RadButton runat="server" Text="Aplicación interna" ID="btnAplicacionInterna" AutoPostBack="true" OnClick="btnAplicacionInterna_Click" />
        </div>
            <div class="ctrlBasico">
            <telerik:RadButton runat="server" Text="Aplicación externa" ID="btnAplicacionExterna" AutoPostBack="true" OnClick="btnAplicacionExterna_Click" />
        </div>
            <div class="ctrlBasico">
            <telerik:RadButton runat="server" Text="Aplicación masiva" ID="btnAplicacionMasiva" AutoPostBack="false" />
        </div>--%>
    <div style="height:10px; clear:both;"></div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" Text="Aceptar" ID="btnGenerar" OnClick="btnGenerar_Click" AutoPostBack="true" />
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_Click" AutoPostBack="false" OnClientClicked="closeWindow" />
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow ID="winSeleccionCandidato" runat="server" Title="Seleccionar empleado" Height="600px" Width="1100px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
