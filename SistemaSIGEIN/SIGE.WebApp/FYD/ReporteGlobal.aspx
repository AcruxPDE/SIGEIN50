<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="ReporteGlobal.aspx.cs" Inherits="SIGE.WebApp.FYD.ReporteGlobal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <style>
        .divRojo {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: red;
        }

        .divAmarillo {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: gold;
        }

        .divVerde {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: green;
        }

        .divNa {
            height: 80%;
            width: 15px;
            border-radius: 5px;
            background: gray;
        }

        table.tablaColor {
            width: 100%;
        }

        td.porcentaje {
            width: 80%;
            padding: 1px;
        }

        td.puesto {
            width: 75%;
            padding: 1px;
            font-weight: bold;
        }

        td.color {
            width: 10%;
            padding: 1px 1px 1px 1px;
        }
    </style>
    <script>
        function OpenInventario(pIdEmpleado) {
            var vURL = "../Administracion/Empleado.aspx";
            var vTitulo = "Editar Empleado";
            vURL = vURL + "?EmpleadoId=" + pIdEmpleado;

            var windowProperties = {};
            windowProperties.width = document.documentElement.clientWidth - 20;
            windowProperties.height = document.documentElement.clientHeight - 20;
            openChildDialog(vURL, "winEmpleado", vTitulo, windowProperties);
        }


        function OpenImpresion() {
            var myWindow = window.open("ReporteGlobalImpresion.aspx?IdReporteGlobal=" + '<%= vIdReporteGlobal %>' + "&Fgfoto=" + '<%= vFgFoto %>' + "&FgGrid=" + '<%= vFgGrid %>', "MsgWindow", "width=650,height=650");
        }

        function OpenDescriptivo(pIdPuesto) {
            var vURL = "../Administracion/VentanaVistaDescriptivo.aspx";
            var vTitulo = "Ver descripción del puesto";

            vURL = vURL + "?PuestoId=" + pIdPuesto

            var windowProperties = {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
            openChildDialog(vURL, "winDescriptivo", vTitulo, windowProperties);
        }

        function OpenReporteIndividual(pIdEvaluado) {
            var vIdPeriodo = '<%= vIdPeriodo %>';
            var vURL = "../FYD/ReporteIndividual.aspx";
            var vTitulo = "Reporte Individual";

            vURL = vURL + "?IdPeriodo=" + vIdPeriodo
            vURL = vURL + "&IdEvaluado=" + pIdEvaluado

            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            openChildDialog(vURL, "rwReporteComparativo", vTitulo, windowProperties);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="clear: both; height: 10px;"></div>
    <telerik:RadTabStrip runat="server" ID="rtsReportes" MultiPageID="rmpReportes" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Contexto"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Reporte Global"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="overflow: auto; height: calc(100% - 80px); width: 100%;">
        <telerik:RadMultiPage runat="server" ID="rmpReportes" SelectedIndex="0" Height="100%" Width="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">
                <div class="ctrlBasico">
                    <table class="ctrlTableForm ctrlTableContext">
                        <tr>
                            <td>
                                <label>Periodo:</label>
                            </td>
                            <td>
                                <span id="txtClave" runat="server" style="width: 300px;"></span>
                            </td>
                            <%-- <td>
                                <span id="txtPeriodo" runat="server" style="width: 300px;"></span>
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripción:</label>
                            </td>
                            <td colspan="2">
                                <span id="txtDescripcion" runat="server" style="width: 300px;"></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Tipo de evaluación:</label></td>
                            <td colspan="2">
                                <span id="txtTiposEvaluacion" runat="server" ></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="clear: both;"></div>
                <div>
                    <table class="ctrlTableForm ctrlTableContext">
                        <tr>
                            <td>
                                <label>Notas:</label></td>
                            <td colspan="2">
                                <div id="txtDsNotas" runat="server" style="min-width: 100px;"></div>
                            </td>
                        </tr>
                    </table>
                </div>
                <label runat="server" id="lblAdvertencia" style="color: red;">* El periodo aún no ha sido cerrado por lo que las calificaciones podrían ser parciales</label>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvReporteGlobal" runat="server">

                      <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="radPanelProgramaCapacitacion" runat="server" Height="100%">

                <%--   <div style="height: calc(100% - 50px);">--%>
                <telerik:RadGrid runat="server" ID="rdgGlobal" ShowFooter="true" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" OnItemDataBound="rdgGlobal_ItemDataBound" OnNeedDataSource="rdgGlobal_NeedDataSource" Height="100%">
                    <ClientSettings EnablePostBackOnRowClick="false" >
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="ID_EMPLEADO" DataKeyNames="ID_EMPLEADO" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridBinaryImageColumn UniqueName="FI_ARCHIVO" DataField="FI_ARCHIVO" Visible="false" HeaderText="Fotografía" ImageHeight="150px" ImageWidth="100px" ResizeMode="Fit" AllowFiltering="false">
                                <ItemStyle Width="120px" HorizontalAlign="Center" />
                                <HeaderStyle Width="120px" Font-Bold="true" />
                            </telerik:GridBinaryImageColumn>
                            <telerik:GridBoundColumn UniqueName="CL_EMPLEADO" DataField="CL_EMPLEADO" AutoPostBackOnFilter="true" HeaderText="No. de Empleado" FilterControlWidth="30" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <HeaderStyle Width="100px" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn UniqueName="NB_EMPLEADO_COMPLETO" HeaderText="Nombre completo" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" DataTextField="NB_EMPLEADO_COMPLETO" DataNavigateUrlFields="ID_EVALUADO" DataNavigateUrlFormatString="javascript:OpenReporteIndividual({0})" FilterControlWidth="180">
                                <HeaderStyle Width="250px" Font-Bold="true" />
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn UniqueName="NB_PUESTO" HeaderText="Puesto" DataTextField="NB_PUESTO" AutoPostBackOnFilter="true" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" DataNavigateUrlFields="ID_PUESTO" DataNavigateUrlFormatString="javascript:OpenDescriptivo({0})" FilterControlWidth="130">
                                <ItemStyle Width="250" />
                                <HeaderStyle Width="250" Font-Bold="true" />
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridTemplateColumn UniqueName="PR_CUMPLIMIENTO" HeaderText="Calificación" FooterStyle-HorizontalAlign="Center" FooterStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false">
                                <ItemTemplate>
                                    <div id="dvTabla" runat="server"></div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%-- <telerik:GridTemplateColumn UniqueName="PR_CUMPLIMIENTO" DataField="PR_CUMPLIMIENTO" HeaderText="% de compatiblidad con su puesto" Aggregate="Avg" FooterText="Total: " FooterAggregateFormatString="{0:N2}" FooterStyle-HorizontalAlign="Right" FilterControlWidth="80">
                                <ItemStyle Width="150px" />
                                <HeaderStyle Width="150px" Font-Bold="true" />
                                <ItemTemplate>
                                    <div style="text-align: right; width: 80%; float: left;">
                                        <a href="javascript:OpenReporteIndividual(<%# Eval("ID_EVALUADO") %>)"><%# Eval("PR_CUMPLIMIENTO") %>% </a>
                                    </div>
                                    <div style="height: 80%; width: 15px; float: right; border-radius: 5px; background: <%# Eval("CL_COLOR_CUMPLIMIENTO") %>;">
                                        <br />
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <%--   </div>
                 <div style="clear: both; height: 5px;"></div>
                <div class="divControlDerecha">
                    
                    <div class="ctrlBasico" style="padding-left: 5px;">
                        <telerik:RadButton runat="server" ID="btnImprimir" Text="Imprimir" AutoPostBack="false" OnClientClicked="OpenImpresion"></telerik:RadButton>
                    </div>
                </div>--%>
                <%--  <div id="dvReporteGeneral" runat="server"></div>--%>

              </telerik:RadPane>
        <telerik:RadPane ID="rpnOpciones" runat="server" Height="50px" Width="22px" Scrolling="None">
            <telerik:RadSlidingZone ID="slzOpciones" runat="server" SlideDirection="Left" ExpandedPaneId="AyudaPrograma" ClickToOpen="true" Width="30px">
                   <telerik:RadSlidingPane ID="rspSemaforo" runat="server" CollapseMode="Forward" EnableResize="false" Width="300px" Title="Código de color" Height="100%">
                                    <div style="padding: 10px; text-align: justify;">
                                        <telerik:RadGrid ID="grdCodigoColores"
                                            runat="server"
                                            Height="300"
                                            Width="250"
                                            AllowSorting="true"
                                            AllowFilteringByColumn="true"
                                            HeaderStyle-Font-Bold="true"
                                            ShowHeader="true"
                                            OnNeedDataSource="grdCodigoColores_NeedDataSource">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                                            </ClientSettings>
                                            <PagerStyle AlwaysVisible="true" />
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView AutoGenerateColumns="false" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true">
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="True" ShowExportToCsvButton="false" ShowRefreshButton="false"
                                                    AddNewRecordText="Insertar" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Color" HeaderStyle-Width="60" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <div style="margin: auto; width: 25px; border: 1px solid gray; background: <%# Eval("COLOR")%>; border-radius: 5px;">&nbsp;&nbsp;</div>
                                                            &nbsp;
                                                        </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" HeaderStyle-Width="260" HeaderText="Descripción" DataField="DESCRIPCION"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
                          </telerik:RadSplitter>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" Animation="Fade"></telerik:RadWindowManager>
</asp:Content>
