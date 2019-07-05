<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="CapturarBajaPendiente.aspx.cs" Inherits="SIGE.WebApp.EO.CapturarBajaPendiente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
<script type="text/javascript">

    function closeWindow() {
        var pDatos = [{
            clTipoCatalogo: "BAJA_PENDIENTE"

        }];
        cerrarVentana(pDatos);
    }

    function cerrarVentana(recargarGrid) {
        sendDataToParent(recargarGrid);
    }

    function OpenSelectionCausaWindow() {
        openChildDialog("../Comunes/SeleccionCatalogos.aspx?ID_CATALOGO=<%=vIdCatalogoBaja%>", "WinSeleccionCausa", "Selección de causa de baja");
    }

    function useDataFromChild(pDato) {
        if (pDato != null) {
            var arr = [];
            var vSelectedData = pDato[0];
            switch (pDato[0].clTipoCatalogo) {
                case "CATALOGO":
                    list = $find("<%=lstCausaBaja.ClientID %>");
                        txtList = $find("<%=txtDsCausa.ClientID %>");
                        if (list != undefined) {
                            list.trackChanges();
                            var items = list.get_items();
                            items.clear();
                            var item = new Telerik.Web.UI.RadListBoxItem();
                            item.set_text(vSelectedData.nbCatalogoValor);
                            item.set_value(vSelectedData.idCatalogo);
                            item.set_selected(true);
                            items.add(item);
                            list.commitChanges();
                            document.getElementById("<%=txtDsCausa.ClientID %>").value = vSelectedData.dsCatalogoValor;
                         }
                         break;
                 }
             }
         }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 60px);">
        <div style="height:10px;"></div>
        <div class="ctrlBasico">
            <table class="ctrlTableForm">
                <tr>
                    <td class="ctrlTableDataContext">
                        <div class="divControlDerecha">
                        <label id="lbEmpleado" runat="server">Empleado:</label>
                            </div>
                    </td>
                    <td  class="ctrlTableDataBorderContext">
                        <div id="txtNbEmpleado" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <div class="divControlDerecha">
                        <label id="lbPuesto" runat="server">Puesto:</label>
                            </div>
                    </td>
                    <td  class="ctrlTableDataBorderContext">
                        <div id="txtNbPuesto" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <div class="divControlDerecha">
                            <label id="Label2" name="lblCausaBaja" runat="server">Causa de baja:</label>
                        </div>
                    </td>
                    <td class="ctrlTableDataContext">
                        <div class="ctrlBasico" style="width: 100%;">
                            <div class="divControlDerecha" style="width: 5%;">
                                <telerik:RadButton ID="btnCausaBaja" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgEstado" OnClientClicked="OpenSelectionCausaWindow"></telerik:RadButton>
                            </div>
                            <div class="divControlDerecha" style="width: 95%;">
                                <telerik:RadListBox ID="lstCausaBaja" Width="100%" runat="server" Height="35px" ValidationGroup="vgEstado"></telerik:RadListBox>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td class="ctrlTableDataContext">
                        <div class="divControlDerecha" style="width: 100%;">
                            <telerik:RadTextBox ID="txtDsCausa" InputType="Text" Width="100%" Height="100px" TextMode="MultiLine" Enabled="false" runat="server"></telerik:RadTextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <div class="divControlDerecha">
                            <label id="lbFechaBaja" name="lbFechaBaja" runat="server">Fecha de baja:</label>
                        </div>
                    </td>
                    <td colspan="2" class="ctrlTableDataContext">
                        <div class="divControlIzquierda">
                            <telerik:RadDatePicker ID="rdpFechaBaja" Enabled="false" runat="server" Visible="true">
                            </telerik:RadDatePicker>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="ctrlTableDataContext">
                        <div class="divControlDerecha">
                            <label id="Label3">Comentario:</label>
                        </div>
                    </td>
                    <td  class="ctrlTableDataContext">
                        <div class="divControlDerecha">
                            <telerik:RadEditor Height="100" Width="100%" ToolsWidth="400" EditModes="Design" ID="reComentarios" runat="server" ToolbarMode="ShowOnFocus" ToolsFile="~/Assets/AdvancedTools.xml"></telerik:RadEditor>
                        </div>
                    </td>
                </tr>
            </table>
            <div style="height:15px;"></div>
            <div class="divControlDerecha">
                <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" AutoPostBack="true" OnClick="btnGuardar_Click" ></telerik:RadButton>
                <telerik:RadButton ID="btnCancelat" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
            </div>
        </div>
    </div>
      <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
</asp:Content>
