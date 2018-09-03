<%@ Page Title="" Language="C#" MasterPageFile="~/IDP/ContextIDP.master" AutoEventWireup="true" CodeBehind="VentanaReporteResultados.aspx.cs" Inherits="SIGE.WebApp.IDP.VentanaReporteResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <script type="text/javascript">

        function CloseWindow() {
            GetRadWindow().close();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 120px);">
        <div class="ctrlBasico">
            <table class="ctrlTableForm">
                <tr>
                    <td class="ctrlTableDataContext">
                        <label>Folio de solicitud:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtFolio" runat="server"></div>
                    </td>
                    <td class="ctrlTableDataContext">
                        <label>Aplicante:</label></td>
                    <td colspan="2" class="ctrlTableDataBorderContext">
                        <div id="txtNbCandidato" runat="server"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 5px; clear: both;"></div>
        <div style="margin-left: 30px; margin-right: 30px; height: 100%">
            <telerik:RadGrid ID="grdResultadosPruebas"
                Height="100%"
                Width="100%"
                ShowHeader="true"
                runat="server"
                GridLines="None"
                AutoGenerateColumns="false"
                OnNeedDataSource="grdResultadosPruebas_NeedDataSource"
                OnDetailTableDataBind="grdResultadosPruebas_DetailTableDataBind"
                OnItemCommand="grdResultadosPruebas_ItemCommand">
                <ClientSettings>
                    <Scrolling UseStaticHeaders="false" AllowScroll="true" />
                    <Selecting AllowRowSelect="false" />
                </ClientSettings>
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView DataKeyNames="DS_FACTOR" ClientDataKeyNames="DS_FACTOR" HierarchyDefaultExpanded="true" EnableHierarchyExpandAll="true">
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="NB_FACTOR"
                            ClientDataKeyNames="NB_FACTOR" Name="PruebaDetails" Width="100%">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Prueba" DataField="NB_FACTOR" HeaderStyle-Width="120" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Resultado" DataField="NO_VALOR_RESPUESTA" HeaderStyle-Width="40" HeaderStyle-Font-Bold="true" DataFormatString = "{0: F2}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Baremos" DataField="NO_VALOR_BAREMOS" HeaderStyle-Width="40" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Prueba" DataField="DS_FACTOR" HeaderStyle-Width="350" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div style="clear: both; height: 10px;"></div>
        <div class="divControlDerecha">
            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" Width="100" OnClientClicked="CloseWindow"></telerik:RadButton>
        </div>
    </div>
</asp:Content>
