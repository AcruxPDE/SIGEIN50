<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="VentanaGrupos.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaGrupos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OnCloseWindows() {
            GetRadWindow().close();
        }

        function OpenWindowPlazas() {
            OpenSelectionWindow("../Comunes/SeleccionPlaza.aspx?mulSel=1", "winSeleccion", "Selección de plazas")
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

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var vDatoSeleccionado = pDato[0];

                switch (vDatoSeleccionado.clTipoCatalogo) {
                    case "PLAZA":
                        InsertarDato(EncapsularDatos("PLAZA", pDato));
                        break;
                    default:
                        break;
                }
            }
        }

        function InsertarDato(pDato) {
            var vAjaxManager = $find('<%= ramGrupos.ClientID %>');
            vAjaxManager.ajaxRequest(pDato);
        }

        function EncapsularDatos(pClTipoDato, pSeleccionDatos) {
            return JSON.stringify({ clTipo: pClTipoDato, oSeleccion: pSeleccionDatos });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramGrupos" runat="server" OnAjaxRequest="ramGrupos_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramGrupos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrupos" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 60px); width: 100%;">
        <div style="height: 10px; clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda" style="width: 160px;">
                <label name="lbClaveGrupo">*Clave del grupo:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtClaveGrupo" MaxLength="20" runat="server" Width="250"></telerik:RadTextBox>
            </div>
        </div>
        <div style="height: 10px; clear: both;"></div>
        <div class="ctrlBasico">
            <div class="divControlIzquierda" style="width: 160px;">
                <label name="lblNombreGrupo">*Nombre del grupo:</label>
            </div>
            <div class="divControlDerecha">
                <telerik:RadTextBox ID="txtNombreGrupo" runat="server" Width="250"></telerik:RadTextBox>
            </div>
        </div>
        <div style="height: 10px; clear: both;"></div>
        <div class="ctrlBasico">
            <label name="lblIntegrantes">Seleccionar integrantes</label>
        </div>
        <div style="clear: both;"></div>
        <telerik:RadGrid
            ID="rgGrupos"
            runat="server"
            Width="100%"
            Height="350"
            AllowPaging="true"
            AutoGenerateColumns="false"
            HeaderStyle-Font-Bold="true"
            EnableHeaderContextMenu="true"
            AllowMultiRowSelection="true"
            OnNeedDataSource="rgGrupos_NeedDataSource"
            OnItemDataBound="rgGrupos_ItemDataBound">
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                <Selecting AllowRowSelect="true" CellSelectionMode="MultiCell" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView DataKeyNames="ID_PLAZA" ClientDataKeyNames="ID_PLAZA" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn UniqueName="CL_PLAZA" DataField="CL_PLAZA" HeaderText="Clave plaza" AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="80" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Nombre puesto" AutoPostBackOnFilter="true" HeaderStyle-Width="250" FilterControlWidth="180" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="NB_EMPLEADO" DataField="NB_EMPLEADO" HeaderText="Nombre empleado" AutoPostBackOnFilter="true" HeaderStyle-Width="300" FilterControlWidth="220" CurrentFilterFunction="Contains"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <div style="height: 10px; clear: both;"></div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnSeleccionar" runat="server" Text="Seleccionar" AutoPostBack="false" OnClientClicked="OpenWindowPlazas"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" AutoPostBack="true" OnClick="btnEliminar_Click"></telerik:RadButton>
        </div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="btnAceptar" runat="server" Text="Aceptar" AutoPostBack="true" OnClick="btnAceptar_Click"></telerik:RadButton>
            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="OnCloseWindows"></telerik:RadButton>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
