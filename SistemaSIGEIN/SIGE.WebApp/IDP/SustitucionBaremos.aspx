<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/MenuIDP.master" AutoEventWireup="true" CodeBehind="SustitucionBaremos.aspx.cs" Inherits="SIGE.WebApp.IDP.SustitucionBaremos1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            var idPrueba = "";
            var vclTokenExterno = "";
            var vFlBateria = "";

            function obtenerIdFila() {
              
                var grid = $find("<%=dgvBateria.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                if (selectedRows.length != 0) {
                    var row = selectedRows[0];
                    vFlBateria = row.getDataKeyValue("ID_BATERIA");
                    vclTokenExterno = row.getDataKeyValue("CL_TOKEN");
                }
            }

            function onCloseWindow(oWnd, args) {
                var idPrueba = "";
                var vclTokenExterno = "";
                $find("<%=dgvBateria.ClientID%>").get_masterTableView().rebind();
            }
         
            function sustitucionBaremos() {

                obtenerIdFila();

                if ((vFlBateria != "")) {
                    var win = radopen("VentanaSustitucionBaremos.aspx?pIdBateria=" + vFlBateria, 'rwBaremos');
                }
                else { radalert("No has seleccionado una bateria.", 400, 150, "Aviso"); }
            }
       
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvBateria">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvBateria" UpdatePanelHeight="100%"  LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    

    <label class="labelTitulo">Sustitución de Baremos</label>
     
   <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splSolicitudes" runat="server" Width="100%" Height="100%" BorderSize="0" Orientation="Vertical">
            <telerik:RadPane ID="rpnGridPruebas" runat="server">

                    <telerik:RadGrid 
                        ID="dgvBateria" 
                        runat="server" 
                        AutoGenerateColumns="false" 
                        EnableHeaderContextMenu="true"                         
                        Height="100%" 
                        ShowGroupPanel="true" 
                        AllowSorting="true" 
                        OnNeedDataSource="dgvBateria_NeedDataSource"
                        OnItemCommand="dgvBateria_ItemCommand"
                        AllowMultiRowSelection="true" 
                        OnItemDataBound="dgvBateria_ItemDataBound">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />                            
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView ClientDataKeyNames="ID_BATERIA,CL_TOKEN" Name="ID_BATERIA,CL_TOKEN" EnableColumnsViewState="false" DataKeyNames="ID_BATERIA,CL_TOKEN" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" PageSize="10">

                               <Columns>
                                        <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="35"></telerik:GridClientSelectColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Folio" DataField="FL_BATERIA" UniqueName="FL_BATERIA"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" Display="false" DataField="CL_TOKEN" UniqueName="CL_TOKEN"></telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Nombre Candidato" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO"></telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Correo" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>                         
                                         <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Estatus" DataField="ESTATUS" UniqueName="ESTATUS"></telerik:GridBoundColumn>                         
                                         <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Fecha de aplic." DataField="FE_TERMINO" UniqueName="FE_TERMINO" DataFormatString="{0:dd/MM/yyyy HH:mm}" ></telerik:GridBoundColumn>                         
                                         <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="Enviado" DataField="FG_ENVIO_CORREO" UniqueName="FG_ENVIO_CORREO"></telerik:GridBoundColumn>                         
                                     </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
        </telerik:RadPane>
        </telerik:RadSplitter>
           </div>
     <div style="clear:both; height:10px;"></div>       
        <div class="ctrlBasico">
            <telerik:RadButton runat="server" Text="Ver baremos" ID="btnBaremos" OnClientClicked="sustitucionBaremos" AutoPostBack="false"></telerik:RadButton>
        </div>
    <!-- Fin Secciones de niveles -->

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>            
            <telerik:RadWindow ID="rwBaremos"
                runat="server"
                Title="Sustitucion de Baremos"
                Height="700"
                Width="1000"
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
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Height="100%"></telerik:RadWindowManager>
</asp:Content>
