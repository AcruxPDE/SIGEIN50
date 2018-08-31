using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.IDP
{
    public partial class ReporteadorResultadosPruebas : System.Web.UI.Page
    {
        List<E_RESULTADOS_BATERIA> vResultados
        {
            get { return (List<E_RESULTADOS_BATERIA>)ViewState["vResultados"]; }
            set { ViewState["vResultados"] = value; }
        }

        private int vIdBateria
        {
            get { return (int)ViewState["vs_vIdBateria"]; }
            set { ViewState["vs_vIdBateria"] = value; }
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

        decimal? ORTOGRAFIA1_TOTAL;
        decimal? ORTOGRAFIA1_ACIERTOS;
        decimal? ORTOGRAFIA2_TOTAL;
        decimal? ORTOGRAFIA2_ACIERTOS;
        decimal? ORTOGRAFIA3_TOTAL;
        decimal? ORTOGRAFIA3_ACIERTOS;
        double PORCENTAJE_TOTAL;
        string NIVEL_ORT;

        decimal? vNoEscala = 180;
        decimal? vPtCenterX = 278;
        decimal? vPtCenterY = 234;

        public string DisplayGraf(string pValue, string pElement, int pNumero)
        {
            string vImage = "";
            string vSp = "";
            int noValor = int.Parse(pValue);
            double valImg = Math.Floor((double)(((noValor - 10) / 2) + 1));
            if (pNumero > 1) --valImg;
            if (valImg > 13) valImg = 13;
            if (valImg < 0) valImg = 0;
            var Color = "";
            var Style = "";
            switch (pNumero)
            {
                case 1:
                    Color = "Azul";
                    break;
                case 2:
                    Color = "Rojo";

                    break;
                case 3:
                    Color = "Verde";
                    break;
                case 4:
                    Color = "Amarillo";
                    break;
            }
            vImage = "../Assets/reports/IDP/LifoLaboral2/Lifo" + Color + valImg + ".jpg";
            vSp = noValor.ToString();

            return vImage;
        }

        public string DisplaySp(string pValue, string pElement, int pNumero)
        {
            string vImage = "";
            string vSp = "";
            int noValor = int.Parse(pValue);
            double valImg = Math.Floor((double)(((noValor - 10) / 2) + 1));
            if (pNumero > 1) --valImg;
            if (valImg > 13) valImg = 13;
            if (valImg < 0) valImg = 0;
            var Color = "";
            var Style = "";
            switch (pNumero)
            {
                case 1:
                    Color = "Azul";
                    break;
                case 2:
                    Color = "Rojo";

                    break;
                case 3:
                    Color = "Verde";
                    break;
                case 4:
                    Color = "Amarillo";
                    break;
            }
            vImage = "../Assets/images/PruebaLaboralII/Lifo" + Color + valImg + ".gif";
            vSp = noValor.ToString();

            return vSp;
        }

        private void configurarComparacion1(int seccion, StiReport pReport)
        {
            switch (seccion)
            {
                case 1:
                    //    pReport.Dictionary.Variables["PS1"].Value = "<div style='background-color:red; height:40px; padding-top:20px; text-align:center;'>Simples</div>";
                    //    pReport.Dictionary.Variables["PS2"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Comunes</div>";
                    //    pReport.Dictionary.Variables["PS3"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Término medio</div>";
                    //    pReport.Dictionary.Variables["PS4"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Difíciles</div>";
                    //    pReport.Dictionary.Variables["PS5"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>De lo más complejo</div>";
                    pReport.Dictionary.Variables["PS1"].Value = "Simples";
                    pReport.Dictionary.Variables["PS2"].Value = "Comunes";
                    pReport.Dictionary.Variables["PS3"].Value = "Término medio";
                    pReport.Dictionary.Variables["PS4"].Value = "Difíciles";
                    pReport.Dictionary.Variables["PS5"].Value = "De lo más complejo";
                    //   // c11.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    break;

                case 2:
                    //    pReport.Dictionary.Variables["PS1"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Simples</div>";
                    //    pReport.Dictionary.Variables["PS2"].Value = "<div style='background-color:red; height:40px; padding-top:20px; text-align:center;'>Comunes</div>";
                    //    pReport.Dictionary.Variables["PS3"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Término medio</div>";
                    //    pReport.Dictionary.Variables["PS4"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Difíciles</div>";
                    //    pReport.Dictionary.Variables["PS5"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>De lo más complejo</div>";
                    pReport.Dictionary.Variables["PS1"].Value = "Simples";
                    pReport.Dictionary.Variables["PS2"].Value = "Comunes";
                    pReport.Dictionary.Variables["PS3"].Value = "Término medio";
                    pReport.Dictionary.Variables["PS4"].Value = "Difíciles";
                    pReport.Dictionary.Variables["PS5"].Value = "De lo más complejo";
                    break;

                case 3:
                    //    pReport.Dictionary.Variables["PS1"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Simples</div>";
                    //    pReport.Dictionary.Variables["PS2"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Comunes</div>";
                    //    pReport.Dictionary.Variables["PS3"].Value = "<div style='background-color:yellow; height:40px; padding-top:20px; text-align:center;'>Término medio</div>";
                    //    pReport.Dictionary.Variables["PS4"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Difíciles</div>";
                    //    pReport.Dictionary.Variables["PS5"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>De lo más complejo</div>";
                    pReport.Dictionary.Variables["PS1"].Value = "Simples";
                    pReport.Dictionary.Variables["PS2"].Value = "Comunes";
                    pReport.Dictionary.Variables["PS3"].Value = "Término medio";
                    pReport.Dictionary.Variables["PS4"].Value = "Difíciles";
                    pReport.Dictionary.Variables["PS5"].Value = "De lo más complejo";
                    break;

                case 4:
                    //    pReport.Dictionary.Variables["PS1"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Simples</div>";
                    //    pReport.Dictionary.Variables["PS2"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Comunes</div>";
                    //    pReport.Dictionary.Variables["PS3"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Término medio</div>";
                    //    pReport.Dictionary.Variables["PS4"].Value = "<div style='background-color:#008000; height:40px; padding-top:20px; text-align:center;'>Difíciles</div>";
                    //    pReport.Dictionary.Variables["PS5"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>De lo más complejo</div>";
                    pReport.Dictionary.Variables["PS1"].Value = "Simples";
                    pReport.Dictionary.Variables["PS2"].Value = "Comunes";
                    pReport.Dictionary.Variables["PS3"].Value = "Término medio";
                    pReport.Dictionary.Variables["PS4"].Value = "Difíciles";
                    pReport.Dictionary.Variables["PS5"].Value = "De lo más complejo";
                    break;

                case 5:
                    //     pReport.Dictionary.Variables["PS1"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Simples</div>";
                    //    pReport.Dictionary.Variables["PS2"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Comunes</div>";
                    //    pReport.Dictionary.Variables["PS3"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Término medio</div>";
                    //    pReport.Dictionary.Variables["PS4"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>Difíciles</div>";
                    //    pReport.Dictionary.Variables["PS5"].Value = "<div style='background-color:green; height:40px; padding-top:20px; text-align:center;'>De lo más complejo</div>";
                    pReport.Dictionary.Variables["PS1"].Value = "Simples";
                    pReport.Dictionary.Variables["PS2"].Value = "Comunes";
                    pReport.Dictionary.Variables["PS3"].Value = "Término medio";
                    pReport.Dictionary.Variables["PS4"].Value = "Difíciles";
                    pReport.Dictionary.Variables["PS5"].Value = "De lo más complejo";
                    break;

                default:
                    //     pReport.Dictionary.Variables["PS1"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Simples</div>";
                    //    pReport.Dictionary.Variables["PS2"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Comunes</div>";
                    //    pReport.Dictionary.Variables["PS3"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>Término medio</div>";
                    //    pReport.Dictionary.Variables["PS4"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>Difíciles</div>";
                    //    pReport.Dictionary.Variables["PS5"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>De lo más complejo</div>";
                    pReport.Dictionary.Variables["PS1"].Value = "Simples";
                    pReport.Dictionary.Variables["PS2"].Value = "Comunes";
                    pReport.Dictionary.Variables["PS3"].Value = "Término medio";
                    pReport.Dictionary.Variables["PS4"].Value = "Difíciles";
                    pReport.Dictionary.Variables["PS5"].Value = "De lo más complejo";
                    break;
            }
        }

        private void configurarComparacion2(int seccion, StiReport pReport)
        {
            switch (seccion)
            {
                case 1:
                    //pReport.Dictionary.Variables["CI1"].Value = "<div style=' background-color:red; height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI2"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI3"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>20</div>";
                    //pReport.Dictionary.Variables["CI4"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>30</div>";
                    //pReport.Dictionary.Variables["CI5"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>40</div>";
                    //pReport.Dictionary.Variables["CI6"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>50</div>";
                    //pReport.Dictionary.Variables["CI7"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>60</div>";
                    //pReport.Dictionary.Variables["CI8"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>70</div>";
                    //pReport.Dictionary.Variables["CI9"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>80</div>";
                    //pReport.Dictionary.Variables["CI10"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>90</div>";
                    pReport.Dictionary.Variables["CI1"].Value = "10";
                    pReport.Dictionary.Variables["CI2"].Value = "10";
                    pReport.Dictionary.Variables["CI3"].Value = "20";
                    pReport.Dictionary.Variables["CI4"].Value = "30";
                    pReport.Dictionary.Variables["CI5"].Value = "40";
                    pReport.Dictionary.Variables["CI6"].Value = "50";
                    pReport.Dictionary.Variables["CI7"].Value = "60";
                    pReport.Dictionary.Variables["CI8"].Value = "70";
                    pReport.Dictionary.Variables["CI9"].Value = "80";
                    pReport.Dictionary.Variables["CI10"].Value = "90";
                    break;

                case 2:
                    // c22.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    //pReport.Dictionary.Variables["CI1"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI2"].Value = "<div style=' background-color:red; height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI3"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>20</div>";
                    //pReport.Dictionary.Variables["CI4"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>30</div>";
                    //pReport.Dictionary.Variables["CI5"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>40</div>";
                    //pReport.Dictionary.Variables["CI6"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>50</div>";
                    //pReport.Dictionary.Variables["CI7"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>60</div>";
                    //pReport.Dictionary.Variables["CI8"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>70</div>";
                    //pReport.Dictionary.Variables["CI9"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>80</div>";
                    //pReport.Dictionary.Variables["CI10"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>90</div>";
                    pReport.Dictionary.Variables["CI1"].Value = "10";
                    pReport.Dictionary.Variables["CI2"].Value = "10";
                    pReport.Dictionary.Variables["CI3"].Value = "20";
                    pReport.Dictionary.Variables["CI4"].Value = "30";
                    pReport.Dictionary.Variables["CI5"].Value = "40";
                    pReport.Dictionary.Variables["CI6"].Value = "50";
                    pReport.Dictionary.Variables["CI7"].Value = "60";
                    pReport.Dictionary.Variables["CI8"].Value = "70";
                    pReport.Dictionary.Variables["CI9"].Value = "80";
                    pReport.Dictionary.Variables["CI10"].Value = "90";
                    break;

                case 3:
                    // c23.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    //pReport.Dictionary.Variables["CI1"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI2"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI3"].Value = "<div style=' background-color:red; height:40px; padding-top:20px; text-align:center;'>20</div>";
                    //pReport.Dictionary.Variables["CI4"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>30</div>";
                    //pReport.Dictionary.Variables["CI5"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>40</div>";
                    //pReport.Dictionary.Variables["CI6"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>50</div>";
                    //pReport.Dictionary.Variables["CI7"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>60</div>";
                    //pReport.Dictionary.Variables["CI8"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>70</div>";
                    //pReport.Dictionary.Variables["CI9"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>80</div>";
                    //pReport.Dictionary.Variables["CI10"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>90</div>";
                    pReport.Dictionary.Variables["CI1"].Value = "10";
                    pReport.Dictionary.Variables["CI2"].Value = "10";
                    pReport.Dictionary.Variables["CI3"].Value = "20";
                    pReport.Dictionary.Variables["CI4"].Value = "30";
                    pReport.Dictionary.Variables["CI5"].Value = "40";
                    pReport.Dictionary.Variables["CI6"].Value = "50";
                    pReport.Dictionary.Variables["CI7"].Value = "60";
                    pReport.Dictionary.Variables["CI8"].Value = "70";
                    pReport.Dictionary.Variables["CI9"].Value = "80";
                    pReport.Dictionary.Variables["CI10"].Value = "90";
                    break;

                case 4:
                    //c24.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    //pReport.Dictionary.Variables["CI1"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI2"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI3"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>20</div>";
                    //pReport.Dictionary.Variables["CI4"].Value = "<div style='background-color:red; height:40px; padding-top:20px; text-align:center;'>30</div>";
                    //pReport.Dictionary.Variables["CI5"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>40</div>";
                    //pReport.Dictionary.Variables["CI6"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>50</div>";
                    //pReport.Dictionary.Variables["CI7"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>60</div>";
                    //pReport.Dictionary.Variables["CI8"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>70</div>";
                    //pReport.Dictionary.Variables["CI9"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>80</div>";
                    //pReport.Dictionary.Variables["CI10"].Value = "<div style='height:40px; padding-top:20px; text-align:center;'>90</div>";
                    pReport.Dictionary.Variables["CI1"].Value = "10";
                    pReport.Dictionary.Variables["CI2"].Value = "10";
                    pReport.Dictionary.Variables["CI3"].Value = "20";
                    pReport.Dictionary.Variables["CI4"].Value = "30";
                    pReport.Dictionary.Variables["CI5"].Value = "40";
                    pReport.Dictionary.Variables["CI6"].Value = "50";
                    pReport.Dictionary.Variables["CI7"].Value = "60";
                    pReport.Dictionary.Variables["CI8"].Value = "70";
                    pReport.Dictionary.Variables["CI9"].Value = "80";
                    pReport.Dictionary.Variables["CI10"].Value = "90";
                    break;

                case 5:
                    //c25.Style["background-color"] = ColorTranslator.ToHtml(Color.Yellow);
                    //pReport.Dictionary.Variables["CI1"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI2"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI3"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>20</div>";
                    //pReport.Dictionary.Variables["CI4"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>30</div>";
                    //pReport.Dictionary.Variables["CI5"].Value = "<div style=' background-color:yellow; height:40px; padding-top:20px; text-align:center;'>40</div>";
                    //pReport.Dictionary.Variables["CI6"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>50</div>";
                    //pReport.Dictionary.Variables["CI7"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>60</div>";
                    //pReport.Dictionary.Variables["CI8"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>70</div>";
                    //pReport.Dictionary.Variables["CI9"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>80</div>";
                    //pReport.Dictionary.Variables["CI10"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>90</div>";
                    pReport.Dictionary.Variables["CI1"].Value = "10";
                    pReport.Dictionary.Variables["CI2"].Value = "10";
                    pReport.Dictionary.Variables["CI3"].Value = "20";
                    pReport.Dictionary.Variables["CI4"].Value = "30";
                    pReport.Dictionary.Variables["CI5"].Value = "40";
                    pReport.Dictionary.Variables["CI6"].Value = "50";
                    pReport.Dictionary.Variables["CI7"].Value = "60";
                    pReport.Dictionary.Variables["CI8"].Value = "70";
                    pReport.Dictionary.Variables["CI9"].Value = "80";
                    pReport.Dictionary.Variables["CI10"].Value = "90";
                    break;

                case 6:
                    // c26.Style["background-color"] = ColorTranslator.ToHtml(Color.Yellow);
                    //pReport.Dictionary.Variables["CI1"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI2"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI3"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>20</div>";
                    //pReport.Dictionary.Variables["CI4"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>30</div>";
                    //pReport.Dictionary.Variables["CI5"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>40</div>";
                    //pReport.Dictionary.Variables["CI6"].Value = "<div style=' background-color:yellow; height:40px; padding-top:20px; text-align:center;'>50</div>";
                    //pReport.Dictionary.Variables["CI7"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>60</div>";
                    //pReport.Dictionary.Variables["CI8"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>70</div>";
                    //pReport.Dictionary.Variables["CI9"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>80</div>";
                    //pReport.Dictionary.Variables["CI10"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>90</div>";
                    pReport.Dictionary.Variables["CI1"].Value = "10";
                    pReport.Dictionary.Variables["CI2"].Value = "10";
                    pReport.Dictionary.Variables["CI3"].Value = "20";
                    pReport.Dictionary.Variables["CI4"].Value = "30";
                    pReport.Dictionary.Variables["CI5"].Value = "40";
                    pReport.Dictionary.Variables["CI6"].Value = "50";
                    pReport.Dictionary.Variables["CI7"].Value = "60";
                    pReport.Dictionary.Variables["CI8"].Value = "70";
                    pReport.Dictionary.Variables["CI9"].Value = "80";
                    pReport.Dictionary.Variables["CI10"].Value = "90";
                    break;

                case 7:
                    // c27.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    //pReport.Dictionary.Variables["CI1"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI2"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI3"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>20</div>";
                    //pReport.Dictionary.Variables["CI4"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>30</div>";
                    //pReport.Dictionary.Variables["CI5"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>40</div>";
                    //pReport.Dictionary.Variables["CI6"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>50</div>";
                    //pReport.Dictionary.Variables["CI7"].Value = "<div style=' background-color:green; height:40px; padding-top:20px; text-align:center;'>60</div>";
                    //pReport.Dictionary.Variables["CI8"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>70</div>";
                    //pReport.Dictionary.Variables["CI9"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>80</div>";
                    //pReport.Dictionary.Variables["CI10"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>90</div>";
                    pReport.Dictionary.Variables["CI1"].Value = "10";
                    pReport.Dictionary.Variables["CI2"].Value = "10";
                    pReport.Dictionary.Variables["CI3"].Value = "20";
                    pReport.Dictionary.Variables["CI4"].Value = "30";
                    pReport.Dictionary.Variables["CI5"].Value = "40";
                    pReport.Dictionary.Variables["CI6"].Value = "50";
                    pReport.Dictionary.Variables["CI7"].Value = "60";
                    pReport.Dictionary.Variables["CI8"].Value = "70";
                    pReport.Dictionary.Variables["CI9"].Value = "80";
                    pReport.Dictionary.Variables["CI10"].Value = "90";
                    break;

                case 8:
                    //pReport.Dictionary.Variables["CI1"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI2"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI3"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>20</div>";
                    //pReport.Dictionary.Variables["CI4"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>30</div>";
                    //pReport.Dictionary.Variables["CI5"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>40</div>";
                    //pReport.Dictionary.Variables["CI6"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>50</div>";
                    //pReport.Dictionary.Variables["CI7"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>60</div>";
                    //pReport.Dictionary.Variables["CI8"].Value = "<div style=' background-color:green; height:40px; padding-top:20px; text-align:center;'>70</div>";
                    //pReport.Dictionary.Variables["CI9"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>80</div>";
                    //pReport.Dictionary.Variables["CI10"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>90</div>";
                    pReport.Dictionary.Variables["CI1"].Value = "10";
                    pReport.Dictionary.Variables["CI2"].Value = "10";
                    pReport.Dictionary.Variables["CI3"].Value = "20";
                    pReport.Dictionary.Variables["CI4"].Value = "30";
                    pReport.Dictionary.Variables["CI5"].Value = "40";
                    pReport.Dictionary.Variables["CI6"].Value = "50";
                    pReport.Dictionary.Variables["CI7"].Value = "60";
                    pReport.Dictionary.Variables["CI8"].Value = "70";
                    pReport.Dictionary.Variables["CI9"].Value = "80";
                    pReport.Dictionary.Variables["CI10"].Value = "90";
                    break;

                case 9:
                    // c29.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    //pReport.Dictionary.Variables["CI1"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI2"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI3"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>20</div>";
                    //pReport.Dictionary.Variables["CI4"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>30</div>";
                    //pReport.Dictionary.Variables["CI5"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>40</div>";
                    //pReport.Dictionary.Variables["CI6"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>50</div>";
                    //pReport.Dictionary.Variables["CI7"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>60</div>";
                    //pReport.Dictionary.Variables["CI8"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>70</div>";
                    //pReport.Dictionary.Variables["CI9"].Value = "<div style=' background-color:green; height:40px; padding-top:20px; text-align:center;'>80</div>";
                    //pReport.Dictionary.Variables["CI10"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>90</div>";
                    pReport.Dictionary.Variables["CI1"].Value = "10";
                    pReport.Dictionary.Variables["CI2"].Value = "10";
                    pReport.Dictionary.Variables["CI3"].Value = "20";
                    pReport.Dictionary.Variables["CI4"].Value = "30";
                    pReport.Dictionary.Variables["CI5"].Value = "40";
                    pReport.Dictionary.Variables["CI6"].Value = "50";
                    pReport.Dictionary.Variables["CI7"].Value = "60";
                    pReport.Dictionary.Variables["CI8"].Value = "70";
                    pReport.Dictionary.Variables["CI9"].Value = "80";
                    pReport.Dictionary.Variables["CI10"].Value = "90";
                    break;

                case 10:
                    // c210.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    //pReport.Dictionary.Variables["CI1"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI2"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI3"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>20</div>";
                    //pReport.Dictionary.Variables["CI4"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>30</div>";
                    //pReport.Dictionary.Variables["CI5"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>40</div>";
                    //pReport.Dictionary.Variables["CI6"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>50</div>";
                    //pReport.Dictionary.Variables["CI7"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>60</div>";
                    //pReport.Dictionary.Variables["CI8"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>70</div>";
                    //pReport.Dictionary.Variables["CI9"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>80</div>";
                    //pReport.Dictionary.Variables["CI10"].Value = "<div style=' background-color:green; height:40px; padding-top:20px; text-align:center;'>90</div>";
                    pReport.Dictionary.Variables["CI1"].Value = "10";
                    pReport.Dictionary.Variables["CI2"].Value = "10";
                    pReport.Dictionary.Variables["CI3"].Value = "20";
                    pReport.Dictionary.Variables["CI4"].Value = "30";
                    pReport.Dictionary.Variables["CI5"].Value = "40";
                    pReport.Dictionary.Variables["CI6"].Value = "50";
                    pReport.Dictionary.Variables["CI7"].Value = "60";
                    pReport.Dictionary.Variables["CI8"].Value = "70";
                    pReport.Dictionary.Variables["CI9"].Value = "80";
                    pReport.Dictionary.Variables["CI10"].Value = "90";
                    break;

                default:
                    //pReport.Dictionary.Variables["CI1"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI2"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>10</div>";
                    //pReport.Dictionary.Variables["CI3"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>20</div>";
                    //pReport.Dictionary.Variables["CI4"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>30</div>";
                    //pReport.Dictionary.Variables["CI5"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>40</div>";
                    //pReport.Dictionary.Variables["CI6"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>50</div>";
                    //pReport.Dictionary.Variables["CI7"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>60</div>";
                    //pReport.Dictionary.Variables["CI8"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>70</div>";
                    //pReport.Dictionary.Variables["CI9"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>80</div>";
                    //pReport.Dictionary.Variables["CI10"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>90</div>";
                    pReport.Dictionary.Variables["CI1"].Value = "10";
                    pReport.Dictionary.Variables["CI2"].Value = "10";
                    pReport.Dictionary.Variables["CI3"].Value = "20";
                    pReport.Dictionary.Variables["CI4"].Value = "30";
                    pReport.Dictionary.Variables["CI5"].Value = "40";
                    pReport.Dictionary.Variables["CI6"].Value = "50";
                    pReport.Dictionary.Variables["CI7"].Value = "60";
                    pReport.Dictionary.Variables["CI8"].Value = "70";
                    pReport.Dictionary.Variables["CI9"].Value = "80";
                    pReport.Dictionary.Variables["CI10"].Value = "90";
                    break;
            }
        }

        private void configurarComparacion3(int seccion, StiReport pReport)
        {
            switch (seccion)
            {
                case 1:
                    //   c31.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    //pReport.Dictionary.Variables["FSIN"].Value = "<div style=' background-color:red; height:40px; padding-top:20px; text-align:center;'>Sin orden Disperso</div>";
                    //pReport.Dictionary.Variables["FDIS"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>Tiende a ser Disperso</div>";
                    //pReport.Dictionary.Variables["NEXT"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>Ningún extremo</div>";
                    //pReport.Dictionary.Variables["MET"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Tiende a ser metódico</div>";
                    //pReport.Dictionary.Variables["METOD"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Metódico organizado</div>";
                    pReport.Dictionary.Variables["FSIN"].Value = "Sin orden Disperso";
                    pReport.Dictionary.Variables["FDIS"].Value = "Tiende a ser Disperso";
                    pReport.Dictionary.Variables["NEXT"].Value = "Ningún extremo";
                    pReport.Dictionary.Variables["MET"].Value = "Tiende a ser metódico";
                    pReport.Dictionary.Variables["METOD"].Value = "Metódico organizado";
                    break;

                case 2:
                    //   c32.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    //pReport.Dictionary.Variables["FSIN"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>Sin orden Disperso</div>";
                    //pReport.Dictionary.Variables["FDIS"].Value = "<div style=' background-color:red; height:40px; padding-top:20px; text-align:center;'>Tiende a ser Disperso</div>";
                    //pReport.Dictionary.Variables["NEXT"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>Ningún extremo</div>";
                    //pReport.Dictionary.Variables["MET"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>Tiende a ser metódico</div>";
                    //pReport.Dictionary.Variables["METOD"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>Metódico organizado</div>";
                    pReport.Dictionary.Variables["FSIN"].Value = "Sin orden Disperso";
                    pReport.Dictionary.Variables["FDIS"].Value = "Tiende a ser Disperso";
                    pReport.Dictionary.Variables["NEXT"].Value = "Ningún extremo";
                    pReport.Dictionary.Variables["MET"].Value = "Tiende a ser metódico";
                    pReport.Dictionary.Variables["METOD"].Value = "Metódico organizado";

                    break;

                case 3:
                    //  c33.Style["background-color"] = ColorTranslator.ToHtml(Color.Yellow);
                    //pReport.Dictionary.Variables["FSIN"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Sin orden Disperso</div>";
                    //pReport.Dictionary.Variables["FDIS"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Tiende a ser Disperso</div>";
                    //pReport.Dictionary.Variables["NEXT"].Value = "<div style=' background-color:yellow; height:40px; padding-top:20px; text-align:center;'>Ningún extremo</div>";
                    //pReport.Dictionary.Variables["MET"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Tiende a ser metódico</div>";
                    //pReport.Dictionary.Variables["METOD"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Metódico organizado</div>";
                    pReport.Dictionary.Variables["FSIN"].Value = "Sin orden Disperso";
                    pReport.Dictionary.Variables["FDIS"].Value = "Tiende a ser Disperso";
                    pReport.Dictionary.Variables["NEXT"].Value = "Ningún extremo";
                    pReport.Dictionary.Variables["MET"].Value = "Tiende a ser metódico";
                    pReport.Dictionary.Variables["METOD"].Value = "Metódico organizado";
                    break;

                case 4:
                    //  c34.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    //pReport.Dictionary.Variables["FSIN"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Sin orden Disperso</div>";
                    //pReport.Dictionary.Variables["FDIS"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Tiende a ser Disperso</div>";
                    //pReport.Dictionary.Variables["NEXT"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Ningún extremo</div>";
                    //pReport.Dictionary.Variables["MET"].Value = "<div style=' background-color:green; height:40px; padding-top:20px; text-align:center;'>Tiende a ser metódico</div>";
                    //pReport.Dictionary.Variables["METOD"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>Metódico organizado</div>";
                    pReport.Dictionary.Variables["FSIN"].Value = "Sin orden Disperso";
                    pReport.Dictionary.Variables["FDIS"].Value = "Tiende a ser Disperso";
                    pReport.Dictionary.Variables["NEXT"].Value = "Ningún extremo";
                    pReport.Dictionary.Variables["MET"].Value = "Tiende a ser metódico";
                    pReport.Dictionary.Variables["METOD"].Value = "Metódico organizado";
                    break;

                case 5:
                    //  c35.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    //pReport.Dictionary.Variables["FSIN"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Sin orden Disperso</div>";
                    //pReport.Dictionary.Variables["FDIS"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Tiende a ser Disperso</div>";
                    //pReport.Dictionary.Variables["NEXT"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Ningún extremo</div>";
                    //pReport.Dictionary.Variables["MET"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Tiende a ser metódico</div>";
                    //pReport.Dictionary.Variables["METOD"].Value = "<div style=' background-color:green; height:40px; padding-top:20px; text-align:center;'>Metódico organizado</div>";
                    pReport.Dictionary.Variables["FSIN"].Value = "Sin orden Disperso";
                    pReport.Dictionary.Variables["FDIS"].Value = "Tiende a ser Disperso";
                    pReport.Dictionary.Variables["NEXT"].Value = "Ningún extremo";
                    pReport.Dictionary.Variables["MET"].Value = "Tiende a ser metódico";
                    pReport.Dictionary.Variables["METOD"].Value = "Metódico organizado";
                    break;

                default:
                    //pReport.Dictionary.Variables["FSIN"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Sin orden Disperso</div>";
                    //pReport.Dictionary.Variables["FDIS"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Tiende a ser Disperso</div>";
                    //pReport.Dictionary.Variables["NEXT"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Ningún extremo</div>";
                    //pReport.Dictionary.Variables["MET"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>Tiende a ser metódico</div>";
                    //pReport.Dictionary.Variables["METOD"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>Metódico organizado</div>";
                    pReport.Dictionary.Variables["FSIN"].Value = "Sin orden Disperso";
                    pReport.Dictionary.Variables["FDIS"].Value = "Tiende a ser Disperso";
                    pReport.Dictionary.Variables["NEXT"].Value = "Ningún extremo";
                    pReport.Dictionary.Variables["MET"].Value = "Tiende a ser metódico";
                    pReport.Dictionary.Variables["METOD"].Value = "Metódico organizado";
                    break;
            }
        }

        private void configurarComparacion4(int seccion, StiReport pReport)
        {
            switch (seccion)
            {
                case 1:
                    // c41.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    //pReport.Dictionary.Variables["ML"].Value = "<div style=' background-color:red; height:40px; padding-top:20px; text-align:center;'>39 Muy lento</div>";
                    //pReport.Dictionary.Variables["L"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>40-59 Lento</div>";
                    //pReport.Dictionary.Variables["NE"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>60-74 Ningún extremo</div>";
                    //pReport.Dictionary.Variables["R"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>75 Rápido</div>";
                    //pReport.Dictionary.Variables["MR"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>>75 Muy rápido</div>";
                    pReport.Dictionary.Variables["ML"].Value = "39 Muy lento";
                    pReport.Dictionary.Variables["L"].Value = "40-59 Lento";
                    pReport.Dictionary.Variables["NE"].Value = "60-74 Ningún extremo";
                    pReport.Dictionary.Variables["R"].Value = "75 Rápido";
                    pReport.Dictionary.Variables["MR"].Value = ">75 Muy rápido";
                    break;

                case 2:
                    //  c42.Style["background-color"] = ColorTranslator.ToHtml(Color.Red);
                    //pReport.Dictionary.Variables["ML"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>39 Muy lento</div>";
                    //pReport.Dictionary.Variables["L"].Value = "<div style=' background-color:red; height:40px; padding-top:20px; text-align:center;'>40-59 Lento</div>";
                    //pReport.Dictionary.Variables["NE"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>60-74 Ningún extremo</div>";
                    //pReport.Dictionary.Variables["R"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>75 Rápido</div>";
                    //pReport.Dictionary.Variables["MR"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>>75 Muy rápido</div>";
                    pReport.Dictionary.Variables["ML"].Value = "39 Muy lento";
                    pReport.Dictionary.Variables["L"].Value = "40-59 Lento";
                    pReport.Dictionary.Variables["NE"].Value = "60-74 Ningún extremo";
                    pReport.Dictionary.Variables["R"].Value = "75 Rápido";
                    pReport.Dictionary.Variables["MR"].Value = ">75 Muy rápido";
                    break;

                case 3:
                    //  c43.Style["background-color"] = ColorTranslator.ToHtml(Color.Yellow);
                    //pReport.Dictionary.Variables["ML"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>39 Muy lento</div>";
                    //pReport.Dictionary.Variables["L"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>40-59 Lento</div>";
                    //pReport.Dictionary.Variables["NE"].Value = "<div style=' background-color:yellow; height:40px; padding-top:20px; text-align:center;'>60-74 Ningún extremo</div>";
                    //pReport.Dictionary.Variables["R"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>75 Rápido</div>";
                    //pReport.Dictionary.Variables["MR"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>>75 Muy rápido</div>";
                    pReport.Dictionary.Variables["ML"].Value = "39 Muy lento";
                    pReport.Dictionary.Variables["L"].Value = "40-59 Lento";
                    pReport.Dictionary.Variables["NE"].Value = "60-74 Ningún extremo";
                    pReport.Dictionary.Variables["R"].Value = "75 Rápido";
                    pReport.Dictionary.Variables["MR"].Value = ">75 Muy rápido";
                    break;

                case 4:
                    //  c44.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    //pReport.Dictionary.Variables["ML"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>39 Muy lento</div>";
                    //pReport.Dictionary.Variables["L"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>40-59 Lento</div>";
                    //pReport.Dictionary.Variables["NE"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>60-74 Ningún extremo</div>";
                    //pReport.Dictionary.Variables["R"].Value = "<div style=' background-color:green; height:40px; padding-top:20px; text-align:center;'>75 Rápido</div>";
                    //pReport.Dictionary.Variables["MR"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>>75 Muy rápido</div>";
                    pReport.Dictionary.Variables["ML"].Value = "39 Muy lento";
                    pReport.Dictionary.Variables["L"].Value = "40-59 Lento";
                    pReport.Dictionary.Variables["NE"].Value = "60-74 Ningún extremo";
                    pReport.Dictionary.Variables["R"].Value = "75 Rápido";
                    pReport.Dictionary.Variables["MR"].Value = ">75 Muy rápido";
                    break;

                case 5:
                    //  c45.Style["background-color"] = ColorTranslator.ToHtml(Color.Green);
                    //pReport.Dictionary.Variables["ML"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>39 Muy lento</div>";
                    //pReport.Dictionary.Variables["L"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>40-59 Lento</div>";
                    //pReport.Dictionary.Variables["NE"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>60-74 Ningún extremo</div>";
                    //pReport.Dictionary.Variables["R"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>75 Rápido</div>";
                    //pReport.Dictionary.Variables["MR"].Value = "<div style=' background-color:green; height:40px; padding-top:20px; text-align:center;'>>75 Muy rápido</div>";
                    pReport.Dictionary.Variables["ML"].Value = "39 Muy lento";
                    pReport.Dictionary.Variables["L"].Value = "40-59 Lento";
                    pReport.Dictionary.Variables["NE"].Value = "60-74 Ningún extremo";
                    pReport.Dictionary.Variables["R"].Value = "75 Rápido";
                    pReport.Dictionary.Variables["MR"].Value = ">75 Muy rápido";
                    break;

                default:
                    //pReport.Dictionary.Variables["ML"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>39 Muy lento</div>";
                    //pReport.Dictionary.Variables["L"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>40-59 Lento</div>";
                    //pReport.Dictionary.Variables["NE"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>60-74 Ningún extremo</div>";
                    //pReport.Dictionary.Variables["R"].Value = "<div style=' height:40px; padding-top:20px; text-align:center;'>75 Rápido</div>";
                    //pReport.Dictionary.Variables["MR"].Value = "<div style='  height:40px; padding-top:20px; text-align:center;'>>75 Muy rápido</div>";
                    pReport.Dictionary.Variables["ML"].Value = "39 Muy lento";
                    pReport.Dictionary.Variables["L"].Value = "40-59 Lento";
                    pReport.Dictionary.Variables["NE"].Value = "60-74 Ningún extremo";
                    pReport.Dictionary.Variables["R"].Value = "75 Rápido";
                    pReport.Dictionary.Variables["MR"].Value = ">75 Muy rápido";
                    break;
            }
        }

        private string DescripcionAdaptacion(int valor)
        {
            string desc = "";

            switch (valor)
            {
                case 0:
                    desc = "Inestabilidad";
                    break;
                case 1:
                    desc = "Optimo riguidez";
                    break;
                case 2:
                    desc = "Movilidad ideal";
                    break;
                case 3:
                    desc = "Movilidad esperada";
                    break;
            }

            return desc;
        }

        public string DescripcionNivelIngles(int x)
        {
            string nivel = "";
            switch (x)
            {
                case 1: nivel = "<p style=\"text-align:justify;\">La habilidad del evaluado en este nivel es extremadamente limitada o inexistente. Rara vez se comunica en inglés. Prácticamente sólo puede responder de forma no verbal a indicaciones, enunciados o preguntas que estén expresadas en forma simple.</p>"; break;
                case 2: nivel = "<p style=\"text-align:justify;\">En este nivel, el evaluado podrá entender conversaciones muy breves sobre temas sencillos. Se encuentra muy titubeante y demuestra dificultad para comunicarse. Para apoyar su conversación suele repetir y usar, gestos y comunicación no verbal, debido a lo limitado de sus habilidades y su vocabulario. Tal vez sea capaz de leer algún texto básico como anuncios o letreros con indicaciones básicas. Necesita apoyo contextual y visual que le ayude a comprender. Puede escribir notas sencillas utilizando un vocabulario básico y estructuras comunes de lenguaje. Algo característico de este nivel son los errores frecuentes al hablar, escribir, comprender o leer.</p>"; break;
                case 3: nivel = "<p style=\"text-align:justify;\">En este nivel, el evaluado puede entender un discurso estándar en casi cualquier contexto que contenga un poco de repetición y frases reelaboradas. Puede comunicarse oralmente en la mayoría de las situaciones de la vida cotidiana con un mínimo de dificultad. El candidato en este nivel puede comprender el contenido de muchos textos profesionales de forma independiente, pero no todos. Puede leer la literatura popular de su elección e incluso leerla por placer. Las expresiones idiomáticas y la jerga del inglés le causan dificultad y podría llegar a impedir la comunicación. Puede escribir reportes sencillos de varios párrafos, cartas y pasajes creativos, sin embargo, el estilo y el vocabulario pueden resultar limitados. Puede exponer sus ideas de forma organizada, pero aún se encuentran errores y titubeos ocasionales.</p>"; break;
                case 4: nivel = "<p style=\"text-align:justify;\">En este nivel, el evaluado tiene habilidades de lenguaje adecuadas para comunicarse tanto en situaciones de la vida cotidiana como en situaciones de la vida profesional. De forma ocasional, aún ocurren errores estructurales y léxicos. Aún puede tener dificultad con expresiones idiomáticas y palabras que tienen múltiples significados. También puede tener dificultad con estructuras complejas y conceptos académicos abstractos, pero es capaz de comunicarse en inglés en situaciones nuevas o desconocidas. Al escribir con un propósito específico lo hace de forma adecuada y continúa. Las estructuras, el vocabulario y la organización en general de su escritura, deberían de aproximarse a las de un angloparlante que esté en el mismo nivel académico. No obstante, aún es posible que ocurran errores aislados.</p>"; break;
                case 5: nivel = "<p style=\"text-align:justify;\">En este nivel, el evaluado puede entender y hablar el idioma adecuadamente tanto en conversaciones cotidianas como profesionales sin ningún titubeo o limitación. Tanto la pronunciación, como la fluidez son cercanas al nivel de un angloparlante. Es capaz de entender y utilizar apropiadamente expresiones idiomáticas y la jerga del inglés. Es capaz de leer y entender sin limitaciones todo tipo de lectura. En este nivel el candidato puede leer, hablar y escribir en inglés en casi cualquier situación profesional o de la vida cotidiana. Están presentes, si acaso, sólo algunos pequeños errores.</p>"; break;
                default: nivel = ""; break;
            }
            return nivel;
        }

        public string NivelIngles(int x)
        {
            string nivel = "";
            switch (x)
            {
                case 1: nivel = "Nivel Sin Inglés"; break;
                case 2: nivel = "Nivel Principiante"; break;
                case 3: nivel = "Nivel Intermedio Bajo"; break;
                case 4: nivel = "Nivel Intermedio Alto"; break;
                case 5: nivel = "Nivel Avanzado"; break;
                default: nivel = ""; break;
            }
            return nivel;
        }

        public string NivelTecnicaPc(int x)
        {
            string nivel = "";
            switch (x)
            {
                case 1: nivel = "Sobresaliente"; break;
                case 2: nivel = "Muy bien"; break;
                case 3: nivel = "Bien"; break;
                case 4: nivel = "Regular"; break;
                case 5: nivel = "Malo"; break;
                default: nivel = "No suficiente"; break;
            }
            return nivel;
        }

        public string coeficienteIntelectual(int x)
        {
            string coeficiente = "";

            switch (x)
            {
                case 1: coeficiente = "Debilidad mental profunda"; break;
                case 2: coeficiente = "Debilidad mental mediana"; break;
                case 3: coeficiente = "Debilidad mental superficial"; break;
                case 4: coeficiente = "Inteligencia limitrofe"; break;
                case 5: coeficiente = "Inteligencia normal"; break;
                case 6: coeficiente = "Inteligencia superior"; break;
                case 7: coeficiente = "Inteligencia sobresaliente"; break;
                default: coeficiente = "Inválido"; break;
            }
            return coeficiente;
        }

        public string Aprendizaje(int x)
        {
            string Aprendizaje = "";

            switch (x)
            {
                case 1: Aprendizaje = "Deficiente"; break;
                case 2: Aprendizaje = "Inferior"; break;
                case 3: Aprendizaje = "Término medio bajo"; break;
                case 4: Aprendizaje = "Término medio"; break;
                case 5: Aprendizaje = "Término medio alto"; break;
                case 6: Aprendizaje = "Superior"; break;
                case 7: Aprendizaje = "Sobresaliente"; break;
                default: Aprendizaje = "Inválido"; break;
            }
            return Aprendizaje;
        }

        public string NivelOrtografias(double x)
        {
            string vNivelOrtografias = "";

            if (x < 59.4)
            {
                vNivelOrtografias = "No suficiente";
            }

            else if (x > 59.5 && x < 69.4)
            {
                vNivelOrtografias = "Malo";
            }

            else if (x > 69.5 && x < 79.4)
            {
                vNivelOrtografias = "Regular";
            }

            else if (x > 79.5 && x < 89.4)
            {
                vNivelOrtografias = "Bien";
            }

            else if (x > 89.5 && x < 99.4)
            {
                vNivelOrtografias = "Muy bien";
            }

            else if (x > 99.5 && x < 100)
            {
                vNivelOrtografias = "Sobresaliente";
            }
            return vNivelOrtografias;
        }

        public void CargarResLaboral1()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;

            PruebasNegocio nSolicitud = new PruebasNegocio();
            SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
            candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);


            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria: vClToken, pIdBateria: vIdBateria);
            if (!xmlResultados.Equals(""))
            {
                XElement res = XElement.Parse(xmlResultados);
                vResultados = new List<E_RESULTADOS_BATERIA>();


                foreach (var element in res.Descendants("PRUEBA"))
                {
                    var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                     {
                         ID_BATERIA = 1,
                         XML_MENSAJES = "",
                         NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                         CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                         CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                     }).ToList();
                    vResultados.AddRange(ListaResultados);
                }



                report.Load(Server.MapPath("~/Assets/reports/IDP/ResultadosLaboral1.mrt"));
                report.Dictionary.Databases.Clear();


                pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

                var vResultadosLaboralI = vResultados.Where(r => r.CL_PRUEBA.Equals("LABORAL-1")).ToList();
                if (vResultadosLaboralI.Count > 0) //CORRECTO
                {
                    ///COTIDIANO
                    decimal? LABORAL1_REP_COTIDIANO_D = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-D")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_COTIDIANO_I = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-I")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_COTIDIANO_S = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-S")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_COTIDIANO_C = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-COTIDIANO-C")).FirstOrDefault().NO_VALOR;
                    ///MOTIVANTE
                    decimal? LABORAL1_REP_MOTIVANTE_D = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-D")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_MOTIVANTE_I = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-I")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_MOTIVANTE_S = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-S")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_MOTIVANTE_C = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-MOTIVANTE-C")).FirstOrDefault().NO_VALOR;
                    //PRESION
                    decimal? LABORAL1_REP_PRESION_D = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-D")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_PRESION_I = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-I")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_PRESION_S = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-S")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_PRESION_C = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-PRESION-C")).FirstOrDefault().NO_VALOR;
                    //TOTAL  MOTIVANTE
                    decimal? LABORAL1_REP_DM = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-DM")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_IM = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-IM")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_SM = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-SM")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_CM = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-CM")).FirstOrDefault().NO_VALOR;
                    //TOTAL PRESIONANTE
                    decimal? LABORAL1_REP_DL = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-DL")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_IL = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-IL")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_SL = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-SL")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_CL = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-CL")).FirstOrDefault().NO_VALOR;
                    //TOTAL COTIDIANO
                    decimal? LABORAL1_REP_DT = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-DT")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_IT = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-IT")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_ST = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-ST")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL1_REP_CT = vResultadosLaboralI.Where(x => x.CL_VARIABLE.Equals("LABORAL1-REP-CT")).FirstOrDefault().NO_VALOR;

                    decimal vNoValidez = 0;

                    //GRAFICA COTIDIANO

                    report.Dictionary.Variables["GTD"].Value = LABORAL1_REP_COTIDIANO_D.ToString();
                    report.Dictionary.Variables["GTI"].Value = LABORAL1_REP_COTIDIANO_I.ToString();
                    report.Dictionary.Variables["GTS"].Value = LABORAL1_REP_COTIDIANO_S.ToString();
                    report.Dictionary.Variables["GTC"].Value = LABORAL1_REP_COTIDIANO_C.ToString();


                    //  //GRAFICA MOTIVANTE
                    report.Dictionary.Variables["GMD"].Value = LABORAL1_REP_MOTIVANTE_D.ToString();
                    report.Dictionary.Variables["GMI"].Value = LABORAL1_REP_MOTIVANTE_I.ToString();
                    report.Dictionary.Variables["GMS"].Value = LABORAL1_REP_MOTIVANTE_S.ToString();
                    report.Dictionary.Variables["GMC"].Value = LABORAL1_REP_MOTIVANTE_C.ToString();



                    //  //GRAFICA PRESIONANTE
                    report.Dictionary.Variables["GLD"].Value = LABORAL1_REP_PRESION_D.ToString();
                    report.Dictionary.Variables["GLI"].Value = LABORAL1_REP_PRESION_I.ToString();
                    report.Dictionary.Variables["GLS"].Value = LABORAL1_REP_PRESION_S.ToString();
                    report.Dictionary.Variables["GLC"].Value = LABORAL1_REP_PRESION_C.ToString();



                    report.Dictionary.Variables["MD"].Value = LABORAL1_REP_DM.ToString();
                    report.Dictionary.Variables["MI"].Value = LABORAL1_REP_IM.ToString();
                    report.Dictionary.Variables["MS"].Value = LABORAL1_REP_SM.ToString();
                    report.Dictionary.Variables["MC"].Value = LABORAL1_REP_CM.ToString();
                    report.Dictionary.Variables["LD"].Value = LABORAL1_REP_DL.ToString();
                    report.Dictionary.Variables["LI"].Value = LABORAL1_REP_IL.ToString();
                    report.Dictionary.Variables["LS"].Value = LABORAL1_REP_SL.ToString();
                    report.Dictionary.Variables["LC"].Value = LABORAL1_REP_CL.ToString();
                    report.Dictionary.Variables["TD"].Value = LABORAL1_REP_DT.ToString();
                    report.Dictionary.Variables["TI"].Value = LABORAL1_REP_IT.ToString();
                    report.Dictionary.Variables["TS"].Value = LABORAL1_REP_ST.ToString();
                    report.Dictionary.Variables["TC"].Value = LABORAL1_REP_CT.ToString();

                    if (LABORAL1_REP_DT.HasValue)
                    {
                        vNoValidez = vNoValidez + LABORAL1_REP_DT.Value;
                    }

                    if (LABORAL1_REP_IT.HasValue)
                    {
                        vNoValidez = vNoValidez + LABORAL1_REP_IT.Value;
                    }

                    if (LABORAL1_REP_ST.HasValue)
                    {
                        vNoValidez = vNoValidez + LABORAL1_REP_ST.Value;
                    }

                    if (LABORAL1_REP_CT.HasValue)
                    {
                        vNoValidez = vNoValidez + LABORAL1_REP_CT.Value;
                    }

                    report.Dictionary.Variables["VALIDEZ"].Value = "<b>" + vNoValidez.ToString("N0") + "</b>";

                    List<E_PRUEBA_LABORAL_I> vListaMensajes = new List<E_PRUEBA_LABORAL_I>();
                    foreach (var element in res.Descendants("PRUEBA").Where(item => item.Attribute("CL_PRUEBA").Value.Equals("LABORAL-1")))
                    {
                        vListaMensajes = element.Element("MENSAJES").Elements("MENSAJE").Select(el => new E_PRUEBA_LABORAL_I
                        {
                            CL_MENSAJE = el.Attribute("CL_MENSAJE").Value,
                            NB_TITULO = el.Attribute("NB_TITULO").Value,
                            DS_CONCEPTO = el.Attribute("DS_MENSAJE").Value,
                            TIPO_MENSAJE = el.Attribute("TIPO_MENSAJE").Value,
                            SECCION = el.Attribute("SECCION").Value,
                            NO_ORDEN = el.Attribute("NO_ORDEN").Value
                        }).ToList();
                    }

                    if (vListaMensajes.Count > 0)
                    {
                        var vMensajCotidiano = vListaMensajes.Where(item => item.TIPO_MENSAJE.Contains("COTIDIANO")).OrderByDescending(t => int.Parse(t.NO_ORDEN)).ToList();
                        var vMensajMotivante = vListaMensajes.Where(item => item.TIPO_MENSAJE.Contains("MOTIVANTE")).ToList();
                        var vMensajPresion = vListaMensajes.Where(item => item.TIPO_MENSAJE.Contains("PRESION")).ToList();
                        string Cotidiano = "";
                        string Motivante = "";
                        string Presion = "";
                        string vCaractPersonal = "";
                        string vSituacionMotivante = "";
                        string vSituacionPresionante = "";
                        foreach (var item in vMensajCotidiano)
                        {
                            Cotidiano += "<b>" + item.NB_TITULO + "</b> </br>" + item.DS_CONCEPTO + "</br>";
                            vCaractPersonal += item.CL_MENSAJE + " ";
                        }

                        var vEncabezados = vMensajMotivante.Where(item => item.SECCION.Contains("HEAD")).ToList();
                        var vListaClaves = vMensajMotivante.Where(item => item.SECCION.Contains("LISTAS")).ToList();


                        Motivante = "<b>" + vListaClaves.Where(item => item.SECCION.Equals("LISTASA")).First().NB_TITULO + "</b> " + vEncabezados.Where(item => item.SECCION.Equals("HEADA")).First().DS_CONCEPTO + "</br> " + vListaClaves.Where(item => item.SECCION.Equals("LISTASA")).First().DS_CONCEPTO + "</br>" +
                                    "<b>" + vListaClaves.Where(item => item.SECCION.Equals("LISTASB")).First().NB_TITULO + "</b> " + vEncabezados.Where(item => item.SECCION.Equals("HEADB")).First().DS_CONCEPTO + "</br> " + vListaClaves.Where(item => item.SECCION.Equals("LISTASB")).First().DS_CONCEPTO + "</br>";

                        vSituacionMotivante = vEncabezados.Where(item => item.SECCION.Equals("HEADA")).First().CL_MENSAJE;
                        vSituacionPresionante = vMensajPresion.Where(item => item.SECCION.Equals("LISTASA")).First().CL_MENSAJE;

                        foreach (var item in vMensajPresion.Where(w => w.SECCION == "LISTASA"))
                        {
                            Presion += "<b>" + item.NB_TITULO + "</b> </br>" + item.DS_CONCEPTO + "</br>";
                        }

                        foreach (var item in vMensajPresion.Where(w => w.SECCION == "LISTASB"))
                        {
                            Presion += "<b>" + item.NB_TITULO + "</b> </br>" + item.DS_CONCEPTO + "</br>";
                        }

                        report.Dictionary.Variables["COTIDIANO"].Value = "<b>" + vCaractPersonal + "</b>";
                        report.Dictionary.Variables["MOTIVANTE"].Value = "<b>" + vSituacionMotivante + "</b>";
                        report.Dictionary.Variables["PRESIONANTE"].Value = "<b>" + vSituacionPresionante + "</b>";
                        report.Dictionary.Variables["DS_COTIDIANO"].Value = Cotidiano;
                        report.Dictionary.Variables["DS_MOTIVANTE"].Value = Motivante;
                        report.Dictionary.Variables["DS_PRESIONANTE"].Value = Presion;
                    }
                }

                report.Dictionary.Variables["NB_CANDIDATO"].Value = candidato.NB_CANDIDATO;
                report.Dictionary.Variables["CL_SOLICITUD"].Value = candidato.CL_SOLICITUD;
            }
            report.Compile();
            swvReporte.Report = report;
        }

        public void CargarInteres()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;

            PruebasNegocio nSolicitud = new PruebasNegocio();
            SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
            candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);


            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria: vClToken, pIdBateria: vIdBateria);
            if (!xmlResultados.Equals(""))
            {
                XElement res = XElement.Parse(xmlResultados);
                vResultados = new List<E_RESULTADOS_BATERIA>();


                foreach (var element in res.Descendants("PRUEBA"))
                {
                    var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                    {
                        ID_BATERIA = 1,
                        XML_MENSAJES = "",
                        NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                        CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                        CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                    }).ToList();
                    vResultados.AddRange(ListaResultados);
                }



                report.Load(Server.MapPath("~/Assets/reports/IDP/ResultadosInteresesPersonales.mrt"));
                report.Dictionary.Databases.Clear();


                pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

                var vResultadosInteres = vResultados.Where(r => r.CL_PRUEBA.Equals("INTERES")).ToList();
                if (vResultadosInteres.Count > 0)
                {
                    decimal? INTERES_REP_T = vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_T")).FirstOrDefault().NO_VALOR;
                    decimal? INTERES_REP_E = vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_E")).FirstOrDefault().NO_VALOR;
                    decimal? INTERES_REP_A = vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_A")).FirstOrDefault().NO_VALOR;
                    decimal? INTERES_REP_S = vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_S")).FirstOrDefault().NO_VALOR;
                    decimal? INTERES_REP_P = vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_P")).FirstOrDefault().NO_VALOR;
                    decimal? INTERES_REP_R = vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_R")).FirstOrDefault().NO_VALOR;
                    decimal? INTERES_REP_TS = vResultadosInteres.Where(x => x.CL_VARIABLE.Equals("INTERES_REP_TS")).FirstOrDefault().NO_VALOR;




                    if (INTERES_REP_T != null)
                        report.Dictionary.Variables["TT"].Value = INTERES_REP_T.ToString() + "%";
                    if (INTERES_REP_E != null)
                        report.Dictionary.Variables["TE"].Value = INTERES_REP_E.ToString() + "%";
                    if (INTERES_REP_A != null)
                        report.Dictionary.Variables["TA"].Value = INTERES_REP_A.ToString() + "%";
                    if (INTERES_REP_S != null)
                        report.Dictionary.Variables["TS"].Value = INTERES_REP_S.ToString() + "%";
                    if (INTERES_REP_P != null)
                        report.Dictionary.Variables["TP"].Value = INTERES_REP_P.ToString() + "%";
                    if (INTERES_REP_R != null)
                        report.Dictionary.Variables["TR"].Value = INTERES_REP_R.ToString() + "%";

                    if (INTERES_REP_T != null)
                        report.Dictionary.Variables["GT"].Value = INTERES_REP_T.ToString();
                    if (INTERES_REP_E != null)
                        report.Dictionary.Variables["GE"].Value = INTERES_REP_E.ToString();
                    if (INTERES_REP_A != null)
                        report.Dictionary.Variables["GA"].Value = INTERES_REP_A.ToString();
                    if (INTERES_REP_S != null)
                        report.Dictionary.Variables["GS"].Value = INTERES_REP_S.ToString();
                    if (INTERES_REP_P != null)
                        report.Dictionary.Variables["GP"].Value = INTERES_REP_P.ToString();
                    if (INTERES_REP_R != null)
                        report.Dictionary.Variables["GR"].Value = INTERES_REP_R.ToString();

                }

                report.Dictionary.Variables["NB_CANDIDATO"].Value = candidato.NB_CANDIDATO;
                report.Dictionary.Variables["CL_SOLICITUD"].Value = candidato.CL_SOLICITUD;
            }
            report.Compile();
            swvReporte.Report = report;
        }

        public void CargarEstilo()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;

            PruebasNegocio nSolicitud = new PruebasNegocio();
            SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
            candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);


            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria: vClToken, pIdBateria: vIdBateria);
            if (!xmlResultados.Equals(""))
            {
                XElement res = XElement.Parse(xmlResultados);
                vResultados = new List<E_RESULTADOS_BATERIA>();


                foreach (var element in res.Descendants("PRUEBA"))
                {
                    var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                    {
                        ID_BATERIA = 1,
                        XML_MENSAJES = "",
                        NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                        CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                        CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                    }).ToList();
                    vResultados.AddRange(ListaResultados);
                }



                report.Load(Server.MapPath("~/Assets/reports/IDP/ResultadosEstilo.mrt"));
                report.Dictionary.Databases.Clear();


                pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));


                var vResultadosPensamiento = vResultados.Where(r => r.CL_PRUEBA.Equals("PENSAMIENTO")).ToList();

                if (vResultadosPensamiento.Count > 0)
                {
                    decimal? PENSAMIENTO_REP_A = vResultadosPensamiento.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_A")).FirstOrDefault().NO_VALOR;
                    decimal? PENSAMIENTO_REP_L = vResultadosPensamiento.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_L")).FirstOrDefault().NO_VALOR;
                    decimal? PENSAMIENTO_REP_I = vResultadosPensamiento.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_I")).FirstOrDefault().NO_VALOR;
                    decimal? PENSAMIENTO_REP_V = vResultadosPensamiento.Where(x => x.CL_VARIABLE.Equals("PENSAMIENTO_REP_V")).FirstOrDefault().NO_VALOR;

                    report.Dictionary.Variables["TA"].Value = PENSAMIENTO_REP_A.ToString() + "%";
                    report.Dictionary.Variables["TV"].Value = PENSAMIENTO_REP_V.ToString() + "%";
                    report.Dictionary.Variables["TI"].Value = PENSAMIENTO_REP_I.ToString() + "%";
                    report.Dictionary.Variables["TL"].Value = PENSAMIENTO_REP_L.ToString() + "%";


                    string strFileName = HttpContext.Current.Server.MapPath("~") + "/Assets/images/EstiloPensamientoCopy.jpg";

                    using (var bmp = new Bitmap(strFileName))
                    using (var gr = Graphics.FromImage(bmp))
                    {
                        int Ax = (int)(vPtCenterX);
                        int Ay = (int)(vPtCenterY - (PENSAMIENTO_REP_A * vNoEscala / 100));
                        int Vx = (int)(vPtCenterX + (PENSAMIENTO_REP_V * vNoEscala / 100));
                        int Vy = (int)(vPtCenterY);
                        int Ix = (int)(vPtCenterX);
                        int Iy = (int)(vPtCenterY + (PENSAMIENTO_REP_I * vNoEscala / 100));
                        int Lx = (int)(vPtCenterX - (PENSAMIENTO_REP_L * vNoEscala / 100));
                        int Ly = (int)(vPtCenterY);

                        gr.DrawLine(new Pen(Color.Red, 1), Ax, Ay, Vx, Vy);
                        gr.DrawLine(new Pen(Color.Red, 1), Vx, Vy, Ix, Iy);
                        gr.DrawLine(new Pen(Color.Red, 1), Ix, Iy, Lx, Ly);
                        gr.DrawLine(new Pen(Color.Red, 1), Lx, Ly, Ax, Ay);
                        //var path = System.IO.Path.Combine(
                        //    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        //    "Example.png");
                        //bmp.Save(path);
                        //string vValue = Convert.ToBase64String(bmp);
                        MemoryStream oMStream = new MemoryStream();
                        bmp.Save(oMStream, System.Drawing.Imaging.ImageFormat.Bmp);
                        byte[] imageBytes = oMStream.ToArray();
                        string vImagen = Convert.ToBase64String(imageBytes);

                        report.Dictionary.Variables["IMG"].Value = vImagen;
                    }





                    report.Dictionary.Variables["A"].Value = PENSAMIENTO_REP_A.ToString();
                    report.Dictionary.Variables["V"].Value = PENSAMIENTO_REP_V.ToString();
                    report.Dictionary.Variables["I"].Value = PENSAMIENTO_REP_I.ToString();
                    report.Dictionary.Variables["L"].Value = PENSAMIENTO_REP_L.ToString();
                }

                report.Dictionary.Variables["NB_CANDIDATO"].Value = candidato.NB_CANDIDATO;
                report.Dictionary.Variables["CL_SOLICITUD"].Value = candidato.CL_SOLICITUD;
            }
            report.Compile();
            swvReporte.Report = report;
        }

        public void CargarAptitud1()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;

            PruebasNegocio nSolicitud = new PruebasNegocio();
            SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
            candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);


            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria: vClToken, pIdBateria: vIdBateria);
            if (!xmlResultados.Equals(""))
            {
                XElement res = XElement.Parse(xmlResultados);
                vResultados = new List<E_RESULTADOS_BATERIA>();


                foreach (var element in res.Descendants("PRUEBA"))
                {
                    var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                    {
                        ID_BATERIA = 1,
                        XML_MENSAJES = "",
                        NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                        CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                        CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                    }).ToList();
                    vResultados.AddRange(ListaResultados);
                }



                report.Load(Server.MapPath("~/Assets/reports/IDP/ResultadosAptitud1.mrt"));
                report.Dictionary.Databases.Clear();


                pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

                var vResultadosAptitudMental1 = vResultados.Where(r => r.CL_PRUEBA.Equals("APTITUD-1")).ToList();
                if (vResultadosAptitudMental1.Count > 0)
                {
                    decimal? APTITUD1_REP_0001 = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0001")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD1_REP_0002 = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0002")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD1_REP_0003 = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0003")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD1_REP_0004 = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0004")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD1_REP_0005 = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0005")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD1_REP_0006 = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0006")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD1_REP_0007 = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0007")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD1_REP_0008 = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0008")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD1_REP_0009 = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0009")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD1_REP_0010 = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_0010")).FirstOrDefault().NO_VALOR;


                    decimal? APTITUD1_REP_TOTAL = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_TOTAL")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD1_REP_CI = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD1_REP_CI")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD_REP_DESC_TOTAL = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD_REP_DESC_TOTAL")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD_REP_DESC_CI = vResultadosAptitudMental1.Where(x => x.CL_VARIABLE.Equals("APTITUD_REP_DESC_CI")).FirstOrDefault().NO_VALOR;


                    report.Dictionary.Variables["TDCI"].Value = coeficienteIntelectual((int)APTITUD_REP_DESC_CI);
                    report.Dictionary.Variables["TDCA"].Value = Aprendizaje((int)APTITUD_REP_DESC_TOTAL);
                    report.Dictionary.Variables["TP"].Value = APTITUD1_REP_TOTAL.ToString();
                    report.Dictionary.Variables["CI"].Value = APTITUD1_REP_CI.ToString();


                    report.Dictionary.Variables["CONOCIMIENTO"].Value = APTITUD1_REP_0001.ToString();
                    report.Dictionary.Variables["COMPRENSION"].Value = APTITUD1_REP_0002.ToString();
                    report.Dictionary.Variables["SIGNIFICADOS"].Value = APTITUD1_REP_0003.ToString();
                    report.Dictionary.Variables["LOGICA"].Value = APTITUD1_REP_0004.ToString();
                    report.Dictionary.Variables["ARITMETICA"].Value = APTITUD1_REP_0005.ToString();
                    report.Dictionary.Variables["JUICIO"].Value = APTITUD1_REP_0006.ToString();
                    report.Dictionary.Variables["ANALOGIAS"].Value = APTITUD1_REP_0007.ToString();
                    report.Dictionary.Variables["ORDENAMIENTO"].Value = APTITUD1_REP_0008.ToString();
                    report.Dictionary.Variables["CLASIFICACION"].Value = APTITUD1_REP_0009.ToString();
                    report.Dictionary.Variables["SERIACION"].Value = APTITUD1_REP_0010.ToString();

                }



                report.Dictionary.Variables["NB_CANDIDATO"].Value = candidato.NB_CANDIDATO;
                report.Dictionary.Variables["CL_SOLICITUD"].Value = candidato.CL_SOLICITUD;
            }
            report.Compile();
            swvReporte.Report = report;
        }

        public void CargarAptitud2()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;

            PruebasNegocio nSolicitud = new PruebasNegocio();
            SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
            candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);


            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria: vClToken, pIdBateria: vIdBateria);
            if (!xmlResultados.Equals(""))
            {
                XElement res = XElement.Parse(xmlResultados);
                vResultados = new List<E_RESULTADOS_BATERIA>();


                foreach (var element in res.Descendants("PRUEBA"))
                {
                    var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                    {
                        ID_BATERIA = 1,
                        XML_MENSAJES = "",
                        NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                        CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                        CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                    }).ToList();
                    vResultados.AddRange(ListaResultados);
                }



                report.Load(Server.MapPath("~/Assets/reports/IDP/ResultadosAptitud2.mrt"));
                report.Dictionary.Databases.Clear();


                pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

                var vResultadosAptitud2 = vResultados.Where(r => r.CL_PRUEBA.Equals("APTITUD-2")).ToList();
                if (vResultadosAptitud2.Count > 0)
                {
                    decimal? APTITUD2_REP_CI = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CI")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_ACIERTOS = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ACIERTOS")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_DESC_CI = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_DESC_CI")).FirstOrDefault().NO_VALOR;

                    decimal? APTITUD2_REP_CONOCIMIENTO = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CONOCIMIENTO")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_COMPRENSION = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_COMPRENSION")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_SIGNIFICADO = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_SIGNIFICADO")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_LOGICA = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_LOGICA")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_ARITMETICA = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ARITMETICA")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_JUICIO = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_JUICIO")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_ANALOGIAS = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ANALOGIAS")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_ORDENAMIENTO = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_ORDENAMIENTO")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_CLASIFICACION = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_CLASIFICACION")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_SERIACION = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_SERIACION")).FirstOrDefault().NO_VALOR;

                    report.Dictionary.Variables["PD"].Value = APTITUD2_REP_ACIERTOS.Value.ToString("N0");
                    report.Dictionary.Variables["CI"].Value = APTITUD2_REP_CI.Value.ToString("N0");
                    report.Dictionary.Variables["INTELIGENCIA"].Value = coeficienteIntelectual(int.Parse(APTITUD2_REP_DESC_CI.Value.ToString("N0")));

                    report.Dictionary.Variables["CONOCIMIENTO"].Value = APTITUD2_REP_CONOCIMIENTO == -100 ? "0" : APTITUD2_REP_CONOCIMIENTO.ToString();
                    report.Dictionary.Variables["COMPRENSION"].Value = APTITUD2_REP_COMPRENSION == -100 ? "0" : APTITUD2_REP_COMPRENSION.ToString();
                    report.Dictionary.Variables["SIGNIFICADOS"].Value = APTITUD2_REP_SIGNIFICADO == -100 ? "0" : APTITUD2_REP_SIGNIFICADO.ToString();
                    report.Dictionary.Variables["LOGICA"].Value = APTITUD2_REP_LOGICA == -100 ? "0" : APTITUD2_REP_LOGICA.ToString();
                    report.Dictionary.Variables["ARITMETICA"].Value = APTITUD2_REP_ARITMETICA == -100 ? "0" : APTITUD2_REP_ARITMETICA.ToString();
                    report.Dictionary.Variables["JUICIO"].Value = APTITUD2_REP_JUICIO == -100 ? "0" : APTITUD2_REP_JUICIO.ToString();
                    report.Dictionary.Variables["ANALOGIAS"].Value = APTITUD2_REP_ANALOGIAS == -100 ? "0" : APTITUD2_REP_ANALOGIAS.ToString();
                    report.Dictionary.Variables["ORDENAMIENTO"].Value = APTITUD2_REP_ORDENAMIENTO == -100 ? "0" : APTITUD2_REP_ORDENAMIENTO.ToString();
                    report.Dictionary.Variables["CLASIFICACION"].Value = APTITUD2_REP_CLASIFICACION == -100 ? "0" : APTITUD2_REP_CLASIFICACION.ToString();
                    report.Dictionary.Variables["SERIACION"].Value = APTITUD2_REP_SERIACION == -100 ? "0" : APTITUD2_REP_SERIACION.ToString();



                    decimal? APTITUD2_REP_C1 = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_C1")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_C2 = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_C2")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_C3 = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_C3")).FirstOrDefault().NO_VALOR;
                    decimal? APTITUD2_REP_C4 = vResultadosAptitud2.Where(x => x.CL_VARIABLE.Equals("APTITUD2_REP_C4")).FirstOrDefault().NO_VALOR;


                    configurarComparacion1(int.Parse(APTITUD2_REP_C1.Value.ToString("N0")), report);
                    configurarComparacion2(int.Parse(APTITUD2_REP_C2.Value.ToString("N0")), report);
                    configurarComparacion3(int.Parse(APTITUD2_REP_C3.Value.ToString("N0")), report);
                    configurarComparacion4(int.Parse(APTITUD2_REP_C4.Value.ToString("N0")), report);
                }
                else
                {
                    configurarComparacion1(6, report);
                    configurarComparacion2(11, report);
                    configurarComparacion3(6, report);
                    configurarComparacion4(6, report);
                }

                report.Dictionary.Variables["NB_CANDIDATO"].Value = candidato.NB_CANDIDATO;
                report.Dictionary.Variables["CL_SOLICITUD"].Value = candidato.CL_SOLICITUD;
            }
            report.Compile();
            swvReporte.Report = report;
        }

        public void CargarOrtografia()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;

            PruebasNegocio nSolicitud = new PruebasNegocio();
            SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
            candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);


            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria: vClToken, pIdBateria: vIdBateria);
            if (!xmlResultados.Equals(""))
            {
                XElement res = XElement.Parse(xmlResultados);
                vResultados = new List<E_RESULTADOS_BATERIA>();


                foreach (var element in res.Descendants("PRUEBA"))
                {
                    var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                    {
                        ID_BATERIA = 1,
                        XML_MENSAJES = "",
                        NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                        CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                        CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                    }).ToList();
                    vResultados.AddRange(ListaResultados);
                }



                report.Load(Server.MapPath("~/Assets/reports/IDP/ResultadosOrtografia.mrt"));
                report.Dictionary.Databases.Clear();


                pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

                var vResultadosORTOGRAFIAI = vResultados.Where(r => r.CL_PRUEBA.Equals("ORTOGRAFIA-1")).ToList();
                var vResultadosORTOGRAFIAII = vResultados.Where(r => r.CL_PRUEBA.Equals("ORTOGRAFIA-2")).ToList();
                var vResultadosORTOGRAFIAIII = vResultados.Where(r => r.CL_PRUEBA.Equals("ORTOGRAFIA-3")).ToList();
                if (vResultadosORTOGRAFIAI.Count > 0)
                {
                    ORTOGRAFIA1_TOTAL = vResultadosORTOGRAFIAI.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA1-REP-TOTAL")).FirstOrDefault().NO_VALOR;
                    ORTOGRAFIA1_ACIERTOS = vResultadosORTOGRAFIAI.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA1-REP-#A")).FirstOrDefault().NO_VALOR;
                    report.Dictionary.Variables["A1"].Value = (Math.Round((decimal)ORTOGRAFIA1_ACIERTOS, 0)).ToString();
                    report.Dictionary.Variables["P1"].Value = ORTOGRAFIA1_TOTAL.ToString() + "%";
                    report.Dictionary.Variables["ORT1"].Value = ORTOGRAFIA1_TOTAL.ToString();
                }
                else
                {
                    report.Dictionary.Variables["A1"].Value = "0";
                    report.Dictionary.Variables["P1"].Value = "0";
                    report.Dictionary.Variables["ORT1"].Value = "0";
                }

                if (vResultadosORTOGRAFIAII.Count > 0)
                {
                    ORTOGRAFIA2_TOTAL = vResultadosORTOGRAFIAII.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA2-REP-TOTAL")).FirstOrDefault().NO_VALOR;
                    ORTOGRAFIA2_ACIERTOS = vResultadosORTOGRAFIAII.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA2-REP-#A")).FirstOrDefault().NO_VALOR;
                    report.Dictionary.Variables["A2"].Value = (Math.Round((decimal)ORTOGRAFIA2_ACIERTOS, 0)).ToString();
                    report.Dictionary.Variables["P2"].Value = (ORTOGRAFIA2_TOTAL).ToString() + "%";
                    report.Dictionary.Variables["ORT2"].Value = (ORTOGRAFIA2_TOTAL).ToString();
                }
                else
                {
                    report.Dictionary.Variables["A2"].Value = "0";
                    report.Dictionary.Variables["P2"].Value = "0";
                    report.Dictionary.Variables["ORT2"].Value = "0";
                }

                if (vResultadosORTOGRAFIAIII.Count > 0)
                {
                    ORTOGRAFIA3_TOTAL = vResultadosORTOGRAFIAIII.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA3-REP-TOTAL")).FirstOrDefault().NO_VALOR;
                    ORTOGRAFIA3_ACIERTOS = vResultadosORTOGRAFIAIII.Where(x => x.CL_VARIABLE.Equals("ORTOGRAFIA3-REP-#A")).FirstOrDefault().NO_VALOR;
                    report.Dictionary.Variables["A3"].Value = (Math.Round((decimal)ORTOGRAFIA3_ACIERTOS, 0)).ToString();
                    report.Dictionary.Variables["P3"].Value = (ORTOGRAFIA3_TOTAL).ToString() + "%";
                    report.Dictionary.Variables["ORT3"].Value = (ORTOGRAFIA3_TOTAL).ToString();
                }
                else
                {
                    report.Dictionary.Variables["A3"].Value = "0";
                    report.Dictionary.Variables["P3"].Value = "0";
                    report.Dictionary.Variables["ORT3"].Value = "0";
                }


                report.Dictionary.Variables["AT"].Value = (Math.Round((decimal)(((ORTOGRAFIA1_ACIERTOS != null) ? ORTOGRAFIA1_ACIERTOS : 0) + ((ORTOGRAFIA2_ACIERTOS != null) ? ORTOGRAFIA2_ACIERTOS : 0) + ((ORTOGRAFIA3_ACIERTOS != null) ? ORTOGRAFIA3_ACIERTOS : 0)), 0)).ToString();
                report.Dictionary.Variables["PT"].Value = (Math.Round((decimal)(((ORTOGRAFIA1_ACIERTOS != null) ? ORTOGRAFIA1_ACIERTOS : 0) + ((ORTOGRAFIA2_ACIERTOS != null) ? ORTOGRAFIA2_ACIERTOS : 0) + ((ORTOGRAFIA3_ACIERTOS != null) ? ORTOGRAFIA3_ACIERTOS : 0)) * 100 / 83, 2)).ToString() + "%";



                PORCENTAJE_TOTAL = (Math.Round((double)(((ORTOGRAFIA1_ACIERTOS != null) ? ORTOGRAFIA1_ACIERTOS : 0) + ((ORTOGRAFIA2_ACIERTOS != null) ? ORTOGRAFIA2_ACIERTOS : 0) + ((ORTOGRAFIA3_ACIERTOS != null) ? ORTOGRAFIA3_ACIERTOS : 0)) * 100 / 83, 2));
                NIVEL_ORT = NivelOrtografias(PORCENTAJE_TOTAL);
                report.Dictionary.Variables["TOTAL"].Value = PORCENTAJE_TOTAL.ToString() + "%";
                report.Dictionary.Variables["DS_TOTAL"].Value = NIVEL_ORT;

                report.Dictionary.Variables["NB_CANDIDATO"].Value = candidato.NB_CANDIDATO;
                report.Dictionary.Variables["CL_SOLICITUD"].Value = candidato.CL_SOLICITUD;
            }
            report.Compile();
            swvReporte.Report = report;
        }

        public void CargarTecnica()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;

            PruebasNegocio nSolicitud = new PruebasNegocio();
            SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
            candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);


            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria: vClToken, pIdBateria: vIdBateria);
            if (!xmlResultados.Equals(""))
            {
                XElement res = XElement.Parse(xmlResultados);
                vResultados = new List<E_RESULTADOS_BATERIA>();


                foreach (var element in res.Descendants("PRUEBA"))
                {
                    var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                    {
                        ID_BATERIA = 1,
                        XML_MENSAJES = "",
                        NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                        CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                        CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                    }).ToList();
                    vResultados.AddRange(ListaResultados);
                }



                report.Load(Server.MapPath("~/Assets/reports/IDP/ResultadosTecnicaPc.mrt"));
                report.Dictionary.Databases.Clear();


                pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

                var vResultadosTecnicaPC = vResultados.Where(r => r.CL_PRUEBA.Equals("TECNICAPC")).ToList();
                if (vResultadosTecnicaPC.Count > 0)
                {
                    decimal? TECNICAPC_REP_C = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_C")).FirstOrDefault().NO_VALOR;
                    decimal? TECNICAPC_REP_S = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_S")).FirstOrDefault().NO_VALOR;
                    decimal? TECNICAPC_REP_I = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_I")).FirstOrDefault().NO_VALOR;
                    decimal? TECNICAPC_REP_H = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_H")).FirstOrDefault().NO_VALOR;

                    decimal? TECNICAPC_REP_P_C = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_P_C")).FirstOrDefault().NO_VALOR;
                    decimal? TECNICAPC_REP_P_S = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_P_S")).FirstOrDefault().NO_VALOR;
                    decimal? TECNICAPC_REP_P_I = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_P_I")).FirstOrDefault().NO_VALOR;
                    decimal? TECNICAPC_REP_P_H = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_P_H")).FirstOrDefault().NO_VALOR;

                    decimal? TECNICAPC_REP_NIVEL_C = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_C")).FirstOrDefault().NO_VALOR;
                    decimal? TECNICAPC_REP_NIVEL_S = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_S")).FirstOrDefault().NO_VALOR;
                    decimal? TECNICAPC_REP_NIVEL_I = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_I")).FirstOrDefault().NO_VALOR;
                    decimal? TECNICAPC_REP_NIVEL_H = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_NIVEL_H")).FirstOrDefault().NO_VALOR;

                    decimal? TECNICAPC_REP_T = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_T")).FirstOrDefault().NO_VALOR;
                    decimal? TECNICAPC_REP_P_T = vResultadosTecnicaPC.Where(x => x.CL_VARIABLE.Equals("TECNICAPC_REP_P_T")).FirstOrDefault().NO_VALOR;



                    report.Dictionary.Variables["COM"].Value = TECNICAPC_REP_P_C.ToString();
                    report.Dictionary.Variables["SOFT"].Value = TECNICAPC_REP_P_S.ToString();
                    report.Dictionary.Variables["INT"].Value = TECNICAPC_REP_P_I.ToString();
                    report.Dictionary.Variables["HARD"].Value = TECNICAPC_REP_P_H.ToString();

                    report.Dictionary.Variables["CP"].Value = TECNICAPC_REP_P_T.ToString();

                    report.Dictionary.Variables["CA"].Value = (Math.Round((decimal)TECNICAPC_REP_C, 0)).ToString();
                    report.Dictionary.Variables["SA"].Value = (Math.Round((decimal)TECNICAPC_REP_S, 0)).ToString();
                    report.Dictionary.Variables["IA"].Value = (Math.Round((decimal)TECNICAPC_REP_I, 0)).ToString();
                    report.Dictionary.Variables["HA"].Value = (Math.Round((decimal)TECNICAPC_REP_H, 0)).ToString();
                    report.Dictionary.Variables["TA"].Value = (Math.Round((decimal)TECNICAPC_REP_T, 0)).ToString();

                    report.Dictionary.Variables["DS_COM"].Value = NivelTecnicaPc((int)TECNICAPC_REP_NIVEL_C);
                    report.Dictionary.Variables["DS_SOFT"].Value = NivelTecnicaPc((int)TECNICAPC_REP_NIVEL_S);
                    report.Dictionary.Variables["DS_INT"].Value = NivelTecnicaPc((int)TECNICAPC_REP_NIVEL_I);
                    report.Dictionary.Variables["DS_HARD"].Value = NivelTecnicaPc((int)TECNICAPC_REP_NIVEL_H);
                }

                report.Dictionary.Variables["NB_CANDIDATO"].Value = candidato.NB_CANDIDATO;
                report.Dictionary.Variables["CL_SOLICITUD"].Value = candidato.CL_SOLICITUD;
            }
            report.Compile();
            swvReporte.Report = report;
        }

        public void CargarIngles()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;

            PruebasNegocio nSolicitud = new PruebasNegocio();
            SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
            candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);


            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria: vClToken, pIdBateria: vIdBateria);
            if (!xmlResultados.Equals(""))
            {
                XElement res = XElement.Parse(xmlResultados);
                vResultados = new List<E_RESULTADOS_BATERIA>();


                foreach (var element in res.Descendants("PRUEBA"))
                {
                    var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                    {
                        ID_BATERIA = 1,
                        XML_MENSAJES = "",
                        NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                        CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                        CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                    }).ToList();
                    vResultados.AddRange(ListaResultados);
                }



                report.Load(Server.MapPath("~/Assets/reports/IDP/ResultadosIngles.mrt"));
                report.Dictionary.Databases.Clear();


                pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

                var vResultadosIngles = vResultados.Where(r => r.CL_PRUEBA.Equals("INGLES")).ToList();
                if (vResultadosIngles.Count > 0)
                {
                    decimal? INGLES_REP_TOTAL = vResultadosIngles.Where(x => x.CL_VARIABLE.Equals("INGLES_REP_TOTAL")).FirstOrDefault().NO_VALOR;
                    decimal? INGLES_NIVEL = vResultadosIngles.Where(x => x.CL_VARIABLE.Equals("INGLES_REP_NIVEL")).FirstOrDefault().NO_VALOR;
                    decimal? INGLES_TOTAL = vResultadosIngles.Where(x => x.CL_VARIABLE.Equals("INGLES_TOTAL")).FirstOrDefault().NO_VALOR;
                    report.Dictionary.Variables["TITULO"].Value = NivelIngles((int)INGLES_NIVEL);
                    report.Dictionary.Variables["DS_NIVEL"].Value = DescripcionNivelIngles((int)INGLES_NIVEL); //INGLES_NIVEL
                    report.Dictionary.Variables["ACIERTOS"].Value = ((int)INGLES_TOTAL).ToString();
                    report.Dictionary.Variables["PORCENTAJE"].Value = ((decimal)INGLES_REP_TOTAL).ToString() + "%";

                }

                report.Dictionary.Variables["NB_CANDIDATO"].Value = candidato.NB_CANDIDATO;
                report.Dictionary.Variables["CL_SOLICITUD"].Value = candidato.CL_SOLICITUD;
            }
            report.Compile();
            swvReporte.Report = report;
        }

        public void CargarAdaptacion()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;

            PruebasNegocio nSolicitud = new PruebasNegocio();
            SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
            candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);


            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria: vClToken, pIdBateria: vIdBateria);
            if (!xmlResultados.Equals(""))
            {
                XElement res = XElement.Parse(xmlResultados);
                vResultados = new List<E_RESULTADOS_BATERIA>();


                foreach (var element in res.Descendants("PRUEBA"))
                {
                    var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                    {
                        ID_BATERIA = 1,
                        XML_MENSAJES = "",
                        NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                        CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                        CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                    }).ToList();
                    vResultados.AddRange(ListaResultados);
                }



                report.Load(Server.MapPath("~/Assets/reports/IDP/ResultadosAdaptacion.mrt"));
                report.Dictionary.Databases.Clear();


                pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

                var vResultadosAdaptacion = vResultados.Where(r => r.CL_PRUEBA.Equals("ADAPTACION")).ToList();
                if (vResultadosAdaptacion.Count > 0)
                {

                    decimal vP2 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P2")).FirstOrDefault().NO_VALOR.Value;
                    decimal vP3 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P3")).FirstOrDefault().NO_VALOR.Value;
                    decimal vP4 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P4")).FirstOrDefault().NO_VALOR.Value;
                    decimal vP5 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P5")).FirstOrDefault().NO_VALOR.Value;
                    decimal vP1 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P1")).FirstOrDefault().NO_VALOR.Value;
                    decimal vP6 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P6")).FirstOrDefault().NO_VALOR.Value;
                    decimal vP0 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P0")).FirstOrDefault().NO_VALOR.Value;
                    decimal vP7 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_P7")).FirstOrDefault().NO_VALOR.Value;

                    decimal vA2 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A2")).FirstOrDefault().NO_VALOR.Value;
                    decimal vA3 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A3")).FirstOrDefault().NO_VALOR.Value;
                    decimal vA4 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A4")).FirstOrDefault().NO_VALOR.Value;
                    decimal vA5 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A5")).FirstOrDefault().NO_VALOR.Value;
                    decimal vA1 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A1")).FirstOrDefault().NO_VALOR.Value;
                    decimal vA6 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A6")).FirstOrDefault().NO_VALOR.Value;
                    decimal vA0 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A0")).FirstOrDefault().NO_VALOR.Value;
                    decimal vA7 = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_A7")).FirstOrDefault().NO_VALOR.Value;


                    report.Dictionary.Variables["PP"].Value = vP2.ToString("N0");
                    report.Dictionary.Variables["PE"].Value = vP3.ToString("N0");
                    report.Dictionary.Variables["PS"].Value = vP4.ToString("N0");
                    report.Dictionary.Variables["PC"].Value = vP5.ToString("N0");
                    report.Dictionary.Variables["PA"].Value = vP1.ToString("N0");
                    report.Dictionary.Variables["PV"].Value = vP6.ToString("N0");
                    report.Dictionary.Variables["PPA"].Value = vP0.ToString("N0");
                    report.Dictionary.Variables["PSA"].Value = vP7.ToString("N0");

                    report.Dictionary.Variables["AP"].Value = vA2.ToString("N0");
                    report.Dictionary.Variables["AE"].Value = vA3.ToString("N0");
                    report.Dictionary.Variables["AS"].Value = vA4.ToString("N0");
                    report.Dictionary.Variables["AC"].Value = vA5.ToString("N0");
                    report.Dictionary.Variables["AA"].Value = vA1.ToString("N0");
                    report.Dictionary.Variables["AV"].Value = vA6.ToString("N0");
                    report.Dictionary.Variables["APA"].Value = vA0.ToString("N0");
                    report.Dictionary.Variables["ASA"].Value = vA7.ToString("N0");


                    //ColumnSeries csPersonalidad = new ColumnSeries();
                    //csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP2, Color.FromArgb(0, 153, 0)));
                    //csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP3, Color.FromArgb(204, 0, 0)));
                    //csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP4, Color.FromArgb(255, 255, 0)));
                    //csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP5, Color.FromArgb(153, 0, 153)));
                    //csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP1, Color.FromArgb(102, 0, 204)));
                    //csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP6, Color.FromArgb(153, 76, 0)));
                    //csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP0, Color.FromArgb(255, 255, 153)));
                    //csPersonalidad.SeriesItems.Add(new CategorySeriesItem(vP7, Color.FromArgb(0, 0, 0)));

                    //ColumnSeries csAdaptacion = new ColumnSeries();
                    //csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA2, Color.FromArgb(102, 255, 102)));
                    //csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA3, Color.FromArgb(255, 102, 102)));
                    //csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA4, Color.FromArgb(255, 255, 102)));
                    //csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA5, Color.FromArgb(255, 153, 254)));
                    //csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA1, Color.FromArgb(178, 102, 255)));
                    //csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA6, Color.FromArgb(255, 178, 102)));
                    //csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA0, Color.FromArgb(255, 255, 204)));
                    //csAdaptacion.SeriesItems.Add(new CategorySeriesItem(vA7, Color.FromArgb(96, 96, 96)));



                    string vNeg = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_NEG")).FirstOrDefault().NO_VALOR.Value.ToString("N0");
                    string vPos = vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_POS")).FirstOrDefault().NO_VALOR.Value.ToString("N0");
                    int vIdDesc = int.Parse(vResultadosAdaptacion.Where(x => x.CL_VARIABLE.Equals("ADAPTACION_REP_DESC")).FirstOrDefault().NO_VALOR.Value.ToString("N0"));

                    report.Dictionary.Variables["RES_ADAPTA"].Value = vNeg + ", " + vPos + " : " + DescripcionAdaptacion(vIdDesc);
                }


                report.Dictionary.Variables["NB_CANDIDATO"].Value = candidato.NB_CANDIDATO;
                report.Dictionary.Variables["CL_SOLICITUD"].Value = candidato.CL_SOLICITUD;
            }
            report.Compile();
            swvReporte.Report = report;
        }

        public void CargarTiva()
        {
            StiReport report = new StiReport();
            string pathValue = string.Empty;

            PruebasNegocio nSolicitud = new PruebasNegocio();
            SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
            candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);


            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria: vClToken, pIdBateria: vIdBateria);
            if (!xmlResultados.Equals(""))
            {
                XElement res = XElement.Parse(xmlResultados);
                vResultados = new List<E_RESULTADOS_BATERIA>();


                foreach (var element in res.Descendants("PRUEBA"))
                {
                    var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                    {
                        ID_BATERIA = 1,
                        XML_MENSAJES = "",
                        NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                        CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                        CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                    }).ToList();
                    vResultados.AddRange(ListaResultados);
                }



                report.Load(Server.MapPath("~/Assets/reports/IDP/ResultadosTiva.mrt"));
                report.Dictionary.Databases.Clear();


                pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));

                var vResultadosTIVA = vResultados.Where(r => r.CL_PRUEBA.Equals("TIVA")).ToList();
                if (vResultadosTIVA.Count > 0)
                {
                    ORTOGRAFIA1_TOTAL = 0;
                    ORTOGRAFIA1_ACIERTOS = 0;
                    ORTOGRAFIA2_TOTAL = 0;
                    ORTOGRAFIA2_ACIERTOS = 0;
                    ORTOGRAFIA3_TOTAL = 0;
                    ORTOGRAFIA3_ACIERTOS = 0;

                    decimal? TIVA_REP_INDICE_IP = vResultadosTIVA.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IP")).FirstOrDefault().NO_VALOR;
                    decimal? TIVA_REP_INDICE_ALR = vResultadosTIVA.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_ALR")).FirstOrDefault().NO_VALOR;
                    decimal? TIVA_REP_INDICE_IEL = vResultadosTIVA.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IEL")).FirstOrDefault().NO_VALOR;
                    decimal? TIVA_REP_INDICE_IC = vResultadosTIVA.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_IC")).FirstOrDefault().NO_VALOR;
                    decimal? TIVA_REP_INDICE_GI = vResultadosTIVA.Where(x => x.CL_VARIABLE.Equals("TIVA-REP_INDICE_GI")).FirstOrDefault().NO_VALOR;




                    PruebasNegocio pruebas = new PruebasNegocio();
                    var vBaremos = pruebas.obtenerVariableBaremos(vIdBateria);

                    decimal vTvTotal = Math.Round(vBaremos.Where(w => w.CL_VARIABLE == "TV-TOTAL").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                    decimal vPersonal = Math.Round(vBaremos.Where(w => w.CL_VARIABLE == "TV-PERSONAL").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                    decimal vReglamento = Math.Round(vBaremos.Where(w => w.CL_VARIABLE == "TV-LEYES Y REGLAMENTOS").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                    decimal vEtica = Math.Round(vBaremos.Where(w => w.CL_VARIABLE == "TV-INTEGRIDAD Y ÉTICA LABORAL").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                    decimal vCivica = Math.Round(vBaremos.Where(w => w.CL_VARIABLE == "TV-CÍVICA").Select(s => s.NO_VALOR).FirstOrDefault(), 0);
                    decimal vResBaremos = 0;
                    decimal vErrores = 0;
                    foreach (var item in vBaremos)
                    {
                        if (item.CL_VARIABLE == "L1-CONSTANCIA" || item.CL_VARIABLE == "L1-CUMPLIMIENTO" || item.CL_VARIABLE == "L2-MANTIENE Y CONSERVA" || item.CL_VARIABLE == "IN-REGULATORIO")
                        {
                            if (vTvTotal == 1)
                                vResBaremos += (item.NO_VALOR == 1 || item.NO_VALOR == 2) ? 1 : 0;

                            if (vTvTotal == 2)
                                vResBaremos += (item.NO_VALOR == 3 || item.NO_VALOR == 2) ? 1 : 0;

                            if (vTvTotal == 3)
                                vResBaremos += (item.NO_VALOR == 3 || item.NO_VALOR == 2) ? 1 : 0;
                        }
                    }

                    vErrores = 4 - vResBaremos;
                    if (vErrores >= 2)
                    {
                        report.Dictionary.Variables["INVALIDA"].Value = "Prueba inválida- No hay datos a mostrar";
                        report.Dictionary.Variables["DS_TIVA"].Value = "<b>Índice global de integridad</b><br>El resultado de la prueba TIVA es poco confiable, debido a la variabilidad en los enfoques de las elecciones de esta persona. Es probable que la persona no haya comprendido la prueba o bien, sea poco congruente en sus respuestas. TIVA NO PUEDE DAR UN RESULTADO CONFIABLE. Se recomienda utilizar otras herramientas (referencias, pruebas proyectivas, etc.) o bien, tener precaución al contratar / evaluar a esta persona.";

                    }
                    else
                    {

                        report.Dictionary.Variables["IP"].Value = (Math.Round((decimal)TIVA_REP_INDICE_IP, 2)).ToString("N2") + "%";
                        report.Dictionary.Variables["IL"].Value = (Math.Round((decimal)TIVA_REP_INDICE_ALR, 2)).ToString("N2") + "%";
                        report.Dictionary.Variables["II"].Value = (Math.Round((decimal)TIVA_REP_INDICE_IEL, 2)).ToString("N2") + "%";
                        report.Dictionary.Variables["IC"].Value = (Math.Round((decimal)TIVA_REP_INDICE_IC, 2)).ToString("N2") + "%";
                        report.Dictionary.Variables["IG"].Value = (Math.Round((decimal)TIVA_REP_INDICE_GI, 2)).ToString("N2") + "%";


                        report.Dictionary.Variables["INTEGRIDAD"].Value = TIVA_REP_INDICE_IP.ToString();
                        report.Dictionary.Variables["LEYES"].Value = TIVA_REP_INDICE_ALR.ToString();
                        report.Dictionary.Variables["ETICA"].Value = TIVA_REP_INDICE_IEL.ToString();
                        report.Dictionary.Variables["CIVICA"].Value = TIVA_REP_INDICE_IC.ToString();

                        string vMensaje = "";

                        if (vPersonal == 3)
                            vMensaje = vMensaje + "<b>Integridad personal</b><br>Esta persona muestra alta congruencia entre lo que dice y lo que piensa, por lo general actúa de manera conciente y se responsabiliza por sus actos. Se caracteriza por tener firmes sus valores y convicciones, actuando generalmente acorde a ellos.<br><br>";
                        if (vReglamento == 3)
                            vMensaje = vMensaje + "<b>Apego a leyes y reglamentos</b><br>Esta persona es altamente apegada a las reglas y reglamentos que son impuestos por una institución y/o una autoridad. Será poco flexible al romper con una regla y se sentirá muy incómodo cuando otros lo hacen, pudiendo incluso denunciar la falta a un tercero.<br><br>";
                        if (vEtica == 3)
                            vMensaje = vMensaje + "<b>Integridad y ética laboral</b><br>Esta persona se muestra con altos niveles de integridad laboral, presentándose como alguien rígido en cuestión del cumplimiento de reglamentos de trabajo, horarios o incluso normas de conducta éticas laborales. Respetará a la autoridad y a las políticas, incomodándose incluso con quienes no lo hacen.<br><br>";
                        if (vCivica == 3)
                            vMensaje = vMensaje + "<b>Integridad cívica</b><br>Esta persona muestra niveles altos de conciencia social y cívica, cumpliendo con normas de urbanidad que servirán para la convivencia social, pero así mismo con una alta conciencia ecológica y/o patriótica. Cumplirán sus obligaciones ciudadanas, además de promover y defender hasta el cansancio sus valores y creencias.<br><br>";
                        if (vTvTotal == 1)
                            vMensaje = vMensaje + "<b>Índice global de integridad</b><br>Esta persona es flexible en sus enfoques, mostrando poco interés e importancia en el cumplimiento de normas y reglas. Tenderá a anteponer el logro de sus objetivos ante la imposición de estructuras o imposiciones éticas.<br><br>";
                        if (vTvTotal == 2)
                            vMensaje = vMensaje + "<b>Índice global de integridad</b><br>Esta persona muestra compromiso y preocupación por las consecuencias de sus actos, sólo cuando se relacionan con el cumplimiento de sus objetivos. Se mostrará flexible en sus enfoques. Tiene la capacidad de negociar y adecuar las reglas de manera que se ajusten al logro de sus objetivos. Tiende a actuar de manera neutral ante situaciones en los que tenga que mostrar alguna postura ética.<br><br>";
                        if (vTvTotal == 3)
                            vMensaje = vMensaje + "<b>Índice global de integridad</b><br>Esta persona muestra una alta rigidez en su forma de actuar ante decisiones éticas. Tiende a ser una persona con alta conciencia social que promueve valores o creencias, en ocasiones imponiendo su punto de vista. Tiende a juzgar lo que es bueno y lo que es malo según su escala de valores. Por lo general será directo, y honesto, externando sus opiniones, aún cuando éstas resulten incómodas o poco populares.<br><br>";

                        report.Dictionary.Variables["DS_TIVA"].Value = vMensaje;
                    }
                }

                report.Dictionary.Variables["NB_CANDIDATO"].Value = candidato.NB_CANDIDATO;
                report.Dictionary.Variables["CL_SOLICITUD"].Value = candidato.CL_SOLICITUD;
            }
            report.Compile();
            swvReporte.Report = report;
        }

        public void CargarLaboral2()
        {

            StiReport report = new StiReport();
            string pathValue = string.Empty;

            PruebasNegocio nSolicitud = new PruebasNegocio();
            SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result candidato = new SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result();
            candidato = nSolicitud.ObtenienePruebasResultadosCandidatos(vIdBateria);


            ResultadosPruebasNegocio nResultadosPruebas = new ResultadosPruebasNegocio();
            String xmlResultados = nResultadosPruebas.Obtener_ResultadosBaterias(pClTokenBateria: vClToken, pIdBateria: vIdBateria);
            if (!xmlResultados.Equals(""))
            {
                XElement res = XElement.Parse(xmlResultados);
                vResultados = new List<E_RESULTADOS_BATERIA>();


                foreach (var element in res.Descendants("PRUEBA"))
                {
                    var ListaResultados = element.Elements("VALORES").Select(el => new E_RESULTADOS_BATERIA
                    {
                        ID_BATERIA = 1,
                        XML_MENSAJES = "",
                        NO_VALOR = decimal.Parse(el.Attribute("NO_VALOR").Value),
                        CL_VARIABLE = el.Attribute("CL_VARIABLE").Value,
                        CL_PRUEBA = el.Attribute("CL_PRUEBA").Value,
                    }).ToList();
                    vResultados.AddRange(ListaResultados);
                }



                report.Load(Server.MapPath("~/Assets/reports/IDP/ResultadosLaboral2.mrt"));
                report.Dictionary.Databases.Clear();


                pathValue = System.Configuration.ConfigurationManager.ConnectionStrings["SigeinReporting"].ConnectionString;
                report.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Sigein", pathValue));


                var vResultadosLaboralII = vResultados.Where(r => r.CL_PRUEBA.Equals("LABORAL-2")).ToList();
                if (vResultadosLaboralII.Count > 0)
                {
                    decimal? LABORAL2_REP_DAAPF = vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-DAAPF")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL2_REP_TMCTF = vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-TMCTF")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL2_REP_MTCSF = vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-MTCSF")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL2_REP_ADNGF = vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-ADNGF")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL2_REP_DAAPD = vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-DAAPD")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL2_REP_TMCTD = vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-TMCTD")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL2_REP_MTCSD = vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-MTCSD")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL2_REP_ADNGD = vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-ADNGD")).FirstOrDefault().NO_VALOR;

                    string vDesFavorablesTitulo = "";
                    string vDesFavorables1 = "";
                    string vDesFavorables2 = "";
                    string vDesFavorables3 = "";
                    string vDesFavorables4 = "";
                    string vDesFavorables5 = "";
                    string vDesFavorables6 = "";
                    string vDesFavorables7 = "";
                    string vDesFavorables8 = "";
                    string vDesFavorables9 = "";

                    string vDesDesfavorablesTitulo = "";
                    string vDesDesfavorables1 = "";
                    string vDesDesfavorables2 = "";
                    string vDesDesfavorables3 = "";

                    #region Textos

                    if (LABORAL2_REP_DAAPF >= LABORAL2_REP_TMCTF & LABORAL2_REP_DAAPF >= LABORAL2_REP_MTCSF & LABORAL2_REP_DAAPF >= LABORAL2_REP_ADNGF)
                    {

                        vDesFavorablesTitulo = "MANEJO DE LAS FUERZAS <br />" +
                                            "DESARROLLO DE LAS FUERZAS <br />" +
                                            "RESUMEN <br />" +
                                            "ESTILO LIFO DA Y APOYA";
                        vDesFavorables1 =
                                "<b>USO PRODUCTIVO DE LAS FUERZAS</b> <br />" +
                                "<b>Estilo productivo</b>" +
                                "<br /><br />" +
                                "Expectativas muy altas para sí mismo y para los demás." +
                                "<br /><br />" +
                                "Admira y apoya las realizaciones de los otros." +
                                "<br /><br />" +
                                "Gran fe y confianza en los demás." +
                                "<br /><br />" +
                                "Ansioso de responder cuando se pide ayuda.";


                        vDesFavorables2 = "<b>IRONIA DE LA FUERZA/FLAQUEZA</b> <br /><br />" +
                                            "<b>Productivo   &nbsp;&nbsp;&nbsp;       Exceso</b> " +
                                       "<br />" +
                                             "Acepta     &nbsp;&nbsp;&nbsp;        Indulgente" +
                                        "<br />" +
                                             "Cooperativo    &nbsp;&nbsp;&nbsp;    Fácilmente influenciable" +
                                         "<br />" +
                                             "Considerado  &nbsp;&nbsp;&nbsp;      Se niega a si mismo" +
                                         "<br />" +
                                             "Idealista    &nbsp;&nbsp;&nbsp;       Impráctico" +
                                     "<br />" +
                                          "Modesto      &nbsp;&nbsp;&nbsp;          Auto-despectivo" +
                                       "<br />" +
                                             "Cortés     &nbsp;&nbsp;&nbsp;          Deferente" +
                                         "<br />" +
                                             "Optimista   &nbsp;&nbsp;&nbsp;         No realista" +
                                           "<br />" +
                                             "Confiado    &nbsp;&nbsp;&nbsp;         Ingenuo</h4>" +
                                            "<br />" +
                                            "Afectuoso    &nbsp;&nbsp;&nbsp;         Sentimental</h4>" +
                                         "<br />" +
                                             "Leal        &nbsp;&nbsp;&nbsp;         Obligado</h4>" +
                                         "<br />" +
                                             "Ayuda     &nbsp;&nbsp;&nbsp;           Paternal" +
                                           "<br />" +
                                             "Receptivo     &nbsp;&nbsp;&nbsp;       Pasivo" +
                                          "<br />" +
                                             "Sensible     &nbsp;&nbsp;&nbsp;        Demasiado comprometido" +
                                           "<br />" +
                                             "Procura excelencia  &nbsp;&nbsp;&nbsp; Perfeccionista";



                        vDesFavorables3 = "<b>Cómo influir en la persona</b> " +
                               "<br /><br />" +
                               "Enfatice causas valederas." +
                               "<br /><br />" +
                               "Apele al idealismo." +
                               "<br /><br />" +
                               "Pida ayuda." +
                               "<br /><br />" +
                               ">Apele a la excelencia." +
                               "<br /><br />" +
                               "Muestre interés y preocupación." +
                               "<br /><br />" +
                               "Enfatice el desarrollo personal.";

                        vDesFavorables4 = "<b>Cuál es el ambiente Más efectivo</b> " +
                             "<br /><br />" +
                             "Respeto." +
                             "<br /><br />" +
                             "<Apoyo." +
                            " <br /><br />" +
                             "Reafirmación." +
                             "<br /><br />" +
                             "Idealismo.";

                        vDesFavorables5 = "<b>Cuál es el ambiente Menos efectivo</b> " +
                            "<br /><br />" +
                            "Traición." +
                            "<br /><br />" +
                            "Critica personal." +
                            "<br /><br />" +
                            "Ridículo." +
                            "<br /><br />" +
                            "Fracaso." +
                            "<br /><br />" +
                           "Falta de apoyo.";


                        vDesFavorables6 = "<b>Cómo ser el Patrón más efectivo</b> " +
                                "<br /><br />" +
                                "<h4>Dé reconocimiento, confianza y gratitud.</h4>" +
                                "<br /><br />" +
                                "<h4>Defina las metas conjuntamente.</h4>" +
                                "<br /><br />" +
                                "<h4>Sea accesible.</h4>" +
                                "<br /><br />" +
                                "<h4>Trate de compartir.</h4>" +
                                "<br /><br />" +
                               " <h4>Sea confiable.</h4>";

                        vDesFavorables7 = "<b>Cómo ser el Empleado más efectivo</b> " +
                             "<br /><br />" +
                             "<h4>Demuestre que vale.</h4>" +
                             "<br /><br />" +
                             "<h4>Muestre lealtad.</h4>" +
                            " <br /><br />" +
                             "<h4>Sea sincero.</h4>" +
                             "<br /><br />" +
                             "<h4>Orientado hacia el equipo de trabajo.</h4>";

                        vDesFavorables8 = "<b>Las Necesidades del estilo</b> " +
                               "<br /><br />" +
                               "<h4>Ser visto como alguien adecuado y valioso.</h4>" +
                               "<br /><br />" +
                               "<h4>Sentirse apreciado, comprendido, aceptado-tenerle confianza.</h4>" +
                               "<br /><br />" +
                               "<h4>Sentir que los ideales no están perdidos.</h4>";

                        vDesFavorables9 = "<b>Cómo apoyarlo a superar sus excesos</b> " +
                             "<br /><br />" +
                             "<h4>Brinde apoyo, reafirmación y aliento.</h4>" +
                             "<br /><br />" +
                             "<h4>Dé auxilio y ayuda específicos.</h4>" +
                             "<br /><br />" +
                             "<h4>Escuche atentamente a la persona.</h4>" +
                            " <br /><br />" +
                             "<h4>Provea justificaciones significativas dirigidas a la ansiedad, la queja o preocupación.</h4>" +
                             "<br /><br />" +
                             "<h4>Reconozca el valor del intento aún cuando las consecuencias no hayan sido las deseadas.</h4>" +
                             "<br /><br />" +
                            " <h4>Sugiera modos como la persona puede compensar por el error, o recuperarse.</h4>" +
                            " <br /><br />" +
                             "<h4>No insista o atice por respuestas retrasadas.</h4>";
                    }

                    if (LABORAL2_REP_TMCTF >= LABORAL2_REP_DAAPF & LABORAL2_REP_TMCTF >= LABORAL2_REP_MTCSF & LABORAL2_REP_TMCTF >= LABORAL2_REP_ADNGF)
                    ////    divTCF.Style.Value = String.Format("display: {0}", "block");
                    {
                        vDesFavorablesTitulo = "MANEJO DE LAS FUERZAS" +
                                            " <br />" +
                                             "DESARROLLO DE LAS FUERZAS <br />" +
                                             "RESUMEN" +
                                             "<br />" +
                                            "ESTILO LIFO TOMA Y CONTROLA";


                        vDesFavorables1 = "<b>USO PRODUCTIVO DE LAS FUERZAS</b> " +
                            "<br /><br />" +
                            "<b>Estilo productivo</b> " +
                            "<br /><br />" +
                           " <h4>Le gusta estar a cargo, asumir el mando y el control.</h4>" +
                            "<br /><br />" +
                            "<h4>Rápido para actuar o correr riesgos.</h4>" +
                           " <br /><br />" +
                           " <h4>Le gusta el desafío, la oportunidad para superar una dificultad.</h4>" +
                            "<br /><br />" +
                            "<h4>Busca la novedad y la variedad.</h4>" +
                            "<br /><br />" +
                            "<h4>Prefiere dirigir y coordinar el trabajo de los demás.</h4>" +
                            "<br /><br />" +
                           " <h4>Se posesiona de una oportunidad cuando la ve.</h4>" +
                            "<br /><br />" +
                           " <h4>Dice: 'Si usted quiere que algo ocurra, usted debe hacer que ello ocurra'.</h4>";

                        vDesFavorables2 = "<b>IRONIA DE LA FUERZA/FLAQUEZA</b> " +
                             "<br /><br />" +
                                          "<b>Productivo       &nbsp;&nbsp;&nbsp;           Exceso</b> " +
                            "<br /><br />" +
                                          "<h4>Controlador       &nbsp;&nbsp;&nbsp;      Dominante</h4>" +
                             "<br />" +
                                          "<h4>Rápido para actuar   &nbsp;&nbsp;&nbsp;    Impulsivo</h4>" +
                            "<br />" +
                                         " <h4>Confianza en sí mismo  &nbsp;&nbsp;&nbsp;   Arrogante</h4>" +
                              "<br />" +
                                          "<h4>Busca el cambio    &nbsp;&nbsp;&nbsp;       Desperdicia lo viejo</h4>" +
                         "<br />" +
                                          "<h4>Persuasivo       &nbsp;&nbsp;&nbsp;         Distorsionador</h4>" +
                         "<br />" +
                                          "<h4>Esforzado       &nbsp;&nbsp;&nbsp;          Coercitivo</h4>" +
                         "<br />" +
                                          "<h4>Competitivo     &nbsp;&nbsp;&nbsp;          Luchador</h4>" +
                         "<br />" +
                                          "<h4>Corre riesgos    &nbsp;&nbsp;&nbsp;         Apuesta</h4>" +
                          "<br />" +
                                          "<h4>Persistente     &nbsp;&nbsp;&nbsp;           Presiona</h4>" +
                           "<br />" +
                                         " <h4>Urgente         &nbsp;&nbsp;&nbsp;            Impaciente</h4>" +
                            "<br />" +
                                          "<h4>Emprendedor    &nbsp;&nbsp;&nbsp;             Oportunista</h4>" +
                             "<br />" +
                                          "<h4>Toma iniciativa     &nbsp;&nbsp;&nbsp;        Actúa sin autorización constantemente, probándose así mismo</h4>" +
                            "<br />" +
                                          "<h4>Responde al desafío</h4>" +
                             "<br />" +
                                          "<h4>Imaginativo   &nbsp;&nbsp;&nbsp;    No realista</h4>";





                        vDesFavorables3 = "<b>Cómo influir en la persona</b> " +
                               "<br /><br />" +
                               "<h4>Dé oportunidades.</h4>" +
                               "<br /><br />" +
                               "<h4>Dé más responsabilidad.</h4>" +
                               "<br /><br />" +
                              " <h4>Desafíe.</h4>" +
                               "<br /><br />" +
                               "<h4>Dé recursos que faciliten el logro.</h4>" +
                              " <br /><br />" +
                               "<h4>Dé autoridad.</h4>";



                        vDesFavorables4 = "<b>Cuál es el ambiente Más efectivo</b> " +
                            "<br /><br />" +
                            "<h4>Competencia.</h4>" +
                            "<br /><br />" +
                            "<h4>Directo.</h4>" +
                            "<br /><br />" +
                            "<h4>Con riesgos.</h4>" +
                            "<br /><br />" +
                            "<h4>Oportunidades.</h4>";

                        vDesFavorables5 = "<b>Cuál es el ambiente Menos efectivo</b> " +
                           " <br /><br />" +
                            "<h4>Sin recursos.</h4>" +
                            "<br /><br />" +
                            "<h4>Autoridad bloqueada.</h4>" +
                            "<br /><br />" +
                            "<h4>Responsabilidad disminuida.</h4>" +
                           " <br /><br />" +
                            "<h4>Sin desafíos.</h4>" +
                            "<br /><br />" +
                            "<h4>Imposible controlar factores que afectan los resultados.</h4>";


                        vDesFavorables6 = "<b>Cómo ser el Patrón más efectivo</b> " +
                            "<br /><br />" +
                            "<h4>Téngase confianza.</h4>" +
                            "<br /><br />" +
                            "<h4>Provea autonomía.</h4>" +
                            "<br /><br />" +
                            "<h4>Recompense los resultados.</h4>" +
                            "<br /><br />" +
                            "<h4>Fije límites firmes, pero reconozca iniciativa.</h4>" +
                            "<br /><br />" +
                            "<h4>Escuche, pero sea decidido.</h4>" +
                            "<br /><br />" +
                            "<h4>Luche en igualdad de condiciones.</h4>";


                        vDesFavorables7 = "<b>Cómo ser el Empleado más efectivo</b> " +
                             "<br /><br />" +
                             "<h4>Responda, no se muestre indiferente.</h4>" +
                             "<br /><br />" +
                             "<h4>Sea capaz.</h4>" +
                             "<br /><br />" +
                             "<h4>Sea independiente.</h4>" +
                             "<br /><br />" +
                             "<h4>Sea directo.</h4>";


                        vDesFavorables8 = "<b>Las Necesidades del estilo</b> " +
                              "<br /><br />" +
                              "<h4>Ser visto como alguien poderoso, capaz y competente.</h4>" +
                              "<br /><br />" +
                              "<h4>Sentirse capaz de superar obstáculos.</h4>" +
                              "<br /><br />" +
                              "<h4>Sentir que hay aún otras oportunidades.</h4>";

                        vDesFavorables9 = "<b>Cómo apoyarlo a superar sus excesos</b> " +
                             "<br /><br />" +
                             "<h4>Trate de responder rápidamente.</h4>" +
                             "<br /><br />" +
                             "<h4>Ofrezca soluciones, no traiga nuevos problemas.</h4>" +
                             "<br /><br />" +
                             "<h4>Sea sincero y firme, pero respetuoso.</h4>" +
                             "<br /><br />" +
                             "<h4>Refleje su comprensión de la preocupación.</h4>" +
                             "<br /><br />" +
                             "<h4>Haga preguntas para ayudar a la persona a sentir que ella ha encontrado su propia solución.</h4>" +
                             "<br /><br />" +
                             "<h4>Provea maneras alternativas de enfocar la situación.</h4>" +
                             "<br /><br />" +
                             "<h4>Espere a que baje la presión antes de exigir.</h4>";

                    }

                    if (LABORAL2_REP_MTCSF >= LABORAL2_REP_DAAPF & LABORAL2_REP_MTCSF >= LABORAL2_REP_TMCTF & LABORAL2_REP_MTCSF >= LABORAL2_REP_ADNGF)
                    ////    divMCF.Style.Value = String.Format("display: {0}", "block");
                    {
                        vDesFavorablesTitulo = "MANEJO DE LAS FUERZAS" +
                                              " <br />" +
                                               "DESARROLLO DE LAS FUERZAS<br />" +
                                               "RESUMEN" +
                                               "<br />" +
                                              " ESTILO LIFO MANTIENE Y CONSERVA";


                        vDesFavorables1 = "<b>USO PRODUCTIVO DE LAS FUERZAS</b> " +
                              "<br /><br />" +
                              "</b> Estilo productivo<b>" +
                              "<br /><br />" +
                              "<h4>Tiene gran confianza en la lógica, los hechos y el sistema.</h4>" +
                              "<br /><br />" +
                              "<h4>A menudo sopesa todas las alternativas para eliminar los riesgos.</h4>" +
                              "<br /><br />" +
                              "<h4>Tiene necesidad de prevenir, no quiere sorpresas.</h4>" +
                              "<br /><br />" +
                              "<h4>Usa al máximo procedimiento y regulaciones.</h4>" +
                              "<br /><br />" +
                              "<H4>Uno debe probar la practicabilidad del cambio para convencerlo de la necesidad del cambio.</H4>" +
                              "<br /><br />" +
                              "<h4>La filosofía subyacente es: Hay que mantener lo que se tiene y construir el futuro sobre la base del pasado.</h4>";


                        vDesFavorables2 = "<b>IRONIA DE LA FUERZA/FLAQUEZA</b> " +
                                   "<br /><br />" +
                                           "<b>Productivo      &nbsp;&nbsp;&nbsp;         Exceso</b> " +
                         "<br /><br />" +
                                           "<h4>Tenaz      &nbsp;&nbsp;&nbsp;          Demasiado persistente</h4>" +
                            "<br />" +
                                           "<h4>Práctico     &nbsp;&nbsp;&nbsp;         No creativo</h4>" +
                          "<br />" +
                                           "<h4>Económico    &nbsp;&nbsp;&nbsp;         Avaro</h4>" +
                                   "<br />" +
                                           "<h4>Reservado    &nbsp;&nbsp;&nbsp;         Inamistoso</h4>" +
                                      "<br />" +
                                           "<h4>Factual      &nbsp;&nbsp;&nbsp;         Limitado por datos</h4>" +
                                    "<br />" +
                                           "<h4>Constante     &nbsp;&nbsp;&nbsp;        Obstinado</h4>" +
                                   "<br />" +
                                           "<h4>Cuidadoso     &nbsp;&nbsp;&nbsp;        Elaborado</h4>" +
                             "<br />" +
                                           "<h4>Metódico     &nbsp;&nbsp;&nbsp;         Lento</h4>" +
                                     "<br />" +
                                          " <h4>Detallado    &nbsp;&nbsp;&nbsp;        Demasiado minucioso</h4>" +
                                       "<br />" +
                                           "<h4>Analítico    &nbsp;&nbsp;&nbsp;        Crítico</h4>" +
                                        "<br />" +
                                           "<h4>Controlado    &nbsp;&nbsp;&nbsp;       Sin sentimientos</h4>" +
                                    "<br />" +
                                           "<h4>Cauteloso    &nbsp;&nbsp;&nbsp;         No arriesga</h4>" +
                                     "<br />" +
                                           "<h4>Realista     &nbsp;&nbsp;&nbsp;         Sin imaginación</h4>" +
                                    "<br />" +
                                           "<h4>Lógico        &nbsp;&nbsp;&nbsp;         Rígido</h4>";




                        vDesFavorables3 = "<b>Cómo influir en la persona</b> " +
                            "<br /><br />" +
                            "<h4>Presente ideas no riesgos.</h4>" +
                            "<br /><br />" +
                            "<h4>Dé oportunidad para analizar.</h4>" +
                            "<br /><br />" +
                            "<h4>Use lógica, use hechos.</h4>" +
                            "<br /><br />" +
                            "<h4>Use familiaridad, rutina y estructura.</h4>" +
                            "<br /><br />" +
                            "<h4>Relacione cosas nuevas a cosas viejas .</h4>";


                        vDesFavorables4 = "<b>Cuál es el ambiente Más efectivo</b> " +
                               "<br /><br />" +
                               "<h4>Neutralidad emocional.</h4>" +
                               "<br /><br />" +
                               "<h4>Basado en hechos.</h4>" +
                               "<br /><br />" +
                               "<h4>Científico.</h4>" +
                               "<br /><br />" +
                               "<h4>Práctico.</h4>";


                        vDesFavorables5 = "<b>Cuál es el ambiente Menos efectivo</b> " +
                                "<br /><br />" +
                                "<h4>Reglas y procedimientos en constante cambio.</h4>" +
                                "<br /><br />" +
                                "<h4>Altamente emocional.</h4>" +
                                "<br /><br />" +
                                "<h4>Decisiones prematuras.</h4>" +
                                "<br /><br />" +
                                "<h4>No se le toma en serio.</h4>";

                        vDesFavorables6 = "<b>Cómo ser el Patrón más efectivo</b> " +
                                 "<br /><br />" +
                                 "<h4>Sea organizado.</h4>" +
                                 "<br /><br />" +
                                 "<h4>Muestre que tiene un propósito.</h4>" +
                                 "<br /><br />" +
                                 "<h4>Preste atención a detalles.</h4>" +
                                 "<br /><br />" +
                                 "<h4>Sea sistemático.</h4>" +
                                 "<br /><br />" +
                                 "<h4>Sea objetivo.</h4>" +
                                 "<br /><br />" +
                                 "<h4>Sea justo.</h4>" +
                                " <br /><br />" +
                                 "<h4>Sea consiente.</h4>";


                        vDesFavorables7 = "<b>Cómo ser el Empleado más efectivo</b> " +
                              "<br /><br />" +
                              "<h4>Sea respetuoso.</h4>" +
                              "<br /><br />" +
                              "<h4>Adaptado.</h4>" +
                              "<br /><br />" +
                              "<h4>Lógico.</h4>" +
                              "<br /><br />" +
                             " <h4>Preste atención.</h4>";


                        vDesFavorables8 = "<b>Las Necesidades del estilo</b> " +
                               "<br /><br />" +
                               "<h4>Ser visto como alguien objetivo, determinado y racional.</h4>" +
                               "<br /><br />" +
                               "<h4>Sentirse a salvo, seguro.</h4>" +
                              " <br /><br />" +
                               "<h4>Sentir que ninguna pérdida es irreparable.</h4>";


                        vDesFavorables9 = "<b>Cómo apoyarlo a superar sus excesos</b> " +
                                   "<br /><br />" +
                                   "<h4>Trate de disminuir la tensión y la amenaza.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Lleve las emociones al mínimo.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Trate un tono más ligero preferentemente con humor.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Pida sugerencias sobre los criterios que podrían utilizarse para evaluar el problema.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Permita un cierto tiempo extra antes de tomar la decisión.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Obtenga datos adicionales en los que la persona pueda confiar.</h4>";
                    }
                    //--------------------------------
                    if (LABORAL2_REP_ADNGF >= LABORAL2_REP_DAAPF & LABORAL2_REP_ADNGF >= LABORAL2_REP_TMCTF & LABORAL2_REP_ADNGF >= LABORAL2_REP_MTCSF)
                    ////    divANF.Style.Value = String.Format("display: {0}", "block");
                    {
                        vDesFavorablesTitulo = "MANEJO DE LAS FUERZAS" +
                                            "<br />" +
                                            "DESARROLLO DE LAS FUERZAS<br />" +
                                            "RESUMEN" +
                                            "<br />" +
                                            "ESTILO LIFO ADAPTA Y NEGOCIA";


                        vDesFavorables1 = "<b>USO PRODUCTIVO DE LAS FUERZAS</b> " +
                            "<br /><br />" +
                            "<b>Estilo productivo</b> " +
                            "<br /><br />" +
                            "<h4>Usa sus habilidades sociales y su encanto personal para manejarse con las realidades del mundo.</h4>" +
                            "<br /><br />" +
                            "<h4>Enfatiza la adaptación y el acuerdo con los demás.</h4>" +

                            "<br /><br />" +
                            "<h4>Tiene maneras joviales, juguetonas, no serias.</h4>" +
                            "<br /><br />" +
                            "<h4>Es socialmente sensible a las necesidades de los demás.</h4>" +
                            "<br /><br />" +
                            "<h4>Dice: 'Si quieres ir hacia adelante, averigüe lo que los otros necesitan y asegúrese de que lo  obtengan'.</h4>";


                        vDesFavorables2 = "<b>IRONIA DE LA FUERZA/FLAQUEZA</b> " +
                                         "<br /><br />" +
                                              "<b>Productivo     &nbsp;&nbsp;&nbsp;         Exceso</b> " +
                                           "<br /><br />" +
                                              "<h4>Flexible    &nbsp;&nbsp;&nbsp;       Inconsistente</h4>" +
                                      "<br />" +
                                              "<h4>Experimentador  &nbsp;&nbsp;&nbsp;    Sin metas</h4>" +
                                     "<br />" +
                                              "<h4>Jovial      &nbsp;&nbsp;&nbsp;        Pueril</h4>" +
                                      "<br />" +
                                              "<h4>Entusiasta    &nbsp;&nbsp;&nbsp;       Agitado</h4>" +
                                       "<br />" +
                                              "<h4>Diplomático    &nbsp;&nbsp;&nbsp;      Acepta demasiado</h4>" +
                                        "<br />" +
                                              "<h4>Adaptable   &nbsp;&nbsp;&nbsp;         Sin convicción</h4>" +
                                      "<br />" +
                                              "<h4>Hábil socialmente  &nbsp;&nbsp;&nbsp;   Manipulador</h4>" +
                                         "<br />" +
                                              "<h4>Negociador    &nbsp;&nbsp;&nbsp;        Renuncia demasiado</h4>" +
                                          "<br />" +
                                             " <h4>Animado      &nbsp;&nbsp;&nbsp;         Melodramático</h4>" +
                                        "<br />" +
                                              "<h4>Inspirador    &nbsp;&nbsp;&nbsp;        No realista</h4>" +
                                       "<br />" +
                                              "<h4>Sociable       &nbsp;&nbsp;&nbsp;       Incapaz de estar solo</h4>" +
                                         "<br />" +
                                              "<h4>Solícito      &nbsp;&nbsp;&nbsp;        Lisonjero</h4>" +
                                         "<br />" +
                                              "<h4>Divertido     &nbsp;&nbsp;&nbsp;        Tonto</h4>" +
                                         "<br />" +
                                              "<h4>Cumplido      &nbsp;&nbsp;&nbsp;        Adulador</h4>";




                        vDesFavorables3 = "<b>Cómo influir en la persona</b> " +
                                  "<br /><br />" +
                                  "<h4>Dé oportunidad para hacer cosas con otros.</h4>" +
                                  "<br /><br />" +
                                  "<h4>Use humor.</h4>" +
                                  "<br /><br />" +
                                  "<h4>Hágale saber que usted está complacido.</h4>" +
                                  "<br /><br />" +
                                  "<h4>Dé oportunidades para lucimiento personal.</h4>";


                        vDesFavorables4 = "<b>Cuál es el ambiente Más efectivo</b> " +
                                     "<br /><br />" +
                                     "<h4>Social.</h4>" +
                                     "<br /><br />" +
                                     "<h4>Cambiante.</h4>" +
                                     "<br /><br />" +
                                     "<h4>Jovial.</h4>" +
                                     "<br /><br />" +
                                     "<h4>Optimista.</h4>";

                        vDesFavorables5 = "<b>Cuál es el ambiente Menos efectivo</b> " +
                                   "<br /><br />" +
                                   "<h4>Autoridad crítica.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Compañeros no amistosos.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Rutinas y detalles.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Horarios firmes y supervisión.</h4>";

                        vDesFavorables6 = "<b>Cómo ser el Patrón más efectivo</b>" +
                                   "<br /><br />" +
                                   "<h4>Sea amistoso.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Dé información.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Hágale saber los resultados.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Sea comprensivo.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Sea alentador.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Sea flexible.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Use su sentido del humor.</h4>";

                        vDesFavorables7 = "<b>Cómo ser el Empleado más efectivo</b> " +
                                    "<br /><br />" +
                                    "<h4>Sea sociable.</h4>" +
                                    "<br /><br />" +
                                    "<h4>Sofisticado.</h4>" +
                                    "<br /><br />" +
                                    "<h4>Tenga tacto.</h4>" +
                                    "<br /><br />" +
                                    "<h4>Tenga influencia.</h4>";


                        vDesFavorables8 = "<b>Las Necesidades del estilo</b> " +
                                   "<br /><br />" +
                                   "<h4>Ser visto como alguien que gusta a los demás, popular.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Hacer sentir feliz a todos con los resultados.</h4>" +
                                   "<br /><br />" +
                                   "<h4>Sentir que hay aún oportunidades para complacer a la gente. </h4>";


                        vDesFavorables9 = "<b>Cómo apoyarlo a superar sus excesos</b> " +
                                    "<br /><br />" +
                                    "<h4>Dele la seguridad de que usted lo aprecia.</h4>" +
                                    "<br /><br />" +
                                    "<h4>Esté dispuesto a tratar de negociar.</h4>" +
                                    "<br /><br />" +
                                    "<h4>Sugiera que usted admira a las personas que son sinceras cuando no están de acuerdo.</h4>" +
                                    "<br /><br />" +
                                    "<h4>Use un enfoque positivo: 'Lo que me gusta de esto es'...'Las reservas que tengo son...'.</h4>" +
                                    "<br /><br />" +
                                    "<h4>Pase algo de tiempo en actitud amistosamente social, antes de exigir la decisión.</h4>" +
                                    "<br /><br />" +
                                    "<h4>Permítale a la persona 'conservar una buena fachada'.</h4>";
                    }

                    if (LABORAL2_REP_DAAPD >= LABORAL2_REP_TMCTD & LABORAL2_REP_DAAPD >= LABORAL2_REP_MTCSD & LABORAL2_REP_DAAPD >= LABORAL2_REP_ADNGD)
                    ////    divDAD.Style.Value = String.Format("display: {0}", "block");
                    {
                        vDesDesfavorablesTitulo = "ESTILO DA Y APOYA";


                        vDesDesfavorables1 = "Estilo de tensión" +
                                "<br /><br />" +
                                "<h4>Se vuelve demasiado confiable e ingenuo.</h4>" +
                                "<br /><br />" +
                                "<h4>Enfatiza tanto el estilo que se vuelve deferente.</h4>" +
                                "<br /><br />" +
                                "<h4>Vulnerable a la desilusión cuando las metas son altas.</h4>" +
                                "<br /><br />" +
                                "<h4>Fácilmente desilusionado y decepcionado por la gente.</h4>";


                        vDesDesfavorables2 = "Estilo de Lucha" +
                               "<br /><br />" +
                               "<h4>Asume la culpa</h4>" +
                               "<br /><br />" +
                               "<h4>Se vuelve inseguro y pide ayuda, se vuelve dependiente.</h4>" +
                               "<br /><br />" +
                               "<h4>Percibido por los demás como demasiado 'blando'.</h4>" +
                               "<br /><br />" +
                               "<h4>Se rinde en vez de pelear y 'causar problemas'</h4>";


                        vDesDesfavorables3 = "Cómo apoyarlo a superar sus excesos" +
                              "<br /><br />" +
                              "<h4>Brinde apoyo, reafirmación y aliento.</h4>" +
                              "<br /><br />" +
                              "<h4>Dé auxilio y ayuda específicos.</h4>" +
                              "<br /><br />" +
                              "<h4>Escuche atentamente a la persona.</h4>" +
                              "<br /><br />" +
                              "<h4>Provea justificaciones significativas dirigidas a la ansiedad, la queja o preocupación.</h4>" +
                              "<br /><br />" +
                              "<h4>Reconozca el valor del intento aún cuando las consecuencias no hayan sido las deseadas.</h4>" +
                              "<br /><br />" +
                              "<h4>Sugiera modos como la persona puede compensar por el error, o recuperarse.</h4>" +
                              "<br /><br />" +
                              "<h4>No insista o atice por respuestas retrasadas. </h4>";
                    }

                    if (LABORAL2_REP_TMCTD >= LABORAL2_REP_DAAPD & LABORAL2_REP_TMCTD >= LABORAL2_REP_MTCSD & LABORAL2_REP_TMCTD >= LABORAL2_REP_ADNGD)
                    //    divTCD.Style.Value = String.Format("display: {0}", "block");
                    {
                        vDesDesfavorablesTitulo = "ESTILO TOMA Y CONTROLA";


                        vDesDesfavorables1 = "Estilo de tensión" +
                         "<br /><br />" +
                         "<h4>Se vuelve manipulador.</h4>" +
                         "<br /><br />" +
                         "<h4>Se vuelve impulsivo.</h4>" +
                         "<br /><br />" +
                         "<h4>Le gustan las cosas nuevas simplemente por la novedad, abandona lo viejo aunque aún sea útil.</h4>" +
                         "<br /><br />" +
                         "<h4>Quita a los otros su autonomía y sus oportunidades.</h4>";


                        vDesDesfavorables2 = "Estilo de Lucha" +
                                  "<br /><br />" +
                                  "<h4>Tiende a reclamar abiertamente que las cosas se hagan como él quiere</h4>" +
                                  "<br /><br />" +
                                  "<h4>Defiende su posición con rapidez.</h4>" +
                                  "<br /><br />" +
                                  "<h4>Pronto para la lucha y coerción.</h4>" +
                                  "<br /><br />" +
                                  "<h4>Es capaz de pelear por sus derechos hasta la eternidad.</h4>";


                        vDesDesfavorables3 = "Cómo apoyarlo a superar sus excesos" +
                                "<br /><br />" +
                                "<h4>Trate de responder rápidamente.</h4>" +
                               " <br /><br />" +
                                "<h4>Ofrezca soluciones, no traiga nuevos problemas.</h4>" +
                                "<br /><br />" +
                                "<h4>Sea sincero y firme, pero respetuoso.</h4>" +
                               " <br /><br />" +
                               " <h4>Refleje su comprensión de la preocupación.</h4>" +
                                "<br /><br />" +
                                "<h4>Haga preguntas para ayudar a la persona a sentir que ella ha encontrado su propia solución.</h4>" +
                                "<br /><br />" +
                                "<h4>Provea maneras alternativas de enfocar la situación.</h4>" +
                                "<br /><br />" +
                               " <h4>Espere a que baje la presión antes de exigir. </h4>";

                    }

                    if (LABORAL2_REP_MTCSD >= LABORAL2_REP_DAAPD & LABORAL2_REP_MTCSD >= LABORAL2_REP_TMCTD & LABORAL2_REP_MTCSD >= LABORAL2_REP_ADNGD)
                    //    divMCD.Style.Value = String.Format("display: {0}", "block");
                    {
                        vDesDesfavorablesTitulo = "ESTILO MANTIENE Y CONSERVA";

                        vDesDesfavorables1 = "Estilo de tensión" +
                                 "<br /><br />" +
                                 "<h4>Llega a tener  'parálisis de análisis'.</h4>" +
                                 "<br /><br />" +
                                 "<h4>Se adhiere a viejos métodos y cosas ante las necesidades del cambio.</h4>";



                        vDesDesfavorables2 = "Estilo de Lucha" +
                                  "<br /><br />" +
                                  "<h4>Acumula gran cantidad de hechos para apoyar sus ideas y espera que los otros reconozcan sus puntos de vista.</h4>" +
                                  "<br /><br />" +
                                  "<h4>Se vuelve obstinado, frío o reservado.</h4>" +
                                  "<br /><br />" +
                                  "<h4>Se 'sale' de la situación y espera que los demás vayan a él.</h4>";


                        vDesDesfavorables3 = "Cómo apoyarlo a superar sus excesos" +
                                 "<br /><br />" +
                                 "<h4>Trate de disminuir la tensión y la amenaza.</h4>" +
                                 "<br /><br />" +
                                " <h4>Lleve las emociones al mínimo.</h4>" +
                                 "<br /><br />" +
                                 "<h4>Trate un tono más ligero preferentemente con humor.</h4>" +
                                 "<br /><br />" +
                                 "<h4>Pida sugerencias sobre los criterios que podrían utilizarse para evaluar el problema.</h4>" +
                                " <br /><br />" +
                                 "<h4>Permita un cierto tiempo extra antes de tomar la decisión.</h4>" +
                                 "<br /><br />" +
                                 "<h4>Obtenga datos adicionales en los que la persona pueda confiar.</h4>";
                    }

                    if (LABORAL2_REP_ADNGD >= LABORAL2_REP_DAAPD & LABORAL2_REP_ADNGD >= LABORAL2_REP_TMCTD & LABORAL2_REP_ADNGD >= LABORAL2_REP_MTCSD)
                    //    divAND.Style.Value = String.Format("display: {0}", "block");
                    {
                        vDesDesfavorablesTitulo = "ESTILO LIFO ADAPTA Y NEGOCIA";

                        vDesDesfavorables1 = "Estilo de tensión" +
                                "<br /><br />" +
                                "<h4>Demasiado solícito.</h4>" +
                                "<br /><br />" +
                               " <h4>Se vuelve infantil y amigo de juguetear.</h4>" +
                                "<br /><br />" +
                                "<h4>Tiende a ser visto como un tonto a veces.</h4>" +
                                "<br /><br />" +
                                "<h4>Puede perder el sentido de su propia identidad.</h4>" +
                                "<br /><br />" +
                                "<h4>Se vuelve ambivalente y demasiado flexible.</h4>";

                        vDesDesfavorables2 = " Estilo de Lucha" +
                                "<br /><br />" +
                                "<h4>Renuncia a demasiadas cosas y da la impresión de estar de acuerdo</h4>" +
                                "<br /><br />" +
                                "<h4>Evita enfrentamientos aún cuando no crea que el otro tiene la razón.</h4>" +
                                "<br /><br />" +
                                "<h4>Mantiene la armonía a cualquier precio.</h4>";

                        vDesDesfavorables3 = "Cómo apoyarlo a superar sus excesos" +
                                "<br /><br />" +
                                "<h4>Déle la seguridad de que usted lo aprecia.</h4>" +
                                "<br /><br />" +
                                "<h4>Esté dispuesto a tratar de negociar.</h4>" +
                                "<br /><br />" +
                                "<h4>Sugiera que usted admira a las personas que son sinceras cuando no están de acuerdo.</h4>" +
                                "<br /><br />" +
                                "<h4>Use un enfoque positivo: 'Lo que me gusta de esto es'...'Las reservas que tengo son...'.</h4>" +
                                "<br /><br />" +
                                "<h4>Pase algo de tiempo en actitud amistosamente social, antes de exigir la decisión.</h4>" +
                                "<br /><br />" +
                               "<h4>Permítale a la persona 'conservar una buena fachada'.</h4>";
                    }

                    #endregion

                    decimal? LABORAL2_REP_TOTALF = vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-TOTALF")).FirstOrDefault().NO_VALOR;
                    decimal? LABORAL2_REP_AD_TOTALD = vResultadosLaboralII.Where(x => x.CL_VARIABLE.Equals("LABORAL2-REP-AD-TOTALD")).FirstOrDefault().NO_VALOR;

                    string lblF1 = DisplayGraf((Math.Round((decimal)LABORAL2_REP_DAAPF, 0)).ToString(), "F1", 1);
                    string lblF2 = DisplayGraf((Math.Round((decimal)LABORAL2_REP_TMCTF)).ToString(), "F2", 2);
                    string lblF3 = DisplayGraf((Math.Round((decimal)LABORAL2_REP_MTCSF)).ToString(), "F3", 3);
                    string lblF4 = DisplayGraf((Math.Round((decimal)LABORAL2_REP_ADNGF)).ToString(), "F4", 4);
                    //string lblTotalF = DisplayGraf((Math.Round((decimal)LABORAL2_REP_TOTALF)).ToString(), "1");
                    report.Dictionary.Variables["IMGF1"].Value = lblF1;
                    report.Dictionary.Variables["IMGF2"].Value = lblF2;
                    report.Dictionary.Variables["IMGF3"].Value = lblF3;
                    report.Dictionary.Variables["IMGF4"].Value = lblF4;

                    string lblD1 = DisplayGraf((Math.Round((decimal)LABORAL2_REP_DAAPD, 0)).ToString(), "D1", 1);
                    string lblD2 = DisplayGraf((Math.Round((decimal)LABORAL2_REP_TMCTD)).ToString(), "D2", 2);
                    string lblD3 = DisplayGraf((Math.Round((decimal)LABORAL2_REP_MTCSD)).ToString(), "D3", 3);
                    string lblD4 = DisplayGraf((Math.Round((decimal)LABORAL2_REP_ADNGD)).ToString(), "D4", 4);
                    //string lblTotalD = DisplayGraf((Math.Round((decimal)LABORAL2_REP_AD_TOTALD)).ToString(), "1");
                    report.Dictionary.Variables["IMGD1"].Value = lblD1;
                    report.Dictionary.Variables["IMGD2"].Value = lblD2;
                    report.Dictionary.Variables["IMGD3"].Value = lblD3;
                    report.Dictionary.Variables["IMGD4"].Value = lblD4;

                    string spF1 = DisplaySp((Math.Round((decimal)LABORAL2_REP_DAAPF, 0)).ToString(), "F1", 1);
                    string spF2 = DisplaySp((Math.Round((decimal)LABORAL2_REP_TMCTF)).ToString(), "F2", 2);
                    string spF3 = DisplaySp((Math.Round((decimal)LABORAL2_REP_MTCSF)).ToString(), "F3", 3);
                    string spF4 = DisplaySp((Math.Round((decimal)LABORAL2_REP_ADNGF)).ToString(), "F4", 4);
                    //string lblTotalF = DisplayGraf((Math.Round((decimal)LABORAL2_REP_TOTALF)).ToString(), "1");
                    report.Dictionary.Variables["F1"].Value = spF1;
                    report.Dictionary.Variables["F2"].Value = spF2;
                    report.Dictionary.Variables["F3"].Value = spF3;
                    report.Dictionary.Variables["F4"].Value = spF4;


                    string spD1 = DisplaySp((Math.Round((decimal)LABORAL2_REP_DAAPD, 0)).ToString(), "D1", 1);
                    string spD2 = DisplaySp((Math.Round((decimal)LABORAL2_REP_TMCTD)).ToString(), "D2", 2);
                    string spD3 = DisplaySp((Math.Round((decimal)LABORAL2_REP_MTCSD)).ToString(), "D3", 3);
                    string spD4 = DisplaySp((Math.Round((decimal)LABORAL2_REP_ADNGD)).ToString(), "D4", 4);
                    //string lblTotalD = DisplayGraf((Math.Round((decimal)LABORAL2_REP_AD_TOTALD)).ToString(), "1");

                    report.Dictionary.Variables["D1"].Value = spD1;
                    report.Dictionary.Variables["D2"].Value = spD2;
                    report.Dictionary.Variables["D3"].Value = spD3;
                    report.Dictionary.Variables["D4"].Value = spD4;

                    //string vUrl = "../Assets/images/PruebaLaboralII/LifoGraficaBase.gif";
                    //string vGrafica = "background-image: url(" + vUrl + ");";

                   // string strFileName1 = HttpContext.Current.Server.MapPath("~") + "/Assets/images/EstiloPensamientoCopy.jpg";


                    //using (var bmp1 = new Bitmap(strFileName1))
                    //using (var gr = Graphics.FromImage(bmp1))
                    //{
        
                    //    MemoryStream oMStream = new MemoryStream();
                    //    bmp1.Save(oMStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    //    byte[] imageBytes = oMStream.ToArray();
                    //    string vImagen = Convert.ToBase64String(imageBytes);

                    //    report.Dictionary.Variables["IMGF2"].Value = vImagen;
                    //}




                    //report.Dictionary.Variables["IMGD"].Value = "<div style='width:410px; height: 380px;'>" +
                    //        "<div style='background: #CCC; width: 100%;'>" +
                    //            "<b>Gráfica de condiciones desfavorables</b>" +
                    //        "</div>" +
                    //        "<div style='clear: both; height: 10px;'></div>" +
                    //        "<table style='" + vGrafica + "'>" +
                    //            "<tbody>" +
                    //                "<tr>" +
                    //                    "<td>" +
                    //                        "<div style='width:202px !important; height: 180px !important; vertical-align: bottom; text-align: right;'>" +
                    //                            "<img id='imgD1' src='" + lblD1 + "' style='margin: -3px 0px 0px 0px;'>" +
                    //                        "</div>" +
                    //                        "<div style='position: relative; float: right; bottom: 5px;'>" +
                    //                            "<span id='spD1' style='padding-right:5px;'>" + spD1 + "</span>" +
                    //                        "</div>" +
                    //                    "</td>" +
                    //                    "<td>" +
                    //                        "<div style=' width:202px !important; height: 180px !important; vertical-align: bottom; text-align: left;'>" +
                    //                            "<img id='imgD2' src='" + lblD2 + "' style='margin: -3px 0px 0px -205px;'>" +
                    //                        "</div>" +
                    //                        "<div style='position: relative; bottom: 5px;'>" +
                    //                            "<span id='spD2' style='padding-left:5px;'>" + spD2 + "</span>" +
                    //                        "</div>" +
                    //                    "</td>" +
                    //                "</tr>" +
                    //                "<tr>" +
                    //                    "<td>" +
                    //                        "<div style='position: relative; top: -5px; float: right;'>" +
                    //                            "<span id='spD3' style='padding-right:5px;'>" + spD3 + "</span>" +
                    //                        "</div>" +
                    //                        "<div style=' width:202px !important; height: 180px !important; vertical-align: top; text-align: right;'>" +
                    //                            "<img id='imgD3' src='" + lblD3 + "' style='margin: -235px 0px 5px 0px;'>" +
                    //                        "</div>" +
                    //                    "</td>" +
                    //                    "<td>" +
                    //                        "<div style='position: relative; top: 5px;'>" +
                    //                            "<span id='spD4' style='padding-left:5px;'>" + spD4 + "</span>" +
                    //                        "</div>" +
                    //                        "<div style='width:202px !important; height: 180px !important; vertical-align: top; text-align: right;'>" +
                    //                            "<img id='imgD4' src='" + lblD4 + "' style='margin: -224px 0px 0px -206px;'>" +
                    //                       " </div>" +

                    //                    "</td>" +
                    //                "</tr>" +
                    //            "</tbody>" +
                    //        "</table> </div>";

                    //report.Dictionary.Variables["IMGF"].Value = "<div style=width:410px; height: 380px;'>" +
                    //        "<div style='background: #CCC; width: 100%'>" +
                    //            "<b>Gráfica de condiciones favorables</b>" +
                    //        "</div>" +
                    //        "<div style='clear: both; height: 10px;'></div>" +

                    //        "<table style='" + vGrafica + "'>" +
                    //            "<tbody>" +
                    //                "<tr>" +
                    //                    "<td>" +
                    //                        "<div style='width:202px !important; height: 180px !important; vertical-align: bottom; text-align: right;'>" +
                    //                            "<img id='imgF1' src='" + lblF1 + "' style='margin: -3px 0px 0px 0px;'>" +
                    //                        "</div>" +
                    //                        "<div style='position: relative; float: right; bottom: 5px;'>" +
                    //                            "<span id='spF1' style='padding-right:5px;'>" + spF1 + "</span>" +
                    //                        "</div>" +
                    //                    "</td>" +
                    //                    "<td>" +
                    //                        "<div style='width:202px !important; height: 180px !important; vertical-align: bottom; text-align: left;'>" +
                    //                            "<img id='imgF2' src='" + lblF2 + "' style='margin: -3px 0px 0px -205px;'>" +
                    //                        "</div>" +
                    //                        "<div style='position: relative; bottom: 5px;'>" +
                    //                            "<span id='spF2' style='padding-left:5px;'>" + spF2 + "</span>" +
                    //                        "</div>" +
                    //                    "</td>" +
                    //                "</tr>" +
                    //                "<tr>" +
                    //                    "<td>" +
                    //                        "<div style='position: relative; top: -5px; float: right;'>" +
                    //                            "<span id='spF3' style='padding-right:5px;'>" + spF3 + "</span>" +
                    //                        "</div>" +
                    //                        "<div style='width:202px !important; height: 180px !important; vertical-align: top; text-align: right;'>" +
                    //                            "<img id='imgF3' src='" + lblF3 + "' style='margin: -235px 0px 5px 0px;'>" +
                    //                        "</div>" +

                    //                    "</td>" +
                    //                    "<td>" +
                    //                        "<div style='position: relative; top: 5px;'>" +
                    //                            "<span id='spF4' style='padding-left:5px;'>" + spF4 + "</span>" +
                    //                        "</div>" +
                    //                        "<div style='width:202px !important; height: 180px !important; vertical-align: top; text-align: left;'>" +
                    //                            "<img id='imgF4' src='" + lblF4 + "' style='margin: -224px 0px 0px -206px;'>" +
                    //                        "</div>" +

                    //                    "</td>" +
                    //                "</tr>" +
                    //            "</tbody>" +
                    //        "</table></div>";

                    report.Dictionary.Variables["DS_FAVORABLESTITULO"].Value = vDesFavorablesTitulo;
                    report.Dictionary.Variables["DS_FAVORABLES1"].Value = vDesFavorables1;
                    report.Dictionary.Variables["DS_FAVORABLES2"].Value = vDesFavorables2;
                    report.Dictionary.Variables["DS_FAVORABLES3"].Value = vDesFavorables3;
                    report.Dictionary.Variables["DS_FAVORABLES4"].Value = vDesFavorables4;
                    report.Dictionary.Variables["DS_FAVORABLES5"].Value = vDesFavorables5;
                    report.Dictionary.Variables["DS_FAVORABLES6"].Value = vDesFavorables6;
                    report.Dictionary.Variables["DS_FAVORABLES7"].Value = vDesFavorables7;
                    report.Dictionary.Variables["DS_FAVORABLES8"].Value = vDesFavorables8;
                    report.Dictionary.Variables["DS_FAVORABLES9"].Value = vDesFavorables9;

                    report.Dictionary.Variables["DS_DESFAVORABLESTITULO"].Value = vDesDesfavorablesTitulo;
                    report.Dictionary.Variables["DS_DESFAVORABLES1"].Value = vDesDesfavorables1;
                    report.Dictionary.Variables["DS_DESFAVORABLES2"].Value = vDesDesfavorables2;
                    report.Dictionary.Variables["DS_DESFAVORABLES3"].Value = vDesDesfavorables3;

                    report.Dictionary.Variables["DA"].Value = (Math.Round((decimal)LABORAL2_REP_DAAPF, 0)).ToString();
                    report.Dictionary.Variables["TM"].Value = (Math.Round((decimal)LABORAL2_REP_TMCTF)).ToString();
                    report.Dictionary.Variables["MT"].Value = (Math.Round((decimal)LABORAL2_REP_MTCSF)).ToString();
                    report.Dictionary.Variables["AD"].Value = (Math.Round((decimal)LABORAL2_REP_ADNGF)).ToString();
                    report.Dictionary.Variables["T1"].Value = (Math.Round((decimal)LABORAL2_REP_TOTALF)).ToString();

                    report.Dictionary.Variables["AP"].Value = (Math.Round((decimal)LABORAL2_REP_DAAPD, 0)).ToString();
                    report.Dictionary.Variables["CT"].Value = (Math.Round((decimal)LABORAL2_REP_TMCTD)).ToString();
                    report.Dictionary.Variables["CS"].Value = (Math.Round((decimal)LABORAL2_REP_MTCSD)).ToString();
                    report.Dictionary.Variables["NG"].Value = (Math.Round((decimal)LABORAL2_REP_ADNGD)).ToString();
                    report.Dictionary.Variables["T2"].Value = (Math.Round((decimal)LABORAL2_REP_AD_TOTALD)).ToString();

                }

                report.Dictionary.Variables["NB_CANDIDATO"].Value = candidato.NB_CANDIDATO;
                report.Dictionary.Variables["CL_SOLICITUD"].Value = candidato.CL_SOLICITUD;
            }
            report.Compile();
            swvReporte.Report = report;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Request.Params["IdBateria"] != null & Request.Params["ClToken"] != null & Request.Params["ClPrueba"] != null)
                {
                    vIdBateria = int.Parse(Request.Params["IdBateria"].ToString());
                    vClToken = Guid.Parse(Request.Params["ClToken"].ToString());
                    vClPrueba = Request.Params["ClPrueba"].ToString();
                    switch (vClPrueba)
                    {
                        case "LABORAL-1":
                            CargarResLaboral1();
                            break;
                        case "INTERES":
                            CargarInteres();
                            break;
                        case "PENSAMIENTO":
                            CargarEstilo();
                            break;
                        case "APTITUD-1":
                            CargarAptitud1();
                            break;
                        case "APTITUD-2":
                            CargarAptitud2();
                            break;
                        case "ORTOGRAFIA":
                            CargarOrtografia();
                            break;
                        case "TECNICAPC":
                            CargarTecnica();
                            break;
                        case "INGLES":
                            CargarIngles();
                            break;
                        case "ADAPTACION":
                            CargarAdaptacion();
                            break;
                        case "TIVA":
                            CargarTiva();
                            break;
                        case "LABORAL-2":
                            CargarLaboral2();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}