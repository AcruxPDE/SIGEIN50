using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaAgregarPruebas : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement vSeleccionXml { get; set; }
        List<SPE_OBTIENE_CANDIDATOS_BATERIA_Result> lstCandidatos = new List<SPE_OBTIENE_CANDIDATOS_BATERIA_Result>();
        public List<E_CANDIDATO> lstCandidatoS
        {
            set { ViewState["lstCandidatoS"] = value; }
            get { return (List<E_CANDIDATO>)ViewState["lstCandidatoS"]; }
        }

        public Guid vIdCandidatosPruebas
        {
            get { return (Guid)ViewState["vs_vIdCandidatosPruebas"]; }
            set { ViewState["vs_vIdCandidatosPruebas"] = value; }
        }

        private int? vIdBateria
        {
            get { return (int?)ViewState["vs_vIdBateria"];}
            set { ViewState["vs_vIdBateria"] = value; }
        }

        public int? vIdCandidatoBateria
        {
            get { return (int?)ViewState["vs_vIdCandidatoBateria"]; }
            set { ViewState["vs_vIdCandidatoBateria"] = value; }
        }

        public int? vFlBateria
        {
            get { return (int?)ViewState["vs_vFlBateria"]; }
            set { ViewState["vs_vFlBateria"] = value; }
        }

        public Guid? vClToken
        {
            get { return (Guid?)ViewState["vs_vClToken"]; }
            set { ViewState["vs_vClToken"] = value; }
        }

        #endregion

        #region Funciones

        //protected void agregarTooltips()
        //{

        //    PruebasNegocio negocio = new PruebasNegocio();
        //    //Adaptación al medio ADAPTACION
        //    AdaptacionMiedo.ToolTip = "Los factores que evalúa son: productividad, empuje, sociabilidad, creatividad, paciencia, energía, participación, autoestima y seguridad";
        //    //Aptitud mental I APTITUD-1
        //    AptitupMental.ToolTip = "Los factores que evalúa son: inteligencia, aprendizaje, cultura general, sentido común, pensamiento organizado, capacidad de juicio, capacidad de análisis y síntesis, capacidad de abstracción, capacidad de razonamiento, capacidad de planeación, capacidad de discriminación y capacidad de deducción";
        //    //Aptitud mental II APTITUD-2
        //    AptitupMentalII.ToolTip = "Los factores que evalúa son: inteligencia, aprendizaje, cultura general, sentido común, pensamiento organizado, capacidad de juicio, capacidad de análisis y síntesis, capacidad de abstracción, capacidad de razonamiento, capacidad de planeación, capacidad de discriminación y capacidad de deducción";
        //    //Entrevista ENTREVISTA
        //    PruebaEntrevista.ToolTip = "Los factores que evalúa son: comunicación verbal y no verbal";
        //    //Pruebas de inglés INGLES
        //    PruebaIngles.ToolTip = "Los factores que evalúa son: inglés";
        //    //Intereses personales INTERES
        //    InteresesPersonales.ToolTip = "Los factores que evalúa son: teórico, económico, artístico estético, social, político y regulatorio";
        //    //Personalidad laboral I LABORAL-1
        //    PersonalidadLab1.ToolTip = "Los factores que evalúa son: empuje, influencia, constancia y cumplimiento";
        //    //Personalidad laboral II LABORAL-2
        //    PersonalidadLaboralII.ToolTip = "Los factores que evalúa son: da y apoya, toma y controla, mantiene y conserva y adapta y negocia";
        //    //Ortografía I ORTOGRAFIA-1
        //    OrtografiaI.ToolTip = "Los factores que evalúa son: ortografía, uso de la v/b y uso de la s/c/z";
        //    //Ortografía II ORTOGRAFIA-2
        //    OrtografiaII.ToolTip = "Los factores que evalúa son: ortografía general";
        //    //Ortografía III ORTOGRAFIA-3
        //    OrtografiaIII.ToolTip = "Los factores que evalúa son: ortografía y uso de acentos";
        //    //Estilo de pensamiento PENSAMIENTO
        //    EstiloPensamiento.ToolTip = "Los factores que evalúa son: análisis, visión, intuición y lógica";
        //    //Redacción REDACCION
        //    Redaccion.ToolTip = "Los factores que evalúa son: ortografía, gramática y puntuación";
        //    //Técnica PC TECNICAPC
        //    TecnicaPC.ToolTip = "Los factores que evalúa son: software, internet, hardware y comunicaciones";
        //    //TIVA TIVA
        //    Tiva.ToolTip = "Los factores que evalúa son: índice de integridad personal, de apego a leyes y reglamentos, de integridad y ética laboral y de integridad cívica";

        //}

        protected void SeleccionarPruebas(List<SPE_OBTIENE_K_PRUEBA_Result> pLstPruebas)
        {
            if (pLstPruebas != null)
            {
                //ActivarDesactivarPruebaEdicion(AdaptacionMiedo, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(1)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(1)));

                //ActivarDesactivarPruebaEdicion(AptitupMental, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(2)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(2)));

                //ActivarDesactivarPruebaEdicion(AptitupMentalII, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(3)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(3)));

                //ActivarDesactivarPruebaEdicion(PruebaIngles, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(4)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(4)));

                //ActivarDesactivarPruebaEdicion(InteresesPersonales, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(5)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(5)));

                //ActivarDesactivarPruebaEdicion(PersonalidadLab1, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(6)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(6)));

                //ActivarDesactivarPruebaEdicion(PersonalidadLaboralII, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(7)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(7)));

                //ActivarDesactivarPruebaEdicion(OrtografiaI, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(8)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(8)));

                //ActivarDesactivarPruebaEdicion(OrtografiaII, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(9)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(9)));

                //ActivarDesactivarPruebaEdicion(OrtografiaIII, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(10)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(10)));

                //ActivarDesactivarPruebaEdicion(EstiloPensamiento, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(11)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(11)));

                //ActivarDesactivarPruebaEdicion(Redaccion, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(12)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(12)));

                //ActivarDesactivarPruebaEdicion(TecnicaPC, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(13)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(13)));

                //ActivarDesactivarPruebaEdicion(Tiva, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(14)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(14)));

                //ActivarDesactivarPruebaEdicion(PruebaEntrevista, pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(15)), pLstPruebas.Exists(w => w.ID_PRUEBA_PLANTILLA.Equals(15)));
            }

        }

        protected void Pruebas()
        {
            PruebasNegocio negocio = new PruebasNegocio();
            //if (radCmbPuesto.SelectedValue != "")
            //TODO: probar esto con cambio a selector de puestos.
            if(lstPuesto.SelectedValue != "")
            {
                decimal? v = 0;
                v = decimal.Parse(radSliderNivel.SelectedItem.Value);
                //var vPruebas = negocio.Obtener_PRUEBA_POR_PUESTO(int.Parse(radCmbPuesto.SelectedValue), v);
                var vPruebas = negocio.Obtener_PRUEBA_POR_PUESTO(int.Parse(lstPuesto.SelectedValue), v);

                //if (vPruebas.Count() > 0)
                //{
                CambiarEstatusDePruebas(false);
              //  ActivarDesactivarPrueba(PruebaEntrevista, true);
                //}
                grdPruebas.ClientSettings.Selecting.AllowRowSelect = true;
                foreach (GridDataItem item in grdPruebas.Items)
                {
                    switch (int.Parse(item.GetDataKeyValue("ID_PRUEBA").ToString()))
                    {
                        case 1: //1 Adaptación al medio
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 1);
                            break;
                        case 2: //2	Mental I
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 2);
                            break;
                        case 3: //3	Mental II
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 3);
                            break;
                        case 4: //4	Inglés
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 4);
                            break;
                        case 5: //5	Intereses personales
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 5);
                            break;
                        case 6: //6	Laboral I
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 6);
                            break;
                        case 7: //7	Laboral II
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 7);
                            break;
                        case 8: //8	Otografía I
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 8);
                            break;
                        case 9: //9	Ortografia II
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 9);
                            break;
                        case 10: //10	Ortografia III
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 10);
                            break;
                        case 11: //11	Estilo de pensamiento
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 11);
                            break;
                        case 12: //12	Redacción
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 12);
                            break;
                        case 13: //13	Técnica PC
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 13);
                            break;
                        case 14: //14	TIVA
                            item.Selected = vPruebas.Exists(e => e.ID_PRUEBA == 14);
                            break;
                        case 15: //15	entrevista
                            item.Selected = true;
                            break;
                    }

                   
                }

                grdPruebas.ClientSettings.Selecting.AllowRowSelect = false;

                //foreach (SPE_OBTIENE_PRUEBA_POR_PUESTO_Result prueba in vPruebas)
                //{
                //    switch (prueba.ID_PRUEBA)
                //    {
                //        case 1: //1 Adaptación al medio
                //            ActivarDesactivarPrueba(AdaptacionMiedo, true);
                //            break;
                //        case 2: //2	Mental I
                //            ActivarDesactivarPrueba(AptitupMental, false);
                //            break;
                //        case 3: //3	Mental II
                //            ActivarDesactivarPrueba(AptitupMentalII, true);
                //            break;
                //        case 4: //4	Inglés
                //            ActivarDesactivarPrueba(PruebaIngles, true);
                //            break;
                //        case 5: //5	Intereses personales
                //            ActivarDesactivarPrueba(InteresesPersonales, true);
                //            break;
                //        case 6: //6	Laboral I
                //            ActivarDesactivarPrueba(PersonalidadLab1, true);
                //            break;
                //        case 7: //7	Laboral II
                //            ActivarDesactivarPrueba(PersonalidadLaboralII, true);
                //            break;
                //        case 8: //8	Otografía I
                //            ActivarDesactivarPrueba(OrtografiaI, true);
                //            break;
                //        case 9: //9	Ortografia II
                //            ActivarDesactivarPrueba(OrtografiaII, true);
                //            break;
                //        case 10: //10	Ortografia III
                //            ActivarDesactivarPrueba(OrtografiaIII, true);
                //            break;
                //        case 11: //11	Estilo de pensamiento
                //            ActivarDesactivarPrueba(EstiloPensamiento, true);
                //            break;
                //        case 12: //12	Redacción
                //            ActivarDesactivarPrueba(Redaccion, true);
                //            break;
                //        case 13: //13	Técnica PC
                //            ActivarDesactivarPrueba(TecnicaPC, true);
                //            break;
                //        case 14: //14	TIVA
                //            ActivarDesactivarPrueba(Tiva, true);
                //            break;
                //        //case 15: //15	Entrevista
                //        //    ActivarDesactivarPrueba(PruebaEntrevista, true);
                //        //    break;
                //    }
            //    }
            }
        }

        protected void cambiaColor(System.Web.UI.HtmlControls.HtmlGenericControl prueba)
        {
            /* if (prueba.Attributes["class"] == classActivado)
             {
                 prueba.Attributes["class"] = classDesactivado;
             }
             else {
                 prueba.Attributes["class"] = classActivado;
             }*/
        }

        protected void ActivarDesactivarPrueba(Telerik.Web.UI.RadButton prueba, bool activo)
        {
            if (activo)
            {
                prueba.SelectedToggleStateIndex = 0;
            }
            else
            {
                prueba.SelectedToggleStateIndex = 1;
            }
        }

        protected void ActivarDesactivarPruebaEdicion(Telerik.Web.UI.RadButton prueba, bool activo, bool habilitado)
        {


           // if (!habilitado)
         //   {
                prueba.Enabled = true;
          //  }
            //else
            //{
            //    prueba.Enabled = true;
            //}

            if (activo)
            {
                prueba.SelectedToggleStateIndex = 0;
            }
            else
            {
                prueba.SelectedToggleStateIndex = 1;
            }


        }

        protected void CambiarEstatusDePruebas(bool status)
        {
            grdPruebas.ClientSettings.Selecting.AllowRowSelect = true;
            grdPruebas.SelectedIndexes.Clear();
            grdPruebas.ClientSettings.Selecting.AllowRowSelect = true;
            //InteresesPersonales.Enabled = status;
            //EstiloPensamiento.Enabled = status;
            //AptitupMental.Enabled = status;
            //PersonalidadLaboralII.Enabled = status;
            //Tiva.Enabled = status;
            //OrtografiaI.Enabled = status;
            //OrtografiaII.Enabled = status;
            //OrtografiaIII.Enabled = status;
            //TecnicaPC.Enabled = status;
            //Redaccion.Enabled = status;
            //PersonalidadLab1.Enabled = status;
            //AptitupMentalII.Enabled = status;
            //AdaptacionMiedo.Enabled = status;
            //PruebaIngles.Enabled = status;
            //PruebaEntrevista.Enabled = status;
            //var indice = ((status) ? 0 : 1);

            //InteresesPersonales.SelectedToggleStateIndex = indice;
            //EstiloPensamiento.SelectedToggleStateIndex = indice;
            //AptitupMental.SelectedToggleStateIndex = indice;
            //PersonalidadLaboralII.SelectedToggleStateIndex = indice;
            //Tiva.SelectedToggleStateIndex = indice;
            //OrtografiaI.SelectedToggleStateIndex = indice;
            //OrtografiaII.SelectedToggleStateIndex = indice;
            //OrtografiaIII.SelectedToggleStateIndex = indice;
            //TecnicaPC.SelectedToggleStateIndex = indice;
            //Redaccion.SelectedToggleStateIndex = indice;
            //PersonalidadLab1.SelectedToggleStateIndex = indice;
            //AptitupMentalII.SelectedToggleStateIndex = indice;
            //AdaptacionMiedo.SelectedToggleStateIndex = indice;
            //PruebaIngles.SelectedToggleStateIndex = indice;
            //PruebaEntrevista.SelectedToggleStateIndex = indice;
        }

        protected List<E_PRUEBA_CANDIDATO> obtenPruebasSeleccionadas()
        {
            //objPruebas.
            List<E_PRUEBA_CANDIDATO> lstPruebas = new List<E_PRUEBA_CANDIDATO>();
            E_PRUEBA_CANDIDATO objLaboral = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objInteres = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objPensamiento = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objAptitud = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objAptitud2 = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objLaboral2 = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objTIVA = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objOrtografia = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objOrtografia2 = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objOrtografia3 = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objTecnicaPc = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objRedaccion = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objAdaptacion = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objIngles = new E_PRUEBA_CANDIDATO();
            E_PRUEBA_CANDIDATO objEntrevista = new E_PRUEBA_CANDIDATO();

            if (radCmbNiveles.SelectedValue == "EJE" || radCmbNiveles.SelectedValue == "OPE")
            {
                if (radCmbNiveles.SelectedValue == "EJE")
                {
                    objLaboral.ID_PRUEBA_PLANTILLA = 6;
                    objLaboral.CL_PRUEBA = "LABORAL-1";
                    objLaboral.CL_ESTADO = "CREADA";
                    objLaboral.FG_ASIGNADA = true;

                    lstPruebas.Add(objLaboral);
                }
                else
                {
                    objLaboral.ID_PRUEBA_PLANTILLA = 6;
                    objLaboral.CL_PRUEBA = "LABORAL-1";
                    objLaboral.CL_ESTADO = "CREADA";
                    objLaboral.FG_ASIGNADA = false;

                    lstPruebas.Add(objLaboral);

                }

                objInteres.ID_PRUEBA_PLANTILLA = 5;
                objInteres.CL_PRUEBA = "INTERES";
                objInteres.CL_ESTADO = "CREADA";
                objInteres.FG_ASIGNADA = true;

                lstPruebas.Add(objInteres);

                objPensamiento.ID_PRUEBA_PLANTILLA = 11;
                objPensamiento.CL_PRUEBA = "PENSAMIENTO";
                objPensamiento.CL_ESTADO = "CREADA";
                objPensamiento.FG_ASIGNADA = true;

                lstPruebas.Add(objPensamiento);

                if (radCmbNiveles.SelectedValue == "EJE")
                {
                    objAptitud.ID_PRUEBA_PLANTILLA = 2;
                    objAptitud.CL_PRUEBA = "APTITUD-1";
                    objAptitud.CL_ESTADO = "CREADA";
                    objAptitud.FG_ASIGNADA = true;

                    lstPruebas.Add(objAptitud);
                }
                else
                {
                    objAptitud.ID_PRUEBA_PLANTILLA = 2;
                    objAptitud.CL_PRUEBA = "APTITUD-1";
                    objAptitud.CL_ESTADO = "CREADA";
                    objAptitud.FG_ASIGNADA = false;

                    lstPruebas.Add(objAptitud);
                }

                if (radCmbNiveles.SelectedValue == "EJE")
                {
                    objAptitud2.ID_PRUEBA_PLANTILLA = 3;
                    objAptitud2.CL_PRUEBA = "APTITUD-2";
                    objAptitud2.CL_ESTADO = "CREADA";
                    objAptitud2.FG_ASIGNADA = false;

                    lstPruebas.Add(objAptitud2);
                }
                else
                {
                    objAptitud2.ID_PRUEBA_PLANTILLA = 3;
                    objAptitud2.CL_PRUEBA = "APTITUD-2";
                    objAptitud2.CL_ESTADO = "CREADA";
                    objAptitud2.FG_ASIGNADA = true;

                    lstPruebas.Add(objAptitud2);
                }

                objLaboral2.ID_PRUEBA_PLANTILLA = 7;
                objLaboral2.CL_PRUEBA = "LABORAL-2";
                objLaboral2.CL_ESTADO = "CREADA";
                objLaboral2.FG_ASIGNADA = true;

                lstPruebas.Add(objLaboral2);


                objAdaptacion.ID_PRUEBA_PLANTILLA = 1;
                objAdaptacion.CL_PRUEBA = "ADAPTACION";
                objAdaptacion.CL_ESTADO = "CREADA";
                objAdaptacion.FG_ASIGNADA = true;

                lstPruebas.Add(objAdaptacion);


                objTIVA.ID_PRUEBA_PLANTILLA = 14;
                objTIVA.CL_PRUEBA = "TIVA";
                objTIVA.CL_ESTADO = "CREADA";
                objTIVA.FG_ASIGNADA = true;

                lstPruebas.Add(objTIVA);

                objOrtografia.ID_PRUEBA_PLANTILLA = 8;
                objOrtografia.CL_PRUEBA = "ORTOGRAFIA-1";
                objOrtografia.CL_ESTADO = "CREADA";
                objOrtografia.FG_ASIGNADA = true;

                lstPruebas.Add(objOrtografia);

                objOrtografia2.ID_PRUEBA_PLANTILLA = 9;
                objOrtografia2.CL_PRUEBA = "ORTOGRAFIA-2";
                objOrtografia2.CL_ESTADO = "CREADA";
                objOrtografia2.FG_ASIGNADA = true;

                lstPruebas.Add(objOrtografia2);

                objOrtografia3.ID_PRUEBA_PLANTILLA = 10;
                objOrtografia3.CL_PRUEBA = "ORTOGRAFIA-3";
                objOrtografia3.CL_ESTADO = "CREADA";
                objOrtografia3.FG_ASIGNADA = true;

                lstPruebas.Add(objOrtografia3);

                objTecnicaPc.ID_PRUEBA_PLANTILLA = 13;
                objTecnicaPc.CL_PRUEBA = "TECNICAPC";
                objTecnicaPc.CL_ESTADO = "CREADA";
                objTecnicaPc.FG_ASIGNADA = true;

                lstPruebas.Add(objTecnicaPc);
                
                objRedaccion.ID_PRUEBA_PLANTILLA = 12;
                objRedaccion.CL_PRUEBA = "REDACCION";
                objRedaccion.CL_ESTADO = "CREADA";
                objRedaccion.FG_ASIGNADA = true;

                lstPruebas.Add(objRedaccion);

                objIngles.ID_PRUEBA_PLANTILLA = 4;
                objIngles.CL_PRUEBA = "INGLES";
                objIngles.CL_ESTADO = "CREADA";
                objIngles.FG_ASIGNADA = false;

                lstPruebas.Add(objIngles);

                objEntrevista.ID_PRUEBA_PLANTILLA = 15;
                objEntrevista.CL_PRUEBA = "ENTREVISTA";
                objEntrevista.CL_ESTADO = "CREADA";
                objEntrevista.FG_ASIGNADA = true;

                lstPruebas.Add(objEntrevista);
            }
            else
            {
                foreach (GridDataItem item in grdPruebas.Items)
                {
                    switch (int.Parse(item.GetDataKeyValue("ID_PRUEBA").ToString()))
                    {
                        case 1: //1 Adaptación al medio
                            if (item.Selected == true)
                            {
                                objAdaptacion.ID_PRUEBA_PLANTILLA = 1;
                                objAdaptacion.CL_PRUEBA = "ADAPTACION";
                                objAdaptacion.CL_ESTADO = "CREADA";
                                objAdaptacion.FG_ASIGNADA = true;

                                lstPruebas.Add(objAdaptacion);
                            }
                            else
                            {
                                objAdaptacion.ID_PRUEBA_PLANTILLA = 1;
                                objAdaptacion.CL_PRUEBA = "ADAPTACION";
                                objAdaptacion.CL_ESTADO = "CREADA";
                                objAdaptacion.FG_ASIGNADA = false;

                                lstPruebas.Add(objAdaptacion);
                            }
                                    
                            break;
                        case 2: //2	Mental I
                            if (item.Selected == true)
                            {
                                objAptitud.ID_PRUEBA_PLANTILLA = 2;
                                objAptitud.CL_PRUEBA = "APTITUD-1";
                                objAptitud.CL_ESTADO = "CREADA";
                                objAptitud.FG_ASIGNADA = true;

                                lstPruebas.Add(objAptitud);
                            }
                            else
                            {
                                objAptitud.ID_PRUEBA_PLANTILLA = 2;
                                objAptitud.CL_PRUEBA = "APTITUD-1";
                                objAptitud.CL_ESTADO = "CREADA";
                                objAptitud.FG_ASIGNADA = false;

                                lstPruebas.Add(objAptitud);
                            }
                            break;
                        case 3: //3	Mental II
                            if (item.Selected == true)
                            {
                                objAptitud2.ID_PRUEBA_PLANTILLA = 3;
                                objAptitud2.CL_PRUEBA = "APTITUD-2";
                                objAptitud2.CL_ESTADO = "CREADA";
                                objAptitud2.FG_ASIGNADA = true;

                                lstPruebas.Add(objAptitud2);
                            }
                            else
                            {
                                objAptitud2.ID_PRUEBA_PLANTILLA = 3;
                                objAptitud2.CL_PRUEBA = "APTITUD-2";
                                objAptitud2.CL_ESTADO = "CREADA";
                                objAptitud2.FG_ASIGNADA = false;

                                lstPruebas.Add(objAptitud2);
                            }
                            break;
                        case 4: //4	Inglés
                            if (item.Selected == true)
                            {
                                objIngles.ID_PRUEBA_PLANTILLA = 4;
                                objIngles.CL_PRUEBA = "INGLES";
                                objIngles.CL_ESTADO = "CREADA";
                                objIngles.FG_ASIGNADA = true;

                                lstPruebas.Add(objIngles);
                            }
                            else
                            {
                                objIngles.ID_PRUEBA_PLANTILLA = 4;
                                objIngles.CL_PRUEBA = "INGLES";
                                objIngles.CL_ESTADO = "CREADA";
                                objIngles.FG_ASIGNADA = false;

                                lstPruebas.Add(objIngles);
                            }
                            break;
                        case 5: //5	Intereses personales
                            if (item.Selected == true)
                            {
                                objInteres.ID_PRUEBA_PLANTILLA = 5;
                                objInteres.CL_PRUEBA = "INTERES";
                                objInteres.CL_ESTADO = "CREADA";
                                objInteres.FG_ASIGNADA = true;

                                lstPruebas.Add(objInteres);
                            }
                            else
                            {
                                objInteres.ID_PRUEBA_PLANTILLA = 5;
                                objInteres.CL_PRUEBA = "INTERES";
                                objInteres.CL_ESTADO = "CREADA";
                                objInteres.FG_ASIGNADA = false;

                                lstPruebas.Add(objInteres);
                            }
                            break;
                        case 6: //6	Laboral I
                            if (item.Selected == true)
                            {
                                objLaboral.ID_PRUEBA_PLANTILLA = 6;
                                objLaboral.CL_PRUEBA = "LABORAL-1";
                                objLaboral.CL_ESTADO = "CREADA";
                                objLaboral.FG_ASIGNADA = true;

                                lstPruebas.Add(objLaboral);
                            }
                            else
                            {
                                objLaboral.ID_PRUEBA_PLANTILLA = 6;
                                objLaboral.CL_PRUEBA = "LABORAL-1";
                                objLaboral.CL_ESTADO = "CREADA";
                                objLaboral.FG_ASIGNADA = false;

                                lstPruebas.Add(objLaboral);
                            }
                            break;
                        case 7: //7	Laboral II
                            if (item.Selected == true)
                            {
                                objLaboral2.ID_PRUEBA_PLANTILLA = 7;
                                objLaboral2.CL_PRUEBA = "LABORAL-2";
                                objLaboral2.CL_ESTADO = "CREADA";
                                objLaboral2.FG_ASIGNADA = true;

                                lstPruebas.Add(objLaboral2);
                            }
                            else
                            {
                                objLaboral2.ID_PRUEBA_PLANTILLA = 7;
                                objLaboral2.CL_PRUEBA = "LABORAL-2";
                                objLaboral2.CL_ESTADO = "CREADA";
                                objLaboral2.FG_ASIGNADA = false;

                                lstPruebas.Add(objLaboral2);
                            }
                            break;
                        case 8: //8	Otografía I
                            if (item.Selected == true)
                            {
                                objOrtografia.ID_PRUEBA_PLANTILLA = 8;
                                objOrtografia.CL_PRUEBA = "ORTOGRAFIA-1";
                                objOrtografia.CL_ESTADO = "CREADA";
                                objOrtografia.FG_ASIGNADA = true;

                                lstPruebas.Add(objOrtografia);
                            }
                            else
                            {
                                objOrtografia.ID_PRUEBA_PLANTILLA = 8;
                                objOrtografia.CL_PRUEBA = "ORTOGRAFIA-1";
                                objOrtografia.CL_ESTADO = "CREADA";
                                objOrtografia.FG_ASIGNADA = false;

                                lstPruebas.Add(objOrtografia);
                            }
                            break;
                        case 9: //9	Ortografia II
                            if (item.Selected == true)
                            {
                                objOrtografia2.ID_PRUEBA_PLANTILLA = 9;
                                objOrtografia2.CL_PRUEBA = "ORTOGRAFIA-2";
                                objOrtografia2.CL_ESTADO = "CREADA";
                                objOrtografia2.FG_ASIGNADA = true;

                                lstPruebas.Add(objOrtografia2);
                            }
                            else
                            {
                                objOrtografia2.ID_PRUEBA_PLANTILLA = 9;
                                objOrtografia2.CL_PRUEBA = "ORTOGRAFIA-2";
                                objOrtografia2.CL_ESTADO = "CREADA";
                                objOrtografia2.FG_ASIGNADA = false;

                                lstPruebas.Add(objOrtografia2);
                            }
                            break;
                        case 10: //10	Ortografia III
                            if (item.Selected == true)
                            {
                                objOrtografia3.ID_PRUEBA_PLANTILLA = 10;
                                objOrtografia3.CL_PRUEBA = "ORTOGRAFIA-3";
                                objOrtografia3.CL_ESTADO = "CREADA";
                                objOrtografia3.FG_ASIGNADA = true;

                                lstPruebas.Add(objOrtografia3);
                            }
                            else
                            {
                                objOrtografia3.ID_PRUEBA_PLANTILLA = 10;
                                objOrtografia3.CL_PRUEBA = "ORTOGRAFIA-3";
                                objOrtografia3.CL_ESTADO = "CREADA";
                                objOrtografia3.FG_ASIGNADA = false;

                                lstPruebas.Add(objOrtografia3);
                            }
                            break;
                        case 11: //11	Estilo de pensamiento
                            if (item.Selected == true)
                            {
                                objPensamiento.ID_PRUEBA_PLANTILLA = 11;
                                objPensamiento.CL_PRUEBA = "PENSAMIENTO";
                                objPensamiento.CL_ESTADO = "CREADA";
                                objPensamiento.FG_ASIGNADA = true;

                                lstPruebas.Add(objPensamiento);
                            }
                            else
                            {
                                objPensamiento.ID_PRUEBA_PLANTILLA = 11;
                                objPensamiento.CL_PRUEBA = "PENSAMIENTO";
                                objPensamiento.CL_ESTADO = "CREADA";
                                objPensamiento.FG_ASIGNADA = false;

                                lstPruebas.Add(objPensamiento);
                            }
                            break;
                        case 12: //12	Redacción
                            if (item.Selected == true)
                            {
                                objRedaccion.ID_PRUEBA_PLANTILLA = 12;
                                objRedaccion.CL_PRUEBA = "REDACCION";
                                objRedaccion.CL_ESTADO = "CREADA";
                                objRedaccion.FG_ASIGNADA = true;

                                lstPruebas.Add(objRedaccion);
                            }
                            else
                            {
                                objRedaccion.ID_PRUEBA_PLANTILLA = 12;
                                objRedaccion.CL_PRUEBA = "REDACCION";
                                objRedaccion.CL_ESTADO = "CREADA";
                                objRedaccion.FG_ASIGNADA = false;

                                lstPruebas.Add(objRedaccion);
                            }
                            break;
                        case 13: //13	Técnica PC
                            if (item.Selected == true)
                            {
                                objTecnicaPc.ID_PRUEBA_PLANTILLA = 13;
                                objTecnicaPc.CL_PRUEBA = "TECNICAPC";
                                objTecnicaPc.CL_ESTADO = "CREADA";
                                objTecnicaPc.FG_ASIGNADA = true;

                                lstPruebas.Add(objTecnicaPc);
                            }
                            else
                            {
                                objTecnicaPc.ID_PRUEBA_PLANTILLA = 13;
                                objTecnicaPc.CL_PRUEBA = "TECNICAPC";
                                objTecnicaPc.CL_ESTADO = "CREADA";
                                objTecnicaPc.FG_ASIGNADA = false;

                                lstPruebas.Add(objTecnicaPc);
                            }
                            break;
                        case 14: //14	TIVA
                            if (item.Selected == true)
                            {
                                objTIVA.ID_PRUEBA_PLANTILLA = 14;
                                objTIVA.CL_PRUEBA = "TIVA";
                                objTIVA.CL_ESTADO = "CREADA";
                                objTIVA.FG_ASIGNADA = true;

                                lstPruebas.Add(objTIVA);
                            }
                            else
                            {
                                objTIVA.ID_PRUEBA_PLANTILLA = 14;
                                objTIVA.CL_PRUEBA = "TIVA";
                                objTIVA.CL_ESTADO = "CREADA";
                                objTIVA.FG_ASIGNADA = false;

                                lstPruebas.Add(objTIVA);
                            }
                            break;
                        case 15: //15	entrevista
                            if (item.Selected == true)
                            {
                                objEntrevista.ID_PRUEBA_PLANTILLA = 15;
                                objEntrevista.CL_PRUEBA = "ENTREVISTA";
                                objEntrevista.CL_ESTADO = "CREADA";
                                objEntrevista.FG_ASIGNADA = true;

                                lstPruebas.Add(objEntrevista);
                            }
                            else
                            {
                                objEntrevista.ID_PRUEBA_PLANTILLA = 15;
                                objEntrevista.CL_PRUEBA = "ENTREVISTA";
                                objEntrevista.CL_ESTADO = "CREADA";
                                objEntrevista.FG_ASIGNADA = false;

                                lstPruebas.Add(objEntrevista);
                            }
                            break;
                    }


                }

                //if (PersonalidadLab1.SelectedToggleStateIndex == 0)
                //{
                //    objLaboral.ID_PRUEBA_PLANTILLA = 6;
                //    objLaboral.CL_PRUEBA = "LABORAL-1";
                //    objLaboral.CL_ESTADO = "CREADA";
                //    objLaboral.FG_ASIGNADA = true;

                //    lstPruebas.Add(objLaboral);
                //}
                //else
                //{
                //    objLaboral.ID_PRUEBA_PLANTILLA = 6;
                //    objLaboral.CL_PRUEBA = "LABORAL-1";
                //    objLaboral.CL_ESTADO = "CREADA";
                //    objLaboral.FG_ASIGNADA = false;

                //    lstPruebas.Add(objLaboral);
                //}
                //if (InteresesPersonales.SelectedToggleStateIndex == 0)
                //{
                //    objInteres.ID_PRUEBA_PLANTILLA = 5;
                //    objInteres.CL_PRUEBA = "INTERES";
                //    objInteres.CL_ESTADO = "CREADA";
                //    objInteres.FG_ASIGNADA = true;

                //    lstPruebas.Add(objInteres);
                //}
                //else
                //{
                //    objInteres.ID_PRUEBA_PLANTILLA = 5;
                //    objInteres.CL_PRUEBA = "INTERES";
                //    objInteres.CL_ESTADO = "CREADA";
                //    objInteres.FG_ASIGNADA = false;

                //    lstPruebas.Add(objInteres);
                //}
                //if (EstiloPensamiento.SelectedToggleStateIndex == 0)
                //{
                //    objPensamiento.ID_PRUEBA_PLANTILLA = 11;
                //    objPensamiento.CL_PRUEBA = "PENSAMIENTO";
                //    objPensamiento.CL_ESTADO = "CREADA";
                //    objPensamiento.FG_ASIGNADA = true;

                //    lstPruebas.Add(objPensamiento);
                //}
                //else
                //{
                //    objPensamiento.ID_PRUEBA_PLANTILLA = 11;
                //    objPensamiento.CL_PRUEBA = "PENSAMIENTO";
                //    objPensamiento.CL_ESTADO = "CREADA";
                //    objPensamiento.FG_ASIGNADA = false;

                //    lstPruebas.Add(objPensamiento);
                //}
                //if (AptitupMental.SelectedToggleStateIndex == 0)
                //{
                //    objAptitud.ID_PRUEBA_PLANTILLA = 2;
                //    objAptitud.CL_PRUEBA = "APTITUD-1";
                //    objAptitud.CL_ESTADO = "CREADA";
                //    objAptitud.FG_ASIGNADA = true;

                //    lstPruebas.Add(objAptitud);
                //}
                //else
                //{
                //    objAptitud.ID_PRUEBA_PLANTILLA = 2;
                //    objAptitud.CL_PRUEBA = "APTITUD-1";
                //    objAptitud.CL_ESTADO = "CREADA";
                //    objAptitud.FG_ASIGNADA = false;

                //    lstPruebas.Add(objAptitud);
                //}
                //if (PersonalidadLaboralII.SelectedToggleStateIndex == 0)
                //{
                //    objLaboral2.ID_PRUEBA_PLANTILLA = 7;
                //    objLaboral2.CL_PRUEBA = "LABORAL-2";
                //    objLaboral2.CL_ESTADO = "CREADA";
                //    objLaboral2.FG_ASIGNADA = true;

                //    lstPruebas.Add(objLaboral2);
                //}
                //else
                //{
                //    objLaboral2.ID_PRUEBA_PLANTILLA = 7;
                //    objLaboral2.CL_PRUEBA = "LABORAL-2";
                //    objLaboral2.CL_ESTADO = "CREADA";
                //    objLaboral2.FG_ASIGNADA = false;

                //    lstPruebas.Add(objLaboral2);
                //}
                //if (Tiva.SelectedToggleStateIndex == 0)
                //{
                //    objTIVA.ID_PRUEBA_PLANTILLA = 14;
                //    objTIVA.CL_PRUEBA = "TIVA";
                //    objTIVA.CL_ESTADO = "CREADA";
                //    objTIVA.FG_ASIGNADA = true;

                //    lstPruebas.Add(objTIVA);
                //}
                //else
                //{
                //    objTIVA.ID_PRUEBA_PLANTILLA = 14;
                //    objTIVA.CL_PRUEBA = "TIVA";
                //    objTIVA.CL_ESTADO = "CREADA";
                //    objTIVA.FG_ASIGNADA = false;

                //    lstPruebas.Add(objTIVA);
                //}
                //if (OrtografiaI.SelectedToggleStateIndex == 0)
                //{
                //    objOrtografia.ID_PRUEBA_PLANTILLA = 8;
                //    objOrtografia.CL_PRUEBA = "ORTOGRAFIA-1";
                //    objOrtografia.CL_ESTADO = "CREADA";
                //    objOrtografia.FG_ASIGNADA = true;

                //    lstPruebas.Add(objOrtografia);
                //}
                //else
                //{
                //    objOrtografia.ID_PRUEBA_PLANTILLA = 8;
                //    objOrtografia.CL_PRUEBA = "ORTOGRAFIA-1";
                //    objOrtografia.CL_ESTADO = "CREADA";
                //    objOrtografia.FG_ASIGNADA = false;

                //    lstPruebas.Add(objOrtografia);
                //}
                //if (OrtografiaII.SelectedToggleStateIndex == 0)
                //{
                //    objOrtografia2.ID_PRUEBA_PLANTILLA = 9;
                //    objOrtografia2.CL_PRUEBA = "ORTOGRAFIA-2";
                //    objOrtografia2.CL_ESTADO = "CREADA";
                //    objOrtografia2.FG_ASIGNADA = true;

                //    lstPruebas.Add(objOrtografia2);
                //}
                //else
                //{
                //    objOrtografia2.ID_PRUEBA_PLANTILLA = 9;
                //    objOrtografia2.CL_PRUEBA = "ORTOGRAFIA-2";
                //    objOrtografia2.CL_ESTADO = "CREADA";
                //    objOrtografia2.FG_ASIGNADA = false;

                //    lstPruebas.Add(objOrtografia2);
                //}
                //if (OrtografiaIII.SelectedToggleStateIndex == 0)
                //{
                //    objOrtografia3.ID_PRUEBA_PLANTILLA = 10;
                //    objOrtografia3.CL_PRUEBA = "ORTOGRAFIA-3";
                //    objOrtografia3.CL_ESTADO = "CREADA";
                //    objOrtografia3.FG_ASIGNADA = true;

                //    lstPruebas.Add(objOrtografia3);
                //}
                //else
                //{
                //    objOrtografia3.ID_PRUEBA_PLANTILLA = 10;
                //    objOrtografia3.CL_PRUEBA = "ORTOGRAFIA-3";
                //    objOrtografia3.CL_ESTADO = "CREADA";
                //    objOrtografia3.FG_ASIGNADA = false;

                //    lstPruebas.Add(objOrtografia3);
                //}
                //if (TecnicaPC.SelectedToggleStateIndex == 0)
                //{
                //    objTecnicaPc.ID_PRUEBA_PLANTILLA = 13;
                //    objTecnicaPc.CL_PRUEBA = "TECNICAPC";
                //    objTecnicaPc.CL_ESTADO = "CREADA";
                //    objTecnicaPc.FG_ASIGNADA = true;

                //    lstPruebas.Add(objTecnicaPc);
                //}
                //else
                //{
                //    objTecnicaPc.ID_PRUEBA_PLANTILLA = 13;
                //    objTecnicaPc.CL_PRUEBA = "TECNICAPC";
                //    objTecnicaPc.CL_ESTADO = "CREADA";
                //    objTecnicaPc.FG_ASIGNADA = false;

                //    lstPruebas.Add(objTecnicaPc);
                //    }
                //if (Redaccion.SelectedToggleStateIndex == 0)
                //{
                //    objRedaccion.ID_PRUEBA_PLANTILLA = 12;
                //    objRedaccion.CL_PRUEBA = "REDACCION";
                //    objRedaccion.CL_ESTADO = "CREADA";
                //    objRedaccion.FG_ASIGNADA = true;

                //    lstPruebas.Add(objRedaccion);
                //}
                //else
                //{
                //    objRedaccion.ID_PRUEBA_PLANTILLA = 12;
                //    objRedaccion.CL_PRUEBA = "REDACCION";
                //    objRedaccion.CL_ESTADO = "CREADA";
                //    objRedaccion.FG_ASIGNADA = false;

                //    lstPruebas.Add(objRedaccion);
                //}
                //if (AptitupMentalII.SelectedToggleStateIndex == 0)
                //{
                //    objAptitud2.ID_PRUEBA_PLANTILLA = 3;
                //    objAptitud2.CL_PRUEBA = "APTITUD-2";
                //    objAptitud2.CL_ESTADO = "CREADA";
                //    objAptitud2.FG_ASIGNADA = true;

                //    lstPruebas.Add(objAptitud2);
                //}
                //else
                //{
                //    objAptitud2.ID_PRUEBA_PLANTILLA = 3;
                //    objAptitud2.CL_PRUEBA = "APTITUD-2";
                //    objAptitud2.CL_ESTADO = "CREADA";
                //    objAptitud2.FG_ASIGNADA = false;

                //    lstPruebas.Add(objAptitud2);
                //}
                //if (AdaptacionMiedo.SelectedToggleStateIndex == 0)
                //{
                //    objAdaptacion.ID_PRUEBA_PLANTILLA = 1;
                //    objAdaptacion.CL_PRUEBA = "ADAPTACION";
                //    objAdaptacion.CL_ESTADO = "CREADA";
                //    objAdaptacion.FG_ASIGNADA = true;

                //    lstPruebas.Add(objAdaptacion);
                //}
                //else
                //{
                //    objAdaptacion.ID_PRUEBA_PLANTILLA = 1;
                //    objAdaptacion.CL_PRUEBA = "ADAPTACION";
                //    objAdaptacion.CL_ESTADO = "CREADA";
                //    objAdaptacion.FG_ASIGNADA = false;

                //    lstPruebas.Add(objAdaptacion);
                //}
                //if (PruebaIngles.SelectedToggleStateIndex == 0)
                //{
                //    objIngles.ID_PRUEBA_PLANTILLA = 4;
                //    objIngles.CL_PRUEBA = "INGLES";
                //    objIngles.CL_ESTADO = "CREADA";
                //    objIngles.FG_ASIGNADA = true;

                //    lstPruebas.Add(objIngles);
                //}
                //else
                //{
                //    objIngles.ID_PRUEBA_PLANTILLA = 4;
                //    objIngles.CL_PRUEBA = "INGLES";
                //    objIngles.CL_ESTADO = "CREADA";
                //    objIngles.FG_ASIGNADA = false;

                //    lstPruebas.Add(objIngles);
                //}
                //if (PruebaEntrevista.SelectedToggleStateIndex == 0)
                //{
                //    objEntrevista.ID_PRUEBA_PLANTILLA = 15;
                //    objEntrevista.CL_PRUEBA = "ENTREVISTA";
                //    objEntrevista.CL_ESTADO = "CREADA";
                //    objEntrevista.FG_ASIGNADA = true;

                //    lstPruebas.Add(objEntrevista);
                //}
                //else
                //{
                //    objEntrevista.ID_PRUEBA_PLANTILLA = 15;
                //    objEntrevista.CL_PRUEBA = "ENTREVISTA";
                //    objEntrevista.CL_ESTADO = "CREADA";
                //    objEntrevista.FG_ASIGNADA = false;

                //    lstPruebas.Add(objEntrevista);
                //}

            }
            return lstPruebas;
        }

        protected void CargarDesdeContexto(List<int> pIdCandidatos)
        {

            foreach (int item in pIdCandidatos)
            {
                E_CANDIDATO f = new E_CANDIDATO
                {
                    ID_CANDIDATO = item
                };

                lstCandidatoS.Add(f);
            }


            var vXelementsCandidato = lstCandidatoS.Select(x =>
                                                        new XElement("CANDIDATO",
                                                        new XAttribute("ID_CANDIDATO", x.ID_CANDIDATO))
                                             ).Distinct();
            XElement xmlCandidatos = new XElement("CANDIDATOS", vXelementsCandidato);

            CandidatoNegocio nCandidato = new CandidatoNegocio();
            lstCandidatos = nCandidato.ObtieneCandidatosBateria(xmlCandidatos);

            lstCandidatoS = new List<E_CANDIDATO>();
            foreach (var item in lstCandidatos)
            {
                E_CANDIDATO f = new E_CANDIDATO
                {
                    CL_SOLICITUD = item.CL_SOLICITUD,
                    NB_CANDIDATO = item.NB_CANDIDATO_COMPLETO,
                    ID_CANDIDATO = item.ID_CANDIDATO,
                    FL_BATERIA = ((item.FOLIO_BATERIA != null) ? (item.FOLIO_BATERIA) : ""),
                    ID_BATERIA = ((item.ID_BATERIA != null) ? ((int)item.ID_BATERIA) : 0)
                };

                lstCandidatoS.Add(f);
            }

            //grdCandidatos.Rebind();
        }

        protected void GenerarBaterias(string clTipoAplicacion)
        {
             if (vIdBateria == null)
            {
                //Validaciones
                if (radCmbNiveles.SelectedValue == "")
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona un nivel.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                }
                //else if (radCmbNiveles.SelectedValue == "COP" && radCmbPuesto.SelectedValue == "")
                else if (radCmbNiveles.SelectedValue == "COP" && lstPuesto.SelectedValue == "")
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona un puesto.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                }
                else if (lstCandidatoS.Count <= 0)
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "Agrega al menos un candidato.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                }
                else
                {
                    //CREAR LISTA DE CANDIDATOS
                    //lstCandidatos = new List<E_CANDIDATO>();
                    //foreach (RadListBoxItem lbItem in listCandidatos.Items)
                    //{
                    //    E_CANDIDATO objCandidato = new E_CANDIDATO();
                    //    objCandidato.ID_CANDIDATO = int.Parse(lbItem.Value);
                    //    lstCandidatos.Add(objCandidato);
                    //}
                    //add pruebas
                    List<E_PRUEBA_CANDIDATO> lstPruebas = obtenPruebasSeleccionadas();
                    if (lstPruebas.Count() <= 0)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, "Ninguna prueba disponible.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                    }
                    else
                    {
                        //Generar XML's
                        var vXelementsCandidato = lstCandidatoS.Select(x =>
                                                   new XElement("CANDIDATO",
                                                   new XAttribute("ID_CANDIDATO", x.ID_CANDIDATO))
                                        ).Distinct();
                        XElement xmlCandidatos = new XElement("CANDIDATOS", vXelementsCandidato);


                        var vXelementsPrueba = lstPruebas.Select(x =>
                                                new XElement("PRUEBA",
                                                new XAttribute("ID_PRUEBA_PLANTILLA", x.ID_PRUEBA_PLANTILLA),
                                                new XAttribute("CL_PRUEBA", x.CL_PRUEBA),
                                                new XAttribute("CL_ESTADO", x.CL_ESTADO),
                                                new XAttribute("FG_ASIGNADA", x.FG_ASIGNADA))
                                    );
                        XElement xmlPruebas = new XElement("PRUEBAS", vXelementsPrueba);


                        //Guardar 
                        PruebasNegocio objInsertar = new PruebasNegocio();
                        var resultado = objInsertar.generarPruebasEmpleado(xmlCandidatos, xmlPruebas, vClUsuario, vNbPrograma);
                        string vMensaje = resultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        if (resultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                        {

                            //UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, resultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
                            if (clTipoAplicacion == "EXTERNA")
                            ClientScript.RegisterStartupScript(GetType(), "script", "OpenEnviarCorreos();", true);

                        }
                        else
                        {
                            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                        }

                    }
                }
            }
            //else
            //{
            //    //Validaciones
            //    if (radCmbNiveles.SelectedValue == "")
            //    {
            //        UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona un nivel.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            //    }
            //    //else if (radCmbNiveles.SelectedValue == "COP" && radCmbPuesto.SelectedValue == "")
            //    else if (radCmbNiveles.SelectedValue == "COP" && lstPuesto.SelectedValue == "")
            //    {
            //        UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona un puesto.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            //    }
            //    else
            //    {

            //        List<E_PRUEBA_CANDIDATO> lstPruebas = obtenPruebasSeleccionadas();
            //        if (lstPruebas.Count() <= 0)
            //        {
            //            UtilMensajes.MensajeResultadoDB(rwmAlertas, "Ninguna prueba disponible.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            //        }
            //        else
            //        {
            //            //Generar XML's
            //            var vXelementsPrueba = lstPruebas.Select(x =>
            //                                    new XElement("PRUEBA",
            //                                    new XAttribute("ID_PRUEBA_PLANTILLA", x.ID_PRUEBA_PLANTILLA),
            //                                    new XAttribute("CL_PRUEBA", x.CL_PRUEBA),
            //                                    new XAttribute("CL_ESTADO", x.CL_ESTADO),
            //                                    new XAttribute("FG_ASIGNADA", x.FG_ASIGNADA))
            //                        );
            //            XElement xmlPruebas = new XElement("PRUEBAS", vXelementsPrueba);


            //            PruebasNegocio objInsertar = new PruebasNegocio();
            //            var resultado = objInsertar.ActualizarPruebasEmpleado(xmlPruebas, vIdBateria, vClUsuario, vNbPrograma);
            //            string vMensaje = resultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            //            if (resultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            //            {
            //                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, resultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");

            //            }
            //            else
            //            {
            //                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            //            }
            //        }
            //    }
            //}
        }


        protected void SeleccionpruebasNivelEje()
        {
            foreach(GridDataItem item in grdPruebas.Items)
            {
                switch(int.Parse(item.GetDataKeyValue("ID_PRUEBA").ToString())){
                    case 1: //1 Adaptación al medio
                        item.Selected = true;
                        break;
                    case 2: //2	Mental I
                        item.Selected = true;
                        break;
                    case 3: //3	Mental II
                        item.Selected = false;
                        break;
                    case 4: //4	Inglés
                        item.Selected = false;
                        break;
                    case 5: //5	Intereses personales
                        item.Selected = true;
                        break;
                    case 6: //6	Laboral I
                        item.Selected = true;
                        break;
                    case 7: //7	Laboral II
                        item.Selected = true;
                        break;
                    case 8: //8	Otografía I
                        item.Selected = true;
                        break;
                    case 9: //9	Ortografia II
                        item.Selected = true;
                        break;
                    case 10: //10	Ortografia III
                        item.Selected = true;
                        break;
                    case 11: //11	Estilo de pensamiento
                        item.Selected = true;
                        break;
                    case 12: //12	Redacción
                        item.Selected = true;
                        break;
                    case 13: //13	Técnica PC
                        item.Selected = true;
                        break;
                    case 14: //14	TIVA
                        item.Selected = true;
                        break;
                    case 15: //15	entrevista
                        item.Selected = true;
                        break;
                }
            }
        }

        protected void SeleccionpruebasNivelOpe()
        {
            foreach (GridDataItem item in grdPruebas.Items)
            {
                switch (int.Parse(item.GetDataKeyValue("ID_PRUEBA").ToString()))
                {
                    case 1: //1 Adaptación al medio
                        item.Selected = true;
                        break;
                    case 2: //2	Mental I
                        item.Selected = false;
                        break;
                    case 3: //3	Mental II
                        item.Selected = true;
                        break;
                    case 4: //4	Inglés
                        item.Selected = false;
                        break;
                    case 5: //5	Intereses personales
                        item.Selected = true;
                        break;
                    case 6: //6	Laboral I
                        item.Selected = false;
                        break;
                    case 7: //7	Laboral II
                        item.Selected = true;
                        break;
                    case 8: //8	Otografía I
                        item.Selected = true;
                        break;
                    case 9: //9	Ortografia II
                        item.Selected = true;
                        break;
                    case 10: //10	Ortografia III
                        item.Selected = true;
                        break;
                    case 11: //11	Estilo de pensamiento
                        item.Selected = true;
                        break;
                    case 12: //12	Redacción
                        item.Selected = true;
                        break;
                    case 13: //13	Técnica PC
                        item.Selected = true;
                        break;
                    case 14: //14	TIVA
                        item.Selected = true;
                        break;
                    case 15: //15	entrevista
                        item.Selected = true;
                        break;
                }
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            List<SPE_OBTIENE_CANDIDATOS_BATERIA_Result> lstCandidatos = new List<SPE_OBTIENE_CANDIDATOS_BATERIA_Result>();

          //  agregarTooltips();

            if (!IsPostBack)
            {

                lstCandidatoS = new List<E_CANDIDATO>();
                PuestoNegocio negocio = new PuestoNegocio();
                //radCmbPuesto.DataValueField = "ID_PUESTO";
                //radCmbPuesto.DataTextField = "NB_PUESTO";
                //radCmbPuesto.DataSource = negocio.ObtienePuestos();
                //radCmbPuesto.DataBind();
                CambiarEstatusDePruebas(false);

                radSliderNivel.SelectedValue = radSliderNivel.Items[1].Value;

                if (Request.Params["pIdCandidatosPruebas"] != null)
                {
                    vIdCandidatosPruebas = Guid.Parse(Request.Params["pIdCandidatosPruebas"].ToString());
                    if (ContextoCandidatosBateria.oCandidatosBateria.Where(w => w.vIdGeneraBaterias == vIdCandidatosPruebas).FirstOrDefault().vListaCandidatos.Count > 0)
                        CargarDesdeContexto(ContextoCandidatosBateria.oCandidatosBateria.Where(w => w.vIdGeneraBaterias == vIdCandidatosPruebas ).FirstOrDefault().vListaCandidatos);
                }

                if (Request.Params["pIdBateria"] != null)
                {
                    vIdBateria = int.Parse(Request.Params["pIdBateria"]);
                    if (vIdBateria != null)
                    {
                        //PruebasNegocio nPruebas = new PruebasNegocio();
                        //var vBateria = nPruebas.ObtieneBateria(pIdBateria: vIdBateria).FirstOrDefault();
                        rtsConfiguracion.Tabs.ElementAt(0).Visible = false;
                        rmpCapturaResultados.SelectedIndex = 1;
                        radCmbNiveles.SelectedValue = "PER";
                        radCmbNiveles.Enabled = false;
                         ClientScript.RegisterStartupScript(GetType(), "script", "EditPruebas();", true);
                        // btnGenerar.Text = "Aceptar";

                        PruebasNegocio pruebas = new PruebasNegocio();
                        List<SPE_OBTIENE_K_PRUEBA_Result> vLstPruebas = new List<SPE_OBTIENE_K_PRUEBA_Result>();
                        vLstPruebas = pruebas.Obtener_K_PRUEBA(pIdBateria: vIdBateria, pFgAsignada: true);
                        SeleccionarPruebas(vLstPruebas);
                        //if (vBateria != null)
                        //{
                        //    if (vBateria.ESTATUS.Equals("TERMINADA"))
                        //    {
                        //        UtilMensajes.MensajeResultadoDB(rwmAlertas, "Este candidato cuenta con pruebas concluidas. Si se vuelven a generar pruebas iguales a las concluidas se eliminaran los resultados anteriores de manera automática.", E_TIPO_RESPUESTA_DB.WARNING, 400, 200, "");
                        //    }
                        //}
                    }
               }

            }
        }

        protected void radCmbPuesto_Load(object sender, EventArgs e)
        {
            PuestoNegocio negocio = new PuestoNegocio();
            //radCmbPuesto.DataValueField = "ID_PUESTO";
            //radCmbPuesto.DataTextField = "NB_PUESTO";
            //radCmbPuesto.DataSource = negocio.ObtienePuestos();
            //radCmbPuesto.DataBind();
        }

        protected void radCmbNiveles_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var value = e.Value;
            if (value == "EJE")
            {
                grdPruebas.ClientSettings.Selecting.AllowRowSelect = true;
                SeleccionpruebasNivelEje();
                grdPruebas.ClientSettings.Selecting.AllowRowSelect = false;
                //CambiarEstatusDePruebas(false);
                //ActivarDesactivarPrueba(PersonalidadLab1, true);
                //ActivarDesactivarPrueba(InteresesPersonales, true);
                //ActivarDesactivarPrueba(EstiloPensamiento, true);
                //ActivarDesactivarPrueba(AptitupMental, true);
                //ActivarDesactivarPrueba(PersonalidadLaboralII, true);
                //ActivarDesactivarPrueba(Tiva, true);
                //ActivarDesactivarPrueba(OrtografiaI, true);
                //ActivarDesactivarPrueba(OrtografiaII, true);
                //ActivarDesactivarPrueba(OrtografiaIII, true);
                //ActivarDesactivarPrueba(TecnicaPC, true);
                //ActivarDesactivarPrueba(Redaccion, true);
                //ActivarDesactivarPrueba(AptitupMentalII, false);
                //ActivarDesactivarPrueba(AdaptacionMiedo, true);
                //ActivarDesactivarPrueba(PruebaIngles, false);
                //ActivarDesactivarPrueba(PruebaEntrevista, true);
            }
            else if (value == "OPE")
            {
                grdPruebas.ClientSettings.Selecting.AllowRowSelect = true;
                SeleccionpruebasNivelOpe();
                grdPruebas.ClientSettings.Selecting.AllowRowSelect = false;
                //CambiarEstatusDePruebas(false);
                //ActivarDesactivarPrueba(InteresesPersonales, true);
                //ActivarDesactivarPrueba(EstiloPensamiento, true);
                //ActivarDesactivarPrueba(AptitupMental, false);
                //ActivarDesactivarPrueba(PersonalidadLaboralII, true);
                //ActivarDesactivarPrueba(Tiva, true);
                //ActivarDesactivarPrueba(OrtografiaI, true);
                //ActivarDesactivarPrueba(OrtografiaII, true);
                //ActivarDesactivarPrueba(OrtografiaIII, true);
                //ActivarDesactivarPrueba(TecnicaPC, true);
                //ActivarDesactivarPrueba(Redaccion, true);
                //ActivarDesactivarPrueba(PersonalidadLab1, false);
                //ActivarDesactivarPrueba(AptitupMentalII, true);
                //ActivarDesactivarPrueba(AdaptacionMiedo, true);
                //ActivarDesactivarPrueba(PruebaIngles, false);
                //ActivarDesactivarPrueba(PruebaEntrevista, true);
            }
            else if (value == "PER")
            {
                CambiarEstatusDePruebas(true);
            }
            else
            {
                grdPruebas.SelectedIndexes.Clear();
                CambiarEstatusDePruebas(false);
                radSliderNivel.SelectedValue = radSliderNivel.Items[2].Value;
                //radCmbPuesto.ClearSelection();
                //radCmbPuesto.Text = string.Empty;

                //TODO: ver como limpiar la lista aqui

                lstPuesto.Items.Clear();
                RadListBoxItem item = new RadListBoxItem();
                item.Value = string.Empty;
                item.Text = "No Seleccionado";
                item.Selected = true;
                lstPuesto.Items.Add(item);      
            }
        }

        protected void radCmbPuesto_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //PruebasNegocio negocio = new PruebasNegocio();
            //if (radCmbPuesto.SelectedValue != "")
            //{
            //    decimal? v = 0;
            //    v = decimal.Parse(radSliderNivel.SelectedItem.Value);
            //    var vPruebas = negocio.Obtener_PRUEBA_POR_PUESTO(int.Parse(radCmbPuesto.SelectedValue),v);
            //    if (vPruebas.Count() > 0 )
            //    {
            //        CambiarEstatusDePruebas(false);
            //    }
            Pruebas();
            //foreach (SPE_OBTIENE_PRUEBA_POR_PUESTO_Result prueba in vPruebas)
            //{
            //    switch (prueba.ID_PRUEBA)
            //    {
            //        case 1: //Personalidad laboral I
            //            ActivarDesactivarPrueba(AdaptacionMiedo, true);
            //            break;
            //        case 2: //2	Intereses personales
            //            ActivarDesactivarPrueba(AptitupMental, true);
            //            break;
            //        case 3: //3	Estilo de pensamiento
            //            ActivarDesactivarPrueba(AptitupMentalII, true);
            //            break;
            //        case 4: //4	Aptitud mental I
            //            ActivarDesactivarPrueba(PruebaIngles, true);
            //            break;
            //        case 5: //5	Aptitud mental II
            //            ActivarDesactivarPrueba(InteresesPersonales, true);
            //            break;
            //        case 6: //6	Personalidad laboral II
            //            ActivarDesactivarPrueba(PersonalidadLab1, true);
            //            break;
            //        case 7: //7	Adaptación al medio
            //            ActivarDesactivarPrueba(PersonalidadLaboralII, true);
            //            break;
            //        case 8: //8	TIVA
            //            ActivarDesactivarPrueba(OrtografiaI, true);
            //            break;
            //        case 9: //9	Ortografia I
            //            ActivarDesactivarPrueba(OrtografiaII, true);
            //            break;
            //        case 10: //10	Ortografia II
            //            ActivarDesactivarPrueba(OrtografiaIII, true);
            //            break;
            //        case 11: //11	Ortografia III
            //            ActivarDesactivarPrueba(EstiloPensamiento, true);
            //            break;
            //        case 12: //12	Técnica PC
            //            ActivarDesactivarPrueba(Redaccion, true);
            //            break;
            //        case 13: //13	Redacción
            //            ActivarDesactivarPrueba(TecnicaPC, true);
            //            break;
            //        case 14: //14	Pruebas de inglés
            //            ActivarDesactivarPrueba(Tiva, true);
            //            break;
            //    }
            //}
            //}
        }

        protected void radSliderNivel_ValueChanged(object sender, EventArgs e)
        {
            Pruebas();
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            if (vIdBateria == null)
            {
                //Validaciones
                if (radCmbNiveles.SelectedValue == "")
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona un nivel.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                }
                //else if (radCmbNiveles.SelectedValue == "COP" && radCmbPuesto.SelectedValue == "")
                else if (radCmbNiveles.SelectedValue == "COP" && lstPuesto.SelectedValue == "")
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona un puesto.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                }
                else if (lstCandidatoS.Count <= 0)
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "Agrega al menos un candidato.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                }
                else
                {
                    //CREAR LISTA DE CANDIDATOS
                    //lstCandidatos = new List<E_CANDIDATO>();
                    //foreach (RadListBoxItem lbItem in listCandidatos.Items)
                    //{
                    //    E_CANDIDATO objCandidato = new E_CANDIDATO();
                    //    objCandidato.ID_CANDIDATO = int.Parse(lbItem.Value);
                    //    lstCandidatos.Add(objCandidato);
                    //}
                    //add pruebas
                    List<E_PRUEBA_CANDIDATO> lstPruebas = obtenPruebasSeleccionadas();
                    if (lstPruebas.Count() <= 0)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, "Ninguna prueba disponible.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                    }
                    else
                    {
                        //Generar XML's
                        var vXelementsCandidato = lstCandidatoS.Select(x =>
                                                   new XElement("CANDIDATO",
                                                   new XAttribute("ID_CANDIDATO", x.ID_CANDIDATO))
                                        ).Distinct();
                        XElement xmlCandidatos = new XElement("CANDIDATOS", vXelementsCandidato);


                        var vXelementsPrueba = lstPruebas.Select(x =>
                                                new XElement("PRUEBA",
                                                new XAttribute("ID_PRUEBA_PLANTILLA", x.ID_PRUEBA_PLANTILLA),
                                                new XAttribute("CL_PRUEBA", x.CL_PRUEBA),
                                                new XAttribute("CL_ESTADO", x.CL_ESTADO),
                                                new XAttribute("FG_ASIGNADA", x.FG_ASIGNADA))
                                    );
                        XElement xmlPruebas = new XElement("PRUEBAS", vXelementsPrueba);


                        //Guardar 
                        PruebasNegocio objInsertar = new PruebasNegocio();
                        var resultado = objInsertar.generarPruebasEmpleado(xmlCandidatos, xmlPruebas, vClUsuario, vNbPrograma);
                        string vMensaje = resultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        if (resultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                        {
                            UtilMensajes.MensajeResultadoDB(rwmAlertas, "Pruebas asignadas con éxito.", resultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");

                        }
                        else
                        {
                            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                        }

                    }
                }
            }
            else
            {
                //Validaciones
                if (radCmbNiveles.SelectedValue == "")
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona un nivel.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                }
                //else if (radCmbNiveles.SelectedValue == "COP" && radCmbPuesto.SelectedValue == "")
                else if (radCmbNiveles.SelectedValue == "COP" && lstPuesto.SelectedValue == "")
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona un puesto.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                }
                else
                {

                    List<E_PRUEBA_CANDIDATO> lstPruebas = obtenPruebasSeleccionadas();
                    if (lstPruebas.Count() <= 0)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, "Ninguna prueba disponible.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                    }
                    else
                    {
                        //Generar XML's
                        var vXelementsPrueba = lstPruebas.Select(x =>
                                                new XElement("PRUEBA",
                                                new XAttribute("ID_PRUEBA_PLANTILLA", x.ID_PRUEBA_PLANTILLA),
                                                new XAttribute("CL_PRUEBA", x.CL_PRUEBA),
                                                new XAttribute("CL_ESTADO", x.CL_ESTADO),
                                                new XAttribute("FG_ASIGNADA", x.FG_ASIGNADA))
                                    );
                        XElement xmlPruebas = new XElement("PRUEBAS", vXelementsPrueba);


                        PruebasNegocio objInsertar = new PruebasNegocio();
                        var resultado = objInsertar.ActualizarPruebasEmpleado(xmlPruebas, vIdBateria, vClUsuario, vNbPrograma);
                        string vMensaje = resultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        if (resultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                        {
                            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, resultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");

                        }
                        else
                        {
                            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                        }
                    }
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/IDP/AplicacionPruebas.aspx");
        }

        protected void btnDelCandidato_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdCandidatos.SelectedItems)
            {

                int dataKey = int.Parse(item.GetDataKeyValue("ID_CANDIDATO").ToString());
                E_CANDIDATO es = lstCandidatoS.Where(t => t.ID_CANDIDATO == dataKey).FirstOrDefault();

                if (es != null)
                {
                    lstCandidatoS.Remove(es);
                }
            }
            grdCandidatos.Rebind();
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            
            string pParameter = e.Argument;
            if (pParameter.Equals("Puesto"))
            {
                Pruebas();
            }
            else
            {

            E_SELECTOR vSeleccion = new E_SELECTOR();
            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);
            List<E_SELECTOR_CANDIDATO> vSeleccionados = JsonConvert.DeserializeObject<List<E_SELECTOR_CANDIDATO>>(vSeleccion.oSeleccion.ToString());

                if (vSeleccionados.Count > 0)
                {
                    foreach (var item in vSeleccionados)
                    {
                        E_CANDIDATO f = new E_CANDIDATO
                        {
                            ID_CANDIDATO = item.idCandidato
                        };

                        lstCandidatoS.Add(f);
                    }

                    var vXelementsCandidato = lstCandidatoS.Select(x =>
                                                                new XElement("CANDIDATO",
                                                                new XAttribute("ID_CANDIDATO", x.ID_CANDIDATO))
                                                     ).Distinct();
                    XElement xmlCandidatos = new XElement("CANDIDATOS", vXelementsCandidato);

                    CandidatoNegocio nCandidato = new CandidatoNegocio();
                    lstCandidatos = nCandidato.ObtieneCandidatosBateria(xmlCandidatos);

                    lstCandidatoS = new List<E_CANDIDATO>();
                    foreach (var item in lstCandidatos)
                    {
                        E_CANDIDATO f = new E_CANDIDATO
                        {
                            CL_SOLICITUD = item.CL_SOLICITUD,
                            NB_CANDIDATO = item.NB_CANDIDATO_COMPLETO,
                            ID_CANDIDATO = item.ID_CANDIDATO,
                            FL_BATERIA = ((item.FOLIO_BATERIA != null) ? (item.FOLIO_BATERIA) : ""),
                            ID_BATERIA = ((item.ID_BATERIA != null) ? ((int)item.ID_BATERIA) : 0)
                        };

                        lstCandidatoS.Add(f);
                    }

                    grdCandidatos.Rebind();

                }
            }

        }

        protected void grdCandidatos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCandidatos.DataSource = lstCandidatoS;
            //if (lstCandidatoS.Count > 1)
            //{
            //    btnAplicacionInterna.Enabled = false;
            //}
        }

        protected void grdCandidatos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                var id_bateria = item.GetDataKeyValue("ID_BATERIA").ToString();
                if (id_bateria != "0")
                {
                    int vIdBateria = int.Parse(id_bateria);
                    //UtilMensajes.MensajeResultadoDB(rwmAlertas, "FALTA FUNCIONALIDAD", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");

                    PruebasNegocio nPruebas = new PruebasNegocio();
                    var resultado = nPruebas.EliminaRespuestasBaterias(vIdBateria, vClUsuario, vNbPrograma);
                    string vMensaje = resultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    if (resultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, resultado.CL_TIPO_ERROR, pCallBackFunction: "");
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                    }

                }
                else
                {

                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "No cuenta con baterías creadas", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                }
            }
        }

        protected void lstPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (lstPuesto.SelectedValue != "")
            //{
            //    Pruebas();
            //}
        }

        protected void lstPuesto_TextChanged(object sender, EventArgs e)
        {
            if (lstPuesto.SelectedValue != "")
            {
                //Pruebas();
            }
        }

        //protected void btnAplicacionExterna_Click(object sender, EventArgs e)
        //{
        //    GenerarBaterias("EXTERNA");
        //}

        //protected void btnAplicacionInterna_Click(object sender, EventArgs e)
        //{
        //    GenerarBaterias("INTERNA");

        //      PruebasNegocio pruebas = new PruebasNegocio();

        //          GridDataItem item = grdCandidatos.Items[0];
        //          string vIdCandidato =  item.GetDataKeyValue("ID_CANDIDATO").ToString();
        //          if(vIdCandidato != null)
        //          {
        //              var vCandidatosPruebas = pruebas.ObtieneBateria(pIdCandidato: vIdCandidato).ToList().OrderByDescending(o => o.ID_BATERIA);
        //          if (vCandidatosPruebas != null)
        //          {
        //              vClToken = vCandidatosPruebas.FirstOrDefault().CL_TOKEN;
        //              vFlBateria = vCandidatosPruebas.FirstOrDefault().ID_BATERIA;
        //              vIdCandidatoBateria = vCandidatosPruebas.FirstOrDefault().ID_CANDIDATO;

        //               ClientScript.RegisterStartupScript(GetType(), "script", "OpenAplicarPruebasInterna();", true);                    
        //          }
        //          }
        //}

        protected void grdPruebas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PruebasNegocio pNeg = new PruebasNegocio();
            grdPruebas.DataSource = pNeg.ObtenerPruebasConfiguradas();
        }
       
    }
}