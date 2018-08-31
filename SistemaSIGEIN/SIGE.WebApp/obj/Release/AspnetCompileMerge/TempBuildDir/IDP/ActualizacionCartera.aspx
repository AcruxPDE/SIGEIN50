<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="MenuIDP.master" CodeBehind="ActualizacionCartera.aspx.cs" Inherits="SIGE.WebApp.IDP.ActualizacionCartera" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script id="MyScript" type="text/javascript">
                      
            function ConfirmarEliminar(sender, args) {
                var masterTable = $find("<%=grdSolicitudes.ClientID %>").get_masterTableView();
                var selectedItem = masterTable.get_selectedItems()[0];
                if (selectedItem != undefined) {
                    var vClSolicitud = masterTable.getCellByColumnUniqueName(selectedItem, "K_SOLICITUD_CL_SOLICITUD").innerHTML;
                    vClSolicitud = ((vClSolicitud == "&nbsp;") ? "" : " " + vClSolicitud);                    
                    var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                    { if (shouldSubmit) { this.click(); } });
                    radconfirm('¿Deseas eliminar la solicitud' + vClSolicitud + '?, se eliminaran y enviaran notificaciones si asi esta configurado, este proceso no podrá revertirse.', callBackFunction, 400, 200, null, "Eliminar solicitud");
                    args.set_cancel(true);
                } else {
                    radalert("Seleccione una solicitud.", 400, 150, "");
                    args.set_cancel(true);
                }
            }

            function ConfirmarEliminarTodas(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                { if (shouldSubmit) { this.click(); } });
                radconfirm('¿Deseas eliminar todas las solicitudes?, se eliminaran y enviaran notificaciones si asi esta configurado, este proceso no podrá revertirse.', callBackFunction, 400, 200, null, "Eliminar solicitudes");
                args.set_cancel(true);             
            }

            function onCloseWindow(oWnd, args) {
                $find("<%=grdSolicitudes.ClientID%>").get_masterTableView().rebind();
            }

            function ConfirmarEnviarNotificacion(sender, args) {
                var masterTable = $find("<%=grdSolicitudes.ClientID %>").get_masterTableView();
                 var selectedItem = masterTable.get_selectedItems()[0];
                 if (selectedItem != undefined) {
                     var vClSolicitud = masterTable.getCellByColumnUniqueName(selectedItem, "K_SOLICITUD_CL_SOLICITUD").innerHTML;
                     vClSolicitud = ((vClSolicitud == "&nbsp;") ? "" : " " + vClSolicitud);
                     var callBackFunction = Function.createDelegate(sender, function (shouldSubmit)
                     { if (shouldSubmit) { this.click(); } });
                     radconfirm('¿Deseas enviar notificación de la solicitud' + vClSolicitud + '?, este proceso enviará un correo con una contraseña para la actualización de datos.', callBackFunction, 400, 200, null, "Eliminar solicitud");
                     args.set_cancel(true);
                 } else {
                     radalert("Seleccione una solicitud.", 400, 150, "");
                     args.set_cancel(true);
                 }
             }
            


        </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdSolicitudes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSolicitudes" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEnviar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSolicitudes" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSolicitudes" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarTodas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdSolicitudes" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ftGrdSolicitudes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ftGrdSolicitudes" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="grdSolicitudes" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>            
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <label class="labelTitulo">Actualización de cartera</label>


    <%--<div style="height:100%;">--%>
    <div style="height: calc(100% - 100px);">
        <telerik:RadSplitter ID="splSolicitudes" runat="server" Width="100%" Height="100%" BorderSize="0">
            <telerik:RadPane ID="rpnGridSolicitudes" runat="server">

                <telerik:RadGrid
                    ID="grdSolicitudes"
                    runat="server"
                    Height="100%"
                    AutoGenerateColumns="false"
                    EnableHeaderContextMenu="true"
  
                    AllowSorting="true"
                    HeaderStyle-Font-Bold="true"
                    AllowMultiRowSelection="true"
                    OnNeedDataSource="grdSolicitudes_NeedDataSource"
                    OnItemDataBound="grdSolicitudes_ItemDataBound">
                    <ClientSettings>
                        <Scrolling UseStaticHeaders="true" AllowScroll="true" />
                        <Selecting AllowRowSelect="True"></Selecting>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView ClientDataKeyNames="ID_SOLICITUD" EnableColumnsViewState="false" DataKeyNames="ID_SOLICITUD" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true" EnableHeaderContextFilterMenu="true">
                        <Columns>
                            <telerik:GridClientSelectColumn Exportable="false" HeaderStyle-Width="30" UniqueName="ClientSelectColumn" ></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="80" FilterControlWidth="20" HeaderText="Folio de solicitud" DataField="K_SOLICITUD_CL_SOLICITUD" UniqueName="K_SOLICITUD_CL_SOLICITUD"></telerik:GridBoundColumn>
                            <%--1--%>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="140" HeaderText="Nombre completo" DataField="C_CANDIDATO_NB_EMPLEADO_COMPLETO" UniqueName="C_CANDIDATO_NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                            <%-- 2--%>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="100" HeaderText="Fecha de solicitud" DataField="K_SOLICITUD_FE_SOLICITUD" UniqueName="K_SOLICITUD_FE_SOLICITUD" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <%-- 3--%>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="100" HeaderText="Fecha de actualización" DataField="K_SOLICITUD_FE_SOLICITUD" UniqueName="K_SOLICITUD_FE_ACTUALIZACION" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave estado de solicitud" DataField="K_SOLICITUD_CL_SOLICITUD_ESTATUS" UniqueName="K_SOLICITUD_CL_SOLICITUD_ESTATUS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="No. Requisición" DataField="M_DEPARTAMENTO_NO_REQUISICION" UniqueName="M_DEPARTAMENTO_NO_REQUISICION"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave empleado (E)" DataField="M_EMPLEADO_CL_EMPLEADO" UniqueName="M_EMPLEADO_CL_EMPLEADO"></telerik:GridBoundColumn>                            
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Clave de acceso a evaluación" DataField="K_SOLICITUD_CL_ACCESO_EVALUACION" UniqueName="K_SOLICITUD_CL_ACCESO_EVALUACION"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Descripción de competencias adicionales" DataField="K_SOLICITUD_DS_COMPETENCIAS_ADICIONALES" UniqueName="K_SOLICITUD_DS_COMPETENCIAS_ADICIONALES"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido paterno (C)" DataField="C_CANDIDATO_NB_APELLIDO_PATERNO" UniqueName="C_CANDIDATO_NB_APELLIDO_PATERNO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Ápellido materno (C)" DataField="C_CANDIDATO_NB_APELLIDO_MATERNO" UniqueName="C_CANDIDATO_NB_APELLIDO_MATERNO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="210" FilterControlWidth="280" HeaderText="Nombre (C)" DataField="C_CANDIDATO_NB_CANDIDATO" UniqueName="C_CANDIDATO_NB_CANDIDATO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Género (C)" DataField="C_CANDIDATO_CL_GENERO" UniqueName="C_CANDIDATO_CL_GENERO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="RFC (C)" DataField="C_CANDIDATO_CL_RFC" UniqueName="C_CANDIDATO_CL_RFC"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="CURP (C)" DataField="C_CANDIDATO_CL_CURP" UniqueName="C_CANDIDATO_CL_CURP"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave estado civil (C)" DataField="C_CANDIDATO_CL_ESTADO_CIVIL" UniqueName="C_CANDIDATO_CL_ESTADO_CIVIL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre de conyugue (C)" DataField="C_CANDIDATO_NB_CONYUGUE" UniqueName="C_CANDIDATO_NB_CONYUGUE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave NSS (C)" DataField="C_CANDIDATO_CL_NSS" UniqueName="C_CANDIDATO_CL_NSS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave tipo sanguineo (C)" DataField="C_CANDIDATO_CL_TIPO_SANGUINEO" UniqueName="C_CANDIDATO_CL_TIPO_SANGUINEO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="País (C)" DataField="C_CANDIDATO_NB_PAIS" UniqueName="C_CANDIDATO_NB_PAIS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Entidad federativa (C)" DataField="C_CANDIDATO_NB_ESTADO" UniqueName="C_CANDIDATO_NB_ESTADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Municipio (C)" DataField="C_CANDIDATO_NB_MUNICIPIO" UniqueName="C_CANDIDATO_NB_MUNICIPIO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Colonia (C)" DataField="C_CANDIDATO_NB_COLONIA" UniqueName="C_CANDIDATO_NB_COLONIA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Calle (C)" DataField="C_CANDIDATO_NB_CALLE" UniqueName="C_CANDIDATO_NB_CALLE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Exterior (C)" DataField="C_CANDIDATO_NO_INTERIOR" UniqueName="C_CANDIDATO_NO_INTERIOR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. Interior (C)" DataField="C_CANDIDATO_NO_EXTERIOR" UniqueName="C_CANDIDATO_NO_EXTERIOR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Código postal (C)" DataField="C_CANDIDATO_CL_CODIGO_POSTAL" UniqueName="C_CANDIDATO_CL_CODIGO_POSTAL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Correo electrónico (C)" DataField="C_CANDIDATO_CL_CORREO_ELECTRONICO" UniqueName="C_CANDIDATO_CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Fecha de nacimiento (C)" DataField="C_CANDIDATO_FE_NACIMIENTO" UniqueName="C_CANDIDATO_FE_NACIMIENTO" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Lugar de nacimiento (C)" DataField="C_CANDIDATO_DS_LUGAR_NACIMIENTO" UniqueName="C_CANDIDATO_DS_LUGAR_NACIMIENTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo (C)" DataField="C_CANDIDATO_MN_SUELDO" UniqueName="C_CANDIDATO_MN_SUELDO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Nacionalidad (C)" DataField="C_CANDIDATO_CL_NACIONALIDAD" UniqueName="C_CANDIDATO_CL_NACIONALIDAD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Descripción de nacionalidad (C)" DataField="C_CANDIDATO_DS_NACIONALIDAD" UniqueName="C_CANDIDATO_DS_NACIONALIDAD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Sueldo" DataField="Nombre de licencia (C)" UniqueName="C_CANDIDATO_NB_LICENCIA" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Descripción de vehiculo (C)" DataField="C_CANDIDATO_DS_VEHICULO" UniqueName="C_CANDIDATO_DS_VEHICULO" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave cartilla militar (C)" DataField="C_CANDIDATO_CL_CARTILLA_MILITAR" UniqueName="C_CANDIDATO_CL_CARTILLA_MILITAR"></telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Telefonos" DataField="C_CANDIDATO_XML_TELEFONOS" UniqueName="C_CANDIDATO_XML_TELEFONOS"></telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Ingresos" DataField="C_CANDIDATO_XML_INGRESOS" UniqueName="C_CANDIDATO_XML_INGRESOS"></telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Egresos" DataField="C_CANDIDATO_XML_EGRESOS" UniqueName="C_CANDIDATO_XML_EGRESOS"></telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Patrimonio" DataField="C_CANDIDATO_XML_PATRIMONIO" UniqueName="C_CANDIDATO_XML_PATRIMONIO"></telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Disponibilidad (C)" DataField="C_CANDIDATO_DS_DISPONIBILIDAD" UniqueName="C_CANDIDATO_DS_DISPONIBILIDAD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="130" FilterControlWidth="60" HeaderText="Disponibilidad de viaje (C)" DataField="C_CANDIDATO_CL_DISPONIBILIDAD_VIAJE" UniqueName="C_CANDIDATO_CL_DISPONIBILIDAD_VIAJE"></telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Perfil de red social" DataField="C_CANDIDATO_XML_PERFIL_RED_SOCIAL" UniqueName="C_CANDIDATO_XML_PERFIL_RED_SOCIAL"></telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Comentarios (C)" DataField="C_CANDIDATO_DS_COMENTARIO" UniqueName="C_CANDIDATO_DS_COMENTARIO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre áctivo (C)" DataField="C_CANDIDATO_NB_ACTIVO" UniqueName="C_CANDIDATO_NB_ACTIVO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre empleado (E)" DataField="M_EMPLEADO_NB_EMPLEADO_COMPLETO" UniqueName="M_EMPLEADO_NB_EMPLEADO_COMPLETO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre (E)" DataField="M_EMPLEADO_NB_EMPLEADO" UniqueName="M_EMPLEADO_NB_EMPLEADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido paterno (E)" DataField="M_EMPLEADO_NB_APELLIDO_PATERNO" UniqueName="M_EMPLEADO_NB_APELLIDO_PATERNO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Apellido materno (E)" DataField="M_EMPLEADO_NB_APELLIDO_MATERNO" UniqueName="M_EMPLEADO_NB_APELLIDO_MATERNO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave estado empleado (E)" DataField="M_EMPLEADO_CL_ESTADO_EMPLEADO" UniqueName="M_EMPLEADO_CL_ESTADO_EMPLEADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Género (E)" DataField="M_EMPLEADO_CL_GENERO" UniqueName="M_EMPLEADO_CL_GENERO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave estado civil (E)" DataField="M_EMPLEADO_CL_ESTADO_CIVIL" UniqueName="M_EMPLEADO_CL_ESTADO_CIVIL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre conyugue (E)" DataField="M_EMPLEADO_NB_CONYUGUE" UniqueName="M_EMPLEADO_NB_CONYUGUE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="RFC (E)" DataField="M_EMPLEADO_CL_RFC" UniqueName="M_EMPLEADO_CL_RFC"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="CURP (E)" DataField="M_EMPLEADO_CL_CURP" UniqueName="M_EMPLEADO_CL_CURP"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="NSS (E)" DataField="M_EMPLEADO_CL_NSS" UniqueName="M_EMPLEADO_CL_NSS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Tipo sanguineo (E)" DataField="M_EMPLEADO_CL_TIPO_SANGUINEO" UniqueName="M_EMPLEADO_CL_TIPO_SANGUINEO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nacionalidad (E)" DataField="M_EMPLEADO_CL_NACIONALIDAD" UniqueName="M_EMPLEADO_CL_NACIONALIDAD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="País (E)" DataField="M_EMPLEADO_NB_PAIS" UniqueName="M_EMPLEADO_NB_PAIS"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Entidad federativa (E)" DataField="M_EMPLEADO_NB_ESTADO" UniqueName="M_EMPLEADO_NB_ESTADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Municipio (E)" DataField="M_EMPLEADO_NB_MUNICIPIO" UniqueName="M_EMPLEADO_NB_MUNICIPIO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Colonia (E)" DataField="M_EMPLEADO_NB_COLONIA" UniqueName="M_EMPLEADO_NB_COLONIA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Calle (E)" DataField="M_EMPLEADO_NB_CALLE" UniqueName="M_EMPLEADO_NB_CALLE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. interior (E)" DataField="M_EMPLEADO_NO_INTERIOR" UniqueName="M_EMPLEADO_NO_INTERIOR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="No. exterior (E)" DataField="M_EMPLEADO_NO_EXTERIOR" UniqueName="M_EMPLEADO_NO_EXTERIOR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Código postal (E)" DataField="M_EMPLEADO_CL_CODIGO_POSTAL" UniqueName="M_EMPLEADO_CL_CODIGO_POSTAL"></telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Telefonos" DataField="M_EMPLEADO_XML_TELEFONOS" UniqueName="M_EMPLEADO_XML_TELEFONOS"></telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Correo electrónico (E)" DataField="M_EMPLEADO_CL_CORREO_ELECTRONICO" UniqueName="M_EMPLEADO_CL_CORREO_ELECTRONICO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre áctivo (E)" DataField="M_EMPLEADO_NB_ACTIVO" UniqueName="M_EMPLEADO_NB_ACTIVO" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Fecha de nacimiento (E)" DataField="M_EMPLEADO_FE_NACIMIENTO" UniqueName="M_EMPLEADO_FE_NACIMIENTO" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Natalicio (E)" DataField="M_EMPLEADO_DS_LUGAR_NACIMIENTO" UniqueName="M_EMPLEADO_DS_LUGAR_NACIMIENTO"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Fecha de alta (E)" DataField="M_EMPLEADO_FE_ALTA" UniqueName="M_EMPLEADO_FE_ALTA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Fecha de baja (E)" DataField="M_EMPLEADO_FE_BAJA" UniqueName="M_EMPLEADO_FE_BAJA" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Sueldo (E)" DataField="M_EMPLEADO_MN_SUELDO" UniqueName="M_EMPLEADO_MN_SUELDO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Sueldo variable (E)" DataField="M_EMPLEADO_MN_SUELDO_VARIABLE" UniqueName="M_EMPLEADO_MN_SUELDO_VARIABLE"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Descripción de composición de sueldo (E)" DataField="M_EMPLEADO_DS_SUELDO_COMPOSICION" UniqueName="M_EMPLEADO_DS_SUELDO_COMPOSICION"></telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Campos adicionales" DataField="M_EMPLEADO_XML_CAMPOS_ADICIONALES" UniqueName="M_EMPLEADO_XML_CAMPOS_ADICIONALES"></telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Nombre áctivo (E)" DataField="NB_ACTIVO" UniqueName="NB_ACTIVO"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Fecha inactivo (E)" DataField="M_PUESTO_FE_INACTIVO" UniqueName="M_PUESTO_FE_INACTIVO" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" ShowFilterIcon="false" HeaderStyle-Width="130" FilterControlWidth="120" HeaderText="Clave puesto" DataField="M_PUESTO_CL_PUESTO" UniqueName="M_PUESTO_CL_PUESTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Nombre puesto" DataField="M_PUESTO_NB_PUESTO" UniqueName="M_PUESTO_NB_PUESTO" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Campos adicionales" DataField="M_PUESTO_XML_CAMPOS_ADICIONALES" UniqueName="M_PUESTO_XML_CAMPOS_ADICIONALES"></telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Edad minima" DataField="M_PUESTO_NO_EDAD_MINIMA" UniqueName="M_PUESTO_NO_EDAD_MINIMA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Edad maxima" DataField="M_PUESTO_NO_EDAD_MAXIMA" UniqueName="M_PUESTO_NO_EDAD_MAXIMA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Género" DataField="M_PUESTO_CL_GENERO" UniqueName="M_PUESTO_CL_GENERO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Estado civil" DataField="M_PUESTO_CL_ESTADO_CIVIL" UniqueName="M_PUESTO_CL_ESTADO_CIVIL"></telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Requerimientos" DataField="M_PUESTO_XML_REQUERIMIENTOS" UniqueName="M_PUESTO_XML_REQUERIMIENTOS"></telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Observaciones" DataField="M_PUESTO_XML_OBSERVACIONES" UniqueName="M_PUESTO_XML_OBSERVACIONES"></telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Responsabilidades" DataField="M_PUESTO_XML_RESPONSABILIDAD" UniqueName="M_PUESTO_XML_RESPONSABILIDAD"></telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Autoridad" DataField="M_PUESTO_XML_AUTORIDAD" UniqueName="M_PUESTO_XML_AUTORIDAD"></telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="true" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Cursos adicionales" DataField="M_PUESTO_XML_CURSOS_ADICIONALES" UniqueName="M_PUESTO_XML_CURSOS_ADICIONALES"></telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Mentor" DataField="M_PUESTO_XML_MENTOR" UniqueName="M_PUESTO_XML_MENTOR"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave tipo de puesto" DataField="M_PUESTO_CL_TIPO_PUESTO" UniqueName="M_PUESTO_CL_TIPO_PUESTO"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Fecha solicitud" DataField="M_DEPARTAMENTO_FE_SOLICITUD" UniqueName="M_DEPARTAMENTO_FE_SOLICITUD" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Estado" DataField="M_DEPARTAMENTO_CL_ESTADO" UniqueName="M_DEPARTAMENTO_CL_ESTADO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Causa" DataField="M_DEPARTAMENTO_CL_CAUSA" UniqueName="M_DEPARTAMENTO_CL_CAUSA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Descripción de causa" DataField="M_DEPARTAMENTO_DS_CAUSA" UniqueName="M_DEPARTAMENTO_DS_CAUSA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave empresa" DataField="C_EMPRESA_CL_EMPRESA" UniqueName="C_EMPRESA_CL_EMPRESA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Nombre empresa" DataField="C_EMPRESA_NB_EMPRESA" UniqueName="C_EMPRESA_NB_EMPRESA"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Nombre razón social" DataField="C_EMPRESA_NB_RAZON_SOCIAL" UniqueName="C_EMPRESA_NB_RAZON_SOCIAL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Nombre áctivo" DataField="M_DEPARTAMENTO_NB_ACTIVO" UniqueName="M_DEPARTAMENTO_NB_ACTIVO"></telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="200" FilterControlWidth="130" HeaderText="Fecha de inactividad" DataField="M_DEPARTAMENTO_FE_INACTIVO" UniqueName="M_DEPARTAMENTO_FE_INACTIVO" DataFormatString="{0:d}"></telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="150" FilterControlWidth="80" HeaderText="Clave departamento" DataField="M_DEPARTAMENTO_CL_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_CL_DEPARTAMENTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="110" FilterControlWidth="40" HeaderText="Nombre de departamento" DataField="M_DEPARTAMENTO_NB_DEPARTAMENTO" UniqueName="M_DEPARTAMENTO_NB_DEPARTAMENTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" Display="false" HeaderStyle-Width="0" FilterControlWidth="0" HeaderText="habilitado correo" DataField="FG_HABILITADO_ENVIO_MAIL_CANDIDATOS" UniqueName="FG_HABILITADO_ENVIO_MAIL_CANDIDATOS"></telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn Visible="true" Display="false" HeaderStyle-Width="0" FilterControlWidth="0" HeaderText="Mensaje correo" DataField="DS_MENSAJE_MAIL_NOTIFICACION" UniqueName="DS_MENSAJE_MAIL_NOTIFICACION"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" Display="false" HeaderStyle-Width="0" FilterControlWidth="0" HeaderText="Mensaje correo" DataField="DS_MENSAJE_MAIL_NOTIFICACION" UniqueName="DS_MENSAJE_MAIL_NOTIFICACION"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true" Display="false" HeaderStyle-Width="95" FilterControlWidth="25" HeaderText="Campos adicionales" DataField="M_DEPARTAMENTO_XML_CAMPOS_ADICIONALES" UniqueName="M_DEPARTAMENTO_XML_CAMPOS_ADICIONALES"></telerik:GridBoundColumn>--%>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPane>
            <telerik:RadPane ID="rpnOpciones" runat="server" Width="30">
                <telerik:RadSlidingZone ID="slzOpciones" runat="server" Width="30" ClickToOpen="true" SlideDirection="Left">
                    <telerik:RadSlidingPane ID="slzAyuda" runat="server" Title="Ayuda" Width="500" MinWidth="500" Height="100%">
                        <div style="padding: 20px;">
                            <p style="text-align:justify">
                                Esta opción te permitirá actualizar la cartera electrónica, invitando al candidato para que actualice sus datos o notificándole que su solicitud ha sido eliminada y si es de su interés pueda entrar a la página de la organización para integrar nuevamente su solicitud. <br/><br/>
                                Para especificar la búsqueda selecciona de los campos disponibles el campo que sea de tu interés (por ejemplo “estado civil”), después selecciona el criterio de búsqueda (por ejemplo “igual a”), y en seguida ingresa la cadena con la que quieres efectuar dicha comparación (por ejemplo “soltero”), a continuación da click en “agregar", y al hacer click el sistema te enviará todos aquellos solicitantes que su estado civil sea soltero.<br/><br/>
                                Puedes afinar la selección combinando varios campos, (por ejemplo, “sexo igual a femenino” , “municipio igual a Salamanca”, y “fecha de solicitud entre 01/01/2013 al 01/01/2014”), cada campo añadido limita el espectro posible de resultados, ya que la solicitud debe cumplir con la información indicada (estado civil soltero; sexo femenino; municipio Salamanca; con fecha de solicitud entre el 01/01/2013 al 01/01/2014). Si no cumple con alguna de estas variables no es incluido dentro de los resultados (por ejemplo: si es mujer, vive en Salamanca y su estado civil es casado, no es tomado en cuenta). 
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                    <telerik:RadSlidingPane ID="RSPAdvSearch" runat="server" Title="Búsqueda avanzada" Width="500" MinWidth="500" Height="100%">
                        <div style="padding: 20px;">
                            <telerik:RadFilter runat="server" ID="ftGrdSolicitudes" FilterContainerID="grdSolicitudes" ShowApplyButton="true" Height="100">
                                <ContextMenu Height="100" EnableAutoScroll="false">
                                    <DefaultGroupSettings Height="100" />
                                </ContextMenu>
                            </telerik:RadFilter>
                        </div>
                    </telerik:RadSlidingPane>                    
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>

    <div style="clear: both; height: 10px;"></div>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEnviar" runat="server" Text="Enviar y eliminar solicitud(es) seleccionada(s)" OnClientClicking="ConfirmarEliminar" Width="310" OnClick="btnEnviar_Click" ></telerik:RadButton>
    </div>
   <%-- <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminar" runat="server" Text="Eliminar solicitud" OnClick="btnEliminar_Click" OnClientClicking="ConfirmarEliminar" Width="170"></telerik:RadButton>
    </div>--%>
    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEliminarTodas" runat="server" Text="Enviar y eliminar todas las solicitudes" Width="270" OnClientClicking="ConfirmarEliminarTodas" OnClick="btnEliminarTodas_Click"></telerik:RadButton>
    </div>

    <div class="ctrlBasico">
        <telerik:RadButton ID="btnEnviarNotificacion" runat="server" Text="Enviar notificación para actualización" Width="270" OnClientClicking="ConfirmarEnviarNotificacion" OnClick="btnEnviarNotificacion_Click"></telerik:RadButton>
    </div>

    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup" OnClientShow="centerPopUp">
        <Windows>
            <telerik:RadWindow ID="winSolicitud" runat="server" Title="Solicitud" Behaviors="Close" Modal="true" VisibleStatusbar="false" ></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            <telerik:RadWindow ID="rwConsultas" runat="server" Title="Consultas Personales" Height="600px" Width="1100px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>