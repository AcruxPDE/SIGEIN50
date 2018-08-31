<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaProcesoSeleccionCandidato.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaProcesoSeleccionCandidato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">

    <script type="text/javascript">

        var idProcesoSeleccion = "";
        var idRequisicion = "";
        var idCandidato = "";

        function obtenerIdFila() {
            var grid = $find("<%=grdProcesoSeleccion.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            if (selectedRows.length != 0) {
                var row = selectedRows[0];
                var SelectDataItem = MasterTable.get_dataItems()[row._itemIndex];
                idRequisicion = SelectDataItem.getDataKeyValue("ID_REQUISICION");
                idProcesoSeleccion = SelectDataItem.getDataKeyValue("ID_PROCESO_SELECCION");
            }
            else {
                idProcesoSeleccion = "";
                idRequisicion = "";
            }
        }

        function recargarGridProcesoSeleccion() {
            idProcesoSeleccion = "";
            idRequisicion = "";
            var vGrid = $find("<%=grdProcesoSeleccion.ClientID %>");
            var vMasterTable = vGrid.get_masterTableView();
            vMasterTable.rebind();
        }

        

        function OpenProcesoSeleccionVerWindow() {
            obtenerIdFila();
            var idCandidato = '<%= vIdCandidato %>';
            var idBateria = '<%= vIdBateria %>';
             var clToken = '<%= vClToken %>';

             var vURL = "ProcesoSeleccion.aspx";
             var vTitulo = "Proceso de evaluación";


             vURL = vURL + "?IdProcesoSeleccion=" + idProcesoSeleccion + "&IdCandidato=" + idCandidato + "&Tipo=REV";

             if (idBateria != 0) {
                 vURL = vURL + "&IdBateria=" + idBateria;
             }

             if (clToken != "") {
                 vURL = vURL + "&ClToken=" + clToken;
             }

             if (idRequisicion != null) {
                 vURL = vURL + "&IdRequisicion=" + idRequisicion;
             }

             if (idProcesoSeleccion == "") {
                 radalert("Selecciona un proceso de evaluación", 400, 150, "");
             }
             else {
                 OpenSelectionWindow(vURL, "rwProcesoSeleccion", vTitulo);
             }

         }

        function OpenProcesoSeleccionContinuarWindow() {
            obtenerIdFila();
            var idCandidato = '<%= vIdCandidato %>';
            var idBateria = '<%= vIdBateria %>';
            var clToken = '<%= vClToken %>';
           

            var vURL = "ProcesoSeleccion.aspx";
            var vTitulo = "Proceso de evaluación";


            vURL = vURL + "?IdProcesoSeleccion=" + idProcesoSeleccion + "&IdCandidato=" + idCandidato;

            if (idBateria != 0) {
                vURL = vURL + "&IdBateria=" + idBateria;
            }

            if (clToken != "") {
                vURL = vURL + "&ClToken=" + clToken;
            }

            if (idRequisicion != null) {
                vURL = vURL + "&IdRequisicion=" + idRequisicion;
            }

            if (idProcesoSeleccion == "") {
                radalert("Selecciona un proceso de evaluación", 400, 150, "");
            }
            else {
                OpenSelectionWindow(vURL, "rwProcesoSeleccion", vTitulo);
            }

        }

        function ConfirmarNuevoProceso(sender, args) {
            var vProcesos = '<%= grdProcesoSeleccion.Items.Count %>';

            if (vProcesos > 0) {
                confirmAction(sender, args, 'Ya cuentas con procesos de evaluación.¿Deseas iniciar un nuevo proceso?');
            }
        }

        function OpenProcesoSeleccionNuevoWindow() {

            var idCandidato = '<%= vIdCandidato %>';
            var idBateria = '<%= vIdBateria %>';
            var clToken = '<%= vClToken %>';
            var vIdRequisicion = '<%= vIdRequisicion %>';
            var vIdEmpleado = '<%= vIdEmpleado %>';
           
                var vURL = "ProcesoSeleccion.aspx";
                var vTitulo = "Proceso de evaluación";
                vURL = vURL + "?IdCandidato=" + idCandidato;

                if (idBateria != 0) {
                    vURL = vURL + "&IdBateria=" + idBateria;
                }

                if (clToken != "") {
                    vURL = vURL + "&ClToken=" + clToken;
                }

                if (vIdRequisicion != null && vIdRequisicion != "") {
                    vURL = vURL + "&IdRequisicion=" + vIdRequisicion;
                }

                if (vIdEmpleado != null && vIdEmpleado != "")
                    vURL = vURL + "&IdEmpleado=" + vIdEmpleado;

                OpenSelectionWindow(vURL, "rwProcesoSeleccion", vTitulo);
        }

        function useDataFromChild(pDato) {
            recargarGridProcesoSeleccion();
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpProcesoSeleccion" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramProcesoSeleccion" runat="server" DefaultLoadingPanelID="ralpProcesoSeleccion">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdProcesoSeleccion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdProcesoSeleccion" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                     <telerik:AjaxUpdatedControl ControlID="lbMensaje" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"/>
                     <telerik:AjaxUpdatedControl ControlID="btnContinuarProceso" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"/>
                     <telerik:AjaxUpdatedControl ControlID="btnVerProceso" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <div style="height: 10px; clear: both;"></div>
        <div>
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Folio de solicitud:</label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtClaveSolicitud" runat="server" style="width: 300px;"></span>
                </td>
                <td class="ctrlTableDataContext">
                    <label>Candidato: </label>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <span id="txtCandidato" runat="server" style="width: 300px;"></span>
                </td>             
            </tr>
        </table>
    </div>

    <div style="clear: both;"></div>
    <div style="height: calc(100% - 60px);">
        <telerik:RadGrid ID="grdProcesoSeleccion" ShowHeader="true" runat="server" AllowPaging="true"
            AllowSorting="true" GroupPanelPosition="Top" Width="100%" GridLines="None"
            Height="85%" AllowFilteringByColumn="true" ClientSettings-EnablePostBackOnRowClick="true" OnItemDataBound="grdProcesoSeleccion_ItemDataBound"
            OnNeedDataSource="grdProcesoSeleccion_NeedDataSource" HeaderStyle-Font-Bold="true" OnSelectedIndexChanged="grdProcesoSeleccion_SelectedIndexChanged">
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings>
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView  AutoGenerateColumns="false" PageSize="10" 
                ClientDataKeyNames="ID_REQUISICION, ID_PROCESO_SELECCION, CL_ESTADO">
                <CommandItemSettings ShowAddNewRecordButton="false" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave de requisición" DataField="NO_REQUISICION" UniqueName="NO_REQUISICION" HeaderStyle-Width="100" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto establecido en la requisición" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de inicio" DataField="FE_INICIO_PROCESO" UniqueName="FE_INICIO_PROCESO" HeaderStyle-Width="120" FilterControlWidth="60" DataFormatString="{0:d}"></telerik:GridBoundColumn>
<<<<<<< HEAD
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de término" DataField="FE_TERMINO_PROCESO" UniqueName="FE_TERMINO_PROCESO" HeaderStyle-Width="120" FilterControlWidth="60" DataFormatString="{0:d}"></telerik:GridBoundColumn>
=======
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de termino" DataField="FE_TERMINO_PROCESO" UniqueName="FE_TERMINO_PROCESO" HeaderStyle-Width="120" FilterControlWidth="60" DataFormatString="{0:d}"></telerik:GridBoundColumn>
>>>>>>> DEV
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estado" DataField="CL_ESTADO" UniqueName="CL_ESTADO" HeaderStyle-Width="100" FilterControlWidth="60"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Observaciones" DataField="DS_OBSERVACIONES_TERMINO_PROCESO" UniqueName="DS_OBSERVACIONES_TERMINO_PROCESO" HeaderStyle-Width="200" FilterControlWidth="130"></telerik:GridBoundColumn>

                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
         <label id="lbMensaje" runat="server" visible="false">*Este proceso ya ha sido terminado. Puedes crear uno nuevo seleccionando "Iniciar nuevo proceso de evaluación".</label>

    <div style="clear: both; height: 10px;"></div>
<<<<<<< HEAD
        <div class="divControlDerecha" style="padding-right:10px;"> 
=======
        <div class="divControlDerecha">
    <div class="ctrlBasico">
>>>>>>> DEV
        <telerik:RadButton ID="btnIniciarProceso" AutoPostBack="true" runat="server" Text="Iniciar nuevo proceso de evaluación" OnClientClicking="ConfirmarNuevoProceso" OnClick="btnIniciarProceso_Click"></telerik:RadButton>
        </div>
          <div class="divControlDerecha" style="padding-right:10px;">
        <telerik:RadButton ID="btnContinuarProceso" AutoPostBack="false" runat="server" Text="Continuar con el proceso de evaluación" OnClientClicked="OpenProcesoSeleccionContinuarWindow"></telerik:RadButton>
    </div>
         <div class="divControlDerecha">
        <telerik:RadButton ID="btnVerProceso" runat="server" Visible="false" Text="Ver proceso de evaluación" OnClientClicked="OpenProcesoSeleccionVerWindow"  AutoPostBack="false"></telerik:RadButton>
    </div>
        
            </div>
            </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server"></telerik:RadWindowManager>

</asp:Content>
