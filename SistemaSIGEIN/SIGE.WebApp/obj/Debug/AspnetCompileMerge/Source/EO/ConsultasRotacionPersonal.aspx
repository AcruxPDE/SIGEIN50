<%@ Page Title="" Language="C#" MasterPageFile="~/EO/MenuEO.master" AutoEventWireup="true" CodeBehind="ConsultasRotacionPersonal.aspx.cs" Inherits="SIGE.WebApp.EO.ConsultasRotacionPersonal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function OpenSelectionGeneroIndice() {
            openChildDialog("../Comunes/SeleccionCatalogos.aspx?ID_CATALOGO=2" + "&CatalogoCl=INDICE_GENERO", "WinConsultaPersonal", "Selección de género");
        }

        function OpenSelectionDepartamentoIndice() {
            openChildDialog("../Comunes/SeleccionArea.aspx?CatalogoCl=INDICE_DEPARTAMENTO", "WinConsultaPersonal", "Selección de departamento");
        }

        function OpenSelectionGeneroCausa() {
            openChildDialog("../Comunes/SeleccionCatalogos.aspx?ID_CATALOGO=2" + "&CatalogoCl=CAUSA_GENERO", "WinConsultaPersonal", "Selección de género");
        }

        function OpenSelectionDepartamentoCausa() {
            openChildDialog("../Comunes/SeleccionArea.aspx?CatalogoCl=CAUSA_DEPARTAMENTO", "WinConsultaPersonal", "Selección de departamento");
        }

        function OpenSelectionAdicionales() {
            openChildDialog("../Comunes/SeleccionAdscripciones.aspx?MultiSeleccion=1", "WinConsultaPersonal", "Selección de campos adicionales");
        }

        //var idEmpleadoBaja = "";
        //var idEmpleado = "";

        //function OpenWindowCapturarBaja() {
        //     obtenerIdFila();
        //    if (idEmpleadoBaja != "")
        //        openChildDialog("CapturarBajaPendiente.aspx?pIdEmpleadoBaja=" + idEmpleadoBaja + "&pIdEmpleado=" + idEmpleado, "winBajaPendiente", "Capturar baja pendiente");
        //     else
        //        radalert("Selecciona un empleado.", 400, 150);

        //}

        //function ConfirmarCancelar(sender, args) {
        //    var MasterTable = $find("<=rgBajasPendientes.ClientID %>").get_masterTableView();
        //    var selectedRows = MasterTable.get_selectedItems();
        //    var row = selectedRows[0];
        //    if (row != null) {
        //        CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_EMPLEADO");
        //        if (selectedRows != "") {
        //            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
        //                if (shouldSubmit) {
        //                    this.click();
        //                }
        //            });
        //            radconfirm('¿Deseas cancelar la baja de "' + CELL_NOMBRE.innerHTML + '"?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Cancelar baja");
        //            args.set_cancel(true);
        //        }
        //    } else {
        //        radalert("Selecciona un empleado", 400, 150, "Error");
        //        args.set_cancel(true);
        //    }
        //}

        //function obtenerIdFila() {
        //    idEmpleadoBaja = "";
        //    idEmpleado = "";
        //    var grid = $find("<=rgBajasPendientes.ClientID %>");
        //     var MasterTable = grid.get_masterTableView();
        //     var selectedRows = MasterTable.get_selectedItems();
        //     if (selectedRows.length != 0) {
        //         var row = selectedRows[0];
        //         var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
        //         idEmpleadoBaja = SelectDataItem.getDataKeyValue("ID_BAJA_EMPLEADO");
        //         idEmpleado = SelectDataItem.getDataKeyValue("ID_EMPLEADO");
        //     }
        // }


        function useDataFromChild(pDato) {
            if (pDato != null) {
                var arr = [];
                var vDatosSeleccionados = pDato[0];
                switch (pDato[0].clTipoCatalogo) {
                    case "INDICE_GENERO":
                        list = $find("<%=lstGeneroIndice.ClientID %>");
                         InsertGenero(list, vDatosSeleccionados);
                         break;

                     case "INDICE_DEPARTAMENTO":
                         var vListBox = $find("<%=lstDepartamentosIndice.ClientID %>");
                         InsertDepartamentos(pDato, vListBox);
                         break;

                     case "ADSCRIPCION":
                         var vListBox = $find("<%= rlbAdicionales.ClientID%>");
                         InsertAdicionales(pDato, vListBox);
                         break;
                    //case "BAJA_PENDIENTE":
                    //    $find("<=rgBajasPendientes.ClientID%>").get_masterTableView().rebind();
                    //    $find("<=grdHistorialBaja.ClientID%>").get_masterTableView().rebind();
                    //    break;
                 }
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

         function DeleteDepartamentoIndice() {
             var vListBox = $find("<%=lstDepartamentosIndice.ClientID %>");
            Delete(vListBox);
        }

        function DeleteGeneroIndice() {
            var vListBox = $find("<%=lstGeneroIndice.ClientID %>");
            Delete(vListBox);
        }

        function DeleteAdicionales() {
            var vListAdscripcion = $find("<%= rlbAdicionales.ClientID %>");
             Delete(vListAdscripcion);
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
             <telerik:AjaxSetting AjaxControlID="btnCapturarBaja">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgBajasPendientes" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 20px);">
        <telerik:RadSplitter ID="rsReportes" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpReportes" runat="server" Width="20px" Height="100%">
                <telerik:RadSlidingZone ID="rszReportes" runat="server" SlideDirection="Right" Height="100%" ExpandedPaneId="rsReportes" Width="20px" DockedPaneId="rsbReportes">
                    <telerik:RadSlidingPane ID="rsbReportes" runat="server" CollapseMode="Forward" EnableResize="false" Width="250px" Title="Reporte" Height="100%">
                        <telerik:RadTabStrip ID="tbReportes" runat="server" SelectedIndex="0" MultiPageID="mpgReportes" Orientation="VerticalLeft" Width="250" Font-Size="Small" Align="Left">
                            <Tabs>
                                <telerik:RadTab Text="Parámetros de Análisis" runat="server" SelectedIndex="0"></telerik:RadTab>
                                <telerik:RadTab Text="Índice de Rotación" runat="server" SelectedIndex="1"></telerik:RadTab>
                                <telerik:RadTab Text="Causas de Rotación" runat="server" SelectedIndex="2"></telerik:RadTab>
                                <telerik:RadTab Text="Historial de Bajas" runat="server" SelectedIndex="3"></telerik:RadTab>
                               <%-- <telerik:RadTab Text="Bajas Pendientes" runat="server" SelectedIndex="3"></telerik:RadTab>--%>
                            </Tabs>
                        </telerik:RadTabStrip>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
            <telerik:RadPane ID="radGraficasReportes" runat="server" Height="100%">
                <telerik:RadMultiPage ID="mpgReportes" runat="server" SelectedIndex="0" Height="100%">
                    <telerik:RadPageView ID="rpvParametrosAnalisis" runat="server" Height="100%">
                        <div style="clear: both; height: 10px"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label1"
                                    name="lbFechaInicio"
                                    runat="server">
                                    Fecha inicio:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadDatePicker ID="rdpFechaInicio" runat="server" Width="150px">
                                </telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label2"
                                    name="lbFechaFin"
                                    runat="server">
                                    Fecha fin:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadDatePicker ID="rdpFechaFin" runat="server" Width="150px">
                                </telerik:RadDatePicker>
                            </div>
                        </div>
                        <div style="clear: both; height: 20px"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label3" name="lblTipoReporte" runat="server">Tipo de reporte:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbTipoReporte" Width="190" MarkFirstMatch="true" AutoPostBack="false" DropDownWidth="190">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Diario" Value="1" />
                                        <telerik:RadComboBoxItem runat="server" Text="Semanal" Value="2" />
                                        <telerik:RadComboBoxItem runat="server" Text="Mensual" Value="3" />
                                        <telerik:RadComboBoxItem runat="server" Text="Anual" Value="4" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div style="clear: both; height: 20px"></div>
                        <div class="ctrlBasico">
                            <label id="Label4"
                                name="lblCriterios"
                                runat="server">
                                Criterios de selección para empleados:
                            </label>
                        </div>
                        <div style="clear: both; height: 20px"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label5" name="lblDepartamento" runat="server">Área:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadListBox ID="lstDepartamentosIndice" Width="200px" Height="100px" runat="server" ValidationGroup="vgDepartamento"></telerik:RadListBox>
                                <telerik:RadButton ID="RadButton1" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="OpenSelectionDepartamentoIndice"></telerik:RadButton>
                                <telerik:RadButton ID="RadButton2" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="DeleteDepartamentoIndice"></telerik:RadButton>
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <label id="Label6" name="lblGenero" runat="server">Género:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadListBox ID="lstGeneroIndice" Width="100" Height="35px" runat="server" ValidationGroup="vgDepartamento"></telerik:RadListBox>
                                <telerik:RadButton ID="RadButton3" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="OpenSelectionGeneroIndice"></telerik:RadButton>
                                <telerik:RadButton ID="RadButton4" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="DeleteGeneroIndice"></telerik:RadButton>
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
                                <label id="Label7" name="lblEdad" runat="server">Edad:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" ID="rnEdadInicial" NumberFormat-DecimalDigits="0" Value="18" Name="rnEdadInicial" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                a
                                        <telerik:RadNumericTextBox runat="server" ID="rnEdadFinal" NumberFormat-DecimalDigits="0" Value="65" Name="rnEdadFinal" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                años.
                            </div>
                        </div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda">
                                <telerik:RadButton RenderMode="Lightweight" ID="rbAntiguedadIndice" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                    AutoPostBack="false">
                                </telerik:RadButton>
                                <label id="Label8" name="lblEdad" runat="server">Antigüedad:</label>
                            </div>
                            <div class="divControlDerecha">
                                <telerik:RadNumericTextBox runat="server" ID="rnAntiguedadInicial" NumberFormat-DecimalDigits="0" Value="0" Name="rnAntiguedadInicial" Width="60px" MinValue="0" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                a
                                        <telerik:RadNumericTextBox runat="server" ID="rnAtiguedadFinal" NumberFormat-DecimalDigits="0" Value="30" Name="rnAtiguedadFinal" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                años.
                            </div>
                        </div>
                        <div style="clear: both; height: 20px"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="RadButton7" runat="server" name="btnGraficaIndiceRotacion" AutoPostBack="true" Text="Reporte" Width="100" OnClick="btnGraficaIndiceRotacion_Click"></telerik:RadButton>
                        </div>
                    </telerik:RadPageView>
                    <%-- Fin de Parámetros de Análisis --%>

                    <%-- Inicio de indice de rotación --%>
                    <telerik:RadPageView ID="rpvIndiceRotacion" runat="server" Height="100%">
                        <%-- <telerik:RadTabStrip ID="rtIndice" runat="server" SelectedIndex="0"  MultiPageID="mpgIndice">
                                <Tabs>
                                    <telerik:RadTab Text="Parámetros de Análisis" runat="server" SelectedIndex="1" ></telerik:RadTab>
                                    <telerik:RadTab Text="Grafica de índice de Rotación" runat="server" SelectedIndex="2" ></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>--%>
                        <div style="height: calc(100% - 50px);">
                            <%-- <telerik:RadMultiPage ID="mpgIndice" runat="server" SelectedIndex="0" Height="100%"  >
                                    <telerik:RadPageView ID="rpvParametrosIndiceRotacion" runat="server" Height="100%">
                                   
                                        <div style="clear: both; height: 10px"></div>
                                        <div class="ctrlBasico">
                                            <div class="divControlIzquierda" >
                                                <label id="lbFechaInicio"
                                                    name="lbFechaInicio"
                                                    runat="server">
                                                    Fecha inicio:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadDatePicker ID="rdpFechaInicio"  runat="server" Width="150px">
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>
                                         <div class="ctrlBasico">
                                            <div class="divControlIzquierda"  >
                                                <label id="lblFechaFin"
                                                    name="lbFechaFin"
                                                    runat="server">
                                                    Fecha fin:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadDatePicker ID="rdpFechaFin"  runat="server" Width="150px">
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>
                                    <div style="clear: both;height:20px"></div>
                                        <div class="ctrlBasico">
                                            <div class="divControlIzquierda">
                                            <label id="lblTipoReporte" name="lblTipoReporte" runat="server">Tipo de reporte:</label>
                                       </div>
                                            <div class="divControlDerecha">
                                            <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbTipoReporte" Width="190" MarkFirstMatch="true" AutoPostBack="false" DropDownWidth="190" >
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="Diario" Value="1" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Semanal" Value="2" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Mensual" Value="3" />
                                                     <telerik:RadComboBoxItem runat="server" Text="Anual" Value="4" />
                                                </Items>
                                            </telerik:RadComboBox>
                                                </div>
                                        </div>
                                        <div style="clear: both;height:20px"></div>
                                        <div class="ctrlBasico">
                                        <label id="lblCriterios"
                                            name="lblCriterios"
                                            runat="server">
                                            Criterios de selección para empleados: </label>
                                        </div>
                                        <div style="clear: both;height:20px"></div>
                                        <div class="ctrlBasico">
                                            <div class="divControlIzquierda">
                                                <label id="lblDepartamento" name="lblDepartamento" runat="server"> Departamento:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadListBox ID="lstDepartamentosIndice" Width="200px" Height="100px" runat="server" ValidationGroup="vgDepartamento"></telerik:RadListBox>
                                                <telerik:RadButton ID="btnSeleccionarDepartamentoInd" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="OpenSelectionDepartamentoIndice" ></telerik:RadButton>
                                                <telerik:RadButton ID="btnEliminarDepartamentoInd" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="DeleteDepartamentoIndice" ></telerik:RadButton>
                                            </div>
                                        </div>
                                     
                                         <div class="ctrlBasico">
                                            <div class="divControlIzquierda">
                                                <label id="lblGenero" name="lblGenero" runat="server"> Género:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadListBox ID="lstGeneroIndice" Width="100" Height="35px" runat="server" ValidationGroup="vgDepartamento"></telerik:RadListBox>
                                                <telerik:RadButton ID="btnSeleccionarGeneroInd" runat="server" Text="B" AutoPostBack="false"  ValidationGroup="vgDepartamento" OnClientClicked="OpenSelectionGeneroIndice"></telerik:RadButton>
                                                <telerik:RadButton ID="btnEliminarGeneroInd" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="DeleteGeneroIndice"></telerik:RadButton>
                                            </div>
                                        </div>
                                        <div style="clear: both;height:20px"></div>
                                        <div class="ctrlBasico">
                                            <div class="divControlIzquierda">
                                                 <telerik:RadButton RenderMode="Lightweight"  ID="rbEdadIndice" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                    AutoPostBack="false">
                                                 </telerik:RadButton>
                                                <label id="lblEdad" name="lblEdad" runat="server">Edad:</label>
                                                </div>
                                            <div class="divControlDerecha">
                                        <telerik:RadNumericTextBox runat="server" ID="rnEdadInicial" NumberFormat-DecimalDigits="0" Value="18" Name="rnEdadInicial" Width="60px" MinValue="1" ShowSpinButtons="true" ></telerik:RadNumericTextBox> a
                                        <telerik:RadNumericTextBox runat="server" ID="rnEdadFinal" NumberFormat-DecimalDigits="0" Value="65"  Name="rnEdadFinal" Width="60px" MinValue="1" ShowSpinButtons="true" ></telerik:RadNumericTextBox> años.
                                                </div>
                                        </div>
                                         <div class="ctrlBasico">
                                             <div class="divControlIzquierda" >
                                                  <telerik:RadButton RenderMode="Lightweight"  ID="rbAntiguedadIndice" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                    AutoPostBack="false">
                                                 </telerik:RadButton>
                                        <label id="lblAntiguedad" name="lblEdad" runat="server">Antigüedad:</label>
                                                 </div>
                                             <div class="divControlDerecha">
                                        <telerik:RadNumericTextBox runat="server" ID="rnAntiguedadInicial" NumberFormat-DecimalDigits="0" Value="0" Name="rnAntiguedadInicial" Width="60px" MinValue="0" ShowSpinButtons="true" ></telerik:RadNumericTextBox> a
                                        <telerik:RadNumericTextBox runat="server" ID="rnAtiguedadFinal" NumberFormat-DecimalDigits="0"  Value="30"   Name="rnAtiguedadFinal" Width="60px" MinValue="1" ShowSpinButtons="true" > </telerik:RadNumericTextBox> años.
                                                 </div>
                                        </div>
                                        <div style="clear: both;height:20px"></div>
                                         <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnGraficaIndiceRotacion" runat="server" name="btnGraficaIndiceRotacion"  AutoPostBack="true" Text="Reporte" Width="100" OnClick="btnGraficaIndiceRotacion_Click" ></telerik:RadButton>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="rpvGraficaIndiceRotacion" runat="server" Height="100%">--%>
                            <div class="ctrlBasico" style="width: 60%; height: 100%">
                                <telerik:RadHtmlChart runat="server" ID="rhlIndiceRotacion" Height="100%" Transitions="true">
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries>
                                                <LabelsAppearance DataFormatString="{0}"></LabelsAppearance>
                                                <TooltipsAppearance DataFormatString="{0}"></TooltipsAppearance>
                                            </telerik:ColumnSeries>
                                        </Series>
                                        <Appearance>
                                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                                        </Appearance>
                                    </PlotArea>
                                    <Appearance>
                                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                                    </Appearance>
                                    <ChartTitle Text="Índice de Rotación">
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
                                <telerik:RadGrid ID="rgGraficaIndiceRotacion"
                                    runat="server"
                                    AllowSorting="false"
                                    AutoGenerateColumns="false" HeaderStyle-Font-Bold="true">
                                    <ClientSettings AllowKeyboardNavigation="false">
                                        <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                                        <Selecting AllowRowSelect="false" />
                                    </ClientSettings>
                                    <MasterTableView AllowFilteringByColumn="false" AllowPaging="false" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="120" DataField="NB_CANTIDAD" UniqueName="NB_CANTIDAD">
                                                <HeaderStyle Font-Bold="true" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="30" DataField="PR_CANTIDAD" UniqueName="PR_CANTIDAD" ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle Font-Bold="true" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <div style="clear: both; height: 20px"></div>
                                 
                                    <telerik:RadGrid ID="rgEmpleadosGrafica"
                                        runat="server"
                                        AllowSorting="false" Height="350px"
                                        AutoGenerateColumns="false"
                                        Width="50%" HeaderStyle-Font-Bold="true">
                                        <ClientSettings AllowKeyboardNavigation="false">
                                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                            <Selecting AllowRowSelect="false" />
                                        </ClientSettings>
                                        <MasterTableView AllowFilteringByColumn="false" AllowPaging="false" ShowHeadersWhenNoRecords="true">
                                            <Columns>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="120" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderText="Empleados seleccionados">
                                                    <HeaderStyle Font-Bold="true" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                
                            </div>
                            <%--  </telerik:RadPageView>
                                </telerik:RadMultiPage> --%>
                        </div>
                    </telerik:RadPageView>
                    <%-- fin de indice de rotación --%>

                    <%-- Inicio causas de rotación --%>
                    <telerik:RadPageView ID="rpvCausasRotacion" runat="server" Height="100%">
                        <div style="height: calc(100% - 50px);">
                            <%--<telerik:RadTabStrip ID="rtsCausas" runat="server" SelectedIndex="0"  MultiPageID="mpgCausas">
                                <Tabs>
                                    <telerik:RadTab Text="Parámetros de Análisis" runat="server" SelectedIndex="1" ></telerik:RadTab>
                                    <telerik:RadTab Text="Grafica de causas de Rotación" runat="server" SelectedIndex="2" ></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                                <telerik:RadMultiPage ID="mpgCausas" runat="server" SelectedIndex="0" Height="100%"  >
                                    <telerik:RadPageView ID="rpvParametrosCausas" runat="server" Height="100%">
                                   
                                        <div style="clear: both; height: 10px"></div>
                                        <div class="ctrlBasico">
                                            <div class="divControlIzquierda" >
                                                <label id="lblFechaInicioCausa"
                                                    name="lbFechaInicio"
                                                    runat="server">
                                                    Fecha inicio:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadDatePicker ID="rdpFechaInicioCausa"  runat="server" Width="150px">
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>
                                         <div class="ctrlBasico">
                                            <div class="divControlIzquierda"  >
                                                <label id="lbFechaFinCausa"
                                                    name="lbFechaFinCausa"
                                                    runat="server">
                                                    Fecha fin:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadDatePicker ID="rdpFechaFinCausa"  runat="server" Width="150px">
                                                </telerik:RadDatePicker>
                                            </div>
                                        </div>
                                    <div style="clear: both;height:20px"></div>

                                      
                                        <div style="clear: both;height:20px"></div>
                                        <div class="ctrlBasico">
                                        <label id="lblCriteriosCausa"
                                            name="lblCriterios"
                                            runat="server">
                                            Criterios de selección para empleados: </label>
                                        </div>
                                        <div style="clear: both;height:20px"></div>
                                        <div class="ctrlBasico">
                                            <div class="divControlIzquierda">
                                                <label id="lblDepartamentosCausa" name="lblDepartamento" runat="server"> Departamento:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadListBox ID="rlbDepartamentoCausa" Width="200px" Height="100px" runat="server" ValidationGroup="vgDepartamento" ></telerik:RadListBox>
                                                <telerik:RadButton ID="rbDepartamentoCausaB" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="OpenSelectionDepartamentoCausa" ></telerik:RadButton>
                                                <telerik:RadButton ID="rbDepartamentoCausaX" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgDepartamento" OnClientClicked="DeleteDepartamentoCausa"></telerik:RadButton>
                                            </div>
                                        </div>
                                     
                                         <div class="ctrlBasico">
                                            <div class="divControlIzquierda">
                                                <label id="lblGeneroCausa" name="lblGenero" runat="server"> Género:</label>
                                            </div>
                                            <div class="divControlDerecha">
                                                <telerik:RadListBox ID="rlbGeneroCausa" Width="100" Height="35px" runat="server" ValidationGroup="vgDepartamento"></telerik:RadListBox>
                                                <telerik:RadButton ID="rbGeneroCausaB" runat="server" Text="B" AutoPostBack="false"  ValidationGroup="vgDepartamento" OnClientClicked="OpenSelectionGeneroCausa"></telerik:RadButton>
                                                <telerik:RadButton ID="rbGeneroCausaX" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgDepartamento"  OnClientClicked="DeleteGeneroCausa"></telerik:RadButton>
                                            </div>
                                        </div>
                                        <div style="clear: both;height:20px"></div>
                                        <div class="ctrlBasico">
                                            <div class="divControlIzquierda">
                                                 <telerik:RadButton RenderMode="Lightweight"  ID="rbFiltroEdadCausa" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                    AutoPostBack="false">
                                                 </telerik:RadButton>
                                                <label id="lblEdadCausa" name="lblEdad" runat="server">Edad:</label>
                                                </div>
                                            <div class="divControlDerecha">
                                        <telerik:RadNumericTextBox runat="server" ID="rntEdadInicialCausa" NumberFormat-DecimalDigits="0" Value="18" Name="rnEdadInicial" Width="60px" MinValue="1" ShowSpinButtons="true" ></telerik:RadNumericTextBox> a
                                        <telerik:RadNumericTextBox runat="server" ID="rntEdadFinalCausa" NumberFormat-DecimalDigits="0" Value="65"  Name="rnEdadFinal" Width="60px" MinValue="1" ShowSpinButtons="true" ></telerik:RadNumericTextBox> años.
                                                </div>
                                        </div>
                                         <div class="ctrlBasico">
                                             <div class="divControlIzquierda" >
                                                  <telerik:RadButton RenderMode="Lightweight"  ID="rbFiltroAntiguedadCausa" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                    AutoPostBack="false">
                                                 </telerik:RadButton>
                                        <label id="lblAntiguedaCausa" name="lblAntiguedadCausa" runat="server">Antigüedad:</label>
                                                 </div>
                                             <div class="divControlDerecha">
                                        <telerik:RadNumericTextBox runat="server" ID="rnAntiguedadInicialCausa" NumberFormat-DecimalDigits="0" Value="0" Name="rnAntiguedadInicial" Width="60px" MinValue="0" ShowSpinButtons="true" ></telerik:RadNumericTextBox> a
                                        <telerik:RadNumericTextBox runat="server" ID="rnAntiguedadFinalCausa" NumberFormat-DecimalDigits="0"  Value="30"   Name="rnAtiguedadFinal" Width="60px" MinValue="1" ShowSpinButtons="true" > </telerik:RadNumericTextBox> años.
                                                 </div>
                                        </div>
                                        <div style="clear: both;height:20px"></div>
                                         <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnGraficaCausaRotacion" runat="server" name="btnGraficaCausaRotacion"  AutoPostBack="true" Text="Reporte" Width="100" OnClick="btnGraficaCausaRotacion_Click"></telerik:RadButton>
                                        </div>
                                    </telerik:RadPageView>

                                     <telerik:RadPageView ID="rpvGraficaCausasRotacion" runat="server" Height="100%">--%>

                            <div class="ctrlBasico" style="width: 60%; height: 100%">
                                <telerik:RadHtmlChart runat="server" ID="rhcGraficaCausasRotacion" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                    <ChartTitle Text="Causas de Rotación">
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
                                <telerik:RadGrid ID="rgGraficaCausaRotacion"
                                    runat="server"
                                    AllowSorting="false"
                                    AutoGenerateColumns="false" HeaderStyle-Font-Bold="true">
                                    <ClientSettings AllowKeyboardNavigation="false">
                                        <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                                        <Selecting AllowRowSelect="false" />
                                    </ClientSettings>
                                    <MasterTableView AllowFilteringByColumn="false" AllowPaging="false" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="120" DataField="NB_CANTIDAD" UniqueName="NB_CANTIDAD">
                                                <HeaderStyle Font-Bold="true" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="30" DataField="PR_CANTIDAD" UniqueName="PR_CANTIDAD" ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle Font-Bold="true" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <%--   </telerik:RadPageView>

                                </telerik:RadMultiPage>--%>
                        </div>
                    </telerik:RadPageView>
                    <%-- fin de causas de rotación --%>

                    <%-- Inicio de historial de bajas --%>
                    <telerik:RadPageView ID="rpHistorialBajas" runat="server" Height="100%">
                        <div style="height: calc(100% - 50px);">
                            <label class="labelTitulo">Historial de baja</label>
                            <div style="height: 10px;"></div>
                            <div style="height: calc(100% - 20px);">
                                <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
                                    <telerik:RadPageView ID="rpvCapturaResultados" runat="server">
                                        <div style="clear: both;"></div>
                                        <telerik:RadGrid ID="grdHistorialBaja" HeaderStyle-Font-Bold="true" runat="server" OnNeedDataSource="grdHistorialBaja_NeedDataSource" AutoGenerateColumns="false" Height="100%" OnItemCommand="grdHistorialBaja_ItemCommand" OnItemDataBound="grdHistorialBaja_ItemDataBound">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                            <PagerStyle AlwaysVisible="true" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView DataKeyNames="ID_EMPLEADO" AllowFilteringByColumn="true" AllowSorting="true" AllowPaging="true">
                                                <Columns>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="70" HeaderText="Fecha de la baja" DataField="FECHA_BAJA" UniqueName="FECHA_BAJA" DataFormatString="{0:d}">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" FilterControlWidth="20" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Nombre completo" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" FilterControlWidth="20" HeaderText="Causa de baja" DataField="NB_CAUSA_BAJA" UniqueName="NB_CAUSA_BAJA">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Comentarios" DataField="DS_COMENTARIO" UniqueName="DS_COMENTARIO">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" FilterControlWidth="20" HeaderText="Activo" DataField="CL_ESTADO" UniqueName="CL_ESTADO">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="70" HeaderText="Fecha de reingreso" DataField="FECHA_REINGRESO" UniqueName="FECHA_REINGRESO" DataFormatString="{0:d}">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </div>
                        </div>
                    </telerik:RadPageView>
                    <%-- fin de historial de bajas --%>

                    <%-- Bajas Pendientes  --%>
          <%--           <telerik:RadPageView ID="rpvBajasPendientes" runat="server" Height="100%">--%>
     <%--                   <div style="height: calc(100% - 50px);">
                            <label class="labelTitulo">Bajas Pendientes</label>
                            <div style="height: 10px;"></div>
                            <div style="height: calc(100% - 70px);">
                                <telerik:RadMultiPage ID="rmpBajasPendientes" runat="server" SelectedIndex="0" Height="100%" >
                                    <telerik:RadPageView ID="rpvBajaPendientes" runat="server">
                                        <div style="clear: both;"></div>
                                        <telerik:RadGrid 
                                            ID="rgBajasPendientes"  HeaderStyle-Font-Bold="true"
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
                                <telerik:RadButton ID="btnCapturarBaja" runat="server" name="btnCapturarBaja" AutoPostBack="false" Text="Capturar baja" Width="150" OnClientClicked="OpenWindowCapturarBaja"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnCancelarBaja" runat="server" Text="Cancelar baja" AutoPostBack="true" OnClientClicking="ConfirmarCancelar" OnClick="btnCancelarBaja_Click"></telerik:RadButton>
                            </div>
                        </div>--%>
                <%--     </telerik:RadPageView>--%>

                    <%--  Fin de bajas pendientes --%>
                </telerik:RadMultiPage>
                <%--</div>--%>
            </telerik:RadPane>
        </telerik:RadSplitter>
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
            <telerik:RadWindow ID="winBajaPendiente" runat="server" Animation="Fade" Width="600" Height="560" VisibleStatusbar="false" ShowContentDuringLoad="true" Behaviors="Close" Modal="true" ></telerik:RadWindow>
              <telerik:RadWindow ID="WinSeleccionCausa" runat="server" Width="1050px" Height="580px" VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="Close" Modal="true" Animation="Fade">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
