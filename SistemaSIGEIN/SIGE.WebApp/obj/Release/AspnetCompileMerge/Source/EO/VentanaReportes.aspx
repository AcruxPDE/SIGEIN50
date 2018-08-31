<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaReportes.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaReportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OpenSelectionGeneroIndiceWindow() {
            openChildDialog("../Comunes/SeleccionCatalogos.aspx?ID_CATALOGO=2" + "&CatalogoCl=INDICE_GENERO", "WinCuestionario", "Selección de género");
        }

        function OpenSelectionDepartamentoIndiceWindow() {
            openChildDialog("../Comunes/SeleccionArea.aspx?CatalogoCl=INDICE_DEPARTAMENTO", "WinCuestionario", "Selección de área");
        }

        function OpenSelectionGeneroDistribucionWindow() {
            openChildDialog("../Comunes/SeleccionCatalogos.aspx?ID_CATALOGO=2" + "&CatalogoCl=DISTRIBUCION_GENERO", "WinCuestionario", "Selección de género");
        }

        function OpenSelectionDepartamentoDistribucionWindow() {
            openChildDialog("../Comunes/SeleccionArea.aspx?CatalogoCl=DISTRIBUCION_DEPARTAMENTO", "WinCuestionario", "Selección de área");
        }

        function OpenSelectionAdicionales() {
            openChildDialog("../Comunes/SeleccionAdscripciones.aspx?MultiSeleccion=1" + "&ClLista=IS", "WinConsultaPersonal", "Selección de campos adicionales");
        }

        function OpenSelectionAdicionalesResultados() {
            openChildDialog("../Comunes/SeleccionAdscripciones.aspx?MultiSeleccion=1" + "&ClLista=RD", "WinConsultaPersonal", "Selección de campos adicionales");
        }

        function OpenSelectionDepartamentoPreguntasWindow() {
            openChildDialog("../Comunes/SeleccionArea.aspx?CatalogoCl=PREGUNTAS_DEPARTAMENTO", "WinCuestionario", "Selección de área");
        }

        function OpenSelectionGeneroPreguntasWindow() {
            openChildDialog("../Comunes/SeleccionCatalogos.aspx?ID_CATALOGO=2" + "&CatalogoCl=PREGUNTAS_GENERO", "WinCuestionario", "Selección de género");
        }

        function OpenSelectionPreAdicionales() {
            openChildDialog("../Comunes/SeleccionAdscripciones.aspx?MultiSeleccion=1" + "&ClLista=PREGUNTAS", "WinConsultaPersonal", "Selección de campos adicionales");
        }

        
        function useDataFromChild(pDato) {
            if (pDato != null) {
                var arr = [];
                var vDatosSeleccionados = pDato[0];
                switch (pDato[0].clTipoCatalogo) {
                    case "INDICE_GENERO":
                        list = $find("<%=lstGeneroIndice.ClientID %>");
                        InsertGenero(list, vDatosSeleccionados);
                        break;
                    case "DISTRIBUCION_GENERO":
                        list = $find("<%=rlbGeneroDistribucion.ClientID %>");
                         InsertGenero(list, vDatosSeleccionados);
                         break;

                     case "INDICE_DEPARTAMENTO":
                         var vListBox = $find("<%=lstDepartamentosIndice.ClientID %>");
                         InsertDepartamentos(pDato, vListBox);
                         break;
                     case "DISTRIBUCION_DEPARTAMENTO":
                         var vListBox = $find("<%=rlbDepartamentoDistribucion.ClientID %>");
                         InsertDepartamentos(pDato, vListBox);
                         break;
                     case "ADSCRIPCION":
                         var vListBox;
                         if (pDato[0].clLista == "IS") {
                             vListBox = $find("<%= rlbAdicionales.ClientID%>");
                             InsertAdicionales(pDato, vListBox);
                         } else if (pDato[0].clLista == "PREGUNTAS")
                         {
                             vListBox = $find("<%= rlbPreAdicionales.ClientID%>");
                             InsertAdicionales(pDato, vListBox);
                         }
                         else {
                             vListBox = $find("<%= rlbAdicionalesDist.ClientID%>");
                             InsertAdicionales(pDato, vListBox);
                         }
                     break;
                 case "PREGUNTAS_DEPARTAMENTO":
                     var vListBox = $find("<%=rlbDepPreguntas.ClientID %>");
                     InsertDepartamentos(pDato, vListBox);
                     break;
                 case "PREGUNTAS_GENERO":
                     list = $find("<%=rlbPreGenero.ClientID %>");
                        InsertGenero(list, vDatosSeleccionados);
                        break;
                }
            }
        }

        function InsertGenero(list, vSelectedData) {
            if (list != undefined) {
                list.trackChanges();
                var items = list.get_items();
                items.clear();
                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(vSelectedData.nbCatalogoValor);
                item.set_value(vSelectedData.clCatalogoValor);
                item.set_selected(true);
                items.add(item);
                list.commitChanges();
            }
        }

        function InsertDepartamentos(pDato, pListBox) {
            var arrSeleccion = [];
            for (var i = 0; i < pDato.length; i++)
                arrSeleccion.push({
                    idItem: pDato[i].idArea,
                    nbItem: pDato[i].nbArea
                });

            OrdenarSeleccion(arrSeleccion, pListBox);
        }

        function InsertAdicionales(pDato, pListBox) {
            var arrSeleccion = [];
            for (var i = 0; i < pDato.length; i++)
                arrSeleccion.push({
                    idItem: pDato[i].idCampo,
                    nbItem: pDato[i].nbValor
                });
            OrdenarSeleccion(arrSeleccion, pListBox);
        }

        function OrdenarSeleccion(pSeleccion, vListBox) {
            vListBox.trackChanges();

            var items = vListBox.get_items();

            for (var i = 0; i < items.get_count() ; i++) {
                var item = items.getItem(i);
                var itemValue = item.get_value();
                var itemText = item.get_text();
                if (itemValue != "0") {
                    var vFgItemEncontrado = false;
                    for (var j = 0; j < pSeleccion.length; j++)
                        vFgItemEncontrado = vFgItemEncontrado || (pSeleccion[j].idItem == itemValue);
                    if (!vFgItemEncontrado)
                        pSeleccion.push({
                            idItem: itemValue,
                            nbItem: itemText
                        });
                }
            }

            var arrOriginal = [];
            for (var i = 0; i < pSeleccion.length; i++)
                arrOriginal.push(pSeleccion[i].nbItem);

            var arrOrdenados = arrOriginal.slice();

            arrOrdenados.sort();

            var arrItemsOrdenados = [];

            for (var i = 0; i < arrOrdenados.length; i++)
                arrItemsOrdenados.push(pSeleccion[arrOriginal.indexOf(arrOrdenados[i])]);

            items.clear();

            for (var i = 0, len = arrItemsOrdenados.length; i < len; i++)
                ChangeListItem(arrItemsOrdenados[i].idItem, arrItemsOrdenados[i].nbItem, vListBox);

            vListBox.commitChanges();
        }

        function ChangeListItem(pIdItem, pNbItem, pListBox) {
            var items = pListBox.get_items();
            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text(pNbItem);
            item.set_value(pIdItem);
            items.add(item);
            item.set_selected(true);
        }

        function DeleteAdicionales() {
            var vListAdscripcion = $find("<%= rlbAdicionales.ClientID %>");
            Delete(vListAdscripcion);
        }

        function DeleteAdicionalesResultados() {
            var vListAdscripcion = $find("<%= rlbAdicionalesDist.ClientID %>");
             Delete(vListAdscripcion);
         }

         function DeletePreAdicionales() {
             var vListAdscripcion = $find("<%= rlbPreAdicionales.ClientID %>");
            Delete(vListAdscripcion);
        }


        function DeleteDepartamentoIndice() {
            var vListBox = $find("<%=lstDepartamentosIndice.ClientID %>");
            Delete(vListBox);
        }

        function DeleteDepartamentoDistribucion() {
            var vListBox = $find("<%=rlbDepartamentoDistribucion.ClientID %>");
            Delete(vListBox);
        }

        function DeleteDepartamentoPreguntas() {
            var vListBox = $find("<%=rlbDepPreguntas.ClientID %>");
            Delete(vListBox);
        }

        function DeleteGeneroIndice() {
            var vListBox = $find("<%=lstGeneroIndice.ClientID %>");
            Delete(vListBox);
        }

        function DeleteGeneroPreguntas() {
            var vListBox = $find("<%=rlbPreGenero.ClientID %>");
            Delete(vListBox);
        }

        function DeleteGeneroDistribucion() {
            var vListBox = $find("<%=rlbGeneroDistribucion.ClientID %>");
            Delete(vListBox);
        }

        function Delete(vListBox) {
            var vSelectedItems = vListBox.get_selectedItems();
            vListBox.trackChanges();
            if (vSelectedItems)
                vSelectedItems.forEach(function (item) {
                    vListBox.get_items().remove(item);
                });
            vListBox.commitChanges();
        }

        function TabSelected() {
            var vtabStrip = $find("<%= tbReportes.ClientID%>").get_selectedIndex();
            console.info(vtabStrip);

            var divContexto;
            var divIndice;
            var divDistribucion;
            var divPreguntas;
            var divGlobal;

            divContexto = document.getElementById('<%= divContexto.ClientID%>');
            divIndice = document.getElementById('<%= divIndice.ClientID%>');
            divDistribucion = document.getElementById('<%= divDistribucion.ClientID%>');
            divPreguntas = document.getElementById('<%= divPreguntas.ClientID%>');
            divGlobal = document.getElementById('<%= divGlobal.ClientID%>');

            switch (vtabStrip) {

                case 0:
                    divContexto.style.display = 'block';
                    divIndice.style.display = 'none';
                    divDistribucion.style.display = 'none';
                    divPreguntas.style.display = 'none';
                    divGlobal.style.display = 'none';
                    break;
                case 1:
                    divContexto.style.display = 'none';
                    divIndice.style.display = 'block';
                    divDistribucion.style.display = 'none';
                    divPreguntas.style.display = 'none';
                    divGlobal.style.display = 'none';
                    break;
                case 2:
                    divContexto.style.display = 'none';
                    divIndice.style.display = 'none';
                    divDistribucion.style.display = 'block';
                    divPreguntas.style.display = 'none';
                    divGlobal.style.display = 'none';
                    break;
                case 3:
                    divContexto.style.display = 'none';
                    divIndice.style.display = 'none';
                    divDistribucion.style.display = 'none';
                    divPreguntas.style.display = 'block';
                    divGlobal.style.display = 'none';
                    break;
                case 4:
                    divContexto.style.display = 'none';
                    divIndice.style.display = 'none';
                    divDistribucion.style.display = 'none';
                    divPreguntas.style.display = 'none';
                    divGlobal.style.display = 'block';
                    break;
            }
        }

        function OnClientClickedIndice(sender, args) {
            var radtabstrip = $find('<%=rtIndice.ClientID %>');
            var count = radtabstrip.get_tabs().get_count();
            var currentindex = radtabstrip.get_selectedIndex();
            var nextindex = currentindex + 1;
            if (nextindex < count) {
                radtabstrip.set_selectedIndex(nextindex);
            }
        }

        function OnClientClickedDistribucion(sender, args) {
            var radtabstrip = $find('<%=rtDistribucion.ClientID %>');
            var count = radtabstrip.get_tabs().get_count();
            var currentindex = radtabstrip.get_selectedIndex();
            var nextindex = currentindex + 1;
            if (nextindex < count) {
                radtabstrip.set_selectedIndex(nextindex);
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramReportes" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramReportes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdEmpleados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgEmpleadosDistribucion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbIndiceSatisfaccion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rhtColumnChart" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgdGraficasIndice" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbMostradoPor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTemaGraficar" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTemaGraficar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rhcGraficaDistribucion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgGraficaDistribucion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: 10px;"></div>
    <div style="clear: both;"></div>
    <div style="height: calc(100% - 70px);">
        <telerik:RadSplitter ID="rsReportes" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpReportes" runat="server" Width="20px" Height="100%">
                <telerik:RadSlidingZone ID="rszReportes" runat="server" SlideDirection="Right" Height="100%" ExpandedPaneId="rsReportes" Width="20px" DockedPaneId="rsbReportes">
                    <telerik:RadSlidingPane ID="rsbReportes" runat="server" CollapseMode="Forward" EnableResize="false" Width="250px" Title="Reporte" Height="100%">
                        <telerik:RadTabStrip ID="tbReportes" runat="server" SelectedIndex="0" OnClientTabSelected="TabSelected" MultiPageID="mpgReportes" Orientation="VerticalLeft" Width="250" Font-Size="Small" Align="Left">
                            <Tabs>
                                <telerik:RadTab Text="Contexto" runat="server" SelectedIndex="0"></telerik:RadTab>
                                <telerik:RadTab Text="Índice de satisfacción" runat="server" SelectedIndex="1"></telerik:RadTab>
                                <telerik:RadTab Text="Distribución de los resultados" runat="server" SelectedIndex="2"></telerik:RadTab>
                                <telerik:RadTab Text="Preguntas abiertas" runat="server" SelectedIndex="3"></telerik:RadTab>
                                <telerik:RadTab Text="Resultado global" runat="server" SelectedIndex="4"></telerik:RadTab>

                            </Tabs>
                        </telerik:RadTabStrip>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
            <telerik:RadPane ID="radGraficasReportes" runat="server" Height="100%">
                <telerik:RadMultiPage ID="mpgReportes" runat="server" SelectedIndex="0" Height="100%">
                    <telerik:RadPageView ID="rpvContexto" runat="server" Height="100%">
                   <%--   <div class="ctrlBasico">
                                <table class="ctrlTableForm">
                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <telerik:RadButton ID="btnPredefinido" runat="server" ToggleType="CheckBox" AutoPostBack="false" ReadOnly="true" Width="260" Text="Cuestionario predefinido de SIGEIN">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </td>
                              
                             
                                        <td class="ctrlTableDataContext">
                                            <telerik:RadButton ID="btnCopia" runat="server" ToggleType="CheckBox" AutoPostBack="false" Width="260" ReadOnly="true" Text="Cuestionario copia de otro periodo">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </td>
                            
                         
                                        <td class="ctrlTableDataContext">
                                            <telerik:RadButton ID="btnVacios" runat="server" ToggleType="CheckBox" AutoPostBack="false" Width="260" ReadOnly="true" Text="Cuestionario creado desde cero">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </td>
                                        </tr>
                                </table>
                            </div>--%>
                        <div style="clear: both; height: 10px;"></div>
                                <div class="ctrlBasico">
                                    <table class="ctrlTableForm" text-align: left; >
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbPeriodo" name="lbTabulador" runat="server">Período:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtClPeriodo" runat="server"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbNbPeriodo" name="lbTabulador" runat="server">Descripción:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtDsPeriodo" runat="server"></div>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label6" name="lbNotas" runat="server">Notas:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <label id="txtNotas" runat="server"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="LbEstado" name="lbTabulador" runat="server">Estado:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtEstatus" runat="server"></div>
                                            </td>
                                        </tr>
                                                           <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label7" name="lbNotas" runat="server">Tipo de cuestionario:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtTipo" runat="server"></div>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="LbTipoCuestionario" name="lbNotas" runat="server">Origen de cuestionario:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="lbCuestionario" runat="server"></div>
                                            </td>
                                        </tr>
                                       <%-- <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbedad" name="LbFiltros" runat="server" visible="false">Edad:</label>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <div id="txtEdad" runat="server" visible="false"></div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbGenero" name="LbFiltros" runat="server" visible="false">Género:</label>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <div id="txtGenero" runat="server" visible="false"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbAntiguedad" name="LbFiltros" runat="server" visible="false">Antigüedad:</label>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <div id="txtAntiguedad" runat="server" visible="false"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbDepartamento" name="LbFiltros" runat="server" visible="false">Área:</label>
                                            </td>
                                            <td colspan="2" class="ctrlTableDataContext">
                                                <telerik:RadTextBox ID="rlDepartamento" Visible="false" ReadOnly="true" runat="server" Width="200" Height="100" TextMode="MultiLine"></telerik:RadTextBox>
                                            </td>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbAdscripciones" name="LbFiltros" runat="server" visible="false">Campos adicionales:</label>
                                            </td>
                                            <td rowspan="3" class="ctrlTableDataContext" visible="false">
                                                <telerik:RadTextBox ID="rlAdicionales" Visible="false" ReadOnly="true" runat="server" Width="200" Height="100" TextMode="MultiLine"></telerik:RadTextBox>
                                            </td>

                                        </tr>--%>
                                    </table>
                                </div>
                    </telerik:RadPageView>
                    <%-- Inicio de indice de Satisfacción --%>
                    <telerik:RadPageView ID="rpIndiceSatisfaccion" runat="server" Height="100%">
                        <telerik:RadTabStrip ID="rtIndice" runat="server" SelectedIndex="0" OnClientTabSelected="TabSelected" MultiPageID="mpgIndice">
                            <Tabs>
                                <telerik:RadTab Text="Parámetros de Análisis" runat="server" SelectedIndex="1"></telerik:RadTab>
                                <telerik:RadTab Text="Grafica de indice de satisfacción" runat="server" SelectedIndex="2"></telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <div style="height: calc(100% - 50px);">
                            <telerik:RadMultiPage ID="mpgIndice" runat="server" SelectedIndex="0" Height="100%">

                                <telerik:RadPageView ID="rpvParametrosIndice" runat="server" Height="100%">
                                    <div style="clear: both; height: 10px"></div>

                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="lblDepartamento" name="lblDepartamento" runat="server">Área:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadListBox ID="lstDepartamentosIndice" Width="200px" Height="100px" runat="server" ValidationGroup="vgDepartamento"></telerik:RadListBox>
                                            <telerik:RadButton ID="btnSeleccionarDepartamentoInd" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="OpenSelectionDepartamentoIndiceWindow"></telerik:RadButton>
                                            <telerik:RadButton ID="btnEliminarDepartamentoInd" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="DeleteDepartamentoIndice"></telerik:RadButton>
                                        </div>
                                    </div>

                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="lblGenero" name="lblGenero" runat="server">Género:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadListBox ID="lstGeneroIndice" Width="100" Height="35px" runat="server" ValidationGroup="vgGenero"></telerik:RadListBox>
                                            <telerik:RadButton ID="btnSeleccionarGeneroInd" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionGeneroIndiceWindow" ValidationGroup="vgGenero"></telerik:RadButton>
                                            <telerik:RadButton ID="btnEliminarGeneroInd" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgGenero" OnClientClicked="DeleteGeneroIndice"></telerik:RadButton>
                                        </div>
                                    </div>

                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="lbAdicionales" name="lbAdscripciones" runat="server">Campos adicionales:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadListBox ID="rlbAdicionales" Width="200" Height="100px" runat="server" ValidationGroup="vgAdicionales"></telerik:RadListBox>
                                            <telerik:RadButton ID="btnAdicionales" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgAdicionales" OnClientClicked="OpenSelectionAdicionales"></telerik:RadButton>
                                            <telerik:RadButton ID="btnXAdicionales" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgAdicionales" OnClientClicked="DeleteAdicionales"></telerik:RadButton>
                                        </div>
                                    </div>

                                    <div style="clear: both; height: 20px"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <telerik:RadButton RenderMode="Lightweight" ID="rbEdadIndice" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                AutoPostBack="false">
                                            </telerik:RadButton>
                                            <label id="lblEdad" name="lblEdad" runat="server">Edad:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadNumericTextBox runat="server" ID="rnEdadInicial" NumberFormat-DecimalDigits="0" Value="18" Name="rnEdadInicial" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            a
                                        <telerik:RadNumericTextBox runat="server" ID="rnEdadFinal" NumberFormat-DecimalDigits="0" Value="65" Name="rnEdadFinal" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            años.
                                        </div>
                                    </div>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda" style="margin-left: 95px">
                                            <telerik:RadButton RenderMode="Lightweight" ID="rbAntiguedadIndice" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                AutoPostBack="false">
                                            </telerik:RadButton>
                                            <label id="lblAntiguedad" name="lblEdad" runat="server">Antigüedad:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadNumericTextBox runat="server" ID="rnAntiguedadInicial" NumberFormat-DecimalDigits="0" Value="0" Name="rnAntiguedadInicial" Width="60px" MinValue="0" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            a
                                        <telerik:RadNumericTextBox runat="server" ID="rnAtiguedadFinal" NumberFormat-DecimalDigits="0" Value="30" Name="rnAtiguedadFinal" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            meses.
                                        </div>
                                    </div>
                                    <div style="clear: both; height: 20px"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnGraficaIndice" runat="server" name="btnGraficaIndice" AutoPostBack="true" Text="Reporte" Width="100" OnClick="btnGraficaIndice_Click"></telerik:RadButton>
                                    </div>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="rpvGraficaIndice" runat="server" Height="100%">
                                    <div style="height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <label id="lblMostrado" name="lblMostrado" runat="server">Mostrado por:</label>
                                    </div>
                                    <div class="ctrlBasico">
                                        <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbIndiceSatisfaccion" Width="190" MarkFirstMatch="true" AutoPostBack="true" DropDownWidth="190" OnSelectedIndexChanged="cmbIndiceSatisfaccion_SelectedIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="Dimensión" Value="1" />
                                                <telerik:RadComboBoxItem runat="server" Text="Tema" Value="2" />
                                                <telerik:RadComboBoxItem runat="server" Text="Pregunta" Value="3" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="ctrlBasico">
                                        <div id="dvColorProm" runat="server" class="ctrlBasico" style="height: 30px; width: 60px; float: right; border-radius: 5px;"></div>
                                    </div>
                                    <div class="ctrlBasico" style="padding-top: 3px;">
                                        <label id="lbTotal" runat="server" style="font-family: Arial; font-size: medium; font-weight: bold"></label>
                                    </div>

                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico" style="width: 60%; height: 80px">
                                        <telerik:RadHtmlChart runat="server" ID="rhtColumnChart" Transitions="true">
                                            <PlotArea>
                                                <Series>
                                                    <telerik:ColumnSeries>
                                                        <TooltipsAppearance DataFormatString="{0}"></TooltipsAppearance>
                                                        <LabelsAppearance>
                                                            <TextStyle Color="White" FontFamily="Arial" FontSize="24" Padding="40" />
                                                        </LabelsAppearance>
                                                    </telerik:ColumnSeries>
                                                </Series>
                                                <YAxis Color="White" Visible="false">
                                                    <LabelsAppearance DataFormatString="{0}"></LabelsAppearance>
                                                </YAxis>
                                                <Appearance>
                                                    <FillStyle BackgroundColor="Transparent"></FillStyle>
                                                </Appearance>
                                            </PlotArea>
                                            <Appearance>
                                                <FillStyle BackgroundColor="Transparent"></FillStyle>
                                            </Appearance>
                                            <ChartTitle Text="Índice de satisfacción">
                                                <Appearance Align="Center" BackgroundColor="Transparent" Position="Top">
                                                </Appearance>
                                            </ChartTitle>
                                            <Legend>
                                                <Appearance BackgroundColor="Transparent" Position="Bottom">
                                                </Appearance>
                                            </Legend>
                                        </telerik:RadHtmlChart>
                                    </div>
                                    <div class="ctrlBasico" style="width: 40%;">
                                        <telerik:RadGrid ID="rgdGraficasIndice"
                                            runat="server"
                                            AllowSorting="false"
                                            AutoGenerateColumns="false">
                                            <ClientSettings>
                                                <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                                                <Selecting AllowRowSelect="false" />
                                            </ClientSettings>
                                            <MasterTableView AllowFilteringByColumn="false" AllowPaging="false" ShowHeadersWhenNoRecords="true">
                                                <Columns>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="30" HeaderText="#" DataField="NO_NOMBRE" UniqueName="NO_NOMBRE" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Dimensión" DataField="NOMBRE" UniqueName="NOMBRE" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Indice de satisfacción" AllowFiltering="true" FilterControlWidth="50px">
                                                        <ItemStyle Width="40px" Height="30px" HorizontalAlign="Left" />
                                                        <HeaderStyle Width="40px" Height="30px" />
                                                        <ItemTemplate>
                                                            <div class="ctrlBasico" style="height: 30px; width: 70px; float: right; background: <%#Eval("COLOR_PORCENTAJE") %>; border-radius: 5px;"></div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>

                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </div>

                    </telerik:RadPageView>
                    <%-- fin de indice de Satisfacción --%>


                    <%-- Inicio de distribución de resultados --%>
                    <telerik:RadPageView ID="rpDistribucionResultados" runat="server" Height="100%">
                        <telerik:RadTabStrip ID="rtDistribucion" runat="server" SelectedIndex="0" MultiPageID="mpgDistribucion">
                            <Tabs>
                                <telerik:RadTab Text="Parámetros de Análisis"></telerik:RadTab>
                                <telerik:RadTab Text="Grafica distribución de resultados"></telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>

                        <div style="height: calc(100% - 50px); overflow: auto;">
                            <telerik:RadMultiPage ID="mpgDistribucion" runat="server" SelectedIndex="0" Height="100%">

                                <telerik:RadPageView ID="rpvParametrosDistribucion" runat="server" Height="100%">
                                    <div style="clear: both; height: 10px"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="lblDepartamentoDistribucion" name="lblDepartamento" runat="server">Área:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadListBox ID="rlbDepartamentoDistribucion" Width="200px" Height="100px" runat="server" ValidationGroup="vgDepartamentoDis"></telerik:RadListBox>
                                            <telerik:RadButton ID="btnDistribucionSeleccionarDep" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgDepartamentoDis" OnClientClicked="OpenSelectionDepartamentoDistribucionWindow"></telerik:RadButton>
                                            <telerik:RadButton ID="btnDistribucionEliminaDep" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgDepartamentoDis" OnClientClicked="DeleteDepartamentoDistribucion "></telerik:RadButton>
                                        </div>
                                    </div>

                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="Label3" name="lblGenero" runat="server">Género:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadListBox ID="rlbGeneroDistribucion" Width="100" Height="35px" runat="server" ValidationGroup="vgGeneroDis"></telerik:RadListBox>
                                            <telerik:RadButton ID="btnDistribucionSeleccionarGen" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionGeneroDistribucionWindow" ValidationGroup="vgGeneroDis"></telerik:RadButton>
                                            <telerik:RadButton ID="btnDistribucionEliminaGen" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgGeneroDis" OnClientClicked="DeleteGeneroDistribucion"></telerik:RadButton>
                                        </div>
                                    </div>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="Label2" name="lbAdscripciones" runat="server">Campos adicionales:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadListBox ID="rlbAdicionalesDist" Width="200" Height="100px" runat="server" ValidationGroup="vgAdicionales"></telerik:RadListBox>
                                            <telerik:RadButton ID="btnBDist" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgAdicionales" OnClientClicked="OpenSelectionAdicionalesResultados"></telerik:RadButton>
                                            <telerik:RadButton ID="btnXDist" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgAdicionales" OnClientClicked="DeleteAdicionalesResultados"></telerik:RadButton>
                                        </div>
                                    </div>
                                    <div style="clear: both; height: 20px"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <telerik:RadButton RenderMode="Lightweight" ID="rbEdadDistribucion" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                AutoPostBack="false">
                                            </telerik:RadButton>
                                            <label id="Label4" name="lblEdad" runat="server">Edad:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadNumericTextBox runat="server" ID="rntEdadInicialDis" NumberFormat-DecimalDigits="0" Value="18" Name="rnEdadInicial" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            a
                                        <telerik:RadNumericTextBox runat="server" ID="rntEdadFinalDis" NumberFormat-DecimalDigits="0" Value="65" Name="rnEdadFinal" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            años.
                                        </div>
                                    </div>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda" style="margin-left: 95px">
                                            <telerik:RadButton RenderMode="Lightweight" ID="rbAntiguedadDistribucion" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                AutoPostBack="false">
                                            </telerik:RadButton>
                                            <label id="Label5" name="lblEdad" runat="server">Antigüedad:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadNumericTextBox runat="server" ID="rntAntiguedadInicialDis" NumberFormat-DecimalDigits="0" Value="0" Name="rnAntiguedadInicial" Width="60px" MinValue="0" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            a
                                        <telerik:RadNumericTextBox runat="server" ID="rntAntiguedadFinalDis" NumberFormat-DecimalDigits="0" Value="30" Name="rnAtiguedadFinal" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            meses.
                                        </div>
                                    </div>
                                    <div style="clear: both; height: 20px"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnGraficaDistribucion" runat="server" name="btnGraficaDistribucion" AutoPostBack="true" Text="Reporte" Width="100" OnClick="btnGraficaDistribucion_Click"></telerik:RadButton>
                                    </div>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="rpvGraficaDistribucion" runat="server" Height="100%">
                                    <div style="clear: both; height: 10px"></div>
                                    <div class="ctrlBasico">
                                        <label id="Label1" name="lblMostrado" runat="server">Mostrado por:</label>
                                    </div>
                                    <div class="ctrlBasico">
                                        <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbMostradoPor" Width="190" MarkFirstMatch="true" AutoPostBack="true" DropDownWidth="190" OnSelectedIndexChanged="cmbMostradoPor_SelectedIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="Dimensión" Value="1" />
                                                <telerik:RadComboBoxItem runat="server" Text="Tema" Value="2" />
                                                <telerik:RadComboBoxItem runat="server" Text="Pregunta" Value="3" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="ctrlBasico">
                                        <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbTemaGraficar" Width="550" MarkFirstMatch="true" AutoPostBack="true" DropDownWidth="650" DropDownHeight="200" OnSelectedIndexChanged="cmbTemaGraficar_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div style="clear: both; height: 10px"></div>

                                    <div class="ctrlBasico" style="width: 60%; height: 80%">
                                        <telerik:RadHtmlChart runat="server" ID="rhcGraficaDistribucion" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                            <ChartTitle Text="Gráfica de distribución">
                                                <Appearance Align="Center" Position="Top">
                                                </Appearance>
                                            </ChartTitle>
                                            <Legend>
                                                <Appearance Position="Right" Visible="true">
                                                </Appearance>
                                            </Legend>
                                            <PlotArea>
                                                <Series>
                                                    <telerik:PieSeries StartAngle="90">
                                                        <LabelsAppearance Position="OutsideEnd" DataFormatString="{0} %">
                                                        </LabelsAppearance>
                                                        <TooltipsAppearance Color="White" DataFormatString="{0} %"></TooltipsAppearance>
                                                    </telerik:PieSeries>
                                                </Series>
                                            </PlotArea>
                                        </telerik:RadHtmlChart>
                                    </div>
                                    <div class="ctrlBasico" style="width: 40%;">
                                        <telerik:RadGrid ID="rgGraficaDistribucion"
                                            runat="server"
                                            AllowSorting="false"
                                            AutoGenerateColumns="false">
                                            <ClientSettings>
                                                <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                                                <Selecting AllowRowSelect="false" />
                                            </ClientSettings>
                                            <MasterTableView AllowFilteringByColumn="false" AllowPaging="false" ShowHeadersWhenNoRecords="true">
                                                <Columns>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="120" HeaderText="Categoría" DataField="NOMBRE" UniqueName="NOMBRE" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="30" HeaderText="Cantidad" DataField="NO_CANTIDAD" UniqueName="NO_CANTIDAD" ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="50" HeaderText="Porcentaje" DataField="PORCENTAJE" UniqueName="PORCENTAJE" DataFormatString="{0:N2}%" ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </div>
                    </telerik:RadPageView>
                    <%-- fin de distribución de resultados --%>
                    <telerik:RadPageView ID="rpvPreguntasAbiertas" runat="server" Height="100%">
                        <telerik:RadTabStrip ID="rtPreguntasAbiertas" runat="server" SelectedIndex="0" MultiPageID="rmpPreguntasabiertas">
                            <Tabs>
                                <telerik:RadTab Text="Parámetros de Análisis"></telerik:RadTab>
                                <telerik:RadTab Text="Reporte preguntas abiertas"></telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <div style="height: calc(100% - 50px);">
                            <telerik:RadMultiPage ID="rmpPreguntasabiertas" runat="server" SelectedIndex="0" Height="100%">
                                <telerik:RadPageView ID="rpvParametros" runat="server" Height="100%">
                                    <div style="clear: both; height: 10px"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="lbDepPreguntas" name="lbDepPreguntas" runat="server">Área:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadListBox ID="rlbDepPreguntas" Width="200px" Height="100px" runat="server" ValidationGroup="vgDepartamento"></telerik:RadListBox>
                                            <telerik:RadButton ID="btnBPreDep" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="OpenSelectionDepartamentoPreguntasWindow"></telerik:RadButton>
                                            <telerik:RadButton ID="btnXPreDep" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="DeleteDepartamentoPreguntas"></telerik:RadButton>
                                        </div>
                                    </div>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="lbPreGenero" name="lbPreGenero" runat="server">Género:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadListBox ID="rlbPreGenero" Width="100" Height="35px" runat="server" ValidationGroup="vgGenero"></telerik:RadListBox>
                                            <telerik:RadButton ID="btnBPreGenero" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionGeneroPreguntasWindow" ValidationGroup="vgGenero"></telerik:RadButton>
                                            <telerik:RadButton ID="btnXPreGenero" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgGenero" OnClientClicked="DeleteGeneroPreguntas"></telerik:RadButton>
                                        </div>
                                    </div>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <label id="lbPreAdicionales" name="lbPreAdicionales" runat="server">Campos adicionales:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadListBox ID="rlbPreAdicionales" Width="200" Height="100px" runat="server" ValidationGroup="vgAdicionales"></telerik:RadListBox>
                                            <telerik:RadButton ID="btnBPreAdicionales" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgAdicionales" OnClientClicked="OpenSelectionPreAdicionales"></telerik:RadButton>
                                            <telerik:RadButton ID="btnXPreAdicionales" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgAdicionales" OnClientClicked="DeletePreAdicionales"></telerik:RadButton>
                                        </div>
                                    </div>
                                    <div style="clear: both; height: 20px"></div>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda">
                                            <telerik:RadButton RenderMode="Lightweight" ID="chkPreguntasEdad" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                AutoPostBack="false">
                                            </telerik:RadButton>
                                            <label id="lbEdadPre" name="lbEdadPre" runat="server">Edad:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadNumericTextBox runat="server" ID="rntbPreguntasMin" NumberFormat-DecimalDigits="0" Value="18" Name="rntbPreguntasMin" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            a
                                        <telerik:RadNumericTextBox runat="server" ID="rntbPreguntasMax" NumberFormat-DecimalDigits="0" Value="65" Name="rntbPreMax" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            años.
                                        </div>
                                    </div>
                                    <div class="ctrlBasico">
                                        <div class="divControlIzquierda" style="margin-left: 95px">
                                            <telerik:RadButton RenderMode="Lightweight" ID="chkAntiguedad" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                AutoPostBack="false">
                                            </telerik:RadButton>
                                            <label id="lbAntiguedadPre" name="lbAntiguedadPre" runat="server">Antigüedad:</label>
                                        </div>
                                        <div class="divControlDerecha">
                                            <telerik:RadNumericTextBox runat="server" ID="rntbAntiguedadMin" NumberFormat-DecimalDigits="0" Value="0" Name="rntbAntiguedadMin" Width="60px" MinValue="0" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            a
                                        <telerik:RadNumericTextBox runat="server" ID="rntbAntiguedadMax" NumberFormat-DecimalDigits="0" Value="30" Name="rntbAntiguedadMax" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                            meses.
                                        </div>
                                    </div>
                                    <div style="clear: both; height: 20px"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnFiltrarPreguntas" runat="server" name="btnFiltrarPreguntas" AutoPostBack="true" Text="Reporte" Width="100" OnClick="btnFiltrarPreguntas_Click"></telerik:RadButton>
                                    </div>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="rpvPreguntas" runat="server" Height="100%">
                                    <div style="height: calc(100% - 50px); padding-right: 10px; padding-left: 10px; padding-top: 10px;">
                                        <telerik:RadGrid
                                            ID="rgResultadosPreguntas"
                                            runat="server"
                                            Height="100%"
                                            AutoGenerateColumns="false"
                                            EnableHeaderContextMenu="false"
                                            AllowSorting="true"
                                            AllowMultiRowSelection="false"
                                            OnNeedDataSource="rgResultadosPreguntas_NeedDataSource"
                                            OnDetailTableDataBind="rgResultadosPreguntas_DetailTableDataBind"
                                            OnItemCommand="rgResultadosPreguntas_ItemCommand">
                                            <ClientSettings EnableAlternatingItems="false">
                                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                                <Selecting AllowRowSelect="false" />
                                            </ClientSettings>
                                            <PagerStyle AlwaysVisible="true" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView DataKeyNames="ID_PREGUNTA" HierarchyDefaultExpanded="true" EnableHierarchyExpandAll="true" ClientDataKeyNames="ID_PREGUNTA" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="false">
                                                <DetailTables>
                                                    <telerik:GridTableView DataKeyNames="ID_CUESTIONARIO_PREGUNTA"
                                                        ClientDataKeyNames="ID_CUESTIONARIO_PREGUNTA" Name="PruebaDetails" Width="100%">
                                                        <Columns>
                                                            <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" HeaderText="Respuesta" DataField="NB_RESPUESTA" UniqueName="DS_META" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" HeaderText="Contestado" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                                        </Columns>
                                                    </telerik:GridTableView>
                                                </DetailTables>
                                                <Columns>
                                                    <telerik:GridTemplateColumn DataField="NB_PREGUNTA" HeaderStyle-Width="250" HeaderText="Preguntas" HeaderStyle-Font-Bold="true" UniqueName="NB_PREGUNTA" ReadOnly="true" ItemStyle-BackColor="#A20804" ItemStyle-ForeColor="White">
                                                        <ItemTemplate>
                                                            <div title="<%# Eval("DS_PREGUNTA") %>"><%# Eval("NB_PREGUNTA") %></div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </div>
                    </telerik:RadPageView>
                    <%-- Inicio de resultados global --%>
                    <telerik:RadPageView ID="rpResultadoGlobal" runat="server" Height="100%">
                        <div class="ctrlBasico" style="width: 60%; height: 100%">
                            <telerik:RadHtmlChart runat="server" ID="rhcGraficaGlobal" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                <ChartTitle Text="Resultado global">
                                    <Appearance Align="Center" Position="Top">
                                    </Appearance>
                                </ChartTitle>
                                <Legend>
                                    <Appearance Position="Right" Visible="true">
                                    </Appearance>
                                </Legend>
                                <PlotArea>
                                </PlotArea>
                            </telerik:RadHtmlChart>
                        </div>
                        <div class="ctrlBasico" style="width: 40%; height: 100%">
                            <telerik:RadGrid ID="rgGraficaGlobal"
                                runat="server"
                                AllowSorting="false"
                                AutoGenerateColumns="false">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                                    <Selecting AllowRowSelect="false" />
                                </ClientSettings>
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="false" AllowPaging="false" ShowHeadersWhenNoRecords="true">
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="120" HeaderText="Categoría" DataField="NOMBRE" UniqueName="NOMBRE" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="30" HeaderText="Cantidad" DataField="NO_CANTIDAD" UniqueName="NO_CANTIDAD" ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="50" HeaderText="Porcentaje" DataField="PORCENTAJE" UniqueName="PORCENTAJE" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </telerik:RadPageView>
                    <%-- fin de resultados global --%>
                </telerik:RadMultiPage>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Width="20px" Height="100%">
                <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" Height="100%" ExpandedPaneId="rsReportes" Width="20px" DockedPaneId="rsbReportes">
                    <telerik:RadSlidingPane ID="rsbAyuda" runat="server" CollapseMode="Forward" EnableResize="false" Width="325px" Title="Ayuda" Height="100%">
                        <div id="divContexto" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p>
                                Información del período.													
                            </p>
                        </div>
                        <div id="divIndice" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p>
                                El reporte de índice de satisfacción refleja el grado de 
                                 conformidad de la persona con respecto a su entorno de trabajo.<br />
                                <br />

                                Indica los filtros búsqueda para Empleados, así como para las 
                                preguntas en los cuestionarios. 																
                            </p>
                        </div>

                        <div id="divDistribucion" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p>
                                El reporte de distribución de resultados muestra la partición de 
                                resultados entre las respuestas a las preguntas realizadas dentro
                                del periodo.<br />
                                <br />

                                Indica los filtros búsqueda para Empleados, así como para las 
                                preguntas en los cuestionarios. 
                            </p>
                        </div>
                        <div id="divPreguntas" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p>
                                El reporte muestra las respuestas a las preguntas abiertas aplicadas dentro del periodo. Los renglones en blanco representan evaluadores que omitieron su respuesta.<br />
                                <br />

                                Indica los filtros búsqueda para Empleados, así como para las preguntas en los cuestionarios. 													
                            </p>
                        </div>
                        <div id="divGlobal" runat="server" style="display: none; padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                            <p>
                                El reporte presenta los resultados obtenidos en el periodo en base a las opciones que se indican a los evaluadores.													
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                    <telerik:RadSlidingPane ID="rspDefinicionCriterios" runat="server" Title="Simbología" Width="380px" RenderMode="Mobile" Height="100%">
                        <div style="padding: 10px; text-align: justify;">
                            <telerik:RadGrid ID="grdCodigoColores"
                                runat="server"
                                Height="400"
                                Width="350"
                                AllowSorting="true"
                                AllowFilteringByColumn="true"
                                ShowHeader="true"
                                OnNeedDataSource="grdCodigoColores_NeedDataSource">
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                                </ClientSettings>
                                <GroupingSettings CaseSensitive="false" />
                                <PagerStyle AlwaysVisible="true" />
                                <MasterTableView AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                        AddNewRecordText="Insertar" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Color" HeaderStyle-Width="60" AllowFiltering="false">
                                            <ItemTemplate>
                                                <div style="margin: auto; width: 25px; border: 1px solid gray; background: <%# Eval("COLOR")%>; border-radius: 5px;">&nbsp;&nbsp;</div>
                                                &nbsp;
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="260" HeaderText="Descripción" DataField="DESCRIPCION"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
</asp:Content>
