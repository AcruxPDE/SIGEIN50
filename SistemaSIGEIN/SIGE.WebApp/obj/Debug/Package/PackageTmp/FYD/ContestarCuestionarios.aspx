<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="ContestarCuestionarios.aspx.cs" Inherits="SIGE.WebApp.FYD.ContestarCuestionarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .RadProgressBar .rpbStateSelected, .RadProgressBar .rpbStateSelected:hover, .RadProgressBar .rpbStateSelected:link, .RadProgressBar .rpbStateSelected:visited {
            background-color: #FF7400 !important;
            border: 0px solid black !important;
            color: black !important;
        }
    </style>
    <script type="text/javascript">

        function OpenSelectionWindow(pURL, pIdWindow, pTitle, pWindowProperties) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 40,
                height: browserWnd.innerHeight - 40
            };

            if (pWindowProperties)
                windowProperties = pWindowProperties;

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenCuestionariosWindow() {
            var vEstadoPeriodo = ('<%= vEstadoPeriodo %>')
            var masterTable = $find('<%= grdCuestionarios.ClientID %>').get_masterTableView();
            var selectedItems = masterTable.get_selectedItems();
            if (selectedItems.length > 0) {
                var vEvaluado = selectedItems[0];
                var vIdEvaluador = vEvaluado.getDataKeyValue("ID_EVALUADOR");
                OpenSelectionWindow("EvaluacionCompetencia/CuestionariosInterno.aspx?ID_EVALUADOR=" + vIdEvaluador + "&ESTADO_PERIODO=" + vEstadoPeriodo, "winContestarCuestionarios", "Evaluación")
            } else {
                radalert("Selecciona un evaluado.", 400, 150);
            }
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

    <%-- <table class="ctrlTableForm">
        <tr>
            <td>
                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Periodo:"></telerik:RadLabel>
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtNoPeriodo" Width="80" Text="10" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right"></telerik:RadTextBox>
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtNbPeriodo" Width="500" Text="Evaluación 360 Mayo 2015" ReadOnly="true"></telerik:RadTextBox>
            </td>
        </tr>
    </table>--%>

            <telerik:RadTabStrip ID="rtsEvaluacion" runat="server" SelectedIndex="0" MultiPageID="rmpContestar">
        <Tabs>
             <telerik:RadTab Text="Contexto" Value="0"></telerik:RadTab>
            <telerik:RadTab Text="Contestar" Value="1"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
     <div style="height: calc(100% - 90px);">
     <telerik:RadMultiPage ID="rmpContestar" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">  
                      <div class="ctrlBasico">
                                    <table class="ctrlTableForm" text-align: left;>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbPeriodo" name="lbTabulador" runat="server">Período:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtClPeriodo" runat="server"></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="lbNbPeriodo" name="lbTabulador" runat="server">Descripción:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtDsPeriodo" runat="server"></div>
                                            </td>
                                        </tr>
                                                 <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label1" name="lbTabulador" runat="server">Estatus:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtEstatus" runat="server"></div>
                                            </td>
                                        </tr>
                                                      <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label2" name="lbTabulador" runat="server">Tipo de evaluación:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtTipoEvaluacion" runat="server"></div>
                                            </td>
                                        </tr>
                                             <tr>
                                            <td class="ctrlTableDataContext">
                                                <label id="Label3" name="lbTabulador" runat="server">Notas:</label>
                                            </td>
                                            <td  class="ctrlTableDataBorderContext">
                                                <div id="txtNotas" runat="server"></div>
                                            </td>
                                        </tr>
                                        </table>
                                    </div>
                </telerik:RadPageView>
           <telerik:RadPageView ID="rpvContestar" runat="server">  
    <div style="height: calc(100% - 40px);">
<%--        <div class="ctrlBasico">
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
        </div>--%>
        <div style="clear: both; height: 10px;"></div>
        <telerik:RadGrid ID="grdCuestionarios" runat="server" Height="100%" Width="100%" AutoGenerateColumns="false" AllowSorting="true" OnNeedDataSource="grdCuestionarios_NeedDataSource" OnItemDataBound="grdCuestionarios_ItemDataBound">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ID_EVALUADOR" ClientDataKeyNames="ID_EVALUADOR" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridBoundColumn HeaderText="Evaluador" DataField="NB_EVALUADOR" UniqueName="NB_EVALUADOR" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                        <HeaderStyle Font-Bold="true" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" FilterControlWidth="170" CurrentFilterFunction="Contains" HeaderStyle-Width="250" AutoPostBackOnFilter="true">
                        <HeaderStyle Font-Bold="true" />
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn HeaderText="Cuestionarios" DataField="DS_CUESTIONARIOS" UniqueName="DS_CUESTIONARIOS" AllowFiltering="false" HeaderStyle-Width="150" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>--%>
                    <telerik:GridTemplateColumn HeaderText="Progreso" HeaderStyle-Width="200" DataField="NO_CUESTIONARIOS_CONTESTADOS" CurrentFilterFunction="Contains" AllowFiltering="false" ItemStyle-HorizontalAlign="Right">
                        <HeaderStyle Font-Bold="true" />
                        <ItemTemplate>
                            <telerik:RadProgressBar Width="100" ID="rpbProgreso" runat="server" MinValue="0" BarType="Percent" Value='<%# toDouble((int?)DataBinder.Eval(Container.DataItem, "NO_CUESTIONARIOS_CONTESTADOS")) %>' MaxValue='<%# toDouble((int?)DataBinder.Eval(Container.DataItem, "NO_CUESTIONARIOS")) %>'></telerik:RadProgressBar>
                            <%# Eval("DS_CUESTIONARIOS") %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <div style="clear: both; height: 10px;"></div>
        <div class="ctrlBasico">
            <telerik:RadButton ID="btnCuestionarios" runat="server" Text="Cuestionarios" OnClientClicked="OpenCuestionariosWindow" AutoPostBack="false" OnClick="btnCuestionarios_Click"></telerik:RadButton>
        </div>
    </div>
               </telerik:RadPageView>
         </telerik:RadMultiPage>
         </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>

</asp:Content>
