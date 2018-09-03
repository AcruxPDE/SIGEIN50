<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogoClasificacionCompetencias.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoClasificacionCompetencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            var idClasificacion = "";

            function obtenerIdFila() {
                var grid = $find("<%=grvClasificacionCompetencia.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idClasificacion = SelectDataItem.getDataKeyValue("ID_CLASIFICACION_COMPETENCIA");
                }
            }

            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }


            function onCloseWindow(oWnd, args) {
                idClasificacion = "";
                $find("<%=grvClasificacionCompetencia.ClientID%>").get_masterTableView().rebind();

                    }

                    function ShowPopupmodalClasificacionCompetenciaEditar() {
                        obtenerIdFila()

                        if (idClasificacion != "") {
                            var oWnd = radopen("VentanaCatalogoClasificacionComp.aspx?&ID=" + idClasificacion + "&TIPO=Editar", "modalClasCompetenciaEditar");
                            oWnd.set_title("Editar clasificación de competencia");
                        } else {
                            radalert("Selecciona un registro.", 400, 150, "");
                        }
                    }

                    function ShowPopupmodalClasificacionCompetenciaNuevo(sender, args) {
                        var oWnd = radopen("VentanaCatalogoClasificacionComp.aspx?&ID=0&TIPO=Editar", "modalClasCompetenciaEditar");
                        oWnd.set_title("Agregar clasificación de competencia");
                    }




                    function ConfirmarEliminar(sender, args) {
                        var MasterTable = $find("<%=grvClasificacionCompetencia.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                var row = selectedRows[0];

                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_CLASIFICACION_COMPETENCIA");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar la clasificación de competencia ' + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione una clasificación de competencia.", 400, 150, "");
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


    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvClasificacionCompetencia" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvClasificacionCompetencia" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvClasificacionCompetencia" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="grvClasificacionCompetencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvClasificacionCompetencia" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <label class="labelTitulo">Clasificación de Competencias laborales</label>
    <div style="height: calc(100% - 100px)">
        <telerik:RadGrid ID="grvClasificacionCompetencia"
            ShowHeader="true"
            runat="server"
            OnItemDataBound="grvClasificacionCompetencia_ItemDataBound"
            AllowPaging="true"
            AllowSorting="true" GroupPanelPosition="Top" GridLines="None"
            ClientSettings-EnablePostBackOnRowClick="false" AllowFilteringByColumn="true"
            OnNeedDataSource="grvClasificacionCompetencia_NeedDataSource" Height="100%" Width="100%" HeaderStyle-Font-Bold="true">
            <ExportSettings ExportOnlyData="true" FileName="Clasificación de competencias" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_CLASIFICACION_COMPETENCIA" DataKeyNames="ID_CLASIFICACION_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToWordButton="false" ShowExportToPdfButton="false" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="Color" HeaderStyle-Width="60" AllowFiltering="false">
                        <ItemTemplate>
                                <div style="margin:auto; width:25px; border:1px solid gray; background: <%# Eval("CL_COLOR")%>;  border-radius: 5px;">&nbsp;&nbsp;</div>&nbsp;
                                       </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderText="Clave" DataField="CL_CLASIFICACION" AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="50" CurrentFilterFunction="Contains" UniqueName="CL_CLASIFICACION" Display="true" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Categoría" DataField="NB_TIPO_COMPETENCIA" AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="50" CurrentFilterFunction="Contains" UniqueName="NB_TIPO_COMPETENCIA" Display="true" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Clasificación" DataField="NB_CLASIFICACION_COMPETENCIA" AutoPostBackOnFilter="true" HeaderStyle-Width="300" FilterControlWidth="230" CurrentFilterFunction="Contains" UniqueName="NB_CLASIFICACION_COMPETENCIA" Display="true" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Descripción" DataField="DS_CLASIFICACION_COMPETENCIA" AutoPostBackOnFilter="true" FilterControlWidth="230" CurrentFilterFunction="Contains" UniqueName="DS_NOTAS_CLASIFICACION" Display="true" ReadOnly="false"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicking="ShowPopupmodalClasificacionCompetenciaNuevo" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupmodalClasificacionCompetenciaEditar" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="modalClasCompetenciaEditar" runat="server" Title="Tipo de competencia" Height="600"
                Width="610" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindow">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
