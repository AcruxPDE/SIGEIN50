using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using Telerik.Web.UI;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;
using System.IO;
using OfficeOpenXml;
using System.Reflection;
using OfficeOpenXml.Style;
using System.Data;
using SIGE.Negocio.Administracion;

namespace SIGE.WebApp.FYD
{
    public partial class AvanceProgramaCapacitacion : System.Web.UI.Page
    {
        #region Variables

        public int pID_PROGRAMA
        {
            get { return (int)ViewState["vs_ID_PROGRAMA"]; }
            set { ViewState["vs_ID_PROGRAMA"] = value; }
        }

        private List<E_PROGRAMA_CAPACITACION> vProgramasCapacitacion
        {
            get { return (List<E_PROGRAMA_CAPACITACION>)ViewState["vs_ProgramasCapacitacion"]; }
            set { ViewState["vs_ProgramasCapacitacion"] = value; }
        }

        private List<E_AVANCE_PROGRAMA_CAPACITACION> vLstSeleccionados
        {
            get { return (List<E_AVANCE_PROGRAMA_CAPACITACION>)ViewState["vs_LstSeleccionados"]; }
            set { ViewState["vs_LstSeleccionados"] = value; }
        }

        private List<E_AVANCE_PROGRAMA_CAPACITACION> vLstSeleccionadosFiltros
        {
            get { return (List<E_AVANCE_PROGRAMA_CAPACITACION>)ViewState["vs_vLstSeleccionadosFiltros"]; }
            set { ViewState["vs_vLstSeleccionadosFiltros"] = value; }
        }

        private List<E_AVANCE_PROGRAMA_CAPACITACION> vLstAvancePrograma
        {
            get { return (List<E_AVANCE_PROGRAMA_CAPACITACION>)ViewState["vs_vLstAvancePrograma"]; }
            set { ViewState["vs_vLstAvancePrograma"] = value; }
        }

        private List<E_AVANCE_PROGRAMA_CAPACITACION> vLstEmpleados
        {
            get { return (List<E_AVANCE_PROGRAMA_CAPACITACION>)ViewState["vs_vLstEmpleados"]; }
            set { ViewState["vs_vLstEmpleados"] = value; }
        }

        private List<E_AVANCE_PROGRAMA_CAPACITACION> vLstEventos
        {
            get { return (List<E_AVANCE_PROGRAMA_CAPACITACION>)ViewState["vs_vLstEventos"]; }
            set { ViewState["vs_vLstEventos"] = value; }
        }

        private string vClUsuario;

        private string vNbPrograma;

        private int? vIdRol;

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vNoEmpleados
        {
            get { return (int)ViewState["vs_no_empleados"]; }
            set { ViewState["vs_no_empleados"] = value; }
        }

        public string vXmlSeleccionados
        {
            get { return (string)ViewState["vs_vXmlSeleccionados"]; }
            set { ViewState["vs_vXmlSeleccionados"] = value; }
        }

        #endregion

        #region Funciones

