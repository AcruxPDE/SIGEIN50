using SIGE.Entidades.Administracion;
using System.Collections.Generic;
using System.Web;
using System.Xml.Linq;
using System.Linq;
using SIGE.Entidades.Externas;
using System;
using System.Web.UI;
using SIGE.Negocio.Utilerias;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using SIGE.Entidades.FormacionDesarrollo;


namespace WebApp.Comunes
{
    public static class Utileria
    {
        public static T GetSessionValue<T>(string key)
        {
            return (HttpContext.Current.Session[key] == null) ? default(T) : (T)HttpContext.Current.Session[key];
        }

        public static void SetSessionValue<T>(string key, T value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static string area()
        {
            string area = String.Empty;
            if (HttpContext.Current.Request.QueryString["area"] != null)
                area = HttpContext.Current.Request.QueryString["area"];
            return area;
        }

        public static RadMenu CrearMenu(List<E_MENU> pLstMenu, bool pIsMobileDevice)
        {
            if (!pIsMobileDevice)
                pLstMenu.Where(w => w.ID_MENU_PADRE.Equals(null)).ToList().ForEach(i => i.NB_MENU = (String.IsNullOrWhiteSpace(i.NB_IMAGEN)) ? i.NB_MENU : "&nbsp;");

            RadMenu mModulo = new RadMenu()
            {
                DataFieldID = "ID_MENU",
                DataFieldParentID = "ID_MENU_PADRE",
                DataNavigateUrlField = "NB_URL",
                DataTextField = "NB_MENU",
                RenderMode = pIsMobileDevice ? RenderMode.Mobile : RenderMode.Classic,
                EnableRootItemScroll = true,
                CssClass = "MenuPrincipal",
                OnClientItemClicking = "MenuItemClicked"

            };

            mModulo.DataBindings.Add(new RadMenuItemBinding()
            {
                ImageUrlField = "NB_IMAGEN",
                ToolTipField = "NB_TOOLTIP"
            });

            mModulo.DataSource = pLstMenu;
            mModulo.DataBind();

            mModulo.Style.Add("float", "right");

            return mModulo;
        }

        public static List<E_MENU> CrearMenuLista(List<E_FUNCION> pFunciones, string pClModulo, bool pFgOcultarRaiz = false, bool pFgLimpiarEstilo = false)
        {
            List<E_MENU> lstMenu = new List<E_MENU>();
            List<E_FUNCION> lstFuncionRaiz = new List<E_FUNCION>();
            foreach (E_FUNCION funcion in pFunciones)
            {
                
                E_FUNCION f = new E_FUNCION()
                {
                    CL_FUNCION = funcion.CL_FUNCION,
                    CL_TIPO_FUNCION = funcion.CL_TIPO_FUNCION,
                    FG_SELECCIONADO = funcion.FG_SELECCIONADO,
                    ID_FUNCION = funcion.ID_FUNCION,
                    ID_FUNCION_PADRE = funcion.ID_FUNCION_PADRE,
                    NB_FUNCION = funcion.NB_FUNCION,
                    DS_FUNCION = funcion.DS_FUNCION,
                    NB_IMAGEN = funcion.NB_IMAGEN,
                    NB_URL = funcion.NB_URL,
                    XML_CONFIGURACION = funcion.XML_CONFIGURACION
                };

                bool vFgCrearItem = false;
                string vIconoURL = f.NB_IMAGEN;
                string vNbFuncion = f.NB_FUNCION;
               // string vNbTooltip = f.NB_FUNCION;
                string vNbTooltip = f.DS_FUNCION;

                XElement root = XElement.Parse(f.XML_CONFIGURACION).Element("ROOT");
                if (root != null)
                {
                    XElement modulos = root.Element("MODULOS");
                    if (modulos != null)
                    {
                        vFgCrearItem = modulos.Elements("MODULO").Any(a => a.Attribute("ID").Value.Equals(pClModulo));

                        if (vFgCrearItem)
                            f.NB_URL = AgregarVariableURL(f.NB_URL, "m", pFgLimpiarEstilo ? modulos.Element("MODULO").Attribute("ID").Value : pClModulo);

                        XElement icono = root.Element("ICONO");
                        if (icono != null)
                        {
                            XAttribute iconoURL = icono.Attribute("URL");
                            if (iconoURL != null)
                            {
                                vIconoURL = iconoURL.Value;
                                vNbFuncion = f.NB_FUNCION;
                            }
                        }
                    }
                }

                if (pFgOcultarRaiz)
                {
                    if (f.ID_FUNCION_PADRE == null)
                    {
                        vFgCrearItem = false;
                        lstFuncionRaiz.Add(f);
                    }
                    else
                    {
                        if (lstFuncionRaiz.Any(a => a.ID_FUNCION.Equals(f.ID_FUNCION_PADRE)))
                        {
                            f.ID_FUNCION_PADRE = null;
                        }
                    }
                }

                if (vFgCrearItem)
                    lstMenu.Add(new E_MENU()
                    {
                        CL_MENU = f.CL_FUNCION,
                        CL_TIPO_MENU = f.CL_TIPO_FUNCION,
                        FG_SELECCIONADO = f.FG_SELECCIONADO,
                        ID_MENU = f.ID_FUNCION,
                        ID_MENU_PADRE = f.ID_FUNCION_PADRE,
                        NB_IMAGEN = vIconoURL,
                        NB_MENU = vNbFuncion,
                        NB_TOOLTIP = vNbTooltip,
                        NB_URL = f.NB_URL,
                        XML_CONFIGURACION = f.XML_CONFIGURACION
                    });
            }

            return lstMenu;
        }

        public static string AgregarVariableURL(string pURL, string pVarName, string pVarValue)
        {
            string vFgIniciar = (pURL.Split('?').Count() == 1) ? "?" : "&";
            return String.Format("{0}{1}{2}={3}", pURL, vFgIniciar, pVarName, pVarValue);
        }

        public static string ObtenerCssModulo(string pClModulo)
        {
            switch (pClModulo)
            {
                case "COMPENSACION":
                    return "MC.css";
                case "EVALUACION":
                    return "EO.css";
                case "CLIMA":
                    return "EO.css";
                case "DESEMPENO":
                    return "EO.css";
                case "ROTACION":
                    return "EO.css";
                case "FORMACION":
                    return "FD.css";
                case "GENERAL":
                    return "General.css";
                case "INTEGRACION":
                    return "IP.css";
                case "NOMINA":
                    return "Nomina.css";
                case "ORGANIZACIONAL":
                    return "EO.css";
                case "PUNTODEENCUENTRO":
                    return "PDE.css";
                case "ENCUENTRO":
                    return "PDE.css";
                default:
                    return "General.css";
            }
        }

        public static Type ObtenerTipoDato(string pTipoDato)
        {
            switch (pTipoDato.ToUpper())
            {
                case "INT":
                    return typeof(int);
                case "DATE":
                    return typeof(DateTime);
                case "BOOLEAN":
                    return typeof(bool);
                case "DECIMAL":
                    return typeof(decimal);
                case "STRING":
                default:
                    return typeof(string);
            }
        }

        public static E_TIPO_DATO ObtenerEnumTipoDato(string pTipoDato)
        {
            switch (pTipoDato.ToUpper())
            {
                case "INT":
                    return E_TIPO_DATO.INT;
                case "DATE":
                    return E_TIPO_DATO.DATETIME;
                case "BOOLEAN":
                    return E_TIPO_DATO.BOOLEAN;
                case "DECIMAL":
                    return E_TIPO_DATO.DECIMAL;
                case "STRING":
                default:
                    return E_TIPO_DATO.STRING;
            }
        }

        public static bool ComprobarFormatoEmail(string pEmail)
        {
            String sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(pEmail, sFormato))
            {
                if (Regex.Replace(pEmail, sFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static XElement GuardarNotas(string pDsContenido, string pNbNodoPadre)
        {
            XElement vXelementNotas = null;

            var vXelementNota = new XElement("NOTA", new XAttribute("FE_NOTA", DateTime.Now.ToString()),
          new XAttribute("DS_NOTA", pDsContenido));

            vXelementNotas = new XElement(pNbNodoPadre, vXelementNota);

            return vXelementNotas;
        }

        public static string MostrarNotas(string pDsNotas)
        {
            E_NOTA pNota = new E_NOTA();
            string vDsNota = "";

            if (!pDsNotas.ToString().Equals(""))
            {
                XElement vNotas = XElement.Parse(pDsNotas);
                if (ValidarRamaXml(vNotas, "NOTA"))
                {
                    pNota = vNotas.Elements("NOTA").Select(el => new E_NOTA
                    {
                        DS_NOTA = UtilXML.ValorAtributo<string>(el.Attribute("DS_NOTA")),
                        FE_NOTA = UtilXML.ValorAtributo<DateTime>(el.Attribute("FE_NOTA")),

                    }).FirstOrDefault();

                    if (pNota.DS_NOTA != null)
                       vDsNota = pNota.DS_NOTA.ToString();
                }
            }


            return vDsNota;
        }

        private static Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);
            if (foundEl != null)
            {
                return true;
            }

            return false;
        }

        public static string LetrasCapitales(string texto)
        {
            texto = texto.ToLower();
            texto = texto.Replace("_", " ");

            char[] array = texto.ToCharArray();
            //Primera letra de la cadena
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }

            //Buscar la primera letra de cada palabra
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    //if (char.IsLower(array[i]))
                    //{
                    //    array[i] = char.ToUpper(array[i]);
                    //}
                }
            }

