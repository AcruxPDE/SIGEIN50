﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="SeleccionMunicipio.aspx.cs" Inherits="SIGE.WebApp.Comunes.SeleccionLocalizacion.SeleccionMunicipio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script id="MyScript" type="text/javascript">
        function generateDataForParent() {
            var info = null;
            var vDatos = [];
            var masterTable = $find("<%= grdMunicipios.ClientID %>").get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                for (i = 0; i < selectedItems.length; i++) {
                    var vDato = {};
                    selectedItem = selectedItems[i];
                    vDato.clDato = selectedItem.getDataKeyValue("CL_MUNICIPIO");
                    vDato.nbDato = masterTable.getCellByColumnUniqueName(selectedItem, "NB_MUNICIPIO").innerHTML;
                    vDato.clTipoCatalogo = "MUNICIPIO"
                    vDatos.push(vDato);
                }
                sendDataToParent(vDatos);
            }
            else {
                var currentWnd = GetRadWindow();
                var browserWnd = window;
                if (currentWnd)
                    browserWnd = currentWnd.BrowserWindow;
                browserWnd.radalert("Selecciona un municipio.", 400, 150);
            }
        }

        function cancelarSeleccion() {
            sendDataToParent(null);
        }
    </script>
     <%--  <style>

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
    <telerik:RadGrid ID="grdMunicipios" runat="server" Height="485" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" AllowSorting="true" OnNeedDataSource="grdMunicipios_NeedDataSource">
        <ClientSettings>
            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <PagerStyle AlwaysVisible="true" />
        <GroupingSettings CaseSensitive="false" />
        <MasterTableView ClientDataKeyNames="ID_MUNICIPIO,CL_MUNICIPIO" DataKeyNames="ID_MUNICIPIO,CL_MUNICIPIO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
            <Columns>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Clave" DataField="CL_MUNICIPIO" UniqueName="CL_MUNICIPIO"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Municipio" DataField="NB_MUNICIPIO" UniqueName="NB_MUNICIPIO"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <div class="divControlDerecha" style="padding-top: 10px;">
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnSeleccion" runat="server" name="btnSeleccion" Text="Seleccionar" AutoPostBack="false" OnClientClicked="generateDataForParent"></telerik:RadButton>
        </div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCancelar" runat="server" name="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="cancelarSeleccion"></telerik:RadButton>
        </div>
    </div>
</asp:Content>