using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.AccesoDatos.Implementaciones.Administracion
{
    public class CatalogoVistaInteligenteOperaciones
    {
        private SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_C_CONSULTA_INTELIGENTE_Result> ObtieneConsultaIntligente(int? idCubo,int? idArchivo,string nbArchivo,string nbCatalogo, Guid? idItem)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_C_CONSULTA_INTELIGENTE(idCubo,idArchivo,nbArchivo,nbCatalogo,idItem).ToList();
            }
        }

        public void InsertaConsultaInteligente(int idArchivo,string nbArchivo, byte[] fiArchivo, string nbCatalogo, Guid? idItem, string usuario, string programa, int Eliminar)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                contexto.SPE_INSERTA_C_CONSULTA_INTELIGENTE(idArchivo,nbArchivo,fiArchivo,nbCatalogo,idItem,usuario,null,programa, null,Eliminar);
            }
        }
    }
}
