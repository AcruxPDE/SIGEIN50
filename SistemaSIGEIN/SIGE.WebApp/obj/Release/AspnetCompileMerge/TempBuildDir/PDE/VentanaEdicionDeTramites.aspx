<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaEdicionDeTramites.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaEdicionDeTramites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>

        function OpenAgregarTramite() {
            var vURL = "VentanaAdministrarTramites.aspx";
            var vTitulo = "Agregar nuevo trámite";
            var oWin = window.radopen(vURL, "rwTramites", document.documentElement.clientWidth - 10, document.documentElement.clientHeight - 10);
            oWin.set_title(vTitulo);
        }

        function OpenCopiarTramites() {
            var selectedItem = $find("<%=rgTramites.ClientID %>").get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined)
                OpenWindowCopiarTramites(selectedItem.getDataKeyValue("ID_FORMATO_TRAMITE"));
            else
                radalert("Selecciona un formato.", 400, 150);

        }

        function OpenWindowCopiarTramites(pIdTramite, pXmlTramite) {
            var vURL = "VentanaCopiarFormatoTramite.aspx?Indice=" + pIdTramite;
            var vTitulo = "Copiar de";
            var oWin = window.radopen(vURL, "rwTramites", document.documentElement.clientWidth - 550, document.documentElement.clientHeight - 180);
            oWin.set_title(vTitulo);


        }
        function OpenEliminarTramites(sender, args) {
            var MasterTable = $find("<%=rgTramites.ClientID %>").get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();
            var row = selectedRows[0];
            if (row != null) {
                CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_FORMATO_TRAMITE");
                if (selectedRows != "") {
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });

                    radconfirm('¿Deseas eliminar el formato ' + CELL_NOMBRE.innerHTML + ' ?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                    args.set_cancel(true);
                }
            } else {
                radalert("Seleccione un trámite.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        function GetEventoId() {
            var vIdFormato;
            var Formatos = $find('<%= rgTramites.ClientID %>').get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined) {
                vIdFormato = selectedItem.getDataKeyValue("ID_FORMATO_TRAMITE");
                return vIdPrograma;
            }
            else {
                return null;
            }

        }
        function OpenEditarTramites(sender, args) {

            var selectedItem = $find("<%=rgTramites.ClientID %>").get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined)
                OpenWindowEditarTramite(selectedItem.getDataKeyValue("ID_FORMATO_TRAMITE"));
            else
                radalert("Selecciona un formato.", 400, 150);
        }

        function OpenWindowEditarTramite(pIdTramite) {
            var vURL = "VentanaAdministrarTramites.aspx?IdTramite=" + pIdTramite
            var vTitulo = "Editar trámite";
            var oWin = window.radopen(vURL, "rwTramites", document.documentElement.clientWidth - 10, document.documentElement.clientHeight - 10);
            oWin.set_title(vTitulo);
        }

        function closeWindow() {

            GetRadWindow().close();

        }

        function onCloseWindow(oWnd, args) {
            $find("<%=rgTramites.ClientID%>").get_masterTableView().rebind();

               }

    </script>
    <style>
        .btnEditar {
            padding-left: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgTramites">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgTramites" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
 
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 60px);">
        <telerik:RadSplitter ID="rsEdicionTramites" BorderSize="0" Width="100%" Height="100%" runat="server">
            <telerik:RadPane ID="rpEdicionTramites" runat="server">
                <div style="margin-right: 20px;">
                    <div style="clear: both; height: 10px;"></div>
                    <label class="labelTitulo">Edición de trámites</label>
                    <telerik:RadGrid ID="rgTramites" ShowHeader="true" runat="server" AllowPaging="true"
                        AllowSorting="true" Width="100%" OnNeedDataSource="grdTramites_NeedDataSource">
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView DataKeyNames="ID_FORMATO_TRAMITE, NB_FORMATO_TRAMITE, XML_FORMATO_TRAMITE,DS_FORMATO_TRAMITE" ClientDataKeyNames="ID_FORMATO_TRAMITE, NB_FORMATO_TRAMITE, XML_FORMATO_TRAMITE,DS_FORMATO_TRAMITE" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="20" EditFormSettings-EditColumn-AutoPostBackOnFilter="true"
                            HorizontalAlign="NotSet" PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains" HeaderText="Clave" HeaderStyle-Width="150" FilterControlWidth="115" DataField="NB_FORMATO_TRAMITE" UniqueName="NB_FORMATO_TRAMITE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains" HeaderText="Descripción" HeaderStyle-Width="250" FilterControlWidth="230" DataField="DS_FORMATO_TRAMITE" UniqueName="DS_FORMATO_TRAMITE"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>

            </telerik:RadPane>

            <telerik:RadPane ID="rpAyudaEdicionDeTramites" runat="server" Scrolling="None" Width="20px">
                <telerik:RadSlidingZone ID="rszAyudaEdicionDeTramites" runat="server" SlideDirection="Left" ExpandedPaneId="rsEdicionDeTramites" Width="20px">
                    <telerik:RadSlidingPane ID="rspAyudaEdicionDeFormatos" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="90%">
                        <div style="padding: 20px; text-align: justify;">
                            <p>Ésta opción te permite administrar los formatos descargables; introduce el texto requerido y selecciona aquellos campos que deseas que lleve el formato.</p>

                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton AutoPostBack="false" ID="btnAgregar" runat="server" Text="Agregar" Width="100" OnClientClicked="OpenAgregarTramite"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton AutoPostBack="false" ID="btnEditar" runat="server" Text="Editar" Width="100" OnClientClicked="OpenEditarTramites"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton AutoPostBack="false" ID="btnCopiar" runat="server" Text="Copiar" Width="100" OnClientClicked="OpenCopiarTramites"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminar_Click" OnClientClicking="OpenEliminarTramites"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmEdicionTramites" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="rwTramites"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
