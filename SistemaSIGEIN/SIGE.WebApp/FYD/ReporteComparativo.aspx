<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="ReporteComparativo.aspx.cs" Inherits="SIGE.WebApp.FYD.ReporteComparativo" %>

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
    <script type="text/javascript">
        function OpenWindow(pWindowProperties) {
            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }

        function OpenImpresion() {
            var myWindow = window.open("ReporteGlobalComparativoImpresion.aspx?IdReporteComparativo=" + '<%= vIdReporteComparativo %>' + "&Fgfoto=" + '<%= vFgFoto %>', "MsgWindow", "width=650,height=650");
         }

        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 20,
                height: document.documentElement.clientHeight - 20
            };
        }

        function OpenSelectionWindow(pURL, pIdWindow, pTitle) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = GetWindowProperties();

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }

        function OpenReporteIndividual(pIdEmpleado) {
            var vIdReporteComparativo = '<%= vIdReporteComparativo %>';
            OpenSelectionWindow("ReporteIndividual.aspx?IdReporteComparativo=" + vIdReporteComparativo + "&IdEmpleado=" + pIdEmpleado + "&ClTipoReporte=COMPARATIVO", "rwReporteComparativo", "Reporte Individual");
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="clear: both; height: 10px;"></div>
    <%-- <div class="ctrlBasico">
        <label>Periodo: </label>
    </div>
    <div class="ctrlBasico">
        <telerik:RadTextBox runat="server" ID="txtClavePeriodo" Width="50px" Enabled="false"></telerik:RadTextBox>
    </div>
    <div class="ctrlBasico">
        <telerik:RadTextBox runat="server" ID="txtNombrePeriodo" Width="300px" Enabled="false"></telerik:RadTextBox>
    </div>
    <div class="ctrlBasico">
        <telerik:RadTextBox runat="server" ID="txtTiposEvaluacion" Width="500px" Enabled="false"></telerik:RadTextBox>
    </div>--%>
    <telerik:RadTabStrip runat="server" ID="rtsReportes" MultiPageID="rmpReportes" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Contexto"></telerik:RadTab>
            <telerik:RadTab runat="server" Text="Reporte comparativo"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="overflow: auto; height: calc(100% - 80px); width: 100%;">
        <telerik:RadMultiPage runat="server" ID="rmpReportes" SelectedIndex="0" Height="100%" Width="100%">
            <telerik:RadPageView ID="rpvContexto" runat="server">
                <div id="dvContexto" runat="server"></div>
                <%--<div class="ctrlBasico">
        <table class="ctrlTableForm">
            <tr>
                <td class="ctrlTableDataContext">
                    <label>Periodo:</label></td>
                <td colspan="2" class="ctrlTableDataBorderContext">
                    <div id="txtClavePeriodo" runat="server" style="min-width: 100px;"></div>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtNombrePeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                </td>
                <td class="ctrlTableDataBorderContext">
                    <div id="txtTiposEvaluacion" runat="server" width="170" maxlength="1000" enabled="false"></div>
                </td>
            </tr>
        </table>
    </div>--%>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvReporteGlobal" runat="server">
                <div style="height: calc(100% - 10px); overflow: auto;">
                        <telerik:RadSplitter ID="splHelp" runat="server" Width="100%" Height="100%" BorderSize="0">
        <telerik:RadPane ID="radPanelProgramaCapacitacion" runat="server" Height="100%">

                    <telerik:RadGrid runat="server" ID="rdComparativo" HeaderStyle-Font-Bold="true" OnNeedDataSource="rdComparativo_NeedDataSource" OnItemDataBound="rdComparativo_ItemDataBound" Height="100%">
                        <ClientSettings AllowKeyboardNavigation="true">
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />

                        <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" ShowFooter="true" EnableHierarchyExpandAll="true" DataKeyNames="ID_EMPLEADO" ClientDataKeyNames="ID_EVALUADO">
                            <ColumnGroups>
                                <telerik:GridColumnGroup Name="gcCalificacion" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" HeaderText="Calificación"></telerik:GridColumnGroup>
                            </ColumnGroups>
                            <Columns>
                                <telerik:GridBinaryImageColumn UniqueName="FI_ARCHIVO" DataField="FI_ARCHIVO" HeaderText="Fotografía" ImageHeight="150px" ImageWidth="100px" ResizeMode="Fit" AllowFiltering="false">
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="120px" Font-Bold="true" />
                                </telerik:GridBinaryImageColumn>
                                <telerik:GridBoundColumn UniqueName="CL_EVALUADO" DataField="CL_EVALUADO" HeaderText="No. de Empleado" FilterControlWidth="35%" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <ItemStyle Width="100px" />
                                    <HeaderStyle Width="100px" Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridHyperLinkColumn UniqueName="HL_EBVALUADO" HeaderText="Nombre completo" DataTextField="NB_EVALUADO" FilterControlWidth="60%" DataNavigateUrlFields="ID_EMPLEADO" DataNavigateUrlFormatString="javascript:OpenReporteIndividual({0})" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle Font-Bold="true" Width="250" />
                                </telerik:GridHyperLinkColumn>
                                <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" FooterText="Total:" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <ItemStyle Width="250px" />
                                    <HeaderStyle Width="250px" Font-Bold="true" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <%-- <telerik:GridTemplateColumn UniqueName="PR_CUMPLIMIENTO" HeaderStyle-Width="800" HeaderText="Calificación" FooterStyle-HorizontalAlign="Center" FooterStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false">
                                <ItemTemplate>
                                    <div id="dvTabla" style="width:700px;" runat="server"></div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <%-- <telerik:GridTemplateColumn UniqueName="PR_CUMPLIMIENTOT" DataField="PR_CUMPLIMIENTO" HeaderText="% de compatibilidad con su puesto" Aggregate="Avg" FooterText="Total: " FooterAggregateFormatString="{0:N2}" CurrentFilterFunction="Contains" FilterControlWidth="60%">
                        <ItemStyle Width="150px" HorizontalAlign="Right" />
                        <HeaderStyle Width="150px" Font-Bold="true" />
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <div style="text-align: right; width: 80%; float: left;">
                                <%# Eval("PR_CUMPLIMIENTO") %>%
                            </div>

                            <div style="height: 80%; width: 15px; float: right; border-radius: 5px; background: <%# Eval("CL_COLOR_CUMPLIMIENTO") %>;">
                                <br />
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
                            <%-- <DetailTables>
                    <telerik:GridTableView Name="gtvComparativo" AutoGenerateColumns="false" NoDetailRecordsText="No existe este empleado en los periodos seleccionados.">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="ID_PERIODO" DataField="ID_PERIODO" HeaderText="Periodo">
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                <HeaderStyle Width="100px" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DS_PERIODO" DataField="DS_PERIODO" HeaderText="Descripción">
                                <HeaderStyle Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FE_INICIO" DataField="FE_INICIO" HeaderText="F. Inicial" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="130px" />
                                <HeaderStyle Width="130px" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FE_TERMINO" DataField="FE_TERMINO" HeaderText="F. Termino" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="130px" />
                                <HeaderStyle Width="130px" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto">
                                <ItemStyle Width="200px" />
                                <HeaderStyle Width="200px" Font-Bold="true"  />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="PR_CUMPLIMIENTOT" DataField="PR_CUMPLIMIENTO" HeaderText="% de compatibilidad">
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                <HeaderStyle Width="100px" Font-Bold="true" />
                                <ItemTemplate>
                                    <div style="text-align: right; width: 80%; float: left;">
                                        <%# Eval("PR_CUMPLIMIENTO") %>%
                                    </div>
                                    <div style="height: 80%; width: 15px; float: right; border-radius: 5px; background: <%# Eval("CL_COLOR_CUMPLIMIENTO") %>;">
                                        <br />
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="PR_DIFERENCIA" DataField="PR_DIFERENCIA" HeaderText="% de diferencia" DataFormatString="{0}">
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                <HeaderStyle Width="100px" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="IM_ICONO" DataField="CL_DIFERENCIA">
                                <ItemTemplate>
                                    <img src='/Assets/images/Icons/25/<%# Eval("CL_DIFERENCIA") %>.png' alt="N/A" />
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                                <HeaderStyle Width="50px" Font-Bold="true" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>--%>
                        </MasterTableView>
                    </telerik:RadGrid>
                                  </telerik:RadPane>
                    <%--        <telerik:RadGrid runat="server" ID="rdComparativo" HeaderStyle-Font-Bold="true" OnNeedDataSource="rdComparativo_NeedDataSource" Height="100%" Width="100%" OnDetailTableDataBind="rdComparativo_DetailTableDataBind">
            <ClientSettings AllowKeyboardNavigation="true">
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" ShowFooter="true" EnableHierarchyExpandAll="true" DataKeyNames="ID_EVALUADO, ID_EMPLEADO, ID_PUESTO_EVALUADO_PERIODO, ID_PUESTO, PR_CUMPLIMIENTO">
                <Columns>
                    <telerik:GridBinaryImageColumn UniqueName="FI_FOTO" DataField="FI_FOTOGRAFIA" HeaderText="Fotografia" ImageHeight="200px" ImageWidth="150px" ResizeMode="Fit" AllowFiltering="false">
                        <ItemStyle Width="150px" />
                        <HeaderStyle Width="150px" Font-Bold="true" />
                    </telerik:GridBinaryImageColumn>
                    <telerik:GridBoundColumn UniqueName="CL_EVALUADO" DataField="CL_EVALUADO" HeaderText="Clave" FilterControlWidth="35%" CurrentFilterFunction="Contains">
                        <ItemStyle Width="100px" />
                        <HeaderStyle Width="100px" Font-Bold="true" />
                    </telerik:GridBoundColumn>
                    <telerik:GridHyperLinkColumn UniqueName="HL_EBVALUADO" HeaderText="Nombre" DataTextField="NB_EVALUADO" DataNavigateUrlFields="ID_EVALUADO" DataNavigateUrlFormatString="javascript:OpenReporteIndividual({0})" FilterControlWidth="60%">
                        <HeaderStyle Font-Bold="true" />
                    </telerik:GridHyperLinkColumn>
                    <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto" CurrentFilterFunction="Contains">
                        <ItemStyle Width="300px" />
                        <HeaderStyle Width="300px" Font-Bold="true" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="PR_CUMPLIMIENTOT" DataField="PR_CUMPLIMIENTO" HeaderText="% de compatibilidad con su puesto" Aggregate="Avg" FooterText="Total: " FooterAggregateFormatString="{0:N2}" CurrentFilterFunction="Contains" FilterControlWidth="60%">
                        <ItemStyle Width="150px" HorizontalAlign="Right" />
                        <HeaderStyle Width="150px" Font-Bold="true" />
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <div style="text-align: right; width: 80%; float: left;">
                                <%# Eval("PR_CUMPLIMIENTO") %>%
                            </div>

                            <div style="height: 80%; width: 15px; float: right; border-radius: 5px; background: <%# Eval("CL_COLOR_CUMPLIMIENTO") %>;">
                                <br />
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <DetailTables>
                    <telerik:GridTableView Name="gtvComparativo" AutoGenerateColumns="false" NoDetailRecordsText="No existe este empleado en los periodos seleccionados.">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="ID_PERIODO" DataField="ID_PERIODO" HeaderText="Periodo">
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                <HeaderStyle Width="100px" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DS_PERIODO" DataField="DS_PERIODO" HeaderText="Descripción">
                                <HeaderStyle Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FE_INICIO" DataField="FE_INICIO" HeaderText="F. Inicial" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="130px" />
                                <HeaderStyle Width="130px" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FE_TERMINO" DataField="FE_TERMINO" HeaderText="F. Termino" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="130px" />
                                <HeaderStyle Width="130px" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto">
                                <ItemStyle Width="200px" />
                                <HeaderStyle Width="200px" Font-Bold="true"  />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="PR_CUMPLIMIENTOT" DataField="PR_CUMPLIMIENTO" HeaderText="% de compatibilidad">
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                <HeaderStyle Width="100px" Font-Bold="true" />
                                <ItemTemplate>
                                    <div style="text-align: right; width: 80%; float: left;">
                                        <%# Eval("PR_CUMPLIMIENTO") %>%
                                    </div>
                                    <div style="height: 80%; width: 15px; float: right; border-radius: 5px; background: <%# Eval("CL_COLOR_CUMPLIMIENTO") %>;">
                                        <br />
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="PR_DIFERENCIA" DataField="PR_DIFERENCIA" HeaderText="% de diferencia" DataFormatString="{0}">
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                <HeaderStyle Width="100px" Font-Bold="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="IM_ICONO" DataField="CL_DIFERENCIA">
                                <ItemTemplate>
                                    <img src='/Assets/images/Icons/25/<%# Eval("CL_DIFERENCIA") %>.png' alt="N/A" />
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                                <HeaderStyle Width="50px" Font-Bold="true" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>
            </MasterTableView>
        </telerik:RadGrid>--%>

 <%--                 <div style="clear: both; height: 5px;"></div>
                <div class="divControlDerecha">
                    <div class="ctrlBasico" style="padding-left: 5px;">
                        <telerik:RadButton runat="server" ID="btnImprimir" Text="Imprimir" AutoPostBack="false" OnClientClicked="OpenImpresion"></telerik:RadButton>
                    </div>
                </div>--%>


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

                  </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <label runat="server" id="lblAdvertencia" style="color: red;">* Alguno de los períodos que aparecen aún no ha sido cerrado por lo que alguna de las calificaciones podrían ser parciales</label>
</asp:Content>
