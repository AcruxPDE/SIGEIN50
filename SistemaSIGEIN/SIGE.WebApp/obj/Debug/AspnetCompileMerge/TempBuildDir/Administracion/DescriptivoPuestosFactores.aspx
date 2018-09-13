<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="DescriptivoPuestosFactores.aspx.cs" Inherits="SIGE.WebApp.Administracion.DescriptivoPuestosFactores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script id="MyScript" type="text/javascript">

            function onCloseWindow(oWnd, args) {
                $find("<%=grdDescriptivo.ClientID%>").get_masterTableView().rebind();
                }

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

                function ShowFactoresForm() {
                //    var selectedItem = $find("<=grdDescriptivo.ClientID %>").get_masterTableView().get_selectedItems()[0];
                //if (selectedItem != undefined)
                    //OpenFactoresWindow(selectedItem.getDataKeyValue("ID_PUESTO"), null);
                    OpenFactoresWindow();
                //else
                //    radalert("Selecciona un puesto.", 400, 150);
            }

            function OpenFactoresWindow() {
                var vURL = "VentanaDescriptivoFactores.aspx";
                var vTitulo = "Definición de factores para consulta global";

                vURL = vURL + "?ConsultaGlobalId=" + '<%= vIdConsultaGlobal %>';


                var windowProperties = {
                    width: 850,
                    height: 500
                };
                openChildDialog(vURL, "winDescriptivo", vTitulo, windowProperties);
            }

        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdDescriptivo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDescriptivo" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>

    <label class="labelTitulo">Definir factores para consulta global</label>
    <div style="height: calc(100% - 100px);">
        <telerik:RadGrid ID="grdDescriptivo" runat="server" AllowPaging="true" HeaderStyle-Font-Bold="true" AllowSorting="true" GroupPanelPosition="Top" Height="100%" AllowFilteringByColumn="true"
            OnNeedDataSource="grdDescriptivo_NeedDataSource" AllowMultiRowSelection="true" OnItemDataBound="grdDescriptivo_ItemDataBound">
            <GroupingSettings CaseSensitive="False" />
            <ExportSettings FileName="DescriptivoPuesto" ExportOnlyData="true" IgnorePaging="true">
                <Excel Format="Xlsx" />
            </ExportSettings>
            <ClientSettings AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView ClientDataKeyNames="ID_PUESTO" DataKeyNames="ID_PUESTO, CL_PUESTO, NB_PUESTO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" CommandItemDisplay="Top">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" />
                <Columns>
                    <telerik:GridClientSelectColumn UniqueName="Select" Exportable="false" HeaderStyle-Width="30"></telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre corto" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Descripción" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="300" FilterControlWidth="230"></telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Último usuario que modifica" DataField="CL_USUARIO_MODIFICA" UniqueName="CL_USUARIO_MODIFICA"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataFormatString="{0:d}"  AutoPostBackOnFilter="true" HeaderText="Última fecha de modificación" DataField="FE_MODIFICACION_PUESTO" UniqueName="FE_MODIFICACION_PUESTO" HeaderStyle-Width="150" FilterControlWidth="80" DataType="System.DateTime"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnFactores" OnClick="btnFactores_Click" AutoPostBack="true" runat="server" Text="Definición de factores" ></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rwmAlertas" runat="server" Animation="Fade">
        <Windows>
            <telerik:RadWindow ID="winDescriptivo" runat="server" Title="Agregar/Editar Puestos" Height="600px" Width="500px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winFactores" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="onCloseWindow"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
