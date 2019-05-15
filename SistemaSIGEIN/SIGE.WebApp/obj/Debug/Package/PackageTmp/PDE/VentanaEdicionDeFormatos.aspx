<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/ContextPDE.master" AutoEventWireup="true" CodeBehind="VentanaEdicionDeFormatos.aspx.cs" Inherits="SIGE.WebApp.PDE.VenatanaEdicionDeFormatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script>

        function OpenAgregarFormato() {
            var vURL = "VentanaAdministrarFormatos.aspx";
            var vTitulo = "Agregar nuevo formato";
            var oWin = window.radopen(vURL, "rwFormatos", document.documentElement.clientWidth - 10, document.documentElement.clientHeight - 10);
            oWin.set_title(vTitulo);
        }

        function OpenCopiarFormatos() {
            var selectedItem = $find("<%=rgFormatos.ClientID %>").get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined)
                OpenWindowCopiarFormato(selectedItem.getDataKeyValue("ID_FORMATO_TRAMITE"));
            else
                radalert("Selecciona un formato.", 400, 150);
        }

        function OpenWindowCopiarFormato(pIdFormato) {
            var vURL = "VentanaCopiarFormatoTramite.aspx?Indice=" + pIdFormato;
            var vTitulo = "Copiar";
            var oWin = window.radopen(vURL, "rwFormatos", document.documentElement.clientWidth - 550, document.documentElement.clientHeight - 180);
            oWin.set_title(vTitulo);


        }
        function OpenEliminarFormatos(sender, args) {
            var MasterTable = $find("<%=rgFormatos.ClientID %>").get_masterTableView();
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
                radalert("Seleccione un formato.", 400, 150, "");
                args.set_cancel(true);
            }
        }

        function GetEventoId() {
            var vIdFormato;
            var Formatos = $find('<%= rgFormatos.ClientID %>').get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined) {
                vIdFormato = selectedItem.getDataKeyValue("ID_FORMATO_TRAMITE");
                return vIdPrograma;
            }
            else {
                return null;
            }

        }


        function OpenEditarFormatos(sender, args) {
            var selectedItem = $find("<%=rgFormatos.ClientID %>").get_masterTableView().get_selectedItems()[0];
            if (selectedItem != undefined)
                OpenWindowEditarFormato(selectedItem.getDataKeyValue("ID_FORMATO_TRAMITE"));
            else
                radalert("Selecciona un formato.", 400, 150);
        }
        function OpenWindowEditarFormato(pIdFormato) {
            var vURL = "VentanaAdministrarFormatos.aspx?IdFormato=" + pIdFormato
            var vTitulo = "Editar formato";
            var oWin = window.radopen(vURL, "rwFormatos", document.documentElement.clientWidth - 10, document.documentElement.clientHeight - 10);
            oWin.set_title(vTitulo);
        }

        function closeWindow() {

            GetRadWindow().close();

        }
        function onCloseWindow(oWnd, args) {
            $find("<%=rgFormatos.ClientID%>").get_masterTableView().rebind();

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
            <telerik:AjaxSetting AjaxControlID="rgFormatos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgFormatos" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 60px);">
        <telerik:RadSplitter ID="rsEdicionFormatos" BorderSize="0" Width="100%" Height="100%" runat="server">
            <telerik:RadPane ID="rpEdicionFormatos" runat="server">
                <div style="margin-right: 20px;">
                    <div style="clear: both; height: 10px;"></div>
                    <label class="labelTitulo">Edición de formatos</label>

                    <telerik:RadGrid ID="rgFormatos" ShowHeader="true" runat="server" AllowPaging="true"
                        OnNeedDataSource="grdFormatos_NeedDataSource"
                        AllowSorting="true" Width="100%">
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView ShowHeadersWhenNoRecords="true" DataKeyNames="ID_FORMATO_TRAMITE, NB_FORMATO_TRAMITE, XML_FORMATO_TRAMITE, DS_FORMATO_TRAMITE" ClientDataKeyNames="ID_FORMATO_TRAMITE, NB_FORMATO_TRAMITE, XML_FORMATO_TRAMITE,DS_FORMATO_TRAMITE" EditFormSettings-EditColumn-AutoPostBackOnFilter="true" AutoGenerateColumns="false" PageSize="20"
                            HorizontalAlign="NotSet" PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="true">
                            <Columns>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains" HeaderText="Clave" HeaderStyle-Width="150" FilterControlWidth="115" DataField="NB_FORMATO_TRAMITE" UniqueName="NB_FORMATO_TRAMITE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains" HeaderText="Descripción" HeaderStyle-Width="250" FilterControlWidth="230" DataField="DS_FORMATO_TRAMITE" UniqueName="DS_FORMATO_TRAMITE"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>


            </telerik:RadPane>

            <telerik:RadPane ID="rpAyudaEdicionDeFormatos" runat="server" Scrolling="None" Width="20px">

                <telerik:RadSlidingZone ID="rszAyudaEdicionDeFormatos" runat="server" SlideDirection="Left" ExpandedPaneId="rsEdicionDeFormatos" Width="20px">
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
        <telerik:RadButton AutoPostBack="false" ID="btnAgregar" runat="server" Text="Agregar" Width="100" OnClientClicked="OpenAgregarFormato"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton AutoPostBack="false" ID="btnEditar" runat="server" Text="Editar" Width="100" OnClientClicked="OpenEditarFormatos"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton AutoPostBack="false" ID="btnCopiar" runat="server" Text="Copiar" Width="100" OnClientClicked="OpenCopiarFormatos"></telerik:RadButton>
    </div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminar_Click" OnClientClicking="OpenEliminarFormatos"></telerik:RadButton>
    </div>
    <telerik:RadWindowManager ID="rwmEdicionFormatos" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="rwFormatos"
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
