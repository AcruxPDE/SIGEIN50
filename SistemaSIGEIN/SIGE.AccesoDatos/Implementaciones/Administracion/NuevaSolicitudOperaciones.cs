using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{
   
    public class NuevaSolicitudOperaciones
    {

		private SistemaSigeinEntities context;		

		#region OBTIENE DATOS  C_CANDIDATO
		public List<SPE_OBTIENE_C_CANDIDATO_Result> Obtener_C_CANDIDATO(int?  ID_CANDIDATO = null,String NB_CANDIDATO = null,String NB_APELLIDO_PATERNO = null,String NB_APELLIDO_MATERNO = null,String CL_GENERO = null,String CL_RFC = null,String CL_CURP = null,String CL_ESTADO_CIVIL = null,String NB_CONYUGUE = null,String CL_NSS = null,String CL_TIPO_SANGUINEO = null,String NB_PAIS = null,String NB_ESTADO = null,String NB_MUNICIPIO = null,String NB_COLONIA = null,String NB_CALLE = null,String NO_INTERIOR = null,String NO_EXTERIOR = null,String CL_CODIGO_POSTAL = null,String CL_CORREO_ELECTRONICO = null,DateTime?  FE_NACIMIENTO = null,String DS_LUGAR_NACIMIENTO = null,Decimal?  MN_SUELDO = null,String CL_NACIONALIDAD = null,String DS_NACIONALIDAD = null,String NB_LICENCIA = null,String DS_VEHICULO = null,String CL_CARTILLA_MILITAR = null,String CL_CEDULA_PROFESIONAL = null,String XML_TELEFONOS = null,String XML_INGRESOS = null,String XML_EGRESOS = null,String XML_PATRIMONIO = null,String DS_DISPONIBILIDAD = null,String CL_DISPONIBILIDAD_VIAJE = null,String XML_PERFIL_RED_SOCIAL = null,String DS_COMENTARIO = null,bool?  FG_ACTIVO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
		{
			using (context = new SistemaSigeinEntities ())
			{
				var q = from V_C_CANDIDATO in context.SPE_OBTIENE_C_CANDIDATO(ID_CANDIDATO,NB_CANDIDATO,NB_APELLIDO_PATERNO,NB_APELLIDO_MATERNO,CL_GENERO,CL_RFC,CL_CURP,CL_ESTADO_CIVIL,NB_CONYUGUE,CL_NSS,CL_TIPO_SANGUINEO,NB_PAIS,NB_ESTADO,NB_MUNICIPIO,NB_COLONIA,NB_CALLE,NO_INTERIOR,NO_EXTERIOR,CL_CODIGO_POSTAL,CL_CORREO_ELECTRONICO,FE_NACIMIENTO,DS_LUGAR_NACIMIENTO,MN_SUELDO,CL_NACIONALIDAD,DS_NACIONALIDAD,NB_LICENCIA,DS_VEHICULO,CL_CARTILLA_MILITAR,CL_CEDULA_PROFESIONAL,XML_TELEFONOS,XML_INGRESOS,XML_EGRESOS,XML_PATRIMONIO,DS_DISPONIBILIDAD,CL_DISPONIBILIDAD_VIAJE,XML_PERFIL_RED_SOCIAL,DS_COMENTARIO,FG_ACTIVO, null)
				select V_C_CANDIDATO;
				return q.ToList();
			}
		}
		#endregion

		#region INSERTA ACTUALIZA DATOS  C_CANDIDATO
		public int InsertaActualiza_C_CANDIDATO(string tipo_transaccion, SPE_OBTIENE_C_CANDIDATO_Result V_C_CANDIDATO,string usuario, string programa)
		{
			using (context = new SistemaSigeinEntities ())
			{
				//Declaramos el objeto de valor de retorno
				ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
				context.SPE_INSERTA_ACTUALIZA_C_CANDIDATO(pout_clave_retorno,  V_C_CANDIDATO.ID_CANDIDATO,V_C_CANDIDATO.NB_CANDIDATO,V_C_CANDIDATO.NB_APELLIDO_PATERNO,V_C_CANDIDATO.NB_APELLIDO_MATERNO,V_C_CANDIDATO.CL_GENERO,V_C_CANDIDATO.CL_RFC,V_C_CANDIDATO.CL_CURP,V_C_CANDIDATO.CL_ESTADO_CIVIL,V_C_CANDIDATO.NB_CONYUGUE,V_C_CANDIDATO.CL_NSS,V_C_CANDIDATO.CL_TIPO_SANGUINEO,V_C_CANDIDATO.NB_PAIS,V_C_CANDIDATO.NB_ESTADO,V_C_CANDIDATO.NB_MUNICIPIO,V_C_CANDIDATO.NB_COLONIA,V_C_CANDIDATO.NB_CALLE,V_C_CANDIDATO.NO_INTERIOR,V_C_CANDIDATO.NO_EXTERIOR,V_C_CANDIDATO.CL_CODIGO_POSTAL,V_C_CANDIDATO.CL_CORREO_ELECTRONICO,V_C_CANDIDATO.FE_NACIMIENTO,V_C_CANDIDATO.DS_LUGAR_NACIMIENTO,V_C_CANDIDATO.MN_SUELDO,V_C_CANDIDATO.CL_NACIONALIDAD,V_C_CANDIDATO.DS_NACIONALIDAD,V_C_CANDIDATO.NB_LICENCIA,V_C_CANDIDATO.DS_VEHICULO,V_C_CANDIDATO.CL_CARTILLA_MILITAR,V_C_CANDIDATO.CL_CEDULA_PROFESIONAL,V_C_CANDIDATO.XML_TELEFONOS,V_C_CANDIDATO.XML_INGRESOS,V_C_CANDIDATO.XML_EGRESOS,V_C_CANDIDATO.XML_PATRIMONIO,V_C_CANDIDATO.DS_DISPONIBILIDAD,V_C_CANDIDATO.CL_DISPONIBILIDAD_VIAJE,V_C_CANDIDATO.XML_PERFIL_RED_SOCIAL,V_C_CANDIDATO.DS_COMENTARIO,V_C_CANDIDATO.FG_ACTIVO,usuario,usuario,programa, programa, tipo_transaccion);
				//regresamos el valor de retorno de sql
		                return Convert.ToInt32(pout_clave_retorno.Value); ; 
			}
		}
		#endregion

		#region ELIMINA DATOS  C_CANDIDATO
		public int Elimina_C_CANDIDATO(SPE_OBTIENE_C_CANDIDATO_Result V_C_CANDIDATO, string usuario  = null, string programa  = null)
		{
			using (context = new SistemaSigeinEntities ())
			{
				//Declaramos el objeto de valor de retorno
				ObjectParameter pout_clave_retorno = new ObjectParameter("POUT_CLAVE_RETORNO", typeof(int));
                context.SPE_ELIMINA_C_CANDIDATO(pout_clave_retorno, V_C_CANDIDATO.ID_CANDIDATO, usuario, programa);
				//regresamos el valor de retorno de sql				
                return Convert.ToInt32(pout_clave_retorno.Value);
			}
		}
		#endregion

	}
}