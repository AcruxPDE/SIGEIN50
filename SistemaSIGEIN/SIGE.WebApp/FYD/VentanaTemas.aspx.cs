using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using Stimulsoft.Base.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class VentanaTemas : System.Web.UI.Page
    {
        #region Variables

        private Guid vIdCurso
        {
            get { return (Guid)ViewState["vs_id_item_curso"]; }
            set { ViewState["vs_id_item_curso"] = value; }
        }

        private Guid vIdTema
        {
            get { return (Guid)ViewState["vs_id_item_tema"]; }
            set { ViewState["vs_id_item_tema"] = value; }
        }


        private E_TEMA vTema
        {
            get { return ContextoCurso.oCursos.Where(t => t.ID_ITEM == vIdCurso).FirstOrDefault().LS_TEMAS.Where(o => o.ID_ITEM == vIdTema).FirstOrDefault(); }
        }

        #endregion

        #region Funciones

        protected void DespacharEventos(string pCatalogo, string pSeleccionados)
        {
            if (pCatalogo == "COMPETENCIA")
                LlenaGridCompetencia(pSeleccionados);

            if (pCatalogo == "MATERIAL")
                LlenaGridMaterial(pSeleccionados);
        }

        protected void CargarDatos()
        {
            txtcl_tema.Text = vTema.CL_TEMA;
            txtTema.Text = vTema.NB_TEMA;
            txtDuracion.Text = vTema.NO_DURACION;
            txtDescripcion.Text = vTema.DS_DESCRIPCION;
        }

        protected void LlenaGridCompetencia(string cCompetencias)
        {
            if (cCompetencias != null & cCompetencias != "")
            {
                List<E_TEMA_COMPETENCIA> competencias = new List<E_TEMA_COMPETENCIA>();
                competencias = JsonConvert.DeserializeObject<List<E_TEMA_COMPETENCIA>>(cCompetencias);
                vTema.LS_COMPETENCIAS.AddRange(competencias.Where(w => !vTema.LS_COMPETENCIAS.Any(a => a.ID_COMPETENCIA == w.ID_COMPETENCIA)));
                grdTemaCompetencia.Rebind();
            }
        }

        protected void LlenaGridMaterial(string cMaterial)
        {
            if (cMaterial != null & cMaterial != "")
            {
                List<E_MATERIAL> material = new List<E_MATERIAL>();
                material = JsonConvert.DeserializeObject<List<E_MATERIAL>>(cMaterial);
                vTema.LS_MATERIALES.AddRange(material);
                for (int i = 0; i < vTema.LS_MATERIALES.Count; i++)
                {
                    vTema.LS_MATERIALES[i].CL_MATERIAL = i + 1;
                    vTema.LS_MATERIALES[i].ID_TEMA = vTema.ID_TEMA;
                    vTema.LS_MATERIALES[i].ID_ITEM = vTema.LS_MATERIALES[i].ID_ITEM;
                    vTema.LS_MATERIALES[i].MN_MATERIAL = vTema.LS_MATERIALES[i].MN_MATERIAL;
                    vTema.LS_MATERIALES[i].NB_MATERIAL = vTema.LS_MATERIALES[i].NB_MATERIAL;
                }

                grdTemaMaterial.Rebind();
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vIdCurso = Guid.Empty;
                vIdTema = Guid.Empty;

                if (Request.Params["IdCursoItem"] != null)
                {
                    vIdCurso = Guid.Parse(Request.Params["IdCursoItem"].ToString());
                }

                if (Request.Params["IdTemaItem"] != null)
                {
                    vIdTema = Guid.Parse(Request.Params["IdTemaItem"].ToString());
                    CargarDatos();
                }
                else
                {
                    vIdTema = Guid.NewGuid();
                    ContextoCurso.oCursos.Where(t => t.ID_ITEM == vIdCurso).FirstOrDefault().LS_TEMAS.Add(new E_TEMA { ID_ITEM = vIdTema });
                }
            }

            DespacharEventos(Request.Params.Get("__EVENTTARGET"), Request.Params.Get("__EVENTARGUMENT"));
        }

        protected void grdTemaCompetencia_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdTemaCompetencia.DataSource = vTema.LS_COMPETENCIAS;
        }

        protected void grdTemaMaterial_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdTemaMaterial.DataSource = vTema.LS_MATERIALES;
        }

        protected void BtnEliminaCompetencia_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdTemaCompetencia.SelectedItems)
                vTema.LS_COMPETENCIAS.RemoveAll(r => r.ID_ITEM.Equals(new Guid(i.GetDataKeyValue("ID_ITEM").ToString())));

            grdTemaCompetencia.Rebind();
        }

        protected void radBtnEliminarMaterial_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdTemaMaterial.SelectedItems)
            {
                E_MATERIAL elemento = vTema.LS_MATERIALES.Where(r => r.ID_ITEM.Equals(new Guid(i.GetDataKeyValue("ID_ITEM").ToString()))).FirstOrDefault();
                vTema.LS_MATERIALES.Remove(elemento);
            }
            for (int i = 0; i < vTema.LS_MATERIALES.Count; i++)
            {
                vTema.LS_MATERIALES[i].CL_MATERIAL = i + 1;
                vTema.LS_MATERIALES[i].ID_ITEM = vTema.LS_MATERIALES[i].ID_ITEM;
                vTema.LS_MATERIALES[i].MN_MATERIAL = vTema.LS_MATERIALES[i].MN_MATERIAL;
                vTema.LS_MATERIALES[i].NB_MATERIAL = vTema.LS_MATERIALES[i].NB_MATERIAL;
            }
            grdTemaMaterial.Rebind();
        }

        protected void radBtnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtcl_tema.Text))
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Indica el número del tema", E_TIPO_RESPUESTA_DB.WARNING);
                return;
            }

            if (string.IsNullOrEmpty(txtTema.Text))
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Indica el nombre del tema", E_TIPO_RESPUESTA_DB.WARNING);
                return;
            }

            if (string.IsNullOrEmpty(txtDuracion.Text))
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Indica la duración del tema", E_TIPO_RESPUESTA_DB.WARNING);
                return;
            }

            vTema.CL_TEMA = txtcl_tema.Text;
            vTema.NB_TEMA = txtTema.Text;
            vTema.NO_DURACION = txtDuracion.Text;
            vTema.DS_DESCRIPCION = txtDescripcion.Text;
            UtilMensajes.MensajeResultadoDB(rwmAlertas, "Proceso con exito.", E_TIPO_RESPUESTA_DB.SUCCESSFUL);

        }

    }
}

