<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="ContestarCuestionarios.aspx.cs" Inherits="SIGE.WebApp.EO.ContestarCuestionarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .RadProgressBar .rpbStateSelected, .RadProgressBar .rpbStateSelected:hover, .RadProgressBar .rpbStateSelected:link, .RadProgressBar .rpbStateSelected:visited {
            background-color: #FF7400 !important;
            border: 0px solid black !important;
            color: black !important;
        }
    </style>
    <script type="text/javascript">
        function OnCommand(sender, args) {

            if (args.get_commandName() == "Cuestionarios") {
                var itemIndex = args.get_commandArgument();
                var rowID=sender.get_masterTableView().get_dataItems()[itemIndex].getDataKeyValue("ID_EVALUADOR");
                OpenCuestionariosWindow(rowID);
                args.set_cancel(true);
            }
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle, pWindowProperties) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            if (pWindowProperties)
                windowProperties = pWindowProperties;

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenCuestionariosWindow(vIdEvaluador) {
            var vEstadoPeriodo = ('<%= vEstadoPeriodo %>')
            var masterTable = $find('<%= grdCuestionarios.ClientID %>').get_masterTableView();
            //var selectedItems = masterTable.get_selectedItems();
            //if (selectedItems.length > 0) {
            //    var vEvaluado = selectedItems[0];
            //    var vIdEvaluador = vEvaluado.getDataKeyValue("ID_EVALUADOR");
                OpenSelectionWindow("Cuestionarios/Cuestionarios.aspx?ID_EVALUADOR=" + vIdEvaluador + "&ESTADO_PERIODO=" + vEstadoPeriodo, "winContestarCuestionarios", "Evaluación")
            //} else {
            //    radalert("Selecciona un evaluado.", 400, 150);
            //}
        }

        function useDataFromChild(pDato) {
            $find('<%= ramContestarCuestionarios.ClientID %>').ajaxRequest();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <telerik:RadAjaxLoadingPanel ID="ralpContestarCuestionarios" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramContestarCuestionarios" runat="server" OnAjaxRequest="ramContestarCuestionarios_AjaxRequest" DefaultLoadingPanelID="ralpContestarCuestionarios">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramContestarCuestionarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdCuestionarios"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdCuestionarios">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl UpdatePanelHeight="100%" ControlID="grdCuestionarios"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <div style="height: calc(100% - 100px); padding-bottom: 10px;">
        <div class="ctrlBasico">
            <table class="ctrlTableForm">
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Período:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtNoPeriodo" runat="server" style="min-width: 100px;"></div>
                    </td>
                    <td class="ctrlTableDataBorderContext">
                        <div id="txtNbPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div style="clear: both; height: 10px;"></div>
        <telerik:RadGrid ID="grdCuestionarios" HeaderStyle-Font-Bold="true" runat="server" Height="100%" Width="100%" AutoGenerateColumns="false" AllowSorting="true" OnNeedDataSource="grdCuestionarios_NeedDataSource" OnItemDataBound="grdCuestionarios_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
                <ClientEvents  OnCommand="OnCommand"/>
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ID_EVALUADOR" ClientDataKeyNames="ID_EVALUADOR" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Evaluador" DataField="NB_EVALUADOR" UniqueName="NB_EVALUADOR" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" FilterControlWidth="170" CurrentFilterFunction="Contains" HeaderStyle-Width="250" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>                    
                    <telerik:GridTemplateColumn HeaderText="Progreso" HeaderStyle-Width="200" DataField="NO_CUESTIONARIOS_CONTESTADOS" CurrentFilterFunction="Contains" AllowFiltering="false" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <telerik:RadProgressBar Width="100" ID="rpbProgreso" runat="server" MinValue="0" BarType="Percent" Value='<%# toDouble((int?)DataBinder.Eval(Container.DataItem, "NO_CUESTIONARIOS_CONTESTADOS")) %>' MaxValue='<%# toDouble((int?)DataBinder.Eval(Container.DataItem, "NO_CUESTIONARIOS")) %>'></telerik:RadProgressBar>
                            <%# Eval("DS_CUESTIONARIOS") %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn CommandName="Cuestionarios"  HeaderStyle-Width="150px" Text="Cuestionarios" UniqueName="Cuestionarios" HeaderText="Cuestionarios"  ></telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <div style="clear: both; height: 2px;"></div>
        <div class="ctrlBasico">
            <%-- %><telerik:RadButton ID="btnCuestionarios" runat="server" Text="Cuestionarios" OnClientClicked="OpenCuestionariosWindow" AutoPostBack="false" OnClick="btnCuestionarios_Click"></telerik:RadButton>--%>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>

</asp:Content>
