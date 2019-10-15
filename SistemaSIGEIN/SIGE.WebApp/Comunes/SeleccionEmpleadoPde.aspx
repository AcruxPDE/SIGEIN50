<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/ContextHTML.master" CodeBehind="SeleccionEmpleadoPde.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionEmpleadoPde" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        var vEmpleados = [];

        function generateDataForParent() {
            if (addSelection()) {
                var dataToReturn = [];
                dataToReturn = vEmpleados;
                sendDataToParent(dataToReturn);
            }
        }

        function addSelection() {
            var info = null;
            var masterTable = $find("<%= grdEmpleados.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vEmpleado = {
                        idEmpleado: selectedItem.getDataKeyValue("M_EMPLEADO_ID_EMPLEADO"),
                        idEmpleado_pde: selectedItem.getDataKeyValue("M_EMPLEADO_ID_EMPLEADO_PDE"),
                        clEmpleado: masterTable.getCellByColumnUniqueName(selectedItem, "M_EMPLEADO_CL_EMPLEADO").innerHTML,
                        nbCorreoElectronico: masterTable.getCellByColumnUniqueName(selectedItem, "M_EMPLEADO_CL_CORREO_ELECTRONICO").innerHTML,
                        nbEmpleado: masterTable.getCellByColumnUniqueName(selectedItem, "M_EMPLEADO_NB_EMPLEADO_COMPLETO").innerHTML,
                        nbPuesto: masterTable.getCellByColumnUniqueName(selectedItem, "M_PUESTO_NB_PUESTO").innerHTML,
                        clTipoCatalogo: "<%= vClCatalogo %>"
                    };
                    if (!existeElemento(vEmpleado)) {
                        vEmpleados.push(vEmpleado);
                        var vLabel = document.getElementsByName('lblAgregar')[0];
                        vLabel.innerText = "Agregados: " + vEmpleados.length;
                    }
                }
                return true;
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un empleado.", 400, 150, "Aviso");
            }

            return false;
        }

        function existeElemento(pEmpleado) {
            for (var i = 0; i < vEmpleados.length; i++) {
                var vValue = vEmpleados[i];
                if (vValue.idEmpleado == pEmpleado.idEmpleado)
                    return true;
            }
            return false;
        }

        function cancelarSeleccion() {
            sendDataToParent(null);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdEmpleados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftrGrdEmpleados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftrGrdEmpleados" />
                    <telerik:AjaxUpdatedControl ControlID="grdEmpleados" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: 100%;">
        <telerik:RadSplitter ID="splEmpleados" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnGridEmpleados" runat="server" Height="100%">
                <div style="height: calc(100% - 54px);">
                    <telerik:RadGrid ID="grdEmpleados" HeaderStyle-Font-Bold="true" runat="server" Height="100%" AllowMultiRowSelection="true" EnableHeaderContextMenu="true" AllowSorting="true" AutoGenerateColumns="false">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="M_EMPLEADO_ID_EMPLEADO, M_EMPLEADO_ID_EMPLEADO_PDE " EnableColumnsViewState="false" DataKeyNames="M_EMPLEADO_ID_EMPLEADO , M_EMPLEADO_ID_EMPLEADO_PDE" AllowPaging="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <Columns>
                                <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="No. de empleado" DataField="M_EMPLEADO_CL_EMPLEADO" UniqueName="M_EMPLEADO_CL_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Nombre completo" DataField="M_EMPLEADO_NB_EMPLEADO_COMPLETO" UniqueName="M_EMPLEADO_NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre" DataField="M_EMPLEADO_NB_EMPLEADO" UniqueName="M_EMPLEADO_NB_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido paterno" DataField="M_EMPLEADO_NB_APELLIDO_PATERNO" UniqueName="M_EMPLEADO_NB_APELLIDO_PATERNO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido materno" DataField="M_EMPLEADO_NB_APELLIDO_MATERNO" UniqueName="M_EMPLEADO_NB_APELLIDO_MATERNO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Puesto" DataField="M_PUESTO_NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave del área" DataField="M_DEPARTAMENTO_CL_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_CL_DEPARTAMENTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Área" DataField="M_DEPARTAMENTO_NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Género" DataField="M_EMPLEADO_CL_GENERO" UniqueName="M_EMPLEADO_CL_GENERO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Estado civil" DataField="M_EMPLEADO_CL_ESTADO_CIVIL" UniqueName="M_EMPLEADO_CL_ESTADO_CIVIL"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre del cónyuge" DataField="M_EMPLEADO_NB_CONYUGUE" UniqueName="M_EMPLEADO_NB_CONYUGUE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="RFC" DataField="M_EMPLEADO_CL_RFC" UniqueName="M_EMPLEADO_CL_RFC"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="CURP" DataField="M_EMPLEADO_CL_CURP" UniqueName="M_EMPLEADO_CL_CURP"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="NSS" DataField="M_EMPLEADO_CL_NSS" UniqueName="M_EMPLEADO_CL_NSS"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Tipo sanguíneo" DataField="M_EMPLEADO_CL_TIPO_SANGUINEO" UniqueName="M_EMPLEADO_CL_TIPO_SANGUINEO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nacionalidad" DataField="M_EMPLEADO_CL_NACIONALIDAD" UniqueName="M_EMPLEADO_CL_NACIONALIDAD"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="País" DataField="M_EMPLEADO_NB_PAIS" UniqueName="M_EMPLEADO_NB_PAIS"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Entidad federativa" DataField="M_EMPLEADO_NB_ESTADO" UniqueName="M_EMPLEADO_NB_ESTADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Municipio" DataField="M_EMPLEADO_NB_MUNICIPIO" UniqueName="M_EMPLEADO_NB_MUNICIPIO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Colonia" DataField="M_EMPLEADO_NB_COLONIA" UniqueName="M_EMPLEADO_NB_COLONIA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Calle" DataField="M_EMPLEADO_NB_CALLE" UniqueName="M_EMPLEADO_NB_CALLE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Exterior" DataField="M_EMPLEADO_NO_EXTERIOR" UniqueName="M_EMPLEADO_NO_EXTERIOR"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Interior" DataField="M_EMPLEADO_NO_INTERIOR" UniqueName="M_EMPLEADO_NO_INTERIOR"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Código postal" DataField="M_EMPLEADO_CL_CODIGO_POSTAL" UniqueName="M_EMPLEADO_CL_CODIGO_POSTAL"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Empresa" DataField="M_EMPLEADO_NB_EMPRESA" UniqueName="M_EMPLEADO_NB_EMPRESA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Correo electrónico" DataField="M_EMPLEADO_CL_CORREO_ELECTRONICO" UniqueName="M_EMPLEADO_CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Natalicio" DataField="M_EMPLEADO_FE_NACIMIENTO" UniqueName="M_EMPLEADO_FE_NACIMIENTO" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Lugar de nacimiento" DataField="M_EMPLEADO_DS_LUGAR_NACIMIENTO" UniqueName="M_EMPLEADO_DS_LUGAR_NACIMIENTO"></telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de alta" DataField="M_EMPLEADO_FE_ALTA" UniqueName="M_EMPLEADO_FE_ALTA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Fecha de baja" DataField="M_EMPLEADO_FE_BAJA" UniqueName="M_EMPLEADO_FE_BAJA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo" DataField="M_EMPLEADO_MN_SUELDO" UniqueName="M_EMPLEADO_MN_SUELDO" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo variable" DataField="M_EMPLEADO_MN_SUELDO_VARIABLE" UniqueName="M_EMPLEADO_MN_SUELDO_VARIABLE" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Composición del sueldo" DataField="M_EMPLEADO_DS_SUELDO_COMPOSICION" UniqueName="M_EMPLEADO_DS_SUELDO_COMPOSICION"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave de la empresa" DataField="C_EMPRESA_CL_EMPRESA" UniqueName="C_EMPRESA_CL_EMPRESA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Nombre de la empresa" DataField="C_EMPRESA_NB_EMPRESA" UniqueName="C_EMPRESA_NB_EMPRESA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Razón social" DataField="C_EMPRESA_NB_RAZON_SOCIAL" UniqueName="C_EMPRESA_NB_RAZON_SOCIAL"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Estado" DataField="M_EMPLEADO_CL_ESTADO_EMPLEADO" UniqueName="M_EMPLEADO_CL_ESTADO_EMPLEADO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Activo" DataField="M_EMPLEADO_FG_ACTIVO" UniqueName="M_EMPLEADO_FG_ACTIVO"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="divControlDerecha" style="padding-top: 10px;">
                    <div class="ctrlBasico">
                        <label runat="server" id="lblAgregar" name="lblAgregar" style="padding-top: 10px; font-weight: lighter;"></label>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnAgregar" runat="server" name="btnAgregar" Text="Agregar" AutoPostBack="false" OnClientClicked="addSelection"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnSeleccion" runat="server" name="btnSeleccion" Text="Seleccionar" AutoPostBack="false" OnClientClicked="generateDataForParent"></telerik:RadButton>
                    </div>
                    <div class="ctrlBasico">
                        <telerik:RadButton ID="btnCancelar" runat="server" name="" Text="Cancelar" AutoPostBack="false" OnClientClicked="cancelarSeleccion"></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnOpciones" runat="server" Width="30">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="slpAdvSearch" runat="server" Title="Búsqueda avanzada" Width="500" MinWidth="500">
                        <div style="padding: 20px;">
                            <telerik:RadFilter runat="server" ID="ftrGrdEmpleados" FilterContainerID="grdEmpleados" ShowApplyButton="true">
                                <ContextMenu Height="300" EnableAutoScroll="true">
                                    <DefaultGroupSettings Height="300" />
                                </ContextMenu>
                            </telerik:RadFilter>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
</asp:Content>
