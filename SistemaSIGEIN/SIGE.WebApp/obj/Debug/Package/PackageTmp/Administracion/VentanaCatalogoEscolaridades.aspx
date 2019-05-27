<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCatalogoEscolaridades.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaCatalogoEscolaridades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <script id="modal" type="text/javascript">
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

     <div style="clear:both; height:15px;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNbIdioma" name="lblNbIdioma" runat="server">Clave:</label>
        </div>
        <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtClCatalogo"  runat="server" Width="300px"></telerik:RadTextBox>
             <asp:RequiredFieldValidator  Display="Dynamic"  CssClass="validacion" ID="RequiredFieldValidator1" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtClCatalogo" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
        </div>
    </div>

     <div style="clear:both;"/>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="Label1" name="lblNbIdioma" runat="server">Nombre:</label>
        </div>
        <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNbCatalogo" runat="server" Width="300px" ></telerik:RadTextBox>
             <asp:RequiredFieldValidator  Display="Dynamic"  CssClass="validacion" ID="RequiredFieldValidator2" runat="server"  Font-Names="Arial" Font-Size="Small" ControlToValidate="txtNbCatalogo" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>

        </div>
    </div>


    <div style="clear:both;"/>
    <div class="ctrlBasico">
     <div class="divControlIzquierda">
            <label id="lblActivoTipo" name="lblDsTipo" runat="server">Activo:&nbsp;</label>
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


           <div style="clear:both;">
        <div class="divControlesBoton">    
            <telerik:RadButton ID="btnGuardarCatalogo" runat="server" Width="100px" Text="Guardar" OnClick="btnSave_click" AutoPostBack="true"></telerik:RadButton>
                <telerik:RadButton ID="btnCancelarCatalogo" runat="server" Width="100px" Text="Cancelar" AutoPostBack="false" OnClientClicking="closeWindow"></telerik:RadButton>
              </div>
    </div>
   
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
