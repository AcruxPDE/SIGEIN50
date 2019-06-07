<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeBehind="ReporteSolicitudEmpleo.aspx.cs" Inherits="SIGE.WebApp.Administracion.ReporteSolicitudEmpleo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          <label class="labelTitulo">Datos de solicitud de empleo</label>
        <div style="height: calc(100% - 60px);">
        <telerik:RadGrid ID="grdSolicitud" runat="server" HeaderStyle-Font-Bold="true" Height="100%" AutoGenerateColumns="false" ShowGroupPanel="True" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
            OnNeedDataSource="grdSolicitud_NeedDataSource" OnItemCommand="grdSolicitud_ItemCommand" OnItemDataBound="grdSolicitud_ItemDataBound">
                  <ClientSettings AllowDragToGroup="True" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="false" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
            </ClientSettings>
              <ExportSettings ExportOnlyData="true" FileName="SolicitudEmpleo"  Excel-Format="Xlsx" IgnorePaging="true">
               </ExportSettings>
                 <GroupingSettings ShowUnGroupButton="true" CaseSensitive="false"></GroupingSettings>
            <PagerStyle AlwaysVisible="true" />
            <MasterTableView   EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true"
                        ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true" CommandItemDisplay="Top" 
                 DataKeyNames="CL_SOLICITUD" ClientDataKeyNames="CL_SOLICITUD" >
                <GroupByExpressions>
                        </GroupByExpressions>
                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />                                           
                <Columns>
                    <telerik:GridBoundColumn  AllowFiltering="false"  HeaderText="#" HeaderStyle-HorizontalAlign="Center" DataField="RENGLON" UniqueName="RENGLON" HeaderStyle-Width="50" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Folio" DataField="CL_SOLICITUD" UniqueName="CL_SOLICITUD" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nombre Completo" DataField="NB_CANDIDATO" UniqueName="NB_CANDIDATO" HeaderStyle-Width="350" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus actual" DataField="ESTADO_ACTUAL" UniqueName="ESTADO_ACTUAL" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="No. de Empleado" DataField="CL_EMPLEADO" UniqueName="CL_EMPLEADO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estatus" DataField="ESTADO" UniqueName="ESTADO" HeaderStyle-Width="100" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Lugar de nacimiento" DataField="DS_LUGAR_NACIMIENTO" UniqueName="DS_LUGAR_NACIMIENTO" HeaderStyle-Width="300" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true"  HeaderText="Fecha de nacimiento " DataField="FE_NACIMIENTO" UniqueName="FE_NACIMIENTO" HeaderStyle-Width="100" FilterControlWidth="70" DataType="System.DateTime"  ></telerik:GridDateTimeColumn>                
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Edad" DataField="EDAD" UniqueName="EDAD" HeaderStyle-Width="120" FilterControlWidth="20"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Sexo" DataField="SEXO" UniqueName="SEXO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estado civil" DataField="CL_ESTADO_CIVIL" UniqueName="CL_ESTADO_CIVIL" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataFormatString="{0:d}" AutoPostBackOnFilter="true"  HeaderText="Fecha de solicitud" DataField="FE_SOLICITUD" UniqueName="FE_SOLICITUD" HeaderStyle-Width="100" FilterControlWidth="70" DataType="System.DateTime"  ></telerik:GridDateTimeColumn>                
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Calle y número" DataField="CALLE_NUMERO" UniqueName="CALLE_NUMERO" HeaderStyle-Width="300" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Colonia" DataField="NB_COLONIA" UniqueName="NB_COLONIA" HeaderStyle-Width="300" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Municipio" DataField="NB_MUNICIPIO" UniqueName="NB_MUNICIPIO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Estado" DataField="NB_ESTADO" UniqueName="NB_ESTADO" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="País" DataField="NB_PAIS" UniqueName="NB_PAIS" HeaderStyle-Width="100" FilterControlWidth="40"></telerik:GridBoundColumn>                 
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Código postal" DataField="CL_CODIGO_POSTAL" UniqueName="CL_CODIGO_POSTAL" HeaderStyle-Width="100" FilterControlWidth="40"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Telefono" DataField="TELEFONO_CASA" UniqueName="TELEFONO_CASA" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Teléfono móvil" DataField="TELEFONO_MOVIL" UniqueName="TELEFONO_MOVIL" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Correo electrónico" DataField="CL_CORREO_ELECTRONICO" UniqueName="CL_CORREO_ELECTRONICO" HeaderStyle-Width="300" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Nível académico" DataField="NIVEL_ESCOLARIDAD" UniqueName="NIVEL_ESCOLARIDAD" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Carrera profesional" DataField="PERIODO_PROFESIONAL" UniqueName="PERIODO_PROFESIONAL" HeaderStyle-Width="240" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Postgrado" DataField="POSTGRADO" UniqueName="POSTGRADO" HeaderStyle-Width="240" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Inglés (conversación)" DataField="INGLES_CONVERSACION" UniqueName="INGLES_CONVERSACION" HeaderStyle-Width="100" FilterControlWidth="50" DataType="System.Int32"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Inglés (traducción)" DataField="INGLES_TRADUCCION" UniqueName="INGLES_TRADUCCION" HeaderStyle-Width="100" FilterControlWidth="50" DataType="System.Int32"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Otro idioma" DataField="OTRO_IDIOMA" UniqueName="OTRO_IDIOMA" HeaderStyle-Width="120" FilterControlWidth="50"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Disponiilidad de horario" DataField="DS_DISPONIBILIDAD" UniqueName="DS_DISPONIBILIDAD" HeaderStyle-Width="200" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Intereses 1" DataField="INTERES1" UniqueName="INTERES1" HeaderStyle-Width="200" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Intereses 2" DataField="INTERES2" UniqueName="INTERES2" HeaderStyle-Width="200" FilterControlWidth="80"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Intereses 3" DataField="INTERES3" UniqueName="INTERES3" HeaderStyle-Width="200" FilterControlWidth="80"></telerik:GridBoundColumn>

                 </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
