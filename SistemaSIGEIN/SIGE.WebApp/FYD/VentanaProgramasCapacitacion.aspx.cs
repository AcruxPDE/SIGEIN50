using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using SIGE.WebApp.MPC;
using Stimulsoft.Base.Json;
using Stimulsoft.Base.Json.Linq;
using Telerik.Web.UI;
using System.Data;
using System.Web.UI.HtmlControls;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaProgramasCapacitacion : System.Web.UI.Page
    {
        #region Variables

        private List<E_PROGRAMA_COMPETENCIA> vCompetencias
        {
            get { return (List<E_PROGRAMA_COMPETENCIA>)ViewState["vs_vCompetencias"]; }
            set { ViewState["vs_vCompetencias"] = value; }
        }

        private E_PERIODO vPeriodo
        {
            get { return (E_PERIODO)ViewState["vs_vPeriodo"]; }
            set { ViewState["vs_vPeriodos"] = value; }
        }

        private string ptipo
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }

        private List<E_PROGRAMA_EMPLEADO> vEmpleados
        {
            get { return (List<E_PROGRAMA_EMPLEADO>)ViewState["vs_vEmpleados"]; }
            set { ViewState["vs_vEmpleados"] = value; }
        }

        List<E_PROGRAMA_CAPACITACION> vProgramas
        {
            get { return (List<E_PROGRAMA_CAPACITACION>)ViewState["vs_vProgramas"]; }
            set { ViewState["vs_vProgramas"] = value; }
        }

        List<E_PROGRAMA_CAPACITACION> vProgramasMatriz
        {
            get {return(List<E_PROGRAMA_CAPACITACION>)ViewState["vs_vProgramasMatriz"];}
            set {ViewState["vs_vProgramasMatriz"] = value;}
        }

        public int? vIdPrograma
        {
            get { return (int?)ViewState["vs_vIdPrograma"]; }
            set { ViewState["vs_vIdPrograma"] = value; }
        }

        private bool vFgProgramaModificado {
            get { return (bool)ViewState["vs_vpc_fg_programa_modificado"]; }
            set { ViewState["vs_vpc_fg_programa_modificado"] = value; }
        }

        E_PROGRAMA pProgramaCapacitacion
        {
            get { return (E_PROGRAMA)ViewState["vs_pProgramaCapacitacion"]; }
            set { ViewState["vs_pProgramaCapacitacion"] = value; }
        }

        E_NOTA pNota
        {
            get { return (E_NOTA)ViewState["vs_pNota"]; }
            set { ViewState["vs_pNota"] = value; }
        }

        List<E_PROGRAMA_COMPETENCIA> vlstCompetencias
        {
            get { return (List<E_PROGRAMA_COMPETENCIA>)ViewState["vs_vlstCompetencias"]; }
            set { ViewState["vs_vlstCompetencias"] = value; }
        }

        List<E_PROGRAMA_EMPLEADO> vlstParticipantes
        {
            get { return (List<E_PROGRAMA_EMPLEADO>)ViewState["vs_vlstParticipantes"]; }
            set { ViewState["vs_vlstParticipantes"] = value; }
        }

        private string vClUsuario;

        private string vNbPrograma;

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        #endregion

        #region Metodos

        public void ValidarBotones(bool pHabilitar)
        {
            btnAgregarCompetencia.Enabled = pHabilitar;
            btnEliminarCompetencia.Enabled = pHabilitar;
            btnAgregarParticipantes.Enabled = pHabilitar;
            btnEliminarParticipantes.Enabled = pHabilitar;
            btnAgregarCombinaciones.Enabled = pHabilitar;
            btnQuitarSeleccionados.Enabled = pHabilitar;
        }

        public void valdarDsNotas()
        {
            if (!pProgramaCapacitacion.DS_NOTAS.ToString().Equals(""))
            {
                XElement vNotas = XElement.Parse(pProgramaCapacitacion.DS_NOTAS.ToString());
                if (ValidarRamaXml(vNotas, "NOTA"))
                {
                    pNota = vNotas.Elements("NOTA").Select(el => new E_NOTA
                    {
                        DS_NOTA = UtilXML.ValorAtributo<string>(el.Attribute("DS_NOTA")),
                        FE_NOTA = UtilXML.ValorAtributo<DateTime>(el.Attribute("FE_NOTA")),

                    }).FirstOrDefault();

                    if (pNota.DS_NOTA != null)
                        radEditorNotas.Content = pNota.DS_NOTA.ToString();
                }
            }
        }

        public void cargarProgramaCapacitacion(XElement vProgramaCapacitacion)
        {
            if (ValidarRamaXml(vProgramaCapacitacion, "PROGRAMAS"))
            {
                pProgramaCapacitacion = vProgramaCapacitacion.Element("PROGRAMAS").Elements("PROGRAMA").Select(el => new E_PROGRAMA
                {
                    ID_PROGRAMA = (int)UtilXML.ValorAtributo(el.Attribute("ID_PROGRAMA"), E_TIPO_DATO.INT),
                    ID_PERIODO = (el.Attribute("ID_PROGRAMA") != null) ? (int?)UtilXML.ValorAtributo(el.Attribute("ID_PERIODO"), E_TIPO_DATO.INT) : null,
                    CL_PROGRAMA = el.Attribute("CL_PROGRAMA").Value,
                    NB_PROGRAMA = el.Attribute("NB_PROGRAMA").Value,
                    CL_ESTADO = el.Attribute("CL_ESTADO").Value,
                    CL_TIPO_PROGRAMA = el.Attribute("CL_TIPO_PROGRAMA").Value,
                    VERSION = (el.Attribute("CL_VERSION") != null) ? el.Attribute("CL_VERSION").Value : "",
                    DS_NOTAS = (el.Attribute("DS_NOTAS") != null) ? el.Attribute("DS_NOTAS").Value : "",
                    ID_DOCUMENTO_AUTORIZACION = (el.Attribute("ID_DOCUMENTO_AUTORIZACION") != null) ? ((int)UtilXML.ValorAtributo(el.Attribute("ID_DOCUMENTO_AUTORIZACION"), E_TIPO_DATO.INT)) : 0,
                    CL_DOCUMENTO = "",
                    NO_COMPETENCIAS = 0,
                    NO_PARTICIPANTES = 0,
                    FE_CREACION = DateTime.Now
                }).FirstOrDefault();
            }


            if (ValidarRamaXml(vProgramaCapacitacion, "COMPETENCIAS"))
            {
                vCompetencias = vProgramaCapacitacion.Element("COMPETENCIAS").Elements("COMPETENCIA").Select(el => new E_PROGRAMA_COMPETENCIA
                {
                    ID_PROGRAMA_COMPETENCIA = (int)UtilXML.ValorAtributo(el.Attribute("ID_PROGRAMA_COMPETENCIA"), E_TIPO_DATO.INT),
                    ID_PROGRAMA = (int)UtilXML.ValorAtributo(el.Attribute("ID_PROGRAMA"), E_TIPO_DATO.INT),
                    ID_COMPETENCIA = (int)UtilXML.ValorAtributo(el.Attribute("ID_COMPETENCIA"), E_TIPO_DATO.INT),
                    NB_COMPETENCIA = el.Attribute("NB_COMPETENCIA").Value,
                    CL_COMPETENCIA = el.Attribute("CL_COMPETENCIA").Value,
                    NB_CLASIFICACION = el.Attribute("NB_CLASIFICACION").Value,
                    NB_CATEGORIA = el.Attribute("NB_CATEGORIA").Value
                }).ToList();
            }

            if (ValidarRamaXml(vProgramaCapacitacion, "EMPLEADOS"))
            {
                vEmpleados = vProgramaCapacitacion.Element("EMPLEADOS").Elements("EMPLEADO").Select(el => new E_PROGRAMA_EMPLEADO
                {
                    ID_PROGRAMA_EMPLEADO = (int)UtilXML.ValorAtributo(el.Attribute("ID_PROGRAMA_EMPLEADO"), E_TIPO_DATO.INT),
                    ID_PROGRAMA = (int)UtilXML.ValorAtributo(el.Attribute("ID_PROGRAMA"), E_TIPO_DATO.INT),
                    ID_EMPLEADO = (int)UtilXML.ValorAtributo(el.Attribute("ID_EMPLEADO"), E_TIPO_DATO.INT),
                    ID_AUXILIAR = (el.Attribute("ID_AUXILIAR") != null) ? (int?)UtilXML.ValorAtributo(el.Attribute("ID_AUXILIAR"), E_TIPO_DATO.INT) : 0,
                    NB_EMPLEADO = el.Attribute("NB_EMPLEADO").Value,
                    CL_EMPLEADO = el.Attribute("CL_EMPLEADO").Value,
                    NB_PUESTO = el.Attribute("NB_PUESTO").Value,
                    CL_PUESTO = el.Attribute("CL_PUESTO").Value,
                    NB_DEPARTAMENTO = el.Attribute("NB_DEPARTAMENTO").Value
                }).ToList();
            }

            if (ValidarRamaXml(vProgramaCapacitacion, "PROGRAMAS_CAPACITACION"))
            {
                vProgramas = vProgramaCapacitacion.Element("PROGRAMAS_CAPACITACION").Elements("PROGRAMA_CAPACITACION").Select(el => new E_PROGRAMA_CAPACITACION
                {
                    ID_PROGRAMA_EMPLEADO_COMPETENCIA = (int)UtilXML.ValorAtributo(el.Attribute("ID_PROGRAMA_EMPLEADO_COMPETENCIA"), E_TIPO_DATO.INT),
                    ID_PROGRAMA = (int)UtilXML.ValorAtributo(el.Attribute("ID_PROGRAMA"), E_TIPO_DATO.INT),
                    ID_PROGRAMA_COMPETENCIA = (int)UtilXML.ValorAtributo(el.Attribute("ID_PROGRAMA_COMPETENCIA"), E_TIPO_DATO.INT),
                    ID_PROGRAMA_EMPLEADO = (int)UtilXML.ValorAtributo(el.Attribute("ID_PROGRAMA_EMPLEADO"), E_TIPO_DATO.INT),
                    ID_EMPLEADO = (int)UtilXML.ValorAtributo(el.Attribute("ID_EMPLEADO"), E_TIPO_DATO.INT),
                    ID_COMPETENCIA = (int)UtilXML.ValorAtributo(el.Attribute("ID_COMPETENCIA"), E_TIPO_DATO.INT),
                    CL_PRIORIDAD = el.Attribute("CL_PRIORIDAD").Value,
                    PR_RESULTADO = (decimal)UtilXML.ValorAtributo(el.Attribute("PR_RESULTADO"), E_TIPO_DATO.DECIMAL),
                    ID_PERIODO = (el.Attribute("ID_PERIODO") != null) ? (int?)UtilXML.ValorAtributo(el.Attribute("ID_PERIODO"), E_TIPO_DATO.INT) : null,
                    NB_PROGRAMA = el.Attribute("NB_PROGRAMA").Value,
                    CL_TIPO_PROGRAMA = el.Attribute("CL_TIPO_PROGRAMA").Value,
                    CL_ESTADO = el.Attribute("CL_ESTADO").Value,
                    CL_VERSION = (el.Attribute("CL_VERSION") != null) ? el.Attribute("CL_VERSION").Value : "",
                    ID_DOCUMENTO_AUTORIZACION = (int?)UtilXML.ValorAtributo(el.Attribute("ID_DOCUMENTO_AUTORIZACION"), E_TIPO_DATO.INT),
                    NB_COMPETENCIA = el.Attribute("NB_COMPETENCIA").Value,
                    NB_CLASIFICACION_COMPETENCIA = el.Attribute("NB_CLASIFICACION").Value,
                    CL_TIPO_COMPETENCIA = el.Attribute("NB_CATEGORIA").Value,
                    NB_EVALUADO = el.Attribute("NB_EMPLEADO").Value,
                    CL_EVALUADO = el.Attribute("CL_EMPLEADO").Value,
                    NB_PUESTO = el.Attribute("NB_PUESTO").Value,
                    CL_PUESTO = el.Attribute("CL_PUESTO").Value,
                    NB_DEPARTAMENTO = el.Attribute("NB_DEPARTAMENTO").Value,
                    CL_PERIODO = (el.Attribute("CL_PERIODO") != null) ? el.Attribute("CL_PERIODO").Value : "",
                    NB_PERIODO = (el.Attribute("NB_PERIODO") != null) ? el.Attribute("NB_PERIODO").Value : "",
                    DS_PERIODO = (el.Attribute("DS_PERIODO") != null) ? el.Attribute("DS_PERIODO").Value : "",
                    TIPO_EVALUACION = (el.Attribute("TIPO_EVALUACION") != null) ? el.Attribute("TIPO_EVALUACION").Value : ""
                }).ToList();
            }


            if (ValidarRamaXml(vProgramaCapacitacion, "PERIODOS"))
            {
                vPeriodo = vProgramaCapacitacion.Element("PERIODOS").Elements("PERIODO").Select(el => new E_PERIODO
                {
                    ID_PERIODO = UtilXML.ValorAtributo<int>(el.Attribute("ID_PERIODO")),
                    CL_PERIODO = UtilXML.ValorAtributo<string>(el.Attribute("CL_PRIORIDAD")),
                    NB_PERIODO = UtilXML.ValorAtributo<string>(el.Attribute("NB_PERIODO")),
                    DS_PERIODO = UtilXML.ValorAtributo<string>(el.Attribute("DS_PERIODO")),
                    FE_INICIO = UtilXML.ValorAtributo<DateTime>(el.Attribute("FE_INICIO")),
                    FE_TERMINO = UtilXML.ValorAtributo<DateTime?>(el.Attribute("FE_TERMINO")),
                    CL_ESTADO_PERIODO = UtilXML.ValorAtributo<string>(el.Attribute("CL_ESTADO_PERIODO")),
                    CL_TIPO_PERIODO = UtilXML.ValorAtributo<string>(el.Attribute("CL_TIPO_PERIODO")),
                    DS_NOTAS = UtilXML.ValorAtributo<string>(el.Attribute("DS_NOTAS")),
                    ID_BITACORA = UtilXML.ValorAtributo<int?>(el.Attribute("ID_BITACORA")),
                    XML_CAMPOS_ADICIONALES = "",
                    TIPO_EVALUACION = UtilXML.ValorAtributo<string>(el.Attribute("TIPO_EVALUACION"))
                }).FirstOrDefault();
            }

        }

        public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);
            if (foundEl != null)
            {
                return true;
            }

            return false;
        }

        public void cargarComboPrioridades()
        {
            List<string> prioridades = new List<string>();
            prioridades.Add(E_PRIORIDAD.ALTA.ToString());
            prioridades.Add(E_PRIORIDAD.BAJA.ToString());
            prioridades.Add(E_PRIORIDAD.INTERMEDIA.ToString());
            cmbPrioridades.DataSource = prioridades;
            cmbPrioridades.DataBind();
        }

        protected void CargarDatosEmpleado(List<int> vSeleccionados)
        {
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
            var empleados = nEmpleado.ObtenerEmpleados(pID_EMPRESA: ContextoUsuario.oUsuario.ID_EMPRESA);

            var filtroEmpleado = empleados.Where(x => vSeleccionados.Contains(x.M_EMPLEADO_ID_EMPLEADO)).ToList();
            vlstParticipantes = cargarParseEmpleados(filtroEmpleado);
            grdParticipantes.Rebind();

        }

        protected void CargarDatosCompetencia(List<int> vSeleccionados)
        {
            CompetenciaNegocio nCompetencia = new CompetenciaNegocio();
            var competencias = nCompetencia.ObtieneCompetencias();

            var lstCompetencias = competencias.Where(x => vSeleccionados.Contains(x.ID_COMPETENCIA)).ToList();
            vlstCompetencias = cargarParseCompetencias(null, lstCompetencias);
            grdCompetencias.Rebind();
        }

        public List<E_PROGRAMA_COMPETENCIA> RevisarCompetenciasSeleccionadas(List<E_PROGRAMA_COMPETENCIA> competencias)
        {
            if (competencias.Count > 0)
            {
                foreach (var competen in competencias)
                {
                    var result = vCompetencias.FirstOrDefault(x => x.ID_COMPETENCIA == competen.ID_COMPETENCIA);
                    if (result == null)
                    {
                        vCompetencias.Add(competen);
                    }
                }
            }
            return vCompetencias;
        }

        public List<E_PROGRAMA_EMPLEADO> RevisarEmpleadosSeleccionados(List<E_PROGRAMA_EMPLEADO> empleados)
        {
            if (empleados.Count > 0)
            {
                foreach (var empleado in empleados)
                {

                    var result = vEmpleados.FirstOrDefault(x => x.ID_EMPLEADO == empleado.ID_EMPLEADO);
                    if (result == null)
                    {
                        vEmpleados.Add(empleado);
                    }
                }
            }
            return vEmpleados;
        }

        public Boolean validadCombinacionExistente(int pIdEmpleado, int pIdCompetencia)
        {
            Boolean vExiste = false; ;

            var vCombinacion = vProgramas.Where(item => item.ID_COMPETENCIA == pIdCompetencia && item.ID_EMPLEADO == pIdEmpleado).FirstOrDefault();
            if (vCombinacion != null)
            {
                vExiste = true;
            }
            return vExiste;
        }

        IEnumerable<char> CharsToTitleCase(string s)
        {
            bool newWord = true;
            foreach (char c in s)
            {
                if (newWord) { yield return Char.ToUpper(c); newWord = false; }
                else yield return Char.ToLower(c);
                if (c == ' ') newWord = true;
            }
        }

        public List<E_PROGRAMA_COMPETENCIA> cargarParseCompetencias(List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result> pKprogramaCompetencia, List<SPE_OBTIENE_C_COMPETENCIA_Result> pCprogramaCompetencia)
        {
            List<E_PROGRAMA_COMPETENCIA> competencias = new List<E_PROGRAMA_COMPETENCIA>();


            if (pKprogramaCompetencia != null)
            {
                foreach (var item in pKprogramaCompetencia)
                {
                    competencias.Add(new E_PROGRAMA_COMPETENCIA
                    {
                        ID_PROGRAMA_COMPETENCIA = item.ID_PROGRAMA_COMPETENCIA,
                        ID_PROGRAMA = item.ID_PROGRAMA,
                        ID_COMPETENCIA = item.ID_COMPETENCIA,
                        NB_COMPETENCIA = item.NB_COMPETENCIA,
                        CL_COMPETENCIA = item.CL_COMPETENCIA,
                        NB_CLASIFICACION = item.NB_CLASIFICACION,
                        NB_CATEGORIA = item.NB_CATEGORIA,
                        CL_PROGRAMA = item.CL_PROGRAMA,
                        NB_PROGRAMA = item.NB_PROGRAMA
                    });
                }
            }

            else
            {
                foreach (var item in pCprogramaCompetencia)
                {
                    competencias.Add(new E_PROGRAMA_COMPETENCIA
                    {
                        ID_PROGRAMA_COMPETENCIA = 0,
                        ID_PROGRAMA = 0,
                        ID_COMPETENCIA = item.ID_COMPETENCIA,
                        NB_COMPETENCIA = item.NB_COMPETENCIA,
                        CL_COMPETENCIA = item.CL_COMPETENCIA,
                        NB_CLASIFICACION = item.NB_CLASIFICACION_COMPETENCIA,
                        NB_CATEGORIA = item.NB_TIPO_COMPETENCIA,
                        CL_PROGRAMA = "",
                        NB_PROGRAMA = ""
                    });
                }

            }
            return competencias;
        }

        public List<E_PROGRAMA_EMPLEADO> cargarParseEmpleados(List<SPE_OBTIENE_EMPLEADOS_Result> lista)
        {
            List<E_PROGRAMA_EMPLEADO> empleados = new List<E_PROGRAMA_EMPLEADO>();
            if (lista.Count > 0)
            {
                foreach (var item in lista)
                {
                    empleados.Add(new E_PROGRAMA_EMPLEADO
                    {
                        ID_PROGRAMA_EMPLEADO = 0,
                        ID_PROGRAMA = 0,
                        ID_EMPLEADO = item.M_EMPLEADO_ID_EMPLEADO,
                        ID_AUXILIAR = item.C_EMPRESA_ID_EMPRESA,
                        NB_EMPLEADO = item.M_EMPLEADO_NB_EMPLEADO_COMPLETO,
                        CL_EMPLEADO = item.M_EMPLEADO_CL_EMPLEADO,
                        NB_PUESTO = item.M_PUESTO_NB_PUESTO,
                        CL_PUESTO = item.M_PUESTO_CL_PUESTO,
                        NB_DEPARTAMENTO = item.M_DEPARTAMENTO_NB_DEPARTAMENTO,
                        CL_PROGRAMA = "",
                        NB_PROGRAMA = "",
                    });
                }
            }
            return empleados;
        }

        public List<E_PROGRAMA_CAPACITACION> cargarParseProgramasCapacitacion(List<SPE_OBTIENE_K_PROGRAMA_Result> lista)
        {
            List<E_PROGRAMA_CAPACITACION> programas = new List<E_PROGRAMA_CAPACITACION>();
            if (lista.Count > 0)
            {
                foreach (var item in lista)
                {
                    programas.Add(new E_PROGRAMA_CAPACITACION
                    {
                        ID_PROGRAMA_EMPLEADO_COMPETENCIA = item.ID_PROGRAMA_EMPLEADO_COMPETENCIA,
                        ID_PROGRAMA = item.ID_PROGRAMA,
                        ID_PROGRAMA_COMPETENCIA = item.ID_PROGRAMA_COMPETENCIA,
                        ID_PROGRAMA_EMPLEADO = item.ID_PROGRAMA_EMPLEADO,
                        CL_PRIORIDAD = item.CL_PRIORIDAD,
                        PR_RESULTADO = item.PR_RESULTADO,
                        CL_PROGRAMA = item.CL_PROGRAMA,
                        ID_PERIODO = item.ID_PERIODO,
                        NB_PROGRAMA = item.NB_PROGRAMA,
                        CL_TIPO_PROGRAMA = item.CL_TIPO_PROGRAMA,
                        CL_ESTADO = item.CL_ESTADO,
                        CL_VERSION = item.CL_VERSION,
                        ID_DOCUMENTO_AUTORIZACION = item.ID_DOCUMENTO_AUTORIZACION,
                        NB_COMPETENCIA = item.NB_COMPETENCIA,
                        NB_CLASIFICACION_COMPETENCIA = item.NB_CLASIFICACION_COMPETENCIA,
                        CL_TIPO_COMPETENCIA = item.CL_TIPO_COMPETENCIA,
                        CL_COLOR = item.CL_COLOR,
                        NB_EVALUADO = item.NB_EVALUADO,
                        CL_EVALUADO = item.CL_EVALUADO,
                        NB_PUESTO = item.NB_PUESTO,
                        CL_PUESTO = item.CL_PUESTO,
                        NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
                        ID_COMPETENCIA = item.ID_COMPETENCIA,
                        ID_EMPLEADO = item.ID_EMPLEADO,
                        CL_PERIODO = item.CL_PERIODO,
                        NB_PERIODO = item.NB_PERIODO,
                        DS_PERIODO = item.DS_PERIODO,
                        TIPO_EVALUACION = item.TIPO_EVALUACION
                    });
                }
            }
            return programas;
        }


        public void GuardarProgramaCapacitacion()
        {
            var vClEstatusPrograma = (!ptipo.Equals("Editar")) ? new string(CharsToTitleCase(txtEstadoProgCapacitacion.Text).ToArray()) : new string(CharsToTitleCase(pProgramaCapacitacion.CL_ESTADO.ToString()).ToArray());
            XElement vXelementNotas = null;

            string plIdPeriodo = "";
            string plIdPrograma = "";
            string plVersion = "";
            string plIdDocumentoAutorizacion = "0";

            var vXelementNota = new XElement("NOTA",
          new XAttribute("FE_NOTA", (!ptipo.Equals("Editar")) ? DateTime.Now.ToString() : (pNota != null) ? DateTime.Now.ToString() : DateTime.Now.ToString()),
          new XAttribute("DS_NOTA", (!ptipo.Equals("Editar")) ? radEditorNotas.Content : radEditorNotas.Content));

            vXelementNotas = new XElement("NOTAS", vXelementNota);

            //if (vProgramas.Count > 0)
            //{
            //    //vClEstatusPrograma = "Terminado";
            //}



            if (pProgramaCapacitacion != null)
            {
                if (pProgramaCapacitacion.ID_PERIODO.HasValue)
                {
                    plIdPeriodo = pProgramaCapacitacion.ID_PERIODO.ToString();
                }

                plIdPrograma = pProgramaCapacitacion.ID_PROGRAMA.ToString();
                plVersion = pProgramaCapacitacion.VERSION;
                plIdDocumentoAutorizacion = pProgramaCapacitacion.ID_DOCUMENTO_AUTORIZACION.ToString();
            }

            var vXelementProgramaCapacitacion =
                  new XElement("PROGRAMA",
                  new XAttribute("ID_PROGRAMA", (!ptipo.Equals("Editar")) ? "0" : plIdPrograma),
                  new XAttribute("ID_PERIODO", ((!ptipo.Equals("Editar")) && (!ptipo.Equals("Copy"))) ? "0" : plIdPeriodo),
                //new XAttribute("CL_PROGRAMA", (!ptipo.Equals("Editar")) ? txtClProgCapacitacion.Text : pProgramaCapacitacion.CL_PROGRAMA.ToString()),
                    new XAttribute("CL_PROGRAMA", txtClProgCapacitacion.InnerText),
                //new XAttribute("NB_PROGRAMA", (!ptipo.Equals("Editar")) ? txtNbProgCapacitacion.Text : pProgramaCapacitacion.NB_PROGRAMA.ToString()),
                new XAttribute("NB_PROGRAMA", txtNbProgCapacitacion.InnerText),
                //new XAttribute("CL_TIPO_PROGRAMA", (!ptipo.Equals("Editar")) ? txtTipoProgCapacitacion.Text : pProgramaCapacitacion.CL_TIPO_PROGRAMA.ToString()),
                new XAttribute("CL_TIPO_PROGRAMA", txtTipoProgCapacitacion.InnerText),
                //new XAttribute("CL_VERSION", (!ptipo.Equals("Editar")) ? "" : pProgramaCapacitacion.VERSION.ToString()),
                new XAttribute("CL_VERSION", plVersion),
                  new XAttribute("CL_ESTADO", vClEstatusPrograma),
                  new XAttribute("DS_NOTAS", (vXelementNotas != null) ? vXelementNotas.ToString() : ""),
                //new XAttribute("ID_DOCUMENTO_AUTORIZACION", (!ptipo.Equals("Editar")) ? 0 : pProgramaCapacitacion.ID_DOCUMENTO_AUTORIZACION
                  new XAttribute("ID_DOCUMENTO_AUTORIZACION", plIdDocumentoAutorizacion)
       );

            var vXelementCompetencias = vCompetencias.Select(x =>
                                     new XElement("COMPETENCIA",
                                     new XAttribute("ID_PROGRAMA_COMPETENCIA", (!ptipo.Equals("Editar")) ? "0" : x.ID_PROGRAMA_COMPETENCIA.ToString()),
                                     new XAttribute("ID_COMPETENCIA", x.ID_COMPETENCIA),
                                     new XAttribute("NB_COMPETENCIA", x.NB_COMPETENCIA),
                                     new XAttribute("NB_CLASIFICACION", x.NB_CLASIFICACION),
                                     new XAttribute("NB_CATEGORIA", x.NB_CATEGORIA)
                          ));

            var vXelementEmpleados = vEmpleados.Select(x =>
                         new XElement("EMPLEADO",
                         new XAttribute("ID_PROGRAMA_EMPLEADO", (!ptipo.Equals("Editar")) ? "0" : x.ID_PROGRAMA_EMPLEADO.ToString()),
                         new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO),
                          new XAttribute("ID_AUXILIAR", x.ID_AUXILIAR),
                         new XAttribute("NB_EMPLEADO", x.NB_EMPLEADO),
                         new XAttribute("CL_EMPLEADO", x.CL_EMPLEADO),
                         new XAttribute("NB_PUESTO", x.NB_PUESTO),
                         new XAttribute("CL_PUESTO", x.CL_PUESTO),
                         new XAttribute("NB_DEPARTAMENTO", x.NB_DEPARTAMENTO)
              ));

            var vXelementProgramasCapacitacion =
             vProgramas.Select(x => new XElement("PROGRAMA_CAPACITACION",
             new XAttribute("ID_PROGRAMA_EMPLEADO_COMPETENCIA", (!ptipo.Equals("Editar")) ? "0" : x.ID_PROGRAMA_EMPLEADO_COMPETENCIA.ToString()),
             new XAttribute("CL_PRIORIDAD", x.CL_PRIORIDAD),
             new XAttribute("PR_RESULTADO", x.PR_RESULTADO),
             new XAttribute("ID_PROGRAMA_EMPLEADO", x.ID_PROGRAMA_EMPLEADO),
             new XAttribute("ID_PROGRAMA_COMPETENCIA", x.ID_PROGRAMA_COMPETENCIA),
             new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO),
             new XAttribute("ID_COMPETENCIA", x.ID_COMPETENCIA)
                 ));


            XElement programaCapacitacion =
                new XElement("CONFIGURACION",
                new XElement("PROGRAMAS", vXelementProgramaCapacitacion),
                new XElement("COMPETENCIAS", vXelementCompetencias),
                new XElement("EMPLEADOS", vXelementEmpleados),
                new XElement("PROGRAMAS_CAPACITACION", vXelementProgramasCapacitacion)
                );

            ProgramaNegocio nPrograma = new ProgramaNegocio();

            if (ptipo.Equals("Editar"))
            {
                vIdPrograma = vIdPrograma;
            }
            else
            {
                vIdPrograma = null;
            }

            E_RESULTADO vResultado = nPrograma.InsertaActualizaProgramaCapacitacion(vIdPrograma, programaCapacitacion.ToString(), vClUsuario, vNbPrograma, ContextoUsuario.oUsuario.ID_EMPRESA, vFgProgramaModificado);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
        }

        public string ObtieneDiv(string pClPrioridad)
        {
            string vDivColor = "divNa";
            switch (pClPrioridad.ToUpper())
            {
                case "ALTA":
                    vDivColor = "divNecesario";
                    break;
                case "BAJA":
                    vDivColor = "divBajo";
                    break;
                case "INTERMEDIA":
                    vDivColor = "divIntermedio";
                    break;
            }

            return vDivColor;
        }

        public DataTable ObtieneMatrizCapacitacion()
        {
            DataTable vTabla = new DataTable();
            string vDivsCeldasChk = "<table class=\"tabladnc\"> " +
               "<tr> " +
               "<td class=\"porcentaje\"> " +
               "<div class=\"divPorcentaje\">{0}</div> " +
               "</td> " +
               "<td class=\"color\"> " +
               "<div class=\"{1}\">&nbsp;</div> " +
               "</td> " +
               "<td class=\"check\"> " +
               "<div class=\"divCheckbox\"> <input type=\"checkbox\" runat=\"server\" class=\"{4}\" id=\"{2}\" value=\"{2}\" {3}> </div> " +
               "</td> </tr> </table>";

            vTabla.Columns.Add("ID_EMPLEADO", typeof(int));
            vTabla.Columns.Add("CL_EMPLEADO",typeof(string));
            vTabla.Columns.Add("NB_EMPLEADO", typeof(string));
            vTabla.Columns.Add("CL_PUESTO", typeof(string));
            vTabla.Columns.Add("NB_PUESTO", typeof(string));

            foreach (var item in vCompetencias)
            {
                vTabla.Columns.Add(item.ID_COMPETENCIA.ToString());
            }

            foreach (var item in vEmpleados)
            {
                DataRow vRenglon = vTabla.NewRow();

                vRenglon["ID_EMPLEADO"] = item.ID_EMPLEADO;
                vRenglon["CL_EMPLEADO"] = item.CL_EMPLEADO;
                vRenglon["NB_EMPLEADO"] = item.NB_EMPLEADO;
                vRenglon["CL_PUESTO"] = item.CL_PUESTO;
                vRenglon["NB_PUESTO"] = item.NB_PUESTO;

                foreach (var itemCompetencia in vCompetencias)
                {
                    var vParticipanteCompetencia = vProgramas.Where(w => w.ID_EMPLEADO == item.ID_EMPLEADO && w.ID_COMPETENCIA == itemCompetencia.ID_COMPETENCIA).FirstOrDefault();
                    if (vParticipanteCompetencia != null)
                    {
                        vRenglon[itemCompetencia.ID_COMPETENCIA.ToString()] = String.Format(vDivsCeldasChk, vParticipanteCompetencia.PR_RESULTADO.ToString() +"%", ObtieneDiv(vParticipanteCompetencia.CL_PRIORIDAD),"C" +itemCompetencia.ID_COMPETENCIA.ToString() + "E" + item.ID_EMPLEADO.ToString(), "checked", "Datos");
                    }
                    else
                    {
                        vRenglon[itemCompetencia.ID_COMPETENCIA.ToString()] = String.Format(vDivsCeldasChk, "N/A", "divNa", "C" + itemCompetencia.ID_COMPETENCIA.ToString() + "E" + item.ID_EMPLEADO.ToString(), "", "NoNecesaria");
                    }
                }

                vTabla.Rows.Add(vRenglon);
            }

            return vTabla;
        }

        private void ConfigurarColumna(GridColumn pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna)
        {
            if (pGenerarEncabezado)
            {
                pEncabezado = GeneraEncabezado(pColumna);
            }

            pColumna.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumna.HeaderStyle.Font.Bold = true;
            pColumna.HeaderText = pEncabezado;
            pColumna.Visible = pVisible;
            pColumna.AutoPostBackOnFilter = true;
            
            

            if (pFiltrarColumna & pVisible)
            {
                (pColumna as GridBoundColumn).CurrentFilterFunction = GridKnownFunction.Contains;
                if (pWidth <= 60)
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth);
                }
                else
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 60);
                }
            }
            else
            {
                (pColumna as GridBoundColumn).CurrentFilterFunction = GridKnownFunction.Contains;
                (pColumna as GridBoundColumn).AllowFiltering = false;
            }
        }

        private string GeneraEncabezado(GridColumn pColumna)
        {
            string vEncabezado = "";

            E_PROGRAMA_COMPETENCIA vCompetencia = vCompetencias.Where(w => w.ID_COMPETENCIA.ToString() == pColumna.UniqueName.ToString()).FirstOrDefault();

            vEncabezado = "<div title=\"" + vCompetencia.NB_CLASIFICACION + "\" style=\"writing-mode: tb-rl;height: 100px;font-size: 9pt; font-weight:bold; \">" + vCompetencia.NB_COMPETENCIA + "</div>";

            return vEncabezado;
        }

        private void GenerarProgramaMatriz(E_SELECTOR_NECESIDADES pSeleccionados)
        {
           vProgramasMatriz = new List<E_PROGRAMA_CAPACITACION>();
            int vIdCompetencia;
            int vIdEmpleado;

            int vIndexCompetencia;
            int vIndexEmpleado;

            foreach (E_SELECTOR_SELECCION item in pSeleccionados.oSeleccion)
            {
                if (item.control != "")
                {
                    vIndexCompetencia = item.control.IndexOf('C');
                    vIndexEmpleado = item.control.IndexOf("E");

                    vIdCompetencia = int.Parse(item.control.Substring(vIndexCompetencia + 1, vIndexEmpleado - 1));
                    vIdEmpleado = int.Parse(item.control.Substring(vIndexEmpleado + 1, item.control.Length - (vIndexEmpleado + 1)));

                    E_PROGRAMA_CAPACITACION vItem = vProgramas.Where(w => w.ID_COMPETENCIA == vIdCompetencia && w.ID_EMPLEADO == vIdEmpleado).FirstOrDefault();
                    if (vItem != null)
                    vProgramas.Remove(vItem);
                }
            }


            GuardarProgramaCapacitacion();

        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                CompetenciaNegocio nEmpleado = new CompetenciaNegocio();
                EmpleadoNegocio nCompetencia = new EmpleadoNegocio();
                ProgramaNegocio nPrograma = new ProgramaNegocio();

                vFgProgramaModificado = false;

                vCompetencias = new List<E_PROGRAMA_COMPETENCIA>();
                vEmpleados = new List<E_PROGRAMA_EMPLEADO>();
                vProgramas = new List<E_PROGRAMA_CAPACITACION>();
                grdCompetencias.DataSource = vCompetencias;
                grdParticipantes.DataSource = vEmpleados;
                grdCategorias.DataSource = vProgramas;
                cargarComboPrioridades();

                ptipo = Request.QueryString["TIPO"];

                if (ptipo.Equals("Editar"))
                {
                    vIdPrograma = int.Parse((Request.QueryString["ID"]));
                    XElement vProgramaCapacitacion = nPrograma.ObtenerProgramaCapacitacionCompleto(vIdPrograma, ContextoUsuario.oUsuario.ID_EMPRESA);

                    if (vProgramaCapacitacion != null)
                    {
                        cargarProgramaCapacitacion(vProgramaCapacitacion);

                        txtClProgCapacitacion.InnerText = pProgramaCapacitacion.CL_PROGRAMA.ToString();
                        txtEstadoProgCapacitacion.Text = pProgramaCapacitacion.CL_ESTADO.ToString();
                        txtNbProgCapacitacion.InnerText = pProgramaCapacitacion.NB_PROGRAMA.ToString();
                        txtTipoProgCapacitacion.InnerText = pProgramaCapacitacion.CL_TIPO_PROGRAMA.ToString();
                        cmbPrioridades.SelectedValue = E_PRIORIDAD.ALTA.ToString();
                        valdarDsNotas();

                        ValidarBotones(!pProgramaCapacitacion.CL_ESTADO.ToUpper().Equals("TERMINADO"));

                        grdCategorias.DataSource = vProgramas;
                        grdParticipantes.DataSource = vEmpleados;
                        grdCompetencias.DataSource = vCompetencias;
                    }
                }
                else if (ptipo.Equals("Copy"))
                {

                    vIdPrograma = int.Parse((Request.QueryString["ID"]));
                    XElement vProgramaCapacitacion = nPrograma.ObtenerProgramaCapacitacionCompleto(vIdPrograma, ContextoUsuario.oUsuario.ID_EMPRESA);

                    if (vProgramaCapacitacion != null)
                    {
                        cargarProgramaCapacitacion(vProgramaCapacitacion);

                        grdCategorias.DataSource = vProgramas;
                        grdParticipantes.DataSource = vEmpleados;
                        grdCompetencias.DataSource = vCompetencias;

                        cmbPrioridades.SelectedValue = vProgramas.ElementAt(0).CL_PRIORIDAD.ToString();
                        txtTipoProgCapacitacion.InnerText = "Copia";
                        txtEstadoProgCapacitacion.Text = "Elaborando";
                    }

                }
                else
                {
                    txtEstadoProgCapacitacion.Text = new string(CharsToTitleCase(E_ESTADO_PROGRAMA_CAPACITACION.ELABORANDO.ToString()).ToArray());
                    txtTipoProgCapacitacion.InnerText = "A partir de 0";
                    cmbPrioridades.SelectedIndex = 0;
                }

                if (ContextoApp.FYD.ClVistaPrograma.ClVistaPrograma == "MATRIZ")
                {
                    tbProgramaCapacitacion.Tabs[1].Visible = false;
                    RPView2.Visible = false;
                    btnAceptar.Visible = false;
                    btnAgregarCombinaciones.Visible = false;
                }
               else if (ContextoApp.FYD.ClVistaPrograma.ClVistaPrograma == "MACROS")
                {
                    tbProgramaCapacitacion.Tabs[2].Visible = false;
                    rpvMatriz.Visible = false;
                    btnAceptarMatriz.Visible = false;
                    btnAgregarCombinacionesMatriz.Visible = false;
                }
            }
        }

        protected void ramProgramasCapacitacion_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            String json = e.Argument;
            String[] stringArray = json.Split("_".ToCharArray());
            E_SELECTOR_NECESIDADES vSeleccionados = new E_SELECTOR_NECESIDADES();

            if (stringArray.Length > 1)
            {
                if (stringArray[1] == "EMPLEADO")
                {
                    List<int> listaSeleccionados = JsonConvert.DeserializeObject<List<int>>(stringArray[0]);
                    CargarDatosEmpleado(listaSeleccionados);
                    vFgProgramaModificado = true;
                }
                else if (stringArray[1] == "COMPETENCIA")
                {
                    List<int> listaSeleccionados = JsonConvert.DeserializeObject<List<int>>(stringArray[0]);
                    CargarDatosCompetencia(listaSeleccionados);
                    vFgProgramaModificado = true;
                }
            }
            else
            {
                string pParameter = e.Argument;
                vSeleccionados = JsonConvert.DeserializeObject<E_SELECTOR_NECESIDADES>(pParameter);
                GenerarProgramaMatriz(vSeleccionados);
            }
            
        }

        protected void grdCategorias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCategorias.DataSource = vProgramas;
        }

        protected void grdCompetencias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCompetencias.DataSource = RevisarCompetenciasSeleccionadas(vlstCompetencias);
        }

        protected void grdParticipantes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdParticipantes.DataSource = RevisarEmpleadosSeleccionados(vlstParticipantes);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            GuardarProgramaCapacitacion();
        }

        protected void btnEliminarCompetencia_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdCompetencias.SelectedItems.Count; i++)
            {
                GridDataItem item = grdCompetencias.SelectedItems[i] as GridDataItem;
                int idCompetencia = int.Parse(item.GetDataKeyValue("ID_COMPETENCIA").ToString());
                var vCompetenciaEliminada = vCompetencias.Where(x => x.ID_COMPETENCIA == idCompetencia).FirstOrDefault();
                vCompetencias.Remove(vCompetenciaEliminada);
                vFgProgramaModificado = true;
            }

            string vMensaje = "Proceso exitoso";
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "");
            grdCompetencias.DataSource = vCompetencias;
            grdCompetencias.Rebind();
        }

        protected void btnEliminarEmpleado_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdParticipantes.SelectedItems.Count; i++)
            {
                GridDataItem item = grdParticipantes.SelectedItems[i] as GridDataItem;
                int idEmpleado = int.Parse(item.GetDataKeyValue("ID_EMPLEADO").ToString());
                var vEmpleadoEliminado = vEmpleados.Where(x => x.ID_EMPLEADO == idEmpleado).FirstOrDefault();
                vEmpleados.Remove(vEmpleadoEliminado);
                vFgProgramaModificado = true;
            }
            string vMensaje = "Proceso exitoso";
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "");
            grdParticipantes.DataSource = vEmpleados;
            grdParticipantes.Rebind();
        }

        protected void btnQuitarSeleccionados_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdCategorias.SelectedItems.Count; i++)
            {
                GridDataItem item = grdCategorias.SelectedItems[i] as GridDataItem;
                int idProgramaCapacitacion = int.Parse(item.GetDataKeyValue("ID_PROGRAMA_EMPLEADO_COMPETENCIA").ToString());
                var vEmpleadoEliminado = vProgramas.Where(x => x.ID_PROGRAMA_EMPLEADO_COMPETENCIA == idProgramaCapacitacion).FirstOrDefault();
                vProgramas.Remove(vEmpleadoEliminado);
                vFgProgramaModificado = true;
            }

            string vMensaje = "Proceso exitoso";
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "");
            grdCategorias.DataSource = vProgramas;
            grdCategorias.Rebind();
        }

        protected void btnAgregarCombinaciones_Click(object sender, EventArgs e)
        {
            int contador = 1;
            for (int i = 0; i < grdCompetencias.SelectedItems.Count; i++)
            {
                GridDataItem item = grdCompetencias.SelectedItems[i] as GridDataItem;
                int idCompetencia = int.Parse(item.GetDataKeyValue("ID_COMPETENCIA").ToString());
                var competencia = vCompetencias.Where(x => x.ID_COMPETENCIA == idCompetencia).FirstOrDefault();

                for (int j = 0; j < grdParticipantes.SelectedItems.Count; j++)
                {
                    GridDataItem item2 = grdParticipantes.SelectedItems[j] as GridDataItem;
                    int idEmpleado = int.Parse(item2.GetDataKeyValue("ID_EMPLEADO").ToString());
                    var Empleado = vEmpleados.Where(x => x.ID_EMPLEADO == idEmpleado).FirstOrDefault();


                    if (!validadCombinacionExistente(Empleado.ID_EMPLEADO, competencia.ID_COMPETENCIA))
                    {
                        E_PROGRAMA_CAPACITACION oProg = new E_PROGRAMA_CAPACITACION
                        {
                            ID_PROGRAMA_EMPLEADO_COMPETENCIA = contador++,
                            ID_PROGRAMA = 0,
                            ID_PROGRAMA_COMPETENCIA = competencia.ID_PROGRAMA_COMPETENCIA,
                            ID_PROGRAMA_EMPLEADO = Empleado.ID_PROGRAMA_EMPLEADO,
                            CL_PRIORIDAD = cmbPrioridades.SelectedItem.Value,
                            PR_RESULTADO = 0,
                            CL_PROGRAMA = txtClProgCapacitacion.InnerText,
                            //ID_PERIODO = pProgramaCapacitacion.ID_PERIODO,
                            NB_PROGRAMA = txtNbProgCapacitacion.InnerText,
                            CL_TIPO_PROGRAMA = txtTipoProgCapacitacion.InnerText,
                            CL_ESTADO = txtEstadoProgCapacitacion.Text,
                            CL_VERSION = "",
                            ID_DOCUMENTO_AUTORIZACION = 0,
                            NB_COMPETENCIA = competencia.NB_COMPETENCIA.ToString(),
                            NB_CLASIFICACION_COMPETENCIA = competencia.NB_CLASIFICACION.ToString(),
                            CL_TIPO_COMPETENCIA = competencia.NB_CATEGORIA.ToString(),
                            NB_EVALUADO = Empleado.NB_EMPLEADO.ToString(),
                            CL_EVALUADO = Empleado.CL_EMPLEADO.ToString(),
                            NB_PUESTO = Empleado.NB_PUESTO.ToString(),
                            CL_PUESTO = Empleado.CL_PUESTO.ToString(),
                            NB_DEPARTAMENTO = Empleado.NB_DEPARTAMENTO.ToString(),
                            ID_EMPLEADO = Empleado.ID_EMPLEADO,
                            ID_AUXILIAR = Empleado.ID_AUXILIAR,
                            ID_COMPETENCIA = competencia.ID_COMPETENCIA,
                        };

                        if (vPeriodo != null)
                        {
                            oProg.ID_PERIODO = vPeriodo.ID_PERIODO;
                            oProg.CL_PERIODO = vPeriodo.CL_PERIODO;
                            oProg.NB_PERIODO = vPeriodo.NB_PERIODO;
                            oProg.DS_PERIODO = vPeriodo.DS_PERIODO;
                            oProg.TIPO_EVALUACION = vPeriodo.TIPO_EVALUACION;
                        }

                        vProgramas.Add(oProg);
                        vFgProgramaModificado = true;
                    }
                    else
                    {
                        vProgramas.Where(t => t.ID_COMPETENCIA == idCompetencia && t.ID_EMPLEADO == idEmpleado).FirstOrDefault().CL_PRIORIDAD = cmbPrioridades.SelectedItem.Value;
                        vFgProgramaModificado = true;
                    }
                }
            }

            if (vProgramas != null && vProgramas.Count > 0)
            {
                //grdCategorias.DataSource = vProgramas;
                grdCategorias.Rebind();

                string vMensaje = "Proceso exitoso";
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 150, "");
                //grdCategorias.DataSource = vProgramas;
                grdCategorias.Rebind();
                grdCapacitacionMatriz.Rebind();
            }
            else
            {
                string vMensaje = "No se ha seleccionado ninguna combinación";
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, 400, 150, "");
            }
        }

        protected void grdCapacitacionMatriz_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCapacitacionMatriz.DataSource = ObtieneMatrizCapacitacion();
        }

        protected void grdCapacitacionMatriz_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            switch (e.Column.UniqueName)
            {
                case "ID_EMPLEADO":
                    ConfigurarColumna(e.Column, 0, "Id empleado", false, false, true);
                    break;
                case "CL_EMPLEADO":
                    ConfigurarColumna(e.Column, 80, "No. de empleado", true, false, true);
                    break;
                case "NB_EMPLEADO":
                    ConfigurarColumna(e.Column, 150, "Nombre completo", true, false, true);
                    break;
                case "CL_PUESTO":
                    ConfigurarColumna(e.Column, 80, "Clave de puesto", true, false, true);
                    break;
                case "NB_PUESTO":
                    ConfigurarColumna(e.Column, 150, "Puesto", true, false, true);
                    break;
                case "ExpandColumn":
                    break;
                default:
                    ConfigurarColumna(e.Column, 100, "", true, true, false);
                    break;
            }
        }
    }
}