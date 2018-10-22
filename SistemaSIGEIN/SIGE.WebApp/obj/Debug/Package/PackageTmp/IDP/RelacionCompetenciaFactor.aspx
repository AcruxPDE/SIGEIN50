<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/MenuIDP.master" AutoEventWireup="true" CodeBehind="RelacionCompetenciaFactor.aspx.cs" Inherits="SIGE.WebApp.IDP.RelacionCompetenciaFactor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">

           function OpenSelectionCompetencias() {
               var currentWnd = GetRadWindow();
               var browserWnd = window;
               if (currentWnd)
                   browserWnd = currentWnd.BrowserWindow;

               var windowProperties = {
                   width: browserWnd.innerWidth - 40,
                   height: browserWnd.innerHeight - 20
               };
               openChildDialog("../Comunes/SeleccionCompetencia.aspx?vClTipoSeleccion=GEN", "winSeleccion", "Selección de competencias", windowProperties)
           }

           function OnCloseWindowC(oWnd, args) {
               $find("<%=rgdCompetencias.ClientID%>").get_masterTableView().rebind();
            }

           function closeWindow() {
               GetRadWindow().close();
           }

           function useDataFromChild(pDato) {
               var arr = [];
               if (pDato != null) {
                   var vDatosSeleccionados = pDato[0];
                   for (var i = 0; i < pDato.length; ++i) {
                       arr.push(pDato[i].idCompetencia);
                   }
                   var datos = JSON.stringify({ clTipo: vDatosSeleccionados.clTipoCatalogo, arrCompetencia: arr });
                   var ajaxManager = $find('<%= ramRelacion.ClientID%>');
                   ajaxManager.ajaxRequest(datos);
               }
           }

           function ConfirmarEliminar(sender, args) {
               var masterTable = $find("<%= rgdCompetencias.ClientID %>").get_masterTableView();
               var selectedItems = masterTable.get_selectedItems();
               if (selectedItems.length > 0) {
                   var vMensaje = "";    
                   vMensaje = "¿Deseas eliminar las competencias seleccionadas?, este proceso no podrá revertirse.";             
                   var vWindowsProperties = { height: 180 };
                   confirmAction(sender, args, vMensaje, vWindowsProperties);
               }
               else {
                   radalert("Selecciona una competencia.", 400, 150);
                   args.set_cancel(true);
               }
           }

           </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramRelacion" runat="server" OnAjaxRequest="ramRelacion_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgdPruebas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdFactores" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgdPruebas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="btnAgregar" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="btnEliminar" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
         <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgdFactores">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdCompetencias" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgdFactores" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                   <telerik:AjaxUpdatedControl ControlID="btnAgregar" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="btnEliminar" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
          <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgdCompetencias">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdFactores" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgdCompetencias" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
           <label>Por favor seleccione la prueba a modificar: </label>
    <div style="height: calc(100% - 70px); width: 100%">
 
    <div style="float: left; width: 25%; height: 100%">
        <telerik:RadGrid ID="rgdPruebas"
            runat="server"
            Height="100%"
            AllowSorting="true"
            AllowFilteringByColumn="true"
            ShowHeader="true"
            HeaderStyle-Font-Bold="true"
            OnNeedDataSource="rgdPruebas_NeedDataSource"
            AllowMultiRowSelection="true"
            OnSelectedIndexChanged="rgdPruebas_SelectedIndexChanged">
            <ClientSettings EnablePostBackOnRowClick="true">
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ID_PRUEBA" ClientDataKeyNames="ID_PRUEBA" AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" >
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                    AddNewRecordText="Insertar" />
                <Columns>
                    <%--<telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>--%>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" FilterControlWidth="70%" HeaderText="Prueba" DataField="NB_PRUEBA" UniqueName="NB_PRUEBA"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="float: left; width: 20px; height: 100%"></div>
    <div style="float: left; width: 25%; height: 100%">
        <telerik:RadGrid ID="rgdFactores"
            runat="server"
            Height="100%"
            Width="100%"
            HeaderStyle-Font-Bold="true"
            AllowSorting="true"
            AllowFilteringByColumn="true"
            ShowHeader="true"
            OnNeedDataSource="rgdFactores_NeedDataSource"
            AllowMultiRowSelection="true"
            OnSelectedIndexChanged="rgdFactores_SelectedIndexChanged">
            <ClientSettings EnablePostBackOnRowClick="true">
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ID_SELECCION" ClientDataKeyNames="ID_SELECCION" AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="true" NoMasterRecordsText="No se ha seleccionado alguna prueba." ShowHeadersWhenNoRecords="true">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                    AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" FilterControlWidth="70%" HeaderText="Factor de prueba" DataField="NB_SELECCION" UniqueName="NB_SELECCION"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="float: left; width: 20px; height: 100%"></div>
    <div style="float: left; width: 45%; height: 100%">
        <telerik:RadGrid ID="rgdCompetencias"
            runat="server"
            Height="100%"
            HeaderStyle-Font-Bold="true"
            Width="100%"
            AllowSorting="true"
            AllowFilteringByColumn="true"
            ShowHeader="true"
            OnNeedDataSource="rgdCompetencias_NeedDataSource"
            AllowMultiRowSelection="true"
            >
            <ClientSettings >
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" CellSelectionMode="MultiCell" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ID_SELECCION" ClientDataKeyNames="ID_SELECCION" AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="true" NoMasterRecordsText="No se ha seleccionado algún factor." ShowHeadersWhenNoRecords="true">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                    AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="80" FilterControlWidth="60%" HeaderText="Clave" DataField="CL_SELECCION" UniqueName="CL_SELECCION"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="100" FilterControlWidth="60%" HeaderText="Competencia" DataField="NB_SELECCION" UniqueName="NB_SELECCION"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="200" FilterControlWidth="60%" HeaderText="Descripción" DataField="DS_SELECCION" UniqueName="DS_SELECCION"></telerik:GridBoundColumn>
                 <%--   <telerik:GridButtonColumn UniqueName="ELIMINAR" Text="Eliminar" CommandName="Delete" HeaderStyle-Width="30" ButtonType="ImageButton" ConfirmText="Este proceso eliminara la relación de la competencia del factor ¿Desea continuar?"></telerik:GridButtonColumn>--%>

                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <div style="clear:both;height:10px"></div>
          <div class="ctrlBasico">
                    <telerik:RadButton runat="server" Enabled="false" ID="btnAgregar" Text="Agregar" AutoPostBack="false" OnClientClicked="OpenSelectionCompetencias"></telerik:RadButton>
         </div>
         <div class="ctrlBasico">
         <telerik:RadButton runat="server" Enabled="false" ID="btnEliminar" Text="Eliminar" AutoPostBack="true" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
             </div>
    </div>
    </div>
     <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="winSeleccion"
                runat="server"
                VisibleStatusbar="false"
                VisibleTitlebar="true"
                ShowContentDuringLoad="false"
                Modal="true"
                ReloadOnShow="true"
                Behaviors="Close">
            </telerik:RadWindow>
            </Windows>
            </telerik:RadWindowManager>
</asp:Content>