            return new string(array);
        }

        //public static void SelectIndexChangeComboboxGrid(object sender, RadGrid Grid_parametro, string UniqueName_textbox)
        //{
        //    if (Grid_parametro.MasterTableView.IsItemInserted)
        //        cargaDatosdecomboboxATextBoxGrid(sender, (GridDataInsertItem)((RadComboBox)sender).NamingContainer, UniqueName_textbox);
        //    else
        //        foreach (GridDataItem item in Grid_parametro.EditItems)
        //            if (item != null)
        //                cargaDatosdecomboboxATextBoxGrid(sender, item, UniqueName_textbox);
        //}

        //public static dynamic ObtenItemEnTemplate(dynamic objeto, RadGrid Grid_parametro)
        //{
        //    dynamic resultado = null;
        //    if (Grid_parametro.MasterTableView.IsItemInserted)
        //        resultado = (GridDataInsertItem)objeto.NamingContainer;
        //    else
        //        foreach (GridDataItem item in Grid_parametro.EditItems)
        //            if (item != null)
        //            {
        //                GridEditableItem item2 = (GridEditableItem)item.EditFormItem;
        //                resultado = item2;
        //            }
        //    return resultado;
        //}
        //public static dynamic ObtenItemEnTemplateNormal(dynamic objeto, RadGrid Grid_parametro)
        //{
        //    //dynamic resultado = null;
        //    return objeto.NamingContainer as GridEditableItem;
        //    //if (Grid_parametro.MasterTableView.IsItemInserted)
        //    //    resultado =  (GridDataInsertItem)objeto.NamingContainer; 
        //    //else
        //    //    foreach (GridDataItem item in Grid_parametro.EditItems)
        //    //        if (item != null)
        //    //        {
        //    //            GridEditableItem item2 = (GridEditableItem)item.EditFormItem;
        //    //            resultado = item2;
        //    //        }
        //    //return resultado;
        //}

