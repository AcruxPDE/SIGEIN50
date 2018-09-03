<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="ConsultaGraficaAnalisis.aspx.cs" Inherits="SIGE.WebApp.MPC.ConsultaGraficaAnalisis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OpenSelectionEmployeeWindow() {
            prueba();
            if (arrIdTabuladores != "") {
                var myUrl = '<%= ResolveClientUrl("../Comunes/SeleccionEmpleado.aspx") %>';
                openChildDialog(myUrl + "?&IdTabuladores=" + arrIdTabuladores + "&vClTipoSeleccion=MC_TABULADORES", "winSeleccion", "Selección de  empleados");
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
        }

        function OpenSelectionWindow() {
            var myUrl = '<%= ResolveClientUrl("SeleccionTabulador.aspx") %>';
            openChildDialog(myUrl, "winSeleccion", "Selección de tabuladores");
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


        function OpenSelectionTabuladorEmployee() {
            var myUrl = '<%= ResolveClientUrl("SeleccionTabuladorEmpleado.aspx") %>';
            openChildDialog(myUrl + "?&IdTabulador=" + <%=vIdTabulador%> + "&vClTipoSeleccion=CONSULTAS", "winSeleccion", "Selección de empleados");
            }

        function OpenTabuladorEmpleadoSueldo() {
            var myUrl = '<%= ResolveClientUrl("SeleccionTabuladorEmpleado.aspx") %>';
            openChildDialog(myUrl+"?&IdTabulador=" + <%=vIdTabulador%> + "&vClTipoSeleccion=CONSULTAS" + "&CatalogoCl=TABULADOR_SUELDOS", "winSeleccion", "Selección de empleados");
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

        function OpenSelectionWindow() {
            var myUrl = '<%= ResolveClientUrl("SeleccionTabulador.aspx") %>';
            openChildDialog(myUrl, "winSeleccion", "Selección de tabuladores");
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

                //function OpenImprimir() {
                //    var myPageView = $find('<= rmpTabuladorSueldos.ClientID %>');
                //    var myIframe = document.getElementById('ifrmPrint');
                //    var pvContent = myPageView.get_selectedPageView().get_element().innerHTML;
                //    var myDoc = (myIframe.contentWindow || myIframe.contentDocument);
                //    if (myDoc.document) myDoc = myDoc.document;
                //    myDoc.write("<html><head><title>title</title>");
                //    myDoc.write("</head><body onload='this.focus(); this.print();'>");
                //    myDoc.write(pvContent + "</body></html>");
                //    myDoc.close();
                //}

                function OpenImprimirReporte() {
                    var pIdTabulador = '<%= vIdTabulador %>';
                    var pNivelMercado = '<%= vCuartilComparativo %>';
                openChildDialog("ReporteGraficaAnalisis.aspx?ID=" + pIdTabulador + "&pNivelMercado=" + pNivelMercado, "winImprimir", "Imprimir consulta");
            }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConsultas" runat="server" OnAjaxRequest="ramConsultas_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rmpTabuladorSueldos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rmpTabuladorSueldos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip ID="rtsTabuladorSueldos" runat="server" SelectedIndex="0" MultiPageID="rmpTabuladorSueldos">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Parámetros de análisis"></telerik:RadTab>
            <telerik:RadTab Text="Definición de criterios"></telerik:RadTab>
            <telerik:RadTab Text="Gráfica de análisis"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 50px); overflow: auto;">
        <telerik:RadSplitter ID="rsConsultas" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpConsultas" runat="server" Width="100%" Height="100%">
                <telerik:RadMultiPage ID="rmpTabuladorSueldos" runat="server" SelectedIndex="0" Height="100%">
                    <telerik:RadPageView ID="rpvContexto" runat="server">
                        <telerik:RadToolTip ID="rttVerde" runat="server" ShowDelay="500" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
                            TargetControlID="Span1" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_IP">
                            <span style="font-weight: bold">Sueldo dentro del nivel establecido por el tabulador (variación inferior al 10%).</span>
                        </telerik:RadToolTip>
                        <div style="height: 10px;"></div>
                        <div class="ctrlBasico">
                            <table class="ctrlTableForm">
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Versión:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtClaveTabulador" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Descripción:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtNbTabulador" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Notas:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtDescripción" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Fecha:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtFecha" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Vigencia:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtVigencia" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Tipo de puestos:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtPuestos" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvParametrosAnalisis" runat="server">
                        <div style="height: 15px; clear: both;"></div>
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
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvDefinicionCriterios" runat="server" Height="100%">
                        <div style="height: 10px;"></div>
                        <div style="height: calc(100% - 70px); overflow: auto;">
                            <telerik:RadGrid ID="rgdEmpleados"
                                runat="server"
                                AllowSorting="true"
                                Height="100%"
                                Width="100%"
                                AutoGenerateColumns="false"
                                HeaderStyle-Font-Bold="true"
                                OnItemCommand="rgdEmpleados_ItemCommand"
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
                            <telerik:RadButton ID="btnSeleccionarEmpleado" runat="server" name="btnSeleccionarEmpleado" OnClientClicked="OpenSelectionEmployeeWindow" AutoPostBack="false" Text="Seleccionar mediante filtros"></telerik:RadButton>
                        </div>
                        <%-- <div class="ctrlBasico">
                            <telerik:RadButton ID="btnSeleccionarTodos" runat="server" name="btnSeleccionarTodos" OnClick="btnSeleccionarTodos_Click" AutoPostBack="true" Text="Seleccionar todos"></telerik:RadButton>
                        </div>--%>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvGraficaAnalisis" runat="server">
                        <div style="height: calc(100% - 60px); overflow: auto;">
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
                        <div style="height: 10px;"></div>
                        <div class="ctrlBasico">
                            <telerik:RadButton ID="btnImprimir" runat="server" AutoPostBack="false" Text="Imprimir" OnClientClicked="OpenImprimirReporte"></telerik:RadButton>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Width="20px" Height="90%">
                <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" Height="100%" ExpandedPaneId="rsConsultas" Width="20px" DockedPaneId="rsbConsultas">
                    <telerik:RadSlidingPane ID="rsbAyuda" runat="server" CollapseMode="Forward" EnableResize="false" Width="325px" Title="Ayuda" Height="100%">
                        <div id="divTabuladorMaestro" runat="server">
                            <p>
                                Esta consulta te dará resultados gráficos según los datos que elijas a 																			
                             continuación.																			
                             Esta información te servirá para hacer comparaciones y analizar tu																			
                             tabulador de manera más efectiva. Si eliges más de una versión para																			
                             compararse entre sí, no podrás comparar con ningún nivel de mercado.																		
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <%--<iframe src="#" style="width: 0; height: 0; border: none" id="ifrmPrint"></iframe>--%>
</asp:Content>
