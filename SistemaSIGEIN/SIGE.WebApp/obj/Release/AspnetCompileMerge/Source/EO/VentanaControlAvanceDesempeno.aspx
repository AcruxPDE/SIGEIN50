<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="VentanaControlAvanceDesempeno.aspx.cs" Inherits="SIGE.WebApp.EO.VentanaControlAvanceDesempeno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpControlAvanceDesempeno" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramControlAvanceDesempeno" runat="server" DefaultLoadingPanelID="ralpControlAvanceDesempeno">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnGuardarPonderacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdControlAvance" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 60px);">
        <div style="clear: both; height: 10px;"></div>
        <telerik:RadTabStrip ID="rtsControlAvance" runat="server" SelectedIndex="0" MultiPageID="rmpControlAvance">
            <Tabs>
                <telerik:RadTab Text="Contexto"></telerik:RadTab>
                <telerik:RadTab Text="Reporte"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="rmpControlAvance" runat="server" SelectedIndex="0" Height="100%">
            <telerik:RadPageView ID="rpvControlAvanceCon" runat="server">
                <div style="height: calc(100% - 20px);">
                    <div class="ctrlBasico">
                        <table class="ctrlTableForm">
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Período:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtClPeriodo" runat="server"></div>
                                </td>
                            </tr>
                           <%-- <tr>
                                <td class="ctrlTableDataContext">
                                    <label>Nombre del periodo:</label></td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtNbPeriodo" runat="server"></div>
                                </td>
                            </tr>--%>
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
                                    <label>Tipo de periodo:</label></td>
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
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvControlAvanceRe" runat="server">
                <div style="height: calc(100% - 20px);">
                    <%--  <div class="ctrlBasico">
                        <table class="ctrlTableForm">
                            <tr>
                                <td class="ctrlTableDataContext">
                                    <label id="lbTabulador" name="lbTabulador" runat="server">Periodo:</label>
                                </td>
                                <td colspan="2" class="ctrlTableDataBorderContext">
                                    <div id="txtNoPeriodo" runat="server"></div>
                                </td>
                                <td class="ctrlTableDataBorderContext">
                                    <div id="txtNbPeriodo" runat="server"></div>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <div style="clear: both; height: 10px;"></div>
                    <div style="height: calc(100% - 60px);">
                        <telerik:RadGrid ID="grdControlAvance" HeaderStyle-Font-Bold="true" ShowFooter="true" runat="server" Height="100%" Width="100%" AutoGenerateColumns="false" AllowSorting="true" OnNeedDataSource="grdControlAvance_NeedDataSource" OnItemDataBound="grdControlAvance_ItemDataBound">
                            <ClientSettings>
                                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView DataKeyNames="ID_PERIODO,ID_EVALUADO" ClientDataKeyNames="ID_PERIODO,ID_EVALUADO" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="No. de Empleado" AutoPostBackOnFilter="true" DataField="CL_EVALUADO" UniqueName="CL_EVALUADO" FilterControlWidth="40" HeaderStyle-Width="100" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Nombre completo" AutoPostBackOnFilter="true" DataField="NB_EVALUADO" UniqueName="NB_EVALUADO" HeaderStyle-Width="200" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Puesto" AutoPostBackOnFilter="true" DataField="NB_PUESTO" UniqueName="NB_PUESTO" FilterControlWidth="170" HeaderStyle-Width="250" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Área" AutoPostBackOnFilter="true" FilterControlWidth="170" HeaderStyle-Width="250" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Metas definidas" DataField="FG_METAS_DEFINIDAS" UniqueName="FG_METAS_DEFINIDAS" AllowFiltering="false" HeaderStyle-Font-Bold="true">
                                        <HeaderStyle Width="80" />
                                        <ItemTemplate>
                                            <div style="width: 80%; text-align: center;">
                                                <img src='<%# Eval("FG_METAS_DEFINIDAS").ToString().Equals("1") ? "/Assets/images/Aceptar.png" : "/Assets/images/Cancelar.png"  %>' />
                                            </div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Metas capturadas" DataField="FG_METAS_CAPTURADAS" UniqueName="FG_METAS_CAPTURADAS" AllowFiltering="false" HeaderStyle-Font-Bold="true">
                                        <HeaderStyle Width="80" />
                                        <ItemTemplate>
                                            <div style="width: 80%; text-align: center;">
                                                <img src='<%# Eval("FG_METAS_CAPTURADAS").ToString().Equals("1") ? "/Assets/images/Aceptar.png" : "/Assets/images/Cancelar.png"  %>' />
                                            </div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridDateTimeColumn HeaderText="Fecha de captura" ItemStyle-HorizontalAlign="Center" FilterControlWidth="100" HeaderStyle-Width="130" DataField="FE_CAPTURA_METAS" UniqueName="FE_CAPTURA_METAS" AutoPostBackOnFilter="true" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true"></telerik:GridDateTimeColumn>
                                    <telerik:GridTemplateColumn HeaderText="Ponderación" DataField="PR_EVALUADO" AutoPostBackOnFilter="true" UniqueName="PR_EVALUADO" FilterControlWidth="40" Aggregate="Sum" FooterAggregateFormatString="Total: {0:N2}%" HeaderStyle-Font-Bold="true">
                                        <HeaderStyle Width="120" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txtPorcentajeEvaluado" Width="100" Value='<%# decimal.Parse(Eval("PR_EVALUADO").ToString()) %>'></telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <div style="clear: both; height: 20px;"></div>
                    <div class="divControlDerecha">
                        <telerik:RadButton runat="server" ID="btnGuardarPonderacion" Text="Guardar ponderación" AutoPostBack="true" OnClick="btnGuardarPonderacion_Click"></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server"></telerik:RadWindowManager>
</asp:Content>
