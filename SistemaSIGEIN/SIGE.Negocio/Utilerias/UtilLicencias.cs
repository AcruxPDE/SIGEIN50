using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using SIGE.Negocio.licenciamiento;
namespace SIGE.Negocio.Utilerias
{
    public class UtilLicencias
    {
        string clConfiguracion = "CL_LICENCIAMIENTO";
        string keyFeCreacion;
        string clPassword;
        string cadenaDesencriptada;
        E_RESULTADO vMensaje = new E_RESULTADO();
        List<E_LICENCIA> lstLicencia = new List<E_LICENCIA>();
        E_LICENCIA vLic = new E_LICENCIA();

        public E_RESULTADO validaLicencia(string clCliente = null, string clSistema = null, string clEmpresa = null, string clModulo = null, string noVersion = null, string usuario = null, string programa = null, XElement confCliente = null, XElement clienteLicencias = null)
        {
            if (confCliente != null && clienteLicencias != null)
            {
                LicenciaNegocio licNeg = new LicenciaNegocio();
                SPE_OBTIENE_CONFIGURACION_LICENCIA_Result resultPass = licNeg.obtieneConfiguracion(CL_CONFIGURACION: "CL_PASS_WS");
                if (resultPass != null)
                {
                    clPassword = resultPass.NO_CONFIGURACION;
                    string keyPassword = clPassword.Substring(0, 16);

                    if (!existeCliente(confCliente, clCliente) || !ExisteSistema(confCliente, clSistema) || !ExisteModulo(confCliente, clModulo))
                    {
                        generaXmlIdentificacion(clCliente, clPassword, usuario, programa);
                        generaXmlLicencias(clCliente, clPassword, usuario, programa);
                    }

                    if (existeCliente(confCliente, clCliente) && ExisteSistema(confCliente, clSistema) && ExisteModulo(confCliente, clModulo))
                    {
                        if (!ExisteLicencia(clienteLicencias, clCliente, clSistema, clEmpresa, clModulo, noVersion))
                        {
                            generaXmlLicencias(clCliente, clPassword, usuario, programa);
                        }

                        if (ExisteLicencia(clienteLicencias, clCliente, clSistema, clEmpresa, clModulo, noVersion))
                        {

                            if (!validaLicencia(vLic))
                            {
                                generaXmlLicencias(clCliente, clPassword, usuario, programa);
                                ExisteLicencia(clienteLicencias, clCliente, clSistema, clEmpresa, clModulo, noVersion);
                            }

                            if (validaLicencia(vLic))
                            {
                                List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                                vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "1" });
                                vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.SUCCESSFUL;
                                vMensaje.MENSAJE = vLstMensaje;
                                return vMensaje;
                            }
                            else
                            {
                                return vMensaje;
                            }
                        }
                        else
                        {
                            return vMensaje;
                        }
                    }
                    else
                    {
                        List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                        vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No existe licencia para el módulo actual." });
                        vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                        vMensaje.MENSAJE = vLstMensaje;
                        return vMensaje;
                    }
                }
                else
                {
                    List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                    vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No se encontro la contraseña del web service" });
                    vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    vMensaje.MENSAJE = vLstMensaje;
                    return vMensaje;
                }
            }
            else
            {
                generaDatosContexto(clCliente, clSistema, clModulo, usuario, programa);
                return vMensaje;
            }
        }

        public E_RESULTADO generaClaves(string clCliente, string usuario, string programa)
        {
            LicenciaNegocio licNeg = new LicenciaNegocio();
            SPE_OBTIENE_CONFIGURACION_LICENCIA_Result resultPass = licNeg.obtieneConfiguracion(CL_CONFIGURACION: "CL_PASS_WS");
            if (resultPass != null)
            {
                clPassword = resultPass.NO_CONFIGURACION;
                if (generaXmlIdentificacion(clCliente, clPassword, usuario, programa) && generaXmlLicencias(clCliente, clPassword, usuario, programa))
                {
                    List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                    vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "Se ha actualizado la información de la licencia." });
                    vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.SUCCESSFUL;
                    vMensaje.MENSAJE = vLstMensaje;
                }
                else
                {
                    List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                    vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "Ocurrio un error al actualizar la información de la licencia." });
                    vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    vMensaje.MENSAJE = vLstMensaje;
                }
            }
            else
            {
                List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No se encontro la contraseña del web service." });
                vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                vMensaje.MENSAJE = vLstMensaje;
            }
            return vMensaje;
        }

        public bool generaXmlIdentificacion(string clCliente, string clPassword, string usuario, string programa)
        {
            try
            {
                Licencia lic = new Licencia();
                string vXmlIdentifi = lic.generaXmlIdentificacion(clCliente, clPassword);
                LicenciaNegocio licNeg = new LicenciaNegocio();
                var resultado = licNeg.InsertaActualiza_S_CONFIGURACION(NO_CONFIGURACION: vXmlIdentifi, CL_CONFIGURACION: clConfiguracion, CL_USUARIO: usuario, NB_PROGRAMA: programa, TIPO_TRANSACCION: "A");
                if (resultado.CL_TIPO_ERROR != E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                    vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No existen datos para el cliente actual." });
                    vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    vMensaje.MENSAJE = vLstMensaje;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No es posible conectarse con el servidor." });
                vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                vMensaje.MENSAJE = vLstMensaje;
                return false;
            }
        }

        public bool generaXmlLicencias(string clCliente, string clPassword, string usuario, string programa)
        {
            try
            {
                Licencia lic = new Licencia();
                string vXmlLicencia = lic.generaXmlLicencia(clCliente, clPassword);
                LicenciaNegocio licNeg = new LicenciaNegocio();
                var resultado = licNeg.InsertaActualiza_S_CONFIGURACION(NO_CONFIGURACION: vXmlLicencia, CL_CONFIGURACION: "OBJ_ADICIONAL", CL_USUARIO: usuario, NB_PROGRAMA: programa, TIPO_TRANSACCION: "A");
                if (resultado.CL_TIPO_ERROR != E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                    vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "El cliente actual no cuenta con licencias." });
                    vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    vMensaje.MENSAJE = vLstMensaje;
                    return false;
                }
                else
                {
                    List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                    vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "Credenciales actualizadas correctamente." });
                    vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.SUCCESSFUL;
                    vMensaje.MENSAJE = vLstMensaje;
                    return true;
                }
            }
            catch (Exception e)
            {
                List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No es posible conectarse con el servidor." });
                vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                vMensaje.MENSAJE = vLstMensaje;
                return false;
            }
        }

        public bool validaLicencia(E_LICENCIA vLicencia)
        {
            DateTime feActual = System.DateTime.Now.Date;
            DateTime Inicio;
            DateTime Fin;
            int VolumenLicencia;

            if (!DateTime.TryParse(vLicencia.FE_INICIO, out Inicio))
            {
                vLicencia.FE_INICIO = null;
            }
            if (!DateTime.TryParse(vLicencia.FE_FIN, out Fin))
            {
                vLicencia.FE_FIN = null;
            }

            if (!int.TryParse(vLicencia.NO_VOLUMEN, out VolumenLicencia))
            {
                vLicencia.NO_VOLUMEN = null;
            }

            if (vLicencia.FE_INICIO != null && vLicencia.FE_FIN != null)
            {
                if (feActual >= Inicio && feActual <= Fin)
                {
                    //if (vLicencia.NO_VOLUMEN != null)
                    //{
                    //    LicenciaNegocio licNeg = new LicenciaNegocio();

                    //    int volSistema = licNeg.ObtenerEmpleados().Count();
                    //    if (VolumenLicencia >= volSistema)
                    //    {
                    //        return true;
                    //    }
                    //    else
                    //    {
                    //        List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                    //        vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "Licencia por volumen excedida. (" + VolumenLicencia + ")" });
                    //        vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    //        vMensaje.MENSAJE = vLstMensaje;
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                        return true;
                    //}
                }
                else
                {
                    List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                    vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "Licencia por periodo expirada el " + Fin });
                    vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    vMensaje.MENSAJE = vLstMensaje;
                    return false;
                }
            }

            return true;
            //else if (vLicencia.NO_VOLUMEN != null)
            //{
            //    LicenciaNegocio licNeg = new LicenciaNegocio();
            //    int volSistema = licNeg.ObtenerEmpleados().Count();
            //    if (VolumenLicencia >= volSistema)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
            //        //vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "Licencia por volumen excedida. (" + VolumenLicencia + ")" });
            //         vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "Se ha alcanzado el máximo número de empleados para la licencia y no es posible agregar más." });
            //        vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
            //        vMensaje.MENSAJE = vLstMensaje;
            //        return true;
            //    }
            //}
            //else
            //{
            //    return false;
            //}
        }

        public bool existeCliente(XElement vXmlConfiguracion, string clCliente)
        {
            List<E_CLIENTE> lstCliente = vXmlConfiguracion.Descendants("CLIENTE").Select(x => new E_CLIENTE
            {
                CL_CLIENTE = UtilXML.ValorAtributo<string>(x.Attribute("CL_CLIENTE"))
            }).ToList();

            return (lstCliente.Exists(w => w.CL_CLIENTE == clCliente));
        }

        public bool ExisteSistema(XElement vXmlConfiguracion, string clSistema)
        {
            List<E_SISTEMA> lstSistemas = vXmlConfiguracion.Descendants("SISTEMA").Select(x => new E_SISTEMA
            {
                NB_SISTEMA = UtilXML.ValorAtributo<string>(x.Attribute("NB_SISTEMA")),
                CL_SISTEMA = UtilXML.ValorAtributo<string>(x.Attribute("CL_SISTEMA"))
            }).ToList();

            return (lstSistemas.Exists(w => w.CL_SISTEMA == clSistema));

        }

        public bool ExisteModulo(XElement vXmlConfiguracion, string clModulo)
        {
            List<E_MODULO> lstModulos = vXmlConfiguracion.Descendants("MODULO").Select(x => new E_MODULO
            {
                NB_MODULO = UtilXML.ValorAtributo<string>(x.Attribute("NB_MODULO")),
                CL_MODULO = UtilXML.ValorAtributo<string>(x.Attribute("CL_MODULO"))
            }).ToList();

            return (lstModulos.Exists(w => w.CL_MODULO == clModulo));

        }

        public bool ExisteLicencia(XElement vXmlLicencias, string clCliente = null, string clSistema = null, string clEmpresa = null, string clModulo = null, string noVersion = null)
        {
            lstLicencia = vXmlLicencias.Descendants("LICENCIA").Select(x => new E_LICENCIA
            {
                CL_CLIENTE = UtilXML.ValorAtributo<string>(x.Attribute("CL_CLIENTE")),
                CL_SISTEMA = UtilXML.ValorAtributo<string>(x.Attribute("CL_SISTEMA")),
                CL_EMPRESA = UtilXML.ValorAtributo<string>(x.Attribute("CL_EMPRESA")),
                CL_MODULO = UtilXML.ValorAtributo<string>(x.Attribute("CL_MODULO")),
                NO_RELEASE = UtilXML.ValorAtributo<string>(x.Attribute("NO_RELEASE")),
                FE_INICIO = UtilXML.ValorAtributo<string>(x.Attribute("FE_INICIO")),
                FE_FIN = UtilXML.ValorAtributo<string>(x.Attribute("FE_FIN")),
                NO_VOLUMEN = UtilXML.ValorAtributo<string>(x.Attribute("NO_VOLUMEN"))
            }).ToList();

            if (lstLicencia.Exists(w => w.CL_CLIENTE == clCliente && w.CL_SISTEMA == clSistema && w.CL_EMPRESA == clEmpresa && w.CL_MODULO == clModulo && w.NO_RELEASE == noVersion))
            {
                vLic = lstLicencia.Where(w => w.CL_CLIENTE == clCliente && w.CL_SISTEMA == clSistema && w.CL_EMPRESA == clEmpresa && w.CL_MODULO == clModulo && w.NO_RELEASE == noVersion).FirstOrDefault();
                return true;
            }
            else
            {
                List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No existe licencia para el módulo actual" });
                vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                vMensaje.MENSAJE = vLstMensaje;
                return false;
            }
        }

        public void obtienePassword(XElement vXmlConfiguracion)
        {
            E_CLIENTE vCliente = vXmlConfiguracion.Descendants("CLIENTE").Select(x => new E_CLIENTE
            {
                CL_PASSWORD = UtilXML.ValorAtributo<string>(x.Attribute("CL_PASSWORD"))
            }).FirstOrDefault();

            clPassword = vCliente.CL_PASSWORD;
        }

        public bool insertaXmlIdentificacion(string clCliente, string clPassword, string usuario, string programa)
        {
            try
            {
                Licencia lic = new Licencia();
                string vXmlIdentifi = lic.generaXmlIdentificacion(clCliente, clPassword);
                LicenciaNegocio licNeg = new LicenciaNegocio();
                var resultado = licNeg.InsertaActualiza_S_CONFIGURACION(CL_CONFIGURACION: clConfiguracion, NO_CONFIGURACION: vXmlIdentifi, CL_USUARIO: usuario, NB_PROGRAMA: programa, TIPO_TRANSACCION: "I");
                if (resultado.CL_TIPO_ERROR != E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                    vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No existen datos para el cliente actual" });
                    vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    vMensaje.MENSAJE = vLstMensaje;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No es posible conectarse con el servidor" });
                vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                vMensaje.MENSAJE = vLstMensaje;
                return false;
            }
        }

        public void generaDatosContexto(string clCliente, string clSistema, string clModulo, string usuario, string programa)
        {
            LicenciaNegocio licNeg = new LicenciaNegocio();
            Crypto desencripta = new Crypto();

            SPE_OBTIENE_CONFIGURACION_LICENCIA_Result resultado = licNeg.obtieneConfiguracion(CL_CONFIGURACION: clConfiguracion);
            SPE_OBTIENE_CONFIGURACION_LICENCIA_Result resultPass = licNeg.obtieneConfiguracion(CL_CONFIGURACION: "CL_PASS_WS");

            if (resultPass != null && resultado != null)
            {
                clPassword = resultPass.NO_CONFIGURACION;
                generaXmlIdentificacion(clCliente, clPassword, usuario, programa);
                resultado = licNeg.obtieneConfiguracion(CL_CONFIGURACION: clConfiguracion);

                if (!string.IsNullOrEmpty(resultado.NO_CONFIGURACION))
                {
                    keyFeCreacion = licNeg.obtieneConfiguracion("FE_CREACION").NO_CONFIGURACION;
                    string keyPassword = clPassword.Substring(0, 16);
                    cadenaDesencriptada = desencripta.descifrarTextoAES(resultado.NO_CONFIGURACION, clCliente, keyFeCreacion, "SHA1", 22, keyPassword, 256);
                    XElement vXmlConfiguracion = XElement.Parse(cadenaDesencriptada);

                    if (existeCliente(vXmlConfiguracion, clCliente) && ExisteSistema(vXmlConfiguracion, clSistema) && ExisteModulo(vXmlConfiguracion, clModulo))
                    {
                        generaXmlLicencias(clCliente, clPassword, usuario, programa);
                        List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                        vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No existen datos en el contexto." });
                        vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.WARNING;
                        vMensaje.MENSAJE = vLstMensaje;
                    }
                    else
                    {
                        List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                        vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No existe licencia para el módulo actual." });
                        vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                        vMensaje.MENSAJE = vLstMensaje;
                    }
                }
                else
                {
                    List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                    vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "El cliente actual no cuenta con licencias." });
                    vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    vMensaje.MENSAJE = vLstMensaje;
                }
            }
            else
            {//ir al web service
                //actualizar bd
                //set valores contexto
                //validar licencia

                if (resultado == null)
                {
                    clPassword = resultPass.NO_CONFIGURACION;
                    if (insertaXmlIdentificacion(clCliente, clPassword, usuario, programa) && generaXmlLicencias(clCliente, clPassword, usuario, programa))
                    {
                        List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                        vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No existen datos en el contexto" });
                        vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.WARNING;
                        vMensaje.MENSAJE = vLstMensaje;
                    }
                    else
                    {
                        List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                        vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "El cliente actual no cuenta con licencias." });
                        vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                        vMensaje.MENSAJE = vLstMensaje;
                    }
                }
                else
                {
                    List<E_MENSAJE> vLstMensaje = new List<E_MENSAJE>();
                    vLstMensaje.Add(new E_MENSAJE { DS_MENSAJE = "No se encontro la contraseña del web service." });
                    vMensaje.CL_TIPO_ERROR = E_TIPO_RESPUESTA_DB.ERROR;
                    vMensaje.MENSAJE = vLstMensaje;
                }
            }
        }

    }
}
