<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="ConsultaIncrementos.aspx.cs" Inherits="SIGE.WebApp.MPC.ConsultaIncrementos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
      <telerik:RadTabStrip ID="rtsTabuladorSueldos" runat="server" SelectedIndex="0" MultiPageID="rmpTabuladorSueldos">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Mercado Salarial"></telerik:RadTab>
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
       <div style="height: calc(100% - 10px);  overflow: auto;">
                                <telerik:RadGrid ID="rgdIncrementosPlaneados"
                                    runat="server"
                                    Width="100%"
                                    Height="100%"
                                    AllowSorting="true"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-Font-Bold="true"
                                    OnNeedDataSource="rgdIncrementosPlaneados_NeedDataSource"
                                    OnItemDataBound="rgdIncrementosPlaneados_ItemDataBound">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Nivel" DataField="NB_TABULADOR_NIVEL"></telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Cl. Puesto" DataField="CL_PUESTO"></telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="150" FilterControlWidth="70" HeaderText="Puesto" DataField="NB_PUESTO"></telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Cl. Empleado" DataField="CL_EMPLEADO"></telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Nombre completo" DataField="NB_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Sueldo nominal actual" DataField="MN_SUELDO_ORIGINAL" ReadOnly="true" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Sueldo nominal nuevo" DataField="MN_SUELDO_NUEVO" ReadOnly="true" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Incremento" DataField="INCREMENTO" ReadOnly="true" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="90" FilterControlWidth="10" HeaderText="Porcentaje" DataField="PR_INCREMENTO" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
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
                            Planeación de incrementos																		
                        </p>
                    </div>
                </telerik:RadSlidingPane>
               </telerik:RadSlidingZone>
            </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>
