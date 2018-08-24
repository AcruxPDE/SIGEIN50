<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogoCatalogos.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoCatalogos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
   
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            var idCatalogo = "";

            function obtenerIdFila() {
                var grid = $find("<%=grvCatalogoLista.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idCatalogo = SelectDataItem.getDataKeyValue("ID_CATALOGO_LISTA");
                }
            }

            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

          

            function onCloseWindow(oWnd, args) {
                idCatalogo = "";
                        $find("<%=grvCatalogoLista.ClientID%>").get_masterTableView().rebind();
                
            }

            function ShowPopupmodalCatalogoGenericoEditar() {
                obtenerIdFila()

                if (idCatalogo != "") {
                    var oWnd = radopen("VentanaCatalogoCatalogos.aspx?&ID=" + idCatalogo + "&TIPO=Editar", "RWPopupmodalCatalogoGenericoEditar");
                    oWnd.set_title("Editar catálogo genérico");
                } else {
                    radalert("Selecciona un registro.", 400,150, "");
                }
            }

            function ShowPopupmodalCatalogoGenericoNuevo() {
                var oWnd = radopen("VentanaCatalogoCatalogos.aspx?&ID=0&TIPO=Editar", "RWPopupmodalCatalogoGenericoEditar");
                oWnd.set_title("Agregar catálogo genérico");
            }

            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grvCatalogoLista.ClientID %>").get_masterTableView();
                    var selectedRows = MasterTable.get_selectedItems();

                    var row = selectedRows[0];

                    if (row != null) {
                        CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_CATALOGO_LISTA");
                        if (selectedRows != "") {
                            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                            { if (shouldSubmit) { this.click(); } });

                            radconfirm('¿Deseas eliminar ' + CELL_NOMBRE.innerHTML + '?, este proceso no podrá revertirse.', callBackFunction, 400, 150, null, "Eliminar Registro");
                            args.set_cancel(true);
                        }
                    } else {
                        radalert("Seleccione un cátalogo general.", 400, 150, "");
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


      <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
   <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart"/>
        <AjaxSettings>
         
            <telerik:AjaxSetting AjaxControlID="grvCatalogoLista">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grvCatalogoLista" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCatalogoLista" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCatalogoLista" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvCatalogoLista" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
           <Windows>  
               <telerik:RadWindow ID="RWPopupmodalCatalogoGenericoEditar" runat="server" Title="Catálogo Genérico" Height="270"
                   Width="600" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                   Modal="true" OnClientClose="onCloseWindow" >
               </telerik:RadWindow>
           </Windows>  
       </telerik:RadWindowManager>


     <label class="labelTitulo">Lista de catálogos</label>
    <div style="height:calc(100% - 100px);">
      <telerik:RadGrid ID="grvCatalogoLista" ShowHeader="true"  runat="server" AllowPaging="true" 
          AllowSorting="true"   GroupPanelPosition="Top"  GridLines="None"   Height="100%" Width="100%"
           ClientSettings-EnablePostBackOnRowClick ="false" AllowFilteringByColumn="true"  
          OnNeedDataSource="GridCatalogoLista_NeedDataSource" HeaderStyle-Font-Bold="true" OnItemDataBound="grvCatalogoLista_ItemDataBound">
          <ExportSettings ExportOnlyData="true" FileName="CatGenéricos" IgnorePaging="true">
           <Excel Format="Xlsx"  />   
              </ExportSettings>
      <GroupingSettings CaseSensitive="False" /> 
           <ClientSettings AllowKeyboardNavigation="true" >  
               <Selecting AllowRowSelect="true" />  
               <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>  
           </ClientSettings>  
           <PagerStyle AlwaysVisible="true" />  
      <MasterTableView  ClientDataKeyNames="ID_CATALOGO_LISTA" DataKeyNames="ID_CATALOGO_LISTA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"    
          CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms" >  
           <CommandItemSettings  ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToWordButton="false"  ShowExportToPdfButton="false" ShowExportToCsvButton="false"    
               RefreshText="Actualizar"  AddNewRecordText="Insertar" />   
             <Columns>  
                 <telerik:GridBoundColumn  HeaderText="Id" DataField="ID_CATALOGO_LISTA" UniqueName="ID_CATALOGO_LISTA" Display="false" ReadOnly="false" ></telerik:GridBoundColumn>  
                 <telerik:GridBoundColumn  HeaderText="Nombre de Catálogo" AutoPostBackOnFilter="true" HeaderStyle-Width="300" FilterControlWidth="220" CurrentFilterFunction="Contains" DataField="NB_CATALOGO_LISTA"   UniqueName="NB_CATALOGO_LISTA"    Display="true" ReadOnly="false" ></telerik:GridBoundColumn>  
                 <telerik:GridBoundColumn  HeaderText="Descripción" AutoPostBackOnFilter="true" HeaderStyle-Width="400" FilterControlWidth="320" CurrentFilterFunction="Contains" DataField="DS_CATALOGO_LISTA"   UniqueName="DS_CATALOGO_LISTA"    Display="true" ReadOnly="false" ></telerik:GridBoundColumn>  
                 <telerik:GridBoundColumn  HeaderText="Tipo de Catálogo" AutoPostBackOnFilter="true" HeaderStyle-Width="250" FilterControlWidth="180" CurrentFilterFunction="Contains" DataField="NB_CATALOGO_TIPO"   UniqueName="NB_CATALOGO_TIPO"    Display="true" ReadOnly="false"  ></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>  
             </Columns>    
       </MasterTableView> 
   </telerik:RadGrid> 
         </div> 

    <div style="clear: both; height: 10px;"></div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowPopupmodalCatalogoGenericoNuevo" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupmodalCatalogoGenericoEditar" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnElemento" runat="server" Text="Ver elementos" Width="200" OnClick="btnElemento_Click"></telerik:RadButton>
        </div>
      
        

       <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
