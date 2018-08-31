<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCatalogoIdiomas.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaCatalogoIdiomas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="clear:both; height:15px;"> </div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblClIdioma" name="lblClIdioma" >Clave:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtClIdioma" runat="server" Width="300px"></telerik:RadTextBox>
             <asp:RequiredFieldValidator   Display="Dynamic"  CssClass="validacion"  ID="RequiredFieldValidator2" runat="server"  Font-Names="Arial" Font-Size="Small" ControlToValidate="txtClIdioma" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>

        </div>
    </div>

    <div style="clear:both;"> </div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNbIdioma" name="lblNbIdioma" runat="server">Nombre:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNbIdioma" runat="server" Width="300px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator   Display="Dynamic"  CssClass="validacion"  ID="RequiredFieldValidator1" runat="server"  Font-Names="Arial" Font-Size="Small" ControlToValidate="txtNbIdioma" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>

        </div>
    </div>

   
    <div style="clear:both;"> </div>
     <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="Label2" name="lblDsTipo" runat="server">Activo:&nbsp;</label>
        </div>
        <div  class="divControlDerecha">
                        <telerik:RadButton ID="chkActivo" runat="server" ToggleType="CheckBox" name="chkActivo" AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Sí" PrimaryIconCssClass="rbToggleCheckboxChecked"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
        </div>

        </div>
       
    <div style="clear:both;"> </div>
        <div class="divControlesBoton">            
            <telerik:RadButton ID="btnGuardar" runat="server" Width="100px" OnClick="btnGuardar_Click" Text="Guardar"  />
            <telerik:RadButton ID="btnCancelar" runat="server" Width="100px" OnClientClicking="closeWindow" Text="Cancelar"/>
        </div>


     <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
