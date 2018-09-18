<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="ValuacionPuestosTabuladores.aspx.cs" Inherits="SIGE.WebApp.MPC.ValuacionPuestosTabuladores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

           <%--- function OpenWindow() {
                openChildDialog("VentanaVistaNivelacion.aspx?&ID=" + <%=vIdTabulador%> + "", "winSeleccion", "Vista previa de los niveles y categorías asignados")
           } --%>

            function closeWindow() {
                GetRadWindow().close();
            }

            function OnCloseWindow() {
                $find("<%=grdValuacion.ClientID%>").get_masterTableView().rebind();
            }

            function GuardarWindow(arg) {
                if (arg) {
                    var pDatos = "GUARDAR";
                    InsertDato(pDatos);
                }
            }

            function ValoresNiveles(pValor) {
                var vIdFactor = pValor;
                var ajaxManager = $find('<%= ramGuardar.ClientID%>');
                ajaxManager.ajaxRequest(vIdFactor);

            }

            function InsertDato(pDato) {
                var ajaxManager = $find('<%= ramGuardar.ClientID%>');
                ajaxManager.ajaxRequest(pDato);
            }

            function confirmarActualizar(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('Para continuar con el proceso guarde los cambios realizados, ¿Estás seguro que deseas continuar?', callBackFunction, 400, 170, null, "Aviso");
                args.set_cancel(true);
            }

            function confirmarGuaradar(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('Este proceso imparactará en los niveles de las competencias establecidos en el descriptivo de puesto, ¿Estás seguro que deseas continuar?', callBackFunction, 400, 170, null, "Aviso");
                args.set_cancel(true);
            }

            function onRequestStart(sender, args) {
                if ($find("<%=btnExcel.ClientID %>")._uniqueID == sender.__EVENTTARGET )
                            args.set_enableAjax(false);
                    }


                    function onResponseEnd(sender, args) {
                        if ($find("<%=btnExcel.ClientID %>")._uniqueID == sender.__EVENTTARGET )
                             args.set_enableAjax(true);
        }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramGuardar" runat="server" OnAjaxRequest="ramGuardar_AjaxRequest">
        <ClientEvents OnRequestStart="onRequestStart" OnResponseEnd="onResponseEnd" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdValuacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardarCerrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdValuacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdValuacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdValuacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadValuacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblCompetencia" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblTitulo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip ID="rtsTabuladorSueldos" runat="server" SelectedIndex="0" MultiPageID="rmpTabuladorSueldos">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Valuación puestos"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpTabuladorSueldos" runat="server" SelectedIndex="0" Height="90%">
        <telerik:RadPageView ID="rpvContexto" runat="server" Height="100%">
            <telerik:RadSplitter ID="rspContexto" Width="100%" Height="100%" BorderSize="0" runat="server">
                <telerik:RadPane ID="rpContexto" runat="server">
                    <div style="height: 10px;"></div>
                    <div class="ctrlBasico">
                        <table class="ctrlTableForm">
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Versión:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtClTabulador" runat="server" style="min-width: 100px;"></div>
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
                </telerik:RadPane>
                <telerik:RadPane ID="RadPane1" runat="server" Scrolling="None" Width="20px">
                    <telerik:RadSlidingZone ID="RadSlidingZone1" runat="server" SlideDirection="Left" ExpandedPaneId="rsValuacionPuestos" Width="20px" ClickToOpen="true">
                        <telerik:RadSlidingPane ID="RadSlidingPane1" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="100%">
                            <div style="padding: 10px; text-align: justify;">
                                <p>
                                    Valúa conforme a la siguiente escala los puestos que deseas Tabular.
                                        <br />
                                    Se recomienda que esta valuación se realice a través de un comité de expertos y de un moderador que actúe como un juez imparcial en caso de algún debate.
                                        <br />
                                    Recuerda que los valores que otorgues en este proceso serán los que definan los niveles de sueldo en tu organización.
                                </p>
                            </div>
                        </telerik:RadSlidingPane>
                    </telerik:RadSlidingZone>
                </telerik:RadPane>
            </telerik:RadSplitter>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvValuacionPuestos" runat="server">
            <div style="height: calc(100% - 40px);">
                <telerik:RadSplitter ID="rsValuacionPuestos" Width="100%" Height="95%" BorderSize="0" runat="server">
                    <telerik:RadPane ID="rpValuacionPuestos" runat="server">
                        <%-- <div class="ctrlBasico">
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
                    <telerik:RadTextBox ID="txtNbTabulador"
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
                <div style="clear: both; height: 5px;"></div>--%>
                        <%--          <div style="height: calc(100% - 70px);">--%>
                        <%-- <telerik:RadGrid ID="grdValuacionPuesto"
                        runat="server"
                        Height="100%"
                        Width="1050"
                        AllowSorting="true"
                        ShowHeader="true"
                        OnNeedDataSource="grdValuacionPuesto_NeedDataSource">
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" DataKeyNames="ID_TABULADOR_FACTOR, ID_TABULADOR_PUESTO, ID_TABULADOR_VALUACION, ID_PUESTO, ID_COMPETENCIA" >
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Clave" DataField="CL_PUESTO" UniqueName="CL_PUESTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="220" FilterControlWidth="180" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="120" FilterControlWidth="100" HeaderText="Categoría" DataField="NB_TIPO_COMPETENCIA" UniqueName="NB_TIPO_COMPETENCIA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="120" FilterControlWidth="100" HeaderText="Competencia/factor" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Valor" HeaderStyle-Width="70" FilterControlWidth="40" AutoPostBackOnFilter="false" DataField="NO_VALOR" UniqueName="NO_VALOR">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox runat="server" ID="txnValor" Name="txnValor" Width="80px" MinValue="0" MaxValue="5" ShowSpinButtons="true"  Text='<%#Eval("NO_VALOR") %>' NumberFormat-DecimalDigits="0" ></telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>--%>
                        <telerik:RadGrid ID="grdValuacion" runat="server" Height="100%" AutoGenerateColumns="false" OnItemCreated="grdValuacion_ItemCreated"
                            OnNeedDataSource="grdValuacion_NeedDataSource" HeaderStyle-Font-Bold="true" AllowMultiRowSelection="true" AllowPaging="false" OnItemDataBound="grdValuacion_ItemDataBound">
                            <ClientSettings EnablePostBackOnRowClick="false">
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" FrozenColumnsCount="1" CountGroupSplitterColumnAsFrozen="false"></Scrolling>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView ClientDataKeyNames="ID_PUESTO" DataKeyNames="ID_PUESTO,NO_NIVEL,NO_PROMEDIO_VALUACION,ID_TABULADOR_PUESTO" EnableColumnsViewState="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                <Columns></Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <%--       </div>--%>
                    </telerik:RadPane>
                    <telerik:RadPane ID="rpAyudaValuacionPuestos" runat="server" Scrolling="None" Width="20px">
                        <telerik:RadSlidingZone ID="rszValuacionPuestos" runat="server" SlideDirection="Left" ExpandedPaneId="rsValuacionPuestos" Width="20px" ClickToOpen="true">
                            <%--  <telerik:RadSlidingPane ID="rspAyudaValuacionPuestos" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="100%">
                                <div style="padding: 10px; text-align: justify;">
                                    <p>
                                        Valúa conforme a la siguiente escala los puestos que deseas Tabular.
                                        <br />
                                        Se recomienda que esta valuación se realice a través de un comité de expertos y de un moderador que actúe como un juez imparcial en caso de algún debate.
                                        <br />
                                        <br />
                                        Recuerda que los valores que otorgues en este proceso serán los que definan los niveles de sueldo en tu organización.
                                    </p>
                                </div>
                            </telerik:RadSlidingPane>--%>
                            <telerik:RadSlidingPane ID="rspValoresValuacionPuestos" runat="server" Title="Niveles" Width="620px" RenderMode="Mobile" Height="100%">
                                <div style="padding: 10px; text-align: justify; height: 100%;">
                                    <div class="ctrlBasico" style="text-align: justify; font-size: 14px;">
                                        <telerik:RadLabel runat="server" ID="lblTitulo" Font-Bold="true"></telerik:RadLabel>
                                        <telerik:RadLabel runat="server" ID="lblCompetencia"></telerik:RadLabel>
                                    </div>
                                    <telerik:RadGrid ID="RadValuacion"
                                        runat="server"
                                        Height="80%"
                                        Width="590"
                                        AllowSorting="true"
                                        AllowFilteringByColumn="true"
                                        HeaderStyle-Font-Bold="true"
                                        ShowHeader="true"
                                        OnNeedDataSource="RadValuacion_NeedDataSource">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                                        </ClientSettings>
                                        <GroupingSettings CaseSensitive="false" />
                                        <PagerStyle AlwaysVisible="true" />
                                        <MasterTableView AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                                AddNewRecordText="Insertar" />
                                            <Columns>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="30" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Nivel" DataField="NIVEL"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="350" HeaderText="Descripción" DataField="DESCRIPCION"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
                <label runat="server" id="lblAdvertencia" visible="false" style="color: red;"></label>
                <div style="height: 5px;"></div>
                 <div class="ctrlBasico">
                 <telerik:RadButton ID="btnExcel" AutoPostBack="true" OnClick="btnExcel_Click" runat="server" Text="Exportar a excel" ></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnGuardar" AutoPostBack="true" OnClick="btnGuardar_Click" OnClientClicking="confirmarGuaradar" runat="server" Text="Guardar" Width="100"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnGaurdarCerrar" AutoPostBack="true" OnClick="btnGaurdarCerrar_Click" runat="server" Text="Guardar y cerrar" Width="150"></telerik:RadButton>
                </div>
                <%--    <div class="ctrlBasico">
        <telerik:RadButton ID="btnVistaNivelacion"  OnClientClicked="OpenWindow" AutoPostBack="false" runat="server" Text="Vista previa nivelación" Width="180"></telerik:RadButton>
    </div>--%>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
