<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionCP.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionCP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OnCloseWindow() {
            GetRadWindow().close();
        }

        function GenerateDataForParent() {
            var info = null;
            var vDatos = [];
            var masterTable = $find("<%= grdCodigoPostal.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();

            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    selectedItem = selectedItems[i];

                    var vClEstado = selectedItem.getDataKeyValue("CL_ESTADO");
                    var vClMunicipio = selectedItem.getDataKeyValue("CL_MUNICIPIO");
                    var vClColonia = selectedItem.getDataKeyValue("CL_COLONIA");

                    var vDato = {
                        nbEstado: masterTable.getCellByColumnUniqueName(selectedItem, "NB_ESTADO").innerHTML,
                        clEstado: vClEstado,
                        nbMunicipio: masterTable.getCellByColumnUniqueName(selectedItem, "NB_MUNICIPIO").innerHTML,
                        clMunicipio: vClMunicipio,
                        nbColonia: masterTable.getCellByColumnUniqueName(selectedItem, "NB_COLONIA").innerHTML,
                        clColonia: vClColonia,
                        nbCP: masterTable.getCellByColumnUniqueName(selectedItem, "CL_CODIGO_POSTAL").innerHTML,
                        clTipoCatalogo: "CODIGOPOSTAL"
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
                browserWnd.radalert("Selecciona un código postal.", 400, 150);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralCP" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramCodigoPostal" runat="server" DefaultLoadingPanelID="ralCP">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnBuscarCp">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdCodigoPostal" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 120px);">
        <div style="clear: both; height: 20px;"></div>
        <div class="ctrlBasico">
            <label>Código postal:</label>
            <telerik:RadNumericTextBox runat="server" NumberFormat-DecimalDigits="0" MinValue="0" NumberFormat-GroupSeparator="" ID="txtCP" Width="60px" MaxLength="5"></telerik:RadNumericTextBox>
            <telerik:RadButton ID="btnBuscarCp" runat="server" AutoPostBack="true" Text="Buscar" Width="65" OnClick="btnBuscarCp_Click"></telerik:RadButton>
        </div>
        <div style="clear: both; height: 10px;"></div>
        <telerik:RadGrid ID="grdCodigoPostal"
            runat="server"
            Width="1100"
            Height="100%"
            AllowSorting="false"
            ShowHeader="true"
            AllowMultiRowSelection="false"
            OnNeedDataSource="grdCodigoPostal_NeedDataSource">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID_COLONIA, CL_COLONIA, CL_MUNICIPIO, CL_ESTADO " ClientDataKeyNames="ID_COLONIA, CL_COLONIA, CL_MUNICIPIO, CL_ESTADO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true">
                <Columns>
                    <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="120" FilterControlWidth="50" HeaderText="Código Postal" DataField="CL_CODIGO_POSTAL" UniqueName="CL_CODIGO_POSTAL"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="280" FilterControlWidth="210" HeaderText="Colonia" DataField="NB_COLONIA" UniqueName="NB_COLONIA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="180" FilterControlWidth="110" HeaderText="Municipio" DataField="NB_MUNICIPIO" UniqueName="NB_MUNICIPIO"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="180" FilterControlWidth="110" HeaderText="Estado" DataField="NB_ESTADO" UniqueName="NB_ESTADO"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <div style="clear: both; height: 10px;"></div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="btnAceptar" Text="Aceptar" runat="server" AutoPostBack="false" Width="100" OnClientClicked="GenerateDataForParent"></telerik:RadButton>
            <telerik:RadButton ID="btnCancelar" Text="Cancelar" runat="server" AutoPostBack="false" Width="100" OnClientClicked="OnCloseWindow"></telerik:RadButton>
        </div>
    </div>
</asp:Content>
