<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PDE/ContextPDE.master"  CodeBehind="Configuracion.aspx.cs" Inherits="SIGE.WebApp.PDE.Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">
        function ShowEditForm() {
            OpenWindow("edit");
            return false;
        }
        function confirmarAccion(sender, args, clAccion) {
            var masterTable = $find("<%= grdPlantillas.ClientID %>").get_masterTableView();
             var selectedItem = masterTable.get_selectedItems()[0];
             if (selectedItem != undefined) {
                 var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_PLANTILLA_SOLICITUD").innerHTML;
                 var mensaje;

                 switch (clAccion) {
                     case "eliminar":
                         mensaje = "¿Deseas eliminar la plantilla " + vNombre + "?, este proceso no podrá revertirse"
                         break;
                     case "general":
                         mensaje = "¿Deseas establecer la plantilla " + vNombre + " por defecto?"
                         break;
                 }

                 var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

                 radconfirm(mensaje, callBackFunction, 400, 170, null, "Aviso");
                 args.set_cancel(true);

             }
             else {
                 radalert("Selecciona una plantilla.", 400, 150, "Aviso");
                 args.set_cancel(true);
             }
         }
        function confirmarEliminarCampo(sender, args) {
            var masterTable = $find("<%= grdPlantillas.ClientID %>").get_masterTableView();
              var selectedItem = masterTable.get_selectedItems()[0];
              var vFgAbrirVentana = false;
              var vDsMensajeAdvertencia = "";
              if (selectedItem != undefined) {
                  vFgAbrirVentana = (selectedItem.getDataKeyValue("FG_SISTEMA").toLowerCase() == "false");

                  if (vFgAbrirVentana) {
                      var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_CAMPO_FORMULARIO").innerHTML;
                      var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

                      radconfirm("¿Deseas eliminar el campo " + vNombre + "?, este proceso no podrá revertirse", callBackFunction, 400, 170, null, "Aviso");
                      args.set_cancel(true);

                  } else
                      vDsMensajeAdvertencia = "Los campos de sistema no pueden se eliminados.";
              }
              else
                  vDsMensajeAdvertencia = "Selecciona un campo.";

              if (!vFgAbrirVentana) {
                  radalert(vDsMensajeAdvertencia, 400, 150);
                  args.set_cancel(true);
              }

          }

        function OpenWindow(pClAccion) {
            var masterTable = $find("<%= grdPlantillas.ClientID %>").get_masterTableView();
            var selectedItem = masterTable.get_selectedItems()[0];
            if (selectedItem != undefined) {
                var vNombre = masterTable.getCellByColumnUniqueName(selectedItem, "NB_PLANTILLA_SOLICITUD").innerHTML;
                var vIdPlantilla = selectedItem.getDataKeyValue("ID_PLANTILLA_SOLICITUD");
                var vClTipoPlantilla = selectedItem.getDataKeyValue("CL_FORMULARIO");
                var vURL = "VentanaPlantillaFormulario.aspx";
                var vTitulo = "Agregar Plantilla";
                if (vIdPlantilla != null) {
                    vURL = vURL + "?PlantillaId=" + vIdPlantilla + "&AccionCl=" + pClAccion + "&PlantillaTipoCl=" + vClTipoPlantilla;
                    switch (pClAccion) {
                        case "edit":
                            vTitulo = "Editar plantilla";
                            break;
                        case "copy":
                            vTitulo = "Copiar plantilla '" + vNombre + "'";
                            break;
                    }
                }
                var windowProperties = {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };
             
                var oWin = window.radopen(vURL, "winPlantillas", document.documentElement.clientWidth - 20, document.documentElement.clientHeight - 20);
                oWin.set_title(vTitulo);
                //openChildDialog(vURL, "winPlantillas", vTitulo, windowProperties);
            }
            else
                radalert("Selecciona un plantilla.", 400, 150, "Aviso");
        }

        function onCloseWindow(oWnd, args) {
            $find("<%= grdPlantillas.ClientID %>").get_masterTableView().rebind();
        }

        function onCloseFieldWindow(oWnd, args) {

        }
        function confirmarEliminar(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) { if (shouldSubmit) { this.click(); } });

            radconfirm("¿Seguro que deseas eliminar", callBackFunction, 400, 170, null, "Aviso");
            args.set_cancel(true);
        }
        function ShowInsertForm() {
            OpenWindow("copy");
            return false;
        }
        function confirmarEstablecerGeneral(sender, args) {
            confirmarAccion(sender, args, "general");
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <label class="labelTitulo" >Configuración de plantillas</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdPlantillas" runat="server" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdPlantillas_NeedDataSource" Height="100%" Width="950" AllowSorting="true">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView ClientDataKeyNames="ID_PLANTILLA_SOLICITUD,CL_FORMULARIO" DataKeyNames="ID_PLANTILLA_SOLICITUD,CL_FORMULARIO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Tipo de plantilla" DataField="CL_FORMULARIO" UniqueName="CL_FORMULARIO">
                        <%--HASTA QUE TENGAMOS TIEMPO DE IMPLEMENTAR ESTE TIPO DE FILTRO--%>
                        <FilterTemplate>
                            <telerik:RadComboBox ID="APComboPlantilla" Width="130" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("CL_FORMULARIO").CurrentFilterValue %>'
                                runat="server" OnClientSelectedIndexChanged="APComboPlantillaIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Text="TODOS" Value="" />
                                    <telerik:RadComboBoxItem Text="SOLICITUD" Value="SOLICITUD" />
                                    <telerik:RadComboBoxItem Text="INVENTARIO" Value="INVENTARIO" />
                                    <telerik:RadComboBoxItem Text="DESCRIPTIVO" Value="DESCRIPTIVO" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">
                                <script type="text/javascript">
                                    function APComboPlantillaIndexChanged(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        tableView.filter("CL_FORMULARIO", args.get_item().get_value(), "EqualTo");
                                    }
                                </script>
                            </telerik:RadScriptBlock>
                        </FilterTemplate>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="240" FilterControlWidth="130" HeaderText="Nombre" DataField="NB_PLANTILLA_SOLICITUD" UniqueName="NB_PLANTILLA_SOLICITUD"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="150" HeaderText="Descripción" DataField="DS_PLANTILLA_SOLICITUD" UniqueName="DS_PLANTILLA_SOLICITUD"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90" FilterControlWidth="20" HeaderText="Por defecto" DataField="FG_GENERAL_CL" UniqueName="FG_GENERAL_CL"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="height: 9px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditarPlantilla" runat="server" Text="Editar" AutoPostBack="false" OnClientClicked="ShowEditForm"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminarPlantilla" runat="server" Text="Eliminar" AutoPostBack="true" OnClientClicking="confirmarEliminar" OnClick="btnEliminarPlantilla_Click"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnCopiarPlantilla" runat="server" Text="Copiar" AutoPostBack="false"  OnClientClicked="ShowInsertForm" ></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEstablecerGeneral" runat="server" Text="Establecer por defecto" AutoPostBack="true"   OnClientClicking="confirmarEstablecerGeneral" OnClick="btnEstablecerGeneral_Click"></telerik:RadButton>
    </div>
     <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winCamposFormulario" runat="server" Title="Campo Formulario" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="onCloseFieldWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winPlantillas" runat="server" Title="Solicitud" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <%--<telerik:RadWindow Height="500px" Width="900px" ReloadOnShow="true" ShowContentDuringLoad="false" OnClientClose="onCloseWindow"></telerik:RadWindow>--%>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>