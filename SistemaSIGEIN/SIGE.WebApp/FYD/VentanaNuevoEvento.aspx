<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaNuevoEvento.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaNuevoEvento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function closeWindow() {
            GetRadWindow().close();
        }

        function ReturnDataToParentEdit() {
            var vAcciones = [];
            var vAccion = { clTipoCatalogo: "ACTUALIZAR" };
            vAcciones.push(vAccion);
            sendDataToParent(vAcciones);
        }

        function ReturnDataToParent() {
            var vAcciones = [];
            var vAccion = { clTipoCatalogo: "ACTUALIZARLISTA" };
            vAcciones.push(vAccion);
            sendDataToParent(vAcciones);
        }

        //Eliminar el tooltip del control
        function pageLoad() {
            var datePicker2 = $find("<%=dtpInicial.ClientID %>");
            datePicker2.get_popupButton().title = "";
            var datePicker3 = $find("<%=dtpFinal.ClientID %>");
            datePicker3.get_popupButton().title = "";
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 60px);">
        <div style="height: 10px; clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblClEvento">Evento:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtClEvento"  name="txtNbPeriodo" runat="server" MaxLength="50" Width="300" ></telerik:RadTextBox><br />
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label name="lblDsEvento">Descripción:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtDsEvento" name="txtDsEvento" runat="server" MaxLength="2000" Width="500"></telerik:RadTextBox><br />
            </div>
        </div>
            <div style="clear: both;"></div>
          <div class="ctrlBasico">
                <div class="divControlIzquierda">
                    <label>Fecha de inicio:</label>
                    </div>
               <div class="divControlDerecha">
                    <telerik:RadDatePicker runat="server" ID="dtpInicial"></telerik:RadDatePicker>
                   </div>
                </div>
                <div class="ctrlBasico">
                      <div class="divControlIzquierda">
                    <label>Fecha de término:</label>
                          </div>
                     <div class="divControlDerecha">
                    <telerik:RadDatePicker runat="server" ID="dtpFinal"></telerik:RadDatePicker>
                         </div>
                </div>
        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label class="Etiqueta">Tipo:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadComboBox runat="server" ID="cmbTipo">
                    <Items>
                        <telerik:RadComboBoxItem Text="Grupal" Value="GRUPAL" />
                        <telerik:RadComboBoxItem Text="Individual" Value="INDIVIDUAL" />
                    </Items>
                </telerik:RadComboBox>
            </div>
        </div>
    </div>
    <div style="height: 10px; clear: both;"></div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" AutoPostBack="true" OnClick="btnGuardar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>
     <telerik:RadWindowManager ID="rwmEvento" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
