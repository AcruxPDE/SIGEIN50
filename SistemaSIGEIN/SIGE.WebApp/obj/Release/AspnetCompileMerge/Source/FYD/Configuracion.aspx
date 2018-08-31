<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/MenuFYD.master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="SIGE.WebApp.FYD.Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: calc(100% - 70px);">
        <label class="labelTitulo">Configuración</label>
        <telerik:RadTabStrip ID="rtsConfiguracion" runat="server" SelectedIndex="0" MultiPageID="rmpConfiguracion">
            <Tabs>
                <telerik:RadTab Text="Programa de capacitación"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <div style="height: 20px;"></div>
        <div style="height: calc(100% - 120px);">
            <telerik:RadMultiPage ID="rmpConfiguracion" runat="server" SelectedIndex="0" Height="100%">
                <telerik:RadPageView ID="rpvCapacitacion" runat="server">
                    <telerik:RadSplitter ID="spHelp" runat="server" Width="100%" Height="100%" BorderSize="0">
                        <telerik:RadPane ID="rpCapacitacion" runat="server">
                            <div class="ctrlBasico">
                                <label id="lbPrograma" runat="server">Programa de capacitación:</label>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnMacros" runat="server" ButtonType="ToggleButton" ToggleType="Radio" GroupName="Programa" Text="Macros"></telerik:RadButton>
                                <div style="height: 10px;"></div>
                                <telerik:RadButton ID="btnMatriz" runat="server" ButtonType="ToggleButton" ToggleType="Radio" GroupName="Programa" Text="Matriz"></telerik:RadButton>
                            </div>
                        </telerik:RadPane>
                        <telerik:RadPane ID="rpayuda" runat="server" Width="30">
                            <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                                <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="200" Height="350">
                                    <div style="padding-left: 10px; padding-right: 10px; padding-top: 20px;">
                                     En esta sección podrás configurar la manera como se mostrará el programa de capacitación al momento de su creación.
                                        <br /><br />
                                     Puedes elegir matriz o macros, al terminar no olvides guardar la configuración.
                                    </div>
                                </telerik:RadSlidingPane>
                            </telerik:RadSlidingZone>
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
        <div style="height: 10px; clear: both;"></div>
        <div class="divControlesBoton">
            <telerik:RadButton ID="btnAceptar" runat="server" Text="Guardar" AutoPostBack="true" OnClick="btnAceptar_Click"></telerik:RadButton>
        </div>
    </div>
        <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Height="100%"></telerik:RadWindowManager>
</asp:Content>
