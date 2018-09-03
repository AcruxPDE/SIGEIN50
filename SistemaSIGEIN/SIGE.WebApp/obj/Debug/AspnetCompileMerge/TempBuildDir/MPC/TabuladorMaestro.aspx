<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="TabuladorMaestro.aspx.cs" Inherits="SIGE.WebApp.MPC.TabuladorMaestro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <style>
            .RadGrid1Class {
                background-color: #FFFFFF;
                /*border: 1px solid #ddd !important;
                border-left: 1px;
                border:1px #e5e5e5;  
                border-style:none none solid solid;  
                padding-top:4px;  
                padding-bottom:4px;*/
                /*border-color: #FFF !important;*/
            }

            .RadGrid2Class {
                /*background-color: #A9E2F3;*/
                background-color: #E6E6FA;
                /*border:1px #e5e5e5;  
                border-style:none none solid solid;  
                padding-top:4px;  
                padding-bottom:4px;*/
                /*border-color: #FFF !important;*/
            }
        </style>

        <script type="text/javascript">
            function closeWindow() {
                GetRadWindow().close();
            }

            function OnCloseWindow() {
                $find("<%=grdTabuladorMaestro.ClientID%>").get_masterTableView().rebind();
            }

            function OpenWindow() {
                var myUrl = '<%= ResolveClientUrl("TabuladorNivel.aspx") %>';
                openChildDialog(myUrl + "?&ID=" + <%=vIdTabulador%> + "", "winSeleccion", "Niveles")
            }

            function OpenWindowConfiguracion() {
                var myUrl = '<%= ResolveClientUrl("VentanaConfigurarTabulador.aspx") %>';
                openChildDialog(myUrl + "?&CL_CONFIGURAR=OCULTAR" + "&ID=" +<%=vIdTabulador%> +"", "winSeleccion", "Configurar")
            }

            function OpenWindowTabuladores() {
                var vIdTabulador = '<%=vIdTabulador%>';
                var myUrl = '<%= ResolveClientUrl("SeleccionTabulador.aspx") %>';
                if (vIdTabulador != null)
                    openChildDialog(myUrl + "?pFgMultSeleccion=0&pIdTabulador=" + vIdTabulador, "winSeleccion", "Selección de tabulador a copiar");
            }

            function useDataFromChild(pDato) {
                if (pDato != null) {
                    var vDatosSeleccionados = pDato[0];
                    if (vDatosSeleccionados.clTipoCatalogo == "TABULADOR") {
                        var vJsonPeriodo = JSON.stringify({ clTipo: "TABULADOR", oSeleccion: pDato[0].idTabulador });
                        var ajaxManager = $find('<%= ramTabuladorMaestro.ClientID%>');
                        ajaxManager.ajaxRequest(vJsonPeriodo);
                    }
                    else if (vDatosSeleccionados.clTipoCatalogo == "ACTUALIZAR") {
                        var vJsonPeriodo = JSON.stringify({ clTipo: "ACTUALIZAR" });
                        var ajaxManager = $find('<%= ramTabuladorMaestro.ClientID%>');
                        ajaxManager.ajaxRequest(vJsonPeriodo);
                    }
                    else {
                        var vJsonPeriodo = JSON.stringify({ clTipo: vDatosSeleccionados.accion });
                        var ajaxManager = $find('<%= ramTabuladorMaestro.ClientID%>');
                        ajaxManager.ajaxRequest(vJsonPeriodo);
                    }
            }
            }

            function ConfirmarGuardar(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('Los cambios efectuados impactará en la planeación de incrementos y las consultas, ¿Deseas continuar?', callBackFunction, 400, 170, null, "Confirmar guardar");
                args.set_cancel(true);
            }

            function ConfirmaGenerar(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });

                radconfirm('Esta opción borrará los valores en tabulador(mostrados actualmente) y recalculará en base al mercado salarial, ¿Deseas continuar?', callBackFunction, 400, 170, null, "Confirmar");
                args.set_cancel(true);
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpTabuladorMaestro" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramTabuladorMaestro" runat="server" OnAjaxRequest="ramTabuladorMaestro_AjaxRequest" DefaultLoadingPanelID="ralpTabuladorMaestro">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramTabuladorMaestro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTabuladorMaestro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnMercadoSalarial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTabuladorMaestro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTabuladorMaestro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarCerrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTabuladorMaestro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTabuladorMaestro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTabuladorMaestro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnVerNiveles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTabuladorMaestro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbCuartil">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbCuartil" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="grdTabuladorMaestro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txnMaximo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTabuladorMaestro" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ramTabuladorMaestro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTabuladorMaestro" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtPorcentajeIncremento" UpdatePanelRenderMode="Inline" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txtNoCategorias" UpdatePanelRenderMode="Inline" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadTabStrip ID="rtsTabuladorMaestro" runat="server" SelectedIndex="0" MultiPageID="rmpTabuladorMaestro">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Tabulador maestro" SelectedIndex="1"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpTabuladorMaestro" runat="server" SelectedIndex="0" Height="90%">
        <telerik:RadPageView ID="rpvContexto" runat="server" Height="100%">
            <div style="clear: both; height: 10px;"></div>
            <div class="ctrlBasico">
                <table class="ctrlTableForm">
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Vigencia:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtClTabulador" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Nombre:</label></td>
                        <td colspan="2" class="ctrlTableDataBorderContext">
                            <div id="txtNbTabulador" runat="server" style="min-width: 100px;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="ctrlTableDataContext">
                            <label>Descripción:</label></td>
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
        <telerik:RadPageView ID="rpvTabMaestro" runat="server" Height="100%">
            <telerik:RadSplitter ID="rsTabuladorMaestro" BorderSize="0" Width="100%" Height="100%" runat="server">
                <telerik:RadPane ID="rpTabuladorMaestro" runat="server">
                    <%-- <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <label id="lbTabulador" name="lbTabulador" runat="server">Tabulador:</label>
                    <telerik:RadTextBox ID="txtClTabulador" runat="server"  Width="150px"  MaxLength="800" Enabled="false">
                    </telerik:RadTextBox>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox ID="txtNbTabulador" runat="server"  Width="260px" MaxLength="800" Enabled="false">
                    </telerik:RadTextBox>
                </div>
                <div class="ctrlBasico">
                    <label id="lbFecha" name="lbFecha" runat="server"> Fecha:</label>
                     <telerik:RadDatePicker ID="rdpCreacion" Enabled="false" runat="server" Width="140px">
                    </telerik:RadDatePicker>
                </div>
                <div class="ctrlBasico">
                    <label id="lbVigencia" name="lbVigencia" runat="server"> Vigencia:</label>
                     <telerik:RadDatePicker ID="rdpVigencia" Enabled="false" runat="server" Width="140px">
                    </telerik:RadDatePicker>
                </div>--%>
                    <div class="ctrlBasico">
                        <label id="lbNoCategorias" name="lbNoCategorias" runat="server">Número de categorías dentro de cada nivel:</label>
                        <telerik:RadTextBox ID="txtNoCategorias" runat="server" Width="40px" MaxLength="800" Enabled="false">
                        </telerik:RadTextBox>
                    </div>
                    <div class="ctrlBasico">
                        <label id="lbPorcentajeIncremento" name="lbPorcentajeIncremento" runat="server">Porcentaje de incremento inflacional para siguiente año:</label>
                        <telerik:RadTextBox ID="txtPorcentajeIncremento" runat="server" Width="60px" MaxLength="800" Enabled="false">
                        </telerik:RadTextBox>
                        <label id="lbPorcentaje" name="lbPorcentaje" runat="server">%</label>
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <div class="ctrlBasico">
                        <label id="lblCuartil"
                            name="lblCuartil"
                            runat="server">
                            Percentil del tabulador para incremento inflacional:</label>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadComboBox Filter="Contains" OnSelectedIndexChanged="cmbCuartil_SelectedIndexChanged" runat="server" ID="cmbCuartil" Width="190" MarkFirstMatch="true" AutoPostBack="true" EmptyMessage="Seleccione..."
                            DropDownWidth="190">
                        </telerik:RadComboBox>
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <div style="height: calc(100% - 170px);">
                        <telerik:RadGrid ID="grdTabuladorMaestro"
                            runat="server"
                            Height="100%"
                            AllowSorting="true"
                            ShowHeader="true"
                            HeaderStyle-Font-Bold="true"
                            AutoGenerateColumns="true"
                            OnNeedDataSource="grdTabuladorMaestro_NeedDataSource"
                            OnItemDataBound="grdTabuladorMaestro_ItemDataBound">
                            <GroupingSettings CaseSensitive="false" />
                            <ClientSettings EnableAlternatingItems="false">
                                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <MasterTableView AutoGenerateColumns="false" DataKeyNames="ID_TABULADOR_NIVEL, NO_CATEGORIA, ID_TABULADOR_MAESTRO,ID_ITEM,NO_NIVEL" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Nivel" DataField="NB_TABULADOR_NIVEL" UniqueName="NB_TABULADOR_NIVEL" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Categoría" DataField="NB_CATEGORIA" UniqueName="NB_CATEGORIA" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Progresión" DataField="PR_PROGRESION" UniqueName="PR_PROGRESION" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Mínimo" HeaderStyle-Width="150" AutoPostBackOnFilter="false" FilterControlWidth="70" DataField="MN_MINIMO" UniqueName="MN_MINIMO" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txnMinimo" Name="txnMinimo" Width="140px" Text='<%#Eval("MN_MINIMO") %>' MinValue="0" AutoPostBack="false" ShowSpinButtons="true" NumberFormat-DecimalDigits="2" Type="Currency" ></telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Primer cuartil" DataField="MN_PRIMER_CUARTIL" UniqueName="MN_PRIMER_CUARTIL" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txnCuartilPrimero" Type="Currency" Name="txnMinimo" Width="140px" Text='<%#Eval("MN_PRIMER_CUARTIL") %>' MinValue="0" AutoPostBack="true" ShowSpinButtons="true" NumberFormat-DecimalDigits="2" OnTextChanged="txnCuartilPrimero_TextChanged"></telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Media" DataField="MNL_MEDIO" UniqueName="MN_MEDIO" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" Type="Currency" ID="txnCuartilMedio" Name="txnMinimo" Width="140px" Text='<%#Eval("MN_MEDIO") %>' MinValue="0" AutoPostBack="true" ShowSpinButtons="true" NumberFormat-DecimalDigits="2" OnTextChanged="txnCuartilPrimero_TextChanged"></telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Tercer cuartil" DataField="MN_SEGUNDO_CUARTIL" UniqueName="MN_SEGUNDO_CUARTIL" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txnCuartilSegundo" Type="Currency" Name="txnMinimo" Width="140px" Text='<%#Eval("MN_SEGUNDO_CUARTIL") %>' MinValue="0" AutoPostBack="true" ShowSpinButtons="true" NumberFormat-DecimalDigits="2" OnTextChanged="txnCuartilPrimero_TextChanged"></telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Máximo" HeaderStyle-Width="150" AutoPostBackOnFilter="false" FilterControlWidth="70" DataField="MN_MAXIMO" UniqueName="MN_MAXIMO" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txnMaximo" Type="Currency" AutoPostBack="false" Name="txnMaximo" Width="140px" Text='<%#Eval("MN_MAXIMO") %>' MinValue="0" ShowSpinButtons="true" NumberFormat-DecimalDigits="2" ></telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Siguiente año" DataField="MN_SIGUIENTE" UniqueName="MN_SIGUIENTE" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%# string.Format("{0:C}",(Eval("MN_SIGUIENTE"))) %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                                        <div class="ctrlBasico">
                        <telerik:RadButton ID="btnCopiarTabulador" OnClientClicked="OpenWindowTabuladores" AutoPostBack="true" runat="server" Text="Copiar datos tabulador" Width="200"></telerik:RadButton>
                    </div>
                      <div class="ctrlBasico">
                    <telerik:RadButton ID="btnMercadoSalarial" AutoPostBack="true" OnClientClicking="ConfirmaGenerar" OnClick="btnMercadoSalarialBoton_Click" runat="server" Text="Generar desde mercado salarial"  ToolTip="Generar el tabulador en base a los cambios en el mercado salarial."></telerik:RadButton>
                    </div>
                                        <div class="ctrlBasico">
                        <telerik:RadButton ID="btnRecalcular" AutoPostBack="true" runat="server" Text="Recalcular" Width="100" ToolTip="Recalcula los valores de la tabla en base a los mínimos y máximos." OnClick="btnRecalcular_Click" ></telerik:RadButton>
                    </div>
                      
                  <%--  <div class="ctrlBasico">
                        <telerik:RadButton ID="btnVerNiveles" OnClientClicked="OpenWindow" AutoPostBack="false" runat="server" Text="Ver niveles" Width="100"></telerik:RadButton>
                    </div>--%>
                    <%--<div class="ctrlBasico">
        <telerik:RadButton ID="btnRecalcular"  OnClick="btnRecalcular_Click"  AutoPostBack="true" runat="server" Text="Recalcular modificaciones" Width="210" ToolTip="Recalcula los valores de la tabla en base a los porcentanjes."></telerik:RadButton>
    </div>--%>
     <%--               <div class="ctrlBasico">
                        <telerik:RadButton ID="btnConfiguracion" OnClientClicked="OpenWindowConfiguracion" AutoPostBack="true" runat="server" Text="Configurar" Width="100"></telerik:RadButton>
                    </div>--%>


                  <%--  <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardar" OnClientClicking="ConfirmarGuardar" OnClick="btnGuardar_Click" AutoPostBack="true" runat="server" Text="Guardar" Width="100" ToolTip="Asignar los valores al tabulador y continuar."></telerik:RadButton>
                    </div>--%>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnGuardarCerrar" OnClientClicking="ConfirmarGuardar" OnClick="btnGuardarCerrar_Click" AutoPostBack="true" runat="server" Text="Guardar y cerrar" Width="150"></telerik:RadButton>
                    </div>
                </telerik:RadPane>
                <telerik:RadPane ID="rpAyudaTabuladorMaestro" runat="server" Scrolling="None" Width="20px">
                    <telerik:RadSlidingZone ID="rszAyudaTabuladorMaestro" runat="server" SlideDirection="Left" ExpandedPaneId="rsTabuladorMaestro" Width="20px" ClickToOpen="true">
                        <telerik:RadSlidingPane ID="rspAyudaTabuladorMaestro" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="100%">
                            <div style="padding: 10px; text-align: justify;">
                                <p>Utilice esta tabla para definir el esquema de niveles del tabulador.</p>
                            </div>
                        </telerik:RadSlidingPane>
                    </telerik:RadSlidingZone>
                </telerik:RadPane>
            </telerik:RadSplitter>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
