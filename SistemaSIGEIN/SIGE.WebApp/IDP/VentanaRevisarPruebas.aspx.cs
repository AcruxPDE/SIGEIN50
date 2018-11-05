using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaRevisarPruebas : System.Web.UI.Page
    {
        #region Variables

        private int vIdBateria
        {
            get { return (int)ViewState["vs_vIdBateria"]; }
            set { ViewState["vs_vIdBateria"] = value; }
        }

        private int vIdCandidato
        {
            get { return (int)ViewState["vs_vIdCandidato"]; }
            set { ViewState["vs_vIdCandidato"] = value; }
        }


        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        #endregion

        protected int ObtenerIdPlantilla()
        {
            int vIdPlantilla = 0;

            switch (tbRevisarPruebas.SelectedIndex)
            {
                case 6: //1 Adaptación al medio
                   vIdPlantilla = 1;
                    break;
                case 3: //2	Mental I
                    vIdPlantilla = 2;
                    break;
                case 4: //3	Mental II
                    vIdPlantilla = 3;
                    break;
                case 13: //4	Inglés
                    vIdPlantilla = 4;
                    break;
                case 1: //5	Intereses personales
                    vIdPlantilla = 5;
                    break;
                case 0: //6	Laboral I
                    vIdPlantilla = 6;
                    break;
                case 5: //7 Laboral II
                    vIdPlantilla = 7;
                    break;
                case 8: //8	Otografía I
                    vIdPlantilla = 8;
                    break;
                case 9: //9	Ortografia II
                    vIdPlantilla = 9;
                    break;
                case 10: //10 Ortografia III
                    vIdPlantilla = 10;
                    break;
                case 2: //11 Estilo de pensamiento
                    vIdPlantilla = 11;
                    break;
                case 12: //12 Redacción
                    vIdPlantilla = 12;
                    break;
                case 11: //13 Técnica PC
                    vIdPlantilla = 13;
                    break;
                case 7: //14 TIVA
                    vIdPlantilla = 14;
                    break;
                case 14: //15 Entrevista
                    vIdPlantilla = 15;
                    break;
            }

            return vIdPlantilla;
        }

        protected void HabilitaPuebas(List<SPE_OBTIENE_K_PRUEBA_Result> pCandidatosPruebas)
        {
            foreach (SPE_OBTIENE_K_PRUEBA_Result item in pCandidatosPruebas.OrderByDescending(o => o.NO_ORDEN))
            {
                switch (item.ID_PRUEBA_PLANTILLA)
                {
                    case 1: //1 Adaptación al medio
                        tbRevisarPruebas.Tabs[6].Enabled = true;
                        tbRevisarPruebas.Tabs[6].Selected = true;
                        rpvAdáptacion.Selected = true;
                        ifAdaptacion.Attributes.Add("src", "VentanaIntegracionMedioManual.aspx?ID=" + item.ID_PRUEBA + "&T=" + item.CL_TOKEN_EXTERNO + "&MOD=EDIT&vIdBateria=" + vIdBateria );
                        break;
                    case 2: //2	Mental I
                        tbRevisarPruebas.Tabs[3].Enabled = true;
                        tbRevisarPruebas.Tabs[3].Selected = true;
                        rpvMentalI.Selected = true;
                        ifMentalI.Attributes.Add("src", "Pruebas/VentanaAptitudMentalI.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 3: //3	Mental II
                        tbRevisarPruebas.Tabs[4].Enabled = true;
                        tbRevisarPruebas.Tabs[4].Selected = true;
                        rpvMentalII.Selected = true;
                        ifMentalII.Attributes.Add("src", "Pruebas/VentanaAptitudMentalII.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 4: //4	Inglés
                        tbRevisarPruebas.Tabs[13].Enabled = true;
                        tbRevisarPruebas.Tabs[13].Selected = true;
                        rpvIngles.Selected = true;
                        ifIngles.Attributes.Add("src", "Pruebas/VentanaIngles.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 5: //5	Intereses personales
                        tbRevisarPruebas.Tabs[1].Enabled = true;
                        tbRevisarPruebas.Tabs[1].Selected = true;
                        rpvInteresesPersonales.Selected = true;
                        ifInteresesPersonales.Attributes.Add("src", "Pruebas/VentanaInteresesPersonales.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 6: //6	Laboral I
                        tbRevisarPruebas.Tabs[0].Enabled = true;
                        tbRevisarPruebas.Tabs[0].Selected = true;
                        rpvLaboralI.Selected = true;
                        ifLaboralI.Attributes.Add("src", "Pruebas/VentanaPersonalidadLaboralI.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 7: //7 Laboral II
                        tbRevisarPruebas.Tabs[5].Enabled = true;
                        tbRevisarPruebas.Tabs[5].Selected = true;
                        rpvLaboralI.Selected = true;
                        ifLaboralII.Attributes.Add("src", "Pruebas/VentanaPersonalidadLaboralII.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 8: //8	Otografía I
                        tbRevisarPruebas.Tabs[8].Enabled = true;
                        tbRevisarPruebas.Tabs[8].Selected = true;
                        rpvOrtgrafiaII.Selected = true;
                        ifOrtografiaI.Attributes.Add("src", "Pruebas/VentanaOrtografiaI.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&CLAVE=" + item.CL_PRUEBA + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 9: //9	Ortografia II
                        tbRevisarPruebas.Tabs[9].Enabled = true;
                        tbRevisarPruebas.Tabs[9].Selected = true;
                        rpvOrtgrafiaII.Selected = true;
                        ifOrtografiaII.Attributes.Add("src", "Pruebas/VentanaOrtografiaII.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&CLAVE=" + item.CL_PRUEBA + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 10: //10 Ortografia III
                        tbRevisarPruebas.Tabs[10].Enabled = true;
                        tbRevisarPruebas.Tabs[10].Enabled = true;
                        rpvOrtografiaIII.Selected = true;
                        ifOrtografiaIII.Attributes.Add("src", "Pruebas/VentanaOrtografiaIII.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&CLAVE=" + item.CL_PRUEBA + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 11: //11 Estilo de pensamiento
                        tbRevisarPruebas.Tabs[2].Enabled = true;
                        tbRevisarPruebas.Tabs[2].Selected = true;
                        rpvEstilo.Selected = true;
                        ifEstilo.Attributes.Add("src", "Pruebas/VentanaEstiloDePensamiento.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 12: //12 Redacción
                        tbRevisarPruebas.Tabs[12].Enabled = true;
                        tbRevisarPruebas.Tabs[12].Selected = true;
                        rpvRedaccion.Selected = true;
                        ifRedaccion.Attributes.Add("src", "Pruebas/VentanaRedaccion.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 13: //13 Técnica PC
                        tbRevisarPruebas.Tabs[11].Enabled = true;
                        tbRevisarPruebas.Tabs[11].Selected = true;
                        rpvTecnica.Selected = true;
                        ifTecnica.Attributes.Add("src", "Pruebas/VentanaTecnicaPC.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 14: //14 TIVA
                        tbRevisarPruebas.Tabs[7].Enabled = true;
                        tbRevisarPruebas.Tabs[7].Selected = true;
                        rpvTiva.Selected = true;
                        ifTiva.Attributes.Add("src", "Pruebas/VentanaTIVA.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&MOD=EDIT&vIdBateria=" + vIdBateria);
                        break;
                    case 15: //15 Entrevista
                        tbRevisarPruebas.Tabs[14].Enabled = true;
                        tbRevisarPruebas.Tabs[14].Selected = true;
                        rpvFactoresAdicionales.Selected = true;
                        ifFactoresAdicionales.Attributes.Add("src", "VentanaResultadosEntrevista.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&CL=EDIT&vIdBateria=" + vIdBateria);
                        break;
                }

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["pIdBateria"] != null)
                {
                    vIdBateria = int.Parse(Request.Params["pIdBateria"]);

                    PruebasNegocio pruebas = new PruebasNegocio();
                    List<SPE_OBTIENE_K_PRUEBA_Result> vCandidatosPruebas = pruebas.Obtener_K_PRUEBA(pIdBateria: vIdBateria, pFgAsignada: true);
                    HabilitaPuebas(vCandidatosPruebas);
                }

                if (Request.Params["pIdCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["pIdCandidato"]);
                }
            }

            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
            vNbPrograma = ContextoUsuario.nbPrograma;

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            PruebasNegocio nPruebas = new PruebasNegocio();
            var vResultado = nPruebas.EliminaRespuestasBaterias(vIdBateria, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }
        }

        protected void btnEliminarRespuesta_Click(object sender, EventArgs e)
        {
            int vIdPlantilla = ObtenerIdPlantilla();

            if(vIdPlantilla > 0){
                 PruebasNegocio pruebas = new PruebasNegocio();
                SPE_OBTIENE_K_PRUEBA_Result vCandidatosPruebas = pruebas.Obtener_K_PRUEBA(pIdBateria: vIdBateria, pIdPruebaPlantilla: vIdPlantilla ).FirstOrDefault();
                if (vCandidatosPruebas != null)
                {
                    var vResultado = pruebas.EliminaRespuestasPrueba(vCandidatosPruebas.ID_PRUEBA, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                    if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    {                      
                        List<SPE_OBTIENE_K_PRUEBA_Result> vCandidatosPruebasCargar = pruebas.Obtener_K_PRUEBA(pIdBateria: vIdBateria, pFgAsignada: true);
                        HabilitaPuebas(vCandidatosPruebasCargar);
                    }
                }
                else
                    UtilMensajes.MensajeResultadoDB(rwMensaje, "Ocurrio un error.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }
            else
              UtilMensajes.MensajeResultadoDB(rwMensaje, "Seleccione una prueba.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
        }
    }
}