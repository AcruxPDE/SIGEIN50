using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using WebApp.Comunes;

namespace SIGE.WebApp.Comunes
{
    public class ControlDinamico
    {
        public Control CtrlLabel { get; set; }
        public Control CtrlControl { get; set; }
        Plantilla plantilla;

        public string ClTipoControl;
        public string IdControl;
        public string NbControl;
        public string NbValor;
        public string NbValorDefecto;
        public string NbTooltip;
        public bool FgHabilitado;
        public bool FgRequerido;
        public XElement XmlCampo;
        public int NoLongitud;
        public int NoAncho;
        public string MsgRequerido = "* ";
        public string NbBoton = "btn";
        public string NbBotonAgregar = "btnAdd";
        public string NbBotonEliminar = "btnDel";
        public string NbBotonEditar = "btnEdit";
        public string NbBotonCancelar = "btnCancelar";
        public Unit DefaultControlUnitWidth;
        public Unit DefaultControlUnitHeight;
        public Unit DefaultLabelUnitWidth;
        public bool FgMultiLinea;

        public ControlDinamico(XElement pXmlControl, bool pFgAddValue, int pDefaultWidth = 200, int pDefaultHeight = 34, int pDefaultLabelWidth = 150, bool pFgCreateControl = true)
        {
            ClTipoControl = UtilXML.ValorAtributo<string>(pXmlControl.Attribute("CL_TIPO"));
            IdControl = UtilXML.ValorAtributo<string>(pXmlControl.Attribute("ID_CAMPO"));
            NbControl = UtilXML.ValorAtributo<string>(pXmlControl.Attribute("NB_CAMPO"));
            NbValor = UtilXML.ValorAtributo<string>(pXmlControl.Attribute("NB_VALOR"));
            NbValorDefecto = UtilXML.ValorAtributo<string>(pXmlControl.Attribute("NO_VALOR_DEFECTO"));
            NbTooltip = UtilXML.ValorAtributo<string>(pXmlControl.Attribute("NB_TOOLTIP"));
            FgHabilitado = UtilXML.ValorAtributo<bool>(pXmlControl.Attribute("FG_HABILITADO"));
            FgRequerido = UtilXML.ValorAtributo<bool>(pXmlControl.Attribute("FG_REQUERIDO"));
            NoLongitud = UtilXML.ValorAtributo<int>(pXmlControl.Attribute("NO_LONGITUD"));
            FgMultiLinea = UtilXML.ValorAtributo<int>(pXmlControl.Attribute("FG_MULTILINEA")) == 0 ? false : true;
            XmlCampo = pXmlControl;

            int? vNoAncho = UtilXML.ValorAtributo<int?>(pXmlControl.Attribute("NO_ANCHO"));
            int? vNoLargo = UtilXML.ValorAtributo<int?>(pXmlControl.Attribute("NO_LARGO"));

            DefaultControlUnitWidth = new Unit(vNoAncho ?? pDefaultWidth);
            DefaultLabelUnitWidth = new Unit(pDefaultWidth);

            DefaultControlUnitHeight = new Unit(vNoLargo ?? pDefaultHeight);

            if (pFgCreateControl)
            {
                CtrlControl = CrearControl(pXmlControl, pFgAddValue);
                CtrlLabel = (ClTipoControl != "GRID") ? CrearLabel() : null;
            }
        }

        protected Control CrearLabel()
        {
            HtmlGenericControl vControlLabel = null;
            if (!String.IsNullOrWhiteSpace(NbControl))
            {
                vControlLabel = new HtmlGenericControl("span");
                vControlLabel.InnerText = String.Format("{0}{1}:", FgRequerido ? MsgRequerido : String.Empty, NbControl);
            }
            return vControlLabel;
        }

