<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogoTipoCompetencia.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoTipoCompetencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            var idTipo = "";
            function obtenerIdFila() {
                var grid = $find("<%=grvTipoCompetencia.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idTipo = SelectDataItem.getDataKeyValue("CL_TIPO_COMPETENCIA");
                }
            }

            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

  
            function onCloseWindow(oWnd, args) {
                idTipo = "";
                        $find("<%=grvTipoCompetencia.ClientID%>").get_masterTableView().rebind();
                  
            }

            function ShowPopupmodalTipoCompetenciaEditar() {
                obtenerIdFila()

                if (idTipo != "") {
                    var oWnd = radopen("VentanaCatalogoTipoCompetencia.aspx?&ID=" + idTipo + "&TIPO=Editar", "modalTipoCompetenciaEditar");
                    oWnd.set_title("Editar categoría de competencia");
                } else {
                    radalert("Selecciona un registro", 400, 150, "");
                }
            }

            function ShowPopupmodalTipoCompetenciaNuevo() {
                var oWnd = radopen("VentanaCatalogoTipoCompetencia.aspx?&ID=0&TIPO=Editar", "modalTipoCompetenciaEditar");
                oWnd.set_title("Tipos de competencias laborales");
            }

            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grvTipoCompetencia.ClientID %>").get_masterTableView();
                  var selectedRows = MasterTable.get_selectedItems();

                  var row = selectedRows[0];

                  if (row != null) {
                      CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_TIPO_COMPETENCIA");
                      if (selectedRows != "") {
                          var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                          { if (shouldSubmit) { this.click(); } });

                          radconfirm('¿Deseas eliminar el tipo de competencia ' + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                          args.set_cancel(true);
                      }
                  } else {
                      radalert("Seleccione un regristro", 400, 150, "");
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

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>  
            <telerik:RadWindow ID="modalTipoCompetenciaEditar" runat="server" Title="Tipo de competencia" Height="300"
                Width="610" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindow" >
            </telerik:RadWindow>
        </Windows>  
    </telerik:RadWindowManager>

      <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
     <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
         <ClientEvents OnRequestStart="onRequestStart"/>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvTipoCompetencia" UpdatePanelHeight="100%"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grvTipoCompetencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvTipoCompetencia" UpdatePanelHeight="100%"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvTipoCompetencia" UpdatePanelHeight="100%"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

                  <telerik:AjaxSetting AjaxControlID="grvTipoCompetencia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvTipoCompetencia" UpdatePanelHeight="100%"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <label class="labelTitulo">Tipos de competencias laborales</label>
    <div style="height:calc(100% - 100px)">
      <telerik:RadGrid ID="grvTipoCompetencia"
                       ShowHeader="true"
                       runat="server"
                       AllowPaging="true" 
                       AllowSorting="true"
                       GroupPanelPosition="Top"
                       GridLines="None"  
                       ClientSettings-EnablePostBackOnRowClick ="false"
                       AllowFilteringByColumn="true"  
                       HeaderStyle-Font-Bold="true"
                       OnNeedDataSource="grvTipoCompetencia_NeedDataSource"
                       OnItemDataBound="grvTipoCompetencia_ItemDataBound"
                       Height="100%" 
                       Width="1000">
          <ExportSettings ExportOnlyData="true" FileName="Cátalogo de tipo de competencias" IgnorePaging="true">
           <Excel Format="Xlsx"  />   
              </ExportSettings>
      <GroupingSettings CaseSensitive="False" /> 
           <ClientSettings AllowKeyboardNavigation="true" >  
               <Selecting AllowRowSelect="true" />  
               <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>  
           </ClientSettings>  
           <PagerStyle AlwaysVisible="true" />  
      <MasterTableView  ClientDataKeyNames="CL_TIPO_COMPETENCIA" DataKeyNames="CL_TIPO_COMPETENCIA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"    
          CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms" >  
           <CommandItemSettings  ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToWordButton="false"  ShowExportToPdfButton="false" ShowExportToCsvButton="false"    
               RefreshText="Actualizar"  AddNewRecordText="Insertar" />   
             <Columns>  
                 <telerik:GridBoundColumn  HeaderText="Clave" DataField="CL_TIPO_COMPETENCIA" AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="145" CurrentFilterFunction="Contains" UniqueName="CL_TIPO_COMPETENCIA"    Display="true" ReadOnly="false" ></telerik:GridBoundColumn>  
                 <telerik:GridBoundColumn  HeaderText="Nombre" DataField="NB_TIPO_COMPETENCIA" AutoPostBackOnFilter="true" HeaderStyle-Width="350" FilterControlWidth="280" CurrentFilterFunction="Contains"   UniqueName="NB_TIPO_COMPETENCIA"    Display="true" ReadOnly="false" ></telerik:GridBoundColumn>  
                 <telerik:GridBoundColumn  HeaderText="Activo" DataField="NB_ACTIVO" AutoPostBackOnFilter="true" FilterControlWidth="25" HeaderStyle-Width="95 " CurrentFilterFunction="Contains"  UniqueName="NB_ACTIVO"    Display="true" ReadOnly="false"  ></telerik:GridBoundColumn>  
                 <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
             </Columns>    
       </MasterTableView> 
   </telerik:RadGrid> 
            </div>
    <div style="clear: both; height: 10px;"></div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowPopupmodalTipoCompetenciaNuevo" AutoPostBack="false" runat="server" Text="Agregar"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupmodalTipoCompetenciaEditar" AutoPostBack="false" runat="server" Text="Editar"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
        </div>

        <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
