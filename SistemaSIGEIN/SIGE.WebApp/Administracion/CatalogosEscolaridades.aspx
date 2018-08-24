<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogosEscolaridades.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogosEscolaridades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">


            var idEscolaridad = "";

            function obtenerIdFila() {
                var grid = $find("<%=grdEscolaridades.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idEscolaridad = SelectDataItem.getDataKeyValue("ID_ESCOLARIDAD");
                }
            }


            function onCloseWindow(oWnd, args) {
                idEscolaridad = "";
                $find("<%=grdEscolaridades.ClientID%>").get_masterTableView().rebind();
            }

            

            function ShowPopupEditarEscolaridades() {
                var MasterTable = $find("<%=grdEscolaridades.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                var row = selectedRows[0];
                if (selectedRows != "") {
                    CELL_ID = MasterTable.getCellByColumnUniqueName(row, "ID_ESCOLARIDAD");
                    var combo = $find('<%=cmbEscolaridades.ClientID %>');
                    var vvalor_combo = combo.get_selectedItem().get_value();

                    if ((CELL_ID != null)) {

                        var oWnd = radopen("VentanaCatalogoEscolaridades.aspx?&ID=" + CELL_ID.innerHTML + "&TIPO=Editar" + "&ID_NIVEL_ESCOLARIDAD=" + vvalor_combo, "RWPopupmodalCatalogoGenericoEditar");
                        oWnd.set_title("Editar Escolaridad");
                    }
                } else {
                    radalert("No has seleccionado un registro.", 400, 150, "");
                }
            }



            function ShowPopupAgregarEscolaridades() {
                var combo = $find('<%=cmbEscolaridades.ClientID %>');
                var vvalor_combo = combo.get_selectedItem().get_value();
                if (vvalor_combo != undefined) {
                    var oWnd = radopen("VentanaCatalogoEscolaridades.aspx?&TIPO=Agregar" + "&ID_NIVEL_ESCOLARIDAD=" + vvalor_combo, "RWPopupmodalCatalogoGenericoEditar");
                    oWnd.set_title("Agregar Escolaridad");
                }
                else { radalert("No has seleccionado el nivel Escolaridad.", 400, 150, ""); }

            }



            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grdEscolaridades.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                var row = selectedRows[0];

                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_ESCOLARIDAD");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar la Escolaridad ' + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                        //Deseas eliminar ___________?, este proceso no podrá revertirse.
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione una escolaridad.", 400, 150, "");
                    args.set_cancel(true);
                }
            }


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

        </script>
    </telerik:RadCodeBlock>



    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEscolaridades" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEscolaridades" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEscolaridades"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbEscolaridades">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEscolaridades" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
          

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <label class="labelTitulo">Escolaridades</label>
    <div class="ctrlBasico">
        <telerik:RadComboBox Filter="Contains" runat="server" ID="cmbEscolaridades" Width="250" MarkFirstMatch="true" AutoPostBack="true"
            OnSelectedIndexChanged="cmbEscolaridades_SelectedIndexChanged" EmptyMessage="Seleccione..." DropDownWidth="430">
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblCL_NIVEL_ESCOLARIDAD" Text="Tipo" runat="server" Width="200px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" Text="Nombre" runat="server" Width="200px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblCL_NIVEL_ESCOLARIDAD" Text='<%# DataBinder.Eval(Container.DataItem, "CL_TIPO_ESCOLARIDAD") %>' runat="server" Width="200px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "DS_NIVEL_ESCOLARIDAD") %>' runat="server" Width="200px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" Text='<%# DataBinder.Eval(Container.DataItem,  "ID_NIVEL_ESCOLARIDAD") %>' runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </telerik:RadComboBox>

    </div>
     <div style="clear: both;"></div>

    <div id="ContenidoGrid" class="ctrlBasico" style="height: calc(100% - 130px);">
       
        <telerik:RadGrid ID="grdEscolaridades" ShowHeader="true" runat="server" AllowPaging="true"
            AllowSorting="true" GroupPanelPosition="Top" GridLines="None"
            AllowFilteringByColumn="true" Height="100%" Width="1000px"
            ClientSettings-EnablePostBackOnRowClick="false"
            OnNeedDataSource="grdEscolaridades_NeedDataSource"
            OnItemDataBound="grdEscolaridades_ItemDataBound"
            OnItemCommand="OnItemCommand"
            HeaderStyle-Font-Bold="true"
            >
            <GroupingSettings CaseSensitive="False" />
            <ExportSettings FileName="Catalogo de Escolaridades" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" ></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />


            <MasterTableView ClientDataKeyNames="ID_ESCOLARIDAD" DataKeyNames="ID_ESCOLARIDAD" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />

                <Columns>

                    <telerik:GridBoundColumn AutoPostBackOnFilter="true"  HeaderText="Clave" CurrentFilterFunction="Contains" DataField="CL_ESCOLARIDAD" UniqueName="CL_NIVEL_ESCOLARIDAD" HeaderStyle-Width="200" FilterControlWidth="130" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderText="Nombre" CurrentFilterFunction="Contains" DataField="NB_ESCOLARIDAD" UniqueName="NB_ESCOLARIDAD" HeaderStyle-Width="400" FilterControlWidth="330"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderText="Activo" CurrentFilterFunction="Contains" DataField="NB_ACTIVO" UniqueName="Activo" FilterControlWidth="25" HeaderStyle-Width="95"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderText="Id_Escolaridad" CurrentFilterFunction="Contains" DataField="ID_ESCOLARIDAD" UniqueName="ID_ESCOLARIDAD" HeaderStyle-Width="0" FilterControlWidth="0"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>

            </MasterTableView>
        </telerik:RadGrid>
    </div>


    <div style="clear: both;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowPopupAgregarEscolaridades" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupEditarEscolaridades" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" AutoPostBack="true" OnClick="btnEliminar_click" OnClientClicking="ConfirmarEliminar"></telerik:RadButton>
    </div>


    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RWPopupmodalCatalogoGenericoEditar" runat="server" Title="Agregar Catálogos de Escolaridades" Height="260"
                Width="600" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindow" >
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadWindowManager ID="grdMensaje" runat="server"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
