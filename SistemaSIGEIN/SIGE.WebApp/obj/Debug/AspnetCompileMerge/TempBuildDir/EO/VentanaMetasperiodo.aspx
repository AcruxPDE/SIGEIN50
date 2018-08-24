<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaMetasperiodo.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaMetasperiodo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <%--      <telerik:RadTabStrip ID="rtsMetasPeriodoDesempeno" runat="server" SelectedIndex="0" MultiPageID="rmpMetas">
        <Tabs>
            <telerik:RadTab Text="Contexto"></telerik:RadTab>
            <telerik:RadTab Text="Metas del período"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>--%>
    <div style="height: calc(100% - 20px); width: 100%;">
        <%-- <telerik:RadMultiPage ID="rmpMetas" runat="server" SelectedIndex="0" Height="100%">--%>
        <%-- <telerik:RadPageView ID="rpvMetas" runat="server" Width="100%">
                <div class="divControlIzquierda" style="width: 60%; text-align: left;">
                    <table class="ctrlTableForm">
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Período:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtClPeriodo" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Descripción:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtPeriodos" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Notas:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtNotas" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Fecha:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtFechas" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de período:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTipoPeriodo" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de bono:</label></td>
                            <td class="ctrlTableDataBorderContext">
                                <div id="txtTipoBono" runat="server" width="170" maxlength="1000" enabled="false"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de metas:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtTipoMetas" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="ctrlTableDataContext">
                                <label>Tipo de capturista:</label></td>
                            <td colspan="2" class="ctrlTableDataBorderContext">
                                <div id="txtTipoCapturista" runat="server"></div>
                            </td>
                        </tr>
                    </table>
                </div>
            </telerik:RadPageView>--%>
        <%-- <telerik:RadPageView ID="rpvDisenoMetas" runat="server">
                <telerik:RadSplitter runat="server" ID="spHelp1" Width="100%" Height="100%" BorderSize="0">
                    <telerik:RadPane ID="rpHelp1" runat="server">--%>
        <telerik:RadGrid ID="grdMetas" runat="server"
            Height="100%" AutoGenerateColumns="false" EnableHierarchyExpandAll="true" MasterTableView-HierarchyDefaultExpanded="true"
            EnableHeaderContextMenu="true" AllowSorting="true"
            AllowMultiRowSelection="false"
            OnNeedDataSource="grdMetas_NeedDataSource"
            OnDetailTableDataBind="grdMetas_DetailTableDataBind"
            OnItemDataBound="grdMetas_ItemDataBound"
            OnItemCommand="grdMetas_ItemCommand" HeaderStyle-Font-Bold="true">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" SaveScrollPosition="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="false" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView DataKeyNames="ID_EMPLEADO,ID_EVALUADO" ClientDataKeyNames="ID_EVALUADO" AllowPaging="true"
                AllowFilteringByColumn="true" AllowSorting="true" Name="Evaluados"
                ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="100" FilterControlWidth="30" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="300" FilterControlWidth="230" HeaderText="Nombre completo" DataField="NB_EMPLEADO_COMPLETO" UniqueName="NB_EMPLEADO_COMPLETO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="80" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                </Columns>
                <DetailTables>
                    <telerik:GridTableView DataKeyNames="ID_EVALUADO_META,ID_EVALUADO,NO_META, FG_EVALUAR" ClientDataKeyNames="ID_EVALUADO_META,ID_EVALUADO,NO_META, FG_EVALUAR" Name="gtvMetas"
                        NoDetailRecordsText="No se han asignado metas en el descriptivo de puestos" ShowFooter="true">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Estatus" DataField="FG_EVALUAR" UniqueName="FG_EVALUAR" AllowFiltering="false" HeaderStyle-Font-Bold="true">
                                <HeaderStyle Width="50" />
                                <ItemTemplate>
                                    <div style="width: 80%; text-align: center;">
                                        <img src='<%# Eval("FG_EVALUAR").ToString().Equals("True") ? "../Assets/images/Aceptar.png" : "../Assets/images/Cancelar.png"  %>' />
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="NB_INDICADOR" DataField="NB_INDICADOR" HeaderText="Indicador" HeaderStyle-Font-Bold="true">
                                <HeaderStyle Width="150" />
                                <ItemStyle Width="150" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DS_META" DataField="DS_META" HeaderText="Meta" HeaderStyle-Font-Bold="true">
                                <HeaderStyle Width="120" />
                                <ItemStyle Width="120" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CL_TIPO_META" DataField="CL_TIPO_META" HeaderText="Tipo de meta" HeaderStyle-Font-Bold="true">
                                <HeaderStyle Width="80" />
                                <ItemStyle Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_CUMPLIMIENTO_ACTUAL" DataField="NB_CUMPLIMIENTO_ACTUAL" HeaderText="Actual" DataType="System.String" HeaderStyle-Font-Bold="true">
                                <HeaderStyle Width="80" />
                                <ItemStyle HorizontalAlign="Right" Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_CUMPLIMIENTO_MINIMO" DataField="NB_CUMPLIMIENTO_MINIMO" HeaderText="Mínimo" DataType="System.String" HeaderStyle-Font-Bold="true">
                                <HeaderStyle Width="80" />
                                <ItemStyle HorizontalAlign="Right" Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_CUMPLIMIENTO_SATISFACTORIO" DataField="NB_CUMPLIMIENTO_SATISFACTORIO" HeaderText="Satisfactorio" DataType="System.String" HeaderStyle-Font-Bold="true">
                                <HeaderStyle Width="80" />
                                <ItemStyle HorizontalAlign="Right" Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NB_CUMPLIMIENTO_SOBRESALIENTE" DataField="NB_CUMPLIMIENTO_SOBRESALIENTE" HeaderText="Sobresaliente" DataType="System.String" HeaderStyle-Font-Bold="true">
                                <HeaderStyle Width="80" />
                                <ItemStyle HorizontalAlign="Right" Width="80" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PR_META" DataField="PR_META" HeaderText="Ponderación" DataType="System.Double" HeaderStyle-Font-Bold="true">
                                <HeaderStyle Width="80" />
                                <ItemStyle HorizontalAlign="Right" Width="80" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>
            </MasterTableView>
        </telerik:RadGrid>
        <%-- </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPageView>
            </telerik:RadMultiPage>--%>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server">
    </telerik:RadWindowManager>
</asp:Content>