        protected Control CrearControl(XElement pXmlControl, bool pFgAddValue)
        {
            HtmlGenericControl vControlHTML = null;

            Control vControl = null;
            switch (ClTipoControl)
            {
                case "TEXTBOX":
                    vControl = new RadTextBox()
                    {
                        ID = IdControl,
                        ToolTip = NbTooltip,
                        Text = NbValor != null? NbValor : NbValorDefecto,
                        Width = DefaultControlUnitWidth,
                        Height = DefaultControlUnitHeight,
                        Enabled = FgHabilitado,
                        MaxLength = NoLongitud,
                        Rows = FgMultiLinea ? 4 : 1,
                        TextMode = FgMultiLinea ? InputMode.MultiLine : InputMode.SingleLine
                    };
                    break;
                case "LABEL":
                    vControl = new RadLabel()
                    {
                        ID = IdControl,
                        ToolTip = NbTooltip,
                        Enabled = FgHabilitado
                    };
                    break;
                case "NUMERICBOX":
                    decimal? vNoValor = UtilXML.ValorAtributo<decimal?>(pXmlControl.Attribute("NB_VALOR"));
                    decimal? vNbValorDefecto = UtilXML.ValorAtributo<decimal?>(pXmlControl.Attribute("NO_VALOR_DEFECTO"));
                    int? vNoDecimales = UtilXML.ValorAtributo<int>(pXmlControl.Attribute("NO_DECIMALES"));
                    double vNbValorFinal = vNoValor == null ? ( vNbValorDefecto == null ? 0 : (double)vNbValorDefecto ): (double)vNoValor; 

                    RadNumericTextBox vCtrlNumericBox = new RadNumericTextBox()
                    {
                        ID = IdControl,
                        ToolTip = NbTooltip,
                        DataType = typeof(decimal),
                        Value = vNbValorFinal,
                        Width = DefaultControlUnitWidth,
                        Enabled = FgHabilitado
                    };

                    vCtrlNumericBox.NumberFormat.DecimalDigits = vNoDecimales ?? 0;
                    vCtrlNumericBox.ShowSpinButtons = true;
                    vControl = vCtrlNumericBox;
                    break;
                case "MASKBOX":
                    string vNbMask = UtilXML.ValorAtributo<string>(pXmlControl.Attribute("NB_MASCARA"));
                    vControl = new RadMaskedTextBox()
                    {
                        ID = IdControl,
                        ToolTip = NbTooltip,
                        Text = NbValor,
                        Width = DefaultControlUnitWidth,
                        Enabled = FgHabilitado,
                        Mask = vNbMask,
                        MaxLength = NoLongitud
                    };
                    break;
                case "DATEPICKER":
                    if (!String.IsNullOrWhiteSpace(NbValor))
                    {
                        string[] vFecha = NbValor.Split('/');
                        int vDia = int.Parse(vFecha[0]);
                        int vMes = int.Parse(vFecha[1]);
                        int vAnio = int.Parse(vFecha[2]);
                        vControl = new RadDatePicker();

                        RadDatePicker vControlFecha = new RadDatePicker()
                        {
                            ID = IdControl,
                            ToolTip = NbTooltip,
                            MinDate = new DateTime(1000, 1, 1),
                            SelectedDate = new DateTime(vAnio, vMes, vDia),
                            
                            Width = DefaultControlUnitWidth,
                            Enabled = FgHabilitado,
                        };


                        vControlFecha.DateInput.DateFormat = "dd/MM/yyyy";
                        vControlFecha.DateInput.DisplayDateFormat = "dd/MM/yyyy";
                        vControlFecha.DateInput.EmptyMessage = "dd/MM/yyyy";
                        vControl = vControlFecha;
                    }
                    else
                    {
                        RadDatePicker vControlFecha = new RadDatePicker()
                        {
                            ID = IdControl,
                            ToolTip = NbTooltip,
                            MinDate = new DateTime(1000, 1, 1),
                            Width = 120,
                            Enabled = FgHabilitado,
                        };

                        if (!String.IsNullOrWhiteSpace(NbValorDefecto))
                        {
                            string[] vFecha = NbValorDefecto.Split('/');
                            int vDia = int.Parse(vFecha[0]);
                            int vMes = int.Parse(vFecha[1]);
                            int vAnio = int.Parse(vFecha[2]);

                            vControlFecha.SelectedDate = new DateTime(vAnio, vMes, vDia);
                        }
                        else
                        {
                            vControlFecha.DateInput.DateFormat = "dd/MM/yyyy";
                            vControlFecha.DateInput.DisplayDateFormat = "dd/MM/yyyy";
                            vControlFecha.DateInput.EmptyMessage = "dd/MM/yyyy";
                        }

                        vControl = vControlFecha;
                    }
                    break;

                case "DATEAGE":

                    int vCtrlEdadWidth = 80;

                    if (!String.IsNullOrWhiteSpace(NbValor))
                    {
                        string[] vFechaEdad = NbValor.Split('/');
                        int vDiaEdad = int.Parse(vFechaEdad[0]);
                        int vMesEdad = int.Parse(vFechaEdad[1]);
                        int vAnioEdad = int.Parse(vFechaEdad[2]);

                        string vIdControlEdad = String.Format("txt{0}", IdControl);
                        DateTime vDateEdad = new DateTime(vAnioEdad, vMesEdad, vDiaEdad);


                        RadDatePicker vCtrlDate = new RadDatePicker()
                        {
                            ID = IdControl,
                            ToolTip = NbTooltip,
                            MinDate = new DateTime(1000, 1, 1),
                            SelectedDate = vDateEdad,
                            Width = new Unit(DefaultControlUnitWidth.Value - vCtrlEdadWidth),
                            Enabled = FgHabilitado,
                            AutoPostBack = true,

                        };

                        vCtrlDate.DateInput.DateFormat = "dd/MM/yyyy";
                        vCtrlDate.DateInput.DisplayDateFormat = "dd/MM/yyyy";
                        vCtrlDate.DateInput.EmptyMessage = "dd/MM/yyyy";

                        RadTextBox vCtrlEdad = new RadTextBox()
                        {
                            ID = vIdControlEdad,
                            Text = ObtenerEdad(vDateEdad),
                            Width = new Unit(vCtrlEdadWidth),
                            ReadOnly = true,
                        };

                        vCtrlDate.SelectedDateChanged += (sender, e) => CalcularEdad(sender, e, vCtrlEdad);

                        HtmlGenericControl vContenedorDatePickerAge = new HtmlGenericControl("span");
                        vContenedorDatePickerAge.Controls.Add(vCtrlDate);
                        vContenedorDatePickerAge.Controls.Add(vCtrlEdad);

                        vControl = vContenedorDatePickerAge;
                    }
                    else
                    {
                        RadDatePicker vCtrlDate = new RadDatePicker()
                        {
                            ID = IdControl,
                            ToolTip = NbTooltip,
                            MinDate = new DateTime(1000, 1, 1),
                            Width = new Unit(DefaultControlUnitWidth.Value - vCtrlEdadWidth),
                            Enabled = FgHabilitado,
                            AutoPostBack = true,

                        };
                        RadTextBox vCtrlEdad;

                        if (!String.IsNullOrWhiteSpace(NbValorDefecto))
                        {
                            string[] vFechaEdad = NbValorDefecto.Split('/');
                            int vDiaEdad = int.Parse(vFechaEdad[0]);
                            int vMesEdad = int.Parse(vFechaEdad[1]);
                            int vAnioEdad = int.Parse(vFechaEdad[2]);

                            DateTime vDateEdad = new DateTime(vAnioEdad, vMesEdad, vDiaEdad);
                            vCtrlDate.SelectedDate = vDateEdad;

                            string vIdControlEdad2 = String.Format("txt{0}", IdControl);
                           vCtrlEdad = new RadTextBox()
                            {
                                ID = vIdControlEdad2,
                                Text = ObtenerEdad(vDateEdad),
                                Width = new Unit(vCtrlEdadWidth),
                                ReadOnly = true,
                            };
                        }
                        else
                        {
                            vCtrlDate.DateInput.DateFormat = "dd/MM/yyyy";
                            vCtrlDate.DateInput.DisplayDateFormat = "dd/MM/yyyy";
                            vCtrlDate.DateInput.EmptyMessage = "dd/MM/yyyy";

                            string vIdControlEdad2 = String.Format("txt{0}", IdControl);
                           vCtrlEdad = new RadTextBox()
                            {
                                ID = vIdControlEdad2,
                                Text = "",
                                Width = new Unit(vCtrlEdadWidth),
                                ReadOnly = true,
                            };
                        }

                        vCtrlDate.SelectedDateChanged += (sender, e) => CalcularEdad(sender, e, vCtrlEdad);

                        HtmlGenericControl vContenedorDatePickerAge = new HtmlGenericControl("span");
                        vContenedorDatePickerAge.Controls.Add(vCtrlDate);
                        vContenedorDatePickerAge.Controls.Add(vCtrlEdad);

                        vControl = vContenedorDatePickerAge;
                    }
                    break;
                case "COMBOBOX":
                    vControl = new RadComboBox()
                    {
                        ID = IdControl,
                        ToolTip = NbTooltip,
                        Width = DefaultControlUnitWidth,
                        Enabled = FgHabilitado,
                        Filter = RadComboBoxFilter.Contains,
                        EmptyMessage = "Selecciona"
                    };

                    if (pXmlControl.Element("ITEMS") != null)
                        foreach (XElement item in pXmlControl.Element("ITEMS").Elements("ITEM"))
                            ((RadComboBox)vControl).Items.Add(new RadComboBoxItem()
                            {
                                Text = UtilXML.ValorAtributo<string>(item.Attribute("NB_TEXTO")),
                                Value = UtilXML.ValorAtributo<string>(item.Attribute("NB_VALOR")),
                                Selected = UtilXML.ValorAtributo<bool>(item.Attribute("FG_SELECCIONADO"))
                            });


                    break;
                case "TEXTBOXCP":
                    RadTextBox vCodigoPistal = new RadTextBox()
                    {
                        ID = IdControl,
                        Text = NbValor,
                        Enabled = FgHabilitado,
                        Width = new Unit(DefaultControlUnitWidth.Value - 30),
                        EnableViewState = true,
                        MaxLength = 5
                    };

                    RadButton vCtrlBtnBuscar = new RadButton()
                    {
                        ID = String.Format("{1}{0}", IdControl, NbBoton),
                        Text = "B",
                        AutoPostBack = false,
                        OnClientClicked = "OpenSelectionWindow"
                    };

                    HtmlGenericControl vContenedorCP = new HtmlGenericControl("span");
                    vContenedorCP.Controls.Add(vCodigoPistal);
                    vContenedorCP.Controls.Add(vCtrlBtnBuscar);

                    vControl = vContenedorCP;
                    break;
                case "LISTBOX":
                    string vClValor = UtilXML.ValorAtributo<string>(pXmlControl.Attribute("CL_VALOR"));
                    RadListBox vCtrlListBox = new RadListBox()
                    {

                        ID = IdControl,
                        ToolTip = NbTooltip,
                        Enabled = FgHabilitado,
                        Width = new Unit(DefaultControlUnitWidth.Value - 30),
                        EnableViewState = true,
                        EmptyMessage = "Selecciona",
                        OnClientLoad = "LoadValue"
                    };

                    if (pFgAddValue)
                    {
                        RadListBoxItem vListBoxItem = new RadListBoxItem(NbValor, vClValor);
                        vListBoxItem.Selected = true;
                        vCtrlListBox.Items.Add(vListBoxItem);
                    }

                    RadButton vCtrlBtnSearch = new RadButton()
                    {
                        ID = String.Format("{1}{0}", IdControl, NbBoton),
                        Text = "B",
                        AutoPostBack = false,
                        OnClientClicked = "OpenSelectionWindow"
                    };

                    HtmlGenericControl vListBox = new HtmlGenericControl("span");
                    vListBox.Controls.Add(vCtrlListBox);
                    vListBox.Controls.Add(vCtrlBtnSearch);
                    vControl = vListBox;
                    break;
                case "CHECKBOX":
                    bool? vFgChecked = UtilXML.ValorAtributo<bool?>(pXmlControl.Attribute("NB_VALOR"));
                    bool? vFgCheckedDefecto = UtilXML.ValorAtributo<bool?>(pXmlControl.Attribute("NO_VALOR_DEFECTO"));
                    bool? vFgCheckedFinal = vFgChecked == null && vFgCheckedDefecto != null? vFgCheckedDefecto : vFgChecked != null? vFgChecked : false;

                    RadButton vCtrlCheckBox = new RadButton()
                    {
                        ID = IdControl,
                        ToolTip = NbTooltip,
                        Enabled = FgHabilitado,
                        Checked = (bool)vFgCheckedFinal,
                        ToggleType = ButtonToggleType.CheckBox,
                        ButtonType = RadButtonType.StandardButton,
                        AutoPostBack = false
                    };

                    vCtrlCheckBox.ToggleStates.Add(new RadButtonToggleState("Sí") { PrimaryIconCssClass = "rbToggleCheckboxChecked" });
                    vCtrlCheckBox.ToggleStates.Add(new RadButtonToggleState("No") { PrimaryIconCssClass = "rbToggleCheckbox" });
                    vControl = vCtrlCheckBox;
                    

                    /*
                    RadButton vCtrlCheckBoxYes = new RadButton()
                    {
                        ID = IdControl + "True",
                        ToolTip = NbTooltip,
                        Enabled = FgHabilitado,
                        Checked =  vFgChecked,
                        ToggleType = ButtonToggleType.Radio,
                        ButtonType = RadButtonType.StandardButton,
                        AutoPostBack = false,
                        GroupName = IdControl
                    };

                    

                    vCtrlCheckBoxYes.ToggleStates.Add(new RadButtonToggleState("Si") { CssClass = "checkedYes" });
                    vCtrlCheckBoxYes.ToggleStates.Add(new RadButtonToggleState("Si") { CssClass = "uncheckedYes" });

                    RadButton vCtrlCheckBoxNo = new RadButton()
                    {
                        ID = IdControl + "False",
                        ToolTip = NbTooltip,
                        Enabled = FgHabilitado,
                        Checked = !vFgChecked,
                        ToggleType = ButtonToggleType.Radio,
                        ButtonType = RadButtonType.StandardButton,
                        AutoPostBack = false,
                        GroupName = IdControl
                    };

                    vCtrlCheckBoxNo.ToggleStates.Add(new RadButtonToggleState("No") { CssClass = "checkedNo" });
                    vCtrlCheckBoxNo.ToggleStates.Add(new RadButtonToggleState("No") { CssClass = "uncheckedNo" });

                    HtmlGenericControl vContenedorCheckBox = new HtmlGenericControl("span");

                    vContenedorCheckBox.Controls.Add(vCtrlCheckBoxYes);
                    vContenedorCheckBox.Controls.Add(vCtrlCheckBoxNo);

                    vControl = vContenedorCheckBox;
                    */
                    break;

                case "GRID":
                    HtmlGenericControl vControlGrid = new HtmlGenericControl("div");

                    HtmlGenericControl vGridLabel = new HtmlGenericControl("label");
                    vGridLabel.Attributes.Add("class", "labelTitulo");
                    vGridLabel.InnerText = NbControl;

                    vControlGrid.Controls.Add(vGridLabel);

                    //Aqui se crea el formulario para el grid
                    foreach (XElement xXmlControlFormularioGrid in pXmlControl.Element("FORMULARIO").Elements("CAMPO"))
                    {
                        ControlDinamico vControlGridFormulario = new ControlDinamico(xXmlControlFormularioGrid, true);
                        if (vControlGridFormulario.CtrlControl != null)
                        {
                            HtmlGenericControl vContenedorControlGridFormulario = new HtmlGenericControl("div");
                            vContenedorControlGridFormulario.Attributes.Add("class", "ctrlBasico");
                                vContenedorControlGridFormulario.Controls.Add(vControlGridFormulario.CtrlLabel);
                            vContenedorControlGridFormulario.Controls.Add(new LiteralControl("<br />"));
                            vContenedorControlGridFormulario.Controls.Add(vControlGridFormulario.CtrlControl);
                            vControlGrid.Controls.Add(vContenedorControlGridFormulario);
                        }
                    }

                    //Se crea y se agrega el botón de agregar
                    RadButton vCtrlBtnAdd = new RadButton()
                    {
                        ID = String.Format("{1}{0}", IdControl, NbBotonAgregar),
                        Text = "Agregar",
                        Enabled = FgHabilitado,
                        AutoPostBack = true
                    };


                    /* AsignarControlAjax */

                    HtmlGenericControl vContenedorBotonAgregarItem = new HtmlGenericControl("div");
                    vContenedorBotonAgregarItem.Attributes.Add("class", "ctrlBasico");
                    vContenedorBotonAgregarItem.Controls.Add(new LiteralControl("<br />"));
                    vContenedorBotonAgregarItem.Controls.Add(vCtrlBtnAdd);

                    vControlGrid.Controls.Add(vContenedorBotonAgregarItem);

                    //Se crea y se agrega el botón de cancelar. Falta ocultarlo
                    RadButton vCtrlBtnCancelar = new RadButton()
                    {
                        ID = String.Format("{1}{0}", IdControl, NbBotonCancelar),
                        Text = "Limpiar"
                    };

                    HtmlGenericControl vContenedorBotonCancelarItem = new HtmlGenericControl("div");
                    vContenedorBotonCancelarItem.Attributes.Add("class", "ctrlBasico");
                    vContenedorBotonCancelarItem.Controls.Add(new LiteralControl("<br />"));
                    vContenedorBotonCancelarItem.Controls.Add(vCtrlBtnCancelar);

                    vControlGrid.Controls.Add(vContenedorBotonCancelarItem);

                    //Creamos, configuramos y agregamos el radgrid
                    int? vAnchoGrid = UtilXML.ValorAtributo<int?>(pXmlControl.Element("GRID").Attribute("NO_ANCHO"));

                    RadGrid vGrid = new RadGrid()
                    {
                        ID = IdControl,
                        Width = vAnchoGrid != null ? (Unit)vAnchoGrid : DefaultControlUnitWidth,
                        AutoGenerateColumns = false,
                    };

                    DataTable dataTable = new DataTable();
                    List<string> lstDataKeyNames = new List<string>();
                    foreach (XElement vXmlColumna in pXmlControl.Element("GRID").Element("HEADER").Elements("COLUMNA"))
                    {

                        dataTable.Columns.Add(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA")), Utileria.ObtenerTipoDato(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("CL_TIPO_DATO"))));

                        if (UtilXML.ValorAtributo<bool>(vXmlColumna.Attribute("FG_DATAKEYVALUE")))
                        {
                            lstDataKeyNames.Add(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA")));
                        }
                        else
                        {
                            int? vNoAncho = UtilXML.ValorAtributo<int?>(vXmlColumna.Attribute("NO_ANCHO"));

                            GridBoundColumn vColumn = new GridBoundColumn();
                            vColumn.HeaderText = UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("NB_COLUMNA"));
                            vColumn.DataField = UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA"));
                            vColumn.HeaderStyle.Font.Bold = true;

                            if (vNoAncho != null)
                                vColumn.HeaderStyle.Width = new Unit((int)vNoAncho);

                            switch (UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("CL_TIPO_DATO")))
                            {
                                case "DATE":
                                    vColumn.DataFormatString = "{0:d}";
                                    vColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                    break;
                                case "INT":
                                    vColumn.DataFormatString = "{0:N}";
                                    vColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                    break;
                            }

                            if (UtilXML.ValorAtributo<bool>(vXmlColumna.Attribute("FG_VISIBLE")))
                            {
                                vColumn.Visible = true;
                            }
                            else
                            {
                                vColumn.Visible = false;
                            }


                            vGrid.Columns.Add(vColumn);
                        }


                        /*

                        dataTable.Columns.Add(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA")), Utileria.ObtenerTipoDato(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("CL_TIPO_DATO"))));
                        if (UtilXML.ValorAtributo<bool>(vXmlColumna.Attribute("FG_VISIBLE")))
                        {
                            int? vNoAncho = UtilXML.ValorAtributo<int?>(vXmlColumna.Attribute("NO_ANCHO"));

                            GridBoundColumn vColumn = new GridBoundColumn();
                            vColumn.HeaderText = UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("NB_COLUMNA"));
                            vColumn.DataField = UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA"));
                            
                            if (vNoAncho != null)
                                vColumn.HeaderStyle.Width = new Unit((int)vNoAncho);

                            switch (UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("CL_TIPO_DATO")))
                            {
                                case "DATE":
                                    vColumn.DataFormatString = "{0:d}";
                                    vColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                    break;
                                case "INT":
                                    vColumn.DataFormatString = "{0:N}";
                                    vColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                    break;
                            }

                            vGrid.Columns.Add(vColumn);
                        }
                        if (UtilXML.ValorAtributo<bool>(vXmlColumna.Attribute("FG_DATAKEYVALUE")))
                            lstDataKeyNames.Add(UtilXML.ValorAtributo<string>(vXmlColumna.Attribute("ID_COLUMNA")));
                         * 
                         * */
                    }

                    vGrid.MasterTableView.DataKeyNames = lstDataKeyNames.ToArray();
                    vGrid.ClientSettings.Selecting.AllowRowSelect = FgHabilitado;

                    vGrid.DataSource = dataTable;

                    vControlGrid.Controls.Add(new LiteralControl("<div style='clear:both;'></div>"));
                    HtmlGenericControl vContenedorControlGrid = new HtmlGenericControl("div");
                    vContenedorControlGrid.Attributes.Add("class", "ctrlBasico");
                    vContenedorControlGrid.Controls.Add(vGrid);
                    vControlGrid.Controls.Add(vContenedorControlGrid);


                    //agregamos el botón de editar y eliminar
                    RadButton vCtrlBtnEdit = new RadButton()
                    {
                        ID = String.Format("{1}{0}", IdControl, NbBotonEditar),
                        Text = "Editar",
                        Enabled = FgHabilitado
                    };

                    RadButton vCtrlBtnDel = new RadButton()
                    {
                        ID = String.Format("{1}{0}", IdControl, NbBotonEliminar),
                        Text = "Eliminar",
                        Enabled = FgHabilitado
                    };

                    vControlGrid.Controls.Add(vCtrlBtnEdit);
                    vControlGrid.Controls.Add(vCtrlBtnDel);

                    vControl = vControlGrid;
                    break;
            }

            if (vControl != null)
            {
                vControlHTML = new HtmlGenericControl("span");
                vControlHTML.Controls.Add(vControl);
            }

            return vControlHTML;
        }