        //public static void cargaDatosdecomboboxATextBoxGrid(object sender, dynamic item, string UniqueName_textbox)
        //{
        //    RadComboBox comboorigen = (RadComboBox)sender;
        //    var value = (comboorigen.SelectedValue);
        //    TableCell cellColumna = item[UniqueName_textbox];
        //    cellColumna.Text = value;
        //}
        //public static void cargaDatosdecomboboxATextBox(object sender, RadTextBox txt)
        //{
        //    RadComboBox comboorigen = (RadComboBox)sender;
        //    var value = (comboorigen.SelectedValue);
        //    txt.Text = value;
        //}

        //public static string ObtenValorCombobox(object sender)
        //{
        //    RadComboBox comboorigen = (RadComboBox)sender;
        //    return comboorigen.SelectedValue;

        //}

        //public static bool EsRFC(string RFC)
        //{
        //    string ExpresionRegular = @"[A-Z,Ñ,&]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?";
        //    Regex re = new Regex(ExpresionRegular);
        //    return (re.IsMatch(RFC));
        //}

        //public static string StreamToString(Stream stream)
        //{
        //    stream.Position = 0;
        //    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        //    {
        //        return reader.ReadToEnd();
        //    }
        //}

        //public static Stream StringToStream(string src)
        //{
        //    byte[] byteArray = Encoding.UTF8.GetBytes(src);
        //    return new MemoryStream(byteArray);
        //}

