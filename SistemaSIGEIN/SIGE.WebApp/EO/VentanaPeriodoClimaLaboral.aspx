<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaPeriodoClimaLaboral.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaNuevoPeriodoClimaLaboral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">
        function OnClientClicked(sender, args) {
            var selectedoption = sender.get_text();
        }

        function closeWindow() {
            var pDatos = [{
                accion: "ACTUALIZARLISTA"

            }];
            cerrarVentana(pDatos);
        }

        function closeWindowEdit() {
            var pDatos = [{
                accion: "ACTUALIZAR"

            }];
            cerrarVentana(pDatos);
        }

        function cerrarVentana(recargarList) {
            sendDataToParent(recargarList);
        }

        function enableCtrlCopiaPeriodo(sender, args) {
            $get("<%=copiarPeriodo.ClientID %>").style.display = sender.get_checked() ? 'block' : 'none';
        }

        function OpenSelectionWindow() {

                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;

                var windowProperties = {
                    width: browserWnd.innerWidth - 20,
                    height: browserWnd.innerHeight - 20
                };

                openChildDialog("../Comunes/SeleccionPeriodoClima.aspx?mulSel=0", "winSeleccion", "Selección de período", windowProperties);

        }

        function useDataFromChild(pData) {
            if (pData != null) {
                var vSelectedData = pData[0];
                var list;
                switch (vSelectedData.clTipoCatalogo) {
                    case "PERIODO":
                        list = $find("<%=lstPeriodo.ClientID %>");
                        if (list != undefined) {
                            list.trackChanges();
                            var items = list.get_items();
                            items.clear();
                            var item = new Telerik.Web.UI.RadListBoxItem();
                            item.set_text(vSelectedData.nbPeriodo);
                            item.set_value(vSelectedData.idPeriodo);
                            item.set_selected(true);
                            items.add(item);
                            list.commitChanges();
                        }
                        break;
                }
            }
        }

        function DeletePeriodo() {
            var vListBox = $find("<%=lstPeriodo.ClientID %>");
            var vSelectedItems = vListBox.get_selectedItems();
            vListBox.trackChanges();
            if (vSelectedItems)
                vSelectedItems.forEach(function (item) {
                    vListBox.get_items().remove(item);
                });
            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text("No Seleccionado");
            vListBox.get_items().add(item)
            item.select();
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: 10px;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblDsPeriodo">Clave de período:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtDsPeriodo" InputType="Text" Width="200" Height="30" runat="server" MaxLength="100"></telerik:RadTextBox>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator2" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtDsPeriodo" ErrorMessage="El campo es obligatorio"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblDsDescripción">Descripción:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtDsDescripcion" InputType="Text" Width="400" Height="30" runat="server"></telerik:RadTextBox>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator1" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtDsDescripcion" ErrorMessage="El campo es obligatorio"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblDsEstado">Estado:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtDsEstado" Enabled="false" InputType="Text" Width="120" Height="30" runat="server"></telerik:RadTextBox>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblDsNotas">Notas:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadEditor Height="100" Width="400" ToolsWidth="400" EditModes="Design" ID="txtDsNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml" ContentFilters="ConvertToXhtml"></telerik:RadEditor>
        </div>
    </div>
    <div style="clear: both; height: 5px"></div>
    <div class="ctrlBasico">
          <div class="divControlIzquierda">
            <label id="lbTipoConfiguracion">Tipo de período:</label>
              </div>
            <telerik:RadButton ID="rbSeleccion" runat="server" ToggleType="Radio"
                GroupName="grbConfiguracion" AutoPostBack="false" Text="Evaluadores asignados">
                <ToggleStates>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
            <telerik:RadButton ID="rbParametros" runat="server" ToggleType="Radio"
                GroupName="grbConfiguracion" AutoPostBack="false" Text="Sin asignación de evaluadores">
                <ToggleStates>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
    </div>
    <div style="clear: both; height: 10px"></div>
    <div class="ctrlBasico">
        <div style="padding-left: 50px;">
            <label id="lblDsOpcionesCuestionario">Elige alguna de las opciones para crear el cuestionario:</label>
        </div>
        <div style="clear: both; height: 10px"></div>
        <div style="padding-left: 200px;">
            <telerik:RadButton ID="rbCuestionarioPredefinido" runat="server" ToggleType="Radio"
                GroupName="grbCuestionario" AutoPostBack="false" Text="Utilizar cuestionario predefinido en SIGEIN">
                <ToggleStates>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
        <div style="padding-left: 200px;">
            <telerik:RadButton ID="rbCopiarCuestionario" runat="server" ToggleType="Radio" GroupName="grbCuestionario" AutoPostBack="false" Text="Copiar el cuestionario de otro período" OnClientCheckedChanged="enableCtrlCopiaPeriodo">
                <ToggleStates>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
        <div id="copiarPeriodo" runat="server" style="display: none">
            <div style="padding-left: 570px;">
                <telerik:RadListBox ID="lstPeriodo" Width="200" runat="server" ValidationGroup="vgPeriodo"></telerik:RadListBox>
                <telerik:RadButton ID="btnBuscarPeriodo" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionWindow" ValidationGroup="vgPeriodo"></telerik:RadButton>
                <telerik:RadButton ID="btnEliminarPeriodo" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgPeriodo" OnClientClicked="DeletePeriodo"></telerik:RadButton>
            </div>
        </div>
        <div style="padding-left: 200px;">
            <telerik:RadButton ID="rbCuestionarioEnBlanco" runat="server" ToggleType="Radio"
                GroupName="grbCuestionario" AutoPostBack="false" Text="Crear un cuestionario en blanco">
                <ToggleStates>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
    </div>
    <div style="height: 15px; clear: both;"></div>
    <div class="divControlesBoton">
        <telerik:RadButton runat="server" ID="btnAceptar" Text="Aceptar" AutoPostBack="true" OnClick="btnAceptar_Click"></telerik:RadButton>
        <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" OnClientClicking="closeWindow"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
