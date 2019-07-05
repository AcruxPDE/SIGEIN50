<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="MercadoSalarial.aspx.cs" Inherits="SIGE.WebApp.MPC.MercadoSalarial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .RadGrid1Class {
            background-color: #FFFFFF;
        }

        .RadGrid2Class {
            background-color: #E6E6FA;
        }
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function closeWindow() {
                //$find("<%=grdMercadoSalarial.ClientID%>").get_masterTableView().rebind();
                GetRadWindow().close();
            }

            function GetWindowProperties() {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                return {
                    width: browserWnd.innerWidth - 30,
                    height: browserWnd.innerHeight - 20
                };
            }

            function winOpenTabuladores() {
                var vPropierties = GetWindowProperties();
                var vIdTabulador = '<%= vIdTabulador %>';
                var myUrl = '<%= ResolveClientUrl("SeleccionTabulador.aspx") %>';
                if (vIdTabulador != null)
                    openChildDialog(myUrl + "?pFgMultSeleccion=0&pIdTabulador=" + vIdTabulador, "winSeleccion", "Selección de tabulador a copiar", vPropierties);
            }

            function useDataFromChild(pDato) {
                if (pDato != null) {
                    var arr = [];
                    var vDatoSeleccionado = pDato[0];
                    switch (pDato[0].clTipoCatalogo) {
                        case "TABULADOR":
                            var vJsonPeriodo = JSON.stringify({ clTipo: "TABULADOR", oSeleccion: pDato[0].idTabulador });
                            var ajaxManager = $find('<%= RadAjaxManager1.ClientID%>');
                            ajaxManager.ajaxRequest(vJsonPeriodo);
                            break;
                    }
                }
            }

            function ConfirmarGuardar(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('Los cambios efectuados impactarán en el tabulador maestro, ¿Deseas continuar?', callBackFunction, 400, 170, null, "Aviso");
                args.set_cancel(true);
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAjuste">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMercadoSalarial" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMercadoSalarial" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardarCerrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMercadoSalarial" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMercadoSalarial" UpdatePanelRenderMode="Inline" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txnMinimo" UpdatePanelRenderMode="Inline" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txnMaximo" UpdatePanelRenderMode="Inline" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip ID="rtsMercadoSalarial" runat="server" SelectedIndex="0" MultiPageID="rmpTabuladorSueldos">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Mercado Salarial"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: calc(100% - 50px);">
        <telerik:RadMultiPage ID="rmpTabuladorSueldos" runat="server" SelectedIndex="0" Height="90%">
            <telerik:RadPageView ID="rpvContexto" runat="server" Height="100%">
                <telerik:RadSplitter ID="rspContexto" Width="100%" Height="100%" BorderSize="0" runat="server">
                    <telerik:RadPane ID="rpContexto" runat="server">
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
                    <telerik:RadPane ID="rpAyudaMercadoSalarial" runat="server" Scrolling="None" Width="30px">
                        <telerik:RadSlidingZone ID="rszAyudaMercadoSalarial" runat="server" SlideDirection="Left" ExpandedPaneId="rsMercadoSalarial" Width="30px" ClickToOpen="true">
                            <telerik:RadSlidingPane ID="rspAyudaMercadoSalarial" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="100%">
                                <div style="padding: 10px; text-align: justify;">
                                    <p>
                                        Registra en la tabla el mínimo y el máximo de sueldo por puesto, de acuerdo a la información del mercado salarial de tu localidad y giro.
                                        <br />
                                        Esta información 
                                        es de vital importancia por lo que te recomendamos basarte en una encuesta seria de sueldos de tu localidad o acudir a un profesional en el área.
                                    </p>
                                </div>
                            </telerik:RadSlidingPane>
                        </telerik:RadSlidingZone>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvMercadoSalarial" runat="server">
                <telerik:RadSplitter ID="rsMercadoSalarial" Width="100%" Height="100%" BorderSize="0" runat="server">
                    <telerik:RadPane ID="rpMercadoSalarial" runat="server">
                        <%--         <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <label id="lbTabulador" name="lbTabulador" runat="server">Tabulador:</label>
                    <telerik:RadTextBox ID="txtClTabulador" runat="server" Width="150px" MaxLength="800" Enabled="false"></telerik:RadTextBox>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtNbTabulador" runat="server" Width="260px" MaxLength="800" Enabled="false"></telerik:RadTextBox>
                </div>
                <div class="ctrlBasico">
                    <label id="lbFecha" name="lbFecha" runat="server">Fecha:</label>
                    <telerik:RadDatePicker ID="rdpCreacion" Enabled="false" runat="server" Width="140px"></telerik:RadDatePicker>
                </div>
                <div class="ctrlBasico">
                    <label id="lbVigencia" name="lbVigencia" runat="server">Vigencia:</label>
                    <telerik:RadDatePicker ID="rdpVigencia" Enabled="false" runat="server" Width="140px"></telerik:RadDatePicker>
                </div>--%>
                        <%--                <div style="height: calc(100% - 10px);">--%>
                        <telerik:RadGrid ID="grdMercadoSalarial"
                            runat="server"
                            Height="100%"
                            AllowSorting="true"
                            AutoGenerateColumns="true"
                            HeaderStyle-Font-Bold="true"
                            OnNeedDataSource="grdMercadoSalarial_NeedDataSource"
                            OnItemDataBound="grdMercadoSalarial_ItemDataBound">
                            <ClientSettings>
                                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AutoGenerateColumns="false" DataKeyNames="ID_PUESTO, CL_ORIGEN, ID_TABULADOR_PUESTO, NO_NIVEL" ClientDataKeyNames="ID_PUESTO" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                                <ColumnGroups>
                                    <telerik:GridColumnGroup HeaderText="Mercado" Name="MERCADO" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></telerik:GridColumnGroup>
                                </ColumnGroups>
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="70" FilterControlWidth="10" HeaderText="No." DataField="NO_RENGLON" UniqueName="NO_RENGLON" ItemStyle-HorizontalAlign="center"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="140" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="260" FilterControlWidth="200" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Mínimo" ColumnGroupName="MERCADO" HeaderStyle-Width="190" AutoPostBackOnFilter="false" FilterControlWidth="120" DataField="MN_MINIMO" UniqueName="MN_MINIMO">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txnMinimo" Name="txnMinimo" Width="160px" Text='<%#Eval("MN_MINIMO") %>' MinValue="0" ShowSpinButtons="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Máximo" ColumnGroupName="MERCADO" HeaderStyle-Width="190" AutoPostBackOnFilter="false" FilterControlWidth="120" DataField="MN_MAXIMO" UniqueName="MN_MAXIMO">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txnMaximo" Name="txnMaximo" Width="160px" Text='<%#Eval("MN_MAXIMO") %>' MinValue="0" ShowSpinButtons="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="120" FilterControlWidth="60" HeaderText="Origen" DataField="CL_ORIGEN" UniqueName="CL_ORIGEN"></telerik:GridBoundColumn>
                                    <%--     <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="50" FilterControlWidth="20" HeaderText="Nivel" DataField="NO_NIVEL" UniqueName="NO_NIVEL" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>--%>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <%--                </div>--%>
                    </telerik:RadPane>
                    <%--  <telerik:RadPane ID="rpAyudaMercadoSalarial" runat="server" Scrolling="None" Width="30px">
                <telerik:RadSlidingZone ID="rszAyudaMercadoSalarial" runat="server" SlideDirection="Left" ExpandedPaneId="rsMercadoSalarial" Width="30px">
                    <telerik:RadSlidingPane ID="rspAyudaMercadoSalarial" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="100%">
                        <div style="padding: 20px; text-align: justify;">
                            <p>
                                Registra en la tabla el mínimo y el máximo de sueldo por puesto, de acuerdo a la información del mercado salarial de tu localidad y giro. <br /> Esta información 
                                        es de vital importancia por lo que te recomendamos basarte en una encuesta seria de sueldos de tu localidad o acudir a un profesional en el área.
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>--%>
                </telerik:RadSplitter>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnAjuste" AutoPostBack="true" runat="server" OnClick="btnAjuste_Click" Text="Rangos desde inventario" Width="240" ToolTip="Esta opción llenará automáticamente los campos del mercado que hayan permanecido en 0 con los sueldos máximos y mínimos de los ocupantes que correspondan a los puestos analizados."></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnCopiarMercadoSalarial" runat="server" Width="250px" Text="Obtener datos de otro tabulador" AutoPostBack="false" OnClientClicking="winOpenTabuladores" ToolTip="Esta opción te permite utilizar los datos de un tabulador ya existente con el mismo número de niveles que este mercado salarial."></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnGuardar" AutoPostBack="true" runat="server" Text="Guardar" Width="100" OnClientClicking="ConfirmarGuardar" OnClick="btnGuardar_Click"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnGuardarCerrar" AutoPostBack="true" runat="server" Text="Guardar y cerrar" Width="150" OnClientClicking="ConfirmarGuardar" OnClick="btnGuardarCerrar_Click"></telerik:RadButton>
                </div>
                <%-- <div class="ctrlBasico">
     <telerik:RadButton ID="btnCancelarNuevo" runat="server" Width="100px" Text="Cancelar" AutoPostBack="true" OnClientClicking="closeWindow"></telerik:RadButton>
    </div>--%>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
