<%@ Page Title=""  Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaLocalizacion.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaLocalizacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
     <telerik:RadCodeBlock runat="server" ID="radCodeblock">
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
</telerik:RadCodeBlock>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="radBtnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rwmAlertas"  />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="padding-top: 10px">
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblColonia" name="lblColonia">Colonia:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox runat="server" ID="txtColonia" name="txtColonia" Width="200px"/>                                                   
                <asp:RequiredFieldValidator    Display="Dynamic"  ID="reqValColonia" runat="server" ControlToValidate="txtColonia" ErrorMessage="Campo obligatorio" CssClass="validacion" ></asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="clear: both"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblCodigoPostal" name="lblCodigoPostal">Código postal:</label>                
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox runat="server" ID="txtCodigoPostal" name="txtCodigoPostal" Width="200px" />
                <asp:RequiredFieldValidator  Display="Dynamic"  ID="reqValCodigoPostal" runat="server" ControlToValidate="txtCodigoPostal" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="clear: both"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblTipoAsentamiento" name="lblTipoAsentamiento">Tipo de Asentamiento:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadComboBox ID="cmbTipoAsentamiento"
                    runat="server"
                    Height="200"
                    Style="width: 200px"
                    EmptyMessage="Selecciona"
                    EnableLoadOnDemand="true"
                    DataTextField="NB_TIPO_ASENTAMIENTO"
                    DataValueField="CL_TIPO_ASENTAMIENTO"
                    AutoPostBack="false">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator  Display="Dynamic"  ID="reqValTipoAsentamiento" runat="server" ControlToValidate="cmbTipoAsentamiento" ErrorMessage="Campo obligatorio" CssClass="validacion"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div id="controles" class="divControlesBoton">
            <telerik:RadButton ID="radBtnGuardar" AutoPostBack="true" runat="server" Text="Guardar" Width="100" OnClick="radBtnGuardar_Click"></telerik:RadButton>
            <telerik:RadButton ID="radBtnCancelar" AutoPostBack="false" runat="server" Text="Cancelar" Width="100" OnClientClicked="closeWindow"></telerik:RadButton>            
        </div>

    </div>
        <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
