<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaReporteListaMateriales.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaReporteListaMateriales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
        <script type="text/javascript">
            function GetWindowProperties() {
                return {
                    width: document.documentElement.clientWidth - 20,
                    height: document.documentElement.clientHeight - 20
                };
            }

            function GetCursosRealizadosDetalleWindowProperties(pIdEvento) {
                var wnd = GetWindowProperties();

                wnd.vTitulo = "Reporte cursos realizados - Detalle";
                wnd.vRadWindowId = "winDetalle";
                wnd.vURL = "VentanaReporteCursosRealizadosDetalle.aspx?IdEvento=" + pIdEvento;

                return wnd;
            }

            function OpenWindow(pWindowProperties) {
                openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
            }

            function OpenCursosRealizadosDetalleWindow(pIdReporte) {
                OpenWindow(GetCursosRealizadosDetalleWindowProperties(pIdReporte));
            }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    
    <telerik:RadGrid runat="server" ID="rgCursos" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" OnNeedDataSource="rgCursos_NeedDataSource" AllowFilteringByColumn="true" ShowGroupPanel="true" Width="100%" Height="100%" OnDetailTableDataBind="rgCursos_DetailTableDataBind">
        <ClientSettings>
            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
          <GroupingSettings CaseSensitive="false" />
        <MasterTableView DataKeyNames="ID_EVENTO" ShowHeadersWhenNoRecords="true" HierarchyDefaultExpanded="true">
            <Columns>

                <telerik:GridBoundColumn CurrentFilterFunction="Contains" UniqueName="NB_EVENTO" DataField="NB_EVENTO" HeaderText="Evento"></telerik:GridBoundColumn>
                <telerik:GridHyperLinkColumn CurrentFilterFunction="Contains" UniqueName="HL_NB_CURSO" DataTextField="NB_CURSO" DataNavigateUrlFields="ID_EVENTO" DataNavigateUrlFormatString="javascript:OpenCursosRealizadosDetalleWindow({0})"></telerik:GridHyperLinkColumn>
                <telerik:GridBoundColumn UniqueName="CL_TIPO_CURSO" DataField="CL_TIPO_CURSO" HeaderText="Tipo"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="Contains" UniqueName="NO_HORAS" DataField="NO_HORAS" HeaderText="Duración" FilterControlWidth="30%">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle Width="100px" />
                </telerik:GridBoundColumn>

            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="gtvMaterial" AutoGenerateColumns="false" NoDetailRecordsText="No hay materiales para este curso" AllowFilteringByColumn="false">
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="NB_TEMA" DataField="NB_TEMA" HeaderText="Tema"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="HTML_MATERIAL" DataField="HTML_MATERIAL" HeaderText="Materiales"></telerik:GridBoundColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>

</asp:Content>
