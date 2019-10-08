using System;
using System.Collections.Generic;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;
using SIGE.Entidades.Administracion;

namespace SIGE.Negocio.Administracion
{
    public class EmpleadoNegocio
    {
        public List<SPE_OBTIENE_M_EMPLEADO_Result> ObtenerEmpleado(int? ID_EMPLEADO = null, String CL_EMPLEADO = null, String NB_EMPLEADO = null, String NB_APELLIDO_PATERNO = null, String NB_APELLIDO_MATERNO = null, String CL_ESTADO_EMPLEADO = null, String CL_GENERO = null, String CL_ESTADO_CIVIL = null, String NB_CONYUGUE = null, String CL_RFC = null, String CL_CURP = null, String CL_NSS = null, String CL_TIPO_SANGUINEO = null, String CL_NACIONALIDAD = null, String NB_PAIS = null, String NB_ESTADO = null, String NB_MUNICIPIO = null, String NB_COLONIA = null, String NB_CALLE = null, String NO_INTERIOR = null, String NO_EXTERIOR = null, String CL_CODIGO_POSTAL = null, String CL_CORREO_ELECTRONICO = null, bool? FG_ACTIVO = null, System.DateTime? FE_NACIMIENTO = null, String DS_LUGAR_NACIMIENTO = null, System.DateTime? FE_ALTA = null, System.DateTime? FE_BAJA = null, int? ID_PUESTO = null, Decimal? MN_SUELDO = null, Decimal? MN_SUELDO_VARIABLE = null, String DS_SUELDO_COMPOSICION = null, int? ID_CANDIDATO = null, int? ID_EMPRESA = null,
               bool? MP_FG_ACTIVO = null, System.DateTime? FE_INACTIVO = null, String CL_PUESTO = null, String NB_PUESTO = null, int? ID_PUESTO_JEFE = null, int? ID_DEPARTAMENTO = null, int? ID_BITACORA = null,
               String NB_CANDIDATO = null, String CC_NB_APELLIDO_PATERNO = null, String CC_NB_APELLIDO_MATERNO = null, String CC_CL_GENERO = null, String CC_CL_RFC = null, String CC_CL_CURP = null, String CC_CL_ESTADO_CIVIL = null, String CC_NB_CONYUGUE = null, String CC_CL_NSS = null, String CC_CL_TIPO_SANGUINEO = null, String CC_NB_PAIS = null, String CC_NB_ESTADO = null, String CC_NB_MUNICIPIO = null, String CC_NB_COLONIA = null, String CC_NB_CALLE = null, String CC_NO_INTERIOR = null, String CC_NO_EXTERIOR = null, String CC_CL_CODIGO_POSTAL = null, String CC_CL_CORREO_ELECTRONICO = null, System.DateTime? CC_FE_NACIMIENTO = null, String CC_DS_LUGAR_NACIMIENTO = null, Decimal? CC_MN_SUELDO = null, String CC_CL_NACIONALIDAD = null, String DS_NACIONALIDAD = null, String NB_LICENCIA = null, String DS_VEHICULO = null, String CL_CARTILLA_MILITAR = null, String CL_CEDULA_PROFESIONAL = null, String DS_DISPONIBILIDAD = null, String CL_DISPONIBILIDAD_VIAJE = null, String DS_COMENTARIO = null, bool? CC_FG_ACTIVO = null,
               String CL_EMPRESA = null, String NB_EMPRESA = null, String NB_RAZON_SOCIAL = null, bool? MD_FG_ACTIVO = null, System.DateTime? MD_FE_INACTIVO = null, String CL_DEPARTAMENTO = null, String NB_DEPARTAMENTO = null, XElement xml = null)
        {

            EmpleadoOperaciones operaciones = new EmpleadoOperaciones();
            return operaciones.ObtieneEmpleado(ID_EMPLEADO, CL_EMPLEADO, NB_EMPLEADO, NB_APELLIDO_PATERNO, NB_APELLIDO_MATERNO, CL_ESTADO_EMPLEADO, CL_GENERO, CL_ESTADO_CIVIL, NB_CONYUGUE, CL_RFC, CL_CURP, CL_NSS, CL_TIPO_SANGUINEO, CL_NACIONALIDAD, NB_PAIS, NB_ESTADO, NB_MUNICIPIO, NB_COLONIA, NB_CALLE, NO_INTERIOR, NO_EXTERIOR, CL_CODIGO_POSTAL, CL_CORREO_ELECTRONICO, FG_ACTIVO, FE_NACIMIENTO, DS_LUGAR_NACIMIENTO, FE_ALTA, FE_BAJA, ID_PUESTO, MN_SUELDO, MN_SUELDO_VARIABLE, DS_SUELDO_COMPOSICION, ID_CANDIDATO, ID_EMPRESA);
        }

        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtenerEmpleados(XElement pXmlSeleccion = null, bool? pFgFoto = null, string pClUsuario = null, bool? pFgActivo = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            EmpleadoOperaciones oEmpleados = new EmpleadoOperaciones();
            return oEmpleados.ObtenerEmpleados(pXmlSeleccion, pFgFoto, pClUsuario, pFgActivo, pID_EMPRESA, pID_ROL);
        }

