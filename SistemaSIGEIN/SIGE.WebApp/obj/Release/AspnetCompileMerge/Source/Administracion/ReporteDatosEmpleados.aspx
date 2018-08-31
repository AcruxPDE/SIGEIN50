<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="ReporteDatosEmpleados.aspx.cs" Inherits="SIGE.WebApp.Administracion.ReporteDatosEmpleados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <label class="labelTitulo">Datos de empleados</label>
        <div style="height: calc(100% - 60px);">
        <telerik:RadGrid ID="grdEmpleados" runat="server" Height="100%" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false" ShowGroupPanel="True" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
            OnNeedDataSource="grdEmpleados_NeedDataSource" OnItemCommand="grdEmpleados_ItemCommand" OnItemDataBound="grdEmpleados_ItemDataBound" >
                  <ClientSettings AllowDragToGroup="True" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="false" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
              <ExportSettings ExportOnlyData="true" FileName="DatosEmpleados"  Excel-Format="Xlsx" IgnorePaging="true">
               </ExportSettings>
                 <GroupingSettings ShowUnGroupButton="true" CaseSensitive="false"></GroupingSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView   EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true"
                        ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" CommandItemDisplay="Top" 
                 DataKeyNames="ID_EMPLEADO" ClientDataKeyNames="ID_EMPLEADO" >
                <GroupByExpressions>
                        </GroupByExpressions>
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />                                           
                <Columns>
                    <telerik:GridBoundColumn  AllowFiltering="false"  HeaderText="Renglón" DataField="RENGLON" UniqueName="RENGLON" HeaderStyle-Width="70" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Width="120" FilterControlWidth="50" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO" HeaderStyle-Width="180" FilterControlWidth="110"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Apellidos" DataField="APELLIDOS" UniqueName="APELLIDOS" HeaderStyle-Width="250" FilterControlWidth="180"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="CL_ESTADO_EMPLEADO" UniqueName="CL_ESTADO_EMPLEADO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true"  HeaderText="Fecha de nacimiento " DataField="FE_NACIMIENTO" UniqueName="FE_NACIMIENTO" HeaderStyle-Width="150" FilterControlWidth="90" DataType="System.DateTime"  ></telerik:GridDateTimeColumn>                
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Edad" DataField="EDAD" UniqueName="EDAD" HeaderStyle-Width="100" FilterControlWidth="30" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="RFC" DataField="CL_RFC" UniqueName="CL_RFC" HeaderStyle-Width="180" FilterControlWidth="100"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="CURP" DataField="CL_CURP" UniqueName="CL_CURP" HeaderStyle-Width="180" FilterControlWidth="100"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave del puesto" DataField="CL_PUESTO" UniqueName="CL_PUESTO" HeaderStyle-Width="120" FilterControlWidth="60"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Puesto" DataField="NB_PUESTO" UniqueName="NB_PUESTO" HeaderStyle-Width="350" FilterControlWidth="260"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Departamento" DataField="NB_DEPARTAMENTO" UniqueName="NB_DEPARTAMENTO" HeaderStyle-Width="180" FilterControlWidth="110"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true"  HeaderText="Fecha de ingreso" DataField="FE_ALTA" UniqueName="FE_ALTA" HeaderStyle-Width="120" FilterControlWidth="70" DataType="System.DateTime"  ></telerik:GridDateTimeColumn>                
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Folio de solicitud" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Número de seguro social" DataField="CL_NSS" UniqueName="CL_NSS" HeaderStyle-Width="130" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Dirección" DataField="DIRECCION" UniqueName="DIRECCION" HeaderStyle-Width="400" FilterControlWidth="320"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Ciudad" DataField="NB_MUNICIPIO" UniqueName="NB_MUNICIPIO" HeaderStyle-Width="150" FilterControlWidth="70"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estado" DataField="NB_ESTADO" UniqueName="NB_ESTADO" HeaderStyle-Width="150" FilterControlWidth="70"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Código postal" DataField="CL_CODIGO_POSTAL" UniqueName="CL_CODIGO_POSTAL" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Correo electrónico" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO" HeaderStyle-Width="230" FilterControlWidth="160"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Teléfono particular" DataField="TELEFONO_CASA" UniqueName="TELEFONO_CASA" HeaderStyle-Width="150" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Teléfono móvil" DataField="TELEFONO_MOVIL" UniqueName="TELEFONO_MOVIL" HeaderStyle-Width="150" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Comentarios" DataField="DS_COMENTARIO" UniqueName="DS_COMENTARIO" HeaderStyle-Width="500" FilterControlWidth="400"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Sexo" DataField="SEXO" UniqueName="SEXO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Tiene dependientes" DataField="TIENE_DEPENDIENTES" UniqueName="TIENE_DEPENDIENTES" HeaderStyle-Width="100" FilterControlWidth="20"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Número de dependientes" DataField="NUMERO_DEPENDIENTES" UniqueName="NUMERO_DEPENDIENTES" HeaderStyle-Width="100" FilterControlWidth="20" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nivel Académico" DataField="NIVEL_ESCOLARIDAD" UniqueName="NIVEL_ESCOLARIDAD" HeaderStyle-Width="150" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Especifique" DataField="NB_ESCOLARIDAD" UniqueName="NB_ESCOLARIDAD" HeaderStyle-Width="150" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Clave de la empresa" DataField="CL_EMPRESA" UniqueName="CL_EMPRESA" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Empresa" DataField="NB_EMPRESA" UniqueName="NB_EMPRESA" HeaderStyle-Width="200" FilterControlWidth="50"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
