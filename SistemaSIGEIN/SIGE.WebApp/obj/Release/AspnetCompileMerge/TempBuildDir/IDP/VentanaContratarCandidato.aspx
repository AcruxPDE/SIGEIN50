<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaContratarCandidato.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaContratarCandidato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <script type="text/javascript">
        function OpenSelectionWindow(sender, args) {

            var vBtnPlaza = $find("<%= btnAgregarPuesto.ClientID %>");
            var vBtnPlazaJefe = $find("<%= btnAgregarJefe.ClientID %>");
            //var vbtnRequisicion = $find("<= btnSelecionarPuestoRequisicion.ClientID %>");

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 20
            };

            if (sender == vBtnPlaza) {
                openChildDialog("/Comunes/SeleccionPlaza.aspx?TipoSeleccionCl=VACANTES", "winSeleccion", "Selección de la plaza a ocupar", windowProperties);
            }

            if (sender == vBtnPlazaJefe) {
                var list = $find("<%=rlbPuesto.ClientID %>");
                openChildDialog("/Comunes/SeleccionPlaza.aspx?CatalogoCl=PLAZAJEFE&TipoSeleccionCl=JEFE", "winSeleccion", "Selección del jefe inmediato", windowProperties);
            }

            //if (sender == vbtnRequisicion) {
            //    var idCandidato = '<= vIdCandidato %>';
            //    windowProperties.width = 900;
            //    openChildDialog("/Comunes/SelectorRequisiciones.aspx?CandidatoId=" + idCandidato, "winSeleccion", "Selección de la requisición", windowProperties);
            //}

        }

        function useDataFromChild(pPuestos) {
            if (pPuestos != null) {
                var vPuestoSeleccionado = pPuestos[0];

                var vCatalogo = vPuestoSeleccionado.clTipoCatalogo;
                var vclPlazaSuperior = vPuestoSeleccionado.clPlazaSuperior;

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
            if (list != undefined & text != undefined & text != "&nbsp;") {
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

    <div>
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Folio de solicitud:</label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtClaveSolicitud" runat="server" style="width: 300px;"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Candidato: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCandidato" runat="server" style="width: 300px;"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Fecha de solicitud: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtFechaSolicitud" runat="server" style="width: 300px;"></span>
                </td>
            </tr>
        </table>
    </div>

    <div style="clear: both;"></div>

    <fieldset>
        <legend>
            <label>Datos generales</label>
        </legend>

        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label>No. de empleado: </label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox runat="server" ID="txtClave" Width="200px"></telerik:RadTextBox>
            </div>
        </div>

        <div style="clear: both;"></div>

        <%--        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="Label2" name="lblPuesto" runat="server">Requisición asociada:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadListBox ID="rlbRequisicion" ReadOnly="false" runat="server" Width="300px" MaxLength="100">
                    <Items>
                        <telerik:RadListBoxItem Text="No seleccionado" Value="0" />
                    </Items>
                </telerik:RadListBox>
                <telerik:RadButton ID="btnSelecionarPuestoRequisicion" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionWindow"></telerik:RadButton>
                <telerik:RadButton ID="btnEliminarRequisicion" runat="server" Text="X" AutoPostBack="false" OnClientClicked="CleanRequisicionSelection"></telerik:RadButton>
            </div>
        </div>--%>

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
<%--        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label id="lblEmpresa" name="lblPuesto" runat="server">Empresa:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadComboBox
                    Filter="Contains" runat="server" ID="cmbEmpresa" Width="200" MarkFirstMatch="true"
                    AutoPostBack="false" DropDownWidth="230">
                </telerik:RadComboBox>
            </div>
        </div>--%>

        <div style="clear: both;"></div>

        <div class="ctrlBasico">
            <div class="divControlIzquierda">
                <label>Salario: </label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadNumericTextBox runat="server" ID="txtSueldo" NumberFormat-DecimalDigits="2" Width="200px"></telerik:RadNumericTextBox>
            </div>
        </div>

    </fieldset>

    <div style="clear: both; height: 10px;"></div>

    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnGurdar" Text="Guardar" OnClick="btnGurdar_Click"></telerik:RadButton>
        </div>

        <div class="ctrlBasico">
            <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="closeWindow"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server"></telerik:RadWindowManager>
</asp:Content>