        public List<SPE_OBTIENE_EMPLEADOS_CAMPOS_EXTRA_Result> ObtenerEmpleadosCamposExtra(XElement pXmlSeleccion = null, bool? pFgFoto = null, string pClUsuario = null, bool? pFgActivo = null, int? pID_EMPRESA = null, int? pIdRol = null)
        {
            EmpleadoOperaciones oEmpleados = new EmpleadoOperaciones();
            return oEmpleados.ObtenerEmpleadosCamposExtra(pXmlSeleccion, pFgFoto, pClUsuario, pFgActivo, pID_EMPRESA, pIdRol);
        }

        public List<SPE_OBTIENE_EMPLEADOS_SELECTOR_Result> ObtenerEmpleadosSelector(XElement pXmlSeleccion = null, bool? pFgFoto = null, string pClUsuario = null, bool? pFgActivo = null, int? pID_EMPRESA = null)
        {
            EmpleadoOperaciones oEmpleados = new EmpleadoOperaciones();
            return oEmpleados.ObtenerEmpleadosSelector(pXmlSeleccion, pFgFoto, pClUsuario, pFgActivo, pID_EMPRESA);
        }

        public SPE_OBTIENE_EMPLEADO_PLANTILLA_Result ObtenerPlantilla(int? pIdPlantilla, int? pIdEmpleado, int? pidEmpresa,int? pidRol=null)
        {
            EmpleadoOperaciones oEmpleado = new EmpleadoOperaciones();
            SPE_OBTIENE_EMPLEADO_PLANTILLA_Result vEmpleadoPlantilla = oEmpleado.ObtenerPlantilla(pIdPlantilla, pIdEmpleado, pidEmpresa,pidRol);

            XElement vEmpleado = XElement.Parse(vEmpleadoPlantilla.XML_SOLICITUD_PLANTILLA);
            XElement vValores = XElement.Parse(vEmpleadoPlantilla.XML_VALORES);

            foreach (XElement vXmlContenedor in vEmpleado.Element("CONTENEDORES").Elements("CONTENEDOR"))
                foreach (XElement vXmlCampo in vXmlContenedor.Elements("CAMPO"))
                    UtilXML.AsignarValorCampo(vXmlCampo, vValores);

            vEmpleadoPlantilla.XML_SOLICITUD_PLANTILLA = vEmpleado.ToString();

            return vEmpleadoPlantilla;
        }

        public SPE_OBTIENE_EMPLEADO_PLANTILLA_PDE_Result ObtenerPlantillaPDE(int? pIdPlantilla, string pIdEmpleado, string pClFormulario)
        {
            EmpleadoOperaciones oEmpleado = new EmpleadoOperaciones();
            SPE_OBTIENE_EMPLEADO_PLANTILLA_PDE_Result vEmpleadoPlantilla = oEmpleado.ObtenerPlantillaPDE(pIdPlantilla, pIdEmpleado, pClFormulario);

            XElement vEmpleado = XElement.Parse(vEmpleadoPlantilla.XML_SOLICITUD_PLANTILLA);
            XElement vValores = XElement.Parse(vEmpleadoPlantilla.XML_VALORES);

            foreach (XElement vXmlContenedor in vEmpleado.Element("CONTENEDORES").Elements("CONTENEDOR"))
                foreach (XElement vXmlCampo in vXmlContenedor.Elements("CAMPO"))
                    UtilXML.AsignarValorCampo(vXmlCampo, vValores);

            vEmpleadoPlantilla.XML_SOLICITUD_PLANTILLA = vEmpleado.ToString();

            return vEmpleadoPlantilla;
        }

