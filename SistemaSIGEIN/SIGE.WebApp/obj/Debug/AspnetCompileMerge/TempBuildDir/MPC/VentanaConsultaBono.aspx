<%@ Page Title="" Language="C#" MasterPageFile="~/MPC/ContextMC.master" AutoEventWireup="true" CodeBehind="VentanaConsultaBono.aspx.cs" Inherits="SIGE.WebApp.MPC.VentanaConsultaBono" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">
        function CloseWindow() {
            GetRadWindow().close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <telerik:RadTabStrip ID="rtsReporteBono" runat="server" SelectedIndex="0" MultiPageID="rmpReporteBono">
        <Tabs>
            <telerik:RadTab SelectedIndex="0" Text="Contexto"></telerik:RadTab>
            <telerik:RadTab SelectedIndex="1" Text="Bonos"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <div style="height: 10px;"></div>
    <div style="height: calc(100% - 60px);">
        <telerik:RadMultiPage ID="rmpReporteBono" runat="server" SelectedIndex="0" Height="90%">
            <telerik:RadPageView ID="rpvContexto" runat="server">
                <div style="height: 10px;"></div>
                <telerik:RadGrid
                    ID="rgContexto"
                    runat="server"
                    Height="90%"
                    Width="100%"
                    AutoGenerateColumns="false"
                    EnableHeaderContextMenu="false"
                    AllowSorting="false" HeaderStyle-Font-Bold="true"
                    AllowMultiRowSelection="false"
                    OnNeedDataSource="rgContexto_NeedDataSource">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="false" AllowScroll="true" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView DataKeyNames="ID_PERIODO" ClientDataKeyNames="ID_PERIODO" AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <ColumnGroups>
                            <telerik:GridColumnGroup Name="Fechas" HeaderText="Duración" HeaderStyle-Font-Bold="true"
                                HeaderStyle-HorizontalAlign="Center" />
                        </ColumnGroups>
                        <Columns>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="180" FilterControlWidth="120" HeaderText="Periodo" DataField="CL_TIPO_PERIODO" UniqueName="CL_TIPO_PERIODO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Descripción" DataField="DS_PERIODO" UniqueName="DS_PERIODO" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="250" FilterControlWidth="180" HeaderText="Notas" DataField="XML_DS_NOTAS" UniqueName="XML_DS_NOTAS" HeaderStyle-Font-Bold="true"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="100" ColumnGroupName="Fechas" HeaderText="De" DataField="FE_INICIO_PERIODO" UniqueName="FE_INICIO_PERIODO" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"></telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" Visible="true" Display="true" HeaderStyle-Width="150" FilterControlWidth="100" ColumnGroupName="Fechas" HeaderText="a" DataField="FE_TERMINO_PERIODO" UniqueName="FE_TERMINO_PERIODO" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"></telerik:GridDateTimeColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPageView>
            <telerik:RadPageView ID="rpvBonos" runat="server">
                <div style="height: 100%">
                    <telerik:RadGrid
                        ID="rgBonosComparativos"
                        runat="server"
                        Height="100%"
                        AutoGenerateColumns="false"
                        EnableHeaderContextMenu="false"
                        AllowSorting="false"
                        AllowMultiRowSelection="false"
                        HeaderStyle-Font-Bold="true"
                        OnNeedDataSource="rgBonosComparativos_NeedDataSource"
                        ShowFooter="true">
                        <ClientSettings>
                            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowPaging="false" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                            <ColumnGroups>
                                <telerik:GridColumnGroup Name="Desempeno" HeaderText="% Desempeño y bono" HeaderStyle-Font-Bold="true"
                                    HeaderStyle-HorizontalAlign="Center" />
                            </ColumnGroups>
                            <Columns>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <div style="height: 10px; clear: both;"></div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="btnCancelar" runat="server" AutoPostBack="false" Width="100" Text="Cancelar" OnClientClicked="CloseWindow"></telerik:RadButton>
        </div>
    </div>
</asp:Content>
