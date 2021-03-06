﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaOcupacionSubarea.aspx.cs" Inherits="SIGE.WebApp.STPS.VentanaOcupacionSubarea" %>

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
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramActualizaciones" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramActualizaciones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbArea" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbArea" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="clear: both; height: 15px;"></div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblArea" name="lblArea" runat="server">Área:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadComboBox ID="cmbArea" Skin="Bootstrap" name="cmbArea" ToolTip="Campo obligatorio" CssClass="textbox"
                runat="server"
                Width="380px"
                MarkFirstMatch="true"
                EmptyMessage="Selecciona"
                AutoPostBack="true"
                OnSelectedIndexChanged="cmbArea_SelectedIndexChanged"
                EnableLoadOnDemand="true"
                ValidationGroup="vArea">
            </telerik:RadComboBox>
        </div>
    </div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblClave" name="lblClave" runat="server">Clave de sub-área:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtClave" runat="server" Width="300px" MaxLength="20"></telerik:RadTextBox><br />
            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" runat="server"
                ControlToValidate="txtClave" ErrorMessage="Sólo se permiten: letras, números y punto(.)"
                ForeColor="Red" CssClass="validacion" Font-Size="Smaller"
                ValidationExpression="^[A-Z0-9a-zÑñ.]*$">
            </asp:RegularExpressionValidator>
        </div>
    </div>

    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <label id="lblSubarea" name="lblSubarea" runat="server">Nombre de la sub-área:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtSubarea" runat="server" Width="300px" MaxLength="200"></telerik:RadTextBox><br />
            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" runat="server"
                ControlToValidate="txtSubarea" ErrorMessage="Sólo se permiten: letras, números, coma(,), punto(,) y guión(-)"
                ForeColor="Red" CssClass="validacion" Font-Size="Smaller"
                ValidationExpression="^[A-Z0-9 a-záéíóúÁÉÍÓÚñÑÜü,.\-]*$">
            </asp:RegularExpressionValidator>
        </div>
    </div>

    <div style="clear: both; height: 15px;"></div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardarSubarea" runat="server" Text="Guardar" OnClick="btnGuardarSubarea_Click" AutoPostBack="true"></telerik:RadButton>
            <telerik:RadButton ID="btnCancelarSubarea" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicking="closeWindow"></telerik:RadButton>
        </div>
    </div>
    <div style="clear: both;"></div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
