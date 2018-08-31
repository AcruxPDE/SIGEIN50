<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="CuestionarioClimaLaboralExterno.aspx.cs" Inherits="SIGE.WebApp.EO.Cuestionarios.CuestionarioClimaLaboralExterno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function closeWindow() {
            sendDataToParent(null);
        }

        function confirmarFinalizar(sender, args) {

            var vWindowsProperties = { height: 180, width: 400 };

            confirmAction(sender, args, "¿Estás seguro que deseas terminar el cuestionario? No podrás volver a contestarlo.", vWindowsProperties);
        }

        function confirmarGuardar(sender, args) {
            confirmAction(sender, args, "¿Estás seguro que deseas guardar los datos y terminar la sesión?");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

     <telerik:RadAjaxLoadingPanel ID="ralpCuestionarioClimaLaboral" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramCuestionarioClimaLaboral" runat="server" DefaultLoadingPanelID="ralpCuestionarioClimaLaboral">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFinalizar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCuestionario" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadSplitter runat="server" ID="spHelp" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="rpDatos" runat="server">
            <div style="height: calc(100% - 50px); width: 100%; padding-top: 10px">
                <telerik:RadGrid runat="server" HeaderStyle-Font-Bold="true" ID="rgCuestionario" AutoGenerateColumns="false" Height="100%" Width="100%" AllowSorting="true" AllowMultiRowSelection="true" OnNeedDataSource="rgCuestionario_NeedDataSource">
                    <ClientSettings EnablePostBackOnRowClick="false" EnableAlternatingItems="false">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_CUESTIONARIO_PREGUNTA">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="#" HeaderStyle-Width="30" DataField="NO_SECUENCIA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Pregunta" HeaderStyle-Width="100" DataField="NB_PREGUNTA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
                                <ItemTemplate>
                                    <telerik:RadButton runat="server" ID="rbTotalmenteAcuerdo" Text="Totalmente de acuerdo" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR1") %>'>
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="125" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
                                <ItemTemplate>
                                    <telerik:RadButton runat="server" ID="rbCasiAcuerdo" Text="Casi siempre de acuerdo" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR2") %>'>
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="140" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
                                <ItemTemplate>
                                    <telerik:RadButton runat="server" ID="rbCasiDesacuerdo" Text="Casi siempre en desacuerdo" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR3") %>'>
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="130" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
                                <ItemTemplate>
                                    <telerik:RadButton runat="server" ID="rbTotalmenteDesacuerdo" Text="Totalmente en desacuerdo" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR4") %>'>
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                 <div style="height: 10px;"></div>
                 <telerik:RadGrid runat="server" ID="rgPreguntasAbiertas" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" Height="60%" Width="100%" AllowSorting="true" AllowMultiRowSelection="true" OnNeedDataSource="rgPreguntasAbiertas_NeedDataSource">
                    <ClientSettings EnablePostBackOnRowClick="false" EnableAlternatingItems="false">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_CUESTIONARIO_PREGUNTA">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="Preguntas abiertas" HeaderStyle-Width="250" DataField="NB_PREGUNTA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="200" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="txtRespuesta" runat="server" Width="400" TextMode="MultiLine" Height="50"></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        </telerik:RadGrid>
                <div style="height: 10px;"></div>
            <div class="divControlDerecha">
                <telerik:RadButton ID="btnFinalizar" runat="server" Text="Finalizar cuestionario" OnClientClicking="confirmarFinalizar" OnClick="btnFinalizar_Click" ></telerik:RadButton>
            </div>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpAyuda" runat="server" Width="30">
            <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="300" MinWidth="300" Height="100%">
                    <div style="padding: 10px; text-align: justify;">
                        <p>Bienvenido al cuestionario de Clima laboral.</p>
                        <p>
                            
                            Recuerda contestar todas las preguntas del cuestionario; una vez que hayas concluido haz clic en el botón "Finalizar cuestionario".
                        </p>
                        <p>Gracias.</p>
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>

