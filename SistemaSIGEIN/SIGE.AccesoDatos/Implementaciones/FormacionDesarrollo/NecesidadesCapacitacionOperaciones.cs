using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class NecesidadesCapacitacionOperaciones
    {
        SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_NECESIDADES_CAPACITACION_Result> obtenerNecesidadesCapacitacion(int? ID_PERIODO, int? ID_DEPARTAMENTO, string DS_PRIORIDADES, int? ID_ROL)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_NECESIDADES_CAPACITACION(ID_PERIODO, ID_DEPARTAMENTO, DS_PRIORIDADES, ID_ROL).ToList();
            }
        }

        public XElement InsertaProgramaDesdeDNC(string xmldatosDnc, string CL_USUARIO, string NB_PROGRAMA)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ///pout_clave_retorno.Value = "";
                //ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                contexto.SPE_INSERTA_PROGRAMA_DESDE_DNC(pout_clave_retorno, xmldatosDnc, CL_USUARIO, NB_PROGRAMA);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }

        public XElement ActualizaProgramaDesdeDNC(int ID_PROGRAMA, string XML_DATOS_DNC, string CL_USUARIO, string NB_PROGRAMA)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                contexto.SPE_ACTUALIZA_PROGRAMA_DESDE_DNC(pout_clave_retorno, ID_PROGRAMA, XML_DATOS_DNC, CL_USUARIO, NB_PROGRAMA);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement InsertaActualizaProgramaDesdeDNC(int? ID_PROGRAMA, string XML_DATOS_DNC, string CL_USUARIO, string NB_PROGRAMA)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                contexto.SPE_INSERTA_ACTUALIZA_PROGRAMA_DESDE_DNC(pout_clave_retorno, ID_PROGRAMA, XML_DATOS_DNC, CL_USUARIO, NB_PROGRAMA);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

    }
}
