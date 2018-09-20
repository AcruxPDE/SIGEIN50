<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaAsignarRequisicion.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaAsignarRequisicion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function onCloseWindows() {
            GetRadWindow().close();
        }

        function OpenSelectionWindow(sender, args) {
            var vbtnRequisicion = $find("<%= btnSeleccionaRequisicion.ClientID %>");

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 20
            };


            if (sender == vbtnRequisicion) {
                windowProperties.width = 1100;
                openChildDialog("../Comunes/SelectorRequisiciones.aspx", "winSeleccion", "Selección de la requisición", windowProperties);
            }

        }

        function useDataFromChild(pPuestos) {
            if (pPuestos != null) {
                var vPuestoSeleccionado = pPuestos[0];
                var vCatalogo = vPuestoSeleccionado.clTipoCatalogo;
                var vclPlazaSuperior = vPuestoSeleccionado.clPlazaSuperior;

                switch (vCatalogo) {
                    case "REQUISICION":
                        var vListaRequicion = $find("<%=rlbRequicion.ClientID %>");
                        SetListBoxItem(vListaRequicion, vPuestoSeleccionado.nbRequicision, vPuestoSeleccionado.idRequisicion);
                        break;
                }
            }
        }

        function SetListBoxItem(list, text, value) {
            if (list != undefined & text != undefined & text != "&nbsp;") {
                list.trackChanges();

                var items = list.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(text);
                item.set_value(value);
                item.set_selected(true);
                items.add(item);

                list.commitChanges();
            }
        }

        function CleanRequicionSelection(sender, args) {
            var list = $find("<%=rlbRequicion.ClientID %>");
            SetListBoxItem(list, "No seleccionado", "0");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 60px);">
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Folio de solicitud:</label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtClaveSolicitud" runat="server" style="width: 300px;"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Candidato: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCandidato" runat="server" style="width: 300px;"></span>
                </td>
            </tr>
        </table>
        <div style="clear: both; height: 30px;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblRequisicion" name="lblRequisicion" runat="server">Requisición:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadListBox ID="rlbRequicion" ReadOnly="false" runat="server" Width="250px" MaxLength="100">
                    <Items>
                        <telerik:RadListBoxItem Text="Ninguna" Value="0" />
                    </Items>
                </telerik:RadListBox>
                <telerik:RadButton ID="btnSeleccionaRequisicion" AutoPostBack="false" runat="server" Text="B" OnClientClicked="OpenSelectionWindow"></telerik:RadButton>
                <telerik:RadButton ID="BtnEliminaRequicion" runat="server" Text="X" AutoPostBack="false" OnClientClicked="CleanRequicionSelection"></telerik:RadButton>
            </div>
        </div>
        <div style="clear: both;"></div>
    </div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
        <telerik:RadButton ID="btnAceptar" runat="server" Text="Aceptar" AutoPostBack="true" OnClick="btnAceptar_Click"></telerik:RadButton>
        </div>
         <div class="ctrlBasico">
        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="onCloseWindows"></telerik:RadButton>
             </div>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Height="100%"></telerik:RadWindowManager>
</asp:Content>
