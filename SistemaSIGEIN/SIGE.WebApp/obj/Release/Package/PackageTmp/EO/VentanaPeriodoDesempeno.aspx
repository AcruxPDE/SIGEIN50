<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaPeriodoDesempeno.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaNuevoPeriodoDesempeno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .rbToggleRadioChecked {
            background-color: #9A0209 !important;
            color: #fff !important;
        }
    </style>
    <script type="text/javascript">

        function closeWindow() {
            GetRadWindow().close();
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenEmployeeSelectionWindow() {
            OpenSelectionWindow("../Comunes/SeleccionEmpleado.aspx", "winSeleccion", "Selección de empleado")
        }

        function ChangeListItem(pIdItem, pNbItem, pList) {
            var vListBox = pList;
            vListBox.trackChanges();
            var items = vListBox.get_items();
            items.clear();
            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text(pNbItem);
            item.set_value(pIdItem);
            items.add(item);
            item.set_selected(true);
            vListBox.commitChanges();
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: 10px;"></div>
    <div style="height: calc(100% - 100px); width: 700px;">
        <div style="height: 7px;"></div>
        <div style="overflow: visible">
            <div class="ctrlBasico" style="text-align: center">
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblDsPeriodo">Clave de período:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtDsPeriodo" InputType="Text" Width="200" Height="30" runat="server"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="height: 40px;"></div>

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblDsDescripción">Descripción:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadTextBox ID="txtDsDescripcion" InputType="Text" Width="400" Height="30" runat="server"></telerik:RadTextBox>
                    </div>
                </div>
                <div style="height: 10px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblDsNotas">Notas:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadEditor Height="100" Width="400" ToolsWidth="400"  EditModes="Design" ID="txtDsNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml">
                        </telerik:RadEditor>
                    </div>
                </div>
                <div style="height: 120px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblFechaInicio">Fecha de inicio:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadDatePicker ID="FeInicio" Width="130px" runat="server">
                            <Calendar ID="Calendar2" runat="server">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </div>
                </div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="width: 150px;">
                        <label id="lblFechaTerminacion">Fecha de término:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadDatePicker ID="FeTerminacion" Width="130px" runat="server">
                            <Calendar ID="Calendar1" runat="server">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </div>
                </div>
                <div style="clear: both; height: 10px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="Label1">Diseño de metas:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadButton ID="rbMetasCero" runat="server" ToggleType="Radio"
                            GroupName="grbDisenoMetas" AutoPostBack="false" Text="A partir de cero">
                            <ToggleStates>
                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="rbMetasDescriptivo" runat="server" ToggleType="Radio"
                            GroupName="grbDisenoMetas" AutoPostBack="false" Text="A partir del descriptivo">
                            <ToggleStates>
                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                </div>
                <div style="height: 50px;"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblResultadosEvaluacion">¿Quién captura los resultados de la evaluación?: </label>
                    </div>
                    <table class="divControlIzquierdaAli" style="width: 400px;" cellpading="5">
                        <tr>
                            <td>
                                <telerik:RadButton ID="rbOcupantePuesto" runat="server" ToggleType="Radio"
                                    GroupName="grbCapturistaResultados" AutoPostBack="false" Text="Ocupante del puesto">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="rbJefeInmediato" runat="server" ToggleType="Radio"
                                    GroupName="grbCapturistaResultados" AutoPostBack="false" Text="Jefe inmediato">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="rbOtro" runat="server" ToggleType="Radio"
                                    GroupName="grbCapturistaResultados" AutoPostBack="false" Text="Otro">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="rbCoordinadorEvaluacion" runat="server" ToggleType="Radio"
                                    GroupName="grbCapturistaResultados" AutoPostBack="false" Text="Coordinador de evaluación">
                                    <ToggleStates>
                                        <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                        <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                    </ToggleStates>
                                </telerik:RadButton>

                            </td>
                        </tr>
                    </table>
                </div>
                 <div class="ctrlBasico" id="divCopiaPeriodo" runat="server" visible="false">
                    <div class="divControlIzquierda">
                        <label id="lblCopiaPeriodo">Copia de Periodo:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadButton ID="btnConsecuente" runat="server" ToggleType="Radio"
                            GroupName="grbTipoCopia" AutoPostBack="false" Text="Consecuente">
                            <ToggleStates>
                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                        <telerik:RadButton ID="btnIndependiente" runat="server" ToggleType="Radio"
                            GroupName="grbTipoCopia" AutoPostBack="false" Text="Independiente">
                            <ToggleStates>
                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>
                </div>


                <div style="clear: both; height: 10px;"></div>
            </div>
            <div style="height: 80px;"></div>
        </div>
        <div class="divControlesBoton">
            <telerik:RadButton runat="server" ID="btnAceptar" AutoPostBack="true" Text="Guardar" OnClick="btnAceptar_Click"></telerik:RadButton>
            <telerik:RadButton runat="server" ID="btnCancelar" AutoPostBack="false" Text="Cancelar" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
