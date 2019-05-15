<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PDE/ContextPDE.master" CodeBehind="VentanaPlantillaFormulario.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaPlantillaFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
 <telerik:RadCodeBlock ID="rcbScripts" runat="server">
        <script type="text/javascript">
            function SeleccionCampo(sender, args) {
                __doPostBack('<%= vClickedItemEventName %>', args.get_item().get_value());
                //var ajaxManager = $find("< %= RadAjaxManager1.ClientID %>");
                //var a = args.get_item().get_value();
                //ajaxManager.ajaxRequest(args.get_item().get_value()); //Making ajax request with the argument
            }

            function closeWindow() {
                GetRadWindow().close();
            }
        </script>
    </telerik:RadCodeBlock>
    <style>
        .divControlIzquierda {
            padding-top: 7px !important;
            width: 70px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 35px); padding: 10px 10px 10px 10px;">
        <telerik:RadSplitter ID="rsPlantilla" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="rpCampos" runat="server">
                <div style="height: calc(100% - 35px);">
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="width:150px !important;">
                            <label name="lblNbPlantilla">Nombre de la plantilla</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtNbPlantilla" runat="server" MaxLength="100"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div class="divControlIzquierda" style="width:200px !important;">
                            <label name="lblDsPlantilla">Descripción de la plantilla</label>
                        </div>
                        <div class="divControlDerecha">
                            <telerik:RadTextBox ID="txtDsPlantilla" runat="server" MaxLength="500" Width="350"></telerik:RadTextBox>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="ctrlBasico">
                        <div>
                            <label name="lblDisponibles">Campos no visibles</label>
                        </div>
                        <div>
                            <telerik:RadListBox ID="lstCamposDisponibles" runat="server" EnableDragAndDrop="true" OnClientDropped="transferListItem" Width="180" Height="200"></telerik:RadListBox>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div>
                            <label name="lblDisponibles">Información personal</label>
                        </div>
                        <div>
                            <telerik:RadListBox ID="lstInformacionGeneral" runat="server" EnableDragAndDrop="true" OnClientItemDoubleClicked="SeleccionCampo" OnClientDropped="transferListItem" Width="180" Height="200"></telerik:RadListBox>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div>
                            <label name="lblDisponibles">Datos familiares</label>
                        </div>
                        <div>
                            <telerik:RadListBox ID="lstDatosFamiliares" runat="server" EnableDragAndDrop="true" OnClientItemDoubleClicked="SeleccionCampo" OnClientDropped="transferListItem" Width="180" Height="200"></telerik:RadListBox>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div>
                            <label name="lblDisponibles">Formación académica</label>
                        </div>
                        <div>
                            <telerik:RadListBox ID="lstFormacionAcademica" runat="server" EnableDragAndDrop="true" OnClientItemDoubleClicked="SeleccionCampo" OnClientDropped="transferListItem" Width="180" Height="200"></telerik:RadListBox>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div>
                            <label name="lblDisponibles">Experiencia profesional</label>
                        </div>
                        <div>
                            <telerik:RadListBox ID="lstExperienciaLaboral" runat="server" EnableDragAndDrop="true" OnClientItemDoubleClicked="SeleccionCampo" OnClientDropped="transferListItem" Width="180" Height="200"></telerik:RadListBox>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div>
                            <label name="lblDisponibles">Intereses y competencias</label>
                        </div>
                        <div>
                            <telerik:RadListBox ID="lstInteresesCompetencias" runat="server" EnableDragAndDrop="true" OnClientItemDoubleClicked="SeleccionCampo" OnClientDropped="transferListItem" Width="180" Height="200"></telerik:RadListBox>
                        </div>
                    </div>
                    <div class="ctrlBasico">
                        <div>
                            <label name="lblDisponibles">Información adicional</label>
                        </div>
                        <div>
                            <telerik:RadListBox ID="lstInformacionAdicional" runat="server" EnableDragAndDrop="true" OnClientItemDoubleClicked="SeleccionCampo" OnClientDropped="transferListItem" Width="180" Height="200"></telerik:RadListBox>
                        </div>
                    </div>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px">
                <telerik:RadSlidingZone ID="rszAuxiliares" runat="server" SlideDirection="Left" DockedPaneId="rspInformacionCampo" Width="22px">
                    <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="200">
                        <div style="padding: 10px; text-align: justify;">
                            <p>Esta página te permite editar las plantillas de los formularios de solicitudes de empleo e inventario de personal por lo cual puedes adecuarla a tus necesidades específicas.</p>
                            <p>Selecciona los campos que deseas que aparezcan en tu formulario, establece los de llenado "obligatorio".</p>
                            <p></p>
                        </div>
                    </telerik:RadSlidingPane>
                    <telerik:RadSlidingPane ID="rspInformacionCampo" runat="server" Title="Campo" Width="315">
                        <div style="padding: 10px;">
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    Identificador
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtIdCampo" runat="server" ReadOnly="true" Width="200"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    Nombre
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtNbCampo" runat="server" Width="200"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    Tooltip
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadTextBox ID="txtDsTooltip" runat="server" InputType="Text" TextMode="MultiLine" Width="200" Height="100" Resize="Vertical"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    Habilitado
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadButton ID="chkHabilitado" runat="server" ToggleType="CheckBox" name="chkHabilitado" AutoPostBack="false">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div class="ctrlBasico">
                                <div class="divControlIzquierda">
                                    Obligatorio
                                </div>
                                <div class="divControlDerecha">
                                    <telerik:RadButton ID="chkRequerido" runat="server" ToggleType="CheckBox" name="chkRequerido" AutoPostBack="false">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div>
                                <telerik:RadButton ID="btnAplicar" runat="server" Text="Aplicar cambios al campo" OnClick="btnAplicar_Click" Enabled="false"></telerik:RadButton>
                            </div>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div>
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
