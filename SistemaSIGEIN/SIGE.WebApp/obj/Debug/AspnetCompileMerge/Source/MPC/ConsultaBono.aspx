<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="ConsultaBono.aspx.cs" Inherits="SIGE.WebApp.MPC.ConsultaBono" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function OpenWindowPeriodos() {
            OpenSelectionWindows("/Comunes/SeleccionPeriodosDesempeno.aspx?CL_TIPO=Bono", "winSeleccion", "Seleccion de periodos a comparar");
        }

        function OpenWindowComparar() {
            OpenSelectionWindows("VentanaConsultaBono.aspx", "winBonos", "Comparación de bonos");
        }

        function OpenSelectionWindows(pURL, pVentana, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;
            var WindowsProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };
            openChildDialog(pURL, pVentana, pTitle, WindowsProperties);
        }

        function useDataFromChild(pDato) {
            if (pDato != null) {
                var arr = [];
                var vDatosSeleccionados = pDato[0];
                switch (pDato[0].clTipoCatalogo) {
                    case "PERIODODESEMPENO":
                        var vJsonPeriodo = JSON.stringify({ clTipo: "PERIODODESEMPENO", oSeleccion: pDato });
                        var ajaxManager = $find('<%= ramConsultas.ClientID%>');
                            ajaxManager.ajaxRequest(vJsonPeriodo);
                            break;
                    }
                }
            }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramConsultas" runat="server" OnAjaxRequest="ramConsultas_AjaxRequest">


       <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ramConsultas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgComparativos" UpdatePanelHeight="100%" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        </telerik:RadAjaxManager>
  <telerik:RadTabStrip ID="rtsTabuladorSueldos" runat="server" SelectedIndex="0" MultiPageID="rmpTabuladorSueldos">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Periodos Bono"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadSplitter ID="rsConsultas" runat="server" Width="100%" Height="95%" BorderSize="0">
        <telerik:RadPane ID="rpConsultas" runat="server" Width="100%" Height="100%">
            <div style="height: calc(100% - 50px); overflow: auto;">
                <telerik:RadMultiPage ID="rmpTabuladorSueldos" runat="server" SelectedIndex="0" Height="100%">
                    <telerik:RadPageView ID="rpvContexto" runat="server">
                        <telerik:RadToolTip ID="rttVerde" runat="server" ShowDelay="500" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
                            TargetControlID="Span1" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_IP">
                            <span style="font-weight: bold">Sueldo dentro del nivel establecido por el tabulador (variación inferior al 10%).</span>
                        </telerik:RadToolTip>
                        <div style="height: 10px;"></div>
                        <div class="ctrlBasico">
                            <table class="ctrlTableForm">
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Tabulador:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtClaveTabulador" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Descripción:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtDescripción" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Fecha:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtFecha" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Vigencia:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtVigencia" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ctrlTableDataContext">
                                        <label>Tipo de puestos:</label></td>
                                    <td colspan="2" class="ctrlTableDataBorderContext">
                                        <div id="txtPuestos" runat="server" style="min-width: 100px;"></div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvParametrosAnalisis" runat="server">
                        <div style="height: 15px; clear: both;"></div>

                          <div style="height: calc(100% - 60px);  overflow: auto;">



                             <telerik:RadGrid
                    ID="rgComparativos"
                    runat="server"
                    Height="90%"
                    AutoGenerateColumns="false"
                    EnableHeaderContextMenu="true"
                    AllowSorting="true"
                    AllowMultiRowSelection="false" HeaderStyle-Font-Bold="true"
                    OnNeedDataSource="rgComparativos_NeedDataSource"
                    OnDeleteCommand="rgComparativos_DeleteCommand"
                    OnItemDataBound="rgComparativos_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="idPeriodo" ClientDataKeyNames="idPeriodo" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="40" HeaderText="Clave" DataField="clPeriodo" UniqueName="clPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Periodo" DataField="nbPeriodo" UniqueName="nbPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Descripción" DataField="dsPeriodo" UniqueName="dsPeriodo" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" ButtonType="ImageButton" HeaderStyle-Width="30" />
                            
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="height: 10px; clear: both;"></div>
                <div class="divControlIzquierda">
                    <telerik:RadButton ID="btnSeleccionar" runat="server" AutoPostBack="false" Width="200" Text="Seleccionar periodos" OnClientClicked="OpenWindowPeriodos"></telerik:RadButton>
                </div>
                <div class="divControlDerecha">
                    <telerik:RadButton ID="btnComparar" runat="server" AutoPostBack="false" Width="100" Text="Comparar" OnClientClicked="OpenWindowComparar"></telerik:RadButton>
                </div>


                            </div>
                          </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </telerik:RadPane>
        <telerik:RadPane ID="rpAyuda" runat="server" Width="20px" Height="90%">
            <telerik:RadSlidingZone ID="rszAyuda" runat="server" SlideDirection="Left" Height="100%" ExpandedPaneId="rsConsultas" Width="20px" DockedPaneId="rsbConsultas">
                <telerik:RadSlidingPane ID="rsbAyuda" runat="server" CollapseMode="Forward" EnableResize="false" Width="325px" Title="Ayuda" Height="100%">
                    <div id="divTabuladorMaestro" runat="server">
                        <p>
                            Bono																	
                        </p>
                    </div>
                </telerik:RadSlidingPane>
               </telerik:RadSlidingZone>
            </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>

