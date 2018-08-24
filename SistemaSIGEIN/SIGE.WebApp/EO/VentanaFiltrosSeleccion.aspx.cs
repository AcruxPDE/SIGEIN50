using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO
{
    public partial class VentanaFiltrosSeleccion : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;


        public int? vIdPeriodo
        {
            get { return(int?)ViewState["vs_vIdPeriodo"];}
            set { ViewState["vs_vIdPeriodo"]=value;}
        }

        public string vXmlFiltrosSel
        {
            get { return (string)ViewState["vs_vXmlFiltros"];}
            set { ViewState["vs_vXmlFiltros"]= value;}
        }

        public bool? vFgFiltroSeleccionado
        {
            get { return (bool?)ViewState["vs_vFgFiltroSeleccionado"]; }
            set { ViewState["vs_vFgFiltroSeleccionado"] = value; }
        }

        public bool vFgEvaluados
        {
            get { return(bool)ViewState["vs_vFgEvaluados"];}
            set { ViewState["vs_vFgEvaluados"] = value; }
        }

        #endregion

        #region Funciones

        public void FiltrosIndice()
        {
            List<E_SELECCIONADOS> vDepartamentos = new List<E_SELECCIONADOS>();
            List<E_ADICIONALES_SELECCIONADOS> vAdicionales = new List<E_ADICIONALES_SELECCIONADOS>();
            XElement vXlmFiltros = new XElement("FILTROS");
            XElement vXlmDepartamentos = new XElement("DEPARTAMENTOS");
            XElement vXlmGeneros = new XElement("GENEROS");
            XElement vXlmEdad = new XElement("EDAD");
            XElement vXlmAntiguedad = new XElement("ANTIGUEDAD");
            XElement vXlmCamposAdicional = new XElement("CAMPOS_ADICIONALES");
            vFgFiltroSeleccionado = false;

            foreach (RadListBoxItem item in rlbDepartamento.Items)
            {
                int vIdDepartamento = int.Parse(item.Value);
                vDepartamentos.Add(new E_SELECCIONADOS { ID_SELECCIONADO = vIdDepartamento });
                vFgFiltroSeleccionado = true;
            }
            var vXelements = vDepartamentos.Select(x => new XElement("DEPARTAMENTO", new XAttribute("ID_DEPARTAMENTO", x.ID_SELECCIONADO)));
            vXlmDepartamentos = new XElement("DEPARTAMENTOS", vXelements);
            vXlmFiltros.Add(vXlmDepartamentos);

            foreach (RadListBoxItem item in rlbAdicionales.Items)
            {
                string vClAdicional = item.Value;
                vAdicionales.Add(new E_ADICIONALES_SELECCIONADOS { CL_CAMPO = vClAdicional });
                vFgFiltroSeleccionado = true;
            }
            var vXelementsAdicionales = vAdicionales.Select(x => new XElement("ADICIONAL", new XAttribute("CL_CAMPO", x.CL_CAMPO)));
            vXlmCamposAdicional = new XElement("ADICIONALES", vXelementsAdicionales);
            vXlmFiltros.Add(vXlmCamposAdicional);

            foreach (RadListBoxItem item in rlbGenero.Items)
            {
                vXlmGeneros = new XElement("GENERO", new XAttribute("NB_GENERO", item.Value));
                vFgFiltroSeleccionado = true;
            }
            vXlmFiltros.Add(vXlmGeneros);

            if (rbEdad.Checked == true)
            {
                vXlmEdad = new XElement("EDAD", new XAttribute("EDAD_INICIAL", rntEdadInicial.Text), new XAttribute("EDAD_FINAL", rntEdadFinal.Text));
                vXlmFiltros.Add(vXlmEdad);
                vFgFiltroSeleccionado = true;
            }
            if (rbAntiguedad.Checked == true)
            {
                vXlmAntiguedad = new XElement("ANTIGUEDAD", new XAttribute("ANTIGUEDAD_INICIAL", rntAntiguedadInicial.Text), new XAttribute("ANTIGUEDAD_FINAL", rntAntiguedadFinal.Text));
                vXlmFiltros.Add(vXlmAntiguedad);
                vFgFiltroSeleccionado = true;
            }
            if (vFgFiltroSeleccionado != false)
            vXmlFiltrosSel = vXlmFiltros.ToString();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.Params["ID_PERIODO"] != null)
                    vIdPeriodo = int.Parse(Request.Params["ID_PERIODO"].ToString());

                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                var vFiltros = nClima.ObtenerFiltrosEvaluadores(vIdPeriodo).FirstOrDefault();
                if (vFiltros != null)
                {
                    if (vFiltros.XML_FILTROS != null)
                    {
                        if (vFiltros.XML_FILTROS.Contains("DEPARTAMENTO"))
                        {
                            
                        }

                        if (vFiltros.XML_FILTROS.Contains("ADICIONAL"))
                        {

                        }

                        if (vFiltros.XML_FILTROS.Contains("NB_GENERO"))
                        {

                        }

                        if (vFiltros.XML_FILTROS.Contains("EDAD_INICIAL"))
                        {

                        }

                        if (vFiltros.XML_FILTROS.Contains("ANTIGUEDAD_INICIAL"))
                        {

                        }


                    }
                }

                vFgEvaluados = false;
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            FiltrosIndice();
            if (vXmlFiltrosSel != null)
            {
                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                E_RESULTADO vResultado = nClima.InsertaEvaluadoresFiltro(vIdPeriodo, vXmlFiltrosSel, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "generateDataForParent");
                if (vResultado.CL_TIPO_ERROR != E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    vFgFiltroSeleccionado = false;
                }
                else
                {
                    int vCountEvaluados = nClima.ObtieneEvaluadoresClima(vIdPeriodo).Count;
                    if (vCountEvaluados > 0)
                    {
                        vFgEvaluados = true;
                    }
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Aplique por lo menos un filtro para seleccionar evaluadores.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                
            }
            
        }
    }
}