        public SPE_OBTIENE_EMPLEADO_PLANTILLA_CAMBIO_PDE_Result ObtenerPlantillaCambioPDE(int? pIdPlantilla, string pIdEmpleado, string pClFormulario)
        {
            EmpleadoOperaciones oEmpleado = new EmpleadoOperaciones();
            SPE_OBTIENE_EMPLEADO_PLANTILLA_CAMBIO_PDE_Result vEmpleadoPlantilla = oEmpleado.ObtenerPlantillaCambioPDE(pIdPlantilla, pIdEmpleado, pClFormulario);

            XElement vEmpleado = XElement.Parse(vEmpleadoPlantilla.XML_SOLICITUD_PLANTILLA);
            XElement vValores = XElement.Parse(vEmpleadoPlantilla.XML_VALORES);

            foreach (XElement vXmlContenedor in vEmpleado.Element("CONTENEDORES").Elements("CONTENEDOR"))
                foreach (XElement vXmlCampo in vXmlContenedor.Elements("CAMPO"))
                    UtilXML.AsignarValorCampo(vXmlCampo, vValores);

            vEmpleadoPlantilla.XML_SOLICITUD_PLANTILLA = vEmpleado.ToString();

            return vEmpleadoPlantilla;
        }

        public E_RESULTADO InsertaActualizaEmpleado(XElement pXmlEmpleado, XElement vPlantillaNomina, int? pIdEmpleado, List<UDTT_ARCHIVO> pLstArchivoTemporales, List<E_DOCUMENTO> pLstDocumentos, string pClUsuario, string pNbPrograma, string vTipoTransaccion)
        {
            EmpleadoOperaciones oEmpleado = new EmpleadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oEmpleado.InsertarActualizarEmpleado(pXmlEmpleado, vPlantillaNomina, pIdEmpleado, pLstArchivoTemporales, pLstDocumentos, pClUsuario, pNbPrograma, vTipoTransaccion));
        }

