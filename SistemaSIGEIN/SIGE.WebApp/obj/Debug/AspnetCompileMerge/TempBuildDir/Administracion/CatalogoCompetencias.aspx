<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogoCompetencias.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoCompetencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            var idCompetencia = "";

            function obtenerIdFila() {
                var grid = $find("<%=grvCompetencias.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idCompetencia = SelectDataItem.getDataKeyValue("ID_COMPETENCIA");
                }
            }


            function onCloseWindow(oWnd, args) {
                idCompetencia = "";
                $find("<%=grvCompetencias.ClientID%>").get_masterTableView().rebind();

            }

            function ShowPopupmodalCatalogoGenericoEditar() {
                obtenerIdFila()

                if (idCompetencia != "") {
                    var oWnd = radopen("VentanaRadDock.aspx?&ID=" + idCompetencia + "&TIPO=Editar", "rwCompetenciaNuevo");
                    oWnd.set_title("Editar competencia");
                } else {
                    radalert("Selecciona un registro.", 400, 150, " ");
                }
            }


            function ShowPopupmodalClasificacionCompetenciaNuevo(sender, args) {

                var oWnd = radopen("VentanaRadDock.aspx?&ID=0&TIPO=Agregar", "rwCompetenciaNuevo");
                oWnd.set_title("Agregar competencia");
            }


            function ShowPopupmodalCatalogoGenericoNuevo() {
                var oWnd = radopen("VentanaRadDock.aspx?&ID=0&TIPO=Editar", "RWPopupmodalCompetencias");
            }

            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grvCompetencias.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                var row = selectedRows[0];

                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_COMPETENCIA");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar la competencia ' + CELL_NOMBRE.innerHTML + '?', callBackFunction, 400, 170, null, "Eliminar Registro");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione una competencia.", 400, 150, "Error");
                    args.set_cancel(true);
                }
            }

            function ConfirmarCopy(sender, args) {

                obtenerIdFila();

                if (idCompetencia != "") {

                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                        if (shouldSubmit) {
                            var oWnd = radopen("VentanaRadDock.aspx?&ID=" + idCompetencia + "&TIPO=Copy", "rwCompetenciaNuevo");
                            // window.open("CatalogoCompetenciaSeleccionado.aspx");
                        }

                    });
                    radconfirm("¿Deseas copiar los niveles de alguna otra Competencia laboral que ya se encuentre en el catálogo?", callBackFunction, 400, 200, null, "Agregar Competencias laborales");

                    args.set_cancel(true);
                }
                else {
                    args.set_cancel(true);
                    radalert("Selecciona un registro.", 400, 150, " ");
                }
            }

            function ConfirmarDock(sender, args) {
                obtenerIdFila();
                var oWnd = radopen("VentanaDock.aspx?&ID=" + idCompetencia + "&TIPO=Copy", "rwCompetenciaNuevo");
                oWnd.set_title("Agregar competencia");
            }
          
            function ConfirmarCopiar(sender, args) {
                obtenerIdFila();
                if (idCompetencia != "") {
                    var oWnd = radopen("VentanaRadDock.aspx?&ID=" + idCompetencia + "&TIPO=Copy", "rwCompetenciaNuevo");
                    oWnd.set_title("Agregar competencia");
                }
                else {
                    radalert("Selecciona un registro.", 400, 150, " ");
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
                    <telerik:AjaxUpdatedControl ControlID="grvCompetencias" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCompetencias" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCompetencias" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCopiarde">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCompetencias" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grvCompetencias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCompetencias" UpdatePanelHeight="100%"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="rwCompetenciaNuevo" runat="server" Title="Tipo de competencia" Height="600"
                Width="800" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindow" >
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <label class="labelTitulo">Competencias laborales</label>

    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid 
            ID="grvCompetencias" 
            ShowHeader="true" 
            runat="server" 
            AllowPaging="true"
            AllowSorting="true" 
            GroupPanelPosition="Top" 
            GridLines="None" 
            Height="100%"
            Width="100%"
            HeaderStyle-Font-Bold="true"
            ClientSettings-EnablePostBackOnRowClick="false" 
            AllowFilteringByColumn="true"
            OnItemDataBound="grvCompetencias_ItemDataBound"
            OnNeedDataSource="grvCompetencias_NeedDataSource">
            <ExportSettings ExportOnlyData="true" FileName="Cátalogo de competencias" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_COMPETENCIA" DataKeyNames="ID_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" 
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToWordButton="false" ShowExportToPdfButton="false" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Clave" AutoPostBackOnFilter="true" HeaderStyle-Width="100" FilterControlWidth="30" CurrentFilterFunction="Contains" DataField="CL_COMPETENCIA" UniqueName="CL_COMPETENCIA" Display="true" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Nombre" AutoPostBackOnFilter="true" HeaderStyle-Width="250" FilterControlWidth="180" CurrentFilterFunction="Contains" DataField="NB_COMPETENCIA" UniqueName="NB_COMPETENCIA" Display="true" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Descripción" AutoPostBackOnFilter="true" HeaderStyle-Width="350" FilterControlWidth="280" CurrentFilterFunction="Contains" DataField="DS_COMPETENCIA" UniqueName="DS_COMPETENCIA" Display="true" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Tipo de Competencia" AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="130" CurrentFilterFunction="Contains" DataField="NB_TIPO_COMPETENCIA" UniqueName="NB_TIPO_COMPETENCIA" Display="true" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Clasificación" AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="130" CurrentFilterFunction="Contains" DataField="CL_CLASIFICACION" UniqueName="CL_CLASIFICACION" Display="true" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Activo" AutoPostBackOnFilter="true" FilterControlWidth="30" HeaderStyle-Width="100 " CurrentFilterFunction="Contains" DataField="NB_ACTIVO" UniqueName="NB_ACTIVO" Display="false" ReadOnly="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>

  <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicking="ShowPopupmodalClasificacionCompetenciaNuevo" AutoPostBack="false" runat="server" Text="Agregar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupmodalCatalogoGenericoEditar" AutoPostBack="false" runat="server" Text="Editar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
    </div>
  
    <div class="ctrlBasico">
        <telerik:RadButton ID="buttonDock" runat="server" Text="Copiar" OnClientClicking="ConfirmarCopiar" AutoPostBack="false"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
