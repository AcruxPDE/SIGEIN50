<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaAgregarPrograma.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaAgregarPrograma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">

        function closeWindow() {
            GetRadWindow().close();
        }

        function OpenSelectionWindow() {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: 1000,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog("../Comunes/SeleccionPeriodo.aspx?m=FORMACION&mulSel=0", "winSeleccion", "Selección de período", windowProperties);
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

        function enableDesdeDNC(sender, args) {
                $get("<%=desdeDNC.ClientID %>").style.display = sender.get_checked() ? 'block' : 'none';
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 50px);">
        <div style="height: 10px;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblClavePrograma" name="lblClavePrograma" runat="server">Programa:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtClProgCapacitacion" runat="server" Width="200px" MaxLength="1000"></telerik:RadTextBox>
                <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator2" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtClProgCapacitacion" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblNombrePrograma" name="lblNombrePrograma" runat="server">Descripción:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNbProgCapacitacion" runat="server" Width="400px" MaxLength="1000"></telerik:RadTextBox>
                <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator1" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtNbProgCapacitacion" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblEstadoPrograma" name="lblEstadoPrograma" runat="server">Estado:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtEstadoProgCapacitacion" runat="server" Width="200px" MaxLength="1000" Enabled="false"></telerik:RadTextBox>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="Label4" name="lblNombrePrograma" runat="server">Tipo:</label>
            </div>
            <%--<div class="divControlDerecha">
                <telerik:RadTextBox ID="txtTipoProgCapacitacion" runat="server" Width="200px" MaxLength="1000" Enabled="false"></telerik:RadTextBox>
            </div>--%>
            <div class="divControlDerecha">
             <telerik:RadButton ID="rbCero" runat="server" ToggleType="Radio"
                GroupName="grbConfiguracion" AutoPostBack="false" Text="A partir de 0">
                <ToggleStates>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
            <div style="clear:both; height:10px;"></div>
            <telerik:RadButton ID="rbDnc" runat="server" ToggleType="Radio"
                GroupName="grbConfiguracion" AutoPostBack="false" Text="A partir de DNC" OnClientCheckedChanged="enableDesdeDNC">
                <ToggleStates>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
             <div style="clear:both; height:10px;"></div>
            <div id="desdeDNC" runat="server" style="display: none">
                <telerik:RadListBox ID="lstPeriodo" Width="200" runat="server" ValidationGroup="vgPeriodo"></telerik:RadListBox>
                <telerik:RadButton ID="btnBuscarPeriodo" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionWindow" ValidationGroup="vgPeriodo"></telerik:RadButton>
                <telerik:RadButton ID="btnEliminarPeriodo" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgPeriodo" OnClientClicked="DeletePeriodo"></telerik:RadButton>
        </div>
             <div style="clear:both; height:10px;"></div>
            <telerik:RadButton ID="rbCopia" runat="server" ToggleType="Radio"
                GroupName="grbConfiguracion" AutoPostBack="false" Text="Copia" Visible="false">
                <ToggleStates>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
                </div>
        </div>
        <div style="clear: both; height: 10px;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="Label1" name="lblEstadoPrograma" runat="server">Notas:</label>
            </div>
            <div class="ctrlBasico">
                <telerik:RadEditor Height="150px" Width="400px" ToolsWidth="400px" EditModes="Design" ID="radEditorNotas" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/BasicTools.xml"></telerik:RadEditor>
            </div>
        </div>
    </div>
    <div style="height: 10px;"></div>
    <div class="divControlDerecha">
        <telerik:RadButton ID="btnAceptar" OnClick="btnAceptar_Click" runat="server" Text="Guardar" ToolTip="Aceptar" CssClass="ctrlBasico"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
