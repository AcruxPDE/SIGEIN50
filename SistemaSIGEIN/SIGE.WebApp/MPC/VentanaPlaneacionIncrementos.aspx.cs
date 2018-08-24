using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.Externas;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.WebApp.Comunes;
using SIGE.Entidades;
using System.IO;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Negocio.Administracion;
using Newtonsoft.Json;
using SIGE.Entidades.Administracion;
using Telerik.Web.UI;
using System.Data;
using System.ComponentModel;
using System.Web.UI.HtmlControls;
namespace SIGE.WebApp.MPC
{
    public partial class VentanaPlaneacionIncrementos : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdTabulador
        {
            get { return (int)ViewState["vsIdTabulador"]; }
            set { ViewState["vsIdTabulador"] = value; }
        }
        private List<E_SELECCIONADOS> vSeleccionEmpleados
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vSeleccionEmpleados"]; }
            set { ViewState["vs_vSeleccionEmpleados"] = value; }
        }
        private List<E_PLANEACION_INCREMENTOS> vObtienePlaneacionIncrementos
        {
            get { return (List<E_PLANEACION_INCREMENTOS>)ViewState["vs_vObtienePlaneacionIncrementos"]; }
            set { ViewState["vs_vObtienePlaneacionIncrementos"] = value; }
        }

        private List<E_PLANEACION_INCREMENTOS> vPlaneacionIncrementos
        {
            get { return (List<E_PLANEACION_INCREMENTOS>)ViewState["vs_vPlaneacionIncrementos"]; }
            set { ViewState["vs_vPlaneacionIncrementos"] = value; }
        }

        private int vRangoVerde
        {
            get { return (int)ViewState["vs_vRangoVerde"]; }
            set { ViewState["vs_vRangoVerde"] = value; }
        }

        private int vRangoAmarillo
        {
            get { return (int)ViewState["vs_vRangoAmarillo"]; }
            set { ViewState["vs_vRangoAmarillo"] = value; }
        }

        private List<E_CUARTILES> vCuartilesTabulador
        {
            get { return (List<E_CUARTILES>)ViewState["vs_vCuartilesTabulador"]; }
            set { ViewState["vs_vCuartilesTabulador"] = value; }
        }

        #endregion

        #region Funciones

        protected void DespacharEventos(string pCatalogo, string pSeleccionados)
        {
            if (pCatalogo == "TABULADOR_EMPLEADO" && pSeleccionados != "undefined")
                CargarDatosEmpleado(pSeleccionados);
            if (pCatalogo == "UPDATE")
            {
                Actualizar();
                grdPlaneacionIncrementos.Rebind();
            }
        }

        protected decimal? CalculoPrDiferencia(decimal? pMnMinimo, decimal? pMnMaximo, decimal? pMnSueldo)
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

                //vMnDivisor = (pMnSueldo - vMnDivisor) / pMnSueldo  100; 
                vMnDivisor = (((pMnSueldo * 100) / vMnDivisor) - 100); // SE CAMBIA LA FORMULA EL DIA 30/04/2018 DE ACUERDO AL TABULADOR QTA
            }
            return vMnDivisor;
        }

        protected string VariacionColor(decimal? pPrDireferencia, decimal? pMnSueldo)
        {
            string vColor;
            decimal vPorcentaje = Math.Abs((decimal)pPrDireferencia);

            if (vPorcentaje >= 0 && vPorcentaje <= vRangoVerde & pMnSueldo == 0)
                vColor = "gray";
            else if (vPorcentaje >= 0 && vPorcentaje <= vRangoVerde & pMnSueldo > 0)
                vColor = "green";
            else if (vPorcentaje > vRangoVerde && vPorcentaje <= vRangoAmarillo & pMnSueldo > 0)
                vColor = "yellow";
            else
                vColor = "red";
            return vColor;
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

        protected string ObtenerIconoDifrencia(decimal? pPrDiferencia, decimal? pMnSueldo)
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

        protected void CargarDatosEmpleado(string pIdsSeleccionados)
        {
            List<int> vSeleccionados = null;
            if (pIdsSeleccionados != null & pIdsSeleccionados != "")
            {
                vSeleccionados = JsonConvert.DeserializeObject<List<int>>(pIdsSeleccionados);
            }

            vSeleccionEmpleados = new List<E_SELECCIONADOS>();

            foreach (int item in vSeleccionados)
            {
                vSeleccionEmpleados.Add(new E_SELECCIONADOS { ID_SELECCIONADO = item });
            }

            var vXelements = vSeleccionEmpleados.Select(x => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", x.ID_SELECCIONADO)));
            XElement SELECCIONADOS = new XElement("EMPLEADOS", vXelements);

            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.ActualizaPlaneacionIncrementosEmpleado(ID_TABULADOR: vIdTabulador, XML_EMPLEADOS: SELECCIONADOS.ToString(), usuario: vClUsuario, programa: vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "CloseWindow");

            Actualizar();
        }

        protected void ActualizarItems()
        {
            int vIdTabuladorEmpleado;
            foreach (GridDataItem item in grdPlaneacionIncrementos.MasterTableView.Items)
            {
                vIdTabuladorEmpleado = int.Parse(item.GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString());
                RadNumericTextBox vNuevoSueldo = (RadNumericTextBox)item.FindControl("txnSueldoNuevo");
                E_PLANEACION_INCREMENTOS vLineaTabulador = vObtienePlaneacionIncrementos.FirstOrDefault(f => f.ID_TABULADOR_EMPLEADO.Equals(vIdTabuladorEmpleado));
                vLineaTabulador.MN_SUELDO_NUEVO = (decimal)vNuevoSueldo.Value;
            }

            foreach (E_PLANEACION_INCREMENTOS item in vObtienePlaneacionIncrementos)
            {
                item.CUARTIL_SELECCIONADO = int.Parse(cmbCuartilIncremento.SelectedValue);
                item.MN_MINIMO_CUARTIL = CalculaMinimo(int.Parse(cmbCuartilIncremento.SelectedValue), item.MN_MINIMO_MINIMO, item.MN_MINIMO_PRIMER_CUARTIL, item.MN_MINIMO_MEDIO, item.MN_MINIMO_SEGUNDO_CUARTIL, item.MN_MINIMO_MAXIMO);
                item.MN_MAXIMO_CUARTIL = CalculaMaximo(int.Parse(cmbCuartilIncremento.SelectedValue), item.MN_MAXIMO_MINIMO, item.MN_MAXIMO_PRIMER_CUARTIL, item.MN_MAXIMO_MEDIO, item.MN_MAXIMO_SEGUNDO_CUARTIL, item.MN_MAXIMO_MAXIMO);
                item.DIFERENCIA = CalculoPrDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_ORIGINAL);
                item.COLOR_DIFERENCIA = VariacionColor(item.DIFERENCIA, item.MN_SUELDO_ORIGINAL);
                item.ICONO = ObtenerIconoDifrencia(item.DIFERENCIA, item.MN_SUELDO_ORIGINAL);
                item.DIFERENCIA_NUEVO = CalculoPrDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_NUEVO);
                item.COLOR_DIFERENCIA_NUEVO = VariacionColor(item.DIFERENCIA_NUEVO, item.MN_SUELDO_NUEVO);
                item.ICONO_NUEVO = ObtenerIconoDifrencia(item.DIFERENCIA_NUEVO, item.MN_SUELDO_NUEVO);
            }
        }

        protected void GenerarHeaderGroup()
        {
            switch (cmbCuartilIncremento.SelectedValue)
            {
                case "1":
                    grdPlaneacionIncrementos.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Mínimo";
                    break;
                case "2":
                    grdPlaneacionIncrementos.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Primer Cuartil";
                    break;
                case "3":
                    grdPlaneacionIncrementos.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Medio";
                    break;
                case "4":
                    grdPlaneacionIncrementos.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Tercer Cuartil";
                    break;
                case "5":
                    grdPlaneacionIncrementos.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Máximo";
                    break;
                default:
                    grdPlaneacionIncrementos.MasterTableView.ColumnGroups.FindGroupByName("TABMEDIO").HeaderText = "Tabulador Medio";
                    break;
            }
        }

        protected decimal ObtieneDiferencia(List<E_PLANEACION_INCREMENTOS> pListaPlaneacion)
        {
            decimal? vDiferencia = 0;
            foreach (var item in pListaPlaneacion)
            {
                vDiferencia = vDiferencia + ((item.MN_SUELDO_NUEVO > 0 ? item.MN_SUELDO_NUEVO : item.MN_SUELDO_ORIGINAL) - item.MN_SUELDO_ORIGINAL);
            }

            return (decimal)vDiferencia;
        }

        //public DataTable ConvertToDataTable<T>(IList<T> data, List<E_CATEGORIA> lstCategoria, string pCategorias)
        //{
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    foreach (PropertyDescriptor prop in properties)
        //        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

        //    foreach (E_CATEGORIA vCategorias in lstCategoria)
        //        table.Columns.Add(vCategorias.NO_CATEGORIA.ToString());

        //    foreach (T item in data)
        //    {
        //        DataRow row = table.NewRow();
        //        foreach (PropertyDescriptor prop in properties)
        //        {
        //            if (!prop.Name.Equals(pCategorias))
        //                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        //            else
        //            {
        //                object opCategorias = prop.GetValue(item);
        //                if (opCategorias != null)
        //                {
        //                    XElement vXmlCamposAdicionales = XElement.Parse(opCategorias.ToString());
        //                    foreach (E_CATEGORIA vCategoriaItem in lstCategoria)
        //                    {
        //                        XElement vXmlCampoAdicional = vXmlCamposAdicionales.Elements("ITEM").FirstOrDefault(f => UtilXML.ValorAtributo<string>(f.Attribute("NO_CATEGORIA")) == vCategoriaItem.NO_CATEGORIA.ToString());
        //                        if (vXmlCampoAdicional != null)
        //                        {
        //                            if (int.Parse(cmbCuartilIncremento.SelectedValue) == 1)
        //                                row[vCategoriaItem.NO_CATEGORIA.ToString()] = UtilXML.ValorAtributo<string>(vXmlCampoAdicional.Attribute("MN_MINIMO"));
        //                            else if (int.Parse(cmbCuartilIncremento.SelectedValue) == 2)
        //                                row[vCategoriaItem.NO_CATEGORIA.ToString()] = UtilXML.ValorAtributo<string>(vXmlCampoAdicional.Attribute("MN_PRIMER_CUARTIL"));
        //                            else if (int.Parse(cmbCuartilIncremento.SelectedValue) == 3)
        //                                row[vCategoriaItem.NO_CATEGORIA.ToString()] = UtilXML.ValorAtributo<string>(vXmlCampoAdicional.Attribute("MN_MEDIO"));
        //                            else if (int.Parse(cmbCuartilIncremento.SelectedValue) == 4)
        //                                row[vCategoriaItem.NO_CATEGORIA.ToString()] = UtilXML.ValorAtributo<string>(vXmlCampoAdicional.Attribute("MN_SEGUNDO_CUARTIL"));
        //                            else if (int.Parse(cmbCuartilIncremento.SelectedValue) == 5)
        //                                row[vCategoriaItem.NO_CATEGORIA.ToString()] = UtilXML.ValorAtributo<string>(vXmlCampoAdicional.Attribute("MN_MAXIMO"));
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        table.Rows.Add(row);
        //    }
        //    return table;
        //}

        //protected DataTable CrearDataTable<T>(IList<T> pLista, RadGrid pCtrlGrid)
        //{

        //    List<E_CATEGORIA> lstCategoria = new List<E_CATEGORIA>();

        //    lstCategoria = vObtienePlaneacionIncrementos.FirstOrDefault().lstCategorias;

        //    DataTable vColumnas = ConvertToDataTable(pLista, lstCategoria, "XML_CATEGORIAS");

        //    foreach (E_CATEGORIA item in lstCategoria)
        //    {
        //        GridBoundColumn vBoundColumn = new GridBoundColumn();
        //        vBoundColumn.DataField = item.NO_CATEGORIA.ToString();
        //        vBoundColumn.UniqueName = item.NO_CATEGORIA.ToString();
        //        vBoundColumn.HeaderText = ((char)(item.NO_CATEGORIA + 64)).ToString();
        //        vBoundColumn.ColumnGroupName = "TABMEDIO";
        //        (vBoundColumn as GridBoundColumn).AllowFiltering = false;
        //        vBoundColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        //        vBoundColumn.ItemStyle.Font.Bold = true;
        //        vBoundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        //        vBoundColumn.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(100);
        //        vBoundColumn.AutoPostBackOnFilter = true;
        //        grdPlaneacionIncrementos.MasterTableView.Columns.Add(vBoundColumn);
        //    }

        //    return vColumnas;
        //}

        protected void GuardarPlaneacionIncrementos(bool pFgCerrarVentana, bool pAfectarInventario)
        {
            int vIdTabuladorEmpleado = 0;
            vPlaneacionIncrementos = new List<E_PLANEACION_INCREMENTOS>();
            foreach (GridDataItem item in grdPlaneacionIncrementos.MasterTableView.Items)
            {
                vIdTabuladorEmpleado = (int.Parse(item.GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString()));
                RadNumericTextBox vNuevoSueldo = (RadNumericTextBox)item.FindControl("txnSueldoNuevo");

                if (!vNuevoSueldo.Equals(""))
                {
                    vPlaneacionIncrementos.Add(new E_PLANEACION_INCREMENTOS
                    {
                        ID_TABULADOR_EMPLEADO = vIdTabuladorEmpleado,
                        MN_SUELDO_NUEVO = decimal.Parse(vNuevoSueldo.Text),
                    });
                }
            }

            List<E_PLANEACION_INCREMENTOS> vResultadoPaneacion = new List<E_PLANEACION_INCREMENTOS>();
            for (int i = 0; i < vPlaneacionIncrementos.Count; i++)
            {
                if (vPlaneacionIncrementos.ElementAt(i).MN_SUELDO_NUEVO != vObtienePlaneacionIncrementos.ElementAt(i).MN_SUELDO_ORIGINAL && vPlaneacionIncrementos.ElementAt(i).MN_SUELDO_NUEVO > 0)
                {
                    vResultadoPaneacion.Add(vPlaneacionIncrementos.ElementAt(i));
                }
            }

            var vXelements = vResultadoPaneacion.Select(x => CrearNodoIncremento(x));
            XElement PLANEACIONINCREMENTO = new XElement("INCREMENTOS", vXelements);

            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.InsertaActualizaNuevoSueldo(vIdTabulador, PLANEACIONINCREMENTO.ToString(), pAfectarInventario, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                bool vCerrarVentana = pFgCerrarVentana;
                string vCallBackFunction = vCerrarVentana ? "onCloseWindow" : null;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: vCallBackFunction);
            }
        }

        protected XElement CrearNodoIncremento(E_PLANEACION_INCREMENTOS pPlaneacionIncremento)
        {
            XElement vXmlPlaneacionIncremento = new XElement("INCREMENTO");
            vXmlPlaneacionIncremento.Add(new XAttribute("ID_TABULADOR_EMPLEADO", pPlaneacionIncremento.ID_TABULADOR_EMPLEADO));
            vXmlPlaneacionIncremento.Add(new XAttribute("MN_SUELDO_NUEVO", pPlaneacionIncremento.MN_SUELDO_NUEVO));
            //if (pPlaneacionIncremento.FE_CAMBIO_SUELDO != null)
            //    vXmlPlaneacionIncremento.Add(new XAttribute("FE_CAMBIO_SUELDO", pPlaneacionIncremento.FE_CAMBIO_SUELDO));
            return vXmlPlaneacionIncremento;
        }

        protected List<E_CATEGORIA> SeleccionCuartil(XElement xlmCuartiles, int ID_TABULADOR_EMPLEADO)
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

            return lstCategoria;
        }

        protected void Actualizar()
        {

            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            vObtienePlaneacionIncrementos = nTabulador.ObtieneEmpleadosPlaneacionIncrementos(ID_TABULADOR: vIdTabulador).Select(s => new E_PLANEACION_INCREMENTOS()
            {
                NUM_ITEM = (int)s.NUM_RENGLON,
                NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
                ID_TABULADOR_EMPLEADO = s.ID_TABULADOR_EMPLEADO,
                NO_NIVEL = s.NO_NIVEL,
                NB_PUESTO = s.NB_PUESTO,
                NB_DEPARTAMENTO = s.NB_DEPARTAMENTO,
                NB_EMPLEADO = s.NOMBRE,
                CL_EMPLEADO = s.CL_EMPLEADO,
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
                //MN_SUELDO_NUEVO_INICIAL = s.MN_SUELDO_NUEVO,
                FE_CAMBIO_SUELDO = s.FE_CAMBIO_SUELDO,
                XML_CATEGORIAS = s.XML_CATEGORIA,
                lstCategorias = SeleccionCuartil(XElement.Parse(s.XML_CATEGORIA), s.ID_TABULADOR_EMPLEADO),
                CUARTIL_SELECCIONADO = int.Parse(cmbCuartilIncremento.SelectedValue)
            }).ToList();

            foreach (E_PLANEACION_INCREMENTOS item in vObtienePlaneacionIncrementos)
            {
                item.MN_MINIMO_CUARTIL = CalculaMinimo(int.Parse(cmbCuartilIncremento.SelectedValue), item.MN_MINIMO_MINIMO, item.MN_MINIMO_PRIMER_CUARTIL, item.MN_MINIMO_MEDIO, item.MN_MINIMO_SEGUNDO_CUARTIL, item.MN_MINIMO_MAXIMO);
                item.MN_MAXIMO_CUARTIL = CalculaMaximo(int.Parse(cmbCuartilIncremento.SelectedValue), item.MN_MAXIMO_MINIMO, item.MN_MAXIMO_PRIMER_CUARTIL, item.MN_MAXIMO_MEDIO, item.MN_MAXIMO_SEGUNDO_CUARTIL, item.MN_MAXIMO_MAXIMO);
                item.DIFERENCIA = CalculoPrDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_ORIGINAL);
                item.DIFERENCIA_NUEVO = CalculoPrDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_NUEVO);
                item.COLOR_DIFERENCIA = VariacionColor(item.DIFERENCIA, item.MN_SUELDO_ORIGINAL);
                item.COLOR_DIFERENCIA_NUEVO = VariacionColor(item.DIFERENCIA_NUEVO, item.MN_SUELDO_NUEVO);
                item.ICONO = ObtenerIconoDifrencia(item.DIFERENCIA, item.MN_SUELDO_ORIGINAL);
                item.ICONO_NUEVO = ObtenerIconoDifrencia(item.DIFERENCIA_NUEVO, item.MN_SUELDO_NUEVO);
            }
        }

        private void UpdateToolTip(string elementID, UpdatePanel panel)
        {
            Control ctrl = Page.LoadControl("TooltipPlaneacionIncrementos.ascx");
            ctrl.ID = "UcTooltipPlaneacionIncrementos1";
            panel.ContentTemplateContainer.Controls.Add(ctrl);
            TooltipPlaneacionIncrementos planeacion = (TooltipPlaneacionIncrementos)ctrl;
            var vSecuancias = vObtienePlaneacionIncrementos.Where(w => w.ID_TABULADOR_EMPLEADO == int.Parse(elementID)).ToList();
            planeacion.vSecuanciasEmpleado = vSecuancias;
        }

        protected string CalculaCantidad(int pMnSeleccionado, decimal? pMnMinimo, decimal? pMnPrimerCuartil, decimal? pMnMedio, decimal? pMnSegundoCuartil, decimal? pMnMaximo)
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


            return String.Format("{0:C}", vCantidad);
        }


        private void SeguridadProcesos()
        {
            btnGuardarCerrar.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.K.A");
            btnGuardar.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.K.A");
            btnAplicarIncremento.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.K.A");
            btnInventario.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.K.A");
            btnSeleccionar.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.K.A");
            // btnRecalcular.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.K.A");
        }

        private HtmlGenericControl GeneraHtml(int? pIdTabuladorEmpleado)
        {
            HtmlGenericControl vCtrlTabla = new HtmlGenericControl("table");

            HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");

            var vResultado = vObtienePlaneacionIncrementos.Where(t => t.ID_TABULADOR_EMPLEADO == pIdTabuladorEmpleado).FirstOrDefault();
            if (vResultado != null)
            {
                foreach (var item in vResultado.lstCategorias)
                {
                    HtmlGenericControl vCtrlColumnaResultado = new HtmlGenericControl("td");
                    vCtrlColumnaResultado.Attributes.Add("style", "font-family:arial; font-size: 11pt; width:110px;");
                    vCtrlColumnaResultado.InnerText = CalculaCantidad(int.Parse(cmbCuartilIncremento.SelectedValue), item.MN_MINIMO, item.MN_PRIMER_CUARTIL, item.MN_MEDIO, item.MN_SEGUNDO_CUARTIL, item.MN_MAXIMO);
                    vCtrlRow.Controls.Add(vCtrlColumnaResultado);

                }
                vCtrlTabla.Controls.Add(vCtrlRow);
            }
            return vCtrlTabla;
        }

        private string GeneraEncabezado()
        {
            string vNbEncabezado = "";
            int? vNoCategorias = 1;

            var vResultado = vObtienePlaneacionIncrementos.FirstOrDefault();
            if (vResultado != null)
            {

                vNoCategorias = vResultado.lstCategorias.Count();

                foreach (E_CATEGORIA item in vResultado.lstCategorias.OrderBy(o => o.NO_CATEGORIA))
                {
                    vNbEncabezado = vNbEncabezado + "<td style='font-family:arial; font-size: 11pt; width:110px; text-align:center;'>" + (char)(item.NO_CATEGORIA + 64) + "</td>";
                }

            }

            grdPlaneacionIncrementos.MasterTableView.Columns[9].HeaderStyle.Width = (Unit)(vNoCategorias * 110);

            return "<table><tr>" + vNbEncabezado + "</tr></table>";
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    vIdTabulador = int.Parse((Request.QueryString["ID"]));
                    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();

                    XElement vXlmCuartiles = XElement.Parse(vTabulador.XML_VW_CUARTILES);
                    vCuartilesTabulador = vXlmCuartiles.Elements("ITEM").Select(x => new E_CUARTILES
                    {
                        ID_CUARTIL = UtilXML.ValorAtributo<int>(x.Attribute("NB_VALOR")),
                        NB_CUARTIL = UtilXML.ValorAtributo<string>(x.Attribute("NB_TEXTO")),
                    }).ToList();

                    SeguridadProcesos();

                    cmbCuartilIncremento.DataSource = vCuartilesTabulador;
                    cmbCuartilIncremento.DataTextField = "NB_CUARTIL";
                    cmbCuartilIncremento.DataValueField = "ID_CUARTIL".ToString();
                    cmbCuartilIncremento.DataBind();

                    XElement vXlmCuartil = XElement.Parse(vTabulador.XML_CUARTILES);
                    //txtClTabulador.Text = vTabulador.CL_TABULADOR;
                    //txtDsTabulador.Text = vTabulador.DS_TABULADOR;
                    //rdpCreacion.SelectedDate = vTabulador.FE_CREACION;
                    //rdpVigencia.SelectedDate = vTabulador.FE_VIGENCIA;
                    txtClTabulador.InnerText = vTabulador.CL_TABULADOR;
                    txtNbTabulador.InnerText = vTabulador.NB_TABULADOR;
                    txtDescripción.InnerText = vTabulador.DS_TABULADOR;
                    txtVigencia.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtFecha.InnerText = vTabulador.FE_CREACION.ToString("dd/MM/yyyy");
                    txtPuestos.InnerText = vTabulador.CL_TIPO_PUESTO;
                    if (vTabulador.CL_ESTADO == "CERRADO")
                    {
                        btnGuardarCerrar.Enabled = false;
                        btnGuardar.Enabled = false;
                        btnAplicarIncremento.Enabled = false;
                        btnInventario.Enabled = false;
                        btnSeleccionar.Enabled = false;
                        //  btnRecalcular.Enabled = false;
                    }
                    if (vTabulador.XML_VARIACION != null)
                    {
                        XElement vXlmVariacion = XElement.Parse(vTabulador.XML_VARIACION);
                        foreach (XElement vXmlVaria in vXlmVariacion.Elements("Rango"))
                            if ((UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("COLOR")).Equals("green")))
                            {
                                vRangoVerde = UtilXML.ValorAtributo<int>(vXmlVaria.Attribute("RANGO_SUPERIOR"));
                                txtSemaforoVerde.Text = vRangoVerde.ToString();
                            }
                        foreach (XElement vXmlVaria in vXlmVariacion.Elements("Rango"))
                            if ((UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("COLOR")).Equals("yellow")))
                            {
                                vRangoAmarillo = UtilXML.ValorAtributo<int>(vXmlVaria.Attribute("RANGO_SUPERIOR"));
                                txtSemaforoAmarillo.Text = vRangoAmarillo.ToString();
                            }
                    }
                    foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
                        if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_INCREMENTO")).Equals(1)))
                        {
                            cmbCuartilIncremento.SelectedValue = UtilXML.ValorAtributo<string>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL"));
                        }

                    Actualizar();

                    //var masterTableView = grdPlaneacionIncrementos.MasterTableView;
                    //var column = masterTableView.GetColumn("TAB_MEDIO");
                    //column.HeaderText = GeneraEncabezado();
                    //masterTableView.Rebind();
                }
            }

            var masterTableView = grdPlaneacionIncrementos.MasterTableView;
            var column = masterTableView.GetColumn("TAB_MEDIO");
            GenerarHeaderGroup();
            column.HeaderText = GeneraEncabezado();
            DespacharEventos(Request.Params.Get("__EVENTTARGET"), Request.Params.Get("__EVENTARGUMENT"));
        }

        protected void grdPlaneacionIncrementos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdPlaneacionIncrementos.DataSource = vObtienePlaneacionIncrementos;
            decimal sumSueldoOriginal = vObtienePlaneacionIncrementos.Sum(s => s.MN_SUELDO_ORIGINAL) ?? 0;
            txtNominaActual.Text = String.Format("{0:C}", sumSueldoOriginal);

            //decimal vDiferencia = (sumNominaPlaneada - sumSueldoOriginal);
            decimal vDiferencia = ObtieneDiferencia(vObtienePlaneacionIncrementos);
            txtDiferencia.Text = String.Format("{0:C}", vDiferencia);


            //decimal sumNominaPlaneada = vObtienePlaneacionIncrementos.Sum(s => s.MN_SUELDO_NUEVO) ?? 0;
            decimal sumNominaPlaneada = sumSueldoOriginal + vDiferencia;
            txtNominaPlaneada.Text = String.Format("{0:C}", sumNominaPlaneada);


            //if (txtDiferencia != null && sumSueldoOriginal != 0)
            //    txtDiferenciaPr.Text = String.Format("{0:N2}", (vDiferencia / sumSueldoOriginal) * 100);
            if (txtDiferencia != null && sumSueldoOriginal != 0)
                txtDiferenciaPr.Text = String.Format("{0:N2}", ((vDiferencia * 100) / sumSueldoOriginal));


            //DataTable PlaneacionIncrementos = CrearDataTable(vObtienePlaneacionIncrementos, grdPlaneacionIncrementos);
            //grdPlaneacionIncrementos.DataSource = PlaneacionIncrementos;

            GridGroupByField field = new GridGroupByField();
            field.FieldName = "NO_NIVEL";
            field.HeaderText = "Nivel";
            field.HeaderText = "<strong>Nivel</strong>";
            field.FormatString = "<strong>{0}</strong>";
            GridGroupByExpression ex = new GridGroupByExpression();
            ex.GroupByFields.Add(field);
            ex.SelectFields.Add(field);
            grdPlaneacionIncrementos.MasterTableView.GroupByExpressions.Add(ex);


        }

        protected void grvClasificacionCompetencia_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<E_CODIGO_COLORES> vCodigoColores = new List<E_CODIGO_COLORES>();
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "green", DESCRIPCION = "Sueldo dentro del nivel establecido por el tabulador (variación inferior al " + vRangoVerde.ToString() + "%)." });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "yellow", DESCRIPCION = "Sueldo superior o inferior al nivel establecido por el tabulador entre el " + vRangoVerde.ToString() + "% y " + vRangoAmarillo.ToString() + "%." });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "red", DESCRIPCION = "Sueldo superior o inferior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%." });
            grdCodigoColores.DataSource = vCodigoColores;
        }

        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            ActualizarItems();
            grdPlaneacionIncrementos.DataSource = vObtienePlaneacionIncrementos;
            grdPlaneacionIncrementos.Rebind();
        }

        protected void btnGuardarCerrar_Click(object sender, EventArgs e)
        {
            GuardarPlaneacionIncrementos(true, false);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarPlaneacionIncrementos(false, false);
        }

        protected void cmbCuartilIncremento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ActualizarItems();
            GenerarHeaderGroup();
            grdPlaneacionIncrementos.Rebind();
        }

        protected void grdPlaneacionIncrementos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            //{
            //    Control target = e.Item.FindControl("gbcMinimo");
            //    Control targetMaximo = e.Item.FindControl("gbcMaximo");

            //    if (!Object.Equals(target, null))
            //    {
            //        if (!Object.Equals(this.rttmSecuancias, null))
            //        {
            //            this.rttmSecuancias.TargetControls.Add(target.ClientID, (e.Item as GridDataItem).GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString(), true);
            //        }
            //    }

            //    if (!Object.Equals(targetMaximo, null))
            //    {
            //        if (!Object.Equals(this.rttmSecuancias, null))
            //        {
            //            this.rttmSecuancias.TargetControls.Add(targetMaximo.ClientID, (e.Item as GridDataItem).GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString(), true);
            //        }
            //    }
            //}

            int strId = 0;

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;

                strId = int.Parse(dataItem.GetDataKeyValue("NO_NIVEL").ToString());

                if (strId % 2 == 0)
                    dataItem.CssClass = "RadGrid1Class";
                else dataItem.CssClass = "RadGrid2Class";


                int? vIdEmpleadoTabulador = int.Parse(dataItem.GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString());
                HtmlGenericControl vCtrlDiv = (HtmlGenericControl)dataItem.FindControl("dvTabulador");
                if (vCtrlDiv != null)
                    vCtrlDiv.Controls.Add(GeneraHtml(vIdEmpleadoTabulador));
            }

        }

        protected void rttmSecuancias_AjaxUpdate(object sender, ToolTipUpdateEventArgs e)
        {
            this.UpdateToolTip(e.Value, e.UpdatePanel);
        }

        protected void btnInventario_Click(object sender, EventArgs e)
        {
            GuardarPlaneacionIncrementos(false, true);
        }

        protected void txnSueldoNuevo_TextChanged(object sender, EventArgs e)
        {
            decimal? vTotalIncremento = 0;
            foreach (GridDataItem item in grdPlaneacionIncrementos.MasterTableView.Items)
            {
                decimal? vSueldoOriginal = vObtienePlaneacionIncrementos.Where(w => w.ID_TABULADOR_EMPLEADO == (int)(item.GetDataKeyValue("ID_TABULADOR_EMPLEADO"))).FirstOrDefault().MN_SUELDO_ORIGINAL;
                RadNumericTextBox vNuevoSueldo = (RadNumericTextBox)item.FindControl("txnSueldoNuevo");
                if (vNuevoSueldo != null)
                    if (vNuevoSueldo.Value != null)
                    {
                        vTotalIncremento = vTotalIncremento + ((vNuevoSueldo.Value > 0? (decimal)vNuevoSueldo.Value : vSueldoOriginal) - vSueldoOriginal);
                    }
            }

            decimal sumSueldoOriginal = vObtienePlaneacionIncrementos.Sum(s => s.MN_SUELDO_ORIGINAL) ?? 0;
            //decimal vDiferencia = (vTotalIncremento - sumSueldoOriginal);
            decimal? vDiferencia = vTotalIncremento;
            txtDiferencia.Text = String.Format("{0:C}", vDiferencia);

            decimal? sumNominaPlaneada = sumSueldoOriginal + vDiferencia;
            txtNominaPlaneada.Text = String.Format("{0:C}", sumNominaPlaneada);

            //if (txtDiferencia != null && sumSueldoOriginal != 0)
            //    txtDiferenciaPr.Text = String.Format("{0:N2}", (vDiferencia / sumSueldoOriginal) * 100);
            if (txtDiferencia != null && sumSueldoOriginal != 0)
                txtDiferenciaPr.Text = String.Format("{0:N2}", (vDiferencia *100) / sumSueldoOriginal);


            ActualizarItems();
            grdPlaneacionIncrementos.DataSource = vObtienePlaneacionIncrementos;
            grdPlaneacionIncrementos.Rebind();
        }

    }
}
