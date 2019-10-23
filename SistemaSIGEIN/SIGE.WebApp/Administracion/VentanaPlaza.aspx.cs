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
        private string vProceso;
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

        private List<E_PLAZA> vLstPlazasInterrelacionadas
        {
            get { return (List<E_PLAZA>)ViewState["vs_vLstPlazasInterrelacionadas"]; }
            set { ViewState["vs_vLstPlazasInterrelacionadas"] = value; }
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
            E_PLAZA vPlaza = nPlaza.ObtienePlazas(vIdPlaza).FirstOrDefault() ?? new E_PLAZA();

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

            if (vPlaza.XML_PLAZAS_INTERRELACIONADAS != null)
            {
                vLstPlazasInterrelacionadas = (XElement.Parse(vPlaza.XML_PLAZAS_INTERRELACIONADAS).Elements("PLAZAS_INTERRELACIONADAS")).Select(s => new E_PLAZA
                {
                    ID_PLAZA = int.Parse(s.Attribute("ID_PLAZA").Value)
                    , CL_PLAZA = s.Attribute("CL_PLAZA").Value
                    , NB_PLAZA = s.Attribute("NB_PLAZA").Value
                    , NB_EMPLEADO = s.Attribute("NB_EMPLEADO").Value
                }).ToList();
            }

            if (vPlaza.ID_EMPLEADO != null)
            {
                vIdEmpleado = vPlaza.ID_EMPLEADO;
                lstEmpleado.Enabled = false;
                lstEmpleado.Items.Clear();
                lstEmpleado.Items.Add(new RadListBoxItem(vPlaza.NB_EMPLEADO_COMPLETO, vPlaza.ID_EMPLEADO.ToString()));
                vProceso = "EDITAR";
            }
            else
                vProceso = "NUEVO";

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

        protected string ObtienePlazasInterrelacionadas()
        {
            XElement vXmlPlazasInterrelacionadas = null;

            var vGrupos = vLstPlazasInterrelacionadas.Select(s => new XElement("PLAZA_INTERRELACIONADA",
                new XAttribute("ID_PLAZA", s.ID_PLAZA.ToString())
                ));

            vXmlPlazasInterrelacionadas = new XElement("PLAZAS_INTERRELACIONADAS", vGrupos);

            return vXmlPlazasInterrelacionadas.ToString();
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
                vLstPlazasInterrelacionadas = new List<E_PLAZA>();
                CargarDatos(vIdPlaza ?? 0);
                SeguridadProcesos();
            }
        }
       
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            PlazaNegocio nPlaza = new PlazaNegocio();
            E_PLAZA vPlazaEditar = nPlaza.ObtienePlazas(vIdPlaza).FirstOrDefault() ?? new E_PLAZA();

            if (vPlazaEditar.ID_EMPLEADO != null)
                vProceso = "EDITAR";
            else
                vProceso = "NUEVO";

            E_PLAZA vPlaza = new E_PLAZA()
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
            vPlaza.XML_PLAZAS_INTERRELACIONADAS = ObtienePlazasInterrelacionadas();

            bool bandera = true;

            if (vFgGuardarPlaza)
            {
                if (vProceso == "NUEVO")
                {
                    if (!chkActivo.Checked && vIdEmpleado != 0)
                        vPlaza.FG_ACTIVO = true;

                    E_RESULTADO vResultado = nPlaza.InsertaActualizaPlaza(vClOperacion, vPlaza, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
                }

                else if (vProceso == "EDITAR" && vIdEmpleado != 0 && !chkActivo.Checked)
                    bandera = false;                    
                
                if (bandera)
                {
                    E_RESULTADO vResultado = nPlaza.InsertaActualizaPlaza(vClOperacion, vPlaza, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
                }
                else
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "No puedes inactivar una plaza que se encuentra ocupada", E_TIPO_RESPUESTA_DB.ERROR);
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
                else if (vLstDatos.clTipo == "PLAZA_INTERRELACIONADA")
                {
                    AgregarPlazaInterrelacionada(vLstDatos.oSeleccion.ToString());
                }
            }
        }

        protected void AgregarPlazaInterrelacionada(string pPlazaInterrelacionada)
        {
            List<E_SELECCION_PLAZA> vPlazasSeleccionados = JsonConvert.DeserializeObject<List<E_SELECCION_PLAZA>>(pPlazaInterrelacionada);
            foreach (E_SELECCION_PLAZA item in vPlazasSeleccionados)
            {
                if (!vLstPlazasInterrelacionadas.Exists(e => e.ID_PLAZA == item.idPlaza))
                {
                    if (item.idPlaza != vIdPlaza && item.idPlaza != 0)
                    {
                        vLstPlazasInterrelacionadas.Add(new E_PLAZA
                        {
                            ID_PLAZA = item.idPlaza,
                            CL_PLAZA = item.clPlaza,
                            NB_PLAZA = item.nbPlaza,
                            NB_EMPLEADO = item.nbEmpleado,
                            NB_PUESTO = item.nbPuesto
                        });
                    }
                }
            }

            grdPuestosInterrelacionados.Rebind();
        }

        protected void btnEliminarPlazaInter_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdPuestosInterrelacionados.SelectedItems)
            {
                int vIdPlazaInterrelacionada = int.Parse(item.GetDataKeyValue("ID_PLAZA").ToString());

                E_PLAZA vItem = vLstPlazasInterrelacionadas.Where(w => w.ID_PLAZA == vIdPlazaInterrelacionada).FirstOrDefault();

                if (vItem != null)
                {
                    vLstPlazasInterrelacionadas.Remove(vItem);
                    grdPuestosInterrelacionados.Rebind();
                }

            }
        }
        

        protected void grdPuestosInterrelacionados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdPuestosInterrelacionados.DataSource = vLstPlazasInterrelacionadas;
        }
    }
}