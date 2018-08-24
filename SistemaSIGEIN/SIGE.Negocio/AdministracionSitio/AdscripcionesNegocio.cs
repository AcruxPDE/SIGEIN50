using SIGE.AccesoDatos.Implementaciones.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIGE.Entidades;
namespace SIGE.Negocio.AdministracionSitio
{
  public  class AdscripcionesNegocio
    {
      public List<SPE_OBTIENE_ADSCRIPCION_PDE_Result> ObtieneAdscripciones()
      {
          AdscripcionOperaciones operaciones = new AdscripcionOperaciones();
          return operaciones.ObtieneAdscripciones();

      }
      public string SeleccionaAdscripcion()
      {
          AdscripcionOperaciones operaciones = new AdscripcionOperaciones();
          return operaciones.SeleccionaAdscripcion().ToString();
      }
      
    }
    
}
