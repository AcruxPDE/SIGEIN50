<%@ Page Title="" Language="C#" MasterPageFile="~/FYD/ContextFYD.master" AutoEventWireup="true" CodeBehind="VentanaReporteCursosRealizados.aspx.cs" Inherits="SIGE.WebApp.FYD.VentanaReporteCursosRealizados" %>

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
    <div style="height:10px; clear:both;"></div>
    <div style="height:calc(100% - 30px);">
    <telerik:RadGrid 
        runat="server" 
        ID="rgCursos" 
        AllowSorting="true" 
        AutoGenerateColumns="false" 
        HeaderStyle-Font-Bold="true" 
        ShowFooter="true" 
        OnNeedDataSource="rgCursos_NeedDataSource" 
        AllowFilteringByColumn="true" 
        Width="100%" 
        Height="100%">
        <ClientSettings>
            <Scrolling UseStaticHeaders="true" AllowScroll="true"  />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
          <GroupingSettings CaseSensitive="false" />
        <MasterTableView DataKeyNames="ID_EVENTO" ShowHeadersWhenNoRecords="true">
            <Columns>
                <telerik:GridBoundColumn CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="NB_EVENTO" DataField="NB_EVENTO" HeaderText="Evento" FilterControlWidth="130" HeaderStyle-Width="200"></telerik:GridBoundColumn>
                <telerik:GridHyperLinkColumn CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"  UniqueName="HL_NB_CURSO" DataTextField="NB_CURSO" HeaderText="Curso" FilterControlWidth="130" HeaderStyle-Width="200" DataNavigateUrlFields="ID_EVENTO" DataNavigateUrlFormatString="javascript:OpenCursosRealizadosDetalleWindow({0})"></telerik:GridHyperLinkColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"  UniqueName="NB_PUESTO" DataField="NB_PUESTO" HeaderText="Puesto objetivo" FilterControlWidth="90" HeaderStyle-Width="150"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"  UniqueName="NB_INSTRUCTOR" DataField="NB_INSTRUCTOR" FooterText="Totales:" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right" HeaderText="Instructor" FilterControlWidth="130" HeaderStyle-Width="200"></telerik:GridBoundColumn>
                  <telerik:GridBoundColumn CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"   UniqueName="MN_COSTO_DIRECTO" DataField="MN_COSTO_DIRECTO" DataFormatString="${0:N2}" FilterControlWidth="40" HeaderStyle-Width="100" HeaderText="Costo directo" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterAggregateFormatString="${0:N2}">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"   UniqueName="MN_COSTO_INDIRECTO" DataField="MN_COSTO_INDIRECTO" DataFormatString="${0:N2}" FilterControlWidth="40" HeaderStyle-Width="100" HeaderText="Costo indirecto" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterAggregateFormatString="${0:N2}">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"  UniqueName="FE_INICIO" DataField="FE_INICIO" HeaderText="Fecha inicio" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80" FilterControlWidth="30">
                    <HeaderStyle Width="120px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"  UniqueName="FE_TERMINO" DataField="FE_TERMINO" HeaderText="Fecha final" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80" FilterControlWidth="30">
                    <HeaderStyle Width="120px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"   UniqueName="NO_HORAS" DataField="NO_HORAS" HeaderText="Duración" HeaderStyle-Width="80" FilterControlWidth="30">
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle Width="100px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CL_TIPO_CURSO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"  DataField="CL_TIPO_CURSO" HeaderText="Tipo" FilterControlWidth="40" HeaderStyle-Width="100"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
   </div>
</asp:Content>
