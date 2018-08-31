<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaValoresGenericos.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaValoresGenericos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function closeWindow(operacion) {
            var oArg = new Object();
            oArg.operacion = operacion;
            var window = GetRadWindow();
            window.close(oArg);
        }

        function Close() {
            GetRadWindow().close();
        }
    </script>
    <br />
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblClValor" name="lblClValor" runat="server">Clave:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtClValor" runat="server" MaxLength="20" Width="300px"></telerik:RadTextBox>        
            <asp:RequiredFieldValidator  Display="Dynamic"  CssClass="validacion" ID="rvtxtClValor" runat="server"  Font-Names="Arial" Font-Size="Small" ControlToValidate="txtClValor" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>  
        </div>
    </div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNbValor" name="lblNbValor" runat="server">Nombre:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNbValor" runat="server" MaxLength="100" Width="300px"></telerik:RadTextBox>    
            <asp:RequiredFieldValidator  Display="Dynamic"  CssClass="validacion" ID="rvtxtNbValor" runat="server"  Font-Names="Arial" Font-Size="Small" ControlToValidate="txtNbValor" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>      
        </div>
    </div>
    <div style="clear:both;"/>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblDsValor" name="lblDsValor" runat="server">Descripción:&nbsp;</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtDsValor" runat="server" MaxLength="1000" Width="300px" Height="150px" TextMode="MultiLine"></telerik:RadTextBox>
        </div>
    </div>
    <div style="clear:both;"/>
    <div style="clear:both;">
        <div class="divControlesBoton">
            <telerik:RadButton ID="btnGuardarRegistro" Width="100px" OnClick="btnGuardarRegistro_Click" Text="Guardar" runat="server"></telerik:RadButton>
            <telerik:RadButton ID="btnCancelarRegistro" Width="100px" OnClientClicked="Close" Text="Cancelar" runat="server"></telerik:RadButton>
        </div>
    </div>
        <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