        //public static void SeleccionaRegitrosEnBaseDataKeyNames(string DataKeyNames, object value_DataKeyNames, RadGrid grid)
        //{
        //    GridDataItem item = grid.MasterTableView.FindItemByKeyValue(DataKeyNames, value_DataKeyNames);
        //    if (item != null)
        //    {
        //        item.Selected = true;
        //    }
        //}

        //public static bool MuestraMensajeEstandar(dynamic mensajes, RadWindowManager rnMensaje, bool muestramsjAlGuardarCorrectamente = false, string msjAMostrarAlguardarCorrectamente = "")
        //{
        //    string claveError = mensajes.clave_retorno.ToString();
        //    string mensajeError = mensajes.mensaje_retorno.ToString();
        //    if (claveError == "-1000")
        //    {
        //        if (muestramsjAlGuardarCorrectamente)
        //        {
        //            if (msjAMostrarAlguardarCorrectamente == "")
        //                Utileria.DisplayMessageRadNotification(1, mensajeError, rnMensaje);
        //            else
        //                Utileria.DisplayMessageRadNotification(1, msjAMostrarAlguardarCorrectamente, rnMensaje);
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        Utileria.DisplayMessageRadNotification(0, mensajeError, rnMensaje);
        //        return false;
        //    }
        //}

        //public static void muestraMsjSeleccionaRegistro(RadWindowManager rnMensaje)
        //{
        //     Utileria.DisplayMessageRadNotification(-1, "Seleccione al menos un registro", rnMensaje);
        //}
        //////////////////////////
        //public static bool ValidaQueContengaDatos(RadComboBox objeto, ref string ContenedordeMsj, string mensajeError, ref bool centinela)
        //{
        //    var centi = true;
        //    if ((objeto.SelectedValue == null) || (objeto.SelectedValue.Trim() == ""))
        //    {
        //        ContenedordeMsj += mensajeError;
        //        centinela = false;
        //        centi = false;
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(RadNumericTextBox objeto, ref string ContenedordeMsj, string mensajeError, ref bool centinela, bool validarcero = false)
        //{
        //    var centi = true;
        //    if ((objeto.Value == null))
        //    {
        //        ContenedordeMsj += mensajeError;
        //        centinela = false;
        //        centi = false;
        //    }
        //    else if (validarcero)
        //    {
        //        if (objeto.Value == 0)
        //        {
        //            ContenedordeMsj += mensajeError;
        //            centinela = false;
        //            centi = false;
        //        }
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(RadAsyncUpload objeto, ref string ContenedordeMsj, string mensajeError, ref bool centinela, bool validarcero = false)
        //{
        //    var centi = true;
        //    if ((objeto.UploadedFiles.Count < 1))
        //    {
        //        ContenedordeMsj += mensajeError;
        //        centinela = false;
        //        centi = false;
        //    }            
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(RadMaskedTextBox objeto, ref string ContenedordeMsj, string mensajeError, ref bool centinela)
        //{
        //    var centi = true;
        //    if ((objeto.Text == null) || (objeto.Text.Trim() == ""))
        //    {
        //        ContenedordeMsj += mensajeError;
        //        centinela = false;
        //        centi = false;
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(RadTextBox objeto, ref string ContenedordeMsj, string mensajeError, ref bool centinela)
        //{
        //    var centi = true;
        //    if ((objeto.Text == null) || (objeto.Text.Trim() == ""))
        //    {
        //        ContenedordeMsj += mensajeError;
        //        centinela = false;
        //        centi = false;
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(RadDatePicker objeto, ref string ContenedordeMsj, string mensajeError, ref bool centinela)
        //{
        //    var centi = true;
        //    if ((objeto.SelectedDate == null))
        //    {
        //        ContenedordeMsj += mensajeError;
        //        centinela = false;
        //        centi = false;
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(string objeto, ref string ContenedordeMsj, string mensajeError, ref bool centinela)
        //{
        //    var centi = true;
        //    if ((objeto.Trim() == ""))
        //    {
        //        ContenedordeMsj += mensajeError;
        //        centinela = false;
        //        centi = false;
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(DateTime? objeto, ref string ContenedordeMsj, string mensajeError, ref bool centinela)
        //{
        //    var centi = true;
        //    if ((objeto == null))
        //    {
        //        ContenedordeMsj += mensajeError;
        //        centinela = false;
        //        centi = false;
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(RadButton objeto, ref string ContenedordeMsj, string mensajeError, ref bool centinela)
        //{
        //    var centi = true;
        //    if ((objeto == null))
        //    {
        //        ContenedordeMsj += mensajeError;
        //        centinela = false;
        //        centi = false;
        //    }
        //    return centi;
        //}
        //////////////////////////
        //public static bool ValidaQueContengaDatos(RadComboBox objeto)
        //{
        //    var centi = true;
        //    if (objeto == null)
        //        centi = false;
        //    else
        //    if ((objeto.SelectedValue == null) || (objeto.SelectedValue.Trim() == ""))
        //    {               
        //        centi = false;
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(RadNumericTextBox objeto,bool validarcero = false)
        //{
        //    var centi = true;
        //    if (objeto == null)
        //        centi = false;
        //    else
        //    if ((objeto.Value == null))
        //    {
        //        centi = false;
        //    }
        //    else if (validarcero)
        //    {
        //        if (objeto.Value == 0)
        //        {
        //            centi = false;
        //        }
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(RadAsyncUpload objeto)
        //{
        //    var centi = true;
        //    if (objeto == null)
        //        centi = false;
        //    else
        //    if ((objeto.UploadedFiles.Count < 1))
        //    {
        //        centi = false;
        //    }            
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(RadMaskedTextBox objeto)
        //{
        //    var centi = true;
        //    if (objeto == null)
        //        centi = false;
        //    else
        //    if ((objeto.Text == null) || (objeto.Text.Trim() == ""))
        //    {
        //        centi = false;
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(RadTextBox objeto)
        //{
        //    var centi = true;
        //    if (objeto == null)
        //        centi = false;
        //    else
        //    if ((objeto.Text == null) || (objeto.Text.Trim() == ""))
        //    {
        //        centi = false;
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(RadDatePicker objeto)
        //{
        //    var centi = true;
        //    if (objeto == null)
        //        centi = false;
        //    else
        //    if ((objeto.SelectedDate == null))
        //    {
        //        centi = false;
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(string objeto)
        //{
        //    var centi = true;
        //    if ((objeto.Trim() == ""))
        //    {
        //        centi = false;
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(DateTime? objeto)
        //{
        //    var centi = true;
        //    if ((objeto == null))
        //    {
        //        centi = false;
        //    }
        //    return centi;
        //}
        //////////////////////////
        //public static bool ValidaQueContengaDatos(Hashtable objeto, string Columna)
        //{
        //    var centi = true;
        //    if ((objeto[Columna] == null))
        //    { 
        //        centi = false;
        //    } else
        //    if (objeto[Columna].ToString() == "")
        //    { 
        //        centi = false; 
        //    }
        //    return centi;
        //}
        //public static bool ValidaQueContengaDatos(Hashtable objeto, string Columna, ref string strValorContenido)
        //{
        //    var centi = true;
        //    strValorContenido = "";
        //    if ((objeto[Columna] == null))
        //    { 
        //        centi = false;
        //    } else
        //    if (objeto[Columna].ToString() == "")
        //    { 
        //        centi = false; 
        //    }
        //    else 
        //    { 
        //        strValorContenido = objeto[Columna].ToString(); 
        //    }
        //    return centi;
        //}

