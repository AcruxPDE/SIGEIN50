<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="ConsultaMercadoSalarial.aspx.cs" Inherits="SIGE.WebApp.MPC.ConsultaMercadoSalarial" %>
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

                          <div style="height: calc(100% - 60px);  overflow: auto;">
                              <telerik:RadGrid ID="rgdMercadoSalarial"
                                    runat="server"
                                    Width="100%"
                                    Height="100%"
                                    AllowSorting="true"
                                    AutoGenerateColumns="false"
                                  HeaderStyle-Font-Bold="true"
                                    OnNeedDataSource="rgdMercadoSalarial_NeedDataSource"
                                  OnItemDataBound="rgdMercadoSalarial_ItemDataBound">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <PagerStyle AlwaysVisible="true" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Área/Departamento" DataField="NB_DEPARTAMENTO" DataFormatString="{0:C}" ReadOnly="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="260" FilterControlWidth="240" HeaderText="Puesto" DataField="NB_PUESTO" DataFormatString="{0:C}" ReadOnly="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Mínimo" DataField="MN_MINIMO" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ReadOnly="true"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" HeaderStyle-Width="130" FilterControlWidth="100" HeaderText="Máximo" DataField="MN_MAXIMO" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ReadOnly="true"></telerik:GridBoundColumn>
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
                            Esta consulta te dará resultados gráficos según los datos que elijas a 																			
                             continuación.																			
                             Esta información te servirá para hacer comparaciones y analizar tu																			
                             tabulador de manera más efectiva. Si eliges más de una versión para																			
                             compararse entre sí, no podrás comparar con ningún nivel de mercado.																		
                        </p>
                    </div>
                </telerik:RadSlidingPane>
               </telerik:RadSlidingZone>
            </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>
