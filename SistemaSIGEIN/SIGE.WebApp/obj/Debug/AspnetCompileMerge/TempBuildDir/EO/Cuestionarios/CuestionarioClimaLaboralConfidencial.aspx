<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="CuestionarioClimaLaboralConfidencial.aspx.cs" Inherits="SIGE.WebApp.EO.Cuestionarios.CuestionarioClimaLaboralConfidencial" %>
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


        //Eliminar el tooltip del control
        function pageLoad() {
            var datePicker = $find("<%=rdpIngreso.ClientID %>");
            if (datePicker != null)
            datePicker.get_popupButton().title = "";
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
            <div style="height: calc(100% - 50px); width: 100%;">
                <div class="ctrlBasico">
                     <table class="ctrlTableForm" text-align: left;">
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label id="lbPeriodo" name="lbTabulador" runat="server">Período:</label>
                                </td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtNoPeriodo" runat="server"></div>
                                </td>
                            </tr>
                     </table>
                </div>
                 <div style="clear: both; height: 5px"></div>
                 <div class="ctrlBasico">
                                    <div class="divControlIzquierda">
                                        <label id="lbEdad" name="lblEdad" runat="server" visible ="false">Edad:</label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadNumericTextBox runat="server" ID="rntEdad" Visible="false" NumberFormat-DecimalDigits="0" Name="rnEdad" Width="70px"  ShowSpinButtons="true"></telerik:RadNumericTextBox>
                                    </div>
                                </div>
                       <div class="ctrlBasico">
                                    <div class="divControlIzquierda">
                                        <label id="lbGenero" name="lbGenero" runat="server" visible="false">Género:</label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadComboBox runat="server" AutoPostBack="false" Visible="false" MarkFirstMatch="true" EmptyMessage="Selecciona" Width="150" ID="cmbGenero"></telerik:RadComboBox>
                                    </div>
                                </div>
                                       <div class="ctrlBasico">
                                    <div class="divControlIzquierda">
                                        <label id="lbArea" name="lbArea" runat="server" visible="false">Área/Departamento:</label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadComboBox runat="server" AutoPostBack="false" Visible="false" MarkFirstMatch="true" EmptyMessage="Selecciona" Width="250" ID="rcbArea"></telerik:RadComboBox>
                                    </div>
                                </div>
                               <div class="ctrlBasico">
                                    <div class="divControlIzquierda">
                                        <label id="Label1" name="lblEdad" runat="server" visible="false">Fecha de ingreso:</label>
                                    </div>
                                    <div class="divControlDerecha">
                                        <telerik:RadDatePicker ID="rdpIngreso" runat="server" Visible="false" Width="130"></telerik:RadDatePicker>
                                        <%--<telerik:RadNumericTextBox runat="server" ID="rntAntiguedad" NumberFormat-DecimalDigits="0" Name="rntAntiguedad" Width="70px"  ShowSpinButtons="true"></telerik:RadNumericTextBox>--%>
                                    </div>
                                </div>
                 <div style="height:5px; clear:both;"></div>
                <div id="dvCamposExtra" runat="server">

                </div>
                <div style="height:5px; clear:both;"></div>
                <telerik:RadGrid runat="server" HeaderStyle-Font-Bold="true" ID="rgCuestionario" AutoGenerateColumns="false" Height="100%" Width="100%" AllowSorting="true" AllowMultiRowSelection="true" OnNeedDataSource="rgCuestionario_NeedDataSource">
                    <ClientSettings EnablePostBackOnRowClick="false" EnableAlternatingItems="false">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_CUESTIONARIO_PREGUNTA">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="#" HeaderStyle-Width="30" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="NO_SECUENCIA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
<<<<<<< HEAD
                      <telerik:GridBoundColumn HeaderText="Pregunta" HeaderStyle-Width="400" DataField="NB_PREGUNTA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" HeaderText="Totalmente de acuerdo">
=======
                            <telerik:GridBoundColumn HeaderText="Pregunta" HeaderStyle-Width="100" DataField="NB_PREGUNTA" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="120" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
>>>>>>> DEV
                                <ItemTemplate>
                                    <telerik:RadButton runat="server" ID="rbTotalmenteAcuerdo" ButtonType="ToggleButton"  ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR1") %>'>
                                       <%-- <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>--%>
                                    </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" HeaderText="Casi siempre de acuerdo">
                                <ItemTemplate>
                                    <telerik:RadButton runat="server" ID="rbCasiAcuerdo" ButtonType="ToggleButton"  ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR2") %>'>
                                       <%-- <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>--%>
                                    </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" HeaderText="Casi siempre en desacuerdo">
                                <ItemTemplate>
                                    <telerik:RadButton runat="server" ID="rbCasiDesacuerdo" ButtonType="ToggleButton" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR3") %>'>
                                      <%--  <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                        </ToggleStates>--%>
                                    </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" HeaderText="Totalmente en desacuerdo">
                                <ItemTemplate>
                                    <telerik:RadButton runat="server" ID="rbTotalmenteDesacuerdo" ButtonType="ToggleButton" ToggleType="Radio" AutoPostBack="false" GroupName="respuestas" Checked='<%# Eval("FG_VALOR4") %>'>
                                      <%--  <ToggleStates>
                                            <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked" ></telerik:RadButtonToggleState>
                                            <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio" ></telerik:RadButtonToggleState>
                                        </ToggleStates>--%>
                                    </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="height: 10px;"></div>
                <telerik:RadGrid runat="server" HeaderStyle-Font-Bold="true" ID="rgPreguntasAbiertas" AutoGenerateColumns="false" Height="60%" Width="100%" AllowSorting="true" AllowMultiRowSelection="true" OnNeedDataSource="rgPreguntasAbiertas_NeedDataSource">
                    <ClientSettings EnablePostBackOnRowClick="false" EnableAlternatingItems="false">
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_PREGUNTA">
                        <Columns>
<<<<<<< HEAD
                            <telerik:GridTemplateColumn DataField="NB_PREGUNTA" HeaderStyle-Width="130" HeaderText="Pregunta abierta" UniqueName="NB_PREGUNTA" ReadOnly="true">
=======
                            <telerik:GridTemplateColumn DataField="NB_PREGUNTA" HeaderStyle-Width="130" HeaderText="Pregunta" UniqueName="NB_PREGUNTA" ReadOnly="true">
>>>>>>> DEV
                                <ItemTemplate>
                                    <div title="<%# Eval("DS_PREGUNTA") %>"><%# Eval("NB_PREGUNTA") %></div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="520" ItemStyle-HorizontalAlign="Center" ReadOnly="true" HeaderText="Respuesta">
                                <ItemTemplate>
<<<<<<< HEAD
                                    <telerik:RadTextBox ID="txtRespuesta" runat="server" Width="950" MaxLength="3500" Text='<%# Eval("NB_RESPUESTA")%>'></telerik:RadTextBox>
=======
                                    <telerik:RadTextBox ID="txtRespuesta" runat="server" Width="950" Text='<%# Eval("NB_RESPUESTA")%>'></telerik:RadTextBox>
>>>>>>> DEV
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="height: 10px;"></div>
                <div class="divControlDerecha">
                    <telerik:RadButton ID="btnFinalizar" runat="server" Text="Finalizar cuestionario" OnClientClicking="confirmarFinalizar" OnClick="btnFinalizar_Click"></telerik:RadButton>
                </div>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpAyuda" runat="server" Width="30">
            <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Instrucciones" Width="300" MinWidth="300" Height="100%">
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
