<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCatalogoTipoCompetencia.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaCatalogoTipoCompetencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
     <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
   


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
            <label id="lblClTipo" name="lblClTipo" runat="server">Clave:&nbsp;</label>
        </div>
        <div  class="divControlDerecha">
            <telerik:RadTextBox ID="txtClTipo" runat="server" MaxLength="20" Width="300px"></telerik:RadTextBox>     
            <asp:RequiredFieldValidator  Display="Dynamic"  CssClass="validacion"  ID="rvtxtClTipo" runat="server"  Font-Names="Arial" Font-Size="Small" ControlToValidate="txtClTipo" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>     
        </div>
    </div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNbTipo" name="lblNbTipo" runat="server">Nombre:&nbsp;</label>
        </div>
        <div style="float:right;">
            <telerik:RadTextBox ID="txtNbTipo" runat="server" MaxLength="100" Width="300px"></telerik:RadTextBox>   
            <asp:RequiredFieldValidator  Display="Dynamic"  CssClass="validacion"  ID="rvtxtNbTipo" runat="server"  Font-Names="Arial" Font-Size="Small" ControlToValidate="txtNbTipo" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>          
        </div>
    </div>
   <%-- <div style="clear:both;"/>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblDsTipo" name="lblDsTipo" runat="server">Descripción:&nbsp;</label>
        </div>
        <div  class="divControlDerecha">
            <telerik:RadTextBox ID="txtDsTipo" runat="server" MaxLength="200" Width="300px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator  Display="Dynamic"  CssClass="validacion"  ID="rvtxtDsTipo" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtDsTipo" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>          
        </div>
    </div>--%>

 <div style="clear:both;"/>
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
    
    <div style="clear:both;">
        <div style="float:right;padding-right: 10px;padding-bottom:10px;">
            <br />
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardarRegistro" Width="100px" OnClick="btnGuardarRegistro_Click" Text="Guardar" runat="server"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelarRegistro" Width="100px" AutoPostBack="false" OnClientClicking="closeWindow" Text="Cancelar" runat="server"></telerik:RadButton>
        </div>
        </div>
    </div>
     <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
