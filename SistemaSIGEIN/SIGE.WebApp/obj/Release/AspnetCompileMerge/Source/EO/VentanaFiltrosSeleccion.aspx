<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaFiltrosSeleccion.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaFiltrosSeleccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OnCloseWindow() {
            GetRadWindow().Close();
        }

        function OpenSelectionDepartamento() {
            openChildDialog("../Comunes/SeleccionArea.aspx?CatalogoCl=INDICE_DEPARTAMENTO", "WinCuestionario", "Selección de departamento");
        }
        function OpenSelectionGenero() {
            openChildDialog("../Comunes/SeleccionCatalogos.aspx?ID_CATALOGO=2" + "&CatalogoCl=INDICE_GENERO", "WinCuestionario", "Selección de género");
        }

        function OpenSelectionAdicionales() {
            openChildDialog("../Comunes/SeleccionAdscripciones.aspx?MultiSeleccion=1" + "&ClLista=IS", "WinConsultaPersonal", "Selección de campos adicionales");
        }


        function DeleteDepartamento() {
            var vListBox = $find("<%=rlbDepartamento.ClientID %>");
            Delete(vListBox);
        }

        function DeleteAdicionales() {
            var vListAdscripcion = $find("<%= rlbAdicionales.ClientID %>");
             Delete(vListAdscripcion);
        }

        function DeleteGenero() {
            var vListBox = $find("<%=rlbGenero.ClientID %>");
            Delete(vListBox);
        }

        function generateDataForParent() {
            var vAreas = [];
                    var vArea = {
                        clTipoCatalogo: "FILTROS",
                        fgAplicados: "<%= vFgFiltroSeleccionado %>",
                        fgEvaluados: "<%= vFgEvaluados %>"
                    };
            vAreas.push(vArea);
                sendDataToParent(vAreas);
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var arr = [];
                var vDatosSeleccionados = pDato[0];
                switch (pDato[0].clTipoCatalogo) {
                    case "INDICE_GENERO":
                        list = $find("<%=rlbGenero.ClientID %>");
                        InsertGenero(list, vDatosSeleccionados);
                        break;
                    case "INDICE_DEPARTAMENTO":
                        var vListBox = $find("<%=rlbDepartamento.ClientID %>");
                         InsertDepartamentos(pDato, vListBox);
                         break;
                     case "ADSCRIPCION":
                         var vListBox = $find("<%= rlbAdicionales.ClientID%>");
                             InsertAdicionales(pDato, vListBox);
                         break;
                 }
             }
        }

        function InsertGenero(list, vSelectedData) {
            if (list != undefined) {
                list.trackChanges();
                var items = list.get_items();
                items.clear();
                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(vSelectedData.nbCatalogoValor);
                item.set_value(vSelectedData.clCatalogoValor);
                item.set_selected(true);
                items.add(item);
                list.commitChanges();
            }
        }

        function InsertDepartamentos(pDato, pListBox) {
            var arrSeleccion = [];
            for (var i = 0; i < pDato.length; i++)
                arrSeleccion.push({
                    idItem: pDato[i].idArea,
                    nbItem: pDato[i].nbArea
                });

            OrdenarSeleccion(arrSeleccion, pListBox);
        }

        function InsertAdicionales(pDato, pListBox) {
            var arrSeleccion = [];
            for (var i = 0; i < pDato.length; i++)
                arrSeleccion.push({
                    idItem: pDato[i].idCampo,
                    nbItem: pDato[i].nbValor
                });
            OrdenarSeleccion(arrSeleccion, pListBox);
        }

        function OrdenarSeleccion(pSeleccion, vListBox) {
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

        function Delete(vListBox) {
            var vSelectedItems = vListBox.get_selectedItems();
            vListBox.trackChanges();
            if (vSelectedItems)
                vSelectedItems.forEach(function (item) {
                    vListBox.get_items().remove(item);
                });
            vListBox.commitChanges();
        }

      
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
        <telerik:RadSplitter runat="server" ID="rsAyuda" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpDatos" runat="server" Height="100%" Width="100%">
                    <div style="height: calc(100% - 50px);">
                <div style="clear: both; height: 20px"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="lblDepartamentoDistribucion" name="lblDepartamento" runat="server">Departamento:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="rlbDepartamento" Width="200px" Height="100px" runat="server" ValidationGroup="vgDepartamentoDis" ></telerik:RadListBox>
                        <telerik:RadButton ID="btnDistribucionSeleccionarDep" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgDepartamentoDis" OnClientClicked="OpenSelectionDepartamento"></telerik:RadButton>
                        <telerik:RadButton ID="btnDistribucionEliminaDep" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgDepartamentoDis" OnClientClicked="DeleteDepartamento"></telerik:RadButton>
                    </div>
                </div>

                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="Label3" name="lblGenero" runat="server">Género:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="rlbGenero" Width="100" Height="35px" runat="server" ValidationGroup="vgGeneroDis"></telerik:RadListBox>
                        <telerik:RadButton ID="btnDistribucionSeleccionarGen" runat="server" Text="B" AutoPostBack="false" OnClientClicked="OpenSelectionGenero"></telerik:RadButton>
                        <telerik:RadButton ID="btnDistribucionEliminaGen" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgGeneroDis" OnClientClicked="DeleteGenero"></telerik:RadButton>
                    </div>
                </div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <label id="Label2" name="lbAdscripciones" runat="server">Adicionales:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadListBox ID="rlbAdicionales" Width="200" Height="100px" runat="server" ValidationGroup="vgAdicionales"></telerik:RadListBox>
                        <telerik:RadButton ID="btnBDist" runat="server" Text="B" AutoPostBack="false" ValidationGroup="vgAdicionales" OnClientClicked="OpenSelectionAdicionales"></telerik:RadButton>
                        <telerik:RadButton ID="btnXDist" runat="server" Text="X" AutoPostBack="false" ValidationGroup="vgAdicionales" OnClientClicked="DeleteAdicionales"></telerik:RadButton>
                    </div>
                </div>
                <div style="clear: both; height: 20px"></div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda">
                        <telerik:RadButton RenderMode="Lightweight" ID="rbEdad" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                            AutoPostBack="false">
                        </telerik:RadButton>
                        <label id="Label4" name="lblEdad" runat="server">Edad:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadNumericTextBox runat="server" ID="rntEdadInicial" NumberFormat-DecimalDigits="0" Value="18" Name="rnEdadInicial" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                        a
                                        <telerik:RadNumericTextBox runat="server" ID="rntEdadFinal" NumberFormat-DecimalDigits="0" Value="65" Name="rnEdadFinal" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                        años.
                    </div>
                </div>
                <div class="ctrlBasico">
                    <div class="divControlIzquierda" style="margin-left: 95px">
                        <telerik:RadButton RenderMode="Lightweight" ID="rbAntiguedad" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                            AutoPostBack="false">
                        </telerik:RadButton>
                        <label id="Label5" name="lblEdad" runat="server">Antigüedad:</label>
                    </div>
                    <div class="divControlDerecha">
                        <telerik:RadNumericTextBox runat="server" ID="rntAntiguedadInicial" NumberFormat-DecimalDigits="0" Value="0" Name="rnAntiguedadInicial" Width="60px" MinValue="0" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                        a
                                        <telerik:RadNumericTextBox runat="server" ID="rntAntiguedadFinal" NumberFormat-DecimalDigits="0" Value="30" Name="rnAtiguedadFinal" Width="60px" MinValue="1" ShowSpinButtons="true"></telerik:RadNumericTextBox>
                        años.
                    </div>
                </div>
                </div>
                <div style="clear: both; height: 5px"></div>
                <div class="divControlDerecha">
                    <telerik:RadButton ID="btnAplicar" runat="server" name="btnAplicar" AutoPostBack="true" Text="Aplicar" Width="100" OnClick="btnAplicar_Click"></telerik:RadButton>
                    <telerik:RadButton ID="btnCancelar" runat="server" name="btnCancelar" Text="Cancelar" Width="100" OnClientClicked="OnCloseWindow"></telerik:RadButton>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Width="30">
                <telerik:RadSlidingZone ID="rszAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Ayuda" Width="250" MinWidth="250" Height="100%">
                        <div style="padding: 10px; text-align: justify;">
                            Este proceso permite seleccionar los filtros para elegir a los evaluadores de manera confidencial. Por ejemplo si tu deseas aplicarlo a todos los empleados entre 18 y 30 años del sexo Masculino
                            podras elegirlo en los parametros de configuración y para palicarlos dar clic en Aplicar.
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>    
</asp:Content>
