<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="ReporteDatosEmpleados.aspx.cs" Inherits="SIGE.WebApp.Administracion.ReporteDatosEmpleados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          <label class="labelTitulo">Datos de empleados</label>
        <div style="height: calc(100% - 60px);">
        <telerik:RadGrid ID="grdEmpleados" runat="server" Height="100%" AutoGenerateColumns="false" ShowGroupPanel="True" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
            OnNeedDataSource="grdEmpleados_NeedDataSource" OnItemCommand="grdEmpleados_ItemCommand" >
                  <ClientSettings AllowDragToGroup="True" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="false" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
              <ExportSettings ExportOnlyData="true" FileName="DatosEmpleados"  Excel-Format="Xlsx" IgnorePaging="true">
               </ExportSettings>
                 <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView   EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true"
                        ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" CommandItemDisplay="Top" 
                 DataKeyNames="ID_EMPLEADO" ClientDataKeyNames="ID_EMPLEADO" >
                <GroupByExpressions>
                        </GroupByExpressions>
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />                                           
                <Columns>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Renglón" DataField="RENGLON" UniqueName="RENGLON" HeaderStyle-Width="50" FilterControlWidth="10"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="ID" DataField="ID_EMPLEADO" UniqueName="ID_EMPLEADO" HeaderStyle-Width="90" FilterControlWidth="40"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO" HeaderStyle-Width="180" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Apellidos" DataField="APELLIDOS" UniqueName="APELLIDOS" HeaderStyle-Width="250" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="CL_ESTADO_EMPLEADO" UniqueName="CL_ESTADO_EMPLEADO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de nacimiento " DataField="FE_NACIMIENTO" UniqueName="FE_NACIMIENTO" HeaderStyle-Width="100" FilterControlWidth="70"></telerik:GridDateTimeColumn>                
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Edad" DataField="EDAD" UniqueName="EDAD" HeaderStyle-Width="90" FilterControlWidth="30"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="RFC" DataField="CL_RFC" UniqueName="CL_RFC" HeaderStyle-Width="140" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="CURP" DataField="CL_CURP" UniqueName="CL_CURP" HeaderStyle-Width="200" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave del puesto" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="100" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="350" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Fecha de ingreso " DataField="FE_ALTA" UniqueName="FE_ALTA" HeaderStyle-Width="100" FilterControlWidth="70"></telerik:GridDateTimeColumn>                
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Folio de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Número de seguro social" DataField="CL_NSS" UniqueName="CL_NSS" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Dirección" DataField="DIRECCION" UniqueName="DIRECCION" HeaderStyle-Width="400" FilterControlWidth="50"></telerik:GridBoundColumn>
                 </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
