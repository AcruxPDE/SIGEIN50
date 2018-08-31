<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCatalogoCatalogos.aspx.cs" Inherits="SIGE.WebApp.Administracion.PopupmodalCatalogoGenerico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <script id="MyScript" type="text/javascript">
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
            GetRadWindow().close();
        }
    </script>
    <br />
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNbCatalogo" name="lblNbCatalogo" runat="server">Nombre:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNbCatalogo" runat="server" MaxLength="100" Width="300px"></telerik:RadTextBox>      
            <asp:RequiredFieldValidator  Display="Dynamic"  CssClass="validacion"   ID="rvtxtNbCatalogo" runat="server"  Font-Names="Arial" Font-Size="Small" ControlToValidate="txtNbCatalogo" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>  
        </div>
    </div>
    <div style="clear:both;"/>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblDsCatalogo" name="lblDsCatalogo" runat="server">Descripción:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtDsCatalogo" runat="server" MaxLength="1000" Width="300px"></telerik:RadTextBox>
        </div>
    </div>


    <div style="clear:both;"/>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblIdCatalogo" name="lblIdCatalogo" style="width:100px;" runat="server">Tipo de catálogo:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadComboBox ID="cmbIdCatalogo" runat="server" Width="200px" EmptyMessage="Selecciona"></telerik:RadComboBox>
            <asp:RequiredFieldValidator  Display="Dynamic"  CssClass="validacion"   ID="rvcmbIdCatalogo" runat="server"  Font-Names="Arial" Font-Size="Small" ControlToValidate="cmbIdCatalogo" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>  
        </div>
        <br />
    </div>
    <div style="clear:both;">
        <div class="divControlesBoton">    
            <telerik:RadButton ID="btnGuardarCatalogo" Width="100px" OnClick="btnGuardarCatalogo_Click" Text="Guardar" runat="server"></telerik:RadButton>
            <telerik:RadButton ID="btnCancelarCatalogo" Width="100px" OnClientClicking="closeWindow" Text="Cancelar" runat="server"></telerik:RadButton>
        </div>
    </div>
      <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
