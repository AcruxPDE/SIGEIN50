using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Linq;
using Telerik.Web.UI;


namespace SIGE.WebApp.Administracion
{
    public partial class VentanaImportarEmpleados : System.Web.UI.Page
    {
        #region VARIABLES
        string vClUsuario = "";
        string vNbPrograma = "";
        string vClCliente = "";
        string clSistema = "SIGEIN";
        string clModulo = "NOMINA";
        string noVersion = "5.00";

        public List<E_MENSAJES> MensajesError
        {
            get { return (List<E_MENSAJES>)ViewState["vs_ip_errores"]; }
            set { ViewState["vs_ip_errores"] = value; }
        }
        #endregion


        #region Metodos

        private List<E_MENSAJES> validarRegistros(string[] row, int i)
        {
            List<E_MENSAJES> listaErrores = new List<E_MENSAJES>();

            /*VALIDAR DATOS OBLIGATORIOS*/
            if (row[0].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta clave de razón social." });

            if (row[2].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta nombre de trabajador." });

            if (row[3].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta el apellido paterno." });

            //if (row[4].ToString() == "")
            //    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta el apellido materno." });

            if (row[5].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la clave del sexo." });

            if (row[6].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la clave del estado civil." });

            if (row[7].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la clave de la nacionalidad." });

            if (row[8].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta RFC." });

            if (row[9].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta CURP." });

            if (row[14].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la fecha de nacimiento." });

            if (row[16].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la clave del estado de nacimiento." });

            if (row[27].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta cotiza IMSS." });

            if (row[36].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la clave del departamento." });

            if (row[37].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la clave del puesto." });

            if (row[38].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la clave del centro administrativo." });

            if (row[39].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la clave del centro operativo." });

            if (row[40].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la clave del paquete de prestaciones." });

            if (row[45].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la fecha de ingreso." });

            if (row[47].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la clave del tipo de nómina." });

            if (row[48].ToString() == "")
                listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta la clave de la forma de pago." });


            //if (row[50].ToString() == "")
            //    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta el sueldo nominal diario." });

            //if (row[52].ToString() == "")
            //    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta el FACTOR SBC." });


            //if (row[35].ToString() == "")
            //    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "Falta el Horario." });


            DateTime Comodin = new DateTime();

            /*FORMATOS DE FECHAS*/
            if (row[14].ToString() != "")
            {
                if (!DateTime.TryParse(row[14].ToString(), out Comodin))
                {
                    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "El formato de la fecha de nacimiento es incorrecto." });
                }
            }

            if (row[45].ToString() != "")
            {
                if (!DateTime.TryParse(row[45].ToString(), out Comodin))
                {
                    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "El formato de la fecha de ingreso o antigüedad es incorrecto." });
                }
            }

            if (row[46].ToString() != "")
            {
                if (!DateTime.TryParse(row[46].ToString(), out Comodin))
                {
                    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "El formato de la fecha de planta es incorrecto." });
                }
            }


            /* VALIDAR NUMEROS DE UN SOLO DIGITO */
            if (row[31].ToString() != "")
            {
                if (row[31].ToString().ToCharArray().Count() > 1)
                {
                    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "La clave del tipo de trabajo debe de ser de un solo dígito." });
                }
            }

            if (row[32].ToString() != "")
            {
                if (row[32].ToString().ToCharArray().Count() > 1)
                {
                    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "La clave de la jornada laboral debe de ser de un dígito." });
                }
            }

            if (row[34].ToString() != "")
            {
                if (row[34].ToString().ToCharArray().Count() > 1)
                {
                    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "La clave del tipo de salario debe de ser de un dígito." });
                }
            }

            /* VALIDAR NUMERO DE CUENTA Y CLABE*/
            Int64 numero;
            if (row[56].ToString() != "")
            {
                if (!Int64.TryParse(row[56].ToString(), out numero))
                {
                    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "El número de cuenta de pago no es un número." });
                }
            }

            if (row[57].ToString() != "")
            {
                if (!Int64.TryParse(row[56].ToString(), out numero))
                {
                    listaErrores.Add(new E_MENSAJES { CL_CLAVE_RETORNO = i.ToString(), NB_MENSAJE_RETORNO = "La clabe no es un número." });
                }
            }

            //plantilla.CL_TIPO_SALARIO_SUA = decimal.Parse(.ToString());

            return listaErrores;
        }

        private int numeroLineasEnArchivo(byte[] archivo)
        {
            int no_lineas = 0;

            using (StreamReader readfile = new StreamReader(new MemoryStream(archivo)))
            {
                while (readfile.ReadLine() != null)
                {
                    no_lineas++;
                }
            }

            return no_lineas;
        }

        private void importarEmpleados(bool EsValidacion)
        {
            if (RadAsyncUpload1.UploadedFiles.Count > 0)
            {
                int i = 0;
                int totalLineas;
                RadProgressContext progress = RadProgressContext.Current;

                MensajesError = new List<E_MENSAJES>();
                GridErrores.Rebind();

                byte[] archivo = new byte[RadAsyncUpload1.UploadedFiles[0].ContentLength];
                RadAsyncUpload1.UploadedFiles[0].InputStream.Read(archivo, 0, int.Parse(RadAsyncUpload1.UploadedFiles[0].ContentLength.ToString()));

                totalLineas = numeroLineasEnArchivo(archivo);
                progress.SecondaryTotal = totalLineas;
                progress.SecondaryPercent = ((i * 100) / totalLineas);
                progress.SecondaryValue = i;

                string line;
                string[] row;


                using (StreamReader readfile = new StreamReader(new MemoryStream(archivo)))
                {
                    while ((line = readfile.ReadLine()) != null)
                    {
                        if (i >= 1)
                        {
                            row = line.Split(',');
                            var lineas = validarRegistros(row, i);

                            if (lineas.Count == 0)
                            {
                                CamposNominaNegocio neg1 = new CamposNominaNegocio();
                                List<E_MENSAJES> msjbd = new List<E_MENSAJES>();

                                E_PLANTILLA_NOMINA plantilla = new E_PLANTILLA_NOMINA();
                                plantilla.CL_CLIENTE = vClCliente;
                                plantilla.CL_RAZON_SOCIAL = row[0].ToString();
                                plantilla.CL_TRABAJADOR = row[1].ToString();
                                plantilla.NB_NOMBRES = row[2].ToString();
                                plantilla.NB_PATERNO = row[3].ToString();
                                plantilla.NB_MATERNO = row[4].ToString();
                                plantilla.NB_TRABAJADOR = row[3].ToString() + " " + row[4].ToString() + " " + row[2].ToString();
                                plantilla.CL_SEXO = row[5].ToString();
                                plantilla.CL_ESTADO_CIVIL = row[6].ToString();
                                plantilla.DS_NACIONALIDAD = row[7].ToString();
                                plantilla.CL_RFC = row[8].ToString();
                                plantilla.CL_CURP = row[9].ToString();
                                plantilla.NO_TELEFONO_FIJO = row[10].ToString();
                                plantilla.NO_TELEFONO_CELULAR = row[11].ToString();
                                plantilla.DS_EMAIL = row[12].ToString();
                                plantilla.DS_ACCIDENTE = row[13].ToString();
                                plantilla.FE_NACIMIENTO = DateTime.Parse(row[14].ToString());
                                plantilla.DS_LUGAR_NACIMIENTO = row[15].ToString();
                                plantilla.CL_ESTADO_NACIMIENTO = row[16].ToString();
                                plantilla.NB_PADRE = row[17].ToString();
                                plantilla.NB_MADRE = row[18].ToString();
                                plantilla.CL_ESTADO = row[19].ToString();
                                plantilla.NB_MUNICIPIO = row[20].ToString();
                                plantilla.NB_COLONIA = row[21].ToString();
                                plantilla.NB_CALLE = row[22].ToString();
                                plantilla.CL_CP = row[23].ToString();
                                plantilla.NO_EXTERIOR = row[24].ToString();
                                plantilla.NO_INTERIOR = row[25].ToString();
                                plantilla.CL_REGISTRO_PATRONAL = row[26].ToString();
                                if (row[27].ToString().ToUpper().Trim() == "SI" || row[27].ToString().ToUpper().Trim() == "S" || row[27].ToString().ToUpper().Trim() == "1")
                                {
                                    plantilla.FG_COTIZA_IMSS = true;
                                }
                                else
                                {
                                    plantilla.FG_COTIZA_IMSS = false;
                                }
                                plantilla.NO_IMSS = row[28].ToString();
                                plantilla.NO_UMF = row[29].ToString();
                                plantilla.DS_GRUPO_SANGUINEO = row[30].ToString();
                                plantilla.CL_TIPO_TRAB_SUA = row[31].ToString();
                                plantilla.CL_JORNADA_SUA = row[32].ToString();
                                plantilla.CL_UBICACION_SUA = row[33].ToString();
                                if (!string.IsNullOrEmpty(row[34].ToString()))
                                {
                                    plantilla.CL_TIPO_SALARIO_SUA = decimal.Parse(row[34].ToString());
                                }
                                else
                                {
                                    plantilla.CL_TIPO_SALARIO_SUA = null;
                                }
                                plantilla.CL_HORARIO = row[35].ToString();
                                plantilla.CL_DEPARTAMENTO = row[36].ToString();
                                plantilla.CL_PUESTO = row[37].ToString();
                                plantilla.CL_CENTRO_ADMVO = row[38].ToString();
                                plantilla.CL_CENTRO_OPERATIVO = row[39].ToString();
                                plantilla.CL_PAQUETE = row[40].ToString(); //        ver la manera de poner el paquete de prestaciones, tienen que poner la clave y solo tiene para poner el ID
                                plantilla.CL_FORMATO_DISPERSION = row[41].ToString();
                                plantilla.NO_CUENTA_DESPENSA = row[42].ToString();
                                plantilla.CL_FORMATO_VALES_D = row[43].ToString();
                                plantilla.CL_FORMATO_VALES_G = row[44].ToString();
                                plantilla.FE_REINGRESO = DateTime.Parse(row[45].ToString());
                                plantilla.FE_ANTIGUEDAD = DateTime.Parse(row[45].ToString());
                                if (!string.IsNullOrEmpty(row[46].ToString()))
                                    plantilla.FE_PLANTA = DateTime.Parse(row[46].ToString());
                                plantilla.CL_TIPO_NOMINA = row[47].ToString();
                                plantilla.CL_FORMA_PAGO = row[48].ToString();
                                plantilla.CL_BANCO_SAT = row[49].ToString();
                                plantilla.MN_SNOMINAL = decimal.Parse(string.IsNullOrWhiteSpace(row[50].ToString()) ? "0" : row[50].ToString()); //decimal.Parse(row[50].ToString());

                                decimal mnSNominalMensual = 0;
                                decimal diasCalc = 30;
                                bool res = decimal.TryParse((row[51].ToString()), out mnSNominalMensual);
                                if (!res || mnSNominalMensual == 0)
                                {
                                    CamposNominaNegocio objConfig = new CamposNominaNegocio();
                                    List<E_OBTIENE_S_CONFIGURACION> configuracion;
                                    configuracion = objConfig.Obtener_S_CONFIGURACION(CL_CLIENTE: vClCliente, CL_CONFIGURACION: "NO_DIAS_CALCULO");
                                    if (configuracion != null && configuracion.Count > 0)
                                    {
                                        diasCalc = (configuracion == null ? 30 : Decimal.Parse(configuracion.FirstOrDefault().NO_CONFIGURACION.ToString()));
                                    }
                                }
                                plantilla.MN_SNOMINAL_MENSUAL = (res && mnSNominalMensual > 0 ? mnSNominalMensual : plantilla.MN_SNOMINAL * diasCalc);

                                if (plantilla.FG_COTIZA_IMSS == true)
                                {
                                    plantilla.NO_FACTOR_SBC = decimal.Parse(string.IsNullOrWhiteSpace(row[52].ToString()) ? "0" : row[52].ToString());
                                    plantilla.MN_SBC_FIJO = decimal.Parse(string.IsNullOrWhiteSpace(row[53].ToString()) ? "0" : row[53].ToString());
                                    plantilla.MN_SBC_VARIABLE = decimal.Parse(string.IsNullOrWhiteSpace(row[54].ToString()) ? "0" : row[54].ToString());
                                    plantilla.MN_SBC = decimal.Parse(string.IsNullOrWhiteSpace(row[55].ToString()) ? "0" : row[55].ToString());
                                    plantilla.MN_SBC_DETERMINADO = 0;
                                    plantilla.MN_SBC_MAXIMO = 0;
                                }
                                else
                                {
                                    plantilla.NO_FACTOR_SBC = 0;
                                    plantilla.MN_SBC_FIJO = 0;
                                    plantilla.MN_SBC_VARIABLE = 0;
                                    plantilla.MN_SBC = 0;
                                    plantilla.MN_SBC_DETERMINADO = 0;
                                    plantilla.MN_SBC_MAXIMO = 0;
                                }
                                plantilla.NO_CUENTA_PAGO = row[56].ToString();
                                plantilla.NO_CLABE_PAGO = row[57].ToString();
                                plantilla.ID_EMPLEADO = row[58].ToString();
                                plantilla.FILLER01 = row[59].ToString();
                                plantilla.FILLER02 = row[60].ToString();
                                plantilla.FILLER03 = row[61].ToString();
                                plantilla.FILLER04 = row[62].ToString();
                                plantilla.FILLER05 = row[63].ToString();
                                plantilla.CL_TIPO_JORNADA = row[65].ToString();
                                plantilla.CL_TIPO_CONTRATO = row[64].ToString();
                                plantilla.CL_TIPO_RIESGO_TRABAJO = string.IsNullOrWhiteSpace(row[66].ToString()) ? "1" : row[66].ToString();
                                plantilla.ID_SOLICITUD = int.Parse(string.IsNullOrWhiteSpace(row[67].ToString()) ? "0" : row[67].ToString());
                                plantilla.CL_SUBCONTRATADO = row[68].ToString().ToUpper().Trim() == "S" ? true : false;
                                plantilla.CL_REGIMEN_CONTRATACION = string.IsNullOrWhiteSpace(row[69].ToString()) ? "" : row[69].ToString();
                                /*****************DETERMINAR ****************/
                                /**/
                                plantilla.XML_TELEFONOS = setXmlTelefonos(plantilla);
                                plantilla.CL_PENSIONADO = null;
                                if (!EsValidacion)
                                {
                                  
                                    E_MENSAJES msjRespuesta = Utileria.verificaLicencia(ContextoApp.Licencia.clCliente, clSistema, ContextoApp.Licencia.clEmpresa, clModulo, noVersion, ContextoUsuario.clUsuario, ContextoUsuario.nbPrograma);
                                    if (msjRespuesta.CL_CLAVE_RETORNO != "-1000")
                                    {
                                        msjbd.Add(msjRespuesta);
                                    }
                                    else
                                    {
                                        msjbd = neg1.InsertaLayoutempleados(EsValidacion, i, plantilla, vClUsuario, vNbPrograma);
                                    }
                                }
                                else
                                {
                                    msjbd = neg1.InsertaLayoutempleados(EsValidacion, i, plantilla, vClUsuario, vNbPrograma);
                                }




                                if (msjbd.Count != 0)
                                {
                                    MensajesError.AddRange(msjbd);
                                }

                            }
                            else
                            {
                                MensajesError.AddRange(lineas);
                            }

                            i++;
                            progress.SecondaryValue = i;
                            progress.SecondaryPercent = ((i * 100) / totalLineas);
                        }
                        else
                        {
                            i++;
                            progress.SecondaryValue = i;
                            progress.SecondaryPercent = ((i * 100) / totalLineas);
                        }
                    }
                }
                GridErrores.Rebind();
            }
        }

        public string setXmlTelefonos(E_PLANTILLA_NOMINA plantilla)
        {
            bool fgTelefonos = false;
            XElement xmlDetalle = new XElement("TELEFONOS");
            if (!string.IsNullOrEmpty(plantilla.NO_TELEFONO_CELULAR))
            {
                fgTelefonos = true;
                xmlDetalle.Add(new XElement("TELEFONO"
                                    , new XAttribute("NO_TELEFONO", plantilla.NO_TELEFONO_CELULAR)
                                    , new XAttribute("CL_TIPO", "MOVIL")
                                    ));
            }
            if (!string.IsNullOrEmpty(plantilla.NO_TELEFONO_FIJO))
            {
                fgTelefonos = true;
                xmlDetalle.Add(new XElement("TELEFONO"
                                    , new XAttribute("NO_TELEFONO", plantilla.NO_TELEFONO_FIJO)
                                    , new XAttribute("CL_TIPO", "FIJO")
                                    ));
            }

            if (fgTelefonos)
            {
                return xmlDetalle.ToString();
            }
            else
            {
                return null;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO.ToString();
            vNbPrograma = ContextoUsuario.nbPrograma.ToString();
            vClCliente = ContextoApp.Licencia.clCliente;

            if (!Page.IsPostBack)
            {
                //E_RESULTADO msjRespuesta = C verificaLicencia(Contexto.clCliente, clSistema, Contexto.clEmpresa, clModulo, noVersion, Contexto.clUsuario, Contexto.nbPrograma);
                //if (msjRespuesta.CL_CLAVE_RETORNO != "-1000")
                //{
                //    Utileria.MuestraMensajeLicencia(-1, msjRespuesta.NB_MENSAJE_RETORNO, rnMensaje, pcallbackFunction: "closePopup");
                //}

                MensajesError = new List<E_MENSAJES>();
            }
        }

        protected void GridErrores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridErrores.DataSource = MensajesError;
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbVersion.SelectedValue))
            {
                switch (cmbVersion.SelectedValue)
                {
                    case "1.0":
                        importarEmpleados(false);
                        break;
                    case "1.1":
                        //importarEmpleadosV1(false);
                        break;
                }
            }
            else
            {
                UtilMensajes.DisplayMessageRadNotification(1, "Seleccione la versión del archivo", rwMensaje);

            }
        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            ModificarEmpleado(true);
        }

        private void ModificarEmpleado(bool? EsValidacion)
        {
            if (RadAsyncUpload1.UploadedFiles.Count > 0)
            {
                int i = 0;
                int totalLineas;
                RadProgressContext progress = RadProgressContext.Current;

                MensajesError = new List<E_MENSAJES>();
                GridErrores.Rebind();
            }



        }
    }
}