        //public static bool ValidaQueContengaDatos(Hashtable objeto, string Columna, ref string strValorContenido,ref string ContenedordeMsj, string mensajeError, ref bool centinela)
        //{
        //    var centi = true;
        //    strValorContenido = "";
        //    if ((objeto[Columna] == null))
        //    { 
        //        ContenedordeMsj += mensajeError;
        //        centinela = false;
        //        centi = false;
        //    } else
        //    if (objeto[Columna].ToString() == "")
        //    { 
        //        ContenedordeMsj += mensajeError;
        //        centinela = false;
        //        centi = false; 
        //    }
        //    else 
        //    { 
        //        strValorContenido = objeto[Columna].ToString(); 
        //    }
        //    return centi;
        //}

        //////////////////////////
        //public static void MuestraMensajeFaltaCamposObligados(string ContenedordeMsj, bool centinela, RadWindowManager rnMensaje, string mensajeprincipal = "")
        //{
        //    if (!centinela)
        //    {
        //        if (mensajeprincipal.Trim() == "")
        //            ContenedordeMsj = "Hace falta llenar los siguientes datos obligatorios: " + ContenedordeMsj.Trim();
        //        else
        //            ContenedordeMsj = mensajeprincipal + ContenedordeMsj.Trim();
        //        ContenedordeMsj = ContenedordeMsj.Substring(0, ContenedordeMsj.Length - 1);
        //        DisplayMessageRadNotification(-1, ContenedordeMsj, rnMensaje);
        //    }

