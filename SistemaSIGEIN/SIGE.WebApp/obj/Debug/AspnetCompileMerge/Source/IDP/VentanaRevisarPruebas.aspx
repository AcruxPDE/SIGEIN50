<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaRevisarPruebas.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaRevisarPruebas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <script>

        function ConfirmarEliminarRespuestas(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    this.click();
                }
            });
            radconfirm("Este proceso borrará las respuestas de todas las pruebas de la batería ¿Desea continuar?", callBackFunction, 400, 180, null, "Eliminar respuestas batería");
            args.set_cancel(true);
        }

        function ConfirmarEliminarPrueba(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    this.click();
                }
            });
            radconfirm("Este proceso borrará las respuestas de la prueba seleccionada ¿Desea continuar?", callBackFunction, 400, 180, null, "Eliminar respuestas prueba");
            args.set_cancel(true);
        }

        function closeWindow() {
            GetRadWindow().close();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
      <div style="height: calc(100% - 50px);">
        <telerik:RadSplitter ID="rsRevisarPruebas" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="rpBotones" runat="server" Width="200px" Height="100%">
                <telerik:RadTabStrip ID="tbRevisarPruebas" runat="server" Align="Right" SelectedIndex="0" Width="100%" MultiPageID="mpgRevisarPruebas" Orientation="VerticalLeft" CssClass="divControlDerecha">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Personalidad laboral I" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Intereses personales" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Estilo de pensamiento" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Aptitud mental I" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Aptitud mental II" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Personalidad laboral II" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Adaptación al medio" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="TIVA" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Ortografía I" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Ortografía II" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Ortografía III" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Técnica PC" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Redacción" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Prueba de inglés" Enabled="false"></telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Factores adicionales" Enabled="false"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
            </telerik:RadPane>
            <telerik:RadSplitBar ID="rsbRevisarPruebas" runat="server" Width="100%" CollapseMode="Forward" EnableResize="false"></telerik:RadSplitBar>
            <telerik:RadPane ID="rpRevisarPruebas" runat="server">
                <telerik:RadMultiPage ID="mpgRevisarPruebas" runat="server" SelectedIndex="0" Height="100%">
                    <telerik:RadPageView ID="rpvLaboralI" runat="server">
                        <iframe id="ifLaboralI" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvInteresesPersonales" runat="server">
                        <iframe id="ifInteresesPersonales" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvEstilo" runat="server">
                        <iframe id="ifEstilo" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvMentalI" runat="server">
                        <iframe id="ifMentalI" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvMentalII" runat="server">
                        <iframe id="ifMentalII" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvLaboralII" runat="server">
                        <iframe id="ifLaboralII" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvAdáptacion" runat="server">
                        <iframe id="ifAdaptacion" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvTiva" runat="server">
                        <iframe id="ifTiva" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvOrtografiaI" runat="server">
                        <iframe id="ifOrtografiaI" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvOrtgrafiaII" runat="server">
                        <iframe id="ifOrtografiaII" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvOrtografiaIII" runat="server">
                        <iframe id="ifOrtografiaIII" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvTecnica" runat="server">
                        <iframe id="ifTecnica" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvRedaccion" runat="server">
                        <iframe id="ifRedaccion" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvIngles" runat="server">
                        <iframe id="ifIngles" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                     <telerik:RadPageView ID="rpvFactoresAdicionales" runat="server">
                        <iframe id="ifFactoresAdicionales" runat="server" frameborder="1"></iframe>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
     <div style="clear: both;"></div>
    <div class="ctrlBasico">            
         <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar batería" AutoPostBack="true" OnClientClicking="ConfirmarEliminarRespuestas" OnClick="btnEliminar_Click" Visible="true"></telerik:RadButton>
    </div>
      <div class="divControlDerecha">            
         <telerik:RadButton ID="btnEliminarRespuesta" runat="server" Text="Eliminar" AutoPostBack="true" OnClientClicking="ConfirmarEliminarPrueba" OnClick="btnEliminarRespuesta_Click" Visible="true"></telerik:RadButton>
    </div>
      <telerik:RadWindowManager ID="rwMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
