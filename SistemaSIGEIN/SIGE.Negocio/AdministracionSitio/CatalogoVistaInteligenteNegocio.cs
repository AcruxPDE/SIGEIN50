using SIGE.AccesoDatos.Implementaciones.Administracion;
using SIGE.Entidades;
using SIGE.Entidades.Modulos_de_apoyo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.AdministracionSitio
{
    public class CatalogoVistaInteligenteNegocio
    {
        public List<E_OBTIENE_C_CONSULTA_INTELIGENTE> ObtieneConsultaIntligente(int? idCubo, int? idArchivo, string nbArchivo, string nbCatalogo, Guid? idItem)
        {
            CatalogoVistaInteligenteOperaciones vistaInteligente = new CatalogoVistaInteligenteOperaciones();
            var vConsultaInteligente = vistaInteligente.ObtieneConsultaIntligente(idCubo, idArchivo, nbArchivo, nbCatalogo, idItem).ToList();
            return (from x in vConsultaInteligente
                    select new E_OBTIENE_C_CONSULTA_INTELIGENTE
                    {
                        ID_CUBO = x.ID_CUBO,
                        NB_ARCHIVO = x.NB_ARCHIVO,
                        NB_CATALOGO = x.NB_CATALOGO, 
                        ID_ARCHIVO = x.ID_ARCHIVO,
                        ID_ITEM = x.ID_ITEM,
                        FI_ARCHIVO = x.FI_ARCHIVO
                    }).ToList();            
        }

        public void InsertaConsultaInteligente(int idArchivo,string nbArchivo, byte[] fiArchivo, string nbCatalogo, Guid? idItem, string usuario, string programa, int Eliminar)
        {       
           CatalogoVistaInteligenteOperaciones vistaInteligente = new CatalogoVistaInteligenteOperaciones(); 
           vistaInteligente.InsertaConsultaInteligente(idArchivo,nbArchivo,fiArchivo,nbCatalogo,idItem,usuario,programa,Eliminar);            
        }
    }
}
