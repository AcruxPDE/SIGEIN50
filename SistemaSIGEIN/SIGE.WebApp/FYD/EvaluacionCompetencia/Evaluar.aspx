<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Evaluar.aspx.cs" Inherits="SIGE.WebApp.FYD.EvaluacionCompetencia.Evaluar" MasterPageFile="~/FYD/ContextFYD.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <style>
        .lblNivel span {
            border: 1px solid gray;
            padding: 2px 7px !important;
            background-color: white;
            border-radius: 14px;
        }

        .rslItemsWrapper {
            z-index: 10 !important;
        }

        .rbToggleButton {
            line-height: 15px;
        }

        .RadProgressBar .rpbStateSelected, .RadProgressBar .rpbStateSelected:hover, .RadProgressBar .rpbStateSelected:link, .RadProgressBar .rpbStateSelected:visited {
            background-color: #FF7400 !important;
            border: 0px solid black !important;
            color: black !important;
        }

        .MostrarCelda {
            border: 1px solid lightgray;
            border-radius: 5px;
            padding-left: 10px;
            padding-right: 10px;
        }

        .OcultarCelda {
            border: 1px solid white;
            border-radius: 5px;
            padding-left: 10px;
            padding-right: 10px;
        }
    </style>

    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if 
                (window.radWindow) oWindow = window.radWindow;
            else
                if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function closeWindow() {
            sendDataToParent(null);
        }

        function confirmarGuardar(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

            radconfirm("¿Deseas guardar el cuestionario? Esta opción solo guarda los datos y cierra la pantalla para poder terminarlo posteriormente.", callBackFunction, 400, 200, null, "Aviso");
            args.set_cancel(true);
        }

        function confirmarTerminar(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

            radconfirm("¿Deseas terminar el cuestionario? Esta opción guarda y marca como contestado el cuestionario.", callBackFunction, 400, 200, null, "Aviso");
            args.set_cancel(true);
        }
    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadToolTipManager ID="rtmEvaluacion" runat="server" AutoTooltipify="true" BackColor="WhiteSmoke" RelativeTo="Element" AutoCloseDelay="60000" Position="TopCenter" Width="300" HideEvent="LeaveTargetAndToolTip">
        <%--        <TargetControls>
            <telerik:ToolTipTargetControl TargetControlID="dgvCompetencias" />
        </TargetControls>--%>
    </telerik:RadToolTipManager>
    <telerik:RadAjaxLoadingPanel ID="ralpEvaluar" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="ramEvaluar" runat="server" DefaultLoadingPanelID="ralpEvaluar">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAnt">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rsPlantilla" UpdatePanelHeight="100%" />
                    <%--<telerik:AjaxUpdatedControl ControlID="dgvCompetencias" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="txtNbClasificacion" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="txtDsSignificado" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="divColorClas" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="cpbEvaluacion" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="divCamposExtras" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="divCompetencias" UpdatePanelHeight="100%" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNext">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rsPlantilla" UpdatePanelHeight="100%" />
                    <%--<telerik:AjaxUpdatedControl ControlID="dgvCompetencias" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="txtNbClasificacion" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="txtDsSignificado" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="divColorClas" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="cpbEvaluacion" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="divCamposExtras" UpdatePanelHeight="100%" />--%>
                    <%--<telerik:AjaxUpdatedControl ControlID="divCompetencias" UpdatePanelHeight="100%" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <div style="height: calc(100% - 40px); padding-top: 10px;">
        <telerik:RadSplitter ID="rsPlantilla" Width="100%" Height="100%" runat="server" BorderSize="0">
            <telerik:RadPane ID="rpGridEvaluar" runat="server">

                <%--   <div class="ctrlBasico">
                    <telerik:RadLabel ID="lblNbPeriodo" runat="server" Text="Periodo:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox runat="server" ID="txtNoPeriodo" Width="80" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox runat="server" ID="txtNbPeriodo" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadLabel ID="RadLabel3" runat="server" Text="Persona a evaluar:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox runat="server" ID="txtNombreEvaluado" Width="315" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div style="clear: both;"></div>
                <div class="ctrlBasico">
                    <telerik:RadLabel ID="lblNbEvaluador" runat="server" Text="Evaluador:"></telerik:RadLabel>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox runat="server" ID="txtEvaluador" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox runat="server" ID="txtPuestoEvaluado" Width="300" ReadOnly="true"></telerik:RadTextBox>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadTextBox runat="server" ID="txtTipo" Width="215" ReadOnly="true"></telerik:RadTextBox>
                </div>--%>

                <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Período:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtNoPeriodo" runat="server" style="min-width: 100px;"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNbPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Persona a evaluar:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtNombreEvaluado" runat="server" style="min-width: 100px;"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtPuestoEvaluado" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="ctrlBasico">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Evaluador:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtEvaluador" runat="server" style="min-width: 100px;"></div>
                            </td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTipo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                    </table>
                </div>


                <div style="clear: both;"></div>

                <label class="labelSubtitulo">Progreso de la evaluación</label>
                <div>
                    <telerik:RadProgressBar runat="server" ID="cpbEvaluacion" BarType="Percent" Width="100%" MinValue="0"></telerik:RadProgressBar>
                </div>

                <table runat="server" id="tblCompetencia" class="ctrlTableForm" style="width: 100%">
                    <tr>
                        <td style="width: 90px;">
                            <telerik:RadButton ID="btnAnt" runat="server" Text="Anterior" Width="90px" OnClick="btnAnt_Click"></telerik:RadButton>
                        </td>
                        <td style="width: 35px;">
                            <div runat="server" id="divColorClas" style="width: 35px; height: 35px; border-radius: 5px;">
                                <br />
                            </div>
                        </td>
                        <td runat="server" id="tdClasificacion" class="MostrarCelda">
                            <span id="txtNbClasificacion" runat="server" style="width: 300px;"></span>
                        </td>
                        <td runat="server" id="tdSignificado" class="MostrarCelda">
                            <div id="txtDsSignificado" runat="server" style="max-height: 50px; overflow: auto;"></div>
                        </td>
              
                        <td style="width: 90px;">
                            <telerik:RadButton ID="btnNext" runat="server" Text="Siguiente" Width="90px" OnClick="btnNext_Click"></telerik:RadButton>
                        </td>
                    </tr>
                </table>
                <div style="clear: both; height:10px;"></div>
                <div id="divCompetencias" runat="server" style="height: 500px; display: block;">
                    <telerik:RadGrid ID="dgvCompetencias" HeaderStyle-Font-Bold="true" runat="server" AutoGenerateColumns="False" Width="100%" Height="100%" OnNeedDataSource="dgvCompetencias_NeedDataSource" OnDataBound="dgvCompetencias_DataBound">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        </ClientSettings>
                         <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ShowHeadersWhenNoRecords="true" Name="Evaluación" DataKeyNames="ID_CUESTIONARIO_PREGUNTA">
                            <Columns>
                                <telerik:GridTemplateColumn DataField="NB_COMPETENCIA" HeaderText="Competencia" UniqueName="NB_COMPETENCIA" ReadOnly="true" HeaderStyle-Width="200">
                                    <HeaderStyle Font-Bold="true" />
                                    <ItemTemplate>
                                        <div><%# Eval("NB_PREGUNTA") %></div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="FG_VALOR0" UniqueName="FG_VALOR0" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="true" />
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            0<br />
                                            0%
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadButton runat="server" ID="rbNivel0" Text="0" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR0") %>'>
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="FG_VALOR1" UniqueName="FG_VALOR1" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="true" />
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            1<br />
                                            20%
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadButton runat="server" ID="rbNivel1" Text="1" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR1") %>'>
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="FG_VALOR2" UniqueName="FG_VALOR2" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="true" />
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            2<br />
                                            40%
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadButton runat="server" ID="rbNivel2" Text="2" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR2") %>'>
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="FG_VALOR3" UniqueName="FG_VALOR3" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="true" />
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            3<br />
                                            60%
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadButton runat="server" ID="rbNivel3" Text="3" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR3") %>'>
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="FG_VALOR4" UniqueName="FG_VALOR4" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="true" />
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            4<br />
                                            80%
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadButton runat="server" ID="rbNivel4" Text="4" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR4") %>'>
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="FG_VALOR5" UniqueName="FG_VALOR5" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle Font-Bold="true" />
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            5<br />
                                            100%
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadButton runat="server" ID="rbNivel5" Text="5" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR5") %>'>
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="DS_PREGUNTA" HeaderText="Descripción de la competencia" UniqueName="DS_PREGUNTA" ReadOnly="true" Visible="true">
                                    <HeaderStyle Font-Bold="true" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div id="divCamposExtras" runat="server" style="height: calc(100% - 200px); width: 100%; display: none;"></div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyudas" runat="server" Scrolling="None" Width="22px">
                <telerik:RadSlidingZone ID="rszMensajeInicial" runat="server" SlideDirection="Left" DockedPaneId="rspInstrucciones" Width="22px">
                    <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Instrucciones de llenado" Width="340px">
                        <div style="padding: 10px; text-align: justify;" id="ayuda" runat="server">
                            El cuestionario está organizado en clasificaciones de competencias y las competencias propias de cada clasificación<br />
                            Desplázate entre clasificaciones con los botones "Anterior" y "Siguiente"<br />
                            Responde a cada competencia con la escala 0 al 5 dando un click en la celda correspondiente, ésta se marcara de color indicando la calificación<br />
                            Puedes guardar tus respuestas en cualquier momento pulsando el botón Guardar<br />
                            <br />
                            Al concluir presiona el botón Terminado, debes evaluar todas las competencias para dar por terminado el cuestionario
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div class="divControlDerecha">
        <telerik:RadButton ID="btnTerminar" runat="server" Text="Terminar" OnClientClicking="confirmarTerminar" OnClick="btnTerminar_Click"></telerik:RadButton>
    </div>
    <div class="divControlDerecha" style="padding-right: 10px;">
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClientClicking="confirmarGuardar" OnClick="btnGuardar_Click"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
