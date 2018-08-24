<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaReporteCostoCapacitacion.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaReporteCostoCapacitacion" %>
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
    
    <telerik:RadGrid runat="server" ID="rgCursos" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" OnNeedDataSource="rgCursos_NeedDataSource" AllowFilteringByColumn="true" ShowGroupPanel="true" Width="100%" Height="100%" ShowFooter="true">
        <ClientSettings>
            <Scrolling UseStaticHeaders="true" AllowScroll="true" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="ID_EVENTO" ShowHeadersWhenNoRecords="true">
            <Columns>
                <telerik:GridBoundColumn CurrentFilterFunction="Contains" UniqueName="NB_EVENTO" DataField="NB_EVENTO" HeaderText="Evento"></telerik:GridBoundColumn>
                <telerik:GridHyperLinkColumn CurrentFilterFunction="Contains" UniqueName="HL_NB_CURSO" DataTextField="NB_CURSO" DataNavigateUrlFields="ID_EVENTO" DataNavigateUrlFormatString="javascript:OpenCursosRealizadosDetalleWindow({0})"></telerik:GridHyperLinkColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="Contains" UniqueName="NB_INSTRUCTOR" DataField="NB_INSTRUCTOR" HeaderText="Instructor"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="Contains" UniqueName="MN_COSTO_DIRECTO" DataField="MN_COSTO_DIRECTO" HeaderText="Costo directo" Aggregate="Sum" FooterText="Total directo: ">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="Contains" UniqueName="MN_COSTO_INDIRECTO" DataField="MN_COSTO_INDIRECTO" HeaderText="Costo indirecto" Aggregate="Sum" FooterText="Total indirecto: ">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="Contains" UniqueName="FE_INICIO" DataField="FE_INICIO" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="30%">
                    <HeaderStyle Width="120px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="Contains" UniqueName="FE_TERMINO" DataField="FE_TERMINO" HeaderText="Fecha Final" DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="30%">
                    <HeaderStyle Width="120px" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
