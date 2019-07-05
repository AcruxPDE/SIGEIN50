using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaPlaza : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int? vIdPlaza
        {
            get { return (int?)ViewState["vs_vIdPlaza"]; }
            set { ViewState["vs_vIdPlaza"] = value; }
        }

        private E_TIPO_OPERACION_DB vClOperacion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vClOperacion"]; }
            set { ViewState["vs_vClOperacion"] = value; }
        }

        private List<E_GRUPOS> vLstGruposPlaza
        {
            get { return (List<E_GRUPOS>)ViewState["vs_vLstGruposPlaza"]; }
            set { ViewState["vs_vLstGruposPlaza"] = value; }
        }
        public int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        #endregion

        #region Metodos

        protected void CargarDatos(int? vIdPlaza)
        {
            PlazaNegocio nPlaza = new PlazaNegocio();
            SPE_OBTIENE_PLAZAS_Result vPlaza = nPlaza.ObtienePlazas(vIdPlaza).FirstOrDefault() ?? new SPE_OBTIENE_PLAZAS_Result();

            txtClPlaza.Text = vPlaza.CL_PLAZA;
            txtNbPlaza.Text = vPlaza.NB_PLAZA;
            chkActivo.Checked = vPlaza.FG_ACTIVO || vClOperacion.Equals(E_TIPO_OPERACION_DB.I);

            if (vPlaza.XML_GRUPOS != null)
            {
                vLstGruposPlaza = (XElement.Parse(vPlaza.XML_GRUPOS).Elements("GRUPOS")).Select(s => new E_GRUPOS
                {
                    ID_GRUPO = int.Parse(s.Attribute("ID_GRUPO").Value)
                   ,CL_GRUPO = s.Attribute("CL_GRUPO").Value
                   ,NB_GRUPO = s.Attribute("NB_GRUPO").Value
                   , FG_SISTEMA = s.Attribute("FG_SISTEMA").Value == "1"? true:false
                }).ToList();
            }



            if (vPlaza.ID_EMPLEADO != null)
            {
                vIdEmpleado = vPlaza.ID_EMPLEADO;
                lstEmpleado.Enabled = false;
                lstEmpleado.Items.Clear();
                lstEmpleado.Items.Add(new RadListBoxItem(vPlaza.NB_EMPLEADO_COMPLETO, vPlaza.ID_EMPLEADO.ToString()));
            }

            if (vPlaza.ID_PUESTO != null)
            {
                lstPuesto.Items.Clear();
                lstPuesto.Items.Add(new RadListBoxItem(vPlaza.NB_PUESTO, vPlaza.ID_PUESTO.ToString()));
                lstPuesto.Enabled = false;
            }

            if (vPlaza.ID_PLAZA_SUPERIOR != null)
            {
                if (vPlaza.ID_PLAZA_SUPERIOR.Value != 0)
                {
                    lstPlazaJefe.Items.Clear();
                    lstPlazaJefe.Items.Add(new RadListBoxItem(vPlaza.NB_PLAZA_JEFE, vPlaza.ID_PLAZA_SUPERIOR.ToString()));   
                }
            }

            if (vPlaza.ID_DEPARTAMENTO != null)
            {
                lstArea.Items.Clear();
                lstArea.Items.Add(new RadListBoxItem(vPlaza.NB_DEPARTAMENTO, vPlaza.ID_DEPARTAMENTO.ToString()));
            }

            EmpresaNegocio nEmpresa = new EmpresaNegocio();
            List<SPE_OBTIENE_C_EMPRESA_Result> vEmpresas = nEmpresa.Obtener_C_EMPRESA(ID_EMPRESA: vIdEmpresa);
            cmbEmpresa.DataValueField = "ID_EMPRESA";
            cmbEmpresa.DataTextField = "NB_EMPRESA";
            cmbEmpresa.DataSource = vEmpresas;
            cmbEmpresa.DataBind();
            cmbEmpresa.SelectedValue = vPlaza.ID_EMPRESA.ToString();

            if (vIdPlaza == 0)
            {
                GruposNegocio oNegocio = new GruposNegocio();
                SPE_OBTIENE_GRUPOS_Result vGrupo = oNegocio.ObtieneGrupos(pCL_GRUPO:"TODOS").FirstOrDefault();
                if (vGrupo != null)
                {
                    vLstGruposPlaza.Add(new E_GRUPOS
                    {
                        ID_GRUPO = (int)vGrupo.ID_GRUPO,
                        CL_GRUPO = vGrupo.CL_GRUPO,
                        NB_GRUPO = vGrupo.NB_GRUPO,
                        FG_SISTEMA = (bool)vGrupo.FG_SISTEMA
                    });
                }
            }

        }

         protected void AgregarGrupos(string pGruposSeleccion)
         {
             List<E_GRUPOS> vGruposSeleccionados = JsonConvert.DeserializeObject<List<E_GRUPOS>>(pGruposSeleccion);
             foreach (E_GRUPOS item in vGruposSeleccionados)
             {
                 if (!vLstGruposPlaza.Exists(e => e.ID_GRUPO == item.ID_GRUPO))
                 {
                     vLstGruposPlaza.Add(new E_GRUPOS
                     {
                         ID_GRUPO = item.ID_GRUPO,
                         CL_GRUPO = item.CL_GRUPO,
                         NB_GRUPO = item.NB_GRUPO
                     });
                 }
             }

             rgGrupos.Rebind();
         }

         protected string ObtieneGrupos()
         {
             XElement vXmlGrupos = null;

             var vGrupos = vLstGruposPlaza.Select(s => new XElement("GRUPO",
                 new XAttribute("ID_GRUPO", s.ID_GRUPO.ToString())
                 ));

             vXmlGrupos = new XElement("GRUPOS", vGrupos);

             return vXmlGrupos.ToString();
         }

         protected void SeguridadProcesos()
         {
             btnGuardar.Enabled = ContextoUsuario.oUsuario.TienePermiso("D.D");
         }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (!Page.IsPostBack)
            {
                int vIdPlazaQS = -1;
                vIdEmpleado = 0;
                vClOperacion = E_TIPO_OPERACION_DB.I;
                if (int.TryParse(Request.QueryString["PlazaId"], out vIdPlazaQS))
                {
                    vIdPlaza = vIdPlazaQS;
                    vClOperacion = E_TIPO_OPERACION_DB.A;
                    btnBuscarPuesto.Enabled = false;
                    
                }
     
                vLstGruposPlaza = new List<E_GRUPOS>();
                CargarDatos(vIdPlaza ?? 0);
                SeguridadProcesos();
            }


        }
       
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            SPE_OBTIENE_PLAZAS_Result vPlaza = new SPE_OBTIENE_PLAZAS_Result()
            {
                ID_PLAZA = vIdPlaza ?? 0,
                CL_PLAZA = txtClPlaza.Text,
                NB_PLAZA = txtNbPlaza.Text,
                FG_ACTIVO = chkActivo.Checked,
                ID_EMPRESA = int.Parse(cmbEmpresa.SelectedValue ?? "0")
            };

            bool vFgGuardarPlaza = true;

            int vIdEmpleado = 0;
            foreach (RadListBoxItem item in lstEmpleado.Items)
                if (int.TryParse(item.Value, out vIdEmpleado))
                    vPlaza.ID_EMPLEADO = vIdEmpleado;

            int vIdPuesto = 0;
            foreach (RadListBoxItem item in lstPuesto.Items)
                if (int.TryParse(item.Value, out vIdPuesto))
                    vPlaza.ID_PUESTO = vIdPuesto;

            if (vPlaza.ID_PUESTO == null)
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Debes selecionar un puesto.", E_TIPO_RESPUESTA_DB.WARNING);
                vFgGuardarPlaza = false;
            }

            int vIdPlazaJefe = 0;
            foreach (RadListBoxItem item in lstPlazaJefe.Items)
                if (int.TryParse(item.Value, out vIdPlazaJefe))
                    vPlaza.ID_PLAZA_SUPERIOR = vIdPlazaJefe;

            int vIdDepartamento = 0;
            foreach (RadListBoxItem item in lstArea.Items)
                if (int.TryParse(item.Value, out vIdDepartamento))
                    vPlaza.ID_DEPARTAMENTO = vIdDepartamento;

            vPlaza.XML_GRUPOS = ObtieneGrupos();

            if (vFgGuardarPlaza)
            {
                PlazaNegocio nPlaza = new PlazaNegocio();

                E_RESULTADO vResultado = nPlaza.InsertaActualizaPlaza(vClOperacion, vPlaza, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
            }
        }

        protected void rgGrupos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgGrupos.DataSource = vLstGruposPlaza;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in rgGrupos.SelectedItems)
            {
                int vIdGrupo = int.Parse(item.GetDataKeyValue("ID_GRUPO").ToString());
                //string vClGrupo = item.GetDataKeyValue("CL_GRUPO").ToString();
                bool vFgSistema = bool.Parse(item.GetDataKeyValue("FG_SISTEMA").ToString());
                if (vFgSistema)
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "Este grupo pertenece al sistema y no es posible eliminarlo.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150,"");
                    return;
                }


                E_GRUPOS vItem = vLstGruposPlaza.Where(w => w.ID_GRUPO == vIdGrupo).FirstOrDefault();

                if (vItem != null)
                {
                    vLstGruposPlaza.Remove(vItem);
                    rgGrupos.Rebind();
                }

            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vLstDatos = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
            {
                vLstDatos = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

                if (vLstDatos.clTipo == "GRUPO")
                {
                    AgregarGrupos(vLstDatos.oSeleccion.ToString());
                }
            }
        }
    }
}