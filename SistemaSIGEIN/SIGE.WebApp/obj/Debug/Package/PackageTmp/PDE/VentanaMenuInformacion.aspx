<%@ Page Language="C#" MasterPageFile="~/PDE/MenuPDE.master" AutoEventWireup="true" CodeBehind="VentanaMenuInformacion.aspx.cs" Inherits="SIGE.WebApp.PDE.VentanaMenuInformacion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <script src="Assets/js/appIndex.js"></script>
    <link href="Assets/css/estilo.css" rel="stylesheet" />--%>
    <style>
        MenuPrincipal {
            width: 600px;
        }

        li a {
            display: block;
            width: 60px;
        }

        .TituloMenu {
            font-size: 24px;
        }
    </style>
    <script>
        function closeWindow() {
            document.cookie = "vIdPuestoRequest=;";
            GetRadWindow().close();

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadPageLayout ID="rplPrincipal" runat="server" GridType="Fluid" ShowGrid="true" HtmlTag="None">
        <telerik:LayoutRow RowType="Generic">
            <Rows>
                <telerik:LayoutRow RowType="Generic">
                    <Content>
                        <div style="float: left; padding: 10px;">
                            <label class="labelTitulo" style="width: 100%">Módulo de información de Inventario de personal y  Descriptivo de puesto</label>
                            <telerik:RadLabel style="text-align:center; color:blue" runat="server" ID="rlMensajePrivado" Text="Usted ya ha realizado cambios, espere a que se lleve a cabo el proceso de autorización." Visible="false"></telerik:RadLabel>
                        </div>
                        <div runat="server" id="divMenu"></div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow RowType="Generic">
                    <Rows>
                        <telerik:LayoutRow RowType="Container" WrapperHtmlTag="Div">
                            <Columns>
                                <telerik:LayoutColumn Span="12" SpanMd="12" SpanSm="12" SpanXs="12">
                                </telerik:LayoutColumn>
                            </Columns>
                        </telerik:LayoutRow>
                    </Rows>
                </telerik:LayoutRow>
                <telerik:LayoutRow RowType="Generic">
                    <Content>
                        <div class="container" id="Div2" style="height: 100%">
                            <div runat="server" visible="false" id="inventario" class="col-xs-12 col-lg-9 col-md-9 col-sm-9" style="margin-top: 15px">
                                <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top: 10px" id="divIP">
                                    <div style="position: relative; cursor: pointer; overflow: hidden; ">
                                        <img id="imgConsultasInteligentes" src="../Assets/images/menu/Inventario.jpg" class="img-responsive" title="Presiona click derecho sobre la imagen para acceder." />
                                    </div>
                                    <div style="background: #DE1648; width: 100%; text-align: center; color: #FFF;">Inventario de personal</div>
                                </div>
                            </div>
                            <div runat="server" visible="false" id="descriptivo" class="col-xs-12 col-lg-9 col-md-9 col-sm-9" style="margin-top: 15px">
                                <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top: 10px" id="divPP">
                                    <div style="position: relative; cursor: pointer; overflow: hidden;">
                                        <img id="imgReportesPersonalizados" src="../Assets/images/menu/Descriptivo.jpg" class="img-responsive" title="Presiona click derecho sobre la imagen para acceder."  />
                                    </div>
                                    <div style="background: #DE1648; width: 100%; text-align: center; color: #FFF;">Perfil de puesto</div>
                                    <div style="display: none">
                                    </div>
                                </div>

                            </div>
                            <telerik:RadContextMenu runat="server" ID="ContextMenu1"
                                EnableRoundedCorners="true" EnableShadows="true"
                                OnClientItemClicked="OnClientItemClicked">
                                <Targets>
                                    <telerik:ContextMenuElementTarget ElementID="divIP" />
                                </Targets>
                                <Items>
                                    <%--<telerik:RadMenuItem Text="Ver" Value="VerIP" />
                                    <telerik:RadMenuItem Text="Editar" Value="EditarIP" />--%>
                                </Items>
                            </telerik:RadContextMenu>
                            <telerik:RadContextMenu runat="server" ID="ContextMenu2"
                                EnableRoundedCorners="true" EnableShadows="true"
                                OnClientItemClicked="OnClientItemClicked">
                                <Targets>
                                    <telerik:ContextMenuElementTarget ElementID="divPP" />
                                </Targets>
                                <Items>
                                    <%--<telerik:RadMenuItem Text="Ver" Value="VerPP" />
                                    <telerik:RadMenuItem Text="Editar" Value="EditarPP" />--%>
                                </Items>
                            </telerik:RadContextMenu>

                            <script type="text/javascript">
                                var $ = $telerik.$;

                                function OnClientItemClicked(sender, args) {
                                    var item = args.get_item(),
                                        itemValue = item.get_value(),
                                        itemText = item.get_text();
                                    NavegacionMenuInfo(itemValue);
                                }

                                //FUNCION NAVEGACION DEL MENU INFO PDE
                                NavegacionMenuInfo = function (accion) {
                                    //sessionStorage.setItem("modulo", modulo);
                                    var navigateURL = "";
                                    var vTipo = "Popup";
                                    switch (accion) {
                                        case "VerPP":
                                            var vTipoTransaccion = ('<%=vTipoTransaccion%>');
                                            var vIdComunicado = ('<%=vIdComunicado%>');

                                            if (vTipoTransaccion == "50") {
                                                //Redireccionar para ver el inventario de personal
                                                //var idServidor = ('//<//%= url %>');

                                                navigateURL = 'VentanaDescriptivoPuesto.aspx?Tipo=v' + '&pBotonesVisbles=false' + '&pBotonesVisblesRH=false';
                                                vTipo = "Popup";
                                            } else {
                                                var url = "";
                                                url = 'http://' + document.location.hostname + "/Sigein/Catalogos/DescriptivoPuesto/CatPuestoPde.aspx?Tipo=v" + "&pBotonesVisbles=false" + "&pBotonesVisblesRH=false" + "&BotonOk=botonOkNo" + "&PrimeraVez=SiPrimeraVez" + "&IdComunicado=" + vIdComunicado;
                                                navigateURL = url;
                                                vTipo = "Popup";
                                            }
                                            //Redireccionar para ver Perfil del puesto

                                            break;
                                        case "VerIP":
                                            var vTipoTransaccion = ('<%=vTipoTransaccion%>');
                                            var vIdComunicado = ('<%=vIdComunicado%>');

                                            if (vTipoTransaccion == "50") {
                                                //Redireccionar para ver el inventario de personal
                                                //var idServidor = ('//<//%= url %>');
                                                navigateURL = 'VentanaInventarioPersonal.aspx?Tipo=v' + '&pBotonesVisbles=false';
                                                // navigateURL = idServidor+'?Tipo=v' + '&pBotonesVisbles=false';
                                                vTipo = "Popup";
                                            } else {

                                                var url = "";
                                                url = 'http://' + document.location.hostname + "/Sigein/Catalogos/CatEmpleadoPde.aspx?Tipo=v" + '&pBotonesVisbles=false' + '&BotonOk=botonOkNo' + '&PrimeraVez=SiPrimeraVez' + '&IdComunicado=' + vIdComunicado;
                                                navigateURL = url;
                                                vTipo = "Popup";
                                            }

                                            break;
                                        case "EditarIP":
                                            ////Redireccionar para editar el inventario de personal
                                            //navigateURL = 'VentanaInventarioPersonal.aspx?Tipo=e' + '&pBotonesVisbles=false';
                                            //vTipo = "Popup";
                                            //break;
                                            var vTipoTransaccion = ('<%=vTipoTransaccion%>');
                                            var vIdComunicado = ('<%=vIdComunicado%>');

                                            if (vTipoTransaccion == "50") {
                                                //Redireccionar para ver el inventario de personal
                                                //var idServidor = ('//<//%= url %>');
                                                navigateURL = 'VentanaInventarioPersonal.aspx?Tipo=e' + '&pBotonesVisbles=false';
                                                // navigateURL = idServidor+'?Tipo=v' + '&pBotonesVisbles=false';
                                                vTipo = "Popup";
                                            } else {

                                                var url = "";
                                                url = 'http://' + document.location.hostname + "/Sigein/Catalogos/CatEmpleadoPde.aspx?Tipo=e" + '&pBotonesVisbles=false' + '&BotonOk=botonOkSi' + '&PrimeraVez=SiPrimeraVez' + '&IdComunicado=' + vIdComunicado;
                                                navigateURL = url;
                                                vTipo = "Popup";
                                            }
                                            break;
                                        case "EditarPP":
                                            ////Redireccionar para editar el perfil del puesto
                                            //navigateURL = 'VentanaDescriptivoPuesto.aspx?Tipo=e' + '&pBotonesVisbles=false' + '&pBotonesVisblesRH=false';
                                            //vTipo = "Popup";
                                            //break;
                                            var vTipoTransaccion = ('<%=vTipoTransaccion%>');
                                            var vIdComunicado = ('<%=vIdComunicado%>');

                                            if (vTipoTransaccion == "50") {
                                                //Redireccionar para ver el inventario de personal
                                                //var idServidor = ('//<//%= url %>');

                                                navigateURL = 'VentanaDescriptivoPuesto.aspx?Tipo=e' + '&pBotonesVisbles=false' + '&pBotonesVisblesRH=false';
                                                vTipo = "Popup";
                                            } else {

                                                var url = "";
                                                url = 'http://' + document.location.hostname + "/Sigein/Catalogos/DescriptivoPuesto/CatPuestoPde.aspx?Tipo=e" + "&pBotonesVisbles=false" + "&pBotonesVisblesRH=false" + "&BotonOk=botonOkSi" + "&PrimeraVez=SiPrimeraVez" + '&IdComunicado=' + vIdComunicado;
                                                navigateURL = url;
                                                vTipo = "Popup";
                                            }
                                            //Redireccionar para ver Perfil del puesto

                                            break;
                                        case "VerFD":
                                            navigateURL = '/PDE/Configuracion.aspx';
                                            vTipo = "Ventana";
                                            break;

                                    }
                                    if (vTipo == "Popup") {
                                        OpenWindow(navigateURL);
                                    }
                                    else {
                                        if (navigateURL.length > 0)
                                            NavegacionInfo(1, navigateURL);
                                    }
                                }

                                //NAVEGACION ENTRE PAGINAS
                                NavegacionInfo = function (segundos, pagina) {
                                    $telerik.$("body").fadeOut("slow");
                                    setTimeout(function () {
                                        location.href = pagina;
                                    }, (parseInt(segundos) * 1000));
                                }


                                function ShowEditFieldForm() {
                                    OpenWindowCampo("edit");
                                    return false;
                                }

                                function OpenWindow(vURL) {
                                    //alert(vURL);
                                    //vURL = vURL + "?PlantillaId=" + vIdPlantilla + "&AccionCl=" + pClAccion + "&PlantillaTipoCl=" + vClTipoPlantilla;

                                    var windowProperties = {
                                        width: document.documentElement.clientWidth - 20,
                                        height: document.documentElement.clientHeight - 20
                                    };
                                    openChildDialog(vURL, "winInventarioPersonal", "Inventario de personal y descriptivo de puesto", windowProperties);
                                }
                                function onCloseWindow(oWnd, args) {

                                }

                            </script>
                        </div>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
        </telerik:LayoutRow>
    </telerik:RadPageLayout>

    <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>

            <telerik:RadWindow ID="winInventarioPersonal" runat="server" Title="Inventario de personal" Behaviors="Close,Reload" Modal="true" VisibleStatusbar="false" OnClientClose="onCloseWindow"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccionPuestos" runat="server" Title="Seleccionar Jefe inmediato" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close" OnClientClose="returnDataToParentPopup"></telerik:RadWindow>
            <telerik:RadWindow
                ID="rwVentanaEditarNotificaciones3"
                runat="server"
                VisibleStatusbar="false"
                ShowContentDuringLoad="true"
                Behaviors="Close"
                Modal="true"
                ReloadOnShow="false"
                AutoSize="false">
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



</asp:Content>
