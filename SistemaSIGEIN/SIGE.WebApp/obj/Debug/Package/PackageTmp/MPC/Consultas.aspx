<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="Consultas.aspx.cs" Inherits="SIGE.WebApp.MPC.Consultas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <style type="text/css">
            .RadTabStrip {
                white-space: normal;
            }
             

           
            .RadGrid1Class
              {
                background-color: #F2F2F2;
                /*border: 1px solid #ddd !important;
                border-left: 1px;
                border:1px #e5e5e5;  
                border-style:none none solid solid;  
                padding-top:4px;  
                padding-bottom:4px;*/  
                 /*border-color: #FFF !important;*/
              }

             .RadGrid2Class
              {
                background-color: #A9E2F3;
                /*border:1px #e5e5e5;  
                border-style:none none solid solid;  
                padding-top:4px;  
                padding-bottom:4px;*/         
                /*border-color: #FFF !important;*/
              }
        </style>

        <script id="modal" type="text/javascript">

            function OpenSelectionWindow() {
                openChildDialog("SeleccionTabulador.aspx", "winSeleccion", "Selección de tabuladores");
            }

            function useDataFromChild(pDato) {
                if (pDato != null) {
                    var arr = [];
                    var vDatosSeleccionados = pDato[0];
                    switch (pDato[0].clTipoCatalogo) {
                        case "TABULADOR":
                            InsertTabuladores(pDato);
                            break;
                        case "EMPLEADO":
                            for (var i = 0; i < pDato.length; ++i) {
                                arr.push(pDato[i].idEmpleado);
                            }
                            prueba()
                            var datos = JSON.stringify({ clTipo: vDatosSeleccionados.clTipoCatalogo, arrEmpleados: arr, arrIdTabulador: arrIdTabuladores });
                            InsertEmpleado(datos);
                            break;
                        case "TABULADOR_EMPLEADO":
                            for (var i = 0; i < pDato.length; ++i) {
                                arr.push(pDato[i].idEmpleado);
                            }
                            var datos = JSON.stringify({ clTipo: vDatosSeleccionados.clTipoCatalogo, arrEmpleados: arr });
                            InsertEmpleado(datos);
                            break;
                        case "TABULADOR_SUELDOS":
                            for (var i = 0; i < pDato.length; ++i) {
                                arr.push(pDato[i].idEmpleado);
                            }
                            var datos = JSON.stringify({ clTipo: vDatosSeleccionados.clTipoCatalogo, arrEmpleados: arr });
                            InsertEmpleado(datos);
                            break;
                        case "PERIODODESEMPENO":
                            var vJsonPeriodo = JSON.stringify({ clTipo: "PERIODODESEMPENO", oSeleccion: pDato });
                            var ajaxManager = $find('<%= ramConsultas.ClientID%>');
                            ajaxManager.ajaxRequest(vJsonPeriodo);
                            break;
                
                    }
                }
            }

            function InsertEmpleado(pDato) {
                var ajaxManager = $find('<%= ramConsultas.ClientID%>');
               ajaxManager.ajaxRequest(pDato);
           }


           var arrIdTabuladores;

           function prueba() {
               var arr = [];
               var vListBox = $find("<%=lstTabuladores.ClientID %>");
                var items = vListBox.get_items();

                if (vListBox.get_items().get_count() == 0) {
                    arr.push("<%=vIdTabulador%>");
                }
                else {
                    for (var i = 0; i < items.get_count() ; i++) {
                        var item = items.getItem(i);
                        arr.push(item.get_value());
                    }
                    arr.push("<%=vIdTabulador%>");
                    }
                    arrIdTabuladores = arr;
                }

                function OpenSelectionEmployeeWindow() {
                    prueba();
                    if (arrIdTabuladores != "") {
                        openChildDialog("../Comunes/SeleccionEmpleado.aspx?&IdTabuladores=" + arrIdTabuladores + "&vClTipoSeleccion=MC_TABULADORES", "winSeleccion", "Selección de  empleados");
                    }
                }

                function OpenSelectionTabuladorEmployee() {
                    openChildDialog("SeleccionTabuladorEmpleado.aspx?&IdTabulador=" + <%=vIdTabulador%> + "&vClTipoSeleccion=CONSULTAS", "winSeleccion", "Selección de empleados");
                }

                function OpenTabuladorEmpleadoSueldo() {
                    openChildDialog("SeleccionTabuladorEmpleado.aspx?&IdTabulador=" + <%=vIdTabulador%> + "&vClTipoSeleccion=CONSULTAS" + "&CatalogoCl=TABULADOR_SUELDOS", "winSeleccion", "Selección de empleados");
                }


            function InsertTabuladores(pDato) {
                var vListBox = $find("<%=lstTabuladores.ClientID %>");
                vListBox.trackChanges();

                var items = vListBox.get_items();

                for (var i = 0; i < items.get_count() ; i++) {
                    var item = items.getItem(i);
                    var itemValue = item.get_value();
                    var itemText = item.get_text();
                    if (itemValue != "") {
                        var vFgItemEncontrado = false;
                        for (var j = 0; j < pDato.length; j++)
                            vFgItemEncontrado = vFgItemEncontrado || (pDato[j].idTabulador == itemValue)
                        if (!vFgItemEncontrado)
                            pDato.push({
                                idTabulador: itemValue,
                                nbTabulador: itemText
                            });
                    }
                }

                var vArrOriginal = [];
                for (var i = 0; i < pDato.length ; i++)
                    vArrOriginal.push(pDato[i].nbTabulador);

                var vArrOrdenados = vArrOriginal.slice();

                vArrOrdenados.sort();

                var vArrItemsOrdenados = [];

                for (var i = 0; i < vArrOrdenados.length; i++)
                    vArrItemsOrdenados.push(pDato[vArrOriginal.indexOf(vArrOrdenados[i])]);

                items.clear();

                vArrItemsOrdenados.forEach(InsertTabuladorItem);
                vListBox.commitChanges();
            }

            function InsertTabuladorItem(pItem) {
                ChangeListItem(pItem.idTabulador, pItem.nbTabulador);
            }

            function ChangeListItem(pIdItem, pNbItem) {
                var vListBox = $find("<%=lstTabuladores.ClientID %>");
            var items = vListBox.get_items();

            if (vListBox.get_items().get_count() < 2) {
                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(pNbItem);
                item.set_value(pIdItem);
                items.add(item);
                item.set_selected(true);
            }
        }

        function DeleteTabulador() {
            var vListBox = $find("<%=lstTabuladores.ClientID %>");
        var vSelectedItems = vListBox.get_selectedItems();
        vListBox.trackChanges();
        if (vSelectedItems)
            vSelectedItems.forEach(function (item) {
                vListBox.get_items().remove(item);
            });
        //var item = new Telerik.Web.UI.RadListBoxItem();
        //ChangeListItem("", "No seleccionado");
        //vListBox.commitChanges();
    }

    function OnClientClicked(sender, args) {
        var radtabstrip = $find('<%=rtGraficaAnalisisMercado.ClientID %>');
        var count = radtabstrip.get_tabs().get_count();
        var currentindex = radtabstrip.get_selectedIndex();
        var nextindex = currentindex + 1;
        if (nextindex < count) {
            radtabstrip.set_selectedIndex(nextindex);
        }
    }

    
   




    function TabSelected() {
        var vtabStrip = $find("<%= tbCpnsultas.ClientID%>").get_selectedIndex();
            console.info(vtabStrip);

            var divTabuladorMaestro;
            var divMercadoSalarial;
            var divIncrementosPlaneados;
            var divComparacionTabulador;
            var divComparacionMercado;
            var divAyudaGraficaAnalisis;
            var divAyudaAnalisisDesviaciones;

            divTabuladorMaestro = document.getElementById('<%= divTabuladorMaestro.ClientID%>');
            divMercadoSalarial = document.getElementById('<%= divMercadoSalarial.ClientID%>');
            divIncrementosPlaneados = document.getElementById('<%= divIncrementosPlaneados.ClientID%>');
            divComparacionTabulador = document.getElementById('<%= divComparacionTabulador.ClientID%>');
            divComparacionMercado = document.getElementById('<%= divComparacionMercado.ClientID%>');
            divAyudaGraficaAnalisis = document.getElementById('<%= divAyudaGraficaAnalisis.ClientID%>');
            divAyudaAnalisisDesviaciones = document.getElementById('<%= divAyudaAnalisisDesviaciones.ClientID%>');

            switch (vtabStrip) {

                case 0:
                    divTabuladorMaestro.style.display = 'none';
                    divMercadoSalarial.style.display = 'none';
                    divIncrementosPlaneados.style.display = 'none';
                    divComparacionTabulador.style.display = 'block';
                    divComparacionMercado.style.display = 'none';
                    divAyudaGraficaAnalisis.style.display = 'none';
                    divAyudaAnalisisDesviaciones.style.display = 'none';
                    break;
                case 1:
                    divTabuladorMaestro.style.display = 'none';
                    divMercadoSalarial.style.display = 'none';
                    divIncrementosPlaneados.style.display = 'none';
                    divComparacionTabulador.style.display = 'none';
                    divComparacionMercado.style.display = 'none';
                    divAyudaGraficaAnalisis.style.display = 'block';
                    divAyudaAnalisisDesviaciones.style.display = 'none';
                    break;
                case 2:
                    divTabuladorMaestro.style.display = 'none';
                    divMercadoSalarial.style.display = 'none';
                    divIncrementosPlaneados.style.display = 'none';
                    divComparacionTabulador.style.display = 'none';
                    divComparacionMercado.style.display = 'none';
                    divAyudaGraficaAnalisis.style.display = 'none';
                    divAyudaAnalisisDesviaciones.style.display = 'block';
                    break;
                case 3:
                    divTabuladorMaestro.style.display = 'block';
                    divMercadoSalarial.style.display = 'none';
                    divIncrementosPlaneados.style.display = 'none';
                    divComparacionTabulador.style.display = 'none';
                    divComparacionMercado.style.display = 'none';
                    divAyudaGraficaAnalisis.style.display = 'none';
                    divAyudaAnalisisDesviaciones.style.display = 'none';
                   
                    break;
                case 4:
                    divTabuladorMaestro.style.display = 'none';
                    divMercadoSalarial.style.display = 'block';
                    divIncrementosPlaneados.style.display = 'none';
                    divComparacionTabulador.style.display = 'none';
                    divComparacionMercado.style.display = 'none';
                    divAyudaGraficaAnalisis.style.display = 'none';
                    divAyudaAnalisisDesviaciones.style.display = 'none';

                    break;
                case 5:
                    divTabuladorMaestro.style.display = 'none';
                    divMercadoSalarial.style.display = 'none';
                    divIncrementosPlaneados.style.display = 'block';
                    divComparacionTabulador.style.display = 'none';
                    divComparacionMercado.style.display = 'none';
                    divAyudaGraficaAnalisis.style.display = 'none';
                    divAyudaAnalisisDesviaciones.style.display = 'none';
                   
                    break;
                case 6:
                    divTabuladorMaestro.style.display = 'none';
                    divMercadoSalarial.style.display = 'none';
                    divIncrementosPlaneados.style.display = 'none';
                    divComparacionTabulador.style.display = 'none';
                    divComparacionMercado.style.display = 'block';
                    divAyudaGraficaAnalisis.style.display = 'none';
                    divAyudaAnalisisDesviaciones.style.display = 'none';
                    break;
            }
        }


            function OpenSelectionWindows(pURL, pVentana, pTitle) {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                var WindowsProperties = {
                    width: browserWnd.innerWidth - 20,
                    height: browserWnd.innerHeight - 20
                };
                openChildDialog(pURL, pVentana, pTitle, WindowsProperties);
            }

            function OpenWindowPeriodos() {
                OpenSelectionWindows("/Comunes/SeleccionPeriodosDesempeno.aspx?CL_TIPO=Bono", "winSeleccion", "Seleccion de períodos a comparar");
            }

        function OpenWindowComparar() {
            OpenSelectionWindows("VentanaConsultaBono.aspx", "winBonos", "Comparación de bonos");
        }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConsultas" runat="server" OnAjaxRequest="ramConsultas_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramConsultas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgComparativos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>

           <telerik:AjaxSetting AjaxControlID="ramConsultas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdEmpleados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ScatterChartGraficaAnalisis" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgEmpleadosTabuladorSueldos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgdComparacionInventarioPersonal" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="rmpGraficaAnalisisMercado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rmpGraficaAnalisisMercado" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
          
           <telerik:AjaxSetting AjaxControlID="rgdEmpleados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdEmpleados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ScatterChartGraficaAnalisis" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
      <telerik:AjaxSetting AjaxControlID="btnSeleccionarEmpleado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdEmpleados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
      <telerik:AjaxSetting AjaxControlID="rgEmpleadosTabuladorSueldos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEmpleadosTabuladorSueldos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgdComparacionInventarioPersonal" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
           <%--  <telerik:AjaxSetting AjaxControlID="rcbNivelMercado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbNivelMercado" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgAnalisisDesviaciones" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            
           <%--  <telerik:AjaxSetting AjaxControlID="btnSeleccionEnpleadosDes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEmpleadosDesviasion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="rgdTabuladorMaestro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdTabuladorMaestro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
                 <telerik:AjaxSetting AjaxControlID="rgdMercadoSalarial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdMercadoSalarial" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgdIncrementosPlaneados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdIncrementosPlaneados" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEmpleadoCriterio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdComparacionInventarioPersonal" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgdComparacionInventarioPersonal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdComparacionInventarioPersonal" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgdComparacionInventarioMercado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdComparacionInventarioMercado" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
           <%-- <telerik:AjaxSetting AjaxControlID="rgAnalisisDesviaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAnalisisDesviaciones" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadToolTip ID="rttVerde" runat="server" ShowDelay="500" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
        TargetControlID="Span1" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_IP">
        <span style="font-weight: bold">Sueldo dentro del nivel establecido por el tabulador (variación inferior al 10%).</span>
    </telerik:RadToolTip>

    <div style="height: 10px;"></div>
    <div class="ctrlBasico">
        <label id="lbTabulador"
            name="lbTabulador"
            runat="server">
            Tabulador:</label>
        <telerik:RadTextBox ID="txtClTabulador"
            runat="server"
            Width="150px"
            MaxLength="800"
            Enabled="false">
        </telerik:RadTextBox>
    </div>
    <div class="ctrlBasico">
        <telerik:RadTextBox ID="txtDsTabulador"
            runat="server"
            Width="260px"
            MaxLength="800"
            Enabled="false">
        </telerik:RadTextBox>
    </div>
    <div class="ctrlBasico">
        <label id="lbFecha"
            name="lbFecha"
            runat="server">
            Fecha:</label>
        <telerik:RadDatePicker ID="rdpCreacion" Enabled="false" runat="server" Width="140px">
        </telerik:RadDatePicker>
    </div>
    <div class="ctrlBasico">
        <label id="lbVigencia"
            name="lbVigencia"
            runat="server">
            Vigencia:</label>
        <telerik:RadDatePicker ID="rdpVigencia" Enabled="false" runat="server" Width="140px">
        </telerik:RadDatePicker>
    </div>
    <div style="clear: both;"></div>
    <div style="height: calc(100% - 55px); padding-top: 10px; overflow:auto">
        <telerik:RadSplitter ID="rsConsultas" runat="server" Width="100%" Height="101%" BorderSize="0">

            <telerik:RadPane ID="rpConsultas" runat="server" Width="20px" Height="100%">
                <telerik:RadSlidingZone ID="rszConsultas" runat="server" SlideDirection="Right" Height="100%" ExpandedPaneId="rsConsultas" Width="20px" DockedPaneId="rsbConsultas">
                    <telerik:RadSlidingPane ID="rsbConsultas" runat="server"  CollapseMode="Forward" EnableResize="false" Width="365px" Title="Reporte" Height="100%">
                        <telerik:RadTabStrip ID="tbCpnsultas" runat="server" OnClientTabSelected="TabSelected" SelectedIndex="0" MultiPageID="mpgResultados" Orientation="VerticalLeft" Width="365" Font-Size="Small" Align="Left">
                            <Tabs>
                                <telerik:RadTab Text="Tabulador de sueldos" runat="server" SelectedIndex="0"></telerik:RadTab>
                                <telerik:RadTab Text="Gráfica de Análisis" runat="server" SelectedIndex="1"></telerik:RadTab>
                                <telerik:RadTab Text="Análisis de desviaciones" runat="server" SelectedIndex="2"></telerik:RadTab>

                                <telerik:RadTab Text="Tabulador maestro" runat="server" SelectedIndex="3"></telerik:RadTab>
                                <telerik:RadTab Text="Mercado salarial" runat="server" SelectedIndex="4"></telerik:RadTab>
                                <telerik:RadTab Text="Incrementos planeados" runat="server" SelectedIndex="5"></telerik:RadTab>
                                <telerik:RadTab Text="Comparación inventario de personal vs mercado salarial" runat="server" SelectedIndex="6"></telerik:RadTab>
                                 <telerik:RadTab Text="Bono" runat="server" SelectedIndex="7"></telerik:RadTab>
                                
                            </Tabs>
                        </telerik:RadTabStrip>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>

            <telerik:RadPane ID="radPanelPreguntas" runat="server">
                <%--<div style="height: calc(100% - 10px);overflow: auto;">--%>
                    <telerik:RadMultiPage ID="mpgResultados" runat="server" SelectedIndex="0" Height="100%">

                        <telerik:RadPageView ID="rpComparacionInventarioPersonal" runat="server">
                           <%-- <div style="margin-left: 10px">--%>
                            
                            <%--<div style="height: calc(100% - 60px);  overflow: auto;">--%>

                                <telerik:RadTabStrip ID="rtsTabuladorSueldos" runat="server" SelectedIndex="0" MultiPageID="rmpTabuladorSueldos">
                            <Tabs>
                                <telerik:RadTab Text="Parámetros de Análisis" SelectedIndex="0"></telerik:RadTab>
                                <telerik:RadTab Text="Definición de Criterios" ></telerik:RadTab>
                                <telerik:RadTab Text="Tabulador de sueldos" ></telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <div style="height: calc(100% - 50px);overflow: auto;">
                        <telerik:RadMultiPage ID="rmpTabuladorSueldos" runat="server" SelectedIndex="0" Height="100%">
                            
                            <telerik:RadPageView ID="rpvParametrosTabuladorSueldos" runat="server">
                                 <label class="labelTitulo">Tabulador de sueldos</label>
                                 <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <label id="Label4" name="lbRangoNivel" runat="server">Rango de nivel:</label>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadNumericTextBox runat="server" ID="rntComienzaNivel" NumberFormat-DecimalDigits="0" Name="rnComienza" Width="140px" MinValue="1" ShowSpinButtons="true" Value="1"></telerik:RadNumericTextBox>
                                        <telerik:RadNumericTextBox runat="server" ID="rntTerminaSueldo" NumberFormat-DecimalDigits="0" Name="rnTermina" Width="140px" MinValue="1" ShowSpinButtons="true" Value="100"></telerik:RadNumericTextBox>
                                    </div>  
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <label id="Label5"
                                            name="lbCuartil"
                                            runat="server">
                                            Nivel del mercado:</label>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadComboBox Filter="Contains" runat="server" ID="rcbMercadoTabuladorSueldos" Width="190" MarkFirstMatch="true" AutoPostBack="false" EmptyMessage="Seleccione..."
                                            DropDownWidth="190">
                                        </telerik:RadComboBox>
                                    </div>
                                </telerik:RadPageView>
                            <telerik:RadPageView ID="rpvCriteriosTabuladorSueldos" runat="server">
                                  <div style="height: calc(100% - 60px);overflow: auto;">
                                    <telerik:RadGrid ID="rgEmpleadosTabuladorSueldos"
                                        runat="server"
                                        AllowSorting="true"
                                        Height="100%"
                                        AutoGenerateColumns="false"
                                        HeaderStyle-Font-Bold="true"
                                        OnItemCommand="rgEmpleadosTabuladorSueldos_ItemCommand"
                                        OnNeedDataSource="rgEmpleadosTabuladorSueldos_NeedDataSource"
                                        OnItemDataBound="rgEmpleadosTabuladorSueldos_ItemDataBound">
                                        <ClientSettings>
                                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true" DataKeyNames="ID_TABULADOR_EMPLEADO">
                                            <Columns>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="210" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="210" FilterControlWidth="130" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                                    <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" ButtonType="ImageButton">
                                                         <ItemStyle Width="35" />
                                                    <HeaderStyle Width="35" />
                                                    </telerik:GridButtonColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    </div>
                                    <div style="height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnEmpleadoCriterio" runat="server" name="btnSeleccionarEmpleado" OnClientClicked="OpenTabuladorEmpleadoSueldo" AutoPostBack="false" Text="Seleccionar" Width="100"></telerik:RadButton>
                                </div>
                                </telerik:RadPageView>
                            <telerik:RadPageView ID="rpvTabuladorSueldos" runat="server">
                                 <telerik:RadGrid ID="rgdComparacionInventarioPersonal" 
                                     runat="server" Height="100%" 
                                     AutoGenerateColumns="true" 
                                     OnItemCreated="rgdComparacionInventarioPersonal_ItemCreated"
                                    OnNeedDataSource="rgdComparacionInventarioPersonal_NeedDataSource" 
                                     HeaderStyle-Font-Bold="true"   
                                     OnColumnCreated="rgdComparacionInventarioPersonal_ColumnCreated" 
                                     AllowMultiRowSelection="true" 
                                     AllowPaging="false">
                                    <ClientSettings EnablePostBackOnRowClick="false" 
                                        Scrolling-FrozenColumnsCount="3">
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                     <GroupingSettings CaseSensitive="false" />
                                     <MasterTableView ClientDataKeyNames="ID_TABULADOR_EMPLEADO, NO_NIVEL" DataKeyNames="ID_TABULADOR_EMPLEADO, NO_NIVEL" EnableColumnsViewState="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                         <ColumnGroups>
                                             <telerik:GridColumnGroup HeaderText="Tabulador medio" Name="TABMEDIO" HeaderStyle-HorizontalAlign="Center">
                                             </telerik:GridColumnGroup>
                                         </ColumnGroups>
                                         <Columns>
                                         </Columns>
                                     </MasterTableView>
                                </telerik:RadGrid>
                                </telerik:RadPageView>

                        </telerik:RadMultiPage>
                            <%--    <telerik:RadGrid ID="rgdComparacionInventarioPersonal"
                                    runat="server"
                                    AllowSorting="true"
                                    Width="100%"
                                    Height="100%"
                                    AutoGenerateColumns="false"
                                    OnItemDataBound="rgdComparacionInventarioPersonal_ItemDataBound"
                                    OnNeedDataSource="rgdComparacionInventarioPersonal_NeedDataSource">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="true" DataKeyNames="ID_TABULADOR_EMPLEADO"  AllowPaging ="true" ShowHeadersWhenNoRecords="true">
                                   <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldAlias="Nivel" FieldName="NB_TABULADOR_NIVEL"  
                                              ></telerik:GridGroupByField>
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="NB_TABULADOR_NIVEL" SortOrder="Ascending" ></telerik:GridGroupByField>
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Nivel" DataField="NB_TABULADOR_NIVEL"></telerik:GridBoundColumn>--%>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Cl. Puesto" DataField="CL_PUESTO"></telerik:GridBoundColumn>--%>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Puesto" DataField="NB_PUESTO"></telerik:GridBoundColumn>--%>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Cl. Empleado" DataField="CL_EMPLEADO"></telerik:GridBoundColumn>--%>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Nombre del ocupante" DataField="NB_EMPLEADO"></telerik:GridBoundColumn>--%>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Sueldo nominal actual" DataField="MN_SUELDO_ORIGINAL" ReadOnly="true" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>--%>
                                            
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Mínimo" DataField="MN_MINIMO_CUARTIL" ReadOnly="true" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>--%>
                                            <%--<telerik:GridTemplateColumn AutoPostBackOnFilter="true" ReadOnly="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Mínimo" DataField="MN_MINIMO_CUARTIL" UniqueName="MN_MINIMO_CUARTIL"  ItemStyle-HorizontalAlign="Right">
                                               <ItemTemplate>
                                                <label  id="gbcMinimo" runat="server"> 
                                                      <%#string.Format("{0:C}",Math.Abs((decimal)Eval("MN_MINIMO_CUARTIL")))%>
                                                </label>
                                               </ItemTemplate>
                                            </telerik:GridTemplateColumn>--%>
                                            
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Máximo" DataField="MN_MAXIMO_CUARTIL" ReadOnly="true" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>--%>
                                          <%-- <telerik:GridTemplateColumn AutoPostBackOnFilter="true" ReadOnly="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Máximo" DataField="MN_MAXIMO_CUARTIL" UniqueName="MN_MAXIMO_CUARTIL" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            <label  id="gbcMaximo" runat="server"> 
                                                  <%#string.Format("{0:C}",Math.Abs((decimal)Eval("MN_MAXIMO_CUARTIL")))%>
                                            </label>
                                            </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                             <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false" ReadOnly="true" HeaderStyle-Width="150" HeaderText="Diferencia" DataField="DIFERENCIA" UniqueName="DIFERENCIA" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%# string.Format("{0:C}", Math.Abs((decimal) Eval("DIFERENCIA")))%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false" ReadOnly="true" HeaderStyle-Width="150" HeaderText="Porcentaje" DataField="PR_DIFERENCIA" UniqueName="PR_DIFERENCIA" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%#string.Format("{0:N2}", Math.Abs((decimal)Eval("PR_DIFERENCIA")))%>%
                                        <span style="border: 1px solid gray; background: <%# Eval("COLOR_DIFERENCIA") %>; border-radius: 5px;">&nbsp;&nbsp;</span>&nbsp;
                                        <img src='/Assets/images/Icons/25/Arrow<%# Eval("ICONO")%>.png' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>--%>
                            </div>

                            <%-- <telerik:RadToolTipManager ID="rttmSecuancias"
                                  runat="server" 
                                  Position="MiddleRight"
                                  CssClass="RadToolTip_MC"
                                 ShowDelay="1000"  
                                 ShowEvent="OnMouseOver" 
                                 OnAjaxUpdate ="rttmSecuancias_AjaxUpdate"
                                 HideEvent="LeaveTargetAndToolTip"
                                  RelativeTo="Element" 
                                 Animation="Fade"
                                >
                                </telerik:RadToolTipManager>--%>

                             <%--OnAjaxUpdate="rttmSecuancias_AjaxUpdate"--%>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="rpAnalisisDesviasiones" runat="server">
                            <telerik:RadTabStrip ID="rtsAnalisisDesviasiones" runat="server" SelectedIndex="0" MultiPageID="rmpAnalisisDesviasiones">
                                <Tabs>
                                 
                                    <telerik:RadTab Text="Parámetros de Análisis" SelectedIndex="0"></telerik:RadTab>
                                    <telerik:RadTab Text="Definición de Criterios (Dispersión de Sueldos en Versión)" SelectedIndex="1"></telerik:RadTab>
                                    <telerik:RadTab Text="Gráfica de Análisis" SelectedIndex="2"></telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                           <div style="height: calc(100% - 80px);overflow: auto;">
                            <telerik:RadMultiPage ID="rmpAnalisisDesviasiones" runat="server" SelectedIndex="0" Height="100%">
                                <telerik:RadPageView ID="rpvParametrosAnalisis" runat="server">
                                    <label class="labelTitulo">Gráfica de Análisis</label>
                                    <div class="ctrlBasico">
                                        <label id="lbCuartilComparacion"
                                            name="lbCuartil"
                                            runat="server">
                                            1. Elige el nivel de mercado con el que quieres comparar tus sueldos:</label>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbCuartilComparacion" Width="190" MarkFirstMatch="true" AutoPostBack="false" EmptyMessage="Seleccione..."
                                            DropDownWidth="190">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <label id="lbSeleccionTabuladoreas" name="lbSeleccionTabuladoreas" runat="server">2. Elige la versión(es) que deseas consultar (max 3):</label>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadListBox ID="lstTabuladores" Width="300" Height="60px" runat="server" ValidationGroup="vgTabulador"></telerik:RadListBox>
                                        <telerik:RadButton ID="btnAgregarTabuladores" runat="server" Text="+" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" ValidationGroup="vgTabulador"></telerik:RadButton>
                                        <telerik:RadButton ID="btnEliminarTabulador" runat="server" Text="x" AutoPostBack="false" ValidationGroup="vgTabulador" OnClientClicked="DeleteTabulador"></telerik:RadButton>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <label id="Label1" name="lbRangoNivel" runat="server">3. Elige el rango de nivel que deseas consultar:</label>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadNumericTextBox runat="server" ID="rnComienza" NumberFormat-DecimalDigits="0" Name="rnComienza" Width="140px" MinValue="1" ShowSpinButtons="true" Value="1"></telerik:RadNumericTextBox>
                                        <telerik:RadNumericTextBox runat="server" ID="rnTermina" NumberFormat-DecimalDigits="0" Name="rnTermina" Width="140px" MinValue="1" ShowSpinButtons="true" Value="100"></telerik:RadNumericTextBox>
                                    </div>
                                    <%--</div>--%>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="rpvDefinicionCriterios" runat="server" Height="100%">
                                    <div style="height: 10px;"></div>
                                     <div style="height: calc(100% - 70px);overflow: auto;">
                                        <telerik:RadGrid ID="rgdEmpleados"
                                            runat="server"
                                            AllowSorting="true"
                                            Height="100%"
                                            Width="100%"
                                            AutoGenerateColumns="false"
                                            HeaderStyle-Font-Bold="true"
                                            OnItemCommand ="rgdEmpleados_ItemCommand"
                                            OnNeedDataSource="rgdEmpleados_NeedDataSource"
                                            OnItemDataBound="rgdEmpleados_ItemDataBound">
                                            <ClientSettings>
                                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                            <PagerStyle AlwaysVisible="true" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true" DataKeyNames="ID_TABULADOR_EMPLEADO,ID_EMPLEADO">
                                                <Columns>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="210" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="210" FilterControlWidth="130" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                                    <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" ButtonType="ImageButton">
                                                    <ItemStyle Width="35" />
                                                    <HeaderStyle Width="35" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                         </div>
                                        <div style="height: 10px;"></div>
                                        <div class="ctrlBasico">
                                            <telerik:RadButton ID="btnSeleccionarEmpleado" runat="server" name="btnSeleccionarEmpleado" OnClientClicked="OpenSelectionEmployeeWindow" AutoPostBack="false" Text="Seleccionar" Width="100"></telerik:RadButton>
                                            <%--<telerik:RadButton ID="btnReporte" runat="server" name="btnReporte" OnClientClicked="OnClientEmpleados" OnClick="btnReporte_Click"     AutoPostBack="true" Text="Reporte" Width="100"></telerik:RadButton>--%>
                                        </div>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="rpvGraficaAnalisis" runat="server">
                                 <%--   <div style="clear: both; height: 10px"></div>--%>
                                     <div style="height: calc(100% - 90px);overflow: auto;">
                                    <telerik:RadHtmlChart runat="server" Height="100%" ID="ScatterChartGraficaAnalisis" Width="100%">
                                            <ChartTitle Text="Sueldos de la versión por niveles">
                                                <Appearance Align="Center" Position="Top" BackgroundColor="Transparent">
                                                </Appearance>
                                            </ChartTitle>
                                            <Legend>
                                                <Appearance Position="Bottom" BackgroundColor="Transparent">
                                                </Appearance>
                                            </Legend>
                                            <PlotArea>
                                                <Series>
                                                    <telerik:ScatterSeries Visible="true">
                                                        <MarkersAppearance MarkersType="Circle" Size="8" BorderWidth="2"></MarkersAppearance>
                                                    </telerik:ScatterSeries>
                                                </Series>
                                            </PlotArea>
                                            <Legend>
                                                <Appearance Position="Right">
                                                </Appearance>
                                            </Legend>
                                        </telerik:RadHtmlChart>
                                         </div>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                               </div>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="rpGraficaAnalisisMercado" runat="server">

                        <telerik:RadTabStrip ID="rtGraficaAnalisisMercado" runat="server" SelectedIndex="0" MultiPageID="rmpGraficaAnalisisMercado">
                            <Tabs>
                                <telerik:RadTab Text="Parámetros de Análisis" SelectedIndex="0"></telerik:RadTab>
                                <telerik:RadTab Text="Definición de Criterios" ></telerik:RadTab>
                                <telerik:RadTab Text="Análisis de Desviaciones"></telerik:RadTab>
                                <telerik:RadTab Text="Gráfica de Desviaciones" ></telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <div style="height: calc(100% - 42px);overflow: auto;">
                        <telerik:RadMultiPage ID="rmpGraficaAnalisisMercado" runat="server" SelectedIndex="0" Height="100%">
                            <telerik:RadPageView ID="rpvParametrosAnalisisGM" runat="server">
                                <label class="labelTitulo">Análisis de desviciones</label>
                                 <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <label id="Label3" name="lbRangoNivel" runat="server">Rango de nivel:</label>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadNumericTextBox runat="server" ID="txtComienza" NumberFormat-DecimalDigits="0" Name="rnComienza" Width="140px" MinValue="1" ShowSpinButtons="true" Value="1"></telerik:RadNumericTextBox>
                                        <telerik:RadNumericTextBox runat="server" ID="txtTermina" NumberFormat-DecimalDigits="0" Name="rnTermina" Width="140px" MinValue="1" ShowSpinButtons="true" Value="100"></telerik:RadNumericTextBox>
                                    </div>  
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <label id="Label2"
                                            name="lbCuartil"
                                            runat="server">
                                            Nivel del mercado:</label>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadComboBox Filter="Contains" runat="server" ID="rcbNivelMercado" Width="190" MarkFirstMatch="true" AutoPostBack="true" EmptyMessage="Seleccione..."
                                            DropDownWidth="190"  OnSelectedIndexChanged="rcbNivelMercado_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </div>
                                

                                </telerik:RadPageView>
                            <telerik:RadPageView ID="rpvDefinicionCriteriosMercado" runat="server">
                                    
                                <div style="height: 10px;"></div>
                                <div style="height: calc(100% - 65px);overflow: auto;">
                                    <telerik:RadGrid ID="rgEmpleadosDesviasion"
                                        runat="server"
                                        AllowSorting="true"
                                        Height="100%"
                                        AutoGenerateColumns="false"
                                        HeaderStyle-Font-Bold="true"
                                        OnItemCommand="rgEmpleadosDesviasion_ItemCommand"
                                        OnNeedDataSource="rgEmpleadosDesviasion_NeedDataSource"
                                        OnItemDataBound="rgEmpleadosDesviasion_ItemDataBound">
                                        <ClientSettings>
                                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true" DataKeyNames="ID_TABULADOR_EMPLEADO">
                                            <Columns>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="210" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="210" FilterControlWidth="130" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                                    <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" ButtonType="ImageButton">
                                                         <ItemStyle Width="35" />
                                                    <HeaderStyle Width="35" />
                                                    </telerik:GridButtonColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    </div>
                                    <div style="height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnSeleccionEnpleadosDes" runat="server" name="btnSeleccionarEmpleado" OnClientClicked="OpenSelectionTabuladorEmployee" AutoPostBack="false" Text="Seleccionar" Width="100"></telerik:RadButton>
                                </div>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="rpvAnalisisDesviaciones" runat="server">

                                <div style="height: 10px;"></div>
                                <div style="height: calc(100% - 65px);overflow: auto;">
                                    <telerik:RadGrid ID="rgAnalisisDesviaciones"
                                        runat="server"
                                        AllowSorting="true"
                                        Height="100%"
                                        AutoGenerateColumns="false"
                                        HeaderStyle-Font-Bold="true"
                                        OnSelectedIndexChanged="rgAnalisisDesviaciones_SelectedIndexChanged"
                                        OnItemDataBound="rgAnalisisDesviaciones_ItemDataBound">
                                        <ClientSettings EnablePostBackOnRowClick="true">
                                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <PagerStyle AlwaysVisible="true" />
                                        <HeaderStyle />
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="false" AllowPaging="true" ShowHeadersWhenNoRecords="true" DataKeyNames="ID_TABULADOR_NIVEL" ClientDataKeyNames="ID_TABULADOR_NIVEL">
                                            <Columns>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="false"  CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Nivel" DataField="NB_TABULADOR_NIVEL" UniqueName="NB_TABULADOR_NIVEL"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false" HeaderStyle-Width="150" FilterControlWidth="70" HeaderImageUrl="/Assets/images/Icons/25/ArrowEqual.png" DataField="PR_VERDE" UniqueName="PR_VERDE">
                                                    <HeaderTemplate>
                                                        <span id="Span1" runat="server"  style="width:40px;border: 1px solid gray; background-color: green; border-radius: 5px; ">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                                    </HeaderTemplate>
                                                    <ItemTemplate><%# string.Format("{0:N2}", Eval("PR_VERDE"))%>% </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false"  HeaderStyle-Width="150" FilterControlWidth="70" HeaderImageUrl="/Assets/images/Icons/25/ArrowUp.png" DataField="PR_AMARILLO_POS" UniqueName="PR_AMARILLO_POS">
                                                    <HeaderTemplate>
                                                        <span id="Span2" runat="server" style="width:40px; height:50px; border: 1px solid gray; background-color: yellow; border-radius: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                                    </HeaderTemplate>
                                                    <ItemTemplate><%#  string.Format("{0:N2}", Eval("PR_AMARILLO_POS"))%>%</ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false"  HeaderStyle-Width="150" FilterControlWidth="70" HeaderImageUrl="/Assets/images/Icons/25/ArrowDown.png" DataField="PR_AMARILLO_NEG" UniqueName="PR_AMARILLO_NEG">
                                                    <HeaderTemplate>
                                                        <span id="Span3" runat="server" style="width:40px; border: 1px solid gray; background-color: #ffd700; border-radius: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                                    </HeaderTemplate>
                                                    <ItemTemplate><%#  string.Format("{0:N2}", Eval("PR_AMARILLO_NEG"))%>%</ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false"  HeaderStyle-Width="150" FilterControlWidth="70" HeaderImageUrl="/Assets/images/Icons/25/ArrowUp.png" DataField="PR_ROJO_POS" UniqueName="PR_ROJO_POS">
                                                    <HeaderTemplate>
                                                        <span id="Span4" runat="server" style="width:40px; border: 1px solid gray; background-color: #ff4500; border-radius: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                                    </HeaderTemplate>
                                                    <ItemTemplate><%#  string.Format("{0:N2}", Eval("PR_ROJO_POS"))%>%</ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AutoPostBackOnFilter="false" AllowFiltering="false"  HeaderStyle-Width="150" FilterControlWidth="70" HeaderImageUrl="/Assets/images/Icons/25/ArrowDown.png" DataField="PR_ROJO_NEG" UniqueName="PR_ROJO_NEG">
                                                    <HeaderTemplate>
                                                        <span id="Span5" runat="server" style="width:40px; border: 1px solid gray; background-color: red; border-radius: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
                                                    </HeaderTemplate>
                                                    <ItemTemplate><%#  string.Format("{0:N2}", Eval("PR_ROJO_NEG"))%>%</ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    </div>
                                    <div style="height: 10px;"></div>
                                    <div class="ctrlBasico">
                                        <telerik:RadButton ID="btnGrafica" runat="server" name="btnGrafica" OnClientClicked="OnClientClicked" AutoPostBack="false" Text="Gráfica" Width="100"></telerik:RadButton>
                                </div>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="rpvGraficaDesviaciones" runat="server">
                                 <div style="height: calc(100% - 90px);overflow: auto;">
                                    <telerik:RadHtmlChart runat="server" ID="PieChartGraficaDesviaciones" Width="100%" Height="100%" Transitions="true" Skin="Silk">
                                        <ChartTitle Text="Gráfica de Desviaciones">
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
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                                  </div>
                        </telerik:RadPageView>



                        <telerik:RadPageView ID="rpTabuladorMaestro" runat="server" Height="100%">
                              <label class="labelTitulo">Tabulador maestro</label>
                            <div style="height: calc(100% - 60px);  overflow: auto;">
                                <telerik:RadGrid ID="rgdTabuladorMaestro"
                                    runat="server"
                                    Width="100%"
                                    Height="100%"
                                    AllowSorting="true"
                                    ShowHeader="true"
                                    HeaderStyle-Font-Bold="true"
                                    AutoGenerateColumns="false"
                                    OnNeedDataSource="rgdTabuladorMaestro_NeedDataSource"
                                    OnItemDataBound="rgdTabuladorMaestro_ItemDataBound">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Nivel" DataField="NB_TABULADOR_NIVEL"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Categoría" DataField="NB_CATEGORIA"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="50" HeaderText="Mínimo" DataField="MN_MINIMO" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ReadOnly="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="50" HeaderText="Primer Cuartil" DataField="MN_PRIMER_CUARTIL" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ReadOnly="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="50" HeaderText="Medio" DataField="MN_MEDIO" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ReadOnly="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="50" HeaderText="Segundo Cuartil" DataField="MN_SEGUNDO_CUARTIL" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ReadOnly="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="50" HeaderText="Máximo" DataField="MN_MAXIMO" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ReadOnly="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="50" HeaderText="Siguiente año" DataField="MN_SIGUIENTE" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ReadOnly="true"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="rpMercadoSalarial" runat="server">
                            <%--<div style="margin-left: 10px">--%>
                             <label class="labelTitulo">Mercado salarial</label>
                            <div style="height: calc(100% - 60px);  overflow: auto;">
                              <telerik:RadGrid ID="rgdMercadoSalarial"
                                    runat="server"
                                    Width="100%"
                                    Height="100%"
                                    AllowSorting="true"
                                    AutoGenerateColumns="false"
                                  HeaderStyle-Font-Bold="true"
                                  OnNeedDataSource="rgdMercadoSalarial_NeedDataSource"
                                  OnItemDataBound="rgdMercadoSalarial_ItemDataBound">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" DataFormatString="{0:C}" ReadOnly="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="260" FilterControlWidth="240" HeaderText="Puesto" DataField="NB_PUESTO" DataFormatString="{0:C}" ReadOnly="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Mínimo" DataField="MN_MINIMO" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ReadOnly="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Máximo" DataField="MN_MAXIMO" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ReadOnly="true"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="rpIncrementosPlaneados" runat="server">
                         <%--   <div style="margin-left: 10px">--%>
                             <label class="labelTitulo">Incrementos planeados</label>
                            <div style="height: calc(100% - 70px);  overflow: auto;">

                                <telerik:RadGrid ID="rgdIncrementosPlaneados"
                                    runat="server"
                                    Width="100%"
                                    Height="100%"
                                    AllowSorting="true"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-Font-Bold="true"
                                    OnNeedDataSource="rgdIncrementosPlaneados_NeedDataSource">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Nivel" DataField="NB_TABULADOR_NIVEL"></telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Cl. Puesto" DataField="CL_PUESTO"></telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Puesto" DataField="NB_PUESTO"></telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Cl. Empleado" DataField="CL_EMPLEADO"></telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Nombre completo" DataField="NB_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Sueldo nominal actual" DataField="MN_SUELDO_ORIGINAL" ReadOnly="true" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Sueldo nominal nuevo" DataField="MN_SUELDO_NUEVO" ReadOnly="true" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Incremento" DataField="INCREMENTO" ReadOnly="true" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Porcentaje" DataField="PR_INCREMENTO" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="rpComparacionInventarioMercado" runat="server">
                          <%--  <div style="margin-left: 10px">--%>
                             <label class="labelTitulo">Comparación inventario de personal vs mercado salarial</label>
                            <div style="height: calc(100% - 60px);  overflow: auto;">

                                <telerik:RadGrid ID="rgdComparacionInventarioMercado"
                                    runat="server"
                                    Width="100%"
                                    Height="100%"
                                    AllowSorting="true"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-Font-Bold="true"
                                    OnNeedDataSource="rgdComparacionInventarioMercado_NeedDataSource">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Nivel" DataField="NB_TABULADOR_NIVEL"></telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Cl. Puesto" DataField="CL_PUESTO"></telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Puesto" DataField="NB_PUESTO"></telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Cl. Empleado" DataField="CL_EMPLEADO"></telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Nombre completo" DataField="NB_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Sueldo nominal actual" DataField="MN_SUELDO_ORIGINAL" ReadOnly="true" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Mínimo" DataField="MN_MINIMO" DataFormatString="{0:C}" ReadOnly="true" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Máximo" DataField="MN_MAXIMO" DataFormatString="{0:C}" ReadOnly="true" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn AutoPostBackOnFilter="true" ReadOnly="true" AllowFiltering="false" HeaderStyle-Width="150" FilterControlWidth="60" HeaderText="Diferencia" DataField="DIFERENCIA_MERCADO" UniqueName="DIFERENCIA_MERCADO" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%# string.Format("{0:C}", Math.Abs((decimal) Eval("DIFERENCIA_MERCADO")))%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AutoPostBackOnFilter="true" ReadOnly="true" AllowFiltering="false" HeaderStyle-Width="150" FilterControlWidth="60" HeaderText="Porcentaje" DataField="PR_DIFERENCIA_MERCADO" UniqueName="PR_DIFERENCIA_MERCADO" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%#string.Format("{0:N2}" , Math.Abs((decimal) Eval("PR_DIFERENCIA_MERCADO")))%>%
                                        <span style="border: 1px solid gray; background: <%# Eval("COLOR_DIFERENCIA_MERCADO") %>; border-radius: 5px;">&nbsp;&nbsp;</span>&nbsp;
                                        <img src='/Assets/images/Icons/25/Arrow<%# Eval("ICONO_MERCADO")%>.png' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="rpvBono" runat="server">
                             <label class="labelTitulo">Bono</label>
                            <div style="height: calc(100% - 60px);  overflow: auto;">

                                      <telerik:RadGrid
                    ID="rgComparativos"
                    runat="server"
                    Height="90%"
                    AutoGenerateColumns="false"
                    EnableHeaderContextMenu="true"
                    AllowSorting="true"
                    AllowMultiRowSelection="false" HeaderStyle-Font-Bold="true"
                    OnNeedDataSource="rgComparativos_NeedDataSource"
                    OnDeleteCommand="rgComparativos_DeleteCommand">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="idPeriodo" ClientDataKeyNames="idPeriodo" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="40" HeaderText="Clave" DataField="clPeriodo" UniqueName="clPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Período" DataField="nbPeriodo" UniqueName="nbPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Descripción" DataField="dsPeriodo" UniqueName="dsPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" />
                            
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="height: 10px; clear: both;"></div>
                <div class="divControlIzquierda">
                    <telerik:RadButton ID="btnSeleccionar" runat="server" AutoPostBack="false" Width="200" Text="Seleccionar períodos" OnClientClicked="OpenWindowPeriodos"></telerik:RadButton>
                </div>
                <div class="divControlDerecha">
                    <telerik:RadButton ID="btnComparar" runat="server" AutoPostBack="false" Width="100" Text="Comparar" OnClientClicked="OpenWindowComparar"></telerik:RadButton>
                </div>
                                </div>
                             </telerik:RadPageView>
                    </telerik:RadMultiPage>
               <%-- </div>--%>
            </telerik:RadPane>

            <telerik:RadPane ID="rpAyuda" runat="server" Width="20px" Height="100%">
                <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" Height="100%" ExpandedPaneId="rsConsultas" Width="20px" DockedPaneId="rsbConsultas">
                    <telerik:RadSlidingPane ID="rsbAyuda" runat="server" CollapseMode="Forward" EnableResize="false" Width="325px" Title="Ayuda" Height="100%">

                        <div id="divTabuladorMaestro" runat="server" style="display: none">
                            <p>
                                Tabulador Maestro.																	
                            </p>
                        </div>

                        <div id="divMercadoSalarial" runat="server" style="display: none">
                            <p>
                                Mercado salarial.																
                            </p>
                        </div>

                        <div id="divIncrementosPlaneados" runat="server" style="display: none">
                            <p>
                                Incrementos Planeados.														
                            </p>
                        </div>

                        <div id="divComparacionTabulador" runat="server" style="display: none">
                            <p>
                                Comparación Inventario de Personal vs Tabulador.														
                            </p>
                        </div>

                        <div id="divComparacionMercado" runat="server" style="display: none">
                            <p>
                                Comparación Inventario de Personal vs Mercado salarial.														
                            </p>
                        </div>

                        <div id="divAyudaGraficaAnalisis" runat="server" style="display: none">
                            <p>
                                Esta consulta te dará resultados gráficos según los datos que elijas a 																			
                               continuación.																			
                               Esta información te servirá para hacer comparaciones y analizar tu																			
                               tabulador de manera más efectiva. Si eliges más de una versión para																			
                               compararse entre sí, no podrás comparar con ningún nivel de mercado.																			
                            </p>
                        </div>

                        <div id="divAyudaAnalisisDesviaciones" runat="server" style="display: none">
                            <p>
                                Esta hoja permite definir tu búsqueda para el reporte solicitado.																	
                                Con excepción de los parámetros de períodos que son mandatorios,																	
                                dejando en blanco los criterios solicitados indicas que dicho criterio																	
                                es irrelevante y el sistema no lo tomará en cuenta para filtrar los datos.																	
                                En caso de ingresar criterios de búsqueda, éstos serán utilizados para 																	
                                acortar el reporte.																	
                                Nota: en algunos casos las opciones pueden ser mutuamente exclusivas.																	
																			
                            </p>
                        </div>

                    </telerik:RadSlidingPane>


                     <telerik:RadSlidingPane ID="rspSemaforo" runat="server" CollapseMode="Forward" EnableResize="false" Width="450px" Title="Semáforo" Height="100%">
                          <div style="padding: 10px; text-align: justify;">
                      <telerik:RadGrid ID="grdCodigoColores"
                        runat="server"
                        Height="215"
                        Width="400"
                        AllowSorting="true"
                        AllowFilteringByColumn="true"
                          HeaderStyle-Font-Bold="true"
                        ShowHeader="true"
                         OnNeedDataSource="grdCodigoColores_NeedDataSource"
                           >
                         <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView  AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
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
</asp:Content>