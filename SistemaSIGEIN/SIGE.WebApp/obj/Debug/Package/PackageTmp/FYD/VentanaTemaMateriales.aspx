<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaTemaMateriales.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaTemaMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function generateDataForParent() {
            var info = null;
            var vDatos = [];

            var vConcepto = $find("<%=txtConepto.ClientID %>").get_value();
            var vCosto = $find("<%=txtCosto.ClientID %>").get_value();

            var costotema = document.getElementById('<%= txtCosto.ClientID %>').value
            var conceptotema = document.getElementById('<%= txtConepto.ClientID %>').value

            if (vConcepto == "") {
                radalert("Ingrese el nombre del material", 400, 150);
                return;
            }

            if (vCosto == "") {
                radalert("Ingrese el costo del material", 400, 150);
                return;
            }

            var vDato = {
                nbConcepto: vConcepto,
                mnCosto: vCosto,
                clTipoCatalogo: "MATERIAL"
            };
            vDatos.push(vDato);
            sendDataToParent(vDatos);

        }

        function cancelarSeleccion() {
            sendDataToParent(null);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 100px); padding: 10px 10px 10px 10px;">
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblConcepto" name="lblConcepto" runat="server">Concepto:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtConepto" runat="server" MaxLength="512" Width="300px"></telerik:RadTextBox>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblCosto" name="lblCosto" runat="server">Costo</label>
            </div>
            <div style="float: right;">
                <telerik:RadNumericTextBox ID="txtCosto" DataType="Decimal" runat="server" Width="150px" MaxLength="13"></telerik:RadNumericTextBox>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div style="clear: both;">
            <div style="float: right; padding-right: 10px; padding-bottom: 10px;">
                <br />
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnGuardarMaterial" Width="100px" Text="Agregar" AutoPostBack="false" runat="server" OnClientClicked="generateDataForParent"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnCancelarMaterial" Width="100px" AutoPostBack="false" OnClientClicking="cancelarSeleccion" Text="Cancelar" runat="server"></telerik:RadButton>
                </div>
            </div>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
