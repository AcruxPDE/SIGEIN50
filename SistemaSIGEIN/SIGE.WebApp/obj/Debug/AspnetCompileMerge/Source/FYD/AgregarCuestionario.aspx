<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="AgregarCuestionario.aspx.cs" Inherits="SIGE.WebApp.FYD.AgregarCuestionario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        fieldset {
            padding-left: 10px;
            /*padding-right: 10px;*/
        }
    </style>
    <script type="text/javascript">
        function OpenEvaluadoSelectionWindow() {
            OpenSelectionWindow("/Comunes/SeleccionEvaluados.aspx?PeriodoId=<%= vIdPeriodo %>", "winSeleccion", "Selección de evaluado");
        }

        function OpenEvaluadorSelectionWindow() {
            var vClRolEvaluador = $find('<%=rcbRolEvaluador.ClientID %>').get_selectedItem().get_value();

            var vUrl = vClRolEvaluador == "OTRO"  ? "/Comunes/SeleccionEvaluadores.aspx?PeriodoId=<%= vIdPeriodo %>" : "/Comunes/SeleccionEmpleado.aspx";

            OpenSelectionWindow(vUrl, "winSeleccion", "Selección de evaluador");
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function DeleteEvaluado() {
            DeleteListItems($find("<%=rlbEvaluado.ClientID %>"));
        }

        function DeleteEvaluador() {
            DeleteListItems($find("<%=rlbEvaluador.ClientID %>"));
        }

        function DeleteListItems(pListBox) {
            var vSelectedItems = pListBox.get_selectedItems();

            pListBox.trackChanges();
            if (vSelectedItems)
                vSelectedItems.forEach(function (item) {
                    pListBox.get_items().remove(item);
                });

            if (pListBox.get_items().get_count() == 0) {
                ChangeListItem("0", "No seleccionado", pListBox);
            }

            pListBox.commitChanges();
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var arrSeleccion = [];
                switch (pDato[0].clTipoCatalogo) {
                    case "EVALUADO":
                        for (var i = 0; i < pDato.length; i++)
                            arrSeleccion.push({
                                idItem: pDato[i].idEvaluado,
                                nbItem: pDato[i].nbEvaluado
                            });
                        OrdenarSeleccion(arrSeleccion, '<%= rlbEvaluado.ClientID %>');
                        break;
                    case "EMPLEADO":
                        for (var i = 0; i < pDato.length; i++)
                            arrSeleccion.push({
                                idItem: pDato[i].idEmpleado,
                                nbItem: pDato[i].nbEmpleado
                            });
                        OrdenarSeleccion(arrSeleccion, '<%= rlbEvaluador.ClientID %>');

                        //var vEvaluador = pDato[0];
                        //var vListBox = $find("<%=rlbEvaluador.ClientID %>");
                        //vListBox.trackChanges();
                        //vListBox.get_items().clear();
                        //ChangeListItem(vEvaluador.idEmpleado, vEvaluador.nbEmpleado, vListBox);
                        //vListBox.commitChanges();
                        break;
                    case "EVALUADOR":
                        for (var i = 0; i < pDato.length; i++)
                            arrSeleccion.push({
                                idItem: pDato[i].idEvaluador,
                                nbItem: pDato[i].nbEvaluador
                            });
                        OrdenarSeleccion(arrSeleccion, '<%= rlbEvaluador.ClientID %>');

                        //var vEvaluador = pDato[0];
                        //var vListBox = $find("<%=rlbEvaluador.ClientID %>");
                        //vListBox.trackChanges();
                        //vListBox.get_items().clear();
                        //ChangeListItem(vEvaluador.idEvaluador, vEvaluador.nbEvaluador, vListBox);
                        //vListBox.commitChanges();
                        break;
                }
            }
        }

        function OrdenarSeleccion(pSeleccion, pNbListaDestino) {
            var vListBox = $find(pNbListaDestino);
            vListBox.trackChanges();

            var items = vListBox.get_items();

            for (var i = 0; i < items.get_count() ; i++) {
                var item = items.getItem(i);
                var itemValue = item.get_value();
                var itemText = item.get_text();
                if (itemValue != "0") {
                    var vFgItemEncontrado = false;
                    for (var j = 0; j < pSeleccion.length; j++)
                        vFgItemEncontrado = vFgItemEncontrado || (pSeleccion[j].idItem == itemValue);
                    if (!vFgItemEncontrado)
                        pSeleccion.push({
                            idItem: itemValue,
                            nbItem: itemText
                        });
                }
            }

            var arrOriginal = [];
            for (var i = 0; i < pSeleccion.length; i++)
                arrOriginal.push(pSeleccion[i].nbItem);

            var arrOrdenados = arrOriginal.slice();

            arrOrdenados.sort();

            var arrItemsOrdenados = [];

            for (var i = 0; i < arrOrdenados.length; i++)
                arrItemsOrdenados.push(pSeleccion[arrOriginal.indexOf(arrOrdenados[i])]);

            items.clear();

            for (var i = 0, len = arrItemsOrdenados.length; i < len; i++)
                ChangeListItem(arrItemsOrdenados[i].idItem, arrItemsOrdenados[i].nbItem, vListBox);

            vListBox.commitChanges();
        }

        function ChangeListItem(pIdItem, pNbItem, pListBox) {
            var items = pListBox.get_items();
            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text(pNbItem);
            item.set_value(pIdItem);
            items.add(item);
            item.set_selected(true);
        }

        function closeWindow() {
            GetRadWindow().close();
        }

        function AllowSelectionChange(sender, eventArgs) {
            var vFgEdicion = "<%= (vIdEvaluadoEvaluador != null) ? "1":"0" %>" == "1";

            if (!vFgEdicion) {
                var oldRolEvaluador = sender.get_selectedItem().get_value();
                var newRolEvaluador = eventArgs.get_item().get_value();
                if ((oldRolEvaluador != newRolEvaluador) && (oldRolEvaluador == 'OTRO' || newRolEvaluador == 'OTRO')) {
                    var vListBox = $find("<%=rlbEvaluador.ClientID %>");
                    vListBox.trackChanges();
                    vListBox.get_items().clear();
                    ChangeListItem("0", "No seleccionado", vListBox);
                    vListBox.commitChanges();
                }
            }
        }

        function generateDataForParent() {
            var vEvaluado = {
                clParent: "<%= vClParent %>",
                clTipoCatalogo: "REBIND"
            };
            var vEvaluados = [];
            vEvaluados.push(vEvaluado);
            sendDataToParent(vEvaluados);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div>
        <div>
            <table class="ctrlTableForm">
                <tr>
                    <td style="text-align: right;">Rol evaluador:</td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="rcbRolEvaluador" runat="server" Width="200" OnClientSelectedIndexChanging="AllowSelectionChange"></telerik:RadComboBox>
                    </td>
                </tr>
            </table>

        </div>
        <fieldset>
            <legend>Evaluados:</legend>
            <div>
                <div class="ctrlBasico">
                    <telerik:RadListBox ID="rlbEvaluado" runat="server" Width="395" Height="200">
                        <Items>
                            <telerik:RadListBoxItem Text="No seleccionado" Value="0" />
                        </Items>
                    </telerik:RadListBox>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnBuscarEvaluado" runat="server" AutoPostBack="false" OnClientClicked="OpenEvaluadoSelectionWindow" Text="+"></telerik:RadButton>
                </div>
                <div class="ctrlBasico">
                    <telerik:RadButton ID="btnEliminarEvaluado" runat="server" AutoPostBack="false" OnClientClicked="DeleteEvaluado" Text="X"></telerik:RadButton>
                </div>
            </div>
        </fieldset>
        <div style="height: 10px;"></div>
        <fieldset>
            <legend>Evaluadores:</legend>
            <div class="ctrlBasico">
                <telerik:RadListBox ID="rlbEvaluador" runat="server" Width="395" Height="200">
                    <Items>
                        <telerik:RadListBoxItem Text="No seleccionado" Value="0" />
                    </Items>
                </telerik:RadListBox>
            </div>
            <div class="ctrlBasico">
                <telerik:RadButton ID="btnBuscarEvaluador" runat="server" AutoPostBack="false" OnClientClicked="OpenEvaluadorSelectionWindow" Text="+"></telerik:RadButton>
            </div>
            <div class="ctrlBasico">
                <telerik:RadButton ID="btnEliminarEvaluador" runat="server" AutoPostBack="false" OnClientClicked="DeleteEvaluador" Text="X"></telerik:RadButton>
            </div>
        </fieldset>
    </div>
    <div style="height: 20px;">
    </div>
    <div class="divControlDerecha">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardarConfiguracion" runat="server" Text="Aceptar" OnClick="btnGuardarConfiguracion_Click"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