        //}

        //public static bool IsNumeric(this string s)
        //{
        //    float output;
        //    return float.TryParse(s, out output);
        //}

        ///// <summary>
        ///// Convierte el arreglo de bytes buffer en un DataTable.
        ///// </summary>
        ///// <param name="ArchivoByte">Arreglo de bytes a ser convertido.</param>
        ///// <param name="IncluyeEncabezado">True si es que dentro del buffer se encuentra la fila de encabezado.</param>
        ///// <returns>Datatable con los datos</returns>
        //public static DataTable ConvertirArchivoByteEnDatatable(byte[] ArchivoByte, bool IncluyeEncabezado)
        //{
        //    DataTable result = new DataTable();

        //    // Se asume que el separador de decimales es punto "." y el de miles "," (aunque este ultimo no se usa) 
        //    CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentCulture;
        //    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        //    Dictionary<string, int> indexs = new Dictionary<string, int>();
        //    DataTable dt = new DataTable();
        //    char[] delimiter = new char[] { ';' };
        //    char[] zero = new char[] { '0' };

        //    using (StreamReader sr = new StreamReader(new MemoryStream(ArchivoByte)))
        //    {
        //        try
        //        {
        //            int rowsCompleted = 0;
        //            int lastLength = 0;
        //            bool readHeader = true;

        //            while (sr.Peek() > -1)
        //            {
        //                bool addLine = true;
        //                string line = sr.ReadLine();
        //                string[] lineArray = line.Split(delimiter);

        //                //Se chequea que tanto el orden como el nombre de las columnas correspondan según el orden dado
        //                if (readHeader)
        //                {
        //                    if (IncluyeEncabezado)
        //                    {
        //                        int j = 0;
        //                        foreach (string column in lineArray)
        //                        {
        //                            DataColumn c = new DataColumn(column);
        //                            dt.Columns.Add(c);
        //                            indexs.Add(column, j);
        //                            j++;
        //                        }
        //                        //Se continua con la lectura del archivo
        //                        line = sr.ReadLine();
        //                        lineArray = line.Split(delimiter);
        //                    }
        //                    else
        //                    {
        //                        //Agrego columnas con nombre estandar, no se pasa a la siguiente linea del doc
        //                        for (int j = 0; j < lineArray.Length; j++)
        //                        {
        //                            DataColumn c = new DataColumn("Column" + j);
        //                            dt.Columns.Add(c);
        //                            indexs.Add("Column" + j, j);
        //                        }
        //                    }
        //                    //Se cambia el estado de esta variable para no volver a chequear el header
        //                    readHeader = false;
        //                }

        //                DataRow nuevaFila = dt.NewRow();
        //                if (lastLength > 0 && lastLength != lineArray.Length)
        //                {
        //                    continue;
        //                }
        //                lastLength = lineArray.Length;
        //                try
        //                {
        //                    foreach (DataColumn column in dt.Columns)
        //                    {
        //                        int index = indexs[column.ColumnName];
        //                        string colName = column.ColumnName;
        //                        string value = lineArray[index];
        //                        nuevaFila[colName] = string.IsNullOrEmpty(value) ? DBNull.Value + "" : value;
        //                    }
        //                }
        //                catch (Exception e)
        //                {
        //                    throw e;
        //                }
        //                if (addLine)
        //                {
        //                    dt.Rows.Add(nuevaFila);
        //                    rowsCompleted++;
        //                }
        //            }
        //            return dt;
        //        }
        //        finally
        //        {
        //            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        //        }
        //    }
        //}

        //public static void DescargarArchivo(string Ruta)
        //ejemplo de uso: DescargarArchivo("~/" + nombrearchivo + ".txt");
        //{
        //  System.IO.FileInfo toDownload = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(Ruta));
        //  HttpContext.Current.Response.Clear();
        //  HttpContext.Current.Response.AddHeader("Content-Disposition","attachment; filename=" + toDownload.Name);
        //  HttpContext.Current.Response.AddHeader("Content-Length",toDownload.Length.ToString());
        //  HttpContext.Current.Response.ContentType = "application/octet-stream";
        //  HttpContext.Current.Response.WriteFile(Ruta);
        //  HttpContext.Current.Response.End();
        //} 


    }
}
