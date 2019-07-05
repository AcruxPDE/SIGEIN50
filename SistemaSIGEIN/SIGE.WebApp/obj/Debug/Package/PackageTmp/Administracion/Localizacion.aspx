<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="Localizacion.aspx.cs" Inherits="SIGE.WebApp.Administracion.Localizacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- inicio: Controles para uso del grid -->
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="GridCatalogoLista">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="GridCatalogoLista" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbEstados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="GridCatalogoLista" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="cmbMunicipio" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFiltrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="GridCatalogoLista" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radBtnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="GridCatalogoLista" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadBtnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="GridCatalogoLista" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                        args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                        args.get_eventTarget().indexOf("ExportToCsvButton") >= 0
                       || args.get_eventTarget().indexOf("ChangePageSizeLabel") >= 0
                       || args.get_eventTarget().indexOf("PageSizeComboBox") >= 0
                       || args.get_eventTarget().indexOf("SaveChangesButton") >= 0
                       || args.get_eventTarget().indexOf("CancelChangesButton") >= 0
                       || args.get_eventTarget().indexOf("Download") >= 0
                       || (args.get_eventTarget().indexOf("Export") >= 0)
                       || (args.get_eventTarget().indexOf("DownloadPDF") >= 0)
                       || (args.get_eventTarget().indexOf("download_file") >= 0)
                    )
                    args.set_enableAjax(false);
            }

            function onCloseWindow(oWnd, args) {
                vIdCatalogo = "";
                vNbColonia = "";
                $find("<%=GridCatalogoLista.ClientID%>").get_masterTableView().rebind();
            }


            var vIdCatalogo = "";
            var vNbColonia = "";

            function obtenerIdFila() {
                var grid = $find("<%=GridCatalogoLista.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    vIdCatalogo = SelectDataItem.getDataKeyValue("ID_COLONIA");
                    vNbColonia = MasterTable.getCellByColumnUniqueName(row, "NB_COLONIA").innerHTML;
                }
            }


            function editarLocalizacion() {

                obtenerIdFila();

                var vURL = "VentanaLocalizacion.aspx";
                var vTitulo = "Editar Colonia";

                //Validacion
                var vEstado = document.getElementById("<%= cmbEstados.ClientID %>").value;
            if (vEstado == "") {
                radalert("Selecciona un Estado.", 400, 150, "Localización");
                return false;
            }

            var vMunicipio = document.getElementById("<%=  cmbMunicipio.ClientID%>").value;
            if (vMunicipio == "") {
                radalert("Selecciona un municipio.", 400, 150, "Localización");
                return false;
            }

            vMunicipio = $find('<%=cmbMunicipio.ClientID %>');
                vMunicipio = vMunicipio.get_selectedItem().get_value();

                vEstado = $find('<%=cmbEstados.ClientID %>')
                vEstado = vEstado.get_selectedItem().get_value();

                if (vIdCatalogo != "") {
                    var oWnd = radopen(vURL + "?pID=" + vIdCatalogo + "&pClEstado=" + vEstado + "&pClMunicipio=" + vMunicipio, "RWPopupmodalColonia");
                    oWnd.set_title(vTitulo);
                } else {
                    radalert("Selecciona una colonia.", 400, 150, "Localización");
                }

            }

            function nuevoLocalizacion() {
                var vURL = "VentanaLocalizacion.aspx";
                var vTitulo = "Agregar Colonia";

                //Validacion
                var vEstado = document.getElementById("<%= cmbEstados.ClientID %>").value;
                if (vEstado == "") {
                    radalert("Selecciona un Estado.", 400, 150, "Localización");
                    return false;
                }

                var vMunicipio = document.getElementById("<%=  cmbMunicipio.ClientID%>").value;
                if (vMunicipio == "") {
                    radalert("Selecciona un municipio.", 400, 150, "Localización");
                    return false;
                }

                vMunicipio = $find('<%=cmbMunicipio.ClientID %>');
                vMunicipio = vMunicipio.get_selectedItem().get_value();

                vEstado = $find('<%=cmbEstados.ClientID %>')
                vEstado = vEstado.get_selectedItem().get_value();

                var oWnd = radopen(vURL + "?pClEstado=" + vEstado + "&pClMunicipio=" + vMunicipio, "RWPopupmodalColonia");
                oWnd.set_title(vTitulo);
            }


            function ConfirmarEliminar(sender, args) {

                obtenerIdFila();

                if (vIdCatalogo != "") {
                    confirmar(sender, args, "¿Deseas eliminar la colonia " + vNbColonia + "?, este proceso no podrá revertirse ");
                    args.set_cancel(true);
                }
                else {
                    radalert("Selecciona una colonia.", 400, 150, "Localización");
                    args.set_cancel(true);
                }
            }


            function confirmar(sender, args, text) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });


                radconfirm(text, callBackFunction, 400, 150, null, "Confirmar");
                //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                args.set_cancel(true);
            }

        </script>
    </telerik:RadCodeBlock>
    <!-- fin: Controles para uso del grid -->
    <label class="labelTitulo" name="lbllocalizacion" id="lbllocalizacion">Localización</label>
    <div style="clear: both;"></div>
    <div id="ContenedorEstado" class="ctrlBasico">
        <div>
            <label id="lblEstado" name="lblEstado">Estado</label>
        </div>
        <div>
            <telerik:RadComboBox ID="cmbEstados"
                runat="server"
                Style="width: 350px"
                Height="200"
                MarkFirstMatch="true"
                EmptyMessage="Selecciona"
                AutoPostBack="true"
                OnSelectedIndexChanged="cmbEstados_SelectedIndexChanged"
                EnableLoadOnDemand="true">
            </telerik:RadComboBox>
        </div>
    </div>

    <div id="ContenedorMunicipio" class="ctrlBasico">
        <div>
            <label id="lblMunicipio" name="lblMunicipio">Delegación o Municipio</label>
        </div>
        <div>
            <telerik:RadComboBox ID="cmbMunicipio"
                runat="server"
                Style="width: 350px"
                Height="200"
                MarkFirstMatch="true"
                EmptyMessage="Selecciona"
                EnableLoadOnDemand="true"
                AutoPostBack="false">
            </telerik:RadComboBox>
        </div>
    </div>

    <div class="ctrlBasico" style="text-align: center; margin-top: 21px;">
        <telerik:RadButton ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" name="btnFiltrar"></telerik:RadButton>
        <%--<asp:Button Text="Filtrar" runat="server" ID="btnFiltrar" name="btnFiltrar" OnClick="btnFiltrar_Click" />--%>
    </div>

    <div style="clear: both;"></div>

    <!-- creación del  grid de colonias -->
    <div id="ContenidoGrid" style="height: calc(100% - 165px)">

        <telerik:RadGrid ID="GridCatalogoLista"
            ShowHeader="true"
            runat="server"
            AllowPaging="true"
            AllowSorting="true"
            GroupPanelPosition="Top"
            Width="960"
            HeaderStyle-Font-Bold="true"
            Height="100%"
            GridLines="None"
            ClientSettings-EnablePostBackOnRowClick="false"
            AllowFilteringByColumn="true"
            OnNeedDataSource="GridCatalogoLista_NeedDataSource"
            OnItemDataBound="GridCatalogoLista_ItemDataBound">
            <ExportSettings ExportOnlyData="true" FileName="Localización" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_COLONIA"
                DataKeyNames="ID_COLONIA"
                ShowHeadersWhenNoRecords="true"
                AutoGenerateColumns="false"
                CommandItemDisplay="Top"
                HorizontalAlign="NotSet"
                EditMode="EditForms">
                <CommandItemSettings
                    ShowAddNewRecordButton="false"
                    ShowExportToExcelButton="true"
                    ShowExportToCsvButton="false"
                    RefreshText="Actualizar"
                    AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Id" DataField="ID_COLONIA" UniqueName="ID_COLONIA" Display="false" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="NB_COLONIA"
                        DataField="NB_COLONIA"
                        HeaderText="Colonia">
                        <FilterTemplate>
                            <telerik:RadComboBox ID="cmbColonia"
                                runat="server"
                                Style="width: 350px"
                                Height="200"
                                MarkFirstMatch="true"
                                EmptyMessage="Selecciona"
                                EnableLoadOnDemand="true"
                                OnLoad="cmbColonia_Load"
                                OnSelectedIndexChanged="cmbColonia_SelectedIndexChanged"
                                AutoPostBack="true">
                            </telerik:RadComboBox>
                            <telerik:RadButton Text="X" title="Sin filtro" runat="server" ID="btnCancelarFiltro" OnClick="btnCancelarFiltro_Click" />
                        </FilterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNombreColonia" runat="server" Text='<%# Eval("NB_COLONIA") %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="460" FilterControlWidth="390" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Colonia" DataField="NB_COLONIA" UniqueName="NB_COLONIA" Display="false" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="150" FilterControlWidth="80" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Código postal " DataField="CL_CODIGO_POSTAL" UniqueName="CL_CODIGO_POSTAL" Display="true" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="300" FilterControlWidth="230" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Tipo de Asentamiento" DataField="NB_TIPO_ASENTAMIENTO" UniqueName="NB_TIPO_ASENTAMIENTO" Display="true" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>

    </div>
    <div style="clear: both; height: 10px"></div>
    <div id="controles">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardar" OnClientClicked="nuevoLocalizacion" AutoPostBack="false" runat="server" Text="Agregar"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEditar" OnClientClicked="editarLocalizacion" AutoPostBack="false" runat="server" Text="Editar" ></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="RadBtnEliminar" runat="server" Text="Eliminar"  OnClientClicking="ConfirmarEliminar" OnClick="RadBtnEliminar_Click"></telerik:RadButton>
        </div>
    </div>

    <!-- controles de ventana modal y alertas -->
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RWPopupmodalColonia"
                runat="server"
                Title="Catálogo Localización"
                Height="280"
                Width="530px"
                Left="5%"
                ReloadOnShow="true"
                ShowContentDuringLoad="false"
                VisibleStatusbar="false"
                VisibleTitlebar="true"
                Behaviors="Close"
                Modal="true"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
