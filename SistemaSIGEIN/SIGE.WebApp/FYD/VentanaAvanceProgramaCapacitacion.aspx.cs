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

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vNoEmpleados
        {
            get { return (int)ViewState["vs_no_empleados"]; }
            set { ViewState["vs_no_empleados"] = value; }
        }

        #endregion

        #region Funciones

        //public List<E_PROGRAMA_CAPACITACION> cargarParseProgramasCapacitacion(List<SPE_OBTIENE_K_PROGRAMA_Result> lista)
        //{
        //    List<E_PROGRAMA_CAPACITACION> programas = new List<E_PROGRAMA_CAPACITACION>();
        //    if (lista.Count > 0)
        //    {
        //        foreach (var item in lista)
        //        {
        //            programas.Add(new E_PROGRAMA_CAPACITACION
        //            {
        //                ID_PROGRAMA_EMPLEADO_COMPETENCIA = item.ID_PROGRAMA_EMPLEADO_COMPETENCIA,
        //                ID_PROGRAMA = item.ID_PROGRAMA,
        //                ID_PROGRAMA_COMPETENCIA = item.ID_PROGRAMA_COMPETENCIA,
        //                ID_PROGRAMA_EMPLEADO = item.ID_PROGRAMA_EMPLEADO,
        //                CL_PRIORIDAD = item.CL_PRIORIDAD,
        //                PR_RESULTADO = item.PR_RESULTADO,
        //                CL_PROGRAMA = item.CL_PROGRAMA,
        //                NB_PROGRAMA = item.NB_PROGRAMA,
        //                CL_TIPO_PROGRAMA = item.CL_TIPO_PROGRAMA,
        //                CL_ESTADO = item.CL_ESTADO,
        //                CL_VERSION = item.CL_VERSION,
        //                ID_DOCUMENTO_AUTORIZACION = item.ID_DOCUMENTO_AUTORIZACION,
        //                NB_COMPETENCIA = item.NB_COMPETENCIA,
        //                NB_CLASIFICACION_COMPETENCIA = item.NB_CLASIFICACION_COMPETENCIA,
        //                CL_TIPO_COMPETENCIA = item.CL_TIPO_COMPETENCIA,
        //                CL_COLOR = item.CL_COLOR,
        //                NB_EVALUADO = item.NB_EVALUADO,
        //                CL_EVALUADO = item.CL_EVALUADO,
        //                NB_PUESTO = item.NB_PUESTO,
        //                CL_PUESTO = item.CL_PUESTO,
        //                NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
        //                ID_COMPETENCIA = item.ID_COMPETENCIA,
        //                ID_EMPLEADO = item.ID_EMPLEADO,
        //                ID_PERIODO = item.ID_PERIODO,
        //                CL_PERIODO = item.CL_PERIODO,
        //                NB_PERIODO = item.NB_PERIODO,
        //                DS_PERIODO = item.DS_PERIODO,
        //                TIPO_EVALUACION = item.TIPO_EVALUACION,
        //                DS_NOTAS = (item.DS_NOTAS != null) ? valdarDsNotas(item.DS_NOTAS.ToString()) : ""
        //            });
        //        }
        //    }
        //    return programas;
        //}

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
                txtPeriodo.InnerText = oPrograma.CL_PROGRAMA + " " + oPrograma.NB_PROGRAMA;
                txtTipoEvaluacion.InnerText = oPrograma.TIPO_EVALUACION;
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

                GeneraAvance(true);
            }
            else
            {
                E_AVANCE_PROGRAMA_CAPACITACION vEmpleado = vLstSeleccionados.Where(w => w.ID_EMPLEADO == pIdEmpleado).FirstOrDefault();

                if (vEmpleado != null)
                {
                    vLstSeleccionados.Remove(vEmpleado);
                }

                GeneraAvance(true);
            }
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

            GeneraAvance(true);
        }

        public void GeneraAvance(bool pFgFiltro)
        {
            string vXmlSeleccionados = null;
            ProgramaNegocio neg = new ProgramaNegocio();

            if (vLstSeleccionadosFiltros.Count > 0)
            {
                if (vLstSeleccionadosFiltros.Count > 0)
                {
                    vXmlSeleccionados = (new XElement("EMPLEADOS", vLstSeleccionadosFiltros.Select(s => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", s.ID_EMPLEADO))))).ToString();

                    rgPrograma.Rebind();
                }
                else if (pFgFiltro)
                    vXmlSeleccionados = "<EMPLEADOS></EMPLEADOS>";

                var vLista = neg.ObtenerAvancePrograma(pID_PROGRAMA, ContextoUsuario.oUsuario.ID_EMPRESA, vXmlSeleccionados);
                vLstAvancePrograma = new List<E_AVANCE_PROGRAMA_CAPACITACION>();
                int vCount = vLstSeleccionadosFiltros.Count();
                vLstEventos = new List<E_AVANCE_PROGRAMA_CAPACITACION>();

                foreach (var s in vLista)
                {
                    E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                    {
                        ID_PROGRAMA_COMPETENCIA = s.ID_PROGRAMA_COMPETENCIA,
                        NB_CATEGORIA = s.NB_CATEGORIA,
                        NB_CLASIFICACION = s.NB_CLASIFICACION,
                        CL_COLOR = s.CL_COLOR,
                        ID_COMPETENCIA = s.ID_COMPETENCIA,
                        NB_COMPETENCIA = s.NB_COMPETENCIA,
                        //CL_EMPLEADO = String.Format("<a href='#' onclick='OpenInventario({1})'>{0}</a>", s.CL_EMPLEADO, s.ID_EMPLEADO),
                        CL_EMPLEADO = s.CL_EMPLEADO,
                        //CL_PUESTO = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", s.CL_PUESTO, s.ID_PUESTO),
                        CL_PUESTO = s.CL_PUESTO,
                        ID_PROGRAMA_EMPLEADO = s.ID_PROGRAMA_COMPETENCIA,
                        NB_EMPLEADO = String.Format("{0}<br>{1}<br>{2}<br>{3}", String.Format("<a href='#' onclick='OpenInventario({1})'>{0}</a>", s.CL_EMPLEADO, s.ID_EMPLEADO), s.NB_EMPLEADO, String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", s.CL_PUESTO, s.ID_PUESTO), s.NB_PUESTO),
                        ID_EMPLEADO = s.ID_EMPLEADO,
                        NB_PUESTO = s.NB_PUESTO,
                        ID_PROGRAMA_EMPLEADO_COMPETENCIA = s.ID_PROGRAMA_EMPLEADO_COMPETENCIA,
                        ID_EVENTO = s.ID_EVENTO,
                        FE_INICIO = s.FE_INICIO,
                        FE_TERMINO = s.FE_TERMINO,
                        ID_EVENTO_PARTICIPANTE = s.ID_EVENTO_PARTICIPANTE,
                        PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO,
                        NB_EVENTOS_RELACIONADOS = String.Format("<a href='#' onclick='OpenEvento({1})'>{0}</a>", s.NB_EVENTOS_RELACIONADOS, s.ID_EVENTO),
                        NO_COLOR_AVANCE = s.NO_COLOR_AVANCE,
                        CL_COLOR_AVANCE = s.CL_COLOR_AVANCE,
                        CL_COLOR_ASISTENCIA = s.CL_COLOR_ASISTENCIA
                    };

                    E_AVANCE_PROGRAMA_CAPACITACION vLstItemEvento = new E_AVANCE_PROGRAMA_CAPACITACION
                    {
                        ID_COMPETENCIA = s.ID_COMPETENCIA,
                        ID_EVENTO = s.ID_EVENTO,
                        FE_INICIO = s.FE_INICIO,
                        FE_TERMINO = s.FE_TERMINO,
                        ID_EVENTO_PARTICIPANTE = s.ID_EVENTO_PARTICIPANTE,
                        PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO,
                        NB_EVENTOS_RELACIONADOS = s.NB_EVENTOS_RELACIONADOS,
                        NO_COLOR_AVANCE = s.NO_COLOR_AVANCE,
                        CL_COLOR_AVANCE = s.CL_COLOR_AVANCE,
                        CL_COLOR_ASISTENCIA = s.CL_COLOR_ASISTENCIA
                    };

                    if (!(vLstEventos.Exists(e => e.ID_EVENTO == s.ID_EVENTO && e.ID_COMPETENCIA == s.ID_COMPETENCIA)))
                        vLstEventos.Add(vLstItemEvento);

                    vLstAvancePrograma.Add(vLstItem);
                }

                if (!pFgFiltro)
                {
                    foreach (var s in vLista)
                    {
                        if (!vLstSeleccionados.Exists(e => e.ID_EMPLEADO == s.ID_EMPLEADO))
                        {
                            E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                            {
                                ID_EMPLEADO = s.ID_EMPLEADO,
                                NB_EMPLEADO = s.NB_EMPLEADO,
                                NB_PUESTO = s.NB_PUESTO
                            };
                            vLstSeleccionados.Add(vLstItem);
                        }
                    }
                }

                vNoEmpleados = (from a in vLista select new { a.ID_EMPLEADO }).Distinct().Count();
                pgridAvanceProgramaCapacitacion.DataSource = vLstAvancePrograma;
            }
            else
            {
                if (vLstSeleccionados.Count > 0)
                {
                    vXmlSeleccionados = (new XElement("EMPLEADOS", vLstSeleccionados.Select(s => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", s.ID_EMPLEADO))))).ToString();

                    rgPrograma.Rebind();
                }
                else if (pFgFiltro)
                    vXmlSeleccionados = "<EMPLEADOS></EMPLEADOS>";

                var vLista = neg.ObtenerAvancePrograma(pID_PROGRAMA, ContextoUsuario.oUsuario.ID_EMPRESA, vXmlSeleccionados);
                vLstAvancePrograma = new List<E_AVANCE_PROGRAMA_CAPACITACION>();
                int vCount = vLstSeleccionados.Count();
                vLstEventos = new List<E_AVANCE_PROGRAMA_CAPACITACION>();

                foreach (var s in vLista)
                {
                    E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                    {
                        ID_PROGRAMA_COMPETENCIA = s.ID_PROGRAMA_COMPETENCIA,
                        NB_CATEGORIA = s.NB_CATEGORIA,
                        NB_CLASIFICACION = s.NB_CLASIFICACION,
                        CL_COLOR = s.CL_COLOR,
                        ID_COMPETENCIA = s.ID_COMPETENCIA,
                        NB_COMPETENCIA = s.NB_COMPETENCIA,
                        //CL_EMPLEADO = String.Format("<a href='#' onclick='OpenInventario({1})'>{0}</a>", s.CL_EMPLEADO, s.ID_EMPLEADO),
                        CL_EMPLEADO = s.CL_EMPLEADO,
                        //CL_PUESTO = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", s.CL_PUESTO, s.ID_PUESTO),
                        CL_PUESTO = s.CL_PUESTO,
                        ID_PROGRAMA_EMPLEADO = s.ID_PROGRAMA_COMPETENCIA,
                        NB_EMPLEADO = String.Format("{0}<br>{1}<br>{2}<br>{3}", String.Format("<a href='#' onclick='OpenInventario({1})'>{0}</a>", s.CL_EMPLEADO, s.ID_EMPLEADO), s.NB_EMPLEADO, String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", s.CL_PUESTO, s.ID_PUESTO), s.NB_PUESTO),
                        ID_EMPLEADO = s.ID_EMPLEADO,
                        NB_PUESTO = s.NB_PUESTO,
                        ID_PROGRAMA_EMPLEADO_COMPETENCIA = s.ID_PROGRAMA_EMPLEADO_COMPETENCIA,
                        ID_EVENTO = s.ID_EVENTO,
                        FE_INICIO = s.FE_INICIO,
                        FE_TERMINO = s.FE_TERMINO,
                        ID_EVENTO_PARTICIPANTE = s.ID_EVENTO_PARTICIPANTE,
                        PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO,
                        NB_EVENTOS_RELACIONADOS = String.Format("<a href='#' onclick='OpenEvento({1})'>{0}</a>", s.NB_EVENTOS_RELACIONADOS, s.ID_EVENTO),
                        NO_COLOR_AVANCE = s.NO_COLOR_AVANCE,
                        CL_COLOR_AVANCE = s.CL_COLOR_AVANCE,
                        CL_COLOR_ASISTENCIA = s.CL_COLOR_ASISTENCIA
                    };

                    E_AVANCE_PROGRAMA_CAPACITACION vLstItemEvento = new E_AVANCE_PROGRAMA_CAPACITACION
                    {
                        ID_COMPETENCIA = s.ID_COMPETENCIA,
                        ID_EVENTO = s.ID_EVENTO,
                        FE_INICIO = s.FE_INICIO,
                        FE_TERMINO = s.FE_TERMINO,
                        ID_EVENTO_PARTICIPANTE = s.ID_EVENTO_PARTICIPANTE,
                        PR_CUMPLIMIENTO = s.PR_CUMPLIMIENTO,
                        NB_EVENTOS_RELACIONADOS = s.NB_EVENTOS_RELACIONADOS,
                        NO_COLOR_AVANCE = s.NO_COLOR_AVANCE,
                        CL_COLOR_AVANCE = s.CL_COLOR_AVANCE,
                        CL_COLOR_ASISTENCIA = s.CL_COLOR_ASISTENCIA
                    };

                    if (!(vLstEventos.Exists(e => e.ID_EVENTO == s.ID_EVENTO && e.ID_COMPETENCIA == s.ID_COMPETENCIA)))
                        vLstEventos.Add(vLstItemEvento);

                    vLstAvancePrograma.Add(vLstItem);
                }

                if (!pFgFiltro)
                {
                    foreach (var s in vLista)
                    {
                        if (!vLstSeleccionados.Exists(e => e.ID_EMPLEADO == s.ID_EMPLEADO))
                        {
                            E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                            {
                                ID_EMPLEADO = s.ID_EMPLEADO,
                                NB_EMPLEADO = s.NB_EMPLEADO,
                                NB_PUESTO = s.NB_PUESTO
                            };
                            vLstSeleccionados.Add(vLstItem);
                        }
                    }
                }

                vNoEmpleados = (from a in vLista select new { a.ID_EMPLEADO }).Distinct().Count();
                pgridAvanceProgramaCapacitacion.DataSource = vLstAvancePrograma;
            }
          
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
            vLstEmpleados = new List<E_AVANCE_PROGRAMA_CAPACITACION>();


            foreach (var item in vLstAvancePrograma)
            {
                E_AVANCE_PROGRAMA_CAPACITACION vLstEmp = new E_AVANCE_PROGRAMA_CAPACITACION
                {
                    ID_EMPLEADO = item.ID_EMPLEADO,
                    CL_EMPLEADO = item.CL_EMPLEADO,
                    NB_EMPLEADO = item.NB_EMPLEADO,
                    CL_PUESTO = item.CL_PUESTO,
                    NB_PUESTO = item.NB_PUESTO,
                };

                if (!vLstEmpleados.Exists(e => e.ID_EMPLEADO == item.ID_EMPLEADO))
                    vLstEmpleados.Add(vLstEmp);
            }

            ProgramaNegocio neg = new ProgramaNegocio();
            int vColorCompetenciasY = 4;
            int vEmpleadoX = 5;
            int vCompetenciaEventoY = 4;
            int vRenglonEmpleado = 4;
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

                    foreach (var item in vLstCompetencias)
                    {
                        int vEventosCompetencia = 1;

                        if (vLstEventos.Count > 0)
                            vEventosCompetencia = vLstEventos.Where(w => w.ID_COMPETENCIA == item.ID_COMPETENCIA).Count();

                        worksheet.Cells[vColorCompetenciasY + 1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[vColorCompetenciasY + 1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml(item.CL_COLOR));


                        ExcelRange RangoCompetencia = worksheet.Cells[vColorCompetenciasY + 1, 1, (vColorCompetenciasY + vEventosCompetencia), 1];
                        RangoCompetencia.Merge = true;
                        RangoCompetencia.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

                        vColorCompetenciasY = vColorCompetenciasY + vEventosCompetencia;
                    }

                    worksheet.Cells[4, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    worksheet.Cells[4, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[4, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells[4, 2].Value = "Categoria";
                    worksheet.Cells[4, 2].Style.Font.Bold = true;
                    worksheet.Cells[4, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    worksheet.Cells[4, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[4, 2].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells[4, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[4, 3].Value = "Clasificación";
                    worksheet.Cells[4, 3].Style.Font.Bold = true;
                    worksheet.Cells[4, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    worksheet.Cells[4, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[4, 3].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells[4, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[4, 4].Value = "Competencia";
                    worksheet.Cells[4, 4].Style.Font.Bold = true;
                    worksheet.Cells[4, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    worksheet.Cells[4, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[4, 4].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells[4, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[4, 5].Value = "Eventos de capacitación asociados";
                    worksheet.Cells[4, 5].Style.Font.Bold = true;
                    worksheet.Cells[4, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    worksheet.Cells[4, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[4, 5].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells[4, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int vCompetencia = 4;
                    foreach (var item in vLstCompetencias)
                    {
                        int vEventosCompetencia = 1;

                        if (vLstEventos.Count > 0)
                            vEventosCompetencia = vLstEventos.Where(w => w.ID_COMPETENCIA == item.ID_COMPETENCIA).Count();

                        worksheet.Cells[vCompetencia + 1, 2].Value = item.NB_CATEGORIA;
                        worksheet.Cells[vCompetencia + 1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[vCompetencia + 1, 3].Value = item.NB_CLASIFICACION;
                        worksheet.Cells[vCompetencia + 1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[vCompetencia + 1, 4].Value = item.NB_COMPETENCIA;
                        worksheet.Cells[vCompetencia + 1, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        ExcelRange RangoCategoria = worksheet.Cells[vCompetencia + 1, 2, (vCompetencia + vEventosCompetencia), 2];
                        RangoCategoria.Merge = true;
                        RangoCategoria.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

                        ExcelRange RangoClasificacion = worksheet.Cells[vCompetencia + 1, 3, (vCompetencia + vEventosCompetencia), 3];
                        RangoClasificacion.Merge = true;
                        RangoClasificacion.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

                        ExcelRange RangoCompetencia = worksheet.Cells[vCompetencia + 1, 4, (vCompetencia + vEventosCompetencia), 4];
                        RangoCompetencia.Merge = true;
                        RangoCompetencia.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

                        vCompetencia = vCompetencia + vEventosCompetencia;
                    }

                    foreach (var item in vLstEmpleados.OrderBy(o => o.ID_EMPLEADO))
                    {
                        worksheet.Cells[1, vEmpleadoX + 1].Value = item.CL_EMPLEADO;
                        worksheet.Row(1).Height = 20;
                        worksheet.Cells[1, vEmpleadoX + 1].Style.Font.Bold = true;
                        worksheet.Cells[1, vEmpleadoX + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        worksheet.Cells[1, vEmpleadoX + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[1, vEmpleadoX + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells[1, vEmpleadoX + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[2, vEmpleadoX + 1].Value = vLstSeleccionados != null? vLstSeleccionados.Where(w => w.ID_EMPLEADO == item.ID_EMPLEADO).FirstOrDefault().NB_EMPLEADO : item.NB_EMPLEADO;
                        worksheet.Cells[2, vEmpleadoX + 1].Style.Font.Bold = true;
                        worksheet.Row(2).Height = 20;
                        worksheet.Cells[2, vEmpleadoX + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        worksheet.Cells[2, vEmpleadoX + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, vEmpleadoX + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells[2, vEmpleadoX + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[3, vEmpleadoX + 1].Value = item.CL_PUESTO;
                        worksheet.Row(3).Height = 20;
                        worksheet.Cells[3, vEmpleadoX + 1].Style.Font.Bold = true;
                        worksheet.Cells[3, vEmpleadoX + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        worksheet.Cells[3, vEmpleadoX + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[3, vEmpleadoX + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells[3, vEmpleadoX + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Cells[4, vEmpleadoX + 1].Value = item.NB_PUESTO;
                        worksheet.Row(4).Height = 20;
                        worksheet.Cells[4, vEmpleadoX + 1].Style.Font.Bold = true;
                        worksheet.Cells[4, vEmpleadoX + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        worksheet.Cells[4, vEmpleadoX + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[4, vEmpleadoX + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells[4, vEmpleadoX + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        vEmpleadoX++;
                    }

                    foreach(var itemCompetencia in vLstCompetencias)
                    {
                        int vColumnaEmpleado = 5;

                        var itemsEventos = vLstEventos.Where(w => w.ID_COMPETENCIA == itemCompetencia.ID_COMPETENCIA);
                        if (itemsEventos != null)
                        {
                            foreach (var item in itemsEventos)
                            {
                                vColumnaEmpleado = 5;
                                worksheet.Cells[vCompetenciaEventoY + 1, 5].Value = item.NB_EVENTOS_RELACIONADOS;
                                worksheet.Cells[vCompetenciaEventoY + 1, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                                worksheet.Row(vCompetenciaEventoY + 1).Height = 20;

                                foreach (var itemEmpleado in vLstEmpleados)
                                {
                                   
                                    var vEmpleadoAvance = vLstAvancePrograma.Where(w => w.ID_COMPETENCIA == itemCompetencia.ID_COMPETENCIA && w.ID_EMPLEADO == itemEmpleado.ID_EMPLEADO && item.ID_EVENTO == w.ID_EVENTO).FirstOrDefault();

                                    var vLstEvaluado = neg.ObtenerReporteEvaluado(vEmpleadoAvance.ID_EVENTO, vEmpleadoAvance.ID_EMPLEADO, vEmpleadoAvance.ID_COMPETENCIA).FirstOrDefault();

                                    worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Style.Fill.BackgroundColor.SetColor(GetColor(vEmpleadoAvance.NO_COLOR_AVANCE));
                                    worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);

                                    if (vLstEvaluado != null && (vEmpleadoAvance.NO_COLOR_AVANCE == 10 || vEmpleadoAvance.NO_COLOR_AVANCE == 11 || vEmpleadoAvance.NO_COLOR_AVANCE == 12))
                                    {
                                        if (vLstEvaluado.PR_EVALUACION_PARTICIPANTE != null)
                                            worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Value = String.Format("{0:0.00}%", vLstEvaluado.PR_EVALUACION_PARTICIPANTE);
                                        else
                                            worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Value = "NC";
                                    }
                                    vColumnaEmpleado++;
                                }

                                vCompetenciaEventoY++;
                                vRenglonEmpleado++;
                            }
                        }
                        else
                        {
                            worksheet.Cells[vCompetenciaEventoY + 1, 5].Value = "";
                            worksheet.Cells[vCompetenciaEventoY + 1, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                            worksheet.Row(vCompetenciaEventoY + 1).Height = 20;

                            foreach(var itemEmpleado in vLstEmpleados)
                            {
                                worksheet.Cells[vRenglonEmpleado + 1, vColumnaEmpleado + 1].Value = "";
                                vColumnaEmpleado++;
                            }
                            vCompetenciaEventoY++;
                            vRenglonEmpleado++;
                        }

                    
                    }

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

        //protected void ButtonExcel_Click(object sender, EventArgs e)
        //{
        //    string alternateText = (sender as ImageButton).AlternateText;
        //    pgridAvanceProgramaCapacitacion.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), alternateText);
        //    pgridAvanceProgramaCapacitacion.ExportSettings.IgnorePaging = true;
        //    pgridAvanceProgramaCapacitacion.ExportToExcel();
        //}

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.QueryString["IdPrograma"] != null)
                {
                    pID_PROGRAMA = int.Parse((Request.QueryString["IdPrograma"]));

                    //  CargarDatos();
                    //txtNbDepartamento.Text = vProgramasCapacitacion.ElementAt(0).NB_DEPARTAMENTO.ToString();
                    //txtPeriodo.Text = vProgramasCapacitacion.ElementAt(0).CL_PROGRAMA.ToString();
                    //txtTipoEvaluacion.Text = vProgramasCapacitacion.ElementAt(0).TIPO_EVALUACION.ToString();
                    //radEditorNotas.Content = vProgramasCapacitacion.ElementAt(0).DS_NOTAS.ToString();
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
                        List<SPE_OBTIENE_AVANCE_PROGRAMA_CAPACITACION_Result> vlstEmpleado = neg.ObtenerAvancePrograma(pID_PROGRAMA, ContextoUsuario.oUsuario.ID_EMPRESA, null);

                        int vIdEmpleadoSucesion = int.Parse(Request.Params["IdEmpleado"].ToString());

                        E_AVANCE_PROGRAMA_CAPACITACION vLstItem = new E_AVANCE_PROGRAMA_CAPACITACION
                        {
                            ID_EMPLEADO = vIdEmpleadoSucesion,
                            NB_EMPLEADO = vlstEmpleado.Where(w => w.ID_EMPLEADO == vIdEmpleadoSucesion).FirstOrDefault().NB_EMPLEADO.ToString(),
                            NB_PUESTO = vlstEmpleado.Where(w => w.ID_EMPLEADO == vIdEmpleadoSucesion).FirstOrDefault().NB_PUESTO.ToString()
                        };
                        vLstSeleccionados.Add(vLstItem);

                        //btnSeleccionarTodos.Enabled = false;
                        btnEmpleadoFiltro.Enabled = false;
                        //pgridAvanceProgramaCapacitacion.Visible = false;
                        //rpgAvanceUnico.Visible = true;
                        GeneraAvance(true);
                    }
                }
                GeneraAvance(false);
                pgridAvanceProgramaCapacitacion.DataSource = vLstAvancePrograma;

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

        protected void pgridAvanceProgramaCapacitacion_CellDataBound(object sender, Telerik.Web.UI.PivotGridCellDataBoundEventArgs e)
        {
            if (e.Cell is PivotGridRowHeaderCell)
            {
                if (e.Cell.Controls.Count > 1)
                {
                    (e.Cell.Controls[0] as Button).Visible = false;
                }
            }

            if (e.Cell is PivotGridColumnHeaderCell)
            {
                //if (e.Cell.Field.DataField == "CL_PUESTO")
                //{
                //    if (e.Cell.Controls.Count > 1)
                //    {
                //        (e.Cell.Controls[0] as Button).Visible = false;
                //        e.Cell.Text = String.Format("<a href='#' onclick='OpenDescriptivo({1})'>{0}</a>", "CHLL", 1);
                //    }
                //}
                //else
                //{
                if (e.Cell.Controls.Count > 1)
                {
                    (e.Cell.Controls[0] as Button).Visible = false;
                }
                // }
            }

            if (e.Cell is PivotGridDataCell)
            {
                ProgramaNegocio neg = new ProgramaNegocio();
                PivotGridDataCell item = e.Cell as PivotGridDataCell;
                HtmlGenericControl vCtrlDiv = (HtmlGenericControl)item.FindControl("dvPorcentaje");
                if (vCtrlDiv != null)
                {
                    if (item.FormattedValue.Equals("10.00") || item.FormattedValue.ToString().Equals("11.00") || item.FormattedValue.ToString().Equals("12.00"))
                    {
                        // int vIdEmpleado = ObtieneId(item.ParentColumnIndexes[0].ToString());
                        int vIdEmpleado = int.Parse(item.ParentColumnIndexes[1].ToString());
                        int vIdEvento = ObtieneId(item.ParentRowIndexes[5].ToString());
                        int vIdCompetencias = int.Parse(item.ParentRowIndexes[0].ToString());
                        var vLstEvaluado = neg.ObtenerReporteEvaluado(vIdEvento, vIdEmpleado, vIdCompetencias).FirstOrDefault();

                        if (vLstEvaluado != null)
                        {
                            if (vLstEvaluado.PR_EVALUACION_PARTICIPANTE != null)
                                vCtrlDiv.InnerText = String.Format("{0:0.00}%", vLstEvaluado.PR_EVALUACION_PARTICIPANTE);
                            else
                                vCtrlDiv.InnerText = "NC";
                        }
                    }
                }
                else
                    vCtrlDiv.InnerHtml = "<br /><br />&nbsp&nbsp&nbsp<br />";
                //    if (item.FormattedValue == "")
                //    {
                //        item.FormattedValue = "5";
                //    }
            }
        }

        //protected void pgridAvanceProgramaCapacitacion_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
        //{
        //    PivotGridBaseModelCell modelDataCell = e.PivotGridModelCell as PivotGridBaseModelCell;
        //    if (modelDataCell != null)
        //    {

        //        if (modelDataCell.Field.DataField == "NO_COLOR_AVANCE")
        //        {
        //            int vNoAvance = Convert.ToInt32(modelDataCell.Data);
        //            switch (vNoAvance)
        //            {
        //                case 0:
        //                    e.ExportedCell.Style.BackColor = Color.Red;
        //                    e.ExportedCell.Style.ForeColor = Color.Red;
        //                    e.ExportedCell.Style.BorderTopColor = Color.White;
        //                    e.ExportedCell.Style.BorderBottomColor = Color.White;
        //                    e.ExportedCell.Style.BorderLeftColor = Color.White;
        //                    e.ExportedCell.Style.BorderRightColor = Color.White;
        //                    break;
        //                case 2:
        //                    e.ExportedCell.Style.BackColor = Color.LightSkyBlue;
        //                    e.ExportedCell.Style.ForeColor = Color.LightSkyBlue;
        //                    e.ExportedCell.Style.BorderTopColor = Color.White;
        //                    e.ExportedCell.Style.BorderBottomColor = Color.White;
        //                    e.ExportedCell.Style.BorderLeftColor = Color.White;
        //                    e.ExportedCell.Style.BorderRightColor = Color.White;
        //                    break;
        //                case 3:
        //                    e.ExportedCell.Style.BackColor = Color.Orange;
        //                    e.ExportedCell.Style.ForeColor = Color.Orange;
        //                    e.ExportedCell.Style.BorderTopColor = Color.White;
        //                    e.ExportedCell.Style.BorderBottomColor = Color.White;
        //                    e.ExportedCell.Style.BorderLeftColor = Color.White;
        //                    e.ExportedCell.Style.BorderRightColor = Color.White;
        //                    break;
        //                case 5:
        //                    e.ExportedCell.Style.ForeColor = Color.White;
        //                    break;
        //                case 4:
        //                    e.ExportedCell.Style.BackColor = Color.Yellow;
        //                    e.ExportedCell.Style.ForeColor = Color.Yellow;
        //                    e.ExportedCell.Style.BorderTopColor = Color.White;
        //                    e.ExportedCell.Style.BorderBottomColor = Color.White;
        //                    e.ExportedCell.Style.BorderLeftColor = Color.White;
        //                    e.ExportedCell.Style.BorderRightColor = Color.White;
        //                    break;
        //                case 10:
        //                    e.ExportedCell.Style.BackColor = Color.GreenYellow;
        //                    e.ExportedCell.Style.ForeColor = Color.GreenYellow;
        //                    e.ExportedCell.Style.BorderTopColor = Color.White;
        //                    e.ExportedCell.Style.BorderBottomColor = Color.White;
        //                    e.ExportedCell.Style.BorderLeftColor = Color.White;
        //                    e.ExportedCell.Style.BorderRightColor = Color.White;
        //                    break;
        //                case 11:
        //                    e.ExportedCell.Style.BackColor = Color.GreenYellow;
        //                    e.ExportedCell.Style.BorderRightColor = Color.Red;
        //                    e.ExportedCell.Style.ForeColor = Color.GreenYellow;
        //                    e.ExportedCell.Style.BorderRightWidth = new Unit(50);
        //                    e.ExportedCell.Style.BorderRightStyle = BorderStyle.Solid;
        //                    e.ExportedCell.Style.BorderTopColor = Color.White;
        //                    e.ExportedCell.Style.BorderBottomColor = Color.White;
        //                    e.ExportedCell.Style.BorderLeftColor = Color.White;
        //                    break;
        //                case 12:
        //                    e.ExportedCell.Style.BackColor = Color.GreenYellow;
        //                    e.ExportedCell.Style.BorderRightColor = Color.Yellow;
        //                    e.ExportedCell.Style.ForeColor = Color.GreenYellow;
        //                    e.ExportedCell.Style.BorderRightWidth = new Unit(50);
        //                    e.ExportedCell.Style.BorderRightStyle = BorderStyle.Solid;
        //                    e.ExportedCell.Style.BorderTopColor = Color.White;
        //                    e.ExportedCell.Style.BorderBottomColor = Color.White;
        //                    e.ExportedCell.Style.BorderLeftColor = Color.White;
        //                    break;
        //                default:
        //                    break;
        //            }

        //        }
        //        else if (modelDataCell.Field.DataField == "NB_PUESTO")
        //        {
        //            e.ExportedCell.Style.BackColor = Color.Gainsboro;
        //            e.ExportedCell.Style.Font.Bold = true;
        //            e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 300;
        //            e.ExportedCell.Table.Rows[e.ExportedCell.ColIndex].Height = 50;
        //            e.ExportedCell.Style.BorderRightStyle = BorderStyle.Solid;
        //            e.ExportedCell.Style.BorderTopColor = Color.White;
        //            e.ExportedCell.Style.BorderBottomColor = Color.White;
        //            e.ExportedCell.Style.BorderLeftColor = Color.White;
        //            e.ExportedCell.Style.BorderRightColor = Color.White;
        //        }
        //        else
        //        {
        //            e.ExportedCell.Style.BackColor = Color.Gainsboro;
        //            e.ExportedCell.Style.Font.Bold = true;
        //            e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 150;
        //            e.ExportedCell.Table.Rows[e.ExportedCell.ColIndex].Height = 30;
        //            e.ExportedCell.Style.BorderRightStyle = BorderStyle.Solid;
        //            e.ExportedCell.Style.BorderTopColor = Color.White;
        //            e.ExportedCell.Style.BorderBottomColor = Color.White;
        //            e.ExportedCell.Style.BorderLeftColor = Color.White;
        //            e.ExportedCell.Style.BorderRightColor = Color.White;
        //        }

        //        if (modelDataCell.Field.DataField == "NB_EVENTOS_RELACIONADOS" && modelDataCell.Data == "")
        //        {
        //            e.ExportedCell.Style.ForeColor = Color.Gainsboro;
        //            e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 2;
        //            e.ExportedCell.Style.BorderTopColor = Color.White;
        //            e.ExportedCell.Style.BorderBottomColor = Color.White;
        //            e.ExportedCell.Style.BorderLeftColor = Color.White;
        //            e.ExportedCell.Style.BorderRightColor = Color.White;
        //        }

        //    }

        //}

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

        //protected void btnSeleccionarTodos_Click(object sender, EventArgs e)
        //{
        //    GeneraAvance(false);
        //}

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            GenerarExcelAvance();
        }

    }
}