<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CentroAdministrativo.aspx.cs" Inherits="SIGE.WebApp.Administracion.CentroAdministrativo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function AbrirVentana()
            {
                var oWnd = radopen("VentanaCentroAdministrativo.aspx", "winCentroAdministrativo");
                oWnd.set_title("Nuevo Centro administrativo de trabajo");
            }

            var idCentroAdmvo = "";

            function obtenerIdFila() {
                var grid = $find("<%=grdCentroAdministrativo.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idCentroAdmvo = SelectDataItem.getDataKeyValue("ID_CENTRO_ADMVO");
                }
            }

            function EditarCentroAdmvo() {

                obtenerIdFila();
                if (idCentroAdmvo != "") {
                    var oWnd = radopen("VentanaCentroAdministrativo.aspx?&ID=" + idCentroAdmvo , "winCentroAdministrativo");
                    oWnd.set_title("Editar Centro administrativo de trabajo");
                } else {
                    radalert("No has seleccionado un registro.", 350, 148, "Aviso");
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

            function onCloseWindow(oWnd, args) {
                idCentroAdmvo = "";
                $find("<%=grdCentroAdministrativo.ClientID%>").get_masterTableView().rebind();
            }
           
       

            function confirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grdCentroAdministrativo.ClientID %>").get_masterTableView();
                  var selectedRows = MasterTable.get_selectedItems();

                  var row = selectedRows[0];

                  if (row != null) {
                      CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "CL_CENTRO_ADMVO");
                      if (selectedRows != "") {
                          var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                          { if (shouldSubmit) { this.click(); } });

                          radconfirm('¿Deseas eliminar el centro administrativo' + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Aviso");
                          args.set_cancel(true);
                      }
                  } else {
                      radalert("Selecciona un centro administrativo", 400, 150, "Aviso");
                      args.set_cancel(true);
                  }
            }


            function useDataFromChild(pDatos) {
                $find("<%= grdCentroAdministrativo.ClientID%>").get_masterTableView().rebind();
            }

            
        </script>
    </telerik:RadCodeBlock>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
         <ClientEvents OnRequestStart="onRequestStart"/>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="grdCentroAdministrativo" UpdatePanelHeight="100%"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        
            <telerik:AjaxSetting AjaxControlID="btnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCentroAdministrativo" UpdatePanelHeight="100%"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
           
             <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCentroAdministrativo" UpdatePanelHeight="100%"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

             
             <telerik:AjaxSetting AjaxControlID="grdCentroAdministrativo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCentroAdministrativo" UpdatePanelHeight="100%"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
                </AjaxSettings>
    </telerik:RadAjaxManager>

    <label class="labelTitulo">Centro Administrativo</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdCentroAdministrativo" 
            runat="server"  
            Height="100%" 
            Width="680" 
            OnNeedDataSource="grdCentroAdministrativo_NeedDataSource"
            AllowSorting ="true"  
            GroupPanelPosition="Top"
            AllowPaging="true"
            GridLines="None"
            AllowFilteringByColumn="true"
            OnItemDataBound="grdCentroAdministrativo_ItemDataBound"
            ShowHeader="true" HeaderStyle-Font-Bold="true">
            <GroupingSettings CaseSensitive="False" />
            <ExportSettings FileName="Centro administrativo" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>            
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" ></Scrolling>               
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_CENTRO_ADMVO" DataKeyNames="ID_CENTRO_ADMVO" AutoGenerateColumns="false" AllowPaging="true" 
                AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true"  CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="95" FilterControlWidth="70" HeaderText="Clave" DataField="CL_CENTRO_ADMVO" UniqueName="CL_CENTRO_ADMVO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="140" FilterControlWidth="130" HeaderText="Nombre" DataField="NB_CENTRO_ADMVO" UniqueName="NB_CENTRO_ADMVO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="190" FilterControlWidth="170" HeaderText="Direccion" DataField="DOMICILIO" UniqueName="DOMICILIO"></telerik:GridBoundColumn>    
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>

    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton 
            ID="btnNuevo" 
            runat="server" 
            name="btnNuevo" 
            AutoPostBack="false" 
            Text="Nuevo" 
            OnClientClicked="AbrirVentana"
            ></telerik:RadButton>
    </div>
    
    <div class="ctrlBasico">
        <telerik:RadButton 
            ID="btnEditar" 
            runat="server" 
            name="btnEditar" 
            AutoPostBack="false" 
            Text="Editar"  
            
             OnClientClicked="EditarCentroAdmvo"></telerik:RadButton>
    </div>
    
    <div class="ctrlBasico">
        <telerik:RadButton 
            ID="btnEliminar" 
            runat="server" 
            name="btnEliminar" 
            AutoPostBack="true" 
            Text="Eliminar" 
          
            OnClientClicking="confirmarEliminar" 
            OnClick="btnEliminar_Click">
        </telerik:RadButton>
    </div>
    
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"  OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow 
                ID="winCentroAdministrativo" 
                runat="server" 
                Title=" Nuevo/Editar Centro Administrativo" 
                Height="530px" 
                Width="660px" 
                VisibleStatusbar="false" 
                VisibleTitlebar="true" 
                ShowContentDuringLoad="false" 
                Modal="true" 
                OnClientClose="onCloseWindow"
                ReloadOnShow="true" 
                Behaviors="Close"
                ></telerik:RadWindow>
        </Windows>
    
    <Windows>
            <telerik:RadWindow 
                ID="winSeleccion" 
                runat="server" 
                Title="Seleccion" 
                Height="620px" 
                Width="480px" 
                VisibleStatusbar="false" 
                VisibleTitlebar="true" 
                ShowContentDuringLoad="false" 
                Modal="true" 
                ReloadOnShow="true" 
                Behaviors="Close"
                ></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
   
</asp:Content>
