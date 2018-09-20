<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaObtenerFolioSolicitud.aspx.cs" Inherits="SIGE.WebApp.IDP.Pruebas.VentanaObtenerFolioSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>

        function CloseWindow() {
            sendDataToParent(null);
        }

        function GenerateDataForParent() {
            var info = null;
            var vDatos = [];
            var masterTable = $find("<%= grdCandidatos.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];
                    var vClSolicitud = selectedItem.getDataKeyValue("CL_SELECCION");
                    var vDato = {
                        clSolicitud: vClSolicitud,
                        clTipoCatalogo: "FOLIOSELECCION"
                    };

                    vDatos.push(vDato);
                }
                sendDataToParent(vDatos);
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona una solicitud.", 400, 150);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadSplitter ID="splBuscarCandidato" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="rpnBuscarCandidato" runat="server" Height="100%">
            <div style="height: calc(100% - 50px); clear: both;">
                <div style="height: 10px; clear: both;"></div>
                <label id="nombre">Nombre:</label><telerik:RadTextBox ID="txtNombre" runat="server" Width="200"></telerik:RadTextBox>
                <label id="apellido">Apellido (paterno):</label><telerik:RadTextBox ID="txtApellido" runat="server" Width="200"></telerik:RadTextBox>
                <telerik:RadButton ID="btnBuscar" runat="server" Text="Buscar" AutoPostBack="true" OnClick="btnBuscar_Click"></telerik:RadButton>
                <div style="height: 10px; clear: both;"></div>
                <telerik:RadGrid ID="grdCandidatos"
                    runat="server"
                    Width="100%"
                    Height="90%"
                    AllowSorting="false"
                    ShowHeader="true"
                    AllowMultiRowSelection="false"
                    HeaderStyle-Font-Bold="true"
                    OnNeedDataSource="grdCandidatos_NeedDataSource"
                    OnItemDataBound="grdCandidatos_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="ID_SELECCION, CL_SELECCION" ClientDataKeyNames="ID_SELECCION, CL_SELECCION" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true">
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="150" FilterControlWidth="90" HeaderText="Folio de solicitud" DataField="CL_SELECCION" UniqueName="CL_SELECCION"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Nombre completo" DataField="NB_SELECCION" UniqueName="NB_SELECCION"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div style="clear: both; height: 10px;"></div>
            <div class="divControlDerecha">
                <telerik:RadButton ID="btnAceptar" Text="Seleccionar" runat="server" AutoPostBack="false" Width="100" OnClientClicked="GenerateDataForParent"></telerik:RadButton>
                <telerik:RadButton ID="btnCancelar" Text="Cancelar" runat="server" AutoPostBack="false" Width="100" OnClientClicked="CloseWindow"></telerik:RadButton>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpnAyuda" runat="server" Width="30">
            <telerik:RadSlidingZone ID="slzAyuda" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                <telerik:RadSlidingPane ID="slpAyuda" runat="server" Title="Ayuda" Width="300" MinWidth="300">
                    <div style="padding: 20px;">
                        Para recuperar el folio de solicitud de empleo, por favor ingresa tu nombre completo y apellido paterno y oprime el botón de "Buscar".
                        <br />
                        Al realizar la búsqueda se mostraran aquellas solicitudes encontradas que coincidan con los criterios especificados. Por favor selecciona el folio deseado.
                        <br />
                        <br />
                        ¿Tu nombre/folio no se encuentra en la lista? Por favor solicita ayuda a alguna persona de Selección.
                    </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
    </telerik:RadSplitter>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Height="100%"></telerik:RadWindowManager>
</asp:Content>
