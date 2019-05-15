<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaEvaluador.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaEvaluador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div>
        <telerik:RadButton ID="btnOtroEvaluadorInventario" runat="server" ToggleType="Radio" GroupName="grpOtroEvaluador" AutoPostBack="false" Text="Del inventario de personal">
            <ToggleStates>
                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
            </ToggleStates>
        </telerik:RadButton>
    </div>
    <div style="padding-top: 10px;">
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblClEmpleado">Empleado:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadListBox ID="lstEmpleado" Width="300" runat="server">
                    <Items>
                        <telerik:RadListBoxItem Text="No seleccionado" Value="" />
                    </Items>
                </telerik:RadListBox>
                <telerik:RadButton ID="btnBuscarEmpleado" runat="server" Text="B" AutoPostBack="false"></telerik:RadButton>
            </div>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div style="padding-top: 10px;">
        <telerik:RadButton ID="btnOtroEvaluadorExterno" runat="server" ToggleType="Radio" GroupName="grpOtroEvaluador" AutoPostBack="false" Text="Personas externas">
            <ToggleStates>
                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                <telerik:RadButtonToggleState CssClass="unchecked" PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
            </ToggleStates>
        </telerik:RadButton>
    </div>
    <div style="padding-top: 10px;">
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblNbEvaluadorExterno">Nombre:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNbEvaluadorExterno" runat="server" Width="300"></telerik:RadTextBox>
            </div>
        </div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblNbEvaluadorExterno">Puesto:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNbEvaluadorExternoPuesto" runat="server" Width="300"></telerik:RadTextBox>
            </div>
        </div>
    </div>
    <div style="clear: both;"></div>
    <div style="padding-top: 10px;">
        <div class="ctrlBasico">
            <telerik:RadButton ID="chkFgOtroEvaluadorTodos" runat="server" ToggleType="CheckBox" AutoPostBack="false" Width="30">
                <ToggleStates>
                    <telerik:RadButtonToggleState></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState CssClass="unchecked"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
            <label name="lblFgGenericas">Evalúa a todos los evaluados</label>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnAgregarOtroEvaluador" runat="server" Text="Agregar"></telerik:RadButton>
        </div>
    </div>
    <div style="clear: both;"></div>
</asp:Content>
