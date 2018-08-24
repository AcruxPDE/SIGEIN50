<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaManualCapturaResultados.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaManualCapturaResultados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>
        function ShowInsertForm(IdEvaluadoMeta) {

            OpenSelectionWindow("VentanaAdjuntarEvidenciaMetas.aspx?pIdEvaluadoMeta=" + IdEvaluadoMeta, "rwAdjuntarArchivos", "Adjuntar evidencias")

        }
        function onCloseWindow(oWnd, args) {
            $find("<%=grdResultados.ClientID%>").get_masterTableView().rebind();

        }

        function CloseWindow(oWnd, args) {
            GetRadWindow().close();
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
    <div style="height: calc(100% - 10px);">
        <div style="clear: both; height: 10px;"></div>

        <telerik:RadTabStrip ID="rtsConfiguracionClima" runat="server" SelectedIndex="0" MultiPageID="rmpCapturaResultados">
            <Tabs>
                <telerik:RadTab SelectedIndex="0" Text="Contexto"></telerik:RadTab>
                <telerik:RadTab SelectedIndex="1" Text="Captura de resultados"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div style="height: 10px;"></div>
        <div style="height: calc(100% - 100px);">
            <telerik:RadMultiPage ID="rmpCapturaResultados" runat="server" SelectedIndex="0" Height="100%">
                <telerik:RadPageView ID="rpvContexto" runat="server">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Período:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtNbPeriodo" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Fechas:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtFechaDel" runat="server"></div>
                            </td>

                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Notas:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtNotas" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de periodo:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtTipoPeriodo" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de bono:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtTipoBono" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Empleado:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtEmpleado" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Responsable de captura de metas:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtResponsable" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Ponderación del puesto:</label></td>
                            <td colspan="2" class="ctrlTableDataContext">
                                <div id="Div2" runat="server">
                                    <telerik:RadNumericTextBox ID="txtPonderacion" MaxValue="100" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" HoveredStyle-HorizontalAlign="Right" MinValue="0" DataType="Decimal" runat="server" Width="100px" MaxLength="13"></telerik:RadNumericTextBox>
                                    <label>%</label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div class="divControlIzquierda" style="width: 10px; padding-left: 250px;">
                        <telerik:RadButton ID="btnActualizar" Visible="false" runat="server" Text="Actualizar" OnClick="btnActualizar_Click"></telerik:RadButton>
                    </div>
                </telerik:RadPageView>

                <telerik:RadPageView ID="rpvapturaDeResultados" runat="server">
                    <div style="height: calc(100% - 10px);">
                        <telerik:RadGrid ID="grdResultados" runat="server" Height="100%" HeaderStyle-Font-Bold="true"
                            AutoGenerateColumns="false" EnableHeaderContextMenu="true" AllowSorting="true"
                            AllowMultiRowSelection="false" OnNeedDataSource="grdResultados_NeedDataSource"
                            OnItemDataBound="grdResultados_ItemDataBound">
                            <ClientSettings>
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="false" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView DataKeyNames="ID_EVALUADO_META, PR_EVALUADO, CL_TIPO_META, NB_CUMPLIMIENTO_MINIMO,NB_CUMPLIMIENTO_SATISFACTORIO,NB_CUMPLIMIENTO_SOBRESALIENTE,NB_RESULTADO" ClientDataKeyNames="ID_EVALUADO_META, PR_EVALUADO, CL_TIPO_META,NB_CUMPLIMIENTO_MINIMO,NB_CUMPLIMIENTO_SATISFACTORIO,NB_CUMPLIMIENTO_SOBRESALIENTE,NB_RESULTADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                                <ColumnGroups>
                                    <telerik:GridColumnGroup Name="NivelCompetencia" HeaderText="Nivel de meta"
                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" />
                                </ColumnGroups>
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="240" FilterControlWidth="60" HeaderText="Descripción" DataField="DS_META" UniqueName="DS_META" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="90" FilterControlWidth="60" HeaderText="Tipo meta" DataField="CL_TIPO_META" UniqueName="CL_TIPO_META" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ColumnGroupName="NivelCompetencia" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="60" FilterControlWidth="20" HeaderText="Actual" DataField="NB_CUMPLIMIENTO_ACTUAL" UniqueName="NB_CUMPLIMIENTO_ACTUAL" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ColumnGroupName="NivelCompetencia" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="60" FilterControlWidth="20" HeaderText="Mínima" DataField="NB_CUMPLIMIENTO_MINIMO" UniqueName="NB_CUMPLIMIENTO_MINIMO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ColumnGroupName="NivelCompetencia" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="60" FilterControlWidth="20" HeaderText="Satisfactoria" DataField="NB_CUMPLIMIENTO_SATISFACTORIO" UniqueName="NB_CUMPLIMIENTO_SATISFACTORIO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ColumnGroupName="NivelCompetencia" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="65" FilterControlWidth="25" HeaderText="Sobresaliente" DataField="NB_CUMPLIMIENTO_SOBRESALIENTE" UniqueName="NB_CUMPLIMIENTO_SOBRESALIENTE" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" HeaderStyle-Width="60" FilterControlWidth="20" HeaderText="Ponderación" DataField="PR_EVALUADO" UniqueName="PR_EVALUADO" DataType="System.Int32" DataFormatString="{0:N2}%" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Resultado" FilterControlWidth="30px" DataType="System.Decimal" HeaderStyle-Font-Bold="true">
                                        <ItemStyle Width="90px" HorizontalAlign="Left" />
                                        <HeaderStyle Width="90px" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox Visible="false" ID="txtResultadoPorcentual" DataType="Decimal" runat="server" Width="100px"></telerik:RadNumericTextBox>
                                            <telerik:RadNumericTextBox Visible="false" ID="txtResultadoMonto" DataType="Decimal" runat="server" Width="100px"></telerik:RadNumericTextBox>
                                            <telerik:RadDatePicker Visible="false" runat="server" ID="dtpResultaFecha" Width="120" Height="35px"></telerik:RadDatePicker>
                                            <telerik:RadComboBox ID="cmbrResultadoSiNo" runat="server" Visible="false" Width="100px"  >
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
                                            <a href="#" onclick="return ShowInsertForm(<%#Eval("ID_EVALUADO_META")%>);">Examinar</a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    <label id="lbMetasConfiguradas" name="lbMetasConfiguradas" runat="server" visible="false" style="color:red;">Las metas deshabilitadas no cuentan con la configuración necesaria para su evaluación. Revíselas en la configuración del período.</label>

                    </div>
                    <div class="divControlDerecha">
                        <div style="clear: both; height: 20px;"></div>
                        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
                         <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" OnClientClicking="CloseWindow"></telerik:RadButton>
                    </div>

                </telerik:RadPageView>



            </telerik:RadMultiPage>
        </div>
        <div style="clear: both;"></div>
    </div>


    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>

