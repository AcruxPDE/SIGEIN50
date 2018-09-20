<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaEnvioSolicitudes.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaEnvioSolicitudes" %>

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

        <telerik:RadTabStrip ID="rtsConfiguracionPeriodo" runat="server" SelectedIndex="0" MultiPageID="rmpEnvioCuestionario">
        <Tabs>
             <telerik:RadTab Text="Contexto" Value="0"></telerik:RadTab>
            <telerik:RadTab Text="Enviar evaluaciones" Value="1"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>

        <div style="height: calc(100% - 50px);">
        <telerik:RadMultiPage ID="rmpEnvioCuestionario" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">    
                 <div style="height: calc(100% - 30px);">         
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
                                                <label id="Label1" name="lbTabulador" runat="server">Estatus:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtEstatus" runat="server"></div>
                                            </td>
                                        </tr>
                                                      <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label2" name="lbTabulador" runat="server">Tipo de evaluación:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtTipoEvaluacion" runat="server"></div>
                                            </td>
                                        </tr>
                                             <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label3" name="lbTabulador" runat="server">Notas:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtNotas" runat="server"></div>
                                            </td>
                                        </tr>
                                        </table>
                                    </div>
                     </div>
                </telerik:RadPageView>
      <telerik:RadPageView ID="rpvEnvio" runat="server">
    <telerik:RadSplitter runat="server" ID="spHelp" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="rpDatos" runat="server">
            <div style="clear: both; height: 10px;"></div>
            <div style="height: calc(100% - 70px); width: 100%;">
                <telerik:RadGrid runat="server" ID="rgEvaluadores" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" OnNeedDataSource="rgEvaluadores_NeedDataSource" Height="100%" Width="100%" AllowSorting="true" AllowMultiRowSelection="true" OnItemDataBound="rgEvaluadores_ItemDataBound">
                    <ClientSettings EnablePostBackOnRowClick="false">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_EVALUADOR, FL_EVALUADOR, CL_TOKEN, NO_CUESTIONARIOS_CONTESTADOS, NO_CUESTIONARIOS">
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30px"></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn UniqueName="NB_EVALUADOR" DataField="NB_EVALUADOR" HeaderText="Evaluador">
                                <HeaderStyle Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto">
                                <HeaderStyle Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CL_CORREO_EVALUADOR" DataField="CL_CORREO_EVALUADOR" HeaderText="Correo electónico">
                                <HeaderStyle Font-Bold="true" />
                                <ItemTemplate>
                                    <telerik:RadTextBox runat="server" ID="txtCorreo" Width="100%" Text='<%# Bind("CL_CORREO_EVALUADOR") %>' AutoPostBack="false"></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>

            <div style="clear: both; height: 10px;"></div>
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
          </telerik:RadPageView>
            </telerik:RadMultiPage>
            </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
