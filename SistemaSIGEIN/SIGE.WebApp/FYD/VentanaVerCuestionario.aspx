<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaVerCuestionario.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaVerCuestionario" %>
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

        function closeWindow() {
            sendDataToParent(null);
        }

        function OpenImpresion() {
            var myWindow = window.open("/FYD/CuestionarioImpresion.aspx?ID_EVALUADOR=" + '<%= pIdEvaluador %>' + "&ID_EVALUADO_EVALUADOR=" + '<%= pIdEvaluadoEvaluador %>', "MsgWindow", "width=650,height=650");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadToolTipManager ID="rtmEvaluacion" runat="server" AutoTooltipify="true" BackColor="WhiteSmoke" RelativeTo="Element" AutoCloseDelay="60000" Position="TopCenter" Width="300" HideEvent="LeaveTargetAndToolTip">
    </telerik:RadToolTipManager>
    <div id="imprimir" runat="server" style="height: calc(100% - 40px); padding-top: 10px;">
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
                    <telerik:RadGrid ID="dgvCompetencias" HeaderStyle-Font-Bold="true" runat="server" AutoGenerateColumns="False" Width="100%" Height="80%" OnNeedDataSource="dgvCompetencias_NeedDataSource" OnDataBound="dgvCompetencias_DataBound">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        </ClientSettings>
                         <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ShowHeadersWhenNoRecords="true" Name="Evaluación" DataKeyNames="ID_CUESTIONARIO_PREGUNTA">
                            <Columns>
                                          <telerik:GridTemplateColumn DataField="CL_COLOR" UniqueName="CL_COLOR" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" HeaderText="Color">
                                    <ItemStyle Width="10px" Height="15px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="50px" Height="20px" />
                                    <ItemTemplate>
                                        <div class="ctrlBasico" style="height: 60px; width: 30px; float: left; background: <%# Eval("CL_COLOR") %>; border-radius: 5px;"></div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
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
                        <div style="clear: both; height:10px;"></div>
                <div id="divCamposExtras" runat="server" style="width: 100%; display: block;"></div>
<%--                <div style="clear: both; height:10px;"></div>
        <div class="divControlDerecha">
                    <div class="ctrlBasico" style="padding-left: 5px;">
                        <telerik:RadButton runat="server" ID="btnImprimir" Text="Imprimir" AutoPostBack="false" OnClientClicked="OpenImpresion"></telerik:RadButton>
                    </div>
            </div>--%>
    </div>
</asp:Content>
