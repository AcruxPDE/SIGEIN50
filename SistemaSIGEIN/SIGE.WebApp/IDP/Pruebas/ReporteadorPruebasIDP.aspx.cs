using SIGE.Entidades;
using SIGE.Negocio.Administracion;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.IDP
{
    public partial class ReporteadorPruebasIDP : System.Web.UI.Page
    {
        #region Variables

        private int vIdPrueba
        {
            get { return (int)ViewState["vs_rpi_id_prueba"]; }
            set { ViewState["vs_rpi_id_prueba"] = value; }
        }

        private Guid vClToken
        {
            get { return (Guid)ViewState["vs_rpi_cl_token"]; }
            set { ViewState["vs_rpi_cl_token"] = value; }
        }

        private string vClPrueba
        {
            get { return (string)ViewState["vs_rpi_cl_prueba"]; }
            set { ViewState["vs_rpi_cl_prueba"] = value; }
        }

        #endregion

        #region Funciones



        public void OrtografiaIIIRespuestas(string pAnswer, string pVariable, StiReport pReport)
        {
            if (pAnswer != null)
            {
                string vListaPalabras = "";
                var vPalabrasContestadas = MensajesPruebaLaboralI(XElement.Parse(pAnswer));
                foreach (var item in vPalabrasContestadas)
                {
                    vListaPalabras = vListaPalabras + (" " + item.NB_RESPUESTA + "\r\n");
                }

                pReport.Dictionary.Variables["RespList1"].Value = vListaPalabras;
            }
            else
            {
               pReport.Dictionary.Variables["RespList1"].Value = "";
            }

        }

        public List<E_ORTOGRAFIA_III> MensajesPruebaLaboralI(XElement pRespuesta)
        {
            List<E_ORTOGRAFIA_III> vListaRespuestas = pRespuesta.Elements("ANSWER").Select(el => new E_ORTOGRAFIA_III
            {
                NB_RESPUESTA = el.Attribute("VALOR").Value,
            }).ToList();
            return vListaRespuestas;
        }

        private void AsignarValorLaboral1(StiReport pReport, string pVariable, List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pRespuestas, string pClPregunta)
        {
            var pPregunta = pRespuestas.Where(t => t.CL_PREGUNTA.Equals(pClPregunta)).FirstOrDefault();

            if (pPregunta != null)
            {
                if (!string.IsNullOrEmpty(pPregunta.NB_RESPUESTA))
                {
                    pReport.Dictionary.Variables[pVariable].Value = pPregunta.NB_RESPUESTA;
                }
            }
        }

        private void AsignarValorLaboral1D(StiReport pReport, string pVariable1, string pVariable2, List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pRespuestas, string pClPregunta)
        {
            var pPregunta = pRespuestas.Where(t => t.CL_PREGUNTA.Equals(pClPregunta)).FirstOrDefault();

            if (pPregunta != null)
            {
                if (!string.IsNullOrEmpty(pPregunta.NB_RESPUESTA))
                {
                    string vrespuesta1 = pPregunta.NB_RESPUESTA.Substring(0, 1);
                    string vrespuesta2 = pPregunta.NB_RESPUESTA.Replace(vrespuesta1, "");

                    pReport.Dictionary.Variables[pVariable1].Value = vrespuesta1;
                    pReport.Dictionary.Variables[pVariable2].Value = vrespuesta2;
                }
            }
        }

        public string AsignarValorOrt1(string pValor)
        {
            string vValorCampo = "";
            if (pValor == "_")
                vValorCampo = "";
            else if (pValor == null)
                vValorCampo = "";
            else
                vValorCampo = pValor;

            return vValorCampo;
        }

        private void AsignarValorOrtografiaI(StiReport pReport, string pVariable1, string pVariable2, string pVariable3,  List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> pRespuestas, string pClPregunta)
        {
            var pPregunta = pRespuestas.Where(t => t.CL_PREGUNTA.Equals(pClPregunta)).FirstOrDefault();

            if (pPregunta != null)
            {
                if (!string.IsNullOrEmpty(pPregunta.NB_RESPUESTA))
                {
                    if (pVariable1 != "")
                    {
                        pReport.Dictionary.Variables[pVariable1].Value = AsignarValorOrt1(pPregunta.NB_RESPUESTA.ToCharArray()[0].ToString());
                    }
                    if (pVariable2 != "")
                    {
                        pReport.Dictionary.Variables[pVariable2].Value = AsignarValorOrt1(pPregunta.NB_RESPUESTA.ToCharArray()[1].ToString());
                    }
                    if (pVariable3 != "")
                    {
                        pReport.Dictionary.Variables[pVariable3].Value = AsignarValorOrt1(pPregunta.NB_RESPUESTA.ToCharArray()[2].ToString());
                    }
                }
                else 
                {

                //    if (pPregunta.CL_PREGUNTA.Contains("ORTOGRAFIA-1-A00"))
                //    {
                //        pReport.Dictionary.Variables[pVariable].Value = formatStringAsParagraphWord(pPregunta.NB_PREGUNTA).Replace("V", "_").Replace("B", "_").Replace("v","_").Replace("b","_");
                //    }
                //    else 
                //    {
                //        switch (pPregunta.NB_PREGUNTA)
                //        {
                //            case "SINALOENSE": 
                //                    string vNewPregunta = pPregunta.NB_PREGUNTA.Substring(1, pPregunta.NB_PREGUNTA.Length-1);
                //                    vNewPregunta = vNewPregunta.Replace("C", "_").Replace("S", "_");
                //                    pReport.Dictionary.Variables[pVariable].Value = "S"+vNewPregunta.ToLower();
                //                break;
                //            case "COMENCEMOS":
                //            case "DISCRECION":
                //                pReport.Dictionary.Variables[pVariable].Value = formatStringAsParagraphWord(pPregunta.NB_PREGUNTA).Replace("c", "_");
                //                break;
                //            case "OCASION":
                //                pReport.Dictionary.Variables[pVariable].Value = formatStringAsParagraphWord(pPregunta.NB_PREGUNTA).Replace("s", "_");
                //                break;
                //            case "BLANCUZCO":
                //                pReport.Dictionary.Variables[pVariable].Value = formatStringAsParagraphWord(pPregunta.NB_PREGUNTA).Replace("z", "_");
                //                break;
                //            case "ESCASEZ":
                //                pReport.Dictionary.Variables[pVariable].Value = formatStringAsParagraphWord(pPregunta.NB_PREGUNTA).Replace("z", "_").Replace("s", "_");
                //                break;
                //            default:
                //                pReport.Dictionary.Variables[pVariable].Value = formatStringAsParagraphWord(pPregunta.NB_PREGUNTA).Replace("c", "_").Replace("s", "_").Replace("z", "_");
                //                break;
                //        }
                //    }
                }
            }
        }

        public string formatStringAsParagraphWord(string word) 
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(word.ToLower());
        }

        private void AsignarValorOrtografiaII(StiReport pReport, string pVariable,string pVCorrecion, string pRespuestas, string pClPregunta)
        {

            if (pRespuestas != null)
            {
                XElement pRespuesta = XElement.Parse(pRespuestas);
                List<E_ORTOGRAFIA_II> vListaRespuestas = pRespuesta.Elements("ANSWER").Select(el => new E_ORTOGRAFIA_II
                {
                    VALOR = el.Attribute("VALOR").Value,
                    NB_RESPUESTA = el.Attribute("NB_RESPUESTA").Value
                }).ToList();

                if (vListaRespuestas.Count > 0)
                {
                    if (vListaRespuestas.FirstOrDefault().VALOR == "C")
                    {
                        pReport.Dictionary.Variables[pVariable].Value = "1";
                    }
                    else if (vListaRespuestas.FirstOrDefault().VALOR == "I")
                    {
                        pReport.Dictionary.Variables[pVariable].Value = "0";
                        pReport.Dictionary.Variables[pVCorrecion].Value = vListaRespuestas.FirstOrDefault().NB_RESPUESTA;
                    }
                    else
                    {
                        pReport.Dictionary.Variables[pVariable].Value = "";
                    }

                  
                }
            }
            
        }

        private void AsignarInteresesPersonale(StiReport pReport, string pVariable, string pRespuesta)
        {
            if (!string.IsNullOrEmpty(pRespuesta))
            {
                pReport.Dictionary.Variables[pVariable].Value = pRespuesta;
            }
        }

        public void asignarValoresManual(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas, string pNbCandidato, StiReport pReport)
        {

            if (respuestas.Count > 0)
            {
                foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
                {
                    switch (resp.CL_PREGUNTA)
                    {
                        case "APTITUD2-RES-0001": SeleccionarVariableRespuesta("Resp1A", "Resp1B", "Resp1C", "Resp1D", "Resp1E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0002": SeleccionarVariableRespuesta("Resp2A", "Resp2B", "Resp2C", "Resp2D", "Resp2E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0003": SeleccionarVariableRespuesta("Resp3A", "Resp3B", "Resp3C", "Resp3D", "Resp3E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0004": SeleccionarVariableRespuesta("Resp4A", "Resp4B", "Resp4C", "Resp4D", "Resp4E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0005": SeleccionarVariableRespuesta("Resp5A", "Resp5B", "Resp5C", "Resp5D", "Resp5E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0006": SeleccionarVariableRespuesta("Resp6A", "Resp6B", "Resp6C", "Resp6D", "Resp6E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0007": SeleccionarVariableRespuesta("Resp7A", "Resp7B", "Resp7C", "Resp7D", "Resp7E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0008": SeleccionarVariableRespuesta("Resp8A", "Resp8B", "Resp8C", "Resp8D", "Resp8E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0009": SeleccionarVariableRespuesta("Resp9A", "Resp9B", "Resp9C", "Resp9D", "Resp9E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0010": SeleccionarVariableRespuesta("Resp10A", "Resp10B", "Resp10C", "Resp10D", "Resp10E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0011": SeleccionarVariableRespuesta("Resp11A", "Resp11B", "Resp11C", "Resp11D", "Resp11E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0012": SeleccionarVariableRespuesta("Resp12A", "Resp12B", "Resp12C", "Resp12D", "Resp12E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0013": SeleccionarVariableRespuesta("Resp13A", "Resp13B", "Resp13C", "Resp13D", "Resp13E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0014": SeleccionarVariableRespuesta("Resp14A", "Resp14B", "Resp14C", "Resp14D", "Resp14E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0015": SeleccionarVariableRespuesta("Resp15A", "Resp15B", "Resp15C", "Resp15D", "Resp15E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0016": SeleccionarVariableRespuesta("Resp16A", "Resp16B", "Resp16C", "Resp16D", "Resp16E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0017": SeleccionarVariableRespuesta("Resp17A", "Resp17B", "Resp17C", "Resp17D", "Resp17E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0018": SeleccionarVariableRespuesta("Resp18A", "Resp18B", "Resp18C", "Resp18D", "Resp18E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0019": SeleccionarVariableRespuesta("Resp19A", "Resp19B", "Resp19C", "Resp19D", "Resp19E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0020": SeleccionarVariableRespuesta("Resp20A", "Resp20B", "Resp20C", "Resp20D", "Resp20E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0021": SeleccionarVariableRespuesta("Resp21A", "Resp21B", "Resp21C", "Resp21D", "Resp21E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0022": SeleccionarVariableRespuesta("Resp22A", "Resp22B", "Resp22C", "Resp22D", "Resp22E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0023": SeleccionarVariableRespuesta("Resp23A", "Resp23B", "Resp23C", "Resp23D", "Resp23E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0024": SeleccionarVariableRespuesta("Resp24A", "Resp24B", "Resp24C", "Resp24D", "Resp24E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0025": SeleccionarVariableRespuesta("Resp25A", "Resp25B", "Resp25C", "Resp25D", "Resp25E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0026": SeleccionarVariableRespuesta("Resp26A", "Resp26B", "Resp26C", "Resp26D", "Resp26E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0027": SeleccionarVariableRespuesta("Resp27A", "Resp27B", "Resp27C", "Resp27D", "Resp27E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0028": SeleccionarVariableRespuesta("Resp28A", "Resp28B", "Resp28C", "Resp28D", "Resp28E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0029": SeleccionarVariableRespuesta("Resp29A", "Resp29B", "Resp29C", "Resp29D", "Resp29E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0030": SeleccionarVariableRespuesta("Resp30A", "Resp30B", "Resp30C", "Resp30D", "Resp30E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0031": SeleccionarVariableRespuesta("Resp31A", "Resp31B", "Resp31C", "Resp31D", "Resp31E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0032": SeleccionarVariableRespuesta("Resp32A", "Resp32B", "Resp32C", "Resp32D", "Resp32E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0033": SeleccionarVariableRespuesta("Resp33A", "Resp33B", "Resp33C", "Resp33D", "Resp33E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0034": SeleccionarVariableRespuesta("Resp34A", "Resp34B", "Resp34C", "Resp34D", "Resp34E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0035": SeleccionarVariableRespuesta("Resp35A", "Resp35B", "Resp35C", "Resp35D", "Resp35E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0036": SeleccionarVariableRespuesta("Resp36A", "Resp36B", "Resp36C", "Resp36D", "Resp36E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0037": SeleccionarVariableRespuesta("Resp37A", "Resp37B", "Resp37C", "Resp37D", "Resp37E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0038": SeleccionarVariableRespuesta("Resp38A", "Resp38B", "Resp38C", "Resp38D", "Resp38E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0039": SeleccionarVariableRespuesta("Resp39A", "Resp39B", "Resp39C", "Resp39D", "Resp39E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0040": SeleccionarVariableRespuesta("Resp40A", "Resp40B", "Resp40C", "Resp40D", "Resp40E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0041": SeleccionarVariableRespuesta("Resp41A", "Resp41B", "Resp41C", "Resp41D", "Resp41E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0042": SeleccionarVariableRespuesta("Resp42A", "Resp42B", "Resp42C", "Resp42D", "Resp42E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0043": SeleccionarVariableRespuesta("Resp43A", "Resp43B", "Resp43C", "Resp43D", "Resp43E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0044": SeleccionarVariableRespuesta("Resp44A", "Resp44B", "Resp44C", "Resp44D", "Resp44E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0045": SeleccionarVariableRespuesta("Resp45A", "Resp45B", "Resp45C", "Resp45D", "Resp45E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0046": SeleccionarVariableRespuesta("Resp46A", "Resp46B", "Resp46C", "Resp46D", "Resp46E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0047": SeleccionarVariableRespuesta("Resp47A", "Resp47B", "Resp47C", "Resp47D", "Resp47E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0048": SeleccionarVariableRespuesta("Resp48A", "Resp48B", "Resp48C", "Resp48D", "Resp48E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0049": SeleccionarVariableRespuesta("Resp49A", "Resp49B", "Resp49C", "Resp49D", "Resp49E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0050": SeleccionarVariableRespuesta("Resp50A", "Resp50B", "Resp50C", "Resp50D", "Resp50E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0051": SeleccionarVariableRespuesta("Resp51A", "Resp51B", "Resp51C", "Resp51D", "Resp51E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0052": SeleccionarVariableRespuesta("Resp52A", "Resp52B", "Resp52C", "Resp52D", "Resp52E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0053": SeleccionarVariableRespuesta("Resp53A", "Resp53B", "Resp53C", "Resp53D", "Resp53E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0054": SeleccionarVariableRespuesta("Resp54A", "Resp54B", "Resp54C", "Resp54D", "Resp54E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0055": SeleccionarVariableRespuesta("Resp55A", "Resp55B", "Resp55C", "Resp55D", "Resp55E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0056": SeleccionarVariableRespuesta("Resp56A", "Resp56B", "Resp56C", "Resp56D", "Resp56E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0057": SeleccionarVariableRespuesta("Resp57A", "Resp57B", "Resp57C", "Resp57D", "Resp57E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0058": SeleccionarVariableRespuesta("Resp58A", "Resp58B", "Resp58C", "Resp58D", "Resp58E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0059": SeleccionarVariableRespuesta("Resp59A", "Resp59B", "Resp59C", "Resp59D", "Resp59E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0060": SeleccionarVariableRespuesta("Resp60A", "Resp60B", "Resp60C", "Resp60D", "Resp60E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0061": SeleccionarVariableRespuesta("Resp61A", "Resp61B", "Resp61C", "Resp61D", "Resp61E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0062": SeleccionarVariableRespuesta("Resp62A", "Resp62B", "Resp62C", "Resp62D", "Resp62E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0063": SeleccionarVariableRespuesta("Resp63A", "Resp63B", "Resp63C", "Resp63D", "Resp63E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0064": SeleccionarVariableRespuesta("Resp64A", "Resp64B", "Resp64C", "Resp64D", "Resp64E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0065": SeleccionarVariableRespuesta("Resp65A", "Resp65B", "Resp65C", "Resp65D", "Resp65E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0066": SeleccionarVariableRespuesta("Resp66A", "Resp66B", "Resp66C", "Resp66D", "Resp66E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0067": SeleccionarVariableRespuesta("Resp67A", "Resp67B", "Resp67C", "Resp67D", "Resp67E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0068": SeleccionarVariableRespuesta("Resp68A", "Resp68B", "Resp68C", "Resp68D", "Resp68E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0069": SeleccionarVariableRespuesta("Resp69A", "Resp69B", "Resp69C", "Resp69D", "Resp69E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0070": SeleccionarVariableRespuesta("Resp70A", "Resp70B", "Resp70C", "Resp70D", "Resp70E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0071": SeleccionarVariableRespuesta("Resp71A", "Resp71B", "Resp71C", "Resp71D", "Resp71E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0072": SeleccionarVariableRespuesta("Resp72A", "Resp72B", "Resp72C", "Resp72D", "Resp72E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0073": SeleccionarVariableRespuesta("Resp73A", "Resp73B", "Resp73C", "Resp73D", "Resp73E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0074": SeleccionarVariableRespuesta("Resp74A", "Resp74B", "Resp74C", "Resp74D", "Resp74E", resp.NB_RESULTADO, pReport); break;
                        case "APTITUD2-RES-0075": SeleccionarVariableRespuesta("Resp75A", "Resp75B", "Resp75C", "Resp75D", "Resp75E", resp.NB_RESULTADO, pReport); break;
                    }
                }

                pReport.Dictionary.Variables["NB_CANDIDATO"].Value = pNbCandidato;

            }
            else
            {
                pReport.Dictionary.Variables["Resp1A"].Value = "0";
                pReport.Dictionary.Variables["Resp1B"].Value = "0";
                pReport.Dictionary.Variables["Resp1C"].Value = "0";
                pReport.Dictionary.Variables["Resp1D"].Value = "0";
                pReport.Dictionary.Variables["Resp1E"].Value = "0";
                pReport.Dictionary.Variables["Resp2A"].Value = "0";
                pReport.Dictionary.Variables["Resp2B"].Value = "0";
                pReport.Dictionary.Variables["Resp2C"].Value = "0";
                pReport.Dictionary.Variables["Resp2D"].Value = "0";
                pReport.Dictionary.Variables["Resp2E"].Value = "0";
                pReport.Dictionary.Variables["Resp3A"].Value = "0";
                pReport.Dictionary.Variables["Resp3B"].Value = "0";
                pReport.Dictionary.Variables["Resp3C"].Value = "0";
                pReport.Dictionary.Variables["Resp3D"].Value = "0";
                pReport.Dictionary.Variables["Resp3E"].Value = "0";
                pReport.Dictionary.Variables["Resp4A"].Value = "0";
                pReport.Dictionary.Variables["Resp4B"].Value = "0";
                pReport.Dictionary.Variables["Resp4C"].Value = "0";
                pReport.Dictionary.Variables["Resp4D"].Value = "0";
                pReport.Dictionary.Variables["Resp4E"].Value = "0";
                pReport.Dictionary.Variables["Resp5A"].Value = "0";
                pReport.Dictionary.Variables["Resp5B"].Value = "0";
                pReport.Dictionary.Variables["Resp5C"].Value = "0";
                pReport.Dictionary.Variables["Resp5D"].Value = "0";
                pReport.Dictionary.Variables["Resp5E"].Value = "0";
                pReport.Dictionary.Variables["Resp6A"].Value = "0";
                pReport.Dictionary.Variables["Resp6B"].Value = "0";
                pReport.Dictionary.Variables["Resp6C"].Value = "0";
                pReport.Dictionary.Variables["Resp6D"].Value = "0";
                pReport.Dictionary.Variables["Resp6E"].Value = "0";
                pReport.Dictionary.Variables["Resp7A"].Value = "0";
                pReport.Dictionary.Variables["Resp7B"].Value = "0";
                pReport.Dictionary.Variables["Resp7C"].Value = "0";
                pReport.Dictionary.Variables["Resp7D"].Value = "0";
                pReport.Dictionary.Variables["Resp7E"].Value = "0";
                pReport.Dictionary.Variables["Resp8A"].Value = "0";
                pReport.Dictionary.Variables["Resp8B"].Value = "0";
                pReport.Dictionary.Variables["Resp8C"].Value = "0";
                pReport.Dictionary.Variables["Resp8D"].Value = "0";
                pReport.Dictionary.Variables["Resp8E"].Value = "0";
                pReport.Dictionary.Variables["Resp9A"].Value = "0";
                pReport.Dictionary.Variables["Resp9B"].Value = "0";
                pReport.Dictionary.Variables["Resp9C"].Value = "0";
                pReport.Dictionary.Variables["Resp9D"].Value = "0";
                pReport.Dictionary.Variables["Resp9E"].Value = "0";
                pReport.Dictionary.Variables["Resp10A"].Value = "0";
                pReport.Dictionary.Variables["Resp10B"].Value = "0";
                pReport.Dictionary.Variables["Resp10C"].Value = "0";
                pReport.Dictionary.Variables["Resp10D"].Value = "0";
                pReport.Dictionary.Variables["Resp10E"].Value = "0";
                pReport.Dictionary.Variables["Resp11A"].Value = "0";
                pReport.Dictionary.Variables["Resp11B"].Value = "0";
                pReport.Dictionary.Variables["Resp11C"].Value = "0";
                pReport.Dictionary.Variables["Resp11D"].Value = "0";
                pReport.Dictionary.Variables["Resp11E"].Value = "0";
                pReport.Dictionary.Variables["Resp12A"].Value = "0";
                pReport.Dictionary.Variables["Resp12B"].Value = "0";
                pReport.Dictionary.Variables["Resp12C"].Value = "0";
                pReport.Dictionary.Variables["Resp12D"].Value = "0";
                pReport.Dictionary.Variables["Resp12E"].Value = "0";
                pReport.Dictionary.Variables["Resp13A"].Value = "0";
                pReport.Dictionary.Variables["Resp13B"].Value = "0";
                pReport.Dictionary.Variables["Resp13C"].Value = "0";
                pReport.Dictionary.Variables["Resp13D"].Value = "0";
                pReport.Dictionary.Variables["Resp13E"].Value = "0";
                pReport.Dictionary.Variables["Resp14A"].Value = "0";
                pReport.Dictionary.Variables["Resp14B"].Value = "0";
                pReport.Dictionary.Variables["Resp14C"].Value = "0";
                pReport.Dictionary.Variables["Resp14D"].Value = "0";
                pReport.Dictionary.Variables["Resp14E"].Value = "0";
                pReport.Dictionary.Variables["Resp15A"].Value = "0";
                pReport.Dictionary.Variables["Resp15B"].Value = "0";
                pReport.Dictionary.Variables["Resp15C"].Value = "0";
                pReport.Dictionary.Variables["Resp15D"].Value = "0";
                pReport.Dictionary.Variables["Resp15E"].Value = "0";
                pReport.Dictionary.Variables["Resp16A"].Value = "0";
                pReport.Dictionary.Variables["Resp16B"].Value = "0";
                pReport.Dictionary.Variables["Resp16C"].Value = "0";
                pReport.Dictionary.Variables["Resp16D"].Value = "0";
                pReport.Dictionary.Variables["Resp16E"].Value = "0";
                pReport.Dictionary.Variables["Resp17A"].Value = "0";
                pReport.Dictionary.Variables["Resp17B"].Value = "0";
                pReport.Dictionary.Variables["Resp17C"].Value = "0";
                pReport.Dictionary.Variables["Resp17D"].Value = "0";
                pReport.Dictionary.Variables["Resp17E"].Value = "0";
                pReport.Dictionary.Variables["Resp18A"].Value = "0";
                pReport.Dictionary.Variables["Resp18B"].Value = "0";
                pReport.Dictionary.Variables["Resp18C"].Value = "0";
                pReport.Dictionary.Variables["Resp18D"].Value = "0";
                pReport.Dictionary.Variables["Resp18E"].Value = "0";
                pReport.Dictionary.Variables["Resp19A"].Value = "0";
                pReport.Dictionary.Variables["Resp19B"].Value = "0";
                pReport.Dictionary.Variables["Resp19C"].Value = "0";
                pReport.Dictionary.Variables["Resp19D"].Value = "0";
                pReport.Dictionary.Variables["Resp19E"].Value = "0";
                pReport.Dictionary.Variables["Resp20A"].Value = "0";
                pReport.Dictionary.Variables["Resp20B"].Value = "0";
                pReport.Dictionary.Variables["Resp20C"].Value = "0";
                pReport.Dictionary.Variables["Resp20D"].Value = "0";
                pReport.Dictionary.Variables["Resp20E"].Value = "0";
                pReport.Dictionary.Variables["Resp21A"].Value = "0";
                pReport.Dictionary.Variables["Resp21B"].Value = "0";
                pReport.Dictionary.Variables["Resp21C"].Value = "0";
                pReport.Dictionary.Variables["Resp21D"].Value = "0";
                pReport.Dictionary.Variables["Resp21E"].Value = "0";
                pReport.Dictionary.Variables["Resp22A"].Value = "0";
                pReport.Dictionary.Variables["Resp22B"].Value = "0";
                pReport.Dictionary.Variables["Resp22C"].Value = "0";
                pReport.Dictionary.Variables["Resp22D"].Value = "0";
                pReport.Dictionary.Variables["Resp22E"].Value = "0";
                pReport.Dictionary.Variables["Resp23A"].Value = "0";
                pReport.Dictionary.Variables["Resp23B"].Value = "0";
                pReport.Dictionary.Variables["Resp23C"].Value = "0";
                pReport.Dictionary.Variables["Resp23D"].Value = "0";
                pReport.Dictionary.Variables["Resp23E"].Value = "0";
                pReport.Dictionary.Variables["Resp24A"].Value = "0";
                pReport.Dictionary.Variables["Resp24B"].Value = "0";
                pReport.Dictionary.Variables["Resp24C"].Value = "0";
                pReport.Dictionary.Variables["Resp24D"].Value = "0";
                pReport.Dictionary.Variables["Resp24E"].Value = "0";
                pReport.Dictionary.Variables["Resp25A"].Value = "0";
                pReport.Dictionary.Variables["Resp25B"].Value = "0";
                pReport.Dictionary.Variables["Resp25C"].Value = "0";
                pReport.Dictionary.Variables["Resp25D"].Value = "0";
                pReport.Dictionary.Variables["Resp25E"].Value = "0";
                pReport.Dictionary.Variables["Resp26A"].Value = "0";
                pReport.Dictionary.Variables["Resp26B"].Value = "0";
                pReport.Dictionary.Variables["Resp26C"].Value = "0";
                pReport.Dictionary.Variables["Resp26D"].Value = "0";
                pReport.Dictionary.Variables["Resp26E"].Value = "0";
                pReport.Dictionary.Variables["Resp27A"].Value = "0";
                pReport.Dictionary.Variables["Resp27B"].Value = "0";
                pReport.Dictionary.Variables["Resp27C"].Value = "0";
                pReport.Dictionary.Variables["Resp27D"].Value = "0";
                pReport.Dictionary.Variables["Resp27E"].Value = "0";
                pReport.Dictionary.Variables["Resp28A"].Value = "0";
                pReport.Dictionary.Variables["Resp28B"].Value = "0";
                pReport.Dictionary.Variables["Resp28C"].Value = "0";
                pReport.Dictionary.Variables["Resp28D"].Value = "0";
                pReport.Dictionary.Variables["Resp28E"].Value = "0";
                pReport.Dictionary.Variables["Resp29A"].Value = "0";
                pReport.Dictionary.Variables["Resp29B"].Value = "0";
                pReport.Dictionary.Variables["Resp29C"].Value = "0";
                pReport.Dictionary.Variables["Resp29D"].Value = "0";
                pReport.Dictionary.Variables["Resp29E"].Value = "0";
                pReport.Dictionary.Variables["Resp30A"].Value = "0";
                pReport.Dictionary.Variables["Resp30B"].Value = "0";
                pReport.Dictionary.Variables["Resp30C"].Value = "0";
                pReport.Dictionary.Variables["Resp30D"].Value = "0";
                pReport.Dictionary.Variables["Resp30E"].Value = "0";
                pReport.Dictionary.Variables["Resp30A"].Value = "0";
                pReport.Dictionary.Variables["Resp31B"].Value = "0";
                pReport.Dictionary.Variables["Resp31C"].Value = "0";
                pReport.Dictionary.Variables["Resp31D"].Value = "0";
                pReport.Dictionary.Variables["Resp31E"].Value = "0";
                pReport.Dictionary.Variables["Resp32A"].Value = "0";
                pReport.Dictionary.Variables["Resp32B"].Value = "0";
                pReport.Dictionary.Variables["Resp32C"].Value = "0";
                pReport.Dictionary.Variables["Resp32D"].Value = "0";
                pReport.Dictionary.Variables["Resp32E"].Value = "0";
                pReport.Dictionary.Variables["Resp33A"].Value = "0";
                pReport.Dictionary.Variables["Resp33B"].Value = "0";
                pReport.Dictionary.Variables["Resp33C"].Value = "0";
                pReport.Dictionary.Variables["Resp33D"].Value = "0";
                pReport.Dictionary.Variables["Resp33E"].Value = "0";
                pReport.Dictionary.Variables["Resp34A"].Value = "0";
                pReport.Dictionary.Variables["Resp34B"].Value = "0";
                pReport.Dictionary.Variables["Resp34C"].Value = "0";
                pReport.Dictionary.Variables["Resp34D"].Value = "0";
                pReport.Dictionary.Variables["Resp34E"].Value = "0";
                pReport.Dictionary.Variables["Resp35A"].Value = "0";
                pReport.Dictionary.Variables["Resp35B"].Value = "0";
                pReport.Dictionary.Variables["Resp35C"].Value = "0";
                pReport.Dictionary.Variables["Resp35D"].Value = "0";
                pReport.Dictionary.Variables["Resp35E"].Value = "0";
                pReport.Dictionary.Variables["Resp36A"].Value = "0";
                pReport.Dictionary.Variables["Resp36B"].Value = "0";
                pReport.Dictionary.Variables["Resp36C"].Value = "0";
                pReport.Dictionary.Variables["Resp36D"].Value = "0";
                pReport.Dictionary.Variables["Resp36E"].Value = "0";
                pReport.Dictionary.Variables["Resp37A"].Value = "0";
                pReport.Dictionary.Variables["Resp37B"].Value = "0";
                pReport.Dictionary.Variables["Resp37C"].Value = "0";
                pReport.Dictionary.Variables["Resp37D"].Value = "0";
                pReport.Dictionary.Variables["Resp37E"].Value = "0";
                pReport.Dictionary.Variables["Resp38A"].Value = "0";
                pReport.Dictionary.Variables["Resp38B"].Value = "0";
                pReport.Dictionary.Variables["Resp38C"].Value = "0";
                pReport.Dictionary.Variables["Resp38D"].Value = "0";
                pReport.Dictionary.Variables["Resp38E"].Value = "0";
                pReport.Dictionary.Variables["Resp39A"].Value = "0";
                pReport.Dictionary.Variables["Resp39B"].Value = "0";
                pReport.Dictionary.Variables["Resp39C"].Value = "0";
                pReport.Dictionary.Variables["Resp39D"].Value = "0";
                pReport.Dictionary.Variables["Resp39E"].Value = "0";
                pReport.Dictionary.Variables["Resp40A"].Value = "0";
                pReport.Dictionary.Variables["Resp40B"].Value = "0";
                pReport.Dictionary.Variables["Resp40C"].Value = "0";
                pReport.Dictionary.Variables["Resp40D"].Value = "0";
                pReport.Dictionary.Variables["Resp40E"].Value = "0";
                pReport.Dictionary.Variables["Resp41A"].Value = "0";
                pReport.Dictionary.Variables["Resp41B"].Value = "0";
                pReport.Dictionary.Variables["Resp41C"].Value = "0";
                pReport.Dictionary.Variables["Resp41D"].Value = "0";
                pReport.Dictionary.Variables["Resp41E"].Value = "0";
                pReport.Dictionary.Variables["Resp42A"].Value = "0";
                pReport.Dictionary.Variables["Resp42B"].Value = "0";
                pReport.Dictionary.Variables["Resp42C"].Value = "0";
                pReport.Dictionary.Variables["Resp42D"].Value = "0";
                pReport.Dictionary.Variables["Resp42E"].Value = "0";
                pReport.Dictionary.Variables["Resp43A"].Value = "0";
                pReport.Dictionary.Variables["Resp43B"].Value = "0";
                pReport.Dictionary.Variables["Resp43C"].Value = "0";
                pReport.Dictionary.Variables["Resp43D"].Value = "0";
                pReport.Dictionary.Variables["Resp43E"].Value = "0";
                pReport.Dictionary.Variables["Resp44A"].Value = "0";
                pReport.Dictionary.Variables["Resp44B"].Value = "0";
                pReport.Dictionary.Variables["Resp44C"].Value = "0";
                pReport.Dictionary.Variables["Resp44D"].Value = "0";
                pReport.Dictionary.Variables["Resp44E"].Value = "0";
                pReport.Dictionary.Variables["Resp45A"].Value = "0";
                pReport.Dictionary.Variables["Resp45B"].Value = "0";
                pReport.Dictionary.Variables["Resp45C"].Value = "0";
                pReport.Dictionary.Variables["Resp45D"].Value = "0";
                pReport.Dictionary.Variables["Resp45E"].Value = "0";
                pReport.Dictionary.Variables["Resp46A"].Value = "0";
                pReport.Dictionary.Variables["Resp46B"].Value = "0";
                pReport.Dictionary.Variables["Resp46C"].Value = "0";
                pReport.Dictionary.Variables["Resp46D"].Value = "0";
                pReport.Dictionary.Variables["Resp46E"].Value = "0";
                pReport.Dictionary.Variables["Resp47A"].Value = "0";
                pReport.Dictionary.Variables["Resp47B"].Value = "0";
                pReport.Dictionary.Variables["Resp47C"].Value = "0";
                pReport.Dictionary.Variables["Resp47D"].Value = "0";
                pReport.Dictionary.Variables["Resp47E"].Value = "0";
                pReport.Dictionary.Variables["Resp48A"].Value = "0";
                pReport.Dictionary.Variables["Resp48B"].Value = "0";
                pReport.Dictionary.Variables["Resp48C"].Value = "0";
                pReport.Dictionary.Variables["Resp48D"].Value = "0";
                pReport.Dictionary.Variables["Resp48E"].Value = "0";
                pReport.Dictionary.Variables["Resp49A"].Value = "0";
                pReport.Dictionary.Variables["Resp49B"].Value = "0";
                pReport.Dictionary.Variables["Resp49C"].Value = "0";
                pReport.Dictionary.Variables["Resp49D"].Value = "0";
                pReport.Dictionary.Variables["Resp49E"].Value = "0";
                pReport.Dictionary.Variables["Resp50A"].Value = "0";
                pReport.Dictionary.Variables["Resp50B"].Value = "0";
                pReport.Dictionary.Variables["Resp50C"].Value = "0";
                pReport.Dictionary.Variables["Resp50D"].Value = "0";
                pReport.Dictionary.Variables["Resp50E"].Value = "0";
                pReport.Dictionary.Variables["Resp51A"].Value = "0";
                pReport.Dictionary.Variables["Resp51B"].Value = "0";
                pReport.Dictionary.Variables["Resp51C"].Value = "0";
                pReport.Dictionary.Variables["Resp51D"].Value = "0";
                pReport.Dictionary.Variables["Resp51E"].Value = "0";
                pReport.Dictionary.Variables["Resp52A"].Value = "0";
                pReport.Dictionary.Variables["Resp52B"].Value = "0";
                pReport.Dictionary.Variables["Resp52C"].Value = "0";
                pReport.Dictionary.Variables["Resp52D"].Value = "0";
                pReport.Dictionary.Variables["Resp52E"].Value = "0";
                pReport.Dictionary.Variables["Resp53A"].Value = "0";
                pReport.Dictionary.Variables["Resp53B"].Value = "0";
                pReport.Dictionary.Variables["Resp53C"].Value = "0";
                pReport.Dictionary.Variables["Resp53D"].Value = "0";
                pReport.Dictionary.Variables["Resp53E"].Value = "0";
                pReport.Dictionary.Variables["Resp54A"].Value = "0";
                pReport.Dictionary.Variables["Resp54B"].Value = "0";
                pReport.Dictionary.Variables["Resp54C"].Value = "0";
                pReport.Dictionary.Variables["Resp54D"].Value = "0";
                pReport.Dictionary.Variables["Resp54E"].Value = "0";
                pReport.Dictionary.Variables["Resp55A"].Value = "0";
                pReport.Dictionary.Variables["Resp55B"].Value = "0";
                pReport.Dictionary.Variables["Resp55C"].Value = "0";
                pReport.Dictionary.Variables["Resp55D"].Value = "0";
                pReport.Dictionary.Variables["Resp55E"].Value = "0";
                pReport.Dictionary.Variables["Resp56A"].Value = "0";
                pReport.Dictionary.Variables["Resp56B"].Value = "0";
                pReport.Dictionary.Variables["Resp56C"].Value = "0";
                pReport.Dictionary.Variables["Resp56D"].Value = "0";
                pReport.Dictionary.Variables["Resp56E"].Value = "0";
                pReport.Dictionary.Variables["Resp57A"].Value = "0";
                pReport.Dictionary.Variables["Resp57B"].Value = "0";
                pReport.Dictionary.Variables["Resp57C"].Value = "0";
                pReport.Dictionary.Variables["Resp57D"].Value = "0";
                pReport.Dictionary.Variables["Resp57E"].Value = "0";
                pReport.Dictionary.Variables["Resp58A"].Value = "0";
                pReport.Dictionary.Variables["Resp58B"].Value = "0";
                pReport.Dictionary.Variables["Resp58C"].Value = "0";
                pReport.Dictionary.Variables["Resp58D"].Value = "0";
                pReport.Dictionary.Variables["Resp58E"].Value = "0";
                pReport.Dictionary.Variables["Resp59A"].Value = "0";
                pReport.Dictionary.Variables["Resp59B"].Value = "0";
                pReport.Dictionary.Variables["Resp59C"].Value = "0";
                pReport.Dictionary.Variables["Resp59D"].Value = "0";
                pReport.Dictionary.Variables["Resp59E"].Value = "0";
                pReport.Dictionary.Variables["Resp60A"].Value = "0";
                pReport.Dictionary.Variables["Resp60B"].Value = "0";
                pReport.Dictionary.Variables["Resp60C"].Value = "0";
                pReport.Dictionary.Variables["Resp60D"].Value = "0";
                pReport.Dictionary.Variables["Resp60E"].Value = "0";
                pReport.Dictionary.Variables["Resp61A"].Value = "0";
                pReport.Dictionary.Variables["Resp61B"].Value = "0";
                pReport.Dictionary.Variables["Resp61C"].Value = "0";
                pReport.Dictionary.Variables["Resp61D"].Value = "0";
                pReport.Dictionary.Variables["Resp61E"].Value = "0";
                pReport.Dictionary.Variables["Resp62A"].Value = "0";
                pReport.Dictionary.Variables["Resp62B"].Value = "0";
                pReport.Dictionary.Variables["Resp62C"].Value = "0";
                pReport.Dictionary.Variables["Resp62D"].Value = "0";
                pReport.Dictionary.Variables["Resp62E"].Value = "0";
                pReport.Dictionary.Variables["Resp63A"].Value = "0";
                pReport.Dictionary.Variables["Resp63B"].Value = "0";
                pReport.Dictionary.Variables["Resp63C"].Value = "0";
                pReport.Dictionary.Variables["Resp63D"].Value = "0";
                pReport.Dictionary.Variables["Resp63E"].Value = "0";
                pReport.Dictionary.Variables["Resp64A"].Value = "0";
                pReport.Dictionary.Variables["Resp64B"].Value = "0";
                pReport.Dictionary.Variables["Resp64C"].Value = "0";
                pReport.Dictionary.Variables["Resp64D"].Value = "0";
                pReport.Dictionary.Variables["Resp64E"].Value = "0";
                pReport.Dictionary.Variables["Resp65A"].Value = "0";
                pReport.Dictionary.Variables["Resp65B"].Value = "0";
                pReport.Dictionary.Variables["Resp65C"].Value = "0";
                pReport.Dictionary.Variables["Resp65D"].Value = "0";
                pReport.Dictionary.Variables["Resp65E"].Value = "0";
                pReport.Dictionary.Variables["Resp66A"].Value = "0";
                pReport.Dictionary.Variables["Resp66B"].Value = "0";
                pReport.Dictionary.Variables["Resp66C"].Value = "0";
                pReport.Dictionary.Variables["Resp66D"].Value = "0";
                pReport.Dictionary.Variables["Resp66E"].Value = "0";
                pReport.Dictionary.Variables["Resp67A"].Value = "0";
                pReport.Dictionary.Variables["Resp67B"].Value = "0";
                pReport.Dictionary.Variables["Resp67C"].Value = "0";
                pReport.Dictionary.Variables["Resp67D"].Value = "0";
                pReport.Dictionary.Variables["Resp67E"].Value = "0";
                pReport.Dictionary.Variables["Resp68A"].Value = "0";
                pReport.Dictionary.Variables["Resp68B"].Value = "0";
                pReport.Dictionary.Variables["Resp68C"].Value = "0";
                pReport.Dictionary.Variables["Resp68D"].Value = "0";
                pReport.Dictionary.Variables["Resp68E"].Value = "0";
                pReport.Dictionary.Variables["Resp69A"].Value = "0";
                pReport.Dictionary.Variables["Resp69B"].Value = "0";
                pReport.Dictionary.Variables["Resp69C"].Value = "0";
                pReport.Dictionary.Variables["Resp69D"].Value = "0";
                pReport.Dictionary.Variables["Resp69E"].Value = "0";
                pReport.Dictionary.Variables["Resp70A"].Value = "0";
                pReport.Dictionary.Variables["Resp70B"].Value = "0";
                pReport.Dictionary.Variables["Resp70C"].Value = "0";
                pReport.Dictionary.Variables["Resp70D"].Value = "0";
                pReport.Dictionary.Variables["Resp70E"].Value = "0";
                pReport.Dictionary.Variables["Resp71A"].Value = "0";
                pReport.Dictionary.Variables["Resp71B"].Value = "0";
                pReport.Dictionary.Variables["Resp71C"].Value = "0";
                pReport.Dictionary.Variables["Resp71D"].Value = "0";
                pReport.Dictionary.Variables["Resp71E"].Value = "0";
                pReport.Dictionary.Variables["Resp72A"].Value = "0";
                pReport.Dictionary.Variables["Resp72B"].Value = "0";
                pReport.Dictionary.Variables["Resp72C"].Value = "0";
                pReport.Dictionary.Variables["Resp72D"].Value = "0";
                pReport.Dictionary.Variables["Resp72E"].Value = "0";
                pReport.Dictionary.Variables["Resp73A"].Value = "0";
                pReport.Dictionary.Variables["Resp73B"].Value = "0";
                pReport.Dictionary.Variables["Resp73C"].Value = "0";
                pReport.Dictionary.Variables["Resp73D"].Value = "0";
                pReport.Dictionary.Variables["Resp73E"].Value = "0";
                pReport.Dictionary.Variables["Resp74A"].Value = "0";
                pReport.Dictionary.Variables["Resp74B"].Value = "0";
                pReport.Dictionary.Variables["Resp74C"].Value = "0";
                pReport.Dictionary.Variables["Resp74D"].Value = "0";
                pReport.Dictionary.Variables["Resp74E"].Value = "0";
                pReport.Dictionary.Variables["Resp75A"].Value = "0";
                pReport.Dictionary.Variables["Resp75B"].Value = "0";
                pReport.Dictionary.Variables["Resp75C"].Value = "0";
                pReport.Dictionary.Variables["Resp75D"].Value = "0";
                pReport.Dictionary.Variables["Resp75E"].Value = "0";

                pReport.Dictionary.Variables["NB_CANDIDATO"].Value = pNbCandidato;
            }

            pReport.Compile();
            swvReporte.Report = pReport;

        }

        public void asignarValores(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas, string pNbCandidato, StiReport pReport)
        {
            if (respuestas.Count > 0)
            {
                foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
                {
                    switch (resp.CL_PREGUNTA)
                    {
                        case "APTITUD2-A-0001": SeleccionarVariableRespuesta("Resp1A", "Resp1B", "Resp1C", "Resp1D", "Resp1E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0002": SeleccionarVariableRespuesta("Resp2A", "Resp2B", "Resp2C", "Resp2D", "Resp2E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0003": SeleccionarVariableRespuesta("Resp3A", "Resp3B", "Resp3C", "Resp3D", "Resp3E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0004": SeleccionarVariableRespuesta("Resp4A", "Resp4B", "Resp4C", "Resp4D", "Resp4E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0005": SeleccionarVariableRespuesta("Resp5A", "Resp5B", "Resp5C", "Resp5D", "Resp5E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0006": SeleccionarVariableRespuesta("Resp6A", "Resp6B", "Resp6C", "Resp6D", "Resp6E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0007": SeleccionarVariableRespuesta("Resp7A", "Resp7B", "Resp7C", "Resp7D", "Resp7E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0008": SeleccionarVariableRespuesta("Resp8A", "Resp8B", "Resp8C", "Resp8D", "Resp8E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0009": SeleccionarVariableRespuesta("Resp9A", "Resp9B", "Resp9C", "Resp9D", "Resp9E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0010": SeleccionarVariableRespuesta("Resp10A", "Resp10B", "Resp10C", "Resp10D", "Resp10E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0011": SeleccionarVariableRespuesta("Resp11A", "Resp11B", "Resp11C", "Resp11D", "Resp11E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0012": SeleccionarVariableRespuesta("Resp12A", "Resp12B", "Resp12C", "Resp12D", "Resp12E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0013": SeleccionarVariableRespuesta("Resp13A", "Resp13B", "Resp13C", "Resp13D", "Resp13E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0014": SeleccionarVariableRespuesta("Resp14A", "Resp14B", "Resp14C", "Resp14D", "Resp14E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0015": SeleccionarVariableRespuesta("Resp15A", "Resp15B", "Resp15C", "Resp15D", "Resp15E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0016": SeleccionarVariableRespuesta("Resp16A", "Resp16B", "Resp16C", "Resp16D", "Resp16E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0017": SeleccionarVariableRespuesta("Resp17A", "Resp17B", "Resp17C", "Resp17D", "Resp17E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0018": SeleccionarVariableRespuesta("Resp18A", "Resp18B", "Resp18C", "Resp18D", "Resp18E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0019": SeleccionarVariableRespuesta("Resp19A", "Resp19B", "Resp19C", "Resp19D", "Resp19E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0020": SeleccionarVariableRespuesta("Resp20A", "Resp20B", "Resp20C", "Resp20D", "Resp20E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0021": SeleccionarVariableRespuesta("Resp21A", "Resp21B", "Resp21C", "Resp21D", "Resp21E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0022": SeleccionarVariableRespuesta("Resp22A", "Resp22B", "Resp22C", "Resp22D", "Resp22E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0023": SeleccionarVariableRespuesta("Resp23A", "Resp23B", "Resp23C", "Resp23D", "Resp23E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0024": SeleccionarVariableRespuesta("Resp24A", "Resp24B", "Resp24C", "Resp24D", "Resp24E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0025": SeleccionarVariableRespuesta("Resp25A", "Resp25B", "Resp25C", "Resp25D", "Resp25E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0026": SeleccionarVariableRespuesta("Resp26A", "Resp26B", "Resp26C", "Resp26D", "Resp26E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0027": SeleccionarVariableRespuesta("Resp27A", "Resp27B", "Resp27C", "Resp27D", "Resp27E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0028": SeleccionarVariableRespuesta("Resp28A", "Resp28B", "Resp28C", "Resp28D", "Resp28E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0029": SeleccionarVariableRespuesta("Resp29A", "Resp29B", "Resp29C", "Resp29D", "Resp29E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0030": SeleccionarVariableRespuesta("Resp30A", "Resp30B", "Resp30C", "Resp30D", "Resp30E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0031": SeleccionarVariableRespuesta("Resp31A", "Resp31B", "Resp31C", "Resp31D", "Resp31E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0032": SeleccionarVariableRespuesta("Resp32A", "Resp32B", "Resp32C", "Resp32D", "Resp32E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0033": SeleccionarVariableRespuesta("Resp33A", "Resp33B", "Resp33C", "Resp33D", "Resp33E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0034": SeleccionarVariableRespuesta("Resp34A", "Resp34B", "Resp34C", "Resp34D", "Resp34E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0035": SeleccionarVariableRespuesta("Resp35A", "Resp35B", "Resp35C", "Resp35D", "Resp35E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0036": SeleccionarVariableRespuesta("Resp36A", "Resp36B", "Resp36C", "Resp36D", "Resp36E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0037": SeleccionarVariableRespuesta("Resp37A", "Resp37B", "Resp37C", "Resp37D", "Resp37E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0038": SeleccionarVariableRespuesta("Resp38A", "Resp38B", "Resp38C", "Resp38D", "Resp38E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0039": SeleccionarVariableRespuesta("Resp39A", "Resp39B", "Resp39C", "Resp39D", "Resp39E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0040": SeleccionarVariableRespuesta("Resp40A", "Resp40B", "Resp40C", "Resp40D", "Resp40E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0041": SeleccionarVariableRespuesta("Resp41A", "Resp41B", "Resp41C", "Resp41D", "Resp41E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0042": SeleccionarVariableRespuesta("Resp42A", "Resp42B", "Resp42C", "Resp42D", "Resp42E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0043": SeleccionarVariableRespuesta("Resp43A", "Resp43B", "Resp43C", "Resp43D", "Resp43E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0044": SeleccionarVariableRespuesta("Resp44A", "Resp44B", "Resp44C", "Resp44D", "Resp44E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0045": SeleccionarVariableRespuesta("Resp45A", "Resp45B", "Resp45C", "Resp45D", "Resp45E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0046": SeleccionarVariableRespuesta("Resp46A", "Resp46B", "Resp46C", "Resp46D", "Resp46E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0047": SeleccionarVariableRespuesta("Resp47A", "Resp47B", "Resp47C", "Resp47D", "Resp47E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0048": SeleccionarVariableRespuesta("Resp48A", "Resp48B", "Resp48C", "Resp48D", "Resp48E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0049": SeleccionarVariableRespuesta("Resp49A", "Resp49B", "Resp49C", "Resp49D", "Resp49E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0050": SeleccionarVariableRespuesta("Resp50A", "Resp50B", "Resp50C", "Resp50D", "Resp50E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0051": SeleccionarVariableRespuesta("Resp51A", "Resp51B", "Resp51C", "Resp51D", "Resp51E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0052": SeleccionarVariableRespuesta("Resp52A", "Resp52B", "Resp52C", "Resp52D", "Resp52E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0053": SeleccionarVariableRespuesta("Resp53A", "Resp53B", "Resp53C", "Resp53D", "Resp53E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0054": SeleccionarVariableRespuesta("Resp54A", "Resp54B", "Resp54C", "Resp54D", "Resp54E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0055": SeleccionarVariableRespuesta("Resp55A", "Resp55B", "Resp55C", "Resp55D", "Resp55E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0056": SeleccionarVariableRespuesta("Resp56A", "Resp56B", "Resp56C", "Resp56D", "Resp56E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0057": SeleccionarVariableRespuesta("Resp57A", "Resp57B", "Resp57C", "Resp57D", "Resp57E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0058": SeleccionarVariableRespuesta("Resp58A", "Resp58B", "Resp58C", "Resp58D", "Resp58E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0059": SeleccionarVariableRespuesta("Resp59A", "Resp59B", "Resp59C", "Resp59D", "Resp59E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0060": SeleccionarVariableRespuesta("Resp60A", "Resp60B", "Resp60C", "Resp60D", "Resp60E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0061": SeleccionarVariableRespuesta("Resp61A", "Resp61B", "Resp61C", "Resp61D", "Resp61E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0062": SeleccionarVariableRespuesta("Resp62A", "Resp62B", "Resp62C", "Resp62D", "Resp62E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0063": SeleccionarVariableRespuesta("Resp63A", "Resp63B", "Resp63C", "Resp63D", "Resp63E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0064": SeleccionarVariableRespuesta("Resp64A", "Resp64B", "Resp64C", "Resp64D", "Resp64E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0065": SeleccionarVariableRespuesta("Resp65A", "Resp65B", "Resp65C", "Resp65D", "Resp65E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0066": SeleccionarVariableRespuesta("Resp66A", "Resp66B", "Resp66C", "Resp66D", "Resp66E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0067": SeleccionarVariableRespuesta("Resp67A", "Resp67B", "Resp67C", "Resp67D", "Resp67E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0068": SeleccionarVariableRespuesta("Resp68A", "Resp68B", "Resp68C", "Resp68D", "Resp68E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0069": SeleccionarVariableRespuesta("Resp69A", "Resp69B", "Resp69C", "Resp69D", "Resp69E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0070": SeleccionarVariableRespuesta("Resp70A", "Resp70B", "Resp70C", "Resp70D", "Resp70E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0071": SeleccionarVariableRespuesta("Resp71A", "Resp71B", "Resp71C", "Resp71D", "Resp71E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0072": SeleccionarVariableRespuesta("Resp72A", "Resp72B", "Resp72C", "Resp72D", "Resp72E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0073": SeleccionarVariableRespuesta("Resp73A", "Resp73B", "Resp73C", "Resp73D", "Resp73E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0074": SeleccionarVariableRespuesta("Resp74A", "Resp74B", "Resp74C", "Resp74D", "Resp74E", resp.NB_RESPUESTA, pReport); break;
                        case "APTITUD2-A-0075": SeleccionarVariableRespuesta("Resp75A", "Resp75B", "Resp75C", "Resp75D", "Resp75E", resp.NB_RESPUESTA, pReport); break;
                    }
                }

                pReport.Dictionary.Variables["NB_CANDIDATO"].Value = pNbCandidato;

            }
            else
            {
                pReport.Dictionary.Variables["Resp1A"].Value = "0";
                pReport.Dictionary.Variables["Resp1B"].Value = "0";
                pReport.Dictionary.Variables["Resp1C"].Value = "0";
                pReport.Dictionary.Variables["Resp1D"].Value = "0";
                pReport.Dictionary.Variables["Resp1E"].Value = "0";
                pReport.Dictionary.Variables["Resp2A"].Value = "0";
                pReport.Dictionary.Variables["Resp2B"].Value = "0";
                pReport.Dictionary.Variables["Resp2C"].Value = "0";
                pReport.Dictionary.Variables["Resp2D"].Value = "0";
                pReport.Dictionary.Variables["Resp2E"].Value = "0";
                pReport.Dictionary.Variables["Resp3A"].Value = "0";
                pReport.Dictionary.Variables["Resp3B"].Value = "0";
                pReport.Dictionary.Variables["Resp3C"].Value = "0";
                pReport.Dictionary.Variables["Resp3D"].Value = "0";
                pReport.Dictionary.Variables["Resp3E"].Value = "0";
                pReport.Dictionary.Variables["Resp4A"].Value = "0";
                pReport.Dictionary.Variables["Resp4B"].Value = "0";
                pReport.Dictionary.Variables["Resp4C"].Value = "0";
                pReport.Dictionary.Variables["Resp4D"].Value = "0";
                pReport.Dictionary.Variables["Resp4E"].Value = "0";
                pReport.Dictionary.Variables["Resp5A"].Value = "0";
                pReport.Dictionary.Variables["Resp5B"].Value = "0";
                pReport.Dictionary.Variables["Resp5C"].Value = "0";
                pReport.Dictionary.Variables["Resp5D"].Value = "0";
                pReport.Dictionary.Variables["Resp5E"].Value = "0";
                pReport.Dictionary.Variables["Resp6A"].Value = "0";
                pReport.Dictionary.Variables["Resp6B"].Value = "0";
                pReport.Dictionary.Variables["Resp6C"].Value = "0";
                pReport.Dictionary.Variables["Resp6D"].Value = "0";
                pReport.Dictionary.Variables["Resp6E"].Value = "0";
                pReport.Dictionary.Variables["Resp7A"].Value = "0";
                pReport.Dictionary.Variables["Resp7B"].Value = "0";
                pReport.Dictionary.Variables["Resp7C"].Value = "0";
                pReport.Dictionary.Variables["Resp7D"].Value = "0";
                pReport.Dictionary.Variables["Resp7E"].Value = "0";
                pReport.Dictionary.Variables["Resp8A"].Value = "0";
                pReport.Dictionary.Variables["Resp8B"].Value = "0";
                pReport.Dictionary.Variables["Resp8C"].Value = "0";
                pReport.Dictionary.Variables["Resp8D"].Value = "0";
                pReport.Dictionary.Variables["Resp8E"].Value = "0";
                pReport.Dictionary.Variables["Resp9A"].Value = "0";
                pReport.Dictionary.Variables["Resp9B"].Value = "0";
                pReport.Dictionary.Variables["Resp9C"].Value = "0";
                pReport.Dictionary.Variables["Resp9D"].Value = "0";
                pReport.Dictionary.Variables["Resp9E"].Value = "0";
                pReport.Dictionary.Variables["Resp10A"].Value = "0";
                pReport.Dictionary.Variables["Resp10B"].Value = "0";
                pReport.Dictionary.Variables["Resp10C"].Value = "0";
                pReport.Dictionary.Variables["Resp10D"].Value = "0";
                pReport.Dictionary.Variables["Resp10E"].Value = "0";
                pReport.Dictionary.Variables["Resp11A"].Value = "0";
                pReport.Dictionary.Variables["Resp11B"].Value = "0";
                pReport.Dictionary.Variables["Resp11C"].Value = "0";
                pReport.Dictionary.Variables["Resp11D"].Value = "0";
                pReport.Dictionary.Variables["Resp11E"].Value = "0";
                pReport.Dictionary.Variables["Resp12A"].Value = "0";
                pReport.Dictionary.Variables["Resp12B"].Value = "0";
                pReport.Dictionary.Variables["Resp12C"].Value = "0";
                pReport.Dictionary.Variables["Resp12D"].Value = "0";
                pReport.Dictionary.Variables["Resp12E"].Value = "0";
                pReport.Dictionary.Variables["Resp13A"].Value = "0";
                pReport.Dictionary.Variables["Resp13B"].Value = "0";
                pReport.Dictionary.Variables["Resp13C"].Value = "0";
                pReport.Dictionary.Variables["Resp13D"].Value = "0";
                pReport.Dictionary.Variables["Resp13E"].Value = "0";
                pReport.Dictionary.Variables["Resp14A"].Value = "0";
                pReport.Dictionary.Variables["Resp14B"].Value = "0";
                pReport.Dictionary.Variables["Resp14C"].Value = "0";
                pReport.Dictionary.Variables["Resp14D"].Value = "0";
                pReport.Dictionary.Variables["Resp14E"].Value = "0";
                pReport.Dictionary.Variables["Resp15A"].Value = "0";
                pReport.Dictionary.Variables["Resp15B"].Value = "0";
                pReport.Dictionary.Variables["Resp15C"].Value = "0";
                pReport.Dictionary.Variables["Resp15D"].Value = "0";
                pReport.Dictionary.Variables["Resp15E"].Value = "0";
                pReport.Dictionary.Variables["Resp16A"].Value = "0";
                pReport.Dictionary.Variables["Resp16B"].Value = "0";
                pReport.Dictionary.Variables["Resp16C"].Value = "0";
                pReport.Dictionary.Variables["Resp16D"].Value = "0";
                pReport.Dictionary.Variables["Resp16E"].Value = "0";
                pReport.Dictionary.Variables["Resp17A"].Value = "0";
                pReport.Dictionary.Variables["Resp17B"].Value = "0";
                pReport.Dictionary.Variables["Resp17C"].Value = "0";
                pReport.Dictionary.Variables["Resp17D"].Value = "0";
                pReport.Dictionary.Variables["Resp17E"].Value = "0";
                pReport.Dictionary.Variables["Resp18A"].Value = "0";
                pReport.Dictionary.Variables["Resp18B"].Value = "0";
                pReport.Dictionary.Variables["Resp18C"].Value = "0";
                pReport.Dictionary.Variables["Resp18D"].Value = "0";
                pReport.Dictionary.Variables["Resp18E"].Value = "0";
                pReport.Dictionary.Variables["Resp19A"].Value = "0";
                pReport.Dictionary.Variables["Resp19B"].Value = "0";
                pReport.Dictionary.Variables["Resp19C"].Value = "0";
                pReport.Dictionary.Variables["Resp19D"].Value = "0";
                pReport.Dictionary.Variables["Resp19E"].Value = "0";
                pReport.Dictionary.Variables["Resp20A"].Value = "0";
                pReport.Dictionary.Variables["Resp20B"].Value = "0";
                pReport.Dictionary.Variables["Resp20C"].Value = "0";
                pReport.Dictionary.Variables["Resp20D"].Value = "0";
                pReport.Dictionary.Variables["Resp20E"].Value = "0";
                pReport.Dictionary.Variables["Resp21A"].Value = "0";
                pReport.Dictionary.Variables["Resp21B"].Value = "0";
                pReport.Dictionary.Variables["Resp21C"].Value = "0";
                pReport.Dictionary.Variables["Resp21D"].Value = "0";
                pReport.Dictionary.Variables["Resp21E"].Value = "0";
                pReport.Dictionary.Variables["Resp22A"].Value = "0";
                pReport.Dictionary.Variables["Resp22B"].Value = "0";
                pReport.Dictionary.Variables["Resp22C"].Value = "0";
                pReport.Dictionary.Variables["Resp22D"].Value = "0";
                pReport.Dictionary.Variables["Resp22E"].Value = "0";
                pReport.Dictionary.Variables["Resp23A"].Value = "0";
                pReport.Dictionary.Variables["Resp23B"].Value = "0";
                pReport.Dictionary.Variables["Resp23C"].Value = "0";
                pReport.Dictionary.Variables["Resp23D"].Value = "0";
                pReport.Dictionary.Variables["Resp23E"].Value = "0";
                pReport.Dictionary.Variables["Resp24A"].Value = "0";
                pReport.Dictionary.Variables["Resp24B"].Value = "0";
                pReport.Dictionary.Variables["Resp24C"].Value = "0";
                pReport.Dictionary.Variables["Resp24D"].Value = "0";
                pReport.Dictionary.Variables["Resp24E"].Value = "0";
                pReport.Dictionary.Variables["Resp25A"].Value = "0";
                pReport.Dictionary.Variables["Resp25B"].Value = "0";
                pReport.Dictionary.Variables["Resp25C"].Value = "0";
                pReport.Dictionary.Variables["Resp25D"].Value = "0";
                pReport.Dictionary.Variables["Resp25E"].Value = "0";
                pReport.Dictionary.Variables["Resp26A"].Value = "0";
                pReport.Dictionary.Variables["Resp26B"].Value = "0";
                pReport.Dictionary.Variables["Resp26C"].Value = "0";
                pReport.Dictionary.Variables["Resp26D"].Value = "0";
                pReport.Dictionary.Variables["Resp26E"].Value = "0";
                pReport.Dictionary.Variables["Resp27A"].Value = "0";
                pReport.Dictionary.Variables["Resp27B"].Value = "0";
                pReport.Dictionary.Variables["Resp27C"].Value = "0";
                pReport.Dictionary.Variables["Resp27D"].Value = "0";
                pReport.Dictionary.Variables["Resp27E"].Value = "0";
                pReport.Dictionary.Variables["Resp28A"].Value = "0";
                pReport.Dictionary.Variables["Resp28B"].Value = "0";
                pReport.Dictionary.Variables["Resp28C"].Value = "0";
                pReport.Dictionary.Variables["Resp28D"].Value = "0";
                pReport.Dictionary.Variables["Resp28E"].Value = "0";
                pReport.Dictionary.Variables["Resp29A"].Value = "0";
                pReport.Dictionary.Variables["Resp29B"].Value = "0";
                pReport.Dictionary.Variables["Resp29C"].Value = "0";
                pReport.Dictionary.Variables["Resp29D"].Value = "0";
                pReport.Dictionary.Variables["Resp29E"].Value = "0";
                pReport.Dictionary.Variables["Resp30A"].Value = "0";
                pReport.Dictionary.Variables["Resp30B"].Value = "0";
                pReport.Dictionary.Variables["Resp30C"].Value = "0";
                pReport.Dictionary.Variables["Resp30D"].Value = "0";
                pReport.Dictionary.Variables["Resp30E"].Value = "0";
                pReport.Dictionary.Variables["Resp30A"].Value = "0";
                pReport.Dictionary.Variables["Resp31B"].Value = "0";
                pReport.Dictionary.Variables["Resp31C"].Value = "0";
                pReport.Dictionary.Variables["Resp31D"].Value = "0";
                pReport.Dictionary.Variables["Resp31E"].Value = "0";
                pReport.Dictionary.Variables["Resp32A"].Value = "0";
                pReport.Dictionary.Variables["Resp32B"].Value = "0";
                pReport.Dictionary.Variables["Resp32C"].Value = "0";
                pReport.Dictionary.Variables["Resp32D"].Value = "0";
                pReport.Dictionary.Variables["Resp32E"].Value = "0";
                pReport.Dictionary.Variables["Resp33A"].Value = "0";
                pReport.Dictionary.Variables["Resp33B"].Value = "0";
                pReport.Dictionary.Variables["Resp33C"].Value = "0";
                pReport.Dictionary.Variables["Resp33D"].Value = "0";
                pReport.Dictionary.Variables["Resp33E"].Value = "0";
                pReport.Dictionary.Variables["Resp34A"].Value = "0";
                pReport.Dictionary.Variables["Resp34B"].Value = "0";
                pReport.Dictionary.Variables["Resp34C"].Value = "0";
                pReport.Dictionary.Variables["Resp34D"].Value = "0";
                pReport.Dictionary.Variables["Resp34E"].Value = "0";
                pReport.Dictionary.Variables["Resp35A"].Value = "0";
                pReport.Dictionary.Variables["Resp35B"].Value = "0";
                pReport.Dictionary.Variables["Resp35C"].Value = "0";
                pReport.Dictionary.Variables["Resp35D"].Value = "0";
                pReport.Dictionary.Variables["Resp35E"].Value = "0";
                pReport.Dictionary.Variables["Resp36A"].Value = "0";
                pReport.Dictionary.Variables["Resp36B"].Value = "0";
                pReport.Dictionary.Variables["Resp36C"].Value = "0";
                pReport.Dictionary.Variables["Resp36D"].Value = "0";
                pReport.Dictionary.Variables["Resp36E"].Value = "0";
                pReport.Dictionary.Variables["Resp37A"].Value = "0";
                pReport.Dictionary.Variables["Resp37B"].Value = "0";
                pReport.Dictionary.Variables["Resp37C"].Value = "0";
                pReport.Dictionary.Variables["Resp37D"].Value = "0";
                pReport.Dictionary.Variables["Resp37E"].Value = "0";
                pReport.Dictionary.Variables["Resp38A"].Value = "0";
                pReport.Dictionary.Variables["Resp38B"].Value = "0";
                pReport.Dictionary.Variables["Resp38C"].Value = "0";
                pReport.Dictionary.Variables["Resp38D"].Value = "0";
                pReport.Dictionary.Variables["Resp38E"].Value = "0";
                pReport.Dictionary.Variables["Resp39A"].Value = "0";
                pReport.Dictionary.Variables["Resp39B"].Value = "0";
                pReport.Dictionary.Variables["Resp39C"].Value = "0";
                pReport.Dictionary.Variables["Resp39D"].Value = "0";
                pReport.Dictionary.Variables["Resp39E"].Value = "0";
                pReport.Dictionary.Variables["Resp40A"].Value = "0";
                pReport.Dictionary.Variables["Resp40B"].Value = "0";
                pReport.Dictionary.Variables["Resp40C"].Value = "0";
                pReport.Dictionary.Variables["Resp40D"].Value = "0";
                pReport.Dictionary.Variables["Resp40E"].Value = "0";
                pReport.Dictionary.Variables["Resp41A"].Value = "0";
                pReport.Dictionary.Variables["Resp41B"].Value = "0";
                pReport.Dictionary.Variables["Resp41C"].Value = "0";
                pReport.Dictionary.Variables["Resp41D"].Value = "0";
                pReport.Dictionary.Variables["Resp41E"].Value = "0";
                pReport.Dictionary.Variables["Resp42A"].Value = "0";
                pReport.Dictionary.Variables["Resp42B"].Value = "0";
                pReport.Dictionary.Variables["Resp42C"].Value = "0";
                pReport.Dictionary.Variables["Resp42D"].Value = "0";
                pReport.Dictionary.Variables["Resp42E"].Value = "0";
                pReport.Dictionary.Variables["Resp43A"].Value = "0";
                pReport.Dictionary.Variables["Resp43B"].Value = "0";
                pReport.Dictionary.Variables["Resp43C"].Value = "0";
                pReport.Dictionary.Variables["Resp43D"].Value = "0";
                pReport.Dictionary.Variables["Resp43E"].Value = "0";
                pReport.Dictionary.Variables["Resp44A"].Value = "0";
                pReport.Dictionary.Variables["Resp44B"].Value = "0";
                pReport.Dictionary.Variables["Resp44C"].Value = "0";
                pReport.Dictionary.Variables["Resp44D"].Value = "0";
                pReport.Dictionary.Variables["Resp44E"].Value = "0";
                pReport.Dictionary.Variables["Resp45A"].Value = "0";
                pReport.Dictionary.Variables["Resp45B"].Value = "0";
                pReport.Dictionary.Variables["Resp45C"].Value = "0";
                pReport.Dictionary.Variables["Resp45D"].Value = "0";
                pReport.Dictionary.Variables["Resp45E"].Value = "0";
                pReport.Dictionary.Variables["Resp46A"].Value = "0";
                pReport.Dictionary.Variables["Resp46B"].Value = "0";
                pReport.Dictionary.Variables["Resp46C"].Value = "0";
                pReport.Dictionary.Variables["Resp46D"].Value = "0";
                pReport.Dictionary.Variables["Resp46E"].Value = "0";
                pReport.Dictionary.Variables["Resp47A"].Value = "0";
                pReport.Dictionary.Variables["Resp47B"].Value = "0";
                pReport.Dictionary.Variables["Resp47C"].Value = "0";
                pReport.Dictionary.Variables["Resp47D"].Value = "0";
                pReport.Dictionary.Variables["Resp47E"].Value = "0";
                pReport.Dictionary.Variables["Resp48A"].Value = "0";
                pReport.Dictionary.Variables["Resp48B"].Value = "0";
                pReport.Dictionary.Variables["Resp48C"].Value = "0";
                pReport.Dictionary.Variables["Resp48D"].Value = "0";
                pReport.Dictionary.Variables["Resp48E"].Value = "0";
                pReport.Dictionary.Variables["Resp49A"].Value = "0";
                pReport.Dictionary.Variables["Resp49B"].Value = "0";
                pReport.Dictionary.Variables["Resp49C"].Value = "0";
                pReport.Dictionary.Variables["Resp49D"].Value = "0";
                pReport.Dictionary.Variables["Resp49E"].Value = "0";
                pReport.Dictionary.Variables["Resp50A"].Value = "0";
                pReport.Dictionary.Variables["Resp50B"].Value = "0";
                pReport.Dictionary.Variables["Resp50C"].Value = "0";
                pReport.Dictionary.Variables["Resp50D"].Value = "0";
                pReport.Dictionary.Variables["Resp50E"].Value = "0";
                pReport.Dictionary.Variables["Resp51A"].Value = "0";
                pReport.Dictionary.Variables["Resp51B"].Value = "0";
                pReport.Dictionary.Variables["Resp51C"].Value = "0";
                pReport.Dictionary.Variables["Resp51D"].Value = "0";
                pReport.Dictionary.Variables["Resp51E"].Value = "0";
                pReport.Dictionary.Variables["Resp52A"].Value = "0";
                pReport.Dictionary.Variables["Resp52B"].Value = "0";
                pReport.Dictionary.Variables["Resp52C"].Value = "0";
                pReport.Dictionary.Variables["Resp52D"].Value = "0";
                pReport.Dictionary.Variables["Resp52E"].Value = "0";
                pReport.Dictionary.Variables["Resp53A"].Value = "0";
                pReport.Dictionary.Variables["Resp53B"].Value = "0";
                pReport.Dictionary.Variables["Resp53C"].Value = "0";
                pReport.Dictionary.Variables["Resp53D"].Value = "0";
                pReport.Dictionary.Variables["Resp53E"].Value = "0";
                pReport.Dictionary.Variables["Resp54A"].Value = "0";
                pReport.Dictionary.Variables["Resp54B"].Value = "0";
                pReport.Dictionary.Variables["Resp54C"].Value = "0";
                pReport.Dictionary.Variables["Resp54D"].Value = "0";
                pReport.Dictionary.Variables["Resp54E"].Value = "0";
                pReport.Dictionary.Variables["Resp55A"].Value = "0";
                pReport.Dictionary.Variables["Resp55B"].Value = "0";
                pReport.Dictionary.Variables["Resp55C"].Value = "0";
                pReport.Dictionary.Variables["Resp55D"].Value = "0";
                pReport.Dictionary.Variables["Resp55E"].Value = "0";
                pReport.Dictionary.Variables["Resp56A"].Value = "0";
                pReport.Dictionary.Variables["Resp56B"].Value = "0";
                pReport.Dictionary.Variables["Resp56C"].Value = "0";
                pReport.Dictionary.Variables["Resp56D"].Value = "0";
                pReport.Dictionary.Variables["Resp56E"].Value = "0";
                pReport.Dictionary.Variables["Resp57A"].Value = "0";
                pReport.Dictionary.Variables["Resp57B"].Value = "0";
                pReport.Dictionary.Variables["Resp57C"].Value = "0";
                pReport.Dictionary.Variables["Resp57D"].Value = "0";
                pReport.Dictionary.Variables["Resp57E"].Value = "0";
                pReport.Dictionary.Variables["Resp58A"].Value = "0";
                pReport.Dictionary.Variables["Resp58B"].Value = "0";
                pReport.Dictionary.Variables["Resp58C"].Value = "0";
                pReport.Dictionary.Variables["Resp58D"].Value = "0";
                pReport.Dictionary.Variables["Resp58E"].Value = "0";
                pReport.Dictionary.Variables["Resp59A"].Value = "0";
                pReport.Dictionary.Variables["Resp59B"].Value = "0";
                pReport.Dictionary.Variables["Resp59C"].Value = "0";
                pReport.Dictionary.Variables["Resp59D"].Value = "0";
                pReport.Dictionary.Variables["Resp59E"].Value = "0";
                pReport.Dictionary.Variables["Resp60A"].Value = "0";
                pReport.Dictionary.Variables["Resp60B"].Value = "0";
                pReport.Dictionary.Variables["Resp60C"].Value = "0";
                pReport.Dictionary.Variables["Resp60D"].Value = "0";
                pReport.Dictionary.Variables["Resp60E"].Value = "0";
                pReport.Dictionary.Variables["Resp61A"].Value = "0";
                pReport.Dictionary.Variables["Resp61B"].Value = "0";
                pReport.Dictionary.Variables["Resp61C"].Value = "0";
                pReport.Dictionary.Variables["Resp61D"].Value = "0";
                pReport.Dictionary.Variables["Resp61E"].Value = "0";
                pReport.Dictionary.Variables["Resp62A"].Value = "0";
                pReport.Dictionary.Variables["Resp62B"].Value = "0";
                pReport.Dictionary.Variables["Resp62C"].Value = "0";
                pReport.Dictionary.Variables["Resp62D"].Value = "0";
                pReport.Dictionary.Variables["Resp62E"].Value = "0";
                pReport.Dictionary.Variables["Resp63A"].Value = "0";
                pReport.Dictionary.Variables["Resp63B"].Value = "0";
                pReport.Dictionary.Variables["Resp63C"].Value = "0";
                pReport.Dictionary.Variables["Resp63D"].Value = "0";
                pReport.Dictionary.Variables["Resp63E"].Value = "0";
                pReport.Dictionary.Variables["Resp64A"].Value = "0";
                pReport.Dictionary.Variables["Resp64B"].Value = "0";
                pReport.Dictionary.Variables["Resp64C"].Value = "0";
                pReport.Dictionary.Variables["Resp64D"].Value = "0";
                pReport.Dictionary.Variables["Resp64E"].Value = "0";
                pReport.Dictionary.Variables["Resp65A"].Value = "0";
                pReport.Dictionary.Variables["Resp65B"].Value = "0";
                pReport.Dictionary.Variables["Resp65C"].Value = "0";
                pReport.Dictionary.Variables["Resp65D"].Value = "0";
                pReport.Dictionary.Variables["Resp65E"].Value = "0";
                pReport.Dictionary.Variables["Resp66A"].Value = "0";
                pReport.Dictionary.Variables["Resp66B"].Value = "0";
                pReport.Dictionary.Variables["Resp66C"].Value = "0";
                pReport.Dictionary.Variables["Resp66D"].Value = "0";
                pReport.Dictionary.Variables["Resp66E"].Value = "0";
                pReport.Dictionary.Variables["Resp67A"].Value = "0";
                pReport.Dictionary.Variables["Resp67B"].Value = "0";
                pReport.Dictionary.Variables["Resp67C"].Value = "0";
                pReport.Dictionary.Variables["Resp67D"].Value = "0";
                pReport.Dictionary.Variables["Resp67E"].Value = "0";
                pReport.Dictionary.Variables["Resp68A"].Value = "0";
                pReport.Dictionary.Variables["Resp68B"].Value = "0";
                pReport.Dictionary.Variables["Resp68C"].Value = "0";
                pReport.Dictionary.Variables["Resp68D"].Value = "0";
                pReport.Dictionary.Variables["Resp68E"].Value = "0";
                pReport.Dictionary.Variables["Resp69A"].Value = "0";
                pReport.Dictionary.Variables["Resp69B"].Value = "0";
                pReport.Dictionary.Variables["Resp69C"].Value = "0";
                pReport.Dictionary.Variables["Resp69D"].Value = "0";
                pReport.Dictionary.Variables["Resp69E"].Value = "0";
                pReport.Dictionary.Variables["Resp70A"].Value = "0";
                pReport.Dictionary.Variables["Resp70B"].Value = "0";
                pReport.Dictionary.Variables["Resp70C"].Value = "0";
                pReport.Dictionary.Variables["Resp70D"].Value = "0";
                pReport.Dictionary.Variables["Resp70E"].Value = "0";
                pReport.Dictionary.Variables["Resp71A"].Value = "0";
                pReport.Dictionary.Variables["Resp71B"].Value = "0";
                pReport.Dictionary.Variables["Resp71C"].Value = "0";
                pReport.Dictionary.Variables["Resp71D"].Value = "0";
                pReport.Dictionary.Variables["Resp71E"].Value = "0";
                pReport.Dictionary.Variables["Resp72A"].Value = "0";
                pReport.Dictionary.Variables["Resp72B"].Value = "0";
                pReport.Dictionary.Variables["Resp72C"].Value = "0";
                pReport.Dictionary.Variables["Resp72D"].Value = "0";
                pReport.Dictionary.Variables["Resp72E"].Value = "0";
                pReport.Dictionary.Variables["Resp73A"].Value = "0";
                pReport.Dictionary.Variables["Resp73B"].Value = "0";
                pReport.Dictionary.Variables["Resp73C"].Value = "0";
                pReport.Dictionary.Variables["Resp73D"].Value = "0";
                pReport.Dictionary.Variables["Resp73E"].Value = "0";
                pReport.Dictionary.Variables["Resp74A"].Value = "0";
                pReport.Dictionary.Variables["Resp74B"].Value = "0";
                pReport.Dictionary.Variables["Resp74C"].Value = "0";
                pReport.Dictionary.Variables["Resp74D"].Value = "0";
                pReport.Dictionary.Variables["Resp74E"].Value = "0";
                pReport.Dictionary.Variables["Resp75A"].Value = "0";
                pReport.Dictionary.Variables["Resp75B"].Value = "0";
                pReport.Dictionary.Variables["Resp75C"].Value = "0";
                pReport.Dictionary.Variables["Resp75D"].Value = "0";
                pReport.Dictionary.Variables["Resp75E"].Value = "0";

                pReport.Dictionary.Variables["NB_CANDIDATO"].Value = pNbCandidato;
            }

            pReport.Compile();
            swvReporte.Report = pReport;

        }

        public void SeleccionarVariableRespuesta(string a, string b, string c, string d, string e, string pAnswer, StiReport pReport)
        {
            switch (pAnswer)
            {
                case "a":
                    pReport.Dictionary.Variables[a].Value = "1";
                    pReport.Dictionary.Variables[b].Value = "0";
                    pReport.Dictionary.Variables[c].Value = "0";
                    pReport.Dictionary.Variables[d].Value = "0";
                    pReport.Dictionary.Variables[e].Value = "0";
                    break;
                case "b":
                    pReport.Dictionary.Variables[a].Value = "0";
                    pReport.Dictionary.Variables[b].Value = "1";
                    pReport.Dictionary.Variables[c].Value = "0";
                    pReport.Dictionary.Variables[d].Value = "0";
                    pReport.Dictionary.Variables[e].Value = "0";
                    break;
                case "c":
                    pReport.Dictionary.Variables[a].Value = "0";
                    pReport.Dictionary.Variables[b].Value = "0";
                    pReport.Dictionary.Variables[c].Value = "1";
                    pReport.Dictionary.Variables[d].Value = "0";
                    pReport.Dictionary.Variables[e].Value = "0";
                    break;
                case "d":
                    pReport.Dictionary.Variables[a].Value = "0";
                    pReport.Dictionary.Variables[b].Value = "0";
                    pReport.Dictionary.Variables[c].Value = "0";
                    pReport.Dictionary.Variables[d].Value = "1";
                    pReport.Dictionary.Variables[e].Value = "0";
                    break;
                case "e":
                    pReport.Dictionary.Variables[a].Value = "0";
                    pReport.Dictionary.Variables[b].Value = "0";
                    pReport.Dictionary.Variables[c].Value = "0";
                    pReport.Dictionary.Variables[d].Value = "0";
                    pReport.Dictionary.Variables[e].Value = "1";
                    break;
                case "1":
                    pReport.Dictionary.Variables[a].Value = "1";
                    pReport.Dictionary.Variables[b].Value = "0";
                    pReport.Dictionary.Variables[c].Value = "0";
                    pReport.Dictionary.Variables[d].Value = "0";
                    pReport.Dictionary.Variables[e].Value = "0";
                    break;
                case "2":
                    pReport.Dictionary.Variables[a].Value = "0";
                    pReport.Dictionary.Variables[b].Value = "1";
                    pReport.Dictionary.Variables[c].Value = "0";
                    pReport.Dictionary.Variables[d].Value = "0";
                    pReport.Dictionary.Variables[e].Value = "0";
                    break;
                case "3":
                    pReport.Dictionary.Variables[a].Value = "0";
                    pReport.Dictionary.Variables[b].Value = "0";
                    pReport.Dictionary.Variables[c].Value = "1";
                    pReport.Dictionary.Variables[d].Value = "0";
                    pReport.Dictionary.Variables[e].Value = "0";
                    break;
                case "4":
                    pReport.Dictionary.Variables[a].Value = "0";
                    pReport.Dictionary.Variables[b].Value = "0";
                    pReport.Dictionary.Variables[c].Value = "0";
                    pReport.Dictionary.Variables[d].Value = "1";
                    pReport.Dictionary.Variables[e].Value = "0";
                    break;
                case "5":
                    pReport.Dictionary.Variables[a].Value = "0";
                    pReport.Dictionary.Variables[b].Value = "0";
                    pReport.Dictionary.Variables[c].Value = "0";
                    pReport.Dictionary.Variables[d].Value = "0";
                    pReport.Dictionary.Variables[e].Value = "1";
                    break;
                default:
                    pReport.Dictionary.Variables[a].Value = "0";
                    pReport.Dictionary.Variables[b].Value = "0";
                    pReport.Dictionary.Variables[c].Value = "0";
                    pReport.Dictionary.Variables[d].Value = "0";
                    pReport.Dictionary.Variables[e].Value = "0";
                    break;
            }
        }

        public void asignarValoresIngles(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas, string pNbCandidato, StiReport pReport)
        {
            if (respuestas != null || respuestas.Count > 0)
            {
                foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
                {
                    switch (resp.CL_PREGUNTA)
                    {
                        case "INGLES-A-0001": SeleccionarBotonRespuesta("Resp1", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0002": SeleccionarBotonRespuesta("Resp2", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0003": SeleccionarBotonRespuesta("Resp3", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0004": SeleccionarBotonRespuesta("Resp4", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0005": SeleccionarBotonRespuesta("Resp5", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0006": SeleccionarBotonRespuesta("Resp6", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0007": SeleccionarBotonRespuesta("Resp7", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0008": SeleccionarBotonRespuesta("Resp8", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0009": SeleccionarBotonRespuesta("Resp9", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0010": SeleccionarBotonRespuesta("Resp10", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0011": SeleccionarBotonRespuesta("Resp11", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0012": SeleccionarBotonRespuesta("Resp12", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0013": SeleccionarBotonRespuesta("Resp13", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0014": SeleccionarBotonRespuesta("Resp14", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0015": SeleccionarBotonRespuesta("Resp15", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0016": SeleccionarBotonRespuesta("Resp16", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0017": SeleccionarBotonRespuesta("Resp17", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0018": SeleccionarBotonRespuesta("Resp18", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0019": SeleccionarBotonRespuesta("Resp19", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0020": SeleccionarBotonRespuesta("Resp20", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0021": SeleccionarBotonRespuesta("Resp21", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0022": SeleccionarBotonRespuesta("Resp22", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0023": SeleccionarBotonRespuesta("Resp23", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0024": SeleccionarBotonRespuesta("Resp24", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0025": SeleccionarBotonRespuesta("Resp25", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0026": SeleccionarBotonRespuesta("Resp26", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0027": SeleccionarBotonRespuesta("Resp27", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0028": SeleccionarBotonRespuesta("Resp28", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0029": SeleccionarBotonRespuesta("Resp29", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-A-0030": SeleccionarBotonRespuesta("Resp30", resp.NB_RESPUESTA, pReport); break;

                        ///////seccion 2

                        case "INGLES-B-0001": SeleccionarBotonRespuesta("Resp31", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0002": SeleccionarBotonRespuesta("Resp32", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0003": SeleccionarBotonRespuesta("Resp33", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0004": SeleccionarBotonRespuesta("Resp34", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0005": SeleccionarBotonRespuesta("Resp35", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0006": SeleccionarBotonRespuesta("Resp36", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0007": SeleccionarBotonRespuesta("Resp37", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0008": SeleccionarBotonRespuesta("Resp38", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0009": SeleccionarBotonRespuesta("Resp39", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0010": SeleccionarBotonRespuesta("Resp40", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0011": SeleccionarBotonRespuesta("Resp41", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0012": SeleccionarBotonRespuesta("Resp42", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0013": SeleccionarBotonRespuesta("Resp43", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0014": SeleccionarBotonRespuesta("Resp44", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0015": SeleccionarBotonRespuesta("Resp45", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0016": SeleccionarBotonRespuesta("Resp46", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0017": SeleccionarBotonRespuesta("Resp47", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0018": SeleccionarBotonRespuesta("Resp48", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0019": SeleccionarBotonRespuesta("Resp49", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0020": SeleccionarBotonRespuesta("Resp50", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0021": SeleccionarBotonRespuesta("Resp51", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0022": SeleccionarBotonRespuesta("Resp52", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0023": SeleccionarBotonRespuesta("Resp53", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0024": SeleccionarBotonRespuesta("Resp54", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0025": SeleccionarBotonRespuesta("Resp55", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0026": SeleccionarBotonRespuesta("Resp56", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0027": SeleccionarBotonRespuesta("Resp57", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0028": SeleccionarBotonRespuesta("Resp58", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0029": SeleccionarBotonRespuesta("Resp59", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-B-0030": SeleccionarBotonRespuesta("Resp60", resp.NB_RESPUESTA, pReport); break;
                        ///////seccion 3


                        case "INGLES-C-0001": SeleccionarBotonRespuesta("Resp61", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0002": SeleccionarBotonRespuesta("Resp62", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0003": SeleccionarBotonRespuesta("Resp63", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0004": SeleccionarBotonRespuesta("Resp64", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0005": SeleccionarBotonRespuesta("Resp65", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0006": SeleccionarBotonRespuesta("Resp66", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0007": SeleccionarBotonRespuesta("Resp67", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0008": SeleccionarBotonRespuesta("Resp68", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0009": SeleccionarBotonRespuesta("Resp69", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0010": SeleccionarBotonRespuesta("Resp70", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0011": SeleccionarBotonRespuesta("Resp71", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0012": SeleccionarBotonRespuesta("Resp72", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0013": SeleccionarBotonRespuesta("Resp73", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0014": SeleccionarBotonRespuesta("Resp74", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0015": SeleccionarBotonRespuesta("Resp75", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0016": SeleccionarBotonRespuesta("Resp76", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0017": SeleccionarBotonRespuesta("Resp77", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0018": SeleccionarBotonRespuesta("Resp78", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0019": SeleccionarBotonRespuesta("Resp79", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0020": SeleccionarBotonRespuesta("Resp80", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0021": SeleccionarBotonRespuesta("Resp81", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0022": SeleccionarBotonRespuesta("Resp82", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0023": SeleccionarBotonRespuesta("Resp83", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0024": SeleccionarBotonRespuesta("Resp84", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0025": SeleccionarBotonRespuesta("Resp85", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0026": SeleccionarBotonRespuesta("Resp86", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0027": SeleccionarBotonRespuesta("Resp87", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0028": SeleccionarBotonRespuesta("Resp88", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0029": SeleccionarBotonRespuesta("Resp89", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-C-0030": SeleccionarBotonRespuesta("Resp90", resp.NB_RESPUESTA, pReport); break;
                        ///////seccion


                        case "INGLES-D-0001": SeleccionarBotonRespuesta("Resp91", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0002": SeleccionarBotonRespuesta("Resp92", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0003": SeleccionarBotonRespuesta("Resp93", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0004": SeleccionarBotonRespuesta("Resp94", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0005": SeleccionarBotonRespuesta("Resp95", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0006": SeleccionarBotonRespuesta("Resp96", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0007": SeleccionarBotonRespuesta("Resp97", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0008": SeleccionarBotonRespuesta("Resp98", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0009": SeleccionarBotonRespuesta("Resp99", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0010": SeleccionarBotonRespuesta("Resp100", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0011": SeleccionarBotonRespuesta("Resp101", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0012": SeleccionarBotonRespuesta("Resp102", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0013": SeleccionarBotonRespuesta("Resp103", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0014": SeleccionarBotonRespuesta("Resp104", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0015": SeleccionarBotonRespuesta("Resp105", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0016": SeleccionarBotonRespuesta("Resp106", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0017": SeleccionarBotonRespuesta("Resp107", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0018": SeleccionarBotonRespuesta("Resp108", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0019": SeleccionarBotonRespuesta("Resp109", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0020": SeleccionarBotonRespuesta("Resp110", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0021": SeleccionarBotonRespuesta("Resp111", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0022": SeleccionarBotonRespuesta("Resp112", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0023": SeleccionarBotonRespuesta("Resp113", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0024": SeleccionarBotonRespuesta("Resp114", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0025": SeleccionarBotonRespuesta("Resp115", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0026": SeleccionarBotonRespuesta("Resp116", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0027": SeleccionarBotonRespuesta("Resp117", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0028": SeleccionarBotonRespuesta("Resp118", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0029": SeleccionarBotonRespuesta("Resp119", resp.NB_RESPUESTA, pReport); break;
                        case "INGLES-D-0030": SeleccionarBotonRespuesta("Resp120", resp.NB_RESPUESTA, pReport); break;
                        ///////seccion
                        default: break;
                    }
                }
                pReport.Dictionary.Variables["NB_CANDIDATO"].Value = pNbCandidato;
            }

            pReport.Compile();
            swvReporte.Report = pReport;
            
        }

        public void SeleccionarBotonRespuesta(string vVariable, string pAnswer, StiReport pReport)
        {
            switch (pAnswer)
            {
                case "A": pReport.Dictionary.Variables[vVariable].Value = pAnswer; 
                    break;
                case "B": pReport.Dictionary.Variables[vVariable].Value = pAnswer; 
                    break;
                case "C": pReport.Dictionary.Variables[vVariable].Value = pAnswer;  
                    break;
                case "D": pReport.Dictionary.Variables[vVariable].Value = pAnswer;  
                    break;
                default: pReport.Dictionary.Variables[vVariable].Value = ""; 
                    break;
            }
        }


        private void CargarLaboral1()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();

            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);

            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();


            report.Load(Server.MapPath("~/Assets/reports/IDP/Laboral1.mrt"));
            report.Dictionary.Databases.Clear();

            //System.Configuration.Configuration rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
            //pathValue = rootWebConfig1.ConnectionStrings.ConnectionStrings[0].ConnectionString;

            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));


            if (vRespuestas.Count > 0)
            {
                AsignarValorLaboral1(report, "M1", vRespuestas, "LABORAL1-A-0001");
                AsignarValorLaboral1(report, "M2", vRespuestas, "LABORAL1-A-0002");
                AsignarValorLaboral1(report, "M3", vRespuestas, "LABORAL1-A-0003");
                AsignarValorLaboral1(report, "M4", vRespuestas, "LABORAL1-A-0004");
                AsignarValorLaboral1(report, "M5", vRespuestas, "LABORAL1-A-0005");
                AsignarValorLaboral1(report, "M6", vRespuestas, "LABORAL1-A-0006");
                AsignarValorLaboral1(report, "M7", vRespuestas, "LABORAL1-A-0007");
                AsignarValorLaboral1(report, "M8", vRespuestas, "LABORAL1-A-0008");
                AsignarValorLaboral1(report, "M9", vRespuestas, "LABORAL1-A-0009");
                AsignarValorLaboral1(report, "M10", vRespuestas, "LABORAL1-A-0010");
                AsignarValorLaboral1(report, "M11", vRespuestas, "LABORAL1-A-0011");
                AsignarValorLaboral1(report, "M12", vRespuestas, "LABORAL1-A-0012");
                AsignarValorLaboral1(report, "M13", vRespuestas, "LABORAL1-A-0013");
                AsignarValorLaboral1(report, "M14", vRespuestas, "LABORAL1-A-0014");
                AsignarValorLaboral1(report, "M15", vRespuestas, "LABORAL1-A-0015");
                AsignarValorLaboral1(report, "M16", vRespuestas, "LABORAL1-A-0016");
                AsignarValorLaboral1(report, "M17", vRespuestas, "LABORAL1-A-0017");
                AsignarValorLaboral1(report, "M18", vRespuestas, "LABORAL1-A-0018");
                AsignarValorLaboral1(report, "M19", vRespuestas, "LABORAL1-A-0019");
                AsignarValorLaboral1(report, "M20", vRespuestas, "LABORAL1-A-0020");
                AsignarValorLaboral1(report, "M21", vRespuestas, "LABORAL1-A-0021");
                AsignarValorLaboral1(report, "M22", vRespuestas, "LABORAL1-A-0022");
                AsignarValorLaboral1(report, "M23", vRespuestas, "LABORAL1-A-0023");
                AsignarValorLaboral1(report, "M24", vRespuestas, "LABORAL1-A-0024");

                AsignarValorLaboral1(report, "L1", vRespuestas, "LABORAL1-B-0001");
                AsignarValorLaboral1(report, "L2", vRespuestas, "LABORAL1-B-0002");
                AsignarValorLaboral1(report, "L3", vRespuestas, "LABORAL1-B-0003");
                AsignarValorLaboral1(report, "L4", vRespuestas, "LABORAL1-B-0004");
                AsignarValorLaboral1(report, "L5", vRespuestas, "LABORAL1-B-0005");
                AsignarValorLaboral1(report, "L6", vRespuestas, "LABORAL1-B-0006");
                AsignarValorLaboral1(report, "L7", vRespuestas, "LABORAL1-B-0007");
                AsignarValorLaboral1(report, "L8", vRespuestas, "LABORAL1-B-0008");
                AsignarValorLaboral1(report, "L9", vRespuestas, "LABORAL1-B-0009");
                AsignarValorLaboral1(report, "L10", vRespuestas, "LABORAL1-B-0010");
                AsignarValorLaboral1(report, "L11", vRespuestas, "LABORAL1-B-0011");
                AsignarValorLaboral1(report, "L12", vRespuestas, "LABORAL1-B-0012");
                AsignarValorLaboral1(report, "L13", vRespuestas, "LABORAL1-B-0013");
                AsignarValorLaboral1(report, "L14", vRespuestas, "LABORAL1-B-0014");
                AsignarValorLaboral1(report, "L15", vRespuestas, "LABORAL1-B-0015");
                AsignarValorLaboral1(report, "L16", vRespuestas, "LABORAL1-B-0016");
                AsignarValorLaboral1(report, "L17", vRespuestas, "LABORAL1-B-0017");
                AsignarValorLaboral1(report, "L18", vRespuestas, "LABORAL1-B-0018");
                AsignarValorLaboral1(report, "L19", vRespuestas, "LABORAL1-B-0019");
                AsignarValorLaboral1(report, "L20", vRespuestas, "LABORAL1-B-0020");
                AsignarValorLaboral1(report, "L21", vRespuestas, "LABORAL1-B-0021");
                AsignarValorLaboral1(report, "L22", vRespuestas, "LABORAL1-B-0022");
                AsignarValorLaboral1(report, "L23", vRespuestas, "LABORAL1-B-0023");
                AsignarValorLaboral1(report, "L24", vRespuestas, "LABORAL1-B-0024");

                AsignarValorLaboral1(report, "M1", vRespuestas, "LABORAL1-RES-A01");
                AsignarValorLaboral1(report, "M2", vRespuestas, "LABORAL1-RES-B01");
                AsignarValorLaboral1(report, "M3", vRespuestas, "LABORAL1-RES-C01");
                AsignarValorLaboral1(report, "M4", vRespuestas, "LABORAL1-RES-D01");
                AsignarValorLaboral1(report, "M5", vRespuestas, "LABORAL1-RES-E01");
                AsignarValorLaboral1(report, "M6", vRespuestas, "LABORAL1-RES-F01");
                AsignarValorLaboral1(report, "M7", vRespuestas, "LABORAL1-RES-G01");
                AsignarValorLaboral1(report, "M8", vRespuestas, "LABORAL1-RES-H01");
                AsignarValorLaboral1(report, "M9", vRespuestas, "LABORAL1-RES-I01");
                AsignarValorLaboral1(report, "M10", vRespuestas, "LABORAL1-RES-J01");
                AsignarValorLaboral1(report, "M11", vRespuestas, "LABORAL1-RES-K01");
                AsignarValorLaboral1(report, "M12", vRespuestas, "LABORAL1-RES-L01");
                AsignarValorLaboral1(report, "M13", vRespuestas, "LABORAL1-RES-M01");
                AsignarValorLaboral1(report, "M14", vRespuestas, "LABORAL1-RES-N01");
                AsignarValorLaboral1(report, "M15", vRespuestas, "LABORAL1-RES-O01");
                AsignarValorLaboral1(report, "M16", vRespuestas, "LABORAL1-RES-P01");
                AsignarValorLaboral1(report, "M17", vRespuestas, "LABORAL1-RES-Q01");
                AsignarValorLaboral1(report, "M18", vRespuestas, "LABORAL1-RES-R01");
                AsignarValorLaboral1(report, "M19", vRespuestas, "LABORAL1-RES-S01");
                AsignarValorLaboral1(report, "M20", vRespuestas, "LABORAL1-RES-T01");
                AsignarValorLaboral1(report, "M21", vRespuestas, "LABORAL1-RES-U01");
                AsignarValorLaboral1(report, "M22", vRespuestas, "LABORAL1-RES-V01");
                AsignarValorLaboral1(report, "M23", vRespuestas, "LABORAL1-RES-W01");
                AsignarValorLaboral1(report, "M24", vRespuestas, "LABORAL1-RES-X01");

                AsignarValorLaboral1(report, "L1", vRespuestas, "LABORAL1-RES-A02");
                AsignarValorLaboral1(report, "L2", vRespuestas, "LABORAL1-RES-B02");
                AsignarValorLaboral1(report, "L3", vRespuestas, "LABORAL1-RES-C02");
                AsignarValorLaboral1(report, "L4", vRespuestas, "LABORAL1-RES-D02");
                AsignarValorLaboral1(report, "L5", vRespuestas, "LABORAL1-RES-E02");
                AsignarValorLaboral1(report, "L6", vRespuestas, "LABORAL1-RES-F02");
                AsignarValorLaboral1(report, "L7", vRespuestas, "LABORAL1-RES-G02");
                AsignarValorLaboral1(report, "L8", vRespuestas, "LABORAL1-RES-H02");
                AsignarValorLaboral1(report, "L9", vRespuestas, "LABORAL1-RES-I02");
                AsignarValorLaboral1(report, "L10", vRespuestas, "LABORAL1-RES-J02");
                AsignarValorLaboral1(report, "L11", vRespuestas, "LABORAL1-RES-K02");
                AsignarValorLaboral1(report, "L12", vRespuestas, "LABORAL1-RES-L02");
                AsignarValorLaboral1(report, "L13", vRespuestas, "LABORAL1-RES-M02");
                AsignarValorLaboral1(report, "L14", vRespuestas, "LABORAL1-RES-N02");
                AsignarValorLaboral1(report, "L15", vRespuestas, "LABORAL1-RES-O02");
                AsignarValorLaboral1(report, "L16", vRespuestas, "LABORAL1-RES-P02");
                AsignarValorLaboral1(report, "L17", vRespuestas, "LABORAL1-RES-Q02");
                AsignarValorLaboral1(report, "L18", vRespuestas, "LABORAL1-RES-R02");
                AsignarValorLaboral1(report, "L19", vRespuestas, "LABORAL1-RES-S02");
                AsignarValorLaboral1(report, "L20", vRespuestas, "LABORAL1-RES-T02");
                AsignarValorLaboral1(report, "L21", vRespuestas, "LABORAL1-RES-U02");
                AsignarValorLaboral1(report, "L22", vRespuestas, "LABORAL1-RES-V02");
                AsignarValorLaboral1(report, "L23", vRespuestas, "LABORAL1-RES-W02");
                AsignarValorLaboral1(report, "L24", vRespuestas, "LABORAL1-RES-X02");

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;

            }
            else
            {
                report.Dictionary.Variables["M1"].Value = "0";
                report.Dictionary.Variables["M2"].Value = "0";
                report.Dictionary.Variables["M3"].Value = "0";
                report.Dictionary.Variables["M4"].Value = "0";
                report.Dictionary.Variables["M5"].Value = "0";
                report.Dictionary.Variables["M6"].Value = "0";
                report.Dictionary.Variables["M7"].Value = "0";
                report.Dictionary.Variables["M8"].Value = "0";
                report.Dictionary.Variables["M9"].Value = "0";
                report.Dictionary.Variables["M10"].Value = "0";
                report.Dictionary.Variables["M11"].Value = "0";
                report.Dictionary.Variables["M12"].Value = "0";
                report.Dictionary.Variables["M13"].Value = "0";
                report.Dictionary.Variables["M14"].Value = "0";
                report.Dictionary.Variables["M15"].Value = "0";
                report.Dictionary.Variables["M16"].Value = "0";
                report.Dictionary.Variables["M17"].Value = "0";
                report.Dictionary.Variables["M18"].Value = "0";
                report.Dictionary.Variables["M19"].Value = "0";
                report.Dictionary.Variables["M20"].Value = "0";
                report.Dictionary.Variables["M21"].Value = "0";
                report.Dictionary.Variables["M22"].Value = "0";
                report.Dictionary.Variables["M23"].Value = "0";
                report.Dictionary.Variables["M24"].Value = "0";

                report.Dictionary.Variables["L1"].Value = "0";
                report.Dictionary.Variables["L2"].Value = "0";
                report.Dictionary.Variables["L3"].Value = "0";
                report.Dictionary.Variables["L4"].Value = "0";
                report.Dictionary.Variables["L5"].Value = "0";
                report.Dictionary.Variables["L6"].Value = "0";
                report.Dictionary.Variables["L7"].Value = "0";
                report.Dictionary.Variables["L8"].Value = "0";
                report.Dictionary.Variables["L9"].Value = "0";
                report.Dictionary.Variables["L10"].Value = "0";
                report.Dictionary.Variables["L11"].Value = "0";
                report.Dictionary.Variables["L12"].Value = "0";
                report.Dictionary.Variables["L13"].Value = "0";
                report.Dictionary.Variables["L14"].Value = "0";
                report.Dictionary.Variables["L15"].Value = "0";
                report.Dictionary.Variables["L16"].Value = "0";
                report.Dictionary.Variables["L17"].Value = "0";
                report.Dictionary.Variables["L18"].Value = "0";
                report.Dictionary.Variables["L19"].Value = "0";
                report.Dictionary.Variables["L20"].Value = "0";
                report.Dictionary.Variables["L21"].Value = "0";
                report.Dictionary.Variables["L22"].Value = "0";
                report.Dictionary.Variables["L23"].Value = "0";
                report.Dictionary.Variables["L24"].Value = "0";

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;

            }

            report.Compile();
            swvReporte.Report = report;
        }

        private void CargarInteresesPersonales()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();

            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);

            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();


            report.Load(Server.MapPath("~/Assets/reports/IDP/InteresesPersonales.mrt"));
            report.Dictionary.Databases.Clear();

            //System.Configuration.Configuration rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
            //pathValue = rootWebConfig1.ConnectionStrings.ConnectionStrings[0].ConnectionString;

            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

            if (vRespuestas.Count > 0)
            {
                AsignarInteresesPersonale(report, "G1R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-A0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G1R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-A0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G1R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-A0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G1R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-A0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G1R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-A0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G1R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-A0006")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "G2R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-B0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G2R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-B0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G2R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-B0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G2R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-B0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G2R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-B0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G2R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-B0006")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "G3R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-C0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G3R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-C0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G3R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-C0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G3R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-C0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G3R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-C0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G3R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-C0006")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "G4R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-D0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G4R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-D0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G4R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-D0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G4R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-D0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G4R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-D0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G4R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-D0006")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "G5R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-E0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G5R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-E0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G5R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-E0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G5R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-E0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G5R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-E0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G5R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-E0006")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "G6R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-F0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G6R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-F0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G6R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-F0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G6R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-F0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G6R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-F0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G6R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-F0006")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "G7R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-G0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G7R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-G0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G7R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-G0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G7R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-G0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G7R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-G0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G7R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-G0006")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "G8R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-H0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G8R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-H0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G8R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-H0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G8R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-H0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G8R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-H0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G8R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-H0006")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "G9R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-I0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G9R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-I0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G9R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-I0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G9R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-I0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G9R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-I0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G9R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-I0006")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "G10R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-J0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G10R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-J0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G10R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-J0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G10R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-J0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G10R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-J0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "G10R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("INTERES-J0006")).FirstOrDefault().NB_RESPUESTA);

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }
            else
            {
                report.Dictionary.Variables["G1R1"].Value = "";
                report.Dictionary.Variables["G1R2"].Value = "";
                report.Dictionary.Variables["G1R3"].Value = "";
                report.Dictionary.Variables["G1R4"].Value = "";
                report.Dictionary.Variables["G1R5"].Value = "";
                report.Dictionary.Variables["G1R6"].Value = "";

                report.Dictionary.Variables["G2R1"].Value = "";
                report.Dictionary.Variables["G2R2"].Value = "";
                report.Dictionary.Variables["G2R3"].Value = "";
                report.Dictionary.Variables["G2R4"].Value = "";
                report.Dictionary.Variables["G2R5"].Value = "";
                report.Dictionary.Variables["G2R6"].Value = "";

                report.Dictionary.Variables["G3R1"].Value = "";
                report.Dictionary.Variables["G3R2"].Value = "";
                report.Dictionary.Variables["G3R3"].Value = "";
                report.Dictionary.Variables["G3R4"].Value = "";
                report.Dictionary.Variables["G3R5"].Value = "";
                report.Dictionary.Variables["G3R6"].Value = "";

                report.Dictionary.Variables["G4R1"].Value = "";
                report.Dictionary.Variables["G4R2"].Value = "";
                report.Dictionary.Variables["G4R3"].Value = "";
                report.Dictionary.Variables["G4R4"].Value = "";
                report.Dictionary.Variables["G4R5"].Value = "";
                report.Dictionary.Variables["G4R6"].Value = "";

                report.Dictionary.Variables["G5R1"].Value = "";
                report.Dictionary.Variables["G5R2"].Value = "";
                report.Dictionary.Variables["G5R3"].Value = "";
                report.Dictionary.Variables["G5R4"].Value = "";
                report.Dictionary.Variables["G5R5"].Value = "";
                report.Dictionary.Variables["G5R6"].Value = "";

                report.Dictionary.Variables["G6R1"].Value = "";
                report.Dictionary.Variables["G6R2"].Value = "";
                report.Dictionary.Variables["G6R3"].Value = "";
                report.Dictionary.Variables["G6R4"].Value = "";
                report.Dictionary.Variables["G6R5"].Value = "";
                report.Dictionary.Variables["G6R6"].Value = "";

                report.Dictionary.Variables["G7R1"].Value = "";
                report.Dictionary.Variables["G7R2"].Value = "";
                report.Dictionary.Variables["G7R3"].Value = "";
                report.Dictionary.Variables["G7R4"].Value = "";
                report.Dictionary.Variables["G7R5"].Value = "";
                report.Dictionary.Variables["G7R6"].Value = "";

                report.Dictionary.Variables["G8R1"].Value = "";
                report.Dictionary.Variables["G8R2"].Value = "";
                report.Dictionary.Variables["G8R3"].Value = "";
                report.Dictionary.Variables["G8R4"].Value = "";
                report.Dictionary.Variables["G8R5"].Value = "";
                report.Dictionary.Variables["G8R6"].Value = "";

                report.Dictionary.Variables["G9R1"].Value = "";
                report.Dictionary.Variables["G9R2"].Value = "";
                report.Dictionary.Variables["G9R3"].Value = "";
                report.Dictionary.Variables["G9R4"].Value = "";
                report.Dictionary.Variables["G9R5"].Value = "";
                report.Dictionary.Variables["G9R6"].Value = "";

                report.Dictionary.Variables["G10R1"].Value = "";
                report.Dictionary.Variables["G01R2"].Value = "";
                report.Dictionary.Variables["G10R3"].Value = "";
                report.Dictionary.Variables["G10R4"].Value = "";
                report.Dictionary.Variables["G10R5"].Value = "";
                report.Dictionary.Variables["G10R6"].Value = "";

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }

            report.Compile();
            swvReporte.Report = report;
        }

        private void CargarEstilo()
        {

            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();

            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);

            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();

            report.Load(Server.MapPath("~/Assets/reports/IDP/EstiloPensamiento.mrt"));
            report.Dictionary.Databases.Clear();

            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

            if (vPrueba.NB_TIPO_PRUEBA == "MANUAL" && vRespuestas.Count > 0)
            {
                AsignarInteresesPersonale(report, "S1R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A1")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A2")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A3")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A4")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A5")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A6")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R7", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A7")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R8", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A8")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R9", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A9")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R10", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A10")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R11", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A11")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R12", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A12")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R13", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A13")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R14", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A14")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R15", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A15")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R16", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A16")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R17", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A17")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R18", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A18")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R19", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A19")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R20", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_A20")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "S2R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B1")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B2")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B3")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B4")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B5")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B6")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R7", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B7")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R8", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B8")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R9", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B9")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R10", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B10")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R11", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B11")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R12", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B12")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R13", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B13")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R14", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B14")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R15", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B15")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R16", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B16")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R17", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B17")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R18", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B18")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R19", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B19")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R20", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_B20")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "S3R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C1")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C2")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C3")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C4")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C5")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C6")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R7", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C7")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R8", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C8")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R9", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C9")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R10", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C10")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R11", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C11")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R12", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C12")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R13", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C13")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R14", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C14")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R15", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C15")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R16", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C16")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R17", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C17")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R18", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C18")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R19", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C19")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R20", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO_RES_C20")).FirstOrDefault().NB_RESPUESTA);

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;

            }
            else if (vPrueba.NB_TIPO_PRUEBA == "APLICACION" && vRespuestas.Count > 0)
            {
                AsignarInteresesPersonale(report, "S1R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0006")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R7", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0007")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R8", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0008")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R9", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0009")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R10", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0010")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R11", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0011")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R12", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0012")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R13", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0013")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R14", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0014")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R15", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0015")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R16", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0016")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R17", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0017")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R18", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0018")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R19", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0019")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S1R20", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-A-0020")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "S2R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0006")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R7", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0007")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R8", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0008")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R9", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0009")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R10", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0010")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R11", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0011")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R12", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0012")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R13", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0013")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R14", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0014")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R15", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0015")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R16", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0016")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R17", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0017")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R18", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0018")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R19", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0019")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S2R20", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-B-0020")).FirstOrDefault().NB_RESPUESTA);

                AsignarInteresesPersonale(report, "S3R1", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0001")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R2", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0002")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R3", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0003")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R4", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0004")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R5", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0005")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R6", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0006")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R7", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0007")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R8", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0008")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R9", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0009")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R10", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0010")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R11", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0011")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R12", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0012")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R13", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0013")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R14", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0014")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R15", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0015")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R16", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0016")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R17", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0017")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R18", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0018")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R19", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0019")).FirstOrDefault().NB_RESPUESTA);
                AsignarInteresesPersonale(report, "S3R20", vRespuestas.Where(t => t.CL_PREGUNTA.Equals("PENSAMIENTO-C-0020")).FirstOrDefault().NB_RESPUESTA);

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }
            else
            {
                report.Dictionary.Variables["S1R1"].Value = "";
                report.Dictionary.Variables["S1R2"].Value = "";
                report.Dictionary.Variables["S1R3"].Value = "";
                report.Dictionary.Variables["S1R4"].Value = "";
                report.Dictionary.Variables["S1R5"].Value = "";
                report.Dictionary.Variables["S1R6"].Value = "";
                report.Dictionary.Variables["S1R7"].Value = "";
                report.Dictionary.Variables["S1R8"].Value = "";
                report.Dictionary.Variables["S1R9"].Value = "";
                report.Dictionary.Variables["S1R10"].Value = "";
                report.Dictionary.Variables["S1R11"].Value = "";
                report.Dictionary.Variables["S1R12"].Value = "";
                report.Dictionary.Variables["S1R13"].Value = "";
                report.Dictionary.Variables["S1R14"].Value = "";
                report.Dictionary.Variables["S1R15"].Value = "";
                report.Dictionary.Variables["S1R16"].Value = "";
                report.Dictionary.Variables["S1R17"].Value = "";
                report.Dictionary.Variables["S1R18"].Value = "";
                report.Dictionary.Variables["S1R19"].Value = "";
                report.Dictionary.Variables["S1R20"].Value = "";

                report.Dictionary.Variables["S2R1"].Value = "";
                report.Dictionary.Variables["S2R2"].Value = "";
                report.Dictionary.Variables["S2R3"].Value = "";
                report.Dictionary.Variables["S2R4"].Value = "";
                report.Dictionary.Variables["S2R5"].Value = "";
                report.Dictionary.Variables["S2R6"].Value = "";
                report.Dictionary.Variables["S2R7"].Value = "";
                report.Dictionary.Variables["S2R8"].Value = "";
                report.Dictionary.Variables["S2R9"].Value = "";
                report.Dictionary.Variables["S2R10"].Value = "";
                report.Dictionary.Variables["S2R11"].Value = "";
                report.Dictionary.Variables["S2R12"].Value = "";
                report.Dictionary.Variables["S2R13"].Value = "";
                report.Dictionary.Variables["S2R14"].Value = "";
                report.Dictionary.Variables["S2R15"].Value = "";
                report.Dictionary.Variables["S2R16"].Value = "";
                report.Dictionary.Variables["S2R17"].Value = "";
                report.Dictionary.Variables["S2R18"].Value = "";
                report.Dictionary.Variables["S2R19"].Value = "";
                report.Dictionary.Variables["S2R20"].Value = "";

                report.Dictionary.Variables["S3R1"].Value = "";
                report.Dictionary.Variables["S3R2"].Value = "";
                report.Dictionary.Variables["S3R3"].Value = "";
                report.Dictionary.Variables["S3R4"].Value = "";
                report.Dictionary.Variables["S3R5"].Value = "";
                report.Dictionary.Variables["S3R6"].Value = "";
                report.Dictionary.Variables["S3R7"].Value = "";
                report.Dictionary.Variables["S3R8"].Value = "";
                report.Dictionary.Variables["S3R9"].Value = "";
                report.Dictionary.Variables["S3R10"].Value = "";
                report.Dictionary.Variables["S3R11"].Value = "";
                report.Dictionary.Variables["S3R12"].Value = "";
                report.Dictionary.Variables["S3R13"].Value = "";
                report.Dictionary.Variables["S3R14"].Value = "";
                report.Dictionary.Variables["S3R15"].Value = "";
                report.Dictionary.Variables["S3R16"].Value = "";
                report.Dictionary.Variables["S3R17"].Value = "";
                report.Dictionary.Variables["S3R18"].Value = "";
                report.Dictionary.Variables["S3R19"].Value = "";
                report.Dictionary.Variables["S3R20"].Value = "";

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }

            report.Compile();
            swvReporte.Report = report;

        }

        private void AsignarRespuestaTecPC(StiReport pReport, string pValorA, string pValorB, string pVariableV, string pVariableF, string pRespuesta)
        {
            if (pValorA.Equals(pRespuesta))
            {
                pReport.Dictionary.Variables[pVariableV].Value = "1";
                pReport.Dictionary.Variables[pVariableF].Value = "0";
            }
            else if (pValorB.Equals(pRespuesta))
            {
                pReport.Dictionary.Variables[pVariableV].Value = "0";
                pReport.Dictionary.Variables[pVariableF].Value = "1";
            }
            else
            {
                pReport.Dictionary.Variables[pVariableV].Value = "0";
                pReport.Dictionary.Variables[pVariableF].Value = "0";
            }


        }

        private void CargarTecnicaPC()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();

            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);

            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();


            report.Load(Server.MapPath("~/Assets/reports/IDP/TecnicaPC.mrt"));
            report.Dictionary.Databases.Clear();

            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

            if (vRespuestas.Count > 0)
            {
                foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in vRespuestas)
                {
                    switch (resp.CL_PREGUNTA)
                    {
                        case "TECNICAPC-A-0001": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC1Av", "RespTecPC1Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0002": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC1Bv", "RespTecPC1Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0003": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC1Cv", "RespTecPC1Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0004": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC1Dv", "RespTecPC1Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0005": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC2Av", "RespTecPC2Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0006": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC2Bv", "RespTecPC2Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0007": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC2Cv", "RespTecPC2Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0008": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC2Dv", "RespTecPC2Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0009": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC3Av", "RespTecPC3Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0010": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC3Bv", "RespTecPC3Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0011": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC3Cv", "RespTecPC3Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0012": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC3Dv", "RespTecPC3Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0013": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC4Av", "RespTecPC4Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0014": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC4Bv", "RespTecPC4Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0015": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC4Cv", "RespTecPC4Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0016": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC4Dv", "RespTecPC4Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0017": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC5Av", "RespTecPC5Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0018": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC5Bv", "RespTecPC5Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0019": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC5Cv", "RespTecPC5Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0020": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC5Dv", "RespTecPC5Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0021": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC6Av", "RespTecPC6Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0022": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC6Bv", "RespTecPC6Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0023": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC6Cv", "RespTecPC6Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0024": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC6Dv", "RespTecPC6Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0025": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC7Av", "RespTecPC7Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0026": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC7Bv", "RespTecPC7Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0027": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC7Cv", "RespTecPC7Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0028": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC7Dv", "RespTecPC7Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0029": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC8Av", "RespTecPC8Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0030": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC8Bv", "RespTecPC8Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0031": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC8Cv", "RespTecPC8Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0032": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC8Dv", "RespTecPC8Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0033": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC9Av", "RespTecPC9Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0034": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC9Bv", "RespTecPC9Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0035": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC9Cv", "RespTecPC9Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0036": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC9Dv", "RespTecPC9Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0037": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC10Av", "RespTecPC10Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0038": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC10Bv", "RespTecPC10Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0039": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC10Cv", "RespTecPC10Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0040": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC10Dv", "RespTecPC10Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0041": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC11Av", "RespTecPC11Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0042": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC11Bv", "RespTecPC11Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0043": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC11Cv", "RespTecPC11Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0044": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC11Dv", "RespTecPC11Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0045": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC12Av", "RespTecPC12Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0046": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC12Bv", "RespTecPC12Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0047": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC12Cv", "RespTecPC12Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0048": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC12Dv", "RespTecPC12Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0049": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC13Av", "RespTecPC13Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0050": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC13Bv", "RespTecPC13Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0051": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC13Cv", "RespTecPC13Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0052": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC13Dv", "RespTecPC13Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0053": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC14Av", "RespTecPC14Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0054": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC14Bv", "RespTecPC14Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0055": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC14Cv", "RespTecPC14Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0056": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC14Dv", "RespTecPC14Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0057": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC15Av", "RespTecPC15Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0058": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC15Bv", "RespTecPC15Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0059": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC15Cv", "RespTecPC15Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0060": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC15Dv", "RespTecPC15Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0061": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC16Av", "RespTecPC16Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0062": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC16Bv", "RespTecPC16Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0063": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC16Cv", "RespTecPC16Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0064": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC16Dv", "RespTecPC16Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0065": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC17Av", "RespTecPC17Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0066": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC17Bv", "RespTecPC17Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0067": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC17Cv", "RespTecPC17Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0068": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC17Dv", "RespTecPC17Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0069": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC18Av", "RespTecPC18Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0070": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC18Bv", "RespTecPC18Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0071": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC18Cv", "RespTecPC18Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0072": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC18Dv", "RespTecPC18Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0073": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC19Av", "RespTecPC19Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0074": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC19Bv", "RespTecPC19Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0075": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC19Cv", "RespTecPC19Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0076": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC19Dv", "RespTecPC19Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0077": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC20Av", "RespTecPC20Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0078": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC20Bv", "RespTecPC20Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0079": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC20Cv", "RespTecPC20Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0080": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC20Dv", "RespTecPC20Df", resp.NB_RESPUESTA); break;

                        case "TECNICAPC-A-0081": AsignarRespuestaTecPC(report, "0", "1", "RespTecPC21Av", "RespTecPC21Af", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0082": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC21Bv", "RespTecPC21Bf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0083": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC21Cv", "RespTecPC21Cf", resp.NB_RESPUESTA); break;
                        case "TECNICAPC-A-0084": AsignarRespuestaTecPC(report, "1", "0", "RespTecPC21Dv", "RespTecPC21Df", resp.NB_RESPUESTA); break;
                    }
                }
                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }
            else
            {
                report.Dictionary.Variables["RespTecPC1Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC1Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC1Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC1Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC1Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC1Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC1Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC1Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC2Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC2Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC2Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC2Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC2Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC2Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC2Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC2Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC3Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC3Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC3Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC3Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC3Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC3Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC3Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC3Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC4Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC4Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC4Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC4Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC4Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC4Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC4Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC4Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC5Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC5Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC5Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC5Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC5Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC5Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC5Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC5Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC6Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC6Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC6Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC6Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC6Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC6Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC6Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC6Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC7Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC7Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC7Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC7Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC7Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC7Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC7Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC7Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC8Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC8Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC8Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC8Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC8Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC8Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC8Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC8Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC9Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC9Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC9Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC9Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC9Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC9Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC9Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC9Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC10Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC10Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC10Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC10Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC10Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC10Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC10Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC10Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC11Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC11Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC11Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC11Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC11Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC11Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC11Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC11Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC12Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC12Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC12Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC12Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC12Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC12Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC12Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC12Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC13Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC13Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC13Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC13Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC13Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC13Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC13Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC13Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC14Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC14Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC14Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC14Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC14Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC14Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC14Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC14Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC15Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC15Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC15Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC15Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC15Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC15Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC15Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC15Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC16Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC16Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC16Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC16Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC16Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC16Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC16Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC16Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC17Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC17Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC17Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC17Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC17Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC17Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC17Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC17Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC18Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC18Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC18Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC18Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC18Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC18Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC18Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC18Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC19Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC19Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC19Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC19Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC19Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC19Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC19Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC19Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC20Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC20Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC20Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC20Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC20Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC20Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC20Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC20Df"].Value = "0";

                report.Dictionary.Variables["RespTecPC21Av"].Value = "0";
                report.Dictionary.Variables["RespTecPC21Af"].Value = "0";
                report.Dictionary.Variables["RespTecPC21Bv"].Value = "0";
                report.Dictionary.Variables["RespTecPC21Bf"].Value = "0";
                report.Dictionary.Variables["RespTecPC21Cv"].Value = "0";
                report.Dictionary.Variables["RespTecPC21Cf"].Value = "0";
                report.Dictionary.Variables["RespTecPC21Dv"].Value = "0";
                report.Dictionary.Variables["RespTecPC21Df"].Value = "0";

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }

            report.Compile();
            swvReporte.Report = report;

        }

        private void CargarLaboral2()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();

            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);

            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();


            report.Load(Server.MapPath("~/Assets/reports/IDP/Laboral2.mrt"));
            report.Dictionary.Databases.Clear();

            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

            if (vRespuestas.Count > 0)
            {
                foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in vRespuestas)
                {
                    switch (resp.CL_PREGUNTA)
                    {
                        case "LABORAL2-A-0001": report.Dictionary.Variables["Resp1A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0002": report.Dictionary.Variables["Resp1B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0003": report.Dictionary.Variables["Resp1C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0004": report.Dictionary.Variables["Resp1D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0005": report.Dictionary.Variables["Resp2A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0006": report.Dictionary.Variables["Resp2B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0007": report.Dictionary.Variables["Resp2C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0008": report.Dictionary.Variables["Resp2D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0009": report.Dictionary.Variables["Resp3A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0010": report.Dictionary.Variables["Resp3B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0011": report.Dictionary.Variables["Resp3C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0012": report.Dictionary.Variables["Resp3D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0013": report.Dictionary.Variables["Resp4A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0014": report.Dictionary.Variables["Resp4B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0015": report.Dictionary.Variables["Resp4C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0016": report.Dictionary.Variables["Resp4D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0017": report.Dictionary.Variables["Resp5A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0018": report.Dictionary.Variables["Resp5B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0019": report.Dictionary.Variables["Resp5C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0020": report.Dictionary.Variables["Resp5D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0021": report.Dictionary.Variables["Resp6A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0022": report.Dictionary.Variables["Resp6B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0023": report.Dictionary.Variables["Resp6C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0024": report.Dictionary.Variables["Resp6D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0025": report.Dictionary.Variables["Resp7A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0026": report.Dictionary.Variables["Resp7B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0027": report.Dictionary.Variables["Resp7C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0028": report.Dictionary.Variables["Resp7D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0029": report.Dictionary.Variables["Resp8A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0030": report.Dictionary.Variables["Resp8B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0031": report.Dictionary.Variables["Resp8C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0032": report.Dictionary.Variables["Resp8D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0033": report.Dictionary.Variables["Resp9A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0034": report.Dictionary.Variables["Resp9B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0035": report.Dictionary.Variables["Resp9C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0036": report.Dictionary.Variables["Resp9D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0037": report.Dictionary.Variables["Resp10A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0038": report.Dictionary.Variables["Resp10B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0039": report.Dictionary.Variables["Resp10C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0040": report.Dictionary.Variables["Resp10D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0041": report.Dictionary.Variables["Resp11A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0042": report.Dictionary.Variables["Resp11B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0043": report.Dictionary.Variables["Resp11C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0044": report.Dictionary.Variables["Resp11D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0045": report.Dictionary.Variables["Resp12A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0046": report.Dictionary.Variables["Resp12B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0047": report.Dictionary.Variables["Resp12C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0048": report.Dictionary.Variables["Resp12D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0049": report.Dictionary.Variables["Resp13A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0050": report.Dictionary.Variables["Resp13B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0051": report.Dictionary.Variables["Resp13C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0052": report.Dictionary.Variables["Resp13D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0053": report.Dictionary.Variables["Resp14A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0054": report.Dictionary.Variables["Resp14B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0055": report.Dictionary.Variables["Resp14C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0056": report.Dictionary.Variables["Resp14D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0057": report.Dictionary.Variables["Resp15A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0058": report.Dictionary.Variables["Resp15B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0059": report.Dictionary.Variables["Resp15C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0060": report.Dictionary.Variables["Resp15D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0061": report.Dictionary.Variables["Resp16A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0062": report.Dictionary.Variables["Resp16B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0063": report.Dictionary.Variables["Resp16C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0064": report.Dictionary.Variables["Resp16D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0065": report.Dictionary.Variables["Resp17A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0066": report.Dictionary.Variables["Resp17B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0067": report.Dictionary.Variables["Resp17C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0068": report.Dictionary.Variables["Resp17D"].Value = resp.NB_RESPUESTA; break;

                        case "LABORAL2-A-0069": report.Dictionary.Variables["Resp18A"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0070": report.Dictionary.Variables["Resp18B"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0071": report.Dictionary.Variables["Resp18C"].Value = resp.NB_RESPUESTA; break;
                        case "LABORAL2-A-0072": report.Dictionary.Variables["Resp18D"].Value = resp.NB_RESPUESTA; break;
                    }
                }

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }
            else
            {
                report.Dictionary.Variables["Resp1A"].Value = "";
                report.Dictionary.Variables["Resp1B"].Value = "";
                report.Dictionary.Variables["Resp1C"].Value = "";
                report.Dictionary.Variables["Resp1D"].Value = "";

                report.Dictionary.Variables["Resp2A"].Value = "";
                report.Dictionary.Variables["Resp2B"].Value = "";
                report.Dictionary.Variables["Resp2C"].Value = "";
                report.Dictionary.Variables["Resp2D"].Value = "";

                report.Dictionary.Variables["Resp3A"].Value = "";
                report.Dictionary.Variables["Resp3B"].Value = "";
                report.Dictionary.Variables["Resp3C"].Value = "";
                report.Dictionary.Variables["Resp3D"].Value = "";

                report.Dictionary.Variables["Resp4A"].Value = "";
                report.Dictionary.Variables["Resp4B"].Value = "";
                report.Dictionary.Variables["Resp4C"].Value = "";
                report.Dictionary.Variables["Resp4D"].Value = "";

                report.Dictionary.Variables["Resp5A"].Value = "";
                report.Dictionary.Variables["Resp5B"].Value = "";
                report.Dictionary.Variables["Resp5C"].Value = "";
                report.Dictionary.Variables["Resp5D"].Value = "";

                report.Dictionary.Variables["Resp6A"].Value = "";
                report.Dictionary.Variables["Resp6B"].Value = "";
                report.Dictionary.Variables["Resp6C"].Value = "";
                report.Dictionary.Variables["Resp6D"].Value = "";

                report.Dictionary.Variables["Resp7A"].Value = "";
                report.Dictionary.Variables["Resp7B"].Value = "";
                report.Dictionary.Variables["Resp7C"].Value = "";
                report.Dictionary.Variables["Resp7D"].Value = "";

                report.Dictionary.Variables["Resp8A"].Value = "";
                report.Dictionary.Variables["Resp8B"].Value = "";
                report.Dictionary.Variables["Resp8C"].Value = "";
                report.Dictionary.Variables["Resp8D"].Value = "";

                report.Dictionary.Variables["Resp9A"].Value = "";
                report.Dictionary.Variables["Resp9B"].Value = "";
                report.Dictionary.Variables["Resp9C"].Value = "";
                report.Dictionary.Variables["Resp9D"].Value = "";

                report.Dictionary.Variables["Resp10A"].Value = "";
                report.Dictionary.Variables["Resp10B"].Value = "";
                report.Dictionary.Variables["Resp10C"].Value = "";
                report.Dictionary.Variables["Resp10D"].Value = "";

                report.Dictionary.Variables["Resp11A"].Value = "";
                report.Dictionary.Variables["Resp11B"].Value = "";
                report.Dictionary.Variables["Resp11C"].Value = "";
                report.Dictionary.Variables["Resp11D"].Value = "";

                report.Dictionary.Variables["Resp12A"].Value = "";
                report.Dictionary.Variables["Resp12B"].Value = "";
                report.Dictionary.Variables["Resp12C"].Value = "";
                report.Dictionary.Variables["Resp12D"].Value = "";

                report.Dictionary.Variables["Resp13A"].Value = "";
                report.Dictionary.Variables["Resp13B"].Value = "";
                report.Dictionary.Variables["Resp13C"].Value = "";
                report.Dictionary.Variables["Resp13D"].Value = "";

                report.Dictionary.Variables["Resp14A"].Value = "";
                report.Dictionary.Variables["Resp14B"].Value = "";
                report.Dictionary.Variables["Resp14C"].Value = "";
                report.Dictionary.Variables["Resp14D"].Value = "";

                report.Dictionary.Variables["Resp15A"].Value = "";
                report.Dictionary.Variables["Resp15B"].Value = "";
                report.Dictionary.Variables["Resp15C"].Value = "";
                report.Dictionary.Variables["Resp15D"].Value = "";

                report.Dictionary.Variables["Resp16A"].Value = "";
                report.Dictionary.Variables["Resp16B"].Value = "";
                report.Dictionary.Variables["Resp16C"].Value = "";
                report.Dictionary.Variables["Resp16D"].Value = "";

                report.Dictionary.Variables["Resp17A"].Value = "";
                report.Dictionary.Variables["Resp17B"].Value = "";
                report.Dictionary.Variables["Resp17C"].Value = "";
                report.Dictionary.Variables["Resp17D"].Value = "";

                report.Dictionary.Variables["Resp18A"].Value = "";
                report.Dictionary.Variables["Resp18B"].Value = "";
                report.Dictionary.Variables["Resp18C"].Value = "";
                report.Dictionary.Variables["Resp18D"].Value = "";


                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }

            report.Compile();
            swvReporte.Report = report;
        }

        public void asignarValoresTIVA(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas, StiReport pReport, SPE_OBTIENE_K_PRUEBA_Result vPrueba)
        {
            if (respuestas.Count > 0)
            {

                foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
                {
                    switch (resp.CL_PREGUNTA)
                    {
                        case "TIVA-A-0001": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp1A", "Resp1B", "Resp1C", "Resp1D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0002": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp2A", "Resp2B", "Resp2C", "Resp2D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0003": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp3A", "Resp3B", "Resp3C", "Resp3D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0004": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp4A", "Resp4B", "Resp4C", "Resp4D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0005": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp5A", "Resp5B", "Resp5C", "Resp5D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0006": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp6A", "Resp6B", "Resp6C", "Resp6D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0007": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp7A", "Resp7B", "Resp7C", "Resp7D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0008": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp8A", "Resp8B", "Resp8C", "Resp8D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0009": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp9A", "Resp9B", "Resp9C", "Resp9D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0010": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp10A", "Resp10B", "Resp10C", "Resp10D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0011": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp11A", "Resp11B", "Resp11C", "Resp11D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0012": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp12A", "Resp12B", "Resp12C", "Resp12D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0013": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp13A", "Resp13B", "Resp13C", "Resp13D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0014": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp14A", "Resp14B", "Resp14C", "Resp14D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0015": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp15A", "Resp15B", "Resp15C", "Resp15D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0016": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp16A", "Resp16B", "Resp16C", "Resp16D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0017": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp17A", "Resp17B", "Resp17C", "Resp17D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0018": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp18A", "Resp18B", "Resp18C", "Resp18D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0019": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp19A", "Resp19B", "Resp19C", "Resp19D", resp.NB_RESPUESTA, pReport); } break;
                        case "TIVA-A-0020": if (resp.NB_RESPUESTA != null) { SeleccionarRespuesta("Resp20A", "Resp20B", "Resp20C", "Resp20D", resp.NB_RESPUESTA, pReport); } break;
                    }
                }
                pReport.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }
            else
            {
                pReport.Dictionary.Variables["Resp1A"].Value = "0";
                pReport.Dictionary.Variables["Resp1B"].Value = "0";
                pReport.Dictionary.Variables["Resp1C"].Value = "0";
                pReport.Dictionary.Variables["Resp1D"].Value = "0";
                pReport.Dictionary.Variables["Resp2A"].Value = "0";
                pReport.Dictionary.Variables["Resp2B"].Value = "0";
                pReport.Dictionary.Variables["Resp2C"].Value = "0";
                pReport.Dictionary.Variables["Resp2D"].Value = "0";
                pReport.Dictionary.Variables["Resp3A"].Value = "0";
                pReport.Dictionary.Variables["Resp3B"].Value = "0";
                pReport.Dictionary.Variables["Resp3C"].Value = "0";
                pReport.Dictionary.Variables["Resp3D"].Value = "0";
                pReport.Dictionary.Variables["Resp4A"].Value = "0";
                pReport.Dictionary.Variables["Resp4B"].Value = "0";
                pReport.Dictionary.Variables["Resp4C"].Value = "0";
                pReport.Dictionary.Variables["Resp4D"].Value = "0";
                pReport.Dictionary.Variables["Resp5A"].Value = "0";
                pReport.Dictionary.Variables["Resp5B"].Value = "0";
                pReport.Dictionary.Variables["Resp5C"].Value = "0";
                pReport.Dictionary.Variables["Resp5D"].Value = "0";
                pReport.Dictionary.Variables["Resp6A"].Value = "0";
                pReport.Dictionary.Variables["Resp6B"].Value = "0";
                pReport.Dictionary.Variables["Resp6C"].Value = "0";
                pReport.Dictionary.Variables["Resp6D"].Value = "0";
                pReport.Dictionary.Variables["Resp7A"].Value = "0";
                pReport.Dictionary.Variables["Resp7B"].Value = "0";
                pReport.Dictionary.Variables["Resp7C"].Value = "0";
                pReport.Dictionary.Variables["Resp7D"].Value = "0";
                pReport.Dictionary.Variables["Resp8A"].Value = "0";
                pReport.Dictionary.Variables["Resp8B"].Value = "0";
                pReport.Dictionary.Variables["Resp8C"].Value = "0";
                pReport.Dictionary.Variables["Resp8D"].Value = "0";
                pReport.Dictionary.Variables["Resp9A"].Value = "0";
                pReport.Dictionary.Variables["Resp9B"].Value = "0";
                pReport.Dictionary.Variables["Resp9C"].Value = "0";
                pReport.Dictionary.Variables["Resp9D"].Value = "0";
                pReport.Dictionary.Variables["Resp10A"].Value = "0";
                pReport.Dictionary.Variables["Resp10B"].Value = "0";
                pReport.Dictionary.Variables["Resp10C"].Value = "0";
                pReport.Dictionary.Variables["Resp10D"].Value = "0";
                pReport.Dictionary.Variables["Resp11A"].Value = "0";
                pReport.Dictionary.Variables["Resp11B"].Value = "0";
                pReport.Dictionary.Variables["Resp11C"].Value = "0";
                pReport.Dictionary.Variables["Resp11D"].Value = "0";
                pReport.Dictionary.Variables["Resp12A"].Value = "0";
                pReport.Dictionary.Variables["Resp12B"].Value = "0";
                pReport.Dictionary.Variables["Resp12C"].Value = "0";
                pReport.Dictionary.Variables["Resp12D"].Value = "0";
                pReport.Dictionary.Variables["Resp13A"].Value = "0";
                pReport.Dictionary.Variables["Resp13B"].Value = "0";
                pReport.Dictionary.Variables["Resp13C"].Value = "0";
                pReport.Dictionary.Variables["Resp13D"].Value = "0";
                pReport.Dictionary.Variables["Resp14A"].Value = "0";
                pReport.Dictionary.Variables["Resp14B"].Value = "0";
                pReport.Dictionary.Variables["Resp14C"].Value = "0";
                pReport.Dictionary.Variables["Resp14D"].Value = "0";
                pReport.Dictionary.Variables["Resp15A"].Value = "0";
                pReport.Dictionary.Variables["Resp15B"].Value = "0";
                pReport.Dictionary.Variables["Resp15C"].Value = "0";
                pReport.Dictionary.Variables["Resp15D"].Value = "0";
                pReport.Dictionary.Variables["Resp16A"].Value = "0";
                pReport.Dictionary.Variables["Resp16B"].Value = "0";
                pReport.Dictionary.Variables["Resp16C"].Value = "0";
                pReport.Dictionary.Variables["Resp16D"].Value = "0";
                pReport.Dictionary.Variables["Resp17A"].Value = "0";
                pReport.Dictionary.Variables["Resp17B"].Value = "0";
                pReport.Dictionary.Variables["Resp17C"].Value = "0";
                pReport.Dictionary.Variables["Resp17D"].Value = "0";
                pReport.Dictionary.Variables["Resp18A"].Value = "0";
                pReport.Dictionary.Variables["Resp18B"].Value = "0";
                pReport.Dictionary.Variables["Resp18C"].Value = "0";
                pReport.Dictionary.Variables["Resp18D"].Value = "0";
                pReport.Dictionary.Variables["Resp19A"].Value = "0";
                pReport.Dictionary.Variables["Resp19B"].Value = "0";
                pReport.Dictionary.Variables["Resp19C"].Value = "0";
                pReport.Dictionary.Variables["Resp19D"].Value = "0";
                pReport.Dictionary.Variables["Resp20A"].Value = "0";
                pReport.Dictionary.Variables["Resp20B"].Value = "0";
                pReport.Dictionary.Variables["Resp20C"].Value = "0";
                pReport.Dictionary.Variables["Resp20D"].Value = "0";

                pReport.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }

            pReport.Compile();
            swvReporte.Report = pReport;
        }

        public void AsignarValoresManualTIVA(List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> respuestas, StiReport pReport, SPE_OBTIENE_K_PRUEBA_Result vPrueba)
        {
            if (respuestas.Count > 0)
            {

                foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in respuestas)
                {
                    switch (resp.CL_PREGUNTA)
                    {
                        case "TIVA_RES_1": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp1A", "Resp1B", "Resp1C", "Resp1D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_2": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp2A", "Resp2B", "Resp2C", "Resp2D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_3": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp3A", "Resp3B", "Resp3C", "Resp3D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_4": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp4A", "Resp4B", "Resp4C", "Resp4D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_5": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp5A", "Resp5B", "Resp5C", "Resp5D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_6": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp6A", "Resp6B", "Resp6C", "Resp6D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_7": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp7A", "Resp7B", "Resp7C", "Resp7D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_8": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp8A", "Resp8B", "Resp8C", "Resp8D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_9": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp9A", "Resp9B", "Resp9C", "Resp9D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_10": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp10A", "Resp10B", "Resp10C", "Resp10D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_11": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp11A", "Resp11B", "Resp11C", "Resp11D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_12": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp12A", "Resp12B", "Resp12C", "Resp12D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_13": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp13A", "Resp13B", "Resp13C", "Resp13D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_14": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp14A", "Resp14B", "Resp14C", "Resp14D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_15": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp15A", "Resp15B", "Resp15C", "Resp15D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_16": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp16A", "Resp16B", "Resp16C", "Resp16D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_17": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp17A", "Resp17B", "Resp17C", "Resp17D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_18": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp18A", "Resp18B", "Resp18C", "Resp18D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_19": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp19A", "Resp19B", "Resp19C", "Resp19D", resp.NB_RESULTADO.ToString(), pReport); } break;
                        case "TIVA_RES_20": if (resp.NB_RESULTADO != null) { SeleccionarRespuesta("Resp20A", "Resp20B", "Resp20C", "Resp20D", resp.NB_RESULTADO.ToString(), pReport); } break;
                    }
                }
                pReport.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }
            else
            {
                pReport.Dictionary.Variables["Resp1A"].Value = "0";
                pReport.Dictionary.Variables["Resp1B"].Value = "0";
                pReport.Dictionary.Variables["Resp1C"].Value = "0";
                pReport.Dictionary.Variables["Resp1D"].Value = "0";
                pReport.Dictionary.Variables["Resp2A"].Value = "0";
                pReport.Dictionary.Variables["Resp2B"].Value = "0";
                pReport.Dictionary.Variables["Resp2C"].Value = "0";
                pReport.Dictionary.Variables["Resp2D"].Value = "0";
                pReport.Dictionary.Variables["Resp3A"].Value = "0";
                pReport.Dictionary.Variables["Resp3B"].Value = "0";
                pReport.Dictionary.Variables["Resp3C"].Value = "0";
                pReport.Dictionary.Variables["Resp3D"].Value = "0";
                pReport.Dictionary.Variables["Resp4A"].Value = "0";
                pReport.Dictionary.Variables["Resp4B"].Value = "0";
                pReport.Dictionary.Variables["Resp4C"].Value = "0";
                pReport.Dictionary.Variables["Resp4D"].Value = "0";
                pReport.Dictionary.Variables["Resp5A"].Value = "0";
                pReport.Dictionary.Variables["Resp5B"].Value = "0";
                pReport.Dictionary.Variables["Resp5C"].Value = "0";
                pReport.Dictionary.Variables["Resp5D"].Value = "0";
                pReport.Dictionary.Variables["Resp6A"].Value = "0";
                pReport.Dictionary.Variables["Resp6B"].Value = "0";
                pReport.Dictionary.Variables["Resp6C"].Value = "0";
                pReport.Dictionary.Variables["Resp6D"].Value = "0";
                pReport.Dictionary.Variables["Resp7A"].Value = "0";
                pReport.Dictionary.Variables["Resp7B"].Value = "0";
                pReport.Dictionary.Variables["Resp7C"].Value = "0";
                pReport.Dictionary.Variables["Resp7D"].Value = "0";
                pReport.Dictionary.Variables["Resp8A"].Value = "0";
                pReport.Dictionary.Variables["Resp8B"].Value = "0";
                pReport.Dictionary.Variables["Resp8C"].Value = "0";
                pReport.Dictionary.Variables["Resp8D"].Value = "0";
                pReport.Dictionary.Variables["Resp9A"].Value = "0";
                pReport.Dictionary.Variables["Resp9B"].Value = "0";
                pReport.Dictionary.Variables["Resp9C"].Value = "0";
                pReport.Dictionary.Variables["Resp9D"].Value = "0";
                pReport.Dictionary.Variables["Resp10A"].Value = "0";
                pReport.Dictionary.Variables["Resp10B"].Value = "0";
                pReport.Dictionary.Variables["Resp10C"].Value = "0";
                pReport.Dictionary.Variables["Resp10D"].Value = "0";
                pReport.Dictionary.Variables["Resp11A"].Value = "0";
                pReport.Dictionary.Variables["Resp11B"].Value = "0";
                pReport.Dictionary.Variables["Resp11C"].Value = "0";
                pReport.Dictionary.Variables["Resp11D"].Value = "0";
                pReport.Dictionary.Variables["Resp12A"].Value = "0";
                pReport.Dictionary.Variables["Resp12B"].Value = "0";
                pReport.Dictionary.Variables["Resp12C"].Value = "0";
                pReport.Dictionary.Variables["Resp12D"].Value = "0";
                pReport.Dictionary.Variables["Resp13A"].Value = "0";
                pReport.Dictionary.Variables["Resp13B"].Value = "0";
                pReport.Dictionary.Variables["Resp13C"].Value = "0";
                pReport.Dictionary.Variables["Resp13D"].Value = "0";
                pReport.Dictionary.Variables["Resp14A"].Value = "0";
                pReport.Dictionary.Variables["Resp14B"].Value = "0";
                pReport.Dictionary.Variables["Resp14C"].Value = "0";
                pReport.Dictionary.Variables["Resp14D"].Value = "0";
                pReport.Dictionary.Variables["Resp15A"].Value = "0";
                pReport.Dictionary.Variables["Resp15B"].Value = "0";
                pReport.Dictionary.Variables["Resp15C"].Value = "0";
                pReport.Dictionary.Variables["Resp15D"].Value = "0";
                pReport.Dictionary.Variables["Resp16A"].Value = "0";
                pReport.Dictionary.Variables["Resp16B"].Value = "0";
                pReport.Dictionary.Variables["Resp16C"].Value = "0";
                pReport.Dictionary.Variables["Resp16D"].Value = "0";
                pReport.Dictionary.Variables["Resp17A"].Value = "0";
                pReport.Dictionary.Variables["Resp17B"].Value = "0";
                pReport.Dictionary.Variables["Resp17C"].Value = "0";
                pReport.Dictionary.Variables["Resp17D"].Value = "0";
                pReport.Dictionary.Variables["Resp18A"].Value = "0";
                pReport.Dictionary.Variables["Resp18B"].Value = "0";
                pReport.Dictionary.Variables["Resp18C"].Value = "0";
                pReport.Dictionary.Variables["Resp18D"].Value = "0";
                pReport.Dictionary.Variables["Resp19A"].Value = "0";
                pReport.Dictionary.Variables["Resp19B"].Value = "0";
                pReport.Dictionary.Variables["Resp19C"].Value = "0";
                pReport.Dictionary.Variables["Resp19D"].Value = "0";
                pReport.Dictionary.Variables["Resp20A"].Value = "0";
                pReport.Dictionary.Variables["Resp20B"].Value = "0";
                pReport.Dictionary.Variables["Resp20C"].Value = "0";
                pReport.Dictionary.Variables["Resp20D"].Value = "0";

                pReport.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }

            pReport.Compile();
            swvReporte.Report = pReport;
        }

        public void SeleccionarRespuesta(string a, string b, string c, string d, string pAnswer, StiReport pReport)
        {
            if (pAnswer != null)
            {
                switch (pAnswer.ToUpper().Substring(0, 1))
                {
                    case "A":
                        pReport.Dictionary.Variables[a].Value = "1";
                        pReport.Dictionary.Variables[b].Value = "0";
                        pReport.Dictionary.Variables[c].Value = "0";
                        pReport.Dictionary.Variables[d].Value = "0";
                        break;
                    case "B":
                        pReport.Dictionary.Variables[a].Value = "0";
                        pReport.Dictionary.Variables[b].Value = "1";
                        pReport.Dictionary.Variables[c].Value = "0";
                        pReport.Dictionary.Variables[d].Value = "0";
                        break;
                    case "C":
                        pReport.Dictionary.Variables[a].Value = "0";
                        pReport.Dictionary.Variables[b].Value = "0";
                        pReport.Dictionary.Variables[c].Value = "1";
                        pReport.Dictionary.Variables[d].Value = "0";
                        break;
                    case "D":
                        pReport.Dictionary.Variables[a].Value = "0";
                        pReport.Dictionary.Variables[b].Value = "0";
                        pReport.Dictionary.Variables[c].Value = "0";
                        pReport.Dictionary.Variables[d].Value = "1";
                        break;
                    case "1":
                        pReport.Dictionary.Variables[a].Value = "1";
                        pReport.Dictionary.Variables[b].Value = "0";
                        pReport.Dictionary.Variables[c].Value = "0";
                        pReport.Dictionary.Variables[d].Value = "0";
                        break;
                    case "2":
                        pReport.Dictionary.Variables[a].Value = "0";
                        pReport.Dictionary.Variables[b].Value = "1";
                        pReport.Dictionary.Variables[c].Value = "0";
                        pReport.Dictionary.Variables[d].Value = "0";
                        break;
                    case "3":
                        pReport.Dictionary.Variables[a].Value = "0";
                        pReport.Dictionary.Variables[b].Value = "0";
                        pReport.Dictionary.Variables[c].Value = "1";
                        pReport.Dictionary.Variables[d].Value = "0";
                        break;
                    case "4":
                        pReport.Dictionary.Variables[a].Value = "0";
                        pReport.Dictionary.Variables[b].Value = "0";
                        pReport.Dictionary.Variables[c].Value = "0";
                        pReport.Dictionary.Variables[d].Value = "1";
                        break;
                    default: break;
                }
            }
            else
            {
                pReport.Dictionary.Variables[a].Value = "0";
                pReport.Dictionary.Variables[b].Value = "0";
                pReport.Dictionary.Variables[c].Value = "0";
                pReport.Dictionary.Variables[d].Value = "0";
            }
        }

        private void CargarTIVA()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();

            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);

            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();


            report.Load(Server.MapPath("~/Assets/reports/IDP/Tiva.mrt"));
            report.Dictionary.Databases.Clear();

            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

            if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
                AsignarValoresManualTIVA(vRespuestas, report, vPrueba);
            else
                asignarValoresTIVA(vRespuestas, report, vPrueba);
        }

        private void CargarAptitud()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();

            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);

            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();

            CuestionarioPreguntaNegocio nCuestionarioPregunta = new CuestionarioPreguntaNegocio();
            var vPruebaSeccion10 = nCuestionarioPregunta.Obtener_K_CUESTIONARIO_PREGUNTA_PRUEBA(ID_PRUEBA: vIdPrueba, CL_TOKEN: vClToken).OrderByDescending(t => t.ID_PREGUNTA).Take(22);


            report.Load(Server.MapPath("~/Assets/reports/IDP/Aptitud1.mrt"));
            report.Dictionary.Databases.Clear();

            //System.Configuration.Configuration rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
            //pathValue = rootWebConfig1.ConnectionStrings.ConnectionStrings[0].ConnectionString;

            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));


            if (vRespuestas.Count > 0)
            {
                // SECCION1
                AsignarValorLaboral1(report, "S1R1", vRespuestas, "APTITUD1-A-0001");
                AsignarValorLaboral1(report, "S1R2", vRespuestas, "APTITUD1-A-0002");
                AsignarValorLaboral1(report, "S1R3", vRespuestas, "APTITUD1-A-0003");
                AsignarValorLaboral1(report, "S1R4", vRespuestas, "APTITUD1-A-0004");
                AsignarValorLaboral1(report, "S1R5", vRespuestas, "APTITUD1-A-0005");
                AsignarValorLaboral1(report, "S1R6", vRespuestas, "APTITUD1-A-0006");
                AsignarValorLaboral1(report, "S1R7", vRespuestas, "APTITUD1-A-0007");
                AsignarValorLaboral1(report, "S1R8", vRespuestas, "APTITUD1-A-0008");
                AsignarValorLaboral1(report, "S1R9", vRespuestas, "APTITUD1-A-0009");
                AsignarValorLaboral1(report, "S1R10", vRespuestas, "APTITUD1-A-0010");
                AsignarValorLaboral1(report, "S1R11", vRespuestas, "APTITUD1-A-0011");
                AsignarValorLaboral1(report, "S1R12", vRespuestas, "APTITUD1-A-0012");
                AsignarValorLaboral1(report, "S1R13", vRespuestas, "APTITUD1-A-0013");
                AsignarValorLaboral1(report, "S1R14", vRespuestas, "APTITUD1-A-0014");
                AsignarValorLaboral1(report, "S1R15", vRespuestas, "APTITUD1-A-0015");
                AsignarValorLaboral1(report, "S1R16", vRespuestas, "APTITUD1-A-0016");

                //SECCION 2
                AsignarValorLaboral1(report, "S2R1", vRespuestas, "APTITUD1-B-0001");
                AsignarValorLaboral1(report, "S2R2", vRespuestas, "APTITUD1-B-0002");
                AsignarValorLaboral1(report, "S2R3", vRespuestas, "APTITUD1-B-0003");
                AsignarValorLaboral1(report, "S2R4", vRespuestas, "APTITUD1-B-0004");
                AsignarValorLaboral1(report, "S2R5", vRespuestas, "APTITUD1-B-0005");
                AsignarValorLaboral1(report, "S2R6", vRespuestas, "APTITUD1-B-0006");
                AsignarValorLaboral1(report, "S2R7", vRespuestas, "APTITUD1-B-0007");
                AsignarValorLaboral1(report, "S2R8", vRespuestas, "APTITUD1-B-0008");
                AsignarValorLaboral1(report, "S2R9", vRespuestas, "APTITUD1-B-0009");
                AsignarValorLaboral1(report, "S2R10", vRespuestas, "APTITUD1-B-0010");
                AsignarValorLaboral1(report, "S2R11", vRespuestas, "APTITUD1-B-0011");

                //SECCION 3
                AsignarValorLaboral1(report, "S3R1", vRespuestas, "APTITUD1-C-0001");
                AsignarValorLaboral1(report, "S3R2", vRespuestas, "APTITUD1-C-0002");
                AsignarValorLaboral1(report, "S3R3", vRespuestas, "APTITUD1-C-0003");
                AsignarValorLaboral1(report, "S3R4", vRespuestas, "APTITUD1-C-0004");
                AsignarValorLaboral1(report, "S3R5", vRespuestas, "APTITUD1-C-0005");
                AsignarValorLaboral1(report, "S3R6", vRespuestas, "APTITUD1-C-0006");
                AsignarValorLaboral1(report, "S3R7", vRespuestas, "APTITUD1-C-0007");
                AsignarValorLaboral1(report, "S3R8", vRespuestas, "APTITUD1-C-0008");
                AsignarValorLaboral1(report, "S3R9", vRespuestas, "APTITUD1-C-0009");
                AsignarValorLaboral1(report, "S3R10", vRespuestas, "APTITUD1-C-0010");
                AsignarValorLaboral1(report, "S3R11", vRespuestas, "APTITUD1-C-0011");
                AsignarValorLaboral1(report, "S3R12", vRespuestas, "APTITUD1-C-0012");
                AsignarValorLaboral1(report, "S3R13", vRespuestas, "APTITUD1-C-0013");
                AsignarValorLaboral1(report, "S3R14", vRespuestas, "APTITUD1-C-0014");
                AsignarValorLaboral1(report, "S3R15", vRespuestas, "APTITUD1-C-0015");
                AsignarValorLaboral1(report, "S3R16", vRespuestas, "APTITUD1-C-0016");
                AsignarValorLaboral1(report, "S3R17", vRespuestas, "APTITUD1-C-0017");
                AsignarValorLaboral1(report, "S3R18", vRespuestas, "APTITUC1-C-0018");
                AsignarValorLaboral1(report, "S3R19", vRespuestas, "APTITUD1-C-0019");
                AsignarValorLaboral1(report, "S3R20", vRespuestas, "APTITUD1-C-0020");
                AsignarValorLaboral1(report, "S3R21", vRespuestas, "APTITUD1-C-0021");
                AsignarValorLaboral1(report, "S3R22", vRespuestas, "APTITUD1-C-0022");
                AsignarValorLaboral1(report, "S3R23", vRespuestas, "APTITUD1-C-0023");
                AsignarValorLaboral1(report, "S3R24", vRespuestas, "APTITUD1-C-0024");
                AsignarValorLaboral1(report, "S3R25", vRespuestas, "APTITUD1-C-0025");
                AsignarValorLaboral1(report, "S3R26", vRespuestas, "APTITUD1-C-0026");
                AsignarValorLaboral1(report, "S3R27", vRespuestas, "APTITUD1-C-0027");
                AsignarValorLaboral1(report, "S3R28", vRespuestas, "APTITUD1-C-0028");
                AsignarValorLaboral1(report, "S3R29", vRespuestas, "APTITUD1-C-0029");
                AsignarValorLaboral1(report, "S3R30", vRespuestas, "APTITUD1-C-0030");

                //SECCION4
                AsignarValorLaboral1D(report, "S4R1", "S4R2", vRespuestas, "APTITUD1-D-0001");
                AsignarValorLaboral1D(report, "S4R3", "S4R4", vRespuestas, "APTITUD1-D-0002");
                AsignarValorLaboral1D(report, "S4R5", "S4R6", vRespuestas, "APTITUD1-D-0003");
                AsignarValorLaboral1D(report, "S4R7", "S4R8", vRespuestas, "APTITUD1-D-0004");
                AsignarValorLaboral1D(report, "S4R9", "S4R10", vRespuestas, "APTITUD1-D-0005");
                AsignarValorLaboral1D(report, "S4R11", "S4R12", vRespuestas, "APTITUD1-D-0006");
                AsignarValorLaboral1D(report, "S4R13", "S4R14", vRespuestas, "APTITUD1-D-0007");
                AsignarValorLaboral1D(report, "S4R15", "S4R16", vRespuestas, "APTITUD1-D-0008");
                AsignarValorLaboral1D(report, "S4R17", "S4R18", vRespuestas, "APTITUD1-D-0009");
                AsignarValorLaboral1D(report, "S4R19", "S4R20", vRespuestas, "APTITUD1-D-0010");
                AsignarValorLaboral1D(report, "S4R21", "S4R22", vRespuestas, "APTITUD1-D-0011");
                AsignarValorLaboral1D(report, "S4R23", "S4R24", vRespuestas, "APTITUD1-D-0012");
                AsignarValorLaboral1D(report, "S4R25", "S4R26", vRespuestas, "APTITUD1-D-0013");
                AsignarValorLaboral1D(report, "S4R27", "S4R28", vRespuestas, "APTITUD1-D-0014");
                AsignarValorLaboral1D(report, "S4R29", "S4R30", vRespuestas, "APTITUD1-D-0015");
                AsignarValorLaboral1D(report, "S4R31", "S4R32", vRespuestas, "APTITUD1-D-0016");
                AsignarValorLaboral1D(report, "S4R33", "S4R34", vRespuestas, "APTITUD1-D-0017");
                AsignarValorLaboral1D(report, "S4R35", "S4R36", vRespuestas, "APTITUD1-D-0018");

                //SECCION5
                AsignarValorLaboral1(report, "S5R1", vRespuestas, "APTITUD1-E-0001");
                AsignarValorLaboral1(report, "S5R2", vRespuestas, "APTITUD1-E-0002");
                AsignarValorLaboral1(report, "S5R3", vRespuestas, "APTITUD1-E-0003");
                AsignarValorLaboral1(report, "S5R4", vRespuestas, "APTITUD1-E-0004");
                AsignarValorLaboral1(report, "S5R5", vRespuestas, "APTITUD1-E-0005");
                AsignarValorLaboral1(report, "S5R6", vRespuestas, "APTITUD1-E-0006");
                AsignarValorLaboral1(report, "S5R7", vRespuestas, "APTITUD1-E-0007");
                AsignarValorLaboral1(report, "S5R8", vRespuestas, "APTITUD1-E-0008");
                AsignarValorLaboral1(report, "S5R9", vRespuestas, "APTITUD1-E-0009");
                AsignarValorLaboral1(report, "S5R10", vRespuestas, "APTITUD1-E-0010");
                AsignarValorLaboral1(report, "S5R11", vRespuestas, "APTITUD1-E-0011");
                AsignarValorLaboral1(report, "S5R12", vRespuestas, "APTITUD1-E-0012");

                //SECCION6
                AsignarValorLaboral1(report, "S6R1", vRespuestas, "APTITUD1-F-0001");
                AsignarValorLaboral1(report, "S6R2", vRespuestas, "APTITUD1-F-0002");
                AsignarValorLaboral1(report, "S6R3", vRespuestas, "APTITUD1-F-0003");
                AsignarValorLaboral1(report, "S6R4", vRespuestas, "APTITUD1-F-0004");
                AsignarValorLaboral1(report, "S6R5", vRespuestas, "APTITUD1-F-0005");
                AsignarValorLaboral1(report, "S6R6", vRespuestas, "APTITUD1-F-0006");
                AsignarValorLaboral1(report, "S6R7", vRespuestas, "APTITUD1-F-0007");
                AsignarValorLaboral1(report, "S6R8", vRespuestas, "APTITUD1-F-0008");
                AsignarValorLaboral1(report, "S6R9", vRespuestas, "APTITUD1-F-0009");
                AsignarValorLaboral1(report, "S6R10", vRespuestas, "APTITUD1-F-0010");
                AsignarValorLaboral1(report, "S6R11", vRespuestas, "APTITUD1-F-0011");
                AsignarValorLaboral1(report, "S6R12", vRespuestas, "APTITUD1-F-0012");
                AsignarValorLaboral1(report, "S6R13", vRespuestas, "APTITUD1-F-0013");
                AsignarValorLaboral1(report, "S6R14", vRespuestas, "APTITUD1-F-0014");
                AsignarValorLaboral1(report, "S6R15", vRespuestas, "APTITUD1-F-0015");
                AsignarValorLaboral1(report, "S6R16", vRespuestas, "APTITUD1-F-0016");
                AsignarValorLaboral1(report, "S6R17", vRespuestas, "APTITUD1-F-0017");
                AsignarValorLaboral1(report, "S6R18", vRespuestas, "APTITUD1-F-0018");
                AsignarValorLaboral1(report, "S6R19", vRespuestas, "APTITUD1-F-0019");
                AsignarValorLaboral1(report, "S6R20", vRespuestas, "APTITUD1-F-0020");

                //SECCION 7
                AsignarValorLaboral1(report, "S7R1", vRespuestas, "APTITUD1-G-0001");
                AsignarValorLaboral1(report, "S7R2", vRespuestas, "APTITUD1-G-0002");
                AsignarValorLaboral1(report, "S7R3", vRespuestas, "APTITUD1-G-0003");
                AsignarValorLaboral1(report, "S7R4", vRespuestas, "APTITUD1-G-0004");
                AsignarValorLaboral1(report, "S7R5", vRespuestas, "APTITUD1-G-0005");
                AsignarValorLaboral1(report, "S7R6", vRespuestas, "APTITUD1-G-0006");
                AsignarValorLaboral1(report, "S7R7", vRespuestas, "APTITUD1-G-0007");
                AsignarValorLaboral1(report, "S7R8", vRespuestas, "APTITUD1-G-0008");
                AsignarValorLaboral1(report, "S7R9", vRespuestas, "APTITUD1-G-0009");
                AsignarValorLaboral1(report, "S7R10", vRespuestas, "APTITUD1-G-0010");
                AsignarValorLaboral1(report, "S7R11", vRespuestas, "APTITUD1-G-0011");
                AsignarValorLaboral1(report, "S7R12", vRespuestas, "APTITUD1-G-0012");
                AsignarValorLaboral1(report, "S7R13", vRespuestas, "APTITUD1-G-0013");
                AsignarValorLaboral1(report, "S7R14", vRespuestas, "APTITUD1-G-0014");
                AsignarValorLaboral1(report, "S7R15", vRespuestas, "APTITUD1-G-0015");
                AsignarValorLaboral1(report, "S7R16", vRespuestas, "APTITUD1-G-0016");
                AsignarValorLaboral1(report, "S7R17", vRespuestas, "APTITUD1-G-0017");
                AsignarValorLaboral1(report, "S7R18", vRespuestas, "APTITUD1-G-0018");
                AsignarValorLaboral1(report, "S7R19", vRespuestas, "APTITUD1-G-0019");
                AsignarValorLaboral1(report, "S7R20", vRespuestas, "APTITUD1-G-0020");

                //SECCION8
                AsignarValorLaboral1(report, "S8R1", vRespuestas, "APTITUD1-H-0001");
                AsignarValorLaboral1(report, "S8R2", vRespuestas, "APTITUD1-H-0002");
                AsignarValorLaboral1(report, "S8R3", vRespuestas, "APTITUD1-H-0003");
                AsignarValorLaboral1(report, "S8R4", vRespuestas, "APTITUD1-H-0004");
                AsignarValorLaboral1(report, "S8R5", vRespuestas, "APTITUD1-H-0005");
                AsignarValorLaboral1(report, "S8R6", vRespuestas, "APTITUD1-H-0006");
                AsignarValorLaboral1(report, "S8R7", vRespuestas, "APTITUD1-H-0007");
                AsignarValorLaboral1(report, "S8R8", vRespuestas, "APTITUD1-H-0008");
                AsignarValorLaboral1(report, "S8R9", vRespuestas, "APTITUD1-H-0009");
                AsignarValorLaboral1(report, "S8R10", vRespuestas, "APTITUD1-H-0010");
                AsignarValorLaboral1(report, "S8R11", vRespuestas, "APTITUD1-H-0011");
                AsignarValorLaboral1(report, "S8R12", vRespuestas, "APTITUD1-H-0012");
                AsignarValorLaboral1(report, "S8R13", vRespuestas, "APTITUD1-H-0013");
                AsignarValorLaboral1(report, "S8R14", vRespuestas, "APTITUD1-H-0014");
                AsignarValorLaboral1(report, "S8R15", vRespuestas, "APTITUD1-H-0015");
                AsignarValorLaboral1(report, "S8R16", vRespuestas, "APTITUD1-H-0016");
                AsignarValorLaboral1(report, "S8R17", vRespuestas, "APTITUD1-H-0017");

                //SECCION9
                AsignarValorLaboral1(report, "S9R1", vRespuestas, "APTITUD1-I-0001");
                AsignarValorLaboral1(report, "S9R2", vRespuestas, "APTITUD1-I-0002");
                AsignarValorLaboral1(report, "S9R3", vRespuestas, "APTITUD1-I-0003");
                AsignarValorLaboral1(report, "S9R4", vRespuestas, "APTITUD1-I-0004");
                AsignarValorLaboral1(report, "S9R5", vRespuestas, "APTITUD1-I-0005");
                AsignarValorLaboral1(report, "S9R6", vRespuestas, "APTITUD1-I-0006");
                AsignarValorLaboral1(report, "S9R7", vRespuestas, "APTITUD1-I-0007");
                AsignarValorLaboral1(report, "S9R8", vRespuestas, "APTITUD1-I-0008");
                AsignarValorLaboral1(report, "S9R9", vRespuestas, "APTITUD1-I-0009");
                AsignarValorLaboral1(report, "S9R10", vRespuestas, "APTITUD1-I-0010");
                AsignarValorLaboral1(report, "S9R11", vRespuestas, "APTITUD1-I-0011");
                AsignarValorLaboral1(report, "S9R12", vRespuestas, "APTITUD1-I-0012");
                AsignarValorLaboral1(report, "S9R13", vRespuestas, "APTITUD1-I-0013");
                AsignarValorLaboral1(report, "S9R14", vRespuestas, "APTITUD1-I-0014");
                AsignarValorLaboral1(report, "S9R15", vRespuestas, "APTITUD1-I-0015");
                AsignarValorLaboral1(report, "S9R16", vRespuestas, "APTITUD1-I-0016");
                AsignarValorLaboral1(report, "S9R17", vRespuestas, "APTITUD1-I-0017");
                AsignarValorLaboral1(report, "S9R18", vRespuestas, "APTITUD1-I-0018");

                //SECCION10
                AsignarValorLaboral1(report, "S10R1", vRespuestas, "APTITUD1-J-0001");
                AsignarValorLaboral1(report, "S10R2", vRespuestas, "APTITUD1-J-0002");
                AsignarValorLaboral1(report, "S10R3", vRespuestas, "APTITUD1-J-0003");
                AsignarValorLaboral1(report, "S10R4", vRespuestas, "APTITUD1-J-0004");
                AsignarValorLaboral1(report, "S10R5", vRespuestas, "APTITUD1-J-0005");
                AsignarValorLaboral1(report, "S10R6", vRespuestas, "APTITUD1-J-0006");
                AsignarValorLaboral1(report, "S10R7", vRespuestas, "APTITUD1-J-0007");
                AsignarValorLaboral1(report, "S10R8", vRespuestas, "APTITUD1-J-0008");
                AsignarValorLaboral1(report, "S10R9", vRespuestas, "APTITUD1-J-0008");
                AsignarValorLaboral1(report, "S10R10", vRespuestas, "APTITUD1-J-0010");
                AsignarValorLaboral1(report, "S10R11", vRespuestas, "APTITUD1-J-0011");
                AsignarValorLaboral1(report, "S10R12", vRespuestas, "APTITUD1-J-0012");
                AsignarValorLaboral1(report, "S10R13", vRespuestas, "APTITUD1-J-0013");
                AsignarValorLaboral1(report, "S10R14", vRespuestas, "APTITUD1-J-0014");
                AsignarValorLaboral1(report, "S10R15", vRespuestas, "APTITUD1-J-0015");
                AsignarValorLaboral1(report, "S10R16", vRespuestas, "APTITUD1-J-0016");
                AsignarValorLaboral1(report, "S10R17", vRespuestas, "APTITUD1-J-0017");
                AsignarValorLaboral1(report, "S10R18", vRespuestas, "APTITUD1-J-0018");
                AsignarValorLaboral1(report, "S10R19", vRespuestas, "APTITUD1-J-0019");
                AsignarValorLaboral1(report, "S10R20", vRespuestas, "APTITUD1-J-0020");
                AsignarValorLaboral1(report, "S10R21", vRespuestas, "APTITUD1-J-0021");
                AsignarValorLaboral1(report, "S10R22", vRespuestas, "APTITUD1-J-0022");


                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }
            else
            {

                report.Dictionary.Variables["S1R1"].Value = "0";
                report.Dictionary.Variables["S1R2"].Value = "0";
                report.Dictionary.Variables["S1R3"].Value = "0";
                report.Dictionary.Variables["S1R4"].Value = "0";
                report.Dictionary.Variables["S1R5"].Value = "0";
                report.Dictionary.Variables["S1R6"].Value = "0";
                report.Dictionary.Variables["S1R7"].Value = "0";
                report.Dictionary.Variables["S1R8"].Value = "0";
                report.Dictionary.Variables["S1R9"].Value = "0";
                report.Dictionary.Variables["S1R10"].Value = "0";
                report.Dictionary.Variables["S1R11"].Value = "0";
                report.Dictionary.Variables["S1R12"].Value = "0";
                report.Dictionary.Variables["S1R13"].Value = "0";
                report.Dictionary.Variables["S1R14"].Value = "0";
                report.Dictionary.Variables["S1R15"].Value = "0";
                report.Dictionary.Variables["S1R16"].Value = "0";

                report.Dictionary.Variables["S2R1"].Value = "0";
                report.Dictionary.Variables["S2R2"].Value = "0";
                report.Dictionary.Variables["S2R3"].Value = "0";
                report.Dictionary.Variables["S2R4"].Value = "0";
                report.Dictionary.Variables["S2R5"].Value = "0";
                report.Dictionary.Variables["S2R6"].Value = "0";
                report.Dictionary.Variables["S2R7"].Value = "0";
                report.Dictionary.Variables["S2R8"].Value = "0";
                report.Dictionary.Variables["S2R9"].Value = "0";
                report.Dictionary.Variables["S2R10"].Value = "0";
                report.Dictionary.Variables["S2R11"].Value = "0";

                report.Dictionary.Variables["S3R1"].Value = "0";
                report.Dictionary.Variables["S3R2"].Value = "0";
                report.Dictionary.Variables["S3R3"].Value = "0";
                report.Dictionary.Variables["S3R4"].Value = "0";
                report.Dictionary.Variables["S3R5"].Value = "0";
                report.Dictionary.Variables["S3R6"].Value = "0";
                report.Dictionary.Variables["S3R7"].Value = "0";
                report.Dictionary.Variables["S3R8"].Value = "0";
                report.Dictionary.Variables["S3R9"].Value = "0";
                report.Dictionary.Variables["S3R10"].Value = "0";
                report.Dictionary.Variables["S3R11"].Value = "0";
                report.Dictionary.Variables["S3R12"].Value = "0";
                report.Dictionary.Variables["S3R13"].Value = "0";
                report.Dictionary.Variables["S3R14"].Value = "0";
                report.Dictionary.Variables["S3R15"].Value = "0";
                report.Dictionary.Variables["S3R16"].Value = "0";
                report.Dictionary.Variables["S3R17"].Value = "0";
                report.Dictionary.Variables["S3R18"].Value = "0";
                report.Dictionary.Variables["S3R19"].Value = "0";
                report.Dictionary.Variables["S3R20"].Value = "0";
                report.Dictionary.Variables["S3R21"].Value = "0";
                report.Dictionary.Variables["S3R22"].Value = "0";
                report.Dictionary.Variables["S3R23"].Value = "0";
                report.Dictionary.Variables["S3R24"].Value = "0";
                report.Dictionary.Variables["S3R25"].Value = "0";
                report.Dictionary.Variables["S3R26"].Value = "0";
                report.Dictionary.Variables["S3R27"].Value = "0";
                report.Dictionary.Variables["S3R28"].Value = "0";
                report.Dictionary.Variables["S3R29"].Value = "0";
                report.Dictionary.Variables["S3R30"].Value = "0";

                report.Dictionary.Variables["S4R1"].Value = "0";
                report.Dictionary.Variables["S4R2"].Value = "0";
                report.Dictionary.Variables["S4R3"].Value = "0";
                report.Dictionary.Variables["S4R4"].Value = "0";
                report.Dictionary.Variables["S4R5"].Value = "0";
                report.Dictionary.Variables["S4R6"].Value = "0";
                report.Dictionary.Variables["S4R7"].Value = "0";
                report.Dictionary.Variables["S4R8"].Value = "0";
                report.Dictionary.Variables["S4R9"].Value = "0";
                report.Dictionary.Variables["S4R10"].Value = "0";
                report.Dictionary.Variables["S4R11"].Value = "0";
                report.Dictionary.Variables["S4R12"].Value = "0";
                report.Dictionary.Variables["S4R13"].Value = "0";
                report.Dictionary.Variables["S4R14"].Value = "0";
                report.Dictionary.Variables["S4R15"].Value = "0";
                report.Dictionary.Variables["S4R16"].Value = "0";
                report.Dictionary.Variables["S4R17"].Value = "0";
                report.Dictionary.Variables["S4R18"].Value = "0";

                report.Dictionary.Variables["S5R1"].Value = "0";
                report.Dictionary.Variables["S5R2"].Value = "0";
                report.Dictionary.Variables["S5R3"].Value = "0";
                report.Dictionary.Variables["S5R4"].Value = "0";
                report.Dictionary.Variables["S5R5"].Value = "0";
                report.Dictionary.Variables["S5R6"].Value = "0";
                report.Dictionary.Variables["S5R7"].Value = "0";
                report.Dictionary.Variables["S5R8"].Value = "0";
                report.Dictionary.Variables["S5R9"].Value = "0";
                report.Dictionary.Variables["S5R10"].Value = "0";
                report.Dictionary.Variables["S5R11"].Value = "0";
                report.Dictionary.Variables["S5R12"].Value = "0";

                report.Dictionary.Variables["S6R1"].Value = "0";
                report.Dictionary.Variables["S6R2"].Value = "0";
                report.Dictionary.Variables["S6R3"].Value = "0";
                report.Dictionary.Variables["S6R4"].Value = "0";
                report.Dictionary.Variables["S6R5"].Value = "0";
                report.Dictionary.Variables["S6R6"].Value = "0";
                report.Dictionary.Variables["S6R7"].Value = "0";
                report.Dictionary.Variables["S6R8"].Value = "0";
                report.Dictionary.Variables["S6R9"].Value = "0";
                report.Dictionary.Variables["S6R10"].Value = "0";
                report.Dictionary.Variables["S6R11"].Value = "0";
                report.Dictionary.Variables["S6R12"].Value = "0";
                report.Dictionary.Variables["S6R13"].Value = "0";
                report.Dictionary.Variables["S6R14"].Value = "0";
                report.Dictionary.Variables["S6R15"].Value = "0";
                report.Dictionary.Variables["S6R16"].Value = "0";
                report.Dictionary.Variables["S6R17"].Value = "0";
                report.Dictionary.Variables["S6R18"].Value = "0";
                report.Dictionary.Variables["S6R19"].Value = "0";
                report.Dictionary.Variables["S6R20"].Value = "0";

                report.Dictionary.Variables["S7R1"].Value = "0";
                report.Dictionary.Variables["S7R2"].Value = "0";
                report.Dictionary.Variables["S7R3"].Value = "0";
                report.Dictionary.Variables["S7R4"].Value = "0";
                report.Dictionary.Variables["S7R5"].Value = "0";
                report.Dictionary.Variables["S7R6"].Value = "0";
                report.Dictionary.Variables["S7R7"].Value = "0";
                report.Dictionary.Variables["S7R8"].Value = "0";
                report.Dictionary.Variables["S7R9"].Value = "0";
                report.Dictionary.Variables["S7R10"].Value = "0";
                report.Dictionary.Variables["S7R11"].Value = "0";
                report.Dictionary.Variables["S7R12"].Value = "0";
                report.Dictionary.Variables["S7R13"].Value = "0";
                report.Dictionary.Variables["S7R14"].Value = "0";
                report.Dictionary.Variables["S7R15"].Value = "0";
                report.Dictionary.Variables["S7R16"].Value = "0";
                report.Dictionary.Variables["S7R17"].Value = "0";
                report.Dictionary.Variables["S7R18"].Value = "0";
                report.Dictionary.Variables["S7R19"].Value = "0";
                report.Dictionary.Variables["S7R20"].Value = "0";

                report.Dictionary.Variables["S8R1"].Value = "0";
                report.Dictionary.Variables["S8R2"].Value = "0";
                report.Dictionary.Variables["S8R3"].Value = "0";
                report.Dictionary.Variables["S8R4"].Value = "0";
                report.Dictionary.Variables["S8R5"].Value = "0";
                report.Dictionary.Variables["S8R6"].Value = "0";
                report.Dictionary.Variables["S8R7"].Value = "0";
                report.Dictionary.Variables["S8R8"].Value = "0";
                report.Dictionary.Variables["S8R9"].Value = "0";
                report.Dictionary.Variables["S8R10"].Value = "0";
                report.Dictionary.Variables["S8R11"].Value = "0";
                report.Dictionary.Variables["S8R12"].Value = "0";
                report.Dictionary.Variables["S8R13"].Value = "0";
                report.Dictionary.Variables["S8R14"].Value = "0";
                report.Dictionary.Variables["S8R15"].Value = "0";
                report.Dictionary.Variables["S8R16"].Value = "0";
                report.Dictionary.Variables["S8R17"].Value = "0";

                report.Dictionary.Variables["S9R1"].Value = "0";
                report.Dictionary.Variables["S9R2"].Value = "0";
                report.Dictionary.Variables["S9R3"].Value = "0";
                report.Dictionary.Variables["S9R4"].Value = "0";
                report.Dictionary.Variables["S9R5"].Value = "0";
                report.Dictionary.Variables["S9R6"].Value = "0";
                report.Dictionary.Variables["S9R7"].Value = "0";
                report.Dictionary.Variables["S9R8"].Value = "0";
                report.Dictionary.Variables["S9R9"].Value = "0";
                report.Dictionary.Variables["S9R10"].Value = "0";
                report.Dictionary.Variables["S9R11"].Value = "0";
                report.Dictionary.Variables["S9R12"].Value = "0";
                report.Dictionary.Variables["S9R13"].Value = "0";
                report.Dictionary.Variables["S9R14"].Value = "0";
                report.Dictionary.Variables["S9R15"].Value = "0";
                report.Dictionary.Variables["S9R16"].Value = "0";
                report.Dictionary.Variables["S9R17"].Value = "0";
                report.Dictionary.Variables["S9R18"].Value = "0";

                report.Dictionary.Variables["S10R1"].Value = "0";
                report.Dictionary.Variables["S10R2"].Value = "0";
                report.Dictionary.Variables["S10R3"].Value = "0";
                report.Dictionary.Variables["S10R4"].Value = "0";
                report.Dictionary.Variables["S10R5"].Value = "0";
                report.Dictionary.Variables["S10R6"].Value = "0";
                report.Dictionary.Variables["S10R7"].Value = "0";
                report.Dictionary.Variables["S10R8"].Value = "0";
                report.Dictionary.Variables["S10R9"].Value = "0";
                report.Dictionary.Variables["S10R10"].Value = "0";
                report.Dictionary.Variables["S10R11"].Value = "0";
                report.Dictionary.Variables["S10R12"].Value = "0";
                report.Dictionary.Variables["S10R13"].Value = "0";
                report.Dictionary.Variables["S10R14"].Value = "0";
                report.Dictionary.Variables["S10R15"].Value = "0";
                report.Dictionary.Variables["S10R16"].Value = "0";
                report.Dictionary.Variables["S10R17"].Value = "0";
                report.Dictionary.Variables["S10R18"].Value = "0";
                report.Dictionary.Variables["S10R19"].Value = "0";
                report.Dictionary.Variables["S10R20"].Value = "0";
                report.Dictionary.Variables["S10R21"].Value = "0";
                report.Dictionary.Variables["S10R22"].Value = "0";

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }

            report.Compile();
            swvReporte.Report = report;
        }

        private void cargarOrtografiaI()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();
            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();

            report.Load(Server.MapPath("~/Assets/reports/IDP/OrtografiaI.mrt"));
            report.Dictionary.Databases.Clear();

            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));


            if (vRespuestas.Count > 0)
            {
                AsignarValorOrtografiaI(report, "A0001", "A0001B", "", vRespuestas, "ORTOGRAFIA-1-A0001");
                AsignarValorOrtografiaI(report, "A0002", "A0002B", "", vRespuestas, "ORTOGRAFIA-1-A0002");
                AsignarValorOrtografiaI(report, "A0003", "A0003B", "", vRespuestas, "ORTOGRAFIA-1-A0003");
                AsignarValorOrtografiaI(report, "A0004", "A0004B", "", vRespuestas, "ORTOGRAFIA-1-A0004");
                AsignarValorOrtografiaI(report, "A0005", "A0005B", "", vRespuestas, "ORTOGRAFIA-1-A0005");
                AsignarValorOrtografiaI(report, "A0006", "", "", vRespuestas, "ORTOGRAFIA-1-A0006");
                AsignarValorOrtografiaI(report, "A0007", "A0007B", "", vRespuestas, "ORTOGRAFIA-1-A0007");
                AsignarValorOrtografiaI(report, "A0008", "", "", vRespuestas, "ORTOGRAFIA-1-A0008");
                AsignarValorOrtografiaI(report, "A0009", "A0009B", "", vRespuestas, "ORTOGRAFIA-1-A0009");
                AsignarValorOrtografiaI(report, "A0010", "", "", vRespuestas, "ORTOGRAFIA-1-A0010");
                AsignarValorOrtografiaI(report, "A0011", "A0011B", "", vRespuestas, "ORTOGRAFIA-1-A0011");
                AsignarValorOrtografiaI(report, "A0012", "", "", vRespuestas, "ORTOGRAFIA-1-A0012");
                AsignarValorOrtografiaI(report, "A0013", "A0013B", "", vRespuestas, "ORTOGRAFIA-1-A0013");
                AsignarValorOrtografiaI(report, "A0014", "A0014B", "", vRespuestas, "ORTOGRAFIA-1-A0014");
                AsignarValorOrtografiaI(report, "A0015", "", "", vRespuestas, "ORTOGRAFIA-1-A0015");
                AsignarValorOrtografiaI(report, "A0016", "", "", vRespuestas, "ORTOGRAFIA-1-A0016");
                AsignarValorOrtografiaI(report, "A0017", "A0017B", "", vRespuestas, "ORTOGRAFIA-1-A0017");
                AsignarValorOrtografiaI(report, "A0018", "", "", vRespuestas, "ORTOGRAFIA-1-A0018");
                AsignarValorOrtografiaI(report, "A0019", "A0019B", "", vRespuestas, "ORTOGRAFIA-1-A0019");
                AsignarValorOrtografiaI(report, "A0020", "", "", vRespuestas, "ORTOGRAFIA-1-A0020");

                AsignarValorOrtografiaI(report, "B0001", "", "", vRespuestas, "ORTOGRAFIA-1-B0001");
                AsignarValorOrtografiaI(report, "B0002", "B0002B", "", vRespuestas, "ORTOGRAFIA-1-B0002");
                AsignarValorOrtografiaI(report, "B0003", "", "", vRespuestas, "ORTOGRAFIA-1-B0003");
                AsignarValorOrtografiaI(report, "B0004", "", "", vRespuestas, "ORTOGRAFIA-1-B0004");
                AsignarValorOrtografiaI(report, "B0005", "B0005B", "", vRespuestas, "ORTOGRAFIA-1-B0005");
                AsignarValorOrtografiaI(report, "B0006", "B0006B", "", vRespuestas, "ORTOGRAFIA-1-B0006");
                AsignarValorOrtografiaI(report, "B0007", "", "", vRespuestas, "ORTOGRAFIA-1-B0007");
                AsignarValorOrtografiaI(report, "B0008", "", "", vRespuestas, "ORTOGRAFIA-1-B0008");
                AsignarValorOrtografiaI(report, "B0009", "", "", vRespuestas, "ORTOGRAFIA-1-B0009");
                AsignarValorOrtografiaI(report, "B0010", "", "", vRespuestas, "ORTOGRAFIA-1-B0010");
                AsignarValorOrtografiaI(report, "B0011", "", "", vRespuestas, "ORTOGRAFIA-1-B0011");
                AsignarValorOrtografiaI(report, "B0012", "", "", vRespuestas, "ORTOGRAFIA-1-B0012");
                AsignarValorOrtografiaI(report, "B0013", "", "", vRespuestas, "ORTOGRAFIA-1-B0013");
                AsignarValorOrtografiaI(report, "B0014", "", "", vRespuestas, "ORTOGRAFIA-1-B0014");
                AsignarValorOrtografiaI(report, "B0015", "", "", vRespuestas, "ORTOGRAFIA-1-B0015");
                AsignarValorOrtografiaI(report, "B0016", "", "", vRespuestas, "ORTOGRAFIA-1-B0016");
                AsignarValorOrtografiaI(report, "B0017", "", "", vRespuestas, "ORTOGRAFIA-1-B0017");
                AsignarValorOrtografiaI(report, "B0018", "", "", vRespuestas, "ORTOGRAFIA-1-B0018");
                AsignarValorOrtografiaI(report, "B0019", "B0019B", "", vRespuestas, "ORTOGRAFIA-1-B0019");
                AsignarValorOrtografiaI(report, "B0020", "B0020B", "B0020C", vRespuestas, "ORTOGRAFIA-1-B0020");

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;

            }
            else
            {
                report.Dictionary.Variables["A0001"].Value = string.Empty;
                report.Dictionary.Variables["A0001B"].Value = string.Empty;
                report.Dictionary.Variables["A0002"].Value = string.Empty;
                report.Dictionary.Variables["A0002B"].Value = string.Empty;
                report.Dictionary.Variables["A0003"].Value = string.Empty;
                report.Dictionary.Variables["A0003B"].Value = string.Empty;
                report.Dictionary.Variables["A0004"].Value = string.Empty;
                report.Dictionary.Variables["A0004B"].Value = string.Empty;
                report.Dictionary.Variables["A0005"].Value = string.Empty;
                report.Dictionary.Variables["A0005B"].Value = string.Empty;
                report.Dictionary.Variables["A0006"].Value = string.Empty;
                report.Dictionary.Variables["A0007"].Value = string.Empty;
                report.Dictionary.Variables["A0007"].Value = string.Empty;
                report.Dictionary.Variables["A0008"].Value = string.Empty;
                report.Dictionary.Variables["A0009"].Value = string.Empty;
                report.Dictionary.Variables["A0009B"].Value = string.Empty;
                report.Dictionary.Variables["A0010"].Value = string.Empty;
                report.Dictionary.Variables["A0011"].Value = string.Empty;
                report.Dictionary.Variables["A0011B"].Value = string.Empty;
                report.Dictionary.Variables["A0012"].Value = string.Empty;
                report.Dictionary.Variables["A0013"].Value = string.Empty;
                report.Dictionary.Variables["A0013B"].Value = string.Empty;
                report.Dictionary.Variables["A0014"].Value = string.Empty;
                report.Dictionary.Variables["A0014B"].Value = string.Empty;
                report.Dictionary.Variables["A0015"].Value = string.Empty;
                report.Dictionary.Variables["A0016"].Value = string.Empty;
                report.Dictionary.Variables["A0017"].Value = string.Empty;
                report.Dictionary.Variables["A0017B"].Value = string.Empty;
                report.Dictionary.Variables["A0018"].Value = string.Empty;
                report.Dictionary.Variables["A0019"].Value = string.Empty;
                report.Dictionary.Variables["A0019B"].Value = string.Empty;
                report.Dictionary.Variables["A0020"].Value = string.Empty;
                report.Dictionary.Variables["B0001"].Value = string.Empty;
                report.Dictionary.Variables["B0002"].Value = string.Empty;
                report.Dictionary.Variables["B0002B"].Value = string.Empty;
                report.Dictionary.Variables["B0003"].Value = string.Empty;
                report.Dictionary.Variables["B0004"].Value = string.Empty;
                report.Dictionary.Variables["B0005"].Value = string.Empty;
                report.Dictionary.Variables["B0005B"].Value = string.Empty;
                report.Dictionary.Variables["B0006"].Value = string.Empty;
                report.Dictionary.Variables["B0006B"].Value = string.Empty;
                report.Dictionary.Variables["B0007"].Value = string.Empty;
                report.Dictionary.Variables["B0008"].Value = string.Empty;
                report.Dictionary.Variables["B0009"].Value = string.Empty;
                report.Dictionary.Variables["B0009B"].Value = string.Empty;
                report.Dictionary.Variables["B0010"].Value = string.Empty;
                report.Dictionary.Variables["B0011"].Value = string.Empty;
                report.Dictionary.Variables["B0012"].Value = string.Empty;
                report.Dictionary.Variables["B0013"].Value = string.Empty;
                report.Dictionary.Variables["B0014"].Value = string.Empty;
                report.Dictionary.Variables["B0015"].Value = string.Empty;
                report.Dictionary.Variables["B0016"].Value = string.Empty;
                report.Dictionary.Variables["B0017"].Value = string.Empty;
                report.Dictionary.Variables["B0018"].Value = string.Empty;
                report.Dictionary.Variables["B0019"].Value = string.Empty;
                report.Dictionary.Variables["B0020"].Value = string.Empty;
                report.Dictionary.Variables["B0020B"].Value = string.Empty;
                report.Dictionary.Variables["B0020C"].Value = string.Empty;

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;

            }

            report.Compile();
            swvReporte.Report = report;
        }

        private void cargarOrtografiaII()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();
            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();

            report.Load(Server.MapPath("~/Assets/reports/IDP/OrtografiaII.mrt"));
            report.Dictionary.Databases.Clear();
            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));


            if (vRespuestas.Count > 0)
            {
                foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in vRespuestas)
            {
                switch (resp.CL_PREGUNTA)
                {
                case "ORTOGRAFIA2-A-0001": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0001","Resp1", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0001"); break; 
                case "ORTOGRAFIA2-A-0002": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0002","Resp2", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0002"); break;
                case "ORTOGRAFIA2-A-0003": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0003", "Resp3", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0003"); break;
                case "ORTOGRAFIA2-A-0004": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0004", "Resp4", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0004"); break;
                case "ORTOGRAFIA2-A-0005": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0005", "Resp5", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0005"); break;
                case "ORTOGRAFIA2-A-0006": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0006", "Resp6", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0006"); break;
                case "ORTOGRAFIA2-A-0007": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0007", "Resp7", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0007"); break;
                case "ORTOGRAFIA2-A-0008": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0008", "Resp8", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0008"); break;
                case "ORTOGRAFIA2-A-0009": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0009", "Resp9", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0009"); break;
                case "ORTOGRAFIA2-A-0010": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0010", "Resp10", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0010"); break;
                case "ORTOGRAFIA2-A-0011": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0011", "Resp11", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0011"); break;
                case "ORTOGRAFIA2-A-0012": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0012", "Resp12", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0012"); break;
                case "ORTOGRAFIA2-A-0013": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0013", "Resp13", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0013"); break;
                case "ORTOGRAFIA2-A-0014": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0014", "Resp14", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0014"); break;
                case "ORTOGRAFIA2-A-0015": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0015", "Resp15", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0015"); break;
                case "ORTOGRAFIA2-A-0016": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0016", "Resp16", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0016"); break;
                case "ORTOGRAFIA2-A-0017": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0017", "Resp17", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0017"); break;
                case "ORTOGRAFIA2-A-0018": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0018", "Resp18", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0018"); break;
                case "ORTOGRAFIA2-A-0019": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0019", "Resp19", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0019"); break;
                case "ORTOGRAFIA2-A-0020": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0020", "Resp20", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0020"); break;
                case "ORTOGRAFIA2-A-0021": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0021", "Resp21", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0021"); break;
                case "ORTOGRAFIA2-A-0022": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0022", "Resp22", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0022"); break;
                case "ORTOGRAFIA2-A-0023": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0023", "Resp23", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0023"); break;
                case "ORTOGRAFIA2-A-0024": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0024", "Resp24", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0024"); break;
                case "ORTOGRAFIA2-A-0025": AsignarValorOrtografiaII(report, "ORTOGRAFIA2_A_0025", "Resp25", resp.NB_RESPUESTA, "ORTOGRAFIA2-A-0025"); break;

               
                }
            }
             report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;

            }
            else
            {
                report.Dictionary.Variables["ORTOGRAFIA2_A_0001"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0002"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0003"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0004"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0005"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0006"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0007"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0008"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0009"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0010"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0011"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0012"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0013"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0014"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0015"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0016"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0017"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0018"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0019"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0020"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0021"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0022"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0023"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0024"].Value = "";
                report.Dictionary.Variables["ORTOGRAFIA2_A_0025"].Value = "";

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }

            report.Compile();
            swvReporte.Report = report;
        }

        private void CargarOrtografiaIII()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();
            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();

            report.Load(Server.MapPath("~/Assets/reports/IDP/OrtografiaIII.mrt"));
            report.Dictionary.Databases.Clear();
            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));


            if (vRespuestas.Count > 0)
            {

                foreach (SPE_OBTIENE_RESULTADO_PRUEBA_Result resp in vRespuestas)
                {
                    switch (resp.CL_PREGUNTA)
                    {
                        case "ORTOGRAFIA-3-A0001": OrtografiaIIIRespuestas(resp.NB_RESPUESTA, "RespList1", report); break;
                    }
                }

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;

            }
            else
            {
                report.Dictionary.Variables["RespList1"].Value = "";

                report.Dictionary.Variables["NB_CANDIDATO"].Value = vPrueba.NB_CANDIDATO_COMPLETO;
            }

            report.Compile();
            swvReporte.Report = report;
        }

        private void cargarAptitudII()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();
            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();

            report.Load(Server.MapPath("~/Assets/reports/IDP/Mental2.mrt"));
            report.Dictionary.Databases.Clear();
            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

            if (vPrueba.NB_TIPO_PRUEBA == "MANUAL")
            {
                asignarValoresManual(vRespuestas, vPrueba.NB_CANDIDATO_COMPLETO, report);
            }
            else
            {
                asignarValores(vRespuestas, vPrueba.NB_CANDIDATO_COMPLETO, report);
            }

        }

        private void CargarIngles()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;
            PruebasNegocio nKprueba = new PruebasNegocio();
            List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> vRespuestas = nKprueba.Obtener_RESULTADO_PRUEBA(vIdPrueba, vClToken);
            SPE_OBTIENE_K_PRUEBA_Result vPrueba = nKprueba.Obtener_K_PRUEBA(pIdPrueba: vIdPrueba).FirstOrDefault();

            report.Load(Server.MapPath("~/Assets/reports/IDP/Ingles.mrt"));
            report.Dictionary.Databases.Clear();
            pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
            report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

            asignarValoresIngles(vRespuestas, vPrueba.NB_CANDIDATO_COMPLETO, report);

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Request.Params["IdPrueba"] != null & Request.Params["ClToken"] != null & Request.Params["ClPrueba"] != null)
                {
                    vIdPrueba = int.Parse(Request.Params["IdPrueba"].ToString());
                    vClToken = Guid.Parse(Request.Params["ClToken"].ToString());
                    vClPrueba = Request.Params["ClPrueba"].ToString();
                    switch (vClPrueba)
                    {
                        case "LABORAL1":
                            CargarLaboral1();
                            break;
                        case "INTERES":
                            CargarInteresesPersonales();
                            break;
                        case "TECNICAPC":
                            CargarTecnicaPC();
                            break;
                        case "LABORAL2":
                            CargarLaboral2();
                            break;
                        case "TIVA":
                            CargarTIVA();
                            break;
                        case "ESTILO":
                            CargarEstilo();
                            break;
                        case "APTITUD1":
                            CargarAptitud();
                            break;
                        case "ORTOGRAFIAI":
                            cargarOrtografiaI();
                            break;
                        case "ORTOGRAFIAII":
                            cargarOrtografiaII();
                            break;
                        case "ORTOGRAFIA3":
                            CargarOrtografiaIII();
                            break;
                        case "APTITUD2":
                            cargarAptitudII();
                            break;
                        case "INGLES":
                            CargarIngles();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        [Serializable]
        public class E_ORTOGRAFIA_III
        {
            public string NB_RESPUESTA { get; set; }
        }

        [Serializable]
        public class E_ORTOGRAFIA_II
        {
            public string VALOR { get; set; }
            public string NB_RESPUESTA { get; set; }
        }

    }
}