using SIGE.Entidades;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaCapturaManualPruebas : System.Web.UI.Page
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


        #endregion

        protected void HabilitaPuebas(List<SPE_OBTIENE_K_PRUEBA_Result> pCandidatosPruebas)
        {
            foreach (SPE_OBTIENE_K_PRUEBA_Result item in pCandidatosPruebas.OrderByDescending(o => o.NO_ORDEN))
            {
                switch (item.ID_PRUEBA_PLANTILLA)
                {
                    case 1: //1 Adaptación al medio
                        tbCapturaManual.Tabs[6].Enabled = true;
                        tbCapturaManual.Tabs[6].Selected = true;
                        rpvAdáptacion.Selected = true;
                        ifAdaptacion.Attributes.Add("src", "VentanaAdaptacionMedioManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO);
                        break;
                    case 2: //2	Mental I
                        tbCapturaManual.Tabs[3].Enabled = true;
                        tbCapturaManual.Tabs[3].Selected = true;
                        rpvMentalI.Selected = true;
                        ifMentalI.Attributes.Add("src", "VentanaAptitudMentalIManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO);
                        break;
                    case 3: //3	Mental II
                        tbCapturaManual.Tabs[4].Enabled = true;
                        tbCapturaManual.Tabs[4].Selected = true;
                        rpvMentalII.Selected = true;
                        ifMentalII.Attributes.Add("src", "VentanaAptitudMentalIIManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO);
                        break;
                    case 4: //4	Inglés
                        tbCapturaManual.Tabs[13].Enabled = true;
                        tbCapturaManual.Tabs[13].Selected = true;
                        rpvIngles.Selected = true;
                        ifIngles.Attributes.Add("src", "VentanaInglesManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO);
                        break;
                    case 5: //5	Intereses personales
                        tbCapturaManual.Tabs[1].Enabled = true;
                        tbCapturaManual.Tabs[1].Selected = true;
                        rpvInteresesPersonales.Selected = true;
                        ifInteresesPersonales.Attributes.Add("src", "VentanaInteresesPersonalesManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO);
                        break;
                    case 6: //6	Laboral I
                        tbCapturaManual.Tabs[0].Enabled = true;
                        tbCapturaManual.Tabs[0].Selected = true;
                        rpvLaboralI.Selected = true;
                        ifLaboralI.Attributes.Add("src", "VentanaPersonalidadLabIManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO);
                        break;
                    case 7: //7 Laboral II
                        tbCapturaManual.Tabs[5].Enabled = true;
                        tbCapturaManual.Tabs[5].Selected = true;
                        rpvLaboralI.Selected = true;
                        ifLaboralII.Attributes.Add("src", "VentanaPersonalidadLaboral2Manual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO);
                        break;
                    case 8: //8	Otografía I
                        tbCapturaManual.Tabs[8].Enabled = true;
                        tbCapturaManual.Tabs[8].Selected = true;
                        rpvOrtgrafiaII.Selected = true;
                        ifOrtografiaI.Attributes.Add("src", "VentanaOrtografiaManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&CLAVE=" + item.CL_PRUEBA);
                        break;
                    case 9: //9	Ortografia II
                        tbCapturaManual.Tabs[9].Enabled = true;
                        tbCapturaManual.Tabs[9].Selected = true;
                        rpvOrtgrafiaII.Selected = true;
                        ifOrtografiaII.Attributes.Add("src", "VentanaOrtografiaManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&CLAVE=" + item.CL_PRUEBA);
                        break;
                    case 10: //10 Ortografia III
                        tbCapturaManual.Tabs[10].Enabled = true;
                        tbCapturaManual.Tabs[10].Enabled = true;
                        rpvOrtografiaIII.Selected = true;
                        ifOrtografiaIII.Attributes.Add("src", "VentanaOrtografiaManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO + "&CLAVE=" + item.CL_PRUEBA);
                        break;
                    case 11: //11 Estilo de pensamiento
                        tbCapturaManual.Tabs[2].Enabled = true;
                        tbCapturaManual.Tabs[2].Selected = true;
                        rpvEstilo.Selected = true;
                        ifEstilo.Attributes.Add("src", "VentanaEstiloDePensamientoManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO );
                        break;
                    case 12: //12 Redacción
                        tbCapturaManual.Tabs[12].Enabled = true;
                        tbCapturaManual.Tabs[12].Selected = true;
                        rpvRedaccion.Selected = true;
                        ifRedaccion.Attributes.Add("src", "VentanaRedaccionManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO);
                        break;
                    case 13: //13 Técnica PC
                        tbCapturaManual.Tabs[11].Enabled = true;
                        tbCapturaManual.Tabs[11].Selected = true;
                        rpvTecnica.Selected = true;
                        ifTecnica.Attributes.Add("src", "VentanaTecnicaPCManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO);
                        break;
                    case 14: //14 TIVA
                        tbCapturaManual.Tabs[7].Enabled = true;
                        tbCapturaManual.Tabs[7].Selected = true;
                        rpvTiva.Selected = true;
                        ifTiva.Attributes.Add("src", "VentanaTIVAManual.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO);
                        break;
                    case 15: //15 Entrevista
                        tbCapturaManual.Tabs[14].Enabled = true;
                        tbCapturaManual.Tabs[14].Selected = true;
                        rpvFactoresAdicionales.Selected = true;
                        ifFactoresAdicionales.Attributes.Add("src", "VentanaResultadosEntrevista.aspx?ID=" + item.ID_PRUEBA + "&&T=" + item.CL_TOKEN_EXTERNO);
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
        }
    }
}