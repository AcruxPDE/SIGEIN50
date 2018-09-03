<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/MenuPDE.master" AutoEventWireup="true" CodeBehind="VentanaInicioPDE.aspx.cs" Inherits="SIGE.WebApp.PDE.MenuPDE1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%----%>
    <style>
        MenuPrincipal {
            width: 600px;
        }

        .noPendientes {
            padding-left: 3px;
            text-align: center;
        }


        #tituloNotificacion {
            background: #F2F2F2;
            padding: 5px;
        }

        #tituloModificacion {
            background: #F2F2F2;
            padding: 5px;
        }

        #tituloNuevos {
            background: #F2DCDB;
            padding: 5px;
        }

        #tituloNoLeidos {
            background: #F2DCDB;
            padding: 5px;
        }

        #contenidoNotificacion {
            background: #F2F2F2;
            padding: 20px 10px;
            display: none;
        }

        #contenidoModificacion {
            background: #F2F2F2;
            padding: 20px 10px;
            display: none;
        }


        .imgcolexp {
            float: right;
            width: 16px;
            cursor: pointer;
        }


        .noPendientes {
            text-align: center;
        }

        .NuevoComunicado {
            padding-left: 5px;
            text-align: center;
        }

        .VisibilidadnUevoComunicado {
            visibility: collapse;
        }

        .scrollgrid {
            max-height: inherit;
        }
    </style>
    <script>


        $(document).ready(function () {
            $("#arrow-up").css("display", "none");

            $("#arrow-up,#arrow-down").click(function () {
                $("#contenidoNotificacion").slideToggle("slow");

                if ($("#arrow-up").css("display") == "block") {
                    $("#arrow-up").css("display", "none");
                    $("#arrow-down").css("display", "block");
                }
                else {
                    $("#arrow-up").css("display", "block");
                    $("#arrow-down").css("display", "none");
                }
            });
        });
        $(document).ready(function () {
            $("#arrow-up-Mod").css("display", "none");

            $("#arrow-up-Mod,#arrow-down-Mod").click(function () {
                $("#contenidoModificacion").slideToggle("slow");

                if ($("#arrow-up-Mod").css("display") == "block") {
                    $("#arrow-up-Mod").css("display", "none");
                    $("#arrow-down-Mod").css("display", "block");
                }
                else {
                    $("#arrow-up-Mod").css("display", "block");
                    $("#arrow-down-Mod").css("display", "none");
                }
            });
        });


        function OpenWindow(pWindowProperties) {

            openChildDialog(pWindowProperties.vURL, pWindowProperties.vRadWindowId, pWindowProperties.vTitulo, pWindowProperties);
        }
        function OpenNotificacionesYTramites() {

            var win = window.open("VentanaNotificacionesYTramites.aspx", '_self', true);
            win.focus();
        }
        function OpenVisorDeComunicados() {
            //  var idServidor = ('<%= url%>');
            var win = window.open("VisorDeComunicados.aspx", '_self', true);
            // var win = window.open(idServidor);
            //alert(idServidor);
            win.focus();
        }
        function OpenVentanaMenuInformacion() {

            var win = window.open("VentanaMenuInformacion.aspx", '_self', true);
            win.focus();

        }

        function OpenConfiguracionWindow(pIdComunicado) {

            var win = window.open("VisorDeComunicados.aspx?&IdComunicado=" + pIdComunicado, '_self', true);
            win.focus();

        }

        function OpenNotificacionWindow(pIdNotificacion, BotonesVisibles) {
            var vURL = "VentanaRespuestaANotificacion.aspx?&pIdNotificacion=" + pIdNotificacion + "&pBotonesVisbles=" + BotonesVisibles;
            var vTitulo = "Notificaciones";
            var oWin = window.radopen(vURL, "rwVentanaEditarNotificaciones", document.documentElement.clientWidth - 500, document.documentElement.clientHeight - 80);
            oWin.set_title(vTitulo);
        }

        function OpenModificacionWindowAdmin(pIdEmpleado, BotonesVisibles, pTipo, pIdCambio, pDsCambio, pIdPuesto, botonOk, PrimeraVez, pidComunicado) {
            var vTipoTransaccion = ('<%=vTipoTransaccion%>');
            if (vTipoTransaccion == "50") {

                if (pIdPuesto == "") {
                    var vURL = "VentanaInventarioPersonalAdmin.aspx?&pIdEmpleado=" + pIdEmpleado + "&pBotonesVisbles=" + BotonesVisibles + "&Tipo=" + pTipo + "&IdCambio=" + pIdCambio + "&DsCambio=" + pDsCambio + "&IdComunicado=" + pidComunicado;
                    var vTitulo = "Modificación a mi información";
                    var oWin = window.radopen(vURL, "rwVentanaEditarNotificaciones", document.documentElement.clientWidth - 20, document.documentElement.clientHeight - 20);
                    oWin.set_title(vTitulo);
                }
                else {
                    var vURL = "VentanaDescriptivoPuesto.aspx?&pIdEmpleado=" + pIdEmpleado + "&pIdPuesto=" + pIdPuesto + "&pBotonesVisbles=" + BotonesVisibles + "&Tipo=" + pTipo + "&IdCambio=" + pIdCambio + "&DsCambio=" + pDsCambio + "&IdComunicado=" + pidComunicado;
                    var vTitulo = "Modificación a mi información";
                    var oWin = window.radopen(vURL, "rwVentanaEditarNotificaciones", document.documentElement.clientWidth - 20, document.documentElement.clientHeight - 20);
                    oWin.set_title(vTitulo);
                }
            } else {
                if (pIdPuesto == "") {
                    var vURL = "";
                    vURL = 'http://' + document.location.hostname + "/Sigein/Catalogos/CatEmpleadoPde.aspx?&pIdEmpleado=" + pIdEmpleado + "&pBotonesVisbles=" + BotonesVisibles + "&Tipo=" + pTipo + "&IdCambio=" + pIdCambio + "&DsCambio=" + pDsCambio + "&BotonOk=" + botonOk + "&PrimeraVez=" + PrimeraVez + "&IdComunicado=" + pidComunicado;
                    var vTitulo = "Modificación";
                    var oWin = window.radopen(vURL, "rwVentanaEditarNotificaciones", document.documentElement.clientWidth - 20, document.documentElement.clientHeight - 20);
                    oWin.set_title(vTitulo);
                }
                else {
                    var vURL = "";
                    vURL = 'http://' + document.location.hostname + "/Sigein/Catalogos/DescriptivoPuesto/CatPuestoPde.aspx?&pIdEmpleado=" + pIdEmpleado + "&pIdPuesto=" + pIdPuesto + "&pBotonesVisbles=" + BotonesVisibles + "&Tipo=" + pTipo + "&IdCambio=" + pIdCambio + "&DsCambio=" + pDsCambio + "&BotonOk=" + botonOk + "&PrimeraVez=" + PrimeraVez + "&IdComunicado=" + pidComunicado;
                    var vTitulo = "Modificación";
                    var oWin = window.radopen(vURL, "rwVentanaEditarNotificaciones", document.documentElement.clientWidth - 20, document.documentElement.clientHeight - 20);
                    oWin.set_title(vTitulo);
                }

            }
        }
        //CAMBIO ENTER//
        function GetConfiguracionWindowProperties(pIdComunicado) {
            var wnd = GetWindowProperties();
            wnd.vTitulo = "Ver Comunicado";
            wnd.vURL = "VisorDeComunicados.aspx?IdComunicado=" + pIdComunicado;
            wnd.vRadWindowId = "VisorDeComunicados";
            return wnd;
        }

        function GetComunicadoId() {
            var gridComunicados = $find('<%= rgdNoLeidos.ClientID %>');
            var selectedIndex = gridComunicados.get_selectedIndexes();
            if (selectedIndex.length > 0)
                return gridComunicados.get_clientDataKeyValue()[selectedIndex]["ID_COMUNICADO"];
            else
                return null;
        }



        function onCloseWindow(oWnd, args) {

            var ajaxManager = $find('<%= raComunicados.ClientID%>');
            ajaxManager.ajaxRequest();

        }

        function useDataFromChild() {

        }




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager runat="server" ID="raComunicados" DefaultLoadingPanelID="ralpAsistencia" OnAjaxRequest="raComunicados_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="imgComunicado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdNoLeidos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="raComunicados">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgdNoLeidos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="rgdComunicadosLeidos" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="grdmodificacionesinformacion" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="grdModificacionesPendientes" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="grdNotificacionesRRHH" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="grdNotificacionesdPendientes" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divModificacionesPendientes" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="divNotificacionesPendientes" UpdatePanelHeight="100%" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>


    <telerik:RadToolTip ID="rttComunicados" runat="server" ShowDelay="1000" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
        TargetControlID="imgIntegracionPersonal" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_IP">
        <span style="font-weight: bold">Comunicados
        </span>
        <div style="clear: both"></div>
        <ul style="text-align: left;">
        </ul>
    </telerik:RadToolTip>
    <telerik:RadPageLayout ID="rplPrincipal" runat="server" GridType="Fluid" ShowGrid="true" HtmlTag="None">
        <telerik:LayoutRow RowType="Generic">

            <Rows>
                <telerik:LayoutRow RowType="Generic">
                    <Content>
                        <div style="width: 100%">
                            <div style="text-align: left; width: 50%; float: left">
                                <label id="Label2" runat="server" style="color: #DE1648; font-weight: bold; font-size: 20px">Bienvenido </label>
                            </div>
                            <div style="text-align: right; width: 50%; float: left">
                                <label id="Nombre" runat="server" style="color: #707172; font-weight: bold; font-size: 16px"></label>
                            </div>
                        </div>
                        <div style="vertical-align:central">
                        <div class="container" id="Div2" style="vertical-align: central" >
                            <div class="col-xs-12 col-lg-9 col-md-9 col-sm-9" style="margin-top: 15px; margin-left: 295px" ; >
                                <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top: 10px">
                                    <div style="position: relative; cursor: pointer; overflow: hidden;">
                                        <img id="imgComunicados" src="../Assets/images/menu/Comunicados.jpg" class="img-responsive" onclick="OpenVisorDeComunicados()" />
                                    </div>
                                    <div style="background-color: #DE1648; width: 100%; text-align: center; color: #FFF;">Comunicados</div>
                                </div>
                                <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top: 10px">
                                    <div style="position: relative; cursor: pointer; overflow: hidden;">
                                        <img id="imgNotificacionesYTramites" src="../Assets/images/menu/NotificacionesTramites.jpg" class="img-responsive" onclick="OpenNotificacionesYTramites()" />
                                    </div>
                                    <div style="background-color: #DE1648; width: 100%; text-align: center; color: #FFF;">Notificaciones y trámites</div>
                                </div>
                                <%--  <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top: 10px">
                                    <div style="position: relative; cursor: pointer; overflow: hidden;">
                                        <img id="imgMiInformacion" src="../Assets/images/menu/Informacion.jpg" class="img-responsive" onclick="OpenVentanaMenuInformacion()" />
                                    </div>
                                    <div style="background-color: #DE1648; width: 100%; text-align: center; color: #FFF;">Mi información</div>
                                </div>--%>
                            </div>
                        </div>
                                    </div>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
        </telerik:LayoutRow>
    </telerik:RadPageLayout>
    <div style="height: calc(100% - 20px); width: calc(100% - 20px)">
        <div style="float: left; width: 50%; padding-left: 10%;">
            <div style="clear: both; height: 20px;"></div>
            <div style="background-color: #DE1648; width: 500px; text-align: left; color: #FFF;">Actualización de comunicados</div>

            <div id="DContenedor2" style="height: 500px;">

                <div id="contenidoNoLeidos" style="width: 500px;">
                    <telerik:RadGrid runat="server"
                        Height="350px"
                        ID="rgdNoLeidos"
                        OnItemDataBound="rgdNoLeidos_ItemDataBound"
                        AutoGenerateColumns="false"
                        EnableHeaderContextMenu="true"
                        OnNeedDataSource="rgdNoLeidos_NeedDataSource">
                        <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView DataKeyNames="ID_COMUNICADO_EMPLEADO, ID_COMUNICADO, FE_REGISTRO" EnableColumnsViewState="false" AllowPaging="true" ShowHeadersWhenNoRecords="true">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="30" HeaderStyle-BackColor="#F2DCDB" >
                                    <HeaderTemplate>No Leídos</HeaderTemplate>
                                    <ItemTemplate>
                                        <div id="divNuevo" runat="server" style="width: 20px; height: 20px; background-color: red; float: left; border-radius: 20px">
                                            <telerik:RadLabel ID="lblNuevoComunicado" runat="server" Text="N" Font-Size="12px" CssClass="NuevoComunicado" ForeColor="White"></telerik:RadLabel>
                                        </div>
                                        <div id="divNoNuevo" runat="server" style="width: 20px; height: 20px; background-color: transparent; float: left; border-radius: 20px">
                                        </div>
                                        <div style="padding-left: 10px; float: left; width: 80%;">
                                            <label style="font-size: 14px; font-weight: lighter;"><%# Eval("NB_COMUNICADO") %></label>
                                        </div>
                                        <div style="float: left; cursor: pointer;">

                                            <img id="imgComunicado" src="../Assets/images/StatusPDE.png" onclick="OpenConfiguracionWindow(<%# Eval("ID_COMUNICADO") %>)" />

                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" HeaderStyle-BackColor="#F2DCDB" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Creación" DataField="FE_COMUNICADO" UniqueName="FE_COMUNICADO" HeaderStyle-Width="10" FilterControlWidth="80"></telerik:GridDateTimeColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>

                </div>
                <div style="clear: both; height: 20px;"></div>
                <div id="contenidoNuevosComunicados" style="width: 500px;">

                    <telerik:RadGrid runat="server"
                        Height="350px"
                        ID="rgdComunicadosLeidos"
                        AllowPaging="true"
                        AutoGenerateColumns="false"
                        EnableHeaderContextMenu="true"
                        OnNeedDataSource="rgdComunicadosLeidos_NeedDataSource">
                        <ClientSettings>
                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <MasterTableView EnableColumnsViewState="false" AllowPaging="true" ShowHeadersWhenNoRecords="true" >
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="30" HeaderStyle-BackColor="#F2DCDB">
                                    <HeaderTemplate>Leídos</HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="padding-left: 10px; float: left; width: 80%;">
                                            <label id="Label1" runat="server" style="font-size: 14px; font-weight: lighter;"><%# Eval("NB_COMUNICADO") %></label>
                                        </div>
                                        <div style="float: left; cursor: pointer;">

                                            <img src="../Assets/images/StatusPDE.png" onclick="OpenConfiguracionWindow(<%# Eval("ID_COMUNICADO") %>)" />
                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridDateTimeColumn AutoPostBackOnFilter="true" HeaderStyle-BackColor="#F2DCDB" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Creación" DataField="FE_COMUNICADO" UniqueName="FE_COMUNICADO" HeaderStyle-Width="10" FilterControlWidth="80"></telerik:GridDateTimeColumn>

                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>

                </div>
            </div>


        </div>
        <div style="float: left; width: 50%; padding-left: 5%;">
            <div style="clear: both; height: 20px;"></div>
            <div style="background-color: #DE1648; width: 500px; text-align: left; color: #FFF;">Notificaciones a RRHH</div>

            <div id="Notificaciones">
                <telerik:RadGrid ID="grdNotificacionesRRHH" ShowHeader="true" runat="server" AllowPaging="true"
                    Width="500px" Height="370" OnNeedDataSource="grdNotificaciones_NeedDataSource" OnItemDataBound="grdNotificacionesRRHH_ItemDataBound">
                    <ClientSettings EnablePostBackOnRowClick="false " Selecting-AllowRowSelect="false">
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <MasterTableView DataKeyNames="CL_ESTADO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                        HorizontalAlign="NotSet" PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="true">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-BackColor="#F2DCDB" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Notificación" HeaderStyle-Width="90" FilterControlWidth="70" DataField="NB_ASUNTO" UniqueName="NB_ASUNTO"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="90" HeaderStyle-BackColor="#F2DCDB" AllowFiltering="false">
                                <HeaderTemplate>Status</HeaderTemplate>
                                <ItemTemplate>
                                    <sdiv style="padding-left: 10px; float: left; width: 20%;">
                                        <div id="divAtendida" runat="server" style="width: 20px; height: 20px; background-color: red; float: left; border-radius: 20px">
                                            <telerik:RadLabel ID="lblNuevoComunicado" runat="server" Text="R" Font-Size="12px" CssClass="NuevoComunicado" ForeColor="White"></telerik:RadLabel>
                                        </div>
                                    </sdiv>
                                    <div style="padding-left: 10px; float: left; width: 80%;">
                                        <label style="font-size: 14px; font-weight: lighter;"><%# Eval("CL_ESTADO") %></label>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn HeaderStyle-Width="150" AllowFiltering="false" HeaderStyle-BackColor="#F2DCDB">
                                <HeaderTemplate>Descripción</HeaderTemplate>
                                <ItemTemplate>
                                    <div style="float: left; width: 80%;">
                                        <label style="font-size: 14px; font-weight: lighter;"><%# Eval("DS_NOTIFICACION") %></label>
                                    </div>
                                    <div style="float: left; cursor: pointer;">

                                        <img src="../Assets/images/StatusPDE.png" onclick="OpenNotificacionWindow(<%# Eval("ID_NOTIFICACION_RRHH") %>, <%= "false"%>)" />

                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div style="clear: both; height: 20px;"></div>


            <div style="background-color: #DE1648; width: 500px; text-align: left; color: #FFF;">Modificaciones a Mi Información</div>
            <div id="Modificaciones">
                <telerik:RadGrid ID="grdmodificacionesinformacion" ShowHeader="true" runat="server" AllowPaging="true"
                    Width="500px" Height="370" OnNeedDataSource="grdModificacionesInformacion_NeedDataSource">
                    <ClientSettings EnablePostBackOnRowClick="false" Selecting-AllowRowSelect="false">
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" />
                    <MasterTableView DataKeyNames="CL_ESTADO, ID_CAMBIO ,ID_COMUNICADO,ID_EMPLEADO, DS_CAMBIO, ID_PUESTO" ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" PageSize="10"
                        HorizontalAlign="NotSet" PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="true">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-BackColor="#F2DCDB" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="Formulario" HeaderStyle-Width="100" FilterControlWidth="75" DataField="NB_FORMULARIO" UniqueName="NB_FORMULARIO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-BackColor="#F2DCDB" AutoPostBackOnFilter="true" AllowFiltering="false" CurrentFilterFunction="Contains" HeaderText="Fecha" HeaderStyle-Width="100" FilterControlWidth="100" DataField="FE_ULTIMA_MODIFICACION" UniqueName="FE_ULTIMA_MODIFICACION"></telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn HeaderStyle-Width="150" AllowFiltering="false" HeaderStyle-BackColor="#F2DCDB">
                                <HeaderTemplate>Status</HeaderTemplate>
                                <ItemTemplate>
                                    <div style="float: left; width: 80%;">
                                        <label style="font-size: 14px; font-weight: lighter;"><%# Eval("CL_ESTADO") %></label>
                                    </div>
                                    <div style="float: left; cursor: pointer;">
                                        <img src="../Assets/images/StatusPDE.png" onclick="OpenModificacionWindowAdmin('<%# Eval("ID_EMPLEADO") %>', <%= "false"%>, '<%= "e"%>', <%# Eval("ID_CAMBIO") %>,'<%# Eval("DS_CAMBIO") %>',  '<%# Eval("ID_PUESTO") %>', '<%= "botonOkNo"%>', '<%= "SiPrimeraVez"%>', <%# Eval("ID_COMUNICADO")%> );" />

                                        <%--<img src="../Assets/images/StatusPDE.png" onclick="OpenModificacionWindowAdmin(<%# Eval("ID_EMPLEADO") %>, <%= "false"%>, '<%= "e"%>', <%# Eval("ID_CAMBIO") %>,'<%# Eval("DS_CAMBIO") %>');" />--%>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>


            </div>
            <div style="clear: both; height: 20px;"></div>

            <div id="dPedientesAdm">
                <div id="pendientes" runat="server" style="background-color: #DE1648; width: 500px; text-align: left; color: #FFF;">Visible solo para administradores</div>


                <div id="ModificacionesNotificaciones" runat="server">

                    <div id="tituloNotificacion" style="width: 500px; height: 30px;">
                        <div style="float: left; width: 90%; font-weight: bold;">Notificaciones pendientes</div>

                        <div id="divNotificacionesPendientes" runat="server" style="width: 20px; height: 20px; background-color: #0070C0; float: left; border-radius: 20px">
                            <telerik:RadLabel ID="lblPendientesN" runat="server" Font-Size="12px" CssClass="noPendientes" ForeColor="White "></telerik:RadLabel>
                        </div>
                        <img src="../Assets/images/arrow.png" id="arrow-down" class="imgcolexp" />
                        <img src="../Assets/images/arrow-up.png" id="arrow-up" class="imgcolexp" />
                    </div>
                    <div id="contenidoNotificacion" style="width: 500px;">

                        <telerik:RadGrid runat="server"
                            ID="grdNotificacionesdPendientes"
                            AutoGenerateColumns="false"
                            EnableHeaderContextMenu="true"
                            AllowPaging="true"
                            Height="400px"
                            Width="490px"
                            OnNeedDataSource="grdNotificacionesdPendientes_NeedDataSource">
                            <GroupingSettings CaseSensitive="false" />
                            <ClientSettings>
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <MasterTableView EnableColumnsViewState="false" AllowPaging="true" AllowFilteringByColumn="true" ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Exportable="false" HeaderStyle-BackColor="#F2DCDB" FilterControlWidth="100" HeaderStyle-Width="35" HeaderText="Notificación" DataField="NB_ASUNTO" UniqueName="NB_ASUNTO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Exportable="false" HeaderStyle-BackColor="#F2DCDB" FilterControlWidth="100" HeaderStyle-Width="40" HeaderText="Empleado" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="30" AllowFiltering="false" HeaderStyle-BackColor="#F2DCDB">
                                        <HeaderTemplate>Status</HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="float: left; width: 80%;">
                                                <label style="font-size: 14px; font-weight: lighter;"><%# Eval("CL_ESTADO") %></label>
                                            </div>
                                            <div style="float: left; cursor: pointer;">

                                                <img id="imgNotificacionesPendientes" src="../Assets/images/StatusPDE.png" onclick="OpenNotificacionWindow(<%# Eval("ID_NOTIFICACION_RRHH") %>,  <%= "true"%>)" />

                                            </div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                </Columns>

                            </MasterTableView>
                        </telerik:RadGrid>


                    </div>
                    <div style="clear: both; height: 10px;"></div>

                    <div id="tituloModificacion" style="width: 500px; height: 30px;">
                        <div style="float: left; width: 90%; font-weight: bold;">Modificaciones pendientes</div>

                        <div id="divModificacionesPendientes" runat="server" style="width: 20px; height: 20px; background-color: #0070C0; float: left; border-radius: 20px">
                            <telerik:RadLabel ID="lblPendientesM" runat="server" Font-Size="12px" CssClass="noPendientes" ForeColor="White "></telerik:RadLabel>
                        </div>
                        <img src="../Assets/images/arrow.png" id="arrow-down-Mod" class="imgcolexp" />
                        <img src="../Assets/images/arrow-up.png" id="arrow-up-Mod" class="imgcolexp" />
                    </div>

                    <div id="contenidoModificacion" style="width: 500px;">
                        <telerik:RadGrid runat="server"
                            ID="grdModificacionesPendientes"
                            AutoGenerateColumns="false"
                            EnableHeaderContextMenu="true"
                            AllowPaging="true"
                            Height="400px"
                            Width="490px"
                            OnNeedDataSource="rgModificacionesPendientes_NeedDataSource1">
                            <ClientSettings>
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                            </ClientSettings>
                            <PagerStyle AlwaysVisible="true" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView DataKeyNames="CL_ESTADO, ID_CAMBIO, ID_COMUNICADO , ID_EMPLEADO, DS_CAMBIO, ID_PUESTO " EnableColumnsViewState="false" AllowFilteringByColumn="true" AllowPaging="true" ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100" Exportable="false" HeaderStyle-BackColor="#F2DCDB" HeaderStyle-Width="35" HeaderText="Nombre" DataField="NB_EMPLEADO" UniqueName="NB_EMPLEADO"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100" Exportable="false" HeaderStyle-BackColor="#F2DCDB" HeaderStyle-Width="40" HeaderText="Formulario" DataField="NB_FORMULARIO" UniqueName="NB_FORMULARIO"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="30" AllowFiltering="false" HeaderStyle-BackColor="#F2DCDB">
                                        <HeaderTemplate>Status</HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="float: left; width: 80%;">
                                                <label style="font-size: 14px; font-weight: lighter;"><%# Eval("CL_ESTADO") %></label>
                                            </div>
                                            <div style="float: left; cursor: pointer;">

                                                <img src="../Assets/images/StatusPDE.png" onclick="OpenModificacionWindowAdmin('<%# Eval("ID_EMPLEADO") %>',  '<%= "true"%>', '<%= "v"%>', <%# Eval("ID_CAMBIO") %>, '<%# Eval("DS_CAMBIO") %>', '<%# Eval("ID_PUESTO") %>', '<%= "botonOkNo"%>', '<%= "SiPrimeraVez"%>', <%# Eval("ID_COMUNICADO") %>);" />

                                            </div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                </Columns>

                            </MasterTableView>
                        </telerik:RadGrid>

                    </div>

                </div>
            </div>
            <div style="clear: both; height: 20px;"></div>
        </div>

    </div>
    <telerik:RadWindowManager ID="rwmMensaje" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow
                ID="VisorDeComunicados"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>

            <telerik:RadWindow
                ID="rwVentanaEditarNotificaciones"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false"
                OnClientClose="onCloseWindow">
            </telerik:RadWindow>

            <telerik:RadWindow ID="winEmpleado" runat="server" Title="Empleado" Behaviors="Close" Modal="true" VisibleStatusbar="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rnMensaje" runat="server" EnableShadow="true"></telerik:RadWindowManager>
</asp:Content>
