<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCatalogoAreas.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaCatalogoAreas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="modal" type="text/javascript">
        function closeWindow() {
            GetRadWindow().close();
        }

        function OpenAreasSelectionWindow(sender, args) {
            var windowProperties = {
                width: 800,
                height: 600
            };
            openChildDialog("../Comunes/SeleccionArea.aspx?mulSel=0", "winSeleccion", "Selección de área/departamento", windowProperties);
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                console.info(pDato);
                var vDatosSeleccionados = pDato[0];
                var vLstDato = {
                    idItem: "",
                    nbItem: ""
                };

                var list;
                switch (vDatosSeleccionados.clTipoCatalogo) {
                    case "DEPARTAMENTO":
                        list = $find("<%=lstDepartamentoJefe.ClientID %>");
                        vLstDato.idItem = vDatosSeleccionados.idArea;
                        vLstDato.nbItem = vDatosSeleccionados.nbArea;
                        break;
                }

                if (list)
                    ChangeListItem(vLstDato.idItem, vLstDato.nbItem, list);
            }
        }

        function CleanAreasSelection(sender, args) {
            var list = $find("<%=lstDepartamentoJefe.ClientID %>");
            ChangeListItem("", "No seleccionado", list);
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
    <div style="height: 15px; clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNbIdioma" name="lblNbIdioma" runat="server">Clave:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtClCatalogo" runat="server" Width="300px" MaxLength="1000"></telerik:RadTextBox>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator2" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtClCatalogo" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>

        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="Label1" name="lblNbIdioma" runat="server">Nombre:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNbCatalogo" runat="server" Width="300px" MaxLength="1000"></telerik:RadTextBox>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="RequiredFieldValidator1" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtNbCatalogo" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblDepartamentoJefe" name="lblDepartamentoJefe" runat="server">Área/Departamento del que depende:&nbsp;</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadListBox ID="lstDepartamentoJefe" Width="300" runat="server">
                <Items>
                    <telerik:RadListBoxItem Text="No Seleccionado" Value="" />
                </Items>
            </telerik:RadListBox>
            <telerik:RadButton runat="server" ID="btnBuscarDepartamentoDenpede" Text="B" Width="35px" AutoPostBack="false" OnClientClicked="OpenAreasSelectionWindow" />
            <telerik:RadButton runat="server" ID="btnCleanDepartamentoDepende" Text="X" Width="35px" AutoPostBack="false" OnClientClicked="CleanAreasSelection" />
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblTipoDepartamento" name="lblTipoDepartamento" runat="server">Tipo:&nbsp;</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadComboBox ID="cmbTipoDepartamento" runat="server">
                <Items>
                    <telerik:RadComboBoxItem Text="Área" Value="AREA" />
                    <telerik:RadComboBoxItem Text="Departamento" Value="DEPARTAMENTO" />
                </Items>
            </telerik:RadComboBox>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="Label2" name="lblDsTipo" runat="server">Activo:&nbsp;</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="chkActivo" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                <ToggleStates>
                    <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
    </div>
    <br />
    <div style="clear: both;"></div>
    <div class="divControlesBoton">
        <telerik:RadButton ID="btnGuardarCatalogo" runat="server" Text="Guardar" OnClick="btnSave_click"></telerik:RadButton>
        <telerik:RadButton ID="btnCancelarCatalogo" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicking="closeWindow"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