        public string validarDsNotas(string vdsNotas)
        {
            E_NOTA pNota = null;
            if (vdsNotas != null)
            {
                XElement vNotas = XElement.Parse(vdsNotas.ToString());
                if (ValidarRamaXml(vNotas, "NOTA"))
                {
                    pNota = vNotas.Elements("NOTA").Select(el => new E_NOTA
                    {
                        DS_NOTA = UtilXML.ValorAtributo<string>(el.Attribute("DS_NOTA")),
                        FE_NOTA = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_NOTA"), E_TIPO_DATO.DATETIME),
                    }).FirstOrDefault();
                }
                if (pNota.DS_NOTA != null)
                {
                    return pNota.DS_NOTA.ToString();
                }
                else return "";
            }
            else
            {
                return "";
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

        public int ObtieneId(string pCadena)
        {
            string a = "(";
            string b = ")";
            int posA = pCadena.IndexOf(a);
            int posB = pCadena.LastIndexOf(b);

            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return 0;
            }
            string vIdEmpleado = pCadena.Substring(adjustedPosA, posB - adjustedPosA);

            return int.Parse(vIdEmpleado);
        }

        private void CargarDatos()
        {
            ProgramaNegocio neg = new ProgramaNegocio();
            var oPrograma = neg.ObtieneProgramasCapacitacion(pIdPrograma: pID_PROGRAMA).FirstOrDefault();

            if (oPrograma != null)
            {
                txtPeriodo.InnerText = oPrograma.CL_PROGRAMA;
                txtDesPeriodo.InnerText = oPrograma.NB_PROGRAMA;
                txtTipoEvaluacion.InnerText = oPrograma.CL_TIPO_PROGRAMA;
                radEditorNotas.InnerHtml = validarDsNotas(oPrograma.DS_NOTAS);

                //ProgramaNegocio neg = new ProgramaNegocio();
                //var programas = nPrograma.ObtieneKernelProgramaCapacitacion(pIdPrograma: pID_PROGRAMA);
                //vProgramasCapacitacion = cargarParseProgramasCapacitacion(programas);
                //var vLista = neg.ObtenerAvancePrograma(pID_PROGRAMA, ContextoUsuario.oUsuario.ID_EMPRESA);
                //List<E_AVANCE_PROGRAMA_CAPACITACION> vLstAvancePrograma = new List<E_AVANCE_PROGRAMA_CAPACITACION>();

                //foreach (var s in vLista)
                //{
                // E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                //{
                //    ID_PROGRAMA_COMPETENCIA = s.ID_PROGRAMA_COMPETENCIA,
                //    NB_CATEGORIA = s.NB_CATEGORIA,
                //    NB_CLASIFICACION = s.NB_CLASIFICACION,
                //    CL_COLOR = s.CL_COLOR,
                //    ID_COMPETENCIA = s.ID_COMPETENCIA,
                //    NB_COMPETENCIA = s.NB_COMPETENCIA,
                //    CL_EMPLEADO = String.Format("<a href='#' onclick='OpenInventario({1})'>{0}</a>", s.CL_EMPLEADO, s.ID_EMPLEADO),
                //    CL_PUESTO = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", s.CL_PUESTO,s.ID_PUESTO),
                //    ID_PROGRAMA_EMPLEADO = s.ID_PROGRAMA_COMPETENCIA,
                //    NB_EMPLEADO = s.NB_EMPLEADO,
                //    ID_EMPLEADO = s.ID_EMPLEADO,
                //    NB_PUESTO = s.NB_PUESTO,
                //    ID_PROGRAMA_EMPLEADO_COMPETENCIA = s.ID_PROGRAMA_EMPLEADO_COMPETENCIA,
                //    ID_EVENTO = s.ID_EVENTO,
                //    FE_INICIO = s.FE_INICIO,
                //    FE_TERMINO = s.FE_TERMINO,
                //    ID_EVENTO_PARTICIPANTE = s.ID_EVENTO_PARTICIPANTE,
                //    PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO,
                //    NB_EVENTOS_RELACIONADOS = String.Format("<a href='#' onclick='OpenEvento({1})'>{0}</a>", s.NB_EVENTOS_RELACIONADOS, s.ID_EVENTO),
                //    NO_COLOR_AVANCE = s.NO_COLOR_AVANCE,
                //    CL_COLOR_AVANCE = s.CL_COLOR_AVANCE,
                //    CL_COLOR_ASISTENCIA = s.CL_COLOR_ASISTENCIA
                //};

                //vLstAvancePrograma.Add(vLstItem);
                //}

                //vNoEmpleados = (from a in vLista select new { a.ID_EMPLEADO }).Distinct().Count();

                //pgridAvanceProgramaCapacitacion.DataSource = vLstAvancePrograma;
                ProgramaNegocio nPrograma = new ProgramaNegocio();
                List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result> vLstCompetenciasPrograma = new List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result>();
                vLstCompetenciasPrograma = nPrograma.ObtenerCompetenciasPrograma(ID_PROGRAMA: pID_PROGRAMA);
                GenerarColumnasGroupName(vLstCompetenciasPrograma);
            }
        }

        private void EliminarEmpleado(int pIdEmpleado)
        {

            if (vLstSeleccionadosFiltros.Count > 0)
            {
                E_AVANCE_PROGRAMA_CAPACITACION vEmpleado = vLstSeleccionadosFiltros.Where(w => w.ID_EMPLEADO == pIdEmpleado).FirstOrDefault();

                if (vEmpleado != null)
                {
                    vLstSeleccionadosFiltros.Remove(vEmpleado);
                }

                GeneraAvance();
            }
            //else
            //{
            //    E_AVANCE_PROGRAMA_CAPACITACION vEmpleado = vLstSeleccionados.Where(w => w.ID_EMPLEADO == pIdEmpleado).FirstOrDefault();

            //    if (vEmpleado != null)
            //    {
            //        vLstSeleccionados.Remove(vEmpleado);
            //    }

            //  //  GeneraAvance(true);
            //}
        }

        public void CargarAvancePrograma(string pSeleccionados)
        {
            if (pSeleccionados != null)
            {
                List<E_SELECTOR_EMPLEADO> vEvaluados = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO>>(pSeleccionados);
                foreach (var item in vEvaluados)
                {
                    if (!vLstSeleccionadosFiltros.Exists(e => e.ID_EMPLEADO == item.idEmpleado))
                    {
                        E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                        {
                            ID_EMPLEADO = item.idEmpleado,
                            NB_EMPLEADO = item.nbEmpleado,
                            CL_PUESTO = item.clPuesto,
                            NB_PUESTO = item.nbPuesto
                        };
                        vLstSeleccionadosFiltros.Add(vLstItem);
                    }
                }
            }

            GeneraAvance();
        }

        public void GeneraAvance()
        {
            ProgramaNegocio neg = new ProgramaNegocio();

            //if (vLstSeleccionadosFiltros.Count > 0)
            //{
                if (vLstSeleccionadosFiltros.Count > 0)
                {
                    vXmlSeleccionados = (new XElement("EMPLEADOS", vLstSeleccionadosFiltros.Select(s => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", s.ID_EMPLEADO))))).ToString();

                    rgPrograma.Rebind();
                    rgAvancePrograma.Rebind();
                }
                //else if (pFgFiltro)
                //    vXmlSeleccionados = "<EMPLEADOS></EMPLEADOS>";

                //var vLista = neg.ObtenerAvancePrograma(pID_PROGRAMA, ContextoUsuario.oUsuario.ID_EMPRESA, vXmlSeleccionados, vIdRol);
                //vLstAvancePrograma = new List<E_AVANCE_PROGRAMA_CAPACITACION>();
                //int vCount = vLstSeleccionadosFiltros.Count();
                //vLstEventos = new List<E_AVANCE_PROGRAMA_CAPACITACION>();

                //foreach (var s in vLista)
                //{
                //    E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                //    {
                //        ID_PROGRAMA_COMPETENCIA = s.ID_PROGRAMA_COMPETENCIA,
                //        NB_CATEGORIA = s.NB_CATEGORIA,
                //        NB_CLASIFICACION = s.NB_CLASIFICACION,
                //        CL_COLOR = s.CL_COLOR,
                //        ID_COMPETENCIA = s.ID_COMPETENCIA,
                //        NB_COMPETENCIA = s.NB_COMPETENCIA,
                //        //CL_EMPLEADO = String.Format("<a href='#' onclick='OpenInventario({1})'>{0}</a>", s.CL_EMPLEADO, s.ID_EMPLEADO),
                //        CL_EMPLEADO = s.CL_EMPLEADO,
                //        //CL_PUESTO = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", s.CL_PUESTO, s.ID_PUESTO),
                //        CL_PUESTO = s.CL_PUESTO,
                //        ID_PROGRAMA_EMPLEADO = s.ID_PROGRAMA_COMPETENCIA,
                //        NB_EMPLEADO = String.Format("{0}<br>{1}<br>{2}<br>{3}", String.Format("<a href='#' onclick='OpenInventario({1})'>{0}</a>", s.CL_EMPLEADO, s.ID_EMPLEADO), s.NB_EMPLEADO, String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", s.CL_PUESTO, s.ID_PUESTO), s.NB_PUESTO),
                //        ID_EMPLEADO = s.ID_EMPLEADO,
                //        NB_PUESTO = s.NB_PUESTO,
                //        ID_PROGRAMA_EMPLEADO_COMPETENCIA = s.ID_PROGRAMA_EMPLEADO_COMPETENCIA,
                //        ID_EVENTO = s.ID_EVENTO,
                //        FE_INICIO = s.FE_INICIO,
                //        FE_TERMINO = s.FE_TERMINO,
                //        ID_EVENTO_PARTICIPANTE = s.ID_EVENTO_PARTICIPANTE,
                //        PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO,
                //        NB_EVENTOS_RELACIONADOS = String.Format("<a href='#' onclick='OpenEvento({1})'>{0}</a>", s.NB_EVENTOS_RELACIONADOS, s.ID_EVENTO),
                //        NO_COLOR_AVANCE = s.NO_COLOR_AVANCE,
                //        CL_COLOR_AVANCE = s.CL_COLOR_AVANCE,
                //        CL_COLOR_ASISTENCIA = s.CL_COLOR_ASISTENCIA
                //    };

                //    E_AVANCE_PROGRAMA_CAPACITACION vLstItemEvento = new E_AVANCE_PROGRAMA_CAPACITACION
                //    {
                //        ID_COMPETENCIA = s.ID_COMPETENCIA,
                //        ID_EVENTO = s.ID_EVENTO,
                //        FE_INICIO = s.FE_INICIO,
                //        FE_TERMINO = s.FE_TERMINO,
                //        ID_EVENTO_PARTICIPANTE = s.ID_EVENTO_PARTICIPANTE,
                //        PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO,
                //        NB_EVENTOS_RELACIONADOS = s.NB_EVENTOS_RELACIONADOS,
                //        NO_COLOR_AVANCE = s.NO_COLOR_AVANCE,
                //        CL_COLOR_AVANCE = s.CL_COLOR_AVANCE,
                //        CL_COLOR_ASISTENCIA = s.CL_COLOR_ASISTENCIA
                //    };

                //    if (!(vLstEventos.Exists(e => e.ID_EVENTO == s.ID_EVENTO && e.ID_COMPETENCIA == s.ID_COMPETENCIA)))
                //        vLstEventos.Add(vLstItemEvento);

                //    vLstAvancePrograma.Add(vLstItem);
                //}

                //if (!pFgFiltro)
                //{
                //    foreach (var s in vLista)
                //    {
                //        if (!vLstSeleccionados.Exists(e => e.ID_EMPLEADO == s.ID_EMPLEADO))
                //        {
                //            E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                //            {
                //                ID_EMPLEADO = s.ID_EMPLEADO,
                //                NB_EMPLEADO = s.NB_EMPLEADO,
                //                NB_PUESTO = s.NB_PUESTO
                //            };
                //            vLstSeleccionados.Add(vLstItem);
                //        }
                //    }
                //}

                //vNoEmpleados = (from a in vLista select new { a.ID_EMPLEADO }).Distinct().Count();
               // pgridAvanceProgramaCapacitacion.DataSource = vLstAvancePrograma;
           // }
            //else
            //{
            //    if (vLstSeleccionados.Count > 0)
            //    {
            //        vXmlSeleccionados = (new XElement("EMPLEADOS", vLstSeleccionados.Select(s => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", s.ID_EMPLEADO))))).ToString();

            //        rgPrograma.Rebind();
            //    }
            //    else if (pFgFiltro)
            //        vXmlSeleccionados = "<EMPLEADOS></EMPLEADOS>";

                //var vLista = neg.ObtenerAvancePrograma(pID_PROGRAMA, ContextoUsuario.oUsuario.ID_EMPRESA, vXmlSeleccionados, vIdRol);
                //vLstAvancePrograma = new List<E_AVANCE_PROGRAMA_CAPACITACION>();
                //int vCount = vLstSeleccionados.Count();
                //vLstEventos = new List<E_AVANCE_PROGRAMA_CAPACITACION>();

                //foreach (var s in vLista)
                //{
                //    E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                //    {
                //        ID_PROGRAMA_COMPETENCIA = s.ID_PROGRAMA_COMPETENCIA,
                //        NB_CATEGORIA = s.NB_CATEGORIA,
                //        NB_CLASIFICACION = s.NB_CLASIFICACION,
                //        CL_COLOR = s.CL_COLOR,
                //        ID_COMPETENCIA = s.ID_COMPETENCIA,
                //        NB_COMPETENCIA = s.NB_COMPETENCIA,
                //        //CL_EMPLEADO = String.Format("<a href='#' onclick='OpenInventario({1})'>{0}</a>", s.CL_EMPLEADO, s.ID_EMPLEADO),
                //        CL_EMPLEADO = s.CL_EMPLEADO,
                //        //CL_PUESTO = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", s.CL_PUESTO, s.ID_PUESTO),
                //        CL_PUESTO = s.CL_PUESTO,
                //        ID_PROGRAMA_EMPLEADO = s.ID_PROGRAMA_COMPETENCIA,
                //        NB_EMPLEADO = String.Format("{0}<br>{1}<br>{2}<br>{3}", String.Format("<a href='#' onclick='OpenInventario({1})'>{0}</a>", s.CL_EMPLEADO, s.ID_EMPLEADO), s.NB_EMPLEADO, String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", s.CL_PUESTO, s.ID_PUESTO), s.NB_PUESTO),
                //        ID_EMPLEADO = s.ID_EMPLEADO,
                //        NB_PUESTO = s.NB_PUESTO,
                //        ID_PROGRAMA_EMPLEADO_COMPETENCIA = s.ID_PROGRAMA_EMPLEADO_COMPETENCIA,
                //        ID_EVENTO = s.ID_EVENTO,
                //        FE_INICIO = s.FE_INICIO,
                //        FE_TERMINO = s.FE_TERMINO,
                //        ID_EVENTO_PARTICIPANTE = s.ID_EVENTO_PARTICIPANTE,
                //        PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO,
                //        NB_EVENTOS_RELACIONADOS = String.Format("<a href='#' onclick='OpenEvento({1})'>{0}</a>", s.NB_EVENTOS_RELACIONADOS, s.ID_EVENTO),
                //        NO_COLOR_AVANCE = s.NO_COLOR_AVANCE,
                //        CL_COLOR_AVANCE = s.CL_COLOR_AVANCE,
                //        CL_COLOR_ASISTENCIA = s.CL_COLOR_ASISTENCIA
                //    };

                //    E_AVANCE_PROGRAMA_CAPACITACION vLstItemEvento = new E_AVANCE_PROGRAMA_CAPACITACION
                //    {
                //        ID_COMPETENCIA = s.ID_COMPETENCIA,
                //        ID_EVENTO = s.ID_EVENTO,
                //        FE_INICIO = s.FE_INICIO,
                //        FE_TERMINO = s.FE_TERMINO,
                //        ID_EVENTO_PARTICIPANTE = s.ID_EVENTO_PARTICIPANTE,
                //        PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO,
                //        NB_EVENTOS_RELACIONADOS = s.NB_EVENTOS_RELACIONADOS,
                //        NO_COLOR_AVANCE = s.NO_COLOR_AVANCE,
                //        CL_COLOR_AVANCE = s.CL_COLOR_AVANCE,
                //        CL_COLOR_ASISTENCIA = s.CL_COLOR_ASISTENCIA
                //    };

                //    if (!(vLstEventos.Exists(e => e.ID_EVENTO == s.ID_EVENTO && e.ID_COMPETENCIA == s.ID_COMPETENCIA)))
                //        vLstEventos.Add(vLstItemEvento);

                //    vLstAvancePrograma.Add(vLstItem);
                //}

                //if (!pFgFiltro)
                //{
                //    foreach (var s in vLista)
                //    {
                //        if (!vLstSeleccionados.Exists(e => e.ID_EMPLEADO == s.ID_EMPLEADO))
                //        {
                //            E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                //            {
                //                ID_EMPLEADO = s.ID_EMPLEADO,
                //                NB_EMPLEADO = s.NB_EMPLEADO,
                //                NB_PUESTO = s.NB_PUESTO
                //            };
                //            vLstSeleccionados.Add(vLstItem);
                //        }
                //    }
                //}

               // vNoEmpleados = (from a in vLista select new { a.ID_EMPLEADO }).Distinct().Count();
               // pgridAvanceProgramaCapacitacion.DataSource = vLstAvancePrograma;
           // }

        }

        public Color GetColor(int pNoColor)
        {
            Color vColorAvance = Color.Gray;

            switch (pNoColor)
            {
                case 0:
                    vColorAvance = Color.Red;
                    break;
                case 2:
                    vColorAvance = Color.LightBlue;
                    break;
                case 3:
                    vColorAvance = Color.Gold;
                    break;
                case 4:
                    vColorAvance = Color.Yellow;
                    break;
                case 5:
                    vColorAvance = Color.LightGray;
                    break;
                case 10:
                    vColorAvance = Color.GreenYellow;
                    break;
                case 11:
                    vColorAvance = Color.GreenYellow;
                    break;
                case 12:
                    vColorAvance = Color.GreenYellow;
                    break;
            }

            return vColorAvance;

        }

        public void GenerarExcelAvance()
        {
            ProgramaNegocio pNegocio = new ProgramaNegocio();

            List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result> vLstCompetencias = pNegocio.ObtenerCompetenciasPrograma(ID_PROGRAMA: pID_PROGRAMA);
          List<SPE_OBTIENE_K_PROGRAMA_EMPLEADO_Result>  vLstEmpleadosProgramaExc = new List<SPE_OBTIENE_K_PROGRAMA_EMPLEADO_Result>();
          List<SPE_OBTIENE_AVANCE_PROGRAMA_CAPACITACION_Result> vLstPrograma = new List<SPE_OBTIENE_AVANCE_PROGRAMA_CAPACITACION_Result>();
          vLstPrograma = pNegocio.ObtenerAvancePrograma(pID_PROGRAMA, ContextoUsuario.oUsuario.ID_EMPRESA, vXmlSeleccionados, vIdRol);


         // vLstEmpleadosPrograma = pNegocio.ObtieneEmpleadosParticipantes(pID_PROGRAMA: pID_PROGRAMA, pID_ROL: vIdRol).ToList();

          if (vLstSeleccionadosFiltros.Count > 0)
          {
              var vLstSeleccionadosExc = pNegocio.ObtieneEmpleadosParticipantes(pID_PROGRAMA: pID_PROGRAMA, pID_ROL: vIdRol).ToList();
              foreach (var item in vLstSeleccionadosExc)
              {
                  if (vLstSeleccionadosFiltros.Exists(e => e.ID_EMPLEADO == item.ID_EMPLEADO))
                  {
                      vLstEmpleadosProgramaExc.Add(new SPE_OBTIENE_K_PROGRAMA_EMPLEADO_Result
                      {
                          ID_PROGRAMA_EMPLEADO = item.ID_PROGRAMA_EMPLEADO,
                          ID_PROGRAMA = item.ID_PROGRAMA,
                          ID_EMPLEADO = item.ID_EMPLEADO,
                          NB_EMPLEADO = item.NB_EMPLEADO,
                          CL_EMPLEADO = item.CL_EMPLEADO,
                          ID_PUESTO = item.ID_PUESTO,
                          NB_PUESTO = item.NB_PUESTO,
                          CL_PUESTO = item.CL_PUESTO,
                          NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
                          CL_PROGRAMA = item.CL_PROGRAMA,
                          NB_PROGRAMA = item.NB_PROGRAMA,
                          ID_EMPRESA = item.ID_EMPRESA
                      });
                  }
              }
          }
          else
              vLstEmpleadosProgramaExc = pNegocio.ObtieneEmpleadosParticipantes(pID_PROGRAMA: pID_PROGRAMA, pID_ROL: vIdRol);
            //foreach (var item in vLstAvancePrograma)
            //{
            //    E_AVANCE_PROGRAMA_CAPACITACION vLstEmp = new E_AVANCE_PROGRAMA_CAPACITACION
            //    {
            //        ID_EMPLEADO = item.ID_EMPLEADO,
            //        CL_EMPLEADO = item.CL_EMPLEADO,
            //        NB_EMPLEADO = item.NB_EMPLEADO,
            //        CL_PUESTO = item.CL_PUESTO,
            //        NB_PUESTO = item.NB_PUESTO,
            //    };

            //    if (!vLstEmpleados.Exists(e => e.ID_EMPLEADO == item.ID_EMPLEADO))
            //        vLstEmpleados.Add(vLstEmp);
            //}

            ProgramaNegocio neg = new ProgramaNegocio();
            int vEmpleadoY = 2;
            try
            {
                Stream newStream = null;
                using (ExcelPackage excelPackage = new ExcelPackage(newStream ?? new MemoryStream()))
                {
                    excelPackage.Workbook.Properties.Author = "Sigein 5.0";
                    excelPackage.Workbook.Properties.Title = "Avance programa de capacitacion";
                    excelPackage.Workbook.Properties.Comments = "Sigein 5.0";
                    excelPackage.Workbook.Worksheets.Add(txtPeriodo.InnerText);

                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];

                    //foreach (var item in vLstCompetencias)
                    //{
                    //   // int vEventosCompetencia = 1;

                    //    //if (vLstEventos.Count > 0)
                    //    //    vEventosCompetencia = vLstEventos.Where(w => w.ID_COMPETENCIA == item.ID_COMPETENCIA).Count();

                    //    worksheet.Cells[vColorCompetenciasY + 1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    worksheet.Cells[vColorCompetenciasY + 1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml(item.CL_COLOR));


                    //    ExcelRange RangoCompetencia = worksheet.Cells[vColorCompetenciasY + 1, 1, (vColorCompetenciasY + 1), 1];
                    //    RangoCompetencia.Merge = true;
                    //    RangoCompetencia.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

                    //    vColorCompetenciasY = vColorCompetenciasY + 1;
                    //}

                    worksheet.Cells[2, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    worksheet.Cells[2, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells[2, 1].Value = "No. de empleado";
                    worksheet.Cells[2, 1].Style.Font.Bold = true;
                    worksheet.Cells[2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[2, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    worksheet.Cells[2, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, 2].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells[2, 2].Value = "Nombre completo";
                    worksheet.Cells[2, 2].Style.Font.Bold = true;
                    worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[2, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    worksheet.Cells[2, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, 3].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells[2, 3].Value = "Clave de puesto";
                    worksheet.Cells[2, 3].Style.Font.Bold = true;
                    worksheet.Cells[2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[2, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    worksheet.Cells[2, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, 4].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells[2, 4].Value = "Puesto";
                    worksheet.Cells[2, 4].Style.Font.Bold = true;
                    worksheet.Cells[2, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //worksheet.Cells[4, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    //worksheet.Cells[4, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[4, 2].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    //worksheet.Cells[4, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //worksheet.Cells[4, 3].Value = "Clasificación";
                    //worksheet.Cells[4, 3].Style.Font.Bold = true;
                    //worksheet.Cells[4, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    //worksheet.Cells[4, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[4, 3].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    //worksheet.Cells[4, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //worksheet.Cells[4, 4].Value = "Competencia";
                    //worksheet.Cells[4, 4].Style.Font.Bold = true;
                    //worksheet.Cells[4, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    //worksheet.Cells[4, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[4, 4].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    //worksheet.Cells[4, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //worksheet.Cells[4, 5].Value = "Eventos de capacitación asociados";
                    //worksheet.Cells[4, 5].Style.Font.Bold = true;
                    //worksheet.Cells[4, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    //worksheet.Cells[4, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[4, 5].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    //worksheet.Cells[4, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    int vClasificacion = 4;
                    foreach (var item in vLstCompetencias.Select(s => s.NB_CLASIFICACION).Distinct())
                    {
                        worksheet.Cells[1, vClasificacion + 1].Value = item;
                        worksheet.Cells[1, vClasificacion + 1].Style.Font.Bold = true;
                        worksheet.Cells[1, vClasificacion + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[1, vClasificacion + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[1, vClasificacion + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[1, vClasificacion + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml(vLstCompetencias.Where(w => w.NB_CLASIFICACION == item).Select(s => s.CL_COLOR).FirstOrDefault()));
                        worksheet.Row(1).Height = 50;

                        ExcelRange RangoClasificacion = worksheet.Cells[1, vClasificacion + 1, 1, (vLstCompetencias.Where(w => w.NB_CLASIFICACION == item).Count()) + vClasificacion];
                        RangoClasificacion.Merge = true;
                        RangoClasificacion.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

                        vClasificacion = vClasificacion + (vLstCompetencias.Where(w => w.NB_CLASIFICACION == item).Count());
                    }


                    int vCompetencia = 4;
                    foreach (var item in vLstCompetencias)
                    {
                       // int vEventosCompetencia = 1;

                        //if (vLstEventos.Count > 0)
                        //    vEventosCompetencia = vLstEventos.Where(w => w.ID_COMPETENCIA == item.ID_COMPETENCIA).Count();

                       
                        worksheet.Cells[2, vCompetencia + 1].Value = item.NB_COMPETENCIA;
                        worksheet.Cells[2, vCompetencia + 1].Style.Font.Bold = true;
                        worksheet.Cells[2, vCompetencia + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[2, vCompetencia + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[2, vCompetencia + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        worksheet.Cells[2, vCompetencia + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, vCompetencia + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Row(2).Height = 30;

                        //worksheet.Cells[vCompetencia + 1].Value = item.NB_COMPETENCIA;
                        //worksheet.Cells[vCompetencia + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        //ExcelRange RangoCategoria = worksheet.Cells[vCompetencia + 1, 2, (vCompetencia + vEventosCompetencia), 2];
                        //RangoCategoria.Merge = true;
                        //RangoCategoria.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);


                        ExcelRange RangoCompetencia = worksheet.Cells[2,vCompetencia + 1, 2,(vCompetencia + 1)];
                        RangoCompetencia.Merge = true;
                        RangoCompetencia.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

                        vCompetencia = vCompetencia + 1;
                    }

                    foreach (var item in vLstEmpleadosProgramaExc)
                    {
                        worksheet.Cells[vEmpleadoY + 1, 1].Value = item.CL_EMPLEADO;
                        worksheet.Cells[vEmpleadoY + 1, 1].Style.Font.Bold = true;
                        worksheet.Cells[vEmpleadoY + 1, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        worksheet.Cells[vEmpleadoY + 1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[vEmpleadoY + 1, 1].Style.Fill.BackgroundColor.SetColor(Color.White);
                        worksheet.Cells[vEmpleadoY + 1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[vEmpleadoY + 1, 2].Value = item.NB_EMPLEADO;
                        worksheet.Cells[vEmpleadoY + 1, 2].Style.Font.Bold = true;
                        worksheet.Cells[vEmpleadoY + 1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        worksheet.Cells[vEmpleadoY + 1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[vEmpleadoY + 1, 2].Style.Fill.BackgroundColor.SetColor(Color.White);
                        worksheet.Cells[vEmpleadoY + 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[vEmpleadoY + 1, 3].Value = item.CL_PUESTO;
                        worksheet.Cells[vEmpleadoY + 1, 3].Style.Font.Bold = true;
                        worksheet.Cells[vEmpleadoY + 1, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        worksheet.Cells[vEmpleadoY + 1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[vEmpleadoY + 1, 3].Style.Fill.BackgroundColor.SetColor(Color.White);
                        worksheet.Cells[vEmpleadoY + 1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[vEmpleadoY + 1, 4].Value = item.NB_PUESTO;
                        worksheet.Cells[vEmpleadoY + 1, 4].Style.Font.Bold = true;
                        worksheet.Cells[vEmpleadoY + 1, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        worksheet.Cells[vEmpleadoY + 1, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[vEmpleadoY + 1, 4].Style.Fill.BackgroundColor.SetColor(Color.White);
                        worksheet.Cells[vEmpleadoY + 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Row(vEmpleadoY + 1).Height = 30;

                        vEmpleadoY++;
                    }

                    int vRengEmpleado = 2;
                    foreach (var itemEmpleado in vLstEmpleadosProgramaExc)
                    {
                        int vColumnaCompetencia = 4;
                        foreach (var itemCompetencia in vLstCompetencias)
                        {
                            var vEmpleadoCompetencia = vLstPrograma.Where(w => w.ID_EMPLEADO == itemEmpleado.ID_EMPLEADO && w.ID_COMPETENCIA == itemCompetencia.ID_COMPETENCIA).FirstOrDefault();

                            if(vEmpleadoCompetencia != null)
                            {
                                    worksheet.Cells[vRengEmpleado + 1, vColumnaCompetencia + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[vRengEmpleado + 1, vColumnaCompetencia + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[vRengEmpleado + 1, vColumnaCompetencia + 1].Style.Fill.BackgroundColor.SetColor(GetColor(vEmpleadoCompetencia.NO_COLOR_AVANCE));
                                    worksheet.Cells[vRengEmpleado + 1, vColumnaCompetencia + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

                                    if (vEmpleadoCompetencia != null && (vEmpleadoCompetencia.NO_COLOR_AVANCE == 10 || vEmpleadoCompetencia.NO_COLOR_AVANCE == 11 || vEmpleadoCompetencia.NO_COLOR_AVANCE == 12))
                                    {

                                        if (vEmpleadoCompetencia.ID_EVENTO != null)
                                            worksheet.Cells[vRengEmpleado + 1, vColumnaCompetencia + 1].Value =  ObtienePorcentajeEvento(vEmpleadoCompetencia.ID_EVENTO, vEmpleadoCompetencia.ID_EMPLEADO, vEmpleadoCompetencia.ID_COMPETENCIA);
                                        else
                                            worksheet.Cells[vRengEmpleado + 1, vColumnaCompetencia + 1].Value = "NC";
                                    }
                            }
                            else{
                                 worksheet.Cells[vRengEmpleado + 1, vColumnaCompetencia + 1].Value = "";
                                 worksheet.Cells[vRengEmpleado+ 1, vColumnaCompetencia + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                                 worksheet.Row(vRengEmpleado + 1).Height = 30;
                            }

                            vColumnaCompetencia++;
                        }
                        vRengEmpleado++;

                    }


                    //foreach(var itemCompetencia in vLstCompetencias)
                    //{
                    //    int vColumnaEmpleado = 5;

                    //    var itemsEventos = vLstEventos.Where(w => w.ID_COMPETENCIA == itemCompetencia.ID_COMPETENCIA);
                    //    if (itemsEventos != null)
                    //    {
                    //        foreach (var item in itemsEventos)
                    //        {
                    //            vColumnaEmpleado = 5;
                    //            worksheet.Cells[vCompetenciaEventoY + 1, 5].Value = item.NB_EVENTOS_RELACIONADOS;
                    //            worksheet.Cells[vCompetenciaEventoY + 1, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    //            worksheet.Row(vCompetenciaEventoY + 1).Height = 20;

                    //            foreach (var itemEmpleado in vLstEmpleados.OrderBy(o=>o.ID_EMPLEADO))
                    //            {
                                   
                    //                var vEmpleadoAvance = vLstAvancePrograma.Where(w => w.ID_COMPETENCIA == itemCompetencia.ID_COMPETENCIA && w.ID_EMPLEADO == itemEmpleado.ID_EMPLEADO && item.ID_EVENTO == w.ID_EVENTO).FirstOrDefault();

                    //                var vLstEvaluado = neg.ObtenerReporteEvaluado(vEmpleadoAvance.ID_EVENTO, vEmpleadoAvance.ID_EMPLEADO, vEmpleadoAvance.ID_COMPETENCIA, vIdRol).FirstOrDefault();

                    //                worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //                worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //                worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Style.Fill.BackgroundColor.SetColor(GetColor(vEmpleadoAvance.NO_COLOR_AVANCE));
                    //                worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

                    //                if (vLstEvaluado != null && (vEmpleadoAvance.NO_COLOR_AVANCE == 10 || vEmpleadoAvance.NO_COLOR_AVANCE == 11 || vEmpleadoAvance.NO_COLOR_AVANCE == 12))
                    //                {
                    //                    if (vLstEvaluado.PR_EVALUACION_PARTICIPANTE != null)
                    //                        worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Value = String.Format("{0:0.00}%", vLstEvaluado.PR_EVALUACION_PARTICIPANTE);
                    //                    else
                    //                        worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Value = "NC";
                    //                }
                    //                vColumnaEmpleado++;
                    //            }

                    //            vCompetenciaEventoY++;
                    //            vRenglonEmpleado++;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        worksheet.Cells[vCompetenciaEventoY + 1, 5].Value = "";
                    //        worksheet.Cells[vCompetenciaEventoY + 1, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    //        worksheet.Row(vCompetenciaEventoY + 1).Height = 20;

                    //        foreach(var itemEmpleado in vLstEmpleados)
                    //        {
                    //            worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Value = "";
                    //            vColumnaEmpleado++;
                    //        }
                    //        vCompetenciaEventoY++;
                    //        vRenglonEmpleado++;
                    //    }

                    
                    //}

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();


                    excelPackage.Save();
                    newStream = excelPackage.Stream;
                }
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=AvanceProgramaCapacitacion.xlsx");
                Response.BinaryWrite(((MemoryStream)newStream).ToArray());
                Response.End();
            }
            catch (Exception ex)
            {
                UtilMensajes.MensajeDB(rwmMensaje, "Ocurrio un error al procesar el Excel, Intente de nuevo.", Entidades.Externas.E_TIPO_RESPUESTA_DB.ERROR);
            }
        }

        public Color ObtenerColor(string pClColor)
        {

            Color vColor = System.Drawing.Color.Gray;

            switch (pClColor)
            {
                case "Orange":
                    vColor =  System.Drawing.Color.Orange;
                    break;
                case "MediumOrchid":
                    vColor = System.Drawing.Color.MediumOrchid;
                    break;
                case "OrangeRed":
                    vColor = System.Drawing.Color.OrangeRed;
                    break;
                case "Brown":
                    vColor = System.Drawing.Color.Brown;
                    break;
                case "Green":
                    vColor = System.Drawing.Color.Green;
                    break;
                case "SkyBlue":
                    vColor = System.Drawing.Color.SkyBlue;
                    break;
                case "LawnGreen":
                    vColor = System.Drawing.Color.LawnGreen;
                    break;
                case "Yellow":
                    vColor = System.Drawing.Color.Yellow;
                    break;

           }

            return vColor;
        }

        public void GenerarColumnasGroupName(List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result> pLstCompetencias)
        {
            foreach (var item in pLstCompetencias.Select(s => s.NB_CLASIFICACION).Distinct())
            {
                GridColumnGroup columnGroupPer = new GridColumnGroup();
                rgAvancePrograma.MasterTableView.ColumnGroups.Add(columnGroupPer);
                columnGroupPer.Name = item;
                columnGroupPer.HeaderText = item;
                columnGroupPer.HeaderStyle.BackColor = ObtenerColor(pLstCompetencias.Where(w => w.NB_CLASIFICACION == item).FirstOrDefault().CL_COLOR);
                columnGroupPer.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

                if (item == "Específicas")
                    columnGroupPer.HeaderStyle.ForeColor = Color.White;
            }
        }

        private DataTable CreateTable()
        {

            ProgramaNegocio nPrograma = new ProgramaNegocio();
            List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result> vLstCompetenciasPrograma = new List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result>();
            vLstCompetenciasPrograma = nPrograma.ObtenerCompetenciasPrograma(ID_PROGRAMA: pID_PROGRAMA);
            //GenerarColumnasGroupName(vLstCompetenciasPrograma);

            DataTable vRadGrid = new DataTable();

            vRadGrid.Columns.Add("ID_EMPLEADO", typeof(int));
            vRadGrid.Columns.Add("CL_EMPLEADO", typeof(string));
            vRadGrid.Columns.Add("NB_EMPLEADO", typeof(string));
            vRadGrid.Columns.Add("CL_PUESTO", typeof(string));
            vRadGrid.Columns.Add("NB_PUESTO", typeof(string));

            foreach (SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result item in vLstCompetenciasPrograma)
            {
                vRadGrid.Columns.Add(item.ID_COMPETENCIA.ToString(), typeof(string));
            }

            List<SPE_OBTIENE_K_PROGRAMA_EMPLEADO_Result> vLstEmpleadosPrograma = new List<SPE_OBTIENE_K_PROGRAMA_EMPLEADO_Result>();

            if (vLstSeleccionadosFiltros.Count > 0)
            {              
               var vLstSeleccionados = nPrograma.ObtieneEmpleadosParticipantes(pID_PROGRAMA: pID_PROGRAMA, pID_ROL: vIdRol).ToList();
               foreach (var item in vLstSeleccionados)
               {
                   if(vLstSeleccionadosFiltros.Exists(e => e.ID_EMPLEADO == item.ID_EMPLEADO))
                   {
                    vLstEmpleadosPrograma.Add(new SPE_OBTIENE_K_PROGRAMA_EMPLEADO_Result
                        {
                    ID_PROGRAMA_EMPLEADO = item.ID_PROGRAMA_EMPLEADO,
                    ID_PROGRAMA  = item.ID_PROGRAMA,
                    ID_EMPLEADO = item.ID_EMPLEADO,
                    NB_EMPLEADO = item.NB_EMPLEADO,
                    CL_EMPLEADO= item.CL_EMPLEADO,
                    ID_PUESTO = item.ID_PUESTO,
                    NB_PUESTO = item.NB_PUESTO,
                    CL_PUESTO = item.CL_PUESTO,
                    NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
                    CL_PROGRAMA = item.CL_PROGRAMA,
                    NB_PROGRAMA = item.NB_PROGRAMA,
                    ID_EMPRESA = item.ID_EMPRESA
                        });
                  }
               }
            }
            else
                vLstEmpleadosPrograma = nPrograma.ObtieneEmpleadosParticipantes(pID_PROGRAMA: pID_PROGRAMA, pID_ROL: vIdRol);

            List<SPE_OBTIENE_AVANCE_PROGRAMA_CAPACITACION_Result> vLstPrograma = new List<SPE_OBTIENE_AVANCE_PROGRAMA_CAPACITACION_Result>();
            vLstPrograma = nPrograma.ObtenerAvancePrograma(pID_PROGRAMA, ContextoUsuario.oUsuario.ID_EMPRESA, vXmlSeleccionados, vIdRol);


            foreach (SPE_OBTIENE_K_PROGRAMA_EMPLEADO_Result item in vLstEmpleadosPrograma)
            {
                DataRow vRegistro = vRadGrid.NewRow();

                vRegistro["ID_EMPLEADO"] = item.ID_EMPLEADO;
                vRegistro["CL_EMPLEADO"] = item.CL_EMPLEADO;
                vRegistro["NB_EMPLEADO"] = String.Format("<a href='#' onclick='OpenInventario({1})'>{0}</a>", item.NB_EMPLEADO, item.ID_EMPLEADO);
                vRegistro["CL_PUESTO"] = item.CL_PUESTO;
                vRegistro["NB_PUESTO"] = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", item.NB_PUESTO, item.ID_PUESTO);

                foreach (SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result vCompetencia in vLstCompetenciasPrograma)
                {
                    var vCompetenciaEmpleado = vLstPrograma.Where(w => w.ID_EMPLEADO == item.ID_EMPLEADO && w.ID_COMPETENCIA == vCompetencia.ID_COMPETENCIA).FirstOrDefault();
                    if (vCompetenciaEmpleado != null)
                    {
                        if (vCompetenciaEmpleado.NO_COLOR_AVANCE == 11 || vCompetenciaEmpleado.NO_COLOR_AVANCE == 12)
                            vRegistro[vCompetencia.ID_COMPETENCIA.ToString()] = "<div class='Color" + vCompetenciaEmpleado.NO_COLOR_AVANCE.ToString() + "'><div class='triangulo" + vCompetenciaEmpleado.NO_COLOR_AVANCE.ToString() + "'><br /></div><br />" + String.Format("<p title='{1}')'>{0}</p>", ObtienePorcentajeEvento(vCompetenciaEmpleado.ID_EVENTO, vCompetenciaEmpleado.ID_EMPLEADO, vCompetenciaEmpleado.ID_COMPETENCIA), vCompetenciaEmpleado.NB_EVENTOS_RELACIONADOS) + "</div>";
                        else if (vCompetenciaEmpleado.NO_COLOR_AVANCE == 10)
                            vRegistro[vCompetencia.ID_COMPETENCIA.ToString()] = "<div class='Color" + vCompetenciaEmpleado.NO_COLOR_AVANCE.ToString() + "'><div class='triangulo" + vCompetenciaEmpleado.NO_COLOR_AVANCE.ToString() + "'><br /></div>" + String.Format("<p title='{1}')'>{0}</p>", ObtienePorcentajeEvento(vCompetenciaEmpleado.ID_EVENTO, vCompetenciaEmpleado.ID_EMPLEADO, vCompetenciaEmpleado.ID_COMPETENCIA), vCompetenciaEmpleado.NB_EVENTOS_RELACIONADOS) + "</div>";
                        else
                            vRegistro[vCompetencia.ID_COMPETENCIA.ToString()] = "<div class='Color" + vCompetenciaEmpleado.NO_COLOR_AVANCE.ToString() + "'><div class='triangulo" + vCompetenciaEmpleado.NO_COLOR_AVANCE.ToString() + "'><br /></div><br /></div>";
                    }
                }

                vRadGrid.Rows.Add(vRegistro);
            }

            return vRadGrid;
        }

        protected string ObtienePorcentajeEvento(int? pIdEvento, int? pIdEmpleado, int? pIdCompetencia)
        {
            string vPorcentaje = "NC";
            ProgramaNegocio nPrograma = new ProgramaNegocio();
            var vLstEvaluado = nPrograma.ObtenerReporteEvaluado(pIdEvento, pIdEmpleado, pIdCompetencia, vIdRol).FirstOrDefault();

            if (vLstEvaluado != null)
            {
                if (vLstEvaluado.PR_EVALUACION_PARTICIPANTE != null)
                    vPorcentaje = String.Format("{0:0.00}%", vLstEvaluado.PR_EVALUACION_PARTICIPANTE);
            }

            return vPorcentaje;
        }

              private void ConfigurarColumna(GridColumn pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pAlinear)
        {
            if (pGenerarEncabezado)
            {
               // pEncabezado = GeneraEncabezado(pColumna);
                //pColumna.ColumnGroupName = "TABMEDIO";
                //pColumna.ItemStyle.Font.Bold = true;
            }

            pColumna.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumna.HeaderText = pEncabezado;
            pColumna.Visible = pVisible;


            if (pAlinear)
            {
                pColumna.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            }

            if (pFiltrarColumna & pVisible)
            {
                pColumna.AutoPostBackOnFilter = true;
                pColumna.CurrentFilterFunction = GridKnownFunction.Contains;

                if (pWidth <= 60)
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth);
                }
                else
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 70);
                }
            }
            else
            {
                (pColumna as GridBoundColumn).AllowFiltering = false;
            }
        }

        private void ConfigurarColumnaCompetencia(GridColumn pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pAlinear, bool pColumnGroup)
        {
            if (pGenerarEncabezado)
            {
                pEncabezado = GeneraEncabezado(pColumna);
            }

            pColumna.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumna.HeaderText = pEncabezado;
            pColumna.HeaderTooltip = ObtieneTooltip(pColumna);
            pColumna.Visible = pVisible;

            if (pColumnGroup)
            {
                pColumna.ColumnGroupName = GenerarColumnGroupName(pColumna);
            }

            if (pAlinear)
            {
                pColumna.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                pColumna.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            }

            if (pFiltrarColumna & pVisible)
            {
                pColumna.AutoPostBackOnFilter = true;
                pColumna.CurrentFilterFunction = GridKnownFunction.Contains;

                if (pWidth <= 60)
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth);
                }
                else
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 70);
                }
            }
            else
            {
                (pColumna as GridBoundColumn).AllowFiltering = false;
            }
        }

        private string ObtieneTooltip(GridColumn pColumna)
        {
            int vResultado;
            string vTooltip = "";
            string vCompetencia = pColumna.UniqueName.ToString();
            ProgramaNegocio nPrograma = new ProgramaNegocio();
            List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result> vLstCompetenciasPrograma = new List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result>();
            vLstCompetenciasPrograma = nPrograma.ObtenerCompetenciasPrograma(ID_PROGRAMA: pID_PROGRAMA);

            if (int.TryParse(vCompetencia, out vResultado))
            {
                var vDatosCompetencia = vLstCompetenciasPrograma.Where(w => w.ID_COMPETENCIA == vResultado).FirstOrDefault();
                if (vDatosCompetencia != null)
                {
                    vTooltip = vDatosCompetencia.NB_CATEGORIA;
                }
            }
            return vTooltip;
        }

        private string GeneraEncabezado(GridColumn pColumna)
        {
            int vResultado;
            string vEncabezado = "";
            string vEmpleado = pColumna.UniqueName.ToString();
            ProgramaNegocio nPrograma = new ProgramaNegocio();
            List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result> vLstCompetenciasPrograma = new List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result>();
            vLstCompetenciasPrograma = nPrograma.ObtenerCompetenciasPrograma(ID_PROGRAMA: pID_PROGRAMA);

            if (int.TryParse(vEmpleado, out vResultado))
            {
                var vDatosEmpleado = vLstCompetenciasPrograma.Where(w => w.ID_COMPETENCIA == vResultado ).FirstOrDefault();
                if (vDatosEmpleado != null)
                {
                    vEncabezado = vDatosEmpleado.NB_COMPETENCIA;
                }
            }
            return vEncabezado;
        }

        private string GenerarColumnGroupName(GridColumn pColumn)
        {
            int vResultado;
            string vGrupo = "";
            string vIdComptencia = pColumn.UniqueName.ToString();
            ProgramaNegocio nPrograma = new ProgramaNegocio();
            List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result> vLstCompetenciasPrograma = new List<SPE_OBTIENE_K_PROGRAMA_COMPETENCIA_Result>();
            vLstCompetenciasPrograma = nPrograma.ObtenerCompetenciasPrograma(ID_PROGRAMA: pID_PROGRAMA, ID_COMPETENCIA: int.Parse(vIdComptencia));

            if (int.TryParse(vIdComptencia, out vResultado))
            {
                var vDatosEmpleado = vLstCompetenciasPrograma.Where(w => w.ID_COMPETENCIA == vResultado).FirstOrDefault();
                if (vDatosEmpleado != null)
                {
                    vGrupo = vDatosEmpleado.NB_CLASIFICACION;
                }
            }
            return vGrupo;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!IsPostBack)
            {
                if (Request.QueryString["IdPrograma"] != null)
                {
                    pID_PROGRAMA = int.Parse((Request.QueryString["IdPrograma"]));
                
                    vLstSeleccionados = new List<E_AVANCE_PROGRAMA_CAPACITACION>();
                    vLstSeleccionadosFiltros = new List<E_AVANCE_PROGRAMA_CAPACITACION>();
                    vLstAvancePrograma = new List<E_AVANCE_PROGRAMA_CAPACITACION>();
                    vNoEmpleados = 0;
                    CargarDatos();
                }
                if (Request.Params["clOrigen"] != null)
                {
                    if (Request.Params["clOrigen"].ToString() == "SUCESION")
                    {
                        ProgramaNegocio neg = new ProgramaNegocio();
                        List<SPE_OBTIENE_AVANCE_PROGRAMA_CAPACITACION_Result> vlstEmpleado = neg.ObtenerAvancePrograma(pID_PROGRAMA, ContextoUsuario.oUsuario.ID_EMPRESA,null, vIdRol);

                        int vIdEmpleadoSucesion = int.Parse(Request.Params["IdEmpleado"].ToString());

                        E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                        {
                            ID_EMPLEADO = vIdEmpleadoSucesion,
                            NB_EMPLEADO = vlstEmpleado.Where(w => w.ID_EMPLEADO == vIdEmpleadoSucesion).FirstOrDefault().NB_EMPLEADO.ToString(),
                            NB_PUESTO = vlstEmpleado.Where(w => w.ID_EMPLEADO == vIdEmpleadoSucesion).FirstOrDefault().NB_PUESTO.ToString()
                        };
                        vLstSeleccionados.Add(vLstItem);

                        btnEmpleadoFiltro.Enabled = false;
                    }
                }

            }
        }

        protected void ramAvanceProgramaCapacitacion_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccionados = new E_SELECTOR();
            string pParameter = e.Argument;

                vSeleccionados = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

                if (vSeleccionados.clTipo == "EMPLEADO")
                {
                    CargarAvancePrograma(vSeleccionados.oSeleccion.ToString());
                }
        }

        protected void rgPrograma_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgPrograma.DataSource = vLstSeleccionadosFiltros;
        }

        protected void rgPrograma_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                EliminarEmpleado(int.Parse(item.GetDataKeyValue("ID_EMPLEADO").ToString()));
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            GenerarExcelAvance();
        }

        protected void rgAvancePrograma_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgAvancePrograma.DataSource = CreateTable();
        }

        protected void rgAvancePrograma_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            switch (e.Column.UniqueName)
            {
                case "ID_EMPLEADO":
                    ConfigurarColumna(e.Column, 0, "", false, false, false, false);
                    break;
                case "CL_EMPLEADO":
                    ConfigurarColumna(e.Column, 100, "No. de empleado", true, false, false, false);
                    break;
                case "NB_EMPLEADO":
                    ConfigurarColumna(e.Column, 200, "Nombre completo", true, false, false, false);
                    break;
                case "CL_PUESTO":
                    ConfigurarColumna(e.Column, 100, "Clave de puesto", true, false, false, false);
                    break;
                case "NB_PUESTO":
                    ConfigurarColumna(e.Column, 200, "Puesto", true, false, false, false);
                    break;
                case "column":
                    break;
                case "ExpandColumn": 
                    break;
                default:
                    ConfigurarColumnaCompetencia(e.Column, 120, "", true, true, false, true, true);
                    break;
            }
        }

    }
}