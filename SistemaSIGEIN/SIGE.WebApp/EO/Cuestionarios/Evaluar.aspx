<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Evaluar.aspx.cs" Inherits="SIGE.WebApp.EO.Cuestionarios.Evaluar" MasterPageFile="~/EO/ContextEO.master" %>

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

            var windowProperties = {
                width: 400,
                height: 200
            };

            confirmAction(sender, args, "¿Deseas finalizar el cuestionario?.", windowProperties);
        }


        function confirmarTerminar(sender, args) {

            var windowProperties = {
                width: 400,
                height: 200
            };

            confirmAction(sender, args, "¿Deseas terminar el cuestionario? Esta opción guarda y marca como contestado el cuestionario.", windowProperties);
        }
    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadToolTipManager ID="rtmEvaluacion" runat="server" AutoTooltipify="true" BackColor="WhiteSmoke" RelativeTo="Element" AutoCloseDelay="60000" Position="TopCenter" Width="300" HideEvent="LeaveTargetAndToolTip">
    </telerik:RadToolTipManager>
    <telerik:RadAjaxLoadingPanel ID="ralpEvaluar" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxManager ID="ramEvaluar" runat="server" DefaultLoadingPanelID="ralpEvaluar">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAnt">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rsPlantilla" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNext">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rsPlantilla" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <div style="height: calc(100% - 40px); padding-top: 10px;">
        <telerik:RadSplitter ID="rsPlantilla" Width="100%" Height="100%" runat="server" BorderSize="0">
            <telerik:RadPane ID="rpGridEvaluar" runat="server">
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


                <label class="labelTitulo">Se debe responder a 10 preguntas (calificación: 1 es el mínimo y 5 el máximo).</label>

                <div style="clear: both;"></div>
                <label class="labelTitulo">1. Evaluación por parte del jefe inmediato sobre el cumplimento de las funciones del área contenidas en el Manual de Organización General</label>

                <div id="divCompetencias" runat="server" style="height: 220px; display: block;">
                    <telerik:RadGrid ID="dgvCompetencias" HeaderStyle-Font-Bold="true" runat="server" AutoGenerateColumns="False" Width="100%" Height="100%" OnNeedDataSource="dgvCompetencias_NeedDataSource" OnDataBound="dgvCompetencias_DataBound">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        </ClientSettings>
                        <MasterTableView ShowHeadersWhenNoRecords="true" Name="Evaluación" DataKeyNames="ID_CUESTIONARIO_PREGUNTA">
                            <Columns>
                                <telerik:GridBoundColumn DataField="DS_PREGUNTA" HeaderText="Pregunta" UniqueName="DS_PREGUNTA" ReadOnly="true" Visible="true"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="FG_VALOR1" UniqueName="FG_VALOR1" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            1
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            2
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            3
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            4
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            5
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
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <br />
                <%-------------------------FACTOR B------------------------ --%>
                <label class="labelTitulo">2. Evaluación por el jefe inmediato del desempeño laboral del funcionario (innovación, cooperación y disponibilidad para el trabajo)</label>


                <div style="clear: both;"></div>
                <div id="div1" runat="server" style="height: 220px; display: block;">
                    <telerik:RadGrid ID="dgvCompetenciasfactorB" HeaderStyle-Font-Bold="true" runat="server" AutoGenerateColumns="False" Width="100%" Height="100%" OnNeedDataSource="dgvCompetenciasfactorB_NeedDataSource">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        </ClientSettings>
                        <MasterTableView ShowHeadersWhenNoRecords="true" Name="Evaluación" DataKeyNames="ID_CUESTIONARIO_PREGUNTA">
                            <Columns>
                                <telerik:GridBoundColumn DataField="DS_PREGUNTA" HeaderText="Pregunta" UniqueName="DS_PREGUNTA" ReadOnly="true" Visible="true"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="FG_VALOR1" UniqueName="FG_VALOR1" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            1
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            2
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            3
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            4
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            5
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
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

                <br />
                <%-------------------------FACTOR C------------------------ --%>
                <label class="labelTitulo">3. Evaluación por el jefe inmediato de la conducta del funcionario y de su ética en el servicio</label>

                <div style="clear: both;"></div>
                <div id="div2" runat="server" style="height: 280px; display: block;">
                    <telerik:RadGrid ID="dgvCompetenciasfactorC" HeaderStyle-Font-Bold="true" runat="server" AutoGenerateColumns="False" Width="100%" Height="100%" OnNeedDataSource="dgvCompetenciasfactorC_NeedDataSource">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        </ClientSettings>
                        <MasterTableView ShowHeadersWhenNoRecords="true" Name="Evaluación" DataKeyNames="ID_CUESTIONARIO_PREGUNTA">
                            <Columns>
                                <telerik:GridBoundColumn DataField="DS_PREGUNTA" HeaderText="Pregunta" UniqueName="DS_PREGUNTA" ReadOnly="true" Visible="true"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="FG_VALOR1" UniqueName="FG_VALOR1" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            1
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            2
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            3
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            4
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            5
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
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

                <br />
                <%-------------------------FACTOR D------------------------ --%>
               <%-- <label class="labelTitulo">4. Puntualidad y asistencia</label>

                <div style="clear: both;"></div>
                <div id="div3" runat="server" style="height: 180px; display: block;">
                    <telerik:RadGrid ID="dgvCompetenciasfactorD" runat="server" AutoGenerateColumns="False" Width="100%" Height="100%" OnNeedDataSource="dgvCompetenciasfactorD_NeedDataSource">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        </ClientSettings>
                        <MasterTableView ShowHeadersWhenNoRecords="true" Name="Evaluación" DataKeyNames="ID_CUESTIONARIO_PREGUNTA">
                            <Columns>
                                <telerik:GridBoundColumn DataField="DS_PREGUNTA" HeaderText="Pregunta" UniqueName="DS_PREGUNTA" ReadOnly="true" Visible="true"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="FG_VALOR1" UniqueName="FG_VALOR1" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            1
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            2
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            3
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            4
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
                                    <HeaderTemplate>
                                        <div style="width: 100%; height: 100%; text-align: center;">
                                            5
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
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>--%>
                <div id="divCamposExtras" runat="server" style="height: calc(100% - 200px); width: 100%; display: none;"></div>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>

    <div class="divControlDerecha" style="padding-right: 10px;">
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClientClicking="confirmarGuardar" OnClick="btnGuardar_Click"></telerik:RadButton>
    </div>


    <div class="divControlDerecha" style="margin: 10px 10px 0px 0px">
        <span class="labelSubtitulo" runat="server" id="lblResultado">0%</span>
    </div>

    <div class="divControlDerecha" style="margin: 10px 10px 0px 0px">
        <label class="labelSubtitulo">RESULTADO:</label>
    </div>

    <div class="divControlDerecha" style="margin-right: 10px">
        <telerik:RadButton ID="btnCalcular" runat="server" Text="Calcular" OnClick="btnCalcular_Click"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
