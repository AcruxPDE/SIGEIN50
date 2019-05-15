﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaCatalogoAreasTematicas.aspx.cs" Inherits="SIGE.WebApp.STPS.VentanaCatalogoAreasTematicas" %>

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

    <div style="clear: both; height: 15px;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblClave" name="lblClave" runat="server">Clave:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtClave" runat="server" Width="300px" MaxLength="20"></telerik:RadTextBox><br />
            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" runat="server"
                ControlToValidate="txtClave" ErrorMessage="Sólo letras y números"
                ForeColor="Red" CssClass="validacion" Font-Size="Smaller"
                ValidationExpression="^[A-Z0-9a-zÑñ]*$">
            </asp:RegularExpressionValidator>
        </div>
    </div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblArea" name="lblArea" runat="server">Nombre del área temática:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtAreaTematica" runat="server" Width="300px" MaxLength="200"></telerik:RadTextBox><br />
            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" runat="server"
                ControlToValidate="txtAreaTematica" ErrorMessage="Sólo se permiten: letras, números, coma(,), punto(,) y guión(-)"
                ForeColor="Red" CssClass="validacion" Font-Size="Smaller"
                ValidationExpression="^[A-Z0-9 a-záéíóúÁÉÍÓÚñÑÜü,.\-]*$">
            </asp:RegularExpressionValidator>
        </div>
    </div>

    <div style="clear: both; height: 15px;"></div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardarCatalogo" runat="server" Text="Guardar" OnClick="btnGuardarCatalogo_Click" AutoPostBack="true"></telerik:RadButton>
            <telerik:RadButton ID="btnCancelarCatalogo" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicking="closeWindow"></telerik:RadButton>
        </div>
    </div>
    <div style="clear: both;"></div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>