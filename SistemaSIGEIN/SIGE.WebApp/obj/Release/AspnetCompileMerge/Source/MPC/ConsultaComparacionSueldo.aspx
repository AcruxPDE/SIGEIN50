<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="ConsultaComparacionSueldo.aspx.cs" Inherits="SIGE.WebApp.MPC.VentanaComparacionSueldo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
       <telerik:RadTabStrip ID="rtsTabuladorDesviaciones" runat="server" SelectedIndex="0" MultiPageID="rmpTabuladorSueldos">
        <Tabs>
            <telerik:RadTab Text="Contexto" SelectedIndex="0"></telerik:RadTab>
            <telerik:RadTab Text="Comparación sueldos"></telerik:RadTab>
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
                    <telerik:RadPageView ID="rpvCriterios" runat="server">
    
                               <div style="height: calc(100% - 10px);  overflow: auto;">

                                <telerik:RadGrid ID="rgdComparacionInventarioMercado"
                                    runat="server"
                                    Width="100%"
                                    Height="100%"
                                    AllowSorting="true"
                                    AutoGenerateColumns="false"
                                    HeaderStyle-Font-Bold="true"
                                    OnNeedDataSource="rgdComparacionInventarioMercado_NeedDataSource"
                                    OnItemDataBound="rgdComparacionInventarioMercado_ItemDataBound">
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
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Puesto" DataField="NB_PUESTO"></telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Cl. Empleado" DataField="CL_EMPLEADO"></telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="200" FilterControlWidth="120" HeaderText="Nombre completo" DataField="NB_EMPLEADO"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Sueldo nominal actual" DataField="MN_SUELDO_ORIGINAL" ReadOnly="true" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Mínimo" DataField="MN_MINIMO" DataFormatString="{0:C}" ReadOnly="true" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="110" FilterControlWidth="30" HeaderText="Máximo" DataField="MN_MAXIMO" DataFormatString="{0:C}" ReadOnly="true" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn AutoPostBackOnFilter="true" ReadOnly="true" AllowFiltering="false" HeaderStyle-Width="150" FilterControlWidth="60" HeaderText="Diferencia" DataField="DIFERENCIA_MERCADO" UniqueName="DIFERENCIA_MERCADO" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%# string.Format("{0:C}", Math.Abs((decimal) Eval("DIFERENCIA_MERCADO")))%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AutoPostBackOnFilter="true" ReadOnly="true" AllowFiltering="false" HeaderStyle-Width="150" FilterControlWidth="60" HeaderText="Porcentaje" DataField="PR_DIFERENCIA_MERCADO" UniqueName="PR_DIFERENCIA_MERCADO" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%#string.Format("{0:N2}" , Math.Abs((decimal) Eval("PR_DIFERENCIA_MERCADO")))%>%
                                        <span style="border: 1px solid gray; background: <%# Eval("COLOR_DIFERENCIA_MERCADO") %>; border-radius: 5px;">&nbsp;&nbsp;</span>&nbsp;
                                        <img src='/Assets/images/Icons/25/Arrow<%# Eval("ICONO_MERCADO")%>.png' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
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
                           Tabulador maestro																	
                        </p>
                    </div>
                </telerik:RadSlidingPane>
      <telerik:RadSlidingPane ID="rspSemaforo" runat="server" CollapseMode="Forward" EnableResize="false" Width="450px" Title="Semáforo" Height="100%">
                    <div style="padding: 10px; text-align: justify;">
                        <telerik:RadGrid ID="grdCodigoColores"
                            runat="server"
                            Height="215"
                            Width="400"
                            AllowSorting="true"
                            AllowFilteringByColumn="true"
                            HeaderStyle-Font-Bold="true"
                            ShowHeader="true"
                            OnNeedDataSource="grdCodigoColores_NeedDataSource">
                            <ClientSettings>
                                <Scrolling AllowScroll="true" UseStaticHeaders="false"></Scrolling>
                            </ClientSettings>
                            <GroupingSettings CaseSensitive="false" />
                            <PagerStyle AlwaysVisible="true" />
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
</asp:Content>
