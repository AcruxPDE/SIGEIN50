<%@ Page Title="" Language="C#" MasterPageFile="~/ContextHTML.master" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="SIGE.WebApp.MenuPrincipal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContexto" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">


    <title>Sistema SIGEIN</title>
    <link href='Assets/images/Icono.png' rel='shortcut icon' />
    <!-- LIBRERIA JS -->
    <script src="Assets/js/jquery.min.js" class="librerias" data-tipo="js"></script>
    <script src="Assets/js/bootstrap.js" class="librerias" data-tipo="js"></script>
    <script src="Assets/js/appIndex.js"></script>

    <!-- LIBRERIA CSS -->
    <link href="Assets/css/bootstrap.min.css" rel="stylesheet" class="librerias" data-tipo="css" />
    <link href="Assets/library/font-awesome/css/font-awesome.css" rel="stylesheet" class="librerias" data-tipo="css" />
    <link href="Assets/css/animate.css" rel="stylesheet" class="librerias" data-tipo="css" />
    <link href="Assets/css/estilo.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContexto" runat="server">

    <div class="container">

         <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="text-align:left" id="Logo">
                    <img src="Assets/images/LogotipoNombre.png" alt="Logo" />
            </div>
             <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="text-align:left" id="MenuAccesoRapido">
                    <div>
                        <div style="text-align: right;cursor:pointer;margin-top:10px" id="menuaccesosrapidos">
                                <i class="fa fa-file zoom"  style="font-size:25px;color: #6F7172;margin-left:20px" data-toggle="popover" data-trigger="hover" data-placement="bottom" data-content="Solicitudes de empleo" title="Menú"></i>
                                <i class="fa fa-users zoom" style="font-size:25px;color: #6F7172;margin-left:20px" data-toggle="popover" data-trigger="hover" data-placement="bottom" data-content="Inventario de personal" title="Menú"></i> 
                                <i class="fa fa-book zoom" style="font-size:25px;color: #6F7172;margin-left:20px" data-toggle="popover" data-trigger="hover" data-placement="bottom" data-content="Descripciones de puesto" title="Menú"></i>
                                <i class="fa fa-line-chart zoom" style="font-size:25px;color: #6F7172;margin-left:20px" data-toggle="popover" data-trigger="hover" data-placement="bottom" data-content="Competencias" title="Menú"></i>
                                <i class="fa fa-cog zoom" style="font-size:25px;color: #6F7172;margin-left:20px" data-toggle="popover" data-trigger="hover" data-placement="bottom" data-content="Configuración" title="Menú"></i>
                                <i class="fa fa-sign-out zoom" style="font-size:25px;color: #6F7172;margin-left:20px" onclick="Salir();" data-toggle="popover" data-trigger="hover" data-placement="bottom" data-content="Salir del sistema" title="Menú"></i>
                         </div>
                    </div>
            </div>
        </div>

         <ul class="nav nav-tabs">
              <li class="active"><a data-toggle="tab" href="#profundamentales">Procesos fundamentales</a></li>
              <li><a data-toggle="tab" href="#modapoyo">Módulos de apoyo</a></li>
              <li><a data-toggle="tab" href="#catalogos">Catálogos </a></li>
            </ul>

         <div class="tab-content">
              <div id="profundamentales" class="tab-pane fade in active">
                 <!-- contenido pestaña procesos fundamentales -->
                     <div class="container" id="Div1">

            <div class="col-xs-12 col-lg-3 col-md-3 col-sm-3" style="margin-top:15px">
                <div class="col-xs-12 col-lg-12 col-ms-12 col-md-12" style="margin-top:10px">

                    <div style="position:relative;cursor: pointer;overflow: hidden;">
                    <img src="Assets/images/menu/IntegraciondePersonal600x344.png" class="img-responsive"  onclick="NavegacionMenu(1)" />
                    <div class="datos menuPrincipalCaracteristicas" style="width: 100%;height: 100%;position: absolute;bottom: 0px;text-align: center;"  onclick="NavegacionMenu(1)">
                        <span style="color:#FFF;font-weight:bold">
                            Integración de personal
                        </span>
                            <div style="clear:both"></div>
                            <ul style="text-align:left;color:#FFF">
                                <li>Solicitud y cartera electrónica</li>
                                <li>Psicometría y evaluación de candidatos</li>
                                <li>Diseño de perfiles de puesto</li> 
                            </ul>
                    </div>
                    </div>
                    <div style="background:#4D8900; width:100%; height:10px"></div>

                    </div>
                    <div class="col-xs-12 col-lg-12 col-ms-12 col-md-12" style="margin-top:24px">

                        <div style="position:relative;cursor: pointer;overflow: hidden;">
                        <img src="Assets/images/menu/FormacionyDesarrollo600x344.png" class="img-responsive" onclick="NavegacionMenu(2)" />
                        <div class="datos menuPrincipalCaracteristicas" style="width: 100%;height: 100%;position: absolute;bottom: 0px;text-align: center;" onclick="NavegacionMenu(2)">
                            <span style="color:#FFF;font-weight:bold">
                                Formación y desarrollo
                            </span>
                            <div style="clear:both"></div>
                            <ul style="text-align:left;color:#FFF">
                                <li>Detección de necesidades de capacitación</li>
                                <li>Programas de formación</li>
                                <li>Plan de vida y carrera</li>
                                <li>Plantillas de reemplazo</li>
                            </ul>
                        </div>
                    </div>
                    <div style="background:#FF7400; width:100%; height:10px"></div>

                    </div>
                
            </div>

            <div class="col-xs-12 col-lg-6 col-md-6 col-sm-6" style="margin-top:15px;">
                <div class="col-xs-12 col-lg-12 col-md-12 col-sm-12">
                     <div style="position:relative;cursor: pointer;overflow: hidden;margin-top:10px">
                    <img src="Assets/images/menu/MetodologiaparalaCompensacion600x344.png" class="img-responsive" onclick="NavegacionMenu(3)" />
                    <div class="datosMenuPrincipal menuPrincipalCaracteristicas" style="width: 100%;height: 100%;position: absolute;bottom: 0px;text-align: center;" onclick="NavegacionMenu(3)">
                        <span style="color:#FFF;font-weight:bold">
                            Metodología para la compensación
                        </span>
                        <div style="clear:both"></div>
                        <ul style="text-align:left;color:#FFF">
                            <li>Valuación de puestos</li>
                            <li>Tabulador de sueldos</li>
                            <li>Sistema de bonos e incentivos con base en el desempeño</li
                        </ul>
                    </div>
                    </div>
                   <div style="background:#0087CF; width:100%; height:10px"></div>
                </div>
                
            </div>

            <div class="col-xs-12 col-lg-3 col-md-3 col-sm-3" style="margin-top:15px">
                <div class="col-xs-12 col-lg-12 col-ms-12 col-md-12" style="margin-top:10px">

                    <div style="position:relative;cursor: pointer;overflow: hidden;">
                    <img src="Assets/images/menu/EvaluacionOrganizacional600x344.png" class="img-responsive"  onclick="NavegacionMenu(4)" />
                    <div class="datos menuPrincipalCaracteristicas" style="width: 100%;height: 100%;position: absolute;bottom: 0px;text-align: center;"  onclick="NavegacionMenu(4)">
                        <span style="color:#FFF;font-weight:bold">
                            Evaluación organizacional
                        </span>
                        <div style="clear:both"></div>
                        <ul style="text-align:left;color:#FFF">
                            <li>Evaluación del desempeño</li>
                            <li>Clima laboral</li>
                            <li>Análisis de rotación</li>
                        </ul>
                    </div>
                    </div>
                    <div style="background:#A20804; width:100%; height:10px"></div>

                    </div>
                    <div class="col-xs-12 col-lg-12 col-ms-12 col-md-12" style="margin-top:24px" >

                        <div style="position:relative;cursor: pointer;overflow: hidden;">
                        <img src="Assets/images/menu/Nomina600x344.png" class="img-responsive" onclick="NavegacionMenu(5)" />
                        <div class="datos menuPrincipalCaracteristicas" style="width: 100%;height: 100%;position: absolute;bottom: 0px;text-align: center;" onclick="NavegacionMenu(5)">
                            <span style="color:#FFF;font-weight:bold">
                                Nómina
                            </span>
                            <div style="clear:both"></div>
                            <ul style="text-align:left;color:#fff">
                                <li>Herramienta de control de personal y pago de nómina</li>
                            </ul>

                        </div>
                    </div>
                    <div style="background:#79026F; width:100%; height:10px"></div>

                    </div>
                
            </div>

        </div>
              </div>
              <div id="modapoyo" class="tab-pane fade">
               <!-- contenido pestaña modulos de apoyo -->

                   <div class="col-xs-12 col-lg-9 col-md-9 col-sm-9" style="margin-top:15px">

                    <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top:10px">
                        <div style="position:relative;cursor: pointer;overflow: hidden;">
                        <img src="Assets/images/menu/ConsultasInteligentes600x344.png" class="img-responsive" />
                        <div class="datosmodadicionales menuPrincipalCaracteristicas" style="width: 100%;height: 100%;position: absolute;bottom: 0px;text-align: center;">
                            <span style="color:#FFF;font-weight:bold">
                                Consultas inteligentes
                            </span>
                        </div>
                        </div>
                        <div style="background:red; width:100%; height:10px"></div>
                    </div>

                    <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top:10px">
                        <div style="position:relative;cursor: pointer;overflow: hidden;">
                        <img src="Assets/images/menu/ReportesPersonalizados600x344.png" class="img-responsive" />
                        <div class="datosmodadicionales menuPrincipalCaracteristicas" style="width: 100%;height: 100%;position: absolute;bottom: 0px;text-align: center;">
                            <span style="color:#FFF;font-weight:bold">
                                 Reportes personalizados
                            </span>
                        </div>
                        </div>
                        <div style="background:red; width:100%; height:10px"></div>
                    </div>

                    <div class="col-xs-12 col-lg-4 col-ms-4 col-md-4" style="margin-top:10px">
                        <div style="position:relative;cursor: pointer;overflow: hidden;">
                        <img src="Assets/images/menu/PuntodeEncuentro600x344.png" class="img-responsive" />
                        <div class="datosmodadicionales menuPrincipalCaracteristicas" style="width: 100%;height: 100%;position: absolute;bottom: 0px;text-align: center;">
                            <span style="color:#FFF;font-weight:bold">
                                Punto de encuentro
                            </span>
                        </div>
                        </div>
                        <div style="background:red; width:100%; height:10px"></div>
                    </div>
                
            </div>

              </div>
              <div id="catalogos" class="tab-pane fade">
                <!-- contenido pestaña de catalogos -->
                  <div class="container">

                      <div style="margin:20px">
                          <div class="col-xs-6 col-lg-2 col-md-2 col-sm-6 zoom" style="text-align: center;cursor:pointer;margin-top:10px">
                               <i class="fa fa-file"  style="font-size:75px;color: #6F7172;"></i>
                               <div style="clear:both"></div>
                               <span>Solicitudes de empleo</span>
                          </div>
                          <div class="col-xs-6 col-lg-2 col-md-2 col-sm-6 zoom" style="text-align: center;cursor:pointer;margin-top:10px">
                               <i class="fa fa-users" style="font-size:75px;color: #6F7172;"></i> 
                               <div style="clear:both"></div>
                               <span>Inventario de personal</span>
                          </div>
                          <div class="col-xs-6 col-lg-2 col-md-2 col-sm-6 zoom" style="text-align: center;cursor:pointer;margin-top:10px">
                               <i class="fa fa-book" style="font-size:75px;color: #6F7172;"></i> 
                              <div style="clear:both"></div>
                               <span>Descripciones de puesto</span>
                          </div>
                          <div class="col-xs-6 col-lg-2 col-md-2 col-sm-6 zoom" style="text-align: center;cursor:pointer;margin-top:10px">
                               <i class="fa fa-line-chart" style="font-size:75px;color: #6F7172;"></i>
                               <div style="clear:both"></div>
                               <span>Competencias</span>
                          </div>
                          <div class="col-xs-6 col-lg-2 col-md-2 col-sm-6 zoom" style="text-align: center;cursor:pointer;margin-top:10px"> 
                              <i class="fa fa-cog"   style="font-size:75px;color: #6F7172;"></i>
                              <div style="clear:both"></div>
                               <span>Configuración</span>
                          </div>
                    </div>
                     

                  </div>
                  
              </div>
            </div>

     </div>

</asp:Content>
