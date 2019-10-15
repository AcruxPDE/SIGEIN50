<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/AppSIGE.Master" CodeBehind="SolicitudExterna.aspx.cs" Inherits="SIGE.WebApp.IDP.Solicitud.SolicitudExterna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveClientUrl("~/Assets/css/Temas/") %><%= cssModulo %>' rel="stylesheet" type="text/css" />
    <link href="<%# ResolveClientUrl(String.Format("~/Assets/css/estilo.css?v={0}",DateTime.Now.ToString("yyyyMMddHHmmss") )) %>" rel="stylesheet" />
    <script src="<%# ResolveClientUrl("~/Assets/js/appPruebas.js") %>"></script>
    <style type="text/css">
       .ruBrowse
       {
           
           width: 150px !important;
       }

        .DivFotoCss {
           background: #fafafa; 
           position: absolute; 
           right: 0px; 
           margin-right: 50px; 
           border: 1px solid lightgray; 
           border-radius: 10px; 
           padding: 5px;
        }

         @media only screen and (max-width: 700px) {
            .DivFotoCss {
               background: #fafafa; 
               position: relative;
               right: 0px; 
               margin-right: 50px; 
               border: 1px solid lightgray; 
               border-radius: 10px; 
               padding: 5px;
               width:170px;
            }
        }

   </style>
    <script id="MyScript" type="text/javascript">

        function closeWindow() {
            GetRadWindow().close();
        }

        function OnClientBeforeClose(sender, args) {

            function confirmCallback(arg) {
                if (arg) {
                    closeWindow();
                }
            }

            radconfirm("¿Estás seguro que quieres salir de la pantalla? Si no has guardado los cambios se perderán", confirmCallback, 400, 170, null, "Aviso");
        }

        //function OpenImpresion() {
            //var myPageView = $find('<= mpgSolicitud.ClientID %>');
            // var myIframe = document.getElementById('ifrmPrint');
            // var pvContent = myPageView.get_selectedPageView().get_element().innerHTML;
            // var myDoc = (myIframe.contentWindow || myIframe.contentDocument);
            // if (myDoc.document) myDoc = myDoc.document;
            // myDoc.write("<html><head><title>Solicitud</title>");
            // myDoc.write("</head><body onload='this.focus(); this.print();'>");
            // myDoc.write(pvContent + "</body></html>");
            // myDoc.close(); (pvContent + "</body></html>");
            // myDoc.close();
         //   var myWindow = window.open("../VentanaImprimirSolicitud.aspx?SolicitudId=" + '<= vIdSolicitudVS %>', "MsgWindow", "width=650,height=650");
         //}

        function OpenSelectionWindowPrivacidad(pURL, pIdWindow, pTitle, pWindowProperties) {
            var currentWnd = GetRadWindow();
            var browserWnd = window;
            if (currentWnd)
                browserWnd = currentWnd.BrowserWindow;

            var windowProperties = {
                width: browserWnd.innerWidth - 20,
                height: browserWnd.innerHeight - 20
            };

            if (pWindowProperties)
                windowProperties = pWindowProperties;

            openChildDialog(pURL, pIdWindow, pTitle, windowProperties)
        }
        function RespuestaAvisoPrivacidad(oWnd, args) {
            var arg = args.get_argument();
            if (arg == 0) {
                closeWindow();
            }
        }

        var windowProperties = {
            width: 600,
            height: 600
        };

        function OpenSelectionWindow(sender, args) {
            var vLstEstados = $find("<%= ObtenerClientId(mpgSolicitud, "NB_ESTADO") %>");
            var vBtnEstados = $find("<%= ObtenerClientId(mpgSolicitud, "btnNB_ESTADO") %>");
            var vLstMunicipios = $find("<%= ObtenerClientId(mpgSolicitud, "NB_MUNICIPIO") %>");
            var vBtnMunicipios = $find("<%= ObtenerClientId(mpgSolicitud, "btnNB_MUNICIPIO") %>");
            var vLstColonia = $find("<%= ObtenerClientId(mpgSolicitud, "NB_COLONIA") %>");
            var vBtnColonia = $find("<%= ObtenerClientId(mpgSolicitud, "btnNB_COLONIA") %>");
            var vBtnCP = $find("<%= ObtenerClientId(mpgSolicitud, "btnCL_CODIGO_POSTAL")%>");
            var vLstEstadosNacimiento = $find("<%= ObtenerClientId(mpgSolicitud, "NB_ESTADO_NACIMIENTO") %>");
            var vBtnEstadosNacimiento = $find("<%= ObtenerClientId(mpgSolicitud, "btnNB_ESTADO_NACIMIENTO") %>");

            if (sender == vBtnEstados) {
                windowProperties.width = document.documentElement.clientWidth - 100;
                windowProperties.height = document.documentElement.clientHeight - 20;
                var myUrl = '<%= ResolveUrl("~/Comunes/SeleccionLocalizacion/SeleccionEstado.aspx") %>';
                openChildDialog(myUrl, "winSeleccion", "Selección de estado", windowProperties);
            }

            if (sender == vBtnMunicipios) {

                if (vLstEstados != null) {
                    //var clEstado = vLstEstados.get_selectedItem().get_value();
                    var nbEstado = vLstEstados.get_selectedItem().get_value();
                    windowProperties.width = document.documentElement.clientWidth - 100;
                    windowProperties.height = document.documentElement.clientHeight - 20;
                    //var myUrl = '<= ResolveUrl("~/Comunes/SeleccionLocalizacion/SeleccionMunicipio.aspx?ClEstado=") %>';
                    var myUrl = '<%= ResolveUrl("~/Comunes/SeleccionLocalizacion/SeleccionMunicipio.aspx?NbEstado=") %>';
                    //openChildDialog(myUrl + clEstado, "winSeleccion", "Selección de municipio", windowProperties);
                    openChildDialog(myUrl + nbEstado, "winSeleccion", "Selección de municipio", windowProperties);
                } else {
                    windowProperties.width = document.documentElement.clientWidth - 100;
                    windowProperties.height = document.documentElement.clientHeight - 20;
                    var myUrl = '<%= ResolveUrl("~/Comunes/SeleccionLocalizacion/SeleccionMunicipio.aspx") %>';
                    openChildDialog(myUrl, "winSeleccion", "Selección de municipio", windowProperties);
                }
            }

            if (sender == vBtnColonia) {

                if (vLstEstados != null && vLstMunicipios != null) {
                    //var clEstado = vLstEstados.get_selectedItem().get_value();
                    //var clMunicipio = vLstMunicipios.get_selectedItem().get_value();
                    var nbEstado = vLstEstados.get_selectedItem().get_value();
                    var nbMunicipio = vLstMunicipios.get_selectedItem().get_value();
                    windowProperties.width = document.documentElement.clientWidth - 100;
                    windowProperties.height = document.documentElement.clientHeight - 20;
                    //var myUrl = '<= ResolveUrl("~/Comunes/SeleccionLocalizacion/SeleccionColonia.aspx?ClEstado=") %>';
                    var myUrl = '<%= ResolveUrl("~/Comunes/SeleccionLocalizacion/SeleccionColonia.aspx?NbEstado=") %>';
                    //openChildDialog(myUrl + clEstado + "&ClMunicipio=" + clMunicipio, "winSeleccion", "Selección de colonia", windowProperties);
                    openChildDialog(myUrl + nbEstado + "&NbMunicipio=" + nbMunicipio, "winSeleccion", "Selección de colonia", windowProperties);
                } else if (vLstMunicipios != null) {
                    var nbMunicipio = vLstMunicipios.get_selectedItem().get_value();
                    windowProperties.width = document.documentElement.clientWidth - 100;
                    windowProperties.height = document.documentElement.clientHeight - 20;
                    var myUrl = '<%= ResolveUrl("~/Comunes/SeleccionLocalizacion/SeleccionColonia.aspx?NbMunicipio=") %>';
                    openChildDialog(myUrl + nbMunicipio, "winSeleccion", "Selección de colonia", windowProperties);
                } else {
                    windowProperties.width = document.documentElement.clientWidth - 100;
                    windowProperties.height = document.documentElement.clientHeight - 20;
                    var myUrl = '<%= ResolveUrl("~/Comunes/SeleccionLocalizacion/SeleccionColonia.aspx") %>';
                    openChildDialog(myUrl, "winSeleccion", "Selección de colonia", windowProperties);
                }
            }

            if (sender == vBtnCP) {
                windowProperties.width = document.documentElement.clientWidth - 100;
                windowProperties.height = document.documentElement.clientHeight - 20;
                var myUrl = '<%= ResolveUrl("~/Comunes/SeleccionLocalizacion/SeleccionCP.aspx?CatalogoCl=CODIGOPOSTAL") %>';
                openChildDialog(myUrl, "winSeleccion", "Selección de código postal", windowProperties);
            }

            if (sender == vBtnEstadosNacimiento) {
                windowProperties.width = document.documentElement.clientWidth - 100;
                windowProperties.height = document.documentElement.clientHeight - 20;
                var myUrl = '<%= ResolveUrl("~/Comunes/SeleccionLocalizacion/SeleccionEstado.aspx?CatalogoCl=ESTADONACIMIENTO") %>';
                openChildDialog(myUrl, "winSeleccion", "Selección de estado", windowProperties);
            }
        }

            function LoadValue(sender, args) {
                var items = sender.get_items();
                for (var i = 0; i < items.get_count() ; i++) {
                    var item = items.getItem(i);
                    var valor = item.get_value();
                    var texto = item.get_text();
                }
                SetListBoxItem(sender, texto, valor);
            }

            function useDataFromChild(pData) {

                if (pData != null) {
                    var vSelectedData = pData[0];
                    var list;
                    switch (vSelectedData.clTipoCatalogo) {
                    case "CANCEL":
                            closeWindow();
                            break;
                    case "ESTADO":
                        list = $find("<%= ObtenerClientId(mpgSolicitud, "NB_ESTADO") %>");
                        vSelectedData.clDato = vSelectedData.nbDato;
                            break;
                    case "MUNICIPIO":
                        list = $find("<%= ObtenerClientId(mpgSolicitud, "NB_MUNICIPIO") %>");
                        vSelectedData.clDato = vSelectedData.nbDato;
                        break;
                    case "COLONIA":
                        list = $find("<%= ObtenerClientId(mpgSolicitud, "NB_COLONIA") %>");
                        vSelectedData.clDato = vSelectedData.nbDato;
                        break;
                    case "CODIGOPOSTAL":
                        boxt = $find("<%= ObtenerClientId(mpgSolicitud, "CL_CODIGO_POSTAL") %>");
                        boxt.set_value(vSelectedData.nbCP);
                        list = $find("<%= ObtenerClientId(mpgSolicitud, "NB_ESTADO") %>");
                        SetListBoxItem(list, vSelectedData.nbEstado);
                        list = $find("<%= ObtenerClientId(mpgSolicitud, "NB_MUNICIPIO") %>");
                        SetListBoxItem(list, vSelectedData.nbMunicipio);
                        list = $find("<%= ObtenerClientId(mpgSolicitud, "NB_COLONIA") %>");
                        vSelectedData.nbDato = vSelectedData.nbColonia;
                        break;
                        case "ACEPTAR":
                            break;
                        case "ESTADONACIMIENTO":
                            list = $find("<%= ObtenerClientId(mpgSolicitud, "NB_ESTADO_NACIMIENTO") %>");
                       break;
                }

                SetListBoxItem(list, vSelectedData.nbDato, vSelectedData.clDato);
            }
        }

        function SetListBoxItem(list, text, value) {
            if (list != undefined) {
                list.trackChanges();

                var items = list.get_items();
                items.clear();

                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(text);
                item.set_value(value);
                item.set_selected(true);
                items.add(item);

                list.commitChanges();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="ralpInventario" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="ramInventario" runat="server" DefaultLoadingPanelID="ralpInventario">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnActualizarFotoEmpleado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rauFotoEmpleado" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbiFotoEmpleado" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdDocumentos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEliminarFotoEmpleado" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnEliminarFotoEmpleado">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rauFotoEmpleado" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="rbiFotoEmpleado" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="grdDocumentos" UpdatePanelHeight="100%" />
                    <telerik:AjaxUpdatedControl ControlID="btnEliminarFotoEmpleado" UpdatePanelHeight="100%" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div style="height: calc(100% - 50px); padding: 10px 10px 10px 10px;">
        <telerik:RadSplitter ID="rsSolicitud" Width="100%" Height="100%" BorderSize="0" runat="server">
            <telerik:RadPane ID="rpSolicitud" runat="server">
                <telerik:RadTabStrip ID="tabSolicitud" runat="server" SelectedIndex="0" MultiPageID="mpgSolicitud">
                    <Tabs>
                        <telerik:RadTab Text="Personal" Selected="True"></telerik:RadTab>
                        <telerik:RadTab Text="Familiar"></telerik:RadTab>
                        <telerik:RadTab Text="Académica"></telerik:RadTab>
                        <telerik:RadTab Text="Laboral"></telerik:RadTab>
                        <telerik:RadTab Text="Intereses y competencias"></telerik:RadTab>
                        <telerik:RadTab Text="Adicional"></telerik:RadTab>
                        <telerik:RadTab Text="Documentación"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div style="height: 100%;">
                    <telerik:RadMultiPage ID="mpgSolicitud" runat="server" SelectedIndex="0" Height="100%">
                        <telerik:RadPageView ID="pvwPersonal" runat="server">

                            <div class="DivFotoCss">
                                <table class="ctrlTableForm">
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadBinaryImage ID="rbiFotoEmpleado" runat="server" Width="128" Height="128" ResizeMode="Fit" ImageUrl="~/Assets/images/LoginUsuario.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadAsyncUpload ID="rauFotoEmpleado" runat="server" MaxFileInputsCount="1" HideFileInput="true" MultipleFileSelection="Disabled" AllowedFileExtensions=".jpeg,.jpg,.png" PostbackTriggers="btnActualizarFotoEmpleado" OnFileUploaded="rauFotoEmpleado_FileUploaded" Width="150" CssClass="CssBoton"> <Localization Select="Seleccionar" /></telerik:RadAsyncUpload>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadButton ID="btnActualizarFotoEmpleado" runat="server" Text="Adjuntar" Width="150"></telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <telerik:RadButton ID="btnEliminarFotoEmpleado" runat="server" Text="Eliminar fotografía" Width="150" OnClick="btnEliminarFotoEmpleado_Click"></telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <label class="labelTitulo">Información personal</label>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pvwFamiliar" runat="server"></telerik:RadPageView>
                        <telerik:RadPageView ID="pvwAcademica" runat="server"></telerik:RadPageView>
                        <telerik:RadPageView ID="pvwLaboral" runat="server"></telerik:RadPageView>
                        <telerik:RadPageView ID="pvwCompetencias" runat="server"></telerik:RadPageView>
                        <telerik:RadPageView ID="pvwAdicional" runat="server">
                            <label class="labelTitulo">Información adicional</label>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pvwDocumentos" runat="server">
                            <label class="labelTitulo">Documentación</label>
                            <div class="ctrlBasico">
                                Tipo documento:<br />
                                <telerik:RadComboBox ID="cmbTipoDocumento" runat="server">
                                    <Items>
                                        <%--<telerik:RadComboBoxItem Text="Fotografía" Value="FOTOGRAFIA" />--%>
                                        <telerik:RadComboBoxItem Text="Imagen" Value="IMAGEN" />
                                        <telerik:RadComboBoxItem Text="Otro" Value="OTRO" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                            <div class="ctrlBasico">
                                Subir documento:<br />
                                <telerik:RadAsyncUpload ID="rauDocumento" runat="server" MultipleFileSelection="Disabled"><Localization Select="Seleccionar" /></telerik:RadAsyncUpload>
                            </div>
                            <div class="ctrlBasico">
                                <telerik:RadButton ID="btnAgregarDocumento" runat="server" Text="Agregar" OnClick="btnAgregarDocumento_Click"></telerik:RadButton>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="ctrlBasico">
                                <telerik:RadGrid ID="grdDocumentos" runat="server" HeaderStyle-Font-Bold="true" OnNeedDataSource="grdDocumentos_NeedDataSource" Width="580">
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView ClientDataKeyNames="ID_ARCHIVO,ID_ITEM" DataKeyNames="ID_ARCHIVO,ID_ITEM" AutoGenerateColumns="false" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridHyperLinkColumn HeaderText="Nombre del documento" DataTextField="NB_DOCUMENTO" DataNavigateUrlFields="ID_ARCHIVO,ID_DOCUMENTO,FE_CREATED_DATE,NB_DOCUMENTO,ID_ITEM" DataNavigateUrlFormatString="~/Comunes/ObtenerDocumento.ashx?ArchivoId={0}&ArchivoNb={2:yyyyMMdd}{4}&ArchivoDescargaNb={3}" Target="_blank"></telerik:GridHyperLinkColumn>
                                            <telerik:GridBoundColumn HeaderText="Tipo de documento" HeaderStyle-Width="200" DataField="CL_TIPO_DOCUMENTO" UniqueName="CL_TIPO_DOCUMENTO"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div class="ctrlBasico" style="padding-left:20px;">
                                <telerik:RadButton ID="btnDelDocumentos" runat="server" Text="Eliminar" OnClick="btnDelDocumentos_Click"></telerik:RadButton>
                            </div>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>
            </telerik:RadPane>
            <telerik:RadPane ID="rpAyuda" runat="server" Scrolling="None" Width="22px">
                <telerik:RadSlidingZone ID="rszAvisoDePrivacidad" runat="server" SlideDirection="Left" ExpandedPaneId="rspAvisoDePrivacidad" Width="22px" ClickToOpen="true">
                    <telerik:RadSlidingPane ID="rspAvisoDePrivacidad" runat="server"  Title="Aviso de privacidad" Width="400px">
                        <div style="padding: 10px; text-align: justify;">
                            <literal id="lbAvisoPrivacidad" runat="server"></literal>
                            <%--<p>Administración Nueva  es una persona moral debidamente constituida de conformidad con las leyes Mexicanas y por medio del presente le avisa que todos los datos personales que usted nos ha proporcionado derivados de la relación comercial , los utilizaremos única y exclusivamente para: identificarlo, localizarlo para cualquier notificación, mantener un expediente físico, procesar sus datos electrónicamente en los diferentes sistemas internos y mantenerle informado sobre estado de su solicitud o trámite realizado ante nosotros o cuestiones de nuestra empresa en general.</p>
                            <p>La protección de sus datos personales es de máxima prioridad para nosotros, es por ello que contamos con equipos físicos y sistemas especializados para resguardar su información. Contamos además con políticas, procedimientos, estándares y guías que están enfocadas a la seguridad de la información y que tienen como objetivo limitar y evitar la divulgación de su información, todos sus datos los tenemos clasificados y tratados como confidenciales.</p>
                            <p>De manera enunciativa, más no limitativa, manejamos los siguientes datos: su nombre; domicilio; fecha de nacimiento; nacionalidad; actividad o giro del negocio al que se dedique; números telefónicos; correo electrónico, escolaridad, habilidades y capacidades.</p>
                            <p>Podremos transferir sus datos personales a terceros mexicanos o extranjeros que nos provean de servicios necesarios para su debida operación teniendo las medidas necesarias para que las personas que tengan acceso a sus datos personales cumplan con la política de privacidad de la empresa, así como con los principios de protección de datos personales establecidos en la Ley.</p>
                            <p>Usted podrá ejercer, cuando proceda, los derechos de acceso, rectificación, cancelación u oposición que la Ley prevé mediante solicitud presentada en el domicilio arriba señalado.</p>
                            <p>Cualquier modificación al presente aviso le será notificada a través de cualquiera de los siguientes medios: un comunicado por escrito enviado a su domicilio; un mensaje enviado a su correo electrónico o a su teléfono móvil; un mensaje dado a conocer a través de nuestra página web.</p>
                            <p style="font-weight: bold;">"Con fundamento en lo dispuesto por el artículo 47 de La Ley Federal del Trabajo, el patrón podrá rescindir la relación de trabajo sin responsabilidad de su parte, cuando el trabajador proporcione en su solicitud datos, información o documentos falsos"</p>--%>
                        </div>
                    </telerik:RadSlidingPane>
                                        <telerik:RadSlidingPane ID="rspAyuda" runat="server" Title="Instrucciones" Width="240px" RenderMode="Mobile" Height="200">
                        <div style="padding: 10px; text-align: justify;">
                             <literal id="lbAyuda" runat="server"></literal>
                           <%-- <p>Por favor ingresa los datos solicitados y al terminar haz clic en el botón de Guardar hasta el final de la página.</p>
                            <p>Recuerda que los campos marcados con asterisco (*) son obligatorios. www.sigein.com.mx</p>
                            <br />
                            <p>En este apartado deberás ingresar tú experiencia laboral, inicia con el empleo</p>
                            <p>actual o el último empleo en el que colaboraste, después continua con el empleo</p>
                            <p>anterior y así sucesivamente hasta el empleo más antiguo.</p>--%>
                           <%-- <p>
                                Para agregar la fotografía de perfil sigue los siguientes pasos:<br />
                                1.- Presiona el botón "Select" para seleccionar la fotografía desde tu ordenador.
                                <br />
                                2.- Al seleccionar la imágen, ésta se cargará. Para ponerla como foto de perfil presiona el botón "Adjuntar". Podrás eliminar la fotografía cargada, seleccionando "Remove".<br />
                                3.- A continuación podrás eliminarla con el botón "Eliminar fotografía" o seleccionar una nueva imagen mediante el botón "Select", repitiendo paso 1 y 2.<br />
                                4.- Al finalizar, no olvides guardar los cambios presionando los botones inferiores izquierdos "Guardar". 
                            </p>--%>
                        </div>
                    </telerik:RadSlidingPane>
                     <telerik:RadSlidingPane ID="rspAyudaFoto" runat="server" Title="Ayuda" Width="240px" RenderMode="Mobile" Height="200">
                        <div style="padding: 10px; text-align: justify;">
                            <p>
                                 Para agregar la fotografía de perfil sigue los siguientes pasos:<br />
                                1.- Presiona el botón "Seleccionar" para seleccionar la fotografía desde tu ordenador.
                                <br />
                                2.- Al seleccionar la imágen, ésta se cargará. Para ponerla como foto de perfil presiona el botón "Adjuntar". Podrás eliminar la fotografía cargada, seleccionando "Remove".<br />
                                3.- A continuación podrás eliminarla con el botón "Eliminar fotografía" o seleccionar una nueva imagen mediante el botón "Select", repitiendo paso 1 y 2.<br />
                                4.- Al finalizar, no olvides guardar los cambios presionando los botones inferiores izquierdos "Guardar". 
                            </p>
                        </div>
                    </telerik:RadSlidingPane>
                </telerik:RadSlidingZone>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    <input type="hidden" value="" id="AceptaTerminos" name="AceptaTerminos" />
    <div style="clear: both; height: 5px;"></div>
    <div class="divControlDerecha">
   <%-- <div class="ctrlBasico">--%>
        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"></telerik:RadButton>
  <%--  </div>--%>

    <%--<div class="ctrlBasico">--%>
        <telerik:RadButton ID="btnGuardarSalir" runat="server" Text="Guardar y cerrar" OnClick="btnGuardarSalir_Click"></telerik:RadButton>
    <%--</div>--%>
 <%-- <div class="ctrlBasico">--%>
             <%-- <telerik:RadButton ID="btnImpresion2" runat="server" Text="Imprimir" Visible="false" OnClientClicked="OpenImpresion" AutoPostBack="false"></telerik:RadButton>--%>
                 <%--   </div>--%>
  <%--  <div class="ctrlBasico">--%>
        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="false" OnClientClicked="OnClientBeforeClose"></telerik:RadButton>
   <%-- </div>--%>
        </div>

    <iframe src="#" style="width: 0; height: 0; border: none" id="ifrmPrint"></iframe>
    <telerik:RadWindowManager ID="rwmAlertas" runat="server" EnableShadow="true" OnClientClose="returnDataToParentPopup">
        <Windows>
            <telerik:RadWindow ID="winAvisoPrivacidad" Height="500" Width="1200" runat="server" ReloadOnShow="true" VisibleStatusbar="false" ShowContentDuringLoad="true" Modal="true" Behaviors="None" OnClientClose="RespuestaAvisoPrivacidad"></telerik:RadWindow>
            <telerik:RadWindow ID="winSeleccion" runat="server" VisibleStatusbar="false" VisibleTitlebar="true" ShowContentDuringLoad="false" Modal="true" ReloadOnShow="true" Behaviors="Close"></telerik:RadWindow>
        </Windows>
        <ConfirmTemplate>
            <div class="rwDialogPopup radconfirm">
                <div class="rwDialogText">
                    {1}
                </div>
                <div>
                    <a onclick="$find('{0}').close(true);" class="rwPopupButton" href="javascript:void(0);">
                        <span class="rwOuterSpan"><span class="rwInnerSpan">Sí</span></span></a>
                    <a onclick="$find('{0}').close(false);" class="rwPopupButton" href="javascript:void(0);">
                        <span class="rwOuterSpan"><span class="rwInnerSpan">No</span></span></a>
                </div>
            </div>
        </ConfirmTemplate>
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">

            function OpenWindowAvisoPrivacidad() {

                var vUsuario = '<%= this.vClUsuario %>';
                var vFgMuestraAviso = '<%= this.MostrarPrivacidad %>';
               // var vIsPostBack = '</= this.vFgAceptaTermino %>';
                var vAceptaTerminos = document.getElementById("AceptaTerminos").value;

                if (vUsuario == "INVITADO SOLICITUD" & vAceptaTerminos != "1" & vFgMuestraAviso == "True") {
                    //var vURL = "VentanaSolicitudAvisoPrivacidad.aspx";
                    //vTitulo = "Aviso de privacidad";
                    //var oWin = window.radopen(vURL, "winAvisoPrivacidad");
                    //oWin.set_title(vTitulo);
                    document.getElementById("AceptaTerminos").value = 1;
                    var vMyUrl = '<%= ResolveUrl("~/IDP/Solicitud/VentanaSolicitudAvisoPrivacidad.aspx") %>';
                    OpenSelectionWindowPrivacidad(vMyUrl, "winPrivacidad", "Aviso de privacidad")

                }
            }

            Sys.Application.add_load(function () {
                OpenWindowAvisoPrivacidad();
            });


        </script>
    </telerik:RadCodeBlock>
</asp:Content>
