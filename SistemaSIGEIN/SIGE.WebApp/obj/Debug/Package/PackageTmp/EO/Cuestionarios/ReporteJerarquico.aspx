<%@ Page Title="" Language="C#" MasterPageFile="~/EO/ContextEO.master" AutoEventWireup="true" CodeBehind="ReporteJerarquico.aspx.cs" Inherits="SIGE.WebApp.EO.Cuestionarios.ReporteJerarquico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">
    <div style="height: calc(100% - 40px);">
        <telerik:RadTreeList ID="grdReporteJerarquico" runat="server" AutoGenerateColumns="false" ExpandCollapseMode="Client"
            DataKeyNames="ID_EMPLEADO_EVALUADO" ParentDataKeyNames="ID_EMPLEADO_EVALUADOR"
            OnNeedDataSource="grdReporteJerarquico_NeedDataSource"
            CommandItemDisplay="Top" Height="100%">
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                <Selecting AllowItemSelection="true" AllowToggleSelection="true" />
            </ClientSettings>
            <ExportSettings
                ExportMode="DefaultContent"
                IgnorePaging="true"
                OpenInNewWindow="true"
                FileName="ReporteJerarquico">
            </ExportSettings>
            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="true" />
            <Columns>
                <telerik:TreeListBoundColumn DataField="NB_EVALUADO" HeaderText="Nombre" UniqueName="NB_EVALUADO"></telerik:TreeListBoundColumn>
                <telerik:TreeListBoundColumn DataField="NB_PUESTO" HeaderText="Puesto" UniqueName="NB_PUESTO" HeaderStyle-Width="480"></telerik:TreeListBoundColumn>
                <telerik:TreeListDateTimeColumn DataFormatString="{0:d}" HeaderText="Fecha" DataField="FE_MODIFICACION" UniqueName="FE_MODIFICACION" HeaderStyle-Width="130" ItemStyle-HorizontalAlign="Right"></telerik:TreeListDateTimeColumn>
                <telerik:TreeListNumericColumn DataField="NO_RESULTADO" HeaderText="Puntaje" UniqueName="NO_RESULTADO" HeaderStyle-Width="90" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#0.00}"></telerik:TreeListNumericColumn>
            </Columns>
        </telerik:RadTreeList>
    </div>
        <div><telerik:RadButton ID="btnExportarExcel" runat="server" Text="Exportar a Excel" OnClick="btnExportarExcel_Click"></telerik:RadButton> </div>

</asp:Content>
