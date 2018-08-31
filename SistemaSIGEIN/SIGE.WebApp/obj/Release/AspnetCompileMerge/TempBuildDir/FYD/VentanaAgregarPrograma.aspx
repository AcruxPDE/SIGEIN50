<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaAgregarPrograma.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaAgregarPrograma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">

        function closeWindow() {
            GetRadWindow().close();
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
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtTipoProgCapacitacion" runat="server" Width="200px" MaxLength="1000" Enabled="false"></telerik:RadTextBox>
            </div>
        </div>
        <div style="clear: both; height: 20px;"></div>
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
