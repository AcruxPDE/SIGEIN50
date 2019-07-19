using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.Externas;
using SIGE.Entidades;
using SIGE.WebApp.Comunes;
using SIGE.Negocio.Administracion;
using SIGE.Entidades.IntegracionDePersonal;

namespace SIGE.WebApp.IDP.Pruebas
{
    public partial class Prueba : System.Web.UI.MasterPage
    {
        private List<E_PRUEBA> vsPruebas
        {
            get 
            {
                if (this.ViewState["vsLstPruebas"] != null)
                {
                    return (List<E_PRUEBA>)(this.ViewState["vsLstPruebas"]);
                }
                return new List<E_PRUEBA>();
            }
            set { ViewState["vsLstPruebas"] = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            /*//Obtener la bateria 
            if (vsPruebas.Count() == 0)
            {
                PruebasNegocio pruebas = new PruebasNegocio();
                var vPruebas = pruebas.Obtener_K_PRUEBA(pIdBateria: ContextoUsuario.idBateriaPruebas,pClTokenBateria: ContextoUsuario.clTokenPruebas, pFgAsignada: true);
                vsPruebas = (from c in vPruebas
                                                    select new E_PRUEBA
                                                    {
                                                        ID_PRUEBA = c.ID_PRUEBA,
                                                        ID_CANDIDATO = c.ID_CANDIDATO, 
                                                        CL_ESTADO = c.CL_ESTADO,
                                                        CL_TOKEN_EXTERNO  = c.CL_TOKEN_EXTERNO,
                                                        CL_PRUEBA = c.CL_PRUEBA,
                                                        NB_PRUEBA = c.NB_PRUEBA,
                                                        CL_CORREO_ELECTRONICO = c.CL_CORREO_ELECTRONICO,
                                                        ID_BATERIA = c.ID_BATERIA
                                                    }).ToList();
            }

            if (ContextoUsuario.clEstadoPruebas == "Ini")
            {
                PruebasNegocio pruebas = new PruebasNegocio();
                var vPruebas = pruebas.Obtener_K_PRUEBA(pIdBateria: ContextoUsuario.idBateriaPruebas, pClTokenBateria: ContextoUsuario.clTokenPruebas, pFgAsignada: true).FirstOrDefault();
                int? vIdCandidato = vPruebas.ID_CANDIDATO;
                var pagina = "";
                pagina = "PruebaBienvenida.aspx?ID=" + ContextoUsuario.idBateriaPruebas + "&T=" + ContextoUsuario.clTokenPruebas + "&idCandidato=" + vIdCandidato;
                Response.Redirect(pagina);
            }

            if (ContextoUsuario.clEstadoPruebas == "sig")
            {
                ContextoUsuario.clEstadoPruebas = "apl";
                var pagina = "";
                //revisar en que estatus esta la prueba y redireccionar
                foreach (E_PRUEBA itemPrueba in vsPruebas)
                {
                    if (itemPrueba.CL_ESTADO == "CREADA" || itemPrueba.CL_ESTADO == "INICIADA")
                    {
                        switch (itemPrueba.CL_PRUEBA)
                        {
                            case "LABORAL-1":
                                pagina = "VentanaPersonalidadLaboralI.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "LABORAL-2":
                                pagina = "VentanaPersonalidadLaboralII.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "INTERES":
                                pagina = "VentanaInteresesPersonales.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "PENSAMIENTO":
                                pagina = "VentanaEstiloDePensamiento.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "APTITUD-1":
                                pagina = "VentanaAptitudMentalI.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "APTITUD-2":
                                pagina = "VentanaAptitudMentalII.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;*/
                            /*  case "ADAPTACION":
                                  pagina = "AdaptacionMedio.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                  itemPrueba.CL_PRUEBA = "INICIADA";
                                  break;*/
                            /*case "TIVA":
                                pagina = "VentanaTIVA.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "ORTOGRAFIA-1":
                                pagina = "VentanaOrtografiaI.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "ORTOGRAFIA-2":
                                pagina = "VentanaOrtografiaII.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "ORTOGRAFIA-3":
                                pagina = "VentanaOrtografiaIII.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "TECNICAPC":
                                pagina = "VentanaTecnicaPC.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "REDACCION":
                                pagina = "VentanaRedaccion.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "INGLES":
                                pagina = "VentanaIngles.aspx?ID=" + itemPrueba.ID_PRUEBA + "&T=" + itemPrueba.CL_TOKEN_EXTERNO;
                                itemPrueba.CL_ESTADO = "INICIADA";
                                break;
                            case "ADAPTACION":
                                break;
                            default:
                                pagina = "Default.aspx?F=1";
                                break;
                        };
                        if (itemPrueba.CL_PRUEBA != "ADAPTACION" && itemPrueba.CL_PRUEBA != "ENTREVISTA")
                        {
                            break;
                        }

                    }
                    else {
                        pagina = "Default.aspx?F=1";
                    }
                }
                if (vsPruebas.Count() == 0)
                {
                    pagina = "Default.aspx?F=1";
                }
                Response.Redirect(pagina);
            }*/
        }
    }
}