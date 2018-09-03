<%@ Page Title="" Language="C#" MasterPageFile="~/PDE/MenuPDE.master" AutoEventWireup="true" CodeBehind="VentanaNotificacionesYTramites.aspx.cs" Inherits="SIGE.WebApp.PDE.Ventana_NotificacionesYTramites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        function OpenNotificacionesARRHH() {
            OpenWindow();
        }

        function OpenEdicionDeTramites() {
            OpenWindowEdicionDeTramites();
        }

        function OpenWindow() {
            var vURL = "VentanaNotificacionesRRHH.aspx";
            var vTitulo = "Notificaciones a RRHH";
            var oWin = window.radopen(vURL, "rwVentanaNotificacionesRRHH", document.documentElement.clientWidth - 500, document.documentElement.clientHeight - 80);
            oWin.set_title(vTitulo);

        }

        function OpenWindowFormatosDescargables(pIndice) {
            var vURL = "VentanaFormatosDescargables.aspx?Indice=" + pIndice;
            var vTitulo = "Formatos Descargables";
            var oWin = window.radopen(vURL, "rwVentanaFormatosDescargables", document.documentElement.clientWidth - 30, document.documentElement.clientHeight - 30);

            oWin.set_title(vTitulo);

        }
        function onCloseWindow(sender, args) {
        }


        function GetWindowProperties() {
            return {
                width: document.documentElement.clientWidth - 70,
                height: document.documentElement.clientHeight - 70
            };
        }


        function useDataFromChild(pDato) {
            if (pDato != null) {
            }
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
                            <label class="labelTitulo" style="width: 100%">Notificaciones y trámites</label>
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
                            <div class="col-xs-12 col-lg-9 col-md-9 col-sm-9" style="margin-top: 15px">
                                <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top: 10px" id="divIP">
                                    <div style="position: relative; cursor: pointer; overflow: hidden;">
                                        <img id="imgConsultasInteligentes" src="../Assets/images/menu/Notificaciones.jpg" class="img-responsive" onclick="OpenNotificacionesARRHH()" />
                                    </div>
                                    <div style="background: #DE1648; width: 100%; text-align: center; color: #FFF;">Notificaciones a RRHH</div>
                                </div>
                                <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top: 10px" id="divPP">
                                    <div style="position: relative; cursor: pointer; overflow: hidden;">
                                        <img id="imgReportesPersonalizados" src="../Assets/images/menu/Tramite.jpg" class="img-responsive" onclick="OpenWindowFormatosDescargables(0)" />
                                    </div>
                                    <div style="background: #DE1648; width: 100%; text-align: center; color: #FFF;">Formatos</div>
                                    <div style="display: none">
                                    </div>
                                </div>
                                <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top: 10px">
                                    <div style="position: relative; cursor: pointer; overflow: hidden;">
                                        <img id="imgPuntoDeEncuentro" src="../Assets/images/menu/Tramite.jpg" class="img-responsive" onclick="OpenWindowFormatosDescargables(1)" />
                                    </div>
                                    <div style="background: #DE1648; width: 100%; text-align: center; color: #FFF;">Trámites</div>
                                </div>
                            </div>
                        </div>
                        <telerik:RadWindowManager ID="rwmNotificacionesyTramites" runat="server" EnableShadow="true">
                            <Windows>
                                <telerik:RadWindow
                                    ID="rwVentanaNotificacionesRRHH"
                                    runat="server"
                                    VisibleStatusbar="false"
                                    ShowContentDuringLoad="true"
                                    Behaviors="Close"
                                    Modal="true"
                                    ReloadOnShow="false"
                                    AutoSize="false">
                                </telerik:RadWindow>
                            </Windows>

                            <Windows>
                                <telerik:RadWindow
                                    ID="rwVentanaFormatosDescargables"
                                    runat="server"
                                    VisibleStatusbar="false"
                                    ShowContentDuringLoad="true"
                                    Behaviors="Close"
                                    Modal="true"
                                    ReloadOnShow="false"
                                    AutoSize="false">
                                </telerik:RadWindow>
                            </Windows>

                            <Windows>
                                <telerik:RadWindow
                                    ID="rwVentanaEditarNotificaciones2"
                                    runat="server"
                                    VisibleStatusbar="false"
                                    ShowContentDuringLoad="true"
                                    Behaviors="Close"
                                    Modal="true"
                                    ReloadOnShow="false"
                                    AutoSize="false">
                                </telerik:RadWindow>
                            </Windows>
                             <Windows>
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
             <telerik:RadWindow ID="winSeleccion" runat="server" Title="Seleccionar" Height="600px" Width="600px" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="false" Modal="true" Behaviors="Close"></telerik:RadWindow>
            
        </Windows>
                        </telerik:RadWindowManager>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
        </telerik:LayoutRow>
    </telerik:RadPageLayout>
</asp:Content>
