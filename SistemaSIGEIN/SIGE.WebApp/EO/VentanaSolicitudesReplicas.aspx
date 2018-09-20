<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaSolicitudesReplicas.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaSolicitudesReplicas" %>
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
    <div style="height: calc(100% - 10px);">
        <telerik:RadSplitter runat="server" ID="rsAyuda" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpDatos" runat="server">
                <div style="clear: both; height: 10px;"></div>
                <div style="height: calc(100% - 120px); width: 100%;">
                    <telerik:RadGrid runat="server" ID="rgCorreos" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" OnNeedDataSource="rgCorreos_NeedDataSource" Height="100%" Width="100%" AllowSorting="true" AllowMultiRowSelection="true">
                        <ClientSettings EnablePostBackOnRowClick="false">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_EVALUADOR,  FL_EVALUADOR, CL_TOKEN, ID_PERIODO, FE_ENVIO_SOLICITUD">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30px"></telerik:GridClientSelectColumn>
                                 <telerik:GridBoundColumn UniqueName="CL_PERIODO" DataField="CL_PERIODO" HeaderText="Período" HeaderStyle-Width="200"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CL_EVALUADOR" DataField="CL_EVALUADOR" HeaderText="Clave" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NB_EVALUADOR" DataField="NB_EVALUADOR" HeaderText="Nombre" HeaderStyle-Width="250"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="CL_CORREO_ELECTRONICO" DataField="CL_CORREO_ELECTRONICO" HeaderText="Correo electónico" HeaderStyle-Width="300">
                                    <ItemTemplate>
                                        <telerik:RadTextBox runat="server" ID="txtCorreo" Width="100%" Text='<%# Bind("CL_CORREO_ELECTRONICO") %>' AutoPostBack="false"></telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                      <label id="lbMensaje2" runat="server" visible="false" style="color: red;">*El evaluador para este periodo es el Coordinador, no se pueden enviar solicitudes, el coordinador realizará la captura ingresando a través del botón capturar.</label>
                </div>
                <div style="clear: both; height: 10px;"></div>

                     <div class="divControlDerecha">
                    <telerik:RadButton runat="server" ID="btnEnviar" Text="Enviar a seleccionados" OnClick="btnEnviar_Click" ></telerik:RadButton>
                    <telerik:RadButton runat="server" ID="btnEnviarTodos" Text="Enviar a todos" OnClick="btnEnviarTodos_Click"></telerik:RadButton>
                    <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
                </div>
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


