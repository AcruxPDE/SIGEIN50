using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_DOCUMENTO
    {
        public Guid ID_ITEM { get; set; }
        public int? ID_ARCHIVO { get; set; }
        public int? ID_DOCUMENTO { get; set; }
        public string NB_DOCUMENTO { get; set; }
        public string CL_TIPO_DOCUMENTO { get; set; }
        public string ID_EMPLEADO { get; set; }
        public DateTime? FE_CREATED_DATE { get; set; }
        public byte[] FI_ARCHIVO { set; get; }
        public int? ID_PROCEDENCIA { get; set; }
        public string CL_PROCEDENCIA { get; set; }
        public string GetDocumentFileName()
        {
            return String.Format(@"{0:yyyyMMdd}{1}", this.FE_CREATED_DATE, this.ID_ITEM);
        }
    }
}
