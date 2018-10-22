<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCatalogoClasificacionComp.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaCatalogoClasificacionComp" %>

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
    <div style="clear: both; height: 15px;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblClValor" name="lblClValor" runat="server">Clave:&nbsp;</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtClClasificacion" runat="server" MaxLength="20" Width="300px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="rvtxtClClasificacion" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtClClasificacion" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblIdTipoCompetencia" name="lblIdTipoCompetencia" style="width: 100px;" runat="server">Cátegoria:&nbsp;</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadComboBox ID="cmbIdTipoCompetencia" runat="server" Width="200px" EmptyMessage="Selecciona"></telerik:RadComboBox>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="rvcmbIdTipoCompetencia" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="cmbIdTipoCompetencia" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
        </div>
        <div style="clear: both; "></div>
    </div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNbCategoria" name="lblNbCategoria" runat="server">Nombre:&nbsp;</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNbCategoria" runat="server" MaxLength="100" Width="300px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator Display="Dynamic" CssClass="validacion" ID="rvtxtNbCategoria" runat="server" Font-Names="Arial" Font-Size="Small" ControlToValidate="txtNbCategoria" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="Label1" name="lblNotasCategoria" runat="server">Descripción:&nbsp;</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtDescripcion" runat="server" MaxLength="1000" Width="300px" Height="120px" TextMode="MultiLine"></telerik:RadTextBox>
        </div>
    </div>

    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblNotasCategoria" name="lblNotasCategoria" runat="server">Observaciones:&nbsp;</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNotasCategoria" runat="server" MaxLength="1000" Width="300px" Height="120px" TextMode="MultiLine"></telerik:RadTextBox>
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
    <div style="clear: both;"></div>

        <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="Label3" name="lblDsTipo" runat="server">Color:</label>
        </div>
        <div class="divControlDerecha">
             <telerik:RadColorPicker AutoPostBack="false"  runat="server" ShowIcon="true"
                        SelectedColor="#FFFF00" ID="rdcClasificacionCompetencia" PaletteModes="All">
             </telerik:RadColorPicker>
        </div>
    </div>




    <div style="clear: both;"></div>
    <div class="divControlesBoton">
        <telerik:RadButton ID="btnGuardarRegistro" Width="100px" OnClick="btnGuardarRegistro_Click" Text="Guardar" runat="server"></telerik:RadButton>
        <telerik:RadButton ID="btnCancelarRegistro" Width="100px" OnClientClicking="closeWindow" Text="Cancelar" runat="server"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
