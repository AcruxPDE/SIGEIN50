﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="CatalogoAreas.aspx.cs" Inherits="SIGE.WebApp.Administracion.CatalogoAreas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">

        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdDepartamentos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDepartamentos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDepartamentos" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDepartamentos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rnMensaje"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEditar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDepartamentos" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
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

            function onCloseWindow(oWnd, args) {
                idDepartamentos = "";
                $find("<%=grdDepartamentos.ClientID%>").get_masterTableView().rebind();
            }

            function ShowInsertForm() {
                OpenWindow(null);
                return false;
            }

            function ShowEditForm() {
                var selectedItem = $find("<%=grdDepartamentos.ClientID %>").get_masterTableView().get_selectedItems()[0];
                if (selectedItem != undefined)
                    OpenWindow(selectedItem.getDataKeyValue("ID_DEPARTAMENTO"));
                else
                    radalert("Selecciona una área.", 400, 150);
            }

            function OpenWindow(pIdArea) {
                var vURL = "VentanaCatalogoAreas.aspx";
                var vTitulo = "Agregar Área";
                var vTipoOperacion = "?TIPO=Agregar";
                if (pIdArea != null) {
                    vURL = vURL + "?ID=" + pIdArea;
                    vTitulo = "Editar área";
                    vTipoOperacion = "&TIPO=Editar";
                }
                var oWin = window.radopen(vURL + vTipoOperacion, "winArea");
                oWin.set_title(vTitulo);
            }

            //function ShowPopupEditarAreas() {
            //    obtenerIdFila();

            //    if ((idDepartamentos != "")) {
            //        var oWnd = radopen("VentanaCatalogoAreas.aspx?&ID=" + idDepartamentos + "&TIPO=Editar", "RWPopupmodalCatalogoGenericoEditar");
            //        oWnd.set_title("Editar Área");
            //    }
            //    else {
            //        radalert("No has seleccionado un registro.", 400, 150, "Error");
            //    }
            //}

            //function ShowPopupAgregarAreas() {
            //    var oWnd = radopen("VentanaCatalogoAreas.aspx?&TIPO=Agregar", "RWPopupmodalCatalogoGenericoEditar");
            //    oWnd.set_title("Agregar Área");
            //}


            function ConfirmarEliminar(sender, args) {
                var MasterTable = $find("<%=grdDepartamentos.ClientID %>").get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();

                var row = selectedRows[0];

                if (row != null) {
                    CELL_NOMBRE = MasterTable.getCellByColumnUniqueName(row, "NB_DEPARTAMENTO");
                    if (selectedRows != "") {
                        var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                        { if (shouldSubmit) { this.click(); } });

                        radconfirm('¿Deseas eliminar el área ' + CELL_NOMBRE.innerHTML + ' ?, este proceso no podrá revertirse.', callBackFunction, 400, 170, null, "Eliminar Registro");
                        args.set_cancel(true);
                    }
                } else {
                    radalert("Seleccione una área.", 400, 150, "");
                    args.set_cancel(true);
                }
            }

        </script>
    </telerik:RadCodeBlock>

    <label class="labelTitulo">Áreas</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdDepartamentos" ShowHeader="true" runat="server" AllowPaging="true" AllowSorting="true" GroupPanelPosition="Top" Width="950px" Height="100%" AllowFilteringByColumn="true"
            OnNeedDataSource="grdDepartamentos_NeedDataSource" HeaderStyle-Font-Bold="true" OnItemDataBound="grdDepartamentos_ItemDataBound">
             <GroupingSettings CaseSensitive="false" />
            <ExportSettings FileName="CatalogoAreas" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_DEPARTAMENTO" DataKeyNames="ID_DEPARTAMENTO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false"
                CommandItemDisplay="Top" HorizontalAlign="NotSet" EditMode="EditForms">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToCsvButton="false"
                    RefreshText="Actualizar" AddNewRecordText="Insertar" />
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave" DataField="CL_DEPARTAMENTO" UniqueName="CL_DEPARTAMENTO" HeaderStyle-Width="150" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="370" FilterControlWidth="300"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Tipo departamento" DataField="NB_TIPO_DEPARTAMENTO" UniqueName="NB_TIPO_DEPARTAMENTO" HeaderStyle-Width="120" FilterControlWidth="70"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnGuardar" OnClientClicked="ShowInsertForm" AutoPostBack="false" runat="server" Text="Agregar" Width="100"></telerik:RadButton>
        <telerik:RadButton ID="btnEditar" OnClientClicked="ShowEditForm" AutoPostBack="false" runat="server" Text="Editar" Width="100"></telerik:RadButton>
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar" Width="100" OnClick="btnEliminar_click" OnClientClicking="ConfirmarEliminar"></telerik:RadButton>
    </div>
<%--    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RWPopupmodalCatalogoGenericoEditar" runat="server" Title="Catálogo de Áreas." Height="260"
                Width="595" Left="5%" ReloadOnShow="true" ShowContentDuringLoad="false" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"
                Modal="true" OnClientClose="onCloseWindow">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>--%>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar área" Width="800" Height="600" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow ID="winArea" runat="server" Title="Agregar/Editar Área" Height="400" Width="600" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>