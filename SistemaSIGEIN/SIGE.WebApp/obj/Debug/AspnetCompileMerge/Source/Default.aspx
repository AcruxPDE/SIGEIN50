<%@ Page Title="" Language="C#" MasterPageFile="~/AppSIGE.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SIGE.WebApp.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <script src="Assets/js/appIndex.js"></script>
    <link href="Assets/css/estilo.css" rel="stylesheet" />--%>

    <script type="text/javascript">

        function AccesoNomina() {
            var vFgNomina = '<%= vFgAccesoNomina %>';
            if (vFgNomina == "1") {
                var arrUrl = window.location.href.split('/');
                var vUrl = arrUrl[0] + '//' + arrUrl[2] + '/NOMINA/Menu.aspx?clUsuario=' + '<%= vClUsuario %>';
                window.open(vUrl, '_blank');
                //var vAjaxManager = $find('<= ramMenu.ClientID %>');
                //vAjaxManager.ajaxRequest(vUrl);
            }
            else {
                alert(vFgNomina)
            }
        }

    </script>
    <style>
        MenuPrincipal {
            width: 600px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramMenu" runat="server" OnAjaxRequest="ramMenu_AjaxRequest">
    </telerik:RadAjaxManager>--%>
    <telerik:RadToolTip ID="rttIntegracionDePersonal" runat="server" ShowDelay="1000" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
        TargetControlID="imgIntegracionPersonal" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_IP">
        <span style="font-weight: bold">Integración de personal
        </span>
        <div style="clear: both"></div>
        <ul style="text-align: left;">
            <li>Solicitud y cartera electrónica</li>
            <li>Psicometría y evaluación de candidatos</li>
            <li>Resultados traducidos a competencias</li>
            <li>Diseño de perfiles de puesto</li>
            <li>Administración de requisiciones y vacantes</li>
        </ul>
    </telerik:RadToolTip>
    <telerik:RadToolTip ID="rttFormacionYDesarrollo" runat="server" ShowDelay="1000" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
        TargetControlID="imgFormacionYDesarrollo" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_FD">
        <span style="color: #FFF; font-weight: bold">Formación y desarrollo</span>
        <div style="clear: both"></div>
        <ul style="text-align: left; color: #FFF">
            <li>Detección de necesidades de capacitación</li>
            <li>Evaluaciones 90, 180 y 360 grados</li>
            <li>Evaluación de competencias</li>
            <li>Programas de formación</li>
            <li>Plan de vida y carrera</li>
            <li>Planes de sucesión</li>
            <li>Plantillas de reemplazo</li>
        </ul>
    </telerik:RadToolTip>
    <telerik:RadToolTip ID="rttEvaluacionOrganizacional" runat="server" ShowDelay="1000" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
        TargetControlID="imgEvaluacionOrganizacional" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_EO">
        <span style="color: #FFF; font-weight: bold">Evaluación organizacional</span>
        <div style="clear: both"></div>
        <ul style="text-align: left; color: #FFF">
            <li>Alineación de metas a la estrategia</li>
            <li>Evaluación del desempeño</li>
            <li>Sistema de bonos e incentivos</li>
            <li>Clima laboral</li>
            <li>Análisis de rotación</li>
            <li>Entrevistas de salida</li>
        </ul>
    </telerik:RadToolTip>
    <telerik:RadToolTip ID="rttNomina" runat="server" ShowDelay="1000" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
        TargetControlID="imgNomina" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_N">
        <span style="color: #FFF; font-weight: bold">Administración de nóminas e incidencias</span>
        <div style="clear: both"></div>
        <ul style="text-align: left; color: #fff">
            <li>Integración con reloj checador</li>
            <li>Conciliación con el sistema SUA</li>
            <li>Control y administración del programa de vacaciones</li>
            <li>Control de créditos, préstamos y resguardos</li>
        </ul>
    </telerik:RadToolTip>
    <telerik:RadToolTip ID="rttMetodologiaCompensacion" runat="server" ShowDelay="1000" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
        TargetControlID="imgMetodologiaCompensacion" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_MC">
        <span style="color: #FFF; font-weight: bold">Metodología para la compensación</span>
        <div style="clear: both"></div>
        <ul style="text-align: left; color: #FFF">
            <li>Valuación de puestos</li>
            <li>Tabulador de sueldos</li>
            <li>Mercado salarial</li>
            <li>Planeación de incrementos salariales</li>
        </ul>
    </telerik:RadToolTip>
    <telerik:RadToolTip ID="rttConsultasInteligentes" runat="server" ShowDelay="1000" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
        TargetControlID="imgConsultasInteligentes" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_GR" OffsetY="20">
        <span style="font-weight: bold">Consultas inteligentes</span>
        <div style="clear: both"></div>
        <ul style="text-align: left;">
            <li>Proporciona información consolidada de forma dinámica</li>
            <li>Análisis profundo de la información con un solo click</li>
            <li>Reportes de forma sumarizada</li>
            <li>Información de indicadores orientados a la toma de decisiones</li>
        </ul>
    </telerik:RadToolTip>
    <telerik:RadToolTip ID="rttReportesPersonalizados" runat="server" ShowDelay="1000" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
        TargetControlID="imgReportesPersonalizados" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_GR" OffsetY="20">
        <span style="font-weight: bold">Reportes personalizados</span>
        <div style="clear: both"></div>
        <ul style="text-align: left;">
            <li>Elabora reportes a la medida</li>
            <li>Herramienta flexible, amigable y de fácil manejo</li>
            <li>Los reportes se pueden exportar como PDF, XPS, Excel, Word, etc.</li>
        </ul>
    </telerik:RadToolTip>
    <telerik:RadToolTip ID="rttPuntoDeEncuentro" runat="server" ShowDelay="1000" ShowEvent="OnMouseOver" RelativeTo="Element" Animation="Resize"
        TargetControlID="imgPuntoDeEncuentro" IsClientID="true" HideEvent="LeaveTargetAndToolTip" Position="TopCenter" CssClass="RadToolTip_GR" OffsetY="20">
        <span style="font-weight: bold">Punto de encuentro</span>
        <div style="clear: both"></div>
        <ul style="text-align: left;">
            <li>Canal de comunicación con todos los empleados</li>
            <li>Facilita la comunicación interactiva entre colaboradores y áreas RRHH</li>
            <li>Permite el diseño de los tableros de comunicación</li>
            <li>Facilita la actualización de la información de los empleados</li>
        </ul>
    </telerik:RadToolTip>
    <div id="mnuPopUp" style="border: 1px solid white; left: 708px; top: 244px; width: 220px; height: 100px; text-align: center; color: white; visibility: hidden; position: absolute; z-index: 10; background-color: #A20804;">
        <table width="100%">
            <tbody>
                <tr style="font-weight: bold; background-color: maroon;">
                    <td>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="text-align: center;"><span style="color: white; font-weight: bold;">Evaluación organizacional</span></td>
                                    <td style="text-align: right; padding-right: 2px;"><span style="color: white; font-weight: bold;" onclick="ClosePopup();">X</span></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr style="text-align: left;">
                    <td style="padding-left: 15px;" id="mnuPop1" onmouseover="PopupIn(this);" onmouseout="PopupOut(this);" onclick="PopupClick(this, '<%= vFgModuloCL %>');">Clima laboral</td>
                </tr>
                <tr style="text-align: left; padding-left: 5px;">
                    <td style="padding-left: 15px;" id="mnuPop2" onmouseover="PopupIn(this);" onmouseout="PopupOut(this);" onclick="PopupClick(this, '<%= vFgModuloED %>');">Evaluación del desempeño</td>
                </tr>
                <tr style="text-align: left; padding-left: 5px;">
                    <td style="padding-left: 15px;" id="mnuPop3" onmouseover="PopupIn(this);" onmouseout="PopupOut(this);" onclick="PopupClick(this, '<%= vFgModuloRDP %>');">Análisis de rotación</td>
                </tr>
            </tbody>
        </table>
    </div>
    <telerik:RadPageLayout ID="rplPrincipal" runat="server" GridType="Fluid" ShowGrid="true" HtmlTag="None">
        <telerik:LayoutRow RowType="Generic">
            <Rows>
                <telerik:LayoutRow RowType="Generic">
                    <Content>
                        <div style="float: left; padding: 10px;">
                            <%--se agrega un width y heigth para hacer mas pequeña la imagen--%>
                            <img src="Assets/images/LogotipoNombre.png" width="200" height="132" alt="Logo" />
                        </div>
                        <div runat="server" id="divMenu"></div>
                    </Content>
                </telerik:LayoutRow>
                <telerik:LayoutRow RowType="Generic">
                    <Rows>
                        <telerik:LayoutRow RowType="Container" WrapperHtmlTag="Div">
                            <Columns>
                                <telerik:LayoutColumn Span="12" SpanMd="12" SpanSm="12" SpanXs="12">
                                    <telerik:RadTabStrip ID="tabMenuPrincipal" runat="server" SelectedIndex="0" MultiPageID="mpgPrincipal">
                                        <Tabs>
                                            <telerik:RadTab Text="Procesos fundamentales"></telerik:RadTab>
                                            <telerik:RadTab Text="Módulos de apoyo"></telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                </telerik:LayoutColumn>
                            </Columns>
                        </telerik:LayoutRow>
                    </Rows>
                </telerik:LayoutRow>
                <telerik:LayoutRow RowType="Generic">
                    <Content>
                        <telerik:RadMultiPage ID="mpgPrincipal" runat="server" SelectedIndex="0" Height="100%">
                            <telerik:RadPageView ID="pvwMenuModulos" runat="server" Height="100%">
                                <div class="container" id="Div1">
                                    <div class="col-xs-12 col-lg-3 col-md-3 col-sm-3" style="margin-top: 15px">
                                        <div class="col-xs-12 col-lg-12 col-ms-12 col-md-12" style="margin-top: 10px">
                                            <div style="position: relative; cursor: pointer; overflow: hidden;">
                                                <img id="imgIntegracionPersonal" src="Assets/images/menu/IntegraciondePersonal600x344.png" class="img-responsive" onclick="NavegacionMenu(1,'<%= vFgModuloIDP %>')" />
                                            </div>
                                            <div style="background: #4D8900; width: 100%; text-align: center; color: #FFF;">Integración de personal</div>
                                        </div>
                                        <div class="col-xs-12 col-lg-12 col-ms-12 col-md-12" style="margin-top: 24px">
                                            <div style="position: relative; cursor: pointer; overflow: hidden;">
                                                <img id="imgFormacionYDesarrollo" src="Assets/images/menu/FormacionyDesarrollo600x344.png" class="img-responsive" onclick="NavegacionMenu(2, '<%= vFgModuloFYD %>')" />
                                            </div>
                                            <div style="background: #FF7400; width: 100%; text-align: center; color: #FFF;">Formación y desarrollo</div>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-lg-6 col-md-6 col-sm-6" style="margin-top: 15px;">
                                        <div class="col-xs-12 col-lg-12 col-md-12 col-sm-12">
                                            <div style="position: relative; cursor: pointer; overflow: hidden; margin-top: 10px">
                                                <img id="imgEvaluacionOrganizacional" src="Assets/images/menu/EvaluacionOrganizacional600x344.png" class="img-responsive" onclick="NavegacionMenu(4, 1)" />
                                            </div>
                                            <div style="background: #A20804; width: 100%; text-align: center; color: #FFF;">Evaluación organizacional</div>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-lg-3 col-md-3 col-sm-3" style="margin-top: 15px">
                                        <div class="col-xs-12 col-lg-12 col-ms-12 col-md-12" style="margin-top: 10px">
                                            <div style="position: relative; cursor: pointer; overflow: hidden;">
                                                <img id="imgMetodologiaCompensacion" src="Assets/images/menu/MetodologiaparalaCompensacion600x344.png" class="img-responsive" onclick="NavegacionMenu(3, '<%= vFgModuloMPC %>')" />
                                            </div>
                                            <div style="background: #0087CF; width: 100%; text-align: center; color: #FFF;">Metodología para la compensación</div>
                                        </div>
                                        <div class="col-xs-12 col-lg-12 col-ms-12 col-md-12" style="margin-top: 24px">
                                            <div style="position: relative; cursor: pointer; overflow: hidden;">
                                                <%--                          <img id="imgNomina" src="Assets/images/menu/Nomina600x344.png" class="img-responsive" onclick="NavegacionMenu(5, '<%= vFgAccesoNomina %>', '<%= vClUsuario %>')" />--%>
                                                <img id="imgNomina" src="Assets/images/menu/Nomina600x344.png" class="img-responsive" onclick="AccesoNomina()" />
                                            </div>
                                            <div style="background: #79026F; width: 100%; text-align: center; color: #FFF;">Nómina</div>
                                        </div>
                                    </div>
                                </div>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pvwMenuApoyo" runat="server" Height="100%">
                                <div class="container" id="Div2">
                                    <div class="col-xs-12 col-lg-9 col-md-9 col-sm-9" style="margin-top: 15px">
                                        <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top: 10px">
                                            <div style="position: relative; cursor: pointer; overflow: hidden;">
                                                <img id="imgConsultasInteligentes" src="Assets/images/menu/ConsultasInteligentes600x344.png" class="img-responsive" onclick="NavegacionMenu(7, '<%= vFgModuloCI %>')" />
                                            </div>
                                            <div style="background: red; width: 100%; text-align: center; color: #FFF;">Consultas inteligentes</div>
                                        </div>
                                        <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top: 10px">
                                            <div style="position: relative; cursor: pointer; overflow: hidden;">
                                                <img id="imgReportesPersonalizados" src="Assets/images/menu/ReportesPersonalizados600x344.png" class="img-responsive" onclick="NavegacionMenu(8, '<%= vFgModuloRP %>')" />
                                            </div>
                                            <div style="background: red; width: 100%; text-align: center; color: #FFF;">Reportes personalizados</div>
                                        </div>
                                        <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top: 10px">
                                            <div style="position: relative; cursor: pointer; overflow: hidden;">
                                                <img id="imgPuntoDeEncuentro" src="Assets/images/menu/PuntodeEncuentro600x344.png" class="img-responsive" onclick="NavegacionMenu(6, '<%= vFgModuloPDE %>')" />
                                            </div>
                                            <div style="background: red; width: 100%; text-align: center; color: #FFF;">Punto de encuentro</div>
                                        </div>
                                    </div>
                                </div>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
            <%--Se agrega para ver la banda con el nombre de la empresa--%>
            <Rows>
                <telerik:LayoutRow RowType="Generic">
                    <Content>
                        <div class="PiedePagina">
                            <div class="container">
                                <div class="col-md-4 col-xs-12">
                                </div>
                                <div class="col-md-4 col-xs-12">
                                    <span id="lblEmpresa" runat="server"></span>
                                </div>
                                <div class="col-md-4 col-xs-12">
                                </div>
                            </div>
                        </div>
                    </Content>
                </telerik:LayoutRow>
            </Rows>
        </telerik:LayoutRow>
    </telerik:RadPageLayout>
</asp:Content>
