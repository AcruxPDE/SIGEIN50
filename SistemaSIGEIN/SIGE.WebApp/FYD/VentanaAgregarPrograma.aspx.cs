using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaAgregarPrograma : System.Web.UI.Page
    {
        #region Variables

        private int? vIdRol;
        private List<E_PROGRAMA_COMPETENCIA> vCompetencias
        {
            get { return (List<E_PROGRAMA_COMPETENCIA>)ViewState["vs_vCompetencias"]; }
            set { ViewState["vs_vCompetencias"] = value; }
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

        RadListBoxItem vNoSeleccionado = new RadListBoxItem("No seleccionado", String.Empty);

        private E_PERIODO vPeriodo
        {
            get { return (E_PERIODO)ViewState["vs_vPeriodo"]; }
            set { ViewState["vs_vPeriodo"] = value; }
        }

        private string ptipo
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }

        int? vIdPrograma
        {
            get { return (int?)ViewState["vs_vIdPrograma"]; }
            set { ViewState["vs_vIdPrograma"] = value; }
        }

        private bool vFgProgramaModificado
        {
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


        private string vClUsuario;
        private string vNbPrograma;

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        #endregion

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

        protected string GenerarXmlProgramaPrograma(int pIdPeriodoDNC)
        {

            NecesidadesCapacitacionNegocio nNegocio = new NecesidadesCapacitacionNegocio();
            List<E_NECESIDADES_CAPACITACION> vLstDnc = new List<E_NECESIDADES_CAPACITACION>();
            List<SPE_OBTIENE_NECESIDADES_CAPACITACION_Result> vListaDnc = new List<SPE_OBTIENE_NECESIDADES_CAPACITACION_Result>();
            vListaDnc = nNegocio.obtenerNecesidadesCapacitacion(pIdPeriodoDNC, null, "<PRIORIDADES><PRIORIDAD NOMBRE='Alta' /><PRIORIDAD NOMBRE='Intermedia' /></PRIORIDADES>", vIdRol);

            vLstDnc = vListaDnc.Select(t => new E_NECESIDADES_CAPACITACION
            {
                ID_PERIODO = t.ID_PERIODO,
                CL_TIPO_COMPETENCIA = t.CL_TIPO_COMPETENCIA,
                NB_TIPO_COMPETENCIA = t.NB_TIPO_COMPETENCIA,
                CL_CLASIFICACION = t.CL_CLASIFICACION,
                NB_CLASIFICACION_COMPETENCIA = t.NB_CLASIFICACION_COMPETENCIA,
                DS_COMPETENCIA = t.DS_COMPETENCIA,
                CL_COLOR = t.CL_COLOR,
                ID_COMPETENCIA = t.ID_COMPETENCIA,
                NB_COMPETENCIA = t.NB_COMPETENCIA,
                ID_EMPLEADO = t.ID_EMPLEADO,
                CL_EVALUADO = t.CL_EVALUADO,
                NB_EVALUADO = t.NB_EVALUADO,
                ID_PUESTO = t.ID_PUESTO,
                CL_PUESTO = t.CL_PUESTO,
                NB_PUESTO = t.NB_PUESTO,
                ID_DEPARTAMENTO = t.ID_DEPARTAMENTO,
                CL_DEPARTAMENTO = t.CL_DEPARTAMENTO,
                NB_DEPARTAMENTO = t.NB_DEPARTAMENTO,
                PR_RESULTADO = t.PR_RESULTADO,
                NB_PRIORIDAD = t.NB_PRIORIDAD
            }).ToList();


            XElement xmlCapacitaciones = new XElement("CAPACITACIONES");

            xmlCapacitaciones.Add(new XAttribute("CL_PROGRAMA", txtClProgCapacitacion.Text), new XAttribute("NB_PROGRAMA", txtNbProgCapacitacion.Text), new XAttribute("ID_PERIODO", pIdPeriodoDNC + ""));

            foreach (E_NECESIDADES_CAPACITACION item in vLstDnc){
                xmlCapacitaciones.Add(new XElement("CAPACITACION",
                new XAttribute("ID_EMPLEADO", item.ID_EMPLEADO.ToString()),
                new XAttribute("NB_EMPLEADO", item.NB_EVALUADO),
                new XAttribute("CL_EMPLEADO", item.CL_EVALUADO),
                new XAttribute("CL_PUESTO", item.CL_PUESTO),
                new XAttribute("NB_PUESTO", item.NB_PUESTO),
                new XAttribute("NB_DEPARTAMENTO", item.NB_DEPARTAMENTO),
                new XAttribute("ID_COMPETENCIA", item.ID_COMPETENCIA),
                new XAttribute("NB_COMPETENCIA", item.NB_COMPETENCIA),
                new XAttribute("NB_CLASIFICACION", item.CL_CLASIFICACION),
                new XAttribute("NB_CATEGORIA", item.CL_TIPO_COMPETENCIA),
                new XAttribute("CL_PRIORIDAD", item.NB_PRIORIDAD),
                new XAttribute("PR_RESULTADO", item.PR_RESULTADO)));
            }

            return xmlCapacitaciones.ToString();
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
                    new XAttribute("CL_PROGRAMA", txtClProgCapacitacion.Text),
                //new XAttribute("NB_PROGRAMA", (!ptipo.Equals("Editar")) ? txtNbProgCapacitacion.Text : pProgramaCapacitacion.NB_PROGRAMA.ToString()),
                new XAttribute("NB_PROGRAMA", txtNbProgCapacitacion.Text),
                //new XAttribute("CL_TIPO_PROGRAMA", (!ptipo.Equals("Editar")) ? txtTipoProgCapacitacion.Text : pProgramaCapacitacion.CL_TIPO_PROGRAMA.ToString()),
               // new XAttribute("CL_TIPO_PROGRAMA", txtTipoProgCapacitacion.Text),
                new XAttribute("CL_TIPO_PROGRAMA", rbDnc.Checked ? "Desde DNC" : rbCopia.Checked ? "Copia" : "A partir de 0"),
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

            if (rbDnc.Checked && ptipo != "Editar")
            {
                if (lstPeriodo.SelectedValue != "")
                {
                    NecesidadesCapacitacionNegocio neg = new NecesidadesCapacitacionNegocio();
                    string vXmlProgramaDnc = GenerarXmlProgramaPrograma(int.Parse(lstPeriodo.SelectedValue.ToString()));

                    E_RESULTADO res = neg.InsertaActualizaProgramaDesdeDNC(vIdPrograma, vXmlProgramaDnc.ToString(), vClUsuario, vNbPrograma);
                    string vMensaje = res.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, res.CL_TIPO_ERROR, pCallBackFunction: "ReturnDataToParent");
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Selecciona el período para generar desde DNC.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
                    desdeDNC.Style.Add("display", "block");
                    return;
                }
            }
            else
            {
                E_RESULTADO vResultado = nPrograma.InsertaActualizaProgramaCapacitacion(vIdPrograma, programaCapacitacion.ToString(), vClUsuario, vNbPrograma, ContextoUsuario.oUsuario.ID_EMPRESA, vFgProgramaModificado);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                if(ptipo == "Editar")
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "ReturnDataToParentEdit");
                else
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "ReturnDataToParent");

            }
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
                    CL_PERIODO = UtilXML.ValorAtributo<string>(el.Attribute("CL_PERIODO")),
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

        protected void SeguridadProcesos()
        {
            btnAceptar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.B.A");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!IsPostBack)
            {
                vFgProgramaModificado = false;
                ProgramaNegocio nPrograma = new ProgramaNegocio();
                ptipo = Request.QueryString["TIPO"];

                vCompetencias = new List<E_PROGRAMA_COMPETENCIA>();
                vEmpleados = new List<E_PROGRAMA_EMPLEADO>();
                vProgramas = new List<E_PROGRAMA_CAPACITACION>();

                if (ptipo.Equals("Editar"))
                {
                    vIdPrograma = int.Parse((Request.QueryString["ID"]));
                    XElement vProgramaCapacitacion = nPrograma.ObtenerProgramaCapacitacionCompleto(vIdPrograma, ContextoUsuario.oUsuario.ID_EMPRESA);

                    SeguridadProcesos();

                    if (vProgramaCapacitacion != null)
                    {
                        cargarProgramaCapacitacion(vProgramaCapacitacion);
                       
                        txtClProgCapacitacion.Text = pProgramaCapacitacion.CL_PROGRAMA.ToString();
                        txtEstadoProgCapacitacion.Text = pProgramaCapacitacion.CL_ESTADO.ToString();
                        txtNbProgCapacitacion.Text = pProgramaCapacitacion.NB_PROGRAMA.ToString();
                        if (pProgramaCapacitacion.CL_TIPO_PROGRAMA.ToString() == "A partir de 0")
                        {
                            rbCero.Checked = true;
                            rbCopia.Enabled = false;
                            rbDnc.Enabled = false;
                        }
                        else if (pProgramaCapacitacion.CL_TIPO_PROGRAMA.ToString() == "Desde DNC")
                        {
                            rbCero.Enabled = false;
                            rbCopia.Enabled = false;
                            rbDnc.Checked = true;
                            desdeDNC.Style.Add("display", "block");
                            lstPeriodo.Items.Add((vPeriodo.CL_PERIODO != null) ? new RadListBoxItem(vPeriodo.CL_PERIODO, vPeriodo.ID_PERIODO.ToString()) : vNoSeleccionado);
                            lstPeriodo.Items.FirstOrDefault().Selected = true;
                            btnBuscarPeriodo.Enabled = false;
                            btnEliminarPeriodo.Enabled = false;
                        }
                        else if (pProgramaCapacitacion.CL_TIPO_PROGRAMA.ToString() == "Copia")
                        {
                            rbCopia.Visible = true;
                            rbCero.Enabled = false;
                            rbCopia.Checked = true;
                            rbDnc.Enabled = false;

                        }

                      //  txtTipoProgCapacitacion.Text = pProgramaCapacitacion.CL_TIPO_PROGRAMA.ToString();
                        valdarDsNotas();
                    }
                }
                else if (ptipo.Equals("Copy"))
                {

                    vIdPrograma = int.Parse((Request.QueryString["ID"]));
                    XElement vProgramaCapacitacion = nPrograma.ObtenerProgramaCapacitacionCompleto(vIdPrograma, ContextoUsuario.oUsuario.ID_EMPRESA);

                    if (vProgramaCapacitacion != null)
                    {
                        cargarProgramaCapacitacion(vProgramaCapacitacion);
                        rbCopia.Visible = true;
                        rbCopia.Checked = true;
                        rbDnc.Enabled = false;
                        rbCero.Enabled = false;
                       // txtTipoProgCapacitacion.Text = "Copia";
                        txtEstadoProgCapacitacion.Text = "Elaborando";
                    }

                }
                else
                {
                    rbDnc.Checked = true;
                    desdeDNC.Style.Add("display", "block");
                    txtEstadoProgCapacitacion.Text = new string(CharsToTitleCase(E_ESTADO_PROGRAMA_CAPACITACION.ELABORANDO.ToString()).ToArray());
                    lstPeriodo.Items.Add(vNoSeleccionado);
                  //  txtTipoProgCapacitacion.Text = "A partir de 0";
                }

            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            GuardarProgramaCapacitacion();
        }
    }
}