        public E_RESULTADO InsertaActualizaEmpleadoPDE(XElement pXmlEmpleado, string pIdEmpleado, List<UDTT_ARCHIVO> pLstArchivoTemporales, List<E_DOCUMENTO> pLstDocumentos, string pClUsuario, string pNbPrograma, XElement xmlNuevaPlantilla)
        {
            EmpleadoOperaciones operaciones = new EmpleadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarEmpleadoPDE(pXmlEmpleado, pIdEmpleado, pLstArchivoTemporales, pLstDocumentos, pClUsuario, pNbPrograma, xmlNuevaPlantilla));
        }

        public E_RESULTADO Elimina_M_EMPLEADO(int? ID_EMPLEADO = null, string CL_EMPLEADO = null, string usuario = null, string programa = null)
        {
            EmpleadoOperaciones operaciones = new EmpleadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_M_EMPLEADO(ID_EMPLEADO, CL_EMPLEADO, usuario, programa));
        }

        public List<SPE_OBTIENE_SUELDO_EMPLEADOS_Result> ObtenerSueldoEmpleados(int? pIdEmpresa = null, int? pIdRol = null)
        {
            EmpleadoOperaciones EmpleadoOp = new EmpleadoOperaciones();
            return EmpleadoOp.ObtenerSueldoEmpleados(pIdEmpresa, pIdRol);
        }

        public List<SPE_OBTIENE_PERFIL_EMPLEADOS_Result> ObtenerPerfilEmpleados(int? pIdEmpresa = null, int? pIdRol = null)
        {
            EmpleadoOperaciones EmpleadoOp = new EmpleadoOperaciones();
            return EmpleadoOp.ObtenerPerfilEmpleados(pIdEmpresa, pIdRol);
        }

        public List<SPE_OBTIENE_CAPACITACIONES_EMPLEADO_Result> ObtenerCapacitacionEmpleados()
        {
            EmpleadoOperaciones EmpleadoOp = new EmpleadoOperaciones();
            return EmpleadoOp.ObtenerCapacitacionEmpleados();
        }

        public E_RESULTADO ActualizaBajaEmpleado()
        {
            EmpleadoOperaciones oEmpleado = new EmpleadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oEmpleado.ActualizarBajaEmpleados());
        }


        public E_RESULTADO CancelaBajaEmpleado(int ID_EMPLEADO, string CL_USUARIO, string NB_PROGRAMA)
        {
            EmpleadoOperaciones oEmpleado = new EmpleadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oEmpleado.CancelarBajaEmpleado(ID_EMPLEADO, CL_USUARIO, NB_PROGRAMA));
        }

        public E_REPORTE_EMPLEADO ReporteEmpleadoPorModulo(int pIdEmpleado)
        {
            string vDsReporte;
            E_REPORTE_EMPLEADO vReporteEmpleado = new E_REPORTE_EMPLEADO();

            EmpleadoOperaciones oEmpleado = new EmpleadoOperaciones();
            vDsReporte = oEmpleado.ReporteEmpleadoPorModulo(pIdEmpleado);


            XElement vXmlReporte = XElement.Parse(vDsReporte);


            if (vXmlReporte.Element("IDP") != null)
            {
                foreach (XAttribute item in vXmlReporte.Element("IDP").Attributes())
                {

                    switch (item.Name.ToString())
                    {
                        case "ID_CANDIDATO":
                            vReporteEmpleado.DatosIdp.ID_CANDIDATO = UtilXML.ValorAtributo<int>(item);
                            break;
                        case "ID_SOLICITUD":
                            vReporteEmpleado.DatosIdp.ID_SOLICITUD = UtilXML.ValorAtributo<int>(item);
                            break;

                        case "CL_SOLICITUD":
                            vReporteEmpleado.DatosIdp.CL_SOLICITUD = UtilXML.ValorAtributo<string>(item);
                            break;

                        case "ID_BATERIA":
                            vReporteEmpleado.DatosIdp.ID_BATERIA = UtilXML.ValorAtributo<int>(item);
                            break;

                        case "FL_BATERIA":
                            vReporteEmpleado.DatosIdp.FL_BATERIA = UtilXML.ValorAtributo<string>(item);
                            break;

                        case "CL_TOKEN":
                            vReporteEmpleado.DatosIdp.CL_TOKEN = UtilXML.ValorAtributo<string>(item);
                            break;
                    }
                }
            }

            if (vXmlReporte.Element("FYD") != null)
            {

                if (vXmlReporte.Element("FYD").Element("PROGRAMAS") != null)
                {

                    foreach (XElement item in vXmlReporte.Element("FYD").Element("PROGRAMAS").Elements("PROGRAMA"))
                    {

                        vReporteEmpleado.DatosFyd.vLstProgramas.Add(new E_PROGRAMAS
                        {
                            ID_PROGRAMA = UtilXML.ValorAtributo<int>(item.Attribute("ID_PROGRAMA")),
                            CL_PROGRAMA = UtilXML.ValorAtributo<string>(item.Attribute("CL_PROGRAMA")),
                            NB_PROGRAMA = UtilXML.ValorAtributo<string>(item.Attribute("NB_PROGRAMA")),
                            CL_USUARIO_APP_CREA = UtilXML.ValorAtributo<string>(item.Attribute("CL_USUARIO")),
                            FE_CREACION = UtilXML.ValorAtributo<DateTime>(item.Attribute("FE_CREACION"))
                        });
                    }
                }


                if (vXmlReporte.Element("FYD").Element("EVENTOS") != null)
                {
                    foreach (XElement item in vXmlReporte.Element("FYD").Element("EVENTOS").Elements("EVENTO"))
                    {
                        vReporteEmpleado.DatosFyd.vLstEventos.Add(new E_EVENTOS
                        {
                            ID_EVENTO = UtilXML.ValorAtributo<int>(item.Attribute("ID_EVENTO")),
                            CL_EVENTO = UtilXML.ValorAtributo<string>(item.Attribute("CL_EVENTO")),
                            NB_EVENTO = UtilXML.ValorAtributo<string>(item.Attribute("NB_EVENTO")),
                            CL_CURSO = UtilXML.ValorAtributo<string>(item.Attribute("CL_CURSO")),
                            NB_CURSO = UtilXML.ValorAtributo<string>(item.Attribute("NB_CURSO")),
                            MN_COSTO_DIRECTO = UtilXML.ValorAtributo<decimal>(item.Attribute("MN_COSTO_DIRECTO")),
                            FE_INICIO = UtilXML.ValorAtributo<DateTime>(item.Attribute("FE_INICIO")),
                            FE_TERMINO = UtilXML.ValorAtributo<DateTime>(item.Attribute("FE_TERMINO")),
                            PR_CUMPLIMIENTO = UtilXML.ValorAtributo<decimal>(item.Attribute("PR_CUMPLIMIENTO"))
                        });
                    }
                }
            }


            if (vXmlReporte.Element("EO") != null)
            {
                if (vXmlReporte.Element("EO").Element("DESEMPENO") != null)
                {
                    foreach (XElement item in vXmlReporte.Element("EO").Element("DESEMPENO").Elements("PERIODO"))
                    {
                        vReporteEmpleado.DatosEo.vLstDesempeno.Add(new E_DESEMPENO
                        {
                            ID_PERIODO = UtilXML.ValorAtributo<int>(item.Attribute("ID_PERIODO")),
                            ID_EVALUADO = UtilXML.ValorAtributo<int>(item.Attribute("ID_EVALUADO")),
                            CL_PERIODO = UtilXML.ValorAtributo<string>(item.Attribute("CL_PERIODO")),
                            NB_PERIODO = UtilXML.ValorAtributo<string>(item.Attribute("NB_PERIODO")),
                            PR_CUMPLIMIENTO_EVALUADO = UtilXML.ValorAtributo<decimal>(item.Attribute("PR_CUMPLIMIENTO_EVALUADO"))
                        });
                    }
                }

                if (vXmlReporte.Element("EO").Element("CLIMA") != null)
                {
                    foreach (XAttribute item in vXmlReporte.Element("EO").Element("CLIMA").Attributes())
                    {

                        switch (item.Name.ToString())
                        {
                            case "ID_PERIODO_CLIMA":
                                vReporteEmpleado.DatosEo.ID_PERIODO_CLIMA = UtilXML.ValorAtributo<int>(item);
                                break;

                            case "ID_EVALUADO_CLIMA":
                                vReporteEmpleado.DatosEo.ID_EVALUADO_CLIMA = UtilXML.ValorAtributo<int>(item);
                                break;                            
                        }
                    }
                }

                if (vXmlReporte.Element("EO").Element("ROTACION") != null)
                {
                    foreach (XElement item in vXmlReporte.Element("EO").Element("ROTACION").Elements("BAJA"))
                    {
                        vReporteEmpleado.DatosEo.vLstRotacion.Add(new E_ROTACION
                        {
                            FE_INGRESO = UtilXML.ValorAtributo<DateTime>(item.Attribute("FE_INGRESO")),
                            ID_BAJA_EMPLEADO = UtilXML.ValorAtributo<int>(item.Attribute("ID_CAUSA_BAJA")),
                            FE_BAJA_EFECTIVA = UtilXML.ValorAtributo<DateTime>(item.Attribute("FE_BAJA_EFECTIVA")),
                            NB_MOTIVO = UtilXML.ValorAtributo<string>(item.Attribute("NB_MOTIVO")),
                            DS_MOTIVO = UtilXML.ValorAtributo<string>(item.Attribute("DS_MOTIVO"))                            
                        });
                    }
                }

            }


            if (vXmlReporte.Element("MC") != null)
            {
                if (vXmlReporte.Element("MC").Element("BITACORA_CAMBIOS") != null)
                {
                    foreach (XElement item in vXmlReporte.Element("MC").Element("BITACORA_CAMBIOS").Elements("SUELDO_PUESTO"))
                    {
                        vReporteEmpleado.DatosMc.vLstBitacoraSueldos.Add(new E_BITACORA_SUELDO 
                        {
                            ID_BITACORA_SUELDO = UtilXML.ValorAtributo<int>(item.Attribute("ID_BITACORA_SUELDO")),
                            FE_CAMBIO = UtilXML.ValorAtributo<DateTime>(item.Attribute("FE_CAMBIO")),
                            NB_PROCESO = UtilXML.ValorAtributo<string>(item.Attribute("NB_PROCESO")),
                            DS_PROCESO = UtilXML.ValorAtributo<string>(item.Attribute("DS_PROCESO")),
                            NB_ANTERIOR = UtilXML.ValorAtributo<string>(item.Attribute("NB_ANTERIOR")),//UtilXML.ValorAtributo<decimal>(item.Attribute("NB_ANTERIOR")),
                            NB_ACTUAL = UtilXML.ValorAtributo<string>(item.Attribute("NB_ACTUAL")), //UtilXML.ValorAtributo<decimal>(item.Attribute("NB_ACTUAL"))
                            CL_TIPO_BITACORA = UtilXML.ValorAtributo<string>(item.Attribute("CL_TIPO_BITACORA"))
                        });
                    }
                }
            }

            return vReporteEmpleado;
        }

        public E_RESULTADO ActualizaReingresoEmpleado(string pXmlDatosEmpleado, string pClUsuario, string pNbPrograma)
        {
            EmpleadoOperaciones oEmpleado = new EmpleadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oEmpleado.ActualizaReingresoEmpleado(pXmlDatosEmpleado, pClUsuario, pNbPrograma));
        }

    }
}