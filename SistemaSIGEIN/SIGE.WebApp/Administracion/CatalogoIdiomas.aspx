<%@ Page Title=""  Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogoIdiomas.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoIdiomas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">

         <ClientEvents OnRequestStart="onRequestStart"/>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="GridIdiomas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GridIdiomas" UpdatePanelHeight="100%"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GridIdiomas"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GridIdiomas" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>

                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GridIdiomas"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">



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


            var idIdioma = "";

            function obtenerIdFila() {
                var grid = $find("<%=GridIdiomas.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
              
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                    idIdioma = SelectDataItem.getDataKeyValue("ID_IDIOMA");
                   
                }
            }

            function onCloseWindow(oWnd, args) {
                idIdioma = "";
                $find("<%=GridIdiomas.ClientID%>").get_masterTableView().rebind();
            }

            function ShowPopupEditarIdiomas() {
                obtenerIdFila();
                
                if (idIdioma != "") {

                    var oWnd = radopen("VentanaCatalogoIdiomas.aspx?&ID=" + idIdioma + "&TIPO=Editar", "RWPopupmodalCatalogoGenericoEditar");
                    oWnd.set_title("Editar Idioma");
                }
                else {
                    radalert("No has seleccionado un registro.", 400, 150, "");
                }
            }



            function ShowPopupAgregarIdiomas() {
                var oWnd = radopen("VentanaCatalogoIdiomas.aspx", "RWPopupmodalCatalogoGenericoEditar");
                oWnd.set_title("Agregar Idioma");
            }


            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=GridIdiomas.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                var row = selectedRows[0];

                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_IDIOMA");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar el idioma ' + CELL_NOMBRE.innerHTML + ' ?, este proceso no podrá revertirse.', callBackFunction, 400, 150, null, "Eliminar Registro");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione una idioma.", 400, 150, "");
                    args.set_cancel(true);
                }
            }




        </script>
    </telerik:RadCodeBlock>
    <label class="labelTitulo">Idiomas</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid 
            ID="GridIdiomas" 
            ShowHeader="true"
            runat="server" 
            AllowPaging="true"
            AllowSorting="true" 
            GroupPanelPosition="Top" 
            Width="950px" 
            GridLines="None"
            Height="100%"
            HeaderStyle-Font-Bold="true"
            AllowFilteringByColumn="true"
            ClientSettings-EnablePostBackOnRowClick="false"
            OnNeedDataSource="GridIdiomas_NeedDataSource"
            OnItemDataBound="GridIdiomas_ItemDataBound"
           >
            <GroupingSettings CaseSensitive="False" />


            <ExportSettings FileName="CatalogoIdiomas" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>

            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" ></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_IDIOMA" DataKeyNames="ID_IDIOMA" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                 <Columns>
                  <telerik:GridBoundColumn  AutoPostBackOnFilter="true"   CurrentFilterFunction="Contains" HeaderText="ID_IDIOMA" DataField="ID_IDIOMA"   UniqueName="ID_IDIOMA"   HeaderStyle-Width="0"  ItemStyle-Width="0"  Display="false" ReadOnly="false"   MaxLength="10"  ></telerik:GridBoundColumn>  
                  <telerik:GridBoundColumn  AutoPostBackOnFilter="true"   CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_IDIOMA"   UniqueName="CL_IDIOMA"   HeaderStyle-Width="250px"  ItemStyle-Width="180px"  Display="true" ReadOnly="false"   MaxLength="10"  ></telerik:GridBoundColumn>                                    
                  <telerik:GridBoundColumn  AutoPostBackOnFilter="true"   CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_IDIOMA"   UniqueName="NB_IDIOMA"   HeaderStyle-Width="250px"  ItemStyle-Width="180px"  Display="true" ReadOnly="false"   MaxLength="10"  ></telerik:GridBoundColumn>                  
                 <telerik:GridBoundColumn AutoPostBackOnFilter="true"   CurrentFilterFunction="Contains" HeaderText="Activo" DataField="CL_ACTIVO"   UniqueName="CL_ACTIVO" HeaderStyle-Width="100" FilterControlWidth="30"></telerik:GridBoundColumn>
                 <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
              </Columns>

            </MasterTableView>
        </telerik:RadGrid>
    </div>



    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowPopupAgregarIdiomas" AutoPostBack="false" runat="server" Text="Agregar" ></telerik:RadButton>
         </div>
    <div class="ctrlBasico">
     <telerik:RadButton ID="btnEditar" OnClientClicked="ShowPopupEditarIdiomas" AutoPostBack="false" runat="server" Text="Editar" ></telerik:RadButton>
        </div>
     <div class="ctrlBasico">
         <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" OnClientClicking="ConfirmarEliminar"></telerik:RadButton>
    </div>
   <div style="clear:both;"></div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow 
                ID="RWPopupmodalCatalogoGenericoEditar" runat="server" Title="Catálogo de Idiomas" Height="260"
                Width="595" 
                Left="5%" 
                ReloadOnShow="true" 
                ShowContentDuringLoad="false" 
                VisibleStatusbar="false" 
                VisibleTitlebar="true" 
                Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindow" >
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