        protected void CalcularEdad(object sender, EventArgs e, RadTextBox pCtrlEdad)
        {
            RadDatePicker vControlF = new RadDatePicker();
            vControlF = (RadDatePicker)sender;
            string value = vControlF.DateInput.InvalidTextBoxValue;
            if (value == string.Empty && !vControlF.SelectedDate.Equals(null))
            {
                pCtrlEdad.Text = ObtenerEdad(((RadDatePicker)sender).SelectedDate.Value);
            }
        }

        protected string ObtenerEdad(DateTime pFeNacimiento)
        {
            DateTime vFechaObjetivo = pFeNacimiento;
            DateTime vFechaHoy = DateTime.Now;
            int vFactor = 1;

            if (vFechaObjetivo < vFechaHoy)
            {
                vFechaObjetivo = DateTime.Now;
                vFechaHoy = pFeNacimiento;
                vFactor = 0;
            }

            vFechaHoy = vFechaHoy.Date;
            vFechaObjetivo = vFechaObjetivo.Date;

            DateTime vDiferencia = new DateTime(vFechaObjetivo.Subtract(vFechaHoy).Ticks);
            int vNoAnios = vDiferencia.Year - 1;
            int vNoMeses = vDiferencia.Month - 1;
            int vNoDias = vDiferencia.Day - 1;

            if (vNoAnios != 0)
                return String.Format("{2}{0} año{1}", vNoAnios, vNoAnios != 1 ? "s" : String.Empty, vFactor == 0 ? "" : "En ");

            if (vNoMeses != 0)
                return String.Format("{2}{0} mes{1}", vNoMeses, vNoMeses != 1 ? "es" : String.Empty, vFactor == 0 ? "" : "En ");

            if (vNoDias != 0)
                return String.Format("{2}{0} día{1}", vNoDias, vNoDias != 1 ? "s" : String.Empty, vFactor == 0 ? "" : "En ");

            return "Hoy";
        }
    }
}