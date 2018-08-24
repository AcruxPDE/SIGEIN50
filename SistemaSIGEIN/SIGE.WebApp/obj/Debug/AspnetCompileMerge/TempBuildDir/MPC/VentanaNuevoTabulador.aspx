<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="VentanaNuevoTabulador.aspx.cs" Inherits="SIGE.WebApp.MPC.VentanaNuevoMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function closeWindow() {
                GetRadWindow().close();
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height:calc(100% - 60px);">
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda" style="width: 150px">
            <label id="lbVersionTabulador"
                name="lbVersionTabulador"
                runat="server">
                Versión de tabulador:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtVersionTabulador"
                runat="server"
                Width="180px">
            </telerik:RadTextBox>
            <asp:RequiredFieldValidator
                Display="Dynamic"
                CssClass="validacion"
                ID="RequiredFieldValidator2"
                runat="server"
                Font-Names="Arial"
                Font-Size="Small"
                ControlToValidate="txtVersionTabulador"
                ErrorMessage="El campo es obligatorio">
            </asp:RequiredFieldValidator>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda" style="width: 150px">
            <label id="lbNombre"
                name="lbNombre"
                runat="server">
                Descripción:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtNombreTabulador"
                runat="server"
                Width="350px">
            </telerik:RadTextBox>
            <asp:RequiredFieldValidator
                Display="Dynamic"
                CssClass="validacion"
                ID="RequiredFieldValidator3"
                runat="server"
                Font-Names="Arial"
                Font-Size="Small"
                ControlToValidate="txtNombreTabulador"
                ErrorMessage="El campo es obligatorio">
            </asp:RequiredFieldValidator>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda" style="width: 150px">
            <label id="lbDescripcion"
                name="lbDescripcion"
                runat="server">
                Notas:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadTextBox ID="txtDescripcion"
                runat="server"
                Width="350px"
                Height="80"
                TextMode="MultiLine"
                MaxLength="800">
            </telerik:RadTextBox>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda" style="width: 150px">
            <label id="lbFechaCreacion"
                name="lbFechaCreacion"
                runat="server">
                Fecha de Creacion:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadDatePicker ID="rdpCreacion" Enabled="true" runat="server" Width="140px" DateInput-DateFormat="dd-MM-yyyy" DateInput-DateInput-DisplayDateFormat="dd-MM-yyyy">
            </telerik:RadDatePicker>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda" style="width: 150px">
            <label id="lbVigencia"
                name="lbVigencia"
                runat="server">
                Vigencia:</label>
        </div>
        <div class="divControlDerecha">
            <telerik:RadDatePicker ID="rdpVigencia" runat="server" Width="140px" Calendar-Height="50px" Calendar-Width="50px" Calendar-CalendarTableStyle-Height="50px" Calendar-CalendarTableStyle-Width="50px" DateInput-DateFormat="dd-MM-yyyy" DateInput-DateInput-DisplayDateFormat="dd-MM-yyyy">
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator
                Display="Dynamic"
                CssClass="validacion"
                ID="RequiredFieldValidator"
                runat="server"
                Font-Names="Arial"
                Font-Size="Small"
                ControlToValidate="rdpVigencia"
                ErrorMessage="El campo es obligatorio"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div style="clear: both;"></div>
    <p style="margin: 10px; text-align: justify;">Selecciona el tipo de puestos con los que deseas realizar el tabulador de sueldos: </p>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <telerik:RadButton ID="btDirectos" runat="server" ToggleType="Radio" name="btDirectos" AutoPostBack="false" Text="Directos" GroupName="TipoPuesto" Width="109px" ValidationGroup="btnsTiposPuestos">
                <ToggleStates>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <telerik:RadButton ID="btIndirectos" runat="server" ToggleType="Radio" name="btIndirectos" AutoPostBack="false" Text="Indirectos" GroupName="TipoPuesto" Width="109px" ValidationGroup="btnsTiposPuestos">
                <ToggleStates>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <div class="divControlIzquierda">
            <telerik:RadButton ID="btAmbos" runat="server" ToggleType="Radio" name="btAmbos" AutoPostBack="false" Text="Ambos" GroupName="TipoPuesto" Width="109px" ValidationGroup="btnsTiposPuestos" Checked="true">
                <ToggleStates>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </div>
    </div>
    </div>
    <div style="clear: both;"></div>
    <div class="divControlesBoton">
        <telerik:RadButton ID="btnGuardarNuevo"
            runat="server"
            Width="100px"
            Text="Guardar"
            AutoPostBack="true"
            OnClick="btnGuardarNuevo_Click">
        </telerik:RadButton>
        <telerik:RadButton ID="btnCancelarNuevo"
            runat="server"
            Width="100px"
            Text="Cancelar"
            AutoPostBack="true"
            OnClientClicking="closeWindow">
        </telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
