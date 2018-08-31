<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="ReporteCandidatosContratados.aspx.cs" Inherits="SIGE.WebApp.Administracion.VentanaCandidatosContratados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label class="labelTitulo">Candidatos contratados</label>
    <div style="height: calc(100% - 60px);">
        <telerik:RadGrid ID="grdCandidatos" runat="server" Height="100%" AutoGenerateColumns="false" ShowGroupPanel="True" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
            OnNeedDataSource="grdCandidatos_NeedDataSource" HeaderStyle-Font-Bold="true" OnItemCommand="grdCandidatos_ItemCommand" OnItemDataBound="grdCandidatos_ItemDataBound">
            <ClientSettings AllowDragToGroup="True" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="false" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
            <ExportSettings ExportOnlyData="true" FileName="CandidatosContratados" Excel-Format="Xlsx" IgnorePaging="true">
            </ExportSettings>
            <GroupingSettings ShowUnGroupButton="true" CaseSensitive="false"></GroupingSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true"
                ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" CommandItemDisplay="Top"
                DataKeyNames="ID_CANDIDATO" ClientDataKeyNames="ID_CANDIDATO">

                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
                <Columns>
                    <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Renglón" DataField="RENGLON" UniqueName="RENGLON" HeaderStyle-Width="70"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="130" FilterControlWidth="70"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre Completo" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO" HeaderStyle-Width="350" FilterControlWidth="150"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Fecha de solicitud " DataField="FE_SOLICITUD" UniqueName="FE_SOLICITUD" HeaderStyle-Width="150" FilterControlWidth="100" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="CL_ESTADO_EMPLEADO" UniqueName="CL_ESTADO_EMPLEADO" HeaderStyle-Width="100" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" HeaderText="Fecha ingreso " DataField="FE_ALTA" UniqueName="FE_ALTA" HeaderStyle-Width="150" FilterControlWidth="100" DataType="System.DateTime"></telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="Nb_PUESTO" UniqueName="Nb_PUESTO" HeaderStyle-Width="350" FilterControlWidth="150"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="200" FilterControlWidth="100"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA" HeaderStyle-Width="200" FilterControlWidth="150"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nivel académico" DataField="NIVEL_ESCOLARIDAD" UniqueName="NIVEL_ESCOLARIDAD" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Preparatoria" DataField="PREPARATORIA" UniqueName="PREPARATORIA" HeaderStyle-Width="120" FilterControlWidth="70"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Carrera Técnica" DataField="CARRERA_TECNICA" UniqueName="CARRERA_TECNICA" HeaderStyle-Width="120" FilterControlWidth="70"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Carrera Profesional" DataField="CARRERA_PROFESIONAL" UniqueName="CARRERA_PROFESIONAL" HeaderStyle-Width="350" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Postgrado" DataField="POSTGRADOS" UniqueName="POSTGRADOS" HeaderStyle-Width="400" FilterControlWidth="200"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
