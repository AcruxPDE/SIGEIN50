using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.MPC
{
    public partial class ConsultaTabuladorSueldos : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private int? vIdRol;

        private string vClTipoSeleccion
        {
            get { return (string)ViewState["vs_vClTipoSeleccion"]; }
            set { ViewState["vs_vClTipoSeleccion"] = value; }
        }

        public XElement vXmlTabuladorEmpleado
        {
            get { return XElement.Parse((string)(ViewState["vs_vXmlTabuladorEmpleado"])); }
            set { ViewState["vs_vXmlTabuladorEmpleado"] = value.ToString(); }
        }

        public int vIdTabulador
        {
            get { return (int)ViewState["vsIdTabulador"]; }
            set { ViewState["vsIdTabulador"] = value; }
        }

        public int vCuartilInflacional
        {
            get { return (int)ViewState["vs_vCuartilInflacional"]; }
            set { ViewState["vs_vCuartilInflacional"] = value; }
        }

        public int vCuartilIncremento
        {
            get { return (int)ViewState["vs_vCuartilIncremento"]; }
            set { ViewState["vs_vCuartilIncremento"] = value; }
        }

        public int vCuartilComparativo
        {
            get { return (int)ViewState["vs_vCuartilComparativo"]; }
            set { ViewState["vs_vCuartilComparativo"] = value; }
        }

        private decimal vPrInflacional
        {
            get { return (decimal)ViewState["vs_vInflacional"]; }
            set { ViewState["vs_vInflacional"] = value; }
        }

        private int? vRangoVerde
        {
            get { return (int?)ViewState["vs_vRangoVerde"]; }
            set { ViewState["vs_vRangoVerde"] = value; }
        }

        private int? vRangoAmarillo
        {
            get { return (int?)ViewState["vs_vRangoAmarillo"]; }
            set { ViewState["vs_vRangoAmarillo"] = value; }
        }

        private List<E_CONSULTA_SUELDOS> vObtienePlaneacionIncremento
        {
            get { return (List<E_CONSULTA_SUELDOS>)ViewState["vs_vObtienePlaneacionIncremento"]; }
            set { ViewState["vs_vObtienePlaneacionIncremento"] = value; }
        }

        private List<E_SELECCIONADOS> vSeleccionadosTabuladores
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vSeleccionadosTabuladores"]; }
            set { ViewState["vs_vSeleccionadosTabuladores"] = value; }
        }

        private List<E_EMPLEADOS_GRAFICAS> vLstEmpleados
        {
            get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vLstEmpleados"]; }
            set { ViewState["vs_vLstEmpleados"] = value; }
        }

        private List<E_CONSULTA_SUELDOS> vListaTabuladorSueldos
        {
            get { return (List<E_CONSULTA_SUELDOS>)ViewState["vs_vListaTabuladorSueldos"]; }
            set { ViewState["vs_vListaTabuladorSueldos"] = value; }
        }

        private List<E_CONSULTA_SUELDOS> vLstSeleccionadosTabuladorSueldos
        {
            get { return (List<E_CONSULTA_SUELDOS>)ViewState["vs_vLstSeleccionadosTabuladorSueldos"]; }
            set { ViewState["vs_vLstSeleccionadosTabuladorSueldos"] = value; }
        }

        #endregion

        #region Funciones

        protected void GenerarHeaderGroup()
        {
            switch (rcbMercadoTabuladorSueldos.SelectedValue)
            {
                case "1":
                    rgdComparacionInventarioPersonal.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Mínimo";
                    break;
                case "2":
                    rgdComparacionInventarioPersonal.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Primer Cuartil";
                    break;
                case "3":
                    rgdComparacionInventarioPersonal.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Medio";
                    break;
                case "4":
                    rgdComparacionInventarioPersonal.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Tercer Cuartil";
                    break;
                case "5":
                    rgdComparacionInventarioPersonal.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Máximo";
                    break;
                default:
                    rgdComparacionInventarioPersonal.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Medio";
                    break;
            }
        }

        public XElement vTipoDeSeleccion(string pTipoSeleccion)
        {
            XElement vXmlSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", vClTipoSeleccion)));
            switch (pTipoSeleccion)
            {
                case "TODOS":
                    break;
                case "CONSULTAS":
                    XElement vXmlClPuesto = new XElement("TIPO", new XAttribute("ID_TABULADOR", vIdTabulador));
                    vXmlSeleccion.Element("FILTRO").Add(vXmlClPuesto);
                    break;
            }
            return vXmlSeleccion;
        }

        private void ObtenerPlaneacionIncrementos()
        {
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();

            vObtienePlaneacionIncremento = nTabuladores.ObtenerConsultaSueldos(ID_TABULADOR: vIdTabulador, ID_ROL: vIdRol).Select(s => new E_CONSULTA_SUELDOS()
            {
                NUM_ITEM = (int?)s.NUM_RENGLON,
                ID_TABULADOR_EMPLEADO = (int?)s.ID_TABULADOR_EMPLEADO,
                NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
                CL_PUESTO = s.CL_PUESTO,
                NB_PUESTO = s.NB_PUESTO,
                CL_DEPARTAMENTO = s.CL_DEPARTAMENTO,
                NB_DEPARTAMENTO = s.NB_DEPARTAMENTO,
                CL_EMPLEADO = s.CL_EMPLEADO,
                NB_EMPLEADO = s.NOMBRE,
                MN_MINIMO_MINIMO = s.MN_MINIMO_MINIMO,
                MN_MAXIMO_MINIMO = s.MN_MAXIMO_MINIMO,
                MN_MINIMO_PRIMER_CUARTIL = s.MN_MINIMO_PRIMER_CUARTIL,
                MN_MAXIMO_PRIMER_CUARTIL = s.MN_MAXIMO_PRIMER_CUARTIL,
                MN_MINIMO_MEDIO = s.MN_MINIMO_MEDIO,
                MN_MAXIMO_MEDIO = s.MN_MAXIMO_MEDIO,
                MN_MINIMO_SEGUNDO_CUARTIL = s.MN_MINIMO_SEGUNDO_CUARTIL,
                MN_MAXIMO_SEGUNDO_CUARTIL = s.MN_MAXIMO_SEGUNDO_CUARTIL,
                MN_MAXIMO_MAXIMO = s.MN_MAXIMO_MAXIMO,
                MN_MINIMO_MAXIMO = s.MN_MINIMO_MAXIMO,
                MN_SUELDO_ORIGINAL = s.MN_SUELDO_ORIGINAL,
                MN_SUELDO_NUEVO = s.MN_SUELDO_NUEVO,
                MN_MINIMO = s.MN_MINIMO,
                MN_MAXIMO = s.MN_MAXIMO,
                INCREMENTO = s.MN_SUELDO_NUEVO == null && s.MN_SUELDO_ORIGINAL == null ? 0 : Math.Abs((decimal)s.MN_SUELDO_NUEVO - (decimal)s.MN_SUELDO_ORIGINAL),
                NO_NIVEL = s.NO_NIVEL,
                XML_CATEGORIAS = s.XML_CATEGORIA,
                CUARTIL_SELECCIONADO = vCuartilComparativo,
                NO_VALUACION = s.NO_VALUACION,
                FG_SUELDO_VISIBLE_TABULADOR = s.FG_SUELDO_VISIBLE_TABULADOR
            }).ToList();
            foreach (E_CONSULTA_SUELDOS item in vObtienePlaneacionIncremento)
            {
                if (item.MN_SUELDO_ORIGINAL != 0)
                    item.PR_INCREMENTO = (item.INCREMENTO / item.MN_SUELDO_ORIGINAL) * 100;
            }
        }

        private void RecalcularConsulta()
        {
            if (vLstSeleccionadosTabuladorSueldos.Count() > 0)
            {
                ActualizarLista(int.Parse(rcbMercadoTabuladorSueldos.SelectedValue));
                var vLstSeleccionados = vLstSeleccionadosTabuladorSueldos;
                vLstSeleccionadosTabuladorSueldos = new List<E_CONSULTA_SUELDOS>();
                int vNumeroItem = 1;
                foreach (var item in vLstSeleccionados)
                {
                    if (item.NO_NIVEL >= int.Parse(rntComienzaNivel.Text) & item.NO_NIVEL <= int.Parse(rntTerminaSueldo.Text))
                    {
                        if (vLstSeleccionadosTabuladorSueldos.Where(w => w.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO).Count() == 0)
                        {
                            vLstSeleccionadosTabuladorSueldos.Add(new E_CONSULTA_SUELDOS
                            {
                                NUM_ITEM = vNumeroItem,
                                ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO,
                                NB_TABULADOR_NIVEL = item.NB_TABULADOR_NIVEL,
                                CL_PUESTO = item.CL_PUESTO,
                                NB_PUESTO = item.NB_PUESTO,
                                CL_DEPARTAMENTO = item.CL_DEPARTAMENTO,
                                NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
                                CL_EMPLEADO = item.CL_EMPLEADO,
                                NB_EMPLEADO = item.NB_EMPLEADO,
                                MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL,
                                MN_SUELDO_NUEVO = item.MN_SUELDO_NUEVO,
                                NO_NIVEL = item.NO_NIVEL,
                                XML_CATEGORIAS = item.XML_CATEGORIAS,
                                DIFERENCIA = item.DIFERENCIA,
                                PR_DIFERENCIA = item.PR_DIFERENCIA,
                                COLOR_DIFERENCIA = item.COLOR_DIFERENCIA,
                                ICONO = item.ICONO,
                                NO_VALUACION = item.NO_VALUACION,
                                lstCategorias = SeleccionCuartil(XElement.Parse(item.XML_CATEGORIAS), item.ID_TABULADOR_EMPLEADO),
                                FG_SUELDO_VISIBLE_TABULADOR = item.FG_SUELDO_VISIBLE_TABULADOR

                            });

                            vNumeroItem++;
                        }
                    }
                }
                rgdComparacionInventarioPersonal.Rebind();

            }
            else
            {
                ActualizarLista(int.Parse(rcbMercadoTabuladorSueldos.SelectedValue));
                var vLstEmpleados = vListaTabuladorSueldos;
                vListaTabuladorSueldos = new List<E_CONSULTA_SUELDOS>();
                int vNumeroItem = 1;
                foreach (var item in vLstEmpleados)
                {
                    if (item.NO_NIVEL >= int.Parse(rntComienzaNivel.Text) & item.NO_NIVEL <= int.Parse(rntTerminaSueldo.Text))
                    {
                        if (vListaTabuladorSueldos.Where(w => w.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO).Count() == 0)
                        {
                            vListaTabuladorSueldos.Add(new E_CONSULTA_SUELDOS
                            {
                                NUM_ITEM = vNumeroItem,
                                ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO,
                                NB_TABULADOR_NIVEL = item.NB_TABULADOR_NIVEL,
                                CL_PUESTO = item.CL_PUESTO,
                                NB_PUESTO = item.NB_PUESTO,
                                CL_DEPARTAMENTO = item.CL_DEPARTAMENTO,
                                NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
                                CL_EMPLEADO = item.CL_EMPLEADO,
                                NB_EMPLEADO = item.NB_EMPLEADO,
                                MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL,
                                MN_SUELDO_NUEVO = item.MN_SUELDO_NUEVO,
                                NO_NIVEL = item.NO_NIVEL,
                                XML_CATEGORIAS = item.XML_CATEGORIAS,
                                DIFERENCIA = item.DIFERENCIA,
                                PR_DIFERENCIA = item.PR_DIFERENCIA,
                                COLOR_DIFERENCIA = item.COLOR_DIFERENCIA,
                                ICONO = item.ICONO,
                                NO_VALUACION = item.NO_VALUACION,
                                lstCategorias = SeleccionCuartil(XElement.Parse(item.XML_CATEGORIAS), item.ID_TABULADOR_EMPLEADO),
                                FG_SUELDO_VISIBLE_TABULADOR = item.FG_SUELDO_VISIBLE_TABULADOR

                            });

                            vNumeroItem++;
                        }
                    }
                }
                rgdComparacionInventarioPersonal.Rebind();
            }
        }

        protected void CargarDatosTabuladorSueldos(List<int> pIdsSeleccionados)
        {
            ActualizarLista(int.Parse(rcbMercadoTabuladorSueldos.SelectedValue));
            var vEmpleadosTabuladorSeleccionados = vObtienePlaneacionIncremento.Where(w => pIdsSeleccionados.Contains(w.ID_TABULADOR_EMPLEADO == null ? 0 : (int)w.ID_TABULADOR_EMPLEADO)).ToList();
            foreach (var item in vEmpleadosTabuladorSeleccionados)
            {
                if (item.NO_NIVEL >= int.Parse(rntComienzaNivel.Text) & item.NO_NIVEL <= int.Parse(rntTerminaSueldo.Text))
                {
                    if (vListaTabuladorSueldos.Where(w => w.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO).Count() == 0)
                    {
                        vListaTabuladorSueldos.Add(new E_CONSULTA_SUELDOS
                        {
                            NUM_ITEM = item.NUM_ITEM,
                            ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO,
                            NB_TABULADOR_NIVEL = item.NB_TABULADOR_NIVEL,
                            CL_PUESTO = item.CL_PUESTO,
                            NB_PUESTO = item.NB_PUESTO,
                            CL_DEPARTAMENTO = item.CL_DEPARTAMENTO,
                            NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
                            CL_EMPLEADO = item.CL_EMPLEADO,
                            NB_EMPLEADO = item.NB_EMPLEADO,
                            MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL,
                            MN_SUELDO_NUEVO = item.MN_SUELDO_NUEVO,
                            NO_NIVEL = item.NO_NIVEL,
                            XML_CATEGORIAS = item.XML_CATEGORIAS,
                            DIFERENCIA = item.DIFERENCIA,
                            PR_DIFERENCIA = item.PR_DIFERENCIA,
                            COLOR_DIFERENCIA = item.COLOR_DIFERENCIA,
                            ICONO = item.ICONO,
                            NO_VALUACION = item.NO_VALUACION,
                            lstCategorias = SeleccionCuartil(XElement.Parse(item.XML_CATEGORIAS), item.ID_TABULADOR_EMPLEADO),
                            FG_SUELDO_VISIBLE_TABULADOR = item.FG_SUELDO_VISIBLE_TABULADOR
                        });
                    }
                }
            }
            //  var vc = vListaTabuladorSueldos;

            rgdComparacionInventarioPersonal.Rebind();
        }

        protected void CargarDatosSeleccionadosSueldos(List<int> pIdsSeleccionados)
        {
            ActualizarLista(int.Parse(rcbMercadoTabuladorSueldos.SelectedValue));
            int vNumeroItem = 1;
            var vEmpleadosTabuladorSeleccionados = vObtienePlaneacionIncremento.Where(w => pIdsSeleccionados.Contains(w.ID_TABULADOR_EMPLEADO == null ? 0 : (int)w.ID_TABULADOR_EMPLEADO)).ToList();
            foreach (var item in vEmpleadosTabuladorSeleccionados)
            {
                if (item.NO_NIVEL >= int.Parse(rntComienzaNivel.Text) & item.NO_NIVEL <= int.Parse(rntTerminaSueldo.Text))
                {
                    if (vLstSeleccionadosTabuladorSueldos.Where(w => w.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO).Count() == 0)
                    {
                        vLstSeleccionadosTabuladorSueldos.Add(new E_CONSULTA_SUELDOS
                        {
                            NUM_ITEM = vNumeroItem,
                            ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO,
                            NB_TABULADOR_NIVEL = item.NB_TABULADOR_NIVEL,
                            CL_PUESTO = item.CL_PUESTO,
                            NB_PUESTO = item.NB_PUESTO,
                            CL_DEPARTAMENTO = item.CL_DEPARTAMENTO,
                            NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
                            CL_EMPLEADO = item.CL_EMPLEADO,
                            NB_EMPLEADO = item.NB_EMPLEADO,
                            MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL,
                            MN_SUELDO_NUEVO = item.MN_SUELDO_NUEVO,
                            NO_NIVEL = item.NO_NIVEL,
                            XML_CATEGORIAS = item.XML_CATEGORIAS,
                            DIFERENCIA = item.DIFERENCIA,
                            PR_DIFERENCIA = item.PR_DIFERENCIA,
                            COLOR_DIFERENCIA = item.COLOR_DIFERENCIA,
                            ICONO = item.ICONO,
                            NO_VALUACION = item.NO_VALUACION,
                            lstCategorias = SeleccionCuartil(XElement.Parse(item.XML_CATEGORIAS), item.ID_TABULADOR_EMPLEADO),
                            FG_SUELDO_VISIBLE_TABULADOR = item.FG_SUELDO_VISIBLE_TABULADOR

                        });

                        vNumeroItem++;
                    }
                }
            }
            // var vc = vLstSeleccionadosTabuladorSueldos;
            GenerarHeaderGroup();
            rgEmpleadosTabuladorSueldos.Rebind();
            rgdComparacionInventarioPersonal.Rebind();
        }

        protected decimal? CalculaCantidadCuartil(int pMnSeleccionado, decimal? pMnMinimo, decimal? pMnPrimerCuartil, decimal? pMnMedio, decimal? pMnSegundoCuartil, decimal? pMnMaximo)
        {
            decimal? vCantidad = 0;
            switch (pMnSeleccionado)
            {
                case 1: vCantidad = pMnMinimo;
                    break;
                case 2: vCantidad = pMnPrimerCuartil;
                    break;
                case 3: vCantidad = pMnMedio;
                    break;
                case 4: vCantidad = pMnSegundoCuartil;
                    break;
                case 5: vCantidad = pMnMaximo;
                    break;
            }
            return vCantidad;
        }

        protected DataTable CrearDataTableSelecion()
        {

            DataTable vDtPivot = new DataTable();

            vDtPivot.Columns.Add("ID_TABULADOR_EMPLEADO", typeof(int));
            vDtPivot.Columns.Add("NO_NIVEL", typeof(int));
            vDtPivot.Columns.Add("NO", typeof(string));
            vDtPivot.Columns.Add("NB_PUESTO", typeof(string));
            vDtPivot.Columns.Add("NB_DEPARTAMENTO", typeof(string));
            vDtPivot.Columns.Add("NB_EMPLEADO", typeof(string));
            vDtPivot.Columns.Add("MN_SUELDO_ORIGINAL", typeof(string));
            //vDtPivot.Columns.Add("DIFERENCIA", typeof(string));
            vDtPivot.Columns.Add("PR_DIFERENCIA", typeof(string));
            vDtPivot.Columns.Add("NO_VALUACION", typeof(string));

            var vLstEmpleados = (from a in vLstSeleccionadosTabuladorSueldos select new { a.ID_TABULADOR_EMPLEADO, a.lstCategorias, a.NO_NIVEL }).Distinct().OrderBy(o => o.NO_NIVEL);
            var vLstCategorias = (from a in vLstSeleccionadosTabuladorSueldos
                                  select new
                                  {
                                      a.NUM_ITEM,
                                      a.ID_TABULADOR_EMPLEADO,
                                      a.NB_TABULADOR_NIVEL,
                                      a.NB_PUESTO,
                                      a.NB_DEPARTAMENTO,
                                      a.NB_EMPLEADO,
                                      a.FG_SUELDO_VISIBLE_TABULADOR,
                                      a.MN_SUELDO_ORIGINAL,
                                      a.DIFERENCIA,
                                      a.PR_DIFERENCIA,
                                      a.NO_VALUACION,
                                      a.COLOR_DIFERENCIA,
                                      a.ICONO,
                                      a.NO_NIVEL
                                  }).Distinct().OrderBy(t => t.NO_NIVEL);

            if (vLstEmpleados.Count() > 0)
            {
                var vCategorias = vLstEmpleados.Where(w => w.ID_TABULADOR_EMPLEADO > 0).FirstOrDefault().lstCategorias;

                if (vCategorias != null)
                {
                    foreach (var item in vCategorias.OrderBy(w => w.NO_CATEGORIA))
                    {
                        vDtPivot.Columns.Add(item.NO_CATEGORIA.ToString() + "E");
                    }
                }
            }


            foreach (var vCate in vLstCategorias)
            {
                DataRow vDr = vDtPivot.NewRow();

                vDr["ID_TABULADOR_EMPLEADO"] = vCate.ID_TABULADOR_EMPLEADO == null ? 0 : vCate.ID_TABULADOR_EMPLEADO;
                vDr["NO_NIVEL"] = vCate.NO_NIVEL;
                vDr["NO"] = vCate.NUM_ITEM;
                vDr["NB_PUESTO"] = vCate.NB_PUESTO;
                vDr["NB_DEPARTAMENTO"] = vCate.NB_DEPARTAMENTO;
                vDr["NB_EMPLEADO"] = vCate.NB_EMPLEADO;

                if (vCate.FG_SUELDO_VISIBLE_TABULADOR == true)
                    vDr["MN_SUELDO_ORIGINAL"] = String.Format("{0:C}", vCate.MN_SUELDO_ORIGINAL);
                else
                    vDr["MN_SUELDO_ORIGINAL"] = "";

                if (vCate.DIFERENCIA == null)
                {
                    //vDr["DIFERENCIA"] = "";
                    vDr["PR_DIFERENCIA"] = "";
                }
                else
                {
                    //vDr["DIFERENCIA"] = String.Format("{0:C}", Math.Abs((decimal)vCate.DIFERENCIA));
                    vDr["PR_DIFERENCIA"] = String.Format("{0:N2}", Math.Abs(vCate.PR_DIFERENCIA == null ? 0 : (decimal)vCate.PR_DIFERENCIA) > 100 ? 100 : Math.Abs(vCate.PR_DIFERENCIA == null ? 0 : (decimal)vCate.PR_DIFERENCIA)) + "%"
                   + "<span style=\"border: 1px solid gray; border-radius: 5px; background:" + vCate.COLOR_DIFERENCIA + ";\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<img src='../Assets/images/Icons/25/Arrow" + vCate.ICONO + ".png' />";
                }
                vDr["NO_VALUACION"] = vCate.NO_VALUACION == null ? "" : vCate.NO_VALUACION.ToString();

                var vResultado = vLstSeleccionadosTabuladorSueldos.Where(t => t.ID_TABULADOR_EMPLEADO == vCate.ID_TABULADOR_EMPLEADO).FirstOrDefault();
                if (vResultado != null)
                {
                    foreach (var item in vResultado.lstCategorias)
                    {
                        vDr[item.NO_CATEGORIA.ToString() + "E"] = String.Format("{0:C}", item.CANTIDAD);
                    }
                }
                vDtPivot.Rows.Add(vDr);
            }
            return vDtPivot;
        }

        protected DataTable CrearDataTable()
        {

            DataTable vDtPivot = new DataTable();

            vDtPivot.Columns.Add("ID_TABULADOR_EMPLEADO", typeof(int));
            vDtPivot.Columns.Add("NO_NIVEL", typeof(int));
            vDtPivot.Columns.Add("NO", typeof(string));
            vDtPivot.Columns.Add("NB_PUESTO", typeof(string));
            vDtPivot.Columns.Add("NB_DEPARTAMENTO", typeof(string));
            vDtPivot.Columns.Add("NB_EMPLEADO", typeof(string));
            vDtPivot.Columns.Add("MN_SUELDO_ORIGINAL", typeof(string));
            //vDtPivot.Columns.Add("DIFERENCIA", typeof(string));
            vDtPivot.Columns.Add("PR_DIFERENCIA", typeof(string));
            vDtPivot.Columns.Add("NO_VALUACION", typeof(string));

            var vLstEmpleados = (from a in vListaTabuladorSueldos select new { a.ID_TABULADOR_EMPLEADO, a.lstCategorias, a.NO_NIVEL }).Distinct().OrderBy(o => o.NO_NIVEL);
            var vLstCategorias = (from a in vListaTabuladorSueldos
                                  select new
                                  {
                                      a.NUM_ITEM,
                                      a.ID_TABULADOR_EMPLEADO,
                                      a.NB_TABULADOR_NIVEL,
                                      a.NB_PUESTO,
                                      a.NB_DEPARTAMENTO,
                                      a.NB_EMPLEADO,
                                      a.FG_SUELDO_VISIBLE_TABULADOR,
                                      a.MN_SUELDO_ORIGINAL,
                                      a.DIFERENCIA,
                                      a.PR_DIFERENCIA,
                                      a.NO_VALUACION,
                                      a.COLOR_DIFERENCIA,
                                      a.ICONO,
                                      a.NO_NIVEL
                                  }).Distinct().OrderBy(t => t.NO_NIVEL);

            if (vLstEmpleados.Count() > 0)
            {
                var vCategorias = vLstEmpleados.Where(w => w.ID_TABULADOR_EMPLEADO > 0).FirstOrDefault().lstCategorias;

                if (vCategorias != null)
                {
                    foreach (var item in vCategorias.OrderBy(w => w.NO_CATEGORIA))
                    {
                        vDtPivot.Columns.Add(item.NO_CATEGORIA.ToString() + "E");
                    }
                }
            }


            foreach (var vCate in vLstCategorias)
            {
                DataRow vDr = vDtPivot.NewRow();

                vDr["ID_TABULADOR_EMPLEADO"] = vCate.ID_TABULADOR_EMPLEADO == null ? 0 : vCate.ID_TABULADOR_EMPLEADO;
                vDr["NO_NIVEL"] = vCate.NO_NIVEL;
                vDr["NO"] = vCate.NUM_ITEM;
                vDr["NB_PUESTO"] = vCate.NB_PUESTO;
                vDr["NB_DEPARTAMENTO"] = vCate.NB_DEPARTAMENTO;
                vDr["NB_EMPLEADO"] = vCate.NB_EMPLEADO;


                if (vCate.FG_SUELDO_VISIBLE_TABULADOR == true)
                    vDr["MN_SUELDO_ORIGINAL"] = String.Format("{0:C}", vCate.MN_SUELDO_ORIGINAL);
                else
                    vDr["MN_SUELDO_ORIGINAL"] = "";

                if (vCate.DIFERENCIA == null)
                {
                    //vDr["DIFERENCIA"] = "";
                    vDr["PR_DIFERENCIA"] = "";
                }
                else
                {
                    //vDr["DIFERENCIA"] = String.Format("{0:C}", Math.Abs((decimal)vCate.DIFERENCIA));
                    vDr["PR_DIFERENCIA"] = String.Format("{0:N2}", Math.Abs(vCate.PR_DIFERENCIA == null ? 0 : (decimal)vCate.PR_DIFERENCIA) > 100 ? 100 : Math.Abs(vCate.PR_DIFERENCIA == null ? 0 : (decimal)vCate.PR_DIFERENCIA)) + "%"
                   + "<span style=\"border: 1px solid gray; border-radius: 5px; background:" + vCate.COLOR_DIFERENCIA + ";\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<img src='../Assets/images/Icons/25/Arrow" + vCate.ICONO + ".png' />";
                }
                vDr["NO_VALUACION"] = vCate.NO_VALUACION == null ? "" : vCate.NO_VALUACION.ToString();

                var vResultado = vListaTabuladorSueldos.Where(t => t.ID_TABULADOR_EMPLEADO == vCate.ID_TABULADOR_EMPLEADO).FirstOrDefault();
                if (vResultado != null)
                {
                    foreach (var item in vResultado.lstCategorias)
                    {
                        vDr[item.NO_CATEGORIA.ToString() + "E"] = String.Format("{0:C}", item.CANTIDAD);
                    }
                }
                vDtPivot.Rows.Add(vDr);
            }
            return vDtPivot;
        }

        protected void ActualizarLista(int pCuartilComparativo)
        {
            foreach (E_CONSULTA_SUELDOS item in vObtienePlaneacionIncremento)
            {
                item.MN_MINIMO_CUARTIL = CalculaMinimo(pCuartilComparativo, item.MN_MINIMO_MINIMO, item.MN_MINIMO_PRIMER_CUARTIL, item.MN_MINIMO_MEDIO, item.MN_MINIMO_SEGUNDO_CUARTIL, item.MN_MINIMO_MAXIMO);
                item.MN_MAXIMO_CUARTIL = CalculaMaximo(pCuartilComparativo, item.MN_MAXIMO_MINIMO, item.MN_MAXIMO_PRIMER_CUARTIL, item.MN_MAXIMO_MEDIO, item.MN_MAXIMO_SEGUNDO_CUARTIL, item.MN_MAXIMO_MAXIMO);
                item.DIFERENCIA = CalculoDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_ORIGINAL);
                if (item.MN_SUELDO_ORIGINAL > 0)
                    // item.PR_DIFERENCIA = (item.DIFERENCIA / item.MN_SUELDO_ORIGINAL) * 100;
                    item.PR_DIFERENCIA = CalculoPrDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_ORIGINAL);
                else item.PR_DIFERENCIA = 0;
                item.COLOR_DIFERENCIA = VariacionColor(item.PR_DIFERENCIA, item.MN_SUELDO_ORIGINAL);
                item.ICONO = ObtenerIconoDiferencia(item.PR_DIFERENCIA, item.MN_SUELDO_ORIGINAL);
            }
        }

        protected decimal? CalculoPrDiferencia(decimal? pMnMinimo, decimal? pMnMaximo, decimal? pMnSueldo) // SE CREA NUEVO METODO PARA CALCULAR EL PR DIFERENCIA 30/04/2018 
        {
            decimal? vMnDivisor = 0;
            if (pMnSueldo > 0)
            {

                if (pMnSueldo < pMnMinimo)
                    vMnDivisor = pMnMinimo;
                else
                    if (pMnSueldo > pMnMaximo)
                        vMnDivisor = pMnMaximo;
                    else
                        vMnDivisor = pMnSueldo;

                if (vMnDivisor > 0)
                vMnDivisor = (((pMnSueldo * 100) / vMnDivisor) - 100);
            }
            return vMnDivisor;
        }
       
        protected decimal? CalculoDiferencia(decimal? pMnMinimo, decimal? pMnMaximo, decimal? pMnSueldo)
        {
            decimal? vMnDivisor = 0;
            if (pMnSueldo < pMnMinimo)
                vMnDivisor = pMnMinimo;
            else
                if (pMnSueldo > pMnMaximo)
                    vMnDivisor = pMnMaximo;
                else
                    vMnDivisor = pMnSueldo;
            return vMnDivisor = pMnSueldo - vMnDivisor;
        }

        protected decimal? CalculaMinimo(int pMnSeleccionado, decimal? pMnMinimo, decimal? pMnPrimerCuartil, decimal? pMnMedio, decimal? pMnSegundoCuartil, decimal? pMnMaximo)
        {
            decimal? vMinimo = 0;
            switch (pMnSeleccionado)
            {
                case 1: vMinimo = pMnMinimo;
                    break;
                case 2: vMinimo = pMnPrimerCuartil;
                    break;
                case 3: vMinimo = pMnMedio;
                    break;
                case 4: vMinimo = pMnSegundoCuartil;
                    break;
                case 5: vMinimo = pMnMaximo;
                    break;
            }
            return vMinimo;
        }

        protected decimal? CalculaMaximo(int pMnSeleccionado, decimal? pMnMinimo, decimal? pMnPrimerCuartil, decimal? pMnMedio, decimal? pMnSegundoCuartil, decimal? pMnMaximo)
        {
            decimal? vMaximo = 0;
            switch (pMnSeleccionado)
            {
                case 1: vMaximo = pMnMinimo;
                    break;
                case 2: vMaximo = pMnPrimerCuartil;
                    break;
                case 3: vMaximo = pMnMedio;
                    break;
                case 4: vMaximo = pMnSegundoCuartil;
                    break;
                case 5: vMaximo = pMnMaximo;
                    break;
            }
            return vMaximo;
        }

        protected string ObtenerIconoDiferencia(decimal? pPrDiferencia, decimal? pMnSueldo)
        {
            //return pPrDiferencia < 0 ? "Down" : pPrDiferencia > 0 ? "Up" : "Equal";
            string vImagen = null;
            if (pPrDiferencia < 0 & pMnSueldo != 0)
                vImagen = "Down";
            else if (pPrDiferencia > 0 & pMnSueldo != 0)
                vImagen = "Up";
            else if (pPrDiferencia == 0 & pMnSueldo == 0)
                vImagen = "Delete";
            else vImagen = "Equal";

            return vImagen;
        }

        protected string VariacionColor(decimal? pPrDireferencia, decimal? pMnSueldo)
        {
            string vColor;
            if (pPrDireferencia == null)
                pPrDireferencia = 0;

            decimal vPorcentaje = Math.Abs((decimal)pPrDireferencia);

            if (vPorcentaje >= 0 && vPorcentaje <= vRangoVerde & pMnSueldo > 0)
                vColor = "green";
            else if (vPorcentaje > vRangoVerde && vPorcentaje <= vRangoAmarillo & pMnSueldo > 0)
                vColor = "yellow";
            else if (pPrDireferencia == 0 & pMnSueldo == 0)
                vColor = "gray";
            else
                vColor = "red";
            return vColor;
        }

        private void ConfigurarColumna(GridColumn pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pAlinear)
        {
            if (pGenerarEncabezado)
            {
                pEncabezado = GeneraEncabezado(pColumna);
                pColumna.ColumnGroupName = "TABMEDIO";
                pColumna.ItemStyle.Font.Bold = true;
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

        private string GeneraEncabezado(GridColumn pColumna)
        {
            int vResultado;
            string vEncabezado = "";
            string vEmpleado = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));

            if (int.TryParse(vEmpleado, out vResultado))
            {
                var vDatosEmpleado = vListaTabuladorSueldos.Where(w => w.ID_TABULADOR_EMPLEADO > 0).FirstOrDefault().lstCategorias.Where(w => w.NO_CATEGORIA == vResultado);
                if (vDatosEmpleado != null)
                {
                    vEncabezado = "<div style=\"text-align:center;\"> " + (char)(vDatosEmpleado.Select(s => s.NO_CATEGORIA).FirstOrDefault() + 64) + "</div>";
                }
            }
            return vEncabezado;
        }

        protected void CargarEmpleados()
        {

            vClTipoSeleccion = "CONSULTAS";
            vXmlTabuladorEmpleado = vTipoDeSeleccion(vClTipoSeleccion);
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
            var vLstEmpleados = nTabuladores.ObtenieneEmpleadosTabulador(XML_SELECCIONADOS: vXmlTabuladorEmpleado, ID_EMPRESA: ContextoUsuario.oUsuario.ID_EMPRESA).Select(s => s.ID_TABULADOR_EMPLEADO).ToList();
            ActualizarLista(int.Parse(rcbMercadoTabuladorSueldos.SelectedValue));
            var vEmpleadosTabuladorSeleccionados = vObtienePlaneacionIncremento;
            foreach (var item in vEmpleadosTabuladorSeleccionados)
            {
                if (item.NO_NIVEL >= int.Parse(rntComienzaNivel.Text) & item.NO_NIVEL <= int.Parse(rntTerminaSueldo.Text))
                {
                    if (vListaTabuladorSueldos.Where(w => w.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO && w.NO_NIVEL == item.NO_NIVEL).Count() == 0)
                    {
                        vListaTabuladorSueldos.Add(new E_CONSULTA_SUELDOS
                        {
                            NUM_ITEM = item.NUM_ITEM,
                            ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO,
                            NB_TABULADOR_NIVEL = item.NB_TABULADOR_NIVEL,
                            CL_PUESTO = item.CL_PUESTO,
                            NB_PUESTO = item.NB_PUESTO,
                            CL_DEPARTAMENTO = item.CL_DEPARTAMENTO,
                            NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
                            CL_EMPLEADO = item.CL_EMPLEADO,
                            NB_EMPLEADO = item.NB_EMPLEADO,
                            MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL,
                            MN_SUELDO_NUEVO = item.MN_SUELDO_NUEVO,
                            NO_NIVEL = item.NO_NIVEL,
                            XML_CATEGORIAS = item.XML_CATEGORIAS,
                            DIFERENCIA = item.DIFERENCIA,
                            PR_DIFERENCIA = item.PR_DIFERENCIA,
                            COLOR_DIFERENCIA = item.COLOR_DIFERENCIA,
                            ICONO = item.ICONO,
                            NO_VALUACION = item.NO_VALUACION,
                            lstCategorias = SeleccionCuartil(XElement.Parse(item.XML_CATEGORIAS == null ? "<ITEMS/>" : item.XML_CATEGORIAS), item.ID_TABULADOR_EMPLEADO),
                            FG_SUELDO_VISIBLE_TABULADOR = item.FG_SUELDO_VISIBLE_TABULADOR

                        });
                    }
                }
            }
            var vc = vListaTabuladorSueldos;
            rgEmpleadosTabuladorSueldos.Rebind();
            rgdComparacionInventarioPersonal.Rebind();
        }

        protected void EliminarEmpleadoSueldos(int pIdTabuladorEmpleado)
        {

            E_CONSULTA_SUELDOS vEmpleado = vListaTabuladorSueldos.Where(w => w.ID_TABULADOR_EMPLEADO == pIdTabuladorEmpleado).FirstOrDefault();

            if (vEmpleado != null)
            {
                vListaTabuladorSueldos.Remove(vEmpleado);
            }

            rgdComparacionInventarioPersonal.Rebind();
        }

        protected List<E_CATEGORIA> SeleccionCuartil(XElement xlmCuartiles, int? ID_TABULADOR_EMPLEADO)
        {
            List<E_CATEGORIA> lstCategoria = new List<E_CATEGORIA>();

            foreach (XElement vXmlSecuencia in xlmCuartiles.Elements("ITEM"))
            {
                lstCategoria.Add(new E_CATEGORIA
                {
                    ID_TABULADOR_EMPLEADO = ID_TABULADOR_EMPLEADO,
                    NO_CATEGORIA = UtilXML.ValorAtributo<int>(vXmlSecuencia.Attribute("NO_CATEGORIA")),
                    MN_MINIMO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MINIMO")),
                    MN_PRIMER_CUARTIL = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_PRIMER_CUARTIL")),
                    MN_MEDIO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MEDIO")),
                    MN_SEGUNDO_CUARTIL = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_SEGUNDO_CUARTIL")),
                    MN_MAXIMO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MAXIMO"))
                });
            }

            foreach (var item in lstCategoria)
            {
                item.CANTIDAD = CalculaCantidadCuartil(int.Parse(rcbMercadoTabuladorSueldos.SelectedValue), item.MN_MINIMO, item.MN_PRIMER_CUARTIL, item.MN_MEDIO, item.MN_SEGUNDO_CUARTIL, item.MN_MAXIMO);
            }

            return lstCategoria;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!IsPostBack)
            {
                TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
                SPE_OBTIENE_TABULADORES_Result vCuartiles = nTabuladores.ObtenerTabuladores().FirstOrDefault();
                XElement vXlmCuartiles = XElement.Parse(vCuartiles.XML_VW_CUARTILES);
                var vCuartilesTabulador = vXlmCuartiles.Elements("ITEM").Select(x => new E_CUARTILES
                {
                    ID_CUARTIL = UtilXML.ValorAtributo<int>(x.Attribute("NB_VALOR")),
                    NB_CUARTIL = UtilXML.ValorAtributo<string>(x.Attribute("NB_TEXTO")),
                }).ToList();

                rcbMercadoTabuladorSueldos.DataSource = vCuartilesTabulador;
                rcbMercadoTabuladorSueldos.DataTextField = "NB_CUARTIL";
                rcbMercadoTabuladorSueldos.DataValueField = "ID_CUARTIL".ToString();
                rcbMercadoTabuladorSueldos.DataBind();

                if (Request.QueryString["ID"] != null)
                {
                    vIdTabulador = int.Parse((Request.QueryString["ID"]));

                    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
                    txtClaveTabulador.InnerText = vTabulador.CL_TABULADOR;
                    txtDescripción.InnerText = vTabulador.DS_TABULADOR;
                    txtNbTabulador.InnerText = vTabulador.NB_TABULADOR;
                    txtVigencia.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtFecha.InnerText = vTabulador.FE_CREACION.ToString("dd/MM/yyyy");
                    txtPuestos.InnerText = vTabulador.CL_TIPO_PUESTO;

                    vPrInflacional = vTabulador.PR_INFLACION;
                    if (vTabulador.XML_VARIACION != null)
                    {
                        XElement vXlmVariacion = XElement.Parse(vTabulador.XML_VARIACION);
                        foreach (XElement vXmlVaria in vXlmVariacion.Elements("Rango"))
                            if ((UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("COLOR")).Equals("green")))
                            {
                                vRangoVerde = UtilXML.ValorAtributo<int>(vXmlVaria.Attribute("RANGO_SUPERIOR"));
                            }
                        foreach (XElement vXmlVaria in vXlmVariacion.Elements("Rango"))
                            if ((UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("COLOR")).Equals("yellow")))
                            {
                                vRangoAmarillo = UtilXML.ValorAtributo<int>(vXmlVaria.Attribute("RANGO_SUPERIOR"));
                            }
                    }

                    XElement vXlmCuartil = XElement.Parse(vTabulador.XML_CUARTILES);
                    foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
                        if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_INFLACIONAL")).Equals(1)))
                        {
                            vCuartilInflacional = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL"));
                        }
                    foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
                        if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_INCREMENTO")).Equals(1)))
                        {
                            vCuartilIncremento = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL"));
                        }
                    foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
                        if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_MERCADO")).Equals(1)))
                        {
                            vCuartilComparativo = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL"));
                            rcbMercadoTabuladorSueldos.SelectedValue = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL")).ToString();
                        }

                    vLstEmpleados = new List<E_EMPLEADOS_GRAFICAS>();
                    vListaTabuladorSueldos = new List<E_CONSULTA_SUELDOS>();
                    vLstSeleccionadosTabuladorSueldos = new List<E_CONSULTA_SUELDOS>();
                    ObtenerPlaneacionIncrementos();
                }

                if (Request.QueryString["pOrigen"] == "TableroControl")
                {
                    List<int> LstIdsSeleccionados = new List<int>();
                    var vObtienePlaneacion = nTabuladores.ObtienePlaneacionIncrementos(ID_TABULADOR: vIdTabulador).ToList();
                    foreach (var item in vObtienePlaneacion)
                    {
                        LstIdsSeleccionados.Add(item.ID_TABULADOR_EMPLEADO);
                    }
                    ObtenerPlaneacionIncrementos();
                    CargarDatosTabuladorSueldos(LstIdsSeleccionados);

                }

                CargarEmpleados();
                GenerarHeaderGroup();
            }

        }

        protected void ramConsultas_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_ARREGLOS vSeleccion = new E_ARREGLOS();
            E_SELECTOR vSeleccionBono = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
            {
                vSeleccion = JsonConvert.DeserializeObject<E_ARREGLOS>(pParameter);
                vSeleccionBono = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);
            }

            if (vSeleccion.clTipo == "TABULADOR_SUELDOS")
            {
                CargarDatosSeleccionadosSueldos(vSeleccion.arrEmpleados);
            }

        }

        protected void rgdComparacionInventarioPersonal_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (vLstSeleccionadosTabuladorSueldos.Count() > 0)
                rgdComparacionInventarioPersonal.DataSource = CrearDataTableSelecion();
            else
                rgdComparacionInventarioPersonal.DataSource = CrearDataTable();

            GridGroupByField field = new GridGroupByField();
            field.FieldName = "NO_NIVEL";
            field.HeaderText = "<strong>Nivel</strong>";
            field.FormatString = "<strong>{0}</strong>";

            GridGroupByExpression ex = new GridGroupByExpression();
            ex.GroupByFields.Add(field);
            ex.SelectFields.Add(field);
            rgdComparacionInventarioPersonal.MasterTableView.GroupByExpressions.Add(ex);
        }

        protected void rgdComparacionInventarioPersonal_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;
                string vClEmpleado = "";
                int vIdCategoria = int.Parse(gridItem.GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString());
                if (vIdCategoria != 0)
                    vClEmpleado = vListaTabuladorSueldos.Where(t => t.ID_TABULADOR_EMPLEADO == vIdCategoria).FirstOrDefault().CL_EMPLEADO;

                gridItem["NB_EMPLEADO"].ToolTip = vClEmpleado;
            }
        }

        protected void rgdComparacionInventarioPersonal_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            switch (e.Column.UniqueName)
            {
                case "ID_TABULADOR_EMPLEADO":
                    ConfigurarColumna(e.Column, 10, "Empleado", false, false, true, false);
                    break;
                case "NO_NIVEL":
                    ConfigurarColumna(e.Column, 10, "No. Nivel", false, false, true, false);
                    break;
                case "NO":
                    ConfigurarColumna(e.Column, 40, "No.", true, false, false, false);
                    break;
                case "NB_PUESTO":
                    ConfigurarColumna(e.Column, 190, "Puesto", true, false, true, false);
                    break;
                case "NB_DEPARTAMENTO":
                    ConfigurarColumna(e.Column, 200, "Área", true, false, true, false);
                    break;
                case "NB_EMPLEADO":
                    ConfigurarColumna(e.Column, 250, "Nombre completo", true, false, true, false);
                    break;
                case "MN_SUELDO_ORIGINAL":
                    ConfigurarColumna(e.Column, 100, "Sueldo", true, false, true, true);
                    break;
                case "DIFERENCIA":
                    //ConfigurarColumna(e.Column, 100, "Diferencia", true, false, true, true);
                    break;
                case "PR_DIFERENCIA":
                    ConfigurarColumna(e.Column, 150, "Diferencia", true, false, true, true);
                    break;
                case "NO_VALUACION":
                    ConfigurarColumna(e.Column, 90, "Valuación", true, false, true, true);
                    break;
                case "column":
                    break;
                case "ExpandColumn": break;
                default:

                    ConfigurarColumna(e.Column, 120, "", true, true, false, true);
                    break;
            }

        }

        protected void rgEmpleadosTabuladorSueldos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                EliminarEmpleadoSueldos(int.Parse(item.GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString()));
            }
        }

        protected void rgEmpleadosTabuladorSueldos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgEmpleadosTabuladorSueldos.DataSource = vLstSeleccionadosTabuladorSueldos;
        }

        protected void grdCodigoColores_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            List<E_CODIGO_COLORES> vCodigoColores = new List<E_CODIGO_COLORES>();
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "green", DESCRIPCION = "Sueldo dentro del nivel establecido por el tabulador (variación inferior al " + vRangoVerde.ToString() + "%)." });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "yellow", DESCRIPCION = "Sueldo superior o inferior al nivel establecido por el tabulador entre el " + vRangoVerde.ToString() + "% y " + vRangoAmarillo.ToString() + "%." });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "red", DESCRIPCION = "Sueldo superior o inferior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%." });
            grdCodigoColores.DataSource = vCodigoColores;
        }

        protected void rgdComparacionInventarioPersonal_ItemDataBound(object sender, GridItemEventArgs e)
        {
            int strId = 0;


            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;

                strId = int.Parse(dataItem.GetDataKeyValue("NO_NIVEL").ToString());

                if (strId % 2 == 0)
                    dataItem.CssClass = "RadGrid1Class";
                else dataItem.CssClass = "RadGrid2Class";
            }
        }

        protected void rgEmpleadosTabuladorSueldos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgEmpleadosTabuladorSueldos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgEmpleadosTabuladorSueldos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgEmpleadosTabuladorSueldos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgEmpleadosTabuladorSueldos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgEmpleadosTabuladorSueldos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            ContextoTabuladores.oLstEmpleadoTabulador = new List<E_REPORTE_TABULADOR_SUELDOS>();

            ContextoTabuladores.oLstEmpleadoTabulador.Add(new E_REPORTE_TABULADOR_SUELDOS
                {
                    ID_TABULADOR = vIdTabulador
                });

            foreach (GridDataItem item in rgdComparacionInventarioPersonal.MasterTableView.Items)
            {
                int? vIdEmpleadoTabulador = int.Parse(item.GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString());
                if (vIdEmpleadoTabulador != null)
                {
                    ContextoTabuladores.oLstEmpleadoTabulador.Where(t => t.ID_TABULADOR == vIdTabulador).FirstOrDefault().vLstEmpleadosTabulador.Add((int)vIdEmpleadoTabulador);
                }
            }

            if (ContextoTabuladores.oLstEmpleadoTabulador.Where(t => t.ID_TABULADOR == vIdTabulador).FirstOrDefault().vLstEmpleadosTabulador.Count > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "script", "OpenImprimirReporte(" + vIdTabulador + "," + rcbMercadoTabuladorSueldos.SelectedValue + ");", true);
            }
        }

        protected void rcbMercadoTabuladorSueldos_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RecalcularConsulta();
        }

        protected void rntComienzaNivel_TextChanged(object sender, EventArgs e)
        {
            RecalcularConsulta();
        }
    }
}

public class E_ARREGLOS
{
    public string clTipo { set; get; }
    public List<int> arrEmpleados { set; get; }
    public List<int> arrIdTabulador { set; get; }

}