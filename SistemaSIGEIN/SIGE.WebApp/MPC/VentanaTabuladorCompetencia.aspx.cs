using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.Entidades;
using Telerik.Web.UI;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;



namespace SIGE.WebApp.MPC
{
    public partial class VentanaTabuladorCompetencia : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int? vIdCompetencia
        {
            get { return (int?)ViewState["vs_vIdCompetencia"]; }
            set { ViewState["vs_vIdCompetencia"] = value; }
        }
        private int vIdTabulador
        {
            get { return (int)ViewState["vs_vIdTabulador"]; }
            set { ViewState["vs_vIdTabulador"] = value; }
        }

        private string vTipoTransaccion = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                vIdTabulador = int.Parse((Request.QueryString["IDtabulador"]));

                if (Request.QueryString["IDFactor"] != null)
                {
                    vIdCompetencia = int.Parse((Request.QueryString["IDFactor"]));
                    TabuladoresNegocio nTabuladorCompetencia = new TabuladoresNegocio();
                    var vTavuladorCompetencia = nTabuladorCompetencia.ObtenieneCompetenciasTabulador(vIdCompetencia).FirstOrDefault();
                    txtNombre.Text = vTavuladorCompetencia.NB_COMPETENCIA;
                    txtDescripccion.Text = vTavuladorCompetencia.DS_COMPETENCIA;
                    vIdTabulador = vTavuladorCompetencia.ID_TABULADOR;
                    radEditorPuesto0.Content = vTavuladorCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N0;
                    radEditorPuesto1.Content = vTavuladorCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N1;
                    radEditorPuesto2.Content = vTavuladorCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N2;
                    radEditorPuesto3.Content = vTavuladorCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N3;
                    radEditorPuesto4.Content = vTavuladorCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N4;
                    radEditorPuesto5.Content = vTavuladorCompetencia.DS_NIVEL_COMPETENCIA_PUESTO_N5;
                }
                else
                {
                    radEditorPuesto0.Content = "Este valor no aplica para el adecuado desempeño del puesto.";
                    radEditorPuesto1.Content = "El desempeño del puesto demanda solamente lo mínimo para aplicar este valor, pudiendo hacerlo bajo asistencia o supervisión. Los resultados de su trabajo se verán poco afectados por este valor.";
                    radEditorPuesto2.Content = "El puesto requiere de una aplicación regular de este valor, los resultados de su trabajo se facilitarían con este, sin embargo pudieran lograrse resultados con el adecuado desarrollo de otras competencias.";
                    radEditorPuesto3.Content = "El puesto requiere de un dominio ligeramente por encima del promedio de la competencia, los resultados de su trabajo dependerán frecuentemente del adecuado desarrollo de esta capacidad .";
                    radEditorPuesto4.Content = "El puesto requiere de una aplicación alta del valor, los resultados de su trabajo dependerán de manera importante de la aplicación de este.";
                    radEditorPuesto5.Content = "Requiere una aplicación superior y profunda de este valor. Los resultados de su trabajo dependen directamente del desarrollo de esta capacidad.";
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["IDFactor"] != null)
            {
                vTipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();
            }
            else
            {
                vTipoTransaccion = E_TIPO_OPERACION_DB.I.ToString();
            }

            E_EMPLEADOS_SELECCIONADOS vEmpleados = new E_EMPLEADOS_SELECCIONADOS();
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            vEmpleados.ID_TABULADOR_FACTOR = vIdCompetencia; 

            XElement vXmlCompetencia = new XElement("COMPETENCIAS",
              new XElement("COMPETENCIA",
                      new XAttribute("NB_COMPETENCIA", txtNombre.Text),
                      new XAttribute("DS_COMPETENCIA", txtDescripccion.Text)));

            XElement vXmlNiveles = new XElement("NIVELES",
                new XElement("NIVEL",
                    new XAttribute("CL_NIVEL_FACTOR", "NIVEL_00"),
                    new XAttribute("NB_FACTOR_NIVEL", "NIVEL 0"),
                    new XAttribute("DS_NIVEL_FACTOR_PUESTO", radEditorPuesto0.Content),
                    new XAttribute("NO_VALOR_NIVEL", 0)
                    ),
                new XElement("NIVEL",
                    new XAttribute("CL_NIVEL_FACTOR", "NIVEL_01"),
                    new XAttribute("NB_FACTOR_NIVEL", "NIVEL 1"),
                    new XAttribute("DS_NIVEL_FACTOR_PUESTO", radEditorPuesto1.Content),
                    new XAttribute("NO_VALOR_NIVEL", 1)
                    ),
            new XElement("NIVEL",
                    new XAttribute("CL_NIVEL_FACTOR", "NIVEL_02"),
                    new XAttribute("NB_FACTOR_NIVEL", "NIVEL 2"),
                    new XAttribute("DS_NIVEL_FACTOR_PUESTO", radEditorPuesto2.Content),
                    new XAttribute("NO_VALOR_NIVEL", 2)
                    ),
            new XElement("NIVEL",
                    new XAttribute("CL_NIVEL_FACTOR", "NIVEL_03"),
                    new XAttribute("NB_FACTOR_NIVEL", "NIVEL 3"),
                    new XAttribute("DS_NIVEL_FACTOR_PUESTO", radEditorPuesto3.Content),
                    new XAttribute("NO_VALOR_NIVEL", 3)
                    ),
            new XElement("NIVEL",
                    new XAttribute("CL_NIVEL_FACTOR", "NIVEL_04"),
                    new XAttribute("NB_FACTOR_NIVEL", "NIVEL 4"),
                    new XAttribute("DS_NIVEL_FACTOR_PUESTO", radEditorPuesto4.Content),
                    new XAttribute("NO_VALOR_NIVEL", 4)
                    ),
            new XElement("NIVEL",
                    new XAttribute("CL_NIVEL_FACTOR", "NIVEL_05"),
                    new XAttribute("NB_FACTOR_NIVEL", "NIVEL 5"),
                    new XAttribute("DS_NIVEL_FACTOR_PUESTO", radEditorPuesto5.Content),
                    new XAttribute("NO_VALOR_NIVEL", 5)
                    )
                    );

            vEmpleados.XML_ID_SELECCIONADOS = vXmlCompetencia.ToString();
            vEmpleados.ID_TABULADOR = vIdTabulador;

            E_RESULTADO vResultado = nTabulador.InsertaActualizaTabuladorFactor(pClTipoOperacion: vTipoTransaccion.ToString(), vEmpleados: vEmpleados,pNiveles:vXmlNiveles.ToString(), usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
        }
    }
}