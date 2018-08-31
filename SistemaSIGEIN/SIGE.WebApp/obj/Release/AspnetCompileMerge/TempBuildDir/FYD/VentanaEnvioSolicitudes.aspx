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


    <telerik:RadSplitter runat="server" ID="spHelp" Width="100%" Height="100%" BorderSize="0">

        <telerik:RadPane ID="rpDatos" runat="server">

            <div style="clear: both; height: 10px;"></div>

           <%-- <div class="ctrlBasico">
                <label>Periodo: </label>
            </div>

            <div class="ctrlBasico">
                <telerik:RadTextBox runat="server" ID="txtNoPeriodo" Width="50px" Enabled="false"></telerik:RadTextBox>
            </div>

            <div class="ctrlBasico">
                <telerik:RadTextBox runat="server" ID="txtNbPeriodo" Width="300px" Enabled="false"></telerik:RadTextBox>
            </div>--%>
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
            <div style="clear: both; height: 10px;"></div>

            <div style="height: calc(100% - 120px); width: 100%;">
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

    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
