<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaEnvioSolicitud.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaEnvioSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function CloseWindow() {
            GetRadWindow().close();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAgregarCorreo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCorreos" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="txbCorreo" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: 15px; clear: both;"></div>
    <div style="height: calc(100% - 20px)">
        <telerik:RadSplitter ID="rsEnvioCorreo" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="rpCampos" runat="server">
                <div style="height: calc(100% - 60px);">
                    <div class="ctrlBasico" style="width: 55%;">
                        <label >Mensaje:</label>
                        <telerik:RadEditor
                            Width="95%"
                            Height="430px"
                            EditModes="Design"
                            ID="txbPrivacidad"
                            runat="server"
                            ToolbarMode="Default"
                            ToolsFile="~/Assets/AdvancedTools.xml">
                        </telerik:RadEditor>
                    </div>
                    <div class="ctrlBasico" style="width: 38%;">
                        <div style="clear: both;"></div>
                        <div class="ctrlBasico">
                            <div class="divControlIzquierda" style="width: 60px !important;">
                                <label id="Label14" name="lblCorreo" runat="server">Correo:</label>
                            </div>
                            <div class="divControlDerecha">
                                <div class="ctrlBasico">
                                    <telerik:RadTextBox ID="txbCorreo" runat="server" Width="220px" MaxLength="1000">
                                    </telerik:RadTextBox>
                                </div>
                                <div class="ctrlBasico">
                                    <telerik:RadButton ID="btnAgregarCorreo" runat="server" name="btnAgregar" AutoPostBack="true" Text="Agregar" Width="100" OnClick="btnAgregarCorreo_Click"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div style="clear: both"></div>
                        <div style="width: 100%; margin-left: 75px;">
                            <telerik:RadGrid ID="grdCorreos" runat="server" Width="80%" Height="400px"
                                AutoGenerateColumns="true" AllowSorting="true" HeaderStyle-Font-Bold="true" OnItemCommand="grdCorreos_ItemCommand" OnNeedDataSource="grdCorreos_NeedDataSource">
                                <ClientSettings AllowKeyboardNavigation="false">
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView AutoGenerateColumns="false" DataKeyNames="NB_MAIL">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderStyle-Width="90%" HeaderText="Correo" DataField="NB_MAIL" UniqueName="NB_MAIL"></telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" HeaderStyle-Width="10%" ButtonType="ImageButton"></telerik:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </div>
                            <div class="ctrlBasico">
                    <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" OnClientClicked="CloseWindow"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click"></telerik:RadButton>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px">
                <telerik:RadSlidingZone ID="rszAuxiliares" runat="server" SlideDirection="Left" Width="25px">
                    <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="200px" RenderMode="Mobile" Height="100%">
                        <div style="padding: 10px; text-align: justify;">
                            <p>Envío Correos</p>
                            <br /><br />
                            <p>En esta página te permite enviar por correo electrónico la liga al acceso de una solicitud utilizando la plantilla seleccionada.
                                <br />
                                Puedes configurar el mensaje que recibirán los candidatos y agregar los correos a quienes se les enviara el acceso a la solicitud.
                                <br />
                                <br />
                                Si deseas salir de la ventana actual sin enviar solicitudes selecciona cancelar, de lo contrario enviar. 
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Height="100%"></telerik:RadWindowManager>
</asp:Content>
