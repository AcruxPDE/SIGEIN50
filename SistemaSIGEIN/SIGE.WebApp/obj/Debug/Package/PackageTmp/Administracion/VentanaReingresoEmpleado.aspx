<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaReingresoEmpleado.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaReingresoEmpleado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

     <script type="text/javascript">
         function OpenSelectionWindow(sender, args) {

             var vBtnPlaza = $find("<%= btnAgregarPuesto.ClientID %>");
            var vBtnPlazaJefe = $find("<%= btnAgregarJefe.ClientID %>");
            //var vbtnRequisicion = $find("<= btnSelecionarPuestoRequisicion.ClientID %>");

            var windowProperties = {
                width: 600,
                height: 600
            };

            if (sender == vBtnPlaza) {
                openChildDialog("../Comunes/SeleccionPlaza.aspx?TipoSeleccionCl=VACANTES&mulSel=0", "winSeleccion", "Selección de la plaza a ocupar", windowProperties);
            }

            if (sender == vBtnPlazaJefe) {
                var list = $find("<%=rlbPuesto.ClientID %>");
                openChildDialog("../Comunes/SeleccionPlaza.aspx?CatalogoCl=PLAZAJEFE&TipoSeleccionCl=JEFE&mulSel=0", "winSeleccion", "Selección del jefe inmediato", windowProperties);
            }

            //if (sender == vbtnRequisicion) {
            //    var idCandidato = '<= vIdCandidato %>';
            //    windowProperties.width = 900;
            //    openChildDialog("/Comunes/SelectorRequisiciones.aspx?CandidatoId=" + idCandidato, "winSeleccion", "Selección de la requisición", windowProperties);
            //}

         }

         //Eliminar el tooltip del control
         function pageLoad() {
             var datePicker = $find("<%=rdpFechaIngreso.ClientID %>");
            datePicker.get_popupButton().title = "";
        }


        function useDataFromChild(pPuestos) {
            if (pPuestos != null) {
                var vPuestoSeleccionado = pPuestos[0];

                var vCatalogo = vPuestoSeleccionado.clTipoCatalogo;

                switch (vCatalogo) {

                    case "PLAZAJEFE":
                        var vListaJefe = $find("<%=rlbJefe.ClientID %>");
                        SetListBoxItem(vListaJefe, vPuestoSeleccionado.clPlaza, vPuestoSeleccionado.idPlaza);
                        break;
                    case "PLAZA":
                        var vListaPuesto = $find("<%=rlbPuesto.ClientID %>");
                        SetListBoxItem(vListaPuesto, vPuestoSeleccionado.clPlaza, vPuestoSeleccionado.idPlaza);
                        var vListaJefe = $find("<%=rlbJefe.ClientID %>");
                        SetListBoxItem(vListaJefe, vPuestoSeleccionado.clPlazaSuperior, vPuestoSeleccionado.idPlazaSuperior);
                        break;
                    case "REQUISICION":
                        break;

                }
            }
        }

        function SetListBoxItem(list, text, value) {
            if (list != undefined && value != null) {
                list.trackChanges();

                var items = list.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                    item.set_text(text);
                    item.set_value(value);
                    item.set_selected(true);
                    items.add(item);

                    list.commitChanges();
                
            }
        }

        function CleanPuestoSelection(sender, args) {
            var list = $find("<%=rlbPuesto.ClientID %>");
            SetListBoxItem(list, "No seleccionado", "0");
        }

        function CleanJefeSelection(sender, args) {
            var list = $find("<%=rlbJefe.ClientID %>");
            SetListBoxItem(list, "No seleccionado", "0");
        }

        //function CleanRequisicionSelection(sender, args) {
        //    var list = $find(<=rlbRequisicion.ClientID >);
        //    SetListBoxItem(list, "No seleccionado", "0");
        //}

        function closeWindow() {
            GetRadWindow().close();
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
<div style="height:calc(100% - 80px);">
    <div>
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Clave Empleado:</label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtClaveEmpleado" runat="server" style="width: 300px;"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Empleado: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtEmpleado" runat="server" style="width: 300px;"></span>
                </td>
                <%--<td class="ctrlTableDataContext">
                    <label>Fecha de solicitud: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtFechaSolicitud" runat="server" style="width: 300px;"></span>
                </td>--%>
            </tr>
        </table>
    </div>

    <div style="clear: both;"></div>

    <fieldset>
        <legend>
            <label>Datos generales</label>
        </legend>

        <div style="clear: both;"></div>

        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblPuesto" name="lblPuesto" runat="server">Puesto a ocupar:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadListBox ID="rlbPuesto" ReadOnly="false" runat="server" Width="300px" MaxLength="100">
                    <Items>
                        <telerik:RadListBoxItem Text="No seleccionado" Value="0" />
                    </Items>
                </telerik:RadListBox>
                <telerik:RadButton ID="btnAgregarPuesto" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="B"></telerik:RadButton>
                <telerik:RadButton ID="btnEliminaPuesto" runat="server" Text="X" AutoPostBack="false" OnClientClicked="CleanPuestoSelection"></telerik:RadButton>

            </div>
        </div>

        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="Label1" name="lblPuesto" runat="server">Jefe inmediato:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadListBox ID="rlbJefe" ReadOnly="false" runat="server" Width="300px" MaxLength="100">
                    <Items>
                        <telerik:RadListBoxItem Text="No seleccionado" Value="0" />
                    </Items>
                </telerik:RadListBox>
                <telerik:RadButton ID="btnAgregarJefe" OnClientClicked="OpenSelectionWindow" AutoPostBack="false" runat="server" Text="B"></telerik:RadButton>
                <telerik:RadButton ID="btnBorrarJefe" runat="server" Text="X" AutoPostBack="false" OnClientClicked="CleanJefeSelection"></telerik:RadButton>
            </div>
        </div>

        <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label>Fecha de ingreso: </label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadDatePicker runat="server" ID="rdpFechaIngreso" Width="150px" ></telerik:RadDatePicker>
            </div>
        </div>
               <div style="clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label>Salario: </label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadNumericTextBox runat="server" ID="txtSueldo" NumberFormat-DecimalDigits="2" Width="150px"></telerik:RadNumericTextBox>
            </div>
        </div>             
    </fieldset>
    </div>
    <div style="clear: both; height: 10px;"></div>

    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnGurdar" Text="Guardar" AutoPostBack="true" OnClick="btnGurdar_Click"></telerik:RadButton>
        </div>

        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>


</asp:Content>
