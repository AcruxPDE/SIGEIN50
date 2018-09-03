using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaReporteCursosRealizadosDetalle : System.Web.UI.Page
    {
        #region Variables

        private int vIdEvento {
            get { return (int)ViewState["vs_vrcrd_id_evento"]; }
            set { ViewState["vs_vrcrd_id_evento"] = value; }
        }

        private string vXmlDatosEvento {
            get { return (string)ViewState["vs_vrcrd_xml_datos_evento"]; }
            set { ViewState["vs_vrcrd_xml_datos_evento"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {
            XElement vXmlDatos;
            ConsultasFYDNegocio neg = new ConsultasFYDNegocio();

            vXmlDatosEvento = neg.ReporteCursosrealizadosDetalle(vIdEvento);

            vXmlDatos = XElement.Parse(vXmlDatosEvento);

            //XElement prueba1 = vXmlDatos.Element("EVENTO");
            XElement prueba2 = vXmlDatos.Element("DATOS");
            XElement prueba3 = prueba2.Element("DATOS");

            txtCurso.Text = vXmlDatos.Element("DATOS").Element("DATOS").Attribute("NB_CURSO").Value;
            txtDescripcion.Text = vXmlDatos.Element("DATOS").Element("DATOS").Attribute("DS_EVENTO").Value;
            txtDuracion.Text = vXmlDatos.Element("DATOS").Element("DATOS").Attribute("NO_DURACION").Value;
            txtEvento.Text = vXmlDatos.Element("DATOS").Element("DATOS").Attribute("NB_EVENTO").Value;
            txtPuesto.Text = vXmlDatos.Element("DATOS").Element("DATOS").Attribute("NB_PUESTO").Value;
            txtTipoCurso.Text = vXmlDatos.Element("DATOS").Element("DATOS").Attribute("CL_TIPO_CURSO").Value;
            reNotas.Content = vXmlDatos.Element("DATOS").Element("DATOS").Attribute("DS_NOTAS").Value;
            
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (Request.Params["IdEvento"] !=  null)
                {
                    vIdEvento = int.Parse(Request.Params["IdEvento"].ToString());
                    CargarDatos();
                }
            }
        }

        protected void rgParticipantes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<E_EVENTO_PARTICIPANTE> Lista = new List<E_EVENTO_PARTICIPANTE>();

            XElement vXmlDatos = XElement.Parse(vXmlDatosEvento);

            Lista.AddRange(vXmlDatos.Element("PARTICIPANTES").Elements().Select(t => new E_EVENTO_PARTICIPANTE{
                NO_FILA = int.Parse(t.Attribute("NO_FILA").Value),
                CL_PARTICIPANTE = t.Attribute("CL_PARTICIPANTE").Value,
                NB_PARTICIPANTE = t.Attribute("NB_PARTICIPANTE").Value,
                NB_PUESTO = t.Attribute("NB_PUESTO").Value,
                DS_CUMPLIMIENTO = t.Attribute("PR_CUMPLIMIENTO").Value
            }));

            rgParticipantes.DataSource = Lista;
        }

        protected void rgCompetencias_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<E_EVENTO_PARTICIPANTE_COMPETENCIA> Lista = new List<E_EVENTO_PARTICIPANTE_COMPETENCIA>();

            XElement vXmlDatos = XElement.Parse(vXmlDatosEvento);

            Lista.AddRange(vXmlDatos.Element("COMPETENCIAS").Elements().Select(t => new E_EVENTO_PARTICIPANTE_COMPETENCIA
            {
                NO_FILA = int.Parse(t.Attribute("NO_FILA").Value),
                CL_TIPO_COMPETENCIA = t.Attribute("CL_TIPO_COMPETENCIA").Value,
                NB_COMPETENCIA = t.Attribute("NB_COMPETENCIA").Value
            }));

            rgCompetencias.DataSource = Lista;
        }
    }
}