<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionCP.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionLocalizacion.SeleccionCP" %>

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
   <%-- <style>

        .BordeModulo {
    border: 2px solid #A9BCF5;
}

.RadButton .rbDecorated {
    background-color: transparent !important;
    background-color: rgba(0, 0, 0, 0) !important;
    color: #333 !important;
}

span.RadButton.rbSkinnedButton, span.RadButton.rbLinkButton, span.RadButton.rbVerticalButton {
    background-color: #A9BCF5 !important;
    color: #333 !important;
}

.RadButton.rbSkinnedButton {
    background-color: #A9BCF5 !important;
}

.RadGrid .rgPagerCell .rgNumPart a.rgCurrentPage, .RadDataPager .rdpNumPart a.rdpCurrentPage {
    border: 1px solid #ddd !important;
    background-color: #A9BCF5 !important;
    color: #333 !important;
}

.rwTitleRow .rwTitlebar, .rwTitleRow .rwCorner {
    border-bottom: 6px solid #A9BCF5 !important;
}

.RadGrid .rgMasterTable .rgSelectedCell, .RadGrid .rgSelectedRow > td, .RadGrid td.rgEditRow .rgSelectedRow, .RadGrid .rgSelectedRow td.rgSorted {
    color: #333 !important;
    background: #A9BCF5 none repeat scroll 0% 0% !important;
    border-color: #FFF !important;
}

.RadTreeList .rtlRSel td {
    background: #A9BCF5 none repeat scroll 0% 0% !important;
}

.RadTreeList .rtlVBorders td.rtlL, .RadTreeList_Bootstrap .rtlLines td.rtlL {
    background-color: #FFF !important;
}

.RadTreeList .rtlRSel {
    color: #333 !important;
}

.RadListBox .rlbItem.rlbSelected {
    color: #000 !important;
    background-color: #eee !important;
}

.RadListBox {
    line-height: 1.75 !important;
}

.RadTabStrip .rtsLevel1 .rtsSelected, .RadTabStrip .rtsLevel1 .rtsSelected:hover {
    background-color: #A9BCF5 !important;
    color: #333 !important;
}

.RadTabStripLeft .rtsLevel1 .rtsSelected, .RadTabStripLeft .rtsLevel1 .rtsSelected:hover {
    background-color: #A9BCF5 !important;
}

.RadToolTip {
    border: 1px solid #A9BCF5 !important;
    background-color: #A9BCF5 !important;
    color: #333 !important;
}

    </style>--%>
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
            <telerik:RadTextBox runat="server"  ID="txtCP" Width="60px" MaxLength="5"></telerik:RadTextBox>
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
            HeaderStyle-Font-Bold="true"
            OnNeedDataSource="grdCodigoPostal_NeedDataSource"
            OnItemDataBound="grdCodigoPostal_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
             <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ID_COLONIA, CL_COLONIA, CL_MUNICIPIO, CL_ESTADO" ClientDataKeyNames="ID_COLONIA, CL_COLONIA, CL_MUNICIPIO, CL_ESTADO" AutoGenerateColumns="false" AllowPaging="true" AllowFilteringByColumn="true">
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
