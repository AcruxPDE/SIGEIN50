<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="EnvioDeSolicitudesClima.aspx.cs" Inherits="SIGE.WebApp.EO.EnvioDeSolicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpEnvioSolicitud" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramEnvioSolicitud" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnEnviar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEvaluadores" LoadingPanelID="ralpEnvioSolicitud" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnEnviarTodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgEvaluadores" LoadingPanelID="ralpEnvioSolicitud" UpdatePanelRenderMode="Inline"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
        <telerik:RadSplitter runat="server" ID="spHelp" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpDatos" runat="server">
                <telerik:RadTabStrip ID="rtsSolicitudes" runat="server" SelectedIndex="0" MultiPageID="rmpSolicitudes">
                    <Tabs>
                        <telerik:RadTab Text="Contexto"></telerik:RadTab>
                        <telerik:RadTab Text="Enviar cuestionarios"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: calc(100% - 60px); width: 100%;">
                <telerik:RadMultiPage ID="rmpSolicitudes" runat="server" SelectedIndex="0" Height="100%">
                    <telerik:RadPageView ID="rpvContexto" runat="server">
                   <%--    <div class="ctrlBasico">
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
                                    <table class="ctrlTableForm" text-align: left;>
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
                                                <label id="Label2" name="lbNotas" runat="server">Notas:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtNotas" runat="server"></div>
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
                                                <label id="Label6" name="lbNotas" runat="server">Tipo de cuestionario:</label>
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
                    <telerik:RadPageView ID="rpvEnvio" runat="server">
                      <div style="clear: both; height: 10px;"></div>
                        <div style="height: calc(100% - 60px);">
                            <telerik:RadGrid runat="server" HeaderStyle-Font-Bold="true" ID="rgEvaluadores" AutoGenerateColumns="false" OnItemDataBound="rgEvaluadores_ItemDataBound" OnNeedDataSource="rgEvaluadores_NeedDataSource" Height="100%" Width="100%" AllowSorting="true" AllowMultiRowSelection="true">
                                <ClientSettings EnablePostBackOnRowClick="false">
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_EVALUADOR, FL_EVALUADOR, CL_TOKEN, FG_CONTESTADO">
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup Name="Evaluadores" HeaderText="Evaluadores"
                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Larger" />
                                    </ColumnGroups>
                                    <Columns>
                                        <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30px"></telerik:GridClientSelectColumn>
                                        <telerik:GridBoundColumn ColumnGroupName="Evaluadores" UniqueName="NB_EVALUADOR" DataField="NB_EVALUADOR" HeaderText="Nombre completo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn ColumnGroupName="Evaluadores" UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn ColumnGroupName="Evaluadores" UniqueName="CL_CORREO_EVALUADOR" DataField="CL_CORREO_EVALUADOR" HeaderText="Correo electónico" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <telerik:RadTextBox runat="server" ID="txtCorreo" Width="100%" Text='<%# Bind("CL_CORREO_ELECTRONICO") %>' AutoPostBack="false"></telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            </div>
                          <div style="clear: both; height: 10px;"></div>
                            <label id="lbMensaje" runat="server" visible="false" style="color: red;">No hay cuestionarios creados que enviar o todos los cuestionarios para este período han sido enviados y contestados.</label>
                            <div class="divControlDerecha">
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnEnviar" Text="Enviar a seleccionados" OnClick="btnEnviar_Click"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnEnviarTodos" Text="Enviar a todos" OnClick="btnEnviarTodos_Click"></telerik:RadButton>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
                            </div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
             </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Width="30">
                <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="500" MinWidth="500" Height="100%">
                        <div style="padding: 10px; text-align: justify;">
                            Este proceso envía una solicitud para evaluación de cuestionarios a cada uno de los correos electrónicos de los evaluadores. Un ejemplo del cuerpo del mensaje se muestra a continuación, si deseas modificarlo deberás hacerlo en el menú de configuración del sistema.
                        </div>
                    </telerik:RadSlidingPane>
                    <telerik:RadSlidingPane ID="rspMensaje" runat="server" Title="Mensaje" Width="500" MinWidth="500" Height="100%">
                        <div style="padding: 10px; text-align: justify;">
                            <fieldset>
                                <legend>
                                    <label>Ejemplo del cuerpo del mensaje:</label></legend>
                                <literal id="lMensaje" runat="server"></literal>
                            </fieldset>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
