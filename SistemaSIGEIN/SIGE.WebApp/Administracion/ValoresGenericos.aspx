<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="ValoresGenericos.aspx.cs" Inherits="SIGE.WebApp.Administracion.ValoresGenericos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
   <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
       <script type="text/javascript">

           var idCatalogo = "";

           function obtenerIdFila() {
               var grid = $find("<%=grvValoresGenericos.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idCatalogo = SelectDataItem.getDataKeyValue("ID_CATALOGO_VALOR");
                }
            }

            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function Cerrarventana() {
                $(".rwCloseButton span").click();
            }

            function onCloseWindow(oWnd, args) {
                        $find("<%=grvValoresGenericos.ClientID%>").get_masterTableView().rebind();
               
            }

            function ShowPopupmodalCatalogoGenericoEditar() {
                obtenerIdFila()
                var cmb = $find("<%=cmbIdCatalogo.ClientID %>");
                var value = cmb.get_selectedItem().get_value()

                if (idCatalogo != "") {
                    var oWnd = radopen("VentanaValoresGenericos.aspx?&ID=" + idCatalogo + "&ID_LISTA=" + value + "&TIPO=Editar", "rwValorGenerico");
                    oWnd.set_title("Editar valor génerico");
                } else {
                    radalert("Selecciona una fila para editar", 350, 148, "Aviso");
                }
            }

            function ShowPopupmodalCatalogoGenericoNuevo() {
                var cmb = $find("<%=cmbIdCatalogo.ClientID %>");
                var value = cmb.get_selectedItem().get_value()
                if (value != "") {
                    var oWnd = radopen("VentanaValoresGenericos.aspx?&ID=0&ID_LISTA=" + value + "&TIPO=Editar", "rwValorGenerico");
                    oWnd.set_title("Agregar valor génerico");
                }
            }

            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grvValoresGenericos.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                var row = selectedRows[0];

                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_CATALOGO_VALOR");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar el valor génerico ' + CELL_NOMBRE.innerHTML + '?', callBackFunction, 400, 170, null, "Aviso");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione un valor génerico.", 400, 150, "Aviso");
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
        <ClientEvents OnRequestStart="onRequestStart"/>
        <AjaxSettings>
         
            <telerik:AjaxSetting AjaxControlID="grvValoresGenericos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grvValoresGenericos" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvValoresGenericos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvValoresGenericos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvValoresGenericos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

              <telerik:AjaxSetting AjaxControlID="cmbIdCatalogo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grvValoresGenericos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
           
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
           <Windows>  
               <telerik:RadWindow ID="rwValorGenerico" runat="server" Title="Catálogo Genérico" Height="400"
                   Width="600" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                   Modal="true" OnClientClose="onCloseWindow" >
               </telerik:RadWindow>
           </Windows>  
       </telerik:RadWindowManager>
      

      <label class="labelTitulo">Valores Génericos</label>
        <div class="ctrlBasico">
            <telerik:RadComboBox 
                runat="server" 
                AutoPostBack="true" 
                EmptyMessage="- -" 
                OnSelectedIndexChanged="cmbIdCatalogo_SelectedIndexChanged" 
                ID="cmbIdCatalogo"></telerik:RadComboBox>
        </div>
         <div style="clear: both;"></div>
     
    <div style="height:calc(100% - 145px);">
      <telerik:RadGrid ID="grvValoresGenericos" ShowHeader="true"  runat="server" AllowPaging="true" 
          AllowSorting="true"   GroupPanelPosition="Top" GridLines="None"  Width="100%"
          AllowAutomaticUpdates="true"  ClientSettings-EnablePostBackOnRowClick ="false" Height="100%"  AllowFilteringByColumn="true"  
          OnNeedDataSource="GridValoresGenericos_NeedDataSource" 
          HeaderStyle-Font-Bold="true" 
          OnItemDataBound="GridValoresGenericos_ItemDataBound"  >
      <GroupingSettings CaseSensitive="False" /> 
      <ExportSettings FileName ="Valores génericos" ExportOnlyData="true" IgnorePaging="true"   >  
      <Excel Format="Xlsx"  />   
      </ExportSettings>  
           <ClientSettings AllowKeyboardNavigation="true" >  
               <Selecting AllowRowSelect="true" />  
               <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>  
           </ClientSettings>  
           <PagerStyle AlwaysVisible="true" />  
      <MasterTableView  ClientDataKeyNames="ID_CATALOGO_VALOR" DataKeyNames="ID_CATALOGO_VALOR" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"    
          CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms" >  
           <CommandItemSettings  ShowAddNewRecordButton="false" ShowExportToExcelButton="True"  ShowExportToCsvButton="false"    
               RefreshText="Actualizar"  AddNewRecordText="Insertar" />   
             <Columns>  
                 <telerik:GridBoundColumn AutoPostBackOnFilter="true"  CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave" DataField="CL_CATALOGO_VALOR"   UniqueName="CL_CATALOGO_VALOR"  Display="true" ReadOnly="false"  ></telerik:GridBoundColumn>  
                 <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Nombre" DataField="NB_CATALOGO_VALOR"   UniqueName="NB_CATALOGO_VALOR"  Display="true" ReadOnly="false" ></telerik:GridBoundColumn>  
                 <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="300" FilterControlWidth="220" HeaderText="Descripción" DataField="DS_CATALOGO_VALOR"   UniqueName="DS_CATALOGO_VALOR"  Display="true" ReadOnly="false"  ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>  
             </Columns>    
       </MasterTableView> 
   </telerik:RadGrid> 
   </div>
   <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowPopupmodalCatalogoGenericoNuevo" AutoPostBack="false" runat="server" Text="Agregar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupmodalCatalogoGenericoEditar" AutoPostBack="false" runat="server" Text="Editar"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" OnClientClicking="ConfirmarEliminar" OnClick="btnEliminar_Click"></telerik:RadButton>
    </div>

       <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
