﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.Externas;

namespace SIGE.AccesoDatos.Implementaciones.Administracion
{
  public  class CentroAdministrativoOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS C_CENTRO_ADMVO
        public List<SPE_OBTIENE_CENTROS_ADMVOS_Result> Obtener_C_CENTRO_ADMVO(Guid? ID_CENTRO_ADMVO = null, String CL_CLIENTE = null, Guid? ID_REGISTRO_PATRONAL = null, String CL_CENTRO_ADMVO = null, String NB_CENTRO_ADMVO = null, String NB_CALLE = null, String NB_NO_EXTERIOR = null, String NB_NO_INTERIOR = null, String NB_COLONIA = null, String CL_MUNICIPIO = null, String NB_MUNICIPIO = null, String CL_ESTADO=null, String NB_ESTADO=null, String CL_CODIGO_POSTAL= null, String CL_ZONA_ECONOMICA= null, DateTime? FE_CREACION= null,DateTime? FE_MODIFICACION=null, String CL_USUARIO_CREA_APP=null, String CL_USUARIO_MODIFICA_APP= null, String NB_PROGRAMA_CREA=null, String NB_PROGRAMA_MODIFICA=null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from vCCentroAdmvos in context.SPE_OBTIENE_CENTROS_ADMVOS(ID_CENTRO_ADMVO, NB_MUNICIPIO, CL_ESTADO, CL_CODIGO_POSTAL, CL_ZONA_ECONOMICA, CL_MUNICIPIO)
                        select vCCentroAdmvos;
                return q.ToList();
            }
        }
        #endregion
        #region INSERTA ACTUALIZA DATOS  C_CENTRO_ADMVO
        public XElement InsertarActualizarCCentroAdmvo(String pClTipoOperacion, E_CENTROS_ADMVOS vCCentroAdmvo, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_CENTRO_ADMVO(poutClaveRetorno, vCCentroAdmvo.ID_CENTRO_ADMVO, vCCentroAdmvo.CL_CLIENTE,vCCentroAdmvo.ID_REGISTRO_PATRONAL,vCCentroAdmvo.CL_CENTRO_ADMVO,vCCentroAdmvo.NB_CENTRO_ADMVO,vCCentroAdmvo.NB_CALLE,vCCentroAdmvo.NB_NO_EXTERIOR,vCCentroAdmvo.NB_NO_INTERIOR,vCCentroAdmvo.NB_COLONIA,vCCentroAdmvo.CL_MUNICIPIO,vCCentroAdmvo.NB_MUNICIPIO,vCCentroAdmvo.CL_ESTADO,vCCentroAdmvo.NB_ESTADO,vCCentroAdmvo.CL_CODIGO_POSTAL,vCCentroAdmvo.CL_ZONA_ECONOMICA, usuario, usuario, programa, programa, pClTipoOperacion.ToString());
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_CENTRO_ADMVO
        public XElement EliminarCCentroAdmvo(Guid pIdCentroAdmvo, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_CENTRO_ADMVO(poutClaveRetorno, pIdCentroAdmvo, usuario, programa);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }
        #endregion
    }
}


