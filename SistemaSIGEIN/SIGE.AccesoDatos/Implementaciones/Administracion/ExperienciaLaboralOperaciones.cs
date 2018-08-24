using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class ExperienciaLaboralOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  K_EXPERIENCIA_LABORAL
        public List<SPE_OBTIENE_K_EXPERIENCIA_LABORAL_Result> Obtener_K_EXPERIENCIA_LABORAL(int? ID_EXPERIENCIA_LABORAL = null, int? ID_CANDIDATO = null, int? ID_EMPLEADO = null, String NB_EMPRESA = null, String DS_DOMICILIO = null, String NB_GIRO = null, DateTime? FE_INICIO = null, DateTime? FE_FIN = null, String NB_PUESTO = null, String NB_FUNCION = null, String DS_FUNCIONES = null, Decimal? MN_PRIMER_SUELDO = null, Decimal? MN_ULTIMO_SUELDO = null, String CL_TIPO_CONTRATO = null, String CL_TIPO_CONTRATO_OTRO = null, String NO_TELEFONO_CONTACTO = null, String CL_CORREO_ELECTRONICO = null, String NB_CONTACTO = null, String NB_PUESTO_CONTACTO = null, bool? CL_INFORMACION_CONFIRMADA = null, String DS_COMENTARIOS = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_K_EXPERIENCIA_LABORAL in context.SPE_OBTIENE_K_EXPERIENCIA_LABORAL(ID_EXPERIENCIA_LABORAL, ID_CANDIDATO, ID_EMPLEADO, NB_EMPRESA, DS_DOMICILIO, NB_GIRO, FE_INICIO, FE_FIN, NB_PUESTO, NB_FUNCION, DS_FUNCIONES, MN_PRIMER_SUELDO, MN_ULTIMO_SUELDO, CL_TIPO_CONTRATO, CL_TIPO_CONTRATO_OTRO, NO_TELEFONO_CONTACTO, CL_CORREO_ELECTRONICO, NB_CONTACTO, NB_PUESTO_CONTACTO, CL_INFORMACION_CONFIRMADA, DS_COMENTARIOS)
                        select V_K_EXPERIENCIA_LABORAL;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  K_EXPERIENCIA_LABORAL
        public int InsertaActualiza_K_EXPERIENCIA_LABORAL(string usuario, string programa, string archivo)
        {
           using (context = new SistemaSigeinEntities())
            {

                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_INSERTA_ACTUALIZA_K_EXPERIENCIA_LABORAL(pout_clave_retorno,usuario, usuario, programa, programa, archivo);

                return Convert.ToInt32(pout_clave_retorno.Value); ;
            }
          
        }
       
        #endregion

        #region ELIMINA DATOS  K_EXPERIENCIA_LABORAL
        public int Elimina_K_EXPERIENCIA_LABORAL(int? ID_EXPERIENCIA_LABORAL = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_K_EXPERIENCIA_LABORAL(pout_clave_retorno, ID_EXPERIENCIA_LABORAL, usuario, programa);
                //regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
            }
        }
        #endregion

    }
}

