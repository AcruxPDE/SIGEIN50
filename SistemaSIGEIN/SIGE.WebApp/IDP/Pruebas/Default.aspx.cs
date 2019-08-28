using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.IDP.Pruebas
{
    public partial class Default : System.Web.UI.Page
    {
        #region Variables
        private string vClUsuario = string.Empty;
        private string vNbPrograma = string.Empty;

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }

        private Guid vClToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }

        private string vClEstadoPrueba
        {
            get { return (string)ViewState["vsClEstadoPrueba"]; }
            set { ViewState["vsClEstadoPrueba"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        { 
            //if (ContextoUsuario.idBateriaPruebas == 0 && ContextoUsuario.clTokenPruebas == Guid.Empty)
            //{
                if (Request.QueryString["ID"] != null && Request.QueryString["T"] != null)
                {
                    //FormsAuthentication.SignOut();
                    vIdBateria = int.Parse(Request.QueryString["ID"]);
                    vClToken = Guid.Parse(Request.QueryString["T"].ToString());
                    //ContextoUsuario.idBateriaPruebas = vIdBateria;
                    //ContextoUsuario.clTokenPruebas = vClToken;

                    vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
                    vNbPrograma = ContextoUsuario.nbPrograma;
                    //txtMensajeDespedida.InnerHtml = ContextoApp.IDP.MensajeDespedidaPrueba.dsMensaje;
                }
            //}

            bool finalizarBateria = false;

            if (Request.QueryString["ty"] != null)
            {
                vClEstadoPrueba = Request.QueryString["ty"].ToString();
                ContextoUsuario.clEstadoPruebas = vClEstadoPrueba;
                string pagina = String.Empty;
                if (vClEstadoPrueba == "sig")
                {
                    PruebasNegocio pruebas = new PruebasNegocio();
                    var vPruebas = pruebas.Obtener_K_PRUEBA(pIdBateria: vIdBateria, pClTokenBateria: vClToken, pFgAsignada: true);
                    var vsPruebas = from c in vPruebas
                                    where c.CL_ESTADO == "CREADA" || c.CL_ESTADO == "INICIADA"
                                    orderby c.NO_ORDEN
                                    select c;                    
                    foreach (var itemPrueba in vsPruebas)
                    {
                        switch (itemPrueba.CL_PRUEBA)
                        {
                            case "LABORAL-1":
                                pagina = "VentanaPersonalidadLaboralI.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "LABORAL-2":
                                pagina = "VentanaPersonalidadLaboralII.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "INTERES":
                                pagina = "VentanaInteresesPersonales.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "PENSAMIENTO":
                                pagina = "VentanaEstiloDePensamiento.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "APTITUD-1":
                                pagina = "VentanaAptitudMentalI.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "APTITUD-2":
                                pagina = "VentanaAptitudMentalII.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            /*  case "ADAPTACION":
                                  pagina = "AdaptacionMedio.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                  itemPrueba.CL_PRUEBA = "INICIADA";
                                  break;*/
                            case "TIVA":
                                pagina = "VentanaTIVA.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "ORTOGRAFIA-1":
                                pagina = "VentanaOrtografiaI.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "ORTOGRAFIA-2":
                                pagina = "VentanaOrtografiaII.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "ORTOGRAFIA-3":
                                pagina = "VentanaOrtografiaIII.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "TECNICAPC":
                                pagina = "VentanaTecnicaPC.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "REDACCION":
                                pagina = "VentanaRedaccion.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "INGLES":
                                pagina = "VentanaIngles.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO + "&IDB=" + vIdBateria + "&TB=" + vClToken;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "ADAPTACION":
                                break;
                            default:
                                pagina = "Default.aspx?F=1&ID=" + vIdBateria + "&T=" + vClToken;
                                break;
                        };
                        if (itemPrueba.CL_PRUEBA != "ADAPTACION" && itemPrueba.CL_PRUEBA != "ENTREVISTA")
                        {
                            break;
                        }
                    }
                    if (!String.IsNullOrEmpty(pagina))
                        Response.Redirect(pagina);
                    else
                        finalizarBateria = true;
                }
                else if (vClEstadoPrueba == "Ini")
                {
                    PruebasNegocio pruebas = new PruebasNegocio();
                    var vPruebas = pruebas.Obtener_K_PRUEBA(pIdBateria: vIdBateria, pClTokenBateria: vClToken, pFgAsignada: true).FirstOrDefault();
                    int? vIdCandidato = vPruebas.ID_CANDIDATO;
                    pagina = "PruebaBienvenida.aspx?ID=" + vIdBateria + "&T=" + vClToken + "&idCandidato=" + vIdCandidato;
                    Response.Redirect(pagina);
                }
            }

            if (Request.QueryString["F"] != null || finalizarBateria)
            {
                vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
                vNbPrograma = ContextoUsuario.nbPrograma;
                //vIdBateria = ContextoUsuario.idBateriaPruebas;
                generarBaremos();

                //Observacion: Solucion al bug al terminar una bateria y querer empezar otra, no deja iniciarla
 
                //ContextoUsuario.idBateriaPruebas = 0;
                //ContextoUsuario.clTokenPruebas = Guid.Empty;
                //ContextoUsuario.clEstadoPruebas =string.Empty;
            }

            if (!Page.IsPostBack)
            {
                txtMensajeDespedida.InnerHtml = ContextoApp.IDP.MensajeDespedidaPrueba.dsMensaje;
            }
            
        }

        private void generarBaremos()
        {
            PruebasNegocio neg = new PruebasNegocio();

            E_RESULTADO vResultado = neg.generaVariablesBaremos(vIdBateria, vClUsuario, vNbPrograma);

            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);                
            }            
        }

    }
}