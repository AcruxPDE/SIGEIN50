<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaCapturaMasiva.aspx.cs" Inherits="SIGE.WebApp.EO.Cuestionarios.VentanaCapturaMasiva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .labelSubtitulo {
            display: block;
            font-size: 1.6em;
        }

        .RadProgressBar .rpbStateSelected, .RadProgressBar .rpbStateSelected:hover, .RadProgressBar .rpbStateSelected:link, .RadProgressBar .rpbStateSelected:visited {
            background-color: #FF7400 !important;
            border: 0px solid black !important;
            color: black !important;
        }
    </style>

    <script>

        function onCloseWindow(oWnd, args) {
            $find("<%=grdMetas.ClientID%>").get_masterTableView().rebind();
        }

        function onRebindEvaluados(oWnd, args) {
            $find("<%=grdEvaluados.ClientID%>").get_masterTableView().rebind();
        }

        function confirmarTerminar(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

            radconfirm("¿Deseas terminar la sesión?", callBackFunction, 400, 170, null, "Aviso");
            args.set_cancel(true);
        }

        function ShowInsertForm(IdMetaEvaluado)
        {
            OpenSelectionWindow("VentanaContextoAdjuntarEvidenciaMetas.aspx?pIdEvaluadoMeta=" + IdMetaEvaluado + "&pIdEvaluador=" + '<%= vIdEvaluador %>', "rwAdjuntarArchivos", "Adjuntar evidencias")
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle, pWindowProperties) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            if (pWindowProperties)
                windowProperties = pWindowProperties;

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
        <telerik:RadAjaxLoadingPanel ID="ralpCapturaMasiva" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramCapturaMasiva" runat="server" DefaultLoadingPanelID="ralpCapturaMasiva">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramCapturaMasiva">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtTotal" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="btnAplicarTodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluados" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 60px); padding-left: 15px; padding-top: 10px; padding-right: 15px;">
        <telerik:RadSplitter ID="rsPlantilla" runat="server" Width="100%" Height="100%" BorderSize="0">          
            <telerik:RadPane ID="rpGridEvaluados" runat="server" Height="100%" ShowContentDuringLoad="false">
                <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Periodo:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtNoPeriodo" runat="server" style="min-width: 100px;"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNbPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="clear: both; height: 2px;"></div>
                <div style="height: calc(100% - 60px);">
                    <div style="height: calc(100% - 10px);">
                        <label class="labelTitulo">Metas establecidas</label>
                            <telerik:RadGrid ID="grdMetas" runat="server" Height="400"
                                AutoGenerateColumns="false" EnableHeaderContextMenu="true" AllowSorting="true"
                                AllowMultiRowSelection="false" OnNeedDataSource="grdMetas_NeedDataSource"
                                OnItemDataBound="grdMetas_ItemDataBound"  HeaderStyle-Font-Bold="true">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="false" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="ID_META_EVALUADO,ID_EVALUADOR, PR_EVALUADO, CL_TIPO_META, NB_CUMPLIMIENTO_MINIMO,NB_CUMPLIMIENTO_SATISFACTORIO,NB_CUMPLIMIENTO_SOBRESALIENTE,NB_RESULTADO" ClientDataKeyNames="ID_META_EVALUADO,ID_EVALUADOR, PR_EVALUADO, CL_TIPO_META,NB_CUMPLIMIENTO_MINIMO,NB_CUMPLIMIENTO_SATISFACTORIO,NB_CUMPLIMIENTO_SOBRESALIENTE,NB_RESULTADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup Name="NivelCompetencia" HeaderText="Nivel de meta" HeaderStyle-Font-Bold="true"
                                            HeaderStyle-HorizontalAlign="Center" />
                                    </ColumnGroups>
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="180" FilterControlWidth="120" HeaderText="Descripción" DataField="DS_META" UniqueName="DS_META" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="90" FilterControlWidth="50" HeaderText="Tipo meta" DataField="CL_TIPO_META" UniqueName="CL_TIPO_META" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ColumnGroupName="NivelCompetencia" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="60" FilterControlWidth="20" HeaderText="Actual" DataField="NB_CUMPLIMIENTO_ACTUAL" UniqueName="NB_CUMPLIMIENTO_ACTUAL" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ColumnGroupName="NivelCompetencia" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="60" FilterControlWidth="20" HeaderText="Mínima" DataField="NB_CUMPLIMIENTO_MINIMO" UniqueName="NB_CUMPLIMIENTO_MINIMO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ColumnGroupName="NivelCompetencia" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="75" FilterControlWidth="20" HeaderText="Satisfactoria" DataField="NB_CUMPLIMIENTO_SATISFACTORIO" UniqueName="NB_CUMPLIMIENTO_SATISFACTORIO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ColumnGroupName="NivelCompetencia" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="75" FilterControlWidth="20" HeaderText="Sobresaliente" DataField="NB_CUMPLIMIENTO_SOBRESALIENTE" UniqueName="NB_CUMPLIMIENTO_SOBRESALIENTE" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="75" FilterControlWidth="20" HeaderText="Ponderación" DataField="PR_EVALUADO" UniqueName="PR_EVALUADO" DataType="System.Int32" DataFormatString="{0:N2}%" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn  HeaderText="Resultado" FilterControlWidth="30px" DataType="System.Decimal" HeaderStyle-Font-Bold="true">
                                            <ItemStyle Width="90px" HorizontalAlign="Left" />
                                            <HeaderStyle Width="90px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox Visible="false" ID="txtResultadoPorcentual" DataType="Decimal" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtResultadoPorcentual_TextChanged"></telerik:RadNumericTextBox>
                                                <telerik:RadNumericTextBox Visible="false" ID="txtResultadoMonto" DataType="Decimal" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtResultadoPorcentual_TextChanged"></telerik:RadNumericTextBox>
                                                <telerik:RadDatePicker Visible="false" runat="server" ID="dtpResultaFecha" Width="120" Height="35px" AutoPostBack="true" OnSelectedDateChanged="dtpResultaFecha_SelectedDateChanged" ></telerik:RadDatePicker>
                                                <telerik:RadComboBox ID="cmbrResultadoSiNo" runat="server" Visible="false" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="cmbrResultadoSiNo_SelectedIndexChanged" >
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="" Value="0" />
                                                        <telerik:RadComboBoxItem Text="Sí" Value="4" />
                                                        <telerik:RadComboBoxItem Text="No" Value="1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Adjuntar evidencias" AllowFiltering="false" HeaderStyle-Font-Bold="true">
                                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                                            <HeaderStyle Width="70px" />
                                            <ItemTemplate>
                                                <a href="#" onclick="return ShowInsertForm(<%#Eval("ID_META_EVALUADO")%>);">Examinar</a>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                          <div style="clear: both; height: 10px;"></div>
                        <div class="ctrlBasico" style="float: right;">
                            Total:<telerik:RadTextBox ID="txtTotal" runat="server" Width="100" ReadOnly="true" EnabledStyle-HorizontalAlign="Right"></telerik:RadTextBox>
                            <telerik:RadButton ID="btnAplicarTodos" runat="server" Text="Aplicar calificación a todos" AutoPostBack="true" OnClick="btnAplicarTodos_Click"></telerik:RadButton>
                        </div>
                    </div>
                    <div style="clear: both; height: 10px;"></div>
                    <div style="height: calc(100% - 20px);">
                        <label class="labelTitulo">Evaluados</label>
                        <div style="height: calc(100% - 50px);">
                            <telerik:RadGrid ID="grdEvaluados" runat="server"
                                AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" EnableHeaderContextMenu="true" AllowSorting="true"
                                AllowMultiRowSelection="false" OnNeedDataSource="grdEvaluados_NeedDataSource" OnItemDataBound="grdEvaluados_ItemDataBound" Height="100%">
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="true" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="ID_EVALUADO,ESTATUS, CL_ESTADO_EMPLEADO" ClientDataKeyNames="ID_EVALUADO,ESTATUS, CL_ESTADO_EMPLEADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                    <Columns>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="20" HeaderText="No. de empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="130" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="130" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="130" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="40" HeaderText="Resultado" DataField="PR_CUMPLIMIENTO_EVALUADO" UniqueName="PR_CUMPLIMIENTO_EVALUADO" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}%"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </div>
            </telerik:RadPane>
             <telerik:RadPane ID="rpAyuda" runat="server" Height="30" Width="100%" Scrolling="None" ShowContentDuringLoad="false">
                <telerik:RadSlidingZone ID="rszMensajeInicial" runat="server" DockedPaneId="rspMensajeInicial" SlideDirection="Left" Width="22px">
                    <telerik:RadSlidingPane ID="rspMensajeInicial" runat="server" Title="Mensaje Inicial"  RenderMode="Mobile"  Width="250" Height="100%">
                        <div style="padding: 10px; text-align: justify;" id="mensajeInicial" runat="server">
                            <p>
                                Estimado(a):
                                <label runat="server" id="lblEvaluador"></label>
                                </p>
                                <p>
                                La lista de evaluados que se muestra, ha sido asignada para que los califiques de manera grupal.
                                <br />
                                El resultado de la meta que captures se aplicará a todas las personas.
                               <br /><br />
                                Para salir de la ventana actual selecciona el botón terminar proceso. Recuerda que puedes volver a ingresar posteriormente para seguir capturando los resultados de los evaluados.
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div class="ctrlBasico" style="float: right;">
        <telerik:RadButton ID="btnTerminar" runat="server" Text="Terminar proceso" OnClientClicking="confirmarTerminar" AutoPostBack="true" OnClick="btnTerminar_Click"></telerik:RadButton>
    </div>
    <div style="clear: both; height: 15px;"></div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server">
        <Windows>
            <telerik:RadWindow ID="rwCaptura" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="rwAdjuntarArchivos" runat="server" Animation="Fade" VisibleStatusbar="false" Behaviors="Close" Modal="true"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>

