<%@ Page Title="" Language="C#" MasterPageFile="~/AppSIGE.Master" AutoEventWireup="true" CodeBehind="VentanaSolicitudAvisoPrivacidad.aspx.cs" Inherits="SIGE.WebApp.IDP.Solicitud.VentanaSolicitudAvisoPrivacidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

       <link href="/Assets/css/Temas/<%= cssModulo %>" rel="stylesheet" type="text/css" />
    <%--<link href="/Assets/css/Temas/IP.css" rel="stylesheet" type="text/css" />--%>
    <link href="/Assets/css/estilo.css?v=<%= DateTime.Now.ToString() %>>" rel="stylesheet" />
    <script src="/Assets/js/appPruebas.js"></script>

    <script type="text/javascript">

        function Aceptar() {
            //var window = GetRadWindow();
            //window.close(1);

           // GetRadWindow().close();

            //var vArray = [];
            //var arreglo = {
            //    clTipoCatalogo: "ACEPTAR"
            //};
            //vArray.push(arreglo);
            //sendDataToParent(vArray);
            GetRadWindow().close();
        }

        

        function Declinar() {
            var vArray = [];
            var arreglo = {
                clTipoCatalogo: "CANCEL"
            };
            vArray.push(arreglo);
            sendDataToParent(vArray);

        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

    

     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <fieldset>
        <legend>Aviso de privacidad</legend>
        <div style="padding: 10px; text-align: justify;">
             <literal id="lbAvisoPrivacidad" runat="server"></literal>
           <%-- <p>Administración Nueva  es una persona moral debidamente constituida de conformidad con las leyes Mexicanas y por medio del presente le avisa que todos los datos personales que usted nos ha proporcionado derivados de la relación comercial , los utilizaremos única y exclusivamente para: identificarlo, localizarlo para cualquier notificación, mantener un expediente físico, procesar sus datos electrónicamente en los diferentes sistemas internos y mantenerle informado sobre estado de su solicitud o trámite realizado ante nosotros o cuestiones de nuestra empresa en general.</p>
            <p>La protección de sus datos personales es de máxima prioridad para nosotros, es por ello que contamos con equipos físicos y sistemas especializados para resguardar su información. Contamos además con políticas, procedimientos, estándares y guías que están enfocadas a la seguridad de la información y que tienen como objetivo limitar y evitar la divulgación de su información, todos sus datos los tenemos clasificados y tratados como confidenciales.</p>
            <p>De manera enunciativa, más no limitativa, manejamos los siguientes datos: su nombre; domicilio; fecha de nacimiento; nacionalidad; actividad o giro del negocio al que se dedique; números telefónicos; correo electrónico, escolaridad, habilidades y capacidades.</p>
            <p>Podremos transferir sus datos personales a terceros mexicanos o extranjeros que nos provean de servicios necesarios para su debida operación teniendo las medidas necesarias para que las personas que tengan acceso a sus datos personales cumplan con la política de privacidad de la empresa, así como con los principios de protección de datos personales establecidos en la Ley.</p>
            <p>Usted podrá ejercer, cuando proceda, los derechos de acceso, rectificación, cancelación u oposición que la Ley prevé mediante solicitud presentada en el domicilio arriba señalado.</p>
            <p>Cualquier modificación al presente aviso le será notificada a través de cualquiera de los siguientes medios: un comunicado por escrito enviado a su domicilio; un mensaje enviado a su correo electrónico o a su teléfono móvil; un mensaje dado a conocer a través de nuestra página web.</p>
            <p style="font-weight: bold;">"Con fundamento en lo dispuesto por el artículo 47 de La Ley Federal del Trabajo, el patrón podrá rescindir la relación de trabajo sin responsabilidad de su parte, cuando el trabajador proporcione en su solicitud datos, información o documentos falsos"</p>--%>
        </div>
    </fieldset>

    <div style="clear:both; height:10px"></div>

    <div class="ctrlBasico">
        <telerik:RadButton runat="server" ID="btnAceptar" Text="Aceptar" AutoPostBack="false" OnClientClicked="Aceptar"></telerik:RadButton>
    </div>

    <div class="cltrBasico">
        <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" AutoPostBack="false" OnClientClicked="Declinar"></telerik:RadButton>
    </div>
</asp:Content>
