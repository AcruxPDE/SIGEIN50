<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaEnvioSolicitudes.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaEnvioSolicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .RadButton.rbSkinnedButton.uncheckedYes {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.uncheckedYes > .rbDecorated {
                color: #eee !important;
            }

        .RadButton.rbSkinnedButton.checkedNo {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.checkedNo > .rbDecorated {
                color: #333 !important;
            }

        .RadButton.rbSkinnedButton.uncheckedNo {
            background-color: #eee !important;
        }

            .RadButton.rbSkinnedButton.uncheckedNo > .rbDecorated {
                color: #eee !important;
            }

        .checkContainer {
            border-radius: 5px;
            border: 1px solid lightgray;
            background: #eee;
        }
    </style>
    <script>
        function closeWindow() {
            GetRadWindow().close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpEnvioSolicitudes" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramEnvioSolicitudes" runat="server" DefaultLoadingPanelID="ralpEnvioSolicitudes">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnEnviar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCorreos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEnviarTodos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCorreos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 10px);">
        <telerik:RadSplitter runat="server" ID="rsAyuda" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpDatos" runat="server">

                <div style="clear: both; height: 10px;"></div>

                <telerik:RadTabStrip ID="rtsEnvioSolicitudes" runat="server" SelectedIndex="0" MultiPageID="rmpSolicitudes">
                    <Tabs>
                        <telerik:RadTab Text="Contexto"></telerik:RadTab>
                        <telerik:RadTab Text="Envío de solicitudes"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: calc(100% - 100px); width: 100%;">
                    <telerik:RadMultiPage ID="rmpSolicitudes" runat="server" SelectedIndex="0" Height="100%">

                        <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%">
                            <div class="divControlIzquierda" style="width: 60%; text-align: left;">
                                <table class="ctrlTableForm">
                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <label>Período</label></td>
                                        <td colspan="2" class="ctrlTableDataBorderContext">
                                            <div id="txtClPeriodo" runat="server"></div>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td class="ctrlTableDataContext">
                                            <label>Nombre del periodo:</label></td>
                                        <td colspan="2" class="ctrlTableDataBorderContext">
                                            <div id="txtNbPeriodo" runat="server"></div>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <label>Descripción:</label></td>
                                        <td colspan="2" class="ctrlTableDataBorderContext">
                                            <div id="txtPeriodos" runat="server"></div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <label>Notas:</label></td>
                                        <td class="ctrlTableDataBorderContext">
                                            <div id="txtNotas" runat="server" width="170" maxlength="1000" enabled="false"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <label>Fecha:</label></td>
                                        <td colspan="2" class="ctrlTableDataBorderContext">
                                            <div id="txtFechas" runat="server"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <label>Tipo de período:</label></td>
                                        <td class="ctrlTableDataBorderContext">
                                            <div id="txtTipoPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <label>Tipo de bono:</label></td>
                                        <td class="ctrlTableDataBorderContext">
                                            <div id="txtTipoBono" runat="server" width="170" maxlength="1000" enabled="false"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <label>Tipo de metas:</label></td>
                                        <td colspan="2" class="ctrlTableDataBorderContext">
                                            <div id="txtTipoMetas" runat="server"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ctrlTableDataContext">
                                            <label>Tipo de capturista:</label></td>
                                        <td colspan="2" class="ctrlTableDataBorderContext">
                                            <div id="txtTipoCapturista" runat="server"></div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvEnvio" runat="server">
                            <div style="clear: both; height: 5px;"></div>
                            <div style="height: calc(100% - 30px); width: 100%;">
                                <telerik:RadGrid runat="server" ID="rgCorreos" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" OnItemDataBound="rgCorreos_ItemDataBound" OnNeedDataSource="rgCorreos_NeedDataSource" Height="100%" Width="100%" AllowSorting="true" AllowMultiRowSelection="true">
                                    <ClientSettings EnablePostBackOnRowClick="false">
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_EVALUADOR,  FL_EVALUADOR, CL_TOKEN">
                                        <Columns>
                                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30px"></telerik:GridClientSelectColumn>
                                            <telerik:GridBoundColumn UniqueName="CL_EVALUADOR" DataField="CL_EVALUADOR" HeaderText="No. de empleado" HeaderStyle-Width="100" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="NB_EVALUADOR" DataField="NB_EVALUADOR" HeaderText="Nombre completo" HeaderStyle-Width="300" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="CL_CORREO_ELECTRONICO" DataField="CL_CORREO_ELECTRONICO" HeaderText="Correo electónico" HeaderStyle-Width="300" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox runat="server" ID="txtCorreo" Width="100%" Text='<%# Bind("CL_CORREO_ELECTRONICO") %>' AutoPostBack="false"></telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <label id="lbMensaje2" runat="server" visible="false" style="color: red;">*El evaluador para este periodo es el Coordinador, no se pueden enviar solicitudes, el coordinador realizará la captura ingresando a través del botón capturar.</label>
                                <label id="lbMensaje" runat="server" visible="false" style="color: red;">*Los evaluadores inhabilitados ya han calificado las metas de los evaluados.</label>
                            </div>
                            <div style="clear: both; height: 20px;"></div>
                            <div class="divControlDerecha">
                                <div id="dvCapturaMasiva" runat="server" visible="false">
                                    <div class="ctrlBasico">
                                        <label id="lbMasiva">Habilitar captura masiva:</label>
                                    </div>
                                    <div class="ctrlBasico">
                                        <div class="checkContainer">
                                            <telerik:RadButton ID="btnCapturaMasivaYes" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCapturaMasiva" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="checkedYes"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="Sí" CssClass="uncheckedYes"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="btnCapturaMasivaFalse" runat="server" ToggleType="Radio" ButtonType="StandardButton" GroupName="grpCapturaMasiva" AutoPostBack="false">
                                                <ToggleStates>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="checkedNo"></telerik:RadButtonToggleState>
                                                    <telerik:RadButtonToggleState Text="No" CssClass="uncheckedNo"></telerik:RadButtonToggleState>
                                                </ToggleStates>
                                            </telerik:RadButton>
                                        </div>
                                    </div>
                                </div>
                                <telerik:RadButton runat="server" ID="btnEnviar" Text="Enviar a seleccionados" OnClick="btnEnviar_Click"></telerik:RadButton>
                                <telerik:RadButton runat="server" ID="btnEnviarTodos" Text="Enviar a todos" OnClick="btnEnviarTodos_Click"></telerik:RadButton>
                                <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
                            </div>
                        </telerik:RadPageView>

                    </telerik:RadMultiPage>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Width="30">
                <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="500" MinWidth="500" Height="100%">
                        <div style="padding: 10px; text-align: justify;">
                            Este proceso envía una solicitud para evaluación de cuestionarios a cada uno de los correos electrónicos de los evaluadores. 
                        Un ejemplo del cuerpo del mensaje se muestra en la pantalla, si deseas modificarlo deberás hacerlo en el menú de configuración del sistema. 
                        Por favor indica el número de período a evaluar.
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
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>

