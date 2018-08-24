using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIGE.Entidades;
//using SIGE.Entidades.Administracion;
using System.Data;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;

namespace SIGE.AccesoDatos.Implementaciones.Administracion
{
    public class AdscripcionOperaciones
    {

        private SistemaSigeinEntities context;

        public List<SPE_OBTIENE_ADSCRIPCION_PDE_Result> ObtieneAdscripciones()
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_ADSCRIPCION_PDE().ToList();
            }
        }

        public string SeleccionaAdscripcion()
        {

            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_VALIDACION_ADSCRIPCION().FirstOrDefault();
            }
        }
    }